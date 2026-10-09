// JScript File
//Created by José R Solórzano on March 24th, 2017, based on FilesInOrder

                    //jrs 2011/11/03
                    var OrdLists = new Array();                       
                    var OrdListsCount = 0;
                    var clickDownY=0;
                    var clickUpY = 0;
                    var clickWshift = false;
                    
                    function lstPacks_ClickDown(e) {
                        clickDownY = e.clientY;
                    }

                    function lstPacks_ClickUp(e) {
                        clickUpY = e.clientY;
                        clickWshift = e.shiftKey;
                    }

                    //jrs 2012/12/05
                    function lstPacks_Change(e) 
                    {
                        var isChrome = !!(window.chrome && chrome.webstore && chrome.webstore.install);
                        if (isChrome) {
                            lstPacks_Click(e);
                        }
                    }

                    function lstPacks_Click(e)
                    {
                        //'jrs 2011/01/13
                        //'Keeping Track of the order in which Dims were selected
                        var doc = document.forms[0];
                        var lst = doc.lstPacks;
                        var DimselCount = selectCount(lst);
                        var lstClick=-1;
                        
                        if (DimselCount == 1 && lst[lst.selectedIndex].selected)
                        {
                            OrdLists = new Array();                       
                            OrdListsCount = 0;
                        }
                     
                      try {
                            if (DimselCount - OrdListsCount == 1)
                            {
                                //find last selected, lst.selectedIndex returns the first one alphabetically, not the last one selected
                                lstClick=findLastAddClick(OrdLists, lst);
                            
                                OrdLists["x" + lstClick] = lstClick;
                                OrdListsCount++;
                            }        
                            else if (OrdListsCount - DimselCount == 1)
                            {
                                var idx = findUnselectedItem(OrdLists, lst);
                                delete OrdLists["x" + idx];
                                OrdListsCount--;
                                lstClick=idx;
                            }
                        }
                        catch(ex) {
                            // reset the array
                            OrdListsCount = 0;
                        }
                        
                        try{
                                //Check if using shift down
                                var x;
                                var bRedo;
                                bRedo = false;
                                if (DimselCount != OrdListsCount) 
                                {
                                    var i = 0;
                                    var bAsc;
                                    bAsc = false;
                                    var lstIdx=findLastIndex(OrdLists);
                                    if (lstIdx == "")
                                    {
                                        if (clickDownY <= clickUpY) //ascending
                                        {
                                            lstIdx = lst.selectedIndex;
                                        }
                                        else
                                        {
                                            lstIdx = "x" + lst.selectedIndex;
                                            lstIdx= findLastAddShiftClick(OrdLists, lst, lstIdx);
                                        }

                                        OrdLists["x" + lstIdx] = lstIdx;
                                        lstIdx = "x" + lstIdx;
                                        OrdListsCount++;
                                    }
                                    else
                                    {
                                        //Check for drag
                                        while (OrdListsCount>0 && !lst[OrdLists[lstIdx]].selected)
                                        {
                                            delete OrdLists[lstIdx];
                                            OrdListsCount--;                                        
                                            if (OrdListsCount == 0)
                                            {
                                                lstIdx = lst.selectedIndex;
                                            }
                                            else
                                            {
                                                lstIdx=findLastIndex(OrdLists);
                                            }
                                        }
                                    }
                                    //Adding
                                    if (DimselCount > OrdListsCount)
                                    {
                                        //jrs 2015/09/17 not working on chorme if (e.shiftKey)
                                        if (clickWshift)
                                        {
                                            lstClick=findLastAddShiftClick(OrdLists, lst, lstIdx);
                                            //From Lower to Higher
                                            if (lstClick > OrdLists[lstIdx])
                                            {
                                                for (i = OrdLists[lstIdx] + 1; i <= lstClick; i++)
                                                {
                                                    OrdLists["x" + i] = i;
                                                    OrdListsCount++;
                                                }
                                            }                                       
                                            else //from higher to lower
                                            {
                                                for (i = OrdLists[lstIdx] - 1; i >= lst.selectedIndex; i--)
                                                {
                                                    OrdLists["x" + i] = i;
                                                    OrdListsCount++;
                                                }
                                            }
                                        }
                                        else // in case of drag
                                        {
                                            if (clickDownY < clickUpY) //ascending
                                            {
                                                for (i=0; i<lst.length; i++)
                                                {
                                                    if (lst[i].selected && !("x"+i in OrdLists))
                                                    {
                                                        OrdLists["x"+i] = i;
                                                        OrdListsCount++;
                                                    }
                                                }
                                            }
                                            else // descending
                                            {
                                                for (i=lst.length-1; i>=0; i--)
                                                {
                                                    if (lst[i].selected && !("x"+i in OrdLists))
                                                    {
                                                        OrdLists["x"+i] = i;
                                                        OrdListsCount++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else //Removing
                                    {
                                        for (i = 0; i < lst.length; i++)
                                        {
                                            if (!lst[i].selected && ("x"+i in OrdLists))
                                            {
                                                delete OrdLists["x" + i];
                                                OrdListsCount--;
                                            }
                                        }
                                    }
                                }
                                
                                //check if using drag
                                //if still not all items in
                                if (DimselCount != OrdListsCount)
                                {
                                    //remove all from collection
                                    OrdLists = new Array();                       
                                    OrdListsCount = 0;
                                    bRedo = true;
                                }             
                        }
                        catch(ex) {
                            // reset the array
                            OrdListsCount = 0;
                        }                                    
   
                        //Validate
                        for (var x in OrdLists)
                        {
                            if (!lst[OrdLists[x]].selected) 
                            {
                                OrdLists = new Array();                       
                                OrdListsCount = 0;
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
                                        OrdLists["x" + i] = i;
                                        OrdListsCount++;
                                    }
                                }
                            }
                            else
                            {
                                for (i = lst.length - 1; i>0; i--)
                                {
                                    if (lst[i].selected)
                                    {
                                        OrdLists["x" + i] = i;
                                        OrdListsCount++;
                                    }
                                }
                            }//bAsc
                        } // bRedo

                        WriteLists(doc, lst, OrdLists, OrdListsCount);
                        // call to logic to filter dimensions
                        lstPacks_click();
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
                    
                    function findUnselectedItem(OrdLists, lst)
                    {
                        var idx=-1;
                        for(var key in OrdLists) {
                            idx = OrdLists[key];
                            if (!lst[idx].selected)
                            {
                                return idx;
                            }
                        }
                    }
                    
                    function findLastIndex(OrdLists)
                    {
                        var key="";
                        for(key in OrdLists) {
                        }                    
                        return key;
                    }
                    
                    function findLastAddShiftClick(OrdLists, lst, lstIdx)
                    {
                        var idx=-1;
                        for (var i=0; i<lst.length; i++)
                        {
                            if (lst[i].selected && !("x" + i in OrdLists) )
                            {
                                if  (i < OrdLists[lstIdx])
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

   
                    function findLastAddClick(OrdLists, lst)
                    {
                        for (var i=0; i<lst.length; i++)
                        {
                            if (lst[i].selected && !("x" + i in OrdLists) )
                            {
                                return i;
                            }
                        }
                    }

                    function findLastDelClick(OrdLists, lst, lstIdx)
                    {
                        var idx=-1;
                        for (var i=0; i<lst.length; i++)
                        {
                            if (lst[i].selected && ("x" + i in OrdLists) )
                            {
                                if  (i > OrdLists[lstIdx])
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

                    function WriteLists(doc, lst, OrdLists, OrdListsCount)
                    {
                        doc.txtPacks.value = "";
                        if (OrdListsCount > 1)
                        {
                            var idx;
                            for(var key in OrdLists) 
                            {
                                idx = OrdLists[key];
                                if (lst[idx].selected)
                                {
                                    if (doc.txtPacks.value > "")
                                    {
                                        //doc.txtPacks.value = doc.txtPacks.value + ","
                                        doc.txtPacks.value = doc.txtPacks.value + ";"
                                    }
                                    //doc.txtPacks.value = doc.txtPacks.value + idx;
                                    doc.txtPacks.value = doc.txtPacks.value + lst[idx].value;
                                }
                            }
                        }
                    }
