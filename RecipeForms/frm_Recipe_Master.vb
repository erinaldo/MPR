Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections

''' <summary>
''' Define Recipe Here
''' </summary>
''' '    '----------------------------------------------------------------
''' '    ' Developed by: Yogesh Chandra Upreti 
''' '    '----------------------------------------------------------------     
Public Class frm_Recipe_Master
    Implements IForm
    Dim obj As New SemiFinishedItem.Cls_Semi_Finished_Item_Master
    Dim prpty As New SemiFinishedItem.Cls_Semi_Finished_Item_Master_prop
    Dim Flag As String
    Dim ItemId As Integer
    Dim ds As New DataSet
    Dim qry As String
    Dim _rights As Form_Rights
    Dim dTable_MenuItems As DataTable
    Public dTable_MenuHeads As DataTable
    Dim commObj As New CommonClass
    Dim Item_Id As Integer
    Dim DtItem As New DataTable

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub frm_Recipe_Master_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Call GetListMenuHeads()

            'Call GetCostCentre()

            Call GetItemsMenuHeads()

            Call GetSemiFinishedItems()


            Flag = "save"

            '  Call obj.FormatGrid(grdListMenuItems)

            'Call obj.FormatGrid(grdItemMaster)

            'Call obj.FormatGrid(grdItemsSelected)

            Call FillGrid()

            'Call FillGridMenuItemList()

            Call grdSelectedItem_style()

            Call CreateTable()

            Call new_initialisation()

            For i As Integer = 0 To grdItemsSelected.Columns.Count - 1
                grdItemsSelected.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next

            For i As Integer = 0 To grdListMenuItems.Columns.Count - 1
                grdListMenuItems.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
            Next

            TabControl1.SelectTab(0)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Form Load")
        End Try
    End Sub


    ''' <summary>
    ''' Giving Style to Grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub gridMenuItem_style()
        Dim txbCol As DataGridViewTextBoxColumn
        txbCol = New DataGridViewTextBoxColumn
        'grdListMenuItems.Columns.Clear()
        With txbCol
            .HeaderText = "Menu Item Id"
            .Name = "MenuItemId"
            .ReadOnly = False
            .Visible = False
            .Width = 120
        End With
        grdListMenuItems.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Menu Item Name"
            .Name = "MenuItem"
            .ReadOnly = False
            .Visible = True
            .Width = 400
        End With
        grdListMenuItems.Columns.Add(txbCol)

    End Sub
    Private Sub grid_style()

        Dim txbCol As DataGridViewTextBoxColumn

        grdItemMaster.Columns.Clear()

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Id"
            .Name = "Item_Id"
            .DataPropertyName = "Item_Id"
            .ReadOnly = True
            .Visible = False
            .Width = 80
        End With
        grdItemMaster.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Code"
            .Name = "Item_Code"
            .DataPropertyName = "Item_Code"
            .ReadOnly = True
            .Visible = True
            .Width = 100
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        grdItemMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Name"
            .Name = "Item_Name"
            .ReadOnly = True
            .Visible = True
            .Width = 400
        End With
        grdItemMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "UOM"
            .Name = "UOM"
            .ReadOnly = True
            .Visible = True
            .Width = 90
        End With
        grdItemMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Unit Id"
            .Name = "Unit_Id"
            .ReadOnly = True
            .Visible = False
            .Width = 70
        End With
        grdItemMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Category Id"
            .Name = "Category_Id"
            .ReadOnly = False
            .Visible = False
            .Width = 70
        End With
        grdItemMaster.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Quantity"
            .Name = "Qty"
            .ReadOnly = False
            .Visible = True
            .Width = 90
        End With
        grdItemMaster.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Yield Quantity"
            .Name = "Yield_Qty"
            .ReadOnly = False
            .Visible = True
            .Width = 110
        End With
        grdItemMaster.Columns.Add(txbCol)


        Dim buttonColumn As New DataGridViewButtonColumn
        buttonColumn = New DataGridViewButtonColumn
        With buttonColumn
            .Name = "Add"
            .Visible = True
            .Text = "Add"

            .UseColumnTextForButtonValue = True
            .Width = 60
            .HeaderCell.Value = ""
            .HeaderText = ""
            .ReadOnly = False
        End With
        grdItemMaster.Columns.Add(buttonColumn)


        Dim cQty As DataGridViewTextBoxColumn = TryCast(Me.grdItemMaster.Columns("Qty"), DataGridViewTextBoxColumn)
        cQty.MaxInputLength = 25
        Dim cYieldQty As DataGridViewTextBoxColumn = TryCast(Me.grdItemMaster.Columns("Yield_Qty"), DataGridViewTextBoxColumn)
        cYieldQty.MaxInputLength = 50


        'Dim cBtnAdd As DataGridViewButtonColumn = TryCast(Me.grdItemMaster.Columns("Add"), DataGridViewButtonColumn)
        'cBtnAdd.MinimumWidth = 30    


    End Sub
    Private Sub grdSelectedItem_style()

        Dim txbCol As DataGridViewTextBoxColumn

        grdItemsSelected.Columns.Clear()

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Id"
            .Name = "Item_Id"
            .DataPropertyName = "Item_Id"
            .ReadOnly = True
            .Visible = False
            .Width = 100
        End With
        grdItemsSelected.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Code"
            .Name = "Item_Code"
            .DataPropertyName = "Item_Code"
            .ReadOnly = True
            .Visible = True
            .Width = 80
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        grdItemsSelected.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Name"
            .Name = "Item_Name"
            .ReadOnly = True
            .Visible = True
            .Width = 410
        End With
        grdItemsSelected.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "UOM"
            .Name = "UOM"
            .ReadOnly = True
            .Visible = True
            .Width = 80
        End With
        grdItemsSelected.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Unit Id"
            .Name = "Unit_Id"
            .ReadOnly = True
            .Visible = False
            .Width = 60
        End With
        grdItemsSelected.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Category Id"
            .Name = "Category_Id"
            .ReadOnly = False
            .Visible = False
            .Width = 70
        End With
        grdItemsSelected.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Quantity"
            .Name = "Qty"
            .ReadOnly = False
            .Visible = True
            .Width = 100
        End With
        grdItemsSelected.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Yield Quantity"
            .Name = "Yield_Qty"
            .ReadOnly = False
            .Visible = True
            .Width = 100
        End With
        grdItemsSelected.Columns.Add(txbCol)


        Dim buttonColumn As New DataGridViewButtonColumn
        buttonColumn = New DataGridViewButtonColumn
        With buttonColumn
            .Name = "Add"
            .Visible = False
            .Text = "Add"
            .ReadOnly = True
            .UseColumnTextForButtonValue = True
            .Width = 60
        End With
        grdItemsSelected.Columns.Add(buttonColumn)


        Dim btndeleteColumn As New DataGridViewButtonColumn
        btndeleteColumn = New DataGridViewButtonColumn
        With btndeleteColumn
            .Name = "Remove"
            .Visible = True
            .Text = "Remove"
            .ReadOnly = True
            .UseColumnTextForButtonValue = True
            .Width = 80
        End With
        grdItemsSelected.Columns.Add(btndeleteColumn)


        Dim cQty As DataGridViewTextBoxColumn = TryCast(Me.grdItemsSelected.Columns("Qty"), DataGridViewTextBoxColumn)
        cQty.MaxInputLength = 25
        Dim cYieldQty As DataGridViewTextBoxColumn = TryCast(Me.grdItemsSelected.Columns("Yield_Qty"), DataGridViewTextBoxColumn)
        cYieldQty.MaxInputLength = 50
    End Sub


    Private Sub new_initialisation()
        cmbMenuHeads.Enabled = True
        cmbMenuItems.Enabled = True
        grdItemsSelected.Rows.Clear()
        DtItem.Clear()
        Flag = "save"
        Call FillGridMenuItemList()
        TabControl1.SelectTab(0)
    End Sub
    Private Sub CreateTable()
        DtItem.Columns.Add(New DataColumn("Item_Id", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Item_Code", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Item_Name", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("UOM", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Category_Id", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Unit_Id", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Qty", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Yield_Qty", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Add", Type.GetType("System.String")))
        DtItem.Columns.Add(New DataColumn("Remove", Type.GetType("System.String")))
    End Sub


    Private Sub FillGridMenuItemList()
        Try
            'ds = obj.fill_Data_set("Pro_GetRecipeMenuItemsList", "@Outletid", v_the_current_division_id)
            ds = obj.fill_Data_sets("Pro_GetRecipeMenuItemsList")

            grdListMenuItems.DataSource = ds.Tables(0)

            grdListMenuItems.RowHeadersDefaultCellStyle.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

            grdListMenuItems.Columns(0).HeaderText = "Menu Item Name"
            grdListMenuItems.Columns(0).Width = 700

            grdListMenuItems.Columns(1).HeaderText = "Menu Item Id"
            grdListMenuItems.Columns(1).Width = 120
            grdListMenuItems.Columns(1).Visible = False

            grdListMenuItems.Columns(2).HeaderText = "Category Head Id"
            grdListMenuItems.Columns(2).Width = 120
            grdListMenuItems.Columns(2).Visible = False
           
            grdListMenuItems.Columns(2).HeaderText = "Status"
            grdListMenuItems.Columns(2).Width = 80
            grdListMenuItems.Columns(2).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in--> List of Recipe Menu Items")
        End Try

    End Sub

    Private Sub grdListMenuItems_RowPostPaint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles grdListMenuItems.RowPostPaint
        If grdListMenuItems.Rows.Count > 0 Then
            Dim gvr As DataGridViewRow = Me.grdListMenuItems.Rows(e.RowIndex)

            If gvr.Cells(3).Value = "N" Then
                gvr.DefaultCellStyle.BackColor = Color.Silver
                gvr.ReadOnly = True
            Else
                gvr.DefaultCellStyle.BackColor = Color.OrangeRed
                gvr.ReadOnly = True
            End If
        End If
    End Sub
    Private Sub FillGrid()

        'Call txtSearchItems_TextChanged(Nothing, Nothing)
        Try
            Dim ds As DataSet = New DataSet()

            Dim txtsearch As String

            If txtSearchItems.Text = Nothing Then
                txtsearch = "%"
            Else
                txtsearch = txtSearchItems.Text
            End If

            ds = obj.fill_Data_set("Pro_GetRecipeIngredients", "@keyword", txtsearch)

            'Fill Data in to Grid 
            Call grid_style()

            Dim dtpRecipeItemDetail As New DataTable
            dtpRecipeItemDetail = ds.Tables(0)

            Dim iRowCount As Int32
            Dim iRow As Int32
            iRowCount = dtpRecipeItemDetail.Rows.Count

            For iRow = 0 To iRowCount - 1
                If dtpRecipeItemDetail.Rows.Count > 0 Then
                    Dim rowindex As Integer = grdItemMaster.Rows.Add()
                    grdItemMaster.Rows(rowindex).Cells("Item_Id").Value = Convert.ToInt32(dtpRecipeItemDetail.Rows(iRow)("Item_Id"))
                    grdItemMaster.Rows(rowindex).Cells("Item_Code").Value = Convert.ToString(dtpRecipeItemDetail.Rows(iRow)("Item_Code"))
                    grdItemMaster.Rows(rowindex).Cells("Item_Name").Value = Convert.ToString(dtpRecipeItemDetail.Rows(iRow)("Item_Name"))
                    grdItemMaster.Rows(rowindex).Cells("UOM").Value = Convert.ToString(dtpRecipeItemDetail.Rows(iRow)("UOM"))
                    grdItemMaster.Rows(rowindex).Cells("Unit_Id").Value = Convert.ToString(dtpRecipeItemDetail.Rows(iRow)("Unit_Id"))
                    grdItemMaster.Rows(rowindex).Cells("Category_Id").Value = Convert.ToDouble(dtpRecipeItemDetail.Rows(iRow)("Category_Id"))
                    grdItemMaster.Rows(rowindex).Cells("Qty").Value = ""
                    grdItemMaster.Rows(rowindex).Cells("Yield_Qty").Value = ""
                    grdItemMaster.Rows(rowindex).Cells("Add").Value = True

                End If
            Next iRow

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
        End Try

    End Sub
    Private Sub FillRecipeGrid()
       
    End Sub
    Private Sub GetItemsMenuHeads()
        Call obj.ComboBinding(cmbMenuHeads, "SELECT '0' AS pk_CategoryId_num, '-- Select Menu Head --' as CategoryName_vch UNION ALL SELECT pk_CategoryId_num, CategoryName_vch FROM CategoryMaster Order By CategoryName_vch", "CategoryName_vch", "pk_CategoryId_num", True)
    End Sub
    Private Sub GetSemiFinishedItems()
        Call obj.ComboBindWithSP(cmbSemiFinishedItems, "Pro_GetRecipeSemiFinishedItems", "Item_Id", "Item_Name", True)
    End Sub
    Private Sub getRecipeDetail(ByVal MenuHead_ID As Integer, ByVal Menu_Item_ID As Integer)

        Try
            Dim ds As DataSet = New DataSet()
            grdItemsSelected.Rows.Clear()
            DtItem.Rows.Clear()
            ds = obj.fill_Data_set("Pro_GetRecipeDefinedIngredients", "itemID", Menu_Item_ID)


            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    Dim dr As DataRow = DtItem.NewRow()
                    dr("Item_Id") = Convert.ToString(ds.Tables(0).Rows(i)("Item_Id"))
                    dr("Item_Code") = Convert.ToString(ds.Tables(0).Rows(i)("Item_Code"))
                    dr("Item_Name") = Convert.ToString((ds.Tables(0).Rows(i)("Item_Name")))
                    dr("UOM") = ds.Tables(0).Rows(i)("UOM")
                    dr("Unit_Id") = ds.Tables(0).Rows(i)("Unit_Id")
                    dr("Category_Id") = ""
                    dr("Qty") = (ds.Tables(0).Rows(i)("Qty"))
                    dr("Yield_Qty") = (ds.Tables(0).Rows(i)("Yield_Qty"))
                    dr("Add") = ""
                    dr("Remove") = ""
                    DtItem.Rows.Add(dr)
                Next
            Else
                MsgBox("Selected item recipe not defined. First define recipe.")
            End If

            '  Dim dr As DataRow = DtItem.NewRow()

            For index As Integer = 0 To DtItem.Rows.Count - 1
                Dim irowindex As Integer = grdItemsSelected.Rows.Add()
                grdItemsSelected.Rows(irowindex).Cells("Item_Id").Value = DtItem.Rows(index)("Item_Id")
                grdItemsSelected.Rows(irowindex).Cells("Item_Code").Value = DtItem.Rows(index)("Item_Code")
                grdItemsSelected.Rows(irowindex).Cells("Item_Name").Value = DtItem.Rows(index)("Item_Name")
                grdItemsSelected.Rows(irowindex).Cells("UOM").Value = DtItem.Rows(index)("UOM")
                grdItemsSelected.Rows(irowindex).Cells("Unit_Id").Value = DtItem.Rows(index)("Unit_Id")
                grdItemsSelected.Rows(irowindex).Cells("Category_Id").Value = DtItem.Rows(index)("Category_Id")
                grdItemsSelected.Rows(irowindex).Cells("Qty").Value = DtItem.Rows(index)("Qty")
                grdItemsSelected.Rows(irowindex).Cells("Yield_Qty").Value = DtItem.Rows(index)("Yield_Qty")
                grdItemsSelected.Rows(irowindex).Cells("Add").Value = DtItem.Rows(index)("Add")
                grdItemsSelected.Rows(irowindex).Cells("Remove").Value = DtItem.Rows(index)("Remove")
            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in -- > Recipe Detail")
        End Try



    End Sub


    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick

        TabControl1.SelectTab(1)

        Call GetItemsMenuHeads()
        Call GetSemiFinishedItems()

        cmbMenuHeads.Enabled = True
        cmbMenuItems.Enabled = True

        grdItemsSelected.Rows.Clear()
    End Sub
    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub
    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub
    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub
    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Try
            If _rights.allow_trans = "N" Then
                RightsMsg()
                Exit Sub
            End If


            If cmbMenuHeads.SelectedValue = 0 Or cmbMenuItems.SelectedValue = 0 Then
                MsgBox("Please First Select Menu Heads and Menu Items Above.", MsgBoxStyle.Information, gblMessageHeading)
                Return
            ElseIf grdItemsSelected.Rows.Count >= 1 And Not grdItemsSelected.Rows(0).Cells(1).Value = Trim("") Then
                prpty.Menu_Item_Id = Convert.ToInt16(cmbMenuItems.SelectedValue)
                obj.fill_Data_set("Pro_GetRecipeDelete", "@menuid", prpty.Menu_Item_Id)

                If Flag = "save" Then
                    If prpty.Menu_Item_Id > 0 Then
                        Dim j As Int32
                        For j = 0 To grdItemsSelected.Rows.Count - 1 Step j + 1

                            prpty.Menu_Item_Id = cmbMenuItems.SelectedValue
                            prpty.ingredientid = Convert.ToInt32(grdItemsSelected.Rows(j).Cells("Item_Id").Value)
                            prpty.qty = Convert.ToDecimal(grdItemsSelected.Rows(j).Cells("Qty").Value)
                            prpty.uomid = Convert.ToInt32(grdItemsSelected.Rows(j).Cells("Unit_Id").Value)
                            prpty.yieldQty = Convert.ToDecimal(grdItemsSelected.Rows(j).Cells("Yield_Qty").Value)

                            obj.insert_RecipeDetails(prpty)
                        Next
                        MsgBox("Recipe Saved Successfully", MsgBoxStyle.Information, gblMessageHeading)
                        Call new_initialisation()
                        Return
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            Else
                MsgBox("Please add atleast one item in Recipe list", vbExclamation, gblMessageHeading)
                Return
            End If

            'TabControl1.SelectTab(0)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in Fill Recipe Details --> Recipe_Master")
        End Try


        '----------------------------------------------------------------
        ' Developed by: Yogesh Chandra Upreti
        '----------------------------------------------------------------       
    End Sub
    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub
    Private Sub btnAddSemiFinishedItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSemiFinishedItems.Click
        If cmbMenuHeads.SelectedValue = 0 And cmbMenuItems.SelectedValue = 0 Then
            MsgBox("Please Select Menu Head And Item above.", MsgBoxStyle.Information, gblMessageHeading)
            Return
        End If
        If cmbSemiFinishedItems.SelectedValue < 0 Then
            MsgBox("First Select Semifinished Item.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        ElseIf txtSemiItemQty.Text = Nothing Then
            MsgBox("Please Insert Quantity.", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        Else
            'Getting All Records From Recipe Items
            prpty.SemItem_Id = Convert.ToInt16(cmbSemiFinishedItems.SelectedValue)
            prpty.Semi_Item_Qty = Convert.ToString(txtSemiItemQty.Text)
            Dim ds As New DataSet
            ds = obj.fill_Data_set_val("Pro_GetRecipe_N_SEmifinshed", "@semi_item_id", "@qty", Convert.ToString(prpty.SemItem_Id), prpty.Semi_Item_Qty)

            Dim rowcount = ds.Tables(0).Rows.Count - 1

            For irow As Integer = 0 To grdItemsSelected.Rows.Count - 1
                Dim index = 0
                While index <= rowcount
                    If Convert.ToInt32(ds.Tables(0).Rows(index)("ItemId")) = Convert.ToInt32(grdItemsSelected.Rows(irow).Cells("item_id").Value) Then
                        grdItemsSelected.Rows(irow).Cells("Qty").Value = Convert.ToDouble(grdItemsSelected.Rows(irow).Cells("Qty").Value) + Convert.ToDouble(ds.Tables(0).Rows(index)("qty"))
                        grdItemsSelected.Rows(irow).Cells("Yield_Qty").Value = Convert.ToDouble(grdItemsSelected.Rows(irow).Cells("Yield_Qty").Value) + Convert.ToDouble(ds.Tables(0).Rows(index)("yieldQty"))
                        ds.Tables(0).Rows.RemoveAt(index)
                        rowcount = rowcount - 1
                    End If
                    index = index + 1
                End While
                If rowcount < 0 Then
                    Exit Sub
                End If
            Next

            If ds.Tables(0).Rows.Count > 0 Then
                DtItem.Rows.Clear()
                'Adding Semifinished Items Details
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim i As Integer
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        Dim dr As DataRow = DtItem.NewRow()
                        dr("Item_Id") = ds.Tables(0).Rows(i)("ItemId")
                        dr("Item_Code") = Convert.ToString(ds.Tables(0).Rows(i)("ItemCode"))
                        dr("Item_Name") = Convert.ToString((ds.Tables(0).Rows(i)("itemName")))
                        dr("UOM") = Convert.ToString(ds.Tables(0).Rows(i)("UOM"))
                        dr("Unit_Id") = (ds.Tables(0).Rows(i)("unitId"))
                        dr("Category_Id") = ""
                        dr("Qty") = (ds.Tables(0).Rows(i)("qty"))
                        dr("Yield_Qty") = (ds.Tables(0).Rows(i)("yieldQty"))
                        dr("Add") = ""
                        dr("Remove") = ""
                        DtItem.Rows.Add(dr)
                    Next
                End If

                For i As Integer = 0 To DtItem.Rows.Count - 1

                    Dim irowindex As Integer = grdItemsSelected.Rows.Add()
                    grdItemsSelected.Rows(irowindex).Cells("Item_Id").Value = DtItem.Rows(i)("Item_Id")
                    grdItemsSelected.Rows(irowindex).Cells("Item_Code").Value = DtItem.Rows(i)("Item_Code")
                    grdItemsSelected.Rows(irowindex).Cells("Item_Name").Value = DtItem.Rows(i)("Item_Name")
                    grdItemsSelected.Rows(irowindex).Cells("UOM").Value = DtItem.Rows(i)("UOM")
                    grdItemsSelected.Rows(irowindex).Cells("Unit_Id").Value = DtItem.Rows(i)("Unit_Id")
                    grdItemsSelected.Rows(irowindex).Cells("Category_Id").Value = DtItem.Rows(i)("Category_Id")
                    grdItemsSelected.Rows(irowindex).Cells("Qty").Value = DtItem.Rows(i)("Qty")
                    grdItemsSelected.Rows(irowindex).Cells("Yield_Qty").Value = DtItem.Rows(i)("Yield_Qty")
                    grdItemsSelected.Rows(irowindex).Cells("Add").Value = DtItem.Rows(i)("Add")
                    grdItemsSelected.Rows(irowindex).Cells("Remove").Value = DtItem.Rows(i)("Remove")
                Next

                TabControl1.SelectTab(1)
            Else
                MsgBox("Selected Semi-Finished Item Recipe Not Defined Yet. First Define Recipe.", MsgBoxStyle.Information, gblMessageHeading)
            End If
        End If

    End Sub
    Private Sub btnSaveRecipe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRecipe.Click
        Call SaveClick(Nothing, Nothing)
    End Sub
    Private Sub txtSearchItems_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchItems.TextChanged
        Call FillGrid()
    End Sub

    Private Sub cmbMenuHeads_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMenuHeads.SelectedIndexChanged
        Try
            Dim dr As DataRow

            ds = commObj.fill_Data_set("Pro_GetRecipeMenuItemsHeadwise", "menuheadid", Convert.ToInt16(cmbMenuHeads.SelectedValue))

            'ds = obj.fill_Data_set_val("Pro_GetRecipeMenuItemsHeadwise", "menuheadid", "OutletId", Convert.ToInt16(cmbMenuHeads.SelectedValue), v_the_current_division_id)
            'ds = obj.fill_Menu_Items("Pro_GetRecipeMenuItemsHeadwise", "menuheadid", "outletIds", "CostCenter_Id", Convert.ToInt16(cmbMenuHeads.SelectedValue), v_the_current_division_id, Convert.ToInt16(cmbCostCentre.SelectedValue))

            If cmbMenuHeads.SelectedValue = 0 Then
                ds.Tables(0).Clear()
                dr = ds.Tables(0).NewRow
                dr("MenuItemId") = 0
                dr("MenuItem") = "-- First Select Menu Head --"
                ds.Tables(0).Rows.Add(dr)
            Else
                If ds.Tables(0).Rows.Count > 0 Then
                    dr = ds.Tables(0).NewRow
                    dr("MenuItemId") = 0
                    dr("MenuItem") = "-- Select Menu Item --"
                    ds.Tables(0).Rows.InsertAt(dr, 0)
                Else
                    dr = ds.Tables(0).NewRow
                    dr("MenuItemId") = 0
                    dr("MenuItem") = "-- Menu Items Not Found --"
                    ds.Tables(0).Rows.Add(dr)
                End If
            End If

            cmbMenuItems.DataSource = ds.Tables(0)
            cmbMenuItems.ValueMember = "MenuItemId"
            cmbMenuItems.DisplayMember = "MenuItem"

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in -- > Menu Item Selection")
        End Try
    End Sub
    Private Sub cmbMenuItems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMenuItems.SelectedIndexChanged

        Dim ds As DataSet = New DataSet()
        Dim i As Integer
        If cmbMenuHeads.SelectedValue > 0 Then

            If cmbMenuItems.SelectedValue > 0 Then

                grdItemsSelected.Rows.Clear()

                DtItem.Rows.Clear()

                ds = obj.fill_Data_set("Pro_GetRecipeDefinedIngredients", "itemID", Convert.ToInt32(cmbMenuItems.SelectedValue))

                If ds.Tables(0).Rows.Count > 0 Then

                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        Dim dr As DataRow = DtItem.NewRow()
                        dr("Item_Id") = Convert.ToString(ds.Tables(0).Rows(i)("Item_Id"))
                        dr("Item_Code") = Convert.ToString(ds.Tables(0).Rows(i)("Item_Code"))
                        dr("Item_Name") = Convert.ToString((ds.Tables(0).Rows(i)("Item_Name")))
                        dr("UOM") = ds.Tables(0).Rows(i)("UOM")
                        dr("Unit_Id") = ds.Tables(0).Rows(i)("Unit_Id")
                        dr("Category_Id") = ""
                        dr("Qty") = (ds.Tables(0).Rows(i)("Qty"))
                        dr("Yield_Qty") = (ds.Tables(0).Rows(i)("Yield_Qty"))
                        dr("Add") = ""
                        dr("Remove") = ""
                        DtItem.Rows.Add(dr)
                    Next
                End If

                For index As Integer = 0 To DtItem.Rows.Count - 1
                    Dim irowindex As Integer = grdItemsSelected.Rows.Add()
                    grdItemsSelected.Rows(irowindex).Cells("Item_Id").Value = DtItem.Rows(index)("Item_Id")
                    grdItemsSelected.Rows(irowindex).Cells("Item_Code").Value = DtItem.Rows(index)("Item_Code")
                    grdItemsSelected.Rows(irowindex).Cells("Item_Name").Value = DtItem.Rows(index)("Item_Name")
                    grdItemsSelected.Rows(irowindex).Cells("UOM").Value = DtItem.Rows(index)("UOM")
                    grdItemsSelected.Rows(irowindex).Cells("Unit_Id").Value = DtItem.Rows(index)("Unit_Id")
                    grdItemsSelected.Rows(irowindex).Cells("Category_Id").Value = DtItem.Rows(index)("Category_Id")
                    grdItemsSelected.Rows(irowindex).Cells("Qty").Value = DtItem.Rows(index)("Qty")
                    grdItemsSelected.Rows(irowindex).Cells("Yield_Qty").Value = DtItem.Rows(index)("Yield_Qty")
                    grdItemsSelected.Rows(irowindex).Cells("Add").Value = DtItem.Rows(index)("Add")
                    grdItemsSelected.Rows(irowindex).Cells("Remove").Value = DtItem.Rows(index)("Remove")
                Next
            Else
                Return
            End If
        Else
            Return
        End If
    End Sub
    Private Sub cmbListMenuHeads_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    ds = obj.Fill_DataSet("SELECT DISTINCT MenuMaster.pk_ItemID_num, MenuMaster.ItemName_vch, CategoryMaster.pk_CategoryID_num FROM RecepieMaster" & _
        '    " INNER JOIN dbo.MenuMaster ON RecepieMaster.fk_MenuId_num = MenuMaster.pk_ItemID_num INNER JOIN CategoryMaster ON MenuMaster.fk_CategoryID_num = CategoryMaster.pk_CategoryID_num")
        '    grdListMenuItems.DataSource = ds.Tables(0)

        '    grdListMenuItems.Columns(0).HeaderText = "Menu Item Id"
        '    grdListMenuItems.Columns(0).Width = 120
        '    grdListMenuItems.Columns(0).Visible = False

        '    grdListMenuItems.Columns(1).HeaderText = "Menu Item Name"
        '    grdListMenuItems.Columns(1).Width = 400

        '    grdListMenuItems.Columns(2).HeaderText = "Category Id"
        '    grdListMenuItems.Columns(2).Width = 120
        '    grdListMenuItems.Columns(2).Visible = False

        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in--> List of Menu Items")
        'End Try
    End Sub



    ''' <summary>
    ''' Adding Items for Recipe
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdItemMaster_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItemMaster.CellClick
        Try
            If e.RowIndex = -1 Or e.ColumnIndex <> 8 Then
                Return
            Else
                ''Checking Menu head and Menu Items selected or not
                If cmbMenuHeads.SelectedValue = 0 Or cmbMenuItems.SelectedValue = 0 Then
                    'If cmbMenuHeads.SelectedValue = -1 Or cmbMenuItems.SelectedValue = -1 Then
                    MsgBox("Please select Menu Head and Item above.", MsgBoxStyle.Information, gblMessageHeading)
                    Return
                Else
                    If grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Add").Value = "ADD" Then
                        'Validating Quantity and Yield Quantity Values 
                        If grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Qty").Value = "" Then
                            MsgBox("Please add some quantity and it should be non negative integer.", MsgBoxStyle.Information, gblMessageHeading)
                            grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Qty").Value = ""
                            'grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Yield_Qty").Value = ""
                            Return
                        Else
                            If grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("yield_Qty").Value = Nothing Then
                                grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Yield_Qty").Value = 0
                            End If

                            If Not IsNumeric(grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Qty").Value) OrElse grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Qty").Value <= 0 Then
                                MsgBox("Sorry! Quantity should be non negative integer.", MsgBoxStyle.Information, gblMessageHeading)
                                grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Qty").Value = Nothing
                                grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Yield_Qty").Value = Nothing
                                Return
                            End If

                            If Not IsNumeric(grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Yield_Qty").Value) OrElse grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Yield_Qty").Value < 0 Then
                                MsgBox("Sorry! Quantity should be non negative integer.", MsgBoxStyle.Information, gblMessageHeading)
                                grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Yield_Qty").Value = Nothing
                                Return
                            End If

                            Dim status As Boolean = False
                            For i As Integer = 0 To grdItemsSelected.Rows.Count - 1
                                If Convert.ToInt32(grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Item_Id").Value) = grdItemsSelected.Rows(i).Cells("Item_Id").Value Then
                                    status = True
                                    MsgBox("Selected Item already added.", MsgBoxStyle.Information, gblMessageHeading)
                                    grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Qty").Value = ""
                                    grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Yield_Qty").Value = ""
                                    Return
                                End If
                            Next

                            Dim irowindex As Integer = grdItemsSelected.Rows.Add()
                            grdItemsSelected.Rows(irowindex).Cells("Item_Id").Value = grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Item_Id").Value
                            grdItemsSelected.Rows(irowindex).Cells("Item_Code").Value = Convert.ToString(grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Item_Code").Value)
                            grdItemsSelected.Rows(irowindex).Cells("Item_Name").Value = Convert.ToString(grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Item_Name").Value)
                            grdItemsSelected.Rows(irowindex).Cells("UOM").Value = Convert.ToString(grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("UOM").Value)
                            grdItemsSelected.Rows(irowindex).Cells("Unit_Id").Value = Convert.ToUInt32(grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Unit_Id").Value)
                            grdItemsSelected.Rows(irowindex).Cells("Category_Id").Value = Convert.ToUInt32(grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Category_Id").Value)
                            grdItemsSelected.Rows(irowindex).Cells("Qty").Value = grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Qty").Value
                            grdItemsSelected.Rows(irowindex).Cells("Yield_Qty").Value = grdItemMaster.Rows(grdItemMaster.CurrentRow.Index).Cells("Yield_Qty").Value
                            grdItemsSelected.Rows(irowindex).Cells("Add").Value = ""
                            grdItemsSelected.Rows(irowindex).Cells("Remove").Value = ""

                            For index As Integer = 0 To grdItemMaster.Rows.Count - 1
                                grdItemMaster.Rows(index).Cells("Qty").Value = ""
                                grdItemMaster.Rows(index).Cells("Yield_Qty").Value = ""
                            Next
                            TabControl1.SelectTab(1)
                        End If
                    End If
                    Return
                End If
                Return
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
        End Try

    End Sub
    Private Sub grdItemsSelected_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItemsSelected.CellClick
        If grdItemsSelected.Rows(grdItemsSelected.CurrentRow.Index).Cells("Remove").Value = "" Then
            Return
        Else
            If grdItemsSelected.Rows.Count > 1 Then
                If grdItemsSelected.Rows(grdItemsSelected.CurrentRow.Index).Cells(e.ColumnIndex).Value.ToString = "REMOVE" Then
                    Dim btnremove As DialogResult
                    btnremove = MessageBox.Show("Are you sure you want to delete the selected item?", gblMessageHeading_delete, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If btnremove = Windows.Forms.DialogResult.Yes Then
                        grdItemsSelected.Rows.RemoveAt(grdItemsSelected.CurrentRow.Index)
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


    Private Sub grdRecipeMaster_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

    End Sub
    Private Sub grdListMenuItems_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdListMenuItems.CellDoubleClick
        If e.RowIndex <> -1 Then

            Dim Menu_Head_Id As Integer
            Dim Menu_Item_Id As Integer

            Menu_Item_Id = grdListMenuItems.Rows(e.RowIndex).Cells(1).Value
            Menu_Head_Id = grdListMenuItems.Rows(e.RowIndex).Cells(2).Value

            cmbMenuHeads.SelectedValue = Menu_Head_Id
            cmbMenuItems.SelectedValue = Menu_Item_Id

            cmbMenuHeads.Enabled = False
            cmbMenuItems.Enabled = False

            getRecipeDetail(Menu_Head_Id, Menu_Item_Id)
            TabControl1.SelectTab(1)
        Else
            prpty.Menu_Item_Id = 0
        End If
    End Sub

    '    '----------------------------------------------------------------
    '    ' Developed by: Yogesh Chandra Upreti
    '    '----------------------------------------------------------------    

    Private Sub txtSearchMenuItem_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchMenuItem.TextChanged
        Dim ds As New DataSet
        ds = obj.fill_Data_sets("Pro_GetRecipeMenuItemsList")
        'ds = obj.fill_Data_set("Pro_GetRecipeMenuItemsList", "@Outletid", v_the_current_division_id)
        'ds = obj.fill_Data_sets("Pro_GetRecipeMenuItemsList")
        grdListMenuItems.DataSource = ds.Tables(0)
        If txtSearchMenuItem.Text = Nothing Then
            grdListMenuItems.DataSource = ds.Tables(0)
        Else
            With grdListMenuItems
                Dim dv As New DataView(ds.Tables(0))
                dv.RowFilter = "MenuItemName like '%" & txtSearchMenuItem.Text & "%'"
                grdListMenuItems.DataSource = dv
            End With
        End If
    End Sub

    Private Sub grdListMenuItems_CellMouseEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdListMenuItems.CellMouseEnter
        If e.ColumnIndex < 0 Then
            Return
        ElseIf e.RowIndex < 0 Then
            Return
        Else

            Dim myrow As Integer = e.RowIndex

            If grdListMenuItems.Rows(myrow).Cells(3).Value = "N" Then

                grdListMenuItems.Rows(myrow).Cells(0).ToolTipText = "First Define Recipe For Menu Item " & Convert.ToString(grdListMenuItems.Rows(myrow).Cells(0).Value) + "."

            Else

                grdListMenuItems.Rows(myrow).Cells(0).ToolTipText = "Recipe Already Defined For " & Convert.ToString(grdListMenuItems.Rows(myrow).Cells(0).Value) + "."

            End If

        End If

    End Sub
End Class
