Option Strict Off
Option Explicit On


'Namespace IFsWeb

'UPGRADE_WARNING: Class instancing was changed to public. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1043"'
'JRSVBNET <System.Runtime.InteropServices.ProgId("Node_NET.Node")> 
Public Class Node
	Dim data As String
	Dim iPLOT As Short
	'UPGRADE_NOTE: left was upgraded to left_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
	Dim left_Renamed As Node
	'UPGRADE_NOTE: right was upgraded to right_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
	Dim right_Renamed As Node
	Dim parent As Node
	 
	 
	Public Property sData() As String
		Get 
			sData = data
		End Get 
		Set (ByVal Value As String)
			data = Value
		End Set 
	End Property 
	 
	 
	Public Property nLeft() As Node
		Get 
			nLeft = left_Renamed
		End Get 
		Set (ByVal Value As Node)
			left_Renamed = Value
		End Set 
	End Property 
	 
	 
	Public Property nRight() As Node
		Get 
			nRight = right_Renamed
		End Get 
		Set (ByVal Value As Node)
			right_Renamed = Value
		End Set 
	End Property 
	 
	 
	Public Property nParent() As Node
		Get 
			nParent = parent
		End Get 
		Set (ByVal Value As Node)
			parent = Value
		End Set 
	End Property 
	 
	 
	Public Property pPlot() As Short
		Get 
			pPlot = iPLOT
		End Get 
		Set (ByVal Value As Short)
			iPLOT = Value
		End Set 
	End Property 
End Class

'End Namespace
