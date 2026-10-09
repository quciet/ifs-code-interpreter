<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_MapImage" Codebehind="frm_MapImage.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IFs Country Map</title>
</head>
<body >
    <form id="form1" runat="server" >
    
       <asp:Image ID="Image1" runat="server"/><asp:HiddenField ID="txtDataHolder" runat="server" Value="dontshowmenu" />
    </form>
</body>
<script language ="javascript" type ="text/javascript" >

  var url=document.getElementById('txtDataHolder').value 
  var pos = url.indexOf(";");	
  var imWidth=parseFloat(url.substring(0,pos));
  var imHeight=parseFloat(url.substring(pos+1,url.length));

  
if (parseInt(navigator.appVersion)>3) {
 window.resizeTo(imWidth+32, imHeight+100);  
 if (navigator.appName=="Netscape") {
  
  winW = window.innerWidth;
  winH = window.innerHeight;
 }
 if (navigator.appName.indexOf("Microsoft")!=-1) {
 
  //window.resizeTo(imWidth+32, imHeight+50);  
  winW = parseInt(document.body.clientWidth);  //GetWidth();
  winH = parseInt(document.body.clientHeight); //GetHeight();
 
 }
}

  var left = parseInt((screen.availWidth/2) - (winW/2));
  var top = parseInt((screen.availHeight/2) - (winH/2));
  

  self.moveTo(left,top);  
  
  function GetWidth()
{
        var x = 0;
        if (self.innerHeight)
        {
                x = self.innerWidth;
        }
        else if (document.documentElement && document.documentElement.clientHeight)
        {
                x = document.documentElement.clientWidth;
        }
        else if (document.body)
        {
                x = document.body.clientWidth;
        }
        return x;
}
 
function GetHeight()
{
        var y = 0;
        if (self.innerHeight)
        {
                y = self.innerHeight;
        }
        else if (document.documentElement && document.documentElement.clientHeight)
        {
                y = document.documentElement.clientHeight;
        }
        else if (document.body)
        {
                y = document.body.clientHeight;
        }
        return y;
}
</script>
</html>
