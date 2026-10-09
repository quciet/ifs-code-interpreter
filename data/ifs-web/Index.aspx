<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.Migrated_Index" Codebehind="Index.aspx.vb" %>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>International Futures</title>
<link href="../inc/yui/build/reset/reset.css" rel="stylesheet" type="text/css" media="screen" />
<link rel="shortcut icon" href="..//favicon.ico" />
</head>
<link href="../inc/screen.css" rel="stylesheet" type="text/css" media="screen" />

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<meta name="keywords" content="international, futures, international futures, IFs, global, simulation, demographics, economics, energy, politics, university, denver, pardee" />
<meta name="description" content="IFs is a computer simulation of global systems for classroom and research use. IFs can be used to teach about or study demographics, economics, food, energy, the environment, and international politics." />
<script type="text/javascript" src="../inc/yui/build/yahoo-dom-event/yahoo-dom-event.js"></script>
<script type="text/javascript" src="../inc/yui/build/animation/animation.js"></script>
<script type="text/javascript" src="../inc/yui/build/container/container_core.js"></script>

<script type="text/javascript" src="../inc/yui/build/menu/menu.js"></script>
<script type="text/javascript" src="../inc/sitemenu.js"></script>
<script type="text/javascript">var siteRoot = "../";</script>
<script type="text/javascript" src="../inc/image.js"></script>
<style type="text/css">
img, div { behavior: url(../iepngfix.htc) };
</style> 


<div id="duStrip">
	<div id="duLogo"></div>

	<ul>
		<li><a href="http://www.du.edu/">DU</a></li>
		<li><a href="http://www.du.edu/today/">News</a></li>
		<li><a href="http://www.du.edu/calendar">Events</a></li>
		<li><a href="http://www.du.edu/Directory/servlet/DirectoryServlet">Directory</a></li>
		<li><a href="http://www.du.edu/az/">A-Z</a></li>

		<li><a href="http://search1.du.edu">QuickSearch</a></li>
	</ul>
</div>

<div id="siteWrapper" class="yui-skin-sam">
	<div id="siteHeader" onclick="location.href='../index.aspx'" title="Back to IFs Home">
		<div id="globe"></div>
		<script type="text/javascript">randomImage();</script>
		<div id="mainNavigation" class="yuimenubar yuimenubarnav">

			<div class="bd">
				<ul class="first-of-type">
					<li class="yuimenuitem"><a class="yuimenubaritemlabel" href="../index.aspx">Home</a></li>
					
					<li class="yuimenubaritem first-of-type"><a class="yuimenubaritemlabel" href="../introduction/index.aspx">Introduction</a>
						<div id="Introduction" class="yuimenu">
							<div class="bd">
								<ul>

									<li class="yuimenuitem"><a class="yuimenuitemlabel" href="../introduction/history.aspx">History</a></li>	
									<li class="yuimenuitem"><a class="yuimenuitemlabel" href="../introduction/index.aspx">Overview</a></li>

									<li class="yuimenuitem"><a class="yuimenuitemlabel" href="../introduction/information.aspx">Further Information</a></li>	
								</ul>
							</div>
						</div>      
					</li>
					
					<li class="yuimenubaritem first-of-type"><a class="yuimenubaritemlabel" href="../community/index.aspx">Community</a>

						<div id="Community" class="yuimenu">
							<div class="bd">
								<ul>

									<li class="yuimenuitem"><a class="yuimenuitemlabel" href="../community/team.aspx">IFs Team</a></li>
									<li class="yuimenuitem"><a class="yuimenuitemlabel" href="../community/forum.aspx">Forum</a></li>	
									<li class="yuimenuitem"><a class="yuimenuitemlabel" href="../community/users.aspx">Users</a></li>
									<li class="yuimenuitem"><a class="yuimenuitemlabel" href="../community/modelers.aspx">Modelers</a></li>

									<li class="yuimenuitem"><a class="yuimenuitemlabel" href="../community/supporters.aspx">Supporters</a></li>	
								</ul>
							</div>

						</div>      
					</li>
					
					<li class="yuimenubaritem first-of-type"><a class="yuimenubaritemlabel" href="../documents/index.aspx">Documents</a>
						<div id="Documents" class="yuimenu">
							<div class="bd">

								<ul>
									<li class="yuimenuitem"><a class="yuimenuitemlabel" href="../documents/index.aspx">PPHP</a></li>
									<li class="yuimenuitem"><a class="yuimenuitemlabel" href="../documents/reports.aspx">Reports</a></li>	
									<li class="yuimenuitem"><a class="yuimenuitemlabel" href="../documents/publications.aspx">Publications</a></li>

									<li class="yuimenuitem"><a class="yuimenuitemlabel" href="../documents/syllabi.aspx">Syllabi</a></li>	
								</ul>
							</div>

						</div>      
					</li>
			
					<li class="yuimenuitem"><a class="yuimenubaritemlabel" href="../pardee/index.aspx">Pardee Center</a></li>
				</ul>            
			</div>		
		</div>
	</div>

	
	<div id="mainBody">
		<div id="sideColumn">

			<ul id="sideMenu">
				<li><a href="../news/index.aspx">News</a></li>
				<li><a href="../ifs/index.aspx">Use IFs</a></li>
				<li><a href="../assets/help/helpoverview.aspx">IFs Help</a></li>
				<li><a href="../contact/index.aspx">Contact</a></li>

			</ul>

			<img src="../assets/images/sidegraph.png" alt="" />
		</div>
		
		<div id="contentArea">
 

<h1>Using International Futures (IFs) with Pardee</h1>
<form name="g2006search" method="post" action="index.aspx" id="g2006search" runat=server>

<p>For Help while using IFs, either in the Standalone Version or the Web Based version, simply hit F1.&nbsp; From the Standalone version, the Contact Sensitive Help System allows users to encounter a problem in a specific page, hit F1, and receive help tailored for that page</p>

     <table>
     
     <tr><td width=50%><h2>Web-Based&nbsp;IFs (<asp:Label id="lblVersion" runat="server">Version xx</asp:Label>)</h2></td>
          <td width=50%><h2>Download IFs with Pardee</h2></td></tr>
    
    <tr><td width=50%>
        <span style="font-size: 10pt">
    <label for="lstVer">Select the scenario set to use:</label>
        </span>

    <asp:ListBox id="lstVer" runat="server" width="150px" height="72px" Font-Size="X-Small">
<asp:ListItem Selected="True" Value="UNEPGEO">UNEPGEO</asp:ListItem>
</asp:ListBox>
        <span style="font-size: 10pt">
    <br />
    <br />
        </span>
    
    <a href="../news/WhatsNew.aspx"><span style="font-size: 10pt">What's new?</span></a><span
            style="font-size: 10pt">
    <br />
        </span>
    
    <label for="txtMaxTime">
        <br />
        <span style="font-size: 10pt">
        Type the Time 
        <br />
        Horizon (between 2011-2100):</span></label><span style="font-size: 10pt"> </span>
    <asp:TextBox id="txtMaxTime" runat=server Width="144px" Font-Size="X-Small">2020</asp:TextBox>
        <span style="font-size: 10pt"></span>
	<p><a href="http://www.ifsprev.du.edu/ifs/"><span style="font-size: 10pt">Legacy Version</span></a></p>
	<p><a href="../news/legacy.aspx"><span style="font-size: 10pt">(What is this?)</span></a></p>
    
    <asp:Button id="cmdContinue" runat=server Width="104px" Height="40px" Text="(C)ontinue" accessKey="c" Font-Size="X-Small"></asp:Button></td>
    <td widht=50%>
 <p>
     <span style="font-size: 10pt">Compatible with PCs with MS Windows (Most recent version of IFs successfully tested on Vista)</span></p>
    <ul>
        <li><a title="Download IFs v7.00" href="javascript:confirm_download(26);"><span style="color: #0000ff; font-size: 10pt;">
            IFsSetup V7.00 (January, 2014)</span></a><span style="font-size: 10pt"></span></li>
        <li><a title="Download IFs v6.75" href="javascript:confirm_download(25);"><span style="color: #0000ff; font-size: 10pt;">
            IFsSetup V6.75 (September, 2013)</span></a><span style="font-size: 10pt"> </span></li>
        <li><a title="Download IFs v6.69" href="javascript:confirm_download(24);"><span style="color: #0000ff; font-size: 10pt;">
            IFsSetup V6.69 (March, 2013)</span></a><span style="font-size: 10pt"> </span></li>
        <li><a title="Download IFs v6.61" href="javascript:confirm_download(23);"><span style="color: #0000ff; font-size: 10pt;">
            IFsSetup V6.61 (August, 2012)</span></a><span style="font-size: 10pt"> </span></li>
        <li><a title="Download IFs v6.54" href="javascript:confirm_download(22);"><span style="color: #0000ff; font-size: 10pt;">
            IFsSetup V6.54 (February, 2012)</span></a><span style="font-size: 10pt"></span></li>
        <li><a title="Download IFs v6.52" href="javascript:confirm_download(20);"><span style="color: #0000ff; font-size: 10pt;">
            IFsSetup V6.52 (December, 2011)</span></a><span style="font-size: 10pt"></span></li>
        <li><span style="font-size: 10pt">Horizon: Up to 2100<br />
    Default Scenarios: UNEP GEO </span></li>
    </ul>

    </td>
    </tr>

    </table>	  
    
    <p>
        We recommend the following citation for references to IFs: The International Futures
        (IFs) modeling system, version x.xx. IFs was initially developed by Barry B. Hughes
        and is based at the Frederick S. Pardee Center for International Futures, Josef
        Korbel School of International Studies, University of Denver, <a href=http://www.ifs.du.edu>www.ifs.du.edu</a>.</p>
    
    <h3>Further Information:</h3>

    <p>The book <a href="http://www.paradigmpublishers.com/Books/BookDetail.aspx?productID=131238">Exploring and Shaping International Futures</a>, Paradigm Publishers will provide support for the use of International Futures and for more general analysis of global change.</p>

    <p>Thanks to the U.S. <a href="../community/supporters.aspx">National Intelligence Council </a>(<a href="http://www.dni.gov/nic/NIC_2020_project.html">NIC</a>) for supporting the development and refinement of web-based IFs as part of their Project 2020.</p>
    <p>Thanks to <a href="../community/Pardee.aspx">Frederick S. Pardee</a> for general, sustaining support of the fifth generation of IFs.&nbsp; IFs with Pardee is being used in support of the United Nations Environmental Programme's Global Environmental Outlook 4 (UNEP GEO-4). Use is planned in support of the <a href="http://www.globalhumancondition.org">Development Report+50</a>.</p>

    <p>Thanks to the University of Denver for providing a home for the IFs server. None of the above parties is responsible for any difficulties associated with the software or its use. Neither Dr. Barry Hughes nor any party associated with International Futures or this website shall be liable for any direct or indirect consequences of the installation or use of the International Futures system.</p>
    
    <script language="javascript">
    if ((document.forms[0].cmdContinue.disabled == false)){
        document.forms[0].cmdContinue.focus();
    }
    
    function confirm_download(version) {
        if (version == 20) {
            var url="ftp://www.ifs.du.edu/IFsSetup_v6_52.zip";
            var filename="IFsSetup v6.52 [Dec.11]";
            var filesize="0.99 GB";
        }        
        else if (version == 22) {
            var url="ftp://www.ifs.du.edu/IFsSetup_v6_54.zip";
            var filename="IFsSetup v6.54 [Feb.12]";
            var filesize="1.15 GB";
        }
        else if (version == 23) {
            var url = "ftp://www.ifs.du.edu/IFsSetup_v6_61.zip";
            var filename = "IFsSetup v6.61 [Aug.12]";
            var filesize = "1.07 GB";
        }
        else if (version == 24) {
            var url = "ftp://www.ifs.du.edu/IFsSetup_v6_69.zip";
            var filename = "IFsSetup v6.69 [Mar.13]";
            var filesize = "1.12 GB";
        }
        else if (version == 25) {
            var url = "ftp://www.ifs.du.edu/IFsSetup_v6_75.zip";
            var filename = "IFsSetup v6.75 [Sep.13]";
            var filesize = "1.16 GB";
        }
        else if (version == 26) {
            var url = "ftp://www.ifs.du.edu/IFsSetup_v7.zip";
            var filename = "IFsSetup v7.00 [Jan.14]";
            var filesize = "1.20 GB";
        }        
        
        var msg="Notice: Neither Dr. Barry Hughes nor any party associated with \nInternational Futures or this website shall be liable for any direct\nor indirect consequences of the installation or use of the\nInternational Futures system.\n\nThis file is approximately "+filesize+".  Installation and effective\nuse of the model requires about 2 GB of dedicated hard disc\nspace.  Download times will vary based upon your internet\nconnection and any concurrent IFs model activities.\n\nClick OK to agree to the above notice and download the file.";
        //var msg="This option has been temporally disabled.";
        if(confirm(msg)){
            document.location.href=url;
        }
    }
    </script>
</form>

			<div class="clear"></div>
		</div>
	</div>
	
	<div id="siteFooter">

		<p>&copy;2013 University of Denver. All Rights Reserved. <a href="http://www.du.edu/privacy/">Privacy</a><br />The University of Denver is an equal opportunity/affirmative action institution.</p>

	</div>
</div>

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


