Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Public Class Frm_IndentPrint
    Implements IForm
    Dim objCommFunction As New CommonClass

    Dim _call_type As Integer
    Dim _report_name As String

    Public Sub New(ByVal call_type As Integer)
        InitializeComponent()
        _call_type = call_type
    End Sub



    Private Sub frm_IndentDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If _call_type = enmReportName.RptIndentPrintDetail Then
            objCommFunction.ComboBind(cmb_Item, "Select 'All' as Item_Name,-1 as Item_Id union select Item_Name,Item_Id from Item_Master", "Item_Name", "Item_Id")
            lbl_Item.Text = "Select Item"
        ElseIf _call_type = enmReportName.RptMRSMainStorePrint Then
            objCommFunction.ComboBind(cmb_Item, "Select 'All' as MRS_No,-1 as MRS_Id union select MRS_Code +''+cast(MRS_no as Varchar) as MRS_No,MRS_Id from MRS_MAIN_STORE_MASTER", "MRS_No", "MRS_Id")
            lbl_Item.Text = "Select MRS"

        End If
    End Sub
    Private Function ValidateData() As Boolean
        ValidateData = False
        If cmb_Item.SelectedIndex < 0 Then
            MsgBox("Please " & lbl_Item.Text, vbExclamation, gblMessageHeading)
            cmb_Item.Focus()
            Exit Function
        End If
       
        ValidateData = True
    End Function

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub
    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try

        Catch ex As Exception

        End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

    End Sub
    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        'Dim ds As New DataSet
        'Dim filepath As String
        ''Dim strQry As String
        ''Dim StrSubQry As String
        'Dim dsSubQry As New DataSet
        'Dim rep As New ReportDocument()

        'If ValidateData() = False Then
        '    Exit Sub
        'End If

        'filepath = ReportFilePath & "cryIndentDetail.rpt"

        'rep.Load(filepath)
        ' ''rep.PrintOptions.PrinterName = CommonFunction.GetValueByKey("PosPrinterName")
        '' Dim marginSet As New PageMargins(1000, 700, 700, 700)
        ' ''rep.PrintOptions.PaperSize = PaperSize.PaperA4
        '' rep.PrintOptions.ApplyPageMargins(marginSet)

        ''  rep.SetDataSource(ds.Tables(0))
        'rep.SetParameterValue("PFromDate", dtp_FromDate.Value.Date)
        'rep.SetParameterValue("PToDate", dtp_ToDate.Value.Date)
        'rep.SetParameterValue("startdate", dtp_FromDate.Value.Date)
        'rep.SetParameterValue("enddate", dtp_ToDate.Value.Date)
        'rep.SetParameterValue("Div_Id", v_the_current_division_id)


        'rep.SetDatabaseLogon("sa", "DataBase", "afblmms", "afbl_mms")
        ''cryMain.ReportSource = rep
        'frm_Report.CryViewer.ReportSource = rep
        'frm_Report.Show()
        ''cryMain.Show()

    End Sub

    'Private Sub cryMain_Load(ByVal sender As Object, ByVal e As System.EventArgs)

    'End Sub

    'Private Sub frm_IndentDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'End Sub
    'Private Sub BtnShowReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShowReport.Click
    '    Dim ds As New DataSet
    '    Dim filepath As String
    '    Dim cryrep As New ReportDocument()
    '    filepath = ReportFilePath & "cryIndentDetail.rpt"
    '    cryrep.Load(filepath)
    '    cryMain.ReportSource = cryrep
    '    cryMain.Refresh()
    'End Sub


    Private Sub BtnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShow.Click
        Try
            Dim ds As New DataSet
            Dim filepath As String = ""
            Dim dsSubQry As New DataSet
            Dim rep As New ReportDocument()

            If ValidateData() = False Then
                Exit Sub
            End If
            If _call_type = enmReportName.RptIndentPrintDetail Then
                filepath = ReportFilePath & "CryIndentPrintRpt.rpt"
                rep.Load(filepath)

            ElseIf _call_type = enmReportName.RptMRSMainStorePrint Then
                filepath = ReportFilePath & "MRS o CostCenter.rpt"
                rep.Load(filepath)

            End If

            'Dim rep As New MyReportDocument(filepath)

            ' rep.Load(filepath)
            ''rep.PrintOptions.PrinterName = CommonFunction.GetValueByKey("PosPrinterName")
            ' Dim marginSet As New PageMargins(1000, 700, 700, 700)
            ''rep.PrintOptions.PaperSize = PaperSize.PaperA4
            ' rep.PrintOptions.ApplyPageMargins(marginSet)

            '  rep.SetDataSource(ds.Tables(0))



            Dim CRXParamDefs As CrystalDecisions.Shared.ParameterFields
            Dim CRXParamDef As CrystalDecisions.Shared.ParameterField
            CRXParamDefs = rep.ParameterFields
            For Each CRXParamDef In CRXParamDefs
                With CRXParamDef
                    Select Case .ParameterFieldName
                        Case "@v_from_date"
                            'rep.SetParameterValue("@v_from_date", dtp_FromDate.Value.Date)
                        Case "@v_to_date"
                            'rep.SetParameterValue("@v_to_date", dtp_ToDate.Value.Date)
                        Case "@v_Item_id"
                            rep.SetParameterValue("@v_Item_id", cmb_Item.SelectedValue)
                        Case "@v_div_id"
                            rep.SetParameterValue("@v_div_id", v_the_current_division_id)
                        Case "PFromDate"
                            'rep.SetParameterValue("PFromDate", dtp_FromDate.Value.Date)
                        Case "PToDate"
                            'rep.SetParameterValue("PToDate", dtp_ToDate.Value.Date)
                        Case "Div_Id"
                            rep.SetParameterValue("Div_Id", v_the_current_division_id)
                        Case "enddate"
                            'rep.SetParameterValue("enddate", dtp_ToDate.Value.Date)
                        Case "startdate"
                            'rep.SetParameterValue("startdate", dtp_FromDate.Value.Date)
                        Case "mrs_id"
                            rep.SetParameterValue("mrs_id", cmb_Item.SelectedValue)

                    End Select
                End With
            Next





            'rep.SetDatabaseLogon("sa", "DataBase", "inderjeet\sql2005", "afbl_mms", True)
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

            If _call_type = enmReportName.RptItemLedgerSummary Then
                frm_Report.Text = "Item Ledger Summary"
            ElseIf _call_type = enmReportName.RptIndentDetail Then
                frm_Report.Text = "Indent Details"
            Else
                frm_Report.Text = "Reports"
            End If

            frm_Report.CryViewer.ReportSource = rep
            frm_Report.Show()
            'cryMain.ReportSource = rep

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try 'cryMain.Show()

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