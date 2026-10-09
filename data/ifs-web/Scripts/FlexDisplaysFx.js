//jrs 2013/05/09 Moved to external file to improve loading, Jaime recomended it, but the testing showed it to be slower, trying it again
//dsb 2025/11/04 Helper function for restoring persisted filter selection\
function persistGeoMode(mode) {
    try {
        var doc = document.forms[0];
        if (doc.hdn_geoMode) doc.hdn_geoMode.value = String(mode);
        if (window.sessionStorage) {
            sessionStorage.setItem("geoMode", String(mode));
        }
    } catch (e) { }
}

function applyGeoMode(mode) {
    bUseReg = bUseGroup = bUseGroupDec = bUseGList = bUseCtryAndGrp = bUseCtryAndGrpDec = false;
    switch (Number(mode)) {
        case 1: bUseGroup = true; break;
        case 2: bUseGroupDec = true; break;
        case 3: bUseGList = true; break;
        case 4: bUseCtryAndGrp = true; break;
        case 5: bUseCtryAndGrpDec = true; break;
        default: bUseReg = true; break;
    }
}

//dsb 2026/05/20 Save last visisble dimension selections when moving through global variables, so that they can be restored on postback
var LastDim1SetTxt = [];
var LastDim2SetTxt = [];
var LastDim3SetTxt = [];
function saveListSelections(lst) {
    var vals = [];
    if (!lst || lst.style.visibility != "visible") return vals;
    for (var i = 0; i < lst.length; i++) {
        if (lst[i].selected && lst[i].text != "") {
            vals.push(lst[i].text);
        }
    }
    return vals;
}
//function restoreListSelections(lst, vals) {
//    if (!lst || !vals || vals.length == 0) return 0;
//    var restored = 0;
//    for (var i = 0; i < lst.length; i++) {
//        lst[i].selected = false;
//        for (var j = 0; j < vals.length; j++) {
//            if (lst[i].text == vals[j]) {
//                lst[i].selected = true;
//                restored++;
//                break;
//            }
//        }
//    }
//    return restored;
//}
function restoreListSelections(lst, vals) {
    if (!lst || !vals || vals.length == 0) return 0;
    var restored = 0;
    var matched = [];
    for (var i = 0; i < lst.length; i++) {
        for (var j = 0; j < vals.length; j++) {
            if (lst[i].text == vals[j]) {
                matched.push(i);
                break;
            }
        }
    }
    if (matched.length == 0) {
        return 0;
    }
    for (var i = 0; i < lst.length; i++) {
        lst[i].selected = false;
    }
    for (var k = 0; k < matched.length; k++) {
        lst[matched[k]].selected = true;
        restored++;
    }
    return restored;
}
//'jrs 2011/01/19 need dimension list boxes visible to hide them or show them from client side
function SetLists() {
    
    var doc = document.forms[0];
    //dsb 2025/11/4 Restore persisted filter selection (bUseGroup) from hidden field
    try {
        //var mode = (doc.hdn_geoMode && doc.hdn_geoMode.value) ? doc.hdn_geoMode.value : "0";
        var mode = "0";
        if (doc.hdn_geoMode && doc.hdn_geoMode.value && doc.hdn_geoMode.value.toString().trim() !== "") {
            mode = doc.hdn_geoMode.value.toString().trim();
        } else if (window.sessionStorage && sessionStorage.getItem('geoMode')) {
            mode = sessionStorage.getItem('geoMode').toString().trim();
        } 
        applyGeoMode(mode);
    } catch (e) { }

    var div1 = document.getElementById("div_dims");
    div1.style.display = "block";
    //2019/03/29
    if (doc.cboFilter.selectedIndex > 0) {
        cboFilter_Click();
    }
    else {
        cboFilter_Click(); //dsb 2025/08/18
        //jrs 2012/03/29 if (doc.lstCategories.selectedIndex > 0)
        if (!doc.lstCategories[0].selected) {
            //Save selected lists
            var doc = document.forms[0];
            //dsb 2025/08/10 store selected categories
            var selCat = [];
            var selCatCount = 0;
            for (var i = 0; i < doc.lstCategories.length; i++) {
                if (doc.lstCategories[i].selected) {
                    selCatCount++;
                    selCat[selCatCount] = doc.lstCategories[i].text;
                }
            }
            lstTopCats_click();
            //Reset selected categories
            doc.lstCategories.selectedIndex = -1;
            var j = 1;
            for (var i = 0; i < doc.lstCategories.length; i++) {
                if (doc.lstCategories[i].text == selCat[j]) {
                    j++;
                    doc.lstCategories[i].selected = true;
                    if (j > selCatCount) {
                        i = doc.lstCategories.length;
                    }
                }
            }

            var SelList = [];
            var SelCount = 0;
            for (var i = 0; i < doc.lstPacks.length; i++) {
                if (doc.lstPacks[i].selected) {
                    SelCount++;
                    SelList[SelCount] = doc.lstPacks[i].text;
                }
            }
            lstCats_click();
            //Reset selected lists
            doc.lstPacks.selectedIndex = -1;
            var j = 1;
            for (var i = 0; i < doc.lstPacks.length; i++) {
                if (doc.lstPacks[i].text == SelList[j]) {
                    j++;
                    doc.lstPacks[i].selected = true;
                    if (j > SelCount) {
                        i = doc.lstPacks.length;
                    }
                }
            }
            //jrs 2013/03/22
            //if packaged was not saved and looking for first one, and not found
            if (doc.lstPacks.selectedIndex == -1) {
                doc.lstPacks.selectedIndex = 0;
                doc.txtPacks.value = "";
            }
        }
    }

    lstPacks_click();

    //Sets Packs
    OrdLists = new Array();
    OrdListsCount = 0;
    //reload 
    if (doc.txtPacks.value > "") {
        var f = new Array();
        //jrs 2019/02/12 changed from , to ;
        f = doc.txtPacks.value.split(";");
        for (var i = 0; i < f.length; i++) {
            //jrs 2019/02/12 OrdLists["x" + f[i]] = f[i];
            for (var j = 0; j < doc.lstPacks.length; j++) {
                if (doc.lstPacks[j].text == f[i]) {
                    OrdLists["x" + j] = j;
                    j = doc.lstPacks.length;
                }        
            }        
            OrdListsCount++;
        }
    }

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

    //jrs 2012/04/11 resize lists
    pageResize();
}

//'jrs 2010/12/10 client side interaction
function lstPacks_click() {
    var doc = document.forms[0];
    var bFirst = true;
    var Dim1M = 0;
    var Dim2M = 0;
    var Dim3M = 0;
    var bDim2M = 0;
    var bDim3M = 0;

    var Dim1 = 0;
    var Dim2 = 0;
    var Dim3 = 0;
    var bDim2 = 0;
    var bDim3 = 0;

    //jrs 2025/06/11
    var bNoTot2M = 0;
    var bNoTot2 = 0;

    var ppFilt = 0;

    for (var pp = 0; pp < doc.lstPacks.length; pp++) {
        if (doc.lstPacks[pp].selected) {
            // jrs 2020/06/30 added && doc.cboFilter.selectedIndex == 0
            if (doc.lstCategories[0].value.toString() == "ALL" && doc.lstCategories.selectedIndex == 0 && doc.cboFilter.selectedIndex == 0) { //dsb 2025/08/11 Added ALL condition
                ppFilt = pp;
            }
            else {
                ppFilt = MapToList[pp];
            }

			
            Dim1M = DimXList[ppFilt][0];
            Dim2M = DimXList[ppFilt][1];
            Dim3M = DimXList[ppFilt][2];
            bDim2M = DimXList[ppFilt][3];
            bDim3M = DimXList[ppFilt][4];
            //jrs 2025/06/11
            bNoTot2M = DimXList[ppFilt][5];

            if (bFirst) {
                Dim1 = Dim1M;
                Dim2 = Dim2M;
                Dim3 = Dim3M;
                bDim2 = bDim2M;
                bDim3 = bDim3M;
                bFirst = false;
                //jrs 2025/06/11
                bNoTot2 = bNoTot2M;
            }
            else {
                //check for compatibility of dimensions
                if (Dim1M > 0 && Dim1 > 0) {
                    if (Dim1M != Dim1) {
                        DisableButtons();
                        alert('Incompatible Variable Lists, please select a different set.');
                        return;
                    }
                    //check 2nd dim
                    if (Dim2M > 0 && Dim2 > 0) {
                        if (Dim2M != Dim2) {
                            DisableButtons();
                            alert('Incompatible Variable Lists, please select a different set.');
                            return;
                        }
                        //if one of them needs the selection allow it
                        if (bDim2 == 0 && bDim2M == 1) {
                            bDim2 = bDim2M;
                            //jrs 2025/06/11
                            bNoTot2 = bNoTot2M;
                        }

                        //check 3rd dim
                        if (Dim3M > 0 && Dim3 > 0) {
                            if (Dim3M != Dim3) {
                                DisableButtons();
                                alert('Incompatible Variable Lists, please select a different set.');
                                return;
                            }
                            //if one of them needs the selection allow it
                            if (bDim3 == 0 && bDim3M == 1) {
                                bDim3 = bDim3M;
                            }
                        }
                        else if (Dim3M > 0) //if saved var has no dim 3, save the current one (M)
                        {
                            Dim1 = Dim1M;
                            Dim2 = Dim2M;
                            Dim3 = Dim3M;
                            bDim2 = bDim2M;
                            bDim3 = bDim3M;
                            //jrs 2025/06/11
                            bNoTot2 = bNoTot2M;
                        }
                    } // We have Dim2 in both current and previous selection
                    else if (Dim2M > 0) //if saved var has no dim 2, save the current one (M)
                    {
                        Dim1 = Dim1M;
                        Dim2 = Dim2M;
                        Dim3 = Dim3M;
                        bDim2 = bDim2M;
                        bDim3 = bDim3M;
                        //jrs 2025/06/11
                        bNoTot2 = bNoTot2M;
                    }
                } // We have Dim1 in both current and previous selection			                        
                else if (Dim1M > 0) //if saved var has no dims, save the current one (M)
                {
                    Dim1 = Dim1M;
                    Dim2 = Dim2M;
                    Dim3 = Dim3M;
                    bDim2 = bDim2M;
                    bDim3 = bDim3M;
                    //jrs 2025/06/11
                    bNoTot2 = bNoTot2M;
                }
            } // end of not first list selected
        } //list selected
    } // for each list

    doc.cmdExport.disabled = false;
    doc.cmdLine.disabled = false;
    doc.cmdTable.disabled = false;

    //Load Dimensions
    LoadDims(Dim1, Dim2, Dim3, bDim2, bDim3, bNoTot2);
    //jrs 2012/10/04 resize listbox widths
    if (myWidth != 0) {
        ChangeListWidth();
    }

    //jrs 2023/07/18
    if (doc.lstPacks.length > 0) {
        //Diable selecting Gender for Death Rates since Population is not gender specific.
        if (doc.lstPacks[doc.lstPacks.selectedIndex].text == "Death Rates (per Thousand) by Category of Cause" ||
            doc.lstPacks[doc.lstPacks.selectedIndex].text == "Death Rates (per Thousand) by Main ICD Categories") {
            if (doc.lstDim3.length == 3) {
                doc.lstDim3.remove(0);
                doc.lstDim3.remove(0);
                doc.lstDim3[0].selected = true;
            }
        }

        //2011/04/12
        //Scroll down to display selected list when refreshing page
        if (doc.lstPacks.scrollTop == 0 && !doc.lstPacks[0].selected) {
            //doc.lstPacks.scrollTop = doc.lstPacks.selectedIndex;
            var idx = doc.lstPacks.selectedIndex;
            doc.lstPacks[idx].selected = false;
            doc.lstPacks[idx].selected = true;
        }
        //dsb 2026/05/21
        if (doc.lstDim.scrollTop == 0 && doc.lstDim.length > 0 && !doc.lstDim[0].selected) {
            var idxDim = doc.lstDim.selectedIndex;
            if (idxDim >= 0 && idxDim < doc.lstDim.length) {
                doc.lstDim[idxDim].selected = false;
                doc.lstDim[idxDim].selected = true;
            }
        }
        //if (doc.lstDim.scrollTop == 0 && !doc.lstDim[0].selected) {
        //    var idxDim = doc.lstDim.selectedIndex;
        //    doc.lstDim[idxDim].selected = false;
        //    doc.lstDim[idxDim].selected = true;
        //}
    }
    //2011/04/19 hide list box if loading horizon
    if (doc.cmb_Horizon != null) {
        doc.lstDim.style.visibility = "hidden";
    }

} // end of procedure

//jrs 2025/06/11 added bNoTot2
function LoadDims(Dim1, Dim2, Dim3, bDim2, bDim3, bNoTot2) {
    var doc = document.forms[0];
    var i = 0;
    var SelCount = 0;
    var DimSel = [];
    var DimSel2 = [];
    var DimSel3 = [];
    var DimSelTxt = [];
    var DimSelTxt2 = [];
    var DimSelTxt3 = [];
    var j = 0;
    var SelCountCheck = 0;

    if (Dim1 != currDim1 || currDim1 == 0) {
        if (currDim1 == 0 && Dim1 > 0) {
            doc.lstDim.style.visibility = "visible";
            document.getElementById('lblDim').firstChild.data = "Geography:";
            //jrs 2022/05/03
            doc.chkAllDim1s.hidden = false;
            if (doc.lstDim.selectedIndex > -1 && !doc.lstDim[doc.lstDim.length - 1].selected) {
                for (i = 0; i < doc.lstDim.length; i++) {
                    if (doc.lstDim[i].selected) {
                        DimSel[SelCount] = i;
                        //jrs 2017/06/31
                        // if all variables are loaded, only save first NR
                        if (doc.lstDim.length > iGroupCount + iGListCount + MaxVal[1]) {
                            //if (i < MaxVal[1]) {
                            if (doc.txtDirtyDim.value == "") {
                                DimSelTxt[SelCount] = doc.lstDim[i].text;
                            }
                            else {
                                DimSelTxt[SelCount] = "";
                            }
                        }
                        else {
                            DimSelTxt[SelCount] = doc.lstDim[i].text;
                        }                        
                        SelCount++;
                    }
                }
            }
        }

        //JAM 2013-09-30 Optimize behavior in IE
        //doc.lstDim.length = 0;
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.getElementById("lstDim").innerHTML = "";
        }
        else {
            //dsb 2026/05/20 save current selections before clearing list to restore after repopulating
            if (currDim1 > 0) {
                LastDim1SetTxt = saveListSelections(doc.lstDim);
            }
            doc.lstDim.length = 0;
        }
        currDim1 = Dim1;
        //jrs 2012/12/06
        OrdDims = new Array();
        OrdDimsCount = 0;
        //jrs 2026/01/26 added || Dim1 == 0 to reset when no dimensions
        //jrs 2023/07/14 only initialize if it's not a postback
        if (IsPostBack4JS = 0 || Dim1 == 0) {
            doc.txtDims.value = "";
            //jrs 2017/07/03
            doc.txtDirtyDim.value = "";
        }

        if (Dim1 == 1 && (bUseGroup || bUseGroupDec)) {
            //Load Groups
            //cdv 2021/08/23 ****START****
            if (!chkHideNonCoreGroups.checked) {
                for (i = 1; i <= iGroupCount; i++) {
                    var opt = document.createElement("option");
                    opt.text = Groups[i];
                    opt.value = Groups[i];
                    if (SelCountCheck < SelCount) {
                        if (Groups[i] == DimSelTxt[SelCountCheck]) {
                            //jrs 2023/07/14 DimSel[SelCountCheck] = i - 1 + MaxVal[1] - 1;
                            DimSel[SelCountCheck] = i - 1 + MaxValTot;
                            SelCountCheck++;
                            opt.selected = true; //cdv 2021/09/08 
                            
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
                for (i = 1; i <= iCoreGroupCount; i++) {
                    var opt = document.createElement("option");
                    opt.text = CoreGroups[i];
                    opt.value = CoreGroups[i];
                    if (SelCountCheck < SelCount) {
                        if (CoreGroups[i] == DimSelTxt[SelCountCheck]) {
                            //jrs 2023/07/14 DimSel[SelCountCheck] = i - 1 + MaxVal[1] - 1;
                            DimSel[SelCountCheck] = i - 1 + MaxValTot;
                            SelCountCheck++;
                            opt.selected = true; //cdv 2021/09/08 
                            
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
            //cdv 2021/08/23 ****END****
        }
        else if (Dim1 == 1 && bUseGList) {
            //Load G-Lists
            for (i = 1; i <= iGListCount; i++) {
                var opt = document.createElement("option");
                opt.text = GLists[i];
                opt.value = GLists[i];
                if (SelCountCheck < SelCount) {
                    if (GLists[i] == DimSelTxt[SelCountCheck]) {
                        DimSel[SelCountCheck] = i - 1;
                        SelCountCheck++;
                        opt.selected = true; //jrs 2023/07/14 
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
            //jrs 2025/05/30 added || bUseCtryAndGrpDec
            //jrs 2016/09/30
        else if (Dim1 == 1 && (bUseCtryAndGrp || bUseCtryAndGrpDec)) {
            //Load Countries
            for (i = 1; i < MaxVal[1]; i++) {
                var opt = document.createElement("option");
                opt.text = GRPS[1][i];
                opt.value = GRPS[1][i];
                if (SelCountCheck < SelCount) {
                    if (GRPS[1][i] == DimSelTxt[SelCountCheck]) {
                        DimSel[SelCountCheck] = i - 1;
                        SelCountCheck++;
                        opt.selected = true; //jrs 2023/07/14
                    }
                }

                try {
                    doc.lstDim.add(opt, null); // standards compliant; doesn't work in IE
                }
                catch (ex) {
                    doc.lstDim.add(opt); // IE only
                }
            }
            //Load Groups
            //CDV 2021/08/23 ****START****
            if (!chkHideNonCoreGroups.checked) {
                for (i = 1; i <= iGroupCount; i++) {
                    var opt = document.createElement("option");
                    opt.text = Groups[i];
                    opt.value = Groups[i];
                    if (SelCountCheck < SelCount) {
                        if (Groups[i] == DimSelTxt[SelCountCheck]) {
                            //jrs 2023/07/14 DimSel[SelCountCheck] = i - 1 + MaxVal[1] - 1;
                            DimSel[SelCountCheck] = i - 1 + MaxValTot;
                            SelCountCheck++;
                            opt.selected = true; //cdv 2021/09/08 
                            
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
                for (i = 1; i <= iCoreGroupCount; i++) {
                    var opt = document.createElement("option");
                    opt.text = CoreGroups[i];
                    opt.value = CoreGroups[i];
                    if (SelCountCheck < SelCount) {
                        if (CoreGroups[i] == DimSelTxt[SelCountCheck]) {
                            //jrs 2023/07/14 DimSel[SelCountCheck] = i - 1 + MaxVal[1] - 1;
                            DimSel[SelCountCheck] = i - 1 + MaxValTot;
                            SelCountCheck++;
                            opt.selected = true; //cdv 2021/09/08 
                            
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
            //CDV 2021/08/23 ****END****
        }
        else if (Dim1 == 0) //handle Variables with no Dimensions (WGDP)
        {
            var opt = document.createElement("option");
            opt.text = "";
            opt.value = "";
            if (SelCount > 0) {
                SelCount = 1;
                DimSel[0] = 0;
            }

            try {
                doc.lstDim.add(opt, null); // standards compliant; doesn't work in IE
            }
            catch (ex) {
                doc.lstDim.add(opt); // IE only
            }

            doc.lstDim.style.visibility = "hidden";
            document.getElementById('lblDim').firstChild.data = "";
            //jrs 2022/05/03
            doc.chkAllDim1s.hidden = true;
        }
        else {
            for (i = 1; i < MaxVal[Dim1]; i++) {
                var opt = document.createElement("option");
                opt.text = GRPS[Dim1][i];
                opt.value = GRPS[Dim1][i];
                //jrs 2023/07/14 if (Dim1 > 1 && SelCountCheck < SelCount) {
                if (SelCountCheck < SelCount) {
                    if (GRPS[Dim1][i] == DimSelTxt[SelCountCheck]) {
                        DimSel[SelCountCheck] = i - 1;
                        SelCountCheck++;
                        opt.selected = true; //jrs 2023/07/14
                    }
                }

                try {
                    doc.lstDim.add(opt, null); // standards compliant; doesn't work in IE
                }
                catch (ex) {
                    doc.lstDim.add(opt); // IE only
                }
            }
            //jrs 2009/01/21
            if ((GRPS[Dim1][MaxVal[Dim1] - 1]).toUpperCase() != "TOTAL" && Dim1 > 1) {
                var opt = document.createElement("option");
                opt.text = "Total";
                opt.value = "Total";
                if (Dim1 > 1 && SelCountCheck < SelCount) {
                    if ("Total" == DimSelTxt[SelCountCheck]) {
                        DimSel[SelCountCheck] = MaxVal[Dim1] - 1;
                        SelCountCheck++;
                        opt.selected = true; //jrs 2023/07/14
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
        //dsb 2026/08/12 Order of selection is lost between first two countries/groups selected, when they are selected individually
        if (SelCount == 1 && doc.txtDims && doc.txtDims.value == "") {
            for (var i = 0; i < doc.lstDim.length; i++) {
                if (doc.lstDim[i].selected) {
                    doc.txtDims.value = i.toString();
                    break;
                }
            }
        }

        if (SelCount == 0 || doc.lstDim.selectedIndex == -1) {
            //dsb 2026/05/20 if there is a saved selection that is not found, don't default to first item since that may not be expected
            //if (Dim1 > 0 && restoreListSelections(doc.lstDim, LastDim1SetTxt) == 0) {
            //    doc.lstDim.selectedIndex = 0;
            //}
            var restoreResult = 0;
            if (SelCount == 0 && Dim1 > 0) {
                if (LastDim1SetTxt.length == 0) {
                    try {
                        var savedDim1 = sessionStorage.getItem("LastDim1SetTxt");
                        if (savedDim1 && savedDim1.length > 0) {
                            LastDim1SetTxt = savedDim1.split("|");
                        }
                    } catch (e) { }
                }
                restoreResult = restoreListSelections(doc.lstDim, LastDim1SetTxt);
            }

            if (restoreResult == 0 && doc.lstDim.length > 0) {
                doc.lstDim.selectedIndex = 0;
            }
            //doc.lstDim.selectedIndex = 0;
        }
        //jrs 2023/07/14 this is selecting a second group when only one has been selected
        ////cdv 2021/09/08 ****START****
        //else {
        //    for (i = 0; i < SelCount; i++) {
        //        if (DimSel[i] < doc.lstDim.length) {
        //            doc.lstDim[DimSel[i]].selected = true;
        //        }
        //        else { //jrs 2012/04/23
        //            if (SelCount == 0) {
        //                doc.lstDim[0].selected = true;
        //            }
        //        }
        //    }
        //cdv 2021/09/08 ****END****
        //}

    } // if changing first dimension

    if (Dim2 != currDim2 || currDim2 == 0 || (doc.lstDim2.style.visibility == "visible" && bDim2 == 0) || (doc.lstDim2.style.visibility == "hidden" && bDim2 == 1)) {
        SelCount2 = 0;
        //jrs 2025/06/12
        doc.cmdMirror.style.visibility = "hidden";
        //dsb 2026/05/20 save current selections before clearing list to restore after repopulating
        if (currDim2 > 0) {
            LastDim2SetTxt = saveListSelections(doc.lstDim2);
        }
        if (Dim2 > 0 && bDim2 == 1) {
            doc.lstDim2.style.visibility = "visible";
            document.getElementById('lblDim2').firstChild.data = "2nd Dimensions:";
            //jrs 2025/06/12
            if (Dim2 == 1) {
                doc.cmdMirror.style.visibility = "visible";
            }
            else {
                doc.cmdMirror.style.visibility = "hidden";
            }

            if (doc.lstDim2.selectedIndex > -1 && !doc.lstDim2[doc.lstDim2.length - 1].selected) {
                for (i = 0; i < doc.lstDim2.length; i++) {
                    if (doc.lstDim2[i].selected) {
                        DimSel2[SelCount2] = i;
                        DimSelTxt2[SelCount2] = doc.lstDim2[i].text;
                        SelCount2++;
                    }
                }
            }
        }
        else {
            doc.lstDim2.style.visibility = "hidden";
            document.getElementById('lblDim2').firstChild.data = "";
        }
        ////dsb 2026/05/20 save current selections before clearing list to restore after repopulating
        //if (currDim2 > 0) {
        //    LastDim2SetTxt = saveListSelections(doc.lstDim2);
        //}
        doc.lstDim2.length = 0;
        SelCountCheck = 0;
        //jrs 2025/06/05
        currDim2 = Dim2;
        if (Dim2 > 0 && bDim2 == 1) {
            //jrs 2025/02/11 Bilateral Dim2 for groups
            if (Dim2 == 1 && (bUseGroup || bUseGroupDec)) {
                //Load Groups
                if (!chkHideNonCoreGroups.checked) {
                    for (i = 1; i <= iGroupCount; i++) {
                        var opt = document.createElement("option");
                        opt.text = Groups[i];
                        opt.value = Groups[i];
                        if (SelCountCheck < SelCount2) {
                            if (Groups[i] == DimSelTxt2[SelCountCheck]) {
                                //jrs 2023/07/14 DimSel[SelCountCheck] = i - 1 + MaxVal[1] - 1;
                                DimSel2[SelCountCheck] = i - 1 + MaxValTot;
                                SelCountCheck++;
                                opt.selected = true; //cdv 2021/09/08 

                            }
                        }

                        try {
                            doc.lstDim2.add(opt, null); // standards compliant; doesn't work in IE
                        }
                        catch (ex) {
                            doc.lstDim2.add(opt); // IE only
                        }
                    }
                }
                else {
                    for (i = 1; i <= iCoreGroupCount; i++) {
                        var opt = document.createElement("option");
                        opt.text = CoreGroups[i];
                        opt.value = CoreGroups[i];
                        if (SelCountCheck < SelCount2) {
                            if (CoreGroups[i] == DimSelTxt2[SelCountCheck]) {
                                //jrs 2023/07/14 DimSel[SelCountCheck] = i - 1 + MaxVal[1] - 1;
                                DimSel2[SelCountCheck] = i - 1 + MaxValTot;
                                SelCountCheck++;
                                opt.selected = true; //cdv 2021/09/08 

                            }

                        }

                        try {
                            doc.lstDim2.add(opt, null); // standards compliant; doesn't work in IE
                        }
                        catch (ex) {
                            doc.lstDim2.add(opt); // IE only
                        }
                    }
                }
            }
            else if (Dim2 == 1 && bUseGList) { //jrs 2025/06/12
                //Load G-Lists
                for (i = 1; i <= iGListCount; i++) {
                    var opt = document.createElement("option");
                    opt.text = GLists[i];
                    opt.value = GLists[i];
                    if (SelCountCheck < SelCount2) {
                        if (GLists[i] == DimSelTxt[SelCountCheck]) {
                            DimSel[SelCountCheck] = i - 1;
                            SelCountCheck++;
                            opt.selected = true; //jrs 2023/07/14 
                        }
                    }

                    try {
                        doc.lstDim2.add(opt, null); // standards compliant; doesn't work in IE
                    }
                    catch (ex) {
                        doc.lstDim2.add(opt); // IE only
                    }
                }

            }
                //jrs 2025/05/30 added || bUseCtryAndGrpDec
            else if (Dim2 == 1 && (bUseCtryAndGrp || bUseCtryAndGrpDec)) {
                //Load Countries
                for (i = 1; i < MaxVal[1]; i++) {
                    var opt = document.createElement("option");
                    opt.text = GRPS[1][i];
                    opt.value = GRPS[1][i];
                    if (SelCountCheck < SelCount2) {
                        if (GRPS[1][i] == DimSelTxt2[SelCountCheck]) {
                            DimSel2[SelCountCheck] = i - 1;
                            SelCountCheck++;
                            opt.selected = true; //jrs 2023/07/14
                        }
                    }

                    try {
                        doc.lstDim2.add(opt, null); // standards compliant; doesn't work in IE
                    }
                    catch (ex) {
                        doc.lstDim2.add(opt); // IE only
                    }
                }
                //Load Groups
                //CDV 2021/08/23 ****START****
                if (!chkHideNonCoreGroups.checked) {
                    for (i = 1; i <= iGroupCount; i++) {
                        var opt = document.createElement("option");
                        opt.text = Groups[i];
                        opt.value = Groups[i];
                        if (SelCountCheck < SelCount2) {
                            if (Groups[i] == DimSelTxt2[SelCountCheck]) {
                                //jrs 2023/07/14 DimSel[SelCountCheck] = i - 1 + MaxVal[1] - 1;
                                DimSel2[SelCountCheck] = i - 1 + MaxValTot;
                                SelCountCheck++;
                                opt.selected = true; //cdv 2021/09/08 

                            }
                        }

                        try {
                            doc.lstDim2.add(opt, null); // standards compliant; doesn't work in IE
                        }
                        catch (ex) {
                            doc.lstDim2.add(opt); // IE only
                        }
                    }
                }
                else {
                    for (i = 1; i <= iCoreGroupCount; i++) {
                        var opt = document.createElement("option");
                        opt.text = CoreGroups[i];
                        opt.value = CoreGroups[i];
                        if (SelCountCheck < SelCount2) { //dsb 2026/06/05 Changed from SelCount to SelCount2 since we're checking against the second dimension saved selections
                            if (CoreGroups[i] == DimSelTxt2[SelCountCheck]) {
                                //jrs 2023/07/14 DimSel[SelCountCheck] = i - 1 + MaxVal[1] - 1;
                                DimSel2[SelCountCheck] = i - 1 + MaxValTot;
                                SelCountCheck++;
                                opt.selected = true; //cdv 2021/09/08 

                            }
                        }

                        try {
                            doc.lstDim2.add(opt, null); // standards compliant; doesn't work in IE
                        }
                        catch (ex) {
                            doc.lstDim2.add(opt); // IE only
                        }
                    }
                }
                //CDV 2021/08/23 ****END****
            }
            else {
                //cdv 2021/02/02 ****START**** Implement reading our global var, and logic for changing gender order
                var ElementHolder = document.getElementById('MaleFirst');
                var bMaleFirst = ElementHolder.value;
                currDim2 = Dim2
                for (i = 1; i < MaxVal[Dim2]; i++) {
                    var opt = document.createElement("option");
                    if (GRPS[Dim2][i] != "Female" && GRPS[Dim2][i] != "Male") {
                        opt.text = GRPS[Dim2][i];
                        opt.value = GRPS[Dim2][i];
                    } else if (bMaleFirst == "true") {
                        opt.text = GRPS[Dim2][i];
                        opt.value = GRPS[Dim2][i];
                    } else if (GRPS[Dim2][i] == "Male") {
                        opt.text = "Female";
                        opt.value = "Female";
                    } else {
                        opt.text = "Male";
                        opt.value = "Male";
                    }
                    //cdv 2021/02/02 ****END****
                    if (Dim2 > 1 && SelCountCheck < SelCount2) {
                        if (GRPS[Dim2][i] == DimSelTxt2[SelCountCheck]) {
                            //jrs 2024/04/12
                            if (GRPS[Dim2][i] != "Female" && GRPS[Dim2][i] != "Male") {
                                DimSel2[SelCountCheck] = i - 1;
                            } else if (bMaleFirst == "true") {
                                DimSel2[SelCountCheck] = i - 1;
                            } else if (GRPS[Dim2][i] == "Male") {
                                DimSel2[SelCountCheck] = i;
                            } else {
                                DimSel2[SelCountCheck] = i - 2;
                            }

                            SelCountCheck++;
                        }
                    }

                    try {
                        doc.lstDim2.add(opt, null); // standards compliant; doesn't work in IE
                    }
                    catch (ex) {
                        doc.lstDim2.add(opt); // IE only
                    }

                }
            }
            //jrs 2025/06/11 added && bNoTot2 == 0
            //jrs 2009/01/21
            if ((GRPS[Dim2][MaxVal[Dim2] - 1]).toUpperCase() != "TOTAL" && Dim2 > 1 && bNoTot2 == 0)
            {
                var opt = document.createElement("option");
                opt.text = "Total";
                opt.value = "Total";
                if (Dim2 > 1 && SelCountCheck < SelCount) {
                    if ("Total" == DimSelTxt2[SelCountCheck]) {
                        DimSel2[SelCountCheck] = MaxVal[Dim2]-1;
                        SelCountCheck++;
                    }
                }

                try {
                    doc.lstDim2.add(opt, null); // standards compliant; doesn't work in IE
                }
                catch (ex) {
                    doc.lstDim2.add(opt); // IE only
                }
            }

        }
        else {
            currDim2 = 0;
            var opt = document.createElement("option");
            opt.text = "";
            opt.value = "";
            if (SelCount2 > 0) {
                SelCount2 = 1;
                DimSel2[0] = 0;
            }

            try {
                doc.lstDim2.add(opt, null); // standards compliant; doesn't work in IE
            }
            catch (ex) {
                doc.lstDim2.add(opt); // IE only
            }
        }

        if (SelCount2 == 0) {
            //dsb 2026/05/20 if there is a saved selection that is not found, don't default to first item since that may not be expected
            //var restoreResult = 0;
            //if (Dim2 > 0 && bDim2 == 1) {
            //    restoreResult = restoreListSelections(doc.lstDim2, LastDim2SetTxt);
            //}
            //if (restoreResult == 0) {
            //    if (currDim2 == 23 && doc.lstDim2.length > 2) {
            //        doc.lstDim2.selectedIndex = 2;
            //    } else if (doc.lstDim2.length > 0){
            //        doc.lstDim2.selectedIndex = 0;
            //    }
            //}
            if (LastDim2SetTxt.length == 0) {
                try {
                    var savedDim2 = sessionStorage.getItem("LastDim2SetTxt");
                    if (savedDim2 && savedDim2.length > 0) {
                        LastDim2SetTxt = savedDim2.split("|");
                    }
                } catch (e) { }
            }
            var restoreResult2 = 0;
            if (Dim2 > 0 && bDim2 == 1) {
                restoreResult2 = restoreListSelections(doc.lstDim2, LastDim2SetTxt);
            }
            if (restoreResult2 == 0) {
                if (currDim2 == 23 && doc.lstDim2.length > 2) {
                    doc.lstDim2.selectedIndex = 2;
                }
                else if (doc.lstDim2.length > 0) {
                    doc.lstDim2.selectedIndex = 0;
                }
            }
            //if (currDim2 == 23) {
            //    doc.lstDim2.selectedIndex = 2;
            //}
            //else {
            //    doc.lstDim2.selectedIndex = 0;
            //}
        }
        else {
            //dsb 2026/05/21
            var selected2 = 0;

            //dsb 2026/06/05 added check for selected2 == 0 to avoid overwriting a restored selection with an invalid selection from DimSel2 array
            if (selected2 == 0 && DimSelTxt2.length > 0) {
                selected2 = restoreListSelections(doc.lstDim2, DimSelTxt2);
            }
            if (selected2 == 0) {
                for (i = 0; i < SelCount2; i++) {
                    if (DimSel2[i] >= 0 && DimSel2[i] < doc.lstDim2.length) {
                        doc.lstDim2[DimSel2[i]].selected = true;
                        selected2++;
                    }
                }
            }

            if (selected2 == 0 && doc.lstDim2.length > 0) {
                if (currDim2 == 23) {
                    doc.lstDim2.selectedIndex = 2;
                }
                else {
                    doc.lstDim2.selectedIndex = 0;
                }
            }
            //for (i = 0; i < SelCount2; i++) {
            //    //jrs 2023/11/01
            //    if (DimSel2[i] > doc.lstDim2.length - 1) {
            //        //jrs 2025/06/02 This was selecting a second group when only one was being selected
            //        //DimSel2[i] = doc.lstDim2.length - 1;
            //        break;
            //    }

            //    doc.lstDim2[DimSel2[i]].selected = true;
            //}
        }

    } // if changing second dimension

    if (Dim3 != currDim3 || currDim3 == 0 || (doc.lstDim3.style.visibility == "visible" && bDim3 == 0) || (doc.lstDim3.style.visibility == "hidden" && bDim3 == 1)) {
        SelCount3 = 0;
        if (Dim3 > 0 && bDim3 == 1) {

            doc.lstDim3.style.visibility = "visible";
            document.getElementById('lblDim3').firstChild.data = "3rd Dimensions:";
            if (doc.lstDim3.selectedIndex > -1 && !doc.lstDim3[doc.lstDim3.length - 1].selected) {
                for (i = 0; i < doc.lstDim3.length; i++) {
                    if (doc.lstDim3[i].selected) {
                        DimSel3[SelCount3] = i;
                        DimSelTxt3[SelCount3] = doc.lstDim3[i].text;
                        SelCount3++;
                    }
                }
            }
        }
        else {
            doc.lstDim3.style.visibility = "hidden";
            document.getElementById('lblDim3').firstChild.data = "";
        }
        doc.lstDim3.length = 0;
        SelCountCheck = 0;
        if (Dim3 > 0 && bDim3 == 1) {
            currDim3 = Dim3
            //cdv 2021/02/02 ****START**** Implement reading our global var, and logic for changing gender order
            var ElementHolder = document.getElementById('MaleFirst');
            var bMaleFirst = ElementHolder.value;
            for (i = 1; i < MaxVal[Dim3]; i++) {
                var opt = document.createElement("option");
                if (GRPS[Dim3][i] != "Female" && GRPS[Dim3][i] != "Male") {
                    opt.text = GRPS[Dim3][i];
                    opt.value = GRPS[Dim3][i];
                } else if (bMaleFirst == "true") {
                    opt.text = GRPS[Dim3][i];
                    opt.value = GRPS[Dim3][i];
                } else if (GRPS[Dim3][i] == "Male") {
                    opt.text = "Female";
                    opt.value = "Female";
                } else {
                    opt.text = "Male";
                    opt.value = "Male";
                }
                 //cdv 2021/02/02 ****END****
                if (Dim3 > 1 && SelCountCheck < SelCount3) {
                    if (GRPS[Dim3][i] == DimSelTxt3[SelCountCheck]) {
                        DimSel3[SelCountCheck] = i - 1;
                        SelCountCheck++;
                    }
                }

                try {
                    doc.lstDim3.add(opt, null); // standards compliant; doesn't work in IE
                }
                catch (ex) {
                    doc.lstDim3.add(opt); // IE only
                }
            }
            //jrs 2009/01/21
            if ((GRPS[Dim3][MaxVal[Dim3] - 1]).toUpperCase() != "TOTAL" && Dim3 > 1)
            {
                var opt = document.createElement("option");
                opt.text = "Total";
                opt.value = "Total";
                if (Dim3 > 1 && SelCountCheck < SelCount) {
                    if ("Total" == DimSelTxt3[SelCountCheck]) {
                        DimSel3[SelCountCheck] = MaxVal[Dim3]-1;
                        SelCountCheck++;
                    }
                }

                try {
                    doc.lstDim3.add(opt, null); // standards compliant; doesn't work in IE
                }
                catch (ex) {
                    doc.lstDim3.add(opt); // IE only
                }
            }

        }
        else {
            currDim3 = 0;
            var opt = document.createElement("option");
            opt.text = "";
            opt.value = "";
            if (SelCount3 > 0) {
                SelCount3 = 1;
                DimSel3[0] = 0;
            }
            try {
                doc.lstDim3.add(opt, null); // standards compliant; doesn't work in IE
            }
            catch (ex) {
                doc.lstDim3.add(opt); // IE only
            }
        }

        if (SelCount3 == 0) {
            if (currDim3 == 23) {
                doc.lstDim3.selectedIndex = 2;
            }
            else {
                doc.lstDim3.selectedIndex = 0;
            }
        }
        else {
            for (i = 0; i < SelCount3; i++) {
                doc.lstDim3[DimSel3[i]].selected = true;
            }
        }
    } // if changing third dimension

    // set position for 3rd list
    if (doc.lstDim3.style.visibility == "visible") {
        if (doc.lstDim2.style.visibility == "visible") {
            doc.lstDim3.style.left = "1104px";
            document.getElementById('lblDim3').style.left = "1104px";
        }
        else {
            doc.lstDim3.style.left = "904px";
            document.getElementById('lblDim3').style.left = "904px";
        }
    }
} // end of function

//dsb 2026/05/22 
function saveDimsBeforeDisplay() {
    var doc = document.forms[0];
    if (doc.lstDim && doc.lstDim.length > 1 && doc.lstDim.style.visibility == "visible") {
        //var dim1 = [];
        var dim1Text = [];
        for (var i = 0; i < doc.lstDim.length; i++) {
            if (doc.lstDim[i].selected && doc.lstDim[i].text != "") {
                //dim1.push(i);
                dim1Text.push(doc.lstDim[i].text);
            }
        }
        //doc.txtDims.value = dim1.join(",");
        if (dim1Text.length > 0) {
            LastDim1SetTxt = dim1Text;
            try {
                sessionStorage.setItem("LastDim1SetTxt", dim1Text.join("|"));
            }
            catch (ex) { }
        }
    }
    //else {
    //    doc.txtDims.value = "";
    //}
    if (doc.lstDim2 && doc.lstDim2.length > 1 && doc.lstDim2.style.visibility == "visible") {
        //var dim2 = [];
        var dim2Text = [];
        for (var j = 0; j < doc.lstDim2.length; j++) {
            if (doc.lstDim2[j].selected && doc.lstDim2[j].text != "") {
                //dim2.push(j);
                dim2Text.push(doc.lstDim2[j].text);
            }
        }
        //doc.txtDims2.value = dim2.join(",");
        if (dim2Text.length > 0) {
            LastDim2SetTxt = dim2Text;
            try {
                sessionStorage.setItem("LastDim2SetTxt", dim2Text.join("|"));
            }
            catch (ex) { }
        }
    }
    //else {
    //    doc.txtDims2.value = "";
    //}
}

function DisableButtons() {
    var doc = document.forms[0];
    doc.cmdExport.disabled = true;
    doc.cmdLine.disabled = true;
    doc.cmdTable.disabled = true;
}

//dsb 2025/07/30
function lstTopCats_click() {
    var PrevSelCatCount = 0;
    var PrevSelCat = [];
    var i = 0;
    var j = 0;
    var doc = document.forms[0];

    for (i = 0; i < doc.lstCategories.length; i++) {
        if (doc.lstCategories[i].selected) {
            PrevSelCat[PrevSelCatCount] = doc.lstCategories[i].text;
            PrevSelCatCount++;
        }
    }

    //filter categories
    doc.lstCategories.length = 0;
    //if All selected, then no filter necessary
    var sFilter = doc.cboFilter.selectedIndex.toString();
    if (doc.cboFilter.selectedIndex < 10) sFilter = "0" + sFilter;
    if (doc.lstTopCategories[0].value.toString() == "ALL" && doc.lstTopCategories[0].selected) {
        FilterCatCount = 0;
        var optAll = document.createElement("option");
        optAll.text = "ALL";
        optAll.value = "ALL";
        try {
            doc.lstCategories.add(optAll, null);
        }
        catch (ex) {
            doc.lstCategories.add(optAll);
        }
        
        for (i = 1; i <= CatCount; i++) {
            //Condition to add No Filter or Filter checked
            if (doc.cboFilter.selectedIndex == 0 || CatXPriority[i][1].indexOf(sFilter) >= 0) {
                var opt = document.createElement("option");
                opt.text = CatXPriority[i][0];
                opt.value = CatXPriority[i][0];
                try {
                    doc.lstCategories.add(opt, null);
                }
                catch (ex) {
                    doc.lstCategories.add(opt);
                }
                //if using filter we also need a map even in Cat ALL
                if (doc.cboFilter.selectedIndex > 0) {
                    MapToCat[i] = FilterCatCount + 1;
                    FilterCatCount++;
                }
            }
        }
    }
    else //Category Selected
    {
        //dsb 2025/08/18
        var topCatSelected = new Set();
        for (var topcatid = 0; topcatid < doc.lstTopCategories.length; topcatid++) {
            if (doc.lstTopCategories[topcatid].selected) {
                topCatSelected.add(topcatid.toString());
            }
        }

        let firstTopCatSet = false;//dsb 2025/08/12 
        FilterCatCount = 0;
        var categoriesToDisplay = []; //dsb 2026/07/14
        for (i = 1; i <= CatCount; i++) {
            var j = CatXPriority[i][3]; //Categoryid
            var defined = 1;//dsb 2025/08/22 Checks if the category is displayed for different filters
            if (doc.cboFilter.selectedIndex > 0) {
                j = MapToTopCat[j];
                if (j == undefined || j == null)
                    defined = 0;
                else defined = 1;

            } //dsb 2025/08/22
            if (j >= 0 && defined && doc.lstTopCategories[j].selected) {
                //dsb 2025/08/12 ***start***
                if (firstTopCatSet == false) {
                    var opt = document.createElement("option");
                    if (topCatSelected.size > 1) {//dsb 2025/08/18 *** start ***
                        opt.text = "All Categories Available";
                        opt.value = "All Categories Available";
                    } else {//dsb 2025/08/18 ***end***
                        opt.text = "ALL - " + CatXPriority[i][2];
                        opt.value = "ALL - " + CatXPriority[i][2];
                    }
                    try {
                        doc.lstCategories.add(opt, null); 
                    }
                    catch (ex) {
                        doc.lstCategories.add(opt); 
                    }
                    firstTopCatSet = true;
                }//dsb 2025/08/12 ***end***

                //Condition to add No Filter or Filter checked
                if (doc.cboFilter.selectedIndex == 0 || CatXPriority[i][1].indexOf(sFilter) >= 0) {
                    var displayText; //dsb 2026/07/14 Display categories in alphabetical order
                    if (topCatSelected.size > 1) {
                        displayText = CatXPriority[i][0];
                    }
                    else {
                        displayText = CatXPriority[i][4];
                    }
                    categoriesToDisplay.push({
                        catIndex: i,
                        text: displayText,
                        topCatName: CatXPriority[i][2]
                    });
                }
            }
        }

        //dsb 2026/07/14 Sort using the displayed category text
        categoriesToDisplay.sort(function (a, b) {
            var aText = a.text.toUpperCase();
            var bText = b.text.toUpperCase();
            if (aText < bText) return -1;
            if (aText > bText) return 1;
            return 0;
        });

        //dsb 2026/07/14 Add the categories after sorting and rebuild MapToCat
        for (var categoryIndex = 0; categoryIndex < categoriesToDisplay.length; categoryIndex++) {
            var category = categoriesToDisplay[categoryIndex];
            var opt = document.createElement("option");
            opt.text = category.text;
            opt.value = category.text;
            try {
                doc.lstCategories.add(opt, null);
            }
            catch (ex) {
                doc.lstCategories.add(opt);
            }
            MapToCat[category.catIndex] = FilterCatCount + 1;
            FilterCatCount++;
        }     

                ////Condition to add No Filter or Filter checked
                //if (doc.cboFilter.selectedIndex == 0 || CatXPriority[i][1].indexOf(sFilter) >= 0) {
                //    MapToCat[i] = FilterCatCount + 1;
                //    FilterCatCount++;

                //    var opt = document.createElement("option");
                //    if (topCatSelected.size > 1) { //dsb 2025/08/18 when multiple categories are selected, display original category name
                //        opt.text = CatXPriority[i][0];
                //        opt.value = CatXPriority[i][0];
                //    } else {
                //        opt.text = CatXPriority[i][4]; //dsb 2025/08/13 Changed the category name displayed in ui
                //        opt.value = CatXPriority[i][4];
                //    }
                //    try {
                //        doc.lstCategories.add(opt, null); // standards compliant; doesn't work in IE
                //    }
                //    catch (ex) {
                //        doc.lstCategories.add(opt); // IE only
                //    }
                //    j = doc.lstTopCategories.length;
                //}   
    }
    // Restore previously selected categories
    if (PrevSelCatCount > 0) {
        var SelCatCount = 0;
        //dsb 2026/07/10 Clear current selections
        for (i = 0; i < doc.lstCategories.length; i++) {
            doc.lstCategories[i].selected = false;
        }
        for (i = 0; i < doc.lstCategories.length; i++) { //dsb 2026/07/10
            var currentCategory = getCanonicalCategoryText(doc.lstCategories[i].text);
            for (j = 0; j < PrevSelCatCount; j++) {
                var previousCategory = getCanonicalCategoryText(PrevSelCat[j]);
                if (currentCategory == previousCategory) {
                    doc.lstCategories[i].selected = true;
                    SelCatCount++;
                    break;
                }
            }
        }
        if (SelCatCount == 0 && doc.lstCategories.length > 0) {
            doc.lstCategories.selectedIndex = 0;
        }
    }
    else if (doc.lstCategories.length > 0) {
        doc.lstCategories.selectedIndex = 0;
    }
}

//dsb 2026/07/10 Always return the full category name.
function getCanonicalCategoryText(categoryText) {
    if (categoryText == null) {
        return "";
    }
    for (var catId = 1; catId <= CatCount; catId++) {
        if (CatXPriority[catId][0] == categoryText ||
            CatXPriority[catId][4] == categoryText) {
            return CatXPriority[catId][0];
        }
    }
    return categoryText;
}
//jrs 2011/04/15 
function lstCats_click() {
    //jrs 2018/08/08 save previously selected dimensions
    var PrevSelCount = 0;
    var PrevSel = [];
    var i = 0;
    var j = 0;
    var doc = document.forms[0];

    for (i = 0; i < doc.lstPacks.length; i++) {
        if (doc.lstPacks[i].selected) {
            PrevSel[PrevSelCount] = doc.lstPacks[i].text;
            PrevSelCount++;
        }
    }

    //filter categories
    doc.lstPacks.length = 0;
    //if All selected, then no filter necessary
    //jrs 2012/03/29 if (doc.lstCategories.selectedIndex == 0)
    var sFilter = doc.cboFilter.selectedIndex.toString();
    if (doc.cboFilter.selectedIndex < 10) sFilter = "0" + sFilter;
    //dsb 2025/07/31 doc.lstCategories[0] == "ALL" && 
    if (doc.lstCategories.length > 0 && doc.lstCategories[0].value.toString() == "ALL" && doc.lstCategories[0].selected) {
        //jrs 2020/06/30 
        FilterListCount = 0;
        for (i = 0; i < listCount; i++) {
            //jrs 2019/03/27
            //Condition to add No Filter or Filter checked
            if (doc.cboFilter.selectedIndex == 0 || CatXList[i][3].indexOf(sFilter) >= 0) {
                var opt = document.createElement("option");
                ////jrs 2026/05/13 opt.text = CatXList[i][0];
                opt.value = CatXList[i][0];
                //jrs 2026/05/13
                // Apply icon based on category properties
                // parameters
                if (CatXList[i][3].includes('06')) {
                    //opt.style.color = "black";
                    opt.text = "🟣 " + CatXList[i][0];
                } // bilateral
                else if (DimXList[i][1] == 1) {
                    //opt.style.color = "black";
                    opt.text = "🟰 " + CatXList[i][0];
                } // strong variables
                else if (CatXList[i][3].includes('01')) {
                    //opt.style.color = "black";
                    opt.text = "🟩 " + CatXList[i][0];
                } // the rest
                else {
                    //opt.style.color = "red";
                    opt.text = "🛑 " + CatXList[i][0];
                }

                try {
                    doc.lstPacks.add(opt, null); // standards compliant; doesn't work in IE
                }
                catch (ex) {
                    doc.lstPacks.add(opt); // IE only
                }
                //jrs 2020/06/30 if using filter we also need a map even in Cat ALL
                if (doc.cboFilter.selectedIndex > 0) {
                    MapToList[FilterListCount] = i;
                    FilterListCount++;
                }
            }
        }
    }
    else if (doc.lstCategories.length > 0 && doc.lstCategories[0].value.toString() != "ALL" && doc.lstCategories[0].selected) { //dsb 2025/08/12 when first lstCategory is selected like "ALL - Agriculture"
        FilterListCount = 0;
        //var topCatSelectedIndex = doc.lstTopCategories.selectedIndex;
        //dsb 2025/08/18 *** start *** identify the topCategories selected
        var topCatSelected = new Set();
        for (var topcatid = 0; topcatid < doc.lstTopCategories.length; topcatid++) {
            if (doc.lstTopCategories[topcatid].selected) {
                topCatSelected.add(topcatid.toString());
            }
        }//dsb 2025/08/18 *** end ***
        for (i = 0; i < listCount; i++) {
            var j;
            var catrows = new Array();
            catrows = CatXList[i][2].split(",");
            for (var k = 0; k < catrows.length; k++) {
                var defined = 1;//dsb 2025/08/22
                topCatIndex = CatXPriority[catrows[k]][3]; //Other 14
                if (doc.cboFilter.selectedIndex > 0) {//dsb 2025/08/22 Checks if the category is displayed for different filters
                    topCatIndex = MapToTopCat[topCatIndex];
                    if (topCatIndex == undefined || topCatIndex == null)
                        defined = 0; //dsb 2025/08/21
                    else 
                        defined = 1;
                }
                if (defined && topCatSelected.has(topCatIndex.toString())) {//dsb 2025/08/18 Display categories from multiple topCategories selected
                    if (doc.cboFilter.selectedIndex == 0 || CatXList[i][3].indexOf(sFilter) >= 0) {
                        MapToList[FilterListCount] = i;
                        FilterListCount++;

                        var opt = document.createElement("option");
                        //jrs 2026/05/13 opt.text = CatXList[i][0];
                        opt.value = CatXList[i][0];
                        //jrs 2026/05/13
                        // Apply icon based on category properties
                        // parameters
                        if (CatXList[i][3].includes('06')) {
                            //opt.style.color = "black";
                            opt.text = "🟣 " + CatXList[i][0];
                        } // bilateral
                        else if (DimXList[i][1] == 1) {
                            //opt.style.color = "black";
                            opt.text = "🟰 " + CatXList[i][0];
                        } // strong variables
                        else if (CatXList[i][3].includes('01')) {
                            //opt.style.color = "black";
                            opt.text = "🟩 " + CatXList[i][0];
                        } // the rest
                        else {
                            //opt.style.color = "red";
                            opt.text = "🛑 " + CatXList[i][0];
                        }

                        try {
                            doc.lstPacks.add(opt, null); // standards compliant; doesn't work in IE
                        }
                        catch (ex) {
                            doc.lstPacks.add(opt); // IE only
                        }
                    }
                }
            }
        }
    }
    else //Category Selected
    {
        //dsb 2025/08/14 identify the topCategories selected
        var topCatSelected = new Set();
        for (var topcatid = 0; topcatid < doc.lstTopCategories.length; topcatid++) {
            if (doc.lstTopCategories[topcatid].selected) {
                topCatSelected.add(topcatid.toString());
            }
        }
        FilterListCount = 0;
        for (i = 0; i < listCount; i++) {
            //jrs 2012/03/29 if (doc.lstCategories[doc.lstCategories.selectedIndex].text == CatXList[i][1])
            //jrs 2012/12/06 var j = CatXList[i][2];
            //now each list can belong to several categories
            var j;
            var catrows = new Array();
            catrows = CatXList[i][2].split(",");
            for (var k = 0; k < catrows.length; k++) {            
                j = catrows[k];
                //jrs 2019/03/28 need to Map from original CatId to new using Map       
                if (doc.lstTopCategories.selectedIndex == 0 && doc.cboFilter.selectedIndex > 0) { //dsb 2025/08/06 added doc.lstTopCategories.selectedIndex == 0, maps only categories when filtered is selected and topcategories = "ALL" 
                    j = MapToCat[j];
                }
                //dsb 2025/08/06 To map j value
                if (doc.lstTopCategories.selectedIndex > 0) {
                    var flagNotFound = 1;
                    for (var kk = 0; kk < doc.lstCategories.length; kk++) {
                        //dsb 2025/08/13 Added condition CatXPriority[j][3] == doc.lstTopCategories.selectedIndex and changed CatXPriority[j][0] to CatXPriority[j][4]
                        //dsb 2025/08/18 added the or condition (topCatSelected.size > 1 && CatXPriority[j][0] == doc.lstCategories[kk].text)
                        var defined = 1; //dsb 2025/08/22 Checks if the category is displayed for different filters
                        var topCatID = CatXPriority[j][3];
                        if (doc.cboFilter.selectedIndex > 0) {
                            topCatID = MapToTopCat[topCatID];
                            if (topCatID == undefined || topCatID == null)
                                defined = 0;
                            else defined = 1;
                        }
                        if (((topCatSelected.size > 1 && CatXPriority[j][0] == doc.lstCategories[kk].text) || (topCatSelected.size == 1 && CatXPriority[j][4] == doc.lstCategories[kk].text)) && doc.lstCategories[kk].selected && defined && topCatSelected.has(topCatID.toString())) {
                            j = kk;
                            flagNotFound = 0;
                            kk = doc.lstCategories.length;
                        }
                    }
                    if (flagNotFound) {
                        j = -1;
                    }
                }
                //jrs 2020/07/02 added j>=0
                if (j >= 0 && j < doc.lstCategories.length) {
                    if (doc.lstCategories[j].selected) {
                        k = catrows.length;
                    }
                }
            }
            //console.log("j after for loop: " + j);
            
            //jrs 2020/07/02 added j>=0
            if (j >= 0 && j < doc.lstCategories.length && doc.lstCategories[j].selected) {
                //jrs 2019/03/27
                //Condition to add No Filter or Filter checked
                if (doc.cboFilter.selectedIndex == 0 || CatXList[i][3].indexOf(sFilter) >= 0) {
                    MapToList[FilterListCount] = i;
                    FilterListCount++;

                    var opt = document.createElement("option");
                    //jrs 2026/05/13 opt.text = CatXList[i][0];
                    opt.value = CatXList[i][0];
                    //jrs 2026/05/13
                    // Apply icon based on category properties
                    // parameters
                    if (CatXList[i][3].includes('06')) {
                        //opt.style.color = "black";
                        opt.text = "🟣 " + CatXList[i][0];
                    } // bilateral
                    else if (DimXList[i][1] == 1) {
                        //opt.style.color = "black";
                        opt.text = "🟰 " + CatXList[i][0];
                    } // strong variables
                    else if (CatXList[i][3].includes('01')) {
                        //opt.style.color = "black";
                        opt.text = "🟩 " + CatXList[i][0];
                    } // the rest
                    else {
                        //opt.style.color = "red";
                        opt.text = "🛑 " + CatXList[i][0];
                    }

                    try {
                        doc.lstPacks.add(opt, null); // standards compliant; doesn't work in IE
                    }
                    catch (ex) {
                        doc.lstPacks.add(opt); // IE only
                    }
                    j = doc.lstCategories.length;
                }
            }
        }
    }

    //jrs 2018/08/08 Restore previously selected lists
    if (PrevSelCount > 0) {
        var SelCount = 0;
        j = 0;
        var bDone = false;
        for (i = 0; i < doc.lstPacks.length; i++) {
            //jrs 2025/08/28 need to consider that sorting might be different depending of casing
            //HDI < dependence
            //while (PrevSel[j] < doc.lstPacks[i].text && !bDone) {
            //jrs 2026/05/13 using .value property instead of .text to compare since .text might have icons now
            while ((PrevSel[j] != undefined || PrevSel[j] != null) && PrevSel[j].toUpperCase() < doc.lstPacks[i].value.toUpperCase() && !bDone) {
                j++;
                if (j >= PrevSelCount) {bDone = true;}
            }

            //jrs 2026/05/13 using .value property instead of .text to compare since .text might have icons now
            if (PrevSel[j] == doc.lstPacks[i].value && !bDone) {
                doc.lstPacks[i].selected = true;
                j++;
                SelCount++;
                if (j >= PrevSelCount) { i = doc.lstPacks.length; }
            }

            if (bDone) { i = doc.lstPacks.length; }
        }
        if (SelCount == 0) {
            doc.lstPacks.selectedIndex = 0;
        }
    } else {
        doc.lstPacks.selectedIndex = 0;
    }
}

//dsb 2025/07/30
function lstTopCategories_click() {
    lstTopCats_click();
    lstCategories_click();
}

//CDV 2021/08/23
function lstCategories_click() {
    lstCats_click();
    lstPacks_click();
}

//2011/05/03                    
//all menu events need to be either in the client or in the server
//can't have some in client and some in server
function optGroup_Click() {
    var bSkip = bUseGroupDec;
    chkHideNonCoreGroups.disabled = false;
    bUseReg = false;
    bUseGroup = true;
    bUseGroupDec = false;
    bUseGList = false;
    //jrs 2016/09/30
    bUseCtryAndGrp = false;
    //jrs 2025/05/30
    bUseCtryAndGrpDec = false;

    //                        document.all['cswmIcomnuGroups_0'].src = "blank.gif";
    //                        document.all['cswmIcomnuGroups_1'].src = "checkbox.gif";
    //                        document.all['cswmIcomnuGroups_2'].src = "blank.gif";
    //                        document.all['cswmIcomnuGroups_3'].src = "blank.gif";
    //dsb 2025/11/4
    //try { document.forms[0].hdn_bUseGroup.value = "1"; } catch (e) { } 
    persistGeoMode(1);

    if (bSkip == false) {
        currDim1 = 0;
        //jrs 2025/02/11 needed for bilateral to refresh
        currDim2 = 0;
        lstPacks_click();
    }
    
}

function optGroupDec_Click() {
    var bSkip = bUseGroup;
    chkHideNonCoreGroups.disabled = false; //CDV 2021/08/23
    bUseReg = false;
    bUseGroup = false;
    bUseGroupDec = true;
    bUseGList = false;
    //jrs 2016/09/30
    bUseCtryAndGrp = false;
    //jrs 2025/05/30
    bUseCtryAndGrpDec = false;

    //try { document.forms[0].hdn_bUseGroup.value = "0"; } catch (e) { } //dsb 2025/11/4 
    persistGeoMode(2);

    if (bSkip == false) {
        currDim1 = 0;
        //jrs 2025/02/11 needed for bilateral to refresh
        currDim2 = 0;

        lstPacks_click();
    }
}

function optGList_Click() {
    chkHideNonCoreGroups.disabled = true; //CDV 2021/08/23
    bUseReg = false;
    bUseGroup = false;
    bUseGroupDec = false;
    bUseGList = true;
    //jrs 2016/09/30
    bUseCtryAndGrp = false;
    //jrs 2025/05/30
    bUseCtryAndGrpDec = false;

    //try { document.forms[0].hdn_bUseGroup.value = "0"; } catch (e) { } //dsb 2025/11/4
    persistGeoMode(3);

    currDim1 = 0;
    //jrs 2025/06/12 needed for bilateral to refresh
    currDim2 = 0;
    lstPacks_click();
    
}

function optReg_Click() {
    chkHideNonCoreGroups.disabled = true; //CDV 2021/08/23
    bUseReg = true;
    bUseGroup = false;
    bUseGroupDec = false;
    bUseGList = false;
    //jrs 2016/09/30
    bUseCtryAndGrp = false;
    //jrs 2025/05/30
    bUseCtryAndGrpDec = false;

    //try { document.forms[0].hdn_bUseGroup.value = "0"; } catch (e) { } //dsb 2025/11/4
    persistGeoMode(0);

    currDim1 = 0;
    //jrs 2025/02/11 needed for bilateral to refresh
    currDim2 = 0;

    lstPacks_click();
    
}

//jrs 2016/09/30
function optCtryAndGrp_Click() {
    chkHideNonCoreGroups.disabled = false; //CDV 2021/08/23
    bUseReg = false;
    bUseGroup = false;
    bUseGroupDec = false;
    bUseGList = false;
    bUseCtryAndGrp = true;
    //jrs 2025/05/30
    bUseCtryAndGrpDec = false;

    //try { document.forms[0].hdn_bUseGroup.value = "0"; } catch (e) { } //dsb 2025/11/4
    persistGeoMode(4);

    currDim1 = 0;
    //jrs 2025/02/11 needed for bilateral to refresh
    currDim2 = 0;

    lstPacks_click();
}

//jrs 2025/05/30
function optCtryAndGrpDec_Click() {
    chkHideNonCoreGroups.disabled = false; //CDV 2021/08/23
    bUseReg = false;
    bUseGroup = false;
    bUseGroupDec = false;
    bUseGList = false;
    bUseCtryAndGrp = false;
    bUseCtryAndGrpDec = true;

    //try { document.forms[0].hdn_bUseGroup.value = "0"; } catch (e) { } //dsb 2025/11/4
    persistGeoMode(5);

    currDim1 = 0;
    //jrs 2025/02/11 needed for bilateral to refresh
    currDim2 = 0;

    lstPacks_click();
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
    var h = myHeight * f2 - (20 + 100) - (106 + 24 + 20) * f;
    if (h < 106 + 20) {
        h = 106 + 20;
    }
    //dsb 2025/07/29
    var hc = h * 0.5;
    var htc = h - hc - 40;
    doc.lstPacks.style.height = h + 'px';
    //dsb 2025/07/29
    //doc.lstCategories.style.height = h + 'px';
    doc.lstCategories.style.height = hc + 'px';
    doc.lstTopCategories.style.height = htc + 'px';
    doc.lstDim.style.height = h + 'px';
    doc.lstDim2.style.height = h + 'px';
    doc.lstDim3.style.height = h + 'px';
    var T = (128 + h) * f;
    document.getElementById('lblFiles').style.top = T + 'px';
    var tt = T + 20;
    doc.lstFiles.style.top = tt + 'px';
    //jrs 2026/05/22
    document.getElementById('lblEmojiList').style.top = tt + 'px';
    //jrs 2025/06/19
    if (document.getElementById('imgLogoNEPAD') !== null) {
        doc.imgLogoNEPAD.style.top = tt + 'px';
        doc.imgRwandaFlag.style.top = tt + 'px';
    }

    //jrs 2012/02/27
    document.getElementById('lblRunHorizon').style.top = T + 'px';
    doc.lstRunHorizon.style.top = tt + 'px';
    var t2 = (T + 106 + 20) * f;
    doc.cmdAnnotation.style.top = t2 + 'px';
    doc.cmdExport.style.top = t2 + 'px';
    doc.cmdLine.style.top = t2 + 'px';
    doc.cmdTable.style.top = t2 + 'px';
    doc.cmdMap.style.top = t2 + 'px';
    doc.cboFilter.style.top = t2 + 'px';
    //dsb 2025/07/29
    var tlc = 128 + htc + 20;
    document.getElementById('lblCategories').style.top = tlc + 'px';
    tlc = tlc + 20;
    doc.lstCategories.style.top = tlc + 'px';

    ChangeListWidth();
}

function ChangeListWidth() {
    //Change width of lists
    var doc = document.forms[0];
    var w3 = 0;
    if (doc.lstDim2.style.visibility == "visible") {
        w3 = 168;
    }
    var w4 = 0;
    if (doc.lstDim3.style.visibility == "visible") {
        w4 = 168;
    }

    var f3 = 0.9;
    var SpaceBtwn = 16;
    var Disp = myWidth - 9 - 11;
    var NumbLst = 4;
    if (doc.lstDim2.style.visibility == "visible" && doc.lstDim3.style.visibility == "visible") {
        NumbLst = 6;
    }
    else if (doc.lstDim2.style.visibility == "visible" || doc.lstDim3.style.visibility == "visible") {
        NumbLst = 5;
    }

    //dsb 2025/09/16
    document.getElementById('frmGroupOpt').style.width = Disp + 'px';

    var DispInd = Disp / NumbLst;
    var dd = DispInd * 2;
    doc.lstPacks.style.width = dd + 'px';
    //jrs 2026/05/22 doc.lstFiles.style.width = dd + 'px';
    doc.lstFiles.style.width = DispInd + 'px';
    if (dd < 281) {
        document.getElementById('lblInstructions').style.width = Disp + 'px';
    }
    else {
        //original value
        document.getElementById('lblInstructions').style.width = '500px';
    }
    //jrs 2026/05/21 
    var searchWidth = dd*0.9;
    doc.txtPackSearch.style.width = searchWidth + 'px';

    dd = DispInd - SpaceBtwn;
    doc.lstCategories.style.width = dd + 'px';
    doc.lstRunHorizon.style.width = dd + 'px';
    //dsb 2025/07/29
    doc.lstTopCategories.style.width = dd + 'px';

    var ll = 9 + dd + SpaceBtwn;
    //doc.lstCategories.style.left = ll + 'px';
    doc.lstPacks.style.left = ll + 'px';
    //document.getElementById('lblCategories').style.left = ll + 'px';
    document.getElementById('lblInstructions').style.left = ll + 'px';
    //jrs 2026/05/21
    var l2 = ll + searchWidth/0.9*0.1;   
    doc.txtPackSearch.style.left = l2 + 'px';

    //doc.lstRunHorizon.style.left = ll + 'px';
    doc.lstFiles.style.left = ll + 'px';
    //document.getElementById('lblRunHorizon').style.left = ll + 'px';
    document.getElementById('lblFiles').style.left = ll + 'px';
    //jrs 2026/05/22
    l2 = ll + DispInd*1.1;
    document.getElementById('lblEmojiList').style.left = l2 + 'px';

    var dd2 = ll + DispInd * 2 + SpaceBtwn;
    doc.lstDim.style.left = dd2 + 'px';
    doc.lstDim.style.width = dd + 'px';
    document.getElementById('lblDim').style.left = dd2 + 'px';
    //jrs 2025/06/19
    if (document.getElementById('imgLogoNEPAD') !== null) {
        document.getElementById('imgLogoNEPAD').style.left = dd2 + 'px';
        var ddflag = dd2 + 228;
        document.getElementById('imgRwandaFlag').style.left = ddflag + 'px';
    }

    //jrs 2022/05/03
    var dd2chk = dd2 + 6 * SpaceBtwn;
    document.getElementById('chkAllDim1s').style.left = dd2chk + 'px';

    //jrs 2025/06/12
    var dd2mirr = dd2chk + 6 * SpaceBtwn;
    document.getElementById('cmdMirror').style.left = dd2mirr + 'px';

    var dd3 = dd2 + dd + SpaceBtwn;
    //jrs 2026/05/22
    if (doc.lstDim2.style.visibility == "visible") {
        doc.lstDim2.style.left = dd3 + 'px';
        document.getElementById('lblDim2').style.left = dd3 + 'px';
        doc.lstDim2.style.width = dd + 'px';
    }

    if (doc.lstDim3.style.visibility == "visible") {
        var dd4 = dd3 + dd + SpaceBtwn;
        if (doc.lstDim2.style.visibility == "visible") {
            doc.lstDim3.style.left = dd4 + 'px';
            document.getElementById('lblDim3').style.left = dd4 + 'px';
        }
        else {
            doc.lstDim3.style.left = dd3 + 'px';
            document.getElementById('lblDim3').style.left = dd3 + 'px';
        }
        doc.lstDim3.style.width = dd + 'px';
    }
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

//jrs 2019/03/27
function cboFilter_Click() {
    var doc = document.forms[0];
    var PrevCatIdx = doc.lstCategories.selectedIndex;
    var PrevCat = doc.lstCategories[PrevCatIdx].value;
    //dsb 2025/11/4 Preserve currently selected top categories
    var selTopCats = [];
    if (doc.lstTopCategories && doc.lstTopCategories.length) {
        for (var iTopCat = 0; iTopCat < doc.lstTopCategories.length; iTopCat++) {
            if (doc.lstTopCategories[iTopCat].selected) {
                selTopCats.push({
                    id: iTopCat,
                    text: doc.lstTopCategories[iTopCat].text
                })
            }
        }
    }
    //dsb 2025/11/4 Preserve currently selected meso categories
    var selCats = [];
    if (doc.lstCategories && doc.lstCategories.length) {
        for (var icat = 0; icat < doc.lstCategories.length; icat++) {
            if (doc.lstCategories[icat].selected) {
                selCats.push({
                    id: icat,
                    text: doc.lstCategories[icat].text
                })
            }
        }
    }
    var NewIdx = PrevCatIdx;
    //dsb 2025/08/29 Displays all TopCategories when filter is changed from any of 1 to 5 to 0
    var PrevTopCatIdx = doc.lstTopCategories.selectedIndex;
    var PrevTopCat = doc.lstTopCategories[PrevTopCatIdx].value;
    var NewTopIdx = PrevTopCatIdx;

    doc.lstTopCategories.length = 0;
    //ALL is always included
    var opt = document.createElement("option");
    opt.text = "ALL";
    opt.value = "ALL";
    try {
        doc.lstTopCategories.add(opt, null); // standards compliant; doesn't work in IE
    }
    catch (ex) {
        doc.lstTopCategories.add(opt); // IE only
    }
    //dsb 2025/09/02
    //Reload Categories
    doc.lstCategories.length = 0;
    //ALL is always included
    var opt = document.createElement("option");
    opt.text = "ALL";
    opt.value = "ALL";
    try {
        doc.lstCategories.add(opt, null); // standards compliant; doesn't work in IE
    }
    catch (ex) {
        doc.lstCategories.add(opt); // IE only
    }

    //ReLoad Top Categories
    if (doc.cboFilter.selectedIndex == 0) {
        var topCatCol = new Map();

        for (i = 1; i <= CatCount; i++) {
            if (!topCatCol.has(CatXPriority[i][3].toString())) {
                topCatCol.set(CatXPriority[i][3].toString(), CatXPriority[i][2]);
            }
        }

        let sortedTopCat = [...topCatCol.entries()].sort((a, b) => Number(a[0]) - Number(b[0]));
        MapToTopCat.length = 0;
        j = 0;
        for (let [topCatKey, topCatName] of sortedTopCat) {
            var opt = document.createElement("option");
            opt.text = topCatName;
            opt.value = topCatName;
            if (opt.text == PrevTopCat) {
                NewTopIdx = j + 1;
            }
            try {
                doc.lstTopCategories.add(opt, null);
            } catch (ex) {
                doc.lstTopCategories.add(opt);
            }
            j = j + 1;
            MapToTopCat[topCatKey] = j;
        }
        doc.lstTopCategories.selectedIndex = NewTopIdx;
        //dsb 2025/11/7 when multiple top categories are selected
        if (selTopCats.length > 0 && doc.lstTopCategories && doc.lstTopCategories.length) {
            for (var itopcat = 0; itopcat < doc.lstTopCategories.length; itopcat++) {
                doc.lstTopCategories[itopcat].selected = false;
            }
            var restoredTopCount = 0;
            for (var itopcat = 0; itopcat < doc.lstTopCategories.length; itopcat++) {
                for (var s = 0; s < selTopCats.length; s++) {
                    if (doc.lstTopCategories[itopcat].text == selTopCats[s].text) {
                        doc.lstTopCategories[itopcat].selected = true;
                        restoredTopCount++;
                        break;
                    }
                }
            }
            if (restoredTopCount == 0) {
                doc.lstTopCategories.selectedIndex = 0;
            }
        }

        j = 0;//dsb 2025/08/20
        for (i = 1; i <= CatCount; i++) {
            //if (doc.lstTopCategories.selectedIndex == 0 || doc.lstTopCategories.selectedIndex == CatXPriority[i][3]) {//dsb 2025/08/20
            if (doc.lstTopCategories.selectedIndex == 0 || doc.lstTopCategories[CatXPriority[i][3]].selected) { //dsb 2025/11/7
                var opt = document.createElement("option");
                //if (doc.lstTopCategories.selectedIndex == 0) {
                if (doc.lstTopCategories.selectedIndex == 0 || selTopCats.length > 1) {//dsb 2025/11/7
                    opt.text = CatXPriority[i][0];
                    opt.value = CatXPriority[i][0];
         
                } else {
                    //if (doc.lstTopCategories.selectedIndex == CatXPriority[i][3]) {
                    if (doc.lstTopCategories[CatXPriority[i][3]].selected) {//dsb 2025/11/7
                        opt.text = CatXPriority[i][4];
                        opt.value = CatXPriority[i][4];
                    }
                }
                if (opt.text == PrevCat) {
                    NewIdx = j + 1;//dsb 2025/08/20
                }
                try {
                    doc.lstCategories.add(opt, null); // standards compliant; doesn't work in IE
                }
                catch (ex) {
                    doc.lstCategories.add(opt); // IE only
                }
                j = j + 1;
                MapToCat[i] = j;
            }
        }
        FilterCatCount = CatCount;
    }
    else {
        var sFilter = doc.cboFilter.selectedIndex.toString();
        if (doc.cboFilter.selectedIndex < 10) sFilter = "0" + sFilter;
        var newTopCatsSelected = new Set();//dsb 2025/11/7
        //dsb 2025/08/27 maintains alphabetical order of the topCategories when filter is selected ***start***
        var topCatCol = new Map();
        for (i = 1; i <= CatCount; i++) {
            if (CatXPriority[i][1].indexOf(sFilter) >= 0) {
                if (!topCatCol.has(CatXPriority[i][3].toString())) {
                    topCatCol.set(CatXPriority[i][3].toString(), CatXPriority[i][2]);
                }
            }
        }

        let sortedTopCat = [...topCatCol.entries()].sort((a, b) => Number(a[0]) - Number(b[0]));
        MapToTopCat.length = 0;
        j = 0;
        for (let [topCatKey, topCatName] of sortedTopCat) {
            var opt = document.createElement("option");
            opt.text = topCatName;
            opt.value = topCatName;
            if (opt.text == PrevTopCat) {
                NewTopIdx = j + 1;
            }
            try {
                doc.lstTopCategories.add(opt, null);
            } catch (ex) {
                doc.lstTopCategories.add(opt);
            }
            j = j + 1;
            MapToTopCat[topCatKey] = j;
        }

        doc.lstTopCategories.selectedIndex = NewTopIdx;
        //dsb 2025/11/7 when multiple top categories are selected
        if (selTopCats.length > 0 && doc.lstTopCategories && doc.lstTopCategories.length) {
            for (var itopcat = 0; itopcat < doc.lstTopCategories.length; itopcat++) {
                doc.lstTopCategories[itopcat].selected = false;
            }
            var restoredTopCount = 0;
            for (var itopcat = 0; itopcat < doc.lstTopCategories.length; itopcat++) {
                for (var s = 0; s < selTopCats.length; s++) {
                    if (doc.lstTopCategories[itopcat].text == selTopCats[s].text) {
                        doc.lstTopCategories[itopcat].selected = true;
                        newTopCatsSelected.add(itopcat);
                        restoredTopCount++;
                        break;
                    }
                }
            }
            if (restoredTopCount == 0) {
                doc.lstTopCategories.selectedIndex = 0;
            }
        }
        //dsb 2025/09/02
        if (doc.lstTopCategories.selectedIndex == -1) {
            doc.lstTopCategories.selectedIndex = 0;
        }

        j = 0;
        for (i = 1; i <= CatCount; i++) {
            //if (CatXPriority[i][1].indexOf(sFilter) >= 0 && (doc.lstTopCategories.selectedIndex == 0 || doc.lstTopCategories.selectedIndex == CatXPriority[i][3])) {
            if (CatXPriority[i][1].indexOf(sFilter) >= 0 && (doc.lstTopCategories.selectedIndex == 0 || newTopCatsSelected.has(MapToTopCat[CatXPriority[i][3]]))) { //dsb 2025/11/7
                var opt = document.createElement("option");
                //opt.text = CatXPriority[i][0];
                //opt.value = CatXPriority[i][0];
                //if (doc.lstTopCategories.selectedIndex == 0) {
                if (doc.lstTopCategories.selectedIndex == 0 || selTopCats.length > 1) { //dsb 2025/11/7
                    opt.text = CatXPriority[i][0];
                    opt.value = CatXPriority[i][0];

                } else {
                    //if (doc.lstTopCategories.selectedIndex == CatXPriority[i][3]) {
                    if (newTopCatsSelected.has(MapToTopCat[CatXPriority[i][3]])) { //dsb 2025/11/7
                        opt.text = CatXPriority[i][4];
                        opt.value = CatXPriority[i][4];
                    }
                }
                if (opt.text == PrevCat) {
                    NewIdx = j+1;
                }
                try {
                    doc.lstCategories.add(opt, null); // standards compliant; doesn't work in IE
                }
                catch (ex) {
                    doc.lstCategories.add(opt); // IE only
                }
                j = j + 1;
                MapToCat[i] = j;
            }
        }
        FilterCatCount = j;
    }//dsb 2025/08/27 ***end***

    doc.lstCategories.selectedIndex = NewIdx;
    //dsb 2025/11/4 Restore previously selected meso categories
    if (selCats.length > 0 && doc.lstCategories && doc.lstCategories.length) {
        for (var icat = 0; icat < doc.lstCategories.length; icat++) {
            doc.lstCategories[icat].selected = false;
        }

        var restoredCount = 0;
        for (var icat = 0; icat < doc.lstCategories.length; icat++) {
            for (var s = 0; s < selCats.length; s++) {
                if (doc.lstCategories[icat].text == selCats[s].text) {
                    doc.lstCategories[icat].selected = true;
                    restoredCount++;
                    break;
                }
            }
        }
        if (restoredCount == 0) {
            doc.lstCategories.selectedIndex = 0;
        }
    }
    //doc.lstCategories[NewIdx].s

    //ReLoad Variables
    //jrs 2020/06/30 lstCats_click();
    lstTopCategories_click();
}
//CDV 2021/08/23
function chkHideNonCoreGroups_Click(){
    currDim1 = 0;
    //dsb 2026/03/19 needed for bilateral to refresh
    currDim2 = 0;
        lstPacks_click();
}

