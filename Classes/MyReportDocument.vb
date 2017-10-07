Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO

Public Class MyReportDocument
    Inherits ReportDocument

    Private _myExePath As String
    Private _myDirectoryPath As String
    Private _myFileName As String
    Private _myImagePath As String

    Public Property myExePath() As String
        Get
            Return _myExePath
        End Get
        Set(value As String)
            _myExePath = value
        End Set
    End Property

    Public Property myDirectoryPath() As String
        Get
            Return _myDirectoryPath
        End Get
        Set(value As String)
            _myDirectoryPath = value
        End Set
    End Property

    Public Property myFileName() As String
        Get
            Return _myFileName
        End Get
        Set(value As String)
            _myFileName = value
        End Set
    End Property

    Public Property myImagePath() As String
        Get
            Return _myImagePath
        End Get
        Set(value As String)
            _myImagePath = value
        End Set
    End Property


    Public Sub New(ByVal filepath As String)
        Me.myExePath = Application.ExecutablePath()
        Me.myDirectoryPath = Path.GetDirectoryName(myExePath)
        Me.myFileName = "CompanyLogo.jpg"
        Me.myImagePath = myDirectoryPath + "\Images\" + myFileName
        Me.Load(filepath)
        Me.SetParameterValue("imageUrl", myImagePath)
    End Sub

    Public Overrides Sub SetDataSource(dataTable As System.Data.DataTable)
        MyBase.SetDataSource(dataTable)
        Me.myExePath = Application.ExecutablePath()
        Me.myDirectoryPath = Path.GetDirectoryName(myExePath)
        Me.myFileName = "CompanyLogo.jpg"
        Me.myImagePath = myDirectoryPath + "\Images\" + myFileName
        Me.Load(FilePath)
        Me.SetParameterValue("imageUrl", myImagePath)
    End Sub
End Class
