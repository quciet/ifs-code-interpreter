<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_MapViewerRanges" Codebehind="frm_MapViewerRanges.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>

        <!--JAM 2017-10-30 Avoiding "Enter" behavior in submit form -->
        <script type="text/javascript">
            function stopEnterKey(evt) {
                var evt = (evt) ? evt : ((event) ? event : null);
                var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
                if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
            }
            document.onkeypress = stopEnterKey;
        </script>
        <!------------------------------------------------------------>

	</HEAD>
	<body LeftMargin="0" RightMargin="0" topmargin="0">
    
		<form runat="server" >
            &nbsp; &nbsp;
			<asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0" style="z-index: 59; left: 0px; position: absolute; top: 0px">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Specify Categories" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
            &nbsp;
			<asp:Button runat="server" STYLE="Z-INDEX:57; LEFT:140px; POSITION:absolute; TOP:32px; TEXT-ALIGN:center"
				Text="S(a)ve" accessKey="a" Width="81" Height="33" TabIndex="8" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
				CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmdSave"></asp:Button>
            &nbsp;
			<asp:Button runat="server" STYLE="Z-INDEX:58; LEFT:240px; POSITION:absolute; TOP:32px; TEXT-ALIGN:center"
				Text="E(x)it" accessKey="x" Width="81" Height="33" TabIndex="9" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
				CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmdExit"></asp:Button>
            &nbsp;
			&nbsp; &nbsp;
            &nbsp;&nbsp;
			

            &nbsp; &nbsp;
            &nbsp;&nbsp;
            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;

            <asp:Table ID="TableLabel" runat="server"  Width="100%" Height="15px" CellSpacing="0" style="z-index: 112; left: 0px; position: absolute; top: 75px">
				<asp:TableRow ForeColor="White" ID="TableRow1" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="TableCell1" Width="40px">
					    <asp:Label ID="Label1a" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="True"
                            Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 114;
                            left: 24px; position: absolute; top: 30px; text-align: left" TabIndex="15" Text="Categories"
                            Width="273px" Enabled="False">
                        </asp:Label>
                    </asp:TableCell>
					
                    <asp:TableCell  ID="TableCell2" Width="40px">
					    <asp:Label ID="Label2" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="True"
                            Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 116;
                            left: 120px; position: absolute; top: 30px; text-align: left" TabIndex="20" Text="Start"
                            Width="273px" Enabled="False">
                        </asp:Label>
                    </asp:TableCell>

                    <asp:TableCell  ID="TableCell3" Width="40px">
					    <asp:Label ID="Label3" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="True"
                            Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 118;
                            left: 430px; position: absolute; top: 30px; text-align: left" TabIndex="25" Text="End"
                            Width="273px" Enabled="False">
                        </asp:Label>
                    </asp:TableCell>

				</asp:TableRow>

                <asp:TableRow ForeColor="White" ID="TableRow2" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="TableCell4" Width="40px">
					    <asp:Label ID="Category1" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="True"
                            Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 120;
                            left: 24px; position: absolute; top: 60px; text-align: left" TabIndex="15" Text="Category 1"
                            Width="273px">
                        </asp:Label>


                    </asp:TableCell>
					
                    <asp:TableCell  ID="TableCell5" Width="40px">
					    
                        
                         <asp:TextBox ID="Range1Start" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="False" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 122; left: 120px; position: absolute;
                        top: 60px; text-align: right" TabIndex="16" Text="Range1Start" TextMode="Singleline"
                        Visible="True" Width="273">
                        </asp:TextBox>


                    </asp:TableCell>

                    <asp:TableCell  ID="TableCell6" Width="40px">
					    <asp:TextBox ID="Range1End" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 50; left: 430px; position: absolute;
                        top: 60px; text-align: right" TabIndex="1" Text="Range1End" TextMode="Singleline"
                        Visible="True" Width="273" onchange="document.getElementById('Range2Start').value = document.getElementById('Range1End').value; ">
                        </asp:TextBox>
                    </asp:TableCell>

				</asp:TableRow>

                <asp:TableRow ForeColor="White" ID="TableRow3" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="TableCell7" Width="40px">
					    <asp:Label ID="Category2" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="True"
                            Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 126;
                            left: 24px; position: absolute; top: 90px; text-align: left" TabIndex="19" Text="Category 2"
                            Width="273px">
                        </asp:Label>


                    </asp:TableCell>
					
                    <asp:TableCell  ID="TableCell8" Width="40px">
					    
                        
                         <asp:TextBox ID="Range2Start" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="False" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 128; left: 120px; position: absolute;
                        top: 90px; text-align: right" TabIndex="20" Text="Range2Start" TextMode="Singleline"
                        Visible="True" Width="273" onchange="document.getElementById('Range1End').value = document.getElementById('Range2Start').value; ">
                        </asp:TextBox>


                    </asp:TableCell>

                    <asp:TableCell  ID="TableCell9" Width="40px">
					    <asp:TextBox ID="Range2End" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 51; left: 430px; position: absolute;
                        top: 90px; text-align: right" TabIndex="2" Text="Range2End" TextMode="Singleline"
                        Visible="True" Width="273" onchange="document.getElementById('Range3Start').value = document.getElementById('Range2End').value; ">
                        </asp:TextBox>
                    </asp:TableCell>

				</asp:TableRow>

                <asp:TableRow ForeColor="White" ID="TableRow4" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="TableCell10" Width="40px">
					    <asp:Label ID="Category3" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="True"
                            Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 130;
                            left: 24px; position: absolute; top: 120px; text-align: left" TabIndex="22" Text="Category 3"
                            Width="273px">
                        </asp:Label>


                    </asp:TableCell>
					
                    <asp:TableCell  ID="TableCell11" Width="40px">
					    
                        
                         <asp:TextBox ID="Range3Start" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="False" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 131; left: 120px; position: absolute;
                        top: 120px; text-align: right" TabIndex="23" Text="Range3Start" TextMode="Singleline"
                        Visible="True" Width="273" onchange="document.getElementById('Range2End').value = document.getElementById('Range3Start').value; ">
                        </asp:TextBox>


                    </asp:TableCell>

                    <asp:TableCell  ID="TableCell12" Width="40px">
					    <asp:TextBox ID="Range3End" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 52; left: 430px; position: absolute;
                        top: 120px; text-align: right" TabIndex="3" Text="Range3End" TextMode="Singleline"
                        Visible="True" Width="273" onchange="document.getElementById('Range4Start').value = document.getElementById('Range3End').value; ">
                        </asp:TextBox>
                    </asp:TableCell>

				</asp:TableRow>


                <asp:TableRow ForeColor="White" ID="TableRow5" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="TableCell13" Width="40px">
					    <asp:Label ID="Category4" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="True"
                            Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 133;
                            left: 24px; position: absolute; top: 150px; text-align: left" TabIndex="25" Text="Category 4"
                            Width="273px">
                        </asp:Label>


                    </asp:TableCell>
					
                    <asp:TableCell  ID="TableCell14" Width="40px">
					    
                        
                         <asp:TextBox ID="Range4Start" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="False" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 134; left: 120px; position: absolute;
                        top: 150px; text-align: right" TabIndex="25" Text="Range4Start" TextMode="Singleline"
                        Visible="True" Width="273" onchange="document.getElementById('Range3End').value = document.getElementById('Range4Start').value; ">
                        </asp:TextBox>


                    </asp:TableCell>

                    <asp:TableCell  ID="TableCell15" Width="40px">
					    <asp:TextBox ID="Range4End" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 53; left: 430px; position: absolute;
                        top: 150px; text-align: right" TabIndex="4" Text="Range4End" TextMode="Singleline"
                        Visible="True" Width="273" onchange="document.getElementById('Range5Start').value = document.getElementById('Range4End').value; ">
                        </asp:TextBox>
                    </asp:TableCell>

				</asp:TableRow>

                
                <asp:TableRow ForeColor="White" ID="TableRow6" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="TableCell16" Width="40px">
					    <asp:Label ID="Category5" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="True"
                            Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 136;
                            left: 24px; position: absolute; top: 180px; text-align: left" TabIndex="27" Text="Category 5"
                            Width="273px">
                        </asp:Label>


                    </asp:TableCell>
					
                    <asp:TableCell  ID="TableCell17" Width="40px">
					    
                        
                         <asp:TextBox ID="Range5Start" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="False" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 137; left: 120px; position: absolute;
                        top: 180px; text-align: right" TabIndex="28" Text="Range5Start" TextMode="Singleline"
                        Visible="True" Width="273" onchange="document.getElementById('Range4End').value = document.getElementById('Range5Start').value; ">
                        </asp:TextBox>


                    </asp:TableCell>

                    <asp:TableCell  ID="TableCell18" Width="40px">
					    <asp:TextBox ID="Range5End" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 54; left: 430px; position: absolute;
                        top: 180px; text-align: right" TabIndex="5" Text="Range5End" TextMode="Singleline"
                        Visible="True" Width="273" onchange="document.getElementById('Range6Start').value = document.getElementById('Range5End').value; ">
                        </asp:TextBox>
                    </asp:TableCell>

				</asp:TableRow>


                
                <asp:TableRow ForeColor="White" ID="TableRow7" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="TableCell19" Width="40px">
					    <asp:Label ID="Category6" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="True"
                            Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 139;
                            left: 24px; position: absolute; top: 210px; text-align: left" TabIndex="30" Text="Category 6"
                            Width="273px">
                        </asp:Label>


                    </asp:TableCell>
					
                    <asp:TableCell  ID="TableCell20" Width="40px">
					    
                        
                         <asp:TextBox ID="Range6Start" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="False" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 140; left: 120px; position: absolute;
                        top: 210px; text-align: right" TabIndex="31" Text="Range6Start" TextMode="Singleline"
                        Visible="True" Width="273" onchange="document.getElementById('Range5End').value = document.getElementById('Range6Start').value; ">
                        </asp:TextBox>


                    </asp:TableCell>

                    <asp:TableCell  ID="TableCell21" Width="40px">
					    <asp:TextBox ID="Range6End" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 55; left: 430px; position: absolute;
                        top: 210px; text-align: right" TabIndex="6" Text="Range6End" TextMode="Singleline"
                        Visible="True" Width="273" onchange="document.getElementById('Range7Start').value = document.getElementById('Range6End').value; ">
                        </asp:TextBox>
                    </asp:TableCell>

				</asp:TableRow>

                <asp:TableRow ForeColor="White" ID="TableRow8" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="TableCell22" Width="40px">
					    <asp:Label ID="Category7" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="True"
                            Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 142;
                            left: 24px; position: absolute; top: 240px; text-align: left" TabIndex="33" Text="Category 7"
                            Width="273px">
                        </asp:Label>


                    </asp:TableCell>
					
                    <asp:TableCell  ID="TableCell23" Width="40px">
					    
                        
                         <asp:TextBox ID="Range7Start" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="False" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 143; left: 120px; position: absolute;
                        top: 240px; text-align: right" TabIndex="34" Text="Range7Start" TextMode="Singleline"
                        Visible="True" Width="273" onchange="document.getElementById('Range6End').value = document.getElementById('Range7Start').value; ">
                        </asp:TextBox>


                    </asp:TableCell>

                    <asp:TableCell  ID="TableCell24" Width="40px">
					    <asp:TextBox ID="Range7End" runat="server" AutoPostBack="False" BackColor="Window"
                        BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                        Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                        Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 56; left: 430px; position: absolute;
                        top: 240px; text-align: right" TabIndex="7" Text="Range7End" TextMode="Singleline"
                        Visible="True" Width="273">
                        </asp:TextBox>
                    </asp:TableCell>

				</asp:TableRow>


			</asp:Table>



			

		</form>
	</body>
</HTML>
