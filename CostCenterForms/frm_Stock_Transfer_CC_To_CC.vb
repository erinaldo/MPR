Imports MMSPlus.Stock_Transfer
'Imports afbsl_mms.item_master
Imports System.Data.SqlClient
Imports System.Data

Public Class frm_Stock_Transfer_CC_To_CC
    Implements IForm
    Dim flag As String
    Dim obj As New CommonClass
    Dim clsObj As New cls_Stock_Transfer_CC_To_CC
    Dim prpty As cls_Stock_Transfer_CC_To_CC_Prop
    Dim Transfer_ID As Int16
    Dim cmd As New SqlCommand


    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub


    Private Sub frm_Stock_Transfer_CC_To_CCn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' obj.FormatGrid(DGVStockTransfer)
        'obj.FormatGrid(DGVMRSMaster)
        lbl_Status.Text = "Fresh"
        lbl_TransferNo.Text = obj.getPrefixCode("TRANSFER_PREFIX", "DIVISION_SETTINGS") & Convert.ToString(obj.getMaxValue("Transfer_ID", "STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER"))

        obj.ListControlBind(cmb_CostCenters, "Select CostCenter_Id,CostCenter_Code + '---' + CostCenter_Name as CostCenter_Name from cost_center_master", "CostCenter_Name", "CostCenter_Id")
        FillGridTransferMaster()
        DGVMRSItem_style()
        new_initilization()
    End Sub

    Private Sub FillGridTransferMaster()
        Try
            'clsObj.Grid_Bind(DGVW_list_StockTransfer, "GET_Transfer_MASTER")
            clsObj.Bind_GridBind_Val(DGVW_list_StockTransfer, "GET_Transfer_MASTER", "@Division_id", "", Convert.ToString(v_the_current_Selected_CostCenter_id), "")
            DGVW_list_StockTransfer.Columns(0).Visible = False                 'Wastage_ID
            DGVW_list_StockTransfer.Columns(1).HeaderText = "Transfer No"       'Transfer_no
            DGVW_list_StockTransfer.Columns(1).Width = 150
            DGVW_list_StockTransfer.Columns(2).HeaderText = "Transfer Code"     'Transfer_code
            DGVW_list_StockTransfer.Columns(2).Visible = False
            DGVW_list_StockTransfer.Columns(3).HeaderText = "Transfer Date"     'Transfer_date
            DGVW_list_StockTransfer.Columns(3).Width = 165
            DGVW_list_StockTransfer.Columns(4).HeaderText = "Transfer Remarks"  'Transfer_remarks
            DGVW_list_StockTransfer.Columns(4).Width = 350
            DGVW_list_StockTransfer.Columns(5).Visible = False
            DGVW_list_StockTransfer.Columns(6).HeaderText = "Cost Center"     'Transfer_date
            DGVW_list_StockTransfer.Columns(6).Width = 200
            ' clsObj.FormatGrid(DGVW_list_StockTransfer)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub
    Private Sub DGVMRSItem_style()
        Try

            DGVStockTransfer.Columns.Clear()
            DGVStockTransfer.Rows.Clear()
            Dim txtMRSItemId = New DataGridViewTextBoxColumn
            Dim txtMRSItemCode = New DataGridViewTextBoxColumn
            Dim txtMRSItemName = New DataGridViewTextBoxColumn
            Dim txtMRSItemUom = New DataGridViewTextBoxColumn
            Dim txtMRSItemCategory = New DataGridViewTextBoxColumn
            Dim txtMRSItemQuantity = New DataGridViewTextBoxColumn
            Dim txtCurrentStock = New DataGridViewTextBoxColumn
            Dim txtavgrate = New DataGridViewTextBoxColumn
            'Dim cmbColItemName As New DataGridViewComboBoxColumn

            With txtMRSItemId
                .HeaderText = "Item Id"
                .Name = "Item_ID"
                .ReadOnly = True
                .Visible = False
                .Width = 10
            End With
            DGVStockTransfer.Columns.Add(txtMRSItemId)

            With txtMRSItemCode
                .HeaderText = "Item Code"
                .Name = "Item_CODE"
                .ReadOnly = True
                .Visible = True
                .Width = 100
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            DGVStockTransfer.Columns.Add(txtMRSItemCode)

            With txtMRSItemName
                .HeaderText = "Item Name"
                .Name = "Item_Name"
                .ReadOnly = True
                .Visible = True
                .Width = 250
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            DGVStockTransfer.Columns.Add(txtMRSItemName)

            With txtMRSItemCategory
                .HeaderText = "Category"
                .Name = "item_cat_name"
                .ReadOnly = True
                .Visible = True
                .Width = 150
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            DGVStockTransfer.Columns.Add(txtMRSItemCategory)

            With txtMRSItemUom
                .HeaderText = "UOM"
                .Name = "UM_Name"
                .ReadOnly = True
                .Visible = True
                .Width = 70
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            DGVStockTransfer.Columns.Add(txtMRSItemUom)

            With txtavgrate
                .HeaderText = "Avg Rate"
                .Name = "Avg_rate"
                .ReadOnly = True
                .Visible = True
                .Width = 80
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            End With
            DGVStockTransfer.Columns.Add(txtavgrate)

            With txtCurrentStock
                .HeaderText = "Current Stock"
                .Name = "Current_Stock"
                .ReadOnly = True
                .Visible = True
                .Width = 105
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            End With
            DGVStockTransfer.Columns.Add(txtCurrentStock)

            With txtMRSItemQuantity
                .HeaderText = "Quantity"
                .Name = "Quantity"
                .ReadOnly = False
                .Visible = True
                .Width = 90
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            End With
            DGVStockTransfer.Columns.Add(txtMRSItemQuantity)
            Dim CellLength As DataGridViewTextBoxColumn = TryCast(Me.DGVStockTransfer.Columns("Quantity"), DataGridViewTextBoxColumn)
            CellLength.MaxInputLength = 10
            DGVStockTransfer.AllowUserToResizeColumns = False
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub get_row(ByVal item_id As String)
        'Dim drv As DataRowView
        Dim IsInsert As Boolean
        Dim ds As DataSet
        'ds = obj.fill_Data_set("GET_ITEM_BY_ID_CC", "@V_ITEM_ID", item_id)
        ds = obj.fill_Data_set("GET_ITEM_BY_ID_CC", "@V_ITEM_ID,@V_Div_id,@V_Todate", item_id & "," & v_the_current_Selected_CostCenter_id & "," & Now.ToString("dd-MMM-yyyy"))
        Dim iRowCount As Int32
        Dim iRow As Int32
        iRowCount = DGVStockTransfer.RowCount
        IsInsert = True
        'DGVMRSItem_style()
        For iRow = 0 To iRowCount - 2
            If DGVStockTransfer.Item(0, iRow).Value = Convert.ToInt32(ds.Tables(0).Rows(0)(0)) Then
                MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, "MRS Main Store Item")
                IsInsert = False
                Exit For
            End If
        Next iRow
        Dim datatbl As New DataTable
        datatbl = ds.Tables(0)
        'DGVMRSItem.DataSource

        If IsInsert = True Then
            Dim introw As Integer
            'For Each dr As DataRow In datatbl.Rows
            ' MsgBox(DGVMRSItem.Rows.Count)
            introw = DGVStockTransfer.Rows.Count - 1
            DGVStockTransfer.Rows.Insert(introw)
            DGVStockTransfer.Rows(introw).Cells("Item_ID").Value = ds.Tables(0).Rows(0)(0)
            DGVStockTransfer.Rows(introw).Cells("Item_CODE").Value = ds.Tables(0).Rows(0)("item_Code").ToString()
            DGVStockTransfer.Rows(introw).Cells("Item_Name").Value = ds.Tables(0).Rows(0)("item_Name").ToString()
            DGVStockTransfer.Rows(introw).Cells("item_cat_name").Value = ds.Tables(0).Rows(0)("item_cat_name").ToString()
            DGVStockTransfer.Rows(introw).Cells("UM_Name").Value = ds.Tables(0).Rows(0)("UM_NAME").ToString()
            DGVStockTransfer.Rows(introw).Cells("Avg_rate").Value = ds.Tables(0).Rows(0)("Avg_Rate").ToString()
            ''New Column Added 
            DGVStockTransfer.Rows(introw).Cells("Current_Stock").Value = ds.Tables(0).Rows(0)("Current_Stock").ToString()
            ''New Column Added 
            DGVStockTransfer.Rows(introw).Cells("Quantity").Value = "0.00"
            'DGVMRSItem.Rows.Add()
            DGVStockTransfer.Rows(introw).Cells("Item_CODE").Selected = False
            DGVStockTransfer.Rows(introw).Cells("Item_Name").Selected = False
            DGVStockTransfer.Rows(introw).Cells("item_cat_name").Selected = False
            DGVStockTransfer.Rows(introw).Cells("UM_Name").Selected = False
            DGVStockTransfer.Rows(introw).Cells("Avg_rate").Selected = False
            DGVStockTransfer.Rows(introw).Cells("Current_Stock").Selected = False
            DGVStockTransfer.Rows(introw).Cells("Quantity").Selected = True

            DGVStockTransfer.Rows(introw + 1).Cells("Item_CODE").Selected = False
            DGVStockTransfer.Rows(introw + 1).Cells("Item_Name").Selected = False
            DGVStockTransfer.Rows(introw + 1).Cells("item_cat_name").Selected = False
            DGVStockTransfer.Rows(introw + 1).Cells("UM_Name").Selected = False
            DGVStockTransfer.Rows(introw + 1).Cells("Avg_rate").Selected = False
            DGVStockTransfer.Rows(introw + 1).Cells("Current_Stock").Selected = False
            DGVStockTransfer.Rows(introw + 1).Cells("Quantity").Selected = False
            'If Me.DGVMRSItem.Rows.Count > 0 Then DGVMRSItem.Rows.Add()
            ' If Me.DGVMRSItem.Rows.Count > 0 Then DGVMRSItem.Rows.Add(1)
            'Next


        End If
        'ds = obj.
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
            'FillGrid(condition)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")

        End Try
    End Sub

    Private Sub new_initilization()
        ''clear_all()
        lbl_Status.Text = "Fresh"
        lbl_TransferNo.Text = obj.getPrefixCode("TRANSFER_PREFIX", "DIVISION_SETTINGS") & Convert.ToString(obj.getMaxValue("Transfer_ID", "STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER"))
        dt_Transfer_Date.Value = Now
        txt_Remarks.Text = String.Empty
        obj.ListControlBind(cmb_CostCenters, "Select CostCenter_Id,CostCenter_Code + '---' + CostCenter_Name as CostCenter_Name from cost_center_master", "CostCenter_Name", "CostCenter_Id")
        If Not DGVStockTransfer Is Nothing Then DGVStockTransfer.Rows.Clear()
        tbctrl_stock_transfer.SelectTab(1)
        FillGridTransferMaster()
        obj.ComboBind(cmbrecipe, "SELECT MIR_ID,MIR_ITEM_NAME FROM MENU_ITEM_RECIPE", "MIR_ITEM_NAME", "MIR_ID", True)
        flag = "save"
    End Sub

    Private Sub DGVMRSItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGVStockTransfer.KeyDown
        Try
            Dim iRowindex As Int32
            If flag = "save" Then
                If e.KeyCode = Keys.Space Then
                    iRowindex = DGVStockTransfer.CurrentRow.Index

                    'frm_Show_search.qry = "Select Item_master.ITEM_ID,Item_master.ITEM_CODE as [Item Code],Item_master.ITEM_NAME  as [Item Name] from Item_master inner join item_detail on item_master.item_id = item_detail.item_id " 'where item_detail.div_id = '" + Convert.ToString(v_the_curre) + "'"
                    'frm_Show_search.extra_condition = ""
                    'frm_Show_search.ret_column = "Item_ID"
                    'frm_Show_search.column_name = "ITEM_NAME"
                    'frm_Show_search.item_rate_column = ""
                    'frm_Show_search.cols_width = "80,300"
                    'frm_Show_search.cols_no_for_width = "1,2"
                    'frm_Show_search.ShowDialog()

                    frm_Show_search.qry = " SELECT  top 50 im.ITEM_ID ,
		                                ISNULL(im.BarCode_vch, '') AS BARCODE ,
                                        im.ITEM_NAME AS [ITEM NAME] ,
                                        im.MRP_Num AS MRP ,
                                        CAST(im.sale_rate AS NUMERIC(18, 2)) AS RATE ,
                                        ISNULL(litems.LabelItemName_vch, '') AS BRAND ,
                                        ic.ITEM_CAT_NAME AS CATEGORY
                                        FROM      Item_master im
                                        LEFT OUTER JOIN item_detail id ON im.item_id = id.item_id
                                        LEFT OUTER JOIN dbo.ITEM_CATEGORY ic ON im.ITEM_CATEGORY_ID = ic.ITEM_CAT_ID
                                        LEFT OUTER JOIN dbo.LabelItem_Mapping lim ON lim.Fk_ItemId_Num = im.ITEM_ID
                                        LEFT OUTER JOIN dbo.Label_Items litems ON lim.Fk_LabelDetailId = litems.Pk_LabelDetailId_Num
                                        WHERE   id.Is_active = 1 "

                    frm_Show_search.column_name = "BARCODE_VCH"
                    frm_Show_search.column_name1 = "ITEM_NAME"
                    frm_Show_search.column_name2 = "MRP_Num"
                    frm_Show_search.column_name3 = "SALE_RATE"
                    frm_Show_search.column_name4 = "LABELITEMNAME_VCH"
                    frm_Show_search.column_name5 = "ITEM_CAT_NAME"
                    frm_Show_search.cols_no_for_width = "1,2,3,4,5,6"
                    frm_Show_search.cols_width = "100,350,60,60,100,100"
                    frm_Show_search.extra_condition = ""
                    frm_Show_search.ret_column = "ITEM_ID"
                    frm_Show_search.item_rate_column = ""
                    frm_Show_search.ShowDialog()

                    get_row(frm_Show_search.search_result)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        new_initilization()
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

        If Validation() = False Then
            Exit Sub
        End If
        Dim cmd As SqlCommand
        cmd = obj.MyCon_BeginTransaction
        Try
            lbl_Status.Focus() ' to remove error input string is not in correct format. remove focus from grid  before saving process start
            If flag = "save" Then
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If
                Dim rowcount As Int32
                For rowcount = 0 To DGVStockTransfer.RowCount - 1
                    If Convert.ToDouble(DGVStockTransfer.Item("Quantity", rowcount).Value()) > Convert.ToDouble(DGVStockTransfer.Item("Current_Stock", rowcount).Value()) Then
                        MsgBox("Quantity Cannot be greater than Current Stock")
                        Exit Sub
                    End If
                Next
                prpty = New cls_Stock_Transfer_CC_To_CC_Prop
                Transfer_ID = Convert.ToInt32(obj.getMaxValue("Transfer_ID", "STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER"))
                prpty.Transfer_ID = Transfer_ID
                ' prpty.INDENT_CODE = lbl_IndentCode.Text()
                prpty.Transfer_Code = obj.getPrefixCode("TRANSFER_PREFIX", "DIVISION_SETTINGS")
                prpty.Transfer_No = Convert.ToInt32(obj.getMaxValue("TRANSFER_ID", "STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER"))
                prpty.Transfer_Date = dt_Transfer_Date.Value
                prpty.Transfer_Status = Convert.ToInt32(GlobalModule.TRANSFER_STATUS.Fresh)
                prpty.Transfer_CC_ID = Convert.ToInt16(cmb_CostCenters.SelectedValue)
                prpty.Received_Date = Convert.ToDateTime("1/1/1900")
                prpty.Transfer_Remarks = txt_Remarks.Text()
                prpty.Created_By = v_the_current_logged_in_user_name
                prpty.Creation_Date = Now
                prpty.Modified_By = v_the_current_logged_in_user_name
                prpty.Modification_Date = Now
                prpty.Division_ID = v_the_current_division_id
                prpty.CostCenter_Id = v_the_current_Selected_CostCenter_id
                clsObj.INSERT_STOCK_TRANSFER_CC(prpty, cmd)
                Dim iRowCount As Int32
                Dim iRow As Int32
                iRowCount = DGVStockTransfer.RowCount
                For iRow = 0 To iRowCount - 1
                    If DGVStockTransfer.Item("Quantity", iRow).Value() IsNot Nothing And Convert.ToString(DGVStockTransfer.Item("Quantity", iRow).Value) <> "" Then
                        If Convert.ToDouble(DGVStockTransfer.Item("Quantity", iRow).Value()) > 0 Then

                            prpty = New cls_Stock_Transfer_CC_To_CC_Prop
                            prpty.Transfer_ID = Transfer_ID
                            prpty.Division_ID = v_the_current_division_id
                            prpty.CostCenter_Id = v_the_current_Selected_CostCenter_id
                            prpty.Item_ID = Convert.ToInt32(DGVStockTransfer.Item("Item_ID", iRow).Value)
                            prpty.Item_Qty = Convert.ToDouble(DGVStockTransfer.Item("Quantity", iRow).Value)
                            prpty.Accepted_Qty = 0
                            prpty.Returned_Qty = 0
                            prpty.Transfer_Rate = Convert.ToDouble(DGVStockTransfer.Item("Avg_Rate", iRow).Value)
                            prpty.Created_By = v_the_current_logged_in_user_name
                            prpty.Creation_Date = Now
                            prpty.Modified_By = v_the_current_logged_in_user_name
                            prpty.Modification_Date = Now
                            clsObj.INSERT_STOCK_TRANSFER_CC_DETAIL(prpty, cmd)
                        End If
                    End If
                Next iRow

                'MsgBox("Indent information has been Saved", MsgBoxStyle.Information, gblMessageHeading)
            Else
            End If
            obj.MyCon_CommitTransaction(cmd)
            new_initilization()
            If flag = "save" Then
                If MsgBox("Transfer information has been Saved" & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptStockTransferCCPrint, "Transfer_ID", CStr(prpty.Transfer_ID), CStr(enmDataType.D_int))
                End If
                MsgBox("Transfer information has been Saved")
            Else
                'If MsgBox("Indent information has been Updated" & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                '    obj.RptShow(enmReportName.RptIndentDetailPrint, "indent_id", CStr(prpty.Transfer_ID), CStr(enmDataType.D_int))
                'End If
            End If
        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)

            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If tbctrl_stock_transfer.SelectedIndex = 0 Then
                obj.RptShow(enmReportName.RptStockTransferCCPrint, "Transfer_id", CStr(DGVW_list_StockTransfer("Transfer_id", DGVW_list_StockTransfer.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                If flag <> "save" Then
                    obj.RptShow(enmReportName.RptStockTransferCCPrint, "Tranfer_id", CStr(DGVW_list_StockTransfer("Transfer_id", DGVW_list_StockTransfer.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function Validation() As Boolean
        Validation = True
        Dim blnRecExist As Boolean
        Dim blnChkQuantity As Boolean
        blnRecExist = False
        blnChkQuantity = False


        If tbctrl_stock_transfer.SelectedIndex = 0 Then
            Validation = False
            Exit Function
        End If
        Dim iRow As Int16
        For iRow = 0 To DGVStockTransfer.RowCount - 1
            If Val(DGVStockTransfer.Item("Quantity", iRow).Value) > 0 And blnRecExist = False Then
                blnRecExist = True
                'Exit For
            End If

        Next iRow

        If blnRecExist = True Then
            Validation = True
            Exit Function
        Else
            Validation = False
            MsgBox("Select aleast one valid item in Transfer to save indent information", vbExclamation, gblMessageHeading)
            Exit Function
        End If

    End Function

    Private Sub DGVW_list_StockTransfer_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVW_list_StockTransfer.CellValueChanged
       
    End Sub

    Private Sub DGVStockTransfer_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVStockTransfer.CellValueChanged
        Try
            Dim row As Integer = e.RowIndex

            If e.ColumnIndex() = 7 Then
                If IsNumeric(Convert.ToDouble(DGVStockTransfer.Item("Quantity", row).Value())) Then
                    If (Convert.ToDouble(DGVStockTransfer.Item("Quantity", row).Value())) > (Convert.ToDouble(DGVStockTransfer.Item("current_stock", row).Value())) Then
                        DGVStockTransfer.Item("Quantity", row).Value() = 0
                    End If
                    'DGVStockTransfer.Rows(e.RowIndex + 1).Cells("Item_Name").Selected = True
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Public Sub get_recipe_items(ByVal item_id As String, ByVal qty As String)
        
        Dim ds As DataSet
        Dim chk As Char
        If rdbrecipe.Checked = True Then
            chk = "R"
        Else
            chk = "S"
        End If

        ds = obj.fill_Data_set("Get_recipe_items", "@V_ITEM_ID,@V_Div_id,@V_Todate,@V_qty,@V_chk", item_id & "," & v_the_current_Selected_CostCenter_id & "," & dt_Transfer_Date.Value.ToString("dd-MMM-yyyy") & "," & qty & "," & chk)


        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            If Convert.ToDouble(ds.Tables(0).Rows(i)("qty")) > Convert.ToDouble(ds.Tables(0).Rows(i)("Current_stock")) Then
                MsgBox("Transfer quantity cannot be greater than current stock")
                Exit Sub
            End If
        Next

        Dim rowcount = ds.Tables(0).Rows.Count - 1

        For irow As Integer = 0 To DGVStockTransfer.Rows.Count - 1
            Dim index = 0
            While index <= rowcount
                If Convert.ToInt32(ds.Tables(0).Rows(index)("Item_ID")) = Convert.ToInt32(DGVStockTransfer.Rows(irow).Cells("Item_ID").Value) Then
                    DGVStockTransfer.Rows(irow).Cells("Quantity").Value = Convert.ToDouble(DGVStockTransfer.Rows(irow).Cells("Quantity").Value) + Convert.ToDouble(ds.Tables(0).Rows(index)("qty"))
                    ds.Tables(0).Rows.RemoveAt(index)
                    rowcount = rowcount - 1
                End If
                index = index + 1
            End While
        Next


        Dim datatbl As New DataTable
        datatbl = ds.Tables(0)
        'DGVMRSItem.DataSource

        If ds.Tables(0).Rows.Count > 0 Then

            Dim introw As Integer
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                'For Each dr As DataRow In datatbl.Rows
                ' MsgBox(DGVMRSItem.Rows.Count)
                introw = DGVStockTransfer.Rows.Count - 1
                DGVStockTransfer.Rows.Insert(introw)
                DGVStockTransfer.Rows(introw).Cells("Item_ID").Value = ds.Tables(0).Rows(i)(0)
                DGVStockTransfer.Rows(introw).Cells("Item_CODE").Value = ds.Tables(0).Rows(i)("item_Code").ToString()
                DGVStockTransfer.Rows(introw).Cells("Item_Name").Value = ds.Tables(0).Rows(i)("item_Name").ToString()
                DGVStockTransfer.Rows(introw).Cells("item_cat_name").Value = ds.Tables(0).Rows(i)("item_cat_name").ToString()
                DGVStockTransfer.Rows(introw).Cells("UM_Name").Value = ds.Tables(0).Rows(i)("UM_NAME").ToString()
                DGVStockTransfer.Rows(introw).Cells("Avg_rate").Value = ds.Tables(0).Rows(i)("Avg_Rate").ToString()
                ''New Column Added 
                DGVStockTransfer.Rows(introw).Cells("Current_Stock").Value = ds.Tables(0).Rows(i)("Current_Stock").ToString()
                ''New Column Added 
                DGVStockTransfer.Rows(introw).Cells("Quantity").Value = ds.Tables(0).Rows(i)("qty").ToString
                'DGVMRSItem.Rows.Add()
                
                'If Me.DGVMRSItem.Rows.Count > 0 Then DGVMRSItem.Rows.Add()
                ' If Me.DGVMRSItem.Rows.Count > 0 Then DGVMRSItem.Rows.Add(1)
                'Next
            Next
            DGVStockTransfer.Rows(introw).Cells("Item_CODE").Selected = False
            DGVStockTransfer.Rows(introw).Cells("Item_Name").Selected = False
            DGVStockTransfer.Rows(introw).Cells("item_cat_name").Selected = False
            DGVStockTransfer.Rows(introw).Cells("UM_Name").Selected = False
            DGVStockTransfer.Rows(introw).Cells("Avg_rate").Selected = False
            DGVStockTransfer.Rows(introw).Cells("Current_Stock").Selected = False
            DGVStockTransfer.Rows(introw).Cells("Quantity").Selected = True

            DGVStockTransfer.Rows(introw + 1).Cells("Item_CODE").Selected = False
            DGVStockTransfer.Rows(introw + 1).Cells("Item_Name").Selected = False
            DGVStockTransfer.Rows(introw + 1).Cells("item_cat_name").Selected = False
            DGVStockTransfer.Rows(introw + 1).Cells("UM_Name").Selected = False
            DGVStockTransfer.Rows(introw + 1).Cells("Avg_rate").Selected = False
            DGVStockTransfer.Rows(introw + 1).Cells("Current_Stock").Selected = False
            DGVStockTransfer.Rows(introw + 1).Cells("Quantity").Selected = False
        End If
        'ds = obj.
    End Sub

    Private Sub rdbrecipe_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbrecipe.CheckedChanged
        Try
            obj.ComboBind(cmbrecipe, "SELECT MIR_ID,MIR_ITEM_NAME FROM MENU_ITEM_RECIPE", "MIR_ITEM_NAME", "MIR_ID", True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbsemirecipe_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbsemirecipe.CheckedChanged
        Try
            obj.ComboBind(cmbrecipe, "select item_id as MIR_ID, Item_name as MIR_ITEM_NAME from Semifinished_Item_Master", "MIR_ITEM_NAME", "MIR_ID", True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btntransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntransfer.Click
        get_recipe_items(cmbrecipe.SelectedValue, txtqty.Text)
    End Sub


End Class
