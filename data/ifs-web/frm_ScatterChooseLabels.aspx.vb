Imports System.Math

''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'
'     JAM 2017-11-11 Infrastructure Profile Form
'
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Partial Class frm_ScatterChooseLabels
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub



    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        'jrs 2005/11/28
        'jrs 2011/05/04 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim a As Integer
        Dim b As Integer
        Dim vrow As GridViewRow
        Dim cbPoints As CheckBox
        Dim cbLabels As CheckBox

        'Put user code to initialize the page here
        If Not Page.IsPostBack Then

            ' Session("TableDisplayParent") = "frm_ScatterChooseLabels.aspx"

            'JAM 2019-02-17
            ReDim Session("PivotShowPoints")(Session("TotCountries"))
            ReDim Session("PivotShowLabels")(Session("TotCountries"))

            For b = 1 To Session("TotCountries")
                Session("PivotShowLabels")(b) = Session("ShowLabels")(b)
            Next

            For b = 1 To Session("TotCountries")
                Session("PivotShowPoints")(b) = Session("ShowPoints")(b)
            Next

            'Load General Values
            LoadMatrixValues()

            a = 1
            'JAM 2019-02-10
            For Each vrow In GridView_Regions.Rows
                cbPoints = vrow.FindControl("chkPoints")
                cbLabels = vrow.FindControl("chkLabels")

                If Session("ShowPoints")(a) Then
                    'vrow.Cells(4).Text = "Checked"
                    cbPoints.Checked = True
                Else
                    'vrow.Cells(4).Text = "Unchecked"
                    cbPoints.Checked = False

                End If

                If Session("ShowLabels")(a) Then
                    'vrow.Cells(4).Text = "Checked"
                    cbLabels.Checked = True
                Else
                    'vrow.Cells(4).Text = "Unchecked"
                    cbLabels.Checked = False

                End If

                a = a + 1

            Next


        End If
        'jrs 09/29/04
        'jrs 2011/09/26
        'If Session("DisplayParent") = Nothing Then
        '    Session("DisplayParent") = "Scatter.aspx"
        'End If



    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e
        Dim abvar As Integer

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Switch"
                abvar = 1

        End Select
    End Sub


    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '             JAM 2018-10-07    Loading Matrix Values
    '
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Sub LoadMatrixValues()

        Dim i As Integer
        Dim sName As String

        Dim dbW As String, TblW As DataTable

        Dim tblWidx As Integer = 0

        'jrs 2005/05/13
        Dim curRow As Integer
        curRow = 1 'jrs 2013/05/13 1

        Dim dt As DataTable

        dt = New DataTable()

        dt.Columns.Add("Countries", GetType(String))
        dt.Columns.Add("XValue", GetType(Double))
        dt.Columns.Add("YValue", GetType(Double))
        dt.Columns.Add("Labels", GetType(Boolean))
        dt.Columns.Add("Points", GetType(Boolean))




        For i = 1 To Session("CCount")

            If Session("mnuCodesChecked") Then
                sName = Session("CVarCode")(i)
            Else
                sName = Session("CVarName")(i)

            End If


            dt.Rows.Add(sName, Session("CVarD")(i), Session("CVarI")(i), True, True)


        Next i


        GridView_Regions.DataSource = dt
        GridView_Regions.DataBind()



    End Sub


    Protected Sub CheckAllLabels_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckAllLabels.Click
        Dim a As Integer
        'JAM 2019-02-17
        For a = 1 To Session("TotCountries")
            Session("ShowLabels")(a) = True
        Next

        'JAM 2019-02-17
        Session("mnuNamesChecked") = True
        Session("mnuCodesChecked") = False
        Session("mnuNolabelsChecked") = False


        ToggleCheckState(True)
    End Sub

    Protected Sub UncheckAllLabels_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UncheckAllLabels.Click
        Dim a As Integer
        'JAM 2019-02-17
        For a = 1 To Session("TotCountries")
            Session("ShowLabels")(a) = False
        Next

        'JAM 2019-02-17
        Session("mnuNamesChecked") = False
        Session("mnuCodesChecked") = False
        Session("mnuNolabelsChecked") = True


        ToggleCheckState(False)
    End Sub

    Private Sub ToggleCheckState(ByVal checkState As Boolean)

        Dim vrow As GridViewRow
        Dim cb As CheckBox


        For Each vrow In GridView_Regions.Rows
            cb = vrow.FindControl("chkLabels")

            cb.Checked = checkState

        Next

    End Sub

    Protected Sub CheckAllPoints_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckAllPoints.Click

        'JAM 2019-02-17
        For a = 1 To Session("TotCountries")
            Session("ShowPoints")(a) = True
        Next

        ToggleCheckStatePoints(True)
    End Sub

    Protected Sub UncheckAllPoints_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UncheckAllPoints.Click

        'JAM 2019-02-17
        For a = 1 To Session("TotCountries")
            Session("ShowPoints")(a) = False
        Next

        ToggleCheckStatePoints(False)
    End Sub

    Private Sub ToggleCheckStatePoints(ByVal checkState As Boolean)

        Dim vrow As GridViewRow
        Dim cb As CheckBox


        For Each vrow In GridView_Regions.Rows
            cb = vrow.FindControl("chkPoints")

            cb.Checked = checkState

        Next

    End Sub



    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Dim vrow As GridViewRow
        Dim cbPoints As CheckBox
        Dim cbLabels As CheckBox
        Dim i As Integer
        Dim oneLabelChecked As Boolean

        i = 1
        oneLabelChecked = False
        For Each vrow In GridView_Regions.Rows
            cbPoints = vrow.FindControl("chkPoints")
            cbLabels = vrow.FindControl("chkLabels")

            If cbPoints.Checked Then
                Session("ShowPoints")(i) = True
            Else
                Session("ShowPoints")(i) = False

            End If

            If cbLabels.Checked Then
                Session("ShowLabels")(i) = True
                oneLabelChecked = True
            Else
                Session("ShowLabels")(i) = False
            End If
            i = i + 1
        Next

        'JAM 2019-02-17
        If oneLabelChecked Then
            Session("mnuNamesChecked") = True
            Session("mnuCodesChecked") = False
            Session("mnuNolabelsChecked") = False
        Else
            Session("mnuNamesChecked") = False
            Session("mnuCodesChecked") = False
            Session("mnuNolabelsChecked") = True
        End If

        Support.FormManager.Show(Me, Session("ChooseLabelsParent"))

        '            // StringBuilder object
        'StringBuilder str = new StringBuilder();

        '// Select the checkboxes from the GridView control
        'for (int i = 0; i < GridView1.Rows.Count; i++)
        '{
        '  GridViewRow row = GridView1.Rows[i];
        '  bool isChecked = ((CheckBox) row.FindControl("chkSelect")).Checked;

        '                If (isChecked) Then
        '  {
        '    // Column 2 is the name column
        '    str.Append(GridView1.Rows[i].Cells[2].Text);
        '  }
        '}

        '// prints out the result
        'Response.Write(str.ToString());



    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Dim b As Integer

        'JAM 2019.02.17
        For b = 1 To Session("TotCountries")
            Session("ShowLabels")(b) = Session("PivotShowLabels")(b)
        Next

        For b = 1 To Session("TotCountries")
            Session("ShowPoints")(b) = Session("PivotShowPoints")(b)
        Next

        Support.FormManager.Show(Me, Session("ChooseLabelsParent"))
    End Sub
End Class
