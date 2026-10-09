<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_ViewDataBatch" Codebehind="frm_ViewDataBatch.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>

    <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/highcharts.js")%>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/exporting.js")%>"></script>
    <link rel="icon" href="<%= Page.ResolveUrl("~/images/favicon.ico")%>">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title>Compare Imported Table with Existing Table</title>
</head>
<body leftmargin="0" topmargin="0" rightmargin="0">
    <style type="text/css">
    .wrap { white-space: normal; width: 100px; }
        .auto-style1 {
            width: 42%;
        }
    </style>
    <script type="text/javascript">

        function checkToImportYears() {
            var doc = document.forms[0];
            var msg = "This step is going to add or overwrite the selected columns in the NEW table using those in the CURRENT IFs table.";
            msg = msg + " Before proceeding with this step you must add Earliest and Most Recent Columns to the NEW table, if they are not already there.";
            msg = msg + " You cannot revert this step. Please save a copy of the import database if you need to. ";
            msg = msg + " Would you like to proceed?";

            var confirmedChanges = confirm(msg);

            if (confirmedChanges) {
                return true;
            }
            else {
                return false;
            }

        };

        function checkToImportCells() {
            var doc = document.forms[0];
            var msg = "This step is going to fill in data points missing in the NEW table from the CURRENT IFs table (for countries you have selected). ";
            msg = msg + " Before proceeding with this step you must add Earliest and Most Recent Columns to the NEW table if they are not already there.";
            msg = msg + " You cannot revert this step. Please save a copy of the import database if you need to.";
            msg = msg + " Would you like to proceed?";

            var confirmedChanges = confirm(msg);

            if (confirmedChanges) {
                return true;
            }
            else {
                return false;
            }

        };

        function checkMergeToMaster() {
            var doc = document.forms[0];
            var msg = "Imported tables will now be inserted/overwritten into IFs master database in the Data Folder."
            msg = msg + "  Master Data Dictionary will also be updated.";
            msg = msg + " Would you like to proceed?";

            var confirmedChanges = confirm(msg);

            if (confirmedChanges) {
                doc.txtConfirmMerge.value = 'True';
            }
            else {
                doc.txtConfirmMerge.value = 'False';
            }

        };
    </script> 
    <form id="frm_ViewDataBatch" runat="server">

        	<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                    <asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>			
					<asp:TableCell Text="Compare Imported Table with Existing Table" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 104; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>
                <asp:CheckBox ID="chkMarkAll" runat="server" Text="Select All Variables for Merging" Font-Bold="False" AutoPostBack="True" />
                <asp:CheckBox ID="chkResetFileWhenMerging" runat="server" Text="Reset File after Merging" Font-Bold="False" AutoPostBack="True" Checked="True" />
        
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="fuOpenFile" runat="server" />
                <asp:Button ID="cmdReloadFile" runat="server" Text="Reload File (20MB Max)" />

                <br /><asp:Label ID="lblNewImportedSeries" runat="server" Text="New Imported Series:" Font-Bold="True"></asp:Label>
        
        <div id="divNew" style="height:250px; overflow:scroll;" runat="server">               
            <asp:GridView ID="GridViewNew" runat="server" AllowSorting="True" AutoGenerateSelectButton="True" CaptionAlign="Left">
                    <Columns>
                            <asp:TemplateField >
                                 <ItemTemplate>
                                     <asp:CheckBox ID="myCheckBox" runat="server" />
                                 </ItemTemplate>
                             </asp:TemplateField>
                      </Columns>
                    <HeaderStyle Font-Bold="True" />
                    <SelectedRowStyle BackColor="#6699FF" />
                </asp:GridView>
        </div>

        <asp:Label ID="lblExistingData" runat="server" Text="Existing Data:" Font-Bold="True"></asp:Label>
        <div id="divExistingData" style="height:250px; overflow:scroll;" runat="server">
                <asp:GridView ID="dbgridexistingdata" runat="server" AllowSorting="True" >
                   <Columns>
                        <asp:TemplateField >
                             <ItemTemplate>
                                 <asp:CheckBox ID="myCheckBox" runat="server" />
                             </ItemTemplate>
                         </asp:TemplateField>
                  </Columns>
                    <HeaderStyle Font-Bold="True" />
                </asp:GridView>
        </div>
                <asp:Panel ID="Panel2" runat="server" BorderStyle="Groove">
                    <table>
                        <tr>
                            <td valign="top" width="20%">
    <asp:Panel ID="Panel1" runat="server" BorderStyle="Groove" Width="300px">
        <table>
            <tr>
                <td><asp:Button ID="cmdUpdateDatadict" runat="server" Text="View/Edit Data Dictionary For The New Table" Width="125px" /></td>
                <td>&nbsp;&nbsp;&nbsp;<asp:Button ID="cmdDeleteZero" runat="server" Text="Delete Zeroes" Width="125px" />
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="cmdPlot" runat="server" Text="Plot Selected" Width="125px" /></td>
                <td>&nbsp;&nbsp;&nbsp;<asp:Button ID="cmdDeleteNegative" runat="server" Text="Delete Negatives" Width="125px" /></td>
            </tr>
        </table>
        </asp:Panel>

                            </td>
                            <td valign="top" class="auto-style1">
<asp:Panel ID="pnlMarks" runat="server" BorderStyle="Groove" Width="550px">
                        Marking Threshold: <asp:TextBox ID="txtMarkThreshold" runat="server">10</asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="optPercent" runat="server" GroupName="Opt_Marking" checked="true" Text="Percent"/>
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="optUnit" runat="server" GroupName="Opt_Marking" Checked="false" Text="Units"/>
    <table>
        <tr>
            <td><asp:Button ID="cmdMarkTransientsNew" runat="server" Text="Mark Year to Year Jumps in New Data" Width="150px" Height="70px" /></td>
            <td><asp:Button ID="cmdMarkTransientsOld" runat="server" Width="150px" Height="70px"  
                            Text="Mark Year to Year Jumps in Current Data"/>
            </td>
        </tr>
        <tr>
            <td><asp:Button ID="cmdMarkAbove" runat="server" Text="Mark Points Above" Width="150px" Height="35px" />
                <br />
                <asp:Button ID="cmdMarkBelow" runat="server" Text="Mark Points Below" Width="150px" Height="35px" />
            </td>
            <td><asp:Button ID="cmdMarkBigChanges" runat="server" Text="Mark Differences between New and Old Data (for same country-year)" Width="150px" Height="70px"/></td>
        </tr>
    </table>
                        
                        
    
                    </asp:Panel>                    

                            </td>
                            <td valign="top" width="40%">
                                <asp:Panel ID="Panel3" runat="server" Width="371px" GroupingText="Identify Data Loss from Update" Font-Size="X-Small" BackColor="White">
                                    <asp:CheckBox ID="chkMarkNonZeroOnlysMissing" runat="server" Checked="True" Text="Mark Non-zero only" TextAlign="Left" />
                                    &nbsp&nbsp&nbsp<asp:Button ID="cmdDisplayMissing" runat="server" Text="Mark Missing" />
                                    &nbsp&nbsp&nbsp<asp:Button ID="cmdDisplayNew" runat="server" Text="Mark  New" />
                                </asp:Panel>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="cmdImportYears" runat="server" Text="Blend Columns (Years)" width="150px"/><br />
                                            <asp:CheckBox ID="chkSelYear" runat="server" Checked="False" Text="Select All" TextAlign="Left" />
                                            <div id="divYear" style="height:115px; overflow:scroll;" runat="server">
                                                <asp:GridView ID="grd_Year" runat="server" Height="150">
                                                   <Columns>
                                                        <asp:TemplateField >
                                                             <ItemTemplate>
                                                                 <asp:CheckBox ID="myCheckBox" runat="server" />
                                                             </ItemTemplate>
                                                         </asp:TemplateField>
                                                  </Columns>
                                                </asp:GridView>
                                            </div>
                                            <asp:CheckBox ID="ChkSelMissingYears" runat="server" Checked="False" Text="Select Missing Years" TextAlign="Left" />
                                        </td>
                                        <td>
                                            <asp:Button ID="cmdImportCells" runat="server" Text="Blend Data Points" width="150px"/><br />
                                            <asp:CheckBox ID="chkSelCountry" runat="server" Checked="False" Text="Select All" TextAlign="Left" />
                                            <div id="divCountry" style="height:115px; overflow:scroll;" runat="server">
                                                <asp:GridView ID="grd_Country" runat="server" Height="150">
                                                   <Columns>
                                                        <asp:TemplateField >
                                                             <ItemTemplate>
                                                                 <asp:CheckBox ID="myCheckBox" runat="server" />
                                                             </ItemTemplate>
                                                         </asp:TemplateField>
                                                  </Columns>
                                                </asp:GridView>
                                            </div>                                            
                                            <asp:CheckBox ID="chkSelMissingCountries" runat="server" Checked="False" Text="Select Countries With No Data" TextAlign="Left" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

            <asp:Label ID="lblNewDataSeries" runat="server" Text="New Data Series:" Font-Bold="True"></asp:Label>
            <div id="divNewData" style="height:250px; overflow:scroll;" runat="server">
                <asp:GridView ID="GridViewDet" runat="server" AllowSorting="True" AutoGenerateEditButton="True"
                    OnRowEditing="GridViewDet_RowEditing" DataKeyNames="Country" OnRowUpdating="GridViewDet_RowUpdating"
                    OnRowCancelingEdit="GridViewDet_RowCancelingEdit" > 
                   <Columns>

                        <asp:TemplateField >
                             <ItemTemplate>
                                 <asp:CheckBox ID="myCheckBox" runat="server" />
                             </ItemTemplate>
                         </asp:TemplateField>

                  </Columns>
                    <HeaderStyle Font-Bold="True" />
                </asp:GridView>
            </div>

                <div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>
                <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>
        
                <input id="txtConfirmMerge" name="txtConfirmMerge"  type="hidden" runat="server"  />
    </form>
</body>
</html>
