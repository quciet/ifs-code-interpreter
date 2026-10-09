<%@ Reference Page="~/Index.aspx" %>
<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_LongVarS" Codebehind="LongVarS.aspx.vb" EnableEventValidation="false" %>


<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="cc2" Namespace="CustomDropDownList.Thycotic.Web.UI.WebControls" Assembly="CustomDropDownList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Analyze Variables Across Time</title>
		<script language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript"></script>	
		<script language="javascript" type="text/javascript" src="Scripts/actb.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/common.js"></script>
        <script language ="javascript" type="text/javascript" src="Scripts/AjaxMethodsHandler.js"></script>
         <!--JAM 2013-11-16-->
        <script language ="javascript" type="text/javascript" src="Scripts/LongVar_Vars.js"></script>

        <!--JAM 2013-11-16-->
        <script language ="javascript" type="text/javascript" src="Scripts/LongVarsFx.js"></script>

		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" onload="initLongVarsOthFilt();initLongVarsGroup();">
		
		<form runat="server" >
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Select Variables to Analyze Across Time" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
            
				
				
				
            <cswm:webmenu id="wmContext" style="Z-INDEX: 130; LEFT: 0px; POSITION: absolute; TOP: 16px" runat="server"
				HideOnMouseOut="False" HideTimer="500" MenuBar-AbsoluteDockPaddingAdjust="False" MenuBar-AbsoluteDockEnabled="False"
				MenuBar-DisplayMode="None"></cswm:webmenu>
				
                
            
            <table>
                <tr>
                    <td width=28px>
                    </td>
                    <td>
                        <asp:Label ID="lbl_VarName" runat="server" BackColor="Window" BorderStyle="None"
                        Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="19px"
                        TabIndex="2" Text="Select Control Variable and Data Table (e.g. GDP - Economic):"
                        Width="1036px"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtVarSearch" runat="server" Width="610px" autocomplete="off"></asp:TextBox>
                        <br />
                        <asp:ListBox ID="lstVarName" runat="server" Height="141px"
                        Width="611px" Rows="2000"></asp:ListBox>
                    </td>
                </tr>
            </table>

            <table>
                <tr>
                    <td width=28px>
                    </td>
                    <td width=200px>
                        <asp:label id="lbl_vcat" 
				        tabIndex="3" runat="server" Width="177" Height="17" Text="Variable Categories" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="WindowText"
				        BackColor="Window" Visible="True" BorderStyle="None"></asp:label>
                    </td>
                    <td>
                        <asp:label id="lbl_Name" 
				        tabIndex="14" runat="server" Width="128px" Height="23px" Text="Treatment of Time:" Font-Name="Arial" Font-Size="8pt" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" BackColor="Window" BorderStyle="None" Font-Names="Arial"></asp:label>
                    </td>
                    <td colspan=2>
                        <asp:RadioButtonList ID="rbTreatments" runat="server" RepeatDirection="Horizontal"
                            Width="332px" Font-Names="Arial" Font-Size="8pt" AutoPostBack="True">
                            <asp:ListItem Selected="True">As Independent Variable</asp:ListItem>
                            <asp:ListItem>As Points</asp:ListItem>
                            <asp:ListItem>As Time</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td width=28px>
                    </td>
                    <td>
                        <asp:dropdownlist id="cmb_VCAT" tabIndex="4"
				        runat="server" Width="177" Height="25" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				        Enabled="True" ForeColor="WindowText" BackColor="Window" Visible="True"></asp:dropdownlist>
                    </td>
                    <td>
                        <asp:label id="lbl_Independ" 
				        tabIndex="5" runat="server" Width="117px" Height="33" Text="Independent Variable:" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				        Font-Underline="False" Enabled="True" ForeColor="WindowText" BackColor="Window" Visible="True" BorderStyle="None"></asp:label>
                    </td>
                    <td>
                        <asp:label id="lbl_Ind" 
				        tabIndex="7" runat="server" Width="145" Height="25" Text="None" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				        Font-Underline="False" Enabled="True" ForeColor="WindowText" BackColor="Window" Visible="True" BorderStyle="None"></asp:label>
                    </td>
                    <td>
                        <asp:radiobutton id="opt_Independent" 
				        tabIndex="11" runat="server" Width="97" Height="17" Text="Independent" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText" BackColor="Window" Visible="True"
				        GroupName="frm_LongVarS" Checked="False"></asp:radiobutton>
                    </td>
                </tr>
                <tr>
                    <td width=28px>
                    </td>
                    <td>
                        <asp:Label ID="lblFilter" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="False"
                        Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="WindowText" Height="17px" TabIndex="3" Text="Other Filters"
                        Width="177px"></asp:Label>
                    </td>
                    <td>
                        <asp:label id="lbl_Depend" 
				        tabIndex="6" runat="server" Width="119px" Height="25" Text="Dependent Variable:" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="WindowText"
				        BackColor="Window" Visible="True" BorderStyle="None"></asp:label>
                    </td>
                    <td>
                        <asp:Label ID="lbl_Dep" runat="server" BackColor="Window" BorderStyle="None" Enabled="True"
                        Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Size="8.0" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="WindowText" Height="25" TabIndex="7" Text="None"
                        Visible="True" Width="145"></asp:Label>
                    </td>
                    <td>
                        <asp:radiobutton id="opt_Dependent" 
				        tabIndex="12" runat="server" Width="97" Height="17" Text="Dependent" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				        Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText" BackColor="Window" Visible="True" GroupName="frm_LongVarS" Checked="False"></asp:radiobutton>
                    </td>
                </tr>
                <tr>
                    <td width=28px>
                    </td>
                    <td>
                        <asp:dropdownlist id="cmb_OthFilt" 
				        runat="server" Width="177" Height="25" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				        Enabled="True" ForeColor="WindowText" BackColor="Window" Visible="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:label id="lbl_Control" 
				        tabIndex="9" runat="server" Width="120px" Height="33" Text="Control Variable:    (Optional)" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				        Font-Underline="False" Enabled="True" ForeColor="WindowText" BackColor="Window" Visible="True" BorderStyle="None"></asp:label>
                    </td>
                    <td>
                        <asp:label id="lbl_Cont" 
				        tabIndex="10" runat="server" Width="145" Height="25" Text="None" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True"
				        ForeColor="WindowText" BackColor="Window" Visible="True" BorderStyle="None"></asp:label>
                    </td>
                    <td>
                        <asp:radiobutton id="opt_Control" 
				        tabIndex="13" runat="server" Width="89" Height="17" AutoPostBack="True" Text="Control" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText" BackColor="Window" Visible="True"
				        GroupName="frm_LongVarS" Checked="False"></asp:radiobutton>
                    </td>
                </tr>
                <tr>
                    <td width=28px>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:label id="lbl_Countries" 
				        tabIndex="17" runat="server" Width="128px" Height="25" Text="Countries Selected?" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				        Enabled="True" ForeColor="WindowText" BackColor="Window" Visible="True" BorderStyle="None"></asp:label>
                    </td>
                    <td>
                        <asp:label id="lbl_CountSel" 
				        tabIndex="18" runat="server" Width="140px" Height="25" Text="None" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				        Enabled="True" ForeColor="WindowText" BackColor="Window" Visible="True" BorderStyle="None"></asp:label>
                    </td>
                    <td>
                        <asp:checkbox id="chk_ControlOff" 
				        tabIndex="19" runat="server" Width="105" Height="25" AutoPostBack="True" Text="Turn Off Control" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				        Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText" BackColor="Window" Visible="True"></asp:checkbox>
                    </td>
                </tr>
            </table>

            <table>
                <tr>
                    <td width=28px>
                    </td>
                    <td rowspan=2 width=200px>
                        <asp:panel id="Frame1" tabIndex="21"
				        runat="server" Width="161" Height="65px" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True"
				        ForeColor="ControlText" Visible="True">
				            <asp:RadioButton id="optNullstoZeros" tabIndex="23" runat="server" Height="13" Width="137" AutoPostBack="True"
					            BackColor="Window" ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False"
					            Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Use Nulls as Zeros"
					            Visible="True" GroupName="Frame1" Checked="False"></asp:RadioButton>
				            <BR>
				            <br>
                                <asp:RadioButton ID="optIgnoreNull" runat="server" AutoPostBack="True" BackColor="Window" Checked="True" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText" GroupName="Frame1" Height="17" tabIndex="22" Text="Ignore Nulls" Visible="True" Width="97" />
                            </br>
                        </asp:panel>
                    </td>
                    <td width=128px>
                        <asp:Button id="cmd_Countries" 
				        tabIndex="24" runat="server" Width="113" Height="25" Text="Select Countries" ToolTip="Select Countries (Alt+E)" Font-Name="Arial"
				        Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				        CausesValidation="True" Enabled="True" ForeColor="ControlText" AccessKey="e"></asp:Button>
                    </td>
                    <td width=128px>
                        <asp:Button id="cmd_Plot" 
				        tabIndex="26" runat="server" Width="113" Height="25" Text="Plot" ToolTip="Plot (Alt+P)" Font-Name="Arial" Font-Size="8.0"
				        Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" CausesValidation="True"
				        Enabled="True" ForeColor="ControlText" accessKey="p"></asp:Button>
                    </td>
                    <td>
                        <asp:Button id="cmd_Exit2" 
				        accessKey="C" tabIndex="28" runat="server" Width="113" Height="25" Text="Continue" ToolTip="Continue (Alt+C)" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				        CausesValidation="True" Enabled="True" ForeColor="ControlText" ></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td width=28px>
                    </td>
                    <td>
                        <asp:Button id="cmdUseGroups" 
				        tabIndex="24" runat="server" Width="113" Height="25" Text="Use Groups" Tooltip="Use Groups (Alt+R)" Font-Name="Arial"
				        Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				        CausesValidation="True" Enabled="True" ForeColor="ControlText" AccessKey="r"></asp:Button>
                    </td>
                    <td>
                        <asp:Button id="cmdComputations" 
				        accessKey="o" tabIndex="27" runat="server" Width="113" Height="25" Text="Computations" ToolTip="Computations (Alt+O)" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				        Font-Underline="False" CausesValidation="True" ForeColor="ControlText" ></asp:Button>
                    </td>
                    <td>
                        <asp:Button id="cmdTable" 
				        accessKey="T" tabIndex="29" runat="server" Width="113" Height="25" Text="Table" ToolTip="Table (Alt+T)" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				        CausesValidation="True" ForeColor="ControlText" ></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td width=28px>
                    </td>
                    <td></td>
                    <td>
                        <asp:Button ID="cmdSearch" runat="server" AccessKey="S" CausesValidation="True"
                        Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Size="8.0" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="ControlText" Height="25" TabIndex="29"
                        Text="Search" Width="113" Tooltip="Search (Alt+S)"/>
                    </td>
                    <td>
                        <input id="btnHelp" onclick="javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16053);"
                                type="button" value="Help" style="width: 112px" />
                    </td>
                    <td>
                     <asp:button id="cmdGeneratejs" 
				            accessKey="j" tabIndex="30" runat="server" Height="25" Width="113" ForeColor="ControlText" CausesValidation="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
				            Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Generate Javascript" ToolTip="Generate Javascript (Alt+J)"></asp:button>

                    </td>
                </tr>
            </table>
			

			<asp:Button ID="btnInvs" runat="server" Text="Hidden Command" />
            
            <!--JAM 2013-11-16 Hidden Fields used for filters-->
             <input ID="fSubSetchosen" runat="server" type="hidden" />	
             <input ID="fOthFiltchosen" runat="server" type="hidden" />	
             <input ID="fSubSetchosenName" runat="server" type="hidden" />	
             <input ID="fOthFiltchosenName" runat="server" type="hidden" />	

            	
			<script language="JavaScript" type="text/javascript">
			//jrs 2010/09/02
			document.getElementById("btnInvs").style.visibility ="hidden"	;
			
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

			
			
	         var Scat_SE; 
	    
  	        function selectDepInd(cname,sval)
	        {	      
	         selectDepOrInd(cname,sval);
	        }
	        function groupClicked()
	        {
	          var bName=document.getElementById("cmdUseGroups").value;
	         
	          if (bName =="Use G(r)oups")
	          {
	             
	            document.getElementById("cmdUseGroups").value = "Use Count(r)ies";
	            
                document.getElementById("lbl_Countries").innerHTML = "Groups Selected?";
                
                document.getElementById("cmd_Countries").value = "S(e)lect Groups";
                
                document.getElementById("cmdTable").disabled =true;
                
                selectGroupClicked("true");
	         }
	          else
	          {
	          
	            document.getElementById("cmdUseGroups").value = "Use G(r)oups";
	          
                document.getElementById("lbl_Countries").innerHTML = "Countries Selected?";
              
                document.getElementById("cmd_Countries").value = "S(e)lect Countries";
              
                document.getElementById("cmdTable").disabled = false;
              
	            selectGroupClicked("false");
	          }
	          return false;
	        } 
	         
	         
	        function setVariableFromSearchClick()
	        {
	       
    	        document.getElementById("lstVarName").value=this.actb_currObject;
                setVariable(); //call the variable select action on an item click from the list;	            
                //jrs 2010/09/02
				if (document.forms[0].cmdComputations == null)                
				{
				    document.getElementById("btnInvs").click();
				}
	         } 
	        function setVariable()
             {
                 var optDI;
                 var strData=document.getElementById("txtVarSearch").value;
                //jrs 2025/02/18
                var pos = strData.indexOf("-");
                //Check if "-" is part of the name of the variable
                var pos2 = strData.lastIndexOf("-");
                if (pos != pos2) pos = strData.indexOf("-", pos+1);
                //strData= strData.substring(0,strData.indexOf("-"));
                strData = strData.substring(0, pos);
             
                 if (document.getElementById("opt_Dependent").checked)
                 {
             
                    document.getElementById("lbl_Dep").innerHTML= strData;
                    var obj=document.getElementById("opt_Independent");
                    optDI=1;
                   if (obj!=null)
                    {
                    document.getElementById("opt_Dependent").checked=false;
                    document.getElementById("opt_Independent").checked=true;
                    }
                 }   
                 else
                 {
                    document.getElementById("lbl_Ind").innerHTML= strData;  
                    var obj=document.getElementById("opt_Dependent");
                    optDI=2;
                   if (obj!=null)
                    {
                    document.getElementById("opt_Dependent").checked=true;
                    document.getElementById("opt_Independent").checked=false;
                    }
                 }   
          
                if (document.getElementById("rbTreatments_0").checked) 
                {            
                   SetVarstoServerSide("varSelected","TIME",document.getElementById("lbl_Dep").innerHTML,0,optDI);                
                }
                else
                {                
                   if (document.getElementById("rbTreatments_1").checked)
                    SetVarstoServerSide("varSelected",document.getElementById("lbl_Ind").innerHTML,document.getElementById("lbl_Dep").innerHTML,1,optDI); 
                   else
                    SetVarstoServerSide("varSelected",document.getElementById("lbl_Ind").innerHTML,document.getElementById("lbl_Dep").innerHTML,2,optDI);               
                 }
                       
             } // end setVariable
        
             function setVariableToSearch()
             {
             var list=document.getElementById("lstVarName");
             var strData=list.options[list.selectedIndex].text;
             document.getElementById("txtVarSearch").value=  strData;
             setVariable();
             cswmHideNow();
             }
             function showAvailability()
             {
                 cswmHideNow();
                 var list=document.getElementById("lstVarName");
                 var strData=list.options[list.selectedIndex].text;
                 //jrs 2025/02/18
                 var pos = strData.indexOf("-");
                 //Check if "-" is part of the name of the variable
                 //var pos2 = strData.lastIndexOf("-");
                 var pos2 = strData.indexOf(" - ")+1;
                 if (pos != pos2) pos = strData.indexOf("-", pos + 1);
                 //strData= strData.substring(0,strData.indexOf("-"));
                 strData = strData.substring(0, pos);
                 //jrs 2025/05/27
                 strData = strData.replace("%", "%25")
             
                 window.location = "Availability.aspx?Scat_SE="+strData.toUpperCase();
             }
             function selectData() { cswmHideNow(); document.getElementById("txtVarSearch").select(); }

             //JAM 2013-11-16 Fuctions managed in LongVarsFx.js
//             function catListChange()
//             {
//               var catlist=document.getElementById('cmb_VCAT');
//               
//               if (catlist.selectedIndex>=0) 
//                 FireCategoryChange(catlist.selectedIndex,document.getElementById('cmb_VCAT').value);
//              }

//             jrs 2011/07/08             
//             function filtListChange()
//             {
//               var filtlist=document.getElementById('cmb_OthFilt');
//               
//               if (filtlist.selectedIndex>=0) 
//                 FireFilterChange(filtlist.selectedIndex,filtlist.value);
//              }
//             
//             document.getElementById("cmb_VCAT").onchange=catListChange;
//             jrs 2011/07/08
//             document.getElementById("cmb_OthFilt").onchange=filtListChange;
             /////
             document.getElementById("txtVarSearch").onclick=selectData;
	         document.getElementById("cswmItmGroup_0bd4_0").onclick=setVariableToSearch;
             document.getElementById("cswmItmGroup_0bd4_1").onclick=showAvailability;
        
             
    	     function ShowContextMenu(e)
				{
 				  var list = document.forms[0].lstVarName;

			       cswmHideNow();
                    
		            if (navigator.appName=='Netscape')
						cswmShow('Group_0bd4', '', 'below', e.pageX, e.pageY, 1);
					
					 else
						cswmShow('Group_0bd4', '', 'below', event.x + document.body.scrollLeft, event.y + document.body.scrollTop, 1);
				    
				
			    }// end of function
				
				if (document.forms[0].lstVarName != null)
				{
			
					document.forms[0].lstVarName.onclick=ShowContextMenu;
				}
             Scat_SE = document.getElementById("lbl_Dep").innerHTML;
             
             if (variableList == null){
                
                var lst=document.getElementById("lstVarName");
                variableList =new Array(lst.length);
                 for (var count = 0; count < lst.length; count++)
	                variableList[count]=lst.options[count].value;
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
                
            </script>		

            
           </form>
	</body>
</HTML>


<script type="text/javascript">
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

</script>

<script async src="https://www.googletagmanager.com/gtag/js?id=G-GLYCMH7BRR"></script>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());
    gtag('config', 'G-GLYCMH7BRR');
</script>
