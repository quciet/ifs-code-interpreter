'--------------------------------------------------------------------------------
'Product
'		AWS FilePicker 2.1
'
'Authors
'		Alex Avrutin (alex@awsystems.biz)
'       Juan C. Lopez (jlopez@awsystems.biz)
'
'Company
'		AW/Systems, LLC (http:www.awsystems.biz)
'				  Off-shore software outsourcing
'       Interested in reducing cost of web development? 
'       Then why not delegate some work to AW/Systems? 
'       We offer .NET web development services at only 
'       $3,400 per a man-month or $30 per an hour.

'Version 
'		2.1
'
'This file:
'       Builds file and directory list in a given folder.


Imports System.IO
Imports System.Resources



Public Class Explorer

    Private _Path As String
    Private _Files As DataTable
    Private _rm As ResourceManager

    ReadOnly Property Path() As String
        Get
            Return _Path
        End Get
    End Property

    ReadOnly Property Files() As DataTable
        Get
            Return _Files
        End Get
    End Property

    '========================================================================================

    Private Sub SetPath(ByVal Path As String)

        _Path = Path

        _Files = New DataTable("Files")

        Dim myColumn As DataColumn
        myColumn = New DataColumn("Id", GetType(Integer))
        myColumn.AutoIncrement = True
        _Files.Columns.Add(myColumn)

        myColumn = New DataColumn("Chk", GetType(Boolean))
        _Files.Columns.Add(myColumn)

        myColumn = New DataColumn("Type", GetType(String))
        _Files.Columns.Add(myColumn)

        myColumn = New DataColumn("Name", GetType(String))
        _Files.Columns.Add(myColumn)

        myColumn = New DataColumn("Length", GetType(String))
        _Files.Columns.Add(myColumn)

        myColumn = New DataColumn("Updated", GetType(String))
        _Files.Columns.Add(myColumn)
    End Sub

    '========================================================================================

    Private Sub GetFiles()

        Dim Path As String = System.Web.HttpContext.Current.Server.MapPath(_Path)
        Dim DirInfo As DirectoryInfo = New DirectoryInfo(Path)
        Dim Directory As DirectoryInfo
        Dim File As FileInfo

        For Each Directory In DirInfo.GetDirectories()
            AddDirectory(Directory)
        Next

        For Each File In DirInfo.GetFiles()
            AddFile(File)
        Next
    End Sub

    '========================================================================================

    Private Sub AddDirectory(ByVal Dir As DirectoryInfo)

        Dim dr As DataRow

        dr = _Files.NewRow()
        dr("Chk") = False
        dr("Type") = "D"
        dr("Name") = Dir.Name
        dr("Length") = _rm.GetString("LOC_LABEL_DIR")
        dr("Updated") = Dir.LastWriteTime()

        _Files.Rows.Add(dr)
    End Sub

    '========================================================================================

    Private Sub AddFile(ByVal File As FileInfo)

        Dim dr As DataRow

        dr = _Files.NewRow()
        dr("Chk") = False
        dr("Type") = "F"
        dr("Name") = File.Name
        dr("Length") = Utils.FormatFileSize(File.Length, _rm)
        dr("Updated") = File.LastWriteTime

        _Files.Rows.Add(dr)
    End Sub

    '========================================================================================

    Public Sub New(ByVal Path As String)

        MyBase.New()
        _rm = (CType(HttpContext.Current.Application("fpRm"), ResourceManager))

        SetPath(Path)
        GetFiles()
    End Sub
End Class
