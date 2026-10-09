//JAM 2014-04-04 Functions used for frm_VarSel.aspx form, code based on FlexDisplayFx developed by Jose

//JAM 2013-04-04 Function for filter property 
function cmb_VCAT_click() {
 //filter categories
 
    var doc = document.forms[0];
    var i = 0;
	var jLoadCovatra = "LoadCoVaTrA";
	//var vGroupSelected = doc.cmb_VCAT[doc.cmb_VCAT.selectedIndex].text
	
	//JAM 2014-04-19
	if (typeof (doc.fEmpOrForeMode.value) != "undefined") {
		if (doc.fEmpOrForeMode.value== "1") {
			
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

				for (i = 0; i < NumVarsvse; i++) {
				  vselect.options[i] = null;
				}*/
			
				//doc.lstVarName.length = 0;
				//document.getElementById("lstVarName").options.length = 0;
				if (navigator.appName == 'Microsoft Internet Explorer')	{
				/*var vselect = document.getElementById("cmb_VCAT");

				for (i = 0; i < NumVarsvse; i++) {
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
				for (i = 0; i < NumVarsvse; i++) {
					if ((jModelLoadvse = jLoadCovatra.toUpperCase()) || (jVarSel_GroupNamesvse != "Automobile") ) {
				   
					
						////if (jbInclude[i] = "True") {
							var opt = document.createElement("option");
							opt.text = VarListvse[i];
							opt.value = VarListvse[i];
							
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
						////}	
					}	
				}
			}	
			else //Category Selected
			{		//JAM 2013-08-10
					//NumVars
				for (i = 0; i < NumVarsvse; i++) {
						var vCountGroup = NumGroupsvse[i];
									
						for (var j = 0; j < vCountGroup; j++) {
							
							if ((jModelLoadvse = jLoadCovatra.toUpperCase()) || (jVarSel_GroupNamesvse != "Automobile")) {
								////if (jbInclude[i] = "True") {
									 //var vtemp = FullGroupsvse[i][j];
									if (vGroupSelected == FullGroupsvse[i][j]) {
										var opt = document.createElement("option");
										opt.text = VarListvse[i];
										opt.value = VarListvse[i];
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
								////}	
							}	
						}

					
				}
			}
			doc.fSubSetchosen.value=doc.cmb_VCAT.selectedIndex;
			doc.fSubSetchosenName.value = vGroupSelected; 
			doc.lstVarName.selectedIndex = 0;
			document.forms[0].txtVarSearch.focus(); 
			
			
			
					 function setVariableFromSearchClick()
					 {
				 
					  document.getElementById("lstVarName").value=this.actb_currObject;
					  document.getElementById("btnInvs").click();
					 
					 }
						
						
					
			 
					   if (document.forms[0].lstVarName!= null)
						{ 
						var lst=document.getElementById("lstVarName");
						variableList =new Array(lst.length);
					   
						 for (var count = 0; count < lst.length; count++)
						 {
							 variableList[count]=lst.options[count].innerHTML;
							  
						 }
						}
				 
					
					 function BindDataToVarSearch()
					 {     
						var obj = actb(document.getElementById('txtVarSearch'),variableList);
					  
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
					  
	
		}
	}
}


//JAM 2014-04-04 Load lst_VarName combo
function initVarSelEmp() {
 //filter categories
 
    var doc = document.forms[0];
    var i = 0;
	var jLoadCovatra = "LoadCoVaTrA";
	
	var glbPrevIEVersion = isPreviousVerIE();
	
	
	//JAM 2014-04-19
	if (typeof (doc.fEmpOrForeMode.value) != "undefined") {
		if (doc.fEmpOrForeMode.value== "1") {
			if (typeof (doc.fSubSetchosenName.value) != "undefined"  && document.getElementById("lstVarName") != null && doc.fSubSetchosenName.value!= "All" && doc.fSubSetchosenName.value != "") {
				
				//JAM 2013-09-21 Try to avoid reloading all variables
				//if (doc.fSubSetchosen.value != "" && doc.fSubSetchosen.value != "0" ) {
				if (doc.fSubSetchosenName.value != "" ) {
				//if doc.fSubSetchosen.value
					
						

						
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

										for (i = 0; i < NumVarsvse; i++) {
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
							for (i = 0; i < NumVarsvse; i++) {
								if ((jModelLoadvse = jLoadCovatra.toUpperCase()) || (jVarSel_GroupNamesvse != "Automobile") ) {
							   
								
									////if (jbInclude[i] = "True") {
										var opt = document.createElement("option");
										opt.text = VarListvse[i];
										opt.value = VarListvse[i];
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
									////}	
								}	
							}
						}	
						else //Category Selected
						{		//JAM 2013-08-10
								//NumVars
							for (i = 0; i < NumVarsvse; i++) {
									var vCountGroup = NumGroupsvse[i];
												
									for (var j = 0; j < vCountGroup; j++) {
										
										if ((jModelLoadvse = jLoadCovatra.toUpperCase()) || (jVarSel_GroupNamesvse != "Automobile")) {
											////if (jbInclude[i] = "True") {
												 //var vtemp = FullGroupsvse[i][j];
												if (vGroupSelected == FullGroupsvse[i][j]) {
													var opt = document.createElement("option");
													opt.text = VarListvse[i];
													opt.value = VarListvse[i];
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
											////}	
										}	
									}

								
							}
						}
						//doc.fSubSetchosen.value=doc.cmb_VCAT.selectedIndex;
						//doc.fSubSetchosenName.value = vGroupSelected; //JAM 2013-09-18
						
						//JAM 2015-03-25 Keeping value for lstVarName
						if (typeof (doc.fVarchosen.value) != "undefined") {
									doc.lstVarName.selectedIndex = doc.fVarchosen.value;
									doc.txtVarSearch.value = doc.lstVarName.value; //JAM 2016-04-03
									
						} else {
									doc.lstVarName.selectedIndex = 0;
							
						}		
						//doc.lstVarName.selectedIndex = 0;
						document.forms[0].txtVarSearch.focus(); //JAM 2013-08-28
						
						 function setVariableFromSearchClick()
						 {
						
						  document.getElementById("lstVarName").value=this.actb_currObject;
						  document.getElementById("btnInvs").click();
						 
						 }
							
							
						
				  
						   if (document.forms[0].lstVarName!= null)
							{ 
							var lst=document.getElementById("lstVarName");
							variableList =new Array(lst.length);
						   
							 for (var count = 0; count < lst.length; count++)
							 {
								 variableList[count]=lst.options[count].innerHTML;
								  
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
							//JAM 2016-04-03 Commented in order to preserve chosen value
							//document.getElementById('txtVarSearch').value="";         
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

								for (i = 0; i < NumVarsvse; i++) {
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
					
					
						for (i = 0; i < NumVarsvse; i++) {
							//if ((jModelLoadvse = jLoadCovatra.toUpperCase()) || (jVarSel_GroupNamesvse != "Automobile") ) {
								   
									
								//if (jbInclude[i] = "True") {
									var opt = document.createElement("option");
									opt.text = VarListvse[i];
									opt.value = VarListvse[i];
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
					//JAM 2015-03-25 Keeping value for lstVarName
					if (typeof (doc.fVarchosen.value) != "undefined") {
								doc.lstVarName.selectedIndex = doc.fVarchosen.value;
								doc.txtVarSearch.value = doc.lstVarName.value; //JAM 2016-04-03
					} else {
								doc.lstVarName.selectedIndex = 0;
							
					}		
					//doc.lstVarName.selectedIndex = 0;
					document.forms[0].txtVarSearch.focus(); //JAM 2013-08-28
						
						 function setVariableFromSearchClick()
						 {
					  
						  document.getElementById("lstVarName").value=this.actb_currObject;
						  document.getElementById("btnInvs").click();
						 
						 }
							
							
						
				 
						   if (document.forms[0].lstVarName!= null)
							{ 
							var lst=document.getElementById("lstVarName");
							variableList =new Array(lst.length);
						   
							 for (var count = 0; count < lst.length; count++)
							 {
								 variableList[count]=lst.options[count].innerHTML;
								  
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
							//JAM 2016-04-03 Commented in order to preserve chosen value
							//document.getElementById('txtVarSearch').value="";         
							document.getElementById('txtVarSearch').select();   
							
						  }
							BindDataToVarSearch();		
						  
				
					
						
					/////////////////////////////////////////////////////////////////		
					}
			}
		}	
	}		
}

//jrs 2023/05/18
function initVarSelL(NumVars, VarList) {
	//filter categories

	var doc = document.forms[0];
	var i = 0;
	var jLoadCovatra = "LoadCoVaTrA";

	var glbPrevIEVersion = isPreviousVerIE();


	if (typeof (doc.fEmpOrForeMode.value) != "undefined") {
		if (document.getElementById("lstVarName") != null) {

			//JAM 2013-10-26 Optimize behavior in IE
			//doc.lstVarName.length = 0;
			//JAM 2013--12-10 In orde to optimize in IE11
			//if (navigator.appName == 'Microsoft Internet Explorer')	{

			if (navigator.appName == 'Microsoft Internet Explorer') {
				/*var vselect = document.getElementById("cmb_VCAT");

				for (i = 0; i < NumVarsvse; i++) {
					vselect.options[i] = null;
				}*/

				//doc.lstVarName.length = 0;
				//document.getElementById("lstVarName").options.length = 0;

				document.getElementById("lstVarName").innerHTML = "";

			}
			else {
				//JAM 2013-12-14	For IE<11
				if (navigator.appName == 'Netscape') {
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
				//if ((jModelLoadvse = jLoadCovatra.toUpperCase()) || (jVarSel_GroupNamesvse != "Automobile") ) {


				//if (jbInclude[i] = "True") {
				var opt = document.createElement("option");
				opt.text = VarList[i];
				opt.value = VarList[i];
				//JAM 2013-12-14	For IE<11
				if (navigator.appName == 'Microsoft Internet Explorer') {
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
			//JAM 2015-03-25 Keeping value for lstVarName
			if (typeof (doc.fVarchosen.value) != "undefined") {
				doc.lstVarName.selectedIndex = doc.fVarchosen.value;
				doc.txtVarSearch.value = doc.lstVarName.value; //JAM 2016-04-03
			} else {
				doc.lstVarName.selectedIndex = 0;

			}
			//doc.lstVarName.selectedIndex = 0;
			document.forms[0].txtVarSearch.focus(); //JAM 2013-08-28

			function setVariableFromSearchClick() {

				document.getElementById("lstVarName").value = this.actb_currObject;
				document.getElementById("btnInvs").click();

			}




			if (document.forms[0].lstVarName != null) {
				var lst = document.getElementById("lstVarName");
				variableList = new Array(lst.length);

				for (var count = 0; count < lst.length; count++) {
					variableList[count] = lst.options[count].innerHTML;

				}
			}


			function BindDataToVarSearch() {
				var obj = actb(document.getElementById('txtVarSearch'), variableList);
				//KIA add the follwing event listener with your desired method what you want to call on a search list item is clicked
				this.actb_cur_obj_clicked.prototype.itemClicked = setVariableFromSearchClick;
				this.actb_fSize = '12px';
				this.actb_bgColor = '#888888';
				this.actb_textColor = '#FFFFFF';
				this.actb_hColor = '#000000';
				this.actb_fFamily = 'Verdana';
				this.actb_lim = 5;
				this.actb_timeOut = -1;
				this.actb_firstText = true;
				this.actb_hStyle = 'text-decoration:underline;font-weight="bold"';
				//JAM 2016-04-03 Commented in order to preserve chosen value
				//document.getElementById('txtVarSearch').value="";         
				document.getElementById('txtVarSearch').select();

			}
			BindDataToVarSearch();




			/////////////////////////////////////////////////////////////////		
		}
	} // if (typeof (doc.fEmpOrForeMode.value) != "undefined") {
}

//jrs 2023/05/19
function initVarSelList() {
	var doc = document.forms[0];
	if (doc.fEmpOrForeMode.value == "3") {
		initVarSelL(NumVarsdlf, VarListdlf);
	}
	else if (doc.fEmpOrForeMode.value == "4") {
		initVarSelL(NumVarsdlh, VarListdlh);
	}
	else if (doc.fEmpOrForeMode.value == "5") {
		initVarSelL(NumVarsdlhf, VarListdlhf);
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
