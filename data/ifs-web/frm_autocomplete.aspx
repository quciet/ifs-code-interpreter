<%@ Reference Page="~/Index.aspx" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<%@ Register TagPrefix="cc2" Namespace="CustomDropDownList.Thycotic.Web.UI.WebControls" Assembly="CustomDropDownList" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_autocomplete" Codebehind="frm_autocomplete.aspx.vb" EnableEventValidation="false" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		
        
        <!--JAM 2015-05-08 autocomplete required files-->
        <link rel="stylesheet" href="Scripts/jquery-ui.css">
        <script language ="javascript" type="text/javascript" src="Scripts/jquery-1.10.2.js"></script>
        <script language ="javascript" type="text/javascript" src="Scripts/jquery-ui.js"></script>
        <script language ="javascript" type="text/javascript" src="Scripts/autocomplete_vars.js"></script>  
        <script language ="javascript" type="text/javascript" src="Scripts/autocompleteFx.js"></script>
        <!--<link rel="stylesheet" href="/resources/demos/style.css">-->

         <!--JAM 2015-05-08 multiselect autocomplete required files-->
         <script type="text/javascript" src="Scripts/tokenize/jquery.tokenize.js"></script>
        <link rel="stylesheet" type="text/css" href="Scripts/tokenize/jquery.tokenize.css" />
        
        <!--------------------------------------------->


        <script language ="javascript" type="text/javascript">
            $(function () {
                  $("#autocompletetxt").autocomplete({ // JAM 2015-05-08 field with autocomplete functionality
                    source: VarListvse //availableTags
                });
            });
        </script>

        <!----------------------------------->
       
      
			
		
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">	--%>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0"" onload="initautocomplete();">
		<form runat="server" >
		    <input id="txtFiles" name="txtFiles"  type="hidden" runat="server"/>
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Autocomplete Test" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 116; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" Height="48px" Width="88px" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>			
            &nbsp;
			

            
            &nbsp;&nbsp;
            
				
			
            

                <table>
                    
                    <tr>
                        <td width=28px>
                        </td>
                        <td>
                            <asp:label id="autocomplete" 
				            tabIndex="3" runat="server" Height="17px" Width="473" BorderStyle="None" Visible="True" BackColor="Window" Enabled="True" Font-Underline="False" ForeColor="WindowText" Text="Label1" Font-Strikeout="False" Font-Italic="False"
				            Font-Bold="False" Font-Size="8.0" Font-Name="Arial">Autocomplete Text</asp:label>                            
                        </td>
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td>
                            <asp:TextBox ID="autocompletetxt" runat="server" Width="606px" AutoPostBack="True"></asp:TextBox>                            
                        </td>
                    </tr>
                    
                     <tr>
                        <td width=28px>
                        </td>
                        <td>
                            <asp:label id="MutiselectLabel" 
				            tabIndex="3" runat="server" Height="17px" Width="473" BorderStyle="None" Visible="True" BackColor="Window" Enabled="True" Font-Underline="False" ForeColor="WindowText" Text="Label1" Font-Strikeout="False" Font-Italic="False"
				            Font-Bold="False" Font-Size="8.0" Font-Name="Arial">Multiselect Text</asp:label>                            
                        </td>
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td>
                             
                             <asp:listbox id="tokenize" SelectionMode="Multiple" runat="server" Height="17px" Width="473"></asp:listbox>
                             

                            <%--<select id="tokenize" multiple="multiple" class="tokenize-sample">
                                <option value="1">Dave</option>
                                <option value="2">Paul</option>
                                <option value="3">Michel</option>
                                <option value="4">Anna</option>
                                <option value="5">Eleanor</option>
                            </select>--%>
                                                  
                        </td>
                    </tr>
                    <script language ="javascript" type="text/javascript">
                        $('#tokenize').tokenize();
                     </script>
        

                </table>	
                			
				<table>
                    <tr>
                        <td colspan=4>
                        </td>
                    </tr>
                    <tr>
                  
                        <td width=150px>
                            <asp:button id="cmdGeneratejs" 
				            accessKey="D" tabIndex="15" runat="server" Height="25" Width="113" BackColor="Control" Enabled="True"
				            Font-Underline="False" ForeColor="ControlText" Text="Generate Javascript" ToolTip="Generate Javascript (Alt+J)" CausesValidation="True" Font-Strikeout="False"
				            Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" ></asp:button>
                        </td>
                    </tr>
                </table>
            
             
 
		
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
    var room = myHeight - (25 * 21.5);
    if (document.forms[0].lstVarName != null) {
        document.getElementById('lstVarName').style.height = room + "px";
    }
    if (document.forms[0].lstDIM1 != null) {
        document.getElementById('lstDIM1').style.height = room + "px";
    }

	 document.getElementById("btnInvs").style.visibility ="hidden"	;
	 document.getElementById("btnCont").style.visibility ="hidden"	;


</script>	 
	 
	 
		</form>
	</body>
</HTML>

