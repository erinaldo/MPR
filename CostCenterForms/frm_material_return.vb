Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports System.Data

Public Class frm_material_return
    Implements IForm
    Dim obj As CommonClass = New CommonClass
    Dim dTable_IssuedItems As DataTable
    Dim flag As String
    Dim clsObj As New material_return_master.cls_material_return_master
    Dim prpty As New material_return_master.cls_material_return_master_prop


    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_material_return_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        bindCombo()
        lbl_Status.Text = "Fresh"
        lbl_ReturnNo.Text = "RetNo/" + obj.ExecuteScalar("select isnull(max(return_no),0) + 1 from material_return_master").ToString()
    End Sub

    Private Sub Grid_styles()

        dTable_IssuedItems = New DataTable()
        dTable_IssuedItems.Columns.Add("ITEM_ID", GetType(System.Int32))
        dTable_IssuedItems.Columns.Add("MIO_DATE", GetType(System.DateTime))
        dTable_IssuedItems.Columns.Add("ITEM_CODE", GetType(System.String))
        dTable_IssuedItems.Columns.Add("ITEM_NAME", GetType(System.String))
        dTable_IssuedItems.Columns.Add("UM_Name", GetType(System.String))
        dTable_IssuedItems.Columns.Add("BATCH_NO", GetType(System.String))
        dTable_IssuedItems.Columns.Add("ISSUED_QTY", GetType(System.Double))
        dTable_IssuedItems.Columns.Add("RETURNED_QTY", GetType(System.Double))
        dTable_IssuedItems.Columns.Add("ACCEPTED_QTY", GetType(System.Double))
        dTable_IssuedItems.Columns.Add("STOCK_DETAIL_ID", GetType(System.Double))

        flx_Material_Issue_CC.DataSource = dTable_IssuedItems

        dTable_IssuedItems.Rows.Add(dTable_IssuedItems.NewRow)
        flx_Material_Issue_CC.Cols(0).Width = 10
        SetGridSettingValues()

    End Sub
    Private Sub SetGridSettingValues()
        flx_Material_Issue_CC.Cols(1).Visible = False
        flx_Material_Issue_CC.Cols(2).Visible = False
        flx_Material_Issue_CC.Cols("ITEM_CODE").Caption = "Item Code"
        flx_Material_Issue_CC.Cols("MIO_DATE").Caption = "Issue Date"
        flx_Material_Issue_CC.Cols("BATCH_NO").Caption = "Batch No."
        flx_Material_Issue_CC.Cols("ISSUED_QTY").Caption = "Issued Qty"
        flx_Material_Issue_CC.Cols("ACCEPTED_QTY").Caption = "Accepted Qty"
        flx_Material_Issue_CC.Cols("RETURNED_QTY").Caption = "Returned Qty"
        flx_Material_Issue_CC.Cols("ITEM_NAME").Caption = "Item Name"
        flx_Material_Issue_CC.Cols("UM_Name").Caption = "UOM"

        flx_Material_Issue_CC.Cols("Item_Code").AllowEditing = False
        flx_Material_Issue_CC.Cols("MIO_DATE").AllowEditing = False
        flx_Material_Issue_CC.Cols("BATCH_NO").AllowEditing = False
        flx_Material_Issue_CC.Cols("ISSUED_QTY").AllowEditing = False
        flx_Material_Issue_CC.Cols("ACCEPTED_QTY").AllowEditing = False
        flx_Material_Issue_CC.Cols("RETURNED_QTY").AllowEditing = True
        flx_Material_Issue_CC.Cols("ITEM_NAME").AllowEditing = False
        flx_Material_Issue_CC.Cols("UM_Name").AllowEditing = False

        flx_Material_Issue_CC.Cols(0).Width = 10
        flx_Material_Issue_CC.Cols("Item_Code").Width = 60
        flx_Material_Issue_CC.Cols("MIO_DATE").Width = 100
        flx_Material_Issue_CC.Cols("BATCH_NO").Width = 70
        flx_Material_Issue_CC.Cols("ISSUED_QTY").Width = 80
        flx_Material_Issue_CC.Cols("ACCEPTED_QTY").Width = 120
        flx_Material_Issue_CC.Cols("RETURNED_QTY").Width = 120
        flx_Material_Issue_CC.Cols("Item_Name").Width = 280
        flx_Material_Issue_CC.Cols("UM_Name").Width = 40
    End Sub


    Private Sub bindCombo()
        Dim qry As String = "select '0' as mio_id,'--Select--' as MIO_No union all select mio_id,mio_code + cast(mio_no as varchar) as MIO_No from material_issue_to_cost_center_master where cs_id = " + Convert.ToString(v_the_current_division_id)

        obj.ComboBind(cmb_IssueSlips, qry, "MIO_No", "mio_id")
    End Sub
    Private Sub FillGrid(Optional ByVal condition As String = "")
        Try
            'obj.GridBind(grdSupplierList, "SELECT srl_id,srl_name,srl_desc,active,creation_date from Supplier_rate_list")
            Dim qry As String
            qry = "select return_id,return_code + cast(return_no as varchar) as return_no,return_date," & _
                   " case when return_status = 1 then 'Fresh' when return_status = 2 then 'Freezed' end as Status," & _
            " return_remarks, freezed_date" & _
            " from material_return_master where division_id = " + Convert.ToString(v_the_current_Selected_CostCenter_id)

            obj.GridBind(DGVW_list_materialReturn, qry)
            DGVW_list_materialReturn.Width = 1000
            DGVW_list_materialReturn.Columns(0).Visible = False 'receiveid
            DGVW_list_materialReturn.Columns(0).Width = 300
            DGVW_list_materialReturn.Columns(0).HeaderText = "return_id"
            DGVW_list_materialReturn.Columns(1).Width = 150
            DGVW_list_materialReturn.Columns(1).HeaderText = "Return No."
            DGVW_list_materialReturn.Columns(2).HeaderText = "Status"
            DGVW_list_materialReturn.Columns(3).Width = 150
            DGVW_list_materialReturn.Columns(3).HeaderText = "Purchase Id"
            DGVW_list_materialReturn.Columns(3).Visible = False
            DGVW_list_materialReturn.Columns(4).HeaderText = "Remarks"
            DGVW_list_materialReturn.Columns(4).Width = 350
            DGVW_list_materialReturn.Columns(5).Width = 120
            DGVW_list_materialReturn.Columns(5).HeaderText = "Freezed Date"

            'dgvList.Columns(6).Visible = False
            'dgvList.Columns(6).HeaderText = "ACC ID"
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
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

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick

    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Dim cmd As SqlCommand
        cmd = obj.MyCon_BeginTransaction

        Try
            Dim RETURN_ID As Integer


            'RETURN_ID = Convert.ToInt32(obj.getMaxValue("RETURN_ID", "MATERIAL_RETURN_MASTER"))
            'prpty.Return_ID = Convert.ToInt32(RETURN_ID)
            'prpty.Return_Code = "Ret/No/" ' GetReceivedCode()
            'prpty.Return_No = RETURN_ID ' Convert.ToInt32(RECEIVEDID)
            'prpty.Received_Date = Now 'Convert.ToDateTime(lbl_PODate.Text).ToString()
            'prpty.Return_Warehouse_ID = 3 'WareHouse Id Fixed'
            'prpty.Remarks = txt_Remarks.Text
            'prpty.mrn_status = Convert.ToInt32(GlobalModule.MRNStatus.normal)
            'prpty.Created_By = v_the_current_logged_in_user_name
            'prpty.Creation_Date = Now
            'prpty.Modified_By = ""
            'prpty.Modification_Date = NULL_DATE
            'prpty.Division_ID = v_the_current_division_id
            'If rb_Amount.Checked Then
            '    prpty.freight_type = "A"
            '    prpty.freight = Convert.ToDecimal(txt_Amount.Text)
            'Else
            '    prpty.freight_type = "P"
            '    prpty.freight = Convert.ToDecimal(txt_Percentage.Text)
            'End If
            'prpty.MRNCompanies_ID = Convert.ToInt16(cmb_MRNAgainst.SelectedValue)
            ''SAVE MASTER ENTERY
            'obj.insert_MATERIAL_RECIEVED_WITHOUT_PO_MASTER(prpty, cmd)

            'Dim iRowCount As Int32
            'Dim iRow As Int32
            'iRowCount = FLXGRD_MaterialItem.Rows.Count - 1

            'For iRow = 1 To iRowCount - 1
            '    If FLXGRD_MaterialItem.Item(iRow, "Batch_Qty") > 0 Then
            '        prpty.Po_ID = RECEIVEDID
            '        prpty.Item_ID = FLXGRD_MaterialItem.Item(iRow, "Item_Id")

            '        prpty.Item_Qty = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Batch_Qty")).ToString()
            '        prpty.Item_Rate = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Item_Rate"))


            '        If Convert.ToString(FLXGRD_MaterialItem.Item(iRow, "VAT_Per")) = "" Then
            '            prpty.Item_vat = 0
            '        Else
            '            prpty.Item_vat = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "VAT_Per"))
            '        End If

            '        If Convert.ToString(FLXGRD_MaterialItem.Item(iRow, "Exe_per")) = "" Then
            '            prpty.Item_exice = 0
            '        Else
            '            prpty.Item_exice = Convert.ToDouble(FLXGRD_MaterialItem.Item(iRow, "Exe_per"))
            '        End If

            '        If FLXGRD_MaterialItem.Item(iRow, "Batch_no").ToString() = "" Then
            '            prpty.Batch_No = "Default Batch"
            '        Else
            '            prpty.Batch_No = FLXGRD_MaterialItem.Item(iRow, "Batch_no").ToString()
            '        End If
            '        If Convert.ToString(FLXGRD_MaterialItem.Item(iRow, "Expiry_Date")) = "" Then
            '            prpty.Expiry_Date = Now.AddYears(2)
            '        Else
            '            prpty.Expiry_Date = Convert.ToDateTime(FLXGRD_MaterialItem.Item(iRow, "Expiry_Date"))
            '        End If

            '        prpty.Created_By = v_the_current_logged_in_user_name
            '        prpty.Creation_Date = now
            '        prpty.Modified_By = v_the_current_logged_in_user_name
            '        prpty.Modification_Date = now
            '        prpty.Division_ID = v_the_current_division_id

            '        'SAVE DETAIL ENTRY
            '        obj.insert_MATERIAL_RECEIVED_WITHOUT_PO_DETAIL(prpty, cmd)



            '        'End If
            '    End If
            'Next iRow


            'new_initilization()

        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub


    Private Sub cmb_IssueSlips_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_IssueSlips.SelectedIndexChanged
        getMaterialIssueDetail(Convert.ToInt16(cmb_IssueSlips.SelectedValue))
    End Sub
    Protected Sub getMaterialIssueDetail(ByVal mio_id As Int16)

        Dim ds As DataSet = New DataSet
        ds = obj.fill_Data_set("GET_MATERIAL_ISSUE_TO_COST_CENTER", "@mio_id", mio_id.ToString())
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                dTable_IssuedItems = ds.Tables(0).Copy
                flx_Material_Issue_CC.DataSource = dTable_IssuedItems
                SetGridSettingValues()
            End If
        End If
    End Sub

    Private Sub flx_Material_Issue_CC_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles flx_Material_Issue_CC.KeyPressEdit
        e.Handled = flx_Material_Issue_CC.Rows(flx_Material_Issue_CC.CursorCell.r1).IsNode
    End Sub

    Private Sub flx_Material_Issue_CC_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles flx_Material_Issue_CC.AfterEdit
        Dim dt As DataTable
        Dim total_issued, TOTAL_RETURNED As Double



        If flx_Material_Issue_CC.Rows(e.Row).IsNode Then Exit Sub

        If flx_Material_Issue_CC.Rows(e.Row)("RETURNED_QTY") > flx_Material_Issue_CC.Rows(e.Row)("ISSUED_QTY") Then
            flx_Material_Issue_CC.Rows(e.Row)("RETURNED_QTY") = 0
        End If


        dt = flx_Material_Issue_CC.DataSource
        TOTAL_RETURNED = dt.Compute("sum(RETURNED_QTY)", "item_id=" & flx_Material_Issue_CC.Rows(e.Row)("item_id"))
        total_issued = dt.Compute("SUM(ISSUED_QTY)", "item_id=" & flx_Material_Issue_CC.Rows(e.Row)("item_id"))
        If TOTAL_RETURNED > total_issued Then
            flx_Material_Issue_CC.Rows(e.Row)("RETURNED_QTY") = 0
        End If
        Dim issued_qty As Double
        Dim returned_qty As Double

        issued_qty = flx_Material_Issue_CC.Rows(e.Row)("ISSUED_QTY")
        returned_qty = flx_Material_Issue_CC.Rows(e.Row)("RETURNED_QTY")
        flx_Material_Issue_CC.Rows(e.Row)("ACCEPTED_QTY") = issued_qty - returned_qty
        'If flxItemList.Rows(e.Row).Item("REQUIRED_QTY") < grdIndentItems.Rows(e.Row).Item("Order_Qty") And Not grdIndentItems.Rows(grdIndentItems.CursorCell.r1).IsNode Then
        '    grdIndentItems.Rows(e.Row).Item("Order_Qty") = 0
        'End If
    End Sub

    Private Sub flx_Material_Issue_CC_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles flx_Material_Issue_CC.AfterDataRefresh
        generate_tree()
    End Sub
    Private Sub generate_tree()

        If flx_Material_Issue_CC.Rows.Count > 1 Then
            Dim strSort As String = flx_Material_Issue_CC.Cols(1).Name + ", " + flx_Material_Issue_CC.Cols(2).Name + ", " + flx_Material_Issue_CC.Cols(3).Name
            Dim dt As DataTable = CType(flx_Material_Issue_CC.DataSource, DataTable)
            If Not dt Is Nothing Then
                dt.DefaultView.Sort = strSort
            End If

            flx_Material_Issue_CC.Tree.Style = TreeStyleFlags.CompleteLeaf
            flx_Material_Issue_CC.Tree.Column = 2
            flx_Material_Issue_CC.AllowMerging = AllowMergingEnum.None

            Dim totalOn As Integer = flx_Material_Issue_CC.Cols("ISSUED_QTY").SafeIndex
            flx_Material_Issue_CC.Subtotal(AggregateEnum.Max, 0, 3, totalOn)
            totalOn = flx_Material_Issue_CC.Cols("ACCEPTED_QTY").SafeIndex
            flx_Material_Issue_CC.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)
            totalOn = flx_Material_Issue_CC.Cols("RETURNED_QTY").SafeIndex
            flx_Material_Issue_CC.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)
            SetGridSettingValues()

        End If


        Dim cs As C1.Win.C1FlexGrid.CellStyle

        cs = Me.flx_Material_Issue_CC.Styles.Add("RETURNED_QTY")
        cs.ForeColor = Color.White
        cs.BackColor = Color.Green
        cs.Border.Style = BorderStyleEnum.Raised

        Dim i As Integer
        For i = 1 To flx_Material_Issue_CC.Rows.Count - 1
            If Not flx_Material_Issue_CC.Rows(i).IsNode Then flx_Material_Issue_CC.SetCellStyle(i, 10, cs)
        Next



    End Sub


End Class
