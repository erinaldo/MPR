Imports System.Data.SqlClient
Imports System.Data

Imports C1.Win.C1FlexGrid



Public Class frm_Material_Received_Without_PO_Master

    Implements IForm
    Dim obj As New CommonClass
    Dim clsObj As New material_recieved_without_po_master.cls_material_recieved_without_po_master
    Dim prpty As New material_recieved_without_po_master.cls_material_recieved_without_po_master_prop
    Dim flag As String
    Dim group_id As Integer
    Dim dtable_Item_List As DataTable
    Dim dtable As DataTable
    Dim dTable_IndentItems As DataTable
    Dim dtable_Item_List_Stockable As DataTable


    Dim grdMaterial_Rowindex As Int16
    Dim grdMaterial_Stockable_Rowindex As Int16
    'Dim frm_Batch_Entry_Qty As String
    Dim FLXGRD_PO_Items_Rowindex As Int16
    Dim v_frm_Batch_Entry_Qty As frm_Batch_Entry_Qty
    Dim intColumnIndex As Integer
    Dim txtQuanity As String
    Dim MRN_No As Integer
    Dim MRN_Code As String
    Dim total_Amt As Double = 0


    Dim receive_Id As Integer
    Dim Pre As String

    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Enum enmPODetail
        ItemId = 0
        ItemCode = 1
        ItemName = 2
        UOM = 3
        ItemRate = 4
        VatPer = 5
        ExePer = 6
        BatchNo = 7
        ExpiryDate = 8
        BatchQty = 9
    End Enum

    Private Sub frm_Material_Received_Without_PO_Master_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        flag = "save"
        Grid_styles()
        'FLXGRD_MaterialItem.Rows.Add()
        clsObj.FormatGrid(dgvList)

        FillGrid()
        clsObj.ComboBind(cmbMRNType, "Select PO_TYPE_ID,PO_TYPE_NAME from PO_TYPE_MASTER", "PO_TYPE_NAME", "PO_TYPE_ID", True)
        obj.ComboBind(cmbPurchaseType, "select pk_PurchaseTypeId ,PurchaseType from PurchaseType_Master ", "PurchaseType", "pk_PurchaseTypeId")
        clsObj.ComboBind(cmbVendor, "SELECT ACC_NAME, ACC_ID FROM ACCOUNT_MASTER where AG_ID=2 order by ACC_NAME", "ACC_NAME", "ACC_ID", True)

        clsObj.ComboBind(cmb_MRNAgainst, "SELECT Company_id, Company_name FROM MRN_COMPANIES", "Company_name", "Company_id")
        SetDefaultValues()
        intColumnIndex = -1
        new_initilization()
        'clsObj.ComboBind(cmbItemGroup, "select ITEM_CAT_ID, ITEM_CAT_NAME  from ITEM_CATEGORY order by ITEM_CAT_NAME", "ITEM_CAT_NAME", "ITEM_CAT_ID")
    End Sub

    Private Sub SetDefaultValues()
        If cmbVendor.Items.Count > 0 Then
            cmbVendor.SelectedIndex = 0
        End If
        cmbPurchaseType.SelectedIndex = 0
        cmbMRNType.SelectedIndex = 0
        cmbVendor.Enabled = False
        If flag = "save" Then
            lbl_PODate.Text = Now.ToString("dd/MMM/yyyy")
            lblMrnStatus.Text = "FRESH"
        End If
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
        TbPO.SelectTab(1)

        FLXGRD_MaterialItem.DataSource = Nothing
        cmb_MRNAgainst.SelectedValue = "1"
        Grid_styles()
        txtdiscount.Text = "0.0"
        lblgrossamt.Text = "0.0"
        lblvatamt.Text = "0.0"
        lblexciseamt.Text = "0.0"
        lblnetamt.Text = "0.0"
        ' DGVIndentItem.Rows.Add()

        flag = "save"
    End Sub

    Private Sub FillGrid(Optional ByVal condition As String = "")
        Try
            'obj.GridBind(grdSupplierList, "SELECT srl_id,srl_name,srl_desc,active,creation_date from Supplier_rate_list")
            Dim qry As String
            qry = "SELECT  MM.Received_ID," &
                    " MM.Received_Code + CAST(MM.Received_No AS VARCHAR(20)) AS MRNNo," &
                    "dbo.fn_Format(MM.Received_Date) AS Received_Date," &
                    "MM.Purchase_Type," &
                    "ISNULL(ACCOUNT_MASTER.ACC_NAME, '--DIRECT PURCHASE--') ACC_NAME, " &
                    "PM.PurchaseType," &
                    "CASE WHEN MM.mrn_status = 1 THEN 'NORMAL'" &
                    "     WHEN MM.mrn_status = 3 THEN 'CLEAR'" &
                    "     ELSE 'CANCEL'" &
                    "END AS MRNStatus " &
            "FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER AS MM" &
                    " INNER JOIN PurchaseType_Master AS PM ON MM.Purchase_Type = PM.pk_PurchaseTypeId" &
                    " LEFT OUTER JOIN ACCOUNT_MASTER ON MM.Vendor_ID = ACCOUNT_MASTER.ACC_ID " & condition

            obj.GridBind(dgvList, qry)
            dgvList.Width = 890
            dgvList.Columns(0).Visible = False 'receiveid
            dgvList.Columns(0).Width = 300
            dgvList.Columns(0).HeaderText = "Receive_ID"
            dgvList.Columns(1).Width = 180
            dgvList.Columns(1).HeaderText = "MRN No"
            dgvList.Columns(2).HeaderText = "Receive Date"
            dgvList.Columns(2).Width = 120
            dgvList.Columns(3).Width = 170
            dgvList.Columns(3).HeaderText = "Purchase Id"
            dgvList.Columns(3).Visible = False
            dgvList.Columns(4).HeaderText = "Purchase Type"
            dgvList.Columns(4).Width = 320
            dgvList.Columns(5).Width = 150
            dgvList.Columns(5).HeaderText = "Status"
            'dgvList.Columns(6).Visible = False
            'dgvList.Columns(6).HeaderText = "ACC ID"
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        FillGrid()
        TbPO.SelectTab(0)
    End Sub

    Private Function validate_data() As Boolean
        Dim iRow As Int32

        For iRow = 1 To FLXGRD_MaterialItem.Rows.Count - 2
            If Convert.ToString(FLXGRD_MaterialItem.Item(iRow, "item_rate")) <> "" Then
                If Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "item_rate")) <= 0 Then
                    MsgBox("Item rate should not be zero for """ & FLXGRD_MaterialItem.Item(iRow, "item_name") & """", MsgBoxStyle.Critical, gblMessageHeading)
                    Return False
                End If
            Else
                MsgBox("Item rate should not be blank for """ & FLXGRD_MaterialItem.Item(iRow, "item_name") & """", MsgBoxStyle.Critical, gblMessageHeading)
                Return False
            End If
        Next


        For iRow = 1 To FLXGRD_MaterialItem.Rows.Count - 2
            If Convert.ToString(FLXGRD_MaterialItem.Item(iRow, "Batch_Qty")) <> "" Then
                If Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Batch_Qty")) <= 0 Then
                    MsgBox("Batch Qty should not be zero for """ & FLXGRD_MaterialItem.Item(iRow, "item_name") & """", MsgBoxStyle.Critical, gblMessageHeading)
                    Return False
                End If
            Else
                MsgBox("Batch Qty should not be blank for """ & FLXGRD_MaterialItem.Item(iRow, "item_name") & """", MsgBoxStyle.Critical, gblMessageHeading)
                Return False
            End If
        Next


        Dim dt As DataTable
        dt = CType(FLXGRD_MatItem_NonStockable.DataSource, DataTable)
        'For iRow = 0 To dt.Rows.Count - 1
        '    If Convert.ToString(dt.Rows(iRow)("Batch_Qty")) <> "" Then
        '        If Convert.ToDouble(dt.Rows(iRow)("Batch_Qty")) <= 0 Then
        '            MsgBox("Batch Qty should not be zero for """ & dt.Rows(iRow)("item_name") & """", MsgBoxStyle.Critical, gblMessageHeading)
        '            Return False
        '        End If
        '        'Else
        '        '    MsgBox("Batch Qty should not be blank for """ & dt.Rows(iRow)("item_name") & """", MsgBoxStyle.Critical, gblMessageHeading)
        '        '    Return False
        '    End If
        'Next


        For iRow = 0 To dt.Rows.Count - 1
            If Convert.ToString(dt.Rows(iRow)("item_rate")) <> "" Then
                If Convert.ToDouble(dt.Rows(iRow)("item_rate")) <= 0 Then
                    If Convert.ToString(dt.Rows(iRow)("Batch_Qty")) <> "" Then
                        If Convert.ToDouble(dt.Rows(iRow)("Batch_Qty")) <= 0 Then
                            MsgBox("Item rate should not be zero for """ & dt.Rows(iRow)("item_name") & """", MsgBoxStyle.Critical, gblMessageHeading)
                            Return False
                        End If
                    End If
                End If
            Else
                If IsNumeric(Convert.ToString(dt.Rows(iRow)("Batch_Qty"))) Then
                    MsgBox("Item rate should not be blank for """ & dt.Rows(iRow)("item_name") & """", MsgBoxStyle.Critical, gblMessageHeading)
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

        Dim cmd As SqlCommand
        'cmd = obj.MyCon_BeginTransaction

        lblMrnStatus.Focus()
        Try
            If validate_data() Then
                Dim RECEIVEDID As Integer


                If flag = "save" Then



                    If cmbMRNType.SelectedIndex <= 0 Then
                        MsgBox("Please Select MRN Type first.")
                        Exit Sub
                    End If
                    If txt_Invoice_No.Text = Nothing Then
                        MsgBox("Please Enter Invoice No. first.")
                        Exit Sub
                    End If

                    If txt_Invoice_No.Text <> "" Then
                        Dim invoicecount = Convert.ToInt32(obj.ExecuteScalar("SELECT COUNT(Received_ID) FROM dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER WHERE Vendor_ID=" & cmbVendor.SelectedValue & " AND Invoice_No='" & txt_Invoice_No.Text & "'"))
                        If invoicecount > 0 Then
                            MsgBox("Invoice No. cannot be same for the singler supplier.")
                            Exit Sub
                        End If
                    End If

                    Dim ds As New DataSet()
                    ds = clsObj.fill_Data_set("GET_MRN_NO", "@DIV_ID", v_the_current_division_id)
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


                    RECEIVEDID = Convert.ToInt32(obj.getMaxValue(" RECEIVED_ID", "MATERIAL_RECIEVED_WITHOUT_PO_MASTER"))
                    prpty.Received_ID = Convert.ToInt32(RECEIVEDID)
                    prpty.Received_Code = MRN_Code ' GetReceivedCode()
                    prpty.Received_No = MRN_No ' Convert.ToInt32(RECEIVEDID)
                    prpty.Received_Date = Now 'Convert.ToDateTime(lbl_PODate.Text).ToString()
                Else
                    prpty.Received_ID = Convert.ToInt32(receive_Id)
                    prpty.Received_Code = ""
                    prpty.Received_No = 0
                    prpty.Received_Date = Now
                    MRN_Code = ""
                End If


                prpty.Purchase_Type = Convert.ToInt32(cmbPurchaseType.SelectedValue)
                prpty.Invoice_Date = Convert.ToDateTime(dt_Invoice_Date.Value)
                prpty.Invoice_No = txt_Invoice_No.Text
                If cmbVendor.Enabled Then
                    prpty.Vendor_ID = Convert.ToInt32(cmbVendor.SelectedValue)
                Else
                    prpty.Vendor_ID = -1
                End If
                prpty.Remarks = txtMrnRemarks.Text
                prpty.Po_ID = "-1"
                prpty.MRN_PREFIX = MRN_Code
                prpty.MRN_NO = MRN_No
                prpty.mrn_status = Convert.ToInt32(GlobalModule.MRNStatus.normal)
                prpty.Created_By = v_the_current_logged_in_user_name
                prpty.Creation_Date = Now
                prpty.Modified_By = ""
                prpty.Modification_Date = NULL_DATE
                prpty.Division_ID = v_the_current_division_id
                prpty.Other_Charges = Convert.ToDouble(txtotherchrgs.Text)
                prpty.Discount_amt = Convert.ToDouble(txtdiscount.Text)
                prpty.GROSS_AMOUNT = Convert.ToDouble(lblgrossamt.Text)
                prpty.GST_AMOUNT = Convert.ToDouble(lblvatamt.Text)
                prpty.NET_AMOUNT = Convert.ToDouble(lblnetamt.Text)
                prpty.MRNType = Convert.ToInt32(cmbMRNType.SelectedValue)

                If chk_VatCal.Checked = True Then
                    prpty.VAT_ON_EXICE = 1
                Else
                    prpty.VAT_ON_EXICE = 0
                End If

                If rb_Amount.Checked Then
                    prpty.freight_type = "A"
                    prpty.freight = Convert.ToDecimal(txt_Amount.Text)
                Else
                    prpty.freight_type = "P"
                    prpty.freight = Convert.ToDecimal(txt_Percentage.Text)
                End If
                prpty.MRNCompanies_ID = Convert.ToInt16(cmb_MRNAgainst.SelectedValue)
                'SAVE MASTER ENTERY
                If flag = "save" Then
                    clsObj.insert_MATERIAL_RECIEVED_WITHOUT_PO_MASTER(prpty, cmd)
                Else
                    clsObj.update_MATERIAL_RECIEVED_WITHOUT_PO_MASTER(prpty)
                End If


                Dim iRowCount As Int32
                Dim iRow As Int32
                iRowCount = FLXGRD_MaterialItem.Rows.Count - 1

                For iRow = 1 To iRowCount - 1
                    If FLXGRD_MaterialItem.Item(iRow, "Batch_Qty") > 0 Then
                        prpty.Po_ID = RECEIVEDID
                        prpty.Item_ID = FLXGRD_MaterialItem.Item(iRow, "Item_Id")

                        prpty.Item_Qty = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Batch_Qty")).ToString()
                        prpty.Item_Rate = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Item_Rate"))
                        prpty.DType = FLXGRD_MaterialItem.Item(iRow, "DType")
                        prpty.DISC = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "DISC"))

                        If Convert.ToString(FLXGRD_MaterialItem.Item(iRow, "VAT_Per")) = "" Then
                            prpty.Item_vat = 0
                        Else
                            prpty.Item_vat = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "VAT_Per"))
                        End If

                        If Convert.ToString(FLXGRD_MaterialItem.Item(iRow, "Exe_per")) = "" Then
                            prpty.Item_exice = 0
                        Else
                            prpty.Item_exice = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Exe_per"))
                        End If

                        If FLXGRD_MaterialItem.Item(iRow, "Batch_no").ToString() = "" Then
                            prpty.Batch_No = "Default Batch"
                        Else
                            prpty.Batch_No = FLXGRD_MaterialItem.Item(iRow, "Batch_no").ToString()
                        End If
                        If Convert.ToString(FLXGRD_MaterialItem.Item(iRow, "Expiry_Date")) = "" Then
                            prpty.Expiry_Date = Now.AddYears(2)
                        Else
                            prpty.Expiry_Date = Convert.ToDateTime(FLXGRD_MaterialItem.Item(iRow, "Expiry_Date"))
                        End If

                        prpty.Created_By = v_the_current_logged_in_user_name
                        prpty.Creation_Date = Now
                        prpty.Modified_By = v_the_current_logged_in_user_name
                        prpty.Modification_Date = Now
                        prpty.Division_ID = v_the_current_division_id

                        'SAVE DETAIL ENTRY
                        If flag = "save" Then
                            clsObj.insert_MATERIAL_RECEIVED_WITHOUT_PO_DETAIL(prpty, cmd)
                        Else
                            clsObj.Update_MATERIAL_RECEIVED_WITHOUT_PO_DETAIL(prpty, cmd)
                        End If




                        'End If
                    End If
                Next iRow

                ''SAVE IN NON STOCKABLE ITEMS 
                Dim i As Int16
                Dim dt As DataTable
                dt = CType(FLXGRD_MatItem_NonStockable.DataSource, DataTable)
                For i = 0 To dt.Rows.Count - 1
                    'Dim item_id_ As Int16
                    'item_id_ = Convert.ToInt16(FLXGRD_MatItem_NonStockable.Item(i, "Item_Id"))
                    If Convert.ToString(dt.Rows(i)("Item_Id")) <> "" Then


                        If dt.Rows(i)("Batch_Qty").ToString() = "" Then
                            prpty.Item_Qty = 0
                        Else
                            prpty.Item_Qty = Convert.ToDouble(dt.Rows(i)("Batch_Qty").ToString())
                        End If


                        If Convert.ToString(dt.Rows(i)("VAT_Per")) = "" Then
                            prpty.Item_vat = 0
                        Else
                            prpty.Item_vat = Convert.ToDouble(dt.Rows(i)("VAT_Per"))
                        End If

                        If Convert.ToString(dt.Rows(i)("Exe_per")) = "" Then
                            prpty.Item_exice = 0
                        Else
                            prpty.Item_exice = Convert.ToDouble(dt.Rows(i)("Exe_per"))
                        End If

                        If dt.Rows(i)("Batch_no").ToString() = "" Then
                            prpty.Batch_No = "Default Batch"
                        Else
                            prpty.Batch_No = dt.Rows(i)("Batch_no").ToString()
                        End If

                        If Convert.ToString(dt.Rows(i)("Expiry_Date")) = "" Then
                            prpty.Expiry_Date = Now.AddYears(2)
                        Else
                            prpty.Expiry_Date = Convert.ToDateTime(FLXGRD_MaterialItem.Item(i, "Expiry_Date"))
                        End If

                        If Convert.ToString(dt.Rows(i)("item_rate")) = "" Then
                            prpty.Item_Rate = 0
                        Else
                            prpty.Item_Rate = Convert.ToDouble(dt.Rows(i)("item_rate"))
                        End If
                        prpty.DType = dt.Rows(i)("DType")
                        prpty.DISC = Convert.ToDouble(dt.Rows(i)("DISC"))


                        '' ''prpty.Item_exice = Convert.ToDouble(dt.Rows(i)("Batch_Qty").ToString())
                        '' ''prpty.Item_vat = Convert.ToDouble(dt.Rows(i)("Batch_Qty").ToString())
                        '' ''prpty.Batch_No = Convert.ToDouble(dt.Rows(i)("Batch_Qty").ToString())
                        '' ''prpty.Expiry_Date = dt.Rows(i)("Batch_Qty")
                        '' ''prpty.Item_Rate = Convert.ToDouble(dt.Rows(i)("Batch_Qty").ToString())

                        prpty.CostCenter_ID = Convert.ToInt16(dt.Rows(i)("CostCenter_ID"))
                        prpty.Item_ID = dt.Rows(i)("Item_Id")
                        If prpty.Item_Qty <> 0 Then _
                            clsObj.Insert_Non_Stockable_Items(prpty)
                    End If
                Next

                ''SAVE IN NON STOCKABLE ITEMS 

                Dim result As String = "MRN saved With No. " & MRN_Code & MRN_No

                MsgBox(result, MsgBoxStyle.Information, gblMessageHeading)

                'obj.MyCon_CommitTransaction(cmd)


                If flag = "save" Then
                    If MsgBox(result & vbCrLf & "Do You Want To Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                        obj.RptShow(enmReportName.RptMRNActualWithoutPOPrint, "Received_ID", CStr(prpty.Received_ID), CStr(enmDataType.D_int))
                        frm_Report.Mrn_id = CInt(prpty.Received_ID)
                        frm_Report.formName = "MRN_WithoutPO"
                    End If
                Else

                End If
                obj.Clear_All_TextBox(Me.GroupBox1.Controls)
                obj.Clear_All_ComoBox(Me.GroupBox1.Controls)
                new_initilization()
            End If
        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Function GetReceivedCode() As String

        Dim CCID As String
        Dim POCode As String
        Pre = obj.getPrefixCode("MIO_PREFIX", "DIVISION_SETTINGS")
        CCID = obj.getMaxValue("RECEIVED_ID", "MATERIAL_RECIEVED_WITHOUT_PO_MASTER")
        POCode = Pre & "" & CCID
        Return POCode
    End Function

    'Public Function GetMrnPrefix() As String
    '    Dim Pre As String
    '    Dim CCID As String
    '    Dim POCode As String
    '    Pre = obj.getPrefixCode("PREFIX", "MRN_SERIES")
    '    CCID = obj.getMaxValue("RECEIVED_ID", "MATERIAL_RECIEVED_WITHOUT_PO_MASTER")
    '    POCode = Pre & "" & CCID
    '    Return POCode
    'End Function

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            Dim cmd As SqlCommand
            cmd = obj.MyCon_BeginTransaction

            If TbPO.SelectedIndex = 0 Then
                If Convert.ToString(dgvList.SelectedRows(0).Cells("Received_ID").Value) <> "" Then _
                obj.RptShow(enmReportName.RptMRNActualWithoutPOPrint, "Received_ID", CStr(dgvList.SelectedRows(0).Cells("Received_ID").Value), CStr(enmDataType.D_int))
                frm_Report.Mrn_id = CInt(dgvList.SelectedRows(0).Cells("Received_ID").Value)
                frm_Report.formName = "MRN_WithoutPO"
            Else
                If flag <> "save" Then
                    obj.RptShow(enmReportName.RptMRNActualWithoutPOPrint, "Received_ID", CStr(receive_Id), CStr(enmDataType.D_int))
                    frm_Report.Mrn_id = CInt(receive_Id)
                    frm_Report.formName = "MRN_WithoutPO"
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmbPurchaseType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPurchaseType.SelectedIndexChanged
        If cmbPurchaseType.SelectedValue = 1 Then 'LOcal Purchase
            cmbVendor.Enabled = False
        ElseIf cmbPurchaseType.SelectedValue = 2 Then 'Vendor Purchase
            cmbVendor.Enabled = True
        End If
    End Sub

    Private Sub Grid_styles()
        If Not dtable_Item_List Is Nothing Then dtable_Item_List.Dispose()
        If Not dtable_Item_List_Stockable Is Nothing Then dtable_Item_List_Stockable.Dispose()


        dtable_Item_List = New DataTable()
        dtable_Item_List.Columns.Add("Item_ID", GetType(System.Int32))
        dtable_Item_List.Columns.Add("Item_Code", GetType(System.String))
        dtable_Item_List.Columns.Add("Item_Name", GetType(System.String))
        dtable_Item_List.Columns.Add("UM_Name", GetType(System.String))
        '  dtable_Item_List.Columns.Add("Item_Qty", GetType(System.Double))
        dtable_Item_List.Columns.Add("BATCH_QTY", GetType(System.Double))
        dtable_Item_List.Columns.Add("Item_Rate", GetType(System.Double))
        dtable_Item_List.Columns.Add("DType", GetType(System.String))
        dtable_Item_List.Columns.Add("DISC", GetType(System.Decimal))

        dtable_Item_List.Columns.Add("Vat_Per", GetType(System.Double))
        dtable_Item_List.Columns.Add("exe_Per", GetType(System.Double))
        dtable_Item_List.Columns.Add("BATCH_NO", GetType(System.String))
        dtable_Item_List.Columns.Add("EXPIRY_DATE", GetType(System.DateTime))



        dtable_Item_List_Stockable = New DataTable()
        dtable_Item_List_Stockable.Columns.Add("SNO", GetType(System.Int32))
        dtable_Item_List_Stockable.Columns.Add("Item_ID", GetType(System.Int32))
        dtable_Item_List_Stockable.Columns.Add("Item_Code", GetType(System.String))
        dtable_Item_List_Stockable.Columns.Add("Item_Name", GetType(System.String))
        dtable_Item_List_Stockable.Columns.Add("UM_Name", GetType(System.String))
        dtable_Item_List_Stockable.Columns.Add("CostCenter_ID", GetType(System.Int32))
        dtable_Item_List_Stockable.Columns.Add("CostCenter_Code", GetType(System.String))
        dtable_Item_List_Stockable.Columns.Add("CostCenter_Name", GetType(System.String))
        dtable_Item_List_Stockable.Columns.Add("BATCH_QTY", GetType(System.Double))
        dtable_Item_List_Stockable.Columns.Add("Item_Rate", GetType(System.Double))
        dtable_Item_List_Stockable.Columns.Add("DType", GetType(System.String))
        dtable_Item_List_Stockable.Columns.Add("DISC", GetType(System.Decimal))
        dtable_Item_List_Stockable.Columns.Add("Vat_Per", GetType(System.Double))
        dtable_Item_List_Stockable.Columns.Add("exe_Per", GetType(System.Double))
        dtable_Item_List_Stockable.Columns.Add("BATCH_NO", GetType(System.String))
        dtable_Item_List_Stockable.Columns.Add("EXPIRY_DATE", GetType(System.DateTime))


        FLXGRD_MaterialItem.DataSource = dtable_Item_List
        FLXGRD_MatItem_NonStockable.DataSource = dtable_Item_List_Stockable

        dtable_Item_List.Rows.Add(dtable_Item_List.NewRow)
        dtable_Item_List_Stockable.Rows.Add(dtable_Item_List_Stockable.NewRow)
        FLXGRD_MaterialItem.Cols(0).Width = 10
        FLXGRD_MaterialItem.Cols(0).AllowEditing = False
        FLXGRD_MatItem_NonStockable.Cols(0).Width = 10

        SetGridSettingValues()


    End Sub

    Private Sub SetGridSettingValues()

        FLXGRD_MaterialItem.Cols(1).Visible = False

        FLXGRD_MaterialItem.Cols("Item_Code").Caption = "Item Code"
        FLXGRD_MaterialItem.Cols("Item_Name").Caption = "Item Name"
        FLXGRD_MaterialItem.Cols("UM_Name").Caption = "UOM"
        ' FLXGRD_MaterialItem.Cols("Item_Qty").Caption = "Item Qty"
        FLXGRD_MaterialItem.Cols("Item_Rate").Caption = "Item Rate"
        FLXGRD_MaterialItem.Cols("DType").Caption = "DType"
        FLXGRD_MaterialItem.Cols("DISC").Caption = "DISC"

        FLXGRD_MaterialItem.Cols("Vat_Per").Caption = "GST%"
        FLXGRD_MaterialItem.Cols("exe_Per").Caption = "EXICE%"

        FLXGRD_MaterialItem.Cols("BATCH_NO").Caption = "Batch No."
        FLXGRD_MaterialItem.Cols("EXPIRY_DATE").Caption = "Expiry Date"
        FLXGRD_MaterialItem.Cols("BATCH_QTY").Caption = "Batch Qty"

        FLXGRD_MaterialItem.Cols("Item_Code").AllowEditing = False
        FLXGRD_MaterialItem.Cols("Item_Name").AllowEditing = False
        FLXGRD_MaterialItem.Cols("UM_Name").AllowEditing = False
        'FLXGRD_MaterialItem.Cols("Item_Qty").AllowEditing = True
        FLXGRD_MaterialItem.Cols("Item_Rate").AllowEditing = True
        FLXGRD_MaterialItem.Cols("DType").AllowEditing = True
        FLXGRD_MaterialItem.Cols("DType").ComboList = "P|A"
        FLXGRD_MaterialItem.Cols("DISC").AllowEditing = True

        FLXGRD_MaterialItem.Cols("Vat_Per").AllowEditing = True
        FLXGRD_MaterialItem.Cols("Vat_Per").ComboList = "0|5|12|18|28"
        FLXGRD_MaterialItem.Cols("exe_Per").AllowEditing = True
        FLXGRD_MaterialItem.Cols("BATCH_NO").AllowEditing = True
        FLXGRD_MaterialItem.Cols("EXPIRY_DATE").AllowEditing = True
        FLXGRD_MaterialItem.Cols("BATCH_QTY").AllowEditing = True

        FLXGRD_MaterialItem.Cols("Item_Code").Width = 65
        FLXGRD_MaterialItem.Cols("Item_Name").Width = 320
        FLXGRD_MaterialItem.Cols("UM_Name").Width = 40
        FLXGRD_MaterialItem.Cols("Item_Rate").Width = 70
        FLXGRD_MaterialItem.Cols("DType").Width = 45
        FLXGRD_MaterialItem.Cols("DISC").Width = 50
        FLXGRD_MaterialItem.Cols("Vat_Per").Width = 50
        FLXGRD_MaterialItem.Cols("exe_Per").Width = 55
        FLXGRD_MaterialItem.Cols("BATCH_NO").Width = 60
        FLXGRD_MaterialItem.Cols("EXPIRY_DATE").Width = 80
        FLXGRD_MaterialItem.Cols("BATCH_QTY").Width = 80

        FLXGRD_MaterialItem.Cols("exe_Per").Visible = False
        'FLXGRD_MaterialItem.Cols(11).Width = 120



        ''**************************************************************
        '' FleXGrid for Stockable Items
        ''**************************************************************

        FLXGRD_MatItem_NonStockable.Cols(1).Visible = False
        FLXGRD_MatItem_NonStockable.Cols(2).Visible = False
        FLXGRD_MatItem_NonStockable.Cols("CostCenter_ID").Visible = False
        FLXGRD_MatItem_NonStockable.Cols("CostCenter_code").Visible = False
        FLXGRD_MatItem_NonStockable.Cols("Item_ID").Visible = False
        FLXGRD_MatItem_NonStockable.Cols("Item_Code").Caption = "Item Code"
        FLXGRD_MatItem_NonStockable.Cols("Item_Name").Caption = "Item Name"
        FLXGRD_MatItem_NonStockable.Cols("UM_Name").Caption = "UOM"
        ' FLXGRD_MatItem_NonStockable.Cols("Item_Qty").Caption = "Item Qty"
        FLXGRD_MatItem_NonStockable.Cols("Item_Rate").Caption = "Item Rate"
        FLXGRD_MatItem_NonStockable.Cols("DType").Caption = "DType"
        FLXGRD_MatItem_NonStockable.Cols("DISC").Caption = "DISC"
        FLXGRD_MatItem_NonStockable.Cols("Vat_Per").Caption = "GST%"

        FLXGRD_MatItem_NonStockable.Cols("exe_Per").Caption = "EXICE%"
        FLXGRD_MatItem_NonStockable.Cols("exe_Per").Visible = False


        FLXGRD_MatItem_NonStockable.Cols("BATCH_NO").Caption = "Batch No."
        FLXGRD_MatItem_NonStockable.Cols("EXPIRY_DATE").Caption = "Expiry Date"
        FLXGRD_MatItem_NonStockable.Cols("BATCH_QTY").Caption = "Batch Qty"

        FLXGRD_MatItem_NonStockable.Cols("Item_Code").AllowEditing = False
        FLXGRD_MatItem_NonStockable.Cols("Item_Name").AllowEditing = False
        FLXGRD_MatItem_NonStockable.Cols("UM_Name").AllowEditing = False
        'FLXGRD_MatItem_NonStockable.Cols("Item_Qty").AllowEditing = True
        FLXGRD_MatItem_NonStockable.Cols("Item_Rate").AllowEditing = True
        FLXGRD_MatItem_NonStockable.Cols("DType").AllowEditing = True
        FLXGRD_MatItem_NonStockable.Cols("DType").ComboList = "P|A"
        FLXGRD_MatItem_NonStockable.Cols("DISC").AllowEditing = True

        FLXGRD_MatItem_NonStockable.Cols("Vat_Per").AllowEditing = True
        FLXGRD_MatItem_NonStockable.Cols("Vat_Per").ComboList = "0|3|5|12|18|28"
        FLXGRD_MatItem_NonStockable.Cols("exe_Per").AllowEditing = True
        FLXGRD_MatItem_NonStockable.Cols("BATCH_NO").AllowEditing = True
        FLXGRD_MatItem_NonStockable.Cols("EXPIRY_DATE").AllowEditing = True
        FLXGRD_MatItem_NonStockable.Cols("BATCH_QTY").AllowEditing = True



        FLXGRD_MatItem_NonStockable.Cols("Item_Code").Width = 65
        FLXGRD_MatItem_NonStockable.Cols("Item_Name").Width = 265
        FLXGRD_MatItem_NonStockable.Cols("UM_Name").Width = 40
        FLXGRD_MatItem_NonStockable.Cols("Item_Rate").Width = 60
        FLXGRD_MatItem_NonStockable.Cols("DType").Width = 45
        FLXGRD_MatItem_NonStockable.Cols("DISC").Width = 50

        FLXGRD_MatItem_NonStockable.Cols("Vat_Per").Width = 40
        FLXGRD_MatItem_NonStockable.Cols("exe_Per").Width = 30
        FLXGRD_MatItem_NonStockable.Cols("BATCH_NO").Width = 60
        FLXGRD_MatItem_NonStockable.Cols("EXPIRY_DATE").Width = 70
        FLXGRD_MatItem_NonStockable.Cols("BATCH_QTY").Width = 60
        'FLXGRD_MatItem_NonStockable.Cols(11).Width = 120



    End Sub

    Private Sub FLXGRD_MaterialItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FLXGRD_MaterialItem.KeyDown
        Try
            'If e.KeyCode = Keys.Space Then
            '    grdMaterial_Rowindex = FLXGRD_MaterialItem.Row

            '    frm_Batch_Entry_Qty.Show()
            'End If

            If e.KeyCode = Keys.Space Then
                grdMaterial_Rowindex = FLXGRD_MaterialItem.Row

                frm_Show_search.qry = " Select " &
                                       " ITEM_MASTER.ITEM_ID,   " &
                                       " ITEM_MASTER.ITEM_CODE, " &
                                       " ITEM_MASTER.ITEM_NAME, " &
                                       " /*ITEM_MASTER.ITEM_DESC,*/ " &
                                       " UNIT_MASTER.UM_Name,   " &
                                       " /*ITEM_CATEGORY.ITEM_CAT_NAME, */" &
                                       " ITEM_DETAIL.IS_STOCKABLE " &
                               " FROM " &
                                       " ITEM_MASTER " &
                                       " INNER JOIN UNIT_MASTER On ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID " &
                                       " INNER JOIN ITEM_CATEGORY On ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID " &
                                        "INNER JOIN ITEM_DETAIL On ITEM_MASTER.ITEM_ID = ITEM_DETAIL.ITEM_ID And ITEM_DETAIL.is_active=1 "


                frm_Show_search.column_name = "Item_Name"
                frm_Show_search.cols_no_for_width = "1,2,3,4"
                frm_Show_search.cols_width = "60,350,50,60"
                frm_Show_search.extra_condition = ""
                frm_Show_search.ret_column = "Item_ID"
                frm_Show_search.item_rate_column = ""
                frm_Show_search.ShowDialog()

                Dim item_is_stockable As Boolean
                item_is_stockable = Convert.ToBoolean(obj.ExecuteScalar("Select isnull(IS_STOCKABLE,1) IS_STOCKABLE from ITEM_DETAIL where item_id = " + frm_Show_search.search_result))
                If item_is_stockable = True Then
                    get_row(frm_Show_search.search_result)
                Else
                    get_row_Stockable(frm_Show_search.search_result)
                End If

                frm_Show_search.Close()

            ElseIf e.KeyCode = Keys.Delete Then

                Dim result As Integer
                Dim item_code As String
                result = MsgBox("Do you want To remove """ & FLXGRD_MaterialItem.Rows(FLXGRD_MaterialItem.CursorCell.r1).Item(3) & """ from the list?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
                item_code = FLXGRD_MaterialItem.Rows(FLXGRD_MaterialItem.CursorCell.r1).Item("item_code")
                If result = MsgBoxResult.Yes Then
restart:
                    Dim dt As DataTable
                    dt = TryCast(FLXGRD_MaterialItem.DataSource, DataTable)
                    dt.AcceptChanges()
                    If Not dt Is Nothing Then
                        For Each dr As DataRow In dt.Rows
                            If Convert.ToString(dr("item_code")) = item_code Then
                                dr.Delete()
                                dt.AcceptChanges()
                                GoTo restart
                            End If
                        Next
                        '        dt.AcceptChanges()
                    End If
                    Calculate_Amount()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub get_row(ByVal item_id As String)
        Dim IsInsert As Boolean
        Dim ds As DataSet
        Dim drItem As DataRow
        'Dim dTable_OpenPoItem As New DataTable
        Try
            If item_id <> -1 Then



                ds = obj.fill_Data_set("GET_ITEM_BY_ID", "@V_ITEM_ID", item_id)
                Dim iRowCount As Int32
                Dim iRow As Int32
                iRowCount = FLXGRD_MaterialItem.Rows.Count
                IsInsert = True
                For iRow = 1 To iRowCount - 2
                    If FLXGRD_MaterialItem.Item(iRow, 1) = Convert.ToInt32(ds.Tables(0).Rows(0)(0)) Then
                        MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
                        IsInsert = False
                        Exit For
                    End If
                Next iRow
                'dTable_IndentItems = ds.Tables(0)
                'dTable_OpenPoItem = FLXGRD_MaterialItem.DataSource
                'If dtable_Item_List Is Nothing Then
                'dTable_OpenPoItem = ds.Tables(0).Copy
                'FLXGRD_MaterialItem.DataSource = dTable_OpenPoItem
                obj.RemoveBlankRow(dtable_Item_List, "item_id")
                If IsInsert = True Then
                    drItem = dtable_Item_List.NewRow

                    drItem("Item_Id") = ds.Tables(0).Rows(0)(0)
                    drItem("Item_Code") = ds.Tables(0).Rows(0)("Item_Code").ToString()
                    drItem("Item_Name") = ds.Tables(0).Rows(0)("Item_Name").ToString()
                    drItem("um_Name") = ds.Tables(0).Rows(0)("UM_Name").ToString()
                    drItem("BATCH_QTY") = 0
                    drItem("item_rate") = ds.Tables(0).Rows(0)("prev_mrn_rate")
                    drItem("DType") = "P"
                    drItem("DISC") = 0
                    drItem("Vat_Per") = 0
                    dtable_Item_List.Rows.Add(drItem)
                    dtable_Item_List.Rows.Add(dtable_Item_List.NewRow)

                    'FLXGRD_MaterialItem.DataSource = dtable_Item_List
                    'Dim introw As Integer
                    'introw = FLXGRD_MaterialItem.Rows.Count
                    'FLXGRD_MaterialItem.Rows.Add()
                    'FLXGRD_MaterialItem.Item(introw - 1, "Item_ID") = ds.Tables(0).Rows(0)("Item_Id").ToString()
                    'FLXGRD_MaterialItem.Item(introw - 1, "Item_Code") = ds.Tables(0).Rows(0)("Item_Code").ToString()
                    'FLXGRD_MaterialItem.Item(introw - 1, "Item_Name") = ds.Tables(0).Rows(0)("Item_Name").ToString()
                    'FLXGRD_MaterialItem.Item(introw - 1, "UM_Name") = ds.Tables(0).Rows(0)("UM_NAME").ToString()
                End If
            End If
            generate_tree()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub get_row_Stockable(ByVal item_id As String)
        Dim IsInsert As Boolean
        Dim ds As DataSet
        Dim ds_CC As DataSet
        Dim drItem As DataRow
        'Dim dTable_OpenPoItem As New DataTable
        Try
            If item_id <> -1 Then



                ds = obj.fill_Data_set("GET_ITEM_BY_ID", "@V_ITEM_ID", item_id)
                ds_CC = obj.Fill_DataSet("Select * FROM dbo.COST_CENTER_MASTER where display_at_mrn = 1")
                Dim iRowCount As Int32
                Dim iRow As Int32
                Dim dt As DataTable
                dt = CType(FLXGRD_MatItem_NonStockable.DataSource, DataTable)
                iRowCount = dt.Rows.Count
                IsInsert = True
                For iRow = 0 To iRowCount - 1
                    If Not IsDBNull(dt.Rows(iRow)("item_id")) Then
                        If dt.Rows(iRow)("item_id") = Convert.ToInt32(ds.Tables(0).Rows(0)(0)) Then
                            MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
                            IsInsert = False
                            Exit For
                        End If
                    End If
                Next iRow
                'dTable_IndentItems = ds.Tables(0)
                'dTable_OpenPoItem = FLXGRD_MaterialItem.DataSource
                'If dtable_Item_List Is Nothing Then
                'dTable_OpenPoItem = ds.Tables(0).Copy
                'FLXGRD_MaterialItem.DataSource = dTable_OpenPoItem
                obj.RemoveBlankRow(dtable_Item_List_Stockable, "item_id")
                If IsInsert = True Then


                    'drItem = dtable_Item_List_Stockable.NewRow

                    'drItem("Item_Id") = ds.Tables(0).Rows(0)(0)
                    'drItem("Item_Code") = ds.Tables(0).Rows(0)("Item_Code").ToString()
                    'drItem("Item_Name") = ds.Tables(0).Rows(0)("Item_Name").ToString()
                    'drItem("um_Name") = ds.Tables(0).Rows(0)("UM_Name").ToString()

                    'dtable_Item_List_Stockable.Rows.Add(drItem)
                    Dim i As Int16
                    For i = 0 To ds_CC.Tables(0).Rows.Count - 1

                        drItem = dtable_Item_List_Stockable.NewRow

                        drItem("SNO") = 1
                        drItem("Item_Id") = ds.Tables(0).Rows(0)(0)
                        drItem("Item_Code") = ds.Tables(0).Rows(0)("Item_Code").ToString()
                        drItem("Item_Name") = ds.Tables(0).Rows(0)("Item_Name").ToString()
                        drItem("um_Name") = ds.Tables(0).Rows(0)("UM_Name").ToString()
                        drItem("BATCH_QTY") = 0
                        drItem("item_rate") = ds.Tables(0).Rows(0)("prev_mrn_rate")
                        drItem("DType") = "P"
                        drItem("DISC") = 0
                        drItem("Vat_Per") = 0
                        drItem("CostCenter_Id") = ds_CC.Tables(0).Rows(i)("CostCenter_Id").ToString()
                        drItem("CostCenter_Code") = ds_CC.Tables(0).Rows(i)("CostCenter_Code").ToString()
                        drItem("CostCenter_Name") = ds_CC.Tables(0).Rows(i)("CostCenter_Name").ToString()
                        dtable_Item_List_Stockable.Rows.Add(drItem)

                    Next
                    'dtable_Item_List_Stockable.Rows.Add(dtable_Item_List_Stockable.NewRow)



                    'drItem("Item_Id") = 2
                    'drItem("Item_Code") = "Test Code"
                    'drItem("Item_Name") = "Test Name"
                    'drItem("um_Name") = "Test UM Name"

                    'dtable_Item_List_Stockable.Rows.Add(drItem)
                    'dtable_Item_List_Stockable.Rows.Add(dtable_Item_List_Stockable.NewRow)

                    'FLXGRD_MaterialItem.DataSource = dtable_Item_List
                    'Dim introw As Integer
                    'introw = FLXGRD_MaterialItem.Rows.Count
                    'FLXGRD_MaterialItem.Rows.Add()
                    'FLXGRD_MaterialItem.Item(introw - 1, "Item_ID") = ds.Tables(0).Rows(0)("Item_Id").ToString()
                    'FLXGRD_MaterialItem.Item(introw - 1, "Item_Code") = ds.Tables(0).Rows(0)("Item_Code").ToString()
                    'FLXGRD_MaterialItem.Item(introw - 1, "Item_Name") = ds.Tables(0).Rows(0)("Item_Name").ToString()
                    'FLXGRD_MaterialItem.Item(introw - 1, "UM_Name") = ds.Tables(0).Rows(0)("UM_NAME").ToString()
                End If
            End If
            generate_tree()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    'Private Sub lnkCalculatePOAmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCalculatePOAmt.Click
    '    Dim i As Integer
    '    Dim total_item_value As Double
    '    Dim total_vat_amount As Double
    '    Dim total_exice_amount As Double
    '    Dim itemamount As Double
    '    total_item_value = 0
    '    total_vat_amount = 0

    '    For i = 1 To FLXGRD_MaterialItem.Rows.Count - 1
    '        With FLXGRD_MaterialItem.Rows(i)
    '            If Not .IsNode Then
    '                itemamount = .Item("Item_Qty") * .Item("Item_Rate")
    '                total_exice_amount = (itemamount * .Item("Exice_Per")) / 100
    '                total_vat_amount = (total_exice_amount * .Item("Vat_Per")) / 100


    '            End If
    '        End With
    '    Next
    '    lblgross_amt.Text = itemamount.ToString("#0.00")
    '    lbl_total_exice.Text = total_exice_amount.ToString("#0.00")
    '    lbl_total_vat.Text = total_vat_amount.ToString("#0.00")
    'End Sub

    Private Sub FLXGRD_MaterialItem_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles FLXGRD_MaterialItem.KeyPressEdit
        'Try


        '    If e.Col = enmPODetail.ItemRate Or e.Col = enmPODetail.ExePer Or e.Col = enmPODetail.VatPer Then
        '        If obj.GridValid_Number(Asc(e.KeyChar), e.Col.GetType.ToString) = False Then
        '            e.Handled = True
        '        End If
        '        'AddHandler e.KeyChar, AddressOf obj.GridValid_Number
        '        'txtQuanity = TryCast(e.KeyChar, TextBox)
        '        ''       cmbItems = TryCast(e.Control, ComboBox)
        '        ''      If cmbItems IsNot Nothing Then
        '        ''AddHandler cmbItems.SelectedIndexChanged, AddressOf cmbItems_SelectedIndexChanged
        '        ''End If
        '        'If txtQuanity IsNot Nothing Then
        '        '    AddHandler txtQuanity.KeyDown, AddressOf txtQuanity_KeyDown

        '        'End If

        '    End If
        'Catch ex As Exception

        '    MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        'End Try
    End Sub

    Private Sub dgvList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvList.DoubleClick

        'MsgBox("You can't Edit this MRN." & vbCrLf & "To view this MRN Click on ""Print""")
        'Dim receive_Id As Integer
        Dim dtMRN As New DataTable
        Dim dtMRNDetail As New DataTable
        Dim dtMRNDetail_NonStockableItems As New DataTable
        Dim ds As New DataSet

        'receive_Id = Convert.ToInt32(dgvList.SelectedRows.Item(0).Cells(0).Value)
        receive_Id = Convert.ToInt32(dgvList.CurrentRow.Cells(0).Value)
        flag = "update"
        ds = obj.fill_Data_set("Get_MRN_WithOutPO_Detail", "@V_Receive_ID", receive_Id)

        If ds.Tables.Count > 0 Then

            dtMRN = ds.Tables(0)

            cmbPurchaseType.SelectedValue = dtMRN.Rows(0)("Purchase_Type")
            cmb_MRNAgainst.SelectedValue = dtMRN.Rows(0)("MRNCompanies_ID")
            If dtMRN.Rows(0)("Purchase_Type") = 2 Then
                cmbVendor.SelectedValue = dtMRN.Rows(0)("Vendor_Id")
            End If
            lbl_PODate.Text = dtMRN.Rows(0)("Received_Date")
            txtMrnRemarks.Text = dtMRN.Rows(0)("Remarks")
            If dtMRN.Rows(0)("MRN_Status") = MRNStatus.cancel Then
                lblMrnStatus.Text = "Cancel"
            ElseIf dtMRN.Rows(0)("MRN_Status") = MRNStatus.clear Then
                lblMrnStatus.Text = "Clear"
            ElseIf dtMRN.Rows(0)("MRN_Status") = MRNStatus.normal Then
                lblMrnStatus.Text = "Normal"
            End If
            txt_Amount.Text = Convert.ToString(dtMRN.Rows(0)("freight"))
            txtotherchrgs.Text = Convert.ToString(dtMRN.Rows(0)("Other_charges"))
            txtdiscount.Text = Convert.ToString(dtMRN.Rows(0)("Discount_amt"))
            dt_Invoice_Date.Value = dtMRN.Rows(0)("Invoice_Date")
            txt_Invoice_No.Text = dtMRN.Rows(0)("Invoice_No")
            ' Grid_styles()
            'dtable_Item_List = ds.Tables(1).Copy

            dtable_Item_List = ds.Tables(1).Copy
            dtable_Item_List.Rows.Add(dtable_Item_List.NewRow)
            FLXGRD_MaterialItem.DataSource = dtable_Item_List

            dtMRNDetail_NonStockableItems = ds.Tables(2).Copy
            FLXGRD_MatItem_NonStockable.DataSource = dtMRNDetail_NonStockableItems
            SetGridSettingValues()
            'FLXGRD_MaterialItem.DataSource = ds.Tables(1)
            'Dim iRowCount As Int32
            'Dim iRow As Int32
            'iRowCount = dtMRNDetail.Rows.Count
            'For iRow = 0 To iRowCount - 1
            '    If dtMRNDetail.Rows.Count > 0 Then
            '        Dim rowindex As Integer = dgvList.Rows.Add()
            '        dgvList.Rows(rowindex).Cells("Sup_Id").Value = Convert.ToInt32(dtMRNDetail.Rows(iRow)("SUPP_ID"))
            '        dgvList.Rows(rowindex).Cells("Item_Id").Value = Convert.ToString(dtMRNDetail.Rows(iRow)("Item_Id"))
            '        dgvList.Rows(rowindex).Cells("Item_Code").Value = Convert.ToString(dtMRNDetail.Rows(iRow)("Item_Code"))
            '        dgvList.Rows(rowindex).Cells("Item_Name").Value = Convert.ToString(dtMRNDetail.Rows(iRow)("Item_Name"))
            '        dgvList.Rows(rowindex).Cells("UOM").Value = Convert.ToString(dtMRNDetail.Rows(iRow)("UM_Name"))
            '        dgvList.Rows(rowindex).Cells("Rate").Value = Convert.ToDouble(dtMRNDetail.Rows(iRow)("ITEM_RATE"))
            '        dgvList.Rows(rowindex).Cells("Del_Qty").Value = Convert.ToDouble(dtMRNDetail.Rows(iRow)("DEL_QTY"))
            '        dgvList.Rows(rowindex).Cells("Del_Days").Value = Convert.ToString(dtMRNDetail.Rows(iRow)("DEL_DAYS"))
            '    End If
            'Next iRow
            Calculate_Amount()
            TbPO.SelectTab(1)

            ds.Dispose()
        End If
    End Sub




    Private Sub FLXGRD_MatItem_NonStockable_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FLXGRD_MatItem_NonStockable.KeyDown
        Try

            'If e.KeyCode = Keys.Space Then
            '    grdMaterial_Stockable_Rowindex = FLXGRD_MatItem_NonStockable.Row

            '    Dim rowcount As Int16
            '    'Dim iRow As Int16
            '    rowcount = FLXGRD_MatItem_NonStockable.Rows.Count
            '    Dim Item_id As Int16

            '    If FLXGRD_MatItem_NonStockable.Item(grdMaterial_Stockable_Rowindex, 1).ToString() <> "" Then
            '        Item_id = Convert.ToInt16(FLXGRD_MatItem_NonStockable.Item(grdMaterial_Stockable_Rowindex, 1))
            '        frm_ItemStockable_CC_List.ShowDialog()
            '    Else
            '        MsgBox("No Item Inserted", MsgBoxStyle.Exclamation, gblMessageHeading)
            '    End If
            '    'For iRow = 1 To rowcount - 1
            '    '    Exit For
            '    'Next iRow
            'End If

            If e.KeyCode = Keys.Delete Then
                RemoveHandler FLXGRD_MatItem_NonStockable.AfterDataRefresh, AddressOf FLXGRD_MatItem_NonStockable_AfterDataRefresh
                Dim result As Integer
                Dim item_code As String
                result = MsgBox("Do you want to remove """ & FLXGRD_MatItem_NonStockable.Rows(FLXGRD_MatItem_NonStockable.CursorCell.r1).Item(3) & """ from the list?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
                item_code = FLXGRD_MatItem_NonStockable.Rows(FLXGRD_MatItem_NonStockable.CursorCell.r1).Item("item_code")
                If result = MsgBoxResult.Yes Then
restart:
                    Dim dt As DataTable
                    dt = TryCast(FLXGRD_MatItem_NonStockable.DataSource, DataTable)
                    If Not dt Is Nothing Then
                        For Each dr As DataRow In dt.Rows
                            If dr("item_code") = item_code Then
                                dr.Delete()
                                GoTo restart
                            End If
                        Next
                        '        dt.AcceptChanges()
                    End If
                End If
                AddHandler FLXGRD_MatItem_NonStockable.AfterDataRefresh, AddressOf FLXGRD_MatItem_NonStockable_AfterDataRefresh
                generate_tree()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FLXGRD_MatItem_NonStockable_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles FLXGRD_MatItem_NonStockable.AfterDataRefresh
        Try
            generate_tree()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub generate_tree()
        Dim strSort As String = FLXGRD_MatItem_NonStockable.Cols(1).Name + " DESC" '+ ", " + FLXGRD_MatItem_NonStockable.Cols(2).Name + ", " + FLXGRD_MatItem_NonStockable.Cols(3).Name
        Dim dt As DataTable = CType(FLXGRD_MatItem_NonStockable.DataSource, DataTable)

        RemoveHandler FLXGRD_MatItem_NonStockable.AfterDataRefresh, AddressOf FLXGRD_MatItem_NonStockable_AfterDataRefresh

        If Not dt Is Nothing Then
            dt.DefaultView.Sort = ""
            dt.DefaultView.Sort = strSort
        End If
        AddHandler FLXGRD_MatItem_NonStockable.AfterDataRefresh, AddressOf FLXGRD_MatItem_NonStockable_AfterDataRefresh

        FLXGRD_MatItem_NonStockable.Tree.Style = TreeStyleFlags.CompleteLeaf
        FLXGRD_MatItem_NonStockable.Tree.Column = 3
        FLXGRD_MatItem_NonStockable.AllowMerging = AllowMergingEnum.Nodes

        Dim totalOn As Integer = FLXGRD_MatItem_NonStockable.Cols("BATCH_Qty").SafeIndex
        FLXGRD_MatItem_NonStockable.Subtotal(AggregateEnum.Sum, 0, 4, totalOn)

        Dim cs As C1.Win.C1FlexGrid.CellStyle

        cs = Me.FLXGRD_MaterialItem.Styles.Add("BATCH_QTY")
        'cs.ForeColor = Color.White
        cs.BackColor = Color.LimeGreen
        cs.Border.Style = BorderStyleEnum.Raised


        Dim cs2 As C1.Win.C1FlexGrid.CellStyle
        cs2 = Me.FLXGRD_MaterialItem.Styles.Add("BATCH_NO")
        'cs2.ForeColor = Color.White
        cs2.BackColor = Color.Gold
        cs2.Border.Style = BorderStyleEnum.Raised

        Dim cs3 As C1.Win.C1FlexGrid.CellStyle
        cs3 = Me.FLXGRD_MaterialItem.Styles.Add("expiry_date")
        'cs3.ForeColor = Color.White
        cs3.BackColor = Color.Gold
        cs3.Border.Style = BorderStyleEnum.Raised

        Dim cs1 As C1.Win.C1FlexGrid.CellStyle
        cs1 = Me.FLXGRD_MaterialItem.Styles.Add("ITEM_RATE")
        'cs2.ForeColor = Color.White
        cs1.BackColor = Color.Orange
        cs1.Border.Style = BorderStyleEnum.Raised

        Dim cs4 As C1.Win.C1FlexGrid.CellStyle
        cs4 = Me.FLXGRD_MaterialItem.Styles.Add("DType")
        'cs3.ForeColor = Color.White
        cs4.BackColor = Color.Gold
        cs4.Border.Style = BorderStyleEnum.Raised

        Dim cs5 As C1.Win.C1FlexGrid.CellStyle
        cs5 = Me.FLXGRD_MaterialItem.Styles.Add("DISC")
        'cs3.ForeColor = Color.White
        cs5.BackColor = Color.Gold
        cs5.Border.Style = BorderStyleEnum.Raised

        Dim cs6 As C1.Win.C1FlexGrid.CellStyle
        cs6 = Me.FLXGRD_MaterialItem.Styles.Add("Vat_Per")
        'cs3.ForeColor = Color.White
        cs6.BackColor = Color.Gold
        cs6.Border.Style = BorderStyleEnum.Raised

        Dim i As Integer
        For i = 1 To FLXGRD_MaterialItem.Rows.Count - 1
            If Not FLXGRD_MaterialItem.Rows(i).IsNode Then
                FLXGRD_MaterialItem.SetCellStyle(i, FLXGRD_MaterialItem.Cols("BATCH_QTY").SafeIndex, cs)
                FLXGRD_MaterialItem.SetCellStyle(i, FLXGRD_MaterialItem.Cols("ITEM_RATE").SafeIndex, cs1)
                FLXGRD_MaterialItem.SetCellStyle(i, FLXGRD_MaterialItem.Cols("BATCH_NO").SafeIndex, cs2)
                FLXGRD_MaterialItem.SetCellStyle(i, FLXGRD_MaterialItem.Cols("expiry_date").SafeIndex, cs3)
                FLXGRD_MaterialItem.SetCellStyle(i, FLXGRD_MaterialItem.Cols("DType").SafeIndex, cs4)
                FLXGRD_MaterialItem.SetCellStyle(i, FLXGRD_MaterialItem.Cols("DISC").SafeIndex, cs5)
                FLXGRD_MaterialItem.SetCellStyle(i, FLXGRD_MaterialItem.Cols("Vat_Per").SafeIndex, cs6)
            End If
        Next


    End Sub

    Private Sub FLXGRD_MatItem_NonStockable_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles FLXGRD_MatItem_NonStockable.KeyPressEdit
        e.Handled = FLXGRD_MatItem_NonStockable.Rows(FLXGRD_MatItem_NonStockable.CursorCell.r1).IsNode
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            'Call Obj.GridBind(grdItemMaster, "SELECT ITEM_MASTER.ITEM_ID,ITEM_MASTER.ITEM_CODE," _
            '  & " ITEM_MASTER.ITEM_NAME,UNIT_MASTER.UM_Name,ITEM_CATEGORY.ITEM_CAT_NAME FROM ITEM_MASTER " _
            '  & " INNER JOIN  UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID INNER JOIN ITEM_CATEGORY " _
            '  & " ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID order by Item_Master.Item_Code")
            'grdItemMaster.Columns(0).Visible = False 'Item Master id
            'grdItemMaster.Columns(0).HeaderText = "Item ID"
            'grdItemMaster.Columns(0).Width = 0
            'grdItemMaster.Columns(1).HeaderText = "Item Code"
            'grdItemMaster.Columns(1).Width = 88
            'grdItemMaster.Columns(2).HeaderText = "Item Name"
            'grdItemMaster.Columns(2).Width = 390
            'grdItemMaster.Columns(3).HeaderText = "Item Unit"
            'grdItemMaster.Columns(3).Width = 85

            'grdItemMaster.Columns(4).HeaderText = "Item Category Name"
            'grdItemMaster.Columns(4).Width = 207
            Dim condition As String
            condition = "WHERE ISNULL((MM.MRN_PREFIX + ISNULL(CAST(mm.MRN_NO AS VARCHAR(10)),'')) + PM.PurchaseType + dbo.fn_format(MM.Received_Date) + ISNULL(ACCOUNT_MASTER.ACC_NAME, '--DIRECT PURCHASE--'),'') like '%" & txtSearch.Text.Replace(" ", "%") & "%' or dbo.fn_format(MM.Received_Date) like '%" & txtSearch.Text.Replace(" ", "%") & "%' or ISNULL(ACCOUNT_MASTER.ACC_NAME, '--DIRECT PURCHASE--')  like '%" & txtSearch.Text.Replace(" ", "%") & "%'"
            condition.Replace(" ", "%")
            FillGrid(condition)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")

        End Try
    End Sub

    Private Sub BtnActualMRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnActualMRN.Click
        Try
            Dim cmd As SqlCommand
            cmd = obj.MyCon_BeginTransaction

            If TbPO.SelectedIndex = 0 Then
                If Convert.ToString(dgvList.SelectedRows(0).Cells("Received_ID").Value) <> "" Then _
                obj.RptShow(enmReportName.RptMRNActualWithoutPOPrint, "Received_ID", CStr(dgvList.SelectedRows(0).Cells("Received_ID").Value), CStr(enmDataType.D_int))

                frm_Report.Mrn_id = CInt(dgvList.SelectedRows(0).Cells("Received_ID").Value)
                frm_Report.formName = "MRN_WithoutPO"

            Else
                If flag <> "save" Then
                    obj.RptShow(enmReportName.RptMRNActualWithoutPOPrint, "Received_ID", CStr(receive_Id), CStr(enmDataType.D_int))
                    frm_Report.Mrn_id = CInt(receive_Id)
                    frm_Report.formName = "MRN_WithoutPO"
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnRevisedMRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRevisedMRN.Click
        Try
            Dim cmd As SqlCommand
            cmd = obj.MyCon_BeginTransaction

            If TbPO.SelectedIndex = 0 Then
                If Convert.ToString(dgvList.SelectedRows(0).Cells("Received_ID").Value) <> "" Then _
                obj.RptShow(enmReportName.RptMRNWithoutPOPrint, "Received_ID", CStr(dgvList.SelectedRows(0).Cells("Received_ID").Value), CStr(enmDataType.D_int))
                frm_Report.Mrn_id = CInt(dgvList.SelectedRows(0).Cells("Received_ID").Value)
                frm_Report.formName = "MRN_WithoutPO"
            Else
                If flag <> "save" Then
                    obj.RptShow(enmReportName.RptMRNWithoutPOPrint, "Received_ID", CStr(receive_Id), CStr(enmDataType.D_int))
                    frm_Report.Mrn_id = CInt(receive_Id)
                    frm_Report.formName = "MRN_WithoutPO"
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub rbPercentage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbPercentage.CheckedChanged
        txt_Percentage.Enabled = True
        txt_Amount.Enabled = False
    End Sub

    Private Sub rb_Amount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_Amount.CheckedChanged
        txt_Percentage.Enabled = False
        txt_Amount.Enabled = True
    End Sub

    Private Sub txt_Percentage_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'If (e.KeyChar < "0" OrElse e.KeyChar > "9") Then


        '    If e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "." Then
        '        'cancel keys
        '        e.Handled = True
        '        MessageBox.Show("Only Numeric Values Allowed")
        '    Else
        '        If (Convert.ToDouble(txt_Percentage.Text) > 0) Then
        '            txt_Amount.Text = Convert.ToString((total_Amt * Convert.ToDouble(txt_Percentage.Text)) / 100)
        '        End If
        '    End If
        'End If





    End Sub

    Private Sub txt_Percentage_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_Percentage.KeyUp
        Select Case Asc(e.KeyCode)
            Case AscW(ControlChars.Cr) 'Enter key
                e.Handled = True
            Case AscW(ControlChars.Back) 'Backspace
            Case 27, 45, 46, 48 To 57 'Negative sign, Decimal and Numbers
            Case Else ' Everything else
                e.Handled = True
                MessageBox.Show("Enter Valid Values")
                Exit Sub
        End Select
        If (txt_Percentage.Text <> "") Then
            If (Convert.ToDouble(txt_Percentage.Text) > 0) Then
                txt_Amount.Text = Convert.ToString((total_Amt * Convert.ToDouble(txt_Percentage.Text)) / 100)
            End If
        End If
    End Sub

    Private Sub txt_Amount_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_Amount.KeyUp
        Select Case Asc(e.KeyCode)
            Case AscW(ControlChars.Cr) 'Enter key
                e.Handled = True
            Case AscW(ControlChars.Back) 'Backspace
            Case 27, 45, 46, 48 To 57 'Negative sign, Decimal and Numbers
            Case Else ' Everything else
                e.Handled = True
                MessageBox.Show("Enter Valid Values")
                Exit Sub
        End Select
        If (txt_Amount.Text <> "") Then
            If (Convert.ToDouble(txt_Amount.Text) > 0) Then
                If total_Amt > 0 Then
                    txt_Percentage.Text = Convert.ToString((Convert.ToDouble(txt_Amount.Text) * 100) / total_Amt)
                End If

            End If
        End If
    End Sub

    Private Sub FLXGRD_MaterialItem_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles FLXGRD_MaterialItem.AfterEdit
        Calculate_Amount()
    End Sub

    Private Sub FLXGRD_MatItem_NonStockable_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles FLXGRD_MatItem_NonStockable.AfterEdit
        Calculate_Amount()
    End Sub

    Private Sub lnkCalculateAmount_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCalculateAmount.LinkClicked
        Calculate_Amount()
    End Sub

    Private Sub Calculate_Amount()
        Dim dt As DataTable
        Dim item_value As Double = 0
        Dim gross_amt As Double = 0
        Dim tot_vat_amt As Double = 0
        Dim tot_exice_amt As Double = 0
        Dim exice_per As Double = 0
        Dim Net_amt As Double = 0

        Dim discamt As Decimal = 0.0
        Dim totdiscamt As Decimal = 0.0

        dt = FLXGRD_MaterialItem.DataSource

        If Not dt Is Nothing Then
            For i As Integer = 0 To dt.Rows.Count - 2

                If (dt.Rows(i)("DType")) = "P" Then
                    discamt = Math.Round(((dt.Rows(i)("Batch_qty") * dt.Rows(i)("Item_Rate")) * dt.Rows(i)("DISC") / 100), 2)
                Else
                    discamt = Math.Round(dt.Rows(i)("DISC"), 2)
                End If

                item_value = (Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate"))) * Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))) - discamt)
                gross_amt = gross_amt + item_value
                totdiscamt = totdiscamt + discamt

                exice_per = IIf((dt.Rows(i)("Exe_Per")) Is DBNull.Value, 0, dt.Rows(i)("Exe_Per"))
                exice_per = exice_per / 100
                tot_exice_amt = tot_exice_amt + (item_value * exice_per)
                tot_vat_amt = tot_vat_amt + ((((dt.Rows(i)("Batch_qty") * dt.Rows(i)("Item_Rate")) - discamt) * dt.Rows(i)("vat_per")) / 100)

                'item_value = (Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate"))) * Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))))
                'gross_amt = gross_amt + item_value
                'exice_per = IIf((dt.Rows(i)("Exe_Per")) Is DBNull.Value, 0, dt.Rows(i)("Exe_Per"))
                'exice_per = exice_per / 100
                'tot_exice_amt = tot_exice_amt + (item_value * exice_per)
                'If chk_VatCal.Checked = True Then
                '    tot_vat_amt = tot_vat_amt + (((Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))) * Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate")))) + (item_value * exice_per)) * Convert.ToDouble(IIf((dt.Rows(i)("vat_per")) Is DBNull.Value, 0, dt.Rows(i)("vat_per"))) / 100)
                'Else
                '    tot_vat_amt = tot_vat_amt + (((Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))) * Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate"))))) * Convert.ToDouble(IIf((dt.Rows(i)("vat_per")) Is DBNull.Value, 0, dt.Rows(i)("vat_per"))) / 100)
                'End If
            Next
        End If


        dt = FLXGRD_MatItem_NonStockable.DataSource
        If Not dt Is Nothing Then
            For i As Integer = 0 To dt.Rows.Count - 2


                If (dt.Rows(i)("DType")) = "P" Then
                    discamt = Math.Round(((dt.Rows(i)("Batch_qty") * dt.Rows(i)("Item_Rate")) * dt.Rows(i)("DISC") / 100), 2)
                Else
                    discamt = Math.Round(dt.Rows(i)("DISC"), 2)
                End If

                item_value = (Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate"))) * Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))) - discamt)
                gross_amt = gross_amt + item_value
                totdiscamt = totdiscamt + discamt

                exice_per = IIf((dt.Rows(i)("Exe_Per")) Is DBNull.Value, 0, dt.Rows(i)("Exe_Per"))
                exice_per = exice_per / 100
                tot_exice_amt = tot_exice_amt + (item_value * exice_per)
                tot_vat_amt = tot_vat_amt + ((((dt.Rows(i)("Batch_qty") * dt.Rows(i)("Item_Rate")) - discamt) * dt.Rows(i)("vat_per")) / 100)


                'item_value = (Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate"))) * Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))))
                'gross_amt = gross_amt + item_value
                'exice_per = IIf((dt.Rows(i)("Exe_Per")) Is DBNull.Value, 0, dt.Rows(i)("Exe_Per"))
                'exice_per = exice_per / 100
                'tot_exice_amt = tot_exice_amt + (item_value * exice_per)
                'If chk_VatCal.Checked = True Then
                '    tot_vat_amt = tot_vat_amt + (((Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))) * Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate")))) + (item_value * exice_per)) * Convert.ToDouble(IIf((dt.Rows(i)("vat_per")) Is DBNull.Value, 0, dt.Rows(i)("vat_per"))) / 100)
                'Else
                '    tot_vat_amt = tot_vat_amt + (((Convert.ToDouble(IIf((dt.Rows(i)("Batch_qty")) Is DBNull.Value, 0, dt.Rows(i)("Batch_qty"))) * Convert.ToDouble(IIf((dt.Rows(i)("Item_Rate")) Is DBNull.Value, 0, dt.Rows(i)("Item_Rate"))))) * Convert.ToDouble(IIf((dt.Rows(i)("vat_per")) Is DBNull.Value, 0, dt.Rows(i)("vat_per"))) / 100)
                'End If

            Next
        End If

        'lblgrossamt.Text = gross_amt.ToString("0.00")
        'lblvatamt.Text = tot_vat_amt.ToString("0.00")
        'lblexciseamt.Text = tot_exice_amt.ToString("0.00")

        'Net_amt = (gross_amt + tot_vat_amt + tot_exice_amt + Convert.ToDouble(IIf(IsNumeric(txt_Amount.Text), txt_Amount.Text, 0)) + Convert.ToDouble(IIf(IsNumeric(txtotherchrgs.Text), txtotherchrgs.Text, 0))) - Convert.ToDouble(IIf(IsNumeric(txtdiscount.Text), txtdiscount.Text, 0))
        'lblnetamt.Text = Net_amt.ToString("0.00")

        txtdiscount.Text = totdiscamt.ToString("#0.00")
        lblgrossamt.Text = gross_amt.ToString("0.00")
        lblvatamt.Text = tot_vat_amt.ToString("0.00")
        lblexciseamt.Text = tot_exice_amt.ToString("0.00")

        Net_amt = (gross_amt + tot_vat_amt + tot_exice_amt + Convert.ToDouble(IIf(IsNumeric(txt_Amount.Text), txt_Amount.Text, 0)) + Convert.ToDouble(IIf(IsNumeric(txtotherchrgs.Text), txtotherchrgs.Text, 0))) ' - Convert.ToDouble(IIf(IsNumeric(txtdiscount.Text), txtdiscount.Text, 0))
        lblnetamt.Text = Net_amt.ToString("0.00")

    End Sub

    Private Sub txt_Amount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Amount.TextChanged
        Calculate_Amount()
    End Sub

    Private Sub txtotherchrgs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtotherchrgs.TextChanged
        Calculate_Amount()
    End Sub

    Private Sub txtdiscount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdiscount.TextChanged
        Calculate_Amount()
    End Sub

    Private Sub FLXGRD_MaterialItem_AfterDeleteRow(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles FLXGRD_MaterialItem.AfterDeleteRow
        Calculate_Amount()
    End Sub

    Private Sub FLXGRD_MatItem_NonStockable_AfterDeleteRow(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles FLXGRD_MatItem_NonStockable.AfterDeleteRow
        Calculate_Amount()
    End Sub

    Private Sub cmbVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVendor.SelectedIndexChanged
        Dim NewstrSql As String
        Dim dsdata As DataSet
        NewstrSql = "SELECT STATE_ID,isUT_bit FROM dbo.STATE_MASTER WHERE STATE_ID IN(SELECT STATE_ID FROM dbo.CITY_MASTER WHERE CITY_ID IN(SELECT CITY_ID FROM dbo.DIVISION_SETTINGS))"
        NewstrSql = NewstrSql & " SELECT STATE_ID,isUT_bit FROM dbo.STATE_MASTER WHERE STATE_ID IN(SELECT STATE_ID FROM dbo.CITY_MASTER WHERE CITY_ID IN(SELECT CITY_ID FROM dbo.ACCOUNT_MASTER WHERE ACC_ID=" & cmbVendor.SelectedValue & "))"
        dsdata = clsObj.Fill_DataSet(NewstrSql)
        'SCGST
        'IGST
        'UGST
        If cmbVendor.SelectedValue > 0 Then
            If dsdata.Tables(0).Rows(0)(0) <> dsdata.Tables(1).Rows(0)(0) Then
                cmbMRNType.Text = "IGST"
            Else
                If dsdata.Tables(0).Rows(0)(1) = True Then
                    cmbMRNType.Text = "UGST"
                Else
                    cmbMRNType.Text = "SGST"
                End If
            End If
        End If
    End Sub

    Private Sub txtBarcodeSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBarcodeSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrEmpty(txtBarcodeSearch.Text) Then

                Dim qry As String = "SELECT  item_id FROM    ITEM_MASTER WHERE   Barcode_vch = '" + txtBarcodeSearch.Text + "'"
                Dim id As Int32 = clsObj.ExecuteScalar(qry)
                If id > 0 Then

                    Dim item_is_stockable As Boolean
                    item_is_stockable = Convert.ToBoolean(obj.ExecuteScalar("select isnull(IS_STOCKABLE,1) IS_STOCKABLE from ITEM_DETAIL where item_id = " + id.ToString()))
                    If item_is_stockable = True Then
                        get_row(id)
                    Else
                        get_row_Stockable(id)
                    End If


                    'If Not check_item_exist(id) Then
                    ' get_row(id)
                    'End If
                End If
                txtBarcodeSearch.Text = ""
                txtBarcodeSearch.Focus()
            End If
        End If
    End Sub

    Private Sub FLXGRD_MaterialItem_AfterDataRefresh(sender As Object, e As System.ComponentModel.ListChangedEventArgs)
        Calculate_Amount()
    End Sub
End Class
