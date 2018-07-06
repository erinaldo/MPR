Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections

Public Class frm_define_recipe
    Implements IForm
    Dim flag As String
    Dim obj As New CommonClass
    Dim objrecipe As New Recipe_Master.Cls_Recipe_Master
    Dim objrecipeprop As New Recipe_Master.cls_Recipe_Master_Prop

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
        new_initialization()
        tbcrecipe.SelectTab(1)
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        new_initialization()
        tbcrecipe.SelectTab(1)
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Try
            DGVmenuitems.EndEdit()

            If cmbmenuheads.SelectedIndex > 0 Then

                If cmbmenuitems.SelectedIndex > 0 Then
                    If DGVrecipedetail.Rows.Count - 1 > 0 Then
                        For index As Integer = 0 To DGVrecipedetail.Rows.Count - 1
                            If DGVrecipedetail.Rows(index).Cells("Item_id").Value <> Nothing Then
                                If IsNumeric(DGVrecipedetail.Rows(index).Cells("Quantity").Value) And IsNumeric(DGVrecipedetail.Rows(index).Cells("yield_qty").Value) Then
                                    If Convert.ToDouble(DGVrecipedetail.Rows(index).Cells("Quantity").Value) <= 0 Or Convert.ToDouble(DGVrecipedetail.Rows(index).Cells("yield_qty").Value) < 0 Then
                                        MsgBox("Quantity cannot be zero and yield quantity cannot be negative.", MsgBoxStyle.Information)
                                        Exit Sub
                                    End If
                                Else
                                    MsgBox("Quantity should be numeric.", MsgBoxStyle.Information)
                                    Exit Sub
                                End If
                            End If
                        Next

                        objrecipeprop.Menu_id = cmbmenuitems.SelectedValue()
                        objrecipe.Delete_Recipe_Master(objrecipeprop)

                        For irow As Integer = 0 To DGVrecipedetail.Rows.Count - 1
                            If IsNumeric(DGVrecipedetail.Rows(irow).Cells("yield_qty").Value) And IsNumeric(DGVrecipedetail.Rows(irow).Cells("yield_qty").Value) Then
                                If Convert.ToDouble(DGVrecipedetail.Rows(irow).Cells("Quantity").Value) > 0 And Convert.ToDouble(DGVrecipedetail.Rows(irow).Cells("yield_qty").Value) >= 0 Then

                                    objrecipeprop.Item_id = Convert.ToInt32(DGVrecipedetail.Rows(irow).Cells("Item_id").Value)
                                    objrecipeprop.Item_Uom = Convert.ToInt32(DGVrecipedetail.Rows(irow).Cells("Um_id").Value)
                                    objrecipeprop.Item_Qty = Convert.ToDouble(DGVrecipedetail.Rows(irow).Cells("Quantity").Value)
                                    objrecipeprop.Item_Yield_Qty = Convert.ToDouble(DGVrecipedetail.Rows(irow).Cells("Yield_qty").Value)
                                    objrecipeprop.Creation_Date = Now()
                                    objrecipeprop.Created_By = v_the_current_logged_in_user_name
                                    objrecipeprop.Modification_Date = Now()
                                    objrecipeprop.Modified_By = v_the_current_logged_in_user_name

                                    objrecipe.insert_Recipe_Master(objrecipeprop)
                                End If
                            End If
                        Next

                        objrecipe.Delete_SemiFinisheditemsinRecipe_Mapping(objrecipeprop)

                        For rowcount As Integer = 0 To DGVSemiFinisheditems.Rows.Count - 1
                            If IsNumeric(DGVSemiFinisheditems.Rows(rowcount).Cells("Item_id").Value) Then
                                objrecipeprop.SemifinishedRecipe_id = Convert.ToInt32(DGVSemiFinisheditems.Rows(rowcount).Cells("Item_id").Value)
                                objrecipeprop.SemifinishedRecipe_qty = Convert.ToDouble(DGVSemiFinisheditems.Rows(rowcount).Cells("Item_qty").Value)
                                objrecipeprop.Creation_Date = Now()
                                objrecipeprop.Created_By = v_the_current_logged_in_user_name
                                objrecipeprop.Modification_Date = Now()
                                objrecipeprop.Modified_By = v_the_current_logged_in_user_name
                                objrecipe.insert_SemiFinishedItemsinRecipe_maping(objrecipeprop)
                            End If
                        Next

                        MsgBox("Recipe Saved Successfully", MsgBoxStyle.Information, gblMessageHeading)
                        new_initialization()
                        cmbmenuheads.Enabled = True
                        cmbmenuitems.Enabled = True
                        cmb_OutletName.Enabled = True
                    Else
                        MsgBox("Please insert ingredients.", MsgBoxStyle.Information)
                    End If
                Else
                    MsgBox("Please select Menu Item.", MsgBoxStyle.Information)
                End If
            Else
                MsgBox("Please select Menu Head.", MsgBoxStyle.Information)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

    Public Sub FillGridMaster()
        Try
            obj.Bind_GridBind_Val(DGVmenuitems, "Get_Recipe_Menu_items", "@txtsearch", "", "%" & txtSearchMenuItem.Text & "%", "")
            DGVmenuitems.Columns(0).Visible = False
            DGVmenuitems.Columns(0).HeaderText = "MenuItem Id"
            DGVmenuitems.Columns(0).ReadOnly = True
            DGVmenuitems.Columns(0).Width = 120
            DGVmenuitems.Columns(1).HeaderText = "Menu Item Name"
            DGVmenuitems.Columns(1).Width = 640
            DGVmenuitems.Columns(1).ReadOnly = True
            DGVmenuitems.Columns(2).HeaderText = "Status"
            DGVmenuitems.Columns(2).Width = 200
            DGVmenuitems.Columns(2).ReadOnly = True
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub clear_all()
        Try
            DGVrecipedetail.Rows.Clear()
            DGVSemiFinisheditems.Rows.Clear()
            txtSemiItemQty.Text = ""
            lblUnit.Text = "Unit"
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub new_initialization()
        Try
            clear_all()
            FillGridMaster()
            tbcrecipe.SelectTab(0)
            cmbmenuheads.Enabled = True
            cmbmenuitems.Enabled = True
            cmb_OutletName.Enabled = True
            obj.ComboBind(cmbmenuheads, "SELECT pk_CategoryID_num,CategoryName_vch FROM  dbo.CategoryMaster order by CategoryName_vch", "CategoryName_vch", "pk_CategoryID_num", True)
            obj.ComboBind(cmbSemiFinishedItems, "SELECT semifinisheditem_id, semifinisheditem_name FROM semifinished_recipe_master order by semifinisheditem_name", "semifinisheditem_name", "semifinisheditem_id", True)
            obj.ComboBind(cmb_OutletName, "SELECT Pk_outletid_num, OutletName_vch FROM outletmaster ORDER BY outletName_vch", "OutletName_vch", "Pk_outletid_num", True)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub DGVrecipedetail_Style()
        Try
            DGVrecipedetail.Columns.Clear()
            DGVrecipedetail.Rows.Clear()
            Dim txtItemId = New DataGridViewTextBoxColumn
            Dim txtItemCode = New DataGridViewTextBoxColumn
            Dim txtItemName = New DataGridViewTextBoxColumn
            Dim txtItemUom = New DataGridViewTextBoxColumn
            Dim txtItemuomId = New DataGridViewTextBoxColumn
            Dim txtItemQuantity = New DataGridViewTextBoxColumn
            Dim txtyieldqty = New DataGridViewTextBoxColumn

            With txtItemId
                .HeaderText = "Item Id"
                .Name = "Item_ID"
                .ReadOnly = True
                .Visible = False
                .Width = 10
            End With
            DGVrecipedetail.Columns.Add(txtItemId)

            With txtItemCode
                .HeaderText = "Item Code"
                .Name = "Item_CODE"
                .ReadOnly = True
                .Visible = True
                .Width = 120
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            DGVrecipedetail.Columns.Add(txtItemCode)

            With txtItemName
                .HeaderText = "Item Name"
                .Name = "Item_Name"
                .ReadOnly = True
                .Visible = True
                .Width = 380
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            DGVrecipedetail.Columns.Add(txtItemName)

            With txtItemUom
                .HeaderText = "UOM"
                .Name = "UM_Name"
                .ReadOnly = True
                .Visible = True
                .Width = 100
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            DGVrecipedetail.Columns.Add(txtItemUom)

            With txtItemuomId
                .HeaderText = "UOM Id"
                .Name = "UM_id"
                .ReadOnly = True
                .Visible = False
                .Width = 10
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            DGVrecipedetail.Columns.Add(txtItemuomId)

            With txtItemQuantity
                .HeaderText = "Item Qty"
                .Name = "Quantity"
                .ReadOnly = False
                .Visible = True
                .Width = 120
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            End With
            DGVrecipedetail.Columns.Add(txtItemQuantity)

            With txtyieldqty
                .HeaderText = "Yield Qty"
                .Name = "yield_qty"
                .ReadOnly = False
                .Visible = True
                .Width = 120
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            End With
            DGVrecipedetail.Columns.Add(txtyieldqty)

            Dim CellLength As DataGridViewTextBoxColumn = TryCast(Me.DGVrecipedetail.Columns("Quantity"), DataGridViewTextBoxColumn)
            CellLength.MaxInputLength = 10
            DGVrecipedetail.AllowUserToResizeColumns = False
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub DGVSemiFinisheditems_style()
        Try
            DGVSemiFinisheditems.Columns.Clear()
            DGVSemiFinisheditems.Rows.Clear()

            Dim txtItemid = New DataGridViewTextBoxColumn

            Dim txtItemname = New DataGridViewTextBoxColumn
            Dim txtuom = New DataGridViewTextBoxColumn
            Dim txtqty = New DataGridViewTextBoxColumn
            Dim btnremove = New DataGridViewButtonColumn

            With txtItemid
                .HeaderText = "Item Id"
                .Name = "Item_id"
                .ReadOnly = True
                .Visible = False
                .Width = 20
            End With
            DGVSemiFinisheditems.Columns.Add(txtItemid)

            With txtItemname
                .HeaderText = "Item Name"
                .Name = "Item_name"
                .ReadOnly = True
                .Visible = True
                .Width = 450
            End With
            DGVSemiFinisheditems.Columns.Add(txtItemname)

            With txtuom
                .HeaderText = "Item UOM"
                .Name = "Item_uom"
                .ReadOnly = True
                .Visible = True
                .Width = 160
            End With
            DGVSemiFinisheditems.Columns.Add(txtuom)

            With txtqty
                .HeaderText = "Item Qty"
                .Name = "Item_qty"
                .ReadOnly = True
                .Visible = True
                .Width = 170
            End With
            DGVSemiFinisheditems.Columns.Add(txtqty)

            With btnremove
                .Headertext = ""
                .Name = "Remove"
                .visible = True
                .Text = "Remove"
                .usecolumntextforbuttonvalue = True
                .width = 60
                .readonly = False
                .HeaderCell.Value = ""
                .HeaderText = ""
            End With
            DGVSemiFinisheditems.Columns.Add(btnremove)

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub get_row(ByVal item_id As String)
        Dim IsInsert As Boolean
        Dim ds As DataSet
        ds = obj.fill_Data_set("GET_ITEM_BY_ID_Recipe", "@V_ITEM_ID", item_id)
        Dim iRowCount As Int32
        Dim iRow As Int32
        iRowCount = DGVrecipedetail.RowCount
        IsInsert = True
        For iRow = 0 To iRowCount - 2
            If DGVrecipedetail.Item(0, iRow).Value = Convert.ToInt32(ds.Tables(0).Rows(0)(0)) Then
                MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, "Recipe Ingredients")
                IsInsert = False
                Exit For
            End If
        Next iRow
        Dim datatbl As New DataTable
        datatbl = ds.Tables(0)

        If IsInsert = True Then
            Dim introw As Integer
            introw = DGVrecipedetail.Rows.Count - 1
            DGVrecipedetail.Rows.Insert(introw)
            DGVrecipedetail.Rows(introw).Cells("Item_ID").Value = ds.Tables(0).Rows(0)(0)
            DGVrecipedetail.Rows(introw).Cells("Item_CODE").Value = ds.Tables(0).Rows(0)("item_Code").ToString()
            DGVrecipedetail.Rows(introw).Cells("Item_Name").Value = ds.Tables(0).Rows(0)("item_Name").ToString()
            DGVrecipedetail.Rows(introw).Cells("UM_Name").Value = ds.Tables(0).Rows(0)("UM_NAME").ToString()
            DGVrecipedetail.Rows(introw).Cells("Um_id").Value = ds.Tables(0).Rows(0)("recp_um_id").ToString()
            DGVrecipedetail.Rows(introw).Cells("Quantity").Value = 0
            DGVrecipedetail.Rows(introw).Cells("Yield_qty").Value = 0
        End If
    End Sub

    Private Sub DGVrecipedetail_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGVrecipedetail.KeyDown
        Try
            Dim iRowindex As Int32
            If e.KeyCode = Keys.Space Then
                If cmbmenuheads.SelectedIndex > 0 Then

                    If cmbmenuitems.SelectedIndex > 0 Then
                        iRowindex = DGVrecipedetail.CurrentRow.Index

                        'frm_Show_search.qry = "Select Item_master.ITEM_ID,Item_master.ITEM_CODE as [Item Code],Item_master.ITEM_NAME  as [Item Name] from Item_master inner join item_detail on item_master.item_id = item_detail.item_id "
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
                    Else
                        MsgBox("Please select Menu Item.", MsgBoxStyle.Information, gblMessageHeading)
                    End If
                
                Else
                    MsgBox("Please select Menu Head.", MsgBoxStyle.Information, gblMessageHeading)
                End If
                End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frm_define_recipe_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tbcrecipe.SelectTab(0)
        'obj.FormatGrid(DGVmenuitems)
        'obj.FormatGrid(DGVrecipedetail)
        'obj.FormatGrid(DGVSemiFinisheditems)

        FillGridMaster()
        DGVrecipedetail_Style()
        DGVSemiFinisheditems_style()
        new_initialization()
    End Sub

    Private Sub cmbmenuheads_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbmenuheads.SelectedIndexChanged
        obj.ComboBind(cmbmenuitems, "SELECT  pk_itemid_num,Itemname_vch FROM dbo.MenuMaster WHERE dbo.MenuMaster.fk_categoryid_num= " & cmbmenuheads.SelectedValue & " order by Itemname_vch", "Itemname_vch", "pk_itemid_num", True)
    End Sub

    Private Sub txtSearchMenuItem_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchMenuItem.TextChanged
        FillGridMaster()
    End Sub

    Private Sub btnAddSemiFinishedItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSemiFinishedItems.Click
        If cmbmenuheads.SelectedIndex = 0 Then
            MsgBox("Please select Menu Head.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If

        If cmbmenuitems.SelectedIndex = 0 Then
            MsgBox("Please select Menu Item.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If

        If cmbSemiFinishedItems.SelectedIndex = 0 Then
            MsgBox("Please select Semi Finished item.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If

        If txtSemiItemQty.Text = "" Then
            MsgBox("Please enter quantity.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If

        'DGVSemiFinisheditems_style()

        Dim ds As New DataSet
        ds = obj.fill_Data_set("Pro_Get_Semifinishedrecipe_detail", "@SemifinishedRecipe_id,@qty", Convert.ToString(cmbSemiFinishedItems.SelectedValue) & "," & txtSemiItemQty.Text)
        Dim flag As String = "True"

        For rowindex As Integer = 0 To DGVSemiFinisheditems.Rows.Count - 1
            If Convert.ToInt32(DGVSemiFinisheditems.Rows(rowindex).Cells("Item_id").Value) = Convert.ToInt32(ds.Tables(0).Rows(0)("semifinisheditem_id")) Then
                DGVSemiFinisheditems.Rows(rowindex).Cells("Item_qty").Value = Convert.ToDouble(DGVSemiFinisheditems.Rows(rowindex).Cells("Item_qty").Value) + Convert.ToDouble(txtSemiItemQty.Text)
                flag = "false"
            End If
        Next

        If flag = "True" Then
            Dim irow As Integer
            irow = DGVSemiFinisheditems.Rows.Count - 1
            DGVSemiFinisheditems.Rows.Insert(irow)
            DGVSemiFinisheditems.Rows(irow).Cells("Item_id").Value = ds.Tables(0).Rows(0)("semifinisheditem_id")
            DGVSemiFinisheditems.Rows(irow).Cells("Item_name").Value = ds.Tables(0).Rows(0)("semifinisheditem_name")
            DGVSemiFinisheditems.Rows(irow).Cells("Item_uom").Value = ds.Tables(0).Rows(0)("recipe_um")
            DGVSemiFinisheditems.Rows(irow).Cells("Item_qty").Value = txtSemiItemQty.Text
        End If
        Dim index = ds.Tables(0).Rows.Count - 1

        For count As Integer = 0 To DGVrecipedetail.Rows.Count - 1
            Dim rowcount As Integer = 0
            While rowcount <= index
                If Convert.ToInt32(DGVrecipedetail.Rows(count).Cells("Item_id").Value) = Convert.ToInt32(ds.Tables(0).Rows(rowcount)("Item_id")) Then
                    DGVrecipedetail.Rows(count).Cells("Quantity").Value = Convert.ToDouble(DGVrecipedetail.Rows(count).Cells("Quantity").Value) + Convert.ToDouble(ds.Tables(0).Rows(rowcount)("item_qty"))
                    DGVrecipedetail.Rows(count).Cells("Yield_qty").Value = Convert.ToDouble(DGVrecipedetail.Rows(count).Cells("Yield_qty").Value) + Convert.ToDouble(ds.Tables(0).Rows(rowcount)("item_yield_qty"))
                    ds.Tables(0).Rows.RemoveAt(rowcount)
                    index = index - 1
                End If
                rowcount = rowcount + 1
            End While
        Next

        Dim introw As Integer
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            introw = DGVrecipedetail.Rows.Count - 1
            DGVrecipedetail.Rows.Insert(introw)
            DGVrecipedetail.Rows(introw).Cells("Item_ID").Value = ds.Tables(0).Rows(i)("Item_id")
            DGVrecipedetail.Rows(introw).Cells("Item_CODE").Value = ds.Tables(0).Rows(i)("item_Code").ToString()
            DGVrecipedetail.Rows(introw).Cells("Item_Name").Value = ds.Tables(0).Rows(i)("item_Name").ToString()
            DGVrecipedetail.Rows(introw).Cells("UM_Name").Value = ds.Tables(0).Rows(i)("item_um").ToString()
            DGVrecipedetail.Rows(introw).Cells("Um_id").Value = ds.Tables(0).Rows(i)("Item_uom").ToString()
            DGVrecipedetail.Rows(introw).Cells("Quantity").Value = ds.Tables(0).Rows(i)("item_qty").ToString()
            DGVrecipedetail.Rows(introw).Cells("Yield_qty").Value = ds.Tables(0).Rows(i)("item_yield_qty").ToString()
        Next

    End Sub

    Private Sub DGVrecipedetail_RowPostPaint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles DGVrecipedetail.RowPostPaint
        
    End Sub

    Private Sub DGVmenuitems_RowPostPaint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles DGVmenuitems.RowPostPaint
        If DGVmenuitems.Rows.Count - 1 > 0 Then
            Dim gvr As DataGridViewRow = Me.DGVmenuitems.Rows(e.RowIndex)

            If gvr.Cells(2).Value = "N" Then
                ' gvr.DefaultCellStyle.BackColor = Color.DimGray
                gvr.ReadOnly = True
            Else
                ' gvr.DefaultCellStyle.BackColor = Color.Silver
                gvr.ReadOnly = True
            End If
        End If
    End Sub

    Public Sub fillRecipedetail(ByVal menuid As Integer)
        Dim ds As New DataSet()
        ds = obj.fill_Data_set("Pro_Get_Recipe_Items", "@menu_id", Convert.ToString(menuid))

        cmbmenuheads.SelectedValue = Convert.ToInt32(ds.Tables(0).Rows(0)("menuhead_id"))
        cmbmenuitems.SelectedValue = Convert.ToInt32(ds.Tables(0).Rows(0)("menu_id"))

        Dim introw As Integer
        For irow As Integer = 0 To ds.Tables(0).Rows.Count - 1
            introw = DGVrecipedetail.Rows.Count - 1
            DGVrecipedetail.Rows.Insert(introw)
            DGVrecipedetail.Rows(introw).Cells("Item_ID").Value = ds.Tables(0).Rows(irow)("Item_id")
            DGVrecipedetail.Rows(introw).Cells("Item_CODE").Value = ds.Tables(0).Rows(irow)("item_Code").ToString()
            DGVrecipedetail.Rows(introw).Cells("Item_Name").Value = ds.Tables(0).Rows(irow)("item_Name").ToString()
            DGVrecipedetail.Rows(introw).Cells("UM_Name").Value = ds.Tables(0).Rows(irow)("UM_NAME").ToString()
            DGVrecipedetail.Rows(introw).Cells("Um_id").Value = ds.Tables(0).Rows(irow)("um_id").ToString()
            DGVrecipedetail.Rows(introw).Cells("Quantity").Value = ds.Tables(0).Rows(irow)("Quantity").ToString()
            DGVrecipedetail.Rows(introw).Cells("Yield_qty").Value = ds.Tables(0).Rows(irow)("yield_qty").ToString()
        Next

        ds = obj.fill_Data_set("Pro_GetSemifinishedItemsinRecipe", "@menu_id", Convert.ToDouble(menuid))
        If ds.Tables(0).Rows.Count > 0 Then
            For rowcount As Integer = 0 To ds.Tables(0).Rows.Count - 1
                introw = DGVSemiFinisheditems.Rows.Count - 1
                DGVSemiFinisheditems.Rows.Insert(introw)
                DGVSemiFinisheditems.Rows(introw).Cells("Item_id").Value = ds.Tables(0).Rows(rowcount)("semifinisheditem_id")
                DGVSemiFinisheditems.Rows(introw).Cells("Item_name").Value = ds.Tables(0).Rows(rowcount)("semifinisheditem_name")
                DGVSemiFinisheditems.Rows(introw).Cells("Item_uom").Value = ds.Tables(0).Rows(rowcount)("um_name")
                DGVSemiFinisheditems.Rows(introw).Cells("Item_qty").Value = ds.Tables(0).Rows(rowcount)("item_qty")
            Next
        End If
    End Sub

    Private Sub DGVmenuitems_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVmenuitems.CellDoubleClick
        If DGVmenuitems.Rows(e.RowIndex).Cells(2).Value = "Y" Then
            Dim menuid As Integer = Convert.ToInt32(DGVmenuitems.Rows(e.RowIndex).Cells(0).Value)
            cmbmenuheads.Enabled = False
            cmbmenuitems.Enabled = False
            cmb_OutletName.Enabled = False
            fillRecipedetail(menuid)
            tbcrecipe.SelectTab(1)
        Else
            MsgBox("Recipe not defined.", MsgBoxStyle.Information)
        End If
        
    End Sub

    Private Sub cmb_OutletName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_OutletName.SelectedIndexChanged
        obj.ComboBind(cmbmenuitems, "SELECT  pk_itemid_num,Itemname_vch FROM dbo.MenuMaster INNER JOIN dbo.MenuMapping ON dbo.MenuMaster.pk_itemid_num=dbo.MenuMapping.fk_ItemId_num WHERE dbo.MenuMapping.fk_Outletid_num= " & cmb_OutletName.SelectedValue & " AND dbo.MenuMaster.fk_categoryid_num= " & cmbmenuheads.SelectedValue & " and (dinein_ch = '1' or TakeAway_ch='1') order by Itemname_vch", "Itemname_vch", "pk_itemid_num", True)
    End Sub

    Private Sub DGVSemiFinisheditems_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVSemiFinisheditems.CellClick
        If DGVSemiFinisheditems.Rows(DGVSemiFinisheditems.CurrentRow.Index).Cells("Remove").Value = "" Then
            Return
        Else
            If DGVSemiFinisheditems.Rows.Count > 1 Then
                If DGVSemiFinisheditems.Rows(DGVSemiFinisheditems.CurrentRow.Index).Cells(e.ColumnIndex).Value.ToString = "REMOVE" Then
                    Dim btnremove As DialogResult
                    btnremove = MessageBox.Show("Are you sure you want to delete the selected item?", gblMessageHeading_delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If btnremove = Windows.Forms.DialogResult.Yes Then

                        Dim ds As New DataSet
                        ds = obj.fill_Data_set("Pro_Get_Semifinishedrecipe_detail", "@SemifinishedRecipe_id,@qty", Convert.ToString(DGVSemiFinisheditems.Rows(e.RowIndex).Cells("Item_id").Value) & "," & Convert.ToString(DGVSemiFinisheditems.Rows(e.RowIndex).Cells("Item_qty").Value))


                        Dim index = ds.Tables(0).Rows.Count - 1
                        Dim rowindex = DGVrecipedetail.Rows.Count - 1

                        
                        Dim rowcount As Integer = 0
                        While rowcount <= index
                            Dim count As Integer = 0
                            While count <= rowindex
                                If Convert.ToInt32(DGVrecipedetail.Rows(count).Cells("Item_id").Value) = Convert.ToInt32(ds.Tables(0).Rows(rowcount)("Item_id")) Then
                                    If Convert.ToDouble(DGVrecipedetail.Rows(count).Cells("Quantity").Value) = Convert.ToDouble(ds.Tables(0).Rows(rowcount)("Item_qty")) Then
                                        DGVrecipedetail.Rows.RemoveAt(count)
                                        rowindex = rowindex - 1
                                        
                                    ElseIf Convert.ToDouble(DGVrecipedetail.Rows(count).Cells("Quantity").Value) > Convert.ToDouble(ds.Tables(0).Rows(rowcount)("Item_qty")) Then

                                        DGVrecipedetail.Rows(count).Cells("Quantity").Value = (Convert.ToDouble(DGVrecipedetail.Rows(count).Cells("Quantity").Value) - Convert.ToDouble(ds.Tables(0).Rows(rowcount)("item_qty"))).ToString("#0.00000")
                                        DGVrecipedetail.Rows(count).Cells("Yield_qty").Value = (Convert.ToDouble(DGVrecipedetail.Rows(count).Cells("Yield_qty").Value) - Convert.ToDouble(ds.Tables(0).Rows(rowcount)("item_yield_qty"))).ToString("#0.00000")
                                        
                                    Else
                                        MsgBox(DGVrecipedetail.Rows(count).Cells("Item_id").Value)
                                        Exit Sub
                                    End If
                                End If
                                count = count + 1

                            End While
                            rowcount = rowcount + 1
                        End While
                        DGVSemiFinisheditems.Rows.RemoveAt(DGVSemiFinisheditems.CurrentRow.Index)
                    Else
                        Return
                    End If
                Else
                    Return
                End If
                Return
            End If
        End If
    End Sub

    Private Sub cmbmenuitems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbmenuitems.SelectedIndexChanged
        If cmbmenuheads.Enabled = True Then
            If cmbmenuheads.SelectedIndex <> 0 Then
                If cmbmenuitems.SelectedIndex <> 0 Then
                    Dim count As Integer
                    count = Convert.ToInt32(obj.get_record_count("SELECT  COUNT(*) FROM recipe_master WHERE   menu_id = " & cmbmenuitems.SelectedValue))

                    If count > 0 Then
                        fillRecipedetail(cmbmenuitems.SelectedValue)
                    End If
                End If
            End If
        End If
    End Sub

End Class
