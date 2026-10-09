Partial Class frm_ApplyGListComp
    Inherits Page

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

    Dim idx As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            idx = Session("VarSelGridCol") + 1
            Session("AplyGLidx") = idx
            LoadGList()
        Else
            idx = Session("AplyGLidx")
        End If
    End Sub

    Private Sub LoadGList()
        Dim db As String
        Dim valtbl As DataTable
        db = Session("IFs_MDB2Active")
        SetDataTablefromAccess(db, "SELECT * FROM GListId ORDER BY Description", "", valtbl)
        Dim idx As Integer = 0
        While Not valtbl.Rows.Count = idx
            lstGList.Items.Add(valtbl.Rows(idx).Item("Description"))
            idx = idx + 1
        End While
        lstGList.SelectedIndex = 0
    End Sub

    Private Sub cmdContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdContinue.Click
        LoadGListComp(lstGList.SelectedValue)
        Support.FormManager.Close(Me)
    End Sub


    Private Sub LoadGListComp(ByVal sGList As String)
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
            Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
            Session("PutFdrvRun"), Session("CurrencySym"))

        Dim dbGL As String
        dbGL = Session("IFs_MDB2Active")
        Dim rsGL As DataTable
        SetDataTablefromAccess(dbGL, "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = """ & sGList & """", "", rsGL)
        Dim idxGL As Integer = 0
        Dim file As Integer
        If Session("DefaultFile") > 0 Then
            file = Session("DefaultFile")
        Else
            file = 0
        End If

        Dim oldCounte As Integer
        oldCounte = Session("COUNTE")

        Do While Not rsGL.Rows.Count = idxGL
            'jrs 2006/01/19
            If Session("aFormulas")(idx).Formula2 > "" Then
                Session("VarSelCheck") = "mnuVarSelOptShowVarPar"
                Dim h As Integer, ly As Integer, h2 As Integer, fn As Integer
                ly = Session("LastYear")
                fn = Session("FileNum")
                Session("FirstYear") = FirstYearHistory
                'load plot for the history part
                gLoadCompGList(idx, Session("aFormulas")(idx).Formula2, Session("aFormulas")(idx).Name,
                    rsGL.Rows(idxGL).Item("Region"), "", "", Session("aFormulas")(idx).Unit, file, True, Session("bFillHis"),
                    Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")), Session("aFormulas"), Session("COUNTE"),
                    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("bComputation"),
                    Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("VARN"),
                    Session("FileforDisplay"), IfsPars, Session("PLOT"), Session("bFillHisOnlyHoles"),
                    Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("StaYearHis")(Session("COUNTE")), Session("mnuSQLite"))
                'jrs 2005/12/19 RAM for Formulas
                Dim VarHis As Vector
                VarHis = Session("aFormulas")(Session("COUNTE")).disVar
                'Save Plot
                h = LastYearHistory - FirstYearHistory + 1
                Dim PlotCopy()
                ReDim PlotCopy(h)
                Dim j As Integer
                For j = 1 To h
                    PlotCopy(j) = Session("PLOT")(j, Session("COUNTE"))
                Next j
                Session("COUNTE") = Session("COUNTE") - 1
                Session("FirstYear") = FirstYearForecast
                Session("LastYear") = ly
                Session("FileNum") = fn

                Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - Session("FirstYear") + 1
                'load plot for the forecast part
                gLoadCompGList(idx, Session("aFormulas")(idx).Formula, Session("aFormulas")(idx).Name,
                    rsGL.Rows(idxGL).Item("Region"), Session("aFormulas")(idx).Dimension2, Session("aFormulas")(idx).Dimension3,
                    Session("aFormulas")(idx).Unit, file, False, Session("bFillHis"), Session("buseAllHis"),
                    Session("FinYearHis")(Session("COUNTE")), Session("aFormulas"), Session("COUNTE"), Session("FirstYear"),
                    Session("LastYear"), Session("LastYearForecast"), Session("bComputation"), Session("bLoadForDisplaySuccess"), Me, Session("FinYear"),
                    Session("VARN"), Session("FileforDisplay"), IfsPars, Session("PLOT"), Session("bFillHisOnlyHoles"),
                    Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("StaYearHis")(Session("COUNTE")), Session("mnuSQLite"))
                h2 = Session("LastYear") - FirstYearForecast + 1
                'Reload Forecast part
                For j = h2 To 1 Step -1
                    Session("PLOT")(j + h - 1, Session("COUNTE")) = Session("PLOT")(j, Session("COUNTE"))
                Next j
                'RELOAD HISTORY
                For j = 1 To h - 1
                    Session("PLOT")(j, Session("COUNTE")) = PlotCopy(j)
                Next j
                Session("FirstYear") = FirstYearHistory
                Session("FinYear")(Session("COUNTE")) = Session("LastYear") - Session("FirstYear") + 1
                Session("aFormulas")(Session("COUNTE")).Formula2 = Session("aFormulas")(idx).Formula2
                Session("aFormulas")(Session("COUNTE")).InitializeVarsHis(VarHis) 'jrs 2005/12/18 RAM for Formulas

            Else
                gLoadCompGList(idx, Session("aFormulas")(idx).Formula_Renamed, Session("aFormulas")(idx).Name,
                    rsGL.Rows(idxGL).Item("Region"), Session("aFormulas")(idx).Dimension2, Session("aFormulas")(idx).Dimension3,
                    Session("aFormulas")(idx).Unit, file, False, Session("bFillHis"), Session("buseAllHis"),
                    Session("FinYearHis")(Session("COUNTE")), Session("aFormulas"), Session("COUNTE"),
                    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("bComputation"),
                    Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("VARN"), Session("FileforDisplay"),
                    IfsPars, Session("PLOT"), Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"),
                    Session("StaYearHis")(Session("COUNTE")), Session("mnuSQLite"))
            End If
            idxGL = idxGL + 1

            FormatFormAfterLoad(file, Session("VARN"), Session("COUNTE"), Session("aFormulas")(Session("COUNTE")),
            IfsPars.vShortSce, Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("buseAllHis"),
            Session("MaxFinYearHis"))

        Loop

        Dim i As Integer
        'jrs 2005/12/19 RAM for Formulas
        'Dim dbV As String
        'dbV = Session("PutFdrvRun") + "IFSVAR.MDB"

        For i = oldCounte To 1 Step -1
            DeleteVar(i)
        Next i
        Support.FormManager.Show(Me, "frm_VarSel.aspx")
    End Sub

    'jrs 2006/06/26
    Public Sub DeleteVar(ByVal curr_col As Short)
        Dim iloop As Short
        Dim ICount As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        ' Delete Variable
        Session("COUNTE") = Session("COUNTE") - 1 ''Do later?
        Dim i2 As Integer
        For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
            For ICount = curr_col To Session("COUNTE")
                Session("PLOT")(i2, ICount) = Session("PLOT")(i2, ICount + 1)
            Next ICount
            Session("PLOT")(i2, Session("COUNTE") + 1) = 0
        Next i2

        'JRS 9/24/01 COMPUTATIONS
        Dim j1 As Object
        Dim uaf As Short
        uaf = UBound(Session("aFormulas")) - 1
        If Session("COUNTE") + 1 < uaf Then uaf = Session("COUNTE") + 1
        For j1 = curr_col To uaf  'jrs 2005/08/18 remove + 1 from curr_col
            'JRS 04/15/03
            If Not (Session("aFormulas")(j1 + 1) Is Nothing) Then
                Session("aFormulas")(j1) = Session("aFormulas")(j1 + 1)
            End If
        Next j1

        ' Take care of Grid Display & Array Variables Shifting
        For iloop = curr_col To Session("COUNTE") Step 1
            Session("FileNumbr")(iloop) = Session("FileNumbr")(iloop + 1)
            Session("VARN")(0, iloop) = Session("VARN")(0, iloop + 1)
            Session("VARN")(1, iloop) = Session("VARN")(1, iloop + 1)
            Session("VARN")(2, iloop) = Session("VARN")(2, iloop + 1)
            Session("VARN")(3, iloop) = Session("VARN")(3, iloop + 1)
            Session("VARN")(6, iloop) = Session("VARN")(6, iloop + 1)
            'mti Validation changes
            Session("VARN")(4, iloop) = Session("VARN")(4, iloop + 1)
            'mti Validation changes
            'mti Validation changes 19 August, 2002
            Session("VARN")(5, iloop) = Session("VARN")(5, iloop + 1)
            'mti Validation changes 19 August, 2002

            Session("GR1N")(iloop) = Session("GR1N")(iloop + 1)
            Session("GR2N")(iloop) = Session("GR2N")(iloop + 1)
            Session("GR3N")(iloop) = Session("GR3N")(iloop + 1)
            Session("GR4N")(iloop) = Session("GR4N")(iloop + 1)

            'jrs 10/27/04 Flexible Dimension Names for Graphs
            Session("FDNG1")(iloop) = Session("FDNG1")(iloop + 1)
            Session("FDNG2")(iloop) = Session("FDNG2")(iloop + 1)
            Session("FDNG3")(iloop) = Session("FDNG3")(iloop + 1)
            Session("FDNG4")(iloop) = Session("FDNG4")(iloop + 1)

        Next iloop
        Session("VARN")(0, Session("COUNTE") + 1) = ""
        Session("VARN")(1, Session("COUNTE") + 1) = ""
        Session("VARN")(2, Session("COUNTE") + 1) = ""
        Session("VARN")(3, Session("COUNTE") + 1) = ""
        Session("VARN")(6, Session("COUNTE") + 1) = ""
        'mti Validation changes July 28, 2002
        Session("VARN")(4, Session("COUNTE") + 1) = ""
        'mti Validation changes July 28, 2002
        'mti Validation changes 19 August, 2002
        Session("VARN")(5, Session("COUNTE") + 1) = ""
        'mti Validation changes 19 August, 2002

        Session("GR1N")(Session("COUNTE") + 1) = ""
        Session("GR2N")(Session("COUNTE") + 1) = ""
        Session("GR3N")(Session("COUNTE") + 1) = ""
        Session("GR4N")(Session("COUNTE") + 1) = ""

        'jrs 10/27/04 Flexible Dimension Names for Graphs
        Session("FDNG1")(Session("COUNTE") + 1) = ""
        Session("FDNG2")(Session("COUNTE") + 1) = ""
        Session("FDNG3")(Session("COUNTE") + 1) = ""
        Session("FDNG4")(Session("COUNTE") + 1) = ""

    End Sub


End Class
