// JScript File
		//Global XMLHTTP Request object
var XmlHttp;

//Creating and setting the instance of appropriate XMLHTTP Request object to a “XmlHttp” variable  
function CreateXmlHttp()
{
	//Creating object of XMLHTTP in IE
	try
	{
		XmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
	}
	catch(e)
	{
		try
		{
			XmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
		} 
		catch(oc)
		{
			XmlHttp = null;
		}
	}
	//Creating object of XMLHTTP in Mozilla and Safari 
	if(!XmlHttp && typeof XMLHttpRequest != "undefined") 
	{
		XmlHttp = new XMLHttpRequest();
	}
}
function HandleResponse()
{
	// To make sure receiving response data from server is completed
	if(XmlHttp.readyState == 4)
	{
		// To make sure valid response is received from the server, 200 means response received is OK
		if(XmlHttp.status == 200)
		{		
		 
         //sending data got from serverl to load into the search list 
		 loadVariablesIntoList(XmlHttp.responseText);
		}
		else
		{
			alert("There was a problem retrieving data from the server." );
		}
	}
}

function loadVariablesIntoList(varData)

{
    var tmpList = document.getElementById("lstVarName");
    var optionItem;
    var vlists = varData.split("|");
    variableList =new Array(vlists.length);
    tmpList.options.length=0;
//    for (var count = tmpList.length-1; count >-1; count--)
//	  {
//		tmpList.options[count] = null;
//	  }
	 for (var count = 0; count < vlists.length; count++)
	  {
   		variableList[count]= vlists[count];
   		optionItem = new Option( variableList[count], variableList[count],  false, false);
		tmpList.options[tmpList.length] = optionItem;
	}
      document.getElementById('txtVarSearch').value="";         
      document.getElementById('txtVarSearch').select(); 
      this.actb_keywords=variableList;
    
} 
function FireCategoryChange(cindx,catname)
{
var requestUrl = "LongVarS.aspx" + "?SelectedAction=categoryChanged&catIndx="+cindx+"&catName=" +catname;
    	
	CreateXmlHttp();
    if(XmlHttp)
	{
		XmlHttp.onreadystatechange = HandleResponse;
		XmlHttp.open("GET", requestUrl,  true);
    	XmlHttp.send(null);		
	}
   
}
//jrs 2011/07/08
function FireFilterChange(cindx,catname)
{
var requestUrl = "LongVarS.aspx" + "?SelectedAction=othFiltChanged&catIndx="+cindx+"&catName=" +catname;
    	
	CreateXmlHttp();
    if(XmlHttp)
	{
		XmlHttp.onreadystatechange = HandleResponse;
		XmlHttp.open("GET", requestUrl,  true);
    	XmlHttp.send(null);		
	}
   
}

function selectGroupClicked(gval)
{
    

	var requestUrl = "LongVarS.aspx" + "?SelectedAction=selectGroupClicked&gVal=" + String(gval);
    	
	CreateXmlHttp();

	// If browser supports XMLHTTPRequest object
	if(XmlHttp)
	{
	
		//Setting the event handler for the response
		//XmlHttp.onreadystatechange = HandleResponse;
	    
		//Initializes the request object with GET (METHOD of posting), 
		//Request URL and sets the request as asynchronous.

		XmlHttp.open("GET", requestUrl,  true);
	
		//Sends the request to server
		XmlHttp.send(null);		
	}
   
}
function selectDepOrInd(cname,gval)
{
    

	var requestUrl = "LongVarS.aspx" + "?SelectedAction=selectDepIndClicked&cName=" + cname +"&gVal=" + String(gval);
    	
	CreateXmlHttp();

	// If browser supports XMLHTTPRequest object
	if(XmlHttp)
	{
	
		//Setting the event handler for the response
		//XmlHttp.onreadystatechange = HandleResponse;
	    
		//Initializes the request object with GET (METHOD of posting), 
		//Request URL and sets the request as asynchronous.

		XmlHttp.open("GET", requestUrl,  true);
	
		//Sends the request to server
		XmlHttp.send(null);		
	}
   
}

function SetVarstoServerSide(commName,idpvar,dpvar,sindex,optindex)
{
	//jrs 2023/09/22
	//replace % with %25
	dpvar = dpvar.replace("%", "%25");
	idpvar = idpvar.replace("%", "%25");
	//jrs 2023/09/28
	//replace &amp; with %25
	dpvar = dpvar.replace("&amp;", "9999AND");
	idpvar = idpvar.replace("&amp;", "9999AND");

	var requestUrl = "LongVarS.aspx" + "?SelectedAction="+commName+"&idpv="+idpvar+"&dpv="+dpvar+"&sindex="+sindex+"&optind="+optindex;
	
	CreateXmlHttp();

	if(XmlHttp)
	{ 
	    requestUrl = requestUrl +"&RK=" + Math.random() * Date.parse(new Date());
		XmlHttp.open("GET", requestUrl,  true);
		XmlHttp.send(null);		
		
	}

}


function CallIDTFunction(commName) 
{
	
    var requestUrl = "LongVarS.aspx" + "?SelectedAction="+commName;
	CreateXmlHttp();
	
	// If browser supports XMLHTTPRequest object
	if(XmlHttp)
	{

		XmlHttp.open("GET", requestUrl,  true);
		
		//Sends the request to server
		XmlHttp.send(null);		
	}
}

// ############################################ FOLLOWING METHODS ARE TO HANDLE CROSS SECTIONAL FROM EVENTS #########################################3

              
function selectCrossVariables(commValue)
{

	var requestUrl = "ScatVarS.aspx" +"?selCommand="+commValue;
	
	CreateXmlHttp();

	if(XmlHttp)
	{ 
	    requestUrl = requestUrl +"&RK=" + Math.random() * Date.parse(new Date());
		XmlHttp.open("POST", requestUrl,  true);
		XmlHttp.send(null);		
		
	}

}
				
// ############################################ FOLLOWING METHODS ARE TO HANDLE Variable SECTION FROM EVENTS #########################################3

              
function varSelCategoryChanged()
{

	var requestUrl = "frm_Varsel.aspx" +"?selCommand="+ document.getElementById("cmb_VCAT").value;
	
	CreateXmlHttp();

	if(XmlHttp)
	{ 
	    requestUrl = requestUrl +"&RK=" + Math.random() * Date.parse(new Date());
	    XmlHttp.onreadystatechange = HandleResponse;
		XmlHttp.open("POST", requestUrl,  true);
		XmlHttp.send(null);		
		
	}

}
