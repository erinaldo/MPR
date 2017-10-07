Imports MMSPlus.Adjustment_master
Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid

Public Class frm_Item_rate_list
    Implements IForm
    Dim _user_role As String
    Dim obj As New CommonClass
    Dim Item_obj As New item_detail.cls_item_detail
    Dim prpty As New item_detail.cls_item_detail_prop
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

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick

    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Try
            DGV_itemlist.EndEdit()

            If _rights.allow_trans = "N" Then
                RightsMsg()
                Exit Sub
            End If

            For irow As Integer = 0 To DGV_itemlist.Rows.Count - 1
                prpty.ITEM_ID = Convert.ToInt32(DGV_itemlist.Item("item_id", irow).Value)
                prpty.TRANSFER_RATE = Convert.ToDouble(DGV_itemlist.Item("item_rate", irow).Value)
                Item_obj.update_item_transfer_rate(prpty)
            Next

            MsgBox("Item Rate saved successfully.")
            new_intialization()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

    Public Sub DGV_itemlist_style()
        Try
            DGV_itemlist.Columns.Clear()
            DGV_itemlist.Rows.Clear()
            Dim txtitemid = New DataGridViewTextBoxColumn
            Dim txtitemcode = New DataGridViewTextBoxColumn
            Dim txtitemname = New DataGridViewTextBoxColumn
            Dim txtitemuom = New DataGridViewTextBoxColumn
            Dim txtitemrate = New DataGridViewTextBoxColumn

            With txtitemid
                .HeaderText = "Item Id"
                .Name = "Item_id"
                .ReadOnly = True
                .visible = False
                .width = 10
            End With
            DGV_itemlist.Columns.Add(txtitemid)

            With txtitemcode
                .HeaderText = "Item Code"
                .Name = "Item_code"
                .Readonly = True
                .visible = True
                .width = 100
            End With
            DGV_itemlist.Columns.Add(txtitemcode)

            With txtitemname
                .HeaderText = "Item Name"
                .Name = "Item_name"
                .Readonly = True
                .visible = True
                .width = 450
            End With
            DGV_itemlist.Columns.Add(txtitemname)

            With txtitemuom
                .HeaderText = "UOM"
                .Name = "Um_name"
                .Readonly = True
                .visible = True
                .width = 100
            End With
            DGV_itemlist.Columns.Add(txtitemuom)

            With txtitemrate
                .HeaderText = "Item Rate"
                .Name = "Item_rate"
                .Readonly = False
                .visible = True
                .width = 200
            End With
            DGV_itemlist.Columns.Add(txtitemrate)

            DGV_itemlist.AllowUserToResizeColumns = False
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub frm_Item_rate_list_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'obj.FormatGrid(DGV_itemlist)
        new_intialization()
    End Sub

    Private Sub new_intialization()
        clear_all()
        obj.ComboBind(ddl_category, "SELECT ITEM_CAT_Head_ID,ITEM_CAT_Head_NAME FROM dbo.ITEM_CATEGORY_HEAD_MASTER", "ITEM_CAT_Head_NAME", "ITEM_CAT_Head_ID", True)
        fillgrid()
    End Sub

    Public Sub clear_all()
        If ddl_category.Items.Count > 0 Then
            ddl_category.SelectedIndex = 0
        End If
        If ddl_subcategory.Items.Count > 0 Then
            ddl_subcategory.SelectedIndex = 0
        End If
        txt_search.Text = Nothing
        DGV_itemlist.Rows.Clear()
    End Sub

    Public Sub fillgrid()
        Dim cat_id As String
        Dim sub_cat_id As String
        Dim txt_ser As String

        If ddl_category.SelectedIndex > 0 Then
            cat_id = ddl_category.SelectedValue.ToString()
        Else
            cat_id = "%"
        End If

        If ddl_subcategory.Items.Count > 0 Then
            If ddl_subcategory.SelectedIndex > 0 Then
                sub_cat_id = ddl_subcategory.SelectedValue.ToString()
            Else
                sub_cat_id = "%"
            End If
        Else
            sub_cat_id = "%"
        End If

        If txt_search.Text = Nothing Then
            txt_ser = "%"
        Else
            txt_ser = "%" & txt_search.Text & "%"
        End If

        DGV_itemlist_style()
        Dim ds As DataSet
        ds = obj.fill_Data_set("proc_fill_grid_itemrate", "@cat_id,@sub_cat_id,@txt_search", cat_id & "," & sub_cat_id & "," & txt_ser)

        For introw As Integer = 0 To ds.Tables(0).Rows.Count - 1
            DGV_itemlist.Rows.Insert(introw)
            DGV_itemlist.Rows(introw).Cells("Item_id").Value = ds.Tables(0).Rows(introw)("Item_id").ToString()
            DGV_itemlist.Rows(introw).Cells("Item_code").Value = ds.Tables(0).Rows(introw)("Item_code").ToString()
            DGV_itemlist.Rows(introw).Cells("Item_name").Value = ds.Tables(0).Rows(introw)("Item_name").ToString()
            DGV_itemlist.Rows(introw).Cells("um_name").Value = ds.Tables(0).Rows(introw)("um_name").ToString()
            DGV_itemlist.Rows(introw).Cells("Item_rate").Value = ds.Tables(0).Rows(introw)("Item_rate").ToString()
        Next
    End Sub

    Private Sub ddl_category_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddl_category.SelectedIndexChanged
        If ddl_category.SelectedIndex = 0 Then
            If ddl_subcategory.Items.Count > 0 Then
                ddl_subcategory.SelectedIndex = 0
            End If
            ddl_subcategory.Enabled = False
        Else
            ddl_subcategory.Enabled = True
            obj.ComboBind(ddl_subcategory, "SELECT ITEM_CAT_ID,ITEM_CAT_NAME FROM dbo.ITEM_CATEGORY WHERE fk_ITEM_CAT_Head_ID = " & ddl_category.SelectedValue, "ITEM_CAT_NAME", "ITEM_CAT_ID", True)
        End If
        fillgrid()
    End Sub

    Private Sub ddl_subcategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddl_subcategory.SelectedIndexChanged
        fillgrid()
    End Sub

    Private Sub txt_search_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_search.TextChanged
        fillgrid()
    End Sub
End Class
