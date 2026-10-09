<%@ Page Language="vb" AutoEventWireup="true" Inherits="IFsWeb.frm_BilateralInteractionDGravity" Codebehind="frm_BilateralInteractionDGravity.aspx.vb" %>

<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Gravity Model</title>
    <script language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
    </script>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <!--jam 2021/11/27-->
    <%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>

    <script language="javascript" type="text/javascript" src="Scripts/FilesInOrder.js"></script>
     <!--jam 2021/11/27-->
   <%-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
     <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
    <script src="Scripts/highcharts.js"></script>
    <script src="Scripts/exporting.js"></script>

    
       <script type="text/javascript">

        $(document).ready(function () {

            $('input[type=text]').focus(function () {
                
                var element = document.getElementById($(this).attr('id'));
                element.style.backgroundColor = "yellow";
                var elementTag = document.getElementById('Transformation2Tag');
                if ($(this).attr('id') === 'T2Text0') {
                  
                    elementTag.value = "X";
                }
                else {
                    if ($(this).attr('id').startsWith("T1Text")) {
                        var elementT1 = document.getElementById('T1TextSelected');
                        elementT1.value = $(this).attr('id');
                        //-----
                        var obj = this;
                        //get X,Y
                        console.log('id=' + $(this).attr('id'));
                        var X = obj.offsetLeft;
                        var Y = obj.offsetTop;
                        while (obj.offsetParent) {
                            X = X + obj.offsetParent.offsetLeft;
                            Y = Y + obj.offsetParent.offsetTop;
                            if (obj == document.getElementsByTagName("body")[0]) {
                                break;
                            }
                            else {
                                obj = obj.offsetParent;
                            }
                        }
                        cswmShow('Group_0bd4', '', 'below', X, Y, 1);
                       
                    }
                    else {
                        elementT1.value = 'T1Text0';
                    }
                    elementTag.value = "";
                }
            }).blur(function () {
                var element = document.getElementById($(this).attr('id'));
                element.style.backgroundColor = "white";
             
            });
        });
    </script>
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" onload="javascript:pageResize();">


    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <form id="Form1" method="post" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <input id="txtFiles" name="txtFiles" type="hidden" runat="server" />
                 <input id="Transformation2Tag" name="Transformation2Tag" type="hidden" value="" runat= "server" />
                  <input id="T1TextSelected" name="T1TextSelected" type="hidden" value="T1Text0" runat= "server" />
                <asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
                    <asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
                        Font-Bold="True">
                        <asp:TableCell ID="LogoCell" Width="40px">
                            <!--jam 2022/04/15-->
                            <a href="https://korbel.du.edu/pardee">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a>
                        </asp:TableCell>
                        <asp:TableCell Text="Gravity Model" ID="TitleCell"></asp:TableCell>
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


                <!-------------------------------------------------------------->

                <table>
                    <asp:Panel ID="pnlMain" runat="server">
                        <tr valign="top">
                            <td width="35%">
                                <asp:Label ID="Label2" runat="server"
                                    Font-Bold="True" Font-Names="Arial">Please select one of the Save options or Continue without Saving.</asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="Label5" runat="server"
                                    Font-Bold="True" Font-Names="Arial">Variables: (click to remove)</asp:Label>
                                <br />
                                <asp:ListBox ID="lstVariableDisplay"
                                    runat="server" Height="260px" Width="400" AutoPostBack="True"></asp:ListBox>
                                <br />
                                <br />
                                 <asp:Label ID="lblReg" runat="server"
                                    Font-Bold="True" Width="148" Font-Names="Arial">Coefficients:</asp:Label>
                                <br />
                                <asp:Panel ID="pnlWeight" runat="server" Width="40">
                                </asp:Panel>
                               
                            </td>
                            <td width="30%">
                               
                                <asp:Label ID="Label6" runat="server"
                                    Font-Bold="True" Width="148" Font-Names="Arial">Transformation Rules (Linear, Logarithmic, etc. click to change or select menu option):</asp:Label>
                                <br />
                                <asp:Panel ID="pnlTransformation" runat="server" Width="40">
                                </asp:Panel>
                                <br />
                                <br />
                                <asp:Label ID="Label7" runat="server"
                                    Font-Bold="True" Width="148" Font-Names="Arial">Transformation Rules (This one is applied to the entire formula, as opossed to each individual term):</asp:Label>
                                <br />
                                <asp:Panel ID="pnlTransformation2" runat="server">
                                </asp:Panel>
                                <br />
                                <br />
                                <asp:Label ID="Label8" runat="server"
                                    Font-Bold="True" Width="148" Font-Names="Arial">Gravity Equation (Click button to update if you have made any changes):</asp:Label>
                                <asp:Button ID="cmdUpdEq" runat="server" Width="88px" Text="Update Eq."></asp:Button>
                                 <asp:Button ID="cmdRenderList" runat="server" style="display:none" ></asp:Button>
                                 <asp:Button ID="cmdRemoveVariable" runat="server" style="display:none" ></asp:Button>
                                <br />
                                <asp:TextBox ID="txtFormula" Width="320" runat="server">

                                </asp:TextBox>
                                <br />
                                 <br />
                                <asp:Label ID="lblGravityVariableName" runat="server"
                                    Font-Bold="True" Width="148" Font-Names="Arial">Gravity Variable Name:</asp:Label>
                                <asp:Label ID="lblAdjVarName" runat="server"
                                    Font-Bold="True" Width="148" Font-Names="Arial">Gravity Adjustment Variable Name:</asp:Label>
                                <br />
                                <asp:TextBox ID="txtFormulaName" Width="320" runat="server" Text="Gravity1">

                                </asp:TextBox>
                                <br />
                                 <br />
                                <asp:Label ID="Label10" runat="server"
                                    Font-Bold="True" Width="148" Font-Names="Arial">Gravity Variable Description:</asp:Label>
                                <br />
                                <asp:TextBox ID="txtGravityDescription" Width="320" runat="server">

                                </asp:TextBox>

                               
                            </td>
                            <td width="35%"></td>
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

            </ContentTemplate>
        </asp:UpdatePanel>
        <SCRIPT language="JavaScript" type="text/javascript">

            // Function which shows the Context Menu by calling
            // cswmShow and passing in coordinates.
            function ShowContextMenu(e) {
              
                 //Quick and Dirty IE NN 6.1 Detection
                var flag = false;
                if (navigator.appName == 'Netscape')
                    flag = true;
                // NN
                if (flag)
                    cswmShow('Group_0bd4', '', 'above', e.pageX, e.pageY, 1);
                // IE
                else
                    cswmShow('Group_0bd4', '', 'above', event.x + document.body.scrollLeft, event.y + document.body.scrollTop, 1);
            }

            // jrs 08/19/04 if list is visible 
            //if (document.forms[0].txtFormula != null) {
            //    if (document.forms[0].txtFormula.captureEvents != null)
            //        document.forms[0].txtFormula.captureEvents(Event.MOUSEDOWN);
            //    document.forms[0].txtFormula.onmousedown = ShowContextMenu;
            //}
			</SCRIPT>
        
			<cswm:webmenu id="wmContext" runat="server" Width="16px" Height="16px" MenuBar-AbsoluteDockEnabled="False"
				MenuBar-AbsoluteDockPaddingAdjust="False" MenuBar-DisplayMode="None" style="Z-INDEX: 109; LEFT: 712px; POSITION: absolute; TOP: 40px"></cswm:webmenu>
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
