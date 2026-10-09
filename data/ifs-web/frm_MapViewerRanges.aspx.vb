Option Strict Off
Option Explicit On
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'
'      JAM 2010-01-30      Parameter form for Graph Labels Costumization
'
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Partial Class frm_MapViewerRanges
    Inherits Page

#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        ' InitializeComponent()
        On Error GoTo Page_Init_Error 'JAM 2017-10-30
        If Not Page.IsPostBack Then

            Range1Start.Text = Session("RangeLimitsStart")(1)
            Range2Start.Text = Session("RangeLimitsStart")(2)
            Range3Start.Text = Session("RangeLimitsStart")(3)
            Range4Start.Text = Session("RangeLimitsStart")(4)
            Range5Start.Text = Session("RangeLimitsStart")(5)

            Range1End.Text = Session("RangeLimitsEnd")(1)
            Range2End.Text = Session("RangeLimitsEnd")(2)
            Range3End.Text = Session("RangeLimitsEnd")(3)
            Range4End.Text = Session("RangeLimitsEnd")(4)
            Range5End.Text = Session("RangeLimitsEnd")(5)


            'JAM 2017-10-29
            If Session("MapNoCat") > 5 Then


                Range6Start.Visible = True
                Range6End.Visible = True
                Category6.Visible = True

                Range6Start.Text = Session("RangeLimitsStart")(6)
                Range6End.Text = Session("RangeLimitsEnd")(6)

                If Session("MapNoCat") > 6 Then
                    'JAM 2017-11-04
                    Range7End.Enabled = False


                    Range7Start.Visible = True
                    Range7End.Visible = True
                    Category7.Visible = True

                    Range7Start.Text = Session("RangeLimitsStart")(7)
                    Range7End.Text = Session("RangeLimitsEnd")(7)
                Else
                    'JAM 2017-11-04
                    Range6End.Enabled = False

                    Category7.Visible = False
                    Range7Start.Visible = False
                    Range7End.Visible = False

                End If
            Else

                'JAM 2017-11-04
                Range5End.Enabled = False

                Range6Start.Visible = False
                Range6End.Visible = False
                Category6.Visible = False

                Range7Start.Visible = False
                Range7End.Visible = False
                Category7.Visible = False

            End If




        End If

Page_Init_Error:
        Exit Sub
    End Sub
    'CONVERSION_NOTE: Method 'Dispose' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
    'Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
    'If Disposing Then 
    'If Not components Is Nothing Then 
    'components.Dispose()
    'End If 
    'End If 
    'MyBase.Dispose(Disposing)
    'End Sub  
    'CONVERSION_NOTE: Field 'components' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
    'Private components As System.ComponentModel.IContainer 
    'CONVERSION_NOTE: Class 'System.Windows.Forms.ToolTip' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public ToolTip1 As System.Windows.Forms.ToolTip 
    '<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    'End Sub
#End Region

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click

        'JAM 2017-10-30  It isn't necessary to change flag status because default is false value
        'Session("RangeChanged") = False

        frm_MapViewerRanges_Closed()
        'Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, Session("MapViewerRangeParent"))
        'Support.FormManager.Show(Me, Session("DisplayControlParent"))

    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''     JAM 2017-11-02 Setting text focus
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="eventSender"></param>
    ''' <param name="eventArgs"></param>
    ''' <remarks></remarks>
    Private Sub frm_MapViewerRanges_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Range1End.Attributes.Add("onfocus", "this.select();")
        Range1End.Focus()



    End Sub

    Private Sub frm_MapViewerRanges_Closed()

        'Range1Start.Text = Session("RangeLimitsStart")(1)
        'Range2Start.Text = Session("RangeLimitsStart")(2)
        'Range3Start.Text = Session("RangeLimitsStart")(3)
        'Range4Start.Text = Session("RangeLimitsStart")(4)
        'Range5Start.Text = Session("RangeLimitsStart")(5)

        'Range1End.Text = Session("RangeLimitsEnd")(1)
        'Range2End.Text = Session("RangeLimitsEnd")(2)
        'Range3End.Text = Session("RangeLimitsEnd")(3)
        'Range4End.Text = Session("RangeLimitsEnd")(4)
        'Range5End.Text = Session("RangeLimitsEnd")(5)

        'Session("RangeChanged") = False



    End Sub
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''   JAM 2017-10-29 A value limit is changed
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        On Error GoTo cmdSave_Error 'JAM 2017-10-30
        If (Range5End.Text <> Session("RangeLimitsEnd")(5)) Or (Range4End.Text <> Session("RangeLimitsEnd")(4)) Or (Range3End.Text <> Session("RangeLimitsEnd")(3)) Or (Range2End.Text <> Session("RangeLimitsEnd")(2)) Or (Range1End.Text <> Session("RangeLimitsEnd")(1)) Or (Range1Start.Text <> Session("RangeLimitsStart")(1)) Or (Range2Start.Text <> Session("RangeLimitsStart")(2)) Or (Range3Start.Text <> Session("RangeLimitsStart")(3)) Or (Range4Start.Text <> Session("RangeLimitsStart")(4)) Or (Range5Start.Text <> Session("RangeLimitsStart")(5)) Or (Range6Start.Text <> Session("RangeLimitsStart")(6)) Or (Range7Start.Text <> Session("RangeLimitsStart")(7)) Then
            Session("RangeChanged") = True
        Else
            Session("RangeChanged") = False
        End If

        If Not (Range1End.Text Is Nothing) Then
            If Session("RangeLimitsEnd")(1) <> Range1End.Text Then
                Session("RangeLimitsEnd")(1) = Range1End.Text
            End If
        End If

        If Not (Range2End.Text Is Nothing) Then
            If Session("RangeLimitsEnd")(2) <> Range2End.Text Then
                Session("RangeLimitsEnd")(2) = Range2End.Text
            End If
        End If

        If Not (Range3End.Text Is Nothing) Then
            If Session("RangeLimitsEnd")(3) <> Range3End.Text Then
                Session("RangeLimitsEnd")(3) = Range3End.Text
            End If
        End If

        If Not (Range4End.Text Is Nothing) Then
            If Session("RangeLimitsEnd")(4) <> Range4End.Text Then
                Session("RangeLimitsEnd")(4) = Range4End.Text
            End If
        End If

        If Not (Range5End.Text Is Nothing) Then
            If Session("RangeLimitsEnd")(5) <> Range5End.Text Then
                Session("RangeLimitsEnd")(5) = Range5End.Text
            End If
        End If


        If Not (Range1Start.Text Is Nothing) Then
            If Session("RangeLimitsStart")(1) <> Range1Start.Text Then
                Session("RangeLimitsStart")(1) = Range1Start.Text
            End If
        End If

        If Not (Range2Start.Text Is Nothing) Then
            If Session("RangeLimitsStart")(2) <> Range2Start.Text Then
                Session("RangeLimitsStart")(2) = Range2Start.Text
            End If
        End If

        If Not (Range3Start.Text Is Nothing) Then
            If Session("RangeLimitsStart")(3) <> Range3Start.Text Then
                Session("RangeLimitsStart")(3) = Range3Start.Text
            End If
        End If

        If Not (Range4Start.Text Is Nothing) Then
            If Session("RangeLimitsStart")(4) <> Range4Start.Text Then
                Session("RangeLimitsStart")(4) = Range4Start.Text
            End If
        End If

        If Not (Range5Start.Text Is Nothing) Then
            If Session("RangeLimitsStart")(5) <> Range5Start.Text Then
                Session("RangeLimitsStart")(5) = Range5Start.Text
            End If
        End If

        'JAM 2017-10-29
        If Session("MapNoCat") > 5 Then
            If Not (Range6Start.Text Is Nothing) Then
                If Session("RangeLimitsStart")(6) <> Range6Start.Text Then
                    Session("RangeLimitsStart")(6) = Range6Start.Text
                End If
            End If

            If Session("MapNoCat") > 6 Then
                If Session("RangeLimitsStart")(7) <> Range7Start.Text Then
                    Session("RangeLimitsStart")(7) = Range7Start.Text
                End If

            End If

        End If

        frm_MapViewerRanges_Closed()
        Support.FormManager.Show(Me, Session("MapViewerRangeParent"))

cmdSave_Error:
        Exit Sub

    End Sub

End Class
