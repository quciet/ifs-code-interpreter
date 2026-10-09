Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility



 
Module DataEnvironment_deSAM_Module
	Public deSAM As DataEnvironment_deSAM = New DataEnvironment_deSAM()
End Module 
 
Public Class DataEnvironment_deSAM
	Inherits VB6.BaseDataEnvironment
	Public WithEvents conIFS As ADODB.Connection
	Public WithEvents rsSAM As ADODB.Recordset
	Private m_SAM As ADODB.Command
	Public Sub New()
		MyBase.New()
		Dim par As ADODB.Parameter
		 
		 
		conIFS = New ADODB.Connection()
        conIFS.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\My Documents\IFs\RUNFILES\IFS.MDB;Persist Security Info=False;"
		m_Connections.Add(conIFS, "conIFS")
		m_SAM = New ADODB.Command()
		rsSAM = New ADODB.Recordset()
		m_SAM.Name = "SAM"
		m_SAM.CommandText = "`SAMValues`"
		m_SAM.CommandType = ADODB.CommandTypeEnum.adCmdTable
		rsSAM.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		rsSAM.CursorType = ADODB.CursorTypeEnum.adOpenStatic
		rsSAM.LockType = ADODB.LockTypeEnum.adLockReadOnly
		rsSAM.Source = m_SAM
		m_Commands.Add(m_SAM, "SAM")
		m_Recordsets.Add(rsSAM, "SAM")
	End Sub
	Public Sub SAM()
		If conIFS.State = ADODB.ObjectStateEnum.adStateClosed Then 
			conIFS.Open()
		End If 
		If rsSAM.State = ADODB.ObjectStateEnum.adStateOpen Then 
			rsSAM.Close()
		End If 
		m_SAM.ActiveConnection = conIFS
		rsSAM.Open()
	End Sub 
End Class 


