Imports MMSPlus.Adjustment_master
Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid

Public Class frm_StockAdjustment
    Implements IForm
    Dim clsObj As New cls_adjustment_master
    Dim prpty As New cls_adjustment_master_prop
    Dim dtWastageItem As New DataTable
    Dim flag As String
    Dim rights As Form_Rights
    Dim cmd As New SqlCommand
    Dim con As New SqlConnection
    Dim Trans As SqlTransaction
    Dim iAdjustmentId As Int32
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
        Adjustment_Qty = 10
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
        'If Validation() = False Then
        '    Exit Sub
        'End If
        grdAdjustmentItem.FinishEditing()
        Try
            If _rights.allow_trans = "N" Then
                RightsMsg()
                Exit Sub
            End If
            Dim msg As String
            iAdjustmentId = Convert.ToInt32(clsObj.getMaxValue("Adjustment_ID", "ADJUSTMENT_MASTER"))
            prpty = New cls_adjustment_master_prop
            prpty.adjustment_ID = iAdjustmentId
            prpty.adjustment_Code = clsObj.getPrefixCode("ADJUSTMENT_PREFIX", "DIVISION_SETTINGS")
            prpty.adjustment_No = iAdjustmentId
            prpty.adjustment_Date = Now
            prpty.adjustment_Remarks = txtAdjustmentRemarks.Text()
            prpty.Created_BY = v_the_current_logged_in_user_name
            prpty.Creation_Date = now
            prpty.Modified_BY = ""
            prpty.Modified_Date = NULL_DATE
            prpty.Division_ID = v_the_current_division_id
            prpty.adjustmentItem = grdAdjustmentItem.DataSource
            If flag = "save" Then
                msg = clsObj.Insert_adjustment_MasterTran(prpty)
                'MsgBox(msg, MsgBoxStyle.Information, gblMessageHeading)
                If MsgBox(msg & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    objComm.RptShow(enmReportName.RptStock_Adjustment, "DataGridView1", CStr(iAdjustmentId), CStr(enmDataType.D_int))
                End If
                FillGridWastageMaster()
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
                objComm.RptShow(enmReportName.RptStock_Adjustment, "adjustment_id", CStr(DataGridView1("adjustment_id", DataGridView1.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                'Else
                '    If flag <> "save" Then
                '        '    obj.RptShow(enmReportName.RptOpenPurchaseOrderPrint, "PO_ID", CStr(_po_id), CStr(enmDataType.D_int))
                '        objComm.RptShow(enmReportName.RptWastagePrint, "adjustment_id", CStr(DataGridView1("adjustment_id", DataGridView1.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))

                '    End If
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
        txtAdjustmentRemarks.Focus()
        For iRow = 1 To grdAdjustmentItem.Rows.Count - 2
            If Convert.ToString(grdAdjustmentItem.Item(iRow, enmgrdWastageItem.Adjustment_Qty)) > "0.0" Then
                blnRecExist = True
                Exit For
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
    Public Function GetAdjustmentCode() As String

        Dim Pre As String
        Dim WID As String
        Dim WastageCode As String
        Pre = clsObj.getPrefixCode("ADJUSTMENT_PREFIX", "DIVISION_SETTINGS")
        WID = clsObj.getMaxValue("Adjustment_ID", "ADJUSTMENT_MASTER")
        WastageCode = Pre & "" & WID
        Return WastageCode

    End Function
    Private Sub frm_Wastage_Master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            rights = clsObj.Get_Form_Rights(Me.Name)
            flag = "save"
            new_initilization()
            FillGridWastageMaster()
            table_style()
            '    clsObj.FormatGrid(grdAdjustmentItem)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub new_initilization()
        lbl_AdjustmentCode.Text = GetAdjustmentCode()
        lbladjustmentdate.Text = Now.ToString("dd-MMM-yyy")
        txtAdjustmentRemarks.Text = ""
        dtWastageItem = grdAdjustmentItem.DataSource
        If Not dtWastageItem Is Nothing Then dtWastageItem.Rows.Clear()
        TBCWastageMaster.SelectTab(1)
        flag = "save"
    End Sub
    Private Sub FillGridWastageMaster()
        Try
            clsObj.Grid_Bind(DataGridView1, "GET_ADJUSTMENT_MASTER")
            DataGridView1.Columns(0).Visible = False                 'Wastage_ID
            DataGridView1.Columns(1).HeaderText = "Adjustment No"       'Wastage_No
            DataGridView1.Columns(1).Width = 185
            DataGridView1.Columns(2).HeaderText = "Adjustment Code"     'Wastage_Code
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).HeaderText = "Adjustment Date"     'Wastage_Date
            DataGridView1.Columns(3).Width = 250
            DataGridView1.Columns(4).HeaderText = "Adjustment Remarks"  'Wastage_Remarks
            DataGridView1.Columns(4).Width = 450
            DataGridView1.Columns(5).Visible = False
            '  clsObj.FormatGrid(DataGridView1)
            lblErrorMsg.Text = ""
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub
    Private Sub DGVWastageMaster_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVWastageMaster.DoubleClick
        Try
            Dim ds As DataSet
            Dim dtAdjustment As New DataTable
            'Dim dtWastageDetail As New DataTable
            Dim iAdjustmentId As Int32
            iAdjustmentId = Convert.ToInt32(DGVWastageMaster.SelectedRows.Item(0).Cells(0).Value)
            flag = "view"
            'grdWastageItem.Rows.RemoveRange(1, grdWastageItem.Rows.Count - 1)
            ds = clsObj.fill_Data_set("GET_Adjustment_MASTERANDAdjustment_DETAIL", "@v_Adjustment_ID", iAdjustmentId)
            Dim dt As DataTable
            If ds.Tables.Count > 0 Then
                'Bind the Wastage Information
                dtAdjustment = ds.Tables(0)
                iAdjustmentId = Convert.ToString(dtAdjustment.Rows(0)("Wastage_ID"))
                lbl_AdjustmentCode.Text = Convert.ToString(dtAdjustment.Rows(0)("Wastage_No"))
                lbladjustmentdate.Text = Convert.ToString(dtAdjustment.Rows(0)("Wastage_Date"))
                txtAdjustmentRemarks.Text = Convert.ToString(dtAdjustment.Rows(0)("Wastage_Remarks"))
                'Bind the Wastage Item Information
                'dt = grdWastageItem.DataSource
                'dt.Rows.Clear()
                dt = ds.Tables(1)
                If dt.Rows.Count > 0 Then
                    grdAdjustmentItem.DataSource = dt
                End If
            End If
            TBCWastageMaster.SelectTab(1)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub DGVWastageItem_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)

    End Sub
    Private Sub DGVWastageItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdAdjustmentItem.KeyDown
        Try
            Dim iRowindex As Int32
            If flag = "save" Then
                If e.KeyCode = Keys.Space Then
                    iRowindex = grdAdjustmentItem.Row
                    AddItem()
                End If

            End If
            If e.KeyCode = Keys.Delete Then

                Dim result As Integer
                Dim item_code As String
                result = MsgBox("Do you want to remove """ & grdAdjustmentItem.Rows(grdAdjustmentItem.CursorCell.r1).Item(3) & """ from the list?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
                item_code = grdAdjustmentItem.Rows(grdAdjustmentItem.CursorCell.r1).Item("item_code")
                If result = MsgBoxResult.Yes Then
restart:
                    Dim dt As DataTable
                    dt = TryCast(grdAdjustmentItem.DataSource, DataTable)
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
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AddItem()
        frm_Show_search.qry = " SELECT " &
                                               " ITEM_MASTER.ITEM_ID,   " &
                                               " ITEM_MASTER.ITEM_CODE, " &
                                               " ITEM_MASTER.ITEM_NAME, " &
                                               " ITEM_MASTER.ITEM_DESC, " &
                                               " UNIT_MASTER.UM_Name,   " &
                                               " ITEM_CATEGORY.ITEM_CAT_NAME, " &
                                               " ITEM_MASTER.IS_STOCKABLE, Barcode_vch " &
                                       " FROM " &
                                               " ITEM_MASTER " &
                                               " INNER JOIN UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID " &
                                               " INNER JOIN ITEM_CATEGORY ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID " &
                                                "INNER JOIN ITEM_DETAIL ON ITEM_MASTER.ITEM_ID = ITEM_DETAIL.ITEM_ID "

        frm_Show_search.txtSearch.Text = ""
        frm_Show_search.column_name = "(Item_Name + isnull(Barcode_vch,''))"
        frm_Show_search.extra_condition = ""
        frm_Show_search.ret_column = "Item_ID"
        frm_Show_search.ShowDialog()
        If Not check_item_exist(frm_Show_search.search_result) Then
            get_row(frm_Show_search.search_result, 0)
        End If
    End Sub

    Function check_item_exist(ByVal item_id As Integer) As Boolean
        Dim iRow As Int32
        check_item_exist = False
        For iRow = 1 To grdAdjustmentItem.Rows.Count - 1
            If grdAdjustmentItem.Item(iRow, "item_id").ToString() <> "" Then
                If Convert.ToInt32(grdAdjustmentItem.Item(iRow, "item_id")) = item_id Then
                    MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
                    check_item_exist = True
                    Exit For
                End If
            Else
                check_item_exist = False

            End If
        Next iRow
    End Function

    Public Sub get_row(ByVal item_id As Integer, ByVal Wastage_id As Integer)
        Try
            Dim ds As DataSet
            Dim sqlqry As String
            sqlqry = "SELECT  " & _
                                        " IM.ITEM_ID , " & _
                                        " IM.ITEM_CODE , " & _
                                        " IM.ITEM_NAME , " & _
                                        " UM.UM_Name , " & _
                                        " SD.Batch_no , " & _
                                        " dbo.fn_Format(SD.Expiry_date) AS Expiry_Date, " & _
                                        " dbo.Get_Average_Rate_as_on_date(IM.ITEM_ID,'" & Now.ToString("dd-MMM-yyyy") & "'," & v_the_current_division_id & ",0) as Item_Rate," & _
                                        " SD.Balance_Qty, " & _
                                        " 0.00  as adjustment_qty, " & _
                                        " SD.STOCK_DETAIL_ID  " & _
                                " FROM " & _
                                        " ITEM_MASTER  IM " & _
                                        " INNER JOIN ITEM_DETAIL ID ON IM.ITEM_ID = ID.ITEM_ID " & _
                                        " INNER JOIN STOCK_DETAIL SD ON ID.ITEM_ID = SD.Item_id " & _
                                        " INNER JOIN UNIT_MASTER UM ON IM.UM_ID = UM.UM_ID" & _
                                " where " & _
            " IM.ITEM_ID = " & item_id & ""
            '" IM.ITEM_ID = " & item_id & " and SD.Balance_Qty > 0"
            ds = clsObj.Fill_DataSet(sqlqry)
            Dim dt As DataTable
            Dim i As Integer
            Dim dr As DataRow
            dt = grdAdjustmentItem.DataSource
            objComm.RemoveBlankRow(dt, "item_id")
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow
                dr("Item_Id") = ds.Tables(0).Rows(i)("ITEM_ID")
                dr("Item_Code") = ds.Tables(0).Rows(i)("ITEM_CODE")
                dr("Item_Name") = ds.Tables(0).Rows(i)("ITEM_NAME")
                dr("UM_Name") = ds.Tables(0).Rows(i)("UM_Name")
                dr("Batch_No") = ds.Tables(0).Rows(i)("Batch_no")
                dr("Expiry_Date") = ds.Tables(0).Rows(i)("Expiry_Date")
                dr("Item_Rate") = ds.Tables(0).Rows(i)("Item_Rate")
                dr("Batch_Qty") = ds.Tables(0).Rows(i)("Balance_Qty")
                dr("Stock_Detail_Id") = ds.Tables(0).Rows(i)("STOCK_DETAIL_ID")
                dr("adjustment_Qty") = ds.Tables(0).Rows(i)("adjustment_Qty")
                dt.Rows.Add(dr)
            Next
            Dim strSort As String = ""
            'grdAdjustmentItem.Cols(1).Name(+", " + grdAdjustmentItem.Cols(2).Name + ", " + grdAdjustmentItem.Cols(3).Name)
            dt = grdAdjustmentItem.DataSource
            If Not dt Is Nothing Then
                dt.DefaultView.Sort = strSort
                dt = dt.DefaultView.ToTable
                dt.DefaultView.Sort = ""
            End If
            dt.Rows.Add(dt.NewRow)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub




    Private Sub grdWastageItem_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdAdjustmentItem.AfterEdit
        If grdAdjustmentItem.Rows(e.Row).IsNode Then Exit Sub
        If Convert.ToDecimal(grdAdjustmentItem.Rows(e.Row)("adjustment_Qty")) + Convert.ToDecimal(grdAdjustmentItem.Rows(e.Row)("Batch_Qty")) < 0 Then
            grdAdjustmentItem.Rows(e.Row)("adjustment_Qty") = 0.0
        End If

    End Sub
    Private Sub table_style()

        If Not dtWastageItem Is Nothing Then dtWastageItem.Dispose()
        dtWastageItem = New DataTable()
        dtWastageItem.Columns.Add("Item_Id", GetType(System.Double))
        dtWastageItem.Columns.Add("Item_Code", GetType(System.String))
        dtWastageItem.Columns.Add("Item_Name", GetType(System.String))
        dtWastageItem.Columns.Add("UM_Name", GetType(System.String))
        dtWastageItem.Columns.Add("Batch_no", GetType(System.String))
        dtWastageItem.Columns.Add("Expiry_date", GetType(System.String))
        dtWastageItem.Columns.Add("Batch_Qty", GetType(System.Double))
        dtWastageItem.Columns.Add("Stock_Detail_Id", GetType(System.Int32))
        dtWastageItem.Columns.Add("Item_Rate", GetType(System.Double))
        dtWastageItem.Columns.Add("adjustment_Qty", GetType(System.Double))
        dtWastageItem.Rows.Add()

        grdAdjustmentItem.DataSource = dtWastageItem
        format_grid()


    End Sub

    Private Sub format_grid()

        grdAdjustmentItem.Cols(0).Width = 10
        grdAdjustmentItem.Cols("Item_Id").Visible = True
        grdAdjustmentItem.Cols("Stock_Detail_Id").Visible = True
        grdAdjustmentItem.Cols("Item_Code").Caption = "Item Code"
        grdAdjustmentItem.Cols("Item_Name").Caption = "Item Name"
        grdAdjustmentItem.Cols("UM_Name").Caption = "UOM"
        grdAdjustmentItem.Cols("Batch_no").Caption = "Batch No"
        grdAdjustmentItem.Cols("Expiry_date").Caption = "Expiry Date"
        grdAdjustmentItem.Cols("batch_qty").Caption = "Batch Qty"
        grdAdjustmentItem.Cols("adjustment_Qty").Caption = "Adjusted Qty"
        grdAdjustmentItem.Cols("Item_Rate").Caption = "Item Rate"

        grdAdjustmentItem.Cols("Item_Code").AllowEditing = False
        grdAdjustmentItem.Cols("Item_Name").AllowEditing = False
        grdAdjustmentItem.Cols("UM_Name").AllowEditing = False
        grdAdjustmentItem.Cols("Batch_no").AllowEditing = False
        grdAdjustmentItem.Cols("Expiry_date").AllowEditing = False
        grdAdjustmentItem.Cols("batch_qty").AllowEditing = False
        grdAdjustmentItem.Cols("Stock_Detail_Id").AllowEditing = False
        grdAdjustmentItem.Cols("adjustment_Qty").AllowEditing = True
        grdAdjustmentItem.Cols("Item_Rate").AllowEditing = False

        grdAdjustmentItem.Cols("Item_Id").Width = 80
        grdAdjustmentItem.Cols("Item_Code").Width = 70
        grdAdjustmentItem.Cols("Item_Name").Width = 250
        grdAdjustmentItem.Cols("UM_Name").Width = 40
        grdAdjustmentItem.Cols("Batch_No").Width = 80
        grdAdjustmentItem.Cols("Expiry_date").Width = 95
        grdAdjustmentItem.Cols("Batch_Qty").Width = 70
        grdAdjustmentItem.Cols("Stock_Detail_Id").Width = 70
        grdAdjustmentItem.Cols("adjustment_Qty").Width = 100
        grdAdjustmentItem.Cols("Item_Rate").Width = 80

        grdAdjustmentItem.Cols("Stock_Detail_Id").Visible = False
    End Sub
    Private Sub grdWastageItem_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles grdAdjustmentItem.AfterDataRefresh
        generate_tree()
    End Sub
    Private Sub generate_tree()
        If grdAdjustmentItem.Rows.Count > 1 Then
            grdAdjustmentItem.Tree.Style = TreeStyleFlags.CompleteLeaf
            grdAdjustmentItem.Tree.Column = 1
            grdAdjustmentItem.AllowMerging = AllowMergingEnum.None
            Dim totalOn As Integer = grdAdjustmentItem.Cols("Batch_Qty").SafeIndex
            grdAdjustmentItem.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)
            'totalOn = grdAdjustmentItem.Cols("adjustment_Qty").SafeIndex
            'grdAdjustmentItem.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)

            Dim cs As C1.Win.C1FlexGrid.CellStyle
            cs = Me.grdAdjustmentItem.Styles.Add("adjustment_Qty")
            cs.ForeColor = Color.White
            cs.BackColor = Color.OrangeRed
            cs.Border.Style = BorderStyleEnum.Raised

            Dim i As Integer

            For i = 1 To grdAdjustmentItem.Rows.Count - 1
                If Not grdAdjustmentItem.Rows(i).IsNode Then grdAdjustmentItem.SetCellStyle(i, enmgrdWastageItem.Adjustment_Qty, cs)
            Next
        End If
    End Sub

    Private Sub grdWastageItem_ChangeEdit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAdjustmentItem.ChangeEdit
    End Sub

    Private Sub grdWastageItem_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles grdAdjustmentItem.KeyPressEdit
        e.Handled = grdAdjustmentItem.Rows(grdAdjustmentItem.CursorCell.r1).IsNode
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim qry As String = ""
        qry = "SELECT  Adjustment_ID," & _
                " Adjustment_No," & _
                " Adjustment_Code + CAST(Adjustment_No AS VARCHAR) AS Adjustment_Code , " & _
                " dbo.fn_Format(Adjustment_Date) AS Adjustment_Date," & _
                " Adjustment_Remarks," & _
                " Division_ID" & _
        " FROM Adjustment_master" & _
        " where (cast(adjustment_No as varchar) + (Adjustment_Code + CAST(Adjustment_No AS VARCHAR)) + Cast(convert(varchar,Adjustment_Date,101) as varchar) + Adjustment_Remarks)" & _
        " like '%" & txtSearch.Text & "%'"

        objComm.GridBind(DataGridView1, qry)
    End Sub

    Private Sub txtBarcodeSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBarcodeSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrEmpty(txtBarcodeSearch.Text) Then

                Dim qry As String = "SELECT  item_id FROM    ITEM_MASTER WHERE   Barcode_vch = '" + txtBarcodeSearch.Text + "'"
                Dim id As Int32 = objComm.ExecuteScalar(qry)
                If id > 0 Then
                    If Not check_item_exist(id) Then
                        get_row(id, 0)
                    End If
                End If
                txtBarcodeSearch.Text = ""
                txtBarcodeSearch.Focus()
            End If
        End If
    End Sub
End Class
