Namespace Support
    Public Class FormManager
        Public Shared Manager As New FormHashTable()
        Public Shared LoadState As Boolean

        Public Shared Sub StateStore(ByVal form As System.Web.UI.Page)
            StateStore(form.Controls, form, True)
        End Sub

        Public Shared Sub StateStore(ByVal controls As System.Web.UI.ControlCollection, ByVal form As System.Web.UI.Page, ByVal isNested As Boolean)
            Dim i, j As Integer
            If isNested Then
                For i = 0 To controls.Count - 1
                    For j = 0 To controls(i).Controls.Count - 1
                        StateStore(controls(i).Controls(j), form)
                    Next j
                Next i
            Else
                For i = 0 To controls.Count - 1
                    StateStore(controls(i), form)
                Next i
            End If
        End Sub

        Public Shared Sub StateStore(ByVal acontrol As System.Web.UI.Control, ByVal form As System.Web.UI.Page)
            If acontrol.GetType Is GetType(System.Web.UI.WebControls.Button) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.Button = acontrol
                Dim PropertiesTable As Hashtable = New Hashtable()
                StoreControlProperty(control, PropertiesTable)
                ''Text, Textalign  
                StoreProperty("Text", control.Text, PropertiesTable)
                StoreProperty("TextAlign", control.Style.Item("TEXT-ALIGN"), PropertiesTable)
                Manager.SetControlValue(form.ToString() + control.ID, PropertiesTable)
            ElseIf acontrol.GetType Is GetType(System.Web.UI.WebControls.CheckBox) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.CheckBox = acontrol
                Dim PropertiesTable As Hashtable = New Hashtable()
                StoreControlProperty(control, PropertiesTable)
                ''Checked,Text,Textalign              
                StoreProperty("Checked", control.Checked, PropertiesTable)
                StoreProperty("Text", control.Text, PropertiesTable)
                StoreProperty("TextAlign", control.Style.Item("TEXT-ALIGN"), PropertiesTable)
                Manager.SetControlValue(form.ToString() + control.ID, PropertiesTable)
            ElseIf acontrol.GetType Is GetType(System.Web.UI.WebControls.Label) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.Label = acontrol
                Dim PropertiesTable As Hashtable = New Hashtable()
                StoreControlProperty(control, PropertiesTable)
                ''Borderstyle,Text,Textalign
                StoreProperty("BorderStyle", control.BorderStyle, PropertiesTable)
                StoreProperty("Text", control.Text, PropertiesTable)
                StoreProperty("TextAlign", control.Style.Item("TEXT-ALIGN"), PropertiesTable)
                Manager.SetControlValue(form.ToString() + control.ID, PropertiesTable)
            ElseIf acontrol.GetType Is GetType(System.Web.UI.WebControls.Panel) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.Panel = acontrol
                Dim PropertiesTable As Hashtable = New Hashtable()
                StoreControlProperty(control, PropertiesTable)
                'Borderstyle 
                StoreProperty("BorderStyle", control.BorderStyle, PropertiesTable)
                Manager.SetControlValue(form.ToString() + control.ID, PropertiesTable)
            ElseIf acontrol.GetType Is GetType(System.Web.UI.WebControls.Image) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.Image = acontrol
                Dim PropertiesTable As Hashtable = New Hashtable()
                StoreControlProperty(control, PropertiesTable)
                'Borderstyle, Image
                StoreProperty("ImageURL", control.ImageUrl, PropertiesTable)
                StoreProperty("BorderStyle", control.BorderStyle, PropertiesTable)
                Manager.SetControlValue(form.ToString() + control.ID, PropertiesTable)
            ElseIf acontrol.GetType Is GetType(System.Web.UI.WebControls.RadioButton) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.RadioButton = acontrol
                Dim PropertiesTable As Hashtable = New Hashtable()
                StoreControlProperty(control, PropertiesTable)
                'Checked,Text,Textalign                 
                StoreProperty("Checked", control.Checked, PropertiesTable)
                StoreProperty("Text", control.Text, PropertiesTable)
                StoreProperty("TextAlign", control.Style.Item("TEXT-ALIGN"), PropertiesTable)
                Manager.SetControlValue(form.ToString() + control.ID, PropertiesTable)
            ElseIf acontrol.GetType Is GetType(System.Web.UI.WebControls.TextBox) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.TextBox
                control = acontrol
                Dim PropertiesTable As Hashtable = New Hashtable()
                StoreControlProperty(control, PropertiesTable)
                'Borderstyle,Text,Textalign,Wordwrap, textMOde
                StoreProperty("BorderStyle", control.BorderStyle, PropertiesTable)
                StoreProperty("Text", control.Text, PropertiesTable)
                StoreProperty("TextAlign", control.Style.Item("TEXT-ALIGN"), PropertiesTable)
                StoreProperty("TextMode", control.TextMode, PropertiesTable)
                StoreProperty("Wrap", control.Wrap, PropertiesTable)
                Manager.SetControlValue(form.ToString() + control.ID, PropertiesTable)
            ElseIf acontrol.GetType Is GetType(System.Web.UI.WebControls.DropDownList) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.DropDownList
                control = acontrol
                Dim PropertiesTable As Hashtable = New Hashtable()
                StoreControlProperty(control, PropertiesTable)
                'Items,SelectedItem(see map for getselecteditem)
                StoreProperty("BorderStyle", control.BorderStyle, PropertiesTable)
                StoreProperty("Items", control.Items, PropertiesTable)
                StoreProperty("SelectedIndex", control.SelectedIndex, PropertiesTable)
                Manager.SetControlValue(form.ToString() + control.ID, PropertiesTable)
            ElseIf acontrol.GetType Is GetType(System.Web.UI.WebControls.ListBox) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.ListBox
                control = acontrol
                Dim PropertiesTable As Hashtable = New Hashtable()
                StoreControlProperty(control, PropertiesTable)
                'BorderStyle,Items,SlectedItem 
                StoreProperty("BorderStyle", control.BorderStyle, PropertiesTable)
                StoreProperty("Items", control.Items, PropertiesTable)
                StoreProperty("SelectedIndex", control.SelectedIndex, PropertiesTable)
                Manager.SetControlValue(form.ToString() + control.ID, PropertiesTable)
            End If
            StateStore(acontrol.Controls, form, False)
        End Sub

        Public Shared Sub StateRestore(ByRef form As System.Web.UI.Page)
            StateRestore(form.Controls, form, True)
        End Sub

        Public Shared Sub StateRestore(ByVal controls As System.Web.UI.ControlCollection, ByVal form As System.Web.UI.Page, ByVal isNested As Boolean)
            Dim i, j As Integer
            If isNested Then
                For i = 0 To controls.Count - 1
                    For j = 0 To controls(i).Controls.Count - 1
                        StateRestore(controls(i).Controls(j), form)
                    Next j
                Next i
            Else
                For j = 0 To controls.Count - 1
                    StateRestore(controls(j), form)
                Next j
            End If
        End Sub

        Public Shared Sub StateRestore(ByVal aControl As System.Web.UI.Control, ByVal form As System.Web.UI.Page)
            If aControl.GetType Is GetType(System.Web.UI.WebControls.Button) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.Button = aControl
                Dim PropertiesTable As Hashtable = Manager.GetControlValue(form.ToString() + control.ID)
                RestoreControlProperty(control, PropertiesTable)
                ''Text,Textalign       
                control.Text = RestoreProperty("Text", PropertiesTable)
                control.Style.Item("TEXT-ALIGN") = RestoreProperty("TextAlign", PropertiesTable)
            ElseIf aControl.GetType Is GetType(System.Web.UI.WebControls.CheckBox) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.CheckBox = aControl
                Dim PropertiesTable As Hashtable = Manager.GetControlValue(form.ToString() + control.ID)
                RestoreControlProperty(control, PropertiesTable)
                ''Checked,Text,Textalign
                control.Checked = RestoreProperty("Checked", PropertiesTable)
                control.Text = RestoreProperty("Text", PropertiesTable)
                control.Style.Item("TEXT-ALIGN") = RestoreProperty("TextAlign", PropertiesTable)
            ElseIf aControl.GetType Is GetType(System.Web.UI.WebControls.Label) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.Label = aControl
                Dim PropertiesTable As Hashtable = Manager.GetControlValue(form.ToString() + control.ID)
                RestoreControlProperty(control, PropertiesTable)
                'Borderstyle,Text,Textalign 
                control.BorderStyle = RestoreProperty("BorderStyle", PropertiesTable)
                control.Text = RestoreProperty("Text", PropertiesTable)
                control.Style.Item("TEXT-ALIGN") = RestoreProperty("TextAlign", PropertiesTable)
            ElseIf aControl.GetType Is GetType(System.Web.UI.WebControls.Panel) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.Panel = aControl
                Dim PropertiesTable As Hashtable = Manager.GetControlValue(form.ToString() + control.ID)
                RestoreControlProperty(control, PropertiesTable)
                'Borderstyle
                control.BorderStyle = RestoreProperty("BorderStyle", PropertiesTable)
            ElseIf aControl.GetType Is GetType(System.Web.UI.WebControls.Image) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.Image = aControl
                Dim PropertiesTable As Hashtable = Manager.GetControlValue(form.ToString() + control.ID)
                RestoreControlProperty(control, PropertiesTable)
                'Borderstyle,ImageURL 
                control.BorderStyle = RestoreProperty("BorderStyle", PropertiesTable)
                control.ImageUrl = RestoreProperty("ImageURL", PropertiesTable)
            ElseIf aControl.GetType Is GetType(System.Web.UI.WebControls.RadioButton) Then
                'Control Properties
                Dim control As System.Web.UI.WebControls.RadioButton = aControl
                Dim PropertiesTable As Hashtable = Manager.GetControlValue(form.ToString() + control.ID)
                RestoreControlProperty(control, PropertiesTable)
                'Checked,Text,style.item("TEXT-ALIGN") 
                control.Checked = RestoreProperty("Checked", PropertiesTable)
                control.Text = RestoreProperty("Text", PropertiesTable)
                control.Style.Item("TEXT-ALIGN") = RestoreProperty("TextAlign", PropertiesTable)
            ElseIf aControl.GetType Is GetType(System.Web.UI.WebControls.TextBox) Then
                'Control Properties
                Dim control As System.Web.UI.WebControls.TextBox = aControl
                Dim PropertiesTable As Hashtable = Manager.GetControlValue(form.ToString() + control.ID)
                RestoreControlProperty(control, PropertiesTable)
                'Borderstyle,Textmode,Text,style.item("TEXT-ALIGN"),Wrap 
                control.BorderStyle = RestoreProperty("BorderStyle", PropertiesTable)
                control.TextMode = RestoreProperty("TextMode", PropertiesTable)
                control.Text = RestoreProperty("Text", PropertiesTable)
                control.Style.Item("TEXT-ALIGN") = RestoreProperty("TextAlign", PropertiesTable)
                control.Wrap = RestoreProperty("Wrap", PropertiesTable)
            ElseIf aControl.GetType Is GetType(System.Web.UI.WebControls.DropDownList) Then
                'Control Properties
                Dim control As System.Web.UI.WebControls.DropDownList = aControl
                Dim PropertiesTable As Hashtable = Manager.GetControlValue(form.ToString() + control.ID())
                'jrs 2024/12/09
                If PropertiesTable Is Nothing Then Exit Sub
                RestoreControlProperty(control, PropertiesTable)
                'BorderStyle, Items,SelectedItem
                control.BorderStyle = RestoreProperty("BorderStyle", PropertiesTable)
                Dim list As ListItemCollection = RestoreProperty("Items", PropertiesTable)
                Dim index = 0
                control.Items.Clear()
                While (index < list.Count)
                    control.Items.Add(list.Item(index))
                    index = index + 1
                End While
                index = RestoreProperty("SelectedIndex", PropertiesTable)
                If (index >= 0) Then
                    control.Items(index).Selected = True ''??
                End If
            ElseIf aControl.GetType Is GetType(System.Web.UI.WebControls.ListBox) Then
                ''Control Properties
                Dim control As System.Web.UI.WebControls.ListBox = aControl
                Dim PropertiesTable As Hashtable = Manager.GetControlValue(form.ToString() + control.ID())
                RestoreControlProperty(control, PropertiesTable)
                'BorderStyle,Items,SlectedItem 
                control.BorderStyle = RestoreProperty("BorderStyle", PropertiesTable)
                Dim list As ListItemCollection = RestoreProperty("Items", PropertiesTable)
                Dim index = 0
                control.Items.Clear()
                While (index < list.Count)
                    control.Items.Add(list.Item(index))
                    index = index + 1
                End While
                index = RestoreProperty("SelectedIndex", PropertiesTable)
                If (index >= 0) Then
                    control.Items(index).Selected = True
                End If
            End If
            StateRestore(aControl.Controls, form, False)
        End Sub
        Private Shared Sub StoreControlProperty(ByVal Control As System.Web.UI.WebControls.WebControl, ByRef PropertiesTable As Hashtable)
            StoreProperty("BackColor", Control.BackColor, PropertiesTable)
            StoreProperty("Enabled", Control.Enabled, PropertiesTable)
            StoreProperty("Font", Control.Font, PropertiesTable)
            StoreProperty("ForeColor", Control.ForeColor, PropertiesTable)
            StoreProperty("Height", Control.Height, PropertiesTable)
            StoreProperty("TabIndex", Control.TabIndex, PropertiesTable)
            StoreProperty("Visible", Control.Visible, PropertiesTable)
            StoreProperty("Width", Control.Width, PropertiesTable)
            StoreProperty("Left", Control.Style.Item("LEFT"), PropertiesTable)
            StoreProperty("Top", Control.Style.Item("TOP"), PropertiesTable)
        End Sub

        Private Shared Sub RestoreControlProperty(ByVal Control As System.Web.UI.WebControls.WebControl, ByRef PropertiesTable As Hashtable)
            Control.BackColor = RestoreProperty("BackColor", PropertiesTable)
            Control.Enabled = RestoreProperty("Enabled", PropertiesTable)
            Control.ForeColor = RestoreProperty("ForeColor", PropertiesTable)
            Control.Height = RestoreProperty("Height", PropertiesTable)
            Control.TabIndex = RestoreProperty("TabIndex", PropertiesTable)
            Control.Visible = RestoreProperty("Visible", PropertiesTable)
            Control.Width = RestoreProperty("Width", PropertiesTable)
            Control.Style.Item("LEFT") = RestoreProperty("Left", PropertiesTable)
            Control.Style.Item("TOP") = RestoreProperty("Top", PropertiesTable)
            Dim ControlFont As FontInfo = RestoreProperty("Font", PropertiesTable)
            If Not ControlFont Is Nothing Then
                Control.Font.Bold = ControlFont.Bold
                Control.Font.Italic = ControlFont.Italic
                Control.Font.Overline = ControlFont.Overline
                Control.Font.Name = ControlFont.Name
                Control.Font.Size = ControlFont.Size
                Control.Font.Strikeout = ControlFont.Strikeout
                Control.Font.Underline = ControlFont.Underline
            End If
        End Sub

        Public Shared Function RestoreProperty(ByRef Prop As String, ByRef PropertiesTable As Hashtable) As Object
            If (PropertiesTable Is Nothing) OrElse (PropertiesTable(Prop) Is Nothing) Then
                Return Nothing
            Else
                Return PropertiesTable.Item(Prop)
            End If
        End Function

        Private Shared Sub StoreProperty(ByVal Prop As String, ByVal Value As Object, ByRef PropertiesTable As Hashtable)
            If (Not Value Is Nothing) AndAlso (Value.ToString() <> "") AndAlso Not (PropertiesTable Is Nothing) Then
                If (PropertiesTable.Contains(Prop)) Then
                    PropertiesTable.Item(Prop) = Value
                Else
                    PropertiesTable.Add(Prop, Value)
                End If
            End If
        End Sub

        Public Shared Sub Close(ByVal Page As System.Web.UI.Page)
            Dim namePage As String
            namePage = Manager.GetReturnURL(GetPageName(Page))
            LoadState = True
            If Not (namePage = "") Then
                Page.Response.Redirect(namePage)
            Else
                Page.Response.End()
            End If
        End Sub

        Public Shared Sub Show(ByVal Page As System.Web.UI.Page, ByVal PageURL As String)
            Manager.SetReturnURL(GetPageName(Page), PageURL)
            Page.Response.Redirect(PageURL)
        End Sub

        Public Shared Function GetPageName(ByRef Page As System.Web.UI.Page) As System.String
            Return (Page.Request.RawUrl).Substring((Page.Request.RawUrl).LastIndexOf("/") + 1)
        End Function

        Public Class FormHashTable
            Inherits System.Collections.Hashtable

            Public Sub SetReturnURL(ByVal URLCaller As String, ByVal URLCalling As String)
                If Not (Me(URLCalling) Is Nothing) Then
                    CType(Me(URLCalling), FormProperties).ReturnURL = URLCaller
                Else
                    Dim tempProps As New FormProperties()
                    tempProps.ReturnURL = URLCaller
                    Add(URLCalling, tempProps)
                End If
            End Sub

            Public Function GetReturnURL(ByRef URLCalling As String) As System.String
                If (Me(URLCalling) Is Nothing) Then
                    Return ""
                Else
                    Return CType(Me(URLCalling), FormProperties).ReturnURL
                End If
            End Function

            Public Sub SetControlValue(ByVal IDControl As String, ByVal Value As Hashtable)
                If (Manager.Contains(IDControl)) Then
                    Manager.Item(IDControl) = Value
                Else
                    Add(IDControl, Value)
                End If
            End Sub

            Public Function GetControlValue(ByVal IDControl As String) As Hashtable
                If (Me(IDControl) Is Nothing) Then
                    Return Nothing
                Else
                    Dim Control As Object = New Object()
                    Control = IDControl
                    Return Manager.Item(Control)
                End If
            End Function

            Class FormProperties
                Public ReturnURL As String = ""
            End Class
        End Class
    End Class
End Namespace
