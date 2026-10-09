Option Strict Off
Option Explicit On


'Namespace IFsWeb

'UPGRADE_WARNING: Class instancing was changed to public. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1043"'
'JRSVBNET <System.Runtime.InteropServices.ProgId("VectorNodes_NET.VectorNodes")> 
Public Class VectorNodes
	Dim size As Short
	Dim data() As Node
	 
	Public Sub push_back(ByRef x As Node)
		size = size + 1
		ReDim Preserve data(size)
		data(size) = x
	End Sub 
	 
	Public Sub pop_back()
		size = size - 1
		ReDim Preserve data(size)
	End Sub 
	 
	Public Sub append(ByRef x As Node, ByRef i As Short)
		'UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		'UPGRADE_WARNING: Couldn't resolve default property of object data(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		'UPGRADE_WARNING: Couldn't resolve default property of object data(i). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
		'JRSVBNET data(i) = CDbl(data(i)) + CDbl(x)
	End Sub 
	 
	Public Function disSize() As Short
		disSize = size
	End Function 
	 
	Public Function display(ByRef i As Short) As Node
		display = data(i)
	End Function 
	 
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
	Private Sub Class_Initialize_Renamed()
		size = 0
	End Sub 
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class

'End Namespace
