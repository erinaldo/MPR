Imports MMSPlus.Adjustment_master
Imports System.Data.SqlClient
Imports System.IO
Imports System.ComponentModel
Imports System.Net
Imports Ionic.Zip

Public Class BackupDB
    Implements IForm
    Dim _user_role As String
    Dim obj As New CommonClass
    Dim Item_obj As New item_detail.cls_item_detail
    Dim prpty As New item_detail.cls_item_detail_prop
    Dim dtWastageItem As New DataTable
    Dim flag As String
    Dim rights As Form_Rights
    Dim cmd As New SqlCommand
    Dim con As New SqlConnection
    Dim Trans As SqlTransaction
    Dim iAdjustmentId As Int32
    Dim objComm As New CommonClass
    Dim _rights As Form_Rights



    Dim FTPBackupFolder As String = String.Empty
    Dim ftpPath As String = "ftp://syncsolz.com/CLIENT_BACKUP/"
    Dim ftpRequest As FtpWebRequest
    Dim credential As NetworkCredential = New NetworkCredential("syncsolz@junifilms.com", "$ync$olz@123")
    Const success As String = "Success"
    Dim worker As BackgroundWorker = New BackgroundWorker

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub BackupDB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeControls()
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick

    End Sub


    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

    End Sub
    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        'FillGrid()
    End Sub
    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub
    Private Sub InitializeControls()
        AddHandler worker.DoWork, AddressOf bw_DoWork
        AddHandler worker.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted
        progressBar.Style = ProgressBarStyle.Blocks
        progressBar.Step = 15
        ftpRequest = FtpWebRequest.Create(ftpPath)
        lblOnlineBackupStatus.Text = GetOnlineBackupStatus()
        lblCurrentBackupDetail.Text = "Current Backup Detail :"
        LoadGrid()
    End Sub
    Private Sub new_initilization()

    End Sub
    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        MsgBox("Backup Progress completed. Please check last record status for detail.", MsgBoxStyle.Information, "Backup Progress completed")
        progressBar.Style = ProgressBarStyle.Blocks
        LoadGrid()
    End Sub

    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        TakeBackup()
    End Sub

    Private Function GetOnlineBackupStatus() As String

        Dim query As String = "SELECT value FROM mmssetting WHERE [key]='FTPBackupFolder'"
        Dim BackupFolderName = obj.ExecuteScalar(query)

        FTPBackupFolder = BackupFolderName
        If String.IsNullOrEmpty(FTPBackupFolder) Then
            Return "Online Backup Status : Disabled"
        Else
            Return "Online Backup Status : Enabled - " & FTPBackupFolder
        End If

    End Function

    Private Sub LoadGrid()
        Dim Query As String = "SELECT top 100 FileName_vch AS [File Name],OnLocaLoaded_bit [On Local] ,OnOnlineLoaded_bit [On Online] ,BackupStatus_vch [Backup Status]," &
            " TimeStamp_dt as [Backup Taken On] FROM dbo.DBBackupDetails ORDER BY TimeStamp_dt DESC"
        Dim table As DataTable = obj.FillDataSet(Query).Tables(0)
        lblLastBackupDetail.Text = "Last Backup Detail : N.A."
        FgrdBykMaster.DataSource = table
        If table.Rows.Count > 0 Then
            Dim row As DataRow = table.Rows(0)
            lblLastBackupDetail.Text = "Last Backup Detail :" &
                Environment.NewLine &
                " File - " & row("File Name") & " @ " & row("Backup Taken On") &
                Environment.NewLine &
                " Status - " & row("Backup Status")
        End If
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs)
        'If worker.IsBusy = True Then
        '    If MsgBox("Backup process is already in progress. Click 'Yes' to cancel the backup process.",
        '                "Backup Process in Progress", MsgBoxStyle.YesNo) = vbYes Then
        '        Me.Close()
        '    End If
        'Else
        '    Me.Close()
        'End If
    End Sub

    Private Sub btnBackupDb_Click(sender As Object, e As EventArgs) Handles btnBackupDb.Click
        If Not worker.IsBusy = True Then
            progressBar.Style = ProgressBarStyle.Marquee
            worker.RunWorkerAsync()
        Else
            MsgBox("Backup process is already in progress.", MsgBoxStyle.Information, "Backup Process in Progress")
        End If
    End Sub

    Private Sub TakeBackup()
        Dim fileName As String = GetFileName()
        Dim localBackupStatus As String = TakeLocalBackup(fileName)
        Dim onlineBackupStatus As String = TakeFtpBackup(ToZipFileName(fileName))
        InsertRecord(fileName, localBackupStatus, onlineBackupStatus)
    End Sub

    Private Sub InsertRecord(fileName As String, localBackupStatus As String, onlineBackupStatus As String)
        Dim Query As String = "INSERT into dbo.DBBackupDetails" &
                "(FileName_vch ," &
                "OnLocaLoaded_bit ," &
                "OnOnlineLoaded_bit , " &
                "BackupStatus_vch ," &
                "TimeStamp_dt" &
                ")" &
                "VALUES( '" & ToZipFileName(fileName) & "' ," &
                IIf(localBackupStatus = success, "1", "0") & ", " &
                IIf(onlineBackupStatus = success, "1", "0") & "," &
                "'" & String.Format("Local : {0}, Remote : {1}", localBackupStatus, onlineBackupStatus) & "' ," &
                "GETDATE()" &
                ")"
        obj.ExecuteNonQuery(Query)
    End Sub

    Private Function TakeFtpBackup(fileName As String) As String
        If String.IsNullOrEmpty(FTPBackupFolder) Then
            Return "Backup FTP Folder not defined."
        End If
        If Not CheckBackupFolderExists() Then
            Return "Backup FTP Folder not created on FTP."
        End If
        Return UploadBackupFileOnFtp(fileName)
    End Function

    Private Function UploadBackupFileOnFtp(fileName As String) As String
        Try
            Dim fileInfo As New FileInfo(fileName)
            Dim client As WebClient = New WebClient
            client.Credentials = credential
            Dim fileToUploadOnFtp As String = ftpPath & FTPBackupFolder & "/" & fileInfo.Name
            client.UploadFile(fileToUploadOnFtp, fileName)
            Return success
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Private Function CheckBackupFolderExists() As Boolean
        ftpRequest.Credentials = credential
        Dim resp As FtpWebResponse = Nothing
        ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory
        ftpRequest.KeepAlive = True
        Using resp
            resp = ftpRequest.GetResponse()
            Dim sr As StreamReader = New StreamReader(resp.GetResponseStream(), System.Text.Encoding.ASCII)
            Dim s As String = sr.ReadToEnd()
            If s.Contains(FTPBackupFolder) Then
                Return True
            Else
                Return False
            End If
        End Using
    End Function

    Private Function TakeLocalBackup(fileName As String) As String
        Try
            Dim Query = "BACKUP DATABASE " & gblDataBase_Name & " TO DISK = '" & fileName & "'"
            obj.ExecuteNonQueryWithoutTransaction(Query)

            Dim fileInfo As New FileInfo(fileName)
            Using zip As ZipFile = New ZipFile()
                zip.AddFile(fileName, "backup")
                zip.Save(ToZipFileName(fileInfo.FullName))
            End Using

            File.Delete(fileName)
            Return success
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Private Shared Function ToZipFileName(fileName As String) As String
        Return fileName.Replace(".bak", ".zip")
    End Function

    Private Function GetFileName() As String
        Dim path As String = Application.StartupPath & "\Backup\"
        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If
        Return path & DateTime.Now.ToString("yyyyMMddhhmmss") & "_MMSR.bak"
    End Function


End Class
