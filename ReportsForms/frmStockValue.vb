Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class frmStockValue
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

    Private Sub frmStockValue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objCommFunction.ComboBind(cmbItemCatId, "select  '%' as ITEM_CAT_HEAD_ID,'--All Categories--' as ITEM_CAT_Head_NAME union select  cast(ITEM_CAT_HEAD_ID as varchar), ITEM_CAT_Head_NAME from ITEM_CATEGORY_HEAD_MASTER ORDER BY ITEM_CAT_HEAD_ID", "ITEM_CAT_Head_NAME", "ITEM_CAT_HEAD_ID")
        objCommFunction.ComboBind(cmbBrand, "select  '%' as Pk_LabelDetailId_Num,'--All Brands--' as LabelItemName_vch union select  cast(Pk_LabelDetailId_Num as varchar), LabelItemName_vch from Label_Items ORDER BY Pk_LabelDetailId_Num", "LabelItemName_vch", "Pk_LabelDetailId_Num")

        cmbBrand.Visible = False
        Label6.Visible = False

        If _call_type = enmReportName.RptStockValue Then
            cmbItemCatId.Visible = True
            cmb_subCategory.Visible = True
            txtItemCode.Visible = True
            txtItemName.Visible = True
            dtpDate.Visible = True
            chk_NotZeroQty.Checked = True
        ElseIf _call_type = enmReportName.RptStockValueCategoryWise Then
            cmbItemCatId.Visible = True
            cmb_subCategory.Visible = True
            txtItemCode.Visible = True
            txtItemName.Visible = True
            dtpDate.Visible = True
            chk_NotZeroQty.Checked = True
        ElseIf _call_type = enmReportName.RptStockValueBrandWise Then
            cmbItemCatId.Visible = False
            cmb_subCategory.Visible = False
            Label1.Visible = False
            Label5.Visible = False
            Label6.Visible = True
            cmbBrand.Visible = True
            txtItemCode.Visible = True
            txtItemName.Visible = True
            dtpDate.Visible = True
            chk_NotZeroQty.Checked = True
        ElseIf _call_type = enmReportName.RptStockValueBatchWise Then
            cmbItemCatId.Visible = True
            cmb_subCategory.Visible = True
            txtItemCode.Visible = True
            txtItemName.Visible = True
            chk_NotZeroQty.Checked = True
            Label4.Visible = False
            dtpDate.Visible = False
            dtpDate.Value = Now
        ElseIf _call_type = enmReportName.RptLastpurchaserate Then
            cmbItemCatId.Visible = True
            cmb_subCategory.Visible = True
            txtItemCode.Visible = True
            txtItemName.Visible = True
            chk_NotZeroQty.Visible = False
            Label4.Visible = False
            dtpDate.Visible = False
            dtpDate.Visible = False
            chkCalculateAllData.Visible = False
        ElseIf _call_type = enmReportName.RptAllPurchaseRate Then
            cmbItemCatId.Visible = True
            cmb_subCategory.Visible = True
            txtItemCode.Visible = True
            txtItemName.Visible = True
            chk_NotZeroQty.Visible = False
            Label4.Visible = False
            dtpDate.Visible = False
            dtpDate.Visible = False
            chkCalculateAllData.Visible = False
        End If
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

    End Sub

    Private Function ValidateData() As Boolean
        'ValidateData = False


        'ValidateData = True
    End Function

    Private Sub cmd_Show_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Show.Click
        Try
            Dim ds As New DataSet
            Dim filepath As String
            Dim selectionFormnula As String
            Dim dsSubQry As New DataSet
            Dim rep As New ReportDocument()

            '--Validation for dropdown--
            'If ValidateData() = False Then
            '    'Exit Sub
            'End If
            filepath = ""

            If _call_type = enmReportName.RptStockValue Then
                filepath = ReportFilePath & "cryStockValue.rpt"
            ElseIf _call_type = enmReportName.RptStockValueCategoryWise Then
                filepath = ReportFilePath & "cryStockValueCategoryWise.rpt"
            ElseIf _call_type = enmReportName.RptStockValueBrandWise Then
                filepath = ReportFilePath & "cryStockValueBrandWise.rpt"
            ElseIf _call_type = enmReportName.RptStockValueBatchWise Then
                filepath = ReportFilePath & "cryStockValue_BatchWise.rpt"
            ElseIf _call_type = enmReportName.RptStockValueCC Then
                filepath = ReportFilePath & "cryStockValue_CostCenter.rpt"
            ElseIf _call_type = enmReportName.RptLastpurchaserate Then
                filepath = ReportFilePath & "cryLastpurchaserater.rpt"
            ElseIf _call_type = enmReportName.RptAllPurchaseRate Then
                filepath = ReportFilePath & "cryAllPurchaserates.rpt"
            End If

            rep.Load(filepath)
            'Dim rep As New MyReportDocument(filepath)


            'Dim CRXParamDefs As CrystalDecisions.Shared.ParameterFields
            'Dim CRXParamDef As CrystalDecisions.Shared.ParameterField
            'CRXParamDefs = rep.ParameterFields
            'For Each CRXParamDef In CRXParamDefs
            '    With CRXParamDef
            '        Select Case .ParameterFieldName

            '            'Case "PO_ID"
            '            'rep.SetParameterValue("PO_ID", cmb_PO_ID.SelectedValue)
            '        End Select
            '    End With
            'Next   

            'rep.SetDatabaseLogon("sa", "DataBase", "afblmms", "afbl_mms")


            If _call_type = enmReportName.RptStockValueCategoryWise Then
                selectionFormnula = ""
                If cmbItemCatId.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {category_stock.ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                    Else
                        selectionFormnula += " and {category_stock.ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                    End If

                End If
                If cmb_subCategory.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {category_stock.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                    Else
                        selectionFormnula += " and {category_stock.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                    End If

                End If
                If Trim(txtItemCode.Text) <> "" Then
                    If Trim(selectionFormnula) = "" Then
                        selectionFormnula = " {category_stock.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                    Else
                        selectionFormnula += " and  {category_stock.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                    End If
                End If

                If Trim(txtItemName.Text) <> "" Then
                    If Trim(selectionFormnula) = "" Then
                        selectionFormnula = " {category_stock.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                    Else
                        selectionFormnula += " and  {category_stock.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                    End If
                End If
                If Not chk_NotZeroQty.Checked Then
                    If Trim(selectionFormnula) = "" Then
                        selectionFormnula = " {category_stock.STOCK}<> 0"
                    Else
                        selectionFormnula += " and {category_stock.STOCK}<> 0"
                    End If

                End If

                If Trim(selectionFormnula) <> "" Then
                    rep.RecordSelectionFormula = selectionFormnula
                End If
                rep.SetParameterValue("On_Date", "'" & dtpDate.Value.Date.ToString("dd-MMM-yyyy") & "'")

            ElseIf _call_type = enmReportName.RptStockValueBrandWise Then
                selectionFormnula = ""
                    If cmbItemCatId.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {category_stock.ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                        Else
                            selectionFormnula += " and {category_stock.ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                        End If

                    End If
                If cmb_subCategory.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {category_stock.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                    Else
                        selectionFormnula += " and {category_stock.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                    End If

                End If
                If cmbBrand.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {category_stock.Pk_LabelDetailId_Num}=" & cmbBrand.SelectedValue
                    Else
                        selectionFormnula += " and {category_stock.Pk_LabelDetailId_Num}=" & cmbBrand.SelectedValue
                    End If

                End If
                If Trim(txtItemCode.Text) <> "" Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {category_stock.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                        Else
                            selectionFormnula += " and  {category_stock.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                        End If
                    End If

                    If Trim(txtItemName.Text) <> "" Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {category_stock.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                        Else
                            selectionFormnula += " and  {category_stock.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                        End If
                    End If
                    If Not chk_NotZeroQty.Checked Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {category_stock.STOCK}<> 0"
                        Else
                            selectionFormnula += " and {category_stock.STOCK}<> 0"
                        End If

                    End If

                    If Trim(selectionFormnula) <> "" Then
                        rep.RecordSelectionFormula = selectionFormnula
                    End If
                    rep.SetParameterValue("On_Date", "'" & dtpDate.Value.Date.ToString("dd-MMM-yyyy") & "'")


                ElseIf _call_type = enmReportName.RptStockValue Then

                    selectionFormnula = ""
                    If cmbItemCatId.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {item_stock.fk_ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                        Else
                            selectionFormnula += " and {item_stock.fk_ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                        End If

                    End If

                    If cmb_subCategory.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {item_stock.ITEM_CATEGORY_ID}=" & cmb_subCategory.SelectedValue
                        Else
                            selectionFormnula += " and {item_stock.ITEM_CATEGORY_ID}=" & cmb_subCategory.SelectedValue
                        End If

                    End If
                    If Trim(txtItemCode.Text) <> "" Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {item_stock.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                        Else
                            selectionFormnula += " and  {item_stock.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                        End If
                    End If

                    If Trim(txtItemName.Text) <> "" Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {item_stock.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                        Else
                            selectionFormnula += " and  {item_stock.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                        End If
                    End If
                    If Not chk_NotZeroQty.Checked Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {item_stock.STOCK}<> 0"
                        Else
                            selectionFormnula += " and {item_stock.STOCK}<> 0"
                        End If

                    End If

                    If Trim(selectionFormnula) <> "" Then
                        rep.RecordSelectionFormula = selectionFormnula
                    End If
                    rep.SetParameterValue("on_date", "'" & dtpDate.Value.Date.ToString("dd-MMM-yyyy") & "'")
                    rep.SetParameterValue("calculate_all_data", chkCalculateAllData.Checked)


                ElseIf _call_type = enmReportName.RptStockValueBatchWise Then

                    selectionFormnula = ""
                    If cmbItemCatId.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {item_stock.fk_ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                        Else
                            selectionFormnula += " and {item_stock.fk_ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                        End If

                    End If
                    If cmb_subCategory.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {item_stock.ITEM_CATEGORY_ID}=" & cmb_subCategory.SelectedValue
                        Else
                            selectionFormnula += " and {item_stock.ITEM_CATEGORY_ID}=" & cmb_subCategory.SelectedValue
                        End If

                    End If
                    If Trim(txtItemCode.Text) <> "" Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {item_stock.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                        Else
                            selectionFormnula += " and  {item_stock.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                        End If
                    End If

                    If Trim(txtItemName.Text) <> "" Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {item_stock.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                        Else
                            selectionFormnula += " and  {item_stock.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                        End If
                    End If

                    If Not chk_NotZeroQty.Checked Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {item_stock.STOCK}<> 0"
                        Else
                            selectionFormnula += " and {item_stock.STOCK}<> 0"
                        End If

                    End If

                    If Trim(selectionFormnula) <> "" Then
                        rep.RecordSelectionFormula = selectionFormnula
                    End If
                    rep.SetParameterValue("on_date", "'" & dtpDate.Value.Date.ToString("dd-MMM-yyyy") & "'")

                ElseIf _call_type = enmReportName.RptLastpurchaserate Then
                    selectionFormnula = ""
                    If cmbItemCatId.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {command_1.ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                        Else
                            selectionFormnula += " and {command_1.ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                        End If

                    End If
                    If cmb_subCategory.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {command_1.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                        Else
                            selectionFormnula += " and {command_1.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                        End If

                    End If
                    If Trim(txtItemCode.Text) <> "" Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {command_1.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                        Else
                            selectionFormnula += " and  {command_1.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                        End If
                    End If

                    If Trim(txtItemName.Text) <> "" Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {command_1.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                        Else
                            selectionFormnula += " and  {command_1.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                        End If
                    End If

                    If Trim(selectionFormnula) <> "" Then
                        rep.RecordSelectionFormula = selectionFormnula
                    End If

                ElseIf _call_type = enmReportName.RptAllPurchaseRate Then
                    selectionFormnula = ""
                    If cmbItemCatId.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {command_1.ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                        Else
                            selectionFormnula += " and {command_1.ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                        End If

                    End If
                    If cmb_subCategory.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {command_1.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                        Else
                            selectionFormnula += " and {command_1.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                        End If

                    End If
                    If Trim(txtItemCode.Text) <> "" Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {command_1.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                        Else
                            selectionFormnula += " and  {command_1.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                        End If
                    End If

                    If Trim(txtItemName.Text) <> "" Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {command_1.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                        Else
                            selectionFormnula += " and  {command_1.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                        End If
                    End If

                    If Trim(selectionFormnula) <> "" Then
                        rep.RecordSelectionFormula = selectionFormnula
                    End If

                ElseIf _call_type = enmReportName.RptStockValueCC Then

                    selectionFormnula = ""
                If cmbItemCatId.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {item_stock.fk_ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                    Else
                        selectionFormnula += " and {item_stock.fk_ITEM_CAT_Head_ID}=" & cmbItemCatId.SelectedValue
                    End If

                End If

                If cmb_subCategory.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {item_stock.ITEM_CATEGORY_ID}=" & cmb_subCategory.SelectedValue
                    Else
                        selectionFormnula += " and {item_stock.ITEM_CATEGORY_ID}=" & cmb_subCategory.SelectedValue
                    End If

                End If
                If Trim(txtItemCode.Text) <> "" Then
                    If Trim(selectionFormnula) = "" Then
                        selectionFormnula = " {item_stock.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                    Else
                        selectionFormnula += " and  {item_stock.ITEM_CODE}='" & Trim(txtItemCode.Text) & "'"
                    End If
                End If

                If Trim(txtItemName.Text) <> "" Then
                    If Trim(selectionFormnula) = "" Then
                        selectionFormnula = " {item_stock.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                    Else
                        selectionFormnula += " and  {item_stock.ITEM_NAME}='" & Trim(txtItemName.Text) & "'"
                    End If
                End If
                If Not chk_NotZeroQty.Checked Then
                    If Trim(selectionFormnula) = "" Then
                        selectionFormnula = " {item_stock.STOCK}<> 0"
                    Else
                        selectionFormnula += " and {item_stock.STOCK}<> 0"
                    End If

                End If

                If Trim(selectionFormnula) <> "" Then
                    rep.RecordSelectionFormula = selectionFormnula
                End If
                rep.SetParameterValue("on_date", "'" & dtpDate.Value.Date.ToString("dd-MMM-yyyy") & "'")
                rep.SetParameterValue("calculate_all_data", chkCalculateAllData.Checked)
                rep.SetParameterValue("costcenter_id", v_the_current_Selected_CostCenter_id)

            End If

            'rep.SetDatabaseLogon(gblDataBase_UserName, gblDataBase_Password, gblDataBaseServer_Name, gblDataBase_Name)

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

            'If _call_type = enmReportName.RptStockValue Then
            '    frm_Report.Text = "Stock Value"
            'ElseIf _call_type = enmReportName.RptStockValueCategoryWise Then
            '    frm_Report.Text = "Stock Value Category Wise"
            'Else
            '    frm_Report.Text = "Reports"
            'End If

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


    Private Sub cmbItemCatId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbItemCatId.SelectedIndexChanged
        'If _call_type = enmReportName.RptStockValueCategoryWise Then
        Dim Cat_head_id As String
        If (cmbItemCatId.SelectedValue.ToString() <> "%" And cmbItemCatId.SelectedValue.ToString() <> "0") Then
            Cat_head_id = cmbItemCatId.SelectedValue.ToString()
            cmb_subCategory.Enabled = True
            objCommFunction.ComboBind(cmb_subCategory, "select  '%' as ITEM_CAT_ID,'--All Categories--' as ITEM_CAT_NAME union select  cast(ITEM_CAT_ID as varchar), ITEM_CAT_NAME from ITEM_CATEGORY where fk_ITEM_CAT_Head_ID = " & Cat_head_id & "ORDER BY ITEM_CAT_ID", "ITEM_CAT_NAME", "ITEM_CAT_ID")
        End If
        'End If
    End Sub
End Class
