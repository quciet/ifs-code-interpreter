<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_BilateralInteractionGraphLabel" Codebehind="frm_BilateralInteractionGraphLabel.aspx.vb" %>

<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Bilateral Interaction</title>
    <script language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
    </script>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
     <!--jam 2021/11/27-->
  <%--  <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    --%>
    <script language="javascript" type="text/javascript" src="Scripts/FilesInOrder.js"></script>
    <!--jam 2021/11/27-->
    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
     <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
    <script src="Scripts/highcharts.js"></script>
    <script src="Scripts/exporting.js"></script>
    <%--VarSel================--%>
    <script language="javascript" type="text/javascript" src="Scripts/actb.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/common.js"></script>
        <script language ="javascript" type="text/javascript" src="Scripts/AjaxMethodsHandler.js"></script>
       

      <%--   <!--JAM 2014-04-04-->
        <script language ="javascript" type="text/javascript" src="Scripts/VarSel_Vars.js"></script>

        <!--JAM 2013-04-04-->
        <script language ="javascript" type="text/javascript" src="Scripts/VarSelFx.js"></script>
        
          <!--JAM 2014-04-15-->
        <script language ="javascript" type="text/javascript" src="Scripts/VarSel_ForeVars.js"></script>

        <!--JAM 2013-04-15-->
        <script language ="javascript" type="text/javascript" src="Scripts/VarSelForeFx.js"></script>  --%>

   
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" onload="javascript:pageResize();">
      <form id="Form1" method="post" runat="server">
         <asp:ScriptManager EnablePageMethods="true" EnablePartialRendering="true" ID="MainSM" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="true"></asp:ScriptManager>
           <%--<asp:ScriptManager EnablePageMethods="true" EnablePartialRendering="true" ID="ScriptManager1" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="true"></asp:ScriptManager>--%>
        <%--<asp:UpdatePanel id="UpdatePanel1" runat="server">--%>
            <%--<ContentTemplate>--%>
        <input id="txtFiles" name="txtFiles" type="hidden" runat="server" />
        <asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
            <asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
                Font-Bold="True">
                <asp:TableCell ID="LogoCell" Width="40px">
                    <!--jam 2022/04/16-->
                    <a href="https://korbel.du.edu/pardee">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a>
                </asp:TableCell>
                <asp:TableCell Text="Format Graph" ID="TitleCell"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#ffffff" border="0">
            <tr>
                <td>
                    <cswm:WebMenu ID="cswmWebMenu" style="z-index: 999; left: 0px; position: absolute; top: 16px"
                        runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:WebMenu>
                </td>
            </tr>
        </table>
   

        <table>
            <asp:Panel ID="pnlMain" runat="server">
                 <tr valign="top" >
                     <td colspan="3">
                           <asp:Label ID="Label5" runat="server" 
                            Font-Bold="True" Font-Names="Arial">Click arrow to move. At a textbox, edit Variable Name for graph and hit enter (or OK) to register your change to the label</asp:Label>
                     <asp:button id="cmdRenameFormula" style="display:none" 
				            runat="server" ></asp:button> 
                         <asp:button id="cmdRename1" style="display:none"
				            runat="server" ></asp:button> 
                          <asp:button id="cmdRename1p" style="display:none"
				            runat="server" ></asp:button> 
                         </td>
                    
                 </tr>
                <tr >
                     <td colspan="2">
                         
                     <asp:button id="btnBack" Text="<"
				            runat="server" OnClick = "MoveBack" ></asp:button> 
                          <asp:TextBox id="txtRowIndex"  runat="server"
				               Width="20">0</asp:TextBox>
                          <asp:button id="btnForw" OnClick = "MoveForw" Text=">"
				            runat="server" ></asp:button> 
                         </td>
                   
                      <td  align="right" style="width: 100%">
                     <asp:Panel ID="Panel1" runat="server" Width="100%"> 	    
			            <asp:button id="btnPageBack" 
				            runat="server" Width="120px" Text="Close" OnClick = "PageBack"></asp:button>
			           
                    </asp:Panel>
                </td>
                 </tr>
              
                <tr>
                    <td width="40%">
                         <asp:TextBox id="txtVarName"  runat="server"
				               Width="80%"></asp:TextBox>
                         <asp:button id="btnOKV" 
				            runat="server" Width="20%" Text="OK"></asp:button>
                    </td>
                      <td width="20%">
                         <asp:TextBox id="txtDim1"  runat="server"
				               Width="80%"></asp:TextBox>
                          <asp:button id="btnOK1" 
				            runat="server" Width="20%" Text="OK"></asp:button>
                    </td>
                      <td width="20%">
                         <asp:TextBox id="txtDim1p"   runat="server"
				               Width="80%"></asp:TextBox>
                             <asp:button id="btnOK1p" 
				            runat="server" Width="20%" Text="OK"></asp:button>
                    </td>
                      <td width="20%">
                        
                    </td>
                </tr>
              
                <tr >
                    <td width="40%">
                       
                        <asp:ListBox ID="lstSeries"
                            runat="server" Height="457px" Width="90%" SelectionMode="Single"></asp:ListBox>
                        <br />
                        <br />

                    </td>
                    <td width="20%">

                        <asp:ListBox ID="lstDim1" runat="server" 
                            Height="457px" Width="100%" SelectionMode="Single"></asp:ListBox>
                    </td>
                    <td width="20%">

                        <asp:ListBox ID="lstDim1p" runat="server" 
                            Height="457px" Width="100%" SelectionMode="Single"></asp:ListBox>
                    </td>


                </tr>
            </asp:Panel>
        
           
          

        </table>



        <script language="javascript">
            var myWidth = 0, myHeight = 0;

            function pageResize() {
                if (typeof (window.innerWidth) == 'number') {
                    /*Non-IE*/
                    myWidth = window.innerWidth;
                    myHeight = window.innerHeight;
                } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                    /*//IE 6+ in 'standards compliant mode*/
                    myWidth = document.documentElement.clientWidth;
                    myHeight = document.documentElement.clientHeight;
                } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                    /*//IE 4 compatible*/
                    myWidth = document.body.clientWidth;
                    myHeight = document.body.clientHeight;
                }
                if (myWidth == 0) {
                    myWidth = screen.width;
                    myHeight = screen.height;
                }

                //jrs 2012/08/03 only if listboxs are visible
                if (document.forms[0].lstDim1 != null) {

                    //var doc = document.forms[0];
                    //var f2 = 0.95;
                    //var f = 1.025;
                    //var h = myHeight * f2 - (20 + 64) - (24 + 20) * f;
                    //if (h < 106 + 20) {
                    //    h = 106 + 20;
                    //}
                    //doc.lstDim1.style.height = h + 'px';
                    //--------------------------------------------------------
                    //var h2 = h * 0.7;
                    //doc.lstDim2.style.height = h2 + 'px';
                    //var T = (92 + h2) * f;
                    //document.getElementById('lblVars').style.top = T + 'px';
                    //var tt = T + 20;
                    //doc.lstVars.style.top = tt + 'px';

                    //var t2 = (tt + 106 + 20) * f;
                    //doc.cmdDraw.style.top = t2 + 'px';
                    //doc.cmdTable.style.top = t2 + 'px';
                    //doc.cmdClear.style.top = t2 + 'px';

                    //t2 = (92 + 106) * f;
                    //document.getElementById('lblFiles').style.top = t2 + 'px';
                    //t2 = t2 + 20;
                    //doc.lstFiles.style.top = t2 + 'px';
                    //h2 = (h - 106 - 20 - 20);
                    //doc.lstFiles.style.height = h2 + 'px';

                 
                }
                
                
            }

          
            if ((document.forms[0].txtVarName != null) && (document.forms[0].txtVarName.disabled == false)) {
                document.forms[0].txtVarName.focus();
            }
          
            function clickButton(e, buttonid) {
                           var evt = e ? e : window.event;
                var bt = document.getElementById(buttonid);
                console.log('z1=' + evt.keyCode);
                if (bt) {
                    console.log('z2=' + evt.keyCode);
                                   if (evt.keyCode == 13) {
                                        bt.click();
                                        return false;
                    }
                }
            }
            function clickButton1(e, buttonid) {
                var evt = e ? e : window.event;
                var bt = document.getElementById(buttonid);
                console.log('z1=' + evt.keyCode);
                if (bt) {
                    console.log('z2=' + evt.keyCode);
                    if (evt.keyCode == 13) {
                        bt.click();
                        return false;
                    }
                }
            }
            function clickButton1p(e, buttonid) {
                var evt = e ? e : window.event;
                var bt = document.getElementById(buttonid);
                console.log('z1=' + evt.keyCode);
                if (bt) {
                    console.log('z2=' + evt.keyCode);
                    if (evt.keyCode == 13) {
                        bt.click();
                        return false;
                    }
                }
            }
                function Confirm() {
            var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                    confirm_value.value = "Yes";
            } else {
                    confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    
        </script>
               
             <%--</ContentTemplate>--%>
     <%--</asp:UpdatePanel>--%>
    </form>
                
</body>
</html>

<script type="text/javascript">

    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-3138457-1']);
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();

   
</script>
