// JScript File
//Created by José R Solórzano on November 11th, 2011

                    //jrs 2011/11/03
                    var SceOrdFiles = new Array();                       
                    var SceOrdFilesCount = 0;
                    var clickDownY=0;
                    var clickUpY=0;
                    var clickWshift = false;

                    function lstFiles_ClickDown(e)
                    {
                        clickDownY = e.clientY;
                    }

                    function lstFiles_ClickUp(e)
                    {
                        clickUpY = e.clientY;
                        clickWshift = e.shiftKey;
                    }

                    //jrs 2012/12/05
                    function lstFiles_Change(e) {
                        var isChrome = !!(window.chrome && chrome.webstore && chrome.webstore.install);
                        if (isChrome) {
                            lstFiles_Click(e);
                        }
                    }

                    function lstFiles_Click(e)
                    {
                        //'jrs 2011/01/13
                        //'Keeping Track of the order in which files were selected
                        var doc = document.forms[0];
                        var lst = doc.lstFiles;
                        var fileSelCount = selectCount(lst);
                        var lstClick=-1;
                        
                        if (fileSelCount == 1 && lst[lst.selectedIndex].selected)
                        {
                            SceOrdFiles = new Array();                       
                            SceOrdFilesCount = 0;
                        }
                     
                      try {
                            if (fileSelCount - SceOrdFilesCount == 1)
                            {
                                //find last selected, lst.selectedIndex returns the first one alphabetically, not the last one selected
                                lstClick=findLastAddClick(SceOrdFiles, lst);
                            
                                SceOrdFiles["x" + lstClick] = lstClick;
                                SceOrdFilesCount++;
                            }        
                            else if (SceOrdFilesCount - fileSelCount == 1)
                            {
                                var idx = findUnselectedItem(SceOrdFiles, lst);
                                delete SceOrdFiles["x" + idx];
                                SceOrdFilesCount--;
                                lstClick=idx;
                            }
                        }
                        catch(ex) {
                            // reset the array
                            SceOrdFilesCount = 0;
                        }
                        
                        try{
                                //Check if using shift down
                                var x;
                                var bRedo;
                                bRedo = false;
                                if (fileSelCount != SceOrdFilesCount) 
                                {
                                    var i = 0;
                                    var bAsc;
                                    bAsc = false;
                                    var lstIdx=findLastIndex(SceOrdFiles);
                                    if (lstIdx == "")
                                    {
                                        if (clickDownY == clickUpY) {
                                            lstIdx = 0;
                                        }
                                        else if (clickDownY < clickUpY) //ascending
                                        {
                                            lstIdx = lst.selectedIndex;
                                        }
                                        else {
                                            lstIdx = "x" + lst.selectedIndex;
                                            lstIdx = findLastAddShiftClick(SceOrdFiles, lst, lstIdx);
                                        }

                                        SceOrdFiles["x" + lstIdx] = lstIdx;
                                        lstIdx = "x" + lstIdx;
                                        SceOrdFilesCount++;
                                    }
                                    else
                                    {
                                        //Check for drag
                                        while (SceOrdFilesCount>0 && !lst[SceOrdFiles[lstIdx]].selected)
                                        {
                                            delete SceOrdFiles[lstIdx];
                                            SceOrdFilesCount--;                                        
                                            if (SceOrdFilesCount == 0)
                                            {
                                                lstIdx = lst.selectedIndex;
                                            }
                                            else
                                            {
                                                lstIdx=findLastIndex(SceOrdFiles);
                                            }
                                        }
                                    }
                                    //Adding
                                    if (fileSelCount > SceOrdFilesCount)
                                    {
                                        //jrs 2015/09/17 not working on chorme if (e.shiftKey)
                                        if (clickWshift)
                                        {
                                            lstClick=findLastAddShiftClick(SceOrdFiles, lst, lstIdx);
                                            //From Lower to Higher
                                            if (lstClick > SceOrdFiles[lstIdx])
                                            {
                                                for (i = SceOrdFiles[lstIdx] + 1; i <= lstClick; i++)
                                                {
                                                    SceOrdFiles["x" + i] = i;
                                                    SceOrdFilesCount++;
                                                }
                                            }                                       
                                            else //from higher to lower
                                            {
                                                for (i = SceOrdFiles[lstIdx] - 1; i >= lst.selectedIndex; i--)
                                                {
                                                    SceOrdFiles["x" + i] = i;
                                                    SceOrdFilesCount++;
                                                }
                                            }
                                        }
                                        else // in case of drag
                                        {
                                            if (clickDownY < clickUpY) //ascending
                                            {
                                                for (i=0; i<lst.length; i++)
                                                {
                                                    if (lst[i].selected && !("x"+i in SceOrdFiles))
                                                    {
                                                        SceOrdFiles["x"+i] = i;
                                                        SceOrdFilesCount++;
                                                    }
                                                }
                                            }
                                            else // descending
                                            {
                                                for (i=lst.length-1; i>=0; i--)
                                                {
                                                    if (lst[i].selected && !("x"+i in SceOrdFiles))
                                                    {
                                                        SceOrdFiles["x"+i] = i;
                                                        SceOrdFilesCount++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else //Removing
                                    {
                                        for (i = 0; i < lst.length; i++)
                                        {
                                            if (!lst[i].selected && ("x"+i in SceOrdFiles))
                                            {
                                                delete SceOrdFiles["x" + i];
                                                SceOrdFilesCount--;
                                            }
                                        }
                                    }
                                }
                                
                                //check if using drag
                                //if still not all items in
                                if (fileSelCount != SceOrdFilesCount)
                                {
                                    //remove all from collection
                                    SceOrdFiles = new Array();                       
                                    SceOrdFilesCount = 0;
                                    bRedo = true;
                                }             
                        }
                        catch(ex) {
                            // reset the array
                            SceOrdFilesCount = 0;
                        }                                    
   
                        //Validate
                        for (var x in SceOrdFiles)
                        {
                            if (!lst[SceOrdFiles[x]].selected) 
                            {
                                SceOrdFiles = new Array();                       
                                SceOrdFilesCount = 0;
                                bRedo = true;
                                break;
                            }
                        }

                        if (bRedo) 
                        {
                            //jrs 2015/09/17 not working on chorme if (e.shiftKey)
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
                                        SceOrdFiles["x" + i] = i;
                                        SceOrdFilesCount++;
                                    }
                                }
                            }
                            else
                            {
                                for (i = lst.length - 1; i>0; i--)
                                {
                                    if (lst[i].selected)
                                    {
                                        SceOrdFiles["x" + i] = i;
                                        SceOrdFilesCount++;
                                    }
                                }
                            }//bAsc
                        } // bRedo

                        WriteFiles(doc, lst, SceOrdFiles, SceOrdFilesCount);
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
                    
                    function findUnselectedItem(SceOrdFiles, lst)
                    {
                        var idx=-1;
                        for(var key in SceOrdFiles) {
                            idx = SceOrdFiles[key];
                            if (!lst[idx].selected)
                            {
                                return idx;
                            }
                        }
                    }
                    
                    function findLastIndex(SceOrdFiles)
                    {
                        var key="";
                        for(key in SceOrdFiles) {
                        }                    
                        return key;
                    }
                    
                    function findLastAddShiftClick(SceOrdFiles, lst, lstIdx)
                    {
                        var idx=-1;
                        for (var i=0; i<lst.length; i++)
                        {
                            if (lst[i].selected && !("x" + i in SceOrdFiles) )
                            {
                                if  (i < SceOrdFiles[lstIdx])
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

   
                    function findLastAddClick(SceOrdFiles, lst)
                    {
                        for (var i=0; i<lst.length; i++)
                        {
                            if (lst[i].selected && !("x" + i in SceOrdFiles) )
                            {
                                return i;
                            }
                        }
                    }

                    function findLastDelClick(SceOrdFiles, lst, lstIdx)
                    {
                        var idx=-1;
                        for (var i=0; i<lst.length; i++)
                        {
                            if (lst[i].selected && ("x" + i in SceOrdFiles) )
                            {
                                if  (i > SceOrdFiles[lstIdx])
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

                    function WriteFiles(doc, lst, SceOrdFiles, SceOrdFilesCount)
                    {
                        doc.txtFiles.value = "";
                        if (SceOrdFilesCount > 1)
                        {
                            var idx;
                            for(var key in SceOrdFiles) 
                            {
                                idx = SceOrdFiles[key];
                                if (lst[idx].selected)
                                {
                                    if (doc.txtFiles.value > "")
                                    {
                                        doc.txtFiles.value = doc.txtFiles.value + ","
                                    }
                                    doc.txtFiles.value = doc.txtFiles.value + idx;
                                }
                            }
                        }
                    }
