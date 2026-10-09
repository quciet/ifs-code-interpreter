<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_CurrencyOpt" Codebehind="frm_CurrencyOpt.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
		<title>IFsWeb</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
    <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
</head>
<body LeftMargin="0" RightMargin="0" topmargin="0">
    <form id="form1" runat="server">
			<asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size=Medium Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Currency Setup" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<cswm:WebMenu id="cswmWebMenu" style="Z-INDEX: 110; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:WebMenu>
        <asp:Label ID="Label1" runat="server" Style="z-index: 100; left: 216px; position: absolute;
            top: 104px" Text="Currency Country Name:" Font-Bold="True" ></asp:Label>
        &nbsp;

        <asp:Label ID="Label3" runat="server" Style="z-index: 104; left: 216px; position: absolute;
            top: 216px" Text="Currency Value (in US Dollars):" Font-Bold="True" ></asp:Label>
        <asp:Label ID="Label2" runat="server" Style="z-index: 105; left: 216px; position: absolute;
            top: 144px" Text="Currency Symbol:" Font-Bold="True"></asp:Label>
        <asp:Label ID="Label4" runat="server" Style="z-index: 105; left: 216px; position: absolute;
            top: 176px" Text="Currency Name:" Font-Bold="True"></asp:Label>

        <asp:DropDownList ID="cboCountry" runat="server" AutoPostBack="True" Font-Bold="True"
            Style="z-index: 101; left: 528px; position: absolute; top: 104px" Width="152px"
            TabIndex="1">
        </asp:DropDownList>

        <asp:TextBox ID="txtCurrencySymbol" runat="server" Style="z-index: 106; left: 528px;
            position: absolute; top: 144px" Font-Bold="True" TabIndex="2"></asp:TextBox>
            
        <asp:TextBox ID="txtCurrencyName" runat="server" Style="z-index: 106; left: 528px;
            position: absolute; top: 176px" Font-Bold="True" TabIndex="2"></asp:TextBox>

        <asp:TextBox ID="txtCurrencyValue" runat="server" Style="z-index: 107; left: 528px;
            position: absolute; top: 216px" Font-Bold="True" TabIndex="3"></asp:TextBox>

        <asp:RadioButton ID="optValsInDollars" runat="server" AccessKey="u" AutoPostBack="True"
            Checked="True" Font-Bold="True" Style="z-index: 102; left: 216px; position: absolute;
            top: 256px; width: 290px;" Text="(U)S Dollars per unit of Currency" 
                TabIndex="4" GroupName="Currency" />
            
        <asp:RadioButton ID="optValsinCurrency" runat="server" AccessKey="c" AutoPostBack="True"
            Font-Bold="True" Style="z-index: 103; left: 528px; position: absolute; top: 256px"
            Text="(C)urrency per unit of US Dollars" TabIndex="5" GroupName="Currency" />
            
        <a href="http://www.x-rates.com/calculator.html" target="_blank" 
        Style="left: 256px; position: absolute; top: 280px">
        Visit www.x-rates.com/calculator.html for current currency rates.</a>
        <br />

        <script language="JavaScript"  type="text/javascript">

	        document.forms[0].cboCountry.focus();

            //ValidatePositiveNumbers();
            
//                function IsNumeric(strString)
//               //  check for valid numeric strings	
//               {
//               var strValidChars = "0123456789."; //removed - to avoid negatives
//               var strChar;
//               var blnResult = true;

//               if (strString.length == 0) return false;
//               
//               //check that is not 0
//               var bAllZeros = true;

//               //  test strString consists of valid characters listed above
//               for (i = 0; i < strString.length && blnResult == true; i++)
//                  {
//                  strChar = strString.charAt(i);
//                  if (strValidChars.indexOf(strChar) == -1)
//                     {
//                     blnResult = false;                     
//                     }
//                  if (strChar <> "0" && bAllZeros)
//                    {
//                        bAllZeros = false;
//                    }
//                  }
//                  
//                if bAllZeros
//                    {
//                        blnResult = false;
//                    }
//               return blnResult;
//               }

//        function ValidatePositiveNumbers()
//            {
//                if (document.forms[0].elements["txtCurrencyValue"].value.length == 0) 
//                      {
//                      alert("Please enter a value.");
//                      } 
//                   else if (chkNumeric(document.forms[0].elements["txtCurrencyValue"].value.value) == false) 
//                      {
//                      alert("Please check - non numeric value!, if not changed 1 will be used instead.");
//                      }
//            }
           

		</script>	

    </form>
</body>
</html>
