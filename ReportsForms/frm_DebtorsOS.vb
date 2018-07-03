Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Public Class frm_DebtorsOS
    Implements IForm
    Dim obj As New CommonClass
    Dim clsObj As New cls_Supplier_Invoice_Settlement
    Dim _rights As Form_Rights
    Dim PaymentId As Int16
    Dim flag As String
    Dim _call_type As Integer
    Dim _report_name As String
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub
    Public Sub CustomerBind()
        clsObj.ComboBind(cmbSupplier, "Select ACC_ID,ACC_NAME from ACCOUNT_MASTER WHERE AG_ID=" & AccountGroups.Sundry_Debtors & " Order by ACC_NAME", "ACC_NAME", "ACC_ID", True)
    End Sub
    Public Sub ReportOS()
        Dim filepath As String = ""
        Dim rep As New ReportDocument()

        'If cmbSupplier.SelectedValue >= 0 Then
        'If _call_type = enmReportName.RptDebtorsOSPrint Then
        filepath = ReportFilePath & "cry_DebtorOutstanding.rpt"
            'End If
            rep.Load(filepath)

        If cmbSupplier.SelectedValue < 0 Then
            rep.SetParameterValue("AccId", "null")
        Else
            rep.SetParameterValue("AccId", cmbSupplier.SelectedValue)
            End If

            rep.SetParameterValue("Date", txtDateSearch.Value.Date)

                Dim connection As New ConnectionInfo()


                connection.DatabaseName = gblDataBase_Name 'myDataBase
                connection.ServerName = gblDataBaseServer_Name '127.0.0.1
                connection.UserID = gblDataBase_UserName 'root
                connection.Password = gblDataBase_Password '12345

                ' First we assign the connection to all tables in the main report
                '
                For Each table As CrystalDecisions.CrystalReports.Engine.Table In rep.Database.Tables
                    AssignTableConnection(table, connection)
                Next

                frm_Report.cryViewer.ReportSource = rep
                frm_Report.Show()

        'End If
    End Sub
    Private Sub AssignTableConnection(ByVal table As CrystalDecisions.CrystalReports.Engine.Table, ByVal connection As ConnectionInfo)
        ' Cache the logon info block
        Dim logOnInfo As TableLogOnInfo = table.LogOnInfo

        connection.Type = logOnInfo.ConnectionInfo.Type

        ' Set the connection
        logOnInfo.ConnectionInfo = connection

        ' Apply the connection to the table!

        table.LogOnInfo.ConnectionInfo.DatabaseName = connection.DatabaseName
        table.LogOnInfo.ConnectionInfo.ServerName = connection.ServerName
        table.LogOnInfo.ConnectionInfo.UserID = connection.UserID
        table.LogOnInfo.ConnectionInfo.Password = connection.Password
        table.LogOnInfo.ConnectionInfo.Type = connection.Type
        table.ApplyLogOnInfo(logOnInfo)
    End Sub
    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick

    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick


    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            ReportOS()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frm_DebtorsOS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CustomerBind()
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            ReportOS()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
