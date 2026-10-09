//JAM 2014-04-18-18 Functions used for frm_VarSel.aspx form, code based on FlexDisplayFx developed by Jose



//JAM 2014-04-18-04 Load lst_VarName combo
function initVarSel() {
 //filter categories
 
    var doc = document.forms[0];
    var i = 0;
	var jLoadCovatra = "LoadCoVaTrA";
	
	var glbPrevIEVersion = isPreviousVerIE();
	
	var chosenindex = doc.lstVarName.selectedIndex ;
	
	var numelements = doc.lstVarName.length;

	//JAM 2014-05-04
	if (numelements == 0) {
			for (i = 0; i < NumVarsvsf; i++) {
									//if ((jModelLoadvsf = jLoadCovatra.toUpperCase()) || (jScat_GroupNames != "Automobile") ) {
										   
											
										//if (jbInclude[i] = "True") {
											var opt = document.createElement("option");
											opt.text = VarListvsf[i];
											opt.value = VarListvsf[i];
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
	}
	numelements = doc.lstVarName.length;
						
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
							document.getElementById('txtVarSearch').value="";         
							document.getElementById('txtVarSearch').select();   
							
						  }
							BindDataToVarSearch();		
						  
				
					
						
					/////////////////////////////////////////////////////////////////		
				
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
