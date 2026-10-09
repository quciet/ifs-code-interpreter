Imports System
Imports System.Collections.Generic
Imports IFs.Core
Imports IFs.Data

Public Interface IQueryService 
    Inherits IDisposable

    ' query data for the specified time range and dimensional terms
    Function Query(sVar As String, iFirstYear As Short, iLastYear As Short, ParamArray sDims() As String) As IDataframe

End Interface
