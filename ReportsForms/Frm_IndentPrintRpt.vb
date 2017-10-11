
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Public Class Frm_IndentPrintRpt
    Implements IForm
    Dim objCommFunction As New CommonClass
    Private Sub frm_IndentPrintRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'objCommFunction.ComboBind(cmb_Indent_ID, "Select '--Select Indent Number--' as INDENT_CODE,-1 as INDENT_NO union select INDENT_CODE,INDENT_NO from INDENT_MASTER", "INDENT_CODE", "INDENT_NO")
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
        If (cmb_Indent_ID.SelectedValue = -1) Then
            cryMain.ReportSource = Nothing
            MessageBox.Show("Please go to select Indent Number.")
            cmb_Indent_ID.Focus()
        Else
            Dim ds As New DataSet
            Dim filepath As String
            'Dim strQry As String
            'Dim StrSubQry As String
            Dim dsSubQry As New DataSet
            Dim rep As New ReportDocument()

            filepath = ReportFilePath & "CryIndentPrintRpt.rpt"
            'Dim rep As New MyReportDocument(filepath)
            rep.Load(filepath)
            ''rep.PrintOptions.PrinterName = CommonFunction.GetValueByKey("PosPrinterName")
            ' Dim marginSet As New PageMargins(1000, 700, 700, 700)
            'rep.PrintOptions.PaperSize = PaperSize.PaperA4

            rep.SetParameterValue("indent_id", cmb_Indent_ID.SelectedValue)
            'rep.SetParameterValue("V_Indent_Id", v_the_current_division_id)
            'rep.SetParameterValue("indent_id", v_the_current_division_id)

            'rep.SetDatabaseLogon("sa","DataBase","inderjeet\sql2005","afbl_mms")
            'rep.SetDatabaseLogon("sa", "DataBase@123", "afblmms", "afbl_mms")

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

            'rep.SetDatabaseLogon("sa", "DataBase", "inderjeet", "afbl_mms")
            'rep.SetDatabaseLogon("afblmms", "alcafblmms", "republicofchicken.com\sql2005", "afbl_mms")
            cryMain.ReportSource = rep
            cryMain.Show()
        End If
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

End Class

