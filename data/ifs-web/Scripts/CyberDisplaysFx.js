//jrs 2013/05/09 Moved to external file to improve loading, Jaime recomended it, but the testing showed it to be slower, trying it again

//'jrs 2011/01/19 need dimension list boxes visible to hide them or show them from client side
function SetLists() {
    var doc = document.forms[0];
    var div1 = document.getElementById("div_dims");
    div1.style.display = "block";

    //Sets List Files
    SceOrdFiles = new Array();
    SceOrdFilesCount = 0;
    //reload 
    if (doc.txtFiles.value > "") {
        var f = new Array();
        f = doc.txtFiles.value.split(",");
        for (var i = 0; i < f.length; i++) {
            SceOrdFiles["x" + f[i]] = f[i];
            SceOrdFilesCount++;
        }
    }

    //jrs 2012/12/05
    //Sets Dim1s
    OrdDims = new Array();
    OrdDimsCount = 0;
    //reload 
    if (doc.txtDims.value > "") {
        var f = new Array();
        f = doc.txtDims.value.split(",");
        for (var i = 0; i < f.length; i++) {
            OrdDims["x" + f[i]] = f[i];
            OrdDimsCount++;
        }
    }

    //Save selected lists
    var SelDim = [];
    var SelCount = 0;
    var lstDim = document.getElementById("lstDim");
    var chkBoxCount = lstDim.getElementsByTagName("input");
    var labelRef = lstDim.getElementsByTagName('label');
    for (var i = 0; i < chkBoxCount.length; i++) {
        //if (lstDim.childNodes[0].childNodes[i].childNodes[0].childNodes[0].selected) {
        if (chkBoxCount[i].checked) {
            SelCount++;
            SelDim[SelCount] = labelRef[i].innerText;
        }
    }

    lstVars_click();

    //Reset selected lists
    Set_First_Selected_Idx('lstDim', -1);

    var j = 1;
    doc.txtDims.value = "";
    for (var i = 0; i < chkBoxCount.length && j<=SelCount; i++) {
        if (labelRef[i].innerText == SelDim[j]) {
            j++;
            lstDim.childNodes[0].childNodes[i].childNodes[0].childNodes[0].selected = true;
            chkBoxCount[i].checked = true;
            chkBoxCount[i].selected = true;
            if (doc.txtDims.value > "") {
                doc.txtDims.value = doc.txtDims.value + ","
            }
            doc.txtDims.value = doc.txtDims.value + i;
            if (j > SelCount) {
                i = chkBoxCount.length;
            }
        }
    }
    
    //jrs 2013/03/22
    //if packaged was not saved and looking for first one, and not found
    if (Get_First_Selected_Idx('lstDim') == -1) {
        Set_First_Selected_Idx('lstDim', 0);
        lstDim_Click(0);
    }

        //jrs 2012/04/11 resize lists
    pageResize();
}



//2011/05/03                    
//all menu events need to be either in the client or in the server
//can't have some in client and some in server
function optGroup_Click() {
    var bSkip = bUseGroupDec;

    bUseReg = false;
    bUseGroup = true;
    bUseGroupDec = false;
    bUseGList = false;

    //                        document.all['cswmIcomnuGroups_0'].src = "blank.gif";
    //                        document.all['cswmIcomnuGroups_1'].src = "checkbox.gif";
    //                        document.all['cswmIcomnuGroups_2'].src = "blank.gif";
    //                        document.all['cswmIcomnuGroups_3'].src = "blank.gif";


    if (bSkip == false) {
        currDim1 = 0;
        lstVars_click();
    }
}

function optGroupDec_Click() {
    var bSkip = bUseGroup;

    bUseReg = false;
    bUseGroup = false;
    bUseGroupDec = true;
    bUseGList = false;

    if (bSkip == false) {
        currDim1 = 0;
        lstVars_click();
    }
}

function optGList_Click() {
    bUseReg = false;
    bUseGroup = false;
    bUseGroupDec = false;
    bUseGList = true;

    currDim1 = 0;
    lstVars_click();
}

function optReg_Click() {
    bUseReg = true;
    bUseGroup = false;
    bUseGroupDec = false;
    bUseGList = false;

    currDim1 = 0;
    lstVars_click();
}

//jrs 2012/04/11 global declarations
var myWidth = 0, myHeight = 0;

//jrs 2012/01/16
function clickButton(e, buttonid) {
    var evt = e ? e : window.event;
    var bt = document.getElementById(buttonid);
    if (bt) {
        if (evt.keyCode == 13) {
            bt.click();
            return false;
        }
    }
}

//jrs 2012/04/11
function pageResize() {
    if (typeof (window.innerWidth) == 'number') {
        /*Non-IE*/
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        /*//IE 6+ in 'standards compliant mode*/
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        /*//IE 4 compatible*/
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }
    if (myWidth == 0) {
        myWidth = screen.width;
        myHeight = screen.height;
    }

    var doc = document.forms[0];
    //pct of screen to be used
    //myWidth = 0.9 * myWidth;
    var f2 = 0.9;
    var f = 1.025;
    var h = myHeight * f2 - (20 + 100);
    if (h < 106 + 20) {
        h = 106 + 20;
    }

    //doc.lstDim.style.height = h + 'px';
    var lstDim1 = document.getElementById("divDim");
    lstDim1.style.maxHeight = h + 'px';
    lstDim1.style.height = h + 'px';
    doc.lstFiles.style.height = h + 'px';
    var T = (h-40)/2 * f;
    doc.lstVars.style.height = T + 'px';
    doc.lstPars.style.height = T + 'px';

    var tt = T + 135;
    document.getElementById('lblPars').style.top = tt + 'px';
    tt = tt + 20;
    document.getElementById('lstPars').style.top = tt + 'px';
    //jrs 2012/02/27
    var t2 = (h + 120) * f;
    doc.cmdLine.style.top = t2 + 'px';
    doc.cmdLineAC.style.top = t2 + 'px';
    doc.cmdLineFC.style.top = t2 + 'px';
    doc.cmdTable.style.top = t2 + 'px';

    ChangeListWidth();
}

function ChangeListWidth() {
    //Change width of lists
    var doc = document.forms[0];

    var f3 = 0.9;
    var SpaceBtwn = 16;
    var Disp = myWidth - 9 - 20;
    var NumbLst = 4;

    var DispInd = Disp / NumbLst;
    var dd = DispInd * 2;
    doc.lstVars.style.width = dd + 'px';
    doc.lstPars.style.width = dd + 'px';

    dd = DispInd - SpaceBtwn;
    //doc.lstDim.style.width = dd + 'px';
    var lstDim1 = document.getElementById("divDim");
    lstDim1.style.width = dd + 'px';

    var ll = 9 + dd + SpaceBtwn;
    //doc.lstPars.style.left = ll + 'px';
    doc.lstVars.style.left = ll + 'px';
    doc.lstPars.style.left = ll + 'px';
    //document.getElementById('lblRunHorizon').style.left = ll + 'px';
    document.getElementById('lblInstructions').style.left = ll + 'px';
    document.getElementById('lblPars').style.left = ll + 'px';

    var dd2 = ll + DispInd * 2 + SpaceBtwn;
    doc.lstFiles.style.left = dd2 + 'px';
    doc.lstFiles.style.width = dd + 'px';
    document.getElementById('lblFiles').style.left = dd2 + 'px';

}

function showhg(cmd) {
    var doc = document.forms[0];

    document.body.style.cursor = "wait";
    cmd.style.cursor = "wait";

}


function loadjsfile(filename) {
    var head = document.getElementsByTagName('head').item(0);
    var fileref = document.createElement('script');
    fileref.setAttribute("type", "text/javascript");
    fileref.setAttribute("src", filename);
    head.appendChild(fileref);
}


//'jrs 2010/12/10 client side interaction
function lstVars_click() {
    var doc = document.forms[0];
    var bFirst = true;

    var ppFilt = 0;

    //Load Dimensions
    LoadDims();

    //2011/04/12
    //Scroll down to display selected list when refreshing page
    if (doc.lstVars.scrollTop == 0 && !doc.lstVars[0].selected) {
        //doc.lstVars.scrollTop = doc.lstVars.selectedIndex;
        var idx = doc.lstVars.selectedIndex;
        doc.lstVars[idx].selected = false;
        doc.lstVars[idx].selected = true;
    }
    //if (doc.lstDim.scrollTop == 0 && !doc.lstDim[0].selected) {
    //    var idxDim = doc.lstDim.selectedIndex;
    //    doc.lstDim[idxDim].selected = false;
    //    doc.lstDim[idxDim].selected = true;
    //}
} // end of procedure

function LoadDims() {
    var doc = document.forms[0];
    var i = 0;
    var SelCount = 0;
    var DimSel = [];
    var DimSelTxt = [];
    var j = 0;
    var SelCountCheck = 0;
    var lstDim = document.getElementById("lstDim");
    var chkBoxCount = lstDim.getElementsByTagName("input");
    var labelRef = lstDim.getElementsByTagName('label');

    if (chkBoxCount.length>0 && Get_First_Selected_Idx('lstDim') > -1 && !chkBoxCount[chkBoxCount.length - 1].checked) {
        for (i = 0; i < chkBoxCount.length; i++) {
            if (chkBoxCount[i].checked) {
                DimSel[SelCount] = i;
                DimSelTxt[SelCount] = labelRef[i].innerText;
                SelCount++;
            }
        }
    }

        //JAM 2013-09-30 Optimize behavior in IE
        //doc.lstDim.childNodes[0].childNodes.length = 0;
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.getElementById("lstDim").innerHTML = "";
        }
        else {
            //doc.lstDim.childNodes[0].childNodes.length = 0;
            document.getElementById("lstDim").innerHTML = "";
        }

        //jrs 2012/12/06
        OrdDims = new Array();
        OrdDimsCount = 0;
        doc.txtDims.value = "";

        if (bUseGroup || bUseGroupDec) {
            //Load Groups
            for (i = 1; i <= iGroupCount; i++) {
                var opt = document.createElement("option");
                opt.text = Groups[i];
                opt.value = Groups[i];
                if (SelCountCheck < SelCount) {
                    if (Groups[i] == DimSelTxt[SelCountCheck]) {
                        DimSel[SelCountCheck] = i - 1;
                        SelCountCheck++;
                    }
                }

                try {
                    doc.lstDim.add(opt, null); // standards compliant; doesn't work in IE
                }
                catch (ex) {
                    doc.lstDim.add(opt); // IE only
                }
            }
        }
        else if (bUseGList) {
            //Load G-Lists
            for (i = 1; i <= iGListCount; i++) {
                var opt = document.createElement("option");
                opt.text = GLists[i];
                opt.value = GLists[i];
                if (SelCountCheck < SelCount) {
                    if (GLists[i] == DimSelTxt[SelCountCheck]) {
                        DimSel[SelCountCheck] = i - 1;
                        SelCountCheck++;
                    }
                }

                try {
                    doc.lstDim.add(opt, null); // standards compliant; doesn't work in IE
                }
                catch (ex) {
                    doc.lstDim.add(opt); // IE only
                }
            }
        }
        else {
            for (i = 1; i <= NR; i++) {
                //var opt = document.createElement("option");
                //opt.text = GRPS[1][i];
                //opt.value = GRPS[1][i];

                //try {
                    //doc.lstDim.add(opt, null); // standards compliant; doesn't work in IE
                    var tableRow = lstDim.insertRow();
                    var tableCell = tableRow.insertCell();

                    var checkBoxRef = document.createElement('input');
                    var labelRef = document.createElement('label');

                    checkBoxRef.type = 'checkbox';
                    labelRef.innerHTML = GRPS[1][i];
                    checkBoxRef.value = GRPS[1][i];

                    tableCell.appendChild(checkBoxRef);
                    tableCell.appendChild(labelRef);
                //}
                //catch (ex) {
                    //doc.lstDim.add(opt); // IE only
                //}
            }
        }

        if (SelCount == 0) {
            //doc.lstDim.selectedIndex = 0;
            chkBoxCount[0].checked = true;
        }
        else {
            for (i = 0; i < SelCount; i++) {
                if (DimSel[i] < chkBoxCount.length) {
                    //doc.lstDim[DimSel[i]].selected = true;
                    chkBoxCount[DimSel[i]].checked = true;
                }
                else { //jrs 2012/04/23
                    //doc.lstDim[0].selected = true;
                    chkBoxCount[0].checked = true;
                }
            }
        }
    
} // end of function

//jrs 2015/04/14
function Get_First_Selected_Idx(lst) {
    var ControlRef = document.getElementById(lst);
    var CheckBoxListArray = ControlRef.getElementsByTagName('input');

    for (var i = 0; i < CheckBoxListArray.length; i++) {
        if (CheckBoxListArray[i].checked == true) {
            return i;
        }
    }
    return -1;
}

//jrs 2015/04/14
function Set_First_Selected_Idx(lst, idx) {
    var ControlRef = document.getElementById(lst);
    var CheckBoxListArray = ControlRef.getElementsByTagName('input');
    ControlRef.selectedIndex = -1;

    for (var i = 0; i < CheckBoxListArray.length; i++) {
        
        if (i == idx) {
            CheckBoxListArray[i].checked = true;
            CheckBoxListArray[i].select = true;
            ControlRef.childNodes[0].childNodes[i].childNodes[0].childNodes[0].selected = true;
            ControlRef.selectedIndex = i;
        }
        else {
            CheckBoxListArray[i].checked = false;
            CheckBoxListArray[i].select = false;
            ControlRef.childNodes[0].childNodes[i].childNodes[0].childNodes[0].selected = false;
        }
    }
}