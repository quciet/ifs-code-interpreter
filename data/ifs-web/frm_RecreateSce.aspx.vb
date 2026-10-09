Option Strict Off
Option Explicit On

Imports IFs.Data

Partial Class frm_RecreateSce
    Inherits Page

    Public Sub LoadFiles()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim fs 'As New Scripting.FileSystemObject
        Dim BASE 'As Scripting.File
        Dim currRun 'As Scripting.File
        ''Dim fRunFile As Folder
        fs = Server.CreateObject("Scripting.FileSystemObject")
        'Set fRunFile = fs.GetFolder(PutFdrvRun2$)
        'fRunFile.Files returns collection of files
        Dim sWorkingFile As String
        cboRunFile.Items.Clear()
        cboBase.Items.Clear()
        'sWorkingFile = VB.Right(Session("WorkingFile"), Len(Session("WorkingFile")) - InStrRev(Session("WorkingFile"), "\"))
        sWorkingFile = Session("WorkingFile")
        sWorkingFile = sWorkingFile.Substring(sWorkingFile.LastIndexOf("\") + 1, sWorkingFile.Length - sWorkingFile.LastIndexOf("\") - 1)

        Dim bHis As Boolean = False 'jrs 2020/06/30

        'JRS 05/20/03
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") = LastYearHistory Then
            'jrs 2021/12/21
            If StandAloneMode = True Then
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBaseHist.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBaseHist.run")
            End If
            bHis = True
        Else
            'jrs 2021/12/21
            If StandAloneMode = True Then
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBase.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBase.run")
            End If
        End If

        ''CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cboRunFile.Items.Add(New ListItem("0 - " & "Working File, based on " & sWorkingFile))
        cboRunFile.Items.Add(New ListItem("1 - " & BASE.Name))
        cboBase.Items.Add(New ListItem("0 - " & "Working File, based on " & sWorkingFile))
        cboBase.Items.Add(New ListItem("1 - " & BASE.Name))

        Dim sRunFile As String
        '' Returns filename with specified extension. If more than one *.ini
        '' file exists, the first file found is returned.
        'jrs 2021/12/21
        If StandAloneMode = True Then
            sRunFile = Dir(Session("PutFdrvRun2") & "\*.run.db")
        Else
            sRunFile = Dir(Session("PutFdrvRun2") & "\*.run")
        End If


        Do While (sRunFile <> "")
            'jrs 2022/03/23 added And UCase(sRunFile) <> "WORKING.RUN.DB" 
            'jrs 2022/05/25 removed And UCase(sRunFile) <> UCase(sWorkingFile)
            If UCase(sRunFile) <> UCase(BASE.Name) And UCase(sRunFile) <> "WORKING.RUN" And
                    UCase(sRunFile) <> "WORKING.RUN.DB" And UCase(sRunFile) <> "IFSBASEHIST.RUN.DB" Then 'JRS 11/08/02
                currRun = fs.GetFile(Session("PutFdrvRun2") & "\" & sRunFile)
                'jrs 2022/01/12 If BASE.Size = currRun.Size Then
                If Session("IFsRunFiles").item(sRunFile).Visible Or (StandAloneMode And Not bHis) Then 'jrs 2005/06/27
                    cboRunFile.Items.Add(New ListItem(Trim(Str(cboRunFile.Items.Count)) & " - " & sRunFile))
                    cboBase.Items.Add(New ListItem(Trim(Str(cboBase.Items.Count)) & " - " & sRunFile))
                    ReDim Preserve Session("RunFiles")(cboBase.Items.Count - 1)
                    Session("RunFiles")(cboBase.Items.Count - 1) = Session("PutFdrvRun2") & "\" & sRunFile
                End If
            End If
            sRunFile = Dir()
        Loop
        'jrs 2006/09/06 Multiple Files
        cboRunFile.Items(0).Selected = True
        cboBase.Items(1).Selected = True
    End Sub

    Private Sub frm_RecreateSce_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not Page.IsPostBack Then
            LoadFiles()
        End If
        cmdRecreate.Attributes.Add("onClick", "return StartProcess();")
    End Sub

    Protected Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Support.FormManager.Show(Me, "frm_MainMenu.aspx")
    End Sub

    Protected Sub cmdRecreate_Click(sender As Object, e As EventArgs) Handles cmdRecreate.Click
        RecreateSce()
    End Sub

    Private Sub RecreateSce()
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
            Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
            Session("PutFdrvRun"), Session("CurrencySym"))

        Dim sVar As String
        Dim DimNum As Integer, VarIdx As Integer

        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        Dim MyFileforDisplayB As IStorage
        Dim MyDFB As IDataframe

        Dim Dim1 As String
        Dim Dim2 As String
        Dim Dim3 As String

        Dim bSceFileCreated As Boolean = False
        Dim vIdx As Integer

        Dim TargetOld(), TargetNew() As Single
        ReDim TargetOld(Session("LastYear") - Session("FirstYear") + 1), TargetNew(Session("LastYear") - Session("FirstYear") + 1)
        Dim bCurrDiff As Boolean = False

        Dim objWriter As System.IO.StreamWriter
        Dim sLine As String

        If cboRunFile.SelectedIndex <> cboBase.SelectedIndex Then
            For vIdx = 1 To NumbVars
                sVar = V(vIdx)
                VarIdx = vIdx
                DimNum = -1
                MyDF = Nothing
                MyDFB = Nothing

                'Only for parameters and initial conditions
                If VType(vIdx) > 1 And VScenario(vIdx) > "" And Not IsDBNull(VScenCat(vIdx)) Then
                    'Not considering if variable has the dimension fixed
                    If VarIdx > 0 Then
                        If VDim3(VarIdx) > 0 Then
                            DimNum = 3
                        ElseIf VDim2(VarIdx) > 0 Then
                            DimNum = 2
                        ElseIf VDim1(VarIdx) > 0 Then
                            DimNum = 1
                        Else
                            DimNum = 0
                        End If
                    Else
                        DimNum = 3
                    End If

                    Dim Dim1Count As Integer
                    If VDim1(VarIdx) = 0 Then
                        Dim1Count = 1
                        Dim1 = ""
                    Else
                        Dim1Count = MaxVal(VDim1(VarIdx)) - 1
                    End If

                    Dim Dim2Count As Integer
                    If VDim2(VarIdx) = 0 Then
                        Dim2Count = 1
                        Dim2 = ""
                    Else
                        Dim2Count = MaxVal(VDim2(VarIdx)) - 1
                    End If

                    Dim Dim3Count As Integer
                    If VDim3(VarIdx) = 0 Then
                        Dim3Count = 1
                        Dim3 = ""
                    Else
                        Dim3Count = MaxVal(VDim3(VarIdx)) - 1
                    End If

                    For DimIdx = 1 To Dim1Count
                        If VDim1(VarIdx) > 0 Then
                            Dim1 = GRPS(VDim1(VarIdx), DimIdx)
                        End If

                        For DimIdx2 = 1 To Dim2Count
                            If VDim2(VarIdx) > 0 Then
                                Dim2 = GRPS(VDim2(VarIdx), DimIdx2)
                            End If

                            For DimIdx3 = 1 To Dim3Count
                                If VDim3(VarIdx) > 0 Then
                                    Dim3 = GRPS(VDim3(VarIdx), DimIdx3)
                                End If

                                ReDim TargetNew(Session("LastYear") - Session("FirstYear") + 1)
                                gLoadForDisplayNET(sVar, VarIdx, Dim1, DimIdx, Dim2, DimIdx2, Dim3, DimIdx3, "", cboRunFile.SelectedIndex, VarIdx = 1 And DimIdx = 1 And DimIdx2 = 1 And DimIdx3 = 1, False, Nothing, 1, TargetNew, 1,
                                    1, Session("FirstYear"), Session("LastYear"), 0, True, Nothing, Nothing, Session("RunFiles"), Session("PutFdrvRun"), ShortSce, Session("FinYear"),
                                    Session("IFs_MDB2Active"), 1, MyFileforDisplay, MyDF)

                                ReDim TargetOld(Session("LastYear") - Session("FirstYear") + 1)
                                gLoadForDisplayNET(sVar, VarIdx, Dim1, DimIdx, Dim2, DimIdx2, Dim3, DimIdx3, "", cboBase.SelectedIndex, VarIdx = 1 And DimIdx = 1 And DimIdx2 = 1 And DimIdx3 = 1, False, Nothing, 1, TargetOld, 1,
                                    1, Session("FirstYear"), Session("LastYear"), 0, True, Nothing, Nothing, Session("RunFiles"), Session("PutFdrvRun"), ShortSce, Session("FinYear"),
                                    Session("IFs_MDB2Active"), 1, MyFileforDisplayB, MyDFB)

                                bCurrDiff = False

                                For YrIdx = 1 To Session("LastYear") - Session("FirstYear") + 1
                                    If Math.Abs(TargetOld(YrIdx) - TargetNew(YrIdx)) >= 0.00005 Then
                                        bCurrDiff = True
                                        Exit For
                                    End If
                                    If VType(vIdx) = 2 Then Exit For 'Only check first year for initial conditions
                                Next YrIdx

                                'Open sce file
                                If bCurrDiff And Not bSceFileCreated Then
                                    bSceFileCreated = True
                                    objWriter = New System.IO.StreamWriter(Session("ScenarioFileDir") & "WorkingRecreated.Sce", append:=False)
                                End If

                                If bCurrDiff Then
                                    sLine = "CUSTOM," & sVar
                                    If VDim1(VarIdx) = 1 Then
                                        sLine = sLine & "," & Dim1
                                    ElseIf VDim1(VarIdx) > 1 Then
                                        sLine = sLine & "," & DimIdx.ToString
                                    End If
                                    If VDim2(VarIdx) = 1 Then
                                        sLine = sLine & "," & Dim2
                                    ElseIf VDim2(VarIdx) > 1 Then
                                        sLine = sLine & "," & DimIdx2.ToString
                                    End If
                                    If VDim3(VarIdx) = 1 Then
                                        sLine = sLine & "," & Dim3
                                    ElseIf VDim3(VarIdx) > 1 Then
                                        sLine = sLine & "," & DimIdx3.ToString
                                    End If

                                    For YrIdx = 1 To Session("LastYear") - Session("FirstYear") + 1
                                        sLine = sLine & "," & TargetNew(YrIdx).ToString
                                    Next
                                    objWriter.WriteLine(sLine)
                                End If
                            Next DimIdx3
                        Next DimIdx2
                    Next DimIdx
                End If
            Next vIdx
        End If


        Dim msg As String

        If bSceFileCreated Then
            objWriter.Close()
            msg = "Sce File Created under SCENARIO."
        Else
            msg = "No differences were found between files, no scenario file was created."
        End If
        cmdRecreate.Text = "(R)ecreate"
        cmdRecreate.Enabled = True

        LoadMessage(msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub
End Class
