'===========================================================================
' This file was modified as part of an ASP.NET 2.0 Web project conversion.
' The class name was changed and the class modified to inherit from the abstract base class 
' in file 'App_Code\Migrated\Stub_Index_aspx_vb.vb'.
' During runtime, this allows other classes in your web application to bind and access 
' the code-behind page using the abstract base class.
' The associated content page 'Index.aspx' was also modified to refer to the new class name.
' For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================
Imports System.IO


'Namespace IFsWeb


'Partial Class Index
Partial Class Migrated_Index

        Inherits Index

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Dim valtblInit As DataTable
            Dim sIP As String
            Dim DInf As New DirectoryInfo(Me.MapPathSecure(TemplateSourceDirectory))
            sIP = DInf.Parent.FullName & "\IFsInit.mdb"
            SetDataTablefromAccess(sIP, "SELECT * FROM IFsInit", "Variable", valtblInit)
            '   Determine model load
            '   --------------------

            If SeekValue(valtblInit, "BaseModelCoVaTra", "Value") Then ModelLoad = "LoadCoVaTrA"
            If SeekValue(valtblInit, "BaseModelHowHappy", "Value") Then ModelLoad = "LoadHowHappy"
            If SeekValue(valtblInit, "BaseModelProfessional", "Value") Then ModelLoad = "LoadProfessional"
            If SeekValue(valtblInit, "BaseModelSAG", "Value") Then ModelLoad = "LoadSAG"
            If SeekValue(valtblInit, "BaseModelStudent", "Value") Then ModelLoad = "LoadStudent"
            If SeekValue(valtblInit, "BaseModelTERRA", "Value") Then ModelLoad = "LoadTERRA"
            If SeekValue(valtblInit, "BaseModelFull", "Value") Then ModelLoad = "LoadFull"
            If SeekValue(valtblInit, "BaseModelRANDPardee", "Value") Then ModelLoad = "LoadRANDPardee"

            If Not Page.IsPostBack Then
                Dim loadtbl As DataTable
                SetDataTablefromAccess(sIP, "SELECT * FROM " & ModelLoad, "Variable", loadtbl)
                lblVersion.Text = SeekValue(loadtbl, "ModelVersion$", "Value")
                'jrs 2008/05/20
                'default last year
                Dim LstYr As Integer
                LstYr = SeekValue(valtblInit, "FirstYearForecast%", "Value")
                LstYr = LstYr + 20
                txtMaxTime.Text = CStr(LstYr)
            End If
        End Sub

        Private Sub cmdContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContinue.Click
            'jrs 2005/06/20
            Session("DisplayLastYear") = txtMaxTime.Text
            Session("LastYearForecast") = Session("LastYearForecast")
            Session("VersionName") = lstVer.SelectedValue
            Response.Redirect("Ifs.aspx")
        End Sub
    End Class

'End Namespace
