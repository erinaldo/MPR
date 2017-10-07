Imports C1.Win.C1FlexGrid

Public Class frm_Material_Issue_To_Cost_Center_Master

    Implements IForm
    Dim rights As Form_Rights
    Dim obj As New CommonClass
    Dim clsObj As New material_issue_to_cost_center_master.cls_material_issue_to_cost_center_master
    Dim prpty As New material_issue_to_cost_center_master.cls_material_issue_to_cost_center_master_prop
    Dim Flag As String
    Dim dtable_Item_List As DataTable
    Dim TemporaryTable As DataTable
    Dim TemporaryRow As DataRow
    Dim Ds As New DataSet
    Dim MIOID As Int32
    Dim MRSID As Int32
    Dim Query As String


    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            set_new_initilize()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_MIO_master")
        End Try
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        TBCMaterialIssueToCosCenter.SelectTab(1)
    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Private Sub set_new_initilize()
        Flag = "save"
        obj.Clear_All_ComoBox(Me.GroupBox1.Controls)
        obj.Clear_All_DTPicker(Me.GroupBox1.Controls)
        obj.Clear_All_TextBox(Me.GroupBox1.Controls)
        cmbCostCenter.Focus()
        TBCMaterialIssueToCosCenter.SelectTab(1)
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

        If Validation() = False Then
            Exit Sub
        End If
        Try
            'If rights.allow_add = "N" Then
            '    RightsMsg()
            '    Exit Sub
            'End If
            Dim MsgResult As String
            Dim MIO_ID As Integer
            MIO_ID = Convert.ToInt32(obj.getMaxValue("MIO_ID", "MATERIAL_ISSUE_TO_COST_CENTER_MASTER"))
            prpty.MIO_ID = MIO_ID
            prpty.MIO_CODE = obj.getPrefixCode("MIO_PREFIX", "DIVISION_SETTINGS")
            prpty.MIO_NO = MIO_ID
            prpty.MIO_DATE = Now
            prpty.CS_ID = Convert.ToDecimal(cmbCostCenter.SelectedValue)
            prpty.MRS_ID = Convert.ToDecimal(cmbMRSDetail.SelectedValue)
            prpty.MIO_REMARKS = txtRemarks.Text
            prpty.MIO_ACCEPT_DATE = NULL_DATE
            prpty.MIO_STATUS = Convert.ToInt32(GlobalModule.MIOStatus.Fresh)
            prpty.CREATED_BY = v_the_current_logged_in_user_name
            prpty.CREATION_DATE = Now
            prpty.MODIFIED_BY = ""
            prpty.MODIFIED_DATE = NULL_DATE
            prpty.DIVISION_ID = v_the_current_division_id
            prpty.MaterialIssueItem = flxItemList.DataSource()
            If Flag = "save" Then
                MsgResult = clsObj.Insert_Material_Issue_to_CostCenter(prpty)
                'MsgBox(MsgResult, MsgBoxStyle.Information, gblMessageHeading)
                If MsgBox(MsgResult & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptMatIssueToCostCenterPrint, "mio_id", CStr(prpty.MIO_ID), CStr(enmDataType.D_int))
                End If
            Else
                MsgBox("You can't Modify it.", MsgBoxStyle.Information, gblMessageHeading)
            End If
            set_new_initilize()


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gblMessageHeading_Error)
        End Try
    End Sub

    Private Function Validation() As Boolean
        Dim iRow As Int32
        Validation = True
        Dim blnRecExist As Boolean
        blnRecExist = False
        txtRemarks.Focus()
        For iRow = 1 To flxItemList.Rows.Count - 2
            If Convert.ToDecimal(flxItemList.Item(iRow, ("issue_qty"))) > 0 Then
                blnRecExist = True
                Exit For
            End If
        Next iRow
        For iRow = 1 To flxItemList.Rows.Count - 2
            If Convert.ToDecimal(flxItemList.Item(iRow, ("issue_qty"))) > 0 Then
                blnRecExist = True
                Exit For
            End If
        Next iRow
        If blnRecExist = True Then
            Validation = True
            Exit Function
        Else
            Validation = False
            MsgBox("Select aleast one valid item in MICC to save Material Issue To Cost Center information", vbExclamation, gblMessageHeading)
            Exit Function
        End If
    End Function

    Private Sub frm_Material_Issue_To_Cost_Center_Master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dtpFrom.Value = Now.AddDays(-30)
            TBCMaterialIssueToCosCenter.SelectTab(1)
            Flag = "save"
            ' obj.FormatGrid(flx_Material_Issue_List)
            ' obj.FormatGrid(flxItemList)
            FillDetail_Grid()
            BindCostCenterCombo()
            BindMRSCombo()
            FillDetail_GridLIST()
            set_new_initilize()
            'table_style()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Material Issue To Cost Center")
        Finally
        End Try
    End Sub

    Private Sub BindCostCenterCombo()
        ''********************************************************************''
        ''Fill cost center combo
        ''********************************************************************''
        Dim Dt As New DataTable
        Dim DtRow As DataRow

        Query = "Select COSTCENTER_ID,COSTCENTER_NAME + '-' + CostCenter_Code as COSTCENTER_NAME" & _
            " from COST_CENTER_MASTER" & _
            " where CostCenter_Status = 1 and Division_Id = " & v_the_current_division_id
        Dt = clsObj.Fill_DataSet(Query).Tables(0)
        DtRow = Dt.NewRow
        DtRow("COSTCENTER_ID") = -1
        DtRow("COSTCENTER_NAME") = "Select Cost Center"
        Dt.Rows.InsertAt(DtRow, 0)
        cmbCostCenter.DisplayMember = "COSTCENTER_NAME"
        cmbCostCenter.ValueMember = "COSTCENTER_ID"
        cmbCostCenter.DataSource = Dt
        cmbCostCenter.SelectedIndex = 0

        cmbLCostCenter.DisplayMember = "COSTCENTER_NAME"
        cmbLCostCenter.ValueMember = "COSTCENTER_ID"
        cmbLCostCenter.DataSource = Dt
        cmbLCostCenter.SelectedIndex = 0
        ''********************************************************************''GET_MATERIAL_ISSUE_CCDETAIL
        ''********************************************************************''
    End Sub

    Private Sub cmbCostCenter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCostCenter.SelectedIndexChanged
        BindMRSCombo()
    End Sub

    Private Sub BindMRSCombo()
        ''********************************************************************''
        ''Fill material requisition combo - show only pending (2) requisitions
        ''********************************************************************''
        Query = " Select MRS_CODE + cast(MRS_NO as varchar) as MRS_CODE, MRS_ID" & _
            " from MRS_MAIN_STORE_MASTER where " & _
            " MRS_STATUS = 2 and CC_ID = " & cmbCostCenter.SelectedValue & " order by MRS_ID DESC"
        TemporaryTable = clsObj.Fill_DataSet(Query).Tables(0)
        TemporaryRow = TemporaryTable.NewRow
        TemporaryRow("MRS_ID") = -1
        TemporaryRow("MRS_CODE") = "Select Cost Center"
        TemporaryTable.Rows.InsertAt(TemporaryRow, 0)
        cmbMRSDetail.DisplayMember = "MRS_CODE"
        cmbMRSDetail.ValueMember = "MRS_ID"
        cmbMRSDetail.DataSource = TemporaryTable
        ''********************************************************************''
        ''********************************************************************''
    End Sub

    Private Sub cmbMRSDetail_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMRSDetail.SelectedIndexChanged
        FillGrid()
    End Sub

    Private Sub FillGrid(Optional ByVal iSt As Boolean = False)
        Try
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Query = " SELECT" & _
            '        "   ITEM_MASTER.ITEM_ID, " & _
            '        "   ITEM_MASTER.ITEM_CODE,   " & _
            '        "   ITEM_MASTER.ITEM_NAME," & _
            '        "   UNIT_MASTER.UM_Name,dbo.Get_Average_Rate_as_on_date(ITEM_MASTER.ITEM_ID,'" & Now.ToString("dd-MMM-yyyy") & "'," & v_the_current_division_id & ",0) as Item_Rate," & _
            '        "   STOCK_DETAIL.Batch_no,  " & _
            '        "   STOCK_DETAIL.Expiry_date, " & _
            '        "   STOCK_DETAIL.Balance_Qty as batch_qty, " & _
            '        "   Bal_QTY as Req_Qty," & _
            '        "   0.0 as Issue_Qty , " & _
            '        "   Stock_Detail_Id " & _
            '        "FROM " & _
            '        "	MRS_MAIN_STORE_DETAIL " & _
            '        "	INNER JOIN ITEM_MASTER ON MRS_MAIN_STORE_DETAIL.ITEM_ID = ITEM_MASTER.ITEM_ID " & _
            '        "   INNER JOIN UNIT_MASTER ON ITEM_MASTER.issue_um_id = UNIT_MASTER.UM_ID " & _
            '        "   LEFT OUTER JOIN STOCK_DETAIL ON STOCK_DETAIL.ITEM_ID =MRS_MAIN_STORE_DETAIL.ITEM_ID " & _
            '        "WHERE " & _
            '        "	MRS_ID = " & cmbMRSDetail.SelectedValue & " AND STOCK_DETAIL.Balance_Qty > 0 order by ITEM_NAME"
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            '"	MRS_ID = " & cmbMRSDetail.SelectedValue & " AND STOCK_DETAIL.Balance_Qty > 0 order by ITEM_ID, ITEM_CODE, ITEM_NAME, Bal_QTY"
            '"   LEFT OUTER JOIN vw_Stock_Detail_withot_zero_Balance as STOCK_DETAIL ON MRS_MAIN_STORE_DETAIL.ITEM_ID = STOCK_DETAIL.Item_id " & _

            Query = " SELECT" & _
                   "   ITEM_MASTER.ITEM_ID, " & _
                   "   ITEM_MASTER.ITEM_CODE,   " & _
                   "   ITEM_MASTER.ITEM_NAME," & _
                   "   UNIT_MASTER.UM_Name,dbo.Get_Average_Rate_as_on_date(ITEM_MASTER.ITEM_ID,'" & Now.ToString("dd-MMM-yyyy") & "'," & v_the_current_division_id & ",0) as Item_Rate," & _
                   "   0 AS Batch_no,  " & _
                   "   'Default' AS Expiry_Date, " & _
                   "   dbo.Get_Stock_as_on_date(item_master.ITEM_ID," & v_the_current_division_id & ", '" & Now.ToString("dd-MMM-yyyy") & "', 1) AS batch_qty, " & _
                   "   Bal_QTY as Req_Qty," & _
                   "   0.0 as Issue_Qty , " & _
                   "   max(Stock_Detail_Id) as Stock_Detail_Id " & _
                   "FROM " & _
                   "	MRS_MAIN_STORE_DETAIL " & _
                   "	INNER JOIN ITEM_MASTER ON MRS_MAIN_STORE_DETAIL.ITEM_ID = ITEM_MASTER.ITEM_ID " & _
                   "   INNER JOIN UNIT_MASTER ON ITEM_MASTER.issue_um_id = UNIT_MASTER.UM_ID " & _
                   "   LEFT OUTER JOIN STOCK_DETAIL ON STOCK_DETAIL.ITEM_ID =MRS_MAIN_STORE_DETAIL.ITEM_ID " & _
                   "WHERE " & _
                   "	MRS_ID = " & cmbMRSDetail.SelectedValue & "" & _
                   "    GROUP BY ITEM_MASTER.ITEM_ID,ITEM_MASTER.ITEM_CODE,ITEM_MASTER.ITEM_NAME,UNIT_MASTER.UM_Name,Bal_QTY " & _
                   "   order by ITEM_NAME"
            TemporaryTable = clsObj.Fill_DataSet(Query).Tables(0)
            flxItemList.DataSource = TemporaryTable


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
        End Try
    End Sub

    Public Function GetMRSCode() As String
        Dim Pre As String
        Dim CCID As String
        Dim MIOCode As String
        Pre = obj.getPrefixCode("MIO_PREFIX", "DIVISION_SETTINGS")
        CCID = obj.getMaxValue("MIO_ID", "MATERIAL_ISSUE_TO_COST_CENTER_MASTER")
        MIOCode = Pre & "" & CCID
        Return MIOCode
    End Function

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Dim _MIO_ID As Integer
        Try
            If TBCMaterialIssueToCosCenter.SelectedIndex = 0 Then
                obj.RptShow(enmReportName.RptMatIssueToCostCenterPrint, "MIO_ID", CStr(flx_Material_Issue_List.Rows(flx_Material_Issue_List.CursorCell.r1).Item("MIO_ID").ToString()), CStr(enmDataType.D_int))
            Else
                If Flag <> "save" Then
                    obj.RptShow(enmReportName.RptMatIssueToCostCenterPrint, "MIO_ID", CStr(_MIO_ID), CStr(enmDataType.D_int))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub table_style()
        'dtable_Item_List = New DataTable()
        'dtable_Item_List.Columns.Add("Item_ID", GetType(System.Int32))
        'dtable_Item_List.Columns.Add("MRS_ID", GetType(System.Int32))
        'dtable_Item_List.Columns.Add("Item_Code", GetType(System.String))
        'dtable_Item_List.Columns.Add("Item_Name", GetType(System.String))
        'dtable_Item_List.Columns.Add("UM_NAME", GetType(System.String))
        'dtable_Item_List.Columns.Add("ITEM_QTY", GetType(System.Double))
        'dtable_Item_List.Columns.Add("TRANSFER_RATE", GetType(System.Double))
        'dtable_Item_List.Columns.Add("ISSUED_QTY", GetType(System.Double))
        'dtable_Item_List.Columns.Add("ACCEPTED QTY", GetType(System.Double))
        'dtable_Item_List.Columns.Add("RETURNED QTY", GetType(System.Double))
        'grdMRNDetail.DataSource = dtable_Item_List
    End Sub

    Private Sub lnkSelectItems_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        frm_MRN_ITEMS.Show()
    End Sub

    Private Sub grd_MaterialIssue_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            'Dim Cntgrd_MaterialIssue As Integer
            'Cntgrd_MaterialIssue = grd_MaterialIssue.Rows.Count - 1
            'Dim CntgrdMRNDetail As Integer
            'CntgrdMRNDetail = grdMRNDetail.Rows.Count - 1
            'If Cntgrd_MaterialIssue = CntgrdMRNDetail AndAlso Not Cntgrd_MaterialIssue > CntgrdMRNDetail Then
            '    grdMRNDetail.AllowUserToAddRows = False
            '    Exit Sub
            'End If
            'If grd_MaterialIssue.Rows(e.RowIndex).Cells("Issued Qty").Value = Trim("") Then
            '    grd_MaterialIssue.Rows(e.RowIndex).Cells("Issued Qty").Value = 0
            'End If
            'Dim ItemQty, IssuedQty, AcceptedQty, ReturnedQty, Item_ID, MRS_ID, Transfer_Rate As Integer
            'Dim ItemName, ItemCode, UOM As String
            'ItemCode = grd_MaterialIssue.Rows(e.RowIndex).Cells("ITEM_CODE").Value
            'ItemName = grd_MaterialIssue.Rows(e.RowIndex).Cells("ITEM_NAME").Value
            'UOM = grd_MaterialIssue.Rows(e.RowIndex).Cells("UM_NAME").Value
            'ItemQty = grd_MaterialIssue.Rows(e.RowIndex).Cells("ITEM_QTY").Value
            'Item_ID = grd_MaterialIssue.Rows(e.RowIndex).Cells("ITEM_ID").Value
            'MRS_ID = grd_MaterialIssue.Rows(e.RowIndex).Cells("MRS_ID").Value
            'Transfer_Rate = grd_MaterialIssue.Rows(e.RowIndex).Cells("TRANSFER_RATE").Value
            'IssuedQty = Convert.ToDecimal(grd_MaterialIssue.Rows(e.RowIndex).Cells("Issued Qty").Value)
            'Dim i As Integer
            'For i = 0 To grdMRNDetail.Rows.Count - 1
            '    If grdMRNDetail.Rows(i).Cells("ITEM_CODE").Value = ItemCode Then
            '        Exit Sub
            '    End If
            'Next
            'dtable_Item_List.Rows.Add(Item_ID, MRS_ID, ItemCode, ItemName, UOM, ItemQty, Transfer_Rate, Convert.ToDecimal(IssuedQty), AcceptedQty, ReturnedQty)
            'grdMRNDetail.DataSource = dtable_Item_List
            'grdMRNDetail.Columns(0).Visible = False
            'grdMRNDetail.Columns(1).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
        End Try
    End Sub

    Private Sub grid_style()
        'Dim txbCol As DataGridViewTextBoxColumn
        'Dim cmbCol As New DataGridViewComboBoxColumn
        'grdMRNDetail.Columns.Clear()
        'txbCol = New DataGridViewTextBoxColumn

        'With txbCol
        '    .HeaderText = "Item Code"
        '    .Name = "Item_Code"
        '    .DataPropertyName = "Item_Code"
        '    .ReadOnly = True
        '    .Visible = True
        '    .Width = 100
        'End With
        'grdMRNDetail.Columns.Add(txbCol)

        'txbCol = New DataGridViewTextBoxColumn
        'With txbCol
        '    .HeaderText = "Item Name"
        '    .Name = "Item_Name"
        '    .ReadOnly = True
        '    .Visible = True
        '    .Width = 100
        'End With
        'grdMRNDetail.Columns.Add(txbCol)

        'txbCol = New DataGridViewTextBoxColumn
        'With txbCol
        '    .HeaderText = "Item Unit"
        '    .Name = "Req_Qty"
        '    .ReadOnly = True
        '    .Visible = True
        '    .Width = 100
        'End With
        'grdMRNDetail.Columns.Add(txbCol)

        'txbCol = New DataGridViewTextBoxColumn
        'With txbCol
        '    .HeaderText = "Item Qty"
        '    .Name = "Req_Qty"
        '    .ReadOnly = True
        '    .Visible = True
        '    .Width = 100
        'End With
        'grdMRNDetail.Columns.Add(txbCol)

        'txbCol = New DataGridViewTextBoxColumn
        'With txbCol
        '    .HeaderText = "Issued Qty"
        '    .Name = "Issued_Qty"
        '    .ReadOnly = True
        '    .Visible = True
        '    .Width = 100
        'End With
        'grdMRNDetail.Columns.Add(txbCol)

        'txbCol = New DataGridViewTextBoxColumn
        'With txbCol
        '    .HeaderText = "Accepted Qty"
        '    .Name = "Accepted_Qty"
        '    .ReadOnly = True
        '    .Visible = True
        '    .Width = 100
        'End With
        'grdMRNDetail.Columns.Add(txbCol)

        'txbCol = New DataGridViewTextBoxColumn
        'With txbCol
        '    .HeaderText = "Returned Qty"
        '    .Name = "Returned_Qty"
        '    .ReadOnly = True
        '    .Visible = True
        '    .Width = 100
        'End With
        'grdMRNDetail.Columns.Add(txbCol)
    End Sub

    Sub NumericValuegrd_MaterialIssue(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'Dim colindex As Decimal = grd_MaterialIssue.CurrentCell.ColumnIndex
        'If colindex = 5 Then
        '    Select Case Asc(e.KeyChar)
        '        Case AscW(ControlChars.Cr) 'Enter key
        '            e.Handled = True
        '        Case AscW(ControlChars.Back) 'Backspace
        '        Case 45, 46, 48 To 57 'Negative sign, Decimal and Numbers
        '        Case Else ' Everything else
        '            e.Handled = True
        '    End Select
        'End If
    End Sub

    Private Sub grd_MaterialIssue_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs)
        Try
            AddHandler e.Control.KeyPress, AddressOf NumericValuegrd_MaterialIssue
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Material Issue To cost Center")
        End Try
    End Sub

    Sub NumericValuegrdMRNDetail(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'Dim colindex As Decimal = grdMRNDetail.CurrentCell.ColumnIndex
        'If colindex = 7 Then
        '    Select Case Asc(e.KeyChar)
        '        Case AscW(ControlChars.Cr) 'Enter key
        '            e.Handled = True
        '        Case AscW(ControlChars.Back) 'Backspace
        '        Case 45, 46, 48 To 57 'Negative sign, Decimal and Numbers
        '        Case Else ' Everything else
        '            e.Handled = True
        '    End Select
        'End If
    End Sub

    Private Sub grdMRNDetail_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs)
        Try
            AddHandler e.Control.KeyPress, AddressOf NumericValuegrdMRNDetail
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Material Issue To cost Center ")
        End Try
    End Sub

    Private Sub FillDetail_GridLIST()
        Try
            flx_Material_Issue_List.Cols.Fixed = 1
            Dim param As String = "", val As String = ""
            param = "@FromDate,@ToDate,@costid"



            If dtpFrom.Text.Trim() <> "" Then
                val += Convert.ToString(dtpFrom.Value.Date)
            Else
                '  val += ","
            End If
            If dtpTo.Text.Trim() <> "" Then
                val += "," & Convert.ToString(dtpTo.Value.Date)
            Else
                val += ","
            End If
            If cmbLCostCenter.SelectedIndex <> -1 Then
                val += "," & Convert.ToString(cmbLCostCenter.SelectedValue)
            Else
                val += "-1"
            End If

            flx_Material_Issue_List.DataSource = clsObj.fill_Data_set("GET_MATERIAL_ISSUE_CCDETAIL", param, val).Tables(0)

            flx_Material_Issue_List.Cols(0).Width = 10

            flx_Material_Issue_List.Cols("MIO_ID").Caption = "MIO ID"
            flx_Material_Issue_List.Cols("MIO_ID").Visible = False

            flx_Material_Issue_List.Cols("MIO_No").Caption = "MIO No"
            flx_Material_Issue_List.Cols("MIO_No").AllowEditing = False
            flx_Material_Issue_List.Cols("MIO_No").Width = 150

            flx_Material_Issue_List.Cols("MIO_Date").Caption = "MIO Date"
            flx_Material_Issue_List.Cols("MIO_Date").AllowEditing = False
            flx_Material_Issue_List.Cols("MIO_Date").Width = 150

            flx_Material_Issue_List.Cols("MIO_STATUS").Caption = "MIO STATUS"
            flx_Material_Issue_List.Cols("MIO_STATUS").AllowEditing = False
            flx_Material_Issue_List.Cols("MIO_STATUS").Width = 100

            flx_Material_Issue_List.Cols("MRS_NO").Caption = "MRS NO"
            flx_Material_Issue_List.Cols("MRS_NO").AllowEditing = False
            flx_Material_Issue_List.Cols("MRS_NO").Width = 150

            flx_Material_Issue_List.Cols("CostCenter_Name").Caption = "Cost Center Name"
            flx_Material_Issue_List.Cols("CostCenter_Name").AllowEditing = False
            flx_Material_Issue_List.Cols("CostCenter_Name").Visible = True
            flx_Material_Issue_List.Cols("CostCenter_Name").Width = 300

            ' flx_Material_Issue_List.Cols("CostCenter_Name").Caption = "Cost Center Name"
            'flx_Material_Issue_List.Cols("CostCenter_Name").AllowEditing = False
            flx_Material_Issue_List.Cols("CostCenter_Id").Visible = False
            ' flx_Material_Issue_List.Cols("CostCenter_Name").Width = 150

            'flx_Material_Issue_List.Cols("MRS_CODE").Caption = "MRS CODE"
            'flx_Material_Issue_List.Cols("MRS_CODE").AllowEditing = False
            ' flx_Material_Issue_List.Cols("MRS_CODE").Width = 100


            ' flx_Material_Issue_List.Cols("MIO_REMARKS").Caption = "Remarks"
            'flx_Material_Issue_List.Cols("MIO_REMARKS").AllowEditing = False

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub
    Private Sub FillDetail_Grid()
        Try
            'obj.Grid_Bind(grdMIODetail, "GET_MATERIAL_ISSUE_DETAIL")
            'grdMIODetail.Columns(0).Visible = False
            'grdMIODetail.Columns(0).HeaderText = "MIO ID"
            'grdMIODetail.Columns(1).HeaderText = "MIO CODE"
            'grdMIODetail.Columns(1).ReadOnly = True
            'grdMIODetail.Columns(2).HeaderText = "MIO Date"
            'grdMIODetail.Columns(2).ReadOnly = True
            'grdMIODetail.Columns(3).HeaderText = "MIO No"
            'grdMIODetail.Columns(3).ReadOnly = True
            'grdMIODetail.Columns(4).HeaderText = "MRS Code"
            'grdMIODetail.Columns(4).ReadOnly = True
            'grdMIODetail.Columns(5).Visible = False
            'grdMIODetail.Columns(5).HeaderText = "MRS ID"
            'grdMIODetail.Columns(6).Visible = False
            'grdMIODetail.Columns(6).HeaderText = "MIO Status"
            'grdMIODetail.Columns(7).HeaderText = "MIO Name"
            'grdMIODetail.Columns(7).ReadOnly = True
            ''grdMIODetail.Columns(8).Visible = False
            ''grdMIODetail.Columns(8).HeaderText = "MRS ID Detail"
            'grdMIODetail.Columns(8).Visible = False
            'grdMIODetail.Columns(8).HeaderText = "Cost Center ID"
            'grdMIODetail.Columns(9).HeaderText = "Cost Center Name"
            'grdMIODetail.Columns(9).ReadOnly = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
        End Try
    End Sub

    Private Sub grdMIODetail_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        'Try
        '    MIOID = grdMIODetail.Rows(e.RowIndex).Cells("MIO_ID").Value
        '    MRSID = grdMIODetail.Rows(e.RowIndex).Cells("MRS_ID").Value
        '    getMIODetail(MIOID, MRSID)
        '    cmbMRSDetail.Enabled = False
        '    TabControl1.SelectTab(1)
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Material Isue To Cost Center")
        'End Try

    End Sub

    Private Sub getMIODetail(ByVal MIO_ID As Integer, ByVal MRS_ID As Integer)
        'Try
        '    Ds = obj.fill_Data_set_val("GET_MATERIAL_ISSUE_TOTAL_DETAIL", "@V_MIO_ID", "@V_MRS_ID", MIOID, MRSID)
        '    If Ds.Tables(0).Rows.Count > 0 Then
        '        Dim Dt As DataTable = Ds.Tables(0)
        '        Dim Dt1 As DataTable = Ds.Tables(1)
        '        Dim Dt2 As DataTable = Ds.Tables(2)
        '        If Ds.Tables(0).Rows.Count > 0 Then
        '            txtDeliveredBy.Text = Dt.Rows(0).Item("DELIVERED_BY")
        '            dtpMIODate.Text = Dt.Rows(0).Item("MIO_DATE") 'dr("MIO_DATE")
        '            txtDeliveredBy.Text = Dt.Rows(0).Item("DELIVERED_BY")
        '            txtDeliveredAt.Text = Dt.Rows(0).Item("DELIVERED_AT")
        '            txtDeliveredAt.Text = Dt.Rows(0).Item("DELIVERED_AT")
        '            txtVehicleNo.Text = Dt.Rows(0).Item("VEHICLE_NO")
        '        End If
        '        If Ds.Tables(1).Rows.Count > 0 Then
        '            If grd_MaterialIssue.Rows.Count > 0 Then obj.RemoveRowsFromGrid(grd_MaterialIssue, grd_MaterialIssue.Rows.Count)
        '            grd_MaterialIssue.DataSource = Dt1
        '        End If
        '        If Ds.Tables(2).Rows.Count > 0 Then
        '            If grdMRNDetail.Rows.Count > 0 Then obj.RemoveRowsFromGrid(grdMRNDetail, grdMRNDetail.Rows.Count)
        '            grdMRNDetail.DataSource = Dt2
        '            grdMRNDetail.Columns(0).Visible = False
        '            grdMRNDetail.Columns(1).Visible = False
        '        End If
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Material Issue To Cost Center")
        'Finally
        '    Ds.Dispose()
        'End Try
    End Sub

    Private Sub grd_MaterialIssue_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles flxItemList.AfterDataRefresh
        generate_tree()
    End Sub

    Private Sub generate_tree()

        If flxItemList.Rows.Count > 1 Then
            'Dim strSort As String = flxItemList.Cols(1).Name + ", " + flxItemList.Cols(2).Name + ", " + flxItemList.Cols(3).Name
            Dim strSort As String = flxItemList.Cols(3).Name
            Dim dt As DataTable = CType(flxItemList.DataSource, DataTable)
            If Not dt Is Nothing Then
                dt.DefaultView.Sort = strSort
            End If

            flxItemList.Tree.Style = TreeStyleFlags.CompleteLeaf
            flxItemList.Tree.Column = 2
            flxItemList.AllowMerging = AllowMergingEnum.None

            Dim totalOn As Integer = flxItemList.Cols("Req_Qty").SafeIndex
            flxItemList.Subtotal(AggregateEnum.Max, 0, 3, totalOn)
            totalOn = flxItemList.Cols("batch_qty").SafeIndex
            flxItemList.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)
            totalOn = flxItemList.Cols("issue_qty").SafeIndex
            flxItemList.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)
            flxItemList.Cols(0).Width = 10
            flxItemList.Cols(1).Visible = False
            flxItemList.Cols("ITEM_CODE").Caption = "Item Code"
            flxItemList.Cols("ITEM_NAME").Caption = "Item Name"
            flxItemList.Cols("UM_Name").Caption = "UOM"
            flxItemList.Cols("Req_Qty").Caption = "Req.Qty"
            flxItemList.Cols("Batch_no").Caption = "Batch No."
            flxItemList.Cols("Expiry_date").Caption = "Expiry Date"
            flxItemList.Cols("batch_qty").Caption = "batch_qty"
            flxItemList.Cols("issue_qty").Caption = "Issue Qty"
            flxItemList.Cols("item_rate").Caption = "Item Rate"
            flxItemList.Cols("Stock_Detail_Id").Caption = "Stock Detail Id"

            'flxItemList.Cols("MIO_ID").Caption = "MIO Id"
            'flxItemList.Cols("MRS_ID").Caption = "MRS Id"

            flxItemList.Cols("ITEM_CODE").Width = 90
            flxItemList.Cols("ITEM_NAME").Width = 300
            flxItemList.Cols("UM_Name").Width = 40
            flxItemList.Cols("Req_Qty").Width = 75
            flxItemList.Cols("Batch_no").Width = 75
            flxItemList.Cols("Expiry_date").Width = 80
            flxItemList.Cols("batch_qty").Width = 60
            flxItemList.Cols("issue_qty").Width = 75
            flxItemList.Cols("item_rate").Width = 70
            flxItemList.Cols("Stock_Detail_Id").Width = 70


            'flxItemList.Cols("MIO_ID").Width = 10
            'flxItemList.Cols("MRS_ID").Width = 10


            flxItemList.Cols("ITEM_CODE").AllowEditing = False
            flxItemList.Cols("Stock_Detail_Id").AllowEditing = False
            flxItemList.Cols("ITEM_NAME").AllowEditing = False
            flxItemList.Cols("UM_Name").AllowEditing = False
            flxItemList.Cols("Req_Qty").AllowEditing = False
            flxItemList.Cols("Batch_no").AllowEditing = False
            flxItemList.Cols("Expiry_date").AllowEditing = False
            flxItemList.Cols("batch_qty").AllowEditing = False
            flxItemList.Cols("issue_qty").AllowEditing = True
            flxItemList.Cols("item_rate").AllowEditing = False

            'flxItemList.Cols("MIO_ID").AllowEditing = False
            ' flxItemList.Cols("MRS_ID").AllowEditing=False


            flxItemList.Cols("Stock_Detail_Id").Visible = False
            'flxItemList.Cols("MIO_ID").Visible = False
            'flxItemList.Cols("MRS_ID").Visible = False
        End If


        Dim cs As C1.Win.C1FlexGrid.CellStyle

        cs = Me.flxItemList.Styles.Add("issue_qty")
        cs.ForeColor = Color.White
        cs.BackColor = Color.OrangeRed
        cs.Border.Style = BorderStyleEnum.Raised

        Dim i As Integer
        For i = 1 To flxItemList.Rows.Count - 1
            If Not flxItemList.Rows(i).IsNode Then flxItemList.SetCellStyle(i, 10, cs)
        Next
    End Sub

    Private Sub format_grid()


    End Sub

    Private Sub flxItemList_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles flxItemList.KeyPressEdit
        e.Handled = flxItemList.Rows(flxItemList.CursorCell.r1).IsNode
    End Sub

    Private Sub flxItemList_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles flxItemList.AfterEdit

        Dim dt As DataTable
        Dim total_issued, total_required As Double

        If flxItemList.Rows(e.Row).IsNode Then Exit Sub

        If flxItemList.Rows(e.Row)("issue_qty") > flxItemList.Rows(e.Row)("batch_qty") Then
            flxItemList.Rows(e.Row)("issue_qty") = 0
        End If

        dt = flxItemList.DataSource
        total_issued = dt.Compute("sum(issue_qty)", "item_id=" & flxItemList.Rows(e.Row)("item_id"))
        total_required = dt.Compute("max(req_qty)", "item_id=" & flxItemList.Rows(e.Row)("item_id"))
        If total_issued > total_required Then
            flxItemList.Rows(e.Row)("issue_qty") = 0
        End If

        'If flxItemList.Rows(e.Row).Item("REQUIRED_QTY") < grdIndentItems.Rows(e.Row).Item("Order_Qty") And Not grdIndentItems.Rows(grdIndentItems.CursorCell.r1).IsNode Then
        '    grdIndentItems.Rows(e.Row).Item("Order_Qty") = 0
        'End If
    End Sub

    Private Sub btnIssueSlip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIssueSlip.Click
        FillDetail_GridLIST()
    End Sub

    Private Sub flx_Material_Issue_List_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flx_Material_Issue_List.DoubleClick
        MsgBox("You can't Modify it." & vbCrLf & "You may see print preview by click on view.", MsgBoxStyle.Information, gblMessageHeading)

        'Dim MIO_Id As Integer
        'Dim dtMaster As New DataTable
        'Dim dtDetail As New DataTable
        'Dim ds As New DataSet
        'MIO_Id = Convert.ToInt32(flx_Material_Issue_List.Rows(flx_Material_Issue_List.CursorCell.r1)("MIO_ID"))
        'Flag = "update"
        'ds = obj.fill_Data_set("Get_Material_Issue_FillDetail", "@V_MIO_ID", MIO_Id)
        'If ds.Tables.Count > 0 Then
        '    dtMaster = ds.Tables(0)
        '    txtRemarks.Text = obj.NZ(dtMaster.Rows(0)("MIO_REMARKS"), True)
        '    dtable_Item_List = ds.Tables(1).Copy
        '    flxItemList.DataSource = dtable_Item_List
        '    cmbMRSDetail.SelectedValue = dtable_Item_List.Rows(0)("MRS_Id")
        '    TBCMaterialIssueToCosCenter.SelectTab(1)
        '    ds.Dispose()
        'End If

    End Sub


End Class
