Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports System.Data

Public Class frm_material_rec_against_PO
    Implements IForm

    Public I As Integer
    Dim obj As New CommonClass
    Dim ds As New DataSet
    Dim FLXGRD_PO_Items_Rowindex As Int16
    Dim v_frm_Batch_Entry_Qty As frm_Batch_Entry_Qty
    Dim dtable_Item_List As DataTable
    Dim prop As material_rec_against_PO.cls_Material_rec_Against_PO_Prop
    Dim Master As material_rec_against_PO.cls_material_recieved_against_po_master
    Dim flag As String
    Dim receive_Id As Integer
    Dim datatbl_NonStockable_Items As DataTable
    Dim Open_Po_Qty As Boolean
    Dim _rights As Form_Rights

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            new_initilization()
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_Indent_Master")
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub new_initilization()
        TabControl1.SelectTab(1)
        cmb_MRNAgainst.SelectedValue = "1"
        FLXGRD_PO_Items.DataSource = Nothing
        FLXGRD_PO_NON_STOCKABLEITEMS.DataSource = Nothing
        table_style()
        ' Grid_Formatting()
        cmbPurchaseOrders.SelectedValue = 0
        dtpReceiveDate.Text = Now.ToString("dd-MMM-yyyy")
        flag = "save"
        txt_Invoice_No.Text = ""
        lblcustid.Visible = False
        lblexciseamt.Text = "0.00"
        lblgrossamt.Text = "0.00"
        lblvatamt.Text = "0.00"
        lblcessamt.Text = "0.00"
        txtAmount.Text = "0.00"
        txtotherchrgs.Text = "0.00"
        txtdiscount.Text = "0.00"
        txtCashDiscount.Text = "0.00"
        lblnetamt.Text = "0.00"
        chk_ApplyTax.Checked = False
        cmbITCEligibility.SelectedIndex = 0
        lblGSTDetail.Text = ""

        If FLXGRD_PO_Items.DataSource IsNot Nothing Then
            'SetGstLabels()
        End If

    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        FLXGRD_PO_Items.FinishEditing()
        Dim cmd As SqlCommand

        prop = New material_rec_against_PO.cls_Material_rec_Against_PO_Prop
        Master = New material_rec_against_PO.cls_material_recieved_against_po_master

        If txt_Invoice_No.Text <> "" Then
            Dim invoicecount = Convert.ToInt32(obj.ExecuteScalar("SELECT COUNT(Receipt_ID) FROM dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER INNER JOIN dbo.PO_MASTER ON dbo.PO_MASTER.PO_ID = dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER.PO_ID WHERE   PO_SUPP_ID in ( SELECT po_supp_id FROM dbo.PO_MASTER WHERE po_id = " & cmbPurchaseOrders.SelectedValue & " ) AND Invoice_No = '" & txt_Invoice_No.Text & "'"))
            If invoicecount > 0 Then
                MsgBox("Invoice No. cannot be same for the single supplier.")
                Exit Sub
            End If
        Else
            MsgBox("Please Enter Invoice No. first.")
            Exit Sub
        End If

        If Open_Po_Qty = False Then
            Dim iRowCount As Int32
            Dim iRow As Int32
            Dim dtable As New DataTable
            dtable = FLXGRD_PO_NON_STOCKABLEITEMS.DataSource
            If Not dtable Is Nothing Then
                iRowCount = dtable.Rows.Count - 1
                For iRow = 0 To iRowCount

                    If dtable.Rows(iRow)("BATCH_QTY") = 0 Then
                        MsgBox("Batch Qty Cannot be Empty")
                        Exit Sub
                    End If

                    If dtable.Rows(iRow)("BATCH_QTY") > dtable.Rows(iRow)("PO_QTY") Then
                        MsgBox("Batch Qty Cannot be greater than PO QTY")
                        Exit Sub
                    End If
                Next iRow
            End If

            dtable = FLXGRD_PO_Items.DataSource
            If Not dtable Is Nothing Then
                iRowCount = dtable.Rows.Count - 1
                For iRow = 0 To iRowCount
                    If dtable.Rows(iRow)("BATCH_QTY") = 0 Then
                        MsgBox("Batch Qty Cannot be Empty")
                        Exit Sub
                    End If

                    If dtable.Rows(iRow)("BATCH_QTY") > dtable.Rows(iRow)("PO_QTY") Then
                        MsgBox("Batch Qty Cannot be greater than PO QTY")
                        Exit Sub
                    End If
                Next iRow
            End If
        End If
        Try
            If flag = "save" Then

                Dim MRN_Code As String
                Dim MRN_No As Integer

                Dim ds As New DataSet()
                ds = obj.fill_Data_set("GET_MRN_NO", "@DIV_ID", v_the_current_division_id)
                If ds.Tables(0).Rows.Count = 0 Then
                    MsgBox("MRN series does not exists", MsgBoxStyle.Information, gblMessageHeading)
                    ds.Dispose()
                    Exit Sub
                Else
                    If ds.Tables(0).Rows(0)(0).ToString() = "-1" Then
                        MsgBox("MRN series does not exists", MsgBoxStyle.Information, gblMessageHeading)
                        ds.Dispose()
                        Exit Sub
                    ElseIf ds.Tables(0).Rows(0)(0).ToString() = "-2" Then
                        MsgBox("MRN series has been completed", MsgBoxStyle.Information, gblMessageHeading)
                        ds.Dispose()
                        Exit Sub
                    Else
                        MRN_Code = ds.Tables(0).Rows(0)(0).ToString()
                        MRN_No = Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString()) + 1
                        ds.Dispose()
                    End If
                End If


                cmd = obj.MyCon_BeginTransaction
                Dim RECEIPT_ID As Integer
                RECEIPT_ID = Convert.ToInt32(obj.getMaxValue("RECEIPT_ID", "MATERIAL_RECEIVED_AGAINST_PO_MASTER"))
                prop.Receipt_ID = RECEIPT_ID
                prop.Receipt_No = RECEIPT_ID
                prop.Receipt_Code = GetReceiptCode()
                prop.Po_ID = Convert.ToInt32(cmbPurchaseOrders.SelectedValue)
                prop.Receipt_Date = Now 'Convert.ToDateTime(dtpReceiveDate.Text).ToString()
                prop.Invoice_Date = Convert.ToDateTime(dt_Invoice_Date.Value)
                prop.Invoice_No = txt_Invoice_No.Text
                prop.Remarks = txt_Remarks.Text
                prop.MRN_NO = MRN_No
                prop.MRN_PREFIX = MRN_Code
                prop.Created_By = v_the_current_logged_in_user_name
                prop.Creation_Date = Now
                prop.Modified_By = v_the_current_logged_in_user_name
                prop.Modification_Date = Now
                prop.Division_ID = v_the_current_division_id
                prop.mrn_status = Convert.ToInt32(GlobalModule.MRNStatus.normal)
                prop.MRNCompanies_ID = Convert.ToInt16(cmb_MRNAgainst.SelectedValue)
                prop.freight = Convert.ToDouble(txtAmount.Text)
                prop.Other_Charges = Convert.ToDouble(txtotherchrgs.Text)
                prop.Discount_amt = Convert.ToDouble(txtdiscount.Text)
                prop.GROSS_AMOUNT = (Convert.ToDouble(lblgrossamt.Text) - Convert.ToDouble(txtAmount.Text))
                prop.GST_AMOUNT = Convert.ToDouble(lblvatamt.Text)
                prop.CESS_AMOUNT = Convert.ToDouble(lblcessamt.Text)
                prop.NET_AMOUNT = Convert.ToDouble(lblnetamt.Text)
                prop.MRN_TYPE = Convert.ToInt32(lblMRNType.Text)
                prop.CUST_ID = Convert.ToInt32(lblcustid.Text)
                If chk_VatCal.Checked = True Then
                    prop.VAT_ON_EXICE = 1
                Else
                    prop.VAT_ON_EXICE = 0
                End If

                prop.CashDiscount_amt = Convert.ToDouble(txtCashDiscount.Text)

                If chk_ApplyTax.Checked = True Then
                    prop.Freight_TaxApplied = 1
                    prop.Freight_TaxValue = Convert.ToDouble(lblFreightTaxTotal.Text)
                Else
                    prop.Freight_TaxApplied = 0
                    prop.Freight_TaxValue = 0.00
                End If

                If cmbITCEligibility.SelectedIndex = 1 Then
                    prop.FK_ITCEligibility_ID = cmbITCEligibility.SelectedValue
                    prop.Reference_ID = cmbCapitalAccount.SelectedValue
                Else
                    prop.FK_ITCEligibility_ID = cmbITCEligibility.SelectedValue
                    prop.Reference_ID = 10070
                End If

                Master.insert_MATERIAL_RECIEVED_AGAINST_PO_MASTER(prop, cmd, FLXGRD_PO_Items.DataSource, FLXGRD_PO_NON_STOCKABLEITEMS.DataSource)

                obj.MyCon_CommitTransaction(cmd)
                fill_PO()
                ''MsgBox("Record Saved", MsgBoxStyle.Information, gblMessageHeading)
                If flag = "save" Then
                    If MsgBox("Record Saved" & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                        obj.RptShow(enmReportName.RptMaterialRecAgainstPOPrint, "rec_id", CStr(prop.Receipt_ID), CStr(enmDataType.D_int))
                        frm_Report.Mrn_id = CInt(prop.Receipt_ID)
                        frm_Report.formName = "MRN_AgainstPO"
                    End If
                End If
                obj.Clear_All_TextBox(Me.GroupBox1.Controls)
                'obj.Clear_All_ComoBox(Me.GroupBox1.Controls)
                new_initilization()
            End If
        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try


    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TabControl1.SelectedIndex = 0 Then
                obj.RptShow(enmReportName.RptMaterialRecAgainstPOPrint, "Receipt_Id", CStr(dgvList.Rows(dgvList.CursorCell.r1).Item("Receipt_Id").ToString()), CStr(enmDataType.D_int))
                frm_Report.Mrn_id = CInt(dgvList.Rows(dgvList.CursorCell.r1).Item("Receipt_Id"))
                frm_Report.formName = "MRN_AgainstPO"
            Else
                If flag <> "save" Then
                    obj.RptShow(enmReportName.RptMaterialRecAgainstPOPrint, "Receipt_Id", CStr(receive_Id), CStr(enmDataType.D_int))
                    frm_Report.Mrn_id = CInt(receive_Id)
                    frm_Report.formName = "MRN_AgainstPO"
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frm_material_rec_against_PO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        obj.ComboBind(cmb_MRNAgainst, "SELECT Company_id, Company_name FROM MRN_COMPANIES", "Company_name", "Company_id")
        obj.ComboBind(cmbITCEligibility, "Select ID, Type from ITC_Eligibility where Is_Active = 1", "Type", "ID")
        fill_PO()
        table_style()
        dtpFrom.Value = Now.AddDays(-7)
        btnIssueSlip_Click(Nothing, Nothing)
        new_initilization()

    End Sub

    Private Sub fill_PO1()
        ''********************************************************************''
        ''Fill material requisition combo - show only pending (2) requisitions
        ''********************************************************************''
        Dim TemporaryTable As New DataTable
        Dim TemporaryRow As DataRow
        Dim DSpO As New DataSet
        DSpO = obj.fill_Data_set("Fill_PO", "@Div_ID", v_the_current_division_id.ToString())
        TemporaryTable = DSpO.Tables(0)
        TemporaryRow = TemporaryTable.NewRow
        TemporaryRow("PO_ID") = -1
        TemporaryRow("PO_CODE") = "Select Cost Center"
        TemporaryTable.Rows.InsertAt(TemporaryRow, 0)
        cmbPurchaseOrders.DisplayMember = "PO_CODE"
        cmbPurchaseOrders.ValueMember = "PO_ID"
        cmbPurchaseOrders.DataSource = TemporaryTable
        ''********************************************************************''
        ''********************************************************************''
    End Sub

    Private Sub fill_PO()
        ds = obj.fill_Data_set("Fill_PO", "@Div_ID", v_the_current_division_id)
        If ds.Tables.Count > 0 Then
            cmbPurchaseOrders.ValueMember = "PO_ID"
            cmbPurchaseOrders.DisplayMember = "PO_CODE"
            cmbPurchaseOrders.DataSource = ds.Tables(0).Copy()
        End If
        ds.Clear()

    End Sub

    Private Sub table_style()
        If Not dtable_Item_List Is Nothing Then dtable_Item_List.Dispose()

        dtable_Item_List = New DataTable()
        dtable_Item_List.Columns.Add("Item_ID", GetType(System.Int32))
        dtable_Item_List.Columns.Add("UM_ID", GetType(System.Int32))
        dtable_Item_List.Columns.Add("Item_Code", GetType(System.String))
        dtable_Item_List.Columns.Add("Item_Name", GetType(System.String))
        dtable_Item_List.Columns.Add("UM_Name", GetType(System.String))
        dtable_Item_List.Columns.Add("PO_Qty", GetType(System.Double))
        dtable_Item_List.Columns.Add("Item_Rate", GetType(System.Double))
        dtable_Item_List.Columns.Add("DType", GetType(System.String))
        dtable_Item_List.Columns.Add("DISC", GetType(System.Decimal))

        dtable_Item_List.Columns.Add("Vat_Per", GetType(System.Double))
        dtable_Item_List.Columns.Add("Cess_Per", GetType(System.Double))
        dtable_Item_List.Columns.Add("EXICE_Per", GetType(System.Double))
        dtable_Item_List.Columns.Add("BATCH_NO", GetType(System.String))
        dtable_Item_List.Columns.Add("EXPIRY_DATE", GetType(System.DateTime))
        dtable_Item_List.Columns.Add("BATCH_QTY", GetType(System.Double))
        dtable_Item_List.Columns.Add("Net_Amount", GetType(System.Double))
        dtable_Item_List.Columns.Add("Gross_Amount", GetType(System.Double))

        FLXGRD_PO_Items.DataSource = dtable_Item_List
        FLXGRD_PO_Items.Rows.Add()
        FLXGRD_PO_Items.Cols(1).Visible = False
        FLXGRD_PO_Items.Cols(2).Visible = False


        FLXGRD_PO_Items.Cols(3).Caption = "BarCode"
        FLXGRD_PO_Items.Cols(4).Caption = "Item Name"
        FLXGRD_PO_Items.Cols(5).Caption = "UOM"
        FLXGRD_PO_Items.Cols(6).Caption = "PO Qty"
        FLXGRD_PO_Items.Cols(7).Caption = "Item Rate"
        FLXGRD_PO_Items.Cols(8).Caption = "DType"
        FLXGRD_PO_Items.Cols(9).Caption = "DISC"
        FLXGRD_PO_Items.Cols(10).Caption = "GST%"
        FLXGRD_PO_Items.Cols(11).Caption = "EXICE%"
        FLXGRD_PO_Items.Cols(12).Caption = "CESS%"
        'FLXGRD_PO_Items.Cols(12).Caption = "BatchNo"
        FLXGRD_PO_Items.Cols(13).Caption = "ExpiryDate"
        FLXGRD_PO_Items.Cols(14).Caption = "BatchQty"
        FLXGRD_PO_Items.Cols(15).Caption = "Net Amt"
        FLXGRD_PO_Items.Cols(16).Caption = "Gross Amt"

        FLXGRD_PO_Items.Cols(3).Width = 75
        FLXGRD_PO_Items.Cols(4).Width = 350
        FLXGRD_PO_Items.Cols(5).Width = 40
        FLXGRD_PO_Items.Cols(6).Width = 70
        FLXGRD_PO_Items.Cols(7).Width = 70
        FLXGRD_PO_Items.Cols(8).Width = 30
        FLXGRD_PO_Items.Cols(9).Width = 50
        FLXGRD_PO_Items.Cols(10).Width = 35
        FLXGRD_PO_Items.Cols(11).Width = 50
        FLXGRD_PO_Items.Cols(11).Visible = False
        FLXGRD_PO_Items.Cols(12).Width = 50
        FLXGRD_PO_Items.Cols(13).Width = 100
        FLXGRD_PO_Items.Cols(14).Width = 100
        FLXGRD_PO_Items.Cols(15).Width = 100
        FLXGRD_PO_Items.Cols(16).Width = 100


        '----------------- Non Stockable Table ---------------------'


        datatbl_NonStockable_Items = New DataTable()
        datatbl_NonStockable_Items.Columns.Add("Item_ID", GetType(System.Int32))
        datatbl_NonStockable_Items.Columns.Add("UM_ID", GetType(System.Int32))
        datatbl_NonStockable_Items.Columns.Add("Item_Code", GetType(System.String))
        datatbl_NonStockable_Items.Columns.Add("Item_Name", GetType(System.String))
        datatbl_NonStockable_Items.Columns.Add("UM_Name", GetType(System.String))
        datatbl_NonStockable_Items.Columns.Add("CostCenter_ID", GetType(System.Int32))
        datatbl_NonStockable_Items.Columns.Add("CostCenter_Code", GetType(System.String))
        datatbl_NonStockable_Items.Columns.Add("CostCenter_Name", GetType(System.String))
        datatbl_NonStockable_Items.Columns.Add("PO_Qty", GetType(System.Double))
        datatbl_NonStockable_Items.Columns.Add("Item_Rate", GetType(System.Double))
        datatbl_NonStockable_Items.Columns.Add("DType", GetType(System.String))
        datatbl_NonStockable_Items.Columns.Add("DISC", GetType(System.Decimal))

        datatbl_NonStockable_Items.Columns.Add("Vat_Per", GetType(System.Double))
        datatbl_NonStockable_Items.Columns.Add("Cess_Per", GetType(System.Double))
        datatbl_NonStockable_Items.Columns.Add("EXICE_Per", GetType(System.Double))
        datatbl_NonStockable_Items.Columns.Add("BATCH_NO", GetType(System.String))
        datatbl_NonStockable_Items.Columns.Add("EXPIRY_DATE", GetType(System.DateTime))
        datatbl_NonStockable_Items.Columns.Add("BATCH_QTY", GetType(System.Double))
        datatbl_NonStockable_Items.Columns.Add("Net_Amount", GetType(System.Double))
        datatbl_NonStockable_Items.Columns.Add("Gross_Amount", GetType(System.Double))
        datatbl_NonStockable_Items.Columns.Add("IS_STOCKABLE", GetType(System.Boolean))
        FLXGRD_PO_NON_STOCKABLEITEMS.DataSource = datatbl_NonStockable_Items

        FLXGRD_PO_NON_STOCKABLEITEMS.Rows.Add()
        ''''FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Item_ID").Visible = False
        ''''FLXGRD_PO_NON_STOCKABLEITEMS.Cols("UM_ID").Visible = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("CostCenter_ID").Visible = False


        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Item_Code").Caption = "BarCode"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Item_Name").Caption = "Item Name"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("UM_Name").Caption = "UOM"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("CostCenter_Code").Caption = "CC Code"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("CostCenter_Name").Caption = "CC Name"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("PO_Qty").Caption = "PO Qty"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Item_Rate").Caption = "Item Rate"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("DType").Caption = "DType"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("DISC").Caption = "DISC"

        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Vat_Per").Caption = "GST%"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Cess_Per").Caption = "CESS%"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("EXICE_Per").Caption = "EXICE%"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("BATCH_NO").Caption = "Batch No."
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("EXPIRY_DATE").Caption = "Expiry Date"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("BATCH_QTY").Caption = "Batch Qty"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Net_Amount").Caption = "Net Amt."
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Gross_Amount").Caption = "Gross Amt."
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("IS_STOCKABLE").Caption = "Stockable"
        ''''FLXGRD_PO_NON_STOCKABLEITEMS.Cols("CostCenter_Code").Visible = False

        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Item_Name").Width = 180
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("UM_Name").Width = 30
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("CostCenter_Name").Width = 70
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("PO_Qty").Width = 30
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Item_Rate").Width = 30
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("DType").Width = 30
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("DISC").Width = 40

        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Vat_Per").Width = 50
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Cess_Per").Width = 50
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("EXICE_Per").Width = 25
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("EXICE_Per").Visible = False
        'FLXGRD_PO_NON_STOCKABLEITEMS.Cols("BATCH_NO").Width = 100
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("EXPIRY_DATE").Width = 100
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("BATCH_QTY").Width = 100
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Net_Amount").Width = 100
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Gross_Amount").Width = 100


        '----------------- Non Stockable Table ---------------------'

    End Sub

    Public Sub C1FlexGrid2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Space Then
                Dim dt_batch As DataTable
                Dim dt_GridData As DataTable
                Dim row As DataRow
                Dim dt_test As New DataTable

                dt_test.Columns.Add("Batch_No", GetType(System.String))
                dt_test.Columns.Add("Expiry_Date", GetType(System.DateTime))
                dt_test.Columns.Add("Item_Qty", GetType(System.Double))
                FLXGRD_PO_Items_Rowindex = FLXGRD_PO_Items.RowSel
                v_frm_Batch_Entry_Qty = New frm_Batch_Entry_Qty
                If Not FLXGRD_PO_Items.Item(FLXGRD_PO_Items_Rowindex, "BATCH_QTY") = "" Then

                    dt_GridData = CType(FLXGRD_PO_Items.DataSource, DataTable)



                    For Each row In dt_GridData.Select("Item_ID = '" + FLXGRD_PO_Items.Item(FLXGRD_PO_Items_Rowindex, "Item_ID").ToString() + "'")
                        frm_Batch_Entry_Qty.InitilizeGrid()
                        dt_test.Rows.Add(row("BATCH_NO"), Convert.ToDateTime(row("EXPIRY_DATE")), Convert.ToDecimal(row("BATCH_QTY")))
                    Next
                    v_frm_Batch_Entry_Qty.grdMaterialQty.DataSource = dt_test
                End If
                frm_Batch_Entry_Qty.ShowDialog()

                dt_batch = frm_Batch_Entry_Qty.grdMaterialQty.DataSource
                If dt_batch.Rows.Count = 1 Then
                    FLXGRD_PO_Items.Item(FLXGRD_PO_Items.RowSel, "BATCH_NO") = dt_batch.Rows(0)("Batch_No").ToString()
                    FLXGRD_PO_Items.Item(FLXGRD_PO_Items.RowSel, "EXPIRY_DATE") = dt_batch.Rows(0)("Expiry_Date").ToString()
                    FLXGRD_PO_Items.Item(FLXGRD_PO_Items.RowSel, "BATCH_QTY") = dt_batch.Rows(0)("Item_Qty").ToString()
                End If
                If dt_batch.Rows.Count > 1 Then
                    Dim i As Int32

                    For i = 0 To dt_batch.Rows.Count Step 1
                        FLXGRD_PO_Items.Rows.Insert(5)
                    Next


                End If
                'MsgBox("hi")
                'get_row(frm_Show_search.search_result)
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub cmbPurchaseOrders_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPurchaseOrders.SelectedIndexChanged
        If cmbPurchaseOrders.SelectedValue = Nothing Then
            lblSupplier.Text = ""

            lblSupplierAddress.Text = ""
            lblPODate.Text = ""

        Else

            datatbl_NonStockable_Items.Clear()

            lblSupplier.Text = ""

            lblSupplierAddress.Text = ""
            lblPODate.Text = ""

            bind_FLXGRD_PO_Items(cmbPurchaseOrders.SelectedValue)
            generate_tree()
            SetGstLabels()
        End If

    End Sub

    Private Sub bind_FLXGRD_PO_Items(ByVal po_id As String)
        ds = obj.fill_Data_set_val("FILL_PO_ITEMS", "@PO_ID", "@DIV_ID", po_id, v_the_current_division_id.ToString())
        dtable_Item_List = ds.Tables(0).Copy()
        If dtable_Item_List.Rows.Count > 0 Then
            Open_Po_Qty = Convert.ToBoolean(dtable_Item_List.Rows(0)("OPEN_PO_QTY"))
        End If
        Dim dv As DataView
        dv = dtable_Item_List.DefaultView
        dv.RowFilter = "IS_STOCKABLE = 1"
        dtable_Item_List = dv.ToTable
        FLXGRD_PO_Items.DataSource = dtable_Item_List



        If FLXGRD_PO_Items.Rows.Count > 0 Then
            FLXGRD_PO_Items.Row = 1
            FLXGRD_PO_Items.RowSel = 1
            FLXGRD_PO_Items.Col = 22
            FLXGRD_PO_Items.ColSel = 22
            FLXGRD_PO_Items.Select()
        End If



        Dim dv1 As DataView
        dtable_Item_List = ds.Tables(0).Copy()
        dv1 = dtable_Item_List.DefaultView

        dv1.RowFilter = "IS_STOCKABLE = 0"

        Dim dtable As New DataTable
        Dim ds_CC As DataSet
        Dim drItem As DataRow

        dtable = dv1.ToTable
        Dim rowcount As Int16
        Dim innerRowCount As Int16
        ds_CC = obj.Fill_DataSet("SELECT * FROM dbo.COST_CENTER_MASTER where display_at_mrn=1")

        For rowcount = 0 To dtable.Rows.Count - 1 Step 1

            If ds_CC.Tables.Count > 0 Then

                If ds_CC.Tables(0).Rows.Count > 0 Then

                    For innerRowCount = 0 To ds_CC.Tables(0).Rows.Count - 1 Step 1
                        drItem = datatbl_NonStockable_Items.NewRow

                        drItem("Item_Id") = dtable.Rows(rowcount)("Item_ID")
                        drItem("Item_Code") = dtable.Rows(rowcount)("Item_Code").ToString()
                        drItem("Item_Name") = dtable.Rows(rowcount)("Item_Name").ToString()
                        drItem("um_Name") = dtable.Rows(rowcount)("UM_Name").ToString()
                        drItem("PO_Qty") = dtable.Rows(rowcount)("PO_Qty").ToString()
                        drItem("Item_Rate") = dtable.Rows(rowcount)("Item_Rate").ToString()
                        drItem("DType") = dtable.Rows(rowcount)("DType").ToString()
                        drItem("DISC") = dtable.Rows(rowcount)("DISC").ToString()

                        drItem("Vat_Per") = dtable.Rows(rowcount)("Vat_Per").ToString()
                        drItem("Cess_Per") = dtable.Rows(rowcount)("Cess_Per").ToString()
                        drItem("EXICE_Per") = dtable.Rows(rowcount)("EXICE_Per").ToString()
                        drItem("BATCH_NO") = dtable.Rows(rowcount)("BATCH_NO").ToString()
                        drItem("EXPIRY_DATE") = dtable.Rows(rowcount)("EXPIRY_DATE").ToString()
                        drItem("BATCH_QTY") = dtable.Rows(rowcount)("BATCH_QTY").ToString()
                        drItem("Net_Amount") = dtable.Rows(rowcount)("Net_Amount").ToString("#0.00")
                        drItem("Gross_Amount") = dtable.Rows(rowcount)("Gross_Amount").ToString()
                        drItem("IS_STOCKABLE") = dtable.Rows(rowcount)("IS_STOCKABLE").ToString()

                        drItem("CostCenter_Id") = ds_CC.Tables(0).Rows(innerRowCount)("CostCenter_Id").ToString()
                        drItem("CostCenter_Code") = ds_CC.Tables(0).Rows(innerRowCount)("CostCenter_Code").ToString()
                        drItem("CostCenter_Name") = ds_CC.Tables(0).Rows(innerRowCount)("CostCenter_Name").ToString()
                        datatbl_NonStockable_Items.Rows.Add(drItem)

                    Next

                End If
            End If
        Next

        FLXGRD_PO_NON_STOCKABLEITEMS.DataSource = datatbl_NonStockable_Items

        'If FLXGRD_PO_Items.Rows.Count > 1 Then

        Grid_Formatting()

        'End If


        'Get Supplier
        lblSupplier.Text = ""

        lblSupplierAddress.Text = ""
        lblPODate.Text = ""
        lblMRNType.Text = ""


        lblSupplier.Text = Convert.ToString(obj.ExecuteScalar("select ACC_NAME from ACCOUNT_MASTER  WHERE Acc_ID = (select po_supp_id from po_master where po_id = " + po_id + ")"))
        lblSupplierAddress.Text = Convert.ToString(obj.ExecuteScalar("select ADDRESS_PRIM from ACCOUNT_MASTER  WHERE Acc_ID = (select po_supp_id from po_master where po_id = " + po_id + ")"))
        lblPODate.Text = Convert.ToString(obj.ExecuteScalar("select dbo.fn_format(po_date) as po_date from po_master where po_id = " + po_id))
        lblMRNType.Text = Convert.ToString(obj.ExecuteScalar("select PO_TYPE from po_master where po_id = " + po_id))
        lblcustid.Text = Convert.ToString(obj.ExecuteScalar("select po_supp_id from po_master where po_id = " + po_id))
    End Sub

    Private Sub Grid_Formatting()
        FLXGRD_PO_Items.Cols("item_id").Caption = "Item Id"
        FLXGRD_PO_Items.Cols("item_code").Caption = "BarCode"
        FLXGRD_PO_Items.Cols("item_name").Caption = "Item Name"
        FLXGRD_PO_Items.Cols("um_name").Caption = "UOM"
        FLXGRD_PO_Items.Cols("PO_QTY").Caption = "PO Qty"
        FLXGRD_PO_Items.Cols("item_rate").Caption = "Item Rate"
        FLXGRD_PO_Items.Cols("DType").Caption = "DType"
        FLXGRD_PO_Items.Cols("DISC").Caption = "DISC"

        FLXGRD_PO_Items.Cols("VAT_PER").Caption = "GST%"
        FLXGRD_PO_Items.Cols("CESS_PER").Caption = "CESS%"

        FLXGRD_PO_Items.Cols("exice_per").Caption = "Exice%"
        FLXGRD_PO_Items.Cols("exice_per").Visible = False
        FLXGRD_PO_Items.Cols("BATCH_NO").Visible = False

        FLXGRD_PO_Items.Cols("BATCH_NO").Caption = "BatchNo"
        FLXGRD_PO_Items.Cols("expiry_date").Caption = "ExpiryDate"
        FLXGRD_PO_Items.Cols("BATCH_QTY").Caption = "Quantity"
        FLXGRD_PO_Items.Cols("Net_amount").Caption = "NetAmount"
        FLXGRD_PO_Items.Cols("Gross_amount").Caption = "GrossAmount"


        FLXGRD_PO_Items.Cols(0).Width = 8
        FLXGRD_PO_Items.Cols("item_id").Width = 40
        FLXGRD_PO_Items.Cols("item_code").Width = 60
        FLXGRD_PO_Items.Cols("item_name").Width = 235
        FLXGRD_PO_Items.Cols("um_name").Width = 30
        FLXGRD_PO_Items.Cols("PO_QTY").Width = 70
        FLXGRD_PO_Items.Cols("item_rate").Width = 60
        FLXGRD_PO_Items.Cols("DType").Width = 40
        FLXGRD_PO_Items.Cols("DISC").Width = 50
        FLXGRD_PO_Items.Cols("VAT_PER").Width = 40
        FLXGRD_PO_Items.Cols("CESS_PER").Width = 50
        FLXGRD_PO_Items.Cols("exice_per").Width = 40
        'FLXGRD_PO_Items.Cols("BATCH_NO").Width = 70
        FLXGRD_PO_Items.Cols("expiry_date").Width = 10
        FLXGRD_PO_Items.Cols("BATCH_QTY").Width = 70
        FLXGRD_PO_Items.Cols("Net_amount").Width = 80
        FLXGRD_PO_Items.Cols("Gross_amount").Width = 80




        FLXGRD_PO_Items.Cols("item_id").AllowEditing = False
        FLXGRD_PO_Items.Cols("item_code").AllowEditing = False
        FLXGRD_PO_Items.Cols("item_name").AllowEditing = False
        FLXGRD_PO_Items.Cols("um_name").AllowEditing = False
        FLXGRD_PO_Items.Cols("PO_QTY").AllowEditing = False
        FLXGRD_PO_Items.Cols("item_rate").AllowEditing = True
        FLXGRD_PO_Items.Cols("DType").AllowEditing = False
        FLXGRD_PO_Items.Cols("DISC").AllowEditing = True
        FLXGRD_PO_Items.Cols("VAT_PER").AllowEditing = False
        FLXGRD_PO_Items.Cols("CESS_PER").AllowEditing = False
        FLXGRD_PO_Items.Cols("exice_per").AllowEditing = True
        'FLXGRD_PO_Items.Cols("BATCH_NO").AllowEditing = True
        FLXGRD_PO_Items.Cols("expiry_date").AllowEditing = True
        FLXGRD_PO_Items.Cols("BATCH_QTY").AllowEditing = True
        FLXGRD_PO_Items.Cols("Net_amount").AllowEditing = False
        FLXGRD_PO_Items.Cols("Gross_amount").AllowEditing = False

        FLXGRD_PO_Items.Cols("po_id").Visible = False
        FLXGRD_PO_Items.Cols("um_id").Visible = False
        FLXGRD_PO_Items.Cols("expiry_date").Visible = False
        FLXGRD_PO_Items.Cols("item_id").Visible = False
        FLXGRD_PO_Items.Cols("IS_STOCKABLE").Visible = False
        FLXGRD_PO_Items.Cols("CostCenter_ID").Visible = False
        FLXGRD_PO_Items.Cols("CostCenter_Code").Visible = False
        FLXGRD_PO_Items.Cols("CostCenter_Name").Visible = False
        FLXGRD_PO_Items.Cols("OPEN_PO_QTY").Visible = False
        'FLXGRD_PO_Items.Cols(1).Visible = False
        'FLXGRD_PO_Items.Cols(2).Visible = False
        'FLXGRD_PO_Items.Cols(3).Visible = False

        '---------------Formatting FLXGRD_PO_NON_STOCKABLEITEMS -----------------'

        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("item_id").Caption = "Item Id"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("item_code").Caption = "Item Code"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("item_name").Caption = "Item Name"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("um_name").Caption = "UOM"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("PO_QTY").Caption = "PO Qty"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Item_Rate").Caption = "Item Rate"

        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("DType").Caption = "DType"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("DISC").Caption = "DISC"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("VAT_PER").Caption = "GST %"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("CESS_PER").Caption = "CESS %"

        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("exice_per").Caption = "Exice %"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("exice_per").Visible = False

        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("BATCH_NO").Visible = False

        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("BATCH_NO").Caption = "Batch No."
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("expiry_date").Caption = "Expiry Date"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("BATCH_QTY").Caption = "Item Qty"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Net_amount").Caption = "Net Amt"
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Gross_amount").Caption = "Gross amt"



        FLXGRD_PO_NON_STOCKABLEITEMS.Cols(0).Width = 8
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("item_id").Width = 40
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("item_code").Width = 60
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("item_name").Width = 180
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("um_name").Width = 30
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("PO_QTY").Width = 50
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("item_rate").Width = 60
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("DType").Width = 40
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("DISC").Width = 40

        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("VAT_PER").Width = 40
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("CESS_PER").Width = 60
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("exice_per").Width = 40
        'FLXGRD_PO_NON_STOCKABLEITEMS.Cols("BATCH_NO").Width = 70
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("expiry_date").Width = 60
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("BATCH_QTY").Width = 45
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Net_amount").Width = 60
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Gross_amount").Width = 60




        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("item_id").AllowEditing = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("item_code").AllowEditing = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("item_name").AllowEditing = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("um_name").AllowEditing = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("PO_QTY").AllowEditing = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Item_Rate").AllowEditing = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("DType").AllowEditing = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("DISC").AllowEditing = False

        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("VAT_PER").AllowEditing = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("CESS_PER").AllowEditing = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("exice_per").AllowEditing = False
        'FLXGRD_PO_NON_STOCKABLEITEMS.Cols("BATCH_NO").AllowEditing = True
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("expiry_date").AllowEditing = True
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("BATCH_QTY").AllowEditing = True
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Net_amount").AllowEditing = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("Gross_amount").AllowEditing = False

        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("CostCenter_ID").Visible = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("CostCenter_Code").Visible = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("IS_STOCKABLE").Visible = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols("CostCenter_Name").Visible = True
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols(1).Visible = False
        FLXGRD_PO_NON_STOCKABLEITEMS.Cols(2).Visible = False
        'FLXGRD_PO_NON_STOCKABLEITEMS.Cols(3).Visible = False


        FLXGRD_PO_Items.Cols(0).Width = 10

        '---------------Formatting FLXGRD_PO_NON_STOCKABLEITEMS -----------------'
    End Sub

    Private Sub FLXGRD_PO_Items_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles FLXGRD_PO_Items.AfterDataRefresh
        'generate_tree()
    End Sub

    Private Sub generate_tree()

        Dim strSort As String = FLXGRD_PO_NON_STOCKABLEITEMS.Cols(1).Name + ", " + FLXGRD_PO_NON_STOCKABLEITEMS.Cols(2).Name + ", " + FLXGRD_PO_NON_STOCKABLEITEMS.Cols(3).Name + ", " + FLXGRD_PO_NON_STOCKABLEITEMS.Cols(4).Name
        Dim dt As DataTable = CType(FLXGRD_PO_NON_STOCKABLEITEMS.DataSource, DataTable)

        'RemoveHandler FLXGRD_PO_NON_STOCKABLEITEMS.AfterDataRefresh, AddressOf FLXGRD_PO_NON_STOCKABLEITEMS_AfterDataRefresh

        If Not dt Is Nothing Then
            dt.DefaultView.Sort = strSort
        End If
        'AddHandler FLXGRD_PO_NON_STOCKABLEITEMS.AfterDataRefresh, AddressOf FLXGRD_PO_NON_STOCKABLEITEMS_AfterDataRefresh

        'FLXGRD_PO_NON_STOCKABLEITEMS.Tree.Style = TreeStyleFlags.Complete
        'FLXGRD_PO_NON_STOCKABLEITEMS.Tree.Column = 3
        'FLXGRD_PO_NON_STOCKABLEITEMS.AllowMerging = AllowMergingEnum.Nodes

        'Dim totalOn As Integer = FLXGRD_PO_NON_STOCKABLEITEMS.Cols("BATCH_QTY").SafeIndex
        'FLXGRD_PO_NON_STOCKABLEITEMS.Subtotal(AggregateEnum.Sum, 0, 4, totalOn)

        Dim cs As C1.Win.C1FlexGrid.CellStyle

        cs = Me.FLXGRD_PO_Items.Styles.Add("BATCH_QTY")
        'cs.ForeColor = Color.White
        cs.BackColor = Color.LimeGreen
        cs.Border.Style = BorderStyleEnum.Raised


        Dim cs2 As C1.Win.C1FlexGrid.CellStyle
        cs2 = Me.FLXGRD_PO_Items.Styles.Add("BATCH_NO")
        'cs2.ForeColor = Color.White
        cs2.BackColor = Color.Gold
        cs2.Border.Style = BorderStyleEnum.Raised

        Dim cs3 As C1.Win.C1FlexGrid.CellStyle
        cs3 = Me.FLXGRD_PO_Items.Styles.Add("expiry_date")
        'cs3.ForeColor = Color.White
        cs3.BackColor = Color.Gold
        cs3.Border.Style = BorderStyleEnum.Raised

        Dim cs4 As C1.Win.C1FlexGrid.CellStyle
        cs4 = Me.FLXGRD_PO_Items.Styles.Add("item_rate")
        'cs3.ForeColor = Color.White
        cs4.BackColor = Color.Gold
        cs4.Border.Style = BorderStyleEnum.Raised

        Dim cs5 As C1.Win.C1FlexGrid.CellStyle
        cs5 = Me.FLXGRD_PO_Items.Styles.Add("DISC")
        'cs3.ForeColor = Color.White
        cs5.BackColor = Color.Gold
        cs5.Border.Style = BorderStyleEnum.Raised

        Dim i As Integer
        For i = 1 To FLXGRD_PO_Items.Rows.Count - 1
            If Not FLXGRD_PO_Items.Rows(i).IsNode Then
                FLXGRD_PO_Items.SetCellStyle(i, FLXGRD_PO_Items.Cols("BATCH_QTY").SafeIndex, cs)
                FLXGRD_PO_Items.SetCellStyle(i, FLXGRD_PO_Items.Cols("BATCH_NO").SafeIndex, cs2)
                FLXGRD_PO_Items.SetCellStyle(i, FLXGRD_PO_Items.Cols("expiry_date").SafeIndex, cs3)
                FLXGRD_PO_Items.SetCellStyle(i, FLXGRD_PO_Items.Cols("item_rate").SafeIndex, cs4)
                FLXGRD_PO_Items.SetCellStyle(i, FLXGRD_PO_Items.Cols("disc").SafeIndex, cs5)
            End If
        Next


    End Sub

    Public Function GetReceiptCode() As String
        Dim Pre As String
        Dim CCID As String
        Dim POCode As String
        Pre = obj.getPrefixCode("RECEIPT_PREFIX", "DIVISION_SETTINGS")
        'CCID = obj.getMaxValue("RECEIPT_ID", "MATERIAL_RECEIVED_AGAINST_PO_MASTER")
        POCode = Pre '& "" & CCID
        Return POCode
    End Function

    Public Function GetMrnPrefix() As String
        Dim Pre As String
        Dim CCID As String
        Dim POCode As String
        Pre = obj.getPrefixCode("PREFIX", "MRN_SERIES")
        CCID = obj.getMaxValue("CURRENT_USED", "MRN_SERIES")
        POCode = Pre & "" & CCID
        Return POCode
    End Function

    Private Sub FLXGRD_PO_Items_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles FLXGRD_PO_Items.AfterEdit
        If Not Open_Po_Qty Then
            Dim Rec_Qty As Double
            Dim PO_Qty As Double
            If FLXGRD_PO_Items.ColSel = 19 Then
                Rec_Qty = FLXGRD_PO_Items.Item(FLXGRD_PO_Items.RowSel, FLXGRD_PO_Items.ColSel)
                PO_Qty = FLXGRD_PO_Items.Item(FLXGRD_PO_Items.RowSel, "PO_QTY")
                If Not Rec_Qty.ToString() = "" Then
                    If Convert.ToDouble(obj.NumericOnly(Rec_Qty)) > Convert.ToDouble(PO_Qty) Then
                        MsgBox("Received Qty cannot be greater than PO Qty")
                    End If
                End If
            End If
        End If

        If Open_Po_Qty Then
            If FLXGRD_PO_Items.ColSel = 7 Then
                Dim ds As New DataSet
                Dim newrate As Double
                Dim minrate As Double
                Dim maxrate As Double

                ds = obj.Fill_DataSet("SELECT  dbo.PO_DETAIL.ITEM_RATE, " &
                                     "dbo.DIVISION_SETTINGS.item_rate_min_per AS minper, " &
                                    "dbo.DIVISION_SETTINGS.item_rate_max_per AS maxper " &
                                    "FROM dbo.PO_MASTER " &
                                    "INNER JOIN dbo.PO_DETAIL ON dbo.PO_MASTER.PO_ID = dbo.PO_DETAIL.PO_ID " &
                                    "INNER JOIN dbo.DIVISION_SETTINGS ON dbo.PO_MASTER.DIVISION_ID = dbo.DIVISION_SETTINGS.DIV_ID " &
                                    "WHERE dbo.PO_MASTER.po_id = " & cmbPurchaseOrders.SelectedValue & " AND ITEM_ID = " & FLXGRD_PO_Items.Item(FLXGRD_PO_Items.RowSel, "item_id") & "")
                minrate = Convert.ToDouble(ds.Tables(0).Rows(0)("item_rate")) - (Convert.ToDouble(ds.Tables(0).Rows(0)("item_rate")) * Convert.ToDouble(ds.Tables(0).Rows(0)("minper"))) / 100
                maxrate = Convert.ToDouble(ds.Tables(0).Rows(0)("item_rate")) + (Convert.ToDouble(ds.Tables(0).Rows(0)("item_rate")) * Convert.ToDouble(ds.Tables(0).Rows(0)("maxper"))) / 100
                newrate = FLXGRD_PO_Items.Item(FLXGRD_PO_Items.RowSel, "item_rate")

                If newrate < minrate Then
                    FLXGRD_PO_Items.Item(FLXGRD_PO_Items.RowSel, "item_rate") = ds.Tables(0).Rows(0)("item_rate")
                    MsgBox("Item rate cannot be less than " & minrate & "")
                End If

                If newrate > maxrate Then
                    FLXGRD_PO_Items.Item(FLXGRD_PO_Items.RowSel, "item_rate") = ds.Tables(0).Rows(0)("item_rate")
                    MsgBox("Item rate cannot be more than " & maxrate & "")
                End If
            End If
        End If
        Calculate_Amount()
    End Sub


    'Sub NumericValueDGVIndentItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

    '    Dim colindex As Decimal = DGVIndentItem.CurrentCell.ColumnIndex
    '    If colindex = 4 Then
    '        Select Case Asc(e.KeyChar)
    '            Case AscW(ControlChars.Cr) 'Enter key
    '                e.Handled = True
    '            Case AscW(ControlChars.Back) 'Backspace
    '            Case 45, 46, 48 To 57 'Negative sign, Decimal and Numbers
    '            Case Else ' Everything else
    '                e.Handled = True
    '        End Select
    '    End If
    'End Sub

    Private Sub btnIssueSlip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIssueSlip.Click
        Try
            Dim qry As String

            qry = " SELECT  MATERIAL_RECEIVED_AGAINST_PO_MASTER.Receipt_ID," &
                    "         MATERIAL_RECEIVED_AGAINST_PO_MASTER.MRN_PREFIX" &
                    "         + CAST(MATERIAL_RECEIVED_AGAINST_PO_MASTER.MRN_NO AS VARCHAR(20)) AS [MRN No]," &
                    "         dbo.fn_Format(MATERIAL_RECEIVED_AGAINST_PO_MASTER.Receipt_Date) AS [MRN Date]," &
                    "         PO_MASTER.PO_CODE + CAST(PO_MASTER.PO_NO AS VARCHAR) AS [PO No]," &
                    "         dbo.fn_Format(PO_MASTER.PO_DATE) AS [PO Date]," &
                    "         ACCOUNT_MASTER.ACC_NAME AS [Supplier]," &
                    "         CASE WHEN mrn_status = 1 THEN 'NORMAL'" &
                    "              WHEN mrn_status = 3 THEN 'CLEAR'" &
                    "              ELSE 'CANCEL'" &
                    "         END AS MRNStatus, PO_MASTER.OPEN_PO_QTY" &
                    " FROM    MATERIAL_RECEIVED_AGAINST_PO_MASTER" &
                    "         INNER JOIN PO_MASTER ON MATERIAL_RECEIVED_AGAINST_PO_MASTER.PO_ID = PO_MASTER.PO_ID" &
                    "         INNER JOIN ACCOUNT_MASTER ON PO_MASTER.PO_SUPP_ID = ACCOUNT_MASTER.ACC_ID" &
                    " WHERE   cast(dbo.fn_format(Receipt_Date) as datetime) >= '" & dtpFrom.Value.Date & "'" &
                    "        AND cast(dbo.fn_format(Receipt_Date) as datetime)<= '" & dtpTo.Value.Date & "'" &
                    " and (MATERIAL_RECEIVED_AGAINST_PO_MASTER.Receipt_Code + CAST(MATERIAL_RECEIVED_AGAINST_PO_MASTER.Receipt_No AS VARCHAR(20)) +dbo.fn_Format(MATERIAL_RECEIVED_AGAINST_PO_MASTER.Receipt_Date) +PO_MASTER.PO_CODE + CAST(PO_MASTER.PO_NO AS VARCHAR) +dbo.fn_Format(PO_MASTER.PO_DATE) + ACCOUNT_MASTER.ACC_NAME) like '%" & txtSearch.Text.Replace(" ", "%") & "%' " &
                    " ORDER BY MATERIAL_RECEIVED_AGAINST_PO_MASTER.Receipt_ID"

            'dgvList.DataSource = obj.fill_Data_set("FILL_MATERIAL_Rec_Against_PO_LIST", "@FromDate,@Todate", dtpFrom.Value.Date & "," & dtpTo.Value.Date).Tables(0)

            dgvList.DataSource = obj.Fill_DataSet(qry).Tables(0)

            dgvList.Cols(0).Width = 10
            dgvList.Cols("Receipt_ID").Visible = False
            dgvList.Cols("OPEN_PO_QTY").Visible = False
            dgvList.Cols("MRN No").Width = 120
            dgvList.Cols("MRN Date").Width = 120
            dgvList.Cols("PO No").Width = 120
            dgvList.Cols("PO Date").Width = 120
            dgvList.Cols("Supplier").Width = 300
            dgvList.Cols("MRNStatus").Width = 80


        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub dgvList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvList.DoubleClick
        MsgBox("You can't Edit this MRN." & vbCrLf & "To view this MRN Click on ""Print""")
        'If _rights.allow_edit = "N" Then
        '    RightsMsg()
        '    Exit Sub
        'End If
        'Dim Receipt_Id As Integer
        'Dim dtMaster As New DataTable
        'Dim dtDetail As New DataTable
        'Dim ds As New DataSet
        'Receipt_Id = Convert.ToInt32(dgvList.Rows(dgvList.CursorCell.r1)("Receipt_ID"))
        'receive_Id = Convert.ToInt32(dgvList.Rows(dgvList.CursorCell.r1)("Receipt_ID"))
        'flag = "update"
        'ds = obj.fill_Data_set("Get_MRN_AgainstPO_Detail", "@V_Receipt_Id", Receipt_Id)
        'If ds.Tables.Count > 0 Then
        '    dtMaster = ds.Tables(0)
        '    lblMRNType.Text = Convert.ToString(dtMaster.Rows(0)("MRN_TYPE"))
        '    lblcustid.Text = dtMaster.Rows(0)("CUST_ID")
        '    cmbPurchaseOrders.SelectedValue = dtMaster.Rows(0)("PO_ID")

        '    txt_Remarks.Text = obj.NZ(dtMaster.Rows(0)("REMARKS"), True)
        '    txtotherchrgs.Text = IIf(String.IsNullOrEmpty(dtMaster.Rows(0)("other_charges")), 0, dtMaster.Rows(0)("other_charges"))
        '    txtdiscount.Text = IIf(String.IsNullOrEmpty(dtMaster.Rows(0)("discount_amt")), 0, dtMaster.Rows(0)("discount_amt"))
        '    txtAmount.Text = IIf(String.IsNullOrEmpty(dtMaster.Rows(0)("freight")), 0, dtMaster.Rows(0)("freight"))

        '    txtCashDiscount.Text = Convert.ToString(dtMaster.Rows(0)("CashDiscount_amt"))
        '    dt_Invoice_Date.Value = dtMaster.Rows(0)("Invoice_Date")
        '    txt_Invoice_No.Text = dtMaster.Rows(0)("Invoice_No")
        '    dtpReceiveDate.Text = dtMaster.Rows(0)("Receipt_Date")

        '    If (dtMaster.Rows(0)("FK_ITCEligibility_ID").ToString.Trim() = "2") Then
        '        cmbITCEligibility.SelectedIndex = 1
        '        lblSelectCapitalAccount.Visible = True
        '        cmbCapitalAccount.Visible = True
        '        cmbCapitalAccount.SelectedValue = dtMaster.Rows(0)("REFERENCE_ID")
        '    End If
        '    cmb_MRNAgainst.SelectedValue = dtMaster.Rows(0)("MRNCompanies_ID")
        '    dtable_Item_List = ds.Tables(1).Copy
        '    FLXGRD_PO_Items.DataSource = dtable_Item_List

        '    datatbl_NonStockable_Items = ds.Tables(2).Copy
        '    FLXGRD_PO_NON_STOCKABLEITEMS.DataSource = datatbl_NonStockable_Items


        '    If (dtMaster.Rows(0)("FreightTaxApplied") = True) Then
        '        Calculate_Amount()
        '        chk_ApplyTax.Checked = True
        '    Else
        '        chk_ApplyTax.Checked = False
        '    End If
        '    Grid_Formatting()

        '    'I = Convert.ToInt32(dtDetail.Rows(0)("PO_Id"))
        '    'cmbPurchaseOrders.SelectedValue = I
        '    TabControl1.SelectTab(1)
        '    ds.Dispose()
        'End If
        'generate_tree()
        'Calculate_Amount()
    End Sub

    Private Sub FLXGRD_PO_NON_STOCKABLEITEMS_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles FLXGRD_PO_NON_STOCKABLEITEMS.KeyPressEdit
        e.Handled = FLXGRD_PO_NON_STOCKABLEITEMS.Rows(FLXGRD_PO_NON_STOCKABLEITEMS.CursorCell.r1).IsNode
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        btnIssueSlip_Click(Nothing, Nothing)
    End Sub

    Private Sub BtnActualMRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnActualMRN.Click

        obj.RptShow(enmReportName.RptMaterialRecAgainstPOPrint, "Receipt_Id", CStr(dgvList.Rows(dgvList.CursorCell.r1).Item("Receipt_Id").ToString()), CStr(enmDataType.D_int))
        frm_Report.Mrn_id = CInt(dgvList.Rows(dgvList.CursorCell.r1).Item("Receipt_Id"))
        frm_Report.formName = "MRN_AgainstPO"

    End Sub

    Private Sub BtnRevisedMRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRevisedMRN.Click

        obj.RptShow(enmReportName.RptMaterialRecAgainstPOPrintRevised, "Receipt_Id", CStr(dgvList.Rows(dgvList.CursorCell.r1).Item("Receipt_Id").ToString()), CStr(enmDataType.D_int))
        frm_Report.Mrn_id = CInt(dgvList.Rows(dgvList.CursorCell.r1).Item("Receipt_Id"))
        frm_Report.formName = "MRN_AgainstPO"

    End Sub

    Private Sub FLXGRD_PO_NON_STOCKABLEITEMS_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles FLXGRD_PO_NON_STOCKABLEITEMS.AfterEdit
        Dim dt As DataTable
        dt = FLXGRD_PO_NON_STOCKABLEITEMS.DataSource

        If Open_Po_Qty Then
            Dim ds As New DataSet
            Dim newrate As Double
            Dim minrate As Double
            Dim maxrate As Double

            For irow As Integer = 0 To dt.Rows.Count - 1

                ds = obj.Fill_DataSet("SELECT  dbo.PO_DETAIL.ITEM_RATE, " &
                                     "dbo.DIVISION_SETTINGS.item_rate_min_per AS minper, " &
                                    "dbo.DIVISION_SETTINGS.item_rate_max_per AS maxper " &
                                    "FROM dbo.PO_MASTER " &
                                    "INNER JOIN dbo.PO_DETAIL ON dbo.PO_MASTER.PO_ID = dbo.PO_DETAIL.PO_ID " &
                                    "INNER JOIN dbo.DIVISION_SETTINGS ON dbo.PO_MASTER.DIVISION_ID = dbo.DIVISION_SETTINGS.DIV_ID " &
                                    "WHERE dbo.PO_MASTER.po_id = " & cmbPurchaseOrders.SelectedValue & " AND ITEM_ID = " & dt.Rows(irow)("item_id") & "")
                minrate = Convert.ToDouble(ds.Tables(0).Rows(0)("item_rate")) - (Convert.ToDouble(ds.Tables(0).Rows(0)("item_rate")) * Convert.ToDouble(ds.Tables(0).Rows(0)("minper"))) / 100
                maxrate = Convert.ToDouble(ds.Tables(0).Rows(0)("item_rate")) + (Convert.ToDouble(ds.Tables(0).Rows(0)("item_rate")) * Convert.ToDouble(ds.Tables(0).Rows(0)("maxper"))) / 100
                newrate = dt.Rows(irow)("item_rate")

                If newrate < minrate Then
                    dt.Rows(irow)("item_rate") = ds.Tables(0).Rows(0)("item_rate")
                    MsgBox("Item rate cannot be less than " & minrate & "")
                End If
                If newrate > maxrate Then
                    dt.Rows(irow)("item_rate") = ds.Tables(0).Rows(0)("item_rate")
                    MsgBox("Item rate cannot be more than " & maxrate & "")
                End If
            Next
        End If
        generate_tree()
        Calculate_Amount()
    End Sub

    Private Sub Calculate_Amount()
        Dim dt As DataTable
        Dim item_value As Double = 0
        Dim gross_amt As Double = 0
        Dim tot_vat_amt As Double = 0
        Dim tot_cess_amt As Double = 0
        Dim tot_exice_amt As Double = 0
        Dim exice_per As Double = 0
        Dim Net_amt As Double = 0
        Dim item_value_dis As Double = 0
        Dim discount_value As Double = 0
        Dim tot_gross_amt As Double = 0

        Dim discamt As Decimal = 0.0
        Dim totdiscamt As Decimal = 0.0

        dt = FLXGRD_PO_Items.DataSource

        If Not dt Is Nothing Then
            For i As Integer = 0 To dt.Rows.Count - 1

                If (dt.Rows(i)("DType")) = "P" Then
                    discamt = Math.Round(((dt.Rows(i)("Batch_qty") * dt.Rows(i)("Item_Rate")) * dt.Rows(i)("DISC") / 100), 2)
                Else
                    discamt = Math.Round(dt.Rows(i)("DISC"), 2)
                End If

                item_value = (Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate"))) * Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))) - discamt)
                tot_gross_amt = tot_gross_amt + item_value
                totdiscamt = totdiscamt + discamt

                exice_per = IIf((dt.Rows(i)("Exice_Per")) Is DBNull.Value, 0, dt.Rows(i)("Exice_Per"))
                exice_per = exice_per / 100
                tot_exice_amt = tot_exice_amt + (item_value * exice_per)
                tot_vat_amt = tot_vat_amt + ((((dt.Rows(i)("Batch_qty") * dt.Rows(i)("Item_Rate")) - discamt) * dt.Rows(i)("Vat_Per")) / 100)
                tot_cess_amt = tot_cess_amt + ((((dt.Rows(i)("Batch_qty") * dt.Rows(i)("Item_Rate")) - discamt) * dt.Rows(i)("Cess_Per")) / 100)
                SetGstLabels()

            Next
        End If

        ' discount_value = totdiscamt

        'If tot_gross_amt > 0 Then
        '    discount_value = Convert.ToDouble(Convert.ToDouble(IIf(IsNumeric(txtdiscount.Text), txtdiscount.Text, 0)) / tot_gross_amt)
        'Else
        '    discount_value = 0
        'End If

        'If Not dt Is Nothing Then

        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        item_value = (Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate"))) * Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))))
        '        gross_amt = gross_amt + item_value
        '        exice_per = Convert.ToDouble(IIf((dt.Rows(i)("exice_per")) Is DBNull.Value, 0, dt.Rows(i)("exice_per")))
        '        exice_per = exice_per / 100
        '        tot_exice_amt = tot_exice_amt + (item_value * exice_per)
        '        item_value_dis = item_value - (item_value * discount_value)

        '        If chk_VatCal.Checked = True Then
        '            tot_vat_amt = tot_vat_amt + ((item_value_dis + (item_value * exice_per)) * Convert.ToDouble(IIf((dt.Rows(i)("VAT_PER")) Is DBNull.Value, 0, dt.Rows(i)("VAT_PER"))) / 100)
        '        Else
        '            tot_vat_amt = tot_vat_amt + ((item_value_dis) * Convert.ToDouble(IIf((dt.Rows(i)("VAT_PER")) Is DBNull.Value, 0, dt.Rows(i)("VAT_PER"))) / 100)
        '        End If
        '    Next
        'End If


        dt = FLXGRD_PO_NON_STOCKABLEITEMS.DataSource

        If Not dt Is Nothing Then
            For i As Integer = 0 To dt.Rows.Count - 1

                '    item_value = (Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate"))) * Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))))
                '    tot_gross_amt = tot_gross_amt + item_value

                If (dt.Rows(i)("DType")) = "P" Then
                    discamt = Math.Round(((dt.Rows(i)("Batch_qty") * dt.Rows(i)("Item_Rate")) * dt.Rows(i)("DISC") / 100), 2)
                Else
                    discamt = Math.Round(dt.Rows(i)("DISC"), 2)
                End If

                item_value = (Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate"))) * Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))) - discamt)
                tot_gross_amt = tot_gross_amt + item_value
                totdiscamt = totdiscamt + discamt

                exice_per = IIf((dt.Rows(i)("Exice_Per")) Is DBNull.Value, 0, dt.Rows(i)("Exice_Per"))
                exice_per = exice_per / 100
                tot_exice_amt = tot_exice_amt + (item_value * exice_per)
                tot_vat_amt = tot_vat_amt + ((((dt.Rows(i)("Batch_qty") * dt.Rows(i)("Item_Rate")) - discamt) * dt.Rows(i)("Vat_Per")) / 100)
                tot_cess_amt = tot_cess_amt + ((((dt.Rows(i)("Batch_qty") * dt.Rows(i)("Item_Rate")) - discamt) * dt.Rows(i)("Cess_Per")) / 100)

            Next
        End If

        'If tot_gross_amt > 0 Then
        '    discount_value = Convert.ToDouble(IIf(IsNumeric(txtdiscount.Text), txtdiscount.Text, 0)) / tot_gross_amt
        'Else
        '    discount_value = 0
        'End If



        'If Not dt Is Nothing Then
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        item_value = (Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate"))) * Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))))
        '        gross_amt = gross_amt + item_value
        '        exice_per = Convert.ToDouble(IIf((dt.Rows(i)("exice_per")) Is DBNull.Value, 0, dt.Rows(i)("exice_per")))
        '        exice_per = exice_per / 100
        '        tot_exice_amt = tot_exice_amt + (item_value * exice_per)
        '        item_value_dis = item_value - (item_value * discount_value)
        '        If chk_VatCal.Checked = True Then
        '            tot_vat_amt = tot_vat_amt + ((item_value_dis + (item_value * exice_per)) * Convert.ToDouble(IIf((dt.Rows(i)("VAT_PER")) Is DBNull.Value, 0, dt.Rows(i)("VAT_PER"))) / 100)
        '        Else
        '            tot_vat_amt = tot_vat_amt + ((item_value_dis) * Convert.ToDouble(IIf((dt.Rows(i)("VAT_PER")) Is DBNull.Value, 0, dt.Rows(i)("VAT_PER"))) / 100)
        '        End If
        '    Next

        'End If

        txtdiscount.Text = totdiscamt.ToString("#0.00")
        lblgrossamt.Text = tot_gross_amt.ToString("0.00")
        'lblvatamt.Text = tot_vat_amt.ToString("0.00")
        lblcessamt.Text = tot_cess_amt.ToString("0.00")
        lblexciseamt.Text = tot_exice_amt.ToString("0.00")

        Net_amt = (tot_gross_amt + Convert.ToDouble(IIf(IsNumeric(lblvatamt.Text), lblvatamt.Text, 0)) + tot_cess_amt + tot_exice_amt + Convert.ToDouble(IIf(IsNumeric(txtAmount.Text), txtAmount.Text, 0)) + Convert.ToDouble(IIf(IsNumeric(txtotherchrgs.Text), txtotherchrgs.Text, 0))) - Convert.ToDouble(IIf(IsNumeric(txtCashDiscount.Text), txtCashDiscount.Text, 0)) '- Convert.ToDouble(IIf(IsNumeric(txtdiscount.Text), txtdiscount.Text, 0))
        lblnetamt.Text = Net_amt.ToString("0.00")

    End Sub

    Private Sub lnkCalculateAmount_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCalculateAmount.LinkClicked
        Calculate_Amount()
    End Sub

    Private Sub txt_Amount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Calculate_Amount()
    End Sub

    Private Sub txtotherchrgs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtotherchrgs.TextChanged
        If String.IsNullOrEmpty(txtotherchrgs.Text) Then
            txtotherchrgs.Text = "0.00"
        Else
            Calculate_Amount()
        End If
    End Sub

    Private Sub txtdiscount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdiscount.TextChanged
        Calculate_Amount()
    End Sub

    Private Sub FLXGRD_PO_Items_BeforeEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles FLXGRD_PO_Items.BeforeEdit

    End Sub

    Private Sub FLXGRD_PO_NON_STOCKABLEITEMS_BeforeEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles FLXGRD_PO_NON_STOCKABLEITEMS.BeforeEdit

    End Sub

    Private Sub chk_VatCal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_VatCal.CheckedChanged
        Calculate_Amount()
    End Sub

    Private Sub Panel14_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub FLXGRD_PO_Items_KeyDown(sender As Object, e As KeyEventArgs) Handles FLXGRD_PO_Items.KeyDown
        If e.KeyCode = Keys.Delete Then

            Dim result As Integer
            Dim item_code As String
            result = MsgBox("Do you want to remove """ & FLXGRD_PO_Items.Rows(FLXGRD_PO_Items.CursorCell.r1).Item(5) & """ from the list?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
            item_code = FLXGRD_PO_Items.Rows(FLXGRD_PO_Items.CursorCell.r1).Item("item_code")
            If result = MsgBoxResult.Yes Then
restart:
                Dim dt As DataTable
                dt = TryCast(FLXGRD_PO_Items.DataSource, DataTable)
                If Not dt Is Nothing Then
                    For Each dr As DataRow In dt.Rows
                        If Convert.ToString(dr("item_code")) = item_code Then
                            dr.Delete()
                            dt.AcceptChanges()
                            GoTo restart
                        End If
                    Next

                End If
            End If
            Calculate_Amount()
        End If
    End Sub

    Private Sub SetGstLabels()

        Dim GSTAmount0 As Decimal = 0
        Dim GSTTax0 As Decimal = 0
        Dim GSTAmount3 As Decimal = 0
        Dim GSTTax3 As Decimal = 0
        Dim GSTAmount5 As Decimal = 0
        Dim GSTTax5 As Decimal = 0
        Dim GSTAmount12 As Decimal = 0
        Dim GSTTax12 As Decimal = 0
        Dim GSTAmount18 As Decimal = 0
        Dim GSTTax18 As Decimal = 0
        Dim GSTAmount28 As Decimal = 0
        Dim GSTTax28 As Decimal = 0
        Dim GSTTaxTotal As Decimal = 0
        Dim CessTotal As Decimal = 0
        Dim Tax As Decimal = 0
        Dim TaxAmount As Decimal = 0

        Dim FreightTaxAmount As Decimal = 0
        Dim FreightTaxAmount0 As Decimal = 0
        Dim FreightTaxAmount3 As Decimal = 0
        Dim FreightTaxAmount5 As Decimal = 0
        Dim FreightTaxAmount12 As Decimal = 0
        Dim FreightTaxAmount18 As Decimal = 0
        Dim FreightTaxAmount28 As Decimal = 0

        Dim FreightTaxTotal As Decimal = 0

        Dim iRow As Integer = 0

        For iRow = 1 To FLXGRD_PO_Items.Rows.Count - 1

            Dim totalAmount As Decimal = FLXGRD_PO_Items.Item(iRow, "Batch_qty") * FLXGRD_PO_Items.Item(iRow, "item_rate")

            If FLXGRD_PO_Items.Item(iRow, "DType") = "P" Then
                totalAmount -= Math.Round((totalAmount * FLXGRD_PO_Items.Item(iRow, "DISC") / 100), 2)
            Else
                totalAmount -= Math.Round(FLXGRD_PO_Items.Item(iRow, "DISC"), 2)
            End If

            'If flxItemList.Item(iRow, "GPAID") = "Y" Then
            '    totalAmount -= (totalAmount - (totalAmount / (1 + (flxItemList.Item(iRow, "vat_per") / 100))))
            'End If

            If chk_ApplyTax.Checked = True Then
                'Tax = (totalAmount * FLXGRD_MaterialItem.Item(iRow, "vat_per") / 100) + (totalAmount / Convert.ToDecimal(lblgrossamt.Text) * Convert.ToDecimal(txt_Amount.Text))

                TaxAmount = ((totalAmount / (Convert.ToDouble(IIf(IsNumeric(lblgrossamt.Text), lblgrossamt.Text, 0)))) * Convert.ToDecimal(txtAmount.Text))
                Tax = (totalAmount + TaxAmount) * FLXGRD_PO_Items.Item(iRow, "vat_per") / 100
                FreightTaxAmount = Tax - (totalAmount * FLXGRD_PO_Items.Item(iRow, "vat_per") / 100)

            Else
                TaxAmount = 0
                Tax = totalAmount * FLXGRD_PO_Items.Item(iRow, "vat_per") / 100
                FreightTaxAmount = 0
            End If

            GSTTaxTotal += Tax
            FreightTaxTotal += FreightTaxAmount

            Select Case FLXGRD_PO_Items.Item(iRow, "vat_per")
                Case 0
                    If chk_ApplyTax.Checked = True Then
                        GSTAmount0 += (totalAmount + TaxAmount)
                    Else
                        GSTAmount0 += totalAmount
                    End If
                    'GSTAmount0 += totalAmount
                    GSTTax0 += Tax
                Case 3
                    If chk_ApplyTax.Checked = True Then
                        GSTAmount3 += (totalAmount + TaxAmount)
                    Else
                        GSTAmount3 += totalAmount
                    End If
                    'GSTAmount3 += totalAmount
                    GSTTax3 += Tax
                Case 5
                    If chk_ApplyTax.Checked = True Then
                        GSTAmount5 += (totalAmount + TaxAmount)
                    Else
                        GSTAmount5 += totalAmount
                    End If
                    'GSTAmount5 += totalAmount
                    GSTTax5 += Tax
                Case 12
                    If chk_ApplyTax.Checked = True Then
                        GSTAmount12 += (totalAmount + TaxAmount)
                    Else
                        GSTAmount12 += totalAmount
                    End If
                    'GSTAmount12 += totalAmount
                    GSTTax12 += Tax
                Case 18
                    If chk_ApplyTax.Checked = True Then
                        GSTAmount18 += (totalAmount + TaxAmount)
                    Else
                        GSTAmount18 += totalAmount
                    End If
                    'GSTAmount18 += totalAmount
                    GSTTax18 += Tax
                Case 28
                    If chk_ApplyTax.Checked = True Then
                        GSTAmount28 += (totalAmount + TaxAmount)
                    Else
                        GSTAmount28 += totalAmount
                    End If
                    'GSTAmount28 += totalAmount
                    GSTTax28 += Tax
            End Select
        Next
        lblFreightTaxTotal.Text = Math.Round(FreightTaxTotal, 2)
        lblvatamt.Text = Math.Round(GSTTaxTotal, 2)
        lblGST0.Text = String.Format("0% - {0:0.00} @ {1}", Math.Round(GSTAmount0, 2), Math.Round(GSTTax0, 2))
        lblGST3.Text = String.Format("3% - {0:0.00} @ {1}", Math.Round(GSTAmount3, 2), Math.Round(GSTTax3, 2))
        lblGST5.Text = String.Format("5% - {0:0.00} @ {1}", Math.Round(GSTAmount5, 2), Math.Round(GSTTax5, 2))
        lblGST12.Text = String.Format("12% - {0:0.00} @ {1}", Math.Round(GSTAmount12, 2), Math.Round(GSTTax12, 2))
        lblGST18.Text = String.Format("18% - {0:0.00} @ {1}", Math.Round(GSTAmount18, 2), Math.Round(GSTTax18, 2))
        lblGST28.Text = String.Format("28% - {0:0.00} @ {1}", Math.Round(GSTAmount28, 2), Math.Round(GSTTax28, 2))

        SetGSTAndCessHeader(GSTTaxTotal, CessTotal)

    End Sub

    Private Sub SetGSTAndCessHeader(TotalGst As Decimal, TotalCess As Decimal)
        Dim PartialGst As Decimal = Math.Round(TotalGst / 2, 2)



        If lblMRNType.Text = "0" Then
            lblGSTDetail.Text = String.Format("Total GST - {0}", TotalGst)
            lblGSTDetail.Tag = Math.Round(TotalGst, 2)
        ElseIf lblMRNType.Text = "3" Then
            lblGSTDetail.Text = String.Format("UTGST - {0}{1}CGST - {0}", Math.Round(PartialGst, 2), Environment.NewLine)
            lblGSTDetail.Tag = Math.Round(PartialGst, 2)
        ElseIf lblMRNType.Text = "1" Then
            lblGSTDetail.Text = String.Format("SGST - {0}{1}CGST - {0}", Math.Round(PartialGst, 2), Environment.NewLine)
            lblGSTDetail.Tag = Math.Round(PartialGst, 2)
        ElseIf lblMRNType.Text = "2" Then
            lblGSTDetail.Text = String.Format("IGST - {0}", Math.Round(TotalGst, 2))
            lblGSTDetail.Tag = Math.Round(TotalGst, 2)
        End If

    End Sub

    Private Sub chk_ApplyTax_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ApplyTax.CheckedChanged
        Calculate_Amount()
    End Sub

    Private Sub txtAmount_Leave(sender As Object, e As EventArgs) Handles txtAmount.Leave
        If String.IsNullOrEmpty(txtAmount.Text) Then
            txtAmount.Text = "0.00"
        Else
            Calculate_Amount()
        End If
    End Sub

    Private Sub txtCashDiscount_TextChanged(sender As Object, e As EventArgs) Handles txtCashDiscount.TextChanged
        If String.IsNullOrEmpty(txtCashDiscount.Text) Then
            txtCashDiscount.Text = "0.00"
        Else
            Calculate_Amount()
        End If
    End Sub

    Private Sub cmbITCEligibility_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbITCEligibility.SelectedIndexChanged
        If (cmbITCEligibility.SelectedIndex = 1) Then
            lblSelectCapitalAccount.Visible = True
            cmbCapitalAccount.Visible = True
            obj.ComboBind(cmbCapitalAccount, "SELECT ACC_ID, ACC_Name FROM dbo.ACCOUNT_MASTER WHERE ag_id = 12", "ACC_NAME", "ACC_ID")
        Else
            lblSelectCapitalAccount.Visible = False
            cmbCapitalAccount.Visible = False
        End If
    End Sub


End Class
