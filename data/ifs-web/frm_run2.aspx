<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_Run2" Codebehind="frm_run2.aspx.vb" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Running International Futures</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	
        <script type="text/javascript" src="Scripts/jquery/1.8.2/jquery.min.js"></script>
        <script type="text/javascript" src="Scripts/highcharts.js"></script>
        <script type="text/javascript" src="Scripts/exporting.js"></script>
		<script type="text/javascript" src="Scripts/Run.js?<%=IFsWeb.Utils.GetVersionTag()%>"></script>

		<style type="text/css">
			.buttonHidden {
				display: none;
			}
			.buttonShow {
				display: inline-block;
			}
		</style>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" onload="showpg();cmdRun_Click();">
		<script language="javascript">
			function showpg() {
				var doc = document.forms[0];

				if (doc.cmd_Cancel!=null && doc.cmd_Cancel.disabled == false && doc.cmd_Cancel.value == "Interrupt")
				{
					document.body.style.cursor = "wait";
					doc.cmd_Cancel.style.cursor = "wait";
					doc.cmd_Start.style.cursor = "wait";
				}
			};

            function cmdRun_Click() {
                var doc = document.forms[0];
                var btn = document.getElementById('cmd_Start');
                var aut = document.getElementById('txtAutomaticRun');
                if (aut.value == "1") {
                    aut.value = "2";
                    btn.click();
                }
            }

        </script>
		<form runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="ActiveCaption" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!-- jam 2022/04/03-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.jpg" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Running International Futures" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<input id="txtAutomaticRun" name="txtAutomaticRun"  type="hidden" runat="server"  />
			<table width="100%">
				<tr>
					<td width="50%"><br>
					&nbsp;&nbsp;
						<asp:label id="lbl_RunYears" tabIndex="5" runat="server" Width="232px" 
                            Height="33px" BorderStyle="None"
							BackColor="Window" Text="Years you wish to run model:" Font-Name="Arial"
							Font-Size="10pt" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" 
                            Font-Underline="False" ForeColor="WindowText" Font-Names="Arial"></asp:label>&nbsp;<asp:textbox 
                            id="txt_RunYears" tabIndex="6" runat="server" Width="57" Height="33" BorderStyle="Solid"
							BackColor="Window" Text="Text1" Visible="True" Font-Name="Arial" Font-Size="10pt" 
                            Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
							Enabled="True" ForeColor="WindowText" ReadOnly="False" MaxLength="0" Textmode="Singleline" 
                            AutoPostBack="False" Font-Names="Arial"></asp:textbox>&nbsp;
						
						<asp:button id="cmdNext" accessKey="N" tabIndex="16" runat="server" Width="81" Height="33" BackColor="Control"
							Text="(N)ext" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
							Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText" CausesValidation="True"></asp:button><asp:button id="cmdBack" accessKey="B" tabIndex="15" runat="server" Width="81" Height="33" BackColor="Control"
							Text="(B)ack" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText" CausesValidation="True"></asp:button><asp:button id="cmdReset" accessKey="R" tabIndex="14" runat="server" Width="81" Height="33"
							BackColor="Control" Text="(R)eset" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText" CausesValidation="True"></asp:button><asp:button id="cmdExit" accessKey="E" tabIndex="13" runat="server" Width="81" Height="33" BackColor="Control"
							Text="(E)xit" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText" CausesValidation="True"></asp:button><asp:button id="cmd_Reset" accessKey="R" tabIndex="12" runat="server" Width="297" Height="41"
							BackColor="Control" Text="(R)eset Start Year to 1992" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText" CausesValidation="True"></asp:button>&nbsp;&nbsp;
						<asp:button id="cmd_Start" accessKey="S" tabIndex="9" runat="server" 
                            Width="127px" Height="32px"
							BackColor="Control" Text="(S)tart Run" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
							Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText"
							CausesValidation="True"></asp:button>&nbsp;&nbsp;
						<asp:button id="cmd_Cancel" accessKey="C" tabIndex="11" runat="server" Width="127" Height="32px"
							BackColor="Control" Text="(C)ancel" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
							Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText"
							CausesValidation="True"></asp:button>&nbsp;
						<asp:button id="cmd_continue" accessKey="C" tabIndex="10" runat="server" Width="302" Height="32px"
							BackColor="Control" Text="Run Successful - (C)ontinue" Visible="True" Font-Name="Arial"
							Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
							Enabled="True" ForeColor="ControlText" CausesValidation="True" CssClass="buttonHidden"></asp:button>
                            &nbsp;&nbsp;<input id="btnHelp" onclick="javascript:ShowHelp();"
                                type="button" value="Help" style="width: 127px; height:32px" />&nbsp;
                        <asp:label id="lbl_WorkingFile" tabIndex="4" runat="server" Width="635px" 
                            Height="27px" BorderStyle="None"
							BackColor="Window" Text="Working file based on:" Font-Name="Arial" Font-Size="10pt" Font-Bold="True"
							Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" 
                            Font-Names="Arial"></asp:label>
							<br>
						<asp:label id="lbl_BeginRun" tabIndex="3" runat="server" Width="249px" 
                            Height="24px" BorderStyle="None"
							BackColor="Window" Text="Run will begin in year:" Font-Name="Arial" Font-Size="10pt"
							Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
							ForeColor="WindowText" Font-Names="Arial"></asp:label></td>
					<td width="50%">
							<br />
							&nbsp;<!--RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16024);--></td>
				</tr>
				<tr>
					<td align="center" width="100%">
                            <table>
                            <tr>
                                <td><div id="container1"></div></td>
                                <td><div id="container2"></div></td>
                            </tr>
                            <tr>
                                <td><div id="container3"></div></td>
                                <td><div id="container4"></div></td>
                            </tr>
                        </table>                        		
					</td>
				</tr>
			</table>
			<script type="text/javascript">
				window.history.forward();
				<% If (CType(Session("RuningProgress"), Boolean)) %>

                SetupRunGraph('1', 'POP', 'World Population (Millions)', null);
                SetupRunGraph('2', 'GDP', 'World GDP (Billion Currency)', null);
                SetupRunGraph('3', 'FST', 'World Forest (Million Hectares)', null);
				SetupRunGraph('4', 'CO2', 'Atmospheric CO2 (PPM)', null);

				var popGraph = $('#container1').highcharts();
				var gdpGraph = $('#container2').highcharts();
				var fstGraph = $('#container3').highcharts();
				var co2Graph = $('#container4').highcharts();

				var url = '<%= IFsWeb.Utils.GetWSUri(Request, "Run.ws")%>';
				console.log('url:' + url);
				var webSocket = new WebSocket(url);
				console.log('websocket - created');

				var updatecnt = 0;
				var initcnt = 0;

				webSocket.onopen = function(event) {
					console.log('websocket - onopen');
                    var sIDmsg = JSON.stringify({ sessionID: "<%=Session.SessionID%>" });
                    webSocket.send(sIDmsg);
				};
				
				webSocket.onmessage = function(event) {
					console.log('websocket - onmessage:' + event.data);
					const msg = JSON.parse(event.data);

					if (msg.IsRunning) {
						if (!msg.HasError) {
							updatecnt++;
							if (updatecnt == 1) {
								$('#statusMsg').text("Running the Model");
							}
							else {
                                var txt = $('#statusMsg').text()
                                $('#statusMsg').text(txt + '.');
							}
                            
							if (msg.RunValues) {
								runVals = msg.RunValues;
								if (runVals.length > 0) {
									popGraph.series[0].addPoint([runVals[0].Year, runVals[0].PopValue]);
									gdpGraph.series[0].addPoint([runVals[0].Year, runVals[0].GdpValue]);
									fstGraph.series[0].addPoint([runVals[0].Year, runVals[0].FstValue]);
									co2Graph.series[0].addPoint([runVals[0].Year, runVals[0].Co2Value]);
								}
								//console.log('values:' + runVals[0].Year + ' - '+ runVals[0].GdpValue + ' - ' + runVals[0].PopValue + ' - ' + runVals[0].FstValue  + ' - ' + runVals[0].Co2Value);
							}
						}
						// was running and got in error
						else {
							console.log('isRunning: true hasError: true');
                            $('#statusMsg').text(msg.StatusMessage);
						}
					}
					else {
                        console.log('websocket - is not running');
						// no more running and no error
						if (!msg.HasError) {
							if (msg.Status == RunStatus.COMPLETED) { 
								$('#statusMsg').text("The model run was completed succesfully.")
								document.body.style.cursor = "default";
                                $('#cmd_continue').removeClass('buttonHidden');
								$('#cmd_continue').addClass('buttonShown');
								$('#cmd_Start').removeClass('buttonShown');
                                $('#cmd_Start').addClass('buttonHidden');
								$('#cmd_Cancel').removeClass('buttonShown');
                                $('#cmd_Cancel').addClass('buttonHidden');
							}
							if (msg.Status == RunStatus.WAITING_PROGRESS_FILE) {
								initcnt++;
								if (initcnt == 1) {
									$('#statusMsg').text("Running the Model, Please wait for the initialization to finish ...")
								}
								else {
									var txt = $('#statusMsg').text();
									$('#statusMsg').text(txt + '.');
								}
							}
						} else {
                            console.log('isRunning: false hasError: true');
							if (msg.Status == RunStatus.ERRORFILE) {
                                $('#cmd_continue').removeClass('buttonHidden');
                                $('#cmd_continue').addClass('buttonShown');
								$('#cmd_continue').text('(C)ontinue');

								$('#statusMsg').text(msg.StatusMessage);

                                $('#cmd_Start').removeClass('buttonShown');
                                $('#cmd_Start').addClass('buttonHidden');
                                $('#cmd_Cancel').removeClass('buttonShown');
                                $('#cmd_Cancel').addClass('buttonHidden');
							}
						}
					}
				};
				webSocket.onclose = function (event) {
					console.log('websocket - onclose');
				}
				<%  End If%>
            </script>
			<!--
		    <p>
                        &nbsp&nbsp&nbsp&nbsp&nbsp<asp:label id="lblRebuildBase" tabIndex="4" runat="server" Width="635px" 
                            Height="27px" BorderStyle="None"
							BackColor="Window" Text="Rebuild Base Message" Font-Name="Arial" Font-Size="10pt" Font-Bold="True"
							Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" 
                            Font-Names="Arial" Visible="False"></asp:label>
							</p>-->
			<p>&nbsp&nbsp&nbsp&nbsp&nbsp
				<span style="width:635px; height:27px; display:inline-block; font-weight:bold; font-style:normal; font-size:10pt; font-family:Arial; color:windowtext;" id="statusMsg">&nbsp;</span>
			</p>
		</form>
	</body>
</HTML>
