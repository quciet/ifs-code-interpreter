<%@ Reference Page="~/ErrorHandler.aspx" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_CountSel" Codebehind="CountSel.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Country/Group Selection</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body leftMargin="0" rightMargin="0" topmargin="0" onload="javascript:InitForm();">
		<form runat="server" onsubmit="javascript:saveSelLocations();">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Select Country(ies)" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<asp:Panel ID="frmGroupOpt" runat="server" Height="116px" Style="z-index: 100; left: 467px;
                position: absolute; top: 308px" Width="183px" Font-Names="Arial">
                <asp:RadioButton ID="optReg" runat="server" AccessKey="r"  Checked="True"
                    Style="z-index: 100; left: 8px; position: absolute; top: 8px" Text="Country" GroupName="GroupingOptions" ToolTip="Loads single Country (Alt+R)" />
                <asp:RadioButton ID="optGroup" runat="server" AccessKey="g"  Style="z-index: 101;
                    left: 8px; position: absolute; top: 34px" Text="Groups" GroupName="GroupingOptions" ToolTip="Loads single Group (Alt+G)" />
                <asp:RadioButton ID="optGroupDec" runat="server" AccessKey="e" 
                    Style="z-index: 102; left: 7px; position: absolute; top: 60px" Text="Decomp. Groups" GroupName="GroupingOptions" ToolTip="Decomposes a Group into its Member Countries (Alt+E)" />
                <asp:RadioButton ID="optProvinces" runat="server" AccessKey="S"  Style="z-index: 104;
                    left: 8px; position: absolute; top: 87px" Text="State/Provinces" GroupName="GroupingOptions" ToolTip="Loads a predefined list of Countries and/or Groups (Alt+S)" Visible="False" />
            </asp:Panel>
			
			<asp:listbox id="lst_Countries" style="Z-INDEX: 100; LEFT: 27px; POSITION: absolute; TOP: 102px"
				runat="server" SelectionMode="Multiple" BackColor="Window" Width="188" Height="338px" Font-Bold="True" Font-Names="Arial" Font-Size="9.75pt"></asp:listbox>
			<asp:listbox id="lst_selections" style="Z-INDEX: 101; LEFT: 464px; POSITION: absolute; TOP: 112px"
				tabIndex="3" runat="server" Height="177px" SelectionMode="Multiple" BackColor="Window" Font-Name="Arial"
				Font-Size="9.75" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				Enabled="True" ForeColor="WindowText" Visible="True" BorderStyle="Solid"
				Width="185"></asp:listbox>
            &nbsp;
			<asp:button id="cmd_Exit2" style="Z-INDEX: 103; LEFT: 62px; POSITION: absolute; TOP: 458px; TEXT-ALIGN: center"
				accessKey="x" tabIndex="5" runat="server" Width="153" Height="25" Text="Continue" ToolTip="Continue (Alt+X)" 
				Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" CausesValidation="True" Enabled="True" ForeColor="ControlText" ></asp:button>
			<asp:dropdownlist id="lst_Countries1" style="Z-INDEX: 104; LEFT: 24px; POSITION: absolute; TOP: 71px"
				tabIndex="1" runat="server" Width="193" Height="259" BackColor="Window" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True"
				ForeColor="WindowText" Visible="False" AutoPostBack="True"></asp:dropdownlist>
			<asp:label id="Label1" style="Z-INDEX: 105; LEFT: 464px; POSITION: absolute; TOP: 40px; TEXT-ALIGN: left"
				tabIndex="4" runat="server" Width="193" Height="17" Text="Selections:" BackColor="Window"
				Font-Name="Arial" Font-Size="9.75" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" Enabled="True" ForeColor="WindowText" Visible="True" BorderStyle="None"></asp:label>
			<asp:label id="lbl_countries" style="Z-INDEX: 106; LEFT: 24px; POSITION: absolute; TOP: 39px; TEXT-ALIGN: left"
				tabIndex="2" runat="server" Width="193" Height="17" Text="Countries:  Select Choices" BackColor="Window"
				Font-Name="Arial" Font-Size="9.75" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" Enabled="True" ForeColor="WindowText" Visible="True" BorderStyle="None"></asp:label>
			<script language="javascript">
{
	if ((document.forms[0].lst_Countries != null)&& (document.forms[0].lst_Countries.disabled == false ) ) 
          {
               document.forms[0].lst_Countries.focus();
          }
}

                    function optGroup_Click()
                    {
                        var bSkip = bUseGroupDec;

                        bUseReg = false;
                        bUseGroup = true;
                        bUseGroupDec = false;
				        var doc = document.forms[0];
                        doc.btnSelect.style.display= "none";
                        doc.cmdSelect.style.display= "block";
                        
                        if (bSkip == false)
                        {
                            //Load Dimensions
                            LoadDims();
                        }
                    }
                    
                    function optGroupDec_Click()
                    {
                        var bSkip = bUseGroup;

                        bUseReg = false;
                        bUseGroup = false;
                        bUseGroupDec = true;
				        var doc = document.forms[0];
                        doc.btnSelect.style.display= "block";
                        doc.cmdSelect.style.display= "none";

                        if (bSkip == false)
                        {
                            //Load Dimensions
                            LoadDims();
                        }
                    }

                    function optReg_Click()
                    {
                        bUseReg = true;
                        bUseGroup = false;
                        bUseGroupDec = false;
				        var doc = document.forms[0];
                        doc.btnSelect.style.display= "none";
                        doc.cmdSelect.style.display= "block";

                        //Load Dimensions
                        LoadDims();
                    }

                    function LoadDims()
				    {
				        var i=0;
				        var doc = document.forms[0];
				        doc.lst_Countries.length = 0;
				        
				        if (bUseGroup || bUseGroupDec)
                        {
                            //Load Groups
                            for (i=1; i<=iGroupCount; i++)
                            {
                                var opt = document.createElement("option");
                                opt.text=Groups[i];
                                opt.value=Groups[i];
                                
                                 try {
                                    doc.lst_Countries.add(opt, null); // standards compliant; doesn't work in IE
                                }
                                catch(ex) {
                                    doc.lst_Countries.add(opt); // IE only
                                }                                    
                            }
                        }
                        else
                        { 
                            //Load Countries
                            for (i=1; i<=iCCount; i++)
                            {
                                var opt = document.createElement("option");
                                opt.text=CountryName[i];
                                opt.value=CountryName[i];
                                
                                 try {
                                    doc.lst_Countries.add(opt, null); // standards compliant; doesn't work in IE
                                }
                                catch(ex) {
                                    doc.lst_Countries.add(opt); // IE only
                                }                                    
                            }                            
                        }
                        doc.lst_Countries.selectedIndex=0;
				    }
				    
				    function cmdSelect_click()
                    {
                        var i=0;
                        var doc = document.forms[0];
                        for (i=0; i<doc.lst_Countries.length; i++)
                        {
                            if (doc.lst_Countries[i].selected)
				            {
				                if (doc.lst_selections.length>=NR)
				                {
				                    alert("You may not select more than " + NR + " countries for analysis.");
				                    i=doc.lst_Countries.length;
				                }
				                else
				                {
                                    var opt = document.createElement("option");
                                    opt.text=doc.lst_Countries[i].text;
                                    opt.value=doc.lst_Countries[i].text;
                                    
                                     try {
                                        doc.lst_selections.add(opt, null); // standards compliant; doesn't work in IE
                                    }
                                    catch(ex) {
                                        doc.lst_selections.add(opt); // IE only
                                    }                                    				            
                                }
				            }
                        }   
                    }

				    function cmdUnSelect_click()
                    {
                        var i=0;
                        var doc = document.forms[0];
                        for (i=doc.lst_selections.length-1; i>=0; i--)
                        {
                            if (doc.lst_selections[i].selected)
				            {
                                doc.lst_selections.remove(i);
				            }
                        }   
                    }
                    
                    function InitForm()
                    {
                        LoadDims();
                        var doc = document.forms[0];
                        doc.lst_selections.length=0;
                        if (bUseGroup)
                        {
                            doc.optGroup.checked=true;
                            doc.optReg.checked=false;
                        }
                        else if (bUseGroupDec)
                        {
                            doc.optGroupDec.checked=true;
                            doc.optReg.checked=false;   
                            doc.btnSelect.style.display= "block";
                            doc.cmdSelect.style.display= "none";                                                 
                        }
                        
                        loadSelLocations();
                    }
                    
                    function saveSelLocations()
                    {
                        var i=0;
                        var doc = document.forms[0];
                        doc.txtSelLocations.value="";
                        
                        for (i=0; i<doc.lst_selections.length; i++)
                        {
                            if (doc.txtSelLocations.value>"")
                            {
                                doc.txtSelLocations.value=doc.txtSelLocations.value + ";";
                            }
                            doc.txtSelLocations.value=doc.txtSelLocations.value + doc.lst_selections[i].text;
                        }
                    }

                    function loadSelLocations()
                    {
                        var i=0;
                        var doc = document.forms[0];
                        var f = new Array();
                        f = doc.txtSelLocations.value.split(";");
                        for (i=0; i<f.length; i++)
                        {
                            if (f[i]>"")
                            {
                                var opt = document.createElement("option");
                                opt.text=f[i];
                                opt.value=f[i];
                                
                                 try {
                                    doc.lst_selections.add(opt, null); // standards compliant; doesn't work in IE
                                }
                                catch(ex) {
                                    doc.lst_selections.add(opt); // IE only
                                }                                    				            
                            }
                        }
                        
                        //set focus to last if available
                        if (bUseGroupDec && doc.txtLastDecGroup.value>"")
                        {
                            for (i=0; i<doc.lst_Countries.length; i++)
                            {
                                if (doc.lst_Countries[i].text==doc.txtLastDecGroup.value)
                                {
                                    doc.lst_Countries.selectedIndex=i;
                                    i=doc.lst_Countries.length;
                                }
                            }
                        }
                    }
                    
                    function ClearSel()
                    {
                        var doc = document.forms[0];
                        doc.lst_selections.length=0;
                    }
			</script>
			<asp:button id="btnSelect" style="Z-INDEX: 107; LEFT: 336px; POSITION: absolute; TOP: 136px; display:none"
				runat="server" Width="30px" Height="20px" Text=">" ></asp:button>
            &nbsp;
				
			<input id="cmdClear" type="button" style="Z-INDEX: 109; LEFT: 61px; POSITION: absolute; TOP: 502px; TEXT-ALIGN: center; width: 153px; height: 25px "
				accessKey="c" tabIndex="5" runat="server" value="Clear" title="Clear (Alt+C)" onclick="ClearSel();">
            <input id="cmdSelect" type="button" value=">" onclick="cmdSelect_click();" style="Z-INDEX: 107; LEFT: 336px; POSITION: absolute; TOP: 136px; Width:30px; Height:20px; display:block"/>
            <input id="cmdUnSelect" type="button" value="<" onclick="cmdUnSelect_click();" style="Z-INDEX: 102; LEFT: 336px; POSITION: absolute; TOP: 184px; Width:30px; Height:20px "/>
	        <input id="txtSelLocations" name="txtSelLocations"  type="hidden" runat="server"/>
	        <input id="txtLastDecGroup" name="txtLastDecGroup"  type="hidden" runat="server"/>
        </form>
	</body>
</HTML>
