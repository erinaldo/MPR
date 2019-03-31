Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared


Public Class frm_ReportInput
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

    Private Sub frm_ReportInput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If _call_type = enmReportName.RptSalesummary Or _call_type = enmReportName.RptSalesummaryList Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True

            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            cmbCategoryHead.Visible = False
            lblSubCategory.Visible = False
            cmb_subCategory.Visible = False

            lblCategoryHead.Text = "Customer Name:"
            lblCategoryHead.Visible = True
            cmbCategoryHead.Visible = True
            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ACC_ID,ACC_NAME FROM dbo.ACCOUNT_MASTER WHERE AG_ID=1 order by ACC_NAME"
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ACC_ID") = -1
            Dtrow("ACC_NAME") = "--ALL Customer--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ACC_NAME"
            cmbCategoryHead.ValueMember = "ACC_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0


        End If

        If _call_type = enmReportName.RptPurchaseSummary_itemwise Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True

            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            cmbCategoryHead.Visible = False
            lblSubCategory.Visible = False
            cmb_subCategory.Visible = False

            lblCategoryHead.Text = "Supplier Name:"
            lblCategoryHead.Visible = True
            cmbCategoryHead.Visible = True
            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ACC_ID,ACC_NAME FROM dbo.ACCOUNT_MASTER WHERE AG_ID=2 order by ACC_NAME"
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ACC_ID") = -1
            Dtrow("ACC_NAME") = "--ALL Supplier--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ACC_NAME"
            cmbCategoryHead.ValueMember = "ACC_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
        End If

        If _call_type = enmReportName.RptBrandWiseSale Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True

            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            cmbCategoryHead.Visible = False
            lblSubCategory.Visible = False
            cmb_subCategory.Visible = False

            lblCategoryHead.Text = "Brand Name:"
            lblCategoryHead.Visible = True
            cmbCategoryHead.Visible = True
            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT Pk_LabelDetailId_Num ,LabelItemName_vch  FROM dbo.Label_Items WHERE fk_LabelId_num=1  ORDER BY LabelItemName_vch "
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("Pk_LabelDetailId_Num") = -1
            Dtrow("LabelItemName_vch") = "--ALL Brand--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "LabelItemName_vch"
            cmbCategoryHead.ValueMember = "Pk_LabelDetailId_Num"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0

        End If



        If _call_type = enmReportName.RptMrsItemList Or _call_type = enmReportName.RptMrsdetailList Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            'rBtnMRS.Visible = True
            rBtnMRS.Checked = True
            ComboBind_Enum(cmbstatus, New MRSStatus)
        ElseIf _call_type = enmReportName.RptWastageDetail_ItemWise_cc Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            lblCategoryHead.Visible = True
            cmbCategoryHead.Visible = True
            lblSubCategory.Visible = True
            cmb_subCategory.Visible = True

            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ITEM_CAT_Head_ID ,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER "
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ITEM_CAT_Head_ID") = -1
            Dtrow("ITEM_CAT_Head_NAME") = "--ALL Category Head--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
            cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
        ElseIf _call_type = enmReportName.RptCatheadWise_Consumption_cc Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            lblCategoryHead.Visible = True
            cmbCategoryHead.Visible = True
            lblSubCategory.Visible = True
            cmb_subCategory.Visible = True

            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ITEM_CAT_Head_ID ,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER "
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ITEM_CAT_Head_ID") = -1
            Dtrow("ITEM_CAT_Head_NAME") = "--ALL Category Head--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
            cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0

        ElseIf _call_type = enmReportName.RpttotPurchase_catwise Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            lblCategoryHead.Visible = True
            cmbCategoryHead.Visible = True
            lblSubCategory.Visible = True
            cmb_subCategory.Visible = True

            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ITEM_CAT_Head_ID ,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER "
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ITEM_CAT_Head_ID") = -1
            Dtrow("ITEM_CAT_Head_NAME") = "--ALL Category Head--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
            cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
        ElseIf _call_type = enmReportName.RptIkt_ItemWise_cc Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            lblCategoryHead.Visible = True
            cmbCategoryHead.Visible = True
            lblSubCategory.Visible = True
            cmb_subCategory.Visible = True

            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ITEM_CAT_Head_ID ,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER "
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ITEM_CAT_Head_ID") = -1
            Dtrow("ITEM_CAT_Head_NAME") = "--ALL Category Head--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
            cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
        ElseIf _call_type = enmReportName.RptConsumption_ItemWise_cc Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            lblCategoryHead.Visible = True
            cmbCategoryHead.Visible = True
            lblSubCategory.Visible = True
            cmb_subCategory.Visible = True

            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ITEM_CAT_Head_ID ,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER "
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ITEM_CAT_Head_ID") = -1
            Dtrow("ITEM_CAT_Head_NAME") = "--ALL Category Head--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
            cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
        ElseIf _call_type = enmReportName.RptItemwiseMRS Or _call_type = enmReportName.RptItemwiseMrsMstore Or _call_type = enmReportName.RptMrsDetailMStore Or _call_type = enmReportName.RptMrsItemListMStore Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Checked = True
            lblCategoryHead.Visible = False
            cmbCategoryHead.Visible = False
            lblSubCategory.Visible = False
            cmb_subCategory.Visible = False
        ElseIf _call_type = enmReportName.RptItemWiseMRNWithPO Or _call_type = enmReportName.RptListMRNWithPO Or _call_type = enmReportName.RptWastageDetailList Or _call_type = enmReportName.RptItemwiseWastage Or _call_type = enmReportName.RptItemWiseIndentDetail Or _call_type = enmReportName.RptListofIndent Or _call_type = enmReportName.RptNonMovingItemList Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            cmbCategoryHead.Visible = True
            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ITEM_CAT_Head_ID,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER "
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ITEM_CAT_Head_ID") = -1
            Dtrow("ITEM_CAT_Head_NAME") = "--ALL Category Head--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
            cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
            lblCategoryHead.Text = "Category Name:"
            lblCategoryHead.Visible = True
            lblSubCategory.Visible = True
            cmb_subCategory.Visible = True
        ElseIf _call_type = enmReportName.RptDetailMRNListWithPO Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            cmbCategoryHead.Visible = False
            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ITEM_CAT_Head_ID,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER "
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ITEM_CAT_Head_ID") = -1
            Dtrow("ITEM_CAT_Head_NAME") = "--ALL Category Head--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
            cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
            lblCategoryHead.Text = "Category Name:"
            lblCategoryHead.Visible = False
            lblSubCategory.Visible = False
            cmb_subCategory.Visible = False
        ElseIf _call_type = enmReportName.RptReverseMaterialWithOutPO Or _call_type = enmReportName.RptListofMRNWithOutPO_ItemWiseSuppliers Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            cmbCategoryHead.Visible = True
            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ITEM_CAT_Head_ID,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER "
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ITEM_CAT_Head_ID") = -1
            Dtrow("ITEM_CAT_Head_NAME") = "--ALL Category Head--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
            cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
            lblCategoryHead.Text = "Category Name:"
            lblCategoryHead.Visible = True
            lblSubCategory.Visible = True
            cmb_subCategory.Visible = True
            ''''''''''''''''''''''''''''''''''''''
            ''''''''''Aman''''''''''''''''''''''''
            ''''''''''''''''''''''''''''''''''''''


        ElseIf _call_type = enmReportName.RptListofMRNWithPO_ItemWiseSuppliers Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            cmbCategoryHead.Visible = True
            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ITEM_CAT_Head_ID,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER "
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ITEM_CAT_Head_ID") = -1
            Dtrow("ITEM_CAT_Head_NAME") = "--ALL Category Head--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
            cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
            lblCategoryHead.Text = "Category Name:"
            lblCategoryHead.Visible = True
            lblSubCategory.Visible = True
            cmb_subCategory.Visible = True

            ''''''''''''''''''''''''''''''''''''

        ElseIf _call_type = enmReportName.RptListofMRNWithOutPO_WithoutSupplier Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False

            lblCategoryHead.Text = "Supplier Name:"
            lblCategoryHead.Visible = True
            cmbCategoryHead.Visible = True
            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = "SELECT acc_id,ACC_NAME FROM dbo.ACCOUNT_MASTER ORDER BY ACC_NAME"
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("acc_id") = 0
            Dtrow("ACC_NAME") = "--ALL PURCHASE (With and Without Supplier)--"
            Dt.Rows.InsertAt(Dtrow, 0)
            Dtrow = Dt.NewRow
            Dtrow("acc_id") = -1
            Dtrow("ACC_NAME") = "--LOCAL PURCHASE--"
            Dt.Rows.InsertAt(Dtrow, 1)
            cmbCategoryHead.DisplayMember = "ACC_NAME"
            cmbCategoryHead.ValueMember = "acc_id"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
            lblSubCategory.Visible = False
            cmb_subCategory.Visible = False
        ElseIf _call_type = enmReportName.RptListofMRNWithpo_SupplierWise Then
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False

            lblCategoryHead.Text = "Supplier Name:"
            lblCategoryHead.Visible = True
            cmbCategoryHead.Visible = True
            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = "SELECT acc_id,ACC_NAME FROM dbo.ACCOUNT_MASTER ORDER BY ACC_NAME"
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("acc_id") = 0
            Dtrow("ACC_NAME") = "--ALL PURCHASE (With and Without Supplier)--"
            Dt.Rows.InsertAt(Dtrow, 0)
            Dtrow = Dt.NewRow
            Dtrow("acc_id") = -1
            Dtrow("ACC_NAME") = "--LOCAL PURCHASE--"
            Dt.Rows.InsertAt(Dtrow, 1)
            cmbCategoryHead.DisplayMember = "ACC_NAME"
            cmbCategoryHead.ValueMember = "acc_id"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
            lblSubCategory.Visible = False
            cmb_subCategory.Visible = False
        ElseIf _call_type = enmReportName.RptListofMRNWithOutPO Or _call_type = enmReportName.RptListofMRNDetailWithOutPO Then

            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False

            lblCategoryHead.Text = ""
            lblCategoryHead.Visible = False
            cmbCategoryHead.Visible = False
            'Dim Query As String
            'Dim Dt As DataTable
            'Dim Dtrow As DataRow
            'Query = "SELECT acc_id,ACC_NAME FROM dbo.ACCOUNT_MASTER ORDER BY ACC_NAME"
            'Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            'Dtrow = Dt.NewRow
            'Dtrow("acc_id") = -1
            'Dtrow("ACC_NAME") = "--LOCAL PURCHASE--"
            'Dt.Rows.InsertAt(Dtrow, 0)
            'Dtrow = Dt.NewRow
            'Dtrow("acc_id") = 0
            'Dtrow("ACC_NAME") = "--ALL PURCHASE (With and Without Supplier)--"
            'Dt.Rows.InsertAt(Dtrow, 1)
            'cmbCategoryHead.DisplayMember = "ACC_NAME"
            'cmbCategoryHead.ValueMember = "acc_id"
            'cmbCategoryHead.DataSource = Dt
            'cmbCategoryHead.SelectedIndex = 0
            lblSubCategory.Visible = False
            cmb_subCategory.Visible = False
        ElseIf _call_type = enmReportName.RptItemWiseMRNWithOutPO Or _call_type = enmReportName.RptIndentDetailCategoryHeadWisePrint Or _call_type = enmReportName.RptMrsItemListMStoreCategoryHeadWisePrint Or _call_type = enmReportName.RptItemwiseWastageCategoryHeadWisePrint Then
            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblCategoryHead.Text = "Category Head:"
            lblCategoryHead.Visible = True
            cmbCategoryHead.Visible = True
            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ITEM_CAT_Head_ID,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER "
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ITEM_CAT_Head_ID") = -1
            Dtrow("ITEM_CAT_Head_NAME") = "--ALL Category Head--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
            cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
            lblSubCategory.Visible = True
            cmb_subCategory.Visible = True

        ElseIf _call_type = enmReportName.RptMatIssueItemWiseToCostCenterPrint Then
            Label1.Visible = True
        ElseIf _call_type = enmReportName.RptCategoryHeadWiseIssue Then

            lblstatus.Visible = False
            cmbstatus.Visible = False
            rBtnMRS.Visible = False
            rBtnReqDate.Visible = False
            dtp_FromDate.Visible = True
            dtp_ToDate.Visible = True
            lblCategoryHead.Visible = True
            cmbCategoryHead.Visible = True
            Dim Query As String
            Dim Dt As DataTable
            Dim Dtrow As DataRow
            Query = " SELECT ITEM_CAT_Head_ID,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER "
            Dt = objCommFunction.Fill_DataSet(Query).Tables(0)
            Dtrow = Dt.NewRow
            Dtrow("ITEM_CAT_Head_ID") = -1
            Dtrow("ITEM_CAT_Head_NAME") = "--ALL Category Head--"
            Dt.Rows.InsertAt(Dtrow, 0)
            cmbCategoryHead.DisplayMember = "ITEM_CAT_Head_NAME"
            cmbCategoryHead.ValueMember = "ITEM_CAT_Head_ID"
            cmbCategoryHead.DataSource = Dt
            cmbCategoryHead.SelectedIndex = 0
            lblSubCategory.Visible = False
            cmb_subCategory.Visible = False

            grp_cost_of_issue.Visible = True
            FLX_ISSUE_CAT_HEAD_WISE.Visible = True
            btn_ExportToExcel.Visible = True
        End If
    End Sub

    Private Sub ComboBind_Enum(ByVal cmb As ComboBox, ByVal enm As [Enum])
        Dim Names() As String = [Enum].GetNames(enm.GetType())
        Dim Values1 As Array = [Enum].GetValues(enm.GetType())
        cmb.Items.Clear()
        cmb.DataSource = Values1
    End Sub

    Private Function ValidateData() As Boolean
        ValidateData = False

        If dtp_FromDate.Value.Date > dtp_ToDate.Value.Date Then
            MessageBox.Show("From date can not be greater than To Date.")
            dtp_FromDate.Focus()

            Exit Function
        End If
        If gblSelectedReportName = enmReportName.RptItemLedgerSummary Then
            If cmbstatus.SelectedIndex < 0 Then
                MsgBox("Select Staus Of MRS Item .", vbExclamation, gblMessageHeading)
                cmbstatus.Focus()
                Exit Function
            End If
        End If
        ValidateData = True
    End Function

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Call btn_show_Click(sender, e)
    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick

    End Sub

    Private Sub btn_show_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_show.Click
        Try
            Dim ds As New DataSet
            Dim filepath As String = ""
            Dim dsSubQry As New DataSet
            Dim rep As New ReportDocument()
            Dim selectionFormnula As String = ""
            If ValidateData() = False Then
                Exit Sub
            End If
            If _call_type = enmReportName.RptMrsItemList Then
                filepath = ReportFilePath & "cryMrsItem.rpt"

            ElseIf _call_type = enmReportName.RptMrsdetailList Then
                filepath = ReportFilePath & "cryMrsDetail.rpt"
            ElseIf _call_type = enmReportName.RptWastageDetail_ItemWise_cc Then
                filepath = ReportFilePath & "cryWastageitemwise_cc.rpt"
            ElseIf _call_type = enmReportName.RptCatheadWise_Consumption_cc Then
                filepath = ReportFilePath & "cryConsumption_CategoryHeadWise_cc.rpt"
            ElseIf _call_type = enmReportName.RptIkt_ItemWise_cc Then
                filepath = ReportFilePath & "cryIKT_ItemWise_cc.rpt"
            ElseIf _call_type = enmReportName.RptConsumption_ItemWise_cc Then
                filepath = ReportFilePath & "cryConsumption_ItemWise_cc.rpt"
            ElseIf _call_type = enmReportName.RptItemwiseMRS Then
                filepath = ReportFilePath & "cryItemwiseMrs.rpt"
            ElseIf _call_type = enmReportName.RptItemwiseMrsMstore Then
                filepath = ReportFilePath & "cryItemwiseMrsMStore.rpt"
            ElseIf _call_type = enmReportName.RptMrsDetailMStore Then
                filepath = ReportFilePath & "cryMrsDetailMStore.rpt"
            ElseIf _call_type = enmReportName.RptMrsItemListMStore Then
                filepath = ReportFilePath & "cryMrsItemMStore.rpt"
            ElseIf _call_type = enmReportName.RptWastageDetailList Then
                filepath = ReportFilePath & "cryWastageItemDetail.rpt"
            ElseIf _call_type = enmReportName.RptItemwiseWastage Then
                filepath = ReportFilePath & "cryWastageItemwise.rpt"
            ElseIf _call_type = enmReportName.RptItemWiseIndentDetail Then
                filepath = ReportFilePath & "cryItemWiseIndentS.rpt"
            ElseIf _call_type = enmReportName.RptListofIndent Then
                filepath = ReportFilePath & "cryListofIndent.rpt"
            ElseIf _call_type = enmReportName.RptNonMovingItemList Then
                filepath = ReportFilePath & "NonMovingItems.rpt"
            ElseIf _call_type = enmReportName.RpttotPurchase_catwise Then
                filepath = ReportFilePath & "cryPurchaserpt.rpt"
            ElseIf _call_type = enmReportName.RptPurchaseSummary_itemwise Then
                filepath = ReportFilePath & "cry_Purchase_Summary.rpt"
            ElseIf _call_type = enmReportName.RptListofMRNWithOutPO Then
                filepath = ReportFilePath & "cryListofMRN_Without_PO.rpt"
            ElseIf _call_type = enmReportName.RptListofMRNWithOutPO_WithoutSupplier Then
                filepath = ReportFilePath & "cryListofMRN_without_po_supplier_wise.rpt"
            ElseIf _call_type = enmReportName.RptListofMRNWithpo_SupplierWise Then
                filepath = ReportFilePath & "cryListofMRN_with_po_supplier_wise.rpt"
            ElseIf _call_type = enmReportName.RptItemWiseMRNWithOutPO Then
                filepath = ReportFilePath & "cryItemWiseMRN(without_po).rpt"
            ElseIf _call_type = enmReportName.RptListofMRNDetailWithOutPO Then
                filepath = ReportFilePath & "cryListofMRNDetail(without_po).rpt"
            ElseIf _call_type = enmReportName.RptListMRNWithPO Then
                filepath = ReportFilePath & "cryListofMRN(against_po).rpt"
            ElseIf _call_type = enmReportName.RptDetailMRNListWithPO Then
                filepath = ReportFilePath & "cryListofMRNDetail(against_po).rpt"
            ElseIf _call_type = enmReportName.RptItemWiseMRNWithPO Then
                filepath = ReportFilePath & "cryItemWiseMRN(against_po).rpt"
            ElseIf _call_type = enmReportName.RptSalesummary Then
                filepath = ReportFilePath & "cry_Sale_Invoice_Summary.rpt"
            ElseIf _call_type = enmReportName.RptSalesummaryList Then
                filepath = ReportFilePath & "crySaleInvoiceList.rpt"
            ElseIf _call_type = enmReportName.RptBrandWiseSale Then
                filepath = ReportFilePath & "cry_Brand_Wise_Sale.rpt"
            ElseIf _call_type = enmReportName.RptListofMRNWithOutPO_ItemWiseSuppliers Then
                filepath = ReportFilePath & "cryListofMRN(without_po_ItemWise).rpt"
                '''''''''''''''''''''
                'Done By Aman
                '''''''''''''''''''''
            ElseIf _call_type = enmReportName.RptListofMRNWithPO_ItemWiseSuppliers Then
                filepath = ReportFilePath & "cryListofMRN(with_po_ItemWise).rpt"

                '''''''''''''''''''''
                'Done By Aman
                '''''''''''''''''''''

            ElseIf _call_type = enmReportName.RptIndentDetailCategoryHeadWisePrint Then
                filepath = ReportFilePath & "CyItemWiseIndentsCategoryHeadWise.rpt"
            ElseIf _call_type = enmReportName.RptMrsItemListMStoreCategoryHeadWisePrint Then
                filepath = ReportFilePath & "CryItemwiseMrsMStoreCategoryHeadWise.rpt"
            ElseIf _call_type = enmReportName.RptItemwiseWastageCategoryHeadWisePrint Then
                filepath = ReportFilePath & "CryWastageItemwiseCategoryHeadWise.rpt"
            ElseIf _call_type = enmReportName.RptReverseMaterialWithOutPO Then
                filepath = ReportFilePath & "cryListofReverseMaterial(without_po).rpt"
            End If

            ' Dim rep As New MyReportDocument(filepath)

            If Not _call_type = enmReportName.RptCategoryHeadWiseIssue Then


                rep.Load(filepath)
                'rep.SetDatabaseLogon("sa", "DataBase@123", "afblmms", "afbl_mms")

                Dim connection As New ConnectionInfo()


                connection.DatabaseName = gblDataBase_Name
                connection.ServerName = gblDataBaseServer_Name
                connection.UserID = gblDataBase_UserName
                connection.Password = gblDataBase_Password



                ' First we assign the connection to all tables in the main report
                '
                For Each table As CrystalDecisions.CrystalReports.Engine.Table In rep.Database.Tables
                    AssignTableConnection(table, connection)
                Next
            End If


            'rep.SetDatabaseLogon("sa", "DataBase", "inderjeet\sql2005", "afbl_mms")


            If _call_type = enmReportName.RptMrsItemList Or _call_type = enmReportName.RptMrsdetailList Then
                If rBtnMRS.Checked = True Then
                    rep.RecordSelectionFormula = "{mrs.MRS_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrs.MRS_DATE}<=#" & dtp_ToDate.Value.Date & "# and {mrs.MRS_Status}=" & cmbstatus.SelectedValue & " and {mrs.CostCenter_Id}=" & v_the_current_Selected_CostCenter_id
                Else
                    rep.RecordSelectionFormula = "{mrs.REQ_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrs.REQ_DATE}<=#" & dtp_ToDate.Value.Date & "# and {mrs.MRS_Status}=" & cmbstatus.SelectedValue & " and {mrs.CostCenter_Id}=" & v_the_current_Selected_CostCenter_id
                End If

            ElseIf _call_type = enmReportName.RptItemwiseMRS Then
                frm_Report.Text = "ITEM Wise MRS"
                If rBtnMRS.Checked = True Then
                    rep.RecordSelectionFormula = "{mrs.MRS_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrs.MRS_DATE}<=#" & dtp_ToDate.Value.Date & "# and {mrs.CostCenter_Id}=" & v_the_current_Selected_CostCenter_id
                Else
                    rep.RecordSelectionFormula = "{mrs.REQ_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrs.REQ_DATE}<=#" & dtp_ToDate.Value.Date & "# and {mrs.CostCenter_Id}=" & v_the_current_Selected_CostCenter_id
                End If


            ElseIf _call_type = enmReportName.RptWastageDetail_ItemWise_cc Then
                frm_Report.Text = "Item Wise Wastage Detail"
                If cmbCategoryHead.SelectedIndex <> 0 Then
                    If cmb_subCategory.SelectedIndex <> 0 Then
                        rep.RecordSelectionFormula = "{ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID}= " & cmbCategoryHead.SelectedValue & " and {mrs.item_category_id} = " & cmb_subCategory.SelectedValue
                    Else
                        rep.RecordSelectionFormula = "{ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID} = " & cmbCategoryHead.SelectedValue
                    End If
                End If
            ElseIf _call_type = enmReportName.RptCatheadWise_Consumption_cc Then
                frm_Report.Text = "Category Head Wise Consumption Detail"
                If cmbCategoryHead.SelectedIndex <> 0 Then
                    If cmb_subCategory.SelectedIndex <> 0 Then
                        rep.RecordSelectionFormula = "{ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID}= " & cmbCategoryHead.SelectedValue & " and {ITEM_CATEGORY.ITEM_CAT_ID} = " & cmb_subCategory.SelectedValue
                    Else
                        rep.RecordSelectionFormula = "{ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID} = " & cmbCategoryHead.SelectedValue
                    End If
                End If

            ElseIf _call_type = enmReportName.RptIkt_ItemWise_cc Then
                frm_Report.Text = "Item Wise Inter Kitchen Transfer"
                If cmbCategoryHead.SelectedIndex <> 0 Then
                    If cmb_subCategory.SelectedIndex <> 0 Then
                        rep.RecordSelectionFormula = "{ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID}= " & cmbCategoryHead.SelectedValue & " and {wastage.ITEM_CATEGORY_ID} = " & cmb_subCategory.SelectedValue
                    Else
                        rep.RecordSelectionFormula = "{ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID} = " & cmbCategoryHead.SelectedValue
                    End If
                End If
            ElseIf _call_type = enmReportName.RptConsumption_ItemWise_cc Then
                frm_Report.Text = "Item Wise Consumption"
                If cmbCategoryHead.SelectedIndex <> 0 Then
                    If cmb_subCategory.SelectedIndex <> 0 Then
                        rep.RecordSelectionFormula = "{ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID}= " & cmbCategoryHead.SelectedValue & " and {wastage.ITEM_CATEGORY_ID} = " & cmb_subCategory.SelectedValue
                    Else
                        rep.RecordSelectionFormula = "{ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID} = " & cmbCategoryHead.SelectedValue
                    End If
                End If
            ElseIf _call_type = enmReportName.RptItemwiseMrsMstore Or _call_type = enmReportName.RptMrsDetailMStore Or _call_type = enmReportName.RptMrsItemListMStore Then
                If _call_type = enmReportName.RptItemwiseMrsMstore Then frm_Report.Text = "ITEM Wise MRS Main Store"
                If _call_type = enmReportName.RptMrsItemListMStore Then frm_Report.Text = "Item List Main Store"
                If _call_type = enmReportName.RptMrsDetailMStore Then frm_Report.Text = "Item Detail Main Store"
                If rBtnMRS.Checked = True Then
                    rep.RecordSelectionFormula = "{mrs.MRS_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrs.MRS_DATE}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                Else
                    rep.RecordSelectionFormula = "{mrs.REQ_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrs.REQ_DATE}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                End If
            ElseIf _call_type = enmReportName.RptWastageDetailList Or _call_type = enmReportName.RptItemwiseWastage Then
                If _call_type = enmReportName.RptWastageDetailList Then frm_Report.Text = "Wastage Items Detail"
                If _call_type = enmReportName.RptItemwiseWastage Then frm_Report.Text = "Item Wise Wastage Detail"
                rep.RecordSelectionFormula = "{wastage.Wastage_DATE}>=#" & dtp_FromDate.Value.Date & "# and {wastage.Wastage_DATE}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                ''for Indent
            ElseIf _call_type = enmReportName.RptItemWiseIndentDetail Then
                If _call_type = enmReportName.RptItemWiseIndentDetail Then frm_Report.Text = "Item Wise Indent Detail"
                rep.RecordSelectionFormula = "{command_1.INDENT_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.INDENT_DATE}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue


            ElseIf _call_type = enmReportName.RptListofIndent Then
                If _call_type = enmReportName.RptListofIndent Then frm_Report.Text = "List of Indents"
                rep.RecordSelectionFormula = "{command_1.INDENT_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.INDENT_DATE}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue

            ElseIf _call_type = enmReportName.RptListofMRNWithOutPO Then
                If _call_type = enmReportName.RptListofMRNWithOutPO Then frm_Report.Text = "List of MRN(without_po)"
                If cmbCategoryHead.SelectedValue = -1 Then
                    'rep.RecordSelectionFormula = "{command_1.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "#"
                    ' rep.RecordSelectionFormula = "{mrn_list.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "#  and {mrn_list.acc_id}=-1"
                ElseIf cmbCategoryHead.SelectedValue = 0 Then
                    'rep.RecordSelectionFormula = "{mrn_list.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "#"
                    'rep.RecordSelectionFormula = "{command_1.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "#  and {command_1.acc_id}=" & cmbCategoryHead.SelectedValue
                Else
                    'rep.RecordSelectionFormula = "{mrn_list.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "#  and {mrn_list.acc_id}=" & cmbCategoryHead.SelectedValue
                End If


            ElseIf _call_type = enmReportName.RptListMRNWithPO Then
                If _call_type = enmReportName.RptListMRNWithPO Then frm_Report.Text = "List of MRN(With Purchase Order)"
                rep.RecordSelectionFormula = "{command_1.Receipt_Date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.Receipt_Date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue

            ElseIf _call_type = enmReportName.RptDetailMRNListWithPO Then
                If _call_type = enmReportName.RptDetailMRNListWithPO Then frm_Report.Text = "Detail MRN List(With Purchase Order)"
                'rep.RecordSelectionFormula = "{mrnwithpo.Receipt_Date}>=#" & dtp_FromDate.Value.Date & "# and {mrnwithpo.Receipt_Date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue

            ElseIf _call_type = enmReportName.RptItemWiseMRNWithPO Then
                If _call_type = enmReportName.RptItemWiseMRNWithPO Then

                    frm_Report.Text = "Item Wise MRN(With Purchase Order)"

                    If Trim(selectionFormnula) = "" Then
                        ' selectionFormnula = "{command_1.Receipt_Date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.Receipt_Date}<=#" & dtp_ToDate.Value.Date & "#"
                    Else
                        ' selectionFormnula += " and {command_1.Receipt_Date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.Receipt_Date}<=#" & dtp_ToDate.Value.Date & "#"
                    End If
                    If cmbCategoryHead.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {command_1.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                        Else
                            selectionFormnula += " and {command_1.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                        End If
                    End If
                    If cmb_subCategory.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {command_1.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                        Else
                            selectionFormnula += " and {command_1.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                        End If

                    End If

                    '    If cmbCategoryHead.SelectedValue = -1 Then
                    '        rep.RecordSelectionFormula = "{command_1.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                    '    Else
                    '        rep.RecordSelectionFormula = "{command_1.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "# and {command_1.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                    '    End If
                    '

                    'End If

                End If
                rep.RecordSelectionFormula = selectionFormnula
                'End If

                'rep.RecordSelectionFormula = "{command_1.Receipt_Date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.Receipt_Date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
            ElseIf _call_type = enmReportName.RptNonMovingItemList Then
                If _call_type = enmReportName.RptNonMovingItemList Then

                    frm_Report.Text = "Non Moving Item List"

                    If Trim(selectionFormnula) = "" Then
                        ' selectionFormnula = "{command_1.Receipt_Date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.Receipt_Date}<=#" & dtp_ToDate.Value.Date & "#"
                    Else
                        ' selectionFormnula += " and {command_1.Receipt_Date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.Receipt_Date}<=#" & dtp_ToDate.Value.Date & "#"
                    End If
                    If cmbCategoryHead.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {item_stock.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                        Else
                            selectionFormnula += " and {item_stock.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                        End If
                    End If
                    If cmb_subCategory.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {item_stock.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                        Else
                            selectionFormnula += " and {item_stock.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                        End If

                    End If

                    '    If cmbCategoryHead.SelectedValue = -1 Then
                    '        rep.RecordSelectionFormula = "{command_1.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                    '    Else
                    '        rep.RecordSelectionFormula = "{command_1.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "# and {command_1.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                    '    End If
                    '

                    'End If

                End If
                rep.RecordSelectionFormula = selectionFormnula

            ElseIf _call_type = enmReportName.RpttotPurchase_catwise Then
                If _call_type = enmReportName.RpttotPurchase_catwise Then

                    frm_Report.Text = "Purchase Report(Category Wise)"

                    If cmbCategoryHead.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {command_1.head_cat_id}=" & cmbCategoryHead.SelectedValue
                        Else
                            selectionFormnula += " and {command_1.head_cat_id}=" & cmbCategoryHead.SelectedValue
                        End If
                    End If
                    If cmb_subCategory.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {command_1.cat_id}=" & cmb_subCategory.SelectedValue
                        Else
                            selectionFormnula += " and {command_1.cat_id}=" & cmb_subCategory.SelectedValue
                        End If

                    End If

                    '    If cmbCategoryHead.SelectedValue = -1 Then
                    '        rep.RecordSelectionFormula = "{command_1.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                    '    Else
                    '        rep.RecordSelectionFormula = "{command_1.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "# and {command_1.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                    '    End If
                    '

                    'End If

                End If
                rep.RecordSelectionFormula = selectionFormnula
            ElseIf _call_type = enmReportName.RptItemWiseMRNWithOutPO Then

                If _call_type = enmReportName.RptItemWiseMRNWithOutPO Then
                    frm_Report.Text = "Item Wise MRN(without_po)"
                    If Trim(selectionFormnula) = "" Then
                        'selectionFormnula = "{mrn_list.Received_Date}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.received_date}<=#" & dtp_ToDate.Value.Date & "#"
                    Else
                        'selectionFormnula += " and {mrn_list.received_date}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.received_date}<=#" & dtp_ToDate.Value.Date & "#"
                    End If
                    If cmbCategoryHead.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then
                            selectionFormnula = " {ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                        Else
                            selectionFormnula += " and {ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                        End If
                    End If
                    If cmb_subCategory.SelectedIndex > 0 Then
                        If Trim(selectionFormnula) = "" Then

                            selectionFormnula = " {ITEM_CATEGORY.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                        Else
                            selectionFormnula += " and {ITEM_CATEGORY.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                        End If

                    End If

                    '    If cmbCategoryHead.SelectedValue = -1 Then
                    '        rep.RecordSelectionFormula = "{command_1.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                    '    Else
                    '        rep.RecordSelectionFormula = "{command_1.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "# and {command_1.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                    '    End If
                    '

                    'End If

                End If
                rep.RecordSelectionFormula = selectionFormnula


            ElseIf _call_type = enmReportName.RptListofMRNWithOutPO_WithoutSupplier Then

                If _call_type = enmReportName.RptListofMRNWithOutPO_WithoutSupplier Then frm_Report.Text = "Supplier Wise MRN(Without PO)"
                ' rep.RecordSelectionFormula = "{command_1.received_date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.received_date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                If Trim(selectionFormnula) = "" Then
                    selectionFormnula = "{mrn_list.received_date}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.received_date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                Else
                    selectionFormnula += " and {mrn_list.received_date}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.received_date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                End If
                If cmbCategoryHead.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {mrn_list.Vendor_ID}=" & cmbCategoryHead.SelectedValue
                    Else
                        selectionFormnula += " and {mrn_list.Vendor_ID}=" & cmbCategoryHead.SelectedValue
                    End If

                End If

                rep.RecordSelectionFormula = selectionFormnula


            ElseIf _call_type = enmReportName.RptStock_Adjustment Then

                If _call_type = enmReportName.RptStock_Adjustment Then frm_Report.Text = "Supplier Wise MRN(Without PO)"
                ' rep.RecordSelectionFormula = "{command_1.received_date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.received_date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                If Trim(selectionFormnula) = "" Then
                    selectionFormnula = "{mrn_list.receipt_date}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.receipt_date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                Else
                    selectionFormnula += " and {mrn_list.receipt_date}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.receipt_date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                End If
                If cmbCategoryHead.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {mrn_list.Vendor_ID}=" & cmbCategoryHead.SelectedValue
                    Else
                        selectionFormnula += " and {mrn_list.Vendor_ID}=" & cmbCategoryHead.SelectedValue
                    End If

                End If

                rep.RecordSelectionFormula = selectionFormnula
            ElseIf _call_type = enmReportName.RptListofMRNWithOutPO_ItemWiseSuppliers Then

                If _call_type = enmReportName.RptListofMRNWithOutPO_ItemWiseSuppliers Then frm_Report.Text = "Supplier Wise and Item Wise MRN(Without PO)"
                ' rep.RecordSelectionFormula = "{command_1.received_date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.received_date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                If Trim(selectionFormnula) = "" Then
                    'selectionFormnula = "{mrn_list.Received_Date}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.Received_Date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                Else
                    'selectionFormnula += " and {mrn_list.Received_Date}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.Received_Date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                End If
                If cmbCategoryHead.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                    Else
                        selectionFormnula += " and {ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                    End If

                End If
                If cmb_subCategory.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {ITEM_CATEGORY.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                    Else
                        selectionFormnula += " and {ITEM_CATEGORY.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                    End If

                End If
                rep.RecordSelectionFormula = selectionFormnula

                ''''''''''''''''''''''''''''''''''''''''''''''
                ''''''''''''''''''''''''''Aman''''''''''''''''
                ''''''''''''''''''''''''''''''''''''''''''''''

            ElseIf _call_type = enmReportName.RptListofMRNWithPO_ItemWiseSuppliers Then

                If _call_type = enmReportName.RptListofMRNWithPO_ItemWiseSuppliers Then frm_Report.Text = "Supplier Wise and Item Wise MRN(With PO)"
                ' rep.RecordSelectionFormula = "{command_1.received_date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.received_date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                If Trim(selectionFormnula) = "" Then
                    'selectionFormnula = "{mrn_list.Received_Date}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.Received_Date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                Else
                    'selectionFormnula += " and {mrn_list.Received_Date}>=#" & dtp_FromDate.Value.Date & "# and {mrn_list.Received_Date}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                End If
                If cmbCategoryHead.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                    Else
                        selectionFormnula += " and {ITEM_CATEGORY_HEAD_MASTER.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                    End If

                End If
                If cmb_subCategory.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {ITEM_CATEGORY.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                    Else
                        selectionFormnula += " and {ITEM_CATEGORY.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                    End If

                End If
                rep.RecordSelectionFormula = selectionFormnula

                ''''''''''''''''''''''''''''''''''''''''''''''



            ElseIf _call_type = enmReportName.RptListofMRNDetailWithOutPO Then
                If _call_type = enmReportName.RptListofMRNDetailWithOutPO Then frm_Report.Text = "List Of MRN Detail(without_po)"

                If Trim(selectionFormnula) = "" Then
                    'selectionFormnula = "{mrn.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrn.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                Else
                    'selectionFormnula += " and {mrn.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrn.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                End If
                If cmbCategoryHead.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {mrn.fk_ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                    Else
                        selectionFormnula += " and {mrn.fk_ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                    End If

                End If
                If cmb_subCategory.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {mrn.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                    Else
                        selectionFormnula += " and {mrn.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                    End If

                End If
                rep.RecordSelectionFormula = selectionFormnula
                'ElseIf _call_type = enmReportName.RptIndentDetailCategoryHeadWisePrint Then
                '    If _call_type = enmReportName.RptIndentDetailCategoryHeadWisePrint Then
                '        frm_Report.Text = "Item Wise Indent Detail Category Head Wise."
                '        If cmbCategoryHead.SelectedValue = -1 Then
                '            rep.RecordSelectionFormula = "{command_1.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "#" ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                '        Else
                '            rep.RecordSelectionFormula = "{command_1.RECEIVED_DATE}>=#" & dtp_FromDate.Value.Date & "# and {command_1.RECEIVED_DATE}<=#" & dtp_ToDate.Value.Date & "# and {command_1.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue ' and {mrs.MRS_Status}=" & cmbstatus.SelectedValue
                '        End If

                '    End If

            ElseIf _call_type = enmReportName.RptListofMRNWithpo_SupplierWise Then
                If cmbCategoryHead.SelectedIndex > 0 Then
                    If Trim(selectionFormnula) = "" Then

                        selectionFormnula = " {mrn_list.vendor_id}=" & cmbCategoryHead.SelectedValue
                    Else
                        selectionFormnula += " and {mrn_list.vendor_id}=" & cmbCategoryHead.SelectedValue
                    End If
                End If

                rep.RecordSelectionFormula = selectionFormnula
            ElseIf _call_type = enmReportName.RptMrsItemListMStoreCategoryHeadWisePrint Then
                If _call_type = enmReportName.RptMrsItemListMStoreCategoryHeadWisePrint Then
                    frm_Report.Text = "Item Wise MRS Detail Category Head Wise."
                    If cmbCategoryHead.SelectedValue = -1 Then
                        rep.RecordSelectionFormula = "{mrs.MRS_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrs.MRS_DATE}<=#" & dtp_ToDate.Value.Date & "#"
                    Else
                        rep.RecordSelectionFormula = "{mrs.MRS_DATE}>=#" & dtp_FromDate.Value.Date & "# and {mrs.MRS_DATE}<=#" & dtp_ToDate.Value.Date & "# and {mrs.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                    End If
                End If
            ElseIf _call_type = enmReportName.RptItemwiseWastageCategoryHeadWisePrint Then
                If _call_type = enmReportName.RptItemwiseWastageCategoryHeadWisePrint Then
                    frm_Report.Text = "Item Wise Wastage Detail Category Head Wise."
                    If cmbCategoryHead.SelectedValue = -1 Then
                        rep.RecordSelectionFormula = "{wastage.Wastage_DATE}>=#" & dtp_FromDate.Value.Date & "# and {wastage.Wastage_DATE}<=#" & dtp_ToDate.Value.Date & "#" '
                    Else
                        rep.RecordSelectionFormula = "{wastage.Wastage_DATE}>=#" & dtp_FromDate.Value.Date & "# and {wastage.Wastage_DATE}<=#" & dtp_ToDate.Value.Date & "# and {wastage.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                    End If
                End If

            ElseIf _call_type = enmReportName.RptReverseMaterialWithOutPO Then
                If _call_type = enmReportName.RptReverseMaterialWithOutPO Then
                    frm_Report.Text = "List Of Reverse Material WithOut PO."
                    If cmbCategoryHead.SelectedValue = -1 AndAlso cmb_subCategory.SelectedValue = "%" Then
                        rep.RecordSelectionFormula = "{command_1.Reverse_Date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.Reverse_Date}<=#" & dtp_ToDate.Value.Date & "#" '
                    ElseIf cmb_subCategory.SelectedValue = "%" Then
                        rep.RecordSelectionFormula = "{command_1.Reverse_Date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.Reverse_Date}<=#" & dtp_ToDate.Value.Date & "# and {command_1.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue
                    Else
                        rep.RecordSelectionFormula = "{command_1.Reverse_Date}>=#" & dtp_FromDate.Value.Date & "# and {command_1.Reverse_Date}<=#" & dtp_ToDate.Value.Date & "# and {command_1.ITEM_CAT_Head_ID}=" & cmbCategoryHead.SelectedValue & " and {command_1.ITEM_CAT_ID}=" & cmb_subCategory.SelectedValue
                    End If

                End If
            ElseIf _call_type = enmReportName.RptCategoryHeadWiseIssue Then
                Dim datast As DataSet
                Dim para As String
                Dim values As String
                para = "@Cat_Head_ID,@From_Date,@To_Date"
                Dim catheadid As String
                If (cmbCategoryHead.SelectedValue.ToString() = "-1") Then
                    catheadid = "%"
                Else
                    catheadid = cmbCategoryHead.SelectedValue
                End If
                values = catheadid & "," & dtp_FromDate.Value.Date.ToString() & "," & dtp_ToDate.Value.ToString()

                datast = objCommFunction.fill_Data_set("Get_Cost_Of_Issue_Cat_Head_Wise", para, values)
                If (datast.Tables.Count > 0) Then
                    FLX_ISSUE_CAT_HEAD_WISE.DataSource = datast.Tables(0)
                End If

            End If

            Dim CrExportOptions As ExportOptions
            Dim CrFormatTypeOptions As New ExcelFormatOptions

            With CrFormatTypeOptions
                .ExcelTabHasColumnHeadings = True
            End With

            CrExportOptions = rep.ExportOptions

            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.Excel
                .FormatOptions = CrFormatTypeOptions
            End With

            If Not _call_type = enmReportName.RptCategoryHeadWiseIssue And Not _call_type = enmReportName.RptWastageDetail_ItemWise_cc And Not _call_type = enmReportName.RptIkt_ItemWise_cc And Not _call_type = enmReportName.RptConsumption_ItemWise_cc And Not _call_type = enmReportName.RptCatheadWise_Consumption_cc And Not _call_type = enmReportName.RptNonMovingItemList And Not _call_type = enmReportName.RptSalesummary And Not _call_type = enmReportName.RptSalesummaryList And Not _call_type = enmReportName.RptNonMovingItemList And Not _call_type = enmReportName.RptBrandWiseSale And Not _call_type = enmReportName.RptPurchaseSummary_itemwise Then
                rep.SetParameterValue("PFromDate", dtp_FromDate.Value.Date)
                rep.SetParameterValue("PToDate", dtp_ToDate.Value.Date)
                'rep.SetParameterValue("Condition", " REQ_DATE ='" & dtp_FromDate.Value.Date & "' and REQ_DATE ='" & dtp_ToDate.Value.Date & "' and MRS_MAIN_STORE_MASTER.MRS_Status = " & v_the_current_division_id & "")
                ' 

                frm_Report.cryViewer.ReportSource = rep
                frm_Report.Show()
            End If
            If _call_type = enmReportName.RptWastageDetail_ItemWise_cc Or _call_type = enmReportName.RptIkt_ItemWise_cc Or _call_type = enmReportName.RptConsumption_ItemWise_cc Then
                rep.SetParameterValue("Pfromdate", dtp_FromDate.Value.Date)
                rep.SetParameterValue("Ptodate", dtp_ToDate.Value.Date)
                rep.SetParameterValue("CostCenter_id", v_the_current_Selected_CostCenter_id)
                frm_Report.cryViewer.ReportSource = rep
                frm_Report.Show()
            End If


            If _call_type = enmReportName.RptCatheadWise_Consumption_cc Then
                rep.SetParameterValue("@fromdate", dtp_FromDate.Value.Date)
                rep.SetParameterValue("@todate", dtp_ToDate.Value.Date)
                rep.SetParameterValue("@Cc_id", v_the_current_Selected_CostCenter_id)
                frm_Report.cryViewer.ReportSource = rep
                frm_Report.Show()
            End If

            If _call_type = enmReportName.RptNonMovingItemList Then
                rep.SetParameterValue("PFromDate", "'" & dtp_FromDate.Value.Date.ToString("dd-MMM-yyyy") & "'")
                rep.SetParameterValue("PToDate", "'" & dtp_ToDate.Value.Date.ToString("dd-MMM-yyyy") & "'")
                frm_Report.cryViewer.ReportSource = rep
                frm_Report.Show()
            End If

            If _call_type = enmReportName.RptSalesummary Or _call_type = enmReportName.RptSalesummaryList Or _call_type = enmReportName.RptPurchaseSummary_itemwise Then
                rep.SetParameterValue("From", Convert.ToDateTime(dtp_FromDate.Value.Date.ToString("dd-MMM-yyyy")))
                rep.SetParameterValue("To", Convert.ToDateTime(dtp_ToDate.Value.Date.ToString("dd-MMM-yyyy")))

                If cmbCategoryHead.SelectedValue = -1 Then
                    rep.SetParameterValue("ACCID", "NULL")
                Else
                    rep.SetParameterValue("ACCID", Convert.ToString(cmbCategoryHead.SelectedValue))
                End If

                frm_Report.cryViewer.ReportSource = rep
                frm_Report.Show()
            End If

            If _call_type = enmReportName.RptBrandWiseSale Then
                rep.SetParameterValue("From", Convert.ToDateTime(dtp_FromDate.Value.Date.ToString("dd-MMM-yyyy")))
                rep.SetParameterValue("To", Convert.ToDateTime(dtp_ToDate.Value.Date.ToString("dd-MMM-yyyy")))

                If cmbCategoryHead.SelectedValue = -1 Then
                    rep.SetParameterValue("BrandID", "NULL")
                Else
                    rep.SetParameterValue("BrandID", Convert.ToString(cmbCategoryHead.SelectedValue))
                End If

                frm_Report.cryViewer.ReportSource = rep
                frm_Report.Show()
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

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

    Private Sub cmbCategoryHead_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCategoryHead.SelectedIndexChanged
        Dim Cat_head_id As String
        If (cmbCategoryHead.SelectedValue.ToString() <> "%" And cmbCategoryHead.SelectedValue.ToString() <> "0") Then
            Cat_head_id = cmbCategoryHead.SelectedValue.ToString()
            cmb_subCategory.Enabled = True
            objCommFunction.ComboBind(cmb_subCategory, "select  '%' as ITEM_CAT_ID,'--All Categories--' as ITEM_CAT_NAME union select  cast(ITEM_CAT_ID as varchar), ITEM_CAT_NAME from ITEM_CATEGORY where fk_ITEM_CAT_Head_ID = " & Cat_head_id & "ORDER BY ITEM_CAT_ID", "ITEM_CAT_NAME", "ITEM_CAT_ID")
        End If
    End Sub

    Private Sub ExportGridToExcel()

    End Sub

    Private Sub btn_ExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ExportToExcel.Click
        Try
            SaveFileDialog1.ShowDialog()
            FLX_ISSUE_CAT_HEAD_WISE.SaveExcel(SaveFileDialog1.FileName, "Sheet1", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class
