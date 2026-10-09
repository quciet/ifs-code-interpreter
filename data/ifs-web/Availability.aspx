<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_Availability" Codebehind="Availability.aspx.vb" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!-- jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body leftMargin="0" rightMargin="0" topmargin="0">
		<form runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Data Availability" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<asp:panel id="FrameAvailability" style="Z-INDEX: 101; LEFT: 26px; POSITION: absolute; TOP: 327px"
				tabIndex="3" runat="server" Height="218" Width="664px" Visible="True" ForeColor="ControlText"
				Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
				Font-Size="8.0" Font-Name="Arial">
				<P>Series Data:</P>
				<asp:Panel id="FrameScope" tabIndex="21" runat="server" Width="416px" Height="96px" Font-Name="Arial"
					Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
					Enabled="True" ForeColor="ControlText" Visible="True">
					<P>Scope:</P>
					<P>
						<asp:Label id="lblCountry" tabIndex="9" runat="server" Width="83px" Height="15" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="No. of Countries:"
							BorderStyle="None"></asp:Label>
						<asp:Label id="lblCountriesVal" tabIndex="10" runat="server" Width="29px" Height="15" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="999"
							BorderStyle="None"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Label id="lblFirstYear" tabIndex="13" runat="server" Width="70px" Height="15" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="First Year:"
							BorderStyle="None"></asp:Label>
						<asp:Label id="lblFirstYearVal" tabIndex="15" runat="server" Width="20px" Height="15" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="1960"
							BorderStyle="None"></asp:Label></P>
					<P>
						<asp:Label id="lblYears" tabIndex="11" runat="server" Width="72px" Height="15" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="No. of Years:"
							BorderStyle="None"></asp:Label>
                        &nbsp; &nbsp;
						<asp:Label id="lblYearsVal" tabIndex="12" runat="server" Width="25px" Height="15" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="999"
							BorderStyle="None"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Label id="lblLastYear" tabIndex="14" runat="server" Width="62px" Height="15" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="Last Year:"
							BorderStyle="None"></asp:Label>
                        &nbsp;&nbsp;
						<asp:Label id="lblLastYearVal" tabIndex="16" runat="server" Width="6px" Height="14" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="2000"
							BorderStyle="None"></asp:Label></P>
				</asp:Panel>
				<asp:Panel id="FrameBest" tabIndex="17" runat="server" Width="416px" Height="57px" Font-Name="Arial"
					Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
					Enabled="True" ForeColor="ControlText" Visible="True">
					<P>Best Availability:</P>
					<P>
						<asp:Label id="lblBestYearVal" tabIndex="19" runat="server" Width="91" Height="15" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="1973 (%)"
							BorderStyle="None"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Label id="lblBestCountryVal" tabIndex="18" runat="server" Width="135" Height="15" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="Country(%)"
							BorderStyle="None"></asp:Label></P>
				</asp:Panel>
				<asp:Panel id="frmYearbyYear" tabIndex="28" runat="server" Width="416px" Height="65px" Font-Name="Arial"
					Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
					Enabled="True" ForeColor="ControlText" Visible="True">
					<P>Year by Year Availability:</P>
					<P>
						<asp:DropDownList id="cmbYear" tabIndex="2" runat="server" Width="116" Height="21" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="WindowText" Visible="True" AutoPostBack="True"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Label id="lblYearAvailability" tabIndex="30" runat="server" Width="54" Height="17" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="99.99%"
							BorderStyle="None"></asp:Label></P>
				</asp:Panel>
				<asp:Panel id="FrameTotal" tabIndex="20" runat="server" Width="416px" Height="98px" Font-Name="Arial"
					Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
					Enabled="True" ForeColor="ControlText" Visible="True">
					<P>Total Availability:</P>
					<P>
						<asp:Label id="lblTotPoints" tabIndex="22" runat="server" Width="60px" Height="17" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="Toal Points:"
							BorderStyle="None"></asp:Label>
						<asp:Label id="lblTotPointsVal" tabIndex="24" runat="server" Width="40px" Height="13" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="99999"
							BorderStyle="None"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Label id="lblAvaPoints" tabIndex="23" runat="server" Width="95px" Height="17" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="Available Points:"
							BorderStyle="None"></asp:Label>
						<asp:Label id="lblAvaPointsVal" tabIndex="25" runat="server" Width="41px" Height="13" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="999"
							BorderStyle="None"></asp:Label></P>
					<P>
						<asp:Label id="lblPercentAva" tabIndex="26" runat="server" Width="138" Height="17" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="Percentage Availability:"
							BorderStyle="None"></asp:Label>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
						<asp:Label id="lblPercentAvaVal" tabIndex="27" runat="server" Width="53px" Height="13" BackColor="Window"
							Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
							Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="99.99%"
							BorderStyle="None"></asp:Label></P>
				</asp:Panel>
			</asp:panel>
			<asp:panel id="FrameSeries" style="Z-INDEX: 102; LEFT: 25px; POSITION: absolute; top: 46px;" tabIndex="1"
				runat="server" Height="180" Width="664px" Visible="True" ForeColor="ControlText" Enabled="True"
				Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
				Font-Name="Arial">
				<P>Series Properties</P>
				<P>
					<asp:Label id="lblSeriesName" tabIndex="2" runat="server" Width="72px" Height="15" BackColor="Window"
						Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
						Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="Series Name:"
						BorderStyle="None"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:Label id="lblSeriesNameVal" tabIndex="4" runat="server" Width="191px" Height="15" BackColor="Window"
						Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
						Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="Name"
						BorderStyle="None"></asp:Label></P>
				<P>
					<asp:Label id="lblSeriesDefn" tabIndex="5" runat="server" Width="63" Height="15" BackColor="Window"
						Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
						Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="Definition:"
						BorderStyle="None"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="lblSeriesDefnVal" tabIndex="6" runat="server" Width="191px" Height="8px" BackColor="Window"
						Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
						Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="Definition"
						BorderStyle="None"></asp:Label></P>
				<P>
				<asp:Label id="lblSource" tabIndex="7" runat="server" Width="63" Height="15" BackColor="Window"
					Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
					Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="Data Source:"
					BorderStyle="None"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="lblSourceVal" tabIndex="8" runat="server" Width="190px" Height="9px" BackColor="Window"
					Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
					Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" Text="Source"
					BorderStyle="None"></asp:Label></P>
				<P>
					<asp:Label id="lblOriginalSource" tabIndex="33" runat="server" Width="112px" Height="24px"
						BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
						Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True"
						Text="Original Data Source:" BorderStyle="None"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="lblOriginalSourceVal" tabIndex="32" runat="server" Width="186px" Height="22px"
						BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
						Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True"
						Text="Original Source" BorderStyle="None"></asp:Label></P>
				
				<P>
					<asp:Label id="lblLastIFsUPdate" tabIndex="7" runat="server" Width="89px" Height="23px" BackColor="Window"
						Font-Name="Arial" Font-Size="8pt" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
						Font-Underline="False" ForeColor="ControlText" Text="Last IFs Update:"
						BorderStyle="None" Font-Names="Arial"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label id="lblLastIFsUPdateVal" tabIndex="8" runat="server" Width="190px" Height="18px" BackColor="Window"
						Font-Name="Arial" Font-Size="8pt" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
						Font-Underline="False" ForeColor="ControlText"
						BorderStyle="None" Font-Names="Arial"></asp:Label></P>
			<P>
				<asp:Label id="lblNotes" tabIndex="7" runat="server" Width="89px" Height="23px" BackColor="Window"
					Font-Name="Arial" Font-Size="8pt" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
					Font-Underline="False" ForeColor="ControlText" Text="Notes:"
					BorderStyle="None" Font-Names="Arial"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label id="lblNotesVal" tabIndex="8" runat="server" Width="190px" Height="18px" BackColor="Window"
					Font-Name="Arial" Font-Size="8pt" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
					Font-Underline="False" ForeColor="ControlText"
					BorderStyle="None" Font-Names="Arial"></asp:Label></P>
			</asp:panel>
			<asp:button id="cmdOK" style="Z-INDEX: 103; LEFT: 28px; POSITION: absolute; TOP: 702px; Width:335px" tabIndex="1"
				runat="server"  Height="28" BackColor="Control" Font-Name="Arial" Font-Size="8.25"
				Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True"
				ForeColor="ControlText" Text="OK" CausesValidation="True" UseSubmitBehavior="False"></asp:button>
			<SCRIPT language="javascript">
{
	if ((document.all['cmdOK'] != null)&& (document.all['cmdOK'].disabled == false ) ) 
          {
               document.all('cmdOK').focus();
          }
      // document.getElementById("cmdOK").onclick=;
}
			</SCRIPT>
		</form>
	</body>
</HTML>
