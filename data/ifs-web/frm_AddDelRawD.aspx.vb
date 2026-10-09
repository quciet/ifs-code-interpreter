Option Strict Off
Option Explicit On
Imports MarkItUp.WebControls
Imports System.IO

Partial Class frm_AddDelRawD
    Inherits Page

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.IfsVB60 = New System.Diagnostics.Process

    End Sub

    Protected WithEvents IfsVB60 As System.Diagnostics.Process

    Private Sub Inquiry()
        '--------------------------------------------------------------
        Dim rs As DataTable
        SetDataTablefromSQLite(Session("FdrvRun") + "ifs.db", "SELECT Country, FIPS_CODE FROM [Country Translation] ORDER BY FIPS_CODE", "", rs)
        GridView1.DataSource = rs
        GridView1.DataBind()

    End Sub

    Private Sub ChangeMode(sMode As String)
        '-------------------------------------------------------------------
        Dim msg2 As String
        'This rutine change the form depending
        'on the action you are going to perform
        Select Case sMode
        'When it is Detail shows the grid
            Case "G"
                pnlData.Visible = False
                GridView1.Visible = True
                cmdDelete.Enabled = True
                cmdAdd.Enabled = True
                lblNewCountry.Visible = False
                txtNewCountry.Visible = False
                'jrs 2006/03/20
                cmdUpdate.Enabled = True

        'When it is an Addition of a country
        'Shows the panel for addition
            Case "A"
                GridView1.Visible = False
                pnlData.Visible = True
                cmdDelete.Enabled = False
                cmdUpdate.Enabled = False
                txtCountry.Text = ""
                txtFIPSCD.Text = ""
                lblJSReb.Text = ""
                txtCountry.Enabled = True
                txtFIPSCD.Enabled = True
                'txtCountry.SetFocus
                Dim Msg As String
                Msg = "This process Adds a row with the New Country in all tables that "
                Msg = Msg + "have a Country in it. Files Affected are IFs, IFsHistSeries, "
                Msg = Msg + " IFsCoVaTra, IFsCoVaTraSeries and IFsWVSCohort. If you want to cancel this option click exit."
                    'MsgBox Msg, vbOKOnly, "Add Process"
        'When it is an Deletion of a country
        'Shows the panel for Deletion
            Case "D"
                GridView1.Visible = False
                pnlData.Visible = True
                cmdAdd.Enabled = False
                cmdUpdate.Enabled = False
                lblCountry.Enabled = False
                txtCountry.Enabled = False
                lblFIPSCD.Enabled = False
                txtFIPSCD.Enabled = False
                lblJSReb.Text = ""

                msg2 = "This process Deletes rows with the current Country in all tables that "
                msg2 = msg2 + "have that Country in it. Files Affected are IFs, IFsHistSeries, "
                msg2 = msg2 + " IFsCoVaTra, IFsCoVaTraSeries and IFsWVSCohort. If you want to cancel this option click exit."
                    'jrs 2004/11/27
                    'MsgBox msg2, vbOKOnly, "Add Process"
        'jrs 2005/04/15
            Case "U"
                GridView1.Visible = False
                pnlData.Visible = True
                cmdAdd.Enabled = False
                cmdDelete.Enabled = False
                lblCountry.Enabled = False
                txtCountry.Enabled = False
                lblFIPSCD.Enabled = False
                txtFIPSCD.Enabled = True 'jrs 2008/08/19 False
                lblNewCountry.Visible = True
                txtNewCountry.Visible = True
                lblJSReb.Text = ""

                msg2 = "This process Updates the country name in rows with the current Country in all tables that "
                msg2 = msg2 + "have that Country in it. Files Affected are IFs, IFsHistSeries, "
                msg2 = msg2 + " IFsCoVaTra, IFsCoVaTraSeries and IFsWVSCohort. If you want to cancel this option click exit."
                'jrs 2004/11/27
        End Select
    End Sub


    'jrs 2023/06/13
    Private Sub AddCountry(sCountry As String, sFIPSCD As String)
        '-------------------------------------------------------------------

        If sCountry = "" Or sFIPSCD = "" Then
            Exit Sub
        End If

        '   Check to see if the country name exists.
        '   --------------------------------------
        Dim NoValidCountry = -1
        While NoValidCountry
            NoValidCountry = 0
            Dim i As Integer
            For i = 0 To GridView1.Rows.Count - 1
                If UCase(sCountry) = UCase(GridView1.Rows(i).Cells(1).Text) Then
                    Dim sMsg = "<script>alert('You have specified an existing Country's name.  Please  try again.');</script>"
                    Response.Write(sMsg)
                    NoValidCountry = -1
                    Exit Sub
                End If
                If UCase(sFIPSCD) = UCase(GridView1.Rows(i).Cells(2).Text) Then
                    Dim sMsg = "<script>alert('You have specified an existing Country's FIPS CODE.  Please  try again.');</script>"
                    Response.Write(sMsg)
                    NoValidCountry = -1
                    Exit Sub
                End If
            Next i
        End While

        cmdAdd.Text = "In Process ..."
        cmdAdd.Enabled = False
        cmdExit.Enabled = False
        '   Add the new country in the set of Tables.
        '   ------------------------------------------
        'jrs 2022/11/11
        'Call a ifs.exe with parameters 9 "South Africa"
        Dim sFileName, sWorkDir As String
        'jrs 2022/08/03
        Dim sExeDir As String
        sExeDir = "net8"

        If DevelopmentMode = True And Dir(StartDirec & "Code.Ifs-Translation\bin\Debug\" & sExeDir & "\") > "" Then
            sFileName = DemandFile
            sWorkDir = StartDirec & "Code.Ifs-Translation\"
        Else
            sWorkDir = StartDirec & sExeDir & "\"
            sFileName = DemandFile
        End If

        IfsVB60.StartInfo.FileName = sFileName
        IfsVB60.StartInfo.WorkingDirectory = sWorkDir
        IfsVB60.StartInfo.Arguments = "8 a """ & sCountry & """ """ & sFIPSCD & """"
        'IfsVB60.StartInfo.FileName = StartDirec & "Notepad.exe"

        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")
        If fs.FileExists(Session("PutFdrvRun") & "progress.txt") Then
            Kill(Session("PutFdrvRun") & "progress.txt")
        End If
        IfsVB60.StartInfo.UseShellExecute = False
        IfsVB60.Start()
        Session("IfsVB60") = IfsVB60
        Timer1.Visible = True
        lblJSReb.Visible = True
        lblJSReb.Text = "Adding Country has begun - Please Wait"

    End Sub

    'jrs 2023/06/13
    Private Sub UpdCountry(sCountry As String, sNew As String, sFIPSCD As String)
        '-------------------------------------------------------------------

        If sCountry = "" Or sNew = "" Then
            Exit Sub
        End If

        cmdUpdate.Text = "In Process ..."
        cmdUpdate.Enabled = False
        cmdExit.Enabled = False
        '   Add the new country in the set of Tables.
        '   ------------------------------------------
        'jrs 2022/11/11
        'Call a ifs.exe with parameters 9 "South Africa"
        Dim sFileName, sWorkDir As String
        'jrs 2022/08/03
        Dim sExeDir As String
        sExeDir = "net8"

        If DevelopmentMode = True And Dir(StartDirec & "Code.Ifs-Translation\bin\Debug\" & sExeDir & "\") > "" Then
            sFileName = DemandFile
            sWorkDir = StartDirec & "Code.Ifs-Translation\"
        Else
            sWorkDir = StartDirec & sExeDir & "\"
            sFileName = DemandFile
        End If

        IfsVB60.StartInfo.FileName = sFileName
        IfsVB60.StartInfo.WorkingDirectory = sWorkDir
        IfsVB60.StartInfo.Arguments = "8 u """ & sCountry & """ """ & sNew & """ """ & sFIPSCD & """"
        'IfsVB60.StartInfo.FileName = StartDirec & "Notepad.exe"

        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")
        If fs.FileExists(Session("PutFdrvRun") & "progress.txt") Then
            Kill(Session("PutFdrvRun") & "progress.txt")
        End If
        IfsVB60.StartInfo.UseShellExecute = False
        IfsVB60.Start()
        Session("IfsVB60") = IfsVB60
        Timer1.Visible = True
        lblJSReb.Visible = True
        lblJSReb.Text = "Updating Country has begun - Please Wait"

    End Sub

    'jrs 2023/06/13
    Private Sub DelCountry(sCountry As String)
        '-------------------------------------------------------------------

        If sCountry = "" Then
            Exit Sub
        End If

        cmdDelete.Text = "In Process ..."
        cmdDelete.Enabled = False
        cmdExit.Enabled = False
        '   Add the new country in the set of Tables.
        '   ------------------------------------------
        'jrs 2022/11/11
        'Call a ifs.exe with parameters 9 "South Africa"
        Dim sFileName, sWorkDir As String
        'jrs 2022/08/03
        Dim sExeDir As String
        sExeDir = "net8"

        If DevelopmentMode = True And Dir(StartDirec & "Code.Ifs-Translation\bin\Debug\" & sExeDir & "\") > "" Then
            sFileName = DemandFile
            sWorkDir = StartDirec & "Code.Ifs-Translation\"
        Else
            sWorkDir = StartDirec & sExeDir & "\"
            sFileName = DemandFile
        End If

        IfsVB60.StartInfo.FileName = sFileName
        IfsVB60.StartInfo.WorkingDirectory = sWorkDir
        IfsVB60.StartInfo.Arguments = "8 d """ & sCountry & """"
        'IfsVB60.StartInfo.FileName = StartDirec & "Notepad.exe"

        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")
        If fs.FileExists(Session("PutFdrvRun") & "progress.txt") Then
            Kill(Session("PutFdrvRun") & "progress.txt")
        End If
        IfsVB60.StartInfo.UseShellExecute = False
        IfsVB60.Start()
        Session("IfsVB60") = IfsVB60
        Timer1.Visible = True
        lblJSReb.Visible = True
        lblJSReb.Text = "Deleting Country has begun - Please Wait"

    End Sub

    Private Sub Timer1_Elapsed(sender As Object, e As TimerElapsedEventArgs) Handles Timer1.Elapsed
        IfsVB60 = Session("IfsVB60")

        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")

        If fs.FileExists(Session("PutFdrvRun") & "progress.txt") Then
            Dim sLine, prevLine As String

            Try
                Using reader As StreamReader = New StreamReader(CStr(Session("PutFdrvRun") & "progress.txt"))
                    ' Read last line from file
                    sLine = reader.ReadLine
                    prevLine = sLine
                    Do While Not sLine = Nothing
                        prevLine = sLine
                        sLine = reader.ReadLine
                    Loop

                    lblJSReb.Text = prevLine
                End Using
            Catch ex As Exception
                lblJSReb.Text = prevLine
            End Try

        End If
        'mti WebForm Changes From June 21, 2004  -end commented for testing

        If IfsVB60.HasExited() Then
            lblJSReb.Text = "Process Complete"

            Timer1.Visible = False

            'Set the flag for the msg
            Session("NeedFileWrite") = True

            'Refresh Data

            'Back to Detail Mode
            ChangeMode("G")
            'Regresh the grid
            Inquiry()

            txtCountry.Text = ""
            txtFIPSCD.Text = ""

            cmdAdd.Text = "(A)dd"
            cmdUpdate.Text = "(U)pdate"
            cmdDelete.Text = "(D)elete"
            cmdAdd.Enabled = True
            cmdUpdate.Enabled = True
            cmdDelete.Enabled = True
            cmdExit.Enabled = True
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        'if it is in mode detail change the mode
        If GridView1.Visible = True Then
            'Change Mode to Adition
            ChangeMode("A")
        Else
            'Calls the function to Add a new country
            AddCountry(txtCountry.Text, txtFIPSCD.Text)
        End If
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        'if it is in mode detail change the mode
        If GridView1.Visible = True Then
            'Change Mode to Adition
            ChangeMode("U")
            'Update txtcontrol
            txtCountry.Text = GridView1.Rows(GridView1.SelectedIndex).Cells(1).Text
            txtFIPSCD.Text = GridView1.Rows(GridView1.SelectedIndex).Cells(2).Text
            txtNewCountry.Text = txtCountry.Text
        Else
            'JRS 2008/08/19 ADDED FIPS_CODE
            'Calls the function to Delete a country
            UpdCountry(txtCountry.Text, txtNewCountry.Text, txtFIPSCD.Text)
        End If

    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        'if it is in mode detail change the mode
        If GridView1.Visible = True Then
            'Change Mode to Adition
            ChangeMode("D")
            'Update txtcontrol
            txtCountry.Text = GridView1.Rows(GridView1.SelectedIndex).Cells(1).Text
            txtFIPSCD.Text = GridView1.Rows(GridView1.SelectedIndex).Cells(2).Text
        Else
            'Calls the function to Delete a country
            DelCountry(txtCountry.Text)
        End If

    End Sub

    Private Sub frm_AddDelRawD_Load(sender As Object, e As EventArgs) Handles Me.Load
        cmdAdd.Attributes.Add("onClick", "return addProcessMsg();")
        cmdUpdate.Attributes.Add("onClick", "return updProcessMsg();")
        cmdDelete.Attributes.Add("onClick", "return delProcessMsg();")
        If Not Page.IsPostBack Then
            Timer1.Visible = False
            'Fill the grid with data
            Inquiry()
            'Set the flag for the copy msg to false
            Session("NeedFileWrite") = False
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        'If it is in mode Detail Exit the form
        If GridView1.Visible = True Then
            Form_Unload()
            'If not go to detail mode
            Support.FormManager.Show(Me, "frm_MainMenu.aspx")
        Else
            ChangeMode("G")
        End If

    End Sub

    Private Sub Form_Unload()
        '-------------------------------------------------------------------
        'jrs 2013/07/01
        If Session("NeedFileWrite") Then
            Dim Msg$ = "IMPORTANT WARNING:  You must copy IFs.MDB, IFsHistSeries.MDB, "
            Msg$ = Msg$ + "IFsCoVaTra.MDB, IFsCoVaTraSeries.MDB and IFsWVSCohort.MDB from "
            Msg$ = Msg$ + "the local drive ('Runfiles' directory) to the Data directory "
            Msg$ = Msg$ + "after exiting IFs.  "
            Msg$ = Msg$ + "You may need help and/or permissions to do so.  "
            Msg$ = Msg$ + "Failure to do so will cause you to lose changes in "
            Msg$ = Msg$ + "Countries upon restarting IFs."
            'jrs 2008/10/29
            'Msg$ = Msg$ + Chr(vbKeyReturn)
            Msg$ = Msg$ + "Do you want the system to copy these files for you?"
            'If MsgBox(Msg$, vbYesNo, "Add Process") = vbYes Then
            'Me.MousePointer = vbHourglass
            'Copy the files
            Dim InFile$ = Session("PutFdrvRun") + "Ifs.db"
            Dim OutFile$ = DataFileDir$ + "Ifs.db"
            FileCopy(InFile$, OutFile$)
            InFile$ = Session("PutFdrvRun") + "IfsHistSeries.db"
            OutFile$ = DataFileDir$ + "IfsHistSeries.db"
            FileCopyCheck(InFile$, OutFile$)
            InFile$ = Session("PutFdrvRun") + "IFsWVSCohort.db"
            OutFile$ = DataFileDir$ + "IFsWVSCohort.db"
            FileCopyCheck(InFile$, OutFile$)
            InFile$ = Session("PutFdrvRun") + "IFsCoVaTra.db"
            OutFile$ = DataFileDir$ + "IFsCoVaTra.db"
            FileCopyCheck(InFile$, OutFile$)
            InFile$ = Session("PutFdrvRun") + "IFsCoVaTraSeries.db"
            OutFile$ = DataFileDir$ + "IFsCoVaTraSeries.db"
            FileCopyCheck(InFile$, OutFile$)
            'Me.MousePointer = vbNormal
            'End If
        End If
        'jrs 2013/07/01
        'daoDB36.Close
    End Sub

    Private Sub frm_AddDelRawD_Init(sender As Object, e As EventArgs) Handles Me.Init
        InitializeComponent()
    End Sub
End Class
