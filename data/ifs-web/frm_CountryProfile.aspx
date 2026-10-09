<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_CountryProfile" Codebehind="frm_CountryProfile.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
		<title>Key Development Forecasts</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
    <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>	

   <%-- <script type="text/javascript">        
        /*jam 2025/02/01 Retriving category value*/
        window.onload = function () {

            var doc = document.forms[0];
            let keepUsrDefCat = sessionStorage.getItem("SUsrDefCat");
            if (keepUsrDefCat) {
                doc.fUsrDefCat.value = keepUsrDefCat;
            }
        };
    </script>   --%>    


</HEAD>

	<body leftMargin="0" rightMargin="0" topmargin="0"  >

    <div id="fb-root"></div>
    <script async defer crossorigin="anonymous" src="https://connect.facebook.net/en_US/sdk.js#xfbml=1&version=v20.0" nonce="BzGIJ0C5"></script>

    <form id="form1" runat="server">
        <input id="txtResValueHolder" name="txtResValueHolder"  type="hidden" />
            <a name="top"></a>

			<asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                    <asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="Image2" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Country Profile" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>

			<cswm:WebMenu id="cswmWebMenu" style="Z-INDEX: 110; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:WebMenu>
        
        <table cellspacing="0" width=100% border="0">
            <tr>
                <td width="2.5%"></td>
                <td align=left width="33%">
                    <font face="Arial" >
                    <a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/PardeeCenterLogo.jpg" Width="325px" /></a><br /><br />
                    Learn more about the International Futures (IFs) forecasting system and the Pardee Center for International Futures at the University of Denver.<br/>
                    <a href="https://korbel.du.edu/pardee">korbel.du.edu/pardee</a>                
                    </font>
                    <br><br>
                    <div class="fb-like" data-href="https://korbel.du.edu/pardee" data-width="350" data-layout="" data-action="" data-size="" data-share="false"></div>
                    <p><a href="https://twitter.com/PardeeCenterIFs" class="twitter-follow-button" data-show-count="false">Follow @PardeeCenterIFs</a>
                    <script>    !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");</script></p>
                    <!-- jrs 2024/06/25 <p><a href="https://listserv.du.edu/mailman/listinfo/ifs-news">Sign up for our monthly newsletter.</a></p> -->
                </td>
                <td align=center width="34%">
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/Maps/SLV.gif" Enabled="False" />
                </td>
                <td align=left width="28%">
                    <asp:Label ID="lblTitLink" runat="server" Font-Bold="True" Font-Names="Arial"></asp:Label>                
                </td>
                <td width="2.5%"></td>
            </tr>
            <tr>
                <td width="2.5%"></td>
                <td width="95%" colspan=3>
                    <asp:Label ID="lblText" runat="server" Text="Label" Font-Bold="True" Font-Names="Arial"></asp:Label>                
                </td>
                <td width="2.5%"></td>
            </tr>
        </table>
        <input ID="fUsrDefCat" name="fUsrDefCat" runat="server" type="hidden" />	

<script language="JavaScript">  
function expandCollapse(divId, anchorID)  
{  
   if(document.getElementById(divId).style.display    == 'none')  
  {  
    document.getElementById(divId).style.display='inline';  
    document.getElementById(anchorID).value='-';  
   }  
   else  
  {   
    document.getElementById(divId).style.display ='none';  
    document.getElementById(anchorID).value='+';   
   } 
   }  
</script>                      
<script language="JavaScript">
    /*jam 2025/02/01 Changing Category*/
function changeCat(divId, anchorID)  
{

    var vpivotstr = "0";
    var vfixedpivot = "0";

    var doc = document.forms[0];

    vpivotstr = divId.substring(1)

    vpivotstr = vpivotstr.substring(0, vpivotstr.length - 1)

    doc.fUsrDefCat.value = vpivotstr;
      

    //var vtempUsrDefCat = document.getElementById("fUsrDefCat").value;
    
   // sessionStorage.setItem("SUsrDefCat", vpivotstr);


    let urlUsrDefCat = document.getElementById("fUsrDefCat").value;
    window.location.href = window.location.pathname + "?pUsrDefCat=" + encodeURIComponent(urlUsrDefCat);
    
   
    //location.reload();


  // if(document.getElementById(divId).style.display    == 'none')  
  //{  
  //  document.getElementById(divId).style.display='inline';  
  //  document.getElementById(anchorID).value='-';  
  // }  
  // else  
  //{   
  //  document.getElementById(divId).style.display ='none';  
  //  document.getElementById(anchorID).value='+';   
  // } 
}  
</script>
      



<script src="http://www.google-analytics.com/urchin.js" type="text/javascript">
</script>
<script type="text/javascript">
_uacct = "UA-3138457-1";
urchinTracker();
</script>

    </form>
</body>
</html>
