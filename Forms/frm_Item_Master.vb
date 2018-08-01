Imports System.Data.SqlClient
Imports MMSPlus.item_detail

Public Class frm_Item_Master
    Implements IForm

    Dim Obj As New item_detail.cls_item_detail
    Dim prpty As item_detail.cls_item_detail_prop
    Dim Flag As String
    Dim ItemId As Integer
    Dim ds As New DataSet
    Dim Qry As String
    Dim _rights As Form_Rights

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_Item_Master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtSearch.Focus()
        Try
            Call ComboBind()
            Flag = "save"

            Call Obj.FormatGrid(grdItemMaster)
            FillGrid()
            new_initialisation()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Form Load")
        End Try
    End Sub

    Private Sub new_initialisation()
        Flag = "save"
        txtItemCat.Text = ""
        txtItemCode.Text = ""
        txtItemName.Text = ""
        txt_UMID.Text = ""
        lblItemDesc.Text = ""
        lblIssueUnit.Text = ""
        lbl_Recipeunit.Text = ""
        lblConvIssue.Text = ""
        lblConvReciepe.Text = ""
        lblAverageRate.Text = ""
        txtReorderLevel.Text = ""
        txtReorderQty.Text = ""
        txtTransferRate.Text = ""
        chkIsStockable.Checked = False
        chkIsActive.Checked = False
        txtOpeningStock.Text = ""
        txtCurrentStock.Text = ""
        txtOpeningStock.ReadOnly = False
        txtSearch.Focus()

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick
        'MsgBox("Delete Click")
    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        new_initialisation()
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Try

            If _rights.allow_trans = "N" Then
                RightsMsg()
                Exit Sub
            End If

            If txtOpeningStock.Text <> "" And txt_OpeningRate.Text = "" Then
                MsgBox("Please enter opening rate of opening stock.")
                Exit Sub
            End If

            If Flag = "save" Then
                prpty = New cls_item_detail_prop
                prpty.ITEM_ID = Convert.ToInt32(ItemId).ToString()
                prpty.DIV_ID = v_the_current_division_id
                prpty.RE_ORDER_LEVEL = Val(txtReorderLevel.Text)
                prpty.RE_ORDER_QTY = Val(txtReorderQty.Text)
                'prpty.PURCHASE_VAT_ID = Val(cmdPurchaseVat.SelectedValue)
                prpty.PURCHASE_VAT_ID = Convert.ToDecimal(cmbSaleVat.SelectedValue)
                prpty.SALE_VAT_ID = cmbSaleVat.SelectedValue
                prpty.OPENING_STOCK = Val(txtOpeningStock.Text)
                prpty.CURRENT_STOCK = Val(txtCurrentStock.Text)
                prpty.TRANSFER_RATE = Val(txtTransferRate.Text)
                prpty.OPENING_RATE = Val(txt_OpeningRate.Text)
                prpty.IS_STOCKABLE = chkIsStockable.Checked
                prpty.IS_active = True
                'prpty.AVERAGE_RATE = Val(txtPurchaseRate.Text)
                prpty.BATCH_NO = txtBatchNo.Text
                prpty.EXPIRY_DATE = IIf(dtpExpiryOpening.Checked, dtpExpiryOpening.Value, Now.AddYears(1))
                Obj.insert_ITEM_DETAIL(prpty)
                MsgBox("Record Saved", MsgBoxStyle.Information, gblMessageHeading)
            Else
                prpty = New cls_item_detail_prop
                prpty.ITEM_ID = Convert.ToInt32(ItemId).ToString()
                prpty.DIV_ID = v_the_current_division_id
                prpty.RE_ORDER_LEVEL = Val(txtReorderLevel.Text)
                prpty.RE_ORDER_QTY = Val(txtReorderQty.Text)
                prpty.PURCHASE_VAT_ID = Convert.ToDecimal(cmbSaleVat.SelectedValue)
                prpty.SALE_VAT_ID = Convert.ToDecimal(cmbSaleVat.SelectedValue)
                prpty.OPENING_STOCK = Val(txtOpeningStock.Text)
                prpty.CURRENT_STOCK = Val(txtCurrentStock.Text)
                prpty.TRANSFER_RATE = Val(txtTransferRate.Text)
                prpty.OPENING_RATE = Val(txt_OpeningRate.Text)
                prpty.IS_STOCKABLE = chkIsStockable.Checked
                prpty.IS_active = chkIsActive.Checked
                prpty.BATCH_NO = txtBatchNo.Text
                prpty.EXPIRY_DATE = IIf(dtpExpiryOpening.Checked, dtpExpiryOpening.Value, Now.AddYears(1))
                Obj.update_ITEM_DETAIL(prpty)
                MsgBox("Record Updated", MsgBoxStyle.Information, gblMessageHeading)
                'MsgBox("Item is already assigned to this division.")
                Flag = "save"
            End If
            TabControl1.SelectTab(0)
            new_initialisation()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_item_master")
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Private Sub FillGrid()
        Try
            'Call Obj.GridBind(grdItemMaster, "SELECT ITEM_MASTER.ITEM_ID,ITEM_MASTER.ITEM_CODE," _
            '    & " ITEM_MASTER.ITEM_NAME,UNIT_MASTER.UM_Name,ITEM_CATEGORY.ITEM_CAT_NAME, Barcode_Vch ,item_master.ITEM_DESC As Brand FROM ITEM_MASTER " _
            '    & " INNER JOIN  UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID INNER JOIN ITEM_CATEGORY " _
            '    & " ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID order by Item_Master.Item_Code")

            Call Obj.GridBind(grdItemMaster, " SELECT    ITEM_MASTER.ITEM_ID ,
            ITEM_MASTER.ITEM_CODE ,
            Barcode_Vch ,
            ITEM_MASTER.ITEM_NAME ,
            UNIT_MASTER.UM_Name ,
            ITEM_CATEGORY.ITEM_CAT_NAME ,       
            ISNULL(litems.LabelItemName_vch, '') AS Brand,
            CASE WHEN ISNULL(Item_Detail.IS_ACTIVE,0) = 1 THEN 'Active'
            ELSE 'InActive' End AS ItemStatus
  FROM      ITEM_MASTER
			LEFT OUTER JOIN Item_Detail ON ITEM_MASTER.item_id = Item_Detail.item_id
            INNER JOIN UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID
            INNER JOIN ITEM_CATEGORY ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID
            LEFT OUTER JOIN dbo.LabelItem_Mapping lim ON lim.Fk_ItemId_Num = ITEM_MASTER.ITEM_ID
            LEFT OUTER JOIN dbo.Label_Items litems ON lim.Fk_LabelDetailId = litems.Pk_LabelDetailId_Num
  ORDER BY  Item_Master.Item_Code")

            grdItemMaster.Columns(0).Visible = False 'Item Master id
            grdItemMaster.Columns(0).HeaderText = "Item ID"
            grdItemMaster.Columns(0).Width = 0
            grdItemMaster.Columns(1).HeaderText = "Item Code"
            grdItemMaster.Columns(1).Width = 100
            grdItemMaster.Columns(2).HeaderText = "Barcode"
            grdItemMaster.Columns(2).Width = 100
            grdItemMaster.Columns(3).HeaderText = "Item Name"
            grdItemMaster.Columns(3).Width = 250
            grdItemMaster.Columns(4).HeaderText = "Item Unit"
            grdItemMaster.Columns(4).Width = 100
            grdItemMaster.Columns(5).HeaderText = "Item Category Name"
            grdItemMaster.Columns(5).Width = 130
            grdItemMaster.Columns(6).HeaderText = "Brand"
            grdItemMaster.Columns(6).Width = 80
            grdItemMaster.Columns(7).HeaderText = "Status"
            grdItemMaster.Columns(7).Width = 60

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
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

            'Call Obj.GridBind(grdItemMaster, "SELECT ITEM_MASTER.ITEM_ID,ITEM_MASTER.ITEM_CODE," _
            '  & " ITEM_MASTER.ITEM_NAME,UNIT_MASTER.UM_Name,ITEM_CATEGORY.ITEM_CAT_NAME, Barcode_vch,item_master.ITEM_DESC As Brand FROM ITEM_MASTER " _
            '  & " INNER JOIN  UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID INNER JOIN ITEM_CATEGORY " _
            '  & " ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID where (item_master.item_code + " _
            '& "item_master.ITEM_DESC+ item_master.item_name + ITEM_CATEGORY.item_cat_name + UNIT_MASTER.um_name + isnull(Barcode_Vch,'')) " _
            '& " like '%" & txtSearch.Text & "%'")

            Call Obj.GridBind(grdItemMaster, "SELECT ITEM_MASTER.ITEM_ID ,
                                                ITEM_MASTER.ITEM_CODE ,
                                                ITEM_MASTER.Barcode_Vch ,
                                                ITEM_MASTER.ITEM_NAME ,
                                                UNIT_MASTER.UM_Name ,
                                                ITEM_CATEGORY.ITEM_CAT_NAME ,       
                                                ISNULL(litems.LabelItemName_vch, '') AS Brand,
                                                CASE WHEN ISNULL(Item_Detail.IS_ACTIVE,0) = 1 THEN 'Active'
                                                ELSE 'InActive' End AS ItemStatus
                                                FROM  ITEM_MASTER
			                                    LEFT OUTER JOIN Item_Detail ON ITEM_MASTER.item_id = Item_Detail.item_id
                                                INNER JOIN UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID
                                                INNER JOIN ITEM_CATEGORY ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID
                                                LEFT OUTER JOIN dbo.LabelItem_Mapping lim ON lim.Fk_ItemId_Num = ITEM_MASTER.ITEM_ID
                                                LEFT OUTER JOIN dbo.Label_Items litems ON lim.Fk_LabelDetailId = litems.Pk_LabelDetailId_Num where (item_master.item_code + isnull(ITEM_MASTER.Barcode_Vch,'') + " _
            & " item_master.item_name + UNIT_MASTER.um_name + ITEM_CATEGORY.item_cat_name + isnull(litems.LabelItemName_vch,'')) " _
            & " like '%" & txtSearch.Text & "%'")


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
        End Try
    End Sub

    Private Sub geItemDetail(ByVal ItemID As Integer)
        Try

            'Qry = "select UNIT_MASTER.UM_ID,UNIT_MASTER.UM_Name,ITEM_CATEGORY.ITEM_CAT_ID," _
            '    & " ITEM_CATEGORY.ITEM_CAT_NAME,ITEM_MASTER.* from ITEM_MASTER inner join " _
            '    & " UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID inner join  " _
            '    & " dbo.ITEM_CATEGORY ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID " _
            '    & " where ITEM_ID =" & Convert.ToString(ItemID)
            Qry = "SELECT UNIT_MASTER.UM_ID, UNIT_MASTER.UM_Name, ITEM_CATEGORY.ITEM_CAT_ID, ITEM_CATEGORY.ITEM_CAT_NAME, ITEM_MASTER.ITEM_ID, " _
                    & " ITEM_MASTER.ITEM_CODE, ITEM_MASTER.ITEM_NAME, ITEM_MASTER.ITEM_DESC, ITEM_MASTER.UM_ID AS Expr1,  " _
                    & "  ITEM_MASTER.ISSUE_UM_ID, ITEM_MASTER.RECP_UM_ID, ITEM_MASTER.ITEM_CATEGORY_ID, " _
                    & " ITEM_MASTER.CONV_FAC_ISSUE, ITEM_MASTER.CONV_FAC_RECP, ITEM_MASTER.RE_ORDER_LEVEL, ITEM_MASTER.RE_ORDER_QTY, " _
                    & " ITEM_MASTER.TRANSFER_RATE, ITEM_MASTER.SALE_RATE, ITEM_MASTER.PURCHASE_RATE, ITEM_MASTER.VAT_ID, ITEM_MASTER.EXCISE_ID, " _
                    & " ITEM_MASTER.CREATED_BY, ITEM_MASTER.CREATION_DATE, ITEM_MASTER.MODIFIED_BY, ITEM_MASTER.MODIFIED_DATE, " _
                    & " ITEM_MASTER.DIVISION_ID, ITEM_MASTER.IS_STOCKABLE, UNIT_MASTER_1.UM_Name AS ISSUE_UOM, " _
                    & " UNIT_MASTER_2.UM_Name AS Recp_UM,MRP_Num AS MRP,sale_rate AS SALERATE,HsnCode_vch AS HSNCODE, BarCode_vch AS BARCODE,ISNULL(LabelItemName_vch,'N/A')AS BrandName" _
                    & " FROM         ITEM_MASTER INNER JOIN" _
                    & " UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID INNER JOIN" _
                    & " ITEM_CATEGORY ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID INNER JOIN" _
                    & " UNIT_MASTER AS UNIT_MASTER_1 ON ITEM_MASTER.ISSUE_UM_ID = UNIT_MASTER_1.UM_ID INNER JOIN" _
                    & " UNIT_MASTER AS UNIT_MASTER_2 ON ITEM_MASTER.RECP_UM_ID = UNIT_MASTER_2.UM_ID " _
                    & " INNER JOIN dbo.HsnCode_Master ON dbo.HsnCode_Master.Pk_HsnId_num=dbo.ITEM_MASTER.fk_HsnId_num " _
                    & " LEFT JOIN dbo.LabelItem_Mapping ON dbo.LabelItem_Mapping.Fk_ItemId_Num=dbo.ITEM_MASTER.ITEM_ID " _
                    & " LEFT JOIN  dbo.Label_Items ON dbo.Label_Items.Pk_LabelDetailId_Num=LabelItem_Mapping.Fk_LabelDetailId " _
                    & " where ITEM_ID =" & Convert.ToString(ItemID)
            ds = Obj.Fill_DataSet(Qry)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim drv As DataRowView
                drv = ds.Tables(0).DefaultView(0)
                txtItemCat.Text = drv("ITEM_CAT_NAME").ToString()
                txtItemCode.Text = drv("ITEM_CODE").ToString()
                txtItemName.Text = drv("ITEM_NAME").ToString()
                txt_UMID.Text = drv("UM_NAME").ToString()
                lblItemDesc.Text = drv("ITEM_DESC").ToString()
                lblIssueUnit.Text = drv("ISSUE_UOM").ToString()
                lbl_Recipeunit.Text = drv("Recp_UM").ToString()
                lblConvIssue.Text = drv("CONV_FAC_ISSUE").ToString()
                lblConvReciepe.Text = drv("CONV_FAC_RECP").ToString()
                txtReorderLevel.Text = drv("RE_ORDER_LEVEL").ToString()
                txtReorderQty.Text = drv("RE_ORDER_QTY").ToString()
                txtTransferRate.Text = drv("TRANSFER_RATE").ToString()
                cmbSaleVat.SelectedValue = drv("VAT_ID").ToString()
                chkIsStockable.Checked = Convert.ToBoolean(drv("IS_STOCKABLE"))
                chkIsActive.Checked = True
                txtCurrentStock.Text = "0.000"

                lblmrp.Text = drv("MRP").ToString()
                lblSalerate.Text = drv("SALERATE").ToString()
                lblhsn.Text = drv("HSNCODE").ToString()
                lblbarcode.Text = drv("BARCODE").ToString()
                lblbrandname.Text = drv("BrandName").ToString()

                Flag = "update"
            Else
                '''''''''''''''''''''''''''new_initilization()
                MsgBox("No Records Found", MsgBoxStyle.Exclamation, "Item Master")
            End If

            ds.Tables(0).Rows.Clear()
            Qry = "SELECT  ITEM_DETAIL.RE_ORDER_LEVEL," &
                    "ITEM_DETAIL.RE_ORDER_QTY," &
                    "ITEM_DETAIL.OPENING_STOCK," &
                    "ITEM_DETAIL.IS_EXTERNAL," &
                    "ITEM_DETAIL.TRANSFER_RATE," &
                    "ITEM_DETAIL.AVERAGE_RATE," &
                    "ITEM_DETAIL.OPENING_RATE," &
                    "ITEM_DETAIL.PURCHASE_VAT_ID," &
                    "ITEM_DETAIL.IS_ACTIVE," &
                    "ITEM_DETAIL.IS_STOCKABLE," &
                    "STOCK_DETAIL.Batch_no," &
                    "STOCK_DETAIL.Expiry_date," &
                    "STOCK_DETAIL.Item_Qty," &
                    "STOCK_DETAIL.Issue_Qty," &
                    "STOCK_DETAIL.Balance_Qty " &
                    "FROM STOCK_DETAIL " &
                    "INNER JOIN ITEM_DETAIL ON STOCK_DETAIL.STOCK_DETAIL_ID = ITEM_DETAIL.STOCK_DETAIL_ID  where ITEM_DETAIL.item_id=" & Convert.ToString(ItemID)
            ds = Obj.Fill_DataSet(Qry)
            If ds.Tables(0).Rows.Count > 0 Then

                Dim drv As DataRowView
                drv = ds.Tables(0).DefaultView(0)
                txtOpeningStock.Text = drv("OPENING_STOCK").ToString()
                'If Convert.ToDouble(drv("Issue_Qty")) <> 0 Then
                '    txtOpeningStock.ReadOnly = True
                '    txt_OpeningRate.ReadOnly = True
                'Else
                '    txtOpeningStock.ReadOnly = False
                '    txt_OpeningRate.ReadOnly = False
                'End If
                txtBatchNo.Text = drv("Batch_no").ToString()
                dtpExpiryOpening.Checked = True
                dtpExpiryOpening.Value = drv("Expiry_date").ToString()
                Dim ds_new As DataSet

                ds_new = Obj.fill_Data_set_val("get_count_Item_Issued", "@Item_ID", "", Convert.ToString(ItemID), "")
                If (ds_new.Tables.Count > 0) Then
                    If (Convert.ToString(ds_new.Tables(0).Rows(0)(0)) = "0") Then
                        txt_OpeningRate.ReadOnly = False
                        txtOpeningStock.ReadOnly = False
                        txt_OpeningRate.Text = drv("OPENING_RATE").ToString()
                    Else
                        txt_OpeningRate.ReadOnly = True
                        txtOpeningStock.ReadOnly = True
                        txt_OpeningRate.Text = drv("OPENING_RATE").ToString()
                    End If
                End If

                txtReorderLevel.Text = drv("RE_ORDER_LEVEL").ToString()
                txtReorderQty.Text = drv("RE_ORDER_QTY").ToString()
                txtTransferRate.Text = drv("TRANSFER_RATE").ToString()
                cmbSaleVat.SelectedValue = drv("PURCHASE_VAT_ID").ToString()
                chkIsStockable.Checked = Convert.ToBoolean(drv("IS_STOCKABLE"))
                chkIsActive.Checked = Convert.ToBoolean(drv("IS_ACTIVE"))
                Flag = "update"
            Else
                txt_OpeningRate.Text = ""
                Flag = "save"
            End If
            ds.Dispose()
            'txtSearch.Text = ""
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> ")
        End Try
    End Sub

    Private Sub ComboBind()
        Call Obj.ComboBind(cmbSaleVat, "SELECT vat_id,vat_name from vat_master", "vat_name", "vat_id")
        'Call Obj.ComboBind(cmdPurchaseVat, "SELECT vat_id,vat_name from vat_master", "vat_name", "vat_id")
    End Sub

    Private Sub txtTransferRate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTransferRate.Leave
        txtTransferRate.Text = txtTransferRate.Text.Trim
    End Sub

    Private Sub txttxtTransferRate(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTransferRate.KeyPress, txtOpeningStock.KeyPress, txtReorderLevel.KeyPress, txtReorderQty.KeyPress, txtBatchNo.KeyPress
        If Obj.Valid_Number(Asc(e.KeyChar), sender) = False Then
            e.Handled = True
        End If
    End Sub

    Private Sub grdItemMaster_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdItemMaster.DoubleClick
        EditItem()
    End Sub

    Private Sub EditItem()
        If _rights.allow_edit = "N" Then
            RightsMsg()
            Exit Sub
        End If
        If grdItemMaster.SelectedRows.Count > 0 Then
            new_initialisation()
            ItemId = grdItemMaster.SelectedRows(0).Cells("ITEM_ID").Value
            Call geItemDetail(ItemId)
            TabControl1.SelectTab(1)
        End If
    End Sub

    Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabControl1.DrawItem
        Dim g As Graphics = e.Graphics
        Dim tp As TabPage = TabControl1.TabPages(e.Index)
        Dim br As Brush
        Dim sf As New StringFormat

        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2)

        sf.Alignment = StringAlignment.Center

        Dim strTitle As String = tp.Text

        'If the current index is the Selected Index, change the color 
        If TabControl1.SelectedIndex = e.Index Then

            'this is the background color of the tabpage header
            br = New SolidBrush(Color.Lime) ' chnge to your choice
            g.FillRectangle(br, e.Bounds)

            'this is the foreground color of the text in the tab header
            br = New SolidBrush(Color.Black) ' change to your choice
            g.DrawString(strTitle, TabControl1.Font, br, r, sf)

        Else

            'these are the colors for the unselected tab pages 
            br = New SolidBrush(Color.Silver) ' Change this to your preference
            g.FillRectangle(br, e.Bounds)
            br = New SolidBrush(Color.Black)
            g.DrawString(strTitle, TabControl1.Font, br, r, sf)

        End If
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            EditItem()
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 0 Then
            txtSearch.Focus()
        End If
    End Sub

    Private Sub lnkSelectItems_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkSelectItems.LinkClicked


        frm_Show_search.qry = " SELECT  TOP 100

                                        im.ITEM_ID ,
                                        ISNULL(im.BarCode_vch, '') AS BARCODE ,
                                        im.ITEM_NAME AS [ITEM NAME] ,
                                        im.MRP_Num AS MRP ,
                                        im.sale_rate AS RATE,
                                        --CAST(im.sale_rate AS NUMERIC(18, 2)) AS RATE ,
                                        ISNULL(litems.LabelItemName_vch,'') AS BRAND ,
                                        ic.ITEM_CAT_NAME AS CATEGORY
                                        FROM    Item_master im
                                        LEFT outer JOIN item_detail id ON im.item_id = id.item_id
                                        LEFT outer JOIN dbo.ITEM_CATEGORY ic ON im.ITEM_CATEGORY_ID = ic.ITEM_CAT_ID
                                        LEFT OUTER JOIN dbo.LabelItem_Mapping lim ON lim.Fk_ItemId_Num = im.ITEM_ID
                                        LEFT OUTER JOIN dbo.Label_Items litems ON lim.Fk_LabelDetailId = litems.Pk_LabelDetailId_Num 
                                where 1=1"

        frm_Show_search.column_name = "BARCODE_VCH"
        frm_Show_search.column_name1 = "ITEM_NAME"
        frm_Show_search.column_name2 = "MRP_Num"
        frm_Show_search.column_name3 = "SALE_RATE"
        frm_Show_search.column_name4 = "LABELITEMNAME_VCH"
        frm_Show_search.column_name5 = "ITEM_CAT_NAME"
        frm_Show_search.cols_no_for_width = "1,2,3,4,5,6"
        frm_Show_search.cols_width = "100,360,60,60,100,105"
        frm_Show_search.extra_condition = ""
        frm_Show_search.ret_column = "ITEM_ID"
        frm_Show_search.item_rate_column = ""
        frm_Show_search.ShowDialog()

        get_row(frm_Show_search.search_result)
        frm_Show_search.Close()

    End Sub

    Public Sub get_row(ByVal item_id As String)
        If item_id <> -1 Then
            new_initialisation()
            ItemId = Convert.ToInt32(item_id)
            Call geItemDetail(ItemId)
            TabControl1.SelectTab(1)
        End If
    End Sub

    Private Sub grdItemMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles grdItemMaster.KeyDown
        If e.KeyCode = Keys.Enter Then
            EditItem()
        End If
    End Sub


    'Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabControl1.DrawItem

    '    'Firstly we'll define some parameters.
    '    Dim CurrentTab As TabPage = TabControl1.TabPages(e.Index)
    '    Dim ItemRect As Rectangle = TabControl1.GetTabRect(e.Index)
    '    Dim FillBrush As New SolidBrush(Color.Red)
    '    Dim TextBrush As New SolidBrush(Color.White)
    '    Dim sf As New StringFormat
    '    sf.Alignment = StringAlignment.Center
    '    sf.LineAlignment = StringAlignment.Center

    '    'If we are currently painting the Selected TabItem we'll 
    '    'change the brush colors and inflate the rectangle.
    '    If CBool(e.State And DrawItemState.Selected) Then
    '        FillBrush.Color = Color.White
    '        TextBrush.Color = Color.Red
    '        ItemRect.Inflate(2, 2)
    '    End If

    '    'Set up rotation for left and right aligned tabs
    '    If TabControl1.Alignment = TabAlignment.Left Or TabControl1.Alignment = TabAlignment.Right Then
    '        Dim RotateAngle As Single = 90
    '        If TabControl1.Alignment = TabAlignment.Left Then RotateAngle = 270
    '        Dim cp As New PointF(ItemRect.Left + (ItemRect.Width \ 2), ItemRect.Top + (ItemRect.Height \ 2))
    '        e.Graphics.TranslateTransform(cp.X, cp.Y)
    '        e.Graphics.RotateTransform(RotateAngle)
    '        ItemRect = New Rectangle(-(ItemRect.Height \ 2), -(ItemRect.Width \ 2), ItemRect.Height, ItemRect.Width)
    '    End If

    '    'Next we'll paint the TabItem with our Fill Brush
    '    e.Graphics.FillRectangle(FillBrush, ItemRect)

    '    'Now draw the text.
    '    e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, RectangleF.op_Implicit(ItemRect), sf)

    '    'Reset any Graphics rotation
    '    e.Graphics.ResetTransform()

    '    'Finally, we should Dispose of our brushes.
    '    FillBrush.Dispose()
    '    TextBrush.Dispose()

    'End Sub

End Class