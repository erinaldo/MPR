Imports MMSPlus.Adjustment_master
Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid

Public Class frm_MenuItemRecipe
    Implements IForm
    Dim _user_role As String
    Dim obj As New CommonClass
    Dim objsave As New SemiFinishedItem.Cls_Semi_Finished_Item_Master
    Dim prpty As New SemiFinishedItem.Cls_Semi_Finished_Item_Master_prop
    Dim dtWastageItem As New DataTable
    Dim flag As String
    Dim rights As Form_Rights
    Dim cmd As New SqlCommand
    Dim con As New SqlConnection
    Dim Trans As SqlTransaction
    Dim iAdjustmentId As Int32
    Dim objComm As New CommonClass
    Dim iclosingid As Integer

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
        new_initilization()
        TabControl1.SelectTab(1)
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        new_initilization()
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Try
            If _rights.allow_trans = "N" Then
                RightsMsg()
                Exit Sub
            End If


            If cmbMenuHeads.SelectedValue = 0 Then
                MsgBox("Please First Select Menu Head.", MsgBoxStyle.Information, gblMessageHeading)
                Return
            ElseIf txtmenuitem.Text = "" Then
                MsgBox("Menu Item name not defined.", MsgBoxStyle.Information, gblMessageHeading)
                Return
            Else
                If flag = "save" Then
                    prpty.Menu_Item_Id = Convert.ToInt32(obj.getMaxValue("MIR_id", "Menu_Item_Recipe"))
                    prpty.Item_Name = txtmenuitem.Text
                    prpty.Menuitem_desc = txtdesc.Text
                    prpty.Category_Id = cmbMenuHeads.SelectedValue
                    prpty.creation_date = Now()
                    prpty.createdby = v_the_current_logged_in_user_name
                    prpty.modified_date = Convert.ToDateTime("01/01/1900")
                    prpty.modified_by = ""
                    objsave.insert_menuitem_Recipe(prpty)
                    MsgBox("Menu Item Saved Successfully", MsgBoxStyle.Information, gblMessageHeading)
                    new_initilization()
                Else
                    prpty.Menu_Item_Id = Convert.ToInt32(lblmenuid.Text)
                    prpty.Item_Name = txtmenuitem.Text
                    prpty.Menuitem_desc = txtdesc.Text
                    prpty.Category_Id = cmbMenuHeads.SelectedValue
                    prpty.creation_date = Now()
                    prpty.createdby = v_the_current_logged_in_user_name
                    prpty.modified_date = Now()
                    prpty.modified_by = v_the_current_logged_in_user_name
                    objsave.update_menuitem_Recipe(prpty)
                    MsgBox("Menu Item updated Successfully", MsgBoxStyle.Information, gblMessageHeading)
                    new_initilization()
                End If
            End If

            'TabControl1.SelectTab(0)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in Fill Recipe Details --> Recipe_Master")
        End Try

    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

    Private Sub frm_MenuItemRecipe_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        new_initilization()
        TabControl1.SelectTab(1)
    End Sub

    Public Sub clear_all()
        txtmenuitem.Text = Nothing
        cmbMenuHeads.SelectedIndex = 0
        txtdesc.Text = Nothing
    End Sub

    Private Sub new_initilization()
        clear_all()

        FillGridMenuItemList()
        TabControl1.SelectTab(0)
        flag = "save"
        GetItemsMenuHeads()
    End Sub

    Private Sub GetItemsMenuHeads()
        Call obj.ComboBind(cmbMenuHeads, "SELECT '0' AS pk_CategoryId_num, '-- Select Menu Head --' as CategoryName_vch UNION ALL SELECT pk_CategoryId_num, CategoryName_vch FROM CategoryMaster Order By CategoryName_vch", "CategoryName_vch", "pk_CategoryId_num", True)
    End Sub

    Private Sub FillGridMenuItemList()
        Try
            'ds = obj.fill_Data_set("Pro_GetRecipeMenuItemsList", "@Outletid", v_the_current_division_id)
            Dim ds As New DataSet

            ds = obj.fill_Data_sets("Pro_GetRecipeMenuItemsList")

            grdListMenuItems.DataSource = ds.Tables(0)

            grdListMenuItems.RowHeadersDefaultCellStyle.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

            grdListMenuItems.Columns(0).HeaderText = "Menu Item Name"
            grdListMenuItems.Columns(0).Width = 400

            grdListMenuItems.Columns(1).HeaderText = "Menu Item Id"
            grdListMenuItems.Columns(1).Width = 120
            grdListMenuItems.Columns(1).Visible = False

            grdListMenuItems.Columns(2).HeaderText = "Category Head Id"
            grdListMenuItems.Columns(2).Width = 120
            grdListMenuItems.Columns(2).Visible = False

            grdListMenuItems.Columns(2).HeaderText = "status"
            grdListMenuItems.Columns(2).Width = 120
            grdListMenuItems.Columns(2).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error in--> List of Recipe Menu Items")
        End Try

    End Sub
    
    Private Sub grdListMenuItems_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdListMenuItems.CellDoubleClick
        flag = "update"
        Dim menuitem_id As Integer
        Dim ds As New DataSet
        menuitem_id = Convert.ToInt32(grdListMenuItems.Rows(e.RowIndex).Cells("MenuItemId").Value)
        ds = obj.Fill_DataSet("SELECT (Menu_Item_Recipe.MIR_Item_name) AS MenuItemName, Menu_Item_Recipe.MIR_id AS MenuItemId,Menu_Item_Recipe.MIR_Item_cat_id AS CategoryHeadId,Menu_Item_Recipe.MIR_Item_desc as itemdesc FROM Menu_Item_Recipe WHERE  Menu_Item_Recipe.MIR_id = " & menuitem_id & "")

        If ds.Tables(0).Rows.Count > 0 Then
            lblmenuid.Text = menuitem_id
            txtmenuitem.Text = ds.Tables(0).Rows(0)("MenuItemName").ToString()
            cmbMenuHeads.SelectedValue = Convert.ToInt32(ds.Tables(0).Rows(0)("CategoryHeadId"))
            txtdesc.Text = ds.Tables(0).Rows(0)("itemdesc").ToString()
            TabControl1.SelectTab(1)
        End If
    End Sub
End Class
