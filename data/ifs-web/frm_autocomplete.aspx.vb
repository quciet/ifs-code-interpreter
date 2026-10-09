Option Strict Off
Option Explicit On
Option Compare Text

'------------------------------------------autocomplete.Frm-------------------------------------
'------------------------------------------------------------------------------------------
'
'
'   Copyright by Barry B. Hughes 1988-2015
'   Model code available for review and education only
'   Reproduction or incorporation in other software by permission only

Partial Class frm_autocomplete
    Inherits Page
#Region " Web Form Designer Generated Code "
    Public Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init




    End Sub
#End Region

    Dim valtbl As DataTable



    Public Sub mnuExit_Click()
        Response.Redirect("frm_MainMenu.aspx")

    End Sub

    Public Sub frm_autocomplete_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True


        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "mnuContinue"


    End Sub



    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption

            Case "Continue"
                mnuExit_Click()
        End Select
    End Sub




    Protected Sub cmdGeneratejs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGeneratejs.Click
        GenerateICT(Session("PutFdrvRun"))
    End Sub

    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''                 JAM 2015-05-07 Building ICT Array
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="gPutFdrvRun"></param>
    ''' <remarks></remarks>
    Public Sub GenerateICT(ByVal gPutFdrvRun As String)

        Dim Rcnt As Short
        Dim Varname As String
        Dim VarDefinition As String

        Dim sJscript As String

        Dim db As String

        Dim idx As Integer
        Dim sql As String
        'Dim Country As String




        ' ''JAM 2013-08-25 For other filter recovering
        ''vFilter = Session("ScatVarFiltName")



        'db = Session("PutFdrvRun") & "DataDict.MDB"
        'JAM 2014-07-20
        '''''''''''''''''Recovering Variables
        db = gPutFdrvRun & "IFSVAR.mdb"
        sql = "SELECT NAME, DEFINITION FROM IFSVAR WHERE TYPE=1 AND NAME LIKE 'ICT%' ORDER BY DEFINITION"
        SetDataTablefromAccess(db, sql, "", valtbl)


        sJscript = "   var VarListvse = [];" & vbCr

        sJscript = sJscript & "   var VarRegions = [];" & vbCr

        sJscript = sJscript & "   var NumRegions = [];" & vbCr

        idx = 0
        For Rcnt = 0 To valtbl.Rows.Count - 1
            Varname = valtbl.Rows(Rcnt).Item("DEFINITION")

            VarDefinition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")


            VarDefinition = Replace(Replace(Replace(VarDefinition, Chr(10), ""), Chr(13), ""), """", "")


            sJscript = sJscript & "  VarListvse[" & idx.ToString & "] = """ & VarDefinition & """;" & vbCr



            idx = idx + 1

        Next Rcnt

        ''''''JAM 2015-05-14 REcovering Countries
        db = gPutFdrvRun & "IFS.mdb"
        sql = "SELECT * FROM Economic ORDER BY country"
        SetDataTablefromAccess(db, sql, "", valtbl)
        idx = 0
        For Rcnt = 0 To valtbl.Rows.Count - 1
            Varname = valtbl.Rows(Rcnt).Item("Country")
            sJscript = sJscript & "  VarRegions[" & idx.ToString & "] = """ & Varname & """;" & vbCr

            idx = idx + 1
        Next Rcnt

        sJscript = sJscript & "     NumRegions = " & valtbl.Rows.Count.ToString & ";" & vbCr

        '''''''''''''''''''''''''''''''''''''''


        Dim intfile As Short, sFileName As String
        intfile = FreeFile()
        sFileName = PutFdrv & "IFsWeb\Scripts\"

        sFileName = sFileName & "autocomplete_vars.js"


        FileOpen(intfile, sFileName, OpenMode.Output)
        PrintLine(intfile, sJscript)
        FileClose(intfile)

    End Sub

    Public Sub CreateJSforFlexDisplays(ByVal ActiveBase As String, ByVal PutFdrvRun As String, ByRef CategoryIds As Collection)

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
End Class
