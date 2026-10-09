//JAM 2015-05-14 Filling country combo


function initautocomplete() {
 
 
    var doc = document.forms[0];
    var i = 0;
	
	
	var glbPrevIEVersion = isPreviousVerIE();
	
	
							for (i = 0; i < NumRegions; i++) {
								
										var opt = document.createElement("option");
										opt.text = VarRegions[i];
										opt.value = VarRegions[i];
										
										
										//JAM 2013-12-14	For IE<11
										if (navigator.appName == 'Microsoft Internet Explorer')	{
											//JAM 2013-12-10 In order to optimize in IE11
											//JAM 2013-12-26 Navigator is lower than 10 version
											
												if (glbPrevIEVersion == 1) { 
													
												    doc.txtRegionsMultiple.add(opt);
												}
												else {
												    doc.txtRegionsMultiple.appendChild(opt);
												}
											
										}
										else {
										    doc.txtRegionsMultiple.appendChild(opt);
																							
										}	
										
							
							
						}	
						
}

//JAM 2015-05-14 Validating if the navigator version is previous to IE 10
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


//jrs 2015/06/12 Showing the description of default scenario
function showSceDesc(sName)
{
    if (document.getElementById('lblScenarioDesc') != null) {
        var i = 0;

        for (i = 0; i < 5; i++) {
            if (sName == SceListNames[i]) {
                document.getElementById('lblScenarioDesc').innerHTML = SceDesc[i];
                break;
            }
        }
    }
}

//jrs 2015/08/24
function showVarDesc(sVar) {
    var i = 0;
    for (i = 0; i <= NumVars; i++) {
        if (sVar == VarListvse[i]) {
            alert('Full Description: \n\n' + VarDescs[i]);
            break;
        }
    }
}