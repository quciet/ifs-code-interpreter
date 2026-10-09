<%@ Control Language="VB" Debug="True" %>
<script runat="server">

<%-------------- Private Internal Variables ------------------%>
Private _width As Integer = 150
Private _rows As Integer = 5
Private _visible as Boolean = True
Private _enabled as Boolean = True
<%------------------------------------------------------------%>

<%------------------ Public Method ---------------------------%>
Public Function ShowMembers() As String
  Dim sResult As String = "<b>Combo Box User Control</b>" _
    & "</p><b>Properties:</b><br />" _
    & "IsDropDownCombo (Boolean, default False)<br />" _
    & "CssClass (String)<br />" _
    & "DataSource (Object)<br />" _
    & "DataTextField (String)<br />" _
    & "DataTextFormatString (String)<br />" _
    & "Items (ListItemCollection, Read-only)<br />" _
    & "Rows (Integer, default 5)<br />" _
    & "SelectedIndex (Integer)<br />" _
    & "SelectedItem (ListItem, Read-only)<br />" _
    & "SelectedValue (String)<br />" _
    & "Width (Integer, default 150 px)"
  Return sResult
End Function
<%------------------------------------------------------------%>

<%-------------- Public Property Variables -------------------%>
Public IsDropDownCombo As Boolean = False
Public CssClass As String
Public DataSource As Object
Public DataTextField As String
Public DataTextFormatString As String
<%------------------------------------------------------------%>

<%------------ Property Accessor Declarations ----------------%>
Public Property Width As Integer
  Get
    Return _width
  End Get
  Set
    If value > 20 Then
      _width = value
      SetWidth()
    End If
  End Set
End Property

Public Property Visible As Boolean
  Get
    Return textbox.visible
  End Get
  Set
    _visible = value
    SetVisible()
  End Set
End Property

Public Property Enabled As Boolean
  Get
    Return textbox.enabled
  End Get
  Set
    _enabled = value
    Setenabled()
  End Set
End Property

Public Property Rows As Integer
  Get
    Return _rows
  End Get
  Set
    If value > 0 Then
      _rows = value
      SetRows()
    End If
  End Set
End Property

Public Property SelectedValue As String
  Get
    If IsDropDownCombo Then
      Return textbox2.Text
    Else
      Return textbox.Text
    End If
  End Get
  Set
    If IsDropDownCombo Then
      textbox2.Text = value
      dropbox.SelectedIndex = -1
      For Each oItem As ListItem In dropbox.Items
        If value.Length <= oItem.Text.Length Then
          If String.Compare(oItem.Text.Substring(0, value.Length), value, True) = 0 Then
            oItem.Selected = True
            Exit For
          End If
        End If
      Next
    Else
      textbox.Text = value
      listbox.SelectedIndex = -1
      For Each oItem As ListItem In listbox.Items
        If value.Length <= oItem.Text.Length Then
          If String.Compare(oItem.Text.Substring(0, value.Length), value, True) = 0 Then
            oItem.Selected = True
            Exit For
          End If
        End If
      Next
    End If
  End Set
End Property

Public ReadOnly Property Items As ListItemCollection
  Get
    If IsDropDownCombo Then
      Return dropbox.Items
    Else
      Return listbox.Items
    End If
  End Get
End Property

Public ReadOnly Property SelectedItem As ListItem
  Get
    If IsDropDownCombo Then
      If dropbox.SelectedIndex < 0 Then
        Return New ListItem(textbox2.Text, textbox2.Text)
      Else
        Return dropbox.SelectedItem
      End If
    Else
      If listbox.SelectedIndex < 0 Then
        Return New ListItem(textbox.Text, textbox.Text)
      Else
        Return listbox.SelectedItem
      End If
    End If
  End Get
End Property

Public Property SelectedIndex As Integer
  Get
    If IsDropDownCombo Then
      Return dropbox.SelectedIndex
    Else
      Return listbox.SelectedIndex
    End If
  End Get
  Set
    If IsDropDownCombo Then
      If (value > -1) And (value < dropbox.Items.Count) Then
        dropbox.SelectedIndex = value
        textbox2.Text = dropbox.Items(SelectedIndex).Text
      elseif value = -1 then
        textbox2.Text = ""
      End If
    Else
      If (value > -1) And (value < listbox.Items.Count) Then
        listbox.SelectedIndex = value
        textbox.Text = listbox.Items(SelectedIndex).Text
      elseif value = -1 then
        textbox.Text = ""
      End If
    End If
  End Set
End Property

<%------------------------------------------------------------%>

Sub Page_Load()

  Dim sCID As String = Me.UniqueID & "_"

  Dim sScript As String = vbCrlf _
  & "<script language='javascript'>" & vbCrlf _
  & "function selectList(sCtrlID, sListID, sTextID) {" & vbCrlf _
  & "  var list = document.getElementById(sCtrlID + sListID);" & vbCrlf _
  & "  var text = document.getElementById(sCtrlID + sTextID);" & vbCrlf _
  & "  var btn = document.getElementById('hButton');" & vbCrlf _
  & "  text.value = list.options[list.selectedIndex].text;" & vbCrlf _
  & "  if (sListID == 'dropbox') openList(sCtrlID);" & vbCrlf _
  & "  btn.click();" & vbCrlf _
  & "}" & vbCrlf _
  & "function scrollList(sCtrlID, sListID, sTextID) {" & vbCrlf _
  & "  var list = document.getElementById(sCtrlID + sListID);" & vbCrlf _
  & "  var text = document.getElementById(sCtrlID + sTextID);" & vbCrlf _
  & "  var btn = document.getElementById('hButton');" & vbCrlf _
  & "  var search = new String(text.value).toLowerCase();" & vbCrlf _
  & "  list.selectedIndex = -1;" & vbCrlf _
  & "  var items = list.options;" & vbCrlf _
  & "  var option = new String();" & vbCrlf _
  & "  for (i = 0; i < items.length; i++) {" & vbCrlf _
  & "    option = items[i].text.toLowerCase();" & vbCrlf _
  & "    if (option.substring(0, search.length) == search ) {" & vbCrlf _
  & "      list.selectedIndex = i;" & vbCrlf _
  & "      break;" & vbCrlf _
  & "    }" & vbCrlf _
  & "  }" & vbCrlf _
  & " if (window.event.keyCode == 13) btn.click();" & vbCrlf _
  & "}" & vbCrlf _
  & "function openList(sCtrlID) {" & vbCrlf _
  & "  var list = document.getElementById(sCtrlID + 'dropbox');" & vbCrlf _
  & "  var btnimg = document.getElementById(sCtrlID + 'dropbtn');" & vbCrlf _
  & "  if(list.style.display == 'none') {" & vbCrlf _
  & "    list.style.display = 'block';" & vbCrlf _
  & "    btnimg.src = document.getElementById(sCtrlID + 'imageup').src;" & vbCrlf _
  & "  }" & vbCrlf _
  & "  else {" & vbCrlf _
  & "    list.style.display = 'none';" & vbCrlf _
  & "    btnimg.src = document.getElementById(sCtrlID + 'imagedown').src;" & vbCrlf _
  & "  }" & vbCrlf _
  & "  return false;" & vbCrlf _
  & "}" & vbCrlf _
  & "<" & "/script>" & vbCrlf

  If Not Page.IsClientScriptBlockRegistered("AHHComboBox") Then
    Page.RegisterClientScriptBlock("AHHComboBox", sScript)
  End If

  If IsDropDownCombo = True Then
    pchDropDown.Visible = True
    If CssClass <> "" Then
      dropbox.CssClass = CssClass.ToString()
      textbox2.CssClass = CssClass.ToString()
    End If
    dropbox.Attributes.Add("onclick", "selectList('" & sCID & "', 'dropbox', 'textbox2')")
    textbox2.Attributes.Add("onkeyup", "scrollList('" & sCID & "', 'dropbox', 'textbox2')")
    dropbtn.Attributes.Add("onclick", "return openList('" & sCID & "')")
    dropbox.DataSource = DataSource
    dropbox.DataTextField = DataTextField
    dropbox.DataTextFormatString = DataTextFormatString
    dropbox.DataBind()
  Else
    pchStandard.Visible = True
    If CssClass <> "" Then
      listbox.CssClass = CssClass
      textbox.CssClass = CssClass
    End If
    listbox.Attributes.Add("onclick", "selectList('" & sCID & "', 'listbox', 'textbox')")
    textbox.Attributes.Add("onkeyup", "scrollList('" & sCID & "', 'listbox', 'textbox')")
    listbox.DataSource = DataSource
    listbox.DataTextField = DataTextField
    listbox.DataTextFormatString = DataTextFormatString
    listbox.DataBind()
  End If

  SetWidth()
  SetRows()

End Sub

Private Sub SetWidth()
  If IsDropDownCombo = True Then
    dropdiv.Style("width") = _width.ToString()
    textbox2.Style("width") = (_width - 17).ToString()
    dropbox.Style("width") = (_width - 20).ToString()
  Else
    textbox.Style("width") = _width.ToString()
    listbox.Style("width") = (_width).ToString()
  End If
End Sub

Private Sub SetVisible()
  If IsDropDownCombo = True Then
    dropdiv.visible = _visible
    textbox2.visible = _visible
    dropbox.visible = _visible
  Else
    textbox.visible = _visible
    listbox.visible = _visible
  End If
End Sub

Private Sub SetEnabled()
	textbox.enabled = _enabled
	listbox.enabled = _enabled
End Sub

Private Sub SetRows()
  If IsDropDownCombo = True Then
    dropbox.Rows = _rows
  Else
    listbox.Rows = _rows
  End If
End Sub

</script>
<%----------------- List-style Combo Box ---------------------%>
<asp:Placeholder id="pchStandard" visible="false" runat="server">
	<table border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td align="right">
				<asp:TextBox id="textbox" runat="server" /><br />
				<asp:ListBox id="listbox" runat="server" />
			</td>
		</tr>
	</table>
</asp:Placeholder>
<%------------------------------------------------------------%>
<%----------------- Drop-down Combo Box ----------------------%>
<asp:Placeholder id="pchDropDown" visible="false" runat="server">
	<div id="dropdiv" Style="position:relative" HorizontalAlign="Right" runat="server">
		<asp:TextBox Style="vertical-align:middle" id="textbox2" runat="server" /><asp:ImageButton id="dropbtn" BorderWidth="0" Width="16" Height="20" Style="vertical-align:middle"
			ImageUrl="~/images/click-down.gif" runat="server" /><br />
		<asp:ListBox Style="display:none;position:absolute;left:20;top:25" id="dropbox" runat="server" />
		<asp:Image id="imageup" ImageUrl="~/images/click-up.gif" Style="display:none" runat="server" />
		<asp:Image id="imagedown" ImageUrl="~/images/click-down.gif" Style="display:none" runat="server" />
	</div>
</asp:Placeholder>
<%------------------------------------------------------------%>
