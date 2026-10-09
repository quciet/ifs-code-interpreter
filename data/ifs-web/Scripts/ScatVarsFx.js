//JAM 2013-08-06 Functions used for ScarVars.aspx form, code based on FlexDisplayFx developed by Jose

//JAM 2013-08-06 Function for filter property
function cmb_VCAT_click() {
 //filter categories
 
    var doc = document.forms[0];
    var i = 0;
	var jLoadCovatra = "LoadCoVaTrA";
	//var vGroupSelected = doc.cmb_VCAT[doc.cmb_VCAT.selectedIndex].text
	
	var jOthFiltchosen=0;
    
	var jSubSetchosen= 0;
	//JAM 2013-12-28 Validating if the navigator version is previous to IE 10
	var glbPrevIEVersion = isPreviousVerIE();
	

	
	if (doc.cmb_VCAT[0].selected) {
		vGroupSelected = "All";
	}
	else {
	
		vGroupSelected = doc.cmb_VCAT[doc.cmb_VCAT.selectedIndex].text;
		}

	//JAM 2013--12-10 In orde to optimize in IE11		
	//if (navigator.appName == 'Microsoft Internet Explorer')	{
		/*var vselect = document.getElementById("cmb_VCAT");

		for (i = 0; i < NumVars; i++) {
		  vselect.options[i] = null;
		}*/
	
		//doc.lstVarName.length = 0;
		//document.getElementById("lstVarName").options.length = 0;
		if (navigator.appName == 'Microsoft Internet Explorer')	{
		/*var vselect = document.getElementById("cmb_VCAT");

		for (i = 0; i < NumVars; i++) {
		  vselect.options[i] = null;
		}*/
	
		//doc.lstVarName.length = 0;
		//document.getElementById("lstVarName").options.length = 0;
		
		document.getElementById("lstVarName").innerHTML = ""; 
		
		}
		else {
			//JAM 2013-12-14	For IE<11
			if (navigator.appName == 'Netscape')	{
				document.getElementById("lstVarName").innerHTML = "";
			}
			else {
				doc.lstVarName.length = 0;
			}	
			
		}
		
	/*}
	else {
		doc.lstVarName.length = 0;
	}*/
    if (doc.cmb_VCAT[0].selected) {  // ALL Category selected
	    for (i = 0; i < NumVars; i++) {
			if ((jModelLoad = jLoadCovatra.toUpperCase()) || (jScat_GroupNames != "Automobile") ) {
		   
			
				if (jbInclude[i] = "True") {
					var opt = document.createElement("option");
					opt.text = VarList[i];
					opt.value = VarList[i];
					
					//JAM 2013-12-14	For IE<11
					if (navigator.appName == 'Microsoft Internet Explorer')	{
						//JAM 2013-12-10 In order to optimize in IE11
						//JAM 2013-12-26 Navigator is lower than 10 version
						if (glbPrevIEVersion == 1) { 
							
							doc.lstVarName.add(opt);
						}
						else {
							doc.lstVarName.appendChild(opt); 
						}
					}
					else {
						 doc.lstVarName.appendChild(opt); 
					}	 
					/*try {
						doc.lstVarName.add(opt, null); // standards compliant; doesn't work in IE
					}
					catch (ex) {
						doc.lstVarName.add(opt); // IE only
					}*/
				}	
			}	
        }
    }	
    else //Category Selected
    {		//JAM 2013-08-10
			//NumVars
        for (i = 0; i < NumVars; i++) {
				var vCountGroup = NumGroups[i];
							
				for (var j = 0; j < vCountGroup; j++) {
					
					if ((jModelLoad = jLoadCovatra.toUpperCase()) || (jScat_GroupNames != "Automobile")) {
						if (jbInclude[i] = "True") {
							 //var vtemp = FullGroups[i][j];
							if (vGroupSelected == FullGroups[i][j]) {
								var opt = document.createElement("option");
								opt.text = VarList[i];
								opt.value = VarList[i];
								//JAM 2013-12-14	For IE<11
								if (navigator.appName == 'Microsoft Internet Explorer')	{
									//JAM 2013-12-10 In order to optimize in IE11
									//JAM 2013-12-26 Navigator is lower than 10 version
									if (glbPrevIEVersion == 1) { 
										
										doc.lstVarName.add(opt);
									}
									else {
										doc.lstVarName.appendChild(opt); 
									}
								}
								else {
									 doc.lstVarName.appendChild(opt); 
								}	
								/*try {
									doc.lstVarName.add(opt, null); // standards compliant; doesn't work in IE
								}
								catch (ex) {
									doc.lstVarName.add(opt); // IE only
								}*/
								break;
							}
						}	
					}	
				}

            
        }
    }
	doc.fSubSetchosen.value=doc.cmb_VCAT.selectedIndex;
	doc.fSubSetchosenName.value = vGroupSelected; //JAM 2013-09-18
	doc.lstVarName.selectedIndex = 0;
	document.forms[0].txtVarSearch.focus(); //JAM 2013-08-28
	
	//JAM 2013-09-07 Ajax Combo must be filled when it has changed
	////////////////////////////////////////////////////////////////////////////////////////
	 /*  function setVariableFromSearchClick()
	         {
	         //JAM 2011-09-28   TFR exception isn't necessary for blank spaces managing
	         // if (this.actb_currObject=="TFR - Total fertility rate: children per woman - Number")
          // {
	      //       this.actb_currObject="TFR - Total fertility rate: children per woman - Number";
	       //    }
	          document.getElementById("lstVarName").value=this.actb_currObject;
	          document.getElementById("btnInvs").click();
	         
	         }
				
				
			
      //  if (variableList == null){
               if (document.forms[0].lstVarName!= null)
                { 
                var lst=document.getElementById("lstVarName");
                variableList =new Array(lst.length);
               
                 for (var count = 0; count < lst.length; count++)
                 {
	                 variableList[count]=lst.options[count].innerHTML;
	                  
	             }
	            }
           // }
            
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
             */ 
	
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
				
				
			
      //  if (variableList == null){
               if (document.forms[0].lstVarName!= null)
                { 
                var lst=document.getElementById("lstVarName");
                variableList =new Array(lst.length);
               
                 for (var count = 0; count < lst.length; count++)
                 {
	                 variableList[count]=lst.options[count].innerHTML;
	                  
	             }
	            }
           // }
            
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
              
	
	
	///////////////////////////////////////////////////////////////////////////////////////
	///////////////////////////////////////////////////////////////////////////////////////
	
	
}


//JAM 2013-08-25 Other filter property 
function cmb_OthFilt_click(){
 //Other Filters
 
  var doc = document.forms[0];
  var jGroupSelected ="";
  var jAddName = "";
  var jdataString = "";
  var jLoadCovatra = "LoadCoVaTrA";
  var i = 0;
  var jothFilterchosen = "";
  var jfilter= 0;
  var jpivotfilter;
  
  //JAM 2013-12-28 Validating if the navigator version is previous to IE 10
  var glbPrevIEVersion = isPreviousVerIE();

	//JAM 2013-09-30 Optimize behavior in IE
	//doc.lstVarName.length = 0;
	//JAM 2013--12-10 In orde to optimize in IE11
	//if (navigator.appName == 'Microsoft Internet Explorer')	{
		
		if (navigator.appName == 'Microsoft Internet Explorer')	{
		/*var vselect = document.getElementById("cmb_VCAT");

		for (i = 0; i < NumVars; i++) {
		  vselect.options[i] = null;
		}*/
	
		//doc.lstVarName.length = 0;
		//document.getElementById("lstVarName").options.length = 0;
		
			document.getElementById("lstVarName").innerHTML = ""; 
		
		}
		else {
			//JAM 2013-12-14	For IE<11
			if (navigator.appName == 'Netscape')	{
				document.getElementById("lstVarName").innerHTML = "";
			}
			else {
				doc.lstVarName.length = 0;
			}	
			
		}
	/*}
	else {
		doc.lstVarName.length = 0;
	}*/
	/////////////////
	
	if (doc.cmb_VCAT[0].selected) {
		jGroupSelected = "All";
	}
	else {
		jGroupSelected = doc.cmb_VCAT[doc.cmb_VCAT.selectedIndex].text;
	}
	
	if (jGroupSelected == "All") {
		//JAM 2013-08-25
			jothFilterchosen = doc.cmb_OthFilt[doc.cmb_OthFilt.selectedIndex].text;
			if (jothFilterchosen == "ALL") {
				jfilter= 0;
			}	
			else if (jothFilterchosen == ("At least through year " + jFilterYear ))  {	
				jfilter= 1;
			
			}
			else if (jothFilterchosen == ("At least " + jFilterPoints + " Data Points"))  {	
				jfilter= 2;
			
			}
			else if (jothFilterchosen == ("Used for Initialization"))  {	
				jfilter= 3;
			
			}
			else  {	
				jfilter= 0;
			
			}
	
			for (i = 0; i < NumVars; i++) {
				if (jfilter == 0) {
					pivotfilter = jbInclude[i];
				}
				else if (jfilter == 1) {
					pivotfilter = jbIncludeTroughYear[i];
				}
				else if (jfilter == 2) {
					pivotfilter = jbIncludeNPoint[i];
				}
				else if (jfilter == 3) {
					pivotfilter = jbIncludeInit[i];
				}
				
				
				if ((jModelLoad = jLoadCovatra.toUpperCase()) || (UCase(sCoVaTra) = "NO") ) {  
				
					if (pivotfilter == "True") {
					
						var opt = document.createElement("option");
						opt.text = VarList[i];
						opt.value = VarList[i];
						//JAM 2013-12-14	For IE<11
						if (navigator.appName == 'Microsoft Internet Explorer')	{
							//JAM 2013-12-10 In order to optimize in IE11
							//JAM 2013-12-26 Navigator is lower than 10 version
							if (glbPrevIEVersion == 1) { 
								
								doc.lstVarName.add(opt);
							}
							else {
								doc.lstVarName.appendChild(opt); 
							}
						}
						else {
							 doc.lstVarName.appendChild(opt); 
						}	
						/*try {
							doc.lstVarName.add(opt, null); // standards compliant; doesn't work in IE
						}
						catch (ex) {
							doc.lstVarName.add(opt); // IE only
						}*/
						
				
					}
				}
			}	
	}
	else {
			//JAM 2013-08-25
			jothFilterchosen = doc.cmb_OthFilt[doc.cmb_OthFilt.selectedIndex].text;
			if (jothFilterchosen == "ALL") {
				jfilter= 0;
			}	
			else if (jothFilterchosen == ("At least through year " + jFilterYear ))  {	
				jfilter= 1;
			
			}
			else if (jothFilterchosen == ("At least " + jFilterPoints + " Data Points"))  {	
				jfilter= 2;
			
			}
			else if (jothFilterchosen == ("Used for Initialization"))  {	
				jfilter= 3;
			
			}
			else  {	
				jfilter= 0;
			
			}
			 for (i = 0; i < NumVars; i++) {
			 	var vCountGroup = NumGroups[i];
				if (jfilter == 0) {
					pivotfilter = jbInclude[i];
				}
				else if (jfilter == 1) {
					pivotfilter = jbIncludeTroughYear[i];
				}
				else if (jfilter == 2) {
					pivotfilter = jbIncludeNPoint[i];
				}
				else if (jfilter == 3) {
					pivotfilter = jbIncludeInit[i];
				}
				
				if (pivotfilter == "True") {
					for (var j = 0; j < vCountGroup; j++) {
					
							
							if (jGroupSelected == FullGroups[i][j]) {
									var opt = document.createElement("option");
									opt.text = VarList[i];
									opt.value = VarList[i];
									//JAM 2013-12-14	For IE<11
									if (navigator.appName == 'Microsoft Internet Explorer')	{
										//JAM 2013-12-10 In order to optimize in IE11
										//JAM 2013-12-26 Navigator is lower than 10 version
										if (glbPrevIEVersion == 1) { 
											
											doc.lstVarName.add(opt);
										}
										else {
											doc.lstVarName.appendChild(opt); 
										}
									}
									else {
										 doc.lstVarName.appendChild(opt); 
									}	
									/*try {
										doc.lstVarName.add(opt, null); // standards compliant; doesn't work in IE
									}
									catch (ex) {
										doc.lstVarName.add(opt); // IE only
									}*/
									break;
							}
								
					}	
				}
			}	
	}
	
	doc.fOthFiltchosen.value=doc.cmb_OthFilt.selectedIndex;
	doc.fOthFiltchosenName.value = jothFilterchosen; //JAM 2013-09-18
	doc.lstVarName.selectedIndex = 0;
	document.forms[0].txtVarSearch.focus(); //JAM 2013-08-28
	
	
	//JAM 2013-09-07 Ajax Combo must be filled when it has changed
	////////////////////////////////////////////////////////////////////////////////////////
	 /*  function setVariableFromSearchClick()
	         {
	         //JAM 2011-09-28   TFR exception isn't necessary for blank spaces managing
//	          if (this.actb_currObject=="TFR - Total fertility rate: children per woman - Number")
//	           {
//	             this.actb_currObject="TFR - Total fertility rate: children per woman - Number";
//	           }
	          document.getElementById("lstVarName").value=this.actb_currObject;
	          document.getElementById("btnInvs").click();
	         
	         }
				
				
			
      //  if (variableList == null){
               if (document.forms[0].lstVarName!= null)
                { 
                var lst=document.getElementById("lstVarName");
                variableList =new Array(lst.length);
               
                 for (var count = 0; count < lst.length; count++)
                 {
	                 variableList[count]=lst.options[count].innerHTML;
	                  
	             }
	            }
           // }
            
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
              
	*/
	
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
				
				
			
      //  if (variableList == null){
               if (document.forms[0].lstVarName!= null)
                { 
                var lst=document.getElementById("lstVarName");
                variableList =new Array(lst.length);
               
                 for (var count = 0; count < lst.length; count++)
                 {
	                 variableList[count]=lst.options[count].innerHTML;
	                  
	             }
	            }
           // }
            
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
              
	
	
	///////////////////////////////////////////////////////////////////////////////////////
	///////////////////////////////////////////////////////////////////////////////////////
	
	 
}

//JAM 2013-09-19 Load for Group Filter
function initScatVarsGroup() {
 //filter categories
 
    var doc = document.forms[0];
    var i = 0;
	var jLoadCovatra = "LoadCoVaTrA";
	//var vGroupSelected = doc.cmb_VCAT[doc.cmb_VCAT.selectedIndex].text
	
	//JAM 2013-12-28 Validating if the navigator version is previous to IE 10
	var glbPrevIEVersion = isPreviousVerIE();
	
	//JAM 2013-09-19
	//doc.fSubSetchosenName.value = "Economic";
	//JAM 2013-10-06
if (typeof (doc.fSubSetchosenName.value) != "undefined"  && document.getElementById("lstVarName") != null && doc.fSubSetchosenName.value!= "All" && doc.fSubSetchosenName.value != "") {
	
	//JAM 2013-09-21 Try to avoid reloading all variables
	//if (doc.fSubSetchosen.value != "" && doc.fSubSetchosen.value != "0" ) {
	if (doc.fSubSetchosenName.value != "" ) {
	//if doc.fSubSetchosen.value
			var jOthFiltchosen=0;
			
			var jSubSetchosen= 0;
			

			
			if (doc.fSubSetchosenName.value== "All") {
				vGroupSelected = "All";
			}
			else {
			
				vGroupSelected = doc.fSubSetchosenName.value;
				}

			//JAM 2013-09-30 Optimize behavior in IE
			//doc.lstVarName.length = 0;
			//JAM 2013--12-10 In orde to optimize in IE11
			//if (navigator.appName == 'Microsoft Internet Explorer')	{
			
				//JAM 2013-10-06
				if (document.getElementById("lstVarName") != null) {
						if (navigator.appName == 'Microsoft Internet Explorer')	{
							/*var vselect = document.getElementById("cmb_VCAT");

							for (i = 0; i < NumVars; i++) {
							  vselect.options[i] = null;
							}*/
						
							//doc.lstVarName.length = 0;
							//document.getElementById("lstVarName").options.length = 0;
							
							document.getElementById("lstVarName").innerHTML = ""; 
		
						}
						else {
							//JAM 2013-12-14	For IE<11
							if (navigator.appName == 'Netscape')	{
								document.getElementById("lstVarName").innerHTML = "";
							}
							else {
								doc.lstVarName.length = 0;
							}	
							
						}
				}
			/*}
			else {
				doc.lstVarName.length = 0;
			}*/
			/////////////////
			if (doc.fSubSetchosenName.value=="All") {  // ALL Category selected
				for (i = 0; i < NumVars; i++) {
					if ((jModelLoad = jLoadCovatra.toUpperCase()) || (jScat_GroupNames != "Automobile") ) {
				   
					
						if (jbInclude[i] = "True") {
							var opt = document.createElement("option");
							opt.text = VarList[i];
							opt.value = VarList[i];
							//JAM 2013-12-14	For IE<11
							if (navigator.appName == 'Microsoft Internet Explorer')	{
								//JAM 2013-12-10 In order to optimize in IE11
								//JAM 2013-12-26 Navigator is lower than 10 version
								if (glbPrevIEVersion == 1) { 
									
									doc.lstVarName.add(opt);
								}
								else {
									doc.lstVarName.appendChild(opt); 
								}
							}
							else {
								 doc.lstVarName.appendChild(opt); 
							}	
							/*try {
								doc.lstVarName.add(opt, null); // standards compliant; doesn't work in IE
							}
							catch (ex) {
								doc.lstVarName.add(opt); // IE only
							}*/
						}	
					}	
				}
			}	
			else //Category Selected
			{		//JAM 2013-08-10
					//NumVars
				for (i = 0; i < NumVars; i++) {
						var vCountGroup = NumGroups[i];
									
						for (var j = 0; j < vCountGroup; j++) {
							
							if ((jModelLoad = jLoadCovatra.toUpperCase()) || (jScat_GroupNames != "Automobile")) {
								if (jbInclude[i] = "True") {
									 //var vtemp = FullGroups[i][j];
									if (vGroupSelected == FullGroups[i][j]) {
										var opt = document.createElement("option");
										opt.text = VarList[i];
										opt.value = VarList[i];
										//JAM 2013-12-14	For IE<11
										if (navigator.appName == 'Microsoft Internet Explorer')	{
											//JAM 2013-12-10 In order to optimize in IE11
											//JAM 2013-12-26 Navigator is lower than 10 version
											if (glbPrevIEVersion == 1) { 
												
												doc.lstVarName.add(opt);
											}
											else {
												doc.lstVarName.appendChild(opt); 
											}
										}
										else {
											 doc.lstVarName.appendChild(opt); 
										}	
										/*try {
											doc.lstVarName.add(opt, null); // standards compliant; doesn't work in IE
										}
										catch (ex) {
											doc.lstVarName.add(opt); // IE only
										}*/
										break;
									}
								}	
							}	
						}

					
				}
			}
			//doc.fSubSetchosen.value=doc.cmb_VCAT.selectedIndex;
			//doc.fSubSetchosenName.value = vGroupSelected; //JAM 2013-09-18
			doc.lstVarName.selectedIndex = 0;
			document.forms[0].txtVarSearch.focus(); //JAM 2013-08-28
			
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
				
				
			
      //  if (variableList == null){
               if (document.forms[0].lstVarName!= null)
                { 
                var lst=document.getElementById("lstVarName");
                variableList =new Array(lst.length);
               
                 for (var count = 0; count < lst.length; count++)
                 {
	                 variableList[count]=lst.options[count].innerHTML;
	                  
	             }
	            }
           // }
            
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
              
	
	
	///////////////////////////////////////////////////////////////////////////////////////
			
	}
}	
else {	//JAM 2013-10-19	Filling Combo
		if ( document.getElementById("lstVarName") != null  ) {
			
			//JAM 2013-10-26 Optimize behavior in IE
			//doc.lstVarName.length = 0;
			//JAM 2013--12-10 In orde to optimize in IE11
			//if (navigator.appName == 'Microsoft Internet Explorer')	{
				
				if (navigator.appName == 'Microsoft Internet Explorer')	{
					/*var vselect = document.getElementById("cmb_VCAT");

					for (i = 0; i < NumVars; i++) {
					  vselect.options[i] = null;
					}*/
				
					//doc.lstVarName.length = 0;
					//document.getElementById("lstVarName").options.length = 0;
					
					document.getElementById("lstVarName").innerHTML = ""; 
		
				}
				else {
					//JAM 2013-12-14	For IE<11
					if (navigator.appName == 'Netscape')	{
						document.getElementById("lstVarName").innerHTML = "";
					}
					else {
						doc.lstVarName.length = 0;
					}	
					
				}

			/*}
			else {
				doc.lstVarName.length = 0;
			}*/
			/////////////////
		
		
			for (i = 0; i < NumVars; i++) {
				//if ((jModelLoad = jLoadCovatra.toUpperCase()) || (jScat_GroupNames != "Automobile") ) {
					   
						
					//if (jbInclude[i] = "True") {
						var opt = document.createElement("option");
						opt.text = VarList[i];
						opt.value = VarList[i];
						//JAM 2013-12-14	For IE<11
							if (navigator.appName == 'Microsoft Internet Explorer')	{
								//JAM 2013-12-10 In order to optimize in IE11
								//JAM 2013-12-26 Navigator is lower than 10 version
								if (glbPrevIEVersion == 1) { 
									
									doc.lstVarName.add(opt);
								}
								else {
									doc.lstVarName.appendChild(opt); 
								}
							}
							else {
								 doc.lstVarName.appendChild(opt); 
							}	
						/*try {
							doc.lstVarName.add(opt, null); // standards compliant; doesn't work in IE
							}
						catch (ex) {
							doc.lstVarName.add(opt); // IE only
						}*/
					//}	
				//}	
			}
			
		////////////////////////////////////////////////////////////////
		doc.lstVarName.selectedIndex = 0;
		document.forms[0].txtVarSearch.focus(); //JAM 2013-08-28
			
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
				
				
			
      //  if (variableList == null){
               if (document.forms[0].lstVarName!= null)
                { 
                var lst=document.getElementById("lstVarName");
                variableList =new Array(lst.length);
               
                 for (var count = 0; count < lst.length; count++)
                 {
	                 variableList[count]=lst.options[count].innerHTML;
	                  
	             }
	            }
           // }
            
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
              
	
		
			
		/////////////////////////////////////////////////////////////////		
		}
}
}
//JAM 2013-09-21 Load for Other filter 
function initScatVarsOthFilt(){
 //Other Filters
 
  var doc = document.forms[0];
  var jGroupSelected ="";
  var jAddName = "";
  var jdataString = "";
  var jLoadCovatra = "LoadCoVaTrA";
  var i = 0;
  var jothFilterchosen = "";
  var jfilter= 0;
  var jpivotfilter;
  
  //JAM 2013-12-28 Validating if the navigator version is previous to IE 10
  var glbPrevIEVersion = isPreviousVerIE();
  
  //JAM 2013-10-06
 if (typeof (doc.fOthFiltchosenName.value) != "undefined" && document.getElementById("lstVarName") != null && doc.fOthFiltchosenName.value != "ALL" ) {
 
	//JAM 2013-09-21
	//JAM 2013-09-28 && added in order to don't reload array
	if ((doc.fOthFiltchosenName.value != "" ) ) {	
	//if (doc.fOthFiltchosen.value != "" && doc.fOthFiltchosen.value != "3") {	
				//JAM 2013-09-30 Optimize behavior in IE
				//doc.lstVarName.length = 0;
				//JAM 2013--12-10 In orde to optimize in IE11
				//if (navigator.appName == 'Microsoft Internet Explorer')	{
					
					//JAM 2013-10-06
					if (document.getElementById("lstVarName") != null) {
						if (navigator.appName == 'Microsoft Internet Explorer')	{
							/*var vselect = document.getElementById("cmb_VCAT");

							for (i = 0; i < NumVars; i++) {
							  vselect.options[i] = null;
							}*/
						
							//doc.lstVarName.length = 0;
							//document.getElementById("lstVarName").options.length = 0;
							
							document.getElementById("lstVarName").innerHTML = ""; 
		
						}
						else {
							//JAM 2013-12-14	For IE<11
							if (navigator.appName == 'Netscape')	{
								document.getElementById("lstVarName").innerHTML = "";
							}
							else {
								doc.lstVarName.length = 0;
							}	
							
						}
					}	
				/*}
				else {
					doc.lstVarName.length = 0;
				}*/
				/////////////////
				
				if (doc.fSubSetchosen.value =="0") {
					jGroupSelected = "All";
				}
				else {
					jGroupSelected = doc.fSubSetchosenName.value;
				}
				
				if (jGroupSelected == "All") {
					//JAM 2013-08-25
						jothFilterchosen = doc.fOthFiltchosenName.value;
						if (jothFilterchosen == "ALL") {
							jfilter= 0;
						}	
						else if (jothFilterchosen == ("At least through year " + jFilterYear ))  {	
							jfilter= 1;
						
						}
						else if (jothFilterchosen == ("At least " + jFilterPoints + " Data Points"))  {	
							jfilter= 2;
						
						}
						else if (jothFilterchosen == ("Used for Initialization"))  {	
							jfilter= 3;
						
						}
						else  {	
							jfilter= 0;
						
						}
				
						for (i = 0; i < NumVars; i++) {
							if (jfilter == 0) {
								pivotfilter = jbInclude[i];
							}
							else if (jfilter == 1) {
								pivotfilter = jbIncludeTroughYear[i];
							}
							else if (jfilter == 2) {
								pivotfilter = jbIncludeNPoint[i];
							}
							else if (jfilter == 3) {
								pivotfilter = jbIncludeInit[i];
							}
							
							
							if ((jModelLoad = jLoadCovatra.toUpperCase()) || (UCase(sCoVaTra) = "NO") ) {  
							
								if (pivotfilter == "True") {
								
									var opt = document.createElement("option");
									opt.text = VarList[i];
									opt.value = VarList[i];
									//JAM 2013-12-14	For IE<11
									if (navigator.appName == 'Microsoft Internet Explorer')	{
										//JAM 2013-12-10 In order to optimize in IE11
										//JAM 2013-12-26 Navigator is lower than 10 version
										if (glbPrevIEVersion == 1) { 
											
											doc.lstVarName.add(opt);
										}
										else {
											doc.lstVarName.appendChild(opt); 
										}
									}
									else {
										 doc.lstVarName.appendChild(opt); 
									}	
									/*try {
										doc.lstVarName.add(opt, null); // standards compliant; doesn't work in IE
									}
									catch (ex) {
										doc.lstVarName.add(opt); // IE only
									}*/
									
							
								}
							}
						}	
				}
				else {
						//JAM 2013-08-25
						jothFilterchosen = doc.fOthFiltchosenName.value;
						if (jothFilterchosen == "ALL") {
							jfilter= 0;
						}	
						else if (jothFilterchosen == ("At least through year " + jFilterYear ))  {	
							jfilter= 1;
						
						}
						else if (jothFilterchosen == ("At least " + jFilterPoints + " Data Points"))  {	
							jfilter= 2;
						
						}
						else if (jothFilterchosen == ("Used for Initialization"))  {	
							jfilter= 3;
						
						}
						else  {	
							jfilter= 0;
						
						}
						 for (i = 0; i < NumVars; i++) {
							var vCountGroup = NumGroups[i];
							if (jfilter == 0) {
								pivotfilter = jbInclude[i];
							}
							else if (jfilter == 1) {
								pivotfilter = jbIncludeTroughYear[i];
							}
							else if (jfilter == 2) {
								pivotfilter = jbIncludeNPoint[i];
							}
							else if (jfilter == 3) {
								pivotfilter = jbIncludeInit[i];
							}
							
							if (pivotfilter == "True") {
								for (var j = 0; j < vCountGroup; j++) {
								
										
										if (jGroupSelected == FullGroups[i][j]) {
												var opt = document.createElement("option");
												opt.text = VarList[i];
												opt.value = VarList[i];
												//JAM 2013-12-14	For IE<11
												if (navigator.appName == 'Microsoft Internet Explorer')	{
													//JAM 2013-12-10 In order to optimize in IE11
													//JAM 2013-12-26 Navigator is lower than 10 version
													if (glbPrevIEVersion == 1) { 
														
														doc.lstVarName.add(opt);
													}
													else {
														doc.lstVarName.appendChild(opt); 
													}
												}
												else {
													 doc.lstVarName.appendChild(opt); 
												}	 
												/*try {
													doc.lstVarName.add(opt, null); // standards compliant; doesn't work in IE
												}
												catch (ex) {
													doc.lstVarName.add(opt); // IE only
												}*/
												break;
										}
											
								}	
							}
						}	
				}
				
				//doc.fOthFiltchosen.value=doc.cmb_OthFilt.selectedIndex;
				//doc.fOthFiltchosenName.value = jothFilterchosen; //JAM 2013-09-18
				doc.lstVarName.selectedIndex = 0;
				document.forms[0].txtVarSearch.focus(); //JAM 2013-08-28
				
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
				
				
			
      //  if (variableList == null){
               if (document.forms[0].lstVarName!= null)
                { 
                var lst=document.getElementById("lstVarName");
                variableList =new Array(lst.length);
               
                 for (var count = 0; count < lst.length; count++)
                 {
	                 variableList[count]=lst.options[count].innerHTML;
	                  
	             }
	            }
           // }
            
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
              
	
	
	///////////////////////////////////////////////////////////////////////////////////////
	
	}
 }	 
}
//JAM 2013-12-28 Validating if the navigator version is previous to IE 10
function isPreviousVerIE()
{
	var isprevious=0;
	var navVersion = navigator.appVersion;
	var navIE7 = navVersion.search("MSIE 7");
	var navIE8 = navVersion.search("MSIE 8");
	var navIE9 = navVersion.search("MSIE 9");
	
	if ((navIE7 != -1) || (navIE8 != -1) || (navIE9 != -1)) {
		isprevious = 1;
	}
	else {
		isprevious = 0;
	}
		
	return isprevious;

}
/*
//JAM 2013-09-22
function AjaxCmbRefresh{

//JAM 2013-09-07 Ajax Combo must be filled when it has changed
	////////////////////////////////////////////////////////////////////////////////////////
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
				
				
			
      //  if (variableList == null){
               if (document.forms[0].lstVarName!= null)
                { 
                var lst=document.getElementById("lstVarName");
                variableList =new Array(lst.length);
               
                 for (var count = 0; count < lst.length; count++)
                 {
	                 variableList[count]=lst.options[count].innerHTML;
	                  
	             }
	            }
           // }
            
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
              
	
	
	///////////////////////////////////////////////////////////////////////////////////////
	

}
*/








