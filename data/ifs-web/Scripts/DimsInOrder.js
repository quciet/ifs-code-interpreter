// JScript File
//Created by José R Solórzano on December 5th, 2012, based on FilesInOrder

                    //jrs 2011/11/03
                    var OrdDims = new Array();                       
                    var OrdDimsCount = 0;
                    var clickDownY=0;
                    var clickUpY = 0;
                    var clickWshift = false;

                    //jrs 2023/09/20
                    var OrdDims2 = new Array();
                    var OrdDimsCount2 = 0;
                    var clickDownY2 = 0;
                    var clickUpY2 = 0;
                    var clickWshift2 = false;


                    function lstDim_ClickDown(e) {
                        clickDownY = e.clientY;
                    }

                    //jrs 2023/09/20
                    function lstDim2_ClickDown(e) {
                        clickDownY2 = e.clientY;
                    }

                    function lstDim_ClickUp(e) {
                        clickUpY = e.clientY;
                        clickWshift = e.shiftKey;
                    }

                    //jrs 2023/09/20
                    function lstDim2_ClickUp(e) {
                        clickUpY2 = e.clientY;
                        clickWshift2 = e.shiftKey;
                    }

                    //jrs 2012/12/05
                    function lstDim_Change(e) 
                    {
                        var isChrome = !!(window.chrome && chrome.webstore && chrome.webstore.install);
                        if (isChrome) {
                            lstDim_Click(e);
                        }
                    }

                    //jrs 2023/09/20
                    function lstDim2_Change(e) {
                        var isChrome = !!(window.chrome && chrome.webstore && chrome.webstore.install);
                        if (isChrome) {
                            lstDim2_Click(e);
                        }
                    }

                    function lstDim_Click(e)
                    {
                        //'jrs 2011/01/13
                        //'Keeping Track of the order in which Dims were selected
                        var doc = document.forms[0];
                        var lst = doc.lstDim;
                        var DimselCount = selectCount(lst);
                        var lstClick=-1;
                        
                        if (DimselCount == 1 && lst[lst.selectedIndex].selected)
                        {
                            OrdDims = new Array();                       
                            OrdDimsCount = 0;
                        }
                     
                      try {
                            if (DimselCount - OrdDimsCount == 1)
                            {
                                //find last selected, lst.selectedIndex returns the first one alphabetically, not the last one selected
                                lstClick=findLastAddClick(OrdDims, lst);
                            
                                OrdDims["x" + lstClick] = lstClick;
                                OrdDimsCount++;
                            }        
                            else if (OrdDimsCount - DimselCount == 1)
                            {
                                var idx = findUnselectedItem(OrdDims, lst);
                                delete OrdDims["x" + idx];
                                OrdDimsCount--;
                                lstClick=idx;
                            }
                        }
                        catch(ex) {
                            // reset the array
                            OrdDimsCount = 0;
                        }
                        
                        try{
                                //Check if using shift down
                                var x;
                                var bRedo;
                                bRedo = false;
                                if (DimselCount != OrdDimsCount) 
                                {
                                    var i = 0;
                                    var bAsc;
                                    bAsc = false;
                                    var lstIdx=findLastIndex(OrdDims);
                                    if (lstIdx == "")
                                    {
                                        if (clickDownY <= clickUpY) //ascending
                                        {
                                            lstIdx = lst.selectedIndex;
                                        }
                                        else
                                        {
                                            lstIdx = "x" + lst.selectedIndex;
                                            lstIdx= findLastAddShiftClick(OrdDims, lst, lstIdx);
                                        }

                                        OrdDims["x" + lstIdx] = lstIdx;
                                        lstIdx = "x" + lstIdx;
                                        OrdDimsCount++;
                                    }
                                    else
                                    {
                                        //Check for drag
                                        while (OrdDimsCount>0 && !lst[OrdDims[lstIdx]].selected)
                                        {
                                            delete OrdDims[lstIdx];
                                            OrdDimsCount--;                                        
                                            if (OrdDimsCount == 0)
                                            {
                                                lstIdx = lst.selectedIndex;
                                            }
                                            else
                                            {
                                                lstIdx=findLastIndex(OrdDims);
                                            }
                                        }
                                    }
                                    //Adding
                                    if (DimselCount > OrdDimsCount)
                                    {
                                        //jrs 2015/09/17 not working on chorme if (e.shiftKey)
                                        if (clickWshift)
                                        {
                                            lstClick=findLastAddShiftClick(OrdDims, lst, lstIdx);
                                            //From Lower to Higher
                                            if (lstClick > OrdDims[lstIdx])
                                            {
                                                for (i = OrdDims[lstIdx] + 1; i <= lstClick; i++)
                                                {
                                                    OrdDims["x" + i] = i;
                                                    OrdDimsCount++;
                                                }
                                            }                                       
                                            else //from higher to lower
                                            {
                                                for (i = OrdDims[lstIdx] - 1; i >= lst.selectedIndex; i--)
                                                {
                                                    OrdDims["x" + i] = i;
                                                    OrdDimsCount++;
                                                }
                                            }
                                        }
                                        else // in case of drag
                                        {
                                            if (clickDownY < clickUpY) //ascending
                                            {
                                                for (i=0; i<lst.length; i++)
                                                {
                                                    if (lst[i].selected && !("x"+i in OrdDims))
                                                    {
                                                        OrdDims["x"+i] = i;
                                                        OrdDimsCount++;
                                                    }
                                                }
                                            }
                                            else // descending
                                            {
                                                for (i=lst.length-1; i>=0; i--)
                                                {
                                                    if (lst[i].selected && !("x"+i in OrdDims))
                                                    {
                                                        OrdDims["x"+i] = i;
                                                        OrdDimsCount++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else //Removing
                                    {
                                        for (i = 0; i < lst.length; i++)
                                        {
                                            if (!lst[i].selected && ("x"+i in OrdDims))
                                            {
                                                delete OrdDims["x" + i];
                                                OrdDimsCount--;
                                            }
                                        }
                                    }
                                }
                                
                                //check if using drag
                                //if still not all items in
                                if (DimselCount != OrdDimsCount)
                                {
                                    //remove all from collection
                                    OrdDims = new Array();                       
                                    OrdDimsCount = 0;
                                    bRedo = true;
                                }             
                        }
                        catch(ex) {
                            // reset the array
                            OrdDimsCount = 0;
                        }                                    
   
                        //Validate
                        for (var x in OrdDims)
                        {
                            if (!lst[OrdDims[x]].selected) 
                            {
                                OrdDims = new Array();                       
                                OrdDimsCount = 0;
                                bRedo = true;
                                break;
                            }
                        }

                        if (bRedo) 
                        {
                            //jrs 2015/09/17 if (e.shiftKey) this was not working in chrome
                            if (clickWshift)
                            {
                                for (i = lst.length - 1; i >= 0; i--)
                                {
                                    if (lst[i].selected)
                                    {
                                        if (i < lstClick)
                                        {
                                            bAsc = true;
                                            i = 0;
                                        }
                                        else if (i > lstClick)
                                        {
                                            bAsc = false;
                                            i = 0;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                bAsc = (clickDownY < clickUpY);
                            }
                                    
                            if (bAsc) 
                            {
                                for (i = 0; i < lst.length; i++)
                                {
                                    if (lst[i].selected)
                                    {
                                        OrdDims["x" + i] = i;
                                        OrdDimsCount++;
                                    }
                                }
                            }
                            else
                            {
                                for (i = lst.length - 1; i>0; i--)
                                {
                                    if (lst[i].selected)
                                    {
                                        OrdDims["x" + i] = i;
                                        OrdDimsCount++;
                                    }
                                }
                            }//bAsc
                        } // bRedo

                        WriteOrdDims(doc, lst, OrdDims, OrdDimsCount);
                    }

                    //jrs 2023/09/20
                    function lstDim2_Click(e) {
                        //'jrs 2011/01/13
                        //'Keeping Track of the order in which Dims were selected
                        var doc = document.forms[0];
                        var lst = doc.lstDim2;
                        var DimselCount2 = selectCount(lst);
                        var lstClick2 = -1;

                        if (DimselCount2 == 1 && lst[lst.selectedIndex].selected) {
                            OrdDims2 = new Array();
                            OrdDimsCount2 = 0;
                        }

                        try {
                            if (DimselCount2 - OrdDimsCount2 == 1) {
                                //find last selected, lst.selectedIndex returns the first one alphabetically, not the last one selected
                                lstClick2 = findLastAddClick(OrdDims2, lst);

                                OrdDims2["x" + lstClick2] = lstClick2;
                                OrdDimsCount2++;
                            }
                            else if (OrdDimsCount2 - DimselCount2 == 1) {
                                var idx = findUnselectedItem(OrdDims2, lst);
                                delete OrdDims2["x" + idx];
                                OrdDimsCount2--;
                                lstClick2 = idx;
                            }
                        }
                        catch (ex) {
                            // reset the array
                            OrdDimsCount2 = 0;
                        }

                        try {
                            //Check if using shift down
                            var x;
                            var bRedo;
                            bRedo = false;
                            if (DimselCount2 != OrdDimsCount2) {
                                var i = 0;
                                var bAsc;
                                bAsc = false;
                                var lstIdx = findLastIndex(OrdDims2);
                                if (lstIdx == "") {
                                    if (clickDownY2 <= clickUpY2) //ascending
                                    {
                                        lstIdx = lst.selectedIndex;
                                    }
                                    else {
                                        lstIdx = "x" + lst.selectedIndex;
                                        lstIdx = findLastAddShiftClick(OrdDims2, lst, lstIdx);
                                    }

                                    OrdDims2["x" + lstIdx] = lstIdx;
                                    lstIdx = "x" + lstIdx;
                                    OrdDimsCount2++;
                                }
                                else {
                                    //Check for drag
                                    while (OrdDimsCount2 > 0 && !lst[OrdDims2[lstIdx]].selected) {
                                        delete OrdDims2[lstIdx];
                                        OrdDimsCount2--;
                                        if (OrdDimsCount2 == 0) {
                                            lstIdx = lst.selectedIndex;
                                        }
                                        else {
                                            lstIdx = findLastIndex(OrdDims);
                                        }
                                    }
                                }
                                //Adding
                                if (DimselCount2 > OrdDimsCount2) {
                                    //jrs 2015/09/17 not working on chorme if (e.shiftKey)
                                    if (clickWshift2) {
                                        lstClick2 = findLastAddShiftClick(OrdDims2, lst, lstIdx);
                                        //From Lower to Higher
                                        if (lstClick2 > OrdDims2[lstIdx]) {
                                            for (i = OrdDims2[lstIdx] + 1; i <= lstClick2; i++) {
                                                OrdDims2["x" + i] = i;
                                                OrdDimsCount2++;
                                            }
                                        }
                                        else //from higher to lower
                                        {
                                            for (i = OrdDims2[lstIdx] - 1; i >= lst.selectedIndex; i--) {
                                                OrdDims2["x" + i] = i;
                                                OrdDimsCount2++;
                                            }
                                        }
                                    }
                                    else // in case of drag
                                    {
                                        if (clickDownY2 < clickUpY2) //ascending
                                        {
                                            for (i = 0; i < lst.length; i++) {
                                                if (lst[i].selected && !("x" + i in OrdDims2)) {
                                                    OrdDims2["x" + i] = i;
                                                    OrdDimsCount2++;
                                                }
                                            }
                                        }
                                        else // descending
                                        {
                                            for (i = lst.length - 1; i >= 0; i--) {
                                                if (lst[i].selected && !("x" + i in OrdDims2)) {
                                                    OrdDims2["x" + i] = i;
                                                    OrdDimsCount2++;
                                                }
                                            }
                                        }
                                    }
                                }
                                else //Removing
                                {
                                    for (i = 0; i < lst.length; i++) {
                                        if (!lst[i].selected && ("x" + i in OrdDims2)) {
                                            delete OrdDims2["x" + i];
                                            OrdDimsCount2--;
                                        }
                                    }
                                }
                            }

                            //check if using drag
                            //if still not all items in
                            if (DimselCount2 != OrdDimsCount2) {
                                //remove all from collection
                                OrdDims2 = new Array();
                                OrdDimsCount2 = 0;
                                bRedo = true;
                            }
                        }
                        catch (ex) {
                            // reset the array
                            OrdDimsCount2 = 0;
                        }

                        //Validate
                        for (var x in OrdDims2) {
                            if (!lst[OrdDims2[x]].selected) {
                                OrdDims2 = new Array();
                                OrdDimsCount2 = 0;
                                bRedo = true;
                                break;
                            }
                        }

                        if (bRedo) {
                            //jrs 2015/09/17 if (e.shiftKey) this was not working in chrome
                            if (clickWshift2) {
                                for (i = lst.length - 1; i >= 0; i--) {
                                    if (lst[i].selected) {
                                        if (i < lstClick2) {
                                            bAsc = true;
                                            i = 0;
                                        }
                                        else if (i > lstClick2) {
                                            bAsc = false;
                                            i = 0;
                                        }
                                    }
                                }
                            }
                            else {
                                bAsc = (clickDownY2 < clickUpY2);
                            }

                            if (bAsc) {
                                for (i = 0; i < lst.length; i++) {
                                    if (lst[i].selected) {
                                        OrdDims2["x" + i] = i;
                                        OrdDimsCount2++;
                                    }
                                }
                            }
                            else {
                                for (i = lst.length - 1; i > 0; i--) {
                                    if (lst[i].selected) {
                                        OrdDims2["x" + i] = i;
                                        OrdDimsCount2++;
                                    }
                                }
                            }//bAsc
                        } // bRedo

                        WriteOrdDims2(doc, lst, OrdDims2, OrdDimsCount2);
                    }

                    //jrs 2022/05/03
                    function lstDimSelectAll_Click(e) {
                        //'jrs 2011/01/13
                        //'Keeping Track of the order in which Dims were selected
                        var doc = document.forms[0];
                        var lst = doc.lstDim;
                        var lstClick = -1;
                        var chk = doc.chkAllDim1s;

                        OrdDims = new Array();
                        OrdDimsCount = 0;

                        //ORIGINAL CODE
                        //af 2023/05/01 changes by Angel Fernandes
                        /*
                        if (chk.checked)
                            {
                            for (i = 0; i < lst.length; i++) {
                                lst[i].selected = true;
                                OrdDims["x" + i] = i;
                                OrdDimsCount++;
                            }
                        }
                        else {
                            lst[0].selected = true;
                            OrdDims["x0"] = 0;
                            OrdDimsCount++;
                            for (i = 1; i < lst.length; i++) {
                                lst[i].selected = false;
                            }
                        }

                        //checkbox selects world but doesnt deselect
                        /*
                        if (chk.checked) {
                            for (i = 0; i < lst.length; i++) {
                                lst[i].selected = false;
                            }
                            for (i = 0; i < lst.length; i++) {
                                if (lst[i].value === 'World') {
                                    lst[i].selected = true;
                                    OrdDims["x" + i] = i;
                                    OrdDimsCount++;
                                    break;
                                }
                            }
                        } else {
                            lst[0].selected = true;
                            OrdDims["x0"] = 0;
                            OrdDimsCount++;
                        }

                        */

                        //jrs 2025/05/30 added || bUseCtryAndGrpDec == true
                        //anf 22/05/2023
                        //checkbox selects only World AND deselects everything else
                        if (bUseGroup == true || bUseGroupDec == true || bUseCtryAndGrp == true || bUseCtryAndGrpDec == true) {
                            if (chk.checked) {
                                for (i = 0; i < lst.length; i++) {
                                    if (lst[i].value === 'World') {
                                        lst[i].selected = true;
                                        OrdDims["x" + i] = i;
                                        OrdDimsCount++;
                                        //jrs 2025/06/12
                                        lst.selectedIndex = i;
                                    } else {
                                        lst[i].selected = false;
                                    }
                                }
                            }
                        }
                        else if (bUseReg == true) {
                            if (chk.checked) {
                                for (i = 0; i < lst.length; i++) {
                                    lst[i].selected = true;
                                    OrdDims["x" + i] = i;
                                    OrdDimsCount++;
                                }
                            } else {
                                for (i = 0; i < lst.length; i++) {
                                    lst[i].selected = false;
                                    OrdDims["x" + i] = null;
                                }
                                OrdDimsCount = 0;
                            }
                        }
                        else {
                            lst[0].selected = true;
                            OrdDims["x0"] = 0;
                            OrdDimsCount++;
                            for (i = 1; i < lst.length; i++) {
                                lst[i].selected = false;
                            }
                        }

                        WriteOrdDims(doc, lst, OrdDims, OrdDimsCount);
                    }


                    function selectCount(lst)
                    {
                        var count=0;
                        for (i = 0; i < lst.length; i++)
                        {
                           if (lst[i].selected == true) 
                           {
                            count++;
                           }
                        }
                        return count;
                    }
                    
                    function findUnselectedItem(OrdDims, lst)
                    {
                        var idx=-1;
                        for(var key in OrdDims) {
                            idx = OrdDims[key];
                            if (!lst[idx].selected)
                            {
                                return idx;
                            }
                        }
                    }
                    
                    function findLastIndex(OrdDims)
                    {
                        var key="";
                        for(key in OrdDims) {
                        }                    
                        return key;
                    }
                    
                    function findLastAddShiftClick(OrdDims, lst, lstIdx)
                    {
                        var idx=-1;
                        for (var i=0; i<lst.length; i++)
                        {
                            if (lst[i].selected && !("x" + i in OrdDims) )
                            {
                                if  (i < OrdDims[lstIdx])
                                {
                                    return i;
                                }
                                else
                                {
                                    idx = i;
                                }
                            }
                        }
                        
                        return idx;
                    }

   
                    function findLastAddClick(OrdDims, lst)
                    {
                        for (var i=0; i<lst.length; i++)
                        {
                            if (lst[i].selected && !("x" + i in OrdDims) )
                            {
                                return i;
                            }
                        }
                    }

                    function findLastDelClick(OrdDims, lst, lstIdx)
                    {
                        var idx=-1;
                        for (var i=0; i<lst.length; i++)
                        {
                            if (lst[i].selected && ("x" + i in OrdDims) )
                            {
                                if  (i > OrdDims[lstIdx])
                                {
                                    return i;
                                }
                                else
                                {
                                    idx = i;
                                }
                            }
                        }
                        
                        return idx;
                    }

                    function WriteOrdDims(doc, lst, OrdDims, OrdDimsCount)
                    {
                        doc.txtDims.value = "";
                        if (OrdDimsCount > 1)
                        {
                            var idx;
                            for(var key in OrdDims) 
                            {
                                idx = OrdDims[key];
                                if (lst[idx].selected)
                                {
                                    if (doc.txtDims.value > "")
                                    {
                                        doc.txtDims.value = doc.txtDims.value + ","
                                    }
                                    doc.txtDims.value = doc.txtDims.value + idx;
                                }
                            }
                        }
}

                    //jrs 2023/09/20
                    function WriteOrdDims2(doc, lst, OrdDims, OrdDimsCount) {
                        doc.txtDims2.value = "";
                        if (OrdDimsCount > 1) {
                            var idx;
                            for (var key in OrdDims) {
                                idx = OrdDims[key];
                                if (lst[idx].selected) {
                                    if (doc.txtDims2.value > "") {
                                        doc.txtDims2.value = doc.txtDims2.value + ","
                                    }
                                    doc.txtDims2.value = doc.txtDims2.value + idx;
                                }
                            }
                        }
                    }

//jrs 2025/06/12
function cmdMirror_Click() {
    var doc = document.forms[0];
    var lst = doc.lstDim;
    var lst2 = doc.lstDim2;

    OrdDims = new Array();
    OrdDimsCount = 0;

    lst2.selectedIndex = lst.selectedIndex;

    //this assumes lst has the same lenght than lst2
    for (var i = 0; i < lst.length; i++) {
        lst2[i].selected = lst[i].selected;
        if (lst2[i].selected) {
            OrdDims["x" + i] = i;
            OrdDimsCount++;
        }
    }

    WriteOrdDims(doc, lst2, OrdDims, OrdDimsCount);

}
