Option Strict Off
Option Explicit On


'Namespace IFsWeb

Public Module StringHandler
'
	'------------------------------------------StringHandler.Bas-------------------------------------
	'------------------------------------------------------------------------------------------
	'
	 
	'
	'   Copyright by Barry B. Hughes 1988-2003
	'   Model code available for review and education only
	'   Reproduction or incorporation in other software by permission only
	'   Last changed bh BBH on October 9, 1999
	 
	'=============================StringHandler.BAS  =======================
	'
	'  General purpose string handling functions.
	'
	 
	'=============================StringHandler.BAS  =======================
	Sub AddStrList(ByRef List() As String, ByRef S As String)
		'- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		 
		'  Add the string "S" to the array "List"
		 
		'UPGRADE_WARNING: Lower bound of array List was changed from LBound(List) to 0. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1033"'
		ReDim Preserve List(UBound(List) + 1)
		List(UBound(List)) = S
		 
	End Sub 
	 
	'=============================StringHandler.BAS  =======================
	Sub AddStrListUnique(ByRef List() As String, ByRef S As String)
		'- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		 
		'   Add string "S" to list "List" only
		'   if it isn't there already.
		 
		If GetElementNum(List, S) =  - 32767 Then 
			Call AddStrList(List, S)
		End If 
		 
	End Sub 
	 
	'=============================StringHandler.BAS  =======================
	Function AfterFirstToken(ByRef StrIn As String, ByRef Delim As String) As String
		'- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		 
		'  Returns the portion of "String" after the
		'  first occurrence of "Delim".
		 
		Dim After As String
		Dim Begin As Integer
		 
		StrIn = Trim(StrIn)
		Begin = InStr(1, StrIn, Delim)
		If (Begin = 0) Or Begin = Len(StrIn) Then 
			After = ""
		Else 
			After = (Trim(Right(StrIn, Len(StrIn) - Begin)))
		End If 
		 
		AfterFirstToken = After
		 
		 
	End Function 
	 
	'=============================StringHandler.BAS  =======================
	Function GetArgs(ByRef InString As String) As String
		'- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		 
		'  Given a command string, pull off the first
		'  word and return just the arguments.
		 
		Dim ArgStr As String
		Dim Begin As String
		 
		InString = Trim(InString)
		Begin = CStr(InStr(1, InString, " "))
		If (CDbl(Begin) = 0) Or CDbl(Begin) = Len(InString) Then 
			ArgStr = ""
		Else 
			ArgStr = (Trim(Right(InString, Len(InString) - CDbl(Begin))))
		End If 
		 
		GetArgs = ArgStr
		 
	End Function 
	 
	'=============================StringHandler.BAS  =======================
	Function GetElementNum(ByRef List() As String, ByRef S As String) As Integer
		'- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		 
		'  find out where or if string "S" occurs in
		'  array "List", and return the index number.
		 
		Dim Place As Integer
		Dim i As Integer
		Place =  - 32767
		 
		For i = LBound(List) To UBound(List)
			If List(i) = S Then 
				Place = i
				Exit For 
			End If 
		Next i
		 
		GetElementNum = Place
		 
	End Function 
	 
	'=============================StringHandler.BAS  =======================
	Function GetFirstToken(ByRef StrIn As String, ByRef Delim As String) As String
		'- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		 
		' Gets the portion of String "S", up to the
		' first occurrance of delimiter "D"
		 
		' Returns String token "T"
		 
		'UPGRADE_NOTE: Split was upgraded to Split_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
		Dim Split_Renamed As Integer
		Dim Tok As String
		StrIn = Trim(StrIn)
		 
		Split_Renamed = InStr(1, StrIn, Delim)
		If (Split_Renamed <= 0) Then 
			 
			' No delimiter in the string. Return the whole thing.
			Tok = StrIn
		Else 
			 
			'  Get everything up to the first delimiter.
			 
			Tok = (Trim(Left(StrIn, Split_Renamed - 1)))
		End If 
		 
		GetFirstToken = Tok
		 
	End Function 
	 
	'=============================StringHandler.BAS  =======================
	Function GetFirstWord(ByRef InString As String) As String
		'- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		 
		'  Given a command string, return just the first
		'  word.
		 
		Dim CmdStr As String
		Dim CmdEnd As String
		InString = Trim(InString)
		CmdEnd = CStr(InStr(1, InString, " ") - 1)
		If (CDbl(CmdEnd) <= 0) Then 
			 
			' No spaces in the string.
			CmdStr = InString
		Else 
			 
			'  Get everything up to the first space.
			 
			CmdStr = (Trim(Left(InString, CInt(CmdEnd))))
		End If 
		 
		GetFirstWord = CmdStr
		 
	End Function 
	 
	'=============================StringHandler.BAS  =======================
	Sub RemoveStrList(ByRef List() As String, ByRef S As String)
		'- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		 
		'  Remove the entry "S" from an array of strings.
		 
		Dim i As Integer
		Dim Found As Integer
		 
		'  Run through the list.  Once S is found,
		'  shuffle all remaining elements up one position.
		 
		Found = False
		For i = 1 To UBound(List)
			If (List(i) = S) Or Found Then 
				Found = True
				If i < UBound(List) Then 
					List(i) = List(i + 1)
				End If 
			End If 
		Next i
		 
		'  Free memory used by S.
		 
		If Found Then 
			ReDim Preserve List(UBound(List) - 1)
		End If 
		 
	End Sub 
	 
	'JRSVBNET **** BEGIN ********
	''=============================StringHandler.BAS  =======================
	'Sub SelListByName(ByRef ctrl As 'JRSVBNET System.Windows.Forms.Control, ByRef fdrName As String)
	'	'- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	 
	'	'  Given a list box control and a string,
	'	'  set the matching entry in the control selected.
	 
	'	'UPGRADE_WARNING: Couldn't resolve default property of object ctrl.ListCount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
	'	Dim i As Integer
	'	If (ctrl.ListCount > 0) Then
	'		'UPGRADE_WARNING: Couldn't resolve default property of object ctrl.ListCount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
	'		For i = 0 To ctrl.ListCount - 1
	'			'UPGRADE_WARNING: Couldn't resolve default property of object ctrl.List. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
	'			If ctrl.List(i) = fdrName Then
	'				'UPGRADE_WARNING: Couldn't resolve default property of object ctrl.ListIndex. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
	'				ctrl.ListIndex = i
	'			End If
	'		Next i
	'	End If
	 
	'End Sub
	'JRSVBNET ****** END ***********
	 
	'JRSVBNET *** BEGIN ***
	'   '=============================StringHandler.BAS  =======================
	'Function StrInCtrl(ByRef ctrl As 'JRSVBNET System.Windows.Forms.Control, ByRef S As String) As Integer
	'	'- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
	 
	'	'  Given a list control, determine if S is an element
	'	'  of the list, and return its index number if it is.
	'	'  If not, return -1
	 
	'	Dim Ind As Integer
	'	Dim i As Integer
	'	Ind = -1
	 
	'	'UPGRADE_WARNING: Couldn't resolve default property of object ctrl.ListCount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
	'	If (ctrl.ListCount > 0) Then
	'		'UPGRADE_WARNING: Couldn't resolve default property of object ctrl.ListCount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
	'		For i = 0 To ctrl.ListCount - 1
	'			'UPGRADE_WARNING: Couldn't resolve default property of object ctrl.List. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
	'			If ctrl.List(i) = S Then
	'				Ind = i
	'				Exit For
	'			End If
	'		Next i
	'	End If
	 
	'	StrInCtrl = Ind
	 
	'End Function
	'JRSVBNET *** END ***
	 
	'=============================StringHandler.BAS  =======================
	Sub Tokenize(ByRef List() As String, ByRef S As String, ByRef Delim As String)
		'- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		 
		'  Tears each token out of String "S", delimited
		'  by "delim", and adds it to array "List"
		 
		Dim StrIn As String
		StrIn = S
		 
		Do Until StrIn = ""
			Call AddStrList(List, GetFirstToken(StrIn, Delim))
			StrIn = AfterFirstToken(StrIn, Delim)
		Loop 
		 
		 
	End Sub 
End Module

'End Namespace
