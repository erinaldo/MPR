Imports System.Data.SqlClient
Imports System.Collections
Imports MMSPlus.SemiFinishedItem

''' <summary>
'''  Semi Finished Items Master + Semi Finished Recipe Master
''' </summary>
''' '    '----------------------------------------------------------------
''' '    ' Developed by: Yogesh Chandra Upreti 
''' '    '----------------------------------------------------------------     

Public Class frm_Semi_Finished_Recipe_Master
    Implements IForm
    Dim obj As New SemiFinishedItem.Cls_Semi_Finished_Item_Master
    Dim prpty As New SemiFinishedItem.Cls_Semi_Finished_Item_Master_prop
    Dim Flag As String
    Dim ItemId As Integer
    Dim ds As New DataSet
    Dim Qry As String
    Dim int_ColumnIndex As Integer
    Dim txtQuanity As TextBox
    Dim txtYieldQuanity As TextBox
    Dim DtItem As New DataTable
    Dim _rights As Form_Rights

    Private Sub frm_Semi_Finished_Recipe_Master_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Flag = "save"
            Call CreateTable()
            Call grid_style()
            ' Call obj.FormatGrid(grdSemiFinishedItemMaster)
            '  Call obj.FormatGrid(grdSemiFinishedRecipeMaster)
            Call Get_UOM()
            Call FillGrid()
            Call grid_style()
            Call new_initialisation()
            'grdSemiFinishedRecipeMaster.AllowUserToAddRows = True
            TabControl1.SelectTab(0)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Form Load")
        End Try
    End Sub

    Private Sub new_initialisation()
        Flag = "save"
        grdSemiFinishedRecipeMaster.Rows.Clear()
        FillGrid()
        txtItem_Code.Text = ""
        txtItem_Name.Text = ""
        txtItem_Code.Enabled = True
        Call Get_UOM()
        'TabControl1.SelectTab(1)
    End Sub
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub


    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        TabControl1.SelectTab(1)
        new_initialisation()
    End Sub
    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub
    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub
    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        Call new_initialisation()
    End Sub
    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Try
            prpty = New Cls_Semi_Finished_Item_Master_prop
            If grdSemiFinishedRecipeMaster.Rows.Count >= 1 And Not grdSemiFinishedRecipeMaster.Rows(0).Cells(1).Value = Trim("") Then
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If

                ''Check For Null Values
                If txtItem_Code.Text = "" And txtItem_Name.Text = "" Then
                    MsgBox("Please enter Item Code and Item Name.", MsgBoxStyle.Exclamation, gblMessageHeading)
                    Exit Sub
                End If

                ''Check for the Blank Row
                For i As Integer = 0 To grdSemiFinishedRecipeMaster.Rows.Count - 2
                    If grdSemiFinishedRecipeMaster.Rows(i).Cells("Ingredient_Id").Value = Nothing Then
                        MsgBox("Add Ingredient First.", MsgBoxStyle.Exclamation, gblMessageHeading)
                        'grdSemiFinishedRecipeMaster.Rows.RemoveAt(grdSemiFinishedRecipeMaster.CurrentRow.Index)
                        'grdSemiFinishedRecipeMaster.CommitEdit(DataGridViewDataErrorContexts.RowDeletion)

                        Return
                    End If
                Next

                ''Check For Quantity and Yield Quantity
                For rowcount As Integer = rowcount To grdSemiFinishedRecipeMaster.Rows.Count - 2
                    If grdSemiFinishedRecipeMaster.Rows(rowcount).Cells("Ingredient_Id").Value <> Nothing Then
                        If grdSemiFinishedRecipeMaster.Rows(rowcount).Cells("Qty").Value = Nothing Then
                            MsgBox("Please enter some quantity.", MsgBoxStyle.Exclamation, gblMessageHeading)
                            Return
                        End If
                        If grdSemiFinishedRecipeMaster.Rows(rowcount).Cells("Yield_Qty").Value = Nothing Then
                            grdSemiFinishedRecipeMaster.Rows(rowcount).Cells("Yield_Qty").Value = 0.0
                        End If
                        If Not IsNumeric(grdSemiFinishedRecipeMaster.Rows(rowcount).Cells("Qty").Value) OrElse grdSemiFinishedRecipeMaster.Rows(rowcount).Cells("Qty").Value < 0 Then
                            MsgBox("Sorry! Quantity should be numeric.", MsgBoxStyle.Exclamation, gblMessageHeading)
                            grdSemiFinishedRecipeMaster.Rows(rowcount).Cells("Qty").Value = Nothing
                            grdSemiFinishedRecipeMaster.Rows(rowcount).Cells("Yield_Qty").Value = Nothing
                            Return
                        End If
                        If Not IsNumeric(grdSemiFinishedRecipeMaster.Rows(rowcount).Cells("Yield_Qty").Value) OrElse grdSemiFinishedRecipeMaster.Rows(rowcount).Cells("Yield_Qty").Value < 0 Then
                            MsgBox("Sorry! Yield Quantity should be numeric.", MsgBoxStyle.Exclamation, gblMessageHeading)
                            grdSemiFinishedRecipeMaster.Rows(rowcount).Cells("Yield_Qty").Value = Nothing
                            Exit Sub
                        End If
                    End If
                Next

                ''Saving Semi Finished Recipe 


                Dim SemItem_Id As Integer

                If Flag = "save" Then
                    prpty.Item_Code = txtItem_Code.Text
                    prpty.Item_Name = txtItem_Name.Text
                    prpty.uomid = Convert.ToDecimal(cmbItemUnit.SelectedValue)

                    'Saving Semi Finished Recipe item s
                    obj.insert_SemiFinishedItem_DETAIL(prpty)

                    ' prpty = New Cls_Semi_Finished_Item_Master_prop
                    ds = obj.Fill_DataSet("Select max(Item_Id) as Semi_item_Id from Semifinished_Item_Master")
                    Dim j As Int32
                    For j = 0 To grdSemiFinishedRecipeMaster.Rows.Count - 2 Step j + 1
                        prpty.SemItem_Id = ds.Tables(0).Rows(0)(0)
                        prpty.ingredientid = grdSemiFinishedRecipeMaster.Rows(j).Cells("Ingredient_Id").Value
                        prpty.uomid = grdSemiFinishedRecipeMaster.Rows(j).Cells("Unit_ID").Value
                        prpty.qty = grdSemiFinishedRecipeMaster.Rows(j).Cells("Qty").Value
                        prpty.yieldQty = grdSemiFinishedRecipeMaster.Rows(j).Cells("Yield_Qty").Value
                        obj.insert_SemiFinishedItemRecipe(prpty)
                    Next
                    MsgBox("Semi Finished Item Recipe Saved Successfully.", MsgBoxStyle.Information, gblMessageHeading)

                Else

                    ''Updating Semi Finished Recipe Items


                    prpty.Item_Code = txtItem_Code.Text
                    prpty.Item_Name = txtItem_Name.Text
                    prpty.uomid = Convert.ToDecimal(cmbItemUnit.SelectedValue)

                    'Saving Semi Finished Recipe item s
                    obj.insert_SemiFinishedItem_DETAIL(prpty)



                    SemItem_Id = grdSemiFinishedItemMaster.Rows(grdSemiFinishedItemMaster.CurrentRow.Index).Cells(0).Value
                    obj.fill_Data_set("Pro_GetRecipeDeleteSemiFinishedItem", "@SemItem_Id", SemItem_Id)

                    ' prpty = New Cls_Semi_Finished_Item_Master_prop

                    Dim j As Int32

                    For j = 0 To grdSemiFinishedRecipeMaster.Rows.Count - 2 Step j + 1
                        prpty.SemItem_Id = grdSemiFinishedItemMaster.Rows(grdSemiFinishedItemMaster.CurrentRow.Index).Cells(0).Value
                        prpty.ingredientid = grdSemiFinishedRecipeMaster.Rows(j).Cells("Ingredient_Id").Value
                        prpty.uomid = grdSemiFinishedRecipeMaster.Rows(j).Cells("Unit_ID").Value
                        prpty.qty = grdSemiFinishedRecipeMaster.Rows(j).Cells("Qty").Value
                        prpty.yieldQty = grdSemiFinishedRecipeMaster.Rows(j).Cells("Yield_Qty").Value
                        obj.insert_SemiFinishedItemRecipe(prpty)
                    Next

                    MsgBox("Recipe Saved Successfully", MsgBoxStyle.Information, gblMessageHeading)

                End If
            Else
                MsgBox("Please Add Atleast One Ingredient In Recipe List.", vbExclamation, gblMessageHeading)
                Return
            End If
            TabControl1.SelectTab(0)
            Call new_initialisation()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_item_master")
        End Try

    End Sub
    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub



    Private Enum enmGrdSemiRecipeDetail
        Item_Id = 0
        Item_Code = 1
        Item_Name = 2
        fk_Uom_num = 3
        Qty_num = 4
        yieldQty_num = 5
        pk_RecepieId_num = 6
        fk_IngredientId_num = 7
        Ingred_Name = 8
        UM_Name = 9
        UM_ID = 10
    End Enum
    Private Sub CreateTable()
        DtItem.Columns.Add(New DataColumn("Item_Id", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Item_Code", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Item_Name", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Unit_Id", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("UOM", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Ingredient_Name", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Qty", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Yield_Qty", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Recipe_Id", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Ingredient_Id", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("SemiFinishedItemCode", Type.GetType("System.String")))
        'DtItem.Columns.Add(New DataColumn("Remove", Type.GetType("System.String")))
    End Sub
    Private Sub grid_style()
        Dim txbCol As DataGridViewTextBoxColumn
        Dim cmbCol As New DataGridViewComboBoxColumn

        grdSemiFinishedRecipeMaster.Columns.Clear()

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Id"
            .Name = "Item_Id"
            .DataPropertyName = "Item_Id"
            .ReadOnly = True
            .Visible = False
            .Width = 100
        End With
        grdSemiFinishedRecipeMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Code"
            .Name = "Item_Code"
            .DataPropertyName = "Item_Code"
            .ReadOnly = True
            .Visible = True
            .Width = 100
        End With

        grdSemiFinishedRecipeMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Ingredient Name"
            .Name = "Ingredient_Name"
            .ReadOnly = True
            .Visible = True
            .Width = 500
        End With
        grdSemiFinishedRecipeMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Name"
            .Name = "Item_Name"
            .ReadOnly = True
            .Visible = False
            .Width = 400
        End With
        grdSemiFinishedRecipeMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "UOM ID"
            .Name = "Unit_ID"
            .ReadOnly = True
            .Visible = False
            .Width = 80
        End With
        grdSemiFinishedRecipeMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "UOM"
            .Name = "UOM"
            .ReadOnly = True
            .Visible = True
            .Width = 80
        End With
        grdSemiFinishedRecipeMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Quantity"
            .Name = "Qty"
            .ReadOnly = False
            .Visible = True
            .Width = 80
        End With
        grdSemiFinishedRecipeMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Yield Qty"
            .Name = "Yield_Qty"
            .ReadOnly = False
            .Visible = True
            .Width = 90
        End With
        grdSemiFinishedRecipeMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Recipe Id"
            .Name = "Recipe_Id"
            .ReadOnly = True
            .Visible = False
            .Width = 80
        End With
        grdSemiFinishedRecipeMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Ingredient Id"
            .Name = "Ingredient_Id"
            .ReadOnly = True
            .Visible = False
            .Width = 80
        End With
        grdSemiFinishedRecipeMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "SemiFinished Item Code"
            .Name = "SemiFinishedItemCode"
            .ReadOnly = True
            .Visible = False
            .Width = 80
        End With
        grdSemiFinishedRecipeMaster.Columns.Add(txbCol)


        'Dim btndeleteColumn As New DataGridViewButtonColumn
        'btndeleteColumn = New DataGridViewButtonColumn
        'With btndeleteColumn
        '    .Name = "Remove"
        '    .Visible = True
        '    .Text = "REMOVE"
        '    .ReadOnly = True
        '    .UseColumnTextForButtonValue = True
        '    .Width = 70
        'End With
        'grdSemiFinishedRecipeMaster.Columns.Add(btndeleteColumn)


        Dim cRate As DataGridViewTextBoxColumn = TryCast(Me.grdSemiFinishedRecipeMaster.Columns("Qty"), DataGridViewTextBoxColumn)
        cRate.MaxInputLength = 25
        Dim cDelQty As DataGridViewTextBoxColumn = TryCast(Me.grdSemiFinishedRecipeMaster.Columns("Yield_Qty"), DataGridViewTextBoxColumn)
        cDelQty.MaxInputLength = 25

        grdSemiFinishedRecipeMaster.Rows.Add()

    End Sub
    Private Sub FillGrid()
        Try
            Call obj.Bind_Grid_Val_With_Para(grdSemiFinishedItemMaster, "Pro_GetRecipeAllSemiFinishedItems", "@pro_type", "1")
            grdSemiFinishedItemMaster.Columns(0).Visible = False
            grdSemiFinishedItemMaster.Columns(0).HeaderText = "Item ID"
            grdSemiFinishedItemMaster.Columns(0).Width = 0
            grdSemiFinishedItemMaster.Columns(1).HeaderText = "Item Code"
            grdSemiFinishedItemMaster.Columns(1).Width = 90
            grdSemiFinishedItemMaster.Columns(1).Visible = False
            grdSemiFinishedItemMaster.Columns(2).HeaderText = "Item Name"
            grdSemiFinishedItemMaster.Columns(2).Width = 380
            grdSemiFinishedItemMaster.Columns(3).HeaderText = "Item Unit"
            grdSemiFinishedItemMaster.Columns(3).Width = 85
            grdSemiFinishedItemMaster.Columns(3).Visible = False
            grdSemiFinishedItemMaster.Columns(4).HeaderText = "Unit ID"
            grdSemiFinishedItemMaster.Columns(4).Width = 85
            grdSemiFinishedItemMaster.Columns(4).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
        End Try
    End Sub
    Private Sub Get_UOM()
        Call obj.ComboBindWithSP_Para(cmbItemUnit, "Pro_GetRecipeUOM", "UM_ID", "UM_DESC", True)
    End Sub
    Private Sub GetSemiFinishedIngredient(ByVal SemiItem_Id As Integer)
        Try
            grdSemiFinishedRecipeMaster.Rows.Clear()
            DtItem.Rows.Clear()
            ds = obj.fill_Data_set("Pro_GetRecipeSemiFinishedItemDetails", "@item_id", SemiItem_Id.ToString())

            If ds.Tables(0).Rows.Count > 0 Then

                'Adding Semifinished Items Details
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        Dim dr As DataRow = DtItem.NewRow()
                        dr("Item_Id") = ds.Tables(0).Rows(i)("Item_Id")
                        dr("Item_Code") = Convert.ToString(ds.Tables(0).Rows(i)("Item_Code"))
                        dr("Item_Name") = Convert.ToString((ds.Tables(0).Rows(i)("Item_Name")))
                        dr("Unit_ID") = Convert.ToString(ds.Tables(0).Rows(i)("Unit_Id"))
                        dr("UOM") = (ds.Tables(0).Rows(i)("UOM"))
                        dr("Ingredient_Name") = (ds.Tables(0).Rows(i)("Ingredient_Name"))
                        dr("Qty") = (ds.Tables(0).Rows(i)("Qty"))
                        dr("Yield_Qty") = (ds.Tables(0).Rows(i)("Yield_Qty"))
                        dr("Recipe_Id") = (ds.Tables(0).Rows(i)("Recipe_Id"))
                        dr("Ingredient_Id") = (ds.Tables(0).Rows(i)("Ingredient_Id"))
                        dr("SemiFinishedItemCode") = (ds.Tables(0).Rows(i)("SemiFinishedItemCode"))

                        'dr("Remove") = ""
                        DtItem.Rows.Add(dr)
                    Next
                End If

                For i As Integer = 0 To DtItem.Rows.Count - 1
                    Dim irowindex As Integer = grdSemiFinishedRecipeMaster.Rows.Add()
                    grdSemiFinishedRecipeMaster.Rows(irowindex).Cells("Item_Id").Value = DtItem.Rows(i)("Item_Id")
                    grdSemiFinishedRecipeMaster.Rows(irowindex).Cells("Item_Code").Value = DtItem.Rows(i)("Item_Code")
                    grdSemiFinishedRecipeMaster.Rows(irowindex).Cells("Item_Name").Value = DtItem.Rows(i)("Item_Name")
                    grdSemiFinishedRecipeMaster.Rows(irowindex).Cells("Unit_ID").Value = DtItem.Rows(i)("Unit_ID")
                    grdSemiFinishedRecipeMaster.Rows(irowindex).Cells("UOM").Value = DtItem.Rows(i)("UOM")
                    grdSemiFinishedRecipeMaster.Rows(irowindex).Cells("Ingredient_Name").Value = DtItem.Rows(i)("Ingredient_Name")
                    grdSemiFinishedRecipeMaster.Rows(irowindex).Cells("Qty").Value = DtItem.Rows(i)("Qty")
                    grdSemiFinishedRecipeMaster.Rows(irowindex).Cells("Yield_Qty").Value = DtItem.Rows(i)("Yield_Qty")
                    grdSemiFinishedRecipeMaster.Rows(irowindex).Cells("Recipe_Id").Value = DtItem.Rows(i)("Recipe_Id")
                    grdSemiFinishedRecipeMaster.Rows(irowindex).Cells("Ingredient_Id").Value = DtItem.Rows(i)("Ingredient_Id")
                    grdSemiFinishedRecipeMaster.Rows(irowindex).Cells("SemiFinishedItemCode").Value = DtItem.Rows(i)("SemiFinishedItemCode")
                    'grdSemiFinishedRecipeMaster.Rows(irowindex).Cells("Remove").Value = DtItem.Rows(i)("Remove")
                    txtItem_Code.Text = DtItem.Rows(i)("SemiFinishedItemCode")
                    txtItem_Name.Text = DtItem.Rows(i)("Item_Name")
                    cmbItemUnit.SelectedValue = DtItem.Rows(i)("Unit_ID")
                    txtItem_Code.Enabled = False
                Next

                TabControl1.SelectTab(1)
            Else
                Return
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in Fill Recipe Details --> Semi Finished Recipe_Master")
        End Try

    End Sub

    Private Sub grdSemiFinishedItemMaster_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSemiFinishedItemMaster.CellDoubleClick
        If e.RowIndex <> -1 Then
            Dim semifinishedid As Integer
            semifinishedid = grdSemiFinishedItemMaster.Rows(e.RowIndex).Cells(0).Value
            Call GetSemiFinishedIngredient(semifinishedid)
            TabControl1.SelectTab(1)
        Else
            Exit Sub
        End If
        Flag = "UPDATE"
    End Sub

    Private Sub grdSemiFinishedRecipeMaster_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub grdSemiFinishedRecipeMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSemiFinishedRecipeMaster.KeyDown
        Try
            If txtItem_Code.Text = "" And txtItem_Name.Text = "" Then
                MsgBox("First insert item code and item name.", MsgBoxStyle.Exclamation, gblMessageHeading)
                Exit Sub
            Else
                If e.KeyCode = Keys.Space Then
                    Dim _Rowindex As Integer

                    _Rowindex = grdSemiFinishedRecipeMaster.CurrentRow.Index
                    If int_ColumnIndex = 2 Then
                        'frm_Show_search.qry = "SELECT  ITEM_ID,ITEM_CODE,ITEM_NAME,UNIT_MASTER.UM_Name AS UOM FROM Item_Master" & _
                        '        " INNER JOIN dbo.UNIT_MASTER ON Item_Master.recp_um_id = dbo.UNIT_MASTER.UM_ID"

                        'frm_Show_search.column_name = "Item_Name"
                        'frm_Show_search.extra_condition = ""
                        'frm_Show_search.item_rate_column = ""
                        'frm_Show_search.ret_column = "Item_ID"
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

                        get_row(frm_Show_search.search_result)

                        frm_Show_search.Close()
                    End If
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in Ingredient insertion.")
        End Try
    End Sub

    Public Sub get_row(ByVal item_id As String)
        Dim IsInsert As Boolean
        Dim ds As DataSet
        If item_id <> -1 Then
            ds = obj.fill_Data_set("Pro_GetRecipeAllIngredients", "@V_ITEM_ID", item_id)
            Dim iRowCount As Int32
            Dim iRow As Int32
            iRowCount = grdSemiFinishedRecipeMaster.RowCount
            IsInsert = True

            For iRow = 0 To iRowCount - 2
                If Trim(grdSemiFinishedRecipeMaster.Rows(iRow).Cells("Ingredient_Id").Value) = Trim(ds.Tables(0).Rows(0)(0)) Then
                    MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
                    IsInsert = False
                    Exit For
                End If
            Next iRow

            If IsInsert = True Then
                Dim introw As Integer

                introw = grdSemiFinishedRecipeMaster.Rows.Count - 1
                grdSemiFinishedRecipeMaster.Rows.Insert(introw)

                grdSemiFinishedRecipeMaster.Rows(introw).Cells("Ingredient_Id").Value = ds.Tables(0).Rows(0)("ITEM_ID")
                grdSemiFinishedRecipeMaster.Rows(introw).Cells("Item_CODE").Value = ds.Tables(0).Rows(0)("ITEM_CODE").ToString()
                grdSemiFinishedRecipeMaster.Rows(introw).Cells("Ingredient_Name").Value = ds.Tables(0).Rows(0)("ITEM_NAME").ToString()
                grdSemiFinishedRecipeMaster.Rows(introw).Cells("UOM").Value = ds.Tables(0).Rows(0)("UOM").ToString()
                grdSemiFinishedRecipeMaster.Rows(introw).Cells("Unit_Id").Value = ds.Tables(0).Rows(0)("Unit_Id").ToString()

            End If
        End If

    End Sub

    Private Sub grdSemiFinishedRecipeMaster_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSemiFinishedRecipeMaster.CellEnter
        int_ColumnIndex = e.ColumnIndex
    End Sub
    Private Sub grdSemiFinishedRecipeMaster_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdSemiFinishedRecipeMaster.DataError
        e.ThrowException = False
    End Sub
    Private Sub grdSemiFinishedRecipeMaster_CellValidated(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSemiFinishedRecipeMaster.CellValidated
        If e.RowIndex < 0 Then
            Return

        Else
            ' grdSemiFinishedRecipeMaster.AllowUserToAddRows = True

            If e.ColumnIndex = 6 Or e.ColumnIndex = 7 Then
                If grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Ingredient_Id").Value = Nothing Then
                    'grdSemiFinishedRecipeMaster.AllowUserToAddRows = False
                    'grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Remove").Value = ""
                    grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("qty").Value = ""
                    grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("yield_qty").Value = ""
                    Exit Sub
                End If
                grdSemiFinishedRecipeMaster.AllowUserToAddRows = True

                ''Validating Cell Value
                If grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Ingredient_Id").Value <> Nothing Then
                    If grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Qty").Value = Nothing Then
                        MsgBox("Please enter some quantity.", MsgBoxStyle.Exclamation, gblMessageHeading)
                        Return
                    End If
                    If grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Yield_Qty").Value = Nothing Then
                        grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Yield_Qty").Value = 0.0
                    End If
                    If Not IsNumeric(grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Qty").Value) OrElse grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Qty").Value < 0 Then
                        MsgBox("Sorry! Quantity should be numeric.", MsgBoxStyle.Exclamation, gblMessageHeading)
                        grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Qty").Value = Nothing
                        'grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Yield_Qty").Value = Nothing
                        Return
                    End If
                    If Not IsNumeric(grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Yield_Qty").Value) OrElse grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Yield_Qty").Value < 0 Then
                        MsgBox("Sorry! Yield Quantity should be numeric.", MsgBoxStyle.Exclamation, gblMessageHeading)
                        grdSemiFinishedRecipeMaster.Rows(grdSemiFinishedRecipeMaster.CurrentRow.Index).Cells("Yield_Qty").Value = Nothing
                        Exit Sub
                    End If
                End If

            Else
                Return
            End If




        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim ds As New DataSet
        ds = obj.fill_Data_set("Pro_GetRecipeAllSemiFinishedItems", "@pro_type", 1)
        grdSemiFinishedItemMaster.DataSource = ds.Tables(0)
        If txtSearch.Text = "" Then
            grdSemiFinishedItemMaster.DataSource = ds.Tables(0)
        Else
            With grdSemiFinishedItemMaster
                Dim dv As New DataView(ds.Tables(0))
                dv.RowFilter = "Item_Name like '%" & txtSearch.Text & "%'"
                grdSemiFinishedItemMaster.DataSource = dv
            End With
        End If
    End Sub
    Private Sub txtYieldQuanity_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub
    Private Sub txtQuanity_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub
    Private Sub grdIngredient_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)
        e.ThrowException = False
    End Sub
End Class
