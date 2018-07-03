Imports MMSPlus.wastage_master
Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid

Public Class frm_Wastage_Master_cc
    Implements IForm
    Dim clsObj As New wastage_master_cc.cls_wastage_master_cc
    Dim prpty As New wastage_master_cc.cls_wastage_master_prop_cc
    Dim dtWastageItem As New DataTable
    Dim flag As String
    Dim rights As Form_Rights
    Dim cmd As New SqlCommand
    Dim con As New SqlConnection
    Dim Trans As SqlTransaction
    Dim iWastageId As Int32
    Dim objComm As New CommonClass


    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Enum enmgrdWastageItem
        rowid = 0
        ItemId = 1
        ItemCode = 2
        ItemName = 3
        ItemUOM = 4
        Batch_N0 = 5
        Expiry_Date = 6
        Batch_Qty = 7
        Stock_Detail_Id = 8
        Wastage_Qty = 10
    End Enum
    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub
    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            flag = "save"
            new_initilization()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_Wastwge_Master")
        End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        FillGridWastageMaster()
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        If txtWatageReamrks.Text = "" Then
            MsgBox("Remarks not entered", MsgBoxStyle.OkOnly)
            Exit Sub
        End If
        If Validation() = False Then
            Exit Sub
        End If

        Dim irowcount As Integer
        Dim irow As Integer

        irowcount = grdwastageItem.RowCount

        For i As Integer = 0 To grdwastageItem.Rows.Count - 1
            If grdwastageItem.Item("Quantity", irow).Value <= 0 Then
                MsgBox("Wastage quantity cannot be zero or negative.")
                Exit Sub
            End If
        Next

        Dim cmd As SqlCommand
        cmd = objComm.MyCon_BeginTransaction

        Try
            If flag = "save" Then
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If

                iWastageId = Convert.ToInt32(clsObj.getMaxValue("Wastage_ID", "WASTAGE_MASTER_CC"))
                'prpty = New cls_wastage_master_prop_cc
                prpty.Wastage_ID = iWastageId
                prpty.Wastage_Code = clsObj.getPrefixCode("WASTAGE_PREFIX_CC", "DIVISION_SETTINGS")
                prpty.Wastage_No = iWastageId
                prpty.Wastage_Date = Now
                prpty.Wastage_Remarks = txtWatageReamrks.Text()
                prpty.Created_BY = v_the_current_logged_in_user_name
                prpty.Creation_Date = Now
                prpty.Modified_BY = ""
                prpty.Modified_Date = NULL_DATE
                prpty.CostCenter_ID = v_the_current_Selected_CostCenter_id
                prpty.Division_Id = v_the_current_division_id
                'prpty.WastageItem = grdwastageItem.DataSource
                clsObj.Insert_Wastage_Master(prpty, cmd)

                

                For irow = 0 To irowcount - 1
                    If grdwastageItem.Item("Quantity", irow).Value() IsNot Nothing And Convert.ToString(grdwastageItem.Item("Quantity", irow).Value) <> "" Then
                        If grdwastageItem.Item("Quantity", irow).Value() > 0 Then
                            prpty.Wastage_ID = iWastageId
                            prpty.Item_ID = Convert.ToDouble(grdwastageItem.Item("item_id", irow).Value)
                            prpty.Item_Qty = Convert.ToDouble(grdwastageItem.Item("Quantity", irow).Value)
                            prpty.Item_Rate = Convert.ToDouble(grdwastageItem.Item("Rate", irow).Value)
                            prpty.Balance_Qty = 0.0
                            prpty.CostCenter_ID = v_the_current_Selected_CostCenter_id
                            prpty.Created_BY = v_the_current_logged_in_user_name
                            prpty.Creation_Date = Now
                            prpty.Modified_BY = ""
                            prpty.Modified_Date = NULL_DATE
                            clsObj.Insert_Wastage_Detail(prpty, cmd)
                        End If
                    End If
                Next irow
                objComm.MyCon_CommitTransaction(cmd)
                'MsgBox(msg, MsgBoxStyle.Information, gblMessageHeading)
                If MsgBox("Record saved successfully with code ( " + Convert.ToString(prpty.Wastage_Code) + Convert.ToString(prpty.Wastage_No) + "  )" & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    objComm.RptShow(enmReportName.RptWastagePrint_cc, "wastage_id", CStr(iWastageId), CStr(enmDataType.D_int))
                End If
            Else
                MsgBox("You can't Modify it.", MsgBoxStyle.Information, gblMessageHeading)
            End If
            new_initilization()
            TBCWastageMaster.SelectTab(0)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error saveClick --> frm_Wastage_Master")
        End Try

    End Sub



    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TBCWastageMaster.SelectedIndex = 0 Then
                objComm.RptShow(enmReportName.RptWastagePrint_cc, "wastage_id", CStr(DGVWastageMaster("wastage_id", DGVWastageMaster.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                If flag <> "save" Then
                    '    obj.RptShow(enmReportName.RptOpenPurchaseOrderPrint, "PO_ID", CStr(_po_id), CStr(enmDataType.D_int))
                    objComm.RptShow(enmReportName.RptWastagePrint_cc, "wastage_id", CStr(DGVWastageMaster("wastage_id", DGVWastageMaster.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function Validation() As Boolean
        Dim iRow As Int32
        Validation = True
        Dim blnRecExist As Boolean
        blnRecExist = False
        txtWatageReamrks.Focus()

        For iRow = 0 To grdwastageItem.Rows.Count - 1
            If Val(grdwastageItem.Item("Quantity", iRow).Value) > 0 And blnRecExist = False Then
                blnRecExist = True
                'Exit For
            End If
        Next iRow
        If blnRecExist = True Then
            Validation = True
            Exit Function
        Else
            Validation = False
            MsgBox("Select aleast one valid item in Wastage to save Wastage information", vbExclamation, gblMessageHeading)
            Exit Function
        End If
    End Function
    Public Function GetWastageCode() As String

        Dim Pre As String
        Dim WID As String
        Dim WastageCode As String
        Pre = clsObj.getPrefixCode("WASTAGE_PREFIX_CC", "DIVISION_SETTINGS")
        WID = clsObj.getMaxValue("Wastage_ID", "WASTAGE_MASTER_CC")
        WastageCode = Pre & "" & WID
        Return WastageCode

    End Function
    Private Sub frm_Wastage_Master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            rights = clsObj.Get_Form_Rights(Me.Name)
            flag = "save"
            new_initilization()
            FillGridWastageMaster()
            TBCWastageMaster.SelectTab(0)
            'table_style()
            format_grid()
            ' clsObj.FormatGrid(grdWastageItem)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub new_initilization()
        lbl_WastageCode.Text = GetWastageCode()
        lbl_WastageDate.Text = now.ToString("dd-MMM-yyy")
        txtWatageReamrks.Text = ""
        If Not grdwastageItem Is Nothing Then grdwastageItem.Rows.Clear()
        FillGridWastageMaster()
        TBCWastageMaster.SelectTab(1)
        flag = "save"
    End Sub
    Private Sub FillGridWastageMaster()
        Try
            clsObj.Bind_GridBind_Val(DGVWastageMaster, "GET_WASTAGE_MASTER_CC", "@CC_ID", "", v_the_current_Selected_CostCenter_id, "")
            DGVWastageMaster.Columns(0).Visible = False                 'Wastage_ID
            DGVWastageMaster.Columns(1).HeaderText = "Wastage No"       'Wastage_No
            DGVWastageMaster.Columns(1).Width = 150
            DGVWastageMaster.Columns(2).HeaderText = "Wastage Code"     'Wastage_Code
            DGVWastageMaster.Columns(2).Visible = False
            DGVWastageMaster.Columns(3).HeaderText = "Wastage Date"     'Wastage_Date
            DGVWastageMaster.Columns(3).Width = 200
            DGVWastageMaster.Columns(4).HeaderText = "Wastage Remarks"  'Wastage_Remarks
            DGVWastageMaster.Columns(4).Width = 500
            DGVWastageMaster.Columns(5).Visible = False
            'clsObj.FormatGrid(DGVWastageMaster)
            lblErrorMsg.Text = ""
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub
    Private Sub DGVWastageMaster_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim ds As DataSet
            Dim dtWastage As New DataTable
            'Dim dtWastageDetail As New DataTable
            Dim iWastageId As Int32
            iWastageId = Convert.ToInt32(DGVWastageMaster.SelectedRows.Item(0).Cells(0).Value)
            flag = "view"
            'grdWastageItem.Rows.RemoveRange(1, grdWastageItem.Rows.Count - 1)
            ds = clsObj.fill_Data_set("GET_WASTAGE_MASTERANDWASTAGE_DETAIL_CC", "@v_Wastage_ID", iWastageId)
            Dim dt As DataTable
            If ds.Tables.Count > 0 Then
                'Bind the Wastage Information
                dtWastage = ds.Tables(0)
                iWastageId = Convert.ToString(dtWastage.Rows(0)("Wastage_ID"))
                lbl_WastageCode.Text = Convert.ToString(dtWastage.Rows(0)("Wastage_No"))
                lbl_WastageDate.Text = Convert.ToString(dtWastage.Rows(0)("Wastage_Date"))
                txtWatageReamrks.Text = Convert.ToString(dtWastage.Rows(0)("Wastage_Remarks"))
                'Bind the Wastage Item Information
                'dt = grdWastageItem.DataSource
                'dt.Rows.Clear()
                dt = ds.Tables(1)
                If dt.Rows.Count > 0 Then
                    grdWastageItem.DataSource = dt
                End If
            End If
            TBCWastageMaster.SelectTab(1)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub DGVWastageItem_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)

    End Sub
    Private Sub grdWastageItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdwastageItem.KeyDown
        Try
            Dim iRowindex As Int32
            iRowindex = grdwastageItem.CurrentRow.Index
            If e.KeyCode = Keys.Delete Then
                Dim result As Integer
                Dim item_code As String
                result = MsgBox("Do you want to remove """ & grdwastageItem.Item("Item_name", iRowindex).Value & """ from the list?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
                item_code = grdwastageItem.Item("item_code", iRowindex).Value
                If result = MsgBoxResult.Yes Then
restart:
                    Dim dt As DataTable
                    dt = TryCast(grdwastageItem.DataSource, DataTable)
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

            ElseIf flag = "save" Then
                If e.KeyCode = Keys.Space Then
                    iRowindex = grdwastageItem.CurrentRow.Index

                    'frm_Show_search.qry = "Select Item_master.ITEM_ID,Item_master.ITEM_CODE as [Item Code],Item_master.ITEM_NAME  as [Item Name] from Item_master inner join item_detail on item_master.item_id = item_detail.item_id " 'where item_detail.div_id = '" + Convert.ToString(v_the_curre) + "'"
                    'frm_Show_search.extra_condition = ""
                    'frm_Show_search.ret_column = "Item_ID"
                    'frm_Show_search.item_rate_column = ""
                    'frm_Show_search.column_name = "ITEM_NAME"
                    'frm_Show_search.cols_width = "80,300"
                    'frm_Show_search.cols_no_for_width = "1,2"
                    'frm_Show_search.ShowDialog()

                    frm_Show_search.qry = " SELECT top 100 im.ITEM_ID ,
		                                ISNULL(im.BarCode_vch, '') AS BARCODE,
                                        im.ITEM_NAME AS [ITEM NAME],
                                        im.MRP_Num AS MRP,
                                        cast(im.sale_rate AS numeric(18,2)) AS RATE,
                                        litems.LabelItemName_vch AS BRAND,
                                        ic.ITEM_CAT_NAME AS CATEGORY
                                        FROM    Item_master im
                                        INNER JOIN item_detail id ON im.item_id = id.item_id
                                        INNER JOIN dbo.ITEM_CATEGORY ic ON im.ITEM_CATEGORY_ID = ic.ITEM_CAT_ID
                                        LEFT JOIN dbo.LabelItem_Mapping lim ON lim.Fk_ItemId_Num = im.ITEM_ID
                                        inner JOIN dbo.Label_Items litems ON lim.Fk_LabelDetailId = litems.Pk_LabelDetailId_Num
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

                    If frm_Show_search.search_result <> -1 Then
                        get_row(frm_Show_search.search_result, 0)
                    End If

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Function check_item_exist(ByVal item_id As Integer) As Boolean
    '    Dim iRow As Int32
    '    check_item_exist = False
    '    For iRow = 1 To grdWastageItem.Rows.Count - 1
    '        If grdWastageItem.Item(iRow, "item_id").ToString() <> "" Then
    '            If Convert.ToInt32(grdWastageItem.Item(iRow, "item_id")) = item_id Then
    '                MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
    '                check_item_exist = True
    '                Exit For
    '            End If
    '        Else
    '            check_item_exist = False

    '        End If
    '    Next iRow
    'End Function

    Public Sub get_row(ByVal item_id As Integer, ByVal Wastage_id As Integer)
        Try
            Dim IsInsert As Boolean
            Dim ds As DataSet
            'ds = obj.fill_Data_set("GET_ITEM_BY_ID_CC", "@V_ITEM_ID", item_id)
            ds = objComm.fill_Data_set("GET_ITEM_STOCK_CC", "@V_ITEM_ID,@V_Div_id,@V_Todate", item_id & "," & v_the_current_Selected_CostCenter_id & "," & Now.ToString("dd-MMM-yyyy"))
            Dim iRowCount As Int32
            Dim iRow As Int32
            iRowCount = grdwastageItem.RowCount
            IsInsert = True
            'DGVMRSItem_style()
            For iRow = 0 To iRowCount - 2
                If grdwastageItem.Item(0, iRow).Value = Convert.ToInt32(ds.Tables(0).Rows(0)(0)) Then
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
                introw = grdwastageItem.Rows.Count - 1
                grdwastageItem.Rows.Insert(introw)
                grdwastageItem.Rows(introw).Cells("Item_ID").Value = ds.Tables(0).Rows(0)(0)
                grdwastageItem.Rows(introw).Cells("Item_CODE").Value = ds.Tables(0).Rows(0)("item_Code").ToString()
                grdwastageItem.Rows(introw).Cells("Item_Name").Value = ds.Tables(0).Rows(0)("item_Name").ToString()
                'grdwastageItem.Rows(introw).Cells("item_cat_name").Value = ds.Tables(0).Rows(0)("item_cat_name").ToString()
                grdwastageItem.Rows(introw).Cells("UM_Name").Value = ds.Tables(0).Rows(0)("UM_NAME").ToString()
                ''New Column Added 
                grdwastageItem.Rows(introw).Cells("Current_Stock").Value = ds.Tables(0).Rows(0)("Current_Stock").ToString()
                grdwastageItem.Rows(introw).Cells("Rate").Value = ds.Tables(0).Rows(0)("Avg_Rate").ToString()
                ''New Column Added 
                grdwastageItem.Rows(introw).Cells("Quantity").Value = "0.00"
                'DGVMRSItem.Rows.Add()
                grdwastageItem.Rows(introw).Cells("Item_CODE").Selected = False
                grdwastageItem.Rows(introw).Cells("Item_Name").Selected = False
                'grdwastageItem.Rows(introw).Cells("item_cat_name").Selected = False
                grdwastageItem.Rows(introw).Cells("UM_Name").Selected = False
                grdwastageItem.Rows(introw).Cells("Current_Stock").Selected = False
                grdwastageItem.Rows(introw).Cells("Quantity").Selected = True

                grdwastageItem.Rows(introw + 1).Cells("Item_CODE").Selected = False
                grdwastageItem.Rows(introw + 1).Cells("Item_Name").Selected = False
                'grdwastageItem.Rows(introw + 1).Cells("item_cat_name").Selected = False
                grdwastageItem.Rows(introw + 1).Cells("UM_Name").Selected = False
                grdwastageItem.Rows(introw + 1).Cells("Current_Stock").Selected = False
                grdwastageItem.Rows(introw + 1).Cells("Rate").Selected = False
                grdwastageItem.Rows(introw + 1).Cells("Quantity").Selected = False
                'If Me.DGVMRSItem.Rows.Count > 0 Then DGVMRSItem.Rows.Add()
                ' If Me.DGVMRSItem.Rows.Count > 0 Then DGVMRSItem.Rows.Add(1)
                'Next
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub grdWastageItem_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs)
        'If grdWastageItem.Rows(e.Row).IsNode Then Exit Sub
        'If Convert.ToDecimal(grdWastageItem.Rows(e.Row)("wastage_qty")) > Convert.ToDecimal(grdWastageItem.Rows(e.Row)("Batch_Qty")) Then
        '    grdWastageItem.Rows(e.Row)("wastage_qty") = 0.0
        'End If

    End Sub
    'Private Sub table_style()

    '    If Not dtWastageItem Is Nothing Then dtWastageItem.Dispose()
    '    dtWastageItem = New DataTable()
    '    dtWastageItem.Columns.Add("Item_Id", GetType(System.Double))
    '    dtWastageItem.Columns.Add("Item_Code", GetType(System.String))
    '    dtWastageItem.Columns.Add("Item_Name", GetType(System.String))
    '    dtWastageItem.Columns.Add("UM_Name", GetType(System.String))
    '    dtWastageItem.Columns.Add("Batch_no", GetType(System.String))
    '    dtWastageItem.Columns.Add("Expiry_date", GetType(System.String))
    '    dtWastageItem.Columns.Add("Batch_Qty", GetType(System.Double))
    '    dtWastageItem.Columns.Add("Stock_Detail_Id", GetType(System.Int32))
    '    dtWastageItem.Columns.Add("Item_Rate", GetType(System.Double))
    '    dtWastageItem.Columns.Add("Wastage_Qty", GetType(System.Double))
    '    dtWastageItem.Rows.Add()

    '    grdWastageItem.DataSource = dtWastageItem
    '    format_grid()


    'End Sub

    Private Sub format_grid()

        grdwastageItem.Columns.Clear()
        grdwastageItem.Rows.Clear()
        Dim txtMRSItemId = New DataGridViewTextBoxColumn
        Dim txtMRSItemCode = New DataGridViewTextBoxColumn
        Dim txtMRSItemName = New DataGridViewTextBoxColumn
        Dim txtMRSItemUom = New DataGridViewTextBoxColumn
        'Dim txtMRSItemCategory = New DataGridViewTextBoxColumn
        Dim txtMRSItemQuantity = New DataGridViewTextBoxColumn
        Dim txtCurrentStock = New DataGridViewTextBoxColumn
        Dim txtitemrate = New DataGridViewTextBoxColumn
        'Dim cmbColItemName As New DataGridViewComboBoxColumn

        With txtMRSItemId
            .HeaderText = "Item Id"
            .Name = "Item_ID"
            .ReadOnly = True
            .Visible = False
            .Width = 10
        End With
        grdwastageItem.Columns.Add(txtMRSItemId)

        With txtMRSItemCode
            .HeaderText = "Item Code"
            .Name = "Item_CODE"
            .ReadOnly = True
            .Visible = True
            .Width = 100
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
        End With
        grdwastageItem.Columns.Add(txtMRSItemCode)

        With txtMRSItemName
            .HeaderText = "Item Name"
            .Name = "Item_Name"
            .ReadOnly = True
            .Visible = True
            .Width = 280
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
        End With
        grdwastageItem.Columns.Add(txtMRSItemName)

        With txtMRSItemUom
            .HeaderText = "UOM"
            .Name = "UM_Name"
            .ReadOnly = True
            .Visible = True
            .Width = 70
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
        End With
        grdwastageItem.Columns.Add(txtMRSItemUom)

        With txtCurrentStock
            .HeaderText = "Current Stock"
            .Name = "Current_Stock"
            .ReadOnly = True
            .Visible = True
            .Width = 120
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        End With
        grdwastageItem.Columns.Add(txtCurrentStock)

        With txtitemrate
            .HeaderText = "Item Rate"
            .Name = "Rate"
            .ReadOnly = True
            .Visible = True
            .Width = 100
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        End With
        grdwastageItem.Columns.Add(txtitemrate)

        With txtMRSItemQuantity
            .HeaderText = "Wastage Quantity"
            .Name = "Quantity"
            .ReadOnly = False
            .Visible = True
            .Width = 180
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        End With
        grdwastageItem.Columns.Add(txtMRSItemQuantity)
        Dim CellLength As DataGridViewTextBoxColumn = TryCast(Me.grdwastageItem.Columns("Quantity"), DataGridViewTextBoxColumn)
        CellLength.MaxInputLength = 10
        grdwastageItem.AllowUserToResizeColumns = False
    End Sub
    Private Sub grdWastageItem_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs)
        'generate_tree()
    End Sub
    'Private Sub generate_tree()
    '    If grdWastageItem.Rows.Count > 1 Then
    '        grdWastageItem.Tree.Style = TreeStyleFlags.CompleteLeaf
    '        grdWastageItem.Tree.Column = 1
    '        grdWastageItem.AllowMerging = AllowMergingEnum.None
    '        Dim totalOn As Integer = grdWastageItem.Cols("Batch_Qty").SafeIndex
    '        grdWastageItem.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)
    '        totalOn = grdWastageItem.Cols("Wastage_Qty").SafeIndex
    '        grdWastageItem.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)

    '        Dim cs As C1.Win.C1FlexGrid.CellStyle
    '        cs = Me.grdWastageItem.Styles.Add("Wastage_Qty")
    '        cs.ForeColor = Color.White
    '        cs.BackColor = Color.Green
    '        cs.Border.Style = BorderStyleEnum.Raised

    '        Dim i As Integer
    '        For i = 1 To grdWastageItem.Rows.Count - 1
    '            If Not grdWastageItem.Rows(i).IsNode Then grdWastageItem.SetCellStyle(i, enmgrdWastageItem.Wastage_Qty, cs)
    '        Next
    '    End If
    'End Sub

    Private Sub grdWastageItem_ChangeEdit(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub grdWastageItem_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs)
        ' e.Handled = grdWastageItem.Rows(grdWastageItem.CursorCell.r1).IsNode
    End Sub



    Private Sub txtSearch_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            clsObj.GridBind(DGVWastageMaster, "SELECT  Wastage_ID," & _
                    "Wastage_Code + CAST(Wastage_No AS VARCHAR) AS Wastage_No ," & _
                    "Wastage_Code," & _
                    "dbo.fn_Format(Wastage_Date) AS Wastage_Date," & _
                    "Wastage_Remarks," & _
            "CostCenter_ID" & _
            " FROM    WASTAGE_MASTER_CC where (cast(Wastage_No as varchar) + Wastage_Code + cast(Wastage_Date as varchar) + Wastage_Remarks) like '%" & txtSearch.Text & "%'")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
        End Try
    End Sub

    Private Sub TBCWastageMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TBCWastageMaster.KeyDown
        Try
            If TBCWastageMaster.TabIndex = 1 Then
                flag = "save"
                new_initilization()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_Wastwge_Master")
        End Try
    End Sub

    Private Sub grdwastageItem_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdwastageItem.CellValueChanged
        Dim row As Integer = e.RowIndex
        If e.ColumnIndex = 6 Then
            If Convert.ToDouble(grdwastageItem.Item("Quantity", row).Value) > Convert.ToDouble(grdwastageItem.Item("Current_Stock", row).Value) Then
                MsgBox("Wastage Quantity cannot be greater than current stock")
                grdwastageItem.Item("Quantity", row).Value = 0
                Exit Sub
            End If
        End If
    End Sub
End Class
