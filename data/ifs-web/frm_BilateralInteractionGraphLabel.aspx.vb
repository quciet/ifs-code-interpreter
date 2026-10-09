Imports System.Data.SQLite

Partial Class frm_BilateralInteractionGraphLabel
    Inherits Page

#Region " Web Form Designer Generated Code "
    'JAM 2019-12-26
    Dim vSQLiteCon As SqliteConnection
    Dim vSQLiteCommand As SqliteCommand
    Dim vSQLiteReader As SqliteDataReader
    Dim vConnString As String
    'Dim TotalGraphs As Integer
    'jrs 2009/07/02

    'Dim totYears As Integer
    'Dim bComparisonOn As Boolean


    'Private forCopyValue As String
    'Public Property ForCopy() As String
    '    Get
    '        Return forCopyValue
    '    End Get
    '    Set(ByVal value As String)
    '        forCopyValue = value
    '    End Set
    'End Property
    'Dim Session("CompBackup")(30, 30) As Integer
    'Dim CompLabel(30, 30) As Integer

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        'jrs 2005/04/22
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
        End If
        Me.txtVarName.Attributes.Add("onkeyup", "return clickButton(event,'" + cmdRenameFormula.ClientID + "')")
        Me.txtDim1.Attributes.Add("onkeydown", "return clickButton1(event,'" + cmdRename1.ClientID + "')")
        Me.txtDim1p.Attributes.Add("onkeydown", "return clickButton1p(event,'" + cmdRename1p.ClientID + "')")
    End Sub


#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            LoadDims()
            Session("iDim") = 0
            Session("iStartDim") = -1
            'AddHandler lstSeries.SelectedIndexChanged, AddressOf lstSeries_SelectedIndexChanged
            'AddHandler lstDim1.SelectedIndexChanged, AddressOf lstDim1_SelectedIndexChanged
            'txtVarName.Visible = True
            'txtDim1.Visible = False
            'txtDim1p.Visible = False
        End If
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Public Sub LoadDims()
        lstSeries.Items.Clear()
        lstDim1.Items.Clear()
        lstDim1p.Items.Clear()
        Dim i As Integer
        For i = 1 To Session("iCOUNTE")
            If Session("mFLegends")(5, i) > "" Then
                lstSeries.Items.Add(Session("mFLegends")(4, i) & " - " & Session("mFLegends")(5, i))
            Else
                lstSeries.Items.Add(Session("mFLegends")(4, i))
            End If
            lstDim1.Items.Add(Session("mFLegends")(1, i))
            lstDim1p.Items.Add(Session("mFLegends")(2, i))
        Next i

        If Session("iCOUNTE") > 0 Then
            lstSeries.SelectedIndex = 0
            txtVarName.Text = lstSeries.Items(0).Text
            lstDim1.SelectedIndex = 0
            txtDim1.Text = lstDim1.Items(0).Text
            lstDim1p.SelectedIndex = 0
            txtDim1p.Text = lstDim1p.Items(0).Text
            Session("iStartDim") = -1
        End If
        'txtVarName.Visible = False
    End Sub
    Private Sub LoadDim1sLabel(ByRef lst As ListBox)

    End Sub

    Private Sub lstSeries_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles lstSeries.SelectedIndexChanged
        If Session("iStartDim") = -1 Then
            Session("iStartDim") = 0
        Else
            Return
        End If
        'txtDim1.Visible = False
        'txtVarName.Visible = True
        'RemoveHandler lstDim1.SelectedIndexChanged, AddressOf lstDim1_SelectedIndexChanged
        txtVarName.Text = lstSeries.Items(lstSeries.SelectedIndex).Text
        lstDim1.SelectedIndex = lstSeries.SelectedIndex
        lstDim1p.SelectedIndex = lstSeries.SelectedIndex
        Session("iDim") = 0
        Session("iStartDim") = -1
        'AddHandler lstDim1.SelectedIndexChanged, AddressOf lstDim1_SelectedIndexChanged
    End Sub
    Private Sub lstDim1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles lstDim1.SelectedIndexChanged
        If Session("iStartDim") = -1 Then
            Session("iStartDim") = 1
        Else
            Return
        End If
        txtVarName.Visible = False
        txtDim1.Visible = True
        'RemoveHandler lstSeries.SelectedIndexChanged, AddressOf lstSeries_SelectedIndexChanged
        txtVarName.Text = lstDim1.Items(lstDim1.SelectedIndex).Text

        txtDim1.Text = lstDim1.Items(lstDim1.SelectedIndex).Text
        'lstSeries.SelectedIndex = lstDim1.SelectedIndex
        'lstDim1p.SelectedIndex = lstDim1.SelectedIndex
        Session("iDim") = 1
        Session("iStartDim") = -1
        'AddHandler lstSeries.SelectedIndexChanged, AddressOf lstSeries_SelectedIndexChanged
    End Sub
    Private Sub lstDim1p_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)  'Handles lstDim1p.SelectedIndexChanged
        'If Session("iStartDim") = -1 Then
        '    Session("iStartDim") = 2
        'Else
        '    Return
        'End If
        'txtVarName.Text = lstDim1p.Items(lstDim1p.SelectedIndex).Text
        'lstSeries.SelectedIndex = lstDim1p.SelectedIndex
        'lstDim1.SelectedIndex = lstDim1p.SelectedIndex
        'Session("iDim") = 2
        'Session("iStartDim") = -1
    End Sub

    Protected Sub ShowAlert(message As String)
        Dim sb As New System.Text.StringBuilder()
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("');")
        ClientScript.RegisterOnSubmitStatement(Me.GetType(), "alert", sb.ToString())
    End Sub
    Protected Sub ShowConfirm(message As String)
        Dim confirmValue As String = Request.Form("confirm_value")
        Dim sb As New System.Text.StringBuilder()
        sb.Append("return confirm('")
        sb.Append(message)
        sb.Append("');")
        ClientScript.RegisterOnSubmitStatement(Me.GetType(), "confirm", sb.ToString)
    End Sub
    Protected Sub OnConfirm(sender As Object, e As EventArgs)
        Dim message As String = "Your Request is being processed."
        Dim sb As New System.Text.StringBuilder()
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("');")
        ClientScript.RegisterOnSubmitStatement(Me.GetType(), "alert", sb.ToString())
    End Sub
    Private Sub cmdRenameFormula_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRenameFormula.Click, btnOKV.Click
        If lstSeries.SelectedIndex >= 0 Then
            If txtVarName.Text.Trim = "" Then
                ShowAlert("Label cannot be blank.")
                Return
            End If
            txtVarName.Text = txtVarName.Text.Trim
            Dim pos As Integer
            pos = InStr(txtVarName.Text, " - ")
            If pos > 0 Then
                setmFLegends(4, lstSeries.SelectedIndex + 1, Mid(1, txtVarName.Text, pos))
                setmFLegends(5, lstSeries.SelectedIndex + 1, Mid(pos + 1, txtVarName.Text))
            Else
                setmFLegends(4, lstSeries.SelectedIndex + 1, txtVarName.Text)
                setmFLegends(5, lstSeries.SelectedIndex + 1, "")
            End If
            lstSeries.Items(lstSeries.SelectedIndex).Text = txtVarName.Text

            'zz??
            'If gThirdDimmOk Then 'JAM 2015-10-25 Adding code for label changed
            '    LabelChanged = True
            'End If
            'txtVarName.Text = ""
        End If
    End Sub
    Private Sub cmdRename1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRename1.Click, btnOK1.Click
        Debug.WriteLine("zz1-0")
        If lstDim1.SelectedIndex >= 0 Then
            If txtDim1.Text.Trim = "" Then
                ShowAlert("Label cannot be blank.")
                Return
            End If
            txtDim1.Text = txtDim1.Text.Trim
            setmFLegends(1, lstDim1.SelectedIndex + 1, txtDim1.Text)
            lstDim1.Items(lstDim1.SelectedIndex).Text = txtDim1.Text
            Debug.WriteLine("zz1")
            'zz??
            'If gThirdDimmOk Then 'JAM 2015-10-25 Adding code for label changed
            '    LabelChanged = True
            'End If
            'txtDim1.Text = ""
        End If
    End Sub
    Private Sub cmdRename1p_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRename1p.Click, btnOK1p.Click
        Debug.WriteLine("zz1p-0")
        If lstDim1p.SelectedIndex >= 0 Then
            If txtDim1p.Text.Trim = "" Then
                ShowAlert("Label cannot be blank.")
                Return
            End If
            txtDim1p.Text = txtDim1p.Text.Trim
            setmFLegends(2, lstDim1p.SelectedIndex + 1, txtDim1p.Text)
            lstDim1p.Items(lstDim1p.SelectedIndex).Text = txtDim1p.Text
            Debug.WriteLine("zz1p")
            'zz??
            'If gThirdDimmOk Then 'JAM 2015-10-25 Adding code for label changed
            '    LabelChanged = True
            'End If
            'txtDim1p.Text = ""
        End If
    End Sub
    Public Sub setmFLegends(i As Integer, j As Integer, sVal As String)
        Session("mFLegends")(i, j) = sVal
    End Sub
    Public Sub MoveForw()
        Dim nu As Integer = Val(txtRowIndex.Text) + 1
        If nu > lstSeries.Items.Count - 1 Then Return
        txtRowIndex.Text = nu
        lstSeries.SelectedIndex = nu
        lstDim1.SelectedIndex = nu
        lstDim1p.SelectedIndex = nu
        txtVarName.Text = lstSeries.Items(nu).Text
        txtDim1.Text = lstDim1.Items(nu).Text
        txtDim1p.Text = lstDim1p.Items(nu).Text
    End Sub
    Public Sub MoveBack()
        Dim nu As Integer = Val(txtRowIndex.Text) - 1
        If nu < 0 Then Return
        txtRowIndex.Text = nu
        lstSeries.SelectedIndex = nu
        lstDim1.SelectedIndex = nu
        lstDim1p.SelectedIndex = nu
        txtVarName.Text = lstSeries.Items(nu).Text
        txtDim1.Text = lstDim1.Items(nu).Text
        txtDim1p.Text = lstDim1p.Items(nu).Text
    End Sub
    Public Sub PageBack()
        Support.FormManager.Show(Me, "frm_BilateralInteractionDisplay.aspx")
    End Sub
End Class


