Option Strict Off
Option Explicit On

Partial Class frm_SelectProjectFile
    Inherits Page

    Private Sub frm_SelectProjectFile_Load(sender As Object, e As EventArgs) Handles Me.Load
        'jrs 2023/10/03
        If Not Page.IsPostBack Then
            'Open Project Folder
            Dim sPrjFile As String
            sPrjFile = Dir(DataFileDir & "ProjectData\*.db")
            'Load listbox with all file names
            Do While sPrjFile > ""
                lstFiles.Items.Add(sPrjFile)
                sPrjFile = Dir()
            Loop
            'Select the first one by default
            If lstFiles.Items.Count > 0 Then
                lstFiles.Items(0).Selected = True
            End If
        End If
        Dim sFileExists As String
        If Dir(DataFileDir$ & "ifsHistSeriesOriginalNoProjectData.db") > "" Then
            sFileExists = "True"
        Else
            sFileExists = "False"
        End If
        cmdLoad.Attributes.Add("onClick", "javascript:checkFile('" & sFileExists & "')")
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Support.FormManager.Show(Me, "frm_MainMenu.aspx")
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        LoadProjectFile(DataFileDir & "ProjectData\" & lstFiles.Text, Session("IFs_MDB2Active").replace(".MDB", ".db"))
        'Rebuild the base
        Support.FormManager.Show(Me, "frm_RebuildBase.aspx")
        'Support.FormManager.Show(Me, "frm_MainMenu.aspx")
    End Sub
End Class
