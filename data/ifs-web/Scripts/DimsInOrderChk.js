// JScript File
//Created by José R Solórzano on December 5th, 2012, based on FilesInOrder

                    //jrs 2011/11/03
                    var OrdDims = new Array();                       
                    var OrdDimsCount = 0;
                    var clickDownY=0;
                    var clickUpY=0;
                    
                    function lstDim_ClickDown(e) {
                        clickDownY = e.clientY;
                    }

                    function lstDim_ClickUp(e) {
                        clickUpY = e.clientY;
                    }

                    //jrs 2012/12/05
                    function lstDim_Change(e) 
                    {
                        //var isChrome = !!(window.chrome && chrome.webstore && chrome.webstore.install);
                        //if (isChrome) {
                            lstDim_Click(e);
                        //}
                    }

                    function lstDim_Click(e)
                    {
                        //'jrs 2011/01/13
                        //'Keeping Track of the order in which Dims were selected
                        var doc = document.forms[0];
                        //jrs 2015/04/09 var lst = doc.lstDim;
                        var lst = document.getElementById("lstDim");
                        var DimselCount = selectCount(lst);
                        var lstClick=-1;
                        
                        var chkBoxCount = lst.getElementsByTagName("input");
                        if (DimselCount == 1 && chkBoxCount[GetFirstSelectedIdx('lstDim')].checked)
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
                                            lstIdx = GetFirstSelectedIdx('lstDim');
                                        }
                                        else
                                        {
                                            lstIdx = "x" + GetFirstSelectedIdx('lstDim');
                                            lstIdx= findLastAddShiftClick(OrdDims, lst, lstIdx);
                                        }

                                        OrdDims["x" + lstIdx] = lstIdx;
                                        lstIdx = "x" + lstIdx;
                                        OrdDimsCount++;
                                    }
                                    else
                                    {
                                        //Check for drag
                                        while (OrdDimsCount > 0 && !chkBoxCount[OrdDims[lstIdx]].checked)
                                        {
                                            delete OrdDims[lstIdx];
                                            OrdDimsCount--;                                        
                                            if (OrdDimsCount == 0)
                                            {
                                                lstIdx = GetFirstSelectedIdx('lstDim');
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
                                        if (e.shiftKey)
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
                                                var selIdx = GetFirstSelectedIdx('lstDim');
                                                for (i = OrdDims[lstIdx] - 1; i >= selIdx; i--)
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
                                                for (i = 0; i < chkBoxCount.length; i++)
                                                {
                                                    if (chkBoxCount[i].checked && !("x" + i in OrdDims))
                                                    {
                                                        OrdDims["x"+i] = i;
                                                        OrdDimsCount++;
                                                    }
                                                }
                                            }
                                            else // descending
                                            {
                                                for (i = chkBoxCount.length - 1; i >= 0; i--)
                                                {
                                                    if (chkBoxCount[i].checked && !("x" + i in OrdDims))
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
                                        for (i = 0; i < chkBoxCount.length; i++)
                                        {
                                            if (!chkBoxCount[i].checked && ("x" + i in OrdDims))
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
                            if (!chkBoxCount[OrdDims[x]].checked)
                            {
                                OrdDims = new Array();                       
                                OrdDimsCount = 0;
                                bRedo = true;
                                break;
                            }
                        }

                        if (bRedo) 
                        {
                            if (e.shiftKey)
                            {
                                for (i = chkBoxCount.length - 1; i >= 0; i--)
                                {
                                    if (chkBoxCount[i].checked)
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
                                for (i = 0; i < chkBoxCount.length; i++)
                                {
                                    if (chkBoxCount[i].checked)
                                    {
                                        OrdDims["x" + i] = i;
                                        OrdDimsCount++;
                                    }
                                }
                            }
                            else
                            {
                                for (i = chkBoxCount.length - 1; i > 0; i--)
                                {
                                    if (chkBoxCount[i].checked)
                                    {
                                        OrdDims["x" + i] = i;
                                        OrdDimsCount++;
                                    }
                                }
                            }//bAsc
                        } // bRedo

                        WriteDims(doc, lst, OrdDims, OrdDimsCount);
                    }
                    
                    function selectCount(lst)
                    {
                        var count=0;
                        var chkBoxCount = lst.getElementsByTagName("input");
                        for (i = 0; i < chkBoxCount.length; i++)
                        {
                            //if (lst.childNodes[0].childNodes[i].childNodes[0].childNodes[0].selected == true)
                            if (chkBoxCount[i].checked)
                           {
                            count++;
                           }
                        }
                        return count;
                    }
                    
                    function findUnselectedItem(OrdDims, lst)
                    {
                        var idx = -1;
                        var chkBoxCount = lst.getElementsByTagName("input");

                        for(var key in OrdDims) {
                            idx = OrdDims[key];

                            if (!chkBoxCount[idx].checked)
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
                        var chkBoxCount = lst.getElementsByTagName("input");
                        for (var i = 0; i < chkBoxCount.length; i++)
                        {
                            if (chkBoxCount[i].checked && !("x" + i in OrdDims))
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
                        var chkBoxCount = lst.getElementsByTagName("input");
                        for (var i = 0; i < chkBoxCount.length; i++)
                        {
                            if (chkBoxCount[i].checked && !("x" + i in OrdDims))
                            {
                                return i;
                            }
                        }
                    }

                    function findLastDelClick(OrdDims, lst, lstIdx)
                    {
                        var idx = -1;
                        var chkBoxCount = lst.getElementsByTagName("input");
                        for (var i = 0; i < chkBoxCount.length; i++)
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

                    function WriteDims(doc, lst, OrdDims, OrdDimsCount)
                    {
                        doc.txtDims.value = "";
                        if (OrdDimsCount > 0)
                        {
                            var idx;
                            var chkBoxCount = lst.getElementsByTagName("input");
                            for(var key in OrdDims) 
                            {
                                idx = OrdDims[key];
                                if (chkBoxCount[idx].checked)
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


                    //jrs 2015/04/14
                    function GetFirstSelectedIdx(lst) {
                        var ControlRef = document.getElementById(lst);
                        var CheckBoxListArray = ControlRef.getElementsByTagName('input');

                        for (var i = 0; i < CheckBoxListArray.length; i++) {
                            var checkBoxRef = CheckBoxListArray[i];

                            if (checkBoxRef.checked == true) {
                                return i;
                            }
                        }
                        return -1;
                    }

                    //jrs 2015/04/14
                    function SetFirstSelectedIdx(lst, idx) {
                        var ControlRef = document.getElementById(lst);
                        var CheckBoxListArray = ControlRef.getElementsByTagName('input');
                        ControlRef.SelectedIndex = -1;

                        for (var i = 0; i < CheckBoxListArray.length; i++) {
                            var checkBoxRef = CheckBoxListArray[i];

                            if (i == idx) {
                                checkBoxRef.checked = true;
                                ControlRef.SelectedIndex = i;
                            }
                            else {
                                checkBoxRef.checked = false;
                            }
                        }
                    }