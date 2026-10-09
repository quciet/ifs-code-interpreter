Option Strict Off
Option Explicit On

'Namespace IFsWeb
Public Class SwitchingHandler
    'jrs 2009/02/23
    'Dim basePaths(15) As String
    'Dim tBasePaths(15) As String
    'jrs 2012/05/17
    'Dim basePaths(16) As String
    'Dim tBasePaths(16) As String
    'jrs 2012/06/28 adding ifsInit.db (CurrencyCountry is saved in there)
    'jrs 2012/09/24 adding ifs.dat
    'jrs 2026/07/23 adding DATA\ifsHistSeriesDyadic.db
    'jrs 2026/03/25 adding ifsHistSeriesDyadic.db
    Dim basePaths(21) As String
    Dim tBasePaths(21) As String
    Dim folderName As String

    Dim numOfFiles As Integer

    Dim PutFdrvRun As String

    Public Sub New(sPutFdrvRun)
        PutFdrvRun = sPutFdrvRun
        'jrs 2009/02/23 adding ifsSmoking.db
        'numOfFiles = 10
        numOfFiles = 13 'jrs 2012/09/24
        basePaths(1) = PutDataFileDir$ & "IFs.db"
        basePaths(2) = PutDataFileDir$ & "IFsHistSeries.db"
        basePaths(3) = PutDataFileDir$ & "IFsWVSCohort.db"
        basePaths(4) = PutDataFileDir$ & "IFsCoVaTra.db"
        basePaths(5) = PutDataFileDir$ & "IFsCoVaTraSeries.db"
        basePaths(6) = PutFdrvRun$ & "IFsBase.run.db"
        basePaths(7) = PutFdrvRun$ & "IFsBaseHist.run.db"
        basePaths(8) = PutDataFileDir$ & "SAMBase.db"
        basePaths(9) = PutDataFileDir$ & "IFsPop.db"
        basePaths(10) = PutDataFileDir$ & "IFsPopHist.db"
        'jrs 2009/02/23
        '    basePaths(11) = PutFdrvRun$ & "IFs.db"
        '    basePaths(12) = PutFdrvRun$ & "IFsHistSeries.db"
        '    basePaths(13) = PutFdrvRun$ & "IFsWVSCohort.db"
        '    basePaths(14) = PutFdrvRun$ & "IFsCoVaTra.db"
        '    basePaths(15) = PutFdrvRun$ & "IFsCoVaTraSeries.db"
        basePaths(11) = PutDataFileDir$ & "IFsSmoking.db"
        'jrs 2012/06/28
        basePaths(12) = PutFdrv$ & "ifsInit.db"
        'jrs 2012/09/24
        basePaths(13) = PutDataFileDir$ & "IFS.DAT"

        basePaths(numOfFiles + 1) = PutFdrvRun$ & "IFs.db"
        basePaths(numOfFiles + 2) = PutFdrvRun$ & "IFsHistSeries.db"
        basePaths(numOfFiles + 3) = PutFdrvRun$ & "IFsWVSCohort.db"
        basePaths(numOfFiles + 4) = PutFdrvRun$ & "IFsCoVaTra.db"
        basePaths(numOfFiles + 5) = PutFdrvRun$ & "IFsCoVaTraSeries.db"
        basePaths(numOfFiles + 6) = PutFdrvRun$ & "IFsSmoking.db"
        'jrs 2026/03/25
        basePaths(numOfFiles + 7) = PutFdrvRun$ & "IFsHistSeriesDyadic.db"
        'jrs 2026/07/23 '21
        basePaths(numOfFiles + 8) = PutDataFileDir$ & "IFsHistSeriesDyadic.db"
    End Sub

    Public Sub Initialize()

        '-----------------------------------------------------------
        tBasePaths(1) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\IFs.db"
        tBasePaths(2) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\IFsHistSeries.db"
        tBasePaths(3) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\IFsWVSCohort.db"
        tBasePaths(4) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\IFsCoVaTra.db"
        tBasePaths(5) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\IFsCoVaTraSeries.db"
        tBasePaths(6) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\IFsBase.run.db"
        tBasePaths(7) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\IFsBaseHist.run.db"
        tBasePaths(8) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\SAMBase.db"
        tBasePaths(9) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\IFsPop.db"
        tBasePaths(10) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\IFsPopHist.db"
        'jrs 2009/02/23 added ifsSmoking.mdb
        tBasePaths(11) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\IFsSmoking.db"
        'jrs 2012/06/28
        tBasePaths(12) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\ifsInit.db"
        'jrs 2012/09/24
        tBasePaths(13) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\IFS.DAT"
        'jrs 2026/03/25
        tBasePaths(20) = PutDataFileDir$ + "ProvinceSwitch\" & folderName & "\IFsHistSeriesDyadic.db"
    End Sub


    Public Sub SaveCurrentSetup()

        If ModelRunningWith <> "Base" Then
            'jrs 2015/08/17
            If File_Exists(PutDataFileDir$ & "ProvinceSwitch\" & ModelRunningWith, True) = False Then
                'Load frm_CreateFolder
                'frm_CreateFolder.Show vbModal
                MkDir(PutDataFileDir$ & "ProvinceSwitch\" & ModelRunningWith)
                folderName = ModelRunningWith
                Copy15ToDifferent()

            Else
                folderName = ModelRunningWith
                Initialize()
                Copy15toBaseAndOthers()
            End If
        Else
            If File_Exists(PutDataFileDir$ & "ProvinceSwitch\Base", True) = False Then
                MkDir(PutDataFileDir$ & "ProvinceSwitch\Base")
                'MsgBox("The process will automatically save current setup/files. After that add process will start. So it will take a little more time. ")
                'Else
                '    If MsgBox("Setup For No State/Province Already Exist. Do you still want to overwright it?", vbYesNo, "Already Exist") = VbMsgBoxResult.vbNo Then
                '        Exit Sub
                '    End If
            End If
            folderName = "Base"
            Initialize()
            Copy15toBaseAndOthers()
        End If

    End Sub


    Public Sub Copy15toBaseAndOthers()
        For i = 1 To numOfFiles
            'frm_AddDelProvinces.lblProgress.Caption = "Copying " & basePaths(i) & " to " & tBasePaths(i)
            'frm_AddDelProvinces.lblProgress.Refresh
            FileCopy(basePaths(i), tBasePaths(i))

        Next i
        'jrs 2026/03/25 ifsHistDyadic is in position 20
        If Dir(basePaths(20)) > "" Then
            FileCopy(basePaths(20), tBasePaths(20))
        End If
    End Sub

    Public Sub PerformPredeleteActions(rname As String)

        'If rname <> "No State/Province" Then
        '    If File_Exists(PutDataFileDir$ & "ProvinceSwitch\Base", True) = False Then
        '        MsgBox("No State/Province Is Not Saved. So Delete Can Not Be Perfomed.")
        '        Exit Sub
        '    End If
        'End If

        'If File_Exists(PutDataFileDir$ & "ProvinceSwitch\" & rname, True) = True Then
        '    If MsgBox("Do you want to completely remove from hard drive?", vbYesNo, "Confirmation") = VbMsgBoxResult.vbYes Then
        '        folderName = rname
        '        Initialize()
        '        Remove15FromDifferent(PutDataFileDir$ & "ProvinceSwitch\" & rname)
        '        '        Else
        '        'jrs 2012/05/21 removed message
        '        'jrs 2012/05/19
        '        '            MsgBox ("You choose not to completely delete which means you just want to remove" & _
        '        '            rname & " and switch with some other availabe runs. So please go for switch procedure.")
        '    End If
        '    LoadBase()
        '    Reinitialize()
        'Else
        '    If MsgBox(rname & " is not saved, Do you want to save it before removing?", vbYesNo, "Not Saved") = VbMsgBoxResult.vbYes Then
        '        SaveCurrentSetup()
        '    End If

        '    MsgBox("Since " & rname & "has been removed now model can not run with no setup. So, it will automatically perform switching operation with No state/province data.")
        '    LoadBase()
        '    Reinitialize()
        'End If

    End Sub
    Public Sub CompletelyErase(rname As String)
        folderName = rname
        Initialize()
        Remove15FromDifferent(PutDataFileDir$ & "ProvinceSwitch\" & rname)
    End Sub
    Public Sub Remove15FromDifferent(path As String)
        Dim i As Integer
        For i = 1 To numOfFiles
            'frm_AddDelProvinces.lblProgress.Caption = "Deleting " & tBasePaths(i)
            'frm_AddDelProvinces.lblProgress.Refresh
            'jrs 2009/02/23 added check
            If Dir(tBasePaths(i)) > "" Then
                Kill(tBasePaths(i))
            End If
        Next i
        'jrs 2026/03/25
        If Dir(tBasePaths(20)) > "" Then
            Kill(tBasePaths(20))
        End If

        RmDir(path)
    End Sub
    Public Sub Remove15From()
        Dim i As Integer
        For i = 1 To numOfFiles
            'frm_AddDelProvinces.lblProgress.Caption = "Deleting " & basePaths(i)
            'frm_AddDelProvinces.lblProgress.Refresh
            Kill(basePaths(i))
        Next i
        'jrs 2026/03/25
        If Dir(basePaths(20)) > "" Then
            Kill(basePaths(20))
            Kill(basePaths(21))
        End If

    End Sub

    Public Sub PerformSwitching(dest As String, txtSaveB4Switch As String)
        If dest = "No State/Province" Then
            dest = "Base"
        End If

        If File_Exists(PutDataFileDir$ & "ProvinceSwitch\" + ModelRunningWith, True) = False Then
            'If MsgBox("Setup For " + ModelRunningWith + " Does Not Exist. Do You Want To Save It?", vbYesNo, "Not Saved") = VbMsgBoxResult.vbYes Then
            If txtSaveB4Switch = "True" Then
                MkDir(PutDataFileDir$ & "ProvinceSwitch\" + ModelRunningWith)
                'MsgBox("After saving " + ModelRunningWith + " switching process will start. So, it will take little more time.")
                folderName = ModelRunningWith
                Initialize()
                Copy15toBaseAndOthers()
            End If
        End If

        folderName = dest
        Initialize()
        CopyToCurrent()
        UpdateCurrentlyRunningModel(dest)
        'frm_AddDelProvinces.lblProgress.Caption = "Switching procedure finished successfully."
        'frm_AddDelProvinces.lblProgress.Refresh
    End Sub

    Public Sub CopyToCurrent()
        For i = 1 To numOfFiles
            'frm_AddDelProvinces.lblProgress.Caption = "Copying " & tBasePaths(i) & " to " & basePaths(i)
            'frm_AddDelProvinces.lblProgress.Refresh
            FileCopy(tBasePaths(i), basePaths(i))
        Next i
        ' overwriting 5 working files into runfiles
        'frm_AddDelProvinces.lblProgress.Caption = "Copying " & tBasePaths(6) & " to " & PutFdrvRun$ + "Working.run"
        'frm_AddDelProvinces.lblProgress.Refresh
        FileCopy(tBasePaths(6), PutFdrvRun$ + "Working.run.db")
        'frm_AddDelProvinces.lblProgress.Caption = "Copying " & tBasePaths(8) & " to " & PutFdrvRun$ + "SAMWorking.mdb"
        'frm_AddDelProvinces.lblProgress.Refresh
        FileCopy(tBasePaths(8), PutFdrvRun$ + "SAMWorking.db")
        'frm_AddDelProvinces.lblProgress.Caption = "Copying " & tBasePaths(9) & " to " & PutFdrvRun$ + "IFsPopWork.mdb"
        'frm_AddDelProvinces.lblProgress.Refresh
        FileCopy(tBasePaths(9), PutFdrvRun$ + "IFsPopWork.db")
        'frm_AddDelProvinces.lblProgress.Caption = "Copying " & tBasePaths(9) & " to " & PutFdrvRun$ + "IFsPop.mdb"
        'frm_AddDelProvinces.lblProgress.Refresh
        FileCopy(tBasePaths(9), PutFdrvRun$ + "IFsPop.db")
        'frm_AddDelProvinces.lblProgress.Caption = "Copying " & tBasePaths(10) & " to " & PutFdrvRun$ + "IFsPopHist.mdb"
        'frm_AddDelProvinces.lblProgress.Refresh
        FileCopy(tBasePaths(10), PutFdrvRun$ + "IFsPopHist.db")
        'jrs 2012/05/17 not in the loop to avoid having to reorder the rest of the files
        'frm_AddDelProvinces.lblProgress.Caption = "Copying " & tBasePaths(11) & " to " & basePaths(numOfFiles + 6)
        'frm_AddDelProvinces.lblProgress.Refresh
        FileCopy(tBasePaths(11), basePaths(numOfFiles + 6)) 'jrs 2012/09/24

        'jrs 2009/02/23 For i = 11 To 15
        For i = numOfFiles + 1 To numOfFiles + 5
            'jrs 2009/02/23 frm_AddDelProvinces.lblProgress.Caption = "Copying " & tBasePaths(i - 10) & " to " & basePaths(i)
            'frm_AddDelProvinces.lblProgress.Caption = "Copying " & tBasePaths(i - numOfFiles) & " to " & basePaths(i)
            'frm_AddDelProvinces.lblProgress.Refresh
            'jrs 2009/02/23 FileCopy tBasePaths(i - 10), basePaths(i)
            FileCopy(tBasePaths(i - numOfFiles), basePaths(i))
        Next i

        'jrs 2026/03/25
        If Dir(tBasePaths(20)) > "" Then
            FileCopy(tBasePaths(20), basePaths(20))
            FileCopy(tBasePaths(20), basePaths(21))
        End If

        'Screen.MousePointer = vbNormal
    End Sub

    Public Sub LoadBase()
        folderName = "Base"
        Initialize()
        CopyToCurrent()
        'UpdateCurrentlyRunningModel("Base")
    End Sub


    Public Sub Reinitialize(sIFSDB As String)
        '    Dim reIni As New ReInitializer
        '    reIni.Reinitialize
        ReLoadRegionsandGroups(sIFSDB)
    End Sub

    'jrs 2022/02/21
    Public Function bAnyDiff() As Boolean
        bAnyDiff = False
        folderName = "Base"
        Initialize()

        'Check every file
        For i = 1 To numOfFiles
            'No need to check ifsInit.mdb, because it always changes timestamp
            If i <> 12 Then
                Dim FDT1 = FileDateTime(basePaths(i))
                Dim FDT2 = FileDateTime(tBasePaths(i))

                If FDT1 <> FDT2 Then
                    bAnyDiff = True
                    Exit For
                End If
            End If
        Next i
    End Function

    Public Sub Copy15ToDifferent()
        Initialize()
        Dim i As Integer
        For i = 1 To 13 'jrs 2014/11/04 '11 'jrs 2012/05/18 changed from 10 to 11
            FileCopy(basePaths(i), tBasePaths(i))
        Next i
        'jrs 2026/03/25
        If Dir(basePaths(20)) > "" Then
            FileCopy(basePaths(20), tBasePaths(20))
        End If
    End Sub

End Class

'End Namespace

