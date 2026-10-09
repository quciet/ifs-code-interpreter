<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_Drivers" Codebehind="frm_Drivers.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Causal Linkages, Forward and Backward.</title>
 
  <style id="compiled-css" type="text/css">
      .highcharts-figure, .highcharts-data-table table {
      margin: 2em auto;
}
#container {
    margin: 1em auto;
    padding: 2em ;
}
.highcharts-data-table table {
	font-family: Verdana, sans-serif;
	border-collapse: collapse;
	border: 1px solid #EBEBEB;
	margin: 10px auto;
    padding: 2em ;
	text-align: center;
	width: 100%;
    height:100%;
}
.highcharts-data-table caption {
    padding: 2em 0;
    font-size: 2.4em;
    color: #555;
}
      .highcharts-data-table th {
          font-weight: 900;
          padding: 0.5em;
      }

      .highcharts-data-table td, .highcharts-data-table th, .highcharts-data-table caption {
          padding: 0.5em;
      }

      .highcharts-data-table thead tr, .highcharts-data-table tr:nth-child(even) {
          background: #f8f8f8;
      }

      .highcharts-data-table tr:hover {
          background: #f1f7ff;
      }

    /* EOS */
  </style>

</head>
<body leftMargin="0" topMargin="0" rightMargin="0" >
      <form id="Form1" method="post" runat="server">
   <asp:ScriptManager EnablePageMethods="true" ID="MainSM" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="true"></asp:ScriptManager>
          <!--jam 2021/11/27-->
  <%--<script src="https://code.highcharts.com/highcharts.js"></script>
<script src="https://code.highcharts.com/modules/networkgraph.js"></script>
<script src="https://code.highcharts.com/modules/exporting.js"></script>
<script src="http://blacklabel.github.io/custom_events/js/customEvents.js"></script>--%>
  <!--jam 2022/05/11-->
<script src="<%= Page.ResolveUrl("~/Scripts/drivers/highcharts.js")%>"></script>
 <script src="<%= Page.ResolveUrl("~/Scripts/drivers/networkgraph.js")%>"></script>
 <script src="<%= Page.ResolveUrl("~/Scripts/drivers/exporting.js")%>"></script>
 <script src="<%= Page.ResolveUrl("~/Scripts/drivers/customEvents.js")%>"></script>


    <table id="WinFormsToWeb_PageBar" cellspacing="0" border="0" style="height:15px;width:100%;border-collapse:collapse;">
	<tr id="TitleRow" style="color:White;background-color:activecaption;font-family:Tahoma;font-size:Medium;font-weight:bold;">
		<td id="LogoCell" style="width:40px;">
                <!--jam 2022/06/01 Fixing Logo calling-->
                <!--jam 2022/04/16-->
                <!--jam 2024/08/17 Bug fix related to GET parameters-->

<%--                <a href="https://korbel.du.edu/pardee"><img id="imgLogo" alt="IFs Home" title="IFs Home" src="~/Images/pcl.png" border="0" style="width:30px;" /></a>--%>

        <!-- jam 2024/10/28 fixing bug about loading logo error-->
       <a href="https://korbel.du.edu/pardee"><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a>

		</td>
                <td id="TitleCell">Causal Linkages, Forward and Backward (Double click on node to navigate)</td>
	
   
    
    </tr>
</table>
          <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#ffffff" border="0">
            <tr>
                <td>
                    <cswm:WebMenu ID="cswmWebMenu" style="z-index: 999; left: 0px; position: absolute; top: 16px"
                   AutoPostBack="True"      runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:WebMenu>
                </td>
            </tr>
            
        </table>
         
          
<figure class="highcharts-figure">
    <div runat="server" id="container" style="width:90%"></div>
          
       <div>
               <asp:Label ID="lblJS" runat="server"></asp:Label>
          </div>
</figure>
 
</form>
</body>
</html>
