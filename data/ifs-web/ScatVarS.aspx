<%@ Reference Page="~/Index.aspx" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_ScatVarS" Codebehind="ScatVarS.aspx.vb" EnableEventValidation="false"%>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="cc2" Namespace="CustomDropDownList.Thycotic.Web.UI.WebControls" Assembly="CustomDropDownList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
    	<script language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript"></script>
        <script language="javascript" type="text/javascript" src="Scripts/actb.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/common.js"></script>
        <script language ="javascript" type="text/javascript" src="Scripts/AjaxMethodsHandler.js"></script>
        <!--JAM 2013-08-10-->
        <script language ="javascript" type="text/javascript" src="Scripts/ScatVars_Vars.js"></script>

        <!--JAM 2013-08-06-->
        <script language ="javascript" type="text/javascript" src="Scripts/ScatVarsFx.js"></script>
        
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">	--%>	
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0"  onload="initScatVarsOthFilt();initScatVarsGroup();"> 
  
  
		<form runat="server" > 
			
            <asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
					<a href="http:\\www.ifs.du.edu"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Select Variables to Analyze Across Countries (with Scattergram)" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
            &nbsp;
									
                <table>
                    <tr>
                        <td width=28px>
                        </td>
                        <td colspan=3 width=602>
                            <asp:label id="lbl_VarName" 
				            tabIndex="2" runat="server" Height="25" Width="472px" BackColor="Window" ForeColor="WindowText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Variable Names" Visible="True" BorderStyle="None"></asp:label>
                        </td>                        
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td colspan=3 width=602>
    		                <asp:TextBox ID="txtVarSearch" runat="server" Width="606px" autocomplete="off"></asp:TextBox><br />
                            <asp:listbox id="lstVarName" runat="server" Height="88px" Width="608px" ></asp:listbox>

                            <%------------JAM 2013-10-12-------------%>
                             <asp:HiddenField ID="HiddenField1" runat="server" />
                           

                        </td>                        
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td colspan=3 width=602>
                            <asp:label id="lbl_Year" 
    				        tabIndex="12" runat="server" Height="17" Width="113" BackColor="Window" ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Year" Visible="True" BorderStyle="None">Please Select Year</asp:label>
                            <br />
			                <cc2:customdropdownlist id="cmb_YearSelection" 
				            tabIndex="1" runat="server" AutoPostBack="True" Width="121px" Height="24px"></cc2:customdropdownlist><br />
                            <asp:ListBox id="lstDIM1" runat="server"
				            AutoPostBack="True" Width="120px" Height="72px" Visible="False"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td width=240px>
                            <asp:label id="lbl_vcat" 
				            tabIndex="3" runat="server" Height="17" Width="177" BackColor="Window" ForeColor="WindowText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Variable Categories" Visible="True" BorderStyle="None"></asp:label>
                        </td>
                        <td width=185px>
                            <asp:label id="lbl_Dep" 
				            tabIndex="6" runat="server" Height="25" Width="185" BackColor="Window" ForeColor="WindowText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="None" Visible="True" BorderStyle="None"></asp:label>
                        </td>
                        <td >
                            <asp:radiobutton id="opt_Dependent" 
				            tabIndex="9" runat="server" Height="17" Width="177" BackColor="Window" ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Dependent Variable"
				            AutoPostBack="True" GroupName="frm_ScatVarS" Visible="True" Checked="True"></asp:radiobutton>
                        </td>
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td width=240px>
				          <%--  JAM 2013-08-10 Commented in order to test javascript implementation
                          <asp:dropdownlist id="cmb_VCAT" tabIndex="4"
				            runat="server" Height="25" Width="177" BackColor="Window" ForeColor="WindowText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" AutoPostBack="True" Visible="True"></asp:dropdownlist>--%>
                             <asp:dropdownlist id="cmb_VCAT" tabIndex="4"
				            runat="server" Height="25" Width="177" BackColor="Window" ForeColor="WindowText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"  Visible="True" ></asp:dropdownlist>
                        </td>
                        <td width=185px>
                            <asp:label id="_lbl_Ind_0" 
				            tabIndex="5" runat="server" Height="25" Width="185" BackColor="Window" ForeColor="WindowText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="None" Visible="True" BorderStyle="None"></asp:label>
                        </td>
                        <td >
                            <asp:radiobutton id="_opt_Independent_0" 
				            tabIndex="8" runat="server" Height="17" Width="201" BackColor="Window" ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
				            Font-Size="8.0" Font-Name="Arial" Text="Independent" AutoPostBack="True" GroupName="frm_ScatVarS" Visible="True" Checked="False"></asp:radiobutton>
                        </td>
                        
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td width=240px>
                            <asp:Label ID="lblFilter" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="False"
                            Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="WindowText" Height="17px" TabIndex="3" Text="Other Filters"
                            Width="177px"></asp:Label>
                        </td>
                        <td width=185px>
                            <asp:label id="_lbl_Ind_1" 
				            tabIndex="16" runat="server" Height="25" Width="145" BackColor="Window" ForeColor="WindowText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="None" Visible="True" BorderStyle="None"></asp:label>
                        </td>
                        <td >
                            <asp:radiobutton id="_opt_Independent_1" 
				            tabIndex="13" runat="server" Height="17" Width="201" BackColor="Window" ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"
				            Text="Independent" AutoPostBack="True" GroupName="frm_ScatVarS" Visible="True" Checked="False"></asp:radiobutton>
                        </td>
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td width=240px>
                            <asp:DropDownList ID="cmb_OthFilt" runat="server" BackColor="Window" Enabled="True"
                                Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Size="8.0" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="WindowText" Height="25" TabIndex="4" Visible="True" Width="177" >
                            </asp:DropDownList>
                             <%--<asp:DropDownList ID="cmb_OthFilt" runat="server" BackColor="Window" Enabled="True"
                                Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Size="8.0" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="WindowText" Height="25" TabIndex="4" Visible="True" Width="177" AutoPostBack="True">
                            </asp:DropDownList>--%>

                        </td>
                        <td width=185px>
                            <asp:label id="_lbl_Ind_2" 
				            tabIndex="17" runat="server" Height="25" Width="193" BackColor="Window" ForeColor="WindowText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="None" Visible="True" BorderStyle="None"></asp:label>
                        </td>
                        <td >
                            <asp:radiobutton id="_opt_Independent_2"
				            tabIndex="14" runat="server" Height="17" Width="201" BackColor="Window" ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
				            Font-Name="Arial" Text="Independent" AutoPostBack="True" GroupName="frm_ScatVarS" Visible="True" Checked="False"></asp:radiobutton>
                        </td>
                    </tr>
                    <tr>
                       <%-- <td width=28px>
                        </td>
                        <td width=240px>
                        </td>--%>
                        <td width=28px>
                        </td>
                        <td width=240px>
                            <asp:Label ID="lblAllYears" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="False"
                            Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                            Font-Underline="False" ForeColor="WindowText" Height="17px" TabIndex="3" Text="Analysis"
                            Width="177px"></asp:Label>
                        </td>
                        <td width=185px>
                            <asp:label id="_lbl_Ind_3" 
				            tabIndex="18" runat="server" Height="25" Width="145" BackColor="Window" ForeColor="WindowText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="None" Visible="True" BorderStyle="None"></asp:label>
                        </td>
                        <td >
                            <asp:radiobutton id="_opt_Independent_3" 
				            tabIndex="15" runat="server" Height="17" Width="209" BackColor="Window" ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"
				            Text="Independent" AutoPostBack="True" GroupName="frm_ScatVarS" Visible="True" Checked="False"></asp:radiobutton>
                        </td>
                    </tr>
                    <tr>
                       <%-- <td width=28px>
                        </td>
                        <td width=240px>
                        </td>--%>
                        <td width=28px>
                        </td>
                        <td width=240px>
                            <asp:DropDownList ID="cmb_AllYears" runat="server" BackColor="Window" Enabled="True"
                                Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Size="8.0" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="WindowText" Height="25" TabIndex="4" Visible="True" Width="177" >
                                <asp:ListItem Value="0" Selected=True>None</asp:ListItem>
                                <asp:ListItem Value="1">Use All Years(In Statistics)</asp:ListItem>
                                <asp:ListItem Value="2">Use All Years and Time(In Statistics)</asp:ListItem>
                            </asp:DropDownList>
                        <td width=185px>
                            <asp:label id="_lbl_Ind_4" 
				            tabIndex="19" runat="server" Height="25" Width="193" BackColor="Window" ForeColor="WindowText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="None" Visible="True" BorderStyle="None"></asp:label>
                        </td>
                        <td >
				            <asp:radiobutton id="_opt_Independent_4" 
				            tabIndex="20" runat="server" Height="17" Width="209" BackColor="Window" ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
				            Font-Name="Arial" Text="Independent" AutoPostBack="True" GroupName="frm_ScatVarS" Visible="True" Checked="False"></asp:radiobutton>
                        </td>
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td width=240px>
                        </td>
                        <td width=185px>
                            <asp:label id="lbl_Cont" 
				            tabIndex="7" runat="server" Height="25" Width="193" BackColor="Window" ForeColor="WindowText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="None" Visible="False" BorderStyle="None">None</asp:label>
                        </td>
                        <td >
                            <asp:radiobutton id="opt_Control" 
				            tabIndex="10" runat="server" Height="33" Width="169" BackColor="Window" ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
				            Font-Name="Arial" Text="Control Variable (Optional)" AutoPostBack="True" GroupName="frm_ScatVarS" Visible="False" Checked="False"></asp:radiobutton>
                        </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td width=200px>
                        </td>
                        <td width=150px>
                            <asp:button id="cmd_Plot" style="Z-INDEX: 106; TEXT-ALIGN: center"
				            tabIndex="21" runat="server" Height="25" Width="113" ForeColor="ControlText" Enabled="True" CausesValidation="True" 
				            Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" 
				            Font-Name="Arial" Text="Plot" accessKey="p" ToolTip="Plot (Alt+P)"></asp:button>
                        </td>
                        <td width=150px>
                            <asp:button id="cmd_Statistics" 
				            tabIndex="23" runat="server" Height="25" Width="113" ForeColor="ControlText" 
                            CausesValidation="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
				            Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Statistics" accessKey="s" 
                            ToolTip="Statistics (Alt+S)"></asp:button>
                        </td>
                        <td width=150px>
                            <asp:button id="cmd_Exit2" 
				            accessKey="C" tabIndex="24" runat="server" Height="25" Width="113" ForeColor="ControlText"
				            Enabled="True" CausesValidation="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
				            Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Continue" ToolTip="Continue (Alt+C)"></asp:button>
                        </td>                        
                    </tr>
                    <tr>
                        <td width=200px>
                        </td>
                        <td width=150px>
				            <asp:button id="cmdComputations" 
				            accessKey="o" tabIndex="22" runat="server" Height="25" Width="113" ForeColor="ControlText" CausesValidation="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
				            Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Computations" ToolTip="Computations (Alt+O)"></asp:button>
                        </td>
                        <td width=150px>
        				    <asp:button id="cmdSearch" 
				            tabIndex="23" runat="server" Height="25" Width="113" ForeColor="ControlText" CausesValidation="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
				            Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Search" accessKey="e" ToolTip="Search (Alt+E)"></asp:button>
                        </td>
                        <td width=150px>
				            <input id="btnHelp" onclick="javascript:ShowHelp();"
                            type="button" value="Help" style="width: 113px; height: 25px; font-size: 11px; font-weight: normal;"8.0" />

                        </td>                        
                    </tr>
                     <tr>  <!--JAM 2013-08-06-->
                        <td width=200px>
                        </td>
                        <td width=150px>
				            <asp:button id="cmdGeneratejs" 
				            accessKey="j" tabIndex="22" runat="server" Height="25" Width="113" ForeColor="ControlText" CausesValidation="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
				            Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Generate Javascript" ToolTip="Generate Javascript (Alt+J)"></asp:button>
                        </td>
                        <td width=150px>
        				  
                        </td>
                        <td width=150px>
				           
                        </td>                        
                    </tr>
                </table>



            &nbsp;
<aiswc:focuscontrol id="WinFormsToWeb_FocusControl" style="Z-INDEX: 125; LEFT: 632px; POSITION: absolute; TOP: 496px"
				runat="server"></aiswc:focuscontrol>
                         
         <cswm:webmenu id="wmContext" style="Z-INDEX: 128; LEFT: 0px; POSITION: absolute; TOP: 16px" runat="server"
				HideOnMouseOut="False" HideTimer="500" MenuBar-AbsoluteDockPaddingAdjust="False" MenuBar-AbsoluteDockEnabled="False"
				MenuBar-DisplayMode="None"></cswm:webmenu>
         <asp:Button ID="btnInvs" runat="server" Text="Hidden Command" />	
         <asp:Button ID="btnEnterKey" runat="server" Text="Hidden Command" />		
         <input ID="fSubSetchosen" runat="server" type="hidden" />	
         <input ID="fOthFiltchosen" runat="server" type="hidden" />	
         <input ID="fSubSetchosenName" runat="server" type="hidden" />	
         <input ID="fOthFiltchosenName" runat="server" type="hidden" />	

        
               
         		
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            
            
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp;
<script language="javascript" type="text/javascript" >

    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }
    //jrs 2011/03/22
    if (myWidth == 0) {
        myWidth = screen.width;
        myHeight = screen.height;
    }

    //resize the variable list box
    var room = myHeight - (25 * 14.5);
    if (document.forms[0].lstVarName != null) {
        document.getElementById('lstVarName').style.height = room + "px";
    }
    if (document.forms[0].lstDIM1 != null) {
        document.getElementById('lstDIM1').style.height = room + "px";
    }

    //JAM 2013-09-10  Code for keeping chosen filter
//    //if (typeof (jSubSetchosen) != "undefined") {
//        document.forms[0].cmb_VCAT.selectedIndex = jSubSetchosen;
//    }
//

    //JAM 2013-09-15

//    if (document.forms[0].fSubSetchosen.value != null) {
//        document.forms[0].cmb_VCAT.selectedIndex = document.forms[0].fSubSetchosen.value;
//    }

    
    document.getElementById("btnInvs").style.visibility="hidden";
	//JAM 2011-11-19 Defining default button for txtVarSearch field
	document.getElementById("btnEnterKey").style.visibility="hidden";
	 
	 //JAM 2011-11-19 Defining default button for txtVarSearch field
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
	 
	
	if (document.forms[0].cmb_YearSelection != null)
	{
		if (document.forms[0].cmb_YearSelection.captureEvents!=null)
				document.forms[0].cmb_YearSelection.captureEvents(Event.KEYPRESS)
		document.forms[0].cmb_YearSelection.onkeypress = updateYear;  
	}
    
  		   function setVariableFromSearchClick()
	         {
	         //JAM 2011-09-28   TFR exception isn't necessary for blank spaces managing
//	          if (this.actb_currObject=="TFR - Total fertility rate: children per woman - Number")
//	           {
//	             this.actb_currObject="TFR - Total fertility rate: children per woman - Number";
//	           }
	          document.getElementById("lstVarName").value=this.actb_currObject;
	          document.getElementById("btnInvs").click();
	         
	         }
				
				
				// Function which shows the Context Menu by calling
				// cswmShow and passing in coordinates.
				function ShowContextMenu(e)
				{
					 cswmHideNow();
					 document.getElementById("txtVarSearch").value= document.getElementById("lstVarName").value;

	                
					if (navigator.appName=='Netscape')
					
						cswmShow('Group_0bd4', '', 'above', e.pageX, e.pageY, 1);
					// IE
					else
						cswmShow('Group_0bd4', '', 'above', event.x + document.body.scrollLeft, event.y + document.body.scrollTop, 1);
				}
				
				
				if (document.forms[0].lstVarName != null)
				{
					
					document.forms[0].lstVarName.onclick=ShowContextMenu;
				}
function selectData(){  cswmHideNow();  
document.getElementById("txtVarSearch").select();}
	document.getElementById("txtVarSearch").onclick=selectData;		
	
			
			function showhg() {
			var doc = document.forms[0];
		
			document.body.style.cursor = "wait";
			//doc.cmd_Exit.value = "Please Wait....";
			doc.cmd_Statistics.style.cursor = "wait";
			
			}
        if (variableList == null){
               if (document.forms[0].lstVarName!= null)
                { 
                var lst=document.getElementById("lstVarName");
                variableList =new Array(lst.length);
               
                 for (var count = 0; count < lst.length; count++)
                 {
	                 variableList[count]=lst.options[count].innerHTML;
	                  
	             }
	            }
            }
            
             function BindDataToVarSearch()
             {     
                var obj = actb(document.getElementById('txtVarSearch'),variableList);
                //KIA add the follwing event listener with your desired method what you want to call on a search list item is clicked
                this.actb_cur_obj_clicked.prototype.itemClicked = setVariableFromSearchClick;
                this.actb_fSize = '12px';
                this.actb_bgColor = '#888888';
                this.actb_textColor = '#FFFFFF';
                this.actb_hColor = '#000000';
                this.actb_fFamily = 'Verdana';
                this.actb_lim =5;
                this.actb_timeOut = -1;
                this.actb_firstText =true;
                this.actb_hStyle = 'text-decoration:underline;font-weight="bold"';  
                document.getElementById('txtVarSearch').value="";         
                document.getElementById('txtVarSearch').select();   
                
              }
              BindDataToVarSearch();


              //////////////////////////////////////////
              
                ///////////////////////////////////	
                
			</script>
			
		</form>
	</body>
</HTML>


<script async src="https://www.googletagmanager.com/gtag/js?id=G-GLYCMH7BRR"></script>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());
    gtag('config', 'G-GLYCMH7BRR');
</script>
