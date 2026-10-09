<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_HealthMeassures" Codebehind="frm_HealthMeassures.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Mortality and Disability Years</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
		<script language="javascript" type="text/javascript" src="Scripts/FilesInOrder.js"></script>
        <!--jam 2021/11/27-->
       <%-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
         <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
        <%--<script src="Scripts/highcharts.js"></script>
        <script src="Scripts/exporting.js"></script>--%>

          <!--jam 2025/03/11 New highcharts version libraries-->
          <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts.js")%>"></script>
          <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/exporting.js")%>"></script>
   
          <!--jam 2025/03/11 New highcharts version libraries-->
          <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/offline-exporting.js")%>"></script>


<!---------------------JAM 2012-04-05------------------------>
		<style type="text/css">
       
       div#GridView1  {
        width: 900px;
        height: 550px;
        overflow: scroll;
        position: relative;
        }
       
       div#GridView1  th
        {  
            top: expression(document.getElementById("AdjResultsDiv").scrollTop-2);
            left: expression(parentNode.parentNode.parentNode.parentNode.scrollLeft);
            position: relative;
            z-index: 20;
        }  
      
      div#AdjResultsDiv {
         width: 90%; /*900px*/
         height: 85%; /*500px*/
        overflow: scroll;
        position: relative;
        }

        div#AdjResultsDiv th
        {  
        top: expression(document.getElementById("AdjResultsDiv").scrollTop-2);
        left: expression(parentNode.parentNode.parentNode.parentNode.scrollLeft);
        position: relative;
        z-index: 20;
        }

        .GridViewHeaderStyle
        {
           /* font-family:Verdana;*/
           /* font-size:10px;*/
	        /*color:White;*/
	        color:Black;
	        background: lightgray;
	         
	        /*background: gray url(Images/grid/header1.png) repeat-x;
	        line-height: 90px; */
	        
            }
     </style>
    <!----------------------------------------------------------->
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" onload="javascript:pageResize();">
		<form id="Form1" method="post" runat="server">
		    <input id="txtFiles" name="txtFiles"  type="hidden" runat="server"/>
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/14-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Mortality and Disability Years" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<table cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td><cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 999; LEFT: 0px; POSITION: absolute; TOP: 16px"
							runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu></td>
				</tr>
			</table>

            <!-------------------JAM 2012-04-04------------------------->				 
                <%--Wrapper Div which will scroll the GridView--%>
                <div id = "AdjResultsDiv">
                
                    <asp:GridView ID="GridView1" runat="server" Visible="false"
                        GridLines  ="Both"    Width ="100%"  CellPadding="6">
                       <HeaderStyle  CssClass="GridViewHeaderStyle" />
                    </asp:GridView>
                    
                </div>
           <!-------------------------------------------------------------->	

        <table>
            <asp:Panel ID="pnlMort" runat="server">
                <tr valign=top>
                    <td  width=40%>
			            <asp:label id="lblReg" runat="server"
				            Font-Bold="True" Width="448" Font-Names="Arial">Region:</asp:label>
                        <br />
			            <asp:listbox id="lstDim1" runat="server"
				            Height="457px" Width="456" SelectionMode="Multiple"></asp:listbox>	                    
                    </td>
    			
                    <td width=15%>
                        <asp:label id="Label2" runat="server"
				            Font-Bold="True" Font-Names="Arial">Mortality Types:</asp:label>
                        <br />
			            <asp:listbox id="lstDim2" 
				            runat="server" Height="318" Width="168" SelectionMode="Multiple"></asp:listbox>
                        <br /><br />
			            <asp:label id="lblVars" runat="server"
				            Font-Bold="True" Font-Names="Arial">Variables:</asp:label>
                        <br />
			            <asp:listbox id="lstVars" 
				            runat="server" Height="106" Width="168" SelectionMode="Multiple"></asp:listbox>
                    </td>
			        
    				<td width=45%>
			            <asp:label id="Label3" runat="server"
				            Width="448" Font-Bold="True" Font-Names="Arial">Gender:</asp:label>				
                        <br />
			            <asp:listbox id="lstDim3" runat="server"
				            SelectionMode="Multiple" Height="106" Width="456"></asp:listbox>
                        <br /><br />
			            <asp:label id="lblFiles" runat="server"
				            Width="448" Font-Bold="True" Font-Names="Arial">Scenarios:</asp:label>				
                        <br />
			            <asp:listbox id="lstFiles" runat="server"
				            SelectionMode="Multiple" Height="406" Width="456"></asp:listbox>

                    </td>
                </tr>
            </asp:Panel>

            <tr>
                <td colspan=3 align=center style="width: 100%">
                    <%--jam 2025/03/11 New highcharts version libraries container width adjusted from 90% to 100%--%>
                    <%--<div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>--%>
                    <div id="container" style="height: 90%; width: 100%; top: 10px; left: 10px; position: relative;"></div>
                    <br />
                    <div><asp:Label ID="lblJS" runat="server" Visible="false"></asp:Label></div>                        
                </td>
            </tr>
            <tr>
                <td colspan=3 style="width: 100%">      
                   	<asp:Table id="grdHL" runat="server" BorderStyle="Outset"
				    BorderColor="DarkGray" Visible="false"></asp:Table>                    
                </td>
            </tr>
            <tr>
                <td colspan=3 align=center style="width: 100%">
                    <asp:Panel ID="pnlButtons" runat="server" Width="1080px"> 	    
			            <asp:button id="cmdDraw" style="Z-INDEX: 104; LEFT: 32px" tabIndex="2"
				            runat="server" Width="88px" Text="Graph"></asp:button>
			            <asp:button id="cmdTable" style="Z-INDEX: 107; LEFT: 136px" runat="server"
				            Width="88px" Text="Table"></asp:button>
			            <asp:button id="cmdClear" style="Z-INDEX: 102; LEFT: 240px"
				            tabIndex="3" runat="server" Width="88px" Text="Clear"></asp:button>
                    </asp:Panel>
                </td>
            </tr>

            <tr>
                <td colspan=3 style="width: 100%">
                    <asp:Panel ID="pnlAWBeta" runat="server" Visible="false">                       
                        <asp:label id="lblDef1" runat="server"
                                Font-Bold="True">Please enter a value for Beta in the Age Weighting Function CXe^(-Beta*X), C will be re-adjusted</asp:label>
                        <asp:TextBox id="txtAWBeta" tabindex="1" runat="server" Width="88px"></asp:TextBox><br />
                        
                        <asp:button id="cmdSaveAWBeta" style="Z-INDEX: 106; LEFT: 32px" tabIndex="1"
	                            runat="server" Width="88px" Text="Save" AccessKey="s"></asp:button>
                        <asp:button id="cmdCancelAWBeta" style="Z-INDEX: 107; LEFT: 136px" runat="server"
	                            Width="88px" Text="Cancel" AccessKey="c"></asp:button><br />
	                            
        	    		<asp:label id="lblMsg" style="Z-INDEX: 111; LEFT: 32px; POSITION: absolute; TOP: 588px" runat="server"
		    	    	Font-Bold="True" Width="424" Height="24px" Visible="False" ForeColor="Red" Font-Size="Large">Label</asp:label>
                    </asp:Panel>
                </td>
            </tr>

            <tr>
                <td colspan=3 style="width: 100%">
                    <asp:Panel ID="pnlDiscRt" runat="server" Visible="false">                       
                        <asp:label id="Label1" runat="server"
                                Font-Bold="True">Please enter Discount Rate in Percentage.</asp:label>
                        <asp:TextBox id="txtDiscRt" tabindex="1" runat="server" Width="88px"></asp:TextBox><br />
                        
                        <asp:button id="cmdSaveDiscRt" style="Z-INDEX: 106; LEFT: 32px" tabIndex="1"
	                            runat="server" Width="88px" Text="Save" AccessKey="s"></asp:button>
                        <asp:button id="cmdCancelDiscRt" style="Z-INDEX: 107; LEFT: 136px" runat="server"
	                            Width="88px" Text="Cancel" AccessKey="c"></asp:button><br />
	                            
        	    		<asp:label id="lblMsgDiscRt" style="Z-INDEX: 111; LEFT: 32px; POSITION: absolute; TOP: 588px" runat="server"
		    	    	Font-Bold="True" Width="424" Height="24px" Visible="False" ForeColor="Red" Font-Size="Large">Label</asp:label>
                    </asp:Panel>
                </td>
            </tr>

             <tr>
                <td colspan=3 style="width: 100%">
                    <asp:Panel ID="pnlEditLbl" runat="server" Visible="false">                       
                        <asp:label id="Label4" runat="server"
                                Font-Bold="True">Please select the label to edit.</asp:label><br />
                        <asp:TextBox id="txtEditLbl" tabindex="1" runat="server" Width="400px"></asp:TextBox>&nbsp; 
                        <asp:button id="cmdUpdEditLbl" style="Z-INDEX: 106; LEFT: 32px" tabIndex="1"
	                    runat="server" Width="88px" Text="Update" AccessKey="s"></asp:button><br />                        
                        <asp:ListBox ID="lstEditLbl" Width="400px" runat="server" AutoPostBack="True"></asp:ListBox><br />
                        
                        <asp:button id="cmdSaveEditLbl" style="Z-INDEX: 106; LEFT: 32px" tabIndex="1"
	                            runat="server" Width="88px" Text="Save" AccessKey="s"></asp:button>
                        <asp:button id="cmdCancelEditLbl" style="Z-INDEX: 107; LEFT: 136px" runat="server"
	                            Width="88px" Text="Cancel" AccessKey="c"></asp:button><br />	                                   	    		
                    </asp:Panel>
                </td>
            </tr>
        </table>			
				
		<script language="javascript">
                var myWidth = 0, myHeight = 0;

                function pageResize() {
                    if( typeof( window.innerWidth ) == 'number' ) {
                    /*Non-IE*/
                    myWidth = window.innerWidth;
                    myHeight = window.innerHeight;
                    } else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {
                    /*//IE 6+ in 'standards compliant mode*/
                    myWidth = document.documentElement.clientWidth;
                    myHeight = document.documentElement.clientHeight;
                    } else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {
                    /*//IE 4 compatible*/
                    myWidth = document.body.clientWidth;
                    myHeight = document.body.clientHeight;
                    }
                   if (myWidth == 0)
                    {
                    myWidth = screen.width;
                    myHeight = screen.height;
                    }

                    //jrs 2012/08/03 only if listboxs are visible
                    if (document.forms[0].lstDim1 != null) {
                        var doc = document.forms[0];
                        //pct of screen to be used
                        //myWidth = 0.9 * myWidth;
                        var f2 = 0.95;
                        var f = 1.025;
                        var h = myHeight * f2 - (20 + 64) - (24 + 20) * f;
                        if (h < 106 + 20) {
                            h = 106 + 20;
                        }
                        doc.lstDim1.style.height = h + 'px';

                        var h2 = h * 0.7;
                        doc.lstDim2.style.height = h2 + 'px';
                        var T = (92 + h2) * f;
                        document.getElementById('lblVars').style.top = T + 'px';
                        var tt = T + 20;
                        doc.lstVars.style.top = tt + 'px';

                        var t2 = (tt + 106 + 20) * f;
                        doc.cmdDraw.style.top = t2 + 'px';
                        doc.cmdTable.style.top = t2 + 'px';
                        doc.cmdClear.style.top = t2 + 'px';

                        t2 = (92 + 106) * f;
                        document.getElementById('lblFiles').style.top = t2 + 'px';
                        t2 = t2 + 20;
                        doc.lstFiles.style.top = t2 + 'px';
                        h2 = (h - 106 - 20 - 20);
                        doc.lstFiles.style.height = h2 + 'px';

                        this.document.getElementById("AdjResultsDiv").style.display = "none";
                    }
                    else if (document.getElementById("lblJS") != null)
                    {
                        this.document.getElementById("AdjResultsDiv").style.display = "none";
                    }
                    else if (this.document.getElementById("AdjResultsDiv") != null) {
                        var w = myWidth * 0.9;
                        var h = myHeight * 0.9;
                        this.document.getElementById("AdjResultsDiv").style.display = "block";
                        this.document.getElementById("AdjResultsDiv").setAttribute("width", w.toFixed(0) + 'px');
                        this.document.getElementById("AdjResultsDiv").setAttribute("height", h.toFixed(0) + 'px');
                    }
                }

                if ((document.forms[0].txtAWBeta != null) && (document.forms[0].txtAWBeta.disabled == false)) {
                    document.forms[0].txtAWBeta.focus();
                }
                if ((document.forms[0].txtDiscRt != null) && (document.forms[0].txtDiscRt.disabled == false)) {
                    document.forms[0].txtDiscRt.focus();
                }
                if ((document.forms[0].txtEditLbl != null) && (document.forms[0].txtEditLbl.disabled == false)) {
                    document.forms[0].txtEditLbl.focus();
                }

                //jrs 2012/01/16
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
		</script>
		</form>
	</body>
</HTML>

<script type="text/javascript">

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-3138457-1']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();

</script>