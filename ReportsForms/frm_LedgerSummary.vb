Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class frm_LedgerSummary
    Implements IForm
    Dim objCommFunction As New CommonClass
    Dim _call_type As Integer
    Dim _report_name As String
    Dim _form_rights As Form_Rights

    Public Sub New(ByVal call_type As Integer, ByVal rights As Form_Rights)
        InitializeComponent()
        _call_type = call_type
        _form_rights = rights
    End Sub

    Private Sub frm_LedgerSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objCommFunction.ComboBind(cmb_Item, "select Item_Name,Item_Id from Item_Master", "Item_Name", "Item_Id")
        objCommFunction.ComboBind(cmb_Indent_ID, "Select '--Select Indent Number--' as INDENT_CODE,-1 as INDENT_NO union select INDENT_CODE +' '+ Convert(varchar(20),INDENT_NO),Indent_No from INDENT_MASTER ORDER BY INDENT_NO", "INDENT_CODE", "INDENT_NO")
        objCommFunction.ComboBind(cmb_PO_ID, "select 0 as PO_ID,'--Select--' as PO_CODE union  select PO_ID,PO_CODE from OPEN_PO_MASTER ORDER BY PO_ID", "PO_CODE", "PO_ID")
        'objCommFunction.ComboBind(cmbItemCatId, "select 0 as ITEM_CAT_ID,'--Select--' as ITEM_CAT_NAME union  select ITEM_CAT_ID,ITEM_CAT_NAME from ITEM_CATEGORY ORDER BY ITEM_CAT_ID", "ITEM_CAT_NAME", "ITEM_CAT_ID")
        If _call_type = enmReportName.RptItemLedgerSummary Then
            cmb_Item.Visible = True
            cmb_Indent_ID.Visible = True
            lbl_Item.Visible = True
            cmb_Indent_ID.Visible = False
            cmb_PO_ID.Visible = False
            lbl_PO_ID.Visible = False
            lbl_Indent_ID.Visible = False
        ElseIf _call_type = enmReportName.RptIndentDetail Then
            cmb_Indent_ID.Visible = False
            cmb_Item.Visible = False
            lbl_Item.Visible = False
            cmb_PO_ID.Visible = False
            lbl_PO_ID.Visible = False
            lbl_Indent_ID.Visible = False
        ElseIf _call_type = enmReportName.RptIndentPrintDetail Then
            lbl_FromDate.Visible = False
            lbl_ToDate.Visible = False
            dtp_FromDate.Visible = False
            dtp_ToDate.Visible = False
            cmb_Item.Visible = False
            lbl_Item.Visible = False
            cmb_PO_ID.Visible = False
            lbl_PO_ID.Visible = False
            cmb_Indent_ID.Visible = True
            lbl_Indent_ID.Visible = True
        ElseIf _call_type = enmReportName.RptListofIndent Then
            lbl_FromDate.Visible = True
            lbl_ToDate.Visible = True
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            cmb_Item.Visible = False
            lbl_Item.Visible = False
            cmb_Indent_ID.Visible = False
            lbl_Indent_ID.Visible = False
            cmb_Item.Visible = False
            cmb_PO_ID.Visible = True
            lbl_PO_ID.Visible = True
            'ElseIf _call_type = enmReportName.RptStockValue Then
            '    lbl_FromDate.Visible = False
            '    lbl_ToDate.Visible = False
            '    dtp_FromDate.Visible = False
            '    dtp_ToDate.Visible = False
            '    cmb_Item.Visible = False
            '    lbl_Item.Visible = False
            '    cmb_Indent_ID.Visible = False
            '    lbl_Indent_ID.Visible = False
            '    cmb_Item.Visible = False
            '    cmb_PO_ID.Visible = False
            '    lbl_PO_ID.Visible = False
            '    cmbItemCatId.Visible = True
            '    txtItemCode.Visible = True
            '    txtItemName.Visible = True
            '    dtpDate.Visible = True
        End If
    End Sub

    Private Function ValidateData() As Boolean
        ValidateData = False

        If dtp_FromDate.Value.Date > dtp_ToDate.Value.Date Then
            MessageBox.Show("From date can not be greater than To Date.")
            dtp_FromDate.Focus()

            Exit Function
        End If
        If gblSelectedReportName = enmReportName.RptItemLedgerSummary Then
            If cmb_Item.SelectedIndex < 0 Then
                MsgBox("Select Item for ledger.", vbExclamation, gblMessageHeading)
                cmb_Item.Focus()
                Exit Function
            End If
        End If

        'If gblSelectedReportName = enmReportName.RptOpenPurchaseOrder Then
        '    If cmb_PO_ID.SelectedValue = -1 Then
        '        MsgBox("Please select PO ID.", vbExclamation, gblMessageHeading)
        '        cmb_PO_ID.Focus()
        '        Exit Function
        '    End If
        'End If

        If gblSelectedReportName = enmReportName.RptIndentPrintDetail Then
            If cmb_Indent_ID.SelectedValue = -1 Then
                MsgBox("Please select Indent Id.", vbExclamation, gblMessageHeading)
                cmb_Indent_ID.Focus()
                Exit Function
            End If
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
      
    End Sub
    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub
    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Private Sub cmd_Show_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Show.Click
        Try
            Dim ds As New DataSet
            Dim filepath As String = ""
            'Dim strQry As String
            'Dim StrSubQry As String
            Dim dsSubQry As New DataSet
            Dim rep As New ReportDocument()

            '--Validation for dropdown--
            If ValidateData() = False Then
                Exit Sub
            End If

            'If gblSelectedReportName = enmReportName.RptItemLedgerSummary Then
            If _call_type = enmReportName.RptItemLedgerSummary Then
                filepath = ReportFilePath & "cryLedgerSummary.rpt"
            ElseIf _call_type = enmReportName.RptIndentDetail Then
                filepath = ReportFilePath & "cryIndentDetail.rpt"
            ElseIf _call_type = enmReportName.RptIndentPrintDetail Then
                filepath = ReportFilePath & "cryIndentPrintRpt.rpt"
            ElseIf _call_type = enmReportName.RptListofIndent Then
                filepath = ReportFilePath & "cryListofIndent.rpt"
                'ElseIf _call_type = enmReportName.RptOpenPurchaseOrder Then
                '    filepath = ReportFilePath & "cryOPEN_PURCHASE_ORDER.rpt"
                'ElseIf _call_type = enmReportName.RptStockValue Then
                '    filepath = ReportFilePath & "cryStockValue.rpt"
            End If

            rep.Load(filepath)
            'Dim rep As New MyReportDocument(filepath)
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
                            rep.SetParameterValue("@v_from_date", dtp_FromDate.Value.Date)
                        Case "@v_to_date"
                            rep.SetParameterValue("@v_to_date", dtp_ToDate.Value.Date)
                        Case "@v_Item_id"
                            rep.SetParameterValue("@v_Item_id", cmb_Item.SelectedValue)
                        Case "@v_div_id"
                            rep.SetParameterValue("@v_div_id", v_the_current_division_id)
                        Case "PFromDate"
                            rep.SetParameterValue("PFromDate", dtp_FromDate.Value.Date)
                        Case "PToDate"
                            rep.SetParameterValue("PToDate", dtp_ToDate.Value.Date)
                        Case "Div_Id"
                            rep.SetParameterValue("Div_Id", v_the_current_division_id)
                        Case "enddate"
                            rep.SetParameterValue("enddate", dtp_ToDate.Value.Date)
                        Case "startdate"
                            rep.SetParameterValue("startdate", dtp_FromDate.Value.Date)
                        Case "indent_id"
                            rep.SetParameterValue("indent_id", cmb_Indent_ID.SelectedValue)
                        Case "PO_ID"
                            rep.SetParameterValue("PO_ID", cmb_PO_ID.SelectedValue)
                    End Select
                End With
            Next

            ' rep.SetDatabaseLogon("sa", "DataBase@123", "afblmms", "afbl_mms")

            Dim connection As New ConnectionInfo()

            connection.DatabaseName = gblDataBase_Name 'myDataBase
            connection.ServerName = gblDataBaseServer_Name '127.0.0.1
            connection.UserID = gblDataBase_UserName 'root
            connection.Password = gblDataBase_Password '12345            '
            For Each table As CrystalDecisions.CrystalReports.Engine.Table In rep.Database.Tables
                AssignTableConnection(table, connection)
            Next




            If _call_type = enmReportName.RptItemLedgerSummary Then
                frm_Report.Text = "Item Ledger Summary"
            ElseIf _call_type = enmReportName.RptIndentDetail Then
                frm_Report.Text = "Indent Details"
            ElseIf _call_type = enmReportName.RptIndentPrintDetail Then
                frm_Report.Text = "Indent Print Details"
            ElseIf _call_type = enmReportName.RptListofIndent Then
                frm_Report.Text = "List Of Indents"
                'ElseIf _call_type = enmReportName.RptOpenPurchaseOrder Then
                '    frm_Report.Text = "Open Purchase Order Detail"
                'ElseIf _call_type = enmReportName.RptStockValue Then
                '    frm_Report.Text = "Stock Value"
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


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MsgBox(_call_type)
    End Sub
End Class
