Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Public Class frm_MRNDetails
    Implements IForm
    Dim objCommFunction As New CommonClass

    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Function ValidateData() As Boolean
        ValidateData = False

        If dtp_FromDate.Value.Date > dtp_ToDate.Value.Date Then
            MessageBox.Show("From date can not be greater than To Date.")
            dtp_FromDate.Focus()

            Exit Function
        End If
        ValidateData = True
    End Function
    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            'DGVIndentItem_style()
            'flag = "save"
            'FillIndentInfo()
            ''FillGrid()
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_Indent_Master")
        End Try
    End Sub
    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub
    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

    End Sub
    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Dim ds As New DataSet
        Dim filepath As String
        'Dim strQry As String
        'Dim StrSubQry As String
        Dim dsSubQry As New DataSet
        Dim rep As New ReportDocument()

        If ValidateData() = False Then
            Exit Sub
        End If


        filepath = ReportFilePath & "cryMRNDetail.rpt"
        'Dim rep As New MyReportDocument(filepath)
        rep.Load(filepath)
        ''rep.PrintOptions.PrinterName = CommonFunction.GetValueByKey("PosPrinterName")
        ' Dim marginSet As New PageMargins(1000, 700, 700, 700)
        ''rep.PrintOptions.PaperSize = PaperSize.PaperA4
        ' rep.PrintOptions.ApplyPageMargins(marginSet)

        '  rep.SetDataSource(ds.Tables(0))
        rep.SetParameterValue("PFromDate", dtp_FromDate.Value.Date)
        rep.SetParameterValue("PToDate", dtp_ToDate.Value.Date)
        rep.SetParameterValue("startdate", dtp_FromDate.Value.Date)
        rep.SetParameterValue("enddate", dtp_ToDate.Value.Date)
        rep.SetParameterValue("Div_Id", v_the_current_division_id)


        'rep.SetDatabaseLogon("sa", "DataBase@123", "afblmms", "afbl_mms")
        Dim connection As New ConnectionInfo()

        connection.DatabaseName = gblDataBase_Name 'myDataBase
        connection.ServerName = gblDataBaseServer_Name '127.0.0.1
        connection.UserID = gblDataBase_UserName 'root
        connection.Password = gblDataBase_Password '12345            '
        For Each table As CrystalDecisions.CrystalReports.Engine.Table In rep.Database.Tables
            AssignTableConnection(table, connection)
        Next

        cryMain.ReportSource = rep

        cryMain.Show()

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


    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub
    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub
End Class
