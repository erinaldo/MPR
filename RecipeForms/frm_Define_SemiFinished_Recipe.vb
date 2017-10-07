Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections

Public Class frm_Define_SemiFinished_Recipe
    Implements IForm
    Dim Flag As String
    Dim ItemId As Integer
    Dim ds As New DataSet
    Dim Qry As String
    Dim int_ColumnIndex As Integer
    Dim txtQuanity As TextBox
    Dim txtYieldQuanity As TextBox
    Dim DtItem As New DataTable
    Dim comm As New CommonClass

    Dim obj As New SemiFinished_Recipe_Master.cls_SemiFinished_Recipe_Master
    Dim objprop As New SemiFinished_Recipe_Master.cls_SemiFinished_Recipe_Master_Prop

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
        tbSemifinisheditems.SelectTab(1)
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        grdSemifinishedItemdetail.EndEdit()

        If txtItem_Code.Text <> "" Then
            If txtItem_Name.Text <> "" Then
                If cmbItemUnit.SelectedIndex > 0 Then
                    If grdSemifinishedItemdetail.Rows.Count - 1 > 0 Then

                      

                        For index As Integer = 0 To grdSemifinishedItemdetail.Rows.Count - 1
                            If grdSemifinishedItemdetail.Rows(index).Cells("Item_id").Value <> Nothing Then

                                If IsNumeric(grdSemifinishedItemdetail.Rows(index).Cells("yield_qty").Value) And IsNumeric(grdSemifinishedItemdetail.Rows(index).Cells("quantity").Value) Then
                                    If Convert.ToDouble(grdSemifinishedItemdetail.Rows(index).Cells("yield_qty").Value) < 0 Or Convert.ToDouble(grdSemifinishedItemdetail.Rows(index).Cells("quantity").Value) <= 0 Then
                                        MsgBox("Quantity cannot be zero and yield quantity cannot be negative.", MsgBoxStyle.Information)
                                        Exit Sub
                                    End If
                                Else
                                    MsgBox("Quantity should be numeric.", MsgBoxStyle.Information)
                                    Exit Sub
                                End If

                            End If
                        Next

                        If txtItem_Code.ReadOnly = False Then
                            Dim count As Integer
                            count = obj.get_record_count("SELECT COUNT(*) FROM semifinished_recipe_master WHERE semifinisheditem_code= '" & txtItem_Code.Text & "'")
                            If count > 0 Then
                                MsgBox("Item Code already exist.", MsgBoxStyle.Information)
                                Exit Sub
                            End If

                            objprop.SemifinishedRecipeid = comm.getMaxValue("SemiFinishedItem_id", "Semifinished_recipe_master")
                            objprop.SemiFinishedRecipeCode = txtItem_Code.Text
                            objprop.SemifinishedRecipeName = txtItem_Name.Text
                            objprop.SemiFinishedRecipeuom = cmbItemUnit.SelectedValue
                            objprop.CreationDate = Now()
                            objprop.CreatedBy = v_the_current_logged_in_user_name
                            objprop.ModificationDate = Now()
                            objprop.ModifiedBy = v_the_current_logged_in_user_name
                            obj.insert_SemiFinished_Recipe_Master(objprop)

                            For irow As Integer = 0 To grdSemifinishedItemdetail.Rows.Count - 1
                                If IsNumeric(grdSemifinishedItemdetail.Rows(irow).Cells("yield_qty").Value) And IsNumeric(grdSemifinishedItemdetail.Rows(irow).Cells("quantity").Value) Then
                                    If Convert.ToDouble(grdSemifinishedItemdetail.Rows(irow).Cells("yield_qty").Value) >= 0 And Convert.ToDouble(grdSemifinishedItemdetail.Rows(irow).Cells("quantity").Value) > 0 Then

                                        objprop.Item_id = Convert.ToInt32(grdSemifinishedItemdetail.Rows(irow).Cells("Item_id").Value)
                                        objprop.Item_uom = Convert.ToInt32(grdSemifinishedItemdetail.Rows(irow).Cells("um_id").Value)
                                        objprop.Item_qty = Convert.ToDouble(grdSemifinishedItemdetail.Rows(irow).Cells("quantity").Value)
                                        objprop.item_yield_qty = Convert.ToDouble(grdSemifinishedItemdetail.Rows(irow).Cells("yield_qty").Value)
                                        objprop.CreationDate = Now()
                                        objprop.CreatedBy = v_the_current_logged_in_user_name
                                        objprop.ModificationDate = Now()
                                        objprop.ModifiedBy = v_the_current_logged_in_user_name
                                        obj.insert_SemiFinished_Recipe_Detail(objprop)
                                    End If
                                End If
                            Next

                            obj.Delete_SemiFinishedItems_in_SemifinishedRecipe(objprop)

                            For rowcount As Integer = 0 To dgv_Semifinisheditemsadded.Rows.Count - 1
                                If IsNumeric(dgv_Semifinisheditemsadded.Rows(rowcount).Cells("Item_id").Value) Then
                                    objprop.SemiFinishedItem_id = Convert.ToInt32(dgv_Semifinisheditemsadded.Rows(rowcount).Cells("Item_id").Value)
                                    objprop.SemiFinishedItem_qty = Convert.ToDouble(dgv_Semifinisheditemsadded.Rows(rowcount).Cells("Item_qty").Value)
                                    objprop.CreationDate = Now()
                                    objprop.CreatedBy = v_the_current_logged_in_user_name
                                    objprop.ModificationDate = Now()
                                    objprop.ModifiedBy = v_the_current_logged_in_user_name
                                    obj.insert_SemiFinishedItems_in_SemiFinishedRecipe(objprop)
                                End If
                            Next

                            MsgBox("Semi Finished Recipe Saved Successfully.", MsgBoxStyle.Information)
                            new_initialization()
                            tbSemifinisheditems.SelectTab(0)
                        Else
                            objprop.SemifinishedRecipeid = Convert.ToInt32(lbl_id.Text)
                            objprop.SemiFinishedRecipeCode = txtItem_Code.Text
                            objprop.SemifinishedRecipeName = txtItem_Name.Text
                            objprop.SemiFinishedRecipeuom = cmbItemUnit.SelectedValue
                            objprop.CreationDate = Now()
                            objprop.CreatedBy = v_the_current_logged_in_user_name
                            objprop.ModificationDate = Now()
                            objprop.ModifiedBy = v_the_current_logged_in_user_name

                            obj.Update_SemiFinished_Recipe_Master(objprop)

                            obj.Delete_SemiFinished_Recipe_Detail(objprop)

                            For irow As Integer = 0 To grdSemifinishedItemdetail.Rows.Count - 1
                                If Convert.ToDouble(grdSemifinishedItemdetail.Rows(irow).Cells("quantity").Value) > 0 Then

                                    objprop.Item_id = Convert.ToInt32(grdSemifinishedItemdetail.Rows(irow).Cells("Item_id").Value)
                                    objprop.Item_uom = Convert.ToInt32(grdSemifinishedItemdetail.Rows(irow).Cells("um_id").Value)
                                    objprop.Item_qty = Convert.ToDouble(grdSemifinishedItemdetail.Rows(irow).Cells("quantity").Value)
                                    objprop.item_yield_qty = Convert.ToDouble(grdSemifinishedItemdetail.Rows(irow).Cells("yield_qty").Value)
                                    objprop.CreationDate = Now()
                                    objprop.CreatedBy = v_the_current_logged_in_user_name
                                    objprop.ModificationDate = Now()
                                    objprop.ModifiedBy = v_the_current_logged_in_user_name
                                    obj.insert_SemiFinished_Recipe_Detail(objprop)
                                End If
                            Next

                            obj.Delete_SemiFinishedItems_in_SemifinishedRecipe(objprop)

                            For rowcount As Integer = 0 To dgv_Semifinisheditemsadded.Rows.Count - 1
                                If IsNumeric(dgv_Semifinisheditemsadded.Rows(rowcount).Cells("Item_id").Value) Then
                                    objprop.SemiFinishedItem_id = Convert.ToInt32(dgv_Semifinisheditemsadded.Rows(rowcount).Cells("Item_id").Value)
                                    objprop.SemiFinishedItem_qty = Convert.ToDouble(dgv_Semifinisheditemsadded.Rows(rowcount).Cells("Item_qty").Value)
                                    objprop.CreationDate = Now()
                                    objprop.CreatedBy = v_the_current_logged_in_user_name
                                    objprop.ModificationDate = Now()
                                    objprop.ModifiedBy = v_the_current_logged_in_user_name
                                    obj.insert_SemiFinishedItems_in_SemiFinishedRecipe(objprop)
                                End If
                            Next

                            MsgBox("Semi Finished Recipe Saved Successfully.", MsgBoxStyle.Information)
                            new_initialization()
                            txtItem_Code.ReadOnly = False

                            tbSemifinisheditems.SelectTab(0)
                        End If
                    Else
                        MsgBox("Please add ingredients.", MsgBoxStyle.Information)
                    End If
                Else
                    MsgBox("Please select Unit of Measurement.", MsgBoxStyle.Information)
                End If
            Else
                MsgBox("Please enter name.", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("Please enter code.", MsgBoxStyle.Information)
        End If
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

    Private Sub frm_Define_SemiFinished_Recipe_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'comm.FormatGrid(grdSemiFinishedItemMaster)
        'comm.FormatGrid(grdSemifinishedItemdetail)
        'comm.FormatGrid(dgv_Semifinisheditemsadded)
        DGVrecipedetail_Style()
        dgv_Semifinisheditemsadded_style()
        new_initialization()
        tbSemifinisheditems.SelectTab(1)
    End Sub

    Private Sub FillMasterGrid()
        Try
            comm.Bind_GridBind_Val(grdSemiFinishedItemMaster, "Pro_Get_Semifinished_Items", "@txtsearch", "", "%" & txtSearch.Text & "%", "")
            grdSemiFinishedItemMaster.Columns(0).Visible = False
            grdSemiFinishedItemMaster.Columns(0).HeaderText = "SemifinishedItem id"
            grdSemiFinishedItemMaster.Columns(0).ReadOnly = True
            grdSemiFinishedItemMaster.Columns(0).Width = 120
            grdSemiFinishedItemMaster.Columns(1).HeaderText = "Semifinished Item Name"
            grdSemiFinishedItemMaster.Columns(1).ReadOnly = True
            grdSemiFinishedItemMaster.Columns(1).Width = 610
            grdSemiFinishedItemMaster.Columns(2).HeaderText = "UOM"
            grdSemiFinishedItemMaster.Columns(2).ReadOnly = True
            grdSemiFinishedItemMaster.Columns(2).Width = 200
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub new_initialization()
        clear_all()
        FillMasterGrid()
        DGVrecipedetail_Style()
        dgv_Semifinisheditemsadded_style()
        comm.ComboBind(cmbItemUnit, "SELECT um_id, um_name FROM unit_master", "um_name", "um_id", True)
        obj.ComboBind(cmbSemiFinishedItems, "SELECT semifinisheditem_id, semifinisheditem_name FROM semifinished_recipe_master order by semifinisheditem_name", "semifinisheditem_name", "semifinisheditem_id", True)
    End Sub

    Public Sub clear_all()
        grdSemifinishedItemdetail.Rows.Clear()
        'grdSemiFinishedItemMaster.Rows.Clear()

        txtSearch.Text = ""
        txtItem_Code.Text = ""
        txtItem_Name.Text = ""
    End Sub

    Public Sub DGVrecipedetail_Style()
        Try
            grdSemifinishedItemdetail.Columns.Clear()
            grdSemifinishedItemdetail.Rows.Clear()
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
            grdSemifinishedItemdetail.Columns.Add(txtItemId)

            With txtItemCode
                .HeaderText = "Item Code"
                .Name = "Item_CODE"
                .ReadOnly = True
                .Visible = True
                .Width = 120
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            grdSemifinishedItemdetail.Columns.Add(txtItemCode)

            With txtItemName
                .HeaderText = "Item Name"
                .Name = "Item_Name"
                .ReadOnly = True
                .Visible = True
                .Width = 380
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            grdSemifinishedItemdetail.Columns.Add(txtItemName)

            With txtItemUom
                .HeaderText = "UOM"
                .Name = "UM_Name"
                .ReadOnly = True
                .Visible = True
                .Width = 110
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            grdSemifinishedItemdetail.Columns.Add(txtItemUom)

            With txtItemuomId
                .HeaderText = "UOM Id"
                .Name = "UM_id"
                .ReadOnly = True
                .Visible = False
                .Width = 10
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            grdSemifinishedItemdetail.Columns.Add(txtItemuomId)

            With txtItemQuantity
                .HeaderText = "Item Qty"
                .Name = "Quantity"
                .ReadOnly = False
                .Visible = True
                .Width = 120
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            End With
            grdSemifinishedItemdetail.Columns.Add(txtItemQuantity)

            With txtyieldqty
                .HeaderText = "Yield Qty"
                .Name = "yield_qty"
                .ReadOnly = False
                .Visible = True
                .Width = 120
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            End With
            grdSemifinishedItemdetail.Columns.Add(txtyieldqty)

            Dim CellLength As DataGridViewTextBoxColumn = TryCast(Me.grdSemifinishedItemdetail.Columns("Quantity"), DataGridViewTextBoxColumn)
            CellLength.MaxInputLength = 10
            grdSemifinishedItemdetail.AllowUserToResizeColumns = False
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub dgv_Semifinisheditemsadded_style()
        Try
            dgv_Semifinisheditemsadded.Columns.Clear()
            dgv_Semifinisheditemsadded.Rows.Clear()

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
            dgv_Semifinisheditemsadded.Columns.Add(txtItemid)

            With txtItemname
                .HeaderText = "Item Name"
                .Name = "Item_name"
                .ReadOnly = True
                .Visible = True
                .Width = 460
            End With
            dgv_Semifinisheditemsadded.Columns.Add(txtItemname)

            With txtuom
                .HeaderText = "Item UOM"
                .Name = "Item_uom"
                .ReadOnly = True
                .Visible = True
                .Width = 160
            End With
            dgv_Semifinisheditemsadded.Columns.Add(txtuom)

            With txtqty
                .HeaderText = "Item Qty"
                .Name = "Item_qty"
                .ReadOnly = True
                .Visible = True
                .Width = 160
            End With
            dgv_Semifinisheditemsadded.Columns.Add(txtqty)

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
            dgv_Semifinisheditemsadded.Columns.Add(btnremove)

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub get_row(ByVal item_id As String)
        Dim IsInsert As Boolean
        Dim ds As DataSet
        ds = comm.fill_Data_set("GET_ITEM_BY_ID_Recipe", "@V_ITEM_ID", item_id)
        Dim iRowCount As Int32
        Dim iRow As Int32
        iRowCount = grdSemifinishedItemdetail.RowCount
        IsInsert = True
        For iRow = 0 To iRowCount - 2
            If grdSemifinishedItemdetail.Item(0, iRow).Value = Convert.ToInt32(ds.Tables(0).Rows(0)(0)) Then
                MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, "Recipe Ingredients")
                IsInsert = False
                Exit For
            End If
        Next iRow
        Dim datatbl As New DataTable
        datatbl = ds.Tables(0)

        If IsInsert = True Then
            Dim introw As Integer
            introw = grdSemifinishedItemdetail.Rows.Count - 1
            grdSemifinishedItemdetail.Rows.Insert(introw)
            grdSemifinishedItemdetail.Rows(introw).Cells("Item_ID").Value = ds.Tables(0).Rows(0)(0)
            grdSemifinishedItemdetail.Rows(introw).Cells("Item_CODE").Value = ds.Tables(0).Rows(0)("item_Code").ToString()
            grdSemifinishedItemdetail.Rows(introw).Cells("Item_Name").Value = ds.Tables(0).Rows(0)("item_Name").ToString()
            grdSemifinishedItemdetail.Rows(introw).Cells("UM_Name").Value = ds.Tables(0).Rows(0)("UM_NAME").ToString()
            grdSemifinishedItemdetail.Rows(introw).Cells("Um_id").Value = ds.Tables(0).Rows(0)("recp_um_id").ToString()
            grdSemifinishedItemdetail.Rows(introw).Cells("Quantity").Value = 0
            grdSemifinishedItemdetail.Rows(introw).Cells("Yield_qty").Value = 0
        End If
    End Sub

    Private Sub grdSemifinishedItemdetail_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSemifinishedItemdetail.KeyDown
        Try
            Dim iRowindex As Int32
            If e.KeyCode = Keys.Space Then
                If txtItem_Code.Text <> "" Then
                    If txtItem_Name.Text <> "" Then
                        If cmbItemUnit.SelectedIndex > 0 Then
                            iRowindex = grdSemifinishedItemdetail.CurrentRow.Index
                            frm_Show_search.qry = "Select Item_master.ITEM_ID,Item_master.ITEM_CODE as [Item Code],Item_master.ITEM_NAME  as [Item Name] from Item_master inner join item_detail on item_master.item_id = item_detail.item_id "
                            frm_Show_search.extra_condition = ""
                            frm_Show_search.ret_column = "Item_ID"
                            frm_Show_search.column_name = "ITEM_NAME"
                            frm_Show_search.cols_width = "80,300"
                            frm_Show_search.cols_no_for_width = "1,2"
                            frm_Show_search.ShowDialog()
                            get_row(frm_Show_search.search_result)
                        Else
                            MsgBox("Please select Unit of Measurement.", MsgBoxStyle.Information, gblMessageHeading)
                        End If
                    Else
                        MsgBox("Please enter Item Name.", MsgBoxStyle.Information, gblMessageHeading)
                    End If

                Else
                    MsgBox("Please enter Item Code.", MsgBoxStyle.Information, gblMessageHeading)
                End If
                End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub grdSemiFinishedItemMaster_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSemiFinishedItemMaster.CellDoubleClick
        Dim SemifinishedItem_id As Integer = Convert.ToInt32(grdSemiFinishedItemMaster.Rows(e.RowIndex).Cells(0).Value)
        Dim ds As New DataSet
        ds = comm.fill_Data_set("Pro_Get_SemiFinishedItemDetail", "@SemiFinishedItem_id", Convert.ToString(SemifinishedItem_id))
        lbl_id.Text = SemifinishedItem_id
        DGVrecipedetail_Style()

        Dim introw As Integer

        If ds.Tables(0).Rows.Count > 0 Then
            txtItem_Code.Text = ds.Tables(0).Rows(0)("semifinishedItem_Code")
            txtItem_Name.Text = ds.Tables(0).Rows(0)("Semifinisheditem_Name")
            cmbItemUnit.SelectedValue = ds.Tables(0).Rows(0)("Semifinisheditem_uom")


            For irow As Integer = 0 To ds.Tables(0).Rows.Count - 1
                introw = grdSemifinishedItemdetail.Rows.Count - 1
                grdSemifinishedItemdetail.Rows.Insert(introw)
                grdSemifinishedItemdetail.Rows(introw).Cells("Item_ID").Value = ds.Tables(0).Rows(irow)("Item_id")
                grdSemifinishedItemdetail.Rows(introw).Cells("Item_CODE").Value = ds.Tables(0).Rows(irow)("item_Code").ToString()
                grdSemifinishedItemdetail.Rows(introw).Cells("Item_Name").Value = ds.Tables(0).Rows(irow)("item_Name").ToString()
                grdSemifinishedItemdetail.Rows(introw).Cells("UM_Name").Value = ds.Tables(0).Rows(irow)("UM_NAME").ToString()
                grdSemifinishedItemdetail.Rows(introw).Cells("Um_id").Value = ds.Tables(0).Rows(irow)("um_id").ToString()
                grdSemifinishedItemdetail.Rows(introw).Cells("Quantity").Value = ds.Tables(0).Rows(irow)("Quantity").ToString()
                grdSemifinishedItemdetail.Rows(introw).Cells("Yield_qty").Value = ds.Tables(0).Rows(irow)("yield_qty").ToString()
            Next
        End If

        ds = obj.fill_Data_set("Pro_GetSemifinishedItemsinSemifinishedRecipe", "@SemiFinishedRecipe_id", Convert.ToDouble(SemifinishedItem_id))
        If ds.Tables(0).Rows.Count > 0 Then
            For rowcount As Integer = 0 To ds.Tables(0).Rows.Count - 1
                introw = dgv_Semifinisheditemsadded.Rows.Count - 1
                dgv_Semifinisheditemsadded.Rows.Insert(introw)
                dgv_Semifinisheditemsadded.Rows(introw).Cells("Item_id").Value = ds.Tables(0).Rows(rowcount)("semifinisheditem_id")
                dgv_Semifinisheditemsadded.Rows(introw).Cells("Item_name").Value = ds.Tables(0).Rows(rowcount)("semifinisheditem_name")
                dgv_Semifinisheditemsadded.Rows(introw).Cells("Item_uom").Value = ds.Tables(0).Rows(rowcount)("um_name")
                dgv_Semifinisheditemsadded.Rows(introw).Cells("Item_qty").Value = ds.Tables(0).Rows(rowcount)("item_qty")
            Next
        End If

        txtItem_Code.ReadOnly = True
        tbSemifinisheditems.SelectTab(1)
    End Sub

    Private Sub btnAddSemiFinishedItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSemiFinishedItems.Click
        If txtItem_Code.Text = "" Then
            MsgBox("Please enter Item Code.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If

        If txtItem_Name.Text = "" Then
            MsgBox("Please enter Item Name.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If

        If cmbSemiFinishedItems.SelectedIndex = 0 Then
            MsgBox("Please select Semi Finished item.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If

        If cmbItemUnit.SelectedIndex = 0 Then
            MsgBox("Please select Unit of Measurement.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If

        If txtSemiItemQty.Text = "" Then
            MsgBox("Please enter Quantity.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If

        Dim ds As New DataSet
        ds = obj.fill_Data_set("Pro_Get_Semifinishedrecipe_detail", "@SemifinishedRecipe_id,@qty", Convert.ToString(cmbSemiFinishedItems.SelectedValue) & "," & txtSemiItemQty.Text)

        Dim flag As String = "True"

        For rowindex As Integer = 0 To dgv_Semifinisheditemsadded.Rows.Count - 1
            If Convert.ToInt32(dgv_Semifinisheditemsadded.Rows(rowindex).Cells("Item_id").Value) = Convert.ToInt32(ds.Tables(0).Rows(0)("semifinisheditem_id")) Then
                dgv_Semifinisheditemsadded.Rows(rowindex).Cells("Item_qty").Value = Convert.ToDouble(dgv_Semifinisheditemsadded.Rows(rowindex).Cells("Item_qty").Value) + Convert.ToDouble(txtSemiItemQty.Text)
                flag = "false"
            End If
        Next

        If flag = "True" Then
            Dim irow As Integer
            irow = dgv_Semifinisheditemsadded.Rows.Count - 1
            dgv_Semifinisheditemsadded.Rows.Insert(irow)
            dgv_Semifinisheditemsadded.Rows(irow).Cells("Item_id").Value = ds.Tables(0).Rows(0)("semifinisheditem_id")
            dgv_Semifinisheditemsadded.Rows(irow).Cells("Item_name").Value = ds.Tables(0).Rows(0)("semifinisheditem_name")
            dgv_Semifinisheditemsadded.Rows(irow).Cells("Item_uom").Value = ds.Tables(0).Rows(0)("recipe_um")
            dgv_Semifinisheditemsadded.Rows(irow).Cells("Item_qty").Value = txtSemiItemQty.Text
        End If

        Dim index = ds.Tables(0).Rows.Count - 1

        For count As Integer = 0 To grdSemifinishedItemdetail.Rows.Count - 1
            Dim rowcount As Integer = 0
            While rowcount <= index
                If Convert.ToInt32(grdSemifinishedItemdetail.Rows(count).Cells("Item_id").Value) = Convert.ToInt32(ds.Tables(0).Rows(rowcount)("Item_id")) Then
                    grdSemifinishedItemdetail.Rows(count).Cells("Quantity").Value = Convert.ToDouble(grdSemifinishedItemdetail.Rows(count).Cells("Quantity").Value) + Convert.ToDouble(ds.Tables(0).Rows(rowcount)("item_qty"))
                    grdSemifinishedItemdetail.Rows(count).Cells("Yield_qty").Value = Convert.ToDouble(grdSemifinishedItemdetail.Rows(count).Cells("Yield_qty").Value) + Convert.ToDouble(ds.Tables(0).Rows(rowcount)("item_yield_qty"))
                    ds.Tables(0).Rows.RemoveAt(rowcount)
                    index = index - 1
                End If
                rowcount = rowcount + 1
            End While

        Next

        Dim introw As Integer
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            introw = grdSemifinishedItemdetail.Rows.Count - 1
            grdSemifinishedItemdetail.Rows.Insert(introw)
            grdSemifinishedItemdetail.Rows(introw).Cells("Item_ID").Value = ds.Tables(0).Rows(i)("Item_id")
            grdSemifinishedItemdetail.Rows(introw).Cells("Item_CODE").Value = ds.Tables(0).Rows(i)("item_Code").ToString()
            grdSemifinishedItemdetail.Rows(introw).Cells("Item_Name").Value = ds.Tables(0).Rows(i)("item_Name").ToString()
            grdSemifinishedItemdetail.Rows(introw).Cells("UM_Name").Value = ds.Tables(0).Rows(i)("item_um").ToString()
            grdSemifinishedItemdetail.Rows(introw).Cells("Um_id").Value = ds.Tables(0).Rows(i)("Item_uom").ToString()
            grdSemifinishedItemdetail.Rows(introw).Cells("Quantity").Value = ds.Tables(0).Rows(i)("item_qty").ToString()
            grdSemifinishedItemdetail.Rows(introw).Cells("Yield_qty").Value = ds.Tables(0).Rows(i)("item_yield_qty").ToString()
        Next
    End Sub

    Private Sub dgv_Semifinisheditemsadded_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_Semifinisheditemsadded.CellClick
        If dgv_Semifinisheditemsadded.Rows(dgv_Semifinisheditemsadded.CurrentRow.Index).Cells("Remove").Value = "" Then
            Return
        Else
            If dgv_Semifinisheditemsadded.Rows.Count > 1 Then
                If dgv_Semifinisheditemsadded.Rows(dgv_Semifinisheditemsadded.CurrentRow.Index).Cells(e.ColumnIndex).Value.ToString = "REMOVE" Then
                    Dim btnremove As DialogResult
                    btnremove = MessageBox.Show("Are you sure you want to delete the selected item?", gblMessageHeading_delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If btnremove = Windows.Forms.DialogResult.Yes Then

                        Dim ds As New DataSet
                        ds = obj.fill_Data_set("Pro_Get_Semifinishedrecipe_detail", "@SemifinishedRecipe_id,@qty", Convert.ToString(dgv_Semifinisheditemsadded.Rows(e.RowIndex).Cells("Item_id").Value) & "," & Convert.ToString(dgv_Semifinisheditemsadded.Rows(e.RowIndex).Cells("Item_qty").Value))


                        Dim index = ds.Tables(0).Rows.Count - 1
                        Dim rowindex = grdSemifinishedItemdetail.Rows.Count - 1


                        Dim rowcount As Integer = 0
                        While rowcount <= index
                            Dim count As Integer = 0
                            While count <= rowindex
                                If Convert.ToInt32(grdSemifinishedItemdetail.Rows(count).Cells("Item_id").Value) = Convert.ToInt32(ds.Tables(0).Rows(rowcount)("Item_id")) Then
                                    If Convert.ToDouble(grdSemifinishedItemdetail.Rows(count).Cells("Quantity").Value) = Convert.ToDouble(ds.Tables(0).Rows(rowcount)("Item_qty")) Then
                                        grdSemifinishedItemdetail.Rows.RemoveAt(count)
                                        rowindex = rowindex - 1

                                    ElseIf Convert.ToDouble(grdSemifinishedItemdetail.Rows(count).Cells("Quantity").Value) > Convert.ToDouble(ds.Tables(0).Rows(rowcount)("Item_qty")) Then

                                        grdSemifinishedItemdetail.Rows(count).Cells("Quantity").Value = (Convert.ToDouble(grdSemifinishedItemdetail.Rows(count).Cells("Quantity").Value) - Convert.ToDouble(ds.Tables(0).Rows(rowcount)("item_qty"))).ToString("#0.00000")
                                        grdSemifinishedItemdetail.Rows(count).Cells("Yield_qty").Value = (Convert.ToDouble(grdSemifinishedItemdetail.Rows(count).Cells("Yield_qty").Value) - Convert.ToDouble(ds.Tables(0).Rows(rowcount)("item_yield_qty"))).ToString("#0.00000")

                                    Else
                                        MsgBox(grdSemifinishedItemdetail.Rows(count).Cells("Item_id").Value)
                                        Exit Sub
                                    End If
                                End If
                                count = count + 1

                            End While
                            rowcount = rowcount + 1
                        End While
                        dgv_Semifinisheditemsadded.Rows.RemoveAt(dgv_Semifinisheditemsadded.CurrentRow.Index)
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
End Class
