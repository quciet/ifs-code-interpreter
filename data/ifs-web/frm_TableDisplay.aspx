<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_TableDisplay" Codebehind="frm_TableDisplay.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD runat="server">
		<title>IFsWeb</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
         <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
         <%-- freezing grid col and rows headers  >--%>
   <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>--%>

         <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.9.0/jquery.min.js")%>"></script>
         <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.9.1/jquery-ui.min.js")%>"></script>


    <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
    <link href="Scripts/GridviewScroll.css" rel="stylesheet" />

	<%--	<style type="text/css">
       
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
     </style>--%>
    <!----------------------------------------------------------->
	
	</HEAD>
	<body BgColor="white" LeftMargin="0" RightMargin="0" topmargin="0" onload="javascript:gridviewScroll0(); ">
		 <script type="text/javascript">        
             // Defining event listener function
             function gridviewScroll0() {
                 var grid = this.document.getElementById("GridView1");
                 if (!grid) {
                      return;
                 }
                 var tw2 = Math.min(document.documentElement.clientWidth - 30, grid.offsetWidth + 30);
                     gridView1 = $('#GridView1').gridviewScroll({
                     width: tw2,
                     height: 680,
                     railcolor: "#F0F0F0",
                     barcolor: "#CDCDCD",
                     barhovercolor: "#606060",
                     bgcolor: "#F0F0F0",
                     freezesize: 1,
                     arrowsize: 30,
                     varrowtopimg: "Images/arrowvt.png",
                     varrowbottomimg: "Images/arrowvb.png",
                     harrowleftimg: "Images/arrowhl.png",
                     harrowrightimg: "Images/arrowhr.png",
                     //headerrowcount: 2,
                     railsize: 16,
                     barsize: 8
                 });
             }
             // Attaching the event listener function to window's resize event
             window.addEventListener("resize", gridviewScroll0);
             // Calling the function for the first time
             gridviewScroll0();
    </script>
        <form runat="server">
            <asp:ScriptManager EnablePageMethods="true" EnablePartialRendering="true" ID="MainSM" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="true"></asp:ScriptManager>
            <!--JAM 2013-04-09-->
            <input id="txtTableCols" name="txtTableCols" type="hidden" value="2" runat="server" />
            <div id="firstloading" style="position:absolute; width:100%; text-align:center; top:300px; visibility:hidden;"><img id="loadimg" src="images/squares-circle.gif"  height="93" width="93"><br>Loading resources...</div>

            <asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/14-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell  Text="Table Display" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<cswm:WebMenu id="cswmWebMenu" style="Z-INDEX: 110; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:WebMenu>
			<!-------------------JAM 2012-04-04------------------------->	
			 
                <%--Wrapper Div which will scroll the GridView--%>
                <div id = "AdjResultsDiv">
                
                    <asp:GridView ID="GridView1" runat="server" 
                        GridLines  ="Both"    Width ="90%"  CellPadding="6" ShowHeader="false">
                       <RowStyle CssClass="GridviewScrollItem" HorizontalAlign="Right" VerticalAlign="Middle"/>
                        
                        <%--<HeaderStyle  CssClass="GridViewHeaderStyle" />--%>
                    </asp:GridView>
                    
                </div>
           <!-------------------------------------------------------------->	
           
			<asp:Table id="grd_TableDisplay" runat="server" Height="224px" Width="448px" BorderStyle="Outset"
				BorderColor="DarkGray"  table-layout="fixed"></asp:Table>
				
				<asp:Panel runat="server" Width="3000" Height="1720" STYLE="Z-INDEX:100; LEFT:0px; POSITION:absolute; TOP:0px"
				TabIndex="7" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" Enabled="True" ID="frm_RankYear" BackColor="White">
                    <asp:TextBox ID="txtRank" runat="server"></asp:TextBox><asp:Button ID="cmdRank"
                    runat="server" Text="Submit" /><br />
                    <asp:DropDownList id="cbo_RY" tabIndex="8" runat="server" Width="153" BackColor="Window"
					Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
					Font-Size="8.0" Font-Name="Arial" Visible="True" AutoPostBack="True" ForeColor="WindowText">
                    </asp:DropDownList>&nbsp;
                </asp:Panel>
                
				<asp:Panel runat="server" Width="3000" Height="1720" STYLE="Z-INDEX:101; LEFT:0px; POSITION:absolute; TOP:0px"
				TabIndex="7" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" Enabled="True" ID="frm_MovingAvg" BackColor="White">
                    <asp:TextBox id="txtMovAvgYrs" tabIndex="8" runat="server" Height="24px" Width="153" BackColor="Window"
					Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
					Font-Size="8.0" Font-Name="Arial" Visible="True" ForeColor="WindowText">5</asp:TextBox>&nbsp;
					<asp:Button ID="cmdMovAvgYrsOk" runat=server text="Ok" Width=75px/>
                </asp:Panel>
                <asp:Panel runat="server" Width="3000" Height="1720" STYLE="Z-INDEX:108; LEFT:0px; POSITION:absolute; TOP:0px"
				TabIndex="7" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" Enabled="True" ID="frm_CumDisRate" BackColor="White">
                    <asp:TextBox ID="txtCumDisRate" runat="server" BackColor="Window" Enabled="True"
                        Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Size="8.0" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="WindowText" Height="24px" TabIndex="8" Visible="True"
                        Width="153">3</asp:TextBox>&nbsp;
                    <asp:Button ID="cmdCumDisRateOk" runat=server text="Ok" Width=75px/>
                </asp:Panel>
                <asp:Panel runat="server" Width="3000" Height="1720" STYLE="Z-INDEX:108; LEFT:0px; POSITION:absolute; TOP:0px"
				TabIndex="7" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" Enabled="True" ID="frm_DefineUnits" BackColor="White">
                    <br />
                    &nbsp;&nbsp;&nbsp;<b>Please enter the name of the units you'd like to use: </b>&nbsp;<asp:TextBox ID="txtUnitName" runat="server" BackColor="Window" Enabled="True"
                        Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Size="8.0" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="WindowText" Height="24px" TabIndex="8" Visible="True"
                        Width="153">Unit Name</asp:TextBox><br /><br />
                    &nbsp;&nbsp;&nbsp;<b>Please enter the conversion factor: </b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtUnitFactor" runat="server" BackColor="Window" Enabled="True"
                        Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Size="8.0" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="WindowText" Height="24px" TabIndex="8" Visible="True"
                        Width="153">1</asp:TextBox><br /><br />
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="cmdDefineUnits" runat=server text="Ok" Width=75px/>
                </asp:Panel>

                <asp:Panel runat="server" Width="3000" Height="1720" STYLE="Z-INDEX:108; LEFT:0px; POSITION:absolute; TOP:0px"
				TabIndex="7" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" Enabled="True" ID="frm_Interval" BackColor="White">
                    <asp:TextBox ID="txtInterval" runat="server" BackColor="Window" Enabled="True"
                        Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Size="8.0" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="WindowText" Height="24px" TabIndex="8" Visible="True"
                        Width="153">1</asp:TextBox>&nbsp;
                    <asp:Button ID="cmdInterval" runat=server text="Ok" Width=75px/>
                </asp:Panel>

			<asp:Panel runat="server" Width="3000" Height="1720" STYLE="Z-INDEX:103; LEFT:0px; POSITION:absolute; TOP:0px"
				TabIndex="7" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" Enabled="True" ID="frm_Horizon" BackColor="White">
				<asp:TextBox ID="txtHorizon" runat="server"></asp:TextBox><asp:Button ID="cmdHorizon"
                    runat="server" Text="Submit" /><br />
				<asp:DropDownList id="cmb_Horizon" tabIndex="8" runat="server" Width="153" BackColor="Window"
					Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
					Font-Size="8.0" Font-Name="Arial" Visible="True" AutoPostBack="True" ForeColor="WindowText"></asp:DropDownList>
			</asp:Panel>
			<asp:Panel runat="server" Width="3000" Height="1720" STYLE="Z-INDEX:104; LEFT:0px; POSITION:absolute; TOP:0px"
				TabIndex="7" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" Enabled="True" ID="frm_FY" BackColor="White">
				<asp:TextBox ID="txtFY" runat="server"></asp:TextBox><asp:Button ID="cmdFY"
                    runat="server" Text="Submit" /><br />
				<asp:DropDownList id="cmb_FY" tabIndex="8" runat="server" Width="153" BackColor="Window"
					Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
					Font-Size="8.0" Font-Name="Arial" Visible="True" AutoPostBack="True" ForeColor="WindowText"></asp:DropDownList>&nbsp;
			</asp:Panel>
			<asp:Button runat="server" STYLE="Z-INDEX:105; LEFT:88px; POSITION:absolute; TOP:304px; TEXT-ALIGN:center"
				Text="Previous" Width="169" Height="33" TabIndex="6" Visible="False" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
				CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmdPrevious"></asp:Button>
			<asp:Button runat="server" STYLE="Z-INDEX:106; LEFT:56px; POSITION:absolute; TOP:304px; TEXT-ALIGN:center"
				Text="Next" Width="169" Height="33" TabIndex="5" Visible="False" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
				CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmdNext"></asp:Button>
			<asp:Button runat="server" STYLE="Z-INDEX:107; LEFT:16px; POSITION:absolute; TOP:304px; TEXT-ALIGN:center"
				Text="Continue" Width="169" Height="33" TabIndex="4" Visible="False" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
				CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmdContinue"></asp:Button>
           <div>
                       
        </div>

			<script language="javascript">
{
        if ((document.forms[0].txtHorizon != null) && (document.forms[0].txtHorizon.disabled == false))
		{
			document.forms[0].txtHorizon.focus();
		}
        if ((document.forms[0].txtRank != null) && (document.forms[0].txtRank.disabled == false))
		{
			document.forms[0].txtRank.focus();
		}
        if ((document.forms[0].txtMovAvgYrs != null) && (document.forms[0].txtMovAvgYrs.disabled == false))
		{
			document.forms[0].txtMovAvgYrs.focus();
		}
        if ((document.forms[0].txtCumDisRate != null) && (document.forms[0].txtCumDisRate.disabled == false))
		{
			document.forms[0].txtCumDisRate.focus();
		}
        if ((document.forms[0].txtInterval != null) && (document.forms[0].txtInterval.disabled == false))
		{
			document.forms[0].txtInterval.focus();
		}
        if ((document.forms[0].txtFY != null) && (document.forms[0].txtFY.disabled == false))
		{
			document.forms[0].txtFY.focus();
		}

	    //jrs 2012/01/16
        function clickButton(e, buttonid){
              var evt = e ? e : window.event;
              var bt = document.getElementById(buttonid);
              if (bt){
                  if (evt.keyCode == 13){
                        bt.click();
                        return false;
                  }
              }
        }				
}
			</script>
			<aiswc:FocusControl ID="WinFormsToWeb_FocusControl" runat="server" style="Z-INDEX: 112; LEFT: 568px; POSITION: absolute; TOP: 88px"></aiswc:FocusControl>
            &nbsp;
            
            
            <script type="text/javascript">
            /*******************JAM 2012-04-05******************/
              var myWidth = 0, myHeight = 0;
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

            myWidth = myWidth * 0.9;
            myHeight = myHeight * 0.9;
		  
		
           
             
             
            this.document.getElementById("AdjResultsDiv").setAttribute("width", myWidth);
            this.document.getElementById("AdjResultsDiv").setAttribute("height", myHeight);
            
            
           
            
            
            
            /**********************************/
			</script>

            <!--JAM 2013-04-09-->
            <script language="javascript" type ="text/javascript" >
                function showhg() {
                    var doc = document.forms[0];

                    document.getElementById('AdjResultsDiv').style.visibility = "hidden";

                    var imgurl = document.getElementById('loadimg').src;
                    setTimeout(function () {
                        document.getElementById('loadimg').src = imgurl;
                    }, 1);
                    document.getElementById('firstloading').style.visibility = "visible";

                    document.body.style.cursor = "wait";
                    //doc.cmd_Exit.value = "Please Wait....";
                    //doc.cmd_Exit.style.cursor = "wait";

                }
               
                function gcopy(text) {
                        window.focus();
                        navigator.clipboard.writeText(text)
                        .then(() => {
                            alert('Table has been copied to clipboard.');
                        })
                        .catch(err => {
                            // This can happen if the user denies clipboard permissions:
                            alert('Could not copy : ' + err);
                        });
                }
            
                  
                function replaceAll(string, search, replace) {
                    return string.split(search).join(replace);
                }
                function tablecopy() {
                    var oTable = document.getElementById('GridView1');
                    //gets rows of table
                    var rowLength = oTable.rows.length;
                    var tx = '';
                    //loops through rows    
                    for (i = 0; i < rowLength; i++) {
                        if (i === 0) {
                            //Header Row------------------------------------
                            var oCells = oTable.rows.item(i).cells;
                            //gets amount of cells of current row
                            var cellLength = oCells.length;
                            var rt = '';
                            //loops through each cell in current row
                            var maxDetailRows = 0;
                            for (var j = 0; j < cellLength; j++) {
                                var cellVal = oCells.item(j).innerHTML;
                                cellVal = replaceAll(cellVal, '<div class="GridCellDiv">', '');
                                cellVal = replaceAll(cellVal, '</div>', '');
                                cellVal = replaceAll(cellVal, '&nbsp;', '');
                                cellVal = replaceAll(cellVal, '<br>', '|');
                                maxDetailRows = Math.max(maxDetailRows, cellVal.split("|").length);
                            }
                            //ExportsGoods	  ExportsGoods
                            //Australia	      Australia 
                            //Argentina	      Brazil

                            for (var k = 0; k < maxDetailRows; k++) {
                                var rowct = '';
                                for (var j = 0; j < cellLength; j++) {
                                    var cellVal = oCells.item(j).innerHTML;
                                    cellVal = replaceAll(cellVal, '<div class="GridCellDiv">', '');
                                    cellVal = replaceAll(cellVal, '</div>', '');
                                    cellVal = replaceAll(cellVal, '&nbsp;', '');
                                    cellVal = replaceAll(cellVal, '<br>', '|');
                                    if (cellVal.split("|").length >= k + 1) {
                                        rowct = rowct + cellVal.split("|")[k] + (j == cellLength - 1 ? '' : '\t');
                                    }
                                    else {
                                        rowct = rowct + (j == cellLength - 1 ? '' : '\t');
                                    }
                                }
                                tx = tx + rowct + '\r\n';
                            }

                        }
                        else {
                            //data rows----------------------------------------------------
                            //gets cells of current row  
                            var oCells = oTable.rows.item(i).cells;

                            //gets amount of cells of current row
                            var cellLength = oCells.length;
                            var rt = '';
                            //loops through each cell in current row
                            for (var j = 0; j < cellLength; j++) {

                                // get your cell info here

                                var cellVal = oCells.item(j).textContent;
                                var et = j === cellLength - 1 ? '' : '\t';
                                rt = rt + cellVal + et;
                            }
                            var er = i === rowLength - 1 ? '' : '\r\n';
                            tx = tx + rt + er;
                        }

                    }

                    gcopy(tx);
                }

                //jrs 2023/05/15
                function transtablecopy() {

                    var oTable = document.getElementById('GridView1');
                    //Header Row------------------------------------
                    var oCells = oTable.rows.item(0).cells;
                    //gets amount of cells 
                    var cellLength = oCells.length;
                    //gets rows of table
                    var rowLength = oTable.rows.length;
                    var tx = '';
                    //loops through columns 
                    for (j = 0; j < cellLength; j++) {
                        if (j === 0) {
                            var rt = '';
                            //loops through each row in current column
                            var maxDetailRows = 0;
                            for (var i = 0; i < rowLength; i++) {
                                //using column=1 where the labels show up for the first time
                                var cellVal = oTable.rows.item(i).cells.item(1).innerHTML;
                                cellVal = replaceAll(cellVal, '<div class="GridCellDiv">', '');
                                cellVal = replaceAll(cellVal, '<div class="GridCellDiv" style="width: 32px;">', '');
                                cellVal = replaceAll(cellVal, '</div>', '');
                                cellVal = replaceAll(cellVal, '&nbsp;', '');
                                cellVal = replaceAll(cellVal, '<br>', '|');
                                maxDetailRows = Math.max(maxDetailRows, cellVal.split("|").length);
                            }
                            //ExportsGoods	  ExportsGoods
                            //Australia	      Australia 
                            //Argentina	      Brazil

                            for (var k = 0; k < maxDetailRows; k++) {
                                tx = tx + '\t';
                            }

                            var rowct = '';
                            for (var i = 1; i < rowLength; i++) {
                                var cellVal = oTable.rows.item(i).cells.item(j).innerHTML;
                                cellVal = replaceAll(cellVal, '<div class="GridCellDiv">', '');
                                cellVal = replaceAll(cellVal, '<div class="GridCellDiv" style="width: 32px;">', '');
                                cellVal = replaceAll(cellVal, '</div>', '');
                                cellVal = replaceAll(cellVal, '&nbsp;', '');
                                cellVal = replaceAll(cellVal, '<br>', '|');
                                rowct = rowct + cellVal + '\t';
                            }
                            tx = tx + rowct + '\r\n';
                        }
                        else {
                            //data columns----------------------------------------------------

                            var rt = '';
                            //loops through each cell in current column
                            for (var i = 0; i < rowLength; i++) {
                                // get your cell info here
                                var cellVal = oTable.rows.item(i).cells.item(j).textContent;
                                var et = i === rowLength - 1 ? '' : '\t';
                                if (i == 0) {
                                    cellVal = oTable.rows.item(i).cells.item(j).innerHTML;
                                    cellVal = replaceAll(cellVal, '<div class="GridCellDiv">', '');
                                    cellVal = replaceAll(cellVal, '<div class="GridCellDiv" style="width: 32px;">', '');
                                    cellVal = replaceAll(cellVal, '</div>', '');
                                    cellVal = replaceAll(cellVal, '&nbsp;', '');
                                    cellVal = replaceAll(cellVal, '<br>', '|');
                                    for (var k = 0; k < maxDetailRows; k++) {
                                        if (cellVal.split("|").length >= k + 1) {
                                            rt = rt + cellVal.split("|")[k] + '\t';
                                        }
                                        else {
                                            rt = rt + '\t';
                                        }
                                    }
                                }
                                else {
                                    rt = rt + cellVal + et;
                                }
                                var er = i === rowLength - 1 ? '\r\n' : '';
                                rt = rt + er;
                            }
                            tx = tx + rt;
                        }

                    }

                    gcopy(tx);
                }
            </script>
                         
		</form>
       
           
	</body>
</HTML>
