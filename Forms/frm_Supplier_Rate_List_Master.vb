Imports System.Object
Imports System.Data.SqlClient
Imports System.Text
Imports MMSPlus.frm_Supplier_Rate_List_Master


Public Class frm_Supplier_Rate_List_Master

    Implements IForm
    Dim obj As New CommonClass
    Dim clsObj As New supplier_rate_list.cls_supplier_rate_list
    Dim prpty As New supplier_rate_list.cls_supplier_rate_list_prop
    Dim flag As String
    Dim Ds As DataSet
    Dim cmbItems As ComboBox
    Dim SRL_ID As Integer
    Dim Dt As DataTable
    Dim qry As String
    Dim grdSupplier_Rowindex As Int16
    Dim int_ColumnIndex As Integer
    Dim txtQuanity As TextBox
    Dim int_RowIndex As Integer
    Dim _rights As Form_Rights
    Dim source1 As New BindingSource()

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Enum enmGrdSupplier
        supId = 0
        ItemId = 1
        ItemCode = 2
        ItemName = 3
        UOM = 4
        Rate = 5
        DelQty = 6
        DelDays = 7
    End Enum

    Private Sub frm_Supplier_Rate_List_Master_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            comboBind()
            flag = "save"
            'obj.FormatGrid(grdSupplierList)
            FillGrid()
            obj.ComboBind(cmbSupplier, "select 0 as ACC_ID,'--Select--' as ACC_NAME union Select ACC_ID,ACC_NAME from ACCOUNT_MASTER WHERE AG_ID in (1,2,3,6) Order by ACC_NAME ", "ACC_NAME", "ACC_ID")
            grid_style()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick
        ' dont delete  this sub procedure it is  in use
    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick
        ' dont delete  this sub procedure it is in use
    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            grid_style()
            FillGrid()
            obj.Clear_All_TextBox(Me.GroupBox1.Controls)
            obj.Clear_All_ComoBox(Me.GroupBox1.Controls)
            flag = "save"
            TabControl1.SelectTab(1)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        Try
            FillGrid()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error refreshClick --> frm_Supplier_master")
        End Try
    End Sub

    Private Function Validation() As Boolean
        Validation = True
        If Trim(txtSupName.Text) = "" Then
            MsgBox("Rate List Name should not be blank. ", vbExclamation, gblMessageHeading)
            txtSupName.Focus()
            Validation = False
            Exit Function
        End If
        If cmbSupplier.SelectedIndex <= 0 Then
            MsgBox("Select supplier name.", vbExclamation, gblMessageHeading)
            cmbSupplier.Focus()
            Validation = False
            Exit Function
        End If

        If Chk_DuplicateRateListName() = False Then
            MsgBox("Name Already Exists,Please Give Another Name", vbExclamation, gblMessageHeading)

            txtSupName.Focus()
            Validation = False
            Exit Function
        End If
    End Function

    Private Function Chk_DuplicateRateListName() As Boolean
        Dim qry As String
        Chk_DuplicateRateListName = True
        If flag = "Save" Then
            qry = "SELECT * FROM SUPPLIER_RATE_LIST WHERE SRL_NAME ='" & Trim(txtSupName.Text) & "'"
        Else
            qry = "SELECT * FROM SUPPLIER_RATE_LIST " &
                " WHERE " &
                        " SRL_NAME ='" & Trim(txtSupName.Text) & "'" &
                        " and SRL_Id<>" & SRL_ID

        End If

        Dim ds As DataSet = obj.FillDataSet(qry)
        If ds.Tables(0).Rows.Count > 0 Then
            Chk_DuplicateRateListName = False

        End If
        ds.Clear()
        ds.Dispose()
    End Function

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

        Dim cmd As SqlCommand
        lblFormHeading.Focus()
        If Validation() = False Then
            Exit Sub
        End If
        If grdSupplier.Rows.Count >= 1 And Not grdSupplier.Rows(0).Cells(1).Value = Trim("") Then
            Dim stringArr() As String = New String() {"Rate"}
            If obj.ValidatingDGV(grdSupplier, stringArr) = False Then
                MsgBox("Please Enter All The Records In Datagrid", vbExclamation, gblMessageHeading)
                Exit Sub
            End If

            cmd = obj.MyCon_BeginTransaction
            Try
                lblFormHeading.Focus()
                If flag = "save" Then
                    Dim int_MaxSRL_Id As Integer

                    int_MaxSRL_Id = Convert.ToInt32(obj.getMaxValue("SRL_ID", "supplier_rate_list"))
                    prpty.SRL_ID = Convert.ToInt32(int_MaxSRL_Id)
                    prpty.SRL_NAME = txtSupName.Text
                    prpty.SRL_DATE = Convert.ToDateTime(dtpDate.Text).ToString()
                    prpty.SRL_DESC = txtDesc.Text
                    prpty.SUPP_ID = cmbSupplier.SelectedValue
                    If CheckBox1.Checked Then
                        prpty.ACTIVE = True
                    Else
                        prpty.ACTIVE = False
                    End If
                    prpty.CREATED_BY = v_the_current_logged_in_user_name
                    prpty.CREATION_DATE = Now
                    prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                    prpty.MODIFIED_DATE = Now
                    prpty.DIVISION_ID = v_the_current_division_id
                    clsObj.insert_SUPPLIER_RATE_LIST(prpty, cmd)

                    Dim iRowCount As Int32
                    Dim iRow As Int32
                    iRowCount = grdSupplier.RowCount - 1
                    For iRow = 0 To iRowCount - 1
                        prpty.SRL_ID = Convert.ToInt32(int_MaxSRL_Id)
                        prpty.ITEM_ID = Convert.ToInt32(grdSupplier.Item("ITEM_ID", iRow).Value)
                        prpty.ITEM_RATE = Convert.ToDouble(grdSupplier.Item("Rate", iRow).Value)
                        prpty.DEL_QTY = 1
                        prpty.DEL_DAYS = 1
                        prpty.CREATED_BY = Convert.ToString(v_the_current_logged_in_user_name)
                        prpty.CREATION_DATE = Convert.ToDateTime(Now)
                        prpty.MODIFIED_BY = Convert.ToString(v_the_current_logged_in_user_name)
                        prpty.MODIFIED_DATE = Convert.ToDateTime(Now)
                        prpty.DIVISION_ID = Convert.ToInt32(v_the_current_division_id)
                        clsObj.insert_SUPPLIER_DETAIL_RATE_LIST(prpty, cmd)
                    Next iRow


                    MsgBox("Rate list infromation Saved.", MsgBoxStyle.Information, gblMessageHeading)

                    grid_style()

                    obj.Clear_All_TextBox(Me.GroupBox1.Controls)
                    obj.Clear_All_ComoBox(Me.GroupBox1.Controls)
                    TabControl1.SelectTab(1)
                Else

                    prpty.SRL_ID = Convert.ToInt32(SRL_ID)
                    prpty.SRL_NAME = txtSupName.Text
                    prpty.SRL_DATE = Convert.ToDateTime(dtpDate.Text).ToString()
                    prpty.SRL_DESC = txtDesc.Text
                    prpty.SUPP_ID = cmbSupplier.SelectedValue
                    If CheckBox1.Checked Then
                        prpty.ACTIVE = True
                    Else
                        prpty.ACTIVE = False
                    End If
                    prpty.CREATED_BY = v_the_current_logged_in_user_name
                    prpty.CREATION_DATE = Now
                    prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                    prpty.MODIFIED_DATE = Now
                    prpty.DIVISION_ID = v_the_current_division_id
                    clsObj.update_SUPPLIER_RATE_LIST(prpty, cmd)





                    prpty.SRL_ID = SRL_ID
                    prpty.DIVISION_ID = -1
                    prpty.ITEM_ID = -1
                    prpty.ITEM_RATE = -1
                    prpty.DEL_QTY = -1
                    prpty.DEL_DAYS = -1
                    prpty.CREATED_BY = "-1"
                    prpty.CREATION_DATE = Now
                    prpty.MODIFIED_BY = "-1"
                    prpty.MODIFIED_DATE = Now
                    clsObj.Delete_SUPPLIER_DETAIL_RATE_LIST(prpty, cmd)


                    ' add modified rate list detail record
                    Dim iRowCount As Int32
                    Dim iRow As Int32
                    iRowCount = grdSupplier.RowCount - 1
                    For iRow = 0 To iRowCount - 1

                        prpty.SRL_ID = Convert.ToInt32(SRL_ID)
                        prpty.ITEM_ID = Convert.ToInt32(grdSupplier.Item("ITEM_ID", iRow).Value)
                        prpty.ITEM_RATE = Convert.ToDouble(grdSupplier.Item("Rate", iRow).Value)
                        prpty.DEL_QTY = 1
                        prpty.DEL_DAYS = 1
                        prpty.CREATED_BY = Convert.ToString(v_the_current_logged_in_user_name)
                        prpty.CREATION_DATE = Convert.ToDateTime(Now)
                        prpty.MODIFIED_BY = Convert.ToString(v_the_current_logged_in_user_name)
                        prpty.MODIFIED_DATE = Convert.ToDateTime(Now)
                        prpty.DIVISION_ID = Convert.ToInt32(v_the_current_division_id)
                        clsObj.insert_SUPPLIER_DETAIL_RATE_LIST(prpty, cmd)
                    Next iRow

                    MsgBox("Record Update", MsgBoxStyle.Information, gblMessageHeading)
                    obj.Clear_All_TextBox(Me.GroupBox1.Controls)
                    obj.Clear_All_ComoBox(Me.GroupBox1.Controls)
                    TabControl1.SelectTab(1)
                End If
                'End If
                obj.MyCon_CommitTransaction(cmd)
                FillGrid()

                'TabControl1.SelectTab(1)

            Catch ex As Exception
                obj.MyCon_RollBackTransaction(cmd)
                MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
            End Try
        Else
            MsgBox("Please add atleast one item in rate list", vbExclamation, gblMessageHeading)
        End If
    End Sub
    'dont delete this sub procedure it is in use
    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        'Try
        '    If TabControl1.SelectedIndex = 0 Then
        '        obj.RptShow(enmReportName.RptSupplierRateList, "SRL_ID", CStr(grdSupplierList("SRL_ID", grdSupplierList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
        '        'Else
        '        'ElseIf TabControl1.SelectedIndex = 1 Then
        '        '    obj.RptShow(enmReportName.RptSupplierRateList, "SRL_ID", CStr(grdSupplier("SRL_ID", grdSupplier.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
        '    Else
        '        If flag <> "save" Then
        '            '    obj.RptShow(enmReportName.RptOpenPurchaseOrderPrint, "PO_ID", CStr(_po_id), CStr(enmDataType.D_int))
        '            obj.RptShow(enmReportName.RptSupplierRateList, "SRL_ID", CStr(grdSupplier("SRL_ID", grdSupplier.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
        '        End If
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try


        Try
            If grdSupplier.Rows.Count > 0 Then
                obj.RptShow(enmReportName.RptSupplierRateList, "SRL_ID", CStr(grdSupplierList("SRL_ID", grdSupplierList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                MsgBox("No Records To Print", MsgBoxStyle.Information, gblMessageHeading)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub grid_style()
        Dim txbCol As DataGridViewTextBoxColumn
        Dim cmbCol As New DataGridViewComboBoxColumn

        grdSupplier.Columns.Clear()

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Sup Id"
            .Name = "Sup_Id"
            .ReadOnly = True
            .Visible = False
            .Width = 100
            .DefaultCellStyle.SelectionBackColor = Color.Orange
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        grdSupplier.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Id"
            .Name = "Item_Id"
            .DataPropertyName = "Item_Id"
            .ReadOnly = True
            .Visible = False
            .Width = 100
            .DefaultCellStyle.SelectionBackColor = Color.Orange
            .DefaultCellStyle.SelectionForeColor = Color.Black

        End With
        grdSupplier.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "BarCode"
            .Name = "Item_Code"
            .DataPropertyName = "Item_Code"
            .ReadOnly = True
            .Visible = True
            .Width = 100
            .DefaultCellStyle.SelectionBackColor = Color.Orange
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With

        grdSupplier.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Item Name"
            .Name = "Item_Name"
            .ReadOnly = True
            .Visible = True
            .Width = 400
            .DefaultCellStyle.SelectionBackColor = Color.Orange
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        grdSupplier.Columns.Add(txbCol)



        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "UOM"
            .Name = "UOM"
            .ReadOnly = True
            .Visible = True
            .Width = 90
            .DefaultCellStyle.SelectionBackColor = Color.Orange
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        grdSupplier.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "GST"
            .Name = "GST"
            .ReadOnly = True
            .Visible = True
            .Width = 70
            .DefaultCellStyle.SelectionBackColor = Color.Orange
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        grdSupplier.Columns.Add(txbCol)

        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Base Rate"
            .Name = "Rate"
            .ReadOnly = True
            .Visible = True
            .Width = 100
            .DefaultCellStyle.SelectionBackColor = Color.Orange
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        grdSupplier.Columns.Add(txbCol)


        txbCol = New DataGridViewTextBoxColumn
        With txbCol
            .HeaderText = "Rate"
            .Name = "Selling_Rate"
            .ReadOnly = False
            .Visible = True
            .Width = 100
            .DefaultCellStyle.SelectionBackColor = Color.Lime
            .DefaultCellStyle.SelectionForeColor = Color.Black


        End With
        grdSupplier.Columns.Add(txbCol)




        Dim cRate As DataGridViewTextBoxColumn = TryCast(Me.grdSupplier.Columns("Rate"), DataGridViewTextBoxColumn)
        cRate.MaxInputLength = 25


    End Sub

    Private Sub grdSupplier_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdSupplier.EditingControlShowing
        Try

            'grdSupplier.CurrentCell = grdSupplier.Item("Selling_Rate", grdSupplier_Rowindex)

            If int_ColumnIndex = enmGrdSupplier.Rate Or int_ColumnIndex = enmGrdSupplier.DelQty Or int_ColumnIndex = enmGrdSupplier.DelDays Then
                AddHandler e.Control.KeyPress, AddressOf obj.Valid_NumberGrid
                txtQuanity = TryCast(e.Control, TextBox)
                cmbItems = TryCast(e.Control, ComboBox)
                If cmbItems IsNot Nothing Then
                    AddHandler cmbItems.SelectedIndexChanged, AddressOf cmbItems_SelectedIndexChanged
                End If
                If txtQuanity IsNot Nothing Then
                    AddHandler txtQuanity.KeyDown, AddressOf txtQuanity_KeyDown

                End If

            End If
        Catch ex As Exception

            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub
    ' dont delete this subprocedure it is in use
    Private Sub txtQuanity_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    Private Sub cmbItems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim drv As DataRowView
        Dim IsInsert As Boolean
        drv = TryCast(TryCast(sender, ComboBox).SelectedItem, DataRowView)
        If drv IsNot Nothing Then
            Dim iRowCount As Int32
            Dim iRow As Int32
            iRowCount = grdSupplier.RowCount
            IsInsert = True
            For iRow = 0 To iRowCount - 3
                If grdSupplier.Item(0, iRow).Value = Convert.ToInt32(drv(0)) Then
                    MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, "Supplier Item")
                    IsInsert = False
                    Exit For
                End If
            Next iRow
            If IsInsert = True Then
                grdSupplier.Rows(grdSupplier.CurrentCell.RowIndex).Cells(0).Value = cmbSupplier.SelectedValue
                grdSupplier.Rows(grdSupplier.CurrentCell.RowIndex).Cells(1).Value = drv(1)
                grdSupplier.Rows(grdSupplier.CurrentCell.RowIndex).Cells(3).Value = drv(4)
            End If
        End If
    End Sub

    Private Sub grdSupplier_CellLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSupplier.CellLeave
        If cmbItems IsNot Nothing Then
            RemoveHandler cmbItems.SelectedIndexChanged, AddressOf cmbItems_SelectedIndexChanged
            cmbItems = Nothing
        End If
    End Sub

    Private Sub comboBind()
        obj.ComboBind(cmbSuppSrch, "select 0 as srl_id,'All' as Srl_name union Select srl_id,srl_name from supplier_rate_list WHERE SUPP_ID IN(SELECT ACC_ID FROM dbo.ACCOUNT_MASTER WHERE AG_ID in (1,2,3,6)) AND SRL_ID NOT IN(SELECT SRL_ID FROM dbo.CUSTOMER_RATE_LIST_MAPPING) order by srl_name", "srl_name", "srl_id")
    End Sub

    Private Sub FillGrid()
        Try
            'obj.GridBind(grdSupplierList, "SELECT srl_id,srl_name,srl_desc,active,creation_date from Supplier_rate_list")
            obj.GridBind(grdSupplierList, "SELECT SUPPLIER_RATE_LIST.SRL_ID,SUPPLIER_RATE_LIST.SRL_NAME,SUPPLIER_RATE_LIST.SRL_DATE,SUPPLIER_RATE_LIST.SRL_DESC,SUPPLIER_RATE_LIST.ACTIVE,ACCOUNT_MASTER.ACC_NAME,ACCOUNT_MASTER.ACC_ID FROM SUPPLIER_RATE_LIST INNER JOIN ACCOUNT_MASTER ON SUPPLIER_RATE_LIST.SUPP_ID = ACCOUNT_MASTER.ACC_ID WHERE AG_ID in (1,2,3,6) AND SRL_ID NOT IN(SELECT SRL_ID FROM dbo.CUSTOMER_RATE_LIST_MAPPING) order by SUPPLIER_RATE_LIST.SRL_NAME")
            grdSupplierList.Width = 860
            grdSupplierList.Columns(0).Visible = False 'Supplier id
            grdSupplierList.Columns(0).Width = 300
            grdSupplierList.Columns(0).HeaderText = "SRL_ID"
            grdSupplierList.Columns(1).Width = 193
            grdSupplierList.Columns(1).HeaderText = "Item Rate List"
            grdSupplierList.Columns(2).HeaderText = "Active Date"
            grdSupplierList.Columns(3).Width = 140
            grdSupplierList.Columns(3).HeaderText = "Rate List Desc."
            grdSupplierList.Columns(4).HeaderText = "Active"
            grdSupplierList.Columns(5).Width = 300
            grdSupplierList.Columns(5).HeaderText = "Supplier Name"
            grdSupplierList.Columns(6).Visible = False
            grdSupplierList.Columns(6).HeaderText = "ACC ID"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
        End Try
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        Try
            Dim IsBol As Boolean
            If chkActive.Checked = True Then
                IsBol = chkActive.Checked
                obj.GridBind(grdSupplierList, "SELECT SRL_ID,SRL_NAME,SRL_DESC,ACTIVE,SRL_date,ACCOUNT_MASTER.ACC_NAME,ACCOUNT_MASTER.ACC_ID FROM SUPPLIER_RATE_LIST INNER JOIN ACCOUNT_MASTER ON SUPPLIER_RATE_LIST.SUPP_ID = ACCOUNT_MASTER.ACC_ID where AG_ID in (1,2,3,6) and active = '" & IsBol & "'")
            Else
                obj.GridBind(grdSupplierList, "SELECT SRL_ID,SRL_NAME,SRL_DESC,ACTIVE,SRL_date,ACCOUNT_MASTER.ACC_NAME,ACCOUNT_MASTER.ACC_ID FROM SUPPLIER_RATE_LIST INNER JOIN ACCOUNT_MASTER ON SUPPLIER_RATE_LIST.SUPP_ID = ACCOUNT_MASTER.ACC_ID where AG_ID in (1,2,3,6)")
                ' where active = '" & IsBol & "'")
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSuppSrch.SelectedIndexChanged
        Try
            If cmbSuppSrch.SelectedValue = 0 Then
                obj.GridBind(grdSupplierList, "SELECT SRL_ID,SRL_NAME,SRL_DESC,ACTIVE,SRL_date,ACCOUNT_MASTER.ACC_NAME,ACCOUNT_MASTER.ACC_ID FROM SUPPLIER_RATE_LIST INNER JOIN ACCOUNT_MASTER ON SUPPLIER_RATE_LIST.SUPP_ID = ACCOUNT_MASTER.ACC_ID where AG_ID in (1,2,3,6) AND SRL_ID NOT IN(SELECT SRL_ID FROM dbo.CUSTOMER_RATE_LIST_MAPPING)")
            Else
                obj.GridBind(grdSupplierList, "SELECT SRL_ID,SRL_NAME,SRL_DESC,ACTIVE,SRL_date,ACCOUNT_MASTER.ACC_NAME,ACCOUNT_MASTER.ACC_ID FROM SUPPLIER_RATE_LIST INNER JOIN ACCOUNT_MASTER ON SUPPLIER_RATE_LIST.SUPP_ID = ACCOUNT_MASTER.ACC_ID where AG_ID in (1,2,3,6) AND SRL_ID NOT IN(SELECT SRL_ID FROM dbo.CUSTOMER_RATE_LIST_MAPPING) and SRL_Id=" & cmbSuppSrch.SelectedValue)
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub getSupplierDetail(ByVal SRL_ID As Integer)
        Dim dtSupplier As New DataTable
        Dim dtSupplierDetail As New DataTable
        flag = "update"
        Ds = obj.fill_Data_set("GET_SUPPLIER_TOTAL_DETAIL", "@V_SRL_ID", SRL_ID)

        If Ds.Tables.Count > 0 Then

            dtSupplier = Ds.Tables(0)
            txtSupName.Text = Convert.ToString(dtSupplier.Rows(0)("SRL_NAME"))
            dtpDate.Text = Convert.ToString(dtSupplier.Rows(0)("SRL_DATE"))
            cmbSupplier.SelectedValue = Convert.ToString(dtSupplier.Rows(0)("SUPP_ID"))
            If dtSupplier.Rows(0)("Active") = False Then
                CheckBox1.Checked = False
            Else
                CheckBox1.Checked = True
            End If
            txtDesc.Text = Convert.ToString(dtSupplier.Rows(0)("SRL_DESC"))

            grid_style()
            dtSupplierDetail = Ds.Tables(1)
            Dim iRowCount As Int32
            Dim iRow As Int32
            iRowCount = dtSupplierDetail.Rows.Count
            For iRow = 0 To iRowCount - 1
                If dtSupplierDetail.Rows.Count > 0 Then
                    Dim rowindex As Integer = grdSupplier.Rows.Add()
                    grdSupplier.Rows(rowindex).Cells("Sup_Id").Value = Convert.ToInt32(dtSupplierDetail.Rows(iRow)("SUPP_ID"))
                    grdSupplier.Rows(rowindex).Cells("Item_Id").Value = Convert.ToString(dtSupplierDetail.Rows(iRow)("Item_Id"))
                    grdSupplier.Rows(rowindex).Cells("Item_Code").Value = Convert.ToString(dtSupplierDetail.Rows(iRow)("Item_Code"))
                    grdSupplier.Rows(rowindex).Cells("Item_Name").Value = Convert.ToString(dtSupplierDetail.Rows(iRow)("Item_Name"))
                    grdSupplier.Rows(rowindex).Cells("UOM").Value = Convert.ToString(dtSupplierDetail.Rows(iRow)("UM_Name"))
                    Dim gst As Decimal = Convert.ToDouble(dtSupplierDetail.Rows(iRow)("VAT_PERCENTAGE"))
                    Dim baseRate As Decimal = Convert.ToDouble(dtSupplierDetail.Rows(iRow)("ITEM_RATE"))
                    grdSupplier.Rows(rowindex).Cells("GST").Value = gst
                    grdSupplier.Rows(rowindex).Cells("Rate").Value = baseRate
                    grdSupplier.Rows(rowindex).Cells("Selling_rate").Value = baseRate + Math.Round((baseRate * (gst / 100)), 2)
                    grdSupplier.Rows(rowindex).Cells("Selling_rate").Style.BackColor = Color.LimeGreen
                    grdSupplier.Rows(rowindex).Cells("Selling_rate").Style.ForeColor = Color.Black


                End If
            Next iRow
            TabControl1.SelectTab(1)
        End If
    End Sub

    Private Sub grdSupplier_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdSupplier.DataError
        e.ThrowException = False
    End Sub

    Sub NumericValuegrd_supplier(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Dim colindex As Decimal = grdSupplier.CurrentCell.ColumnIndex
        If colindex = 4 Then
            Select Case Asc(e.KeyChar)
                Case AscW(ControlChars.Cr) 'Enter key
                    e.Handled = True
                Case AscW(ControlChars.Back) 'Backspace
                Case 45, 46, 48 To 57 'Negative sign, Decimal and Numbers
                Case Else ' Everything else
                    e.Handled = True
            End Select
        ElseIf colindex = 5 Then
            Select Case Asc(e.KeyChar)
                Case AscW(ControlChars.Cr) 'Enter key
                    e.Handled = True
                Case AscW(ControlChars.Back) 'Backspace
                Case 45, 46, 48 To 57 'Negative sign, Decimal and Numbers
                Case Else ' Everything else
                    e.Handled = True
            End Select

        ElseIf colindex = 6 Then
            Select Case Asc(e.KeyChar)
                Case AscW(ControlChars.Cr) 'Enter key
                    e.Handled = True
                Case AscW(ControlChars.Back) 'Backspace
                Case 45, 46, 48 To 57 'Negative sign, Decimal and Numbers
                Case Else ' Everything else
                    e.Handled = True
            End Select
        End If
    End Sub

    Private Sub grdSupplier_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSupplier.KeyDown
        Try
            If e.KeyCode = Keys.Space Then

                grdSupplier_Rowindex = grdSupplier.CurrentRow.Index
                'If int_ColumnIndex = 3 Then

                'frm_Show_search.qry = " SELECT " & _
                '                            " ITEM_MASTER.ITEM_ID, " & _
                '                            " ITEM_MASTER.ITEM_CODE, " & _
                '                            " ITEM_MASTER.ITEM_NAME, " & _
                '                            " ITEM_MASTER.ITEM_DESC, " & _
                '                            " UNIT_MASTER.UM_Name, " & _
                '                            " ITEM_CATEGORY.ITEM_CAT_NAME " & _
                '                    " FROM " & _
                '                            " ITEM_MASTER " & _
                '                            " INNER JOIN UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID " & _
                '                            " INNER JOIN ITEM_CATEGORY ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID " & _
                '                             "INNER JOIN ITEM_DETAIL ON ITEM_MASTER.ITEM_ID = ITEM_DETAIL.ITEM_ID "

                'frm_Show_search.column_name = "Item_Name"
                'frm_Show_search.extra_condition = ""
                'frm_Show_search.ret_column = "Item_ID"
                'frm_Show_search.item_rate_column = ""
                'frm_Show_search.cols_no_for_width = "1,2,3"
                'frm_Show_search.cols_width = "60,350,50"

                'frm_Show_search.ShowDialog()


                frm_Show_Search_RateList.qry = " SELECT  top 50                                         
                                        ISNULL(im.BarCode_vch, '') AS BARCODE ,
                                        im.ITEM_NAME AS [ITEM NAME] ,
                                        CAST(im.MRP_Num AS NUMERIC(18, 2)) AS MRP ,
                                        CAST(im.sale_rate AS NUMERIC(18, 2)) AS RATE ,
                                        ISNULL(litems.LabelItemName_vch, '') AS BRAND ,
                                        ic.ITEM_CAT_NAME AS CATEGORY,
                                        im.ITEM_ID
                                        FROM  Item_master im
                                        LEFT OUTER JOIN item_detail id ON im.item_id = id.item_id
                                        LEFT OUTER JOIN dbo.ITEM_CATEGORY ic ON im.ITEM_CATEGORY_ID = ic.ITEM_CAT_ID
                                        LEFT OUTER JOIN dbo.LabelItem_Mapping lim ON lim.Fk_ItemId_Num = im.ITEM_ID
                                        LEFT OUTER JOIN dbo.Label_Items litems ON lim.Fk_LabelDetailId = litems.Pk_LabelDetailId_Num
                                        WHERE id.Is_active = 1 "

                'frm_Show_Search_RateList.checkbox_column_name = "chkBxSelect"
                frm_Show_Search_RateList.column_name = "BARCODE_VCH"
                frm_Show_Search_RateList.column_name1 = "ITEM_NAME"
                frm_Show_Search_RateList.column_name2 = "MRP_Num"
                frm_Show_Search_RateList.column_name3 = "SALE_RATE"
                frm_Show_Search_RateList.column_name4 = "LABELITEMNAME_VCH"
                frm_Show_Search_RateList.column_name5 = "ITEM_CAT_NAME"
                frm_Show_Search_RateList.cols_no_for_width = "1,2,3,4,5,6"
                frm_Show_Search_RateList.cols_width = "100,320,70,70,100,105"
                frm_Show_Search_RateList.extra_condition = ""
                frm_Show_Search_RateList.ret_column = "ITEM_ID"
                frm_Show_Search_RateList.item_rate_column = ""
                frm_Show_Search_RateList.ShowDialog()

                get_row(frm_Show_Search_RateList.search_result)
                frm_Show_Search_RateList.Close()

                'End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message())
        End Try
    End Sub

    Public Sub get_row(ByVal item_id As String)

        If item_id <> -1 Then
            Dim stringSeparators() As String = {","}
            Dim result() As String
            result = item_id.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries)

            Dim IsInsert As Boolean
            Dim ds As DataSet

            For Each element As String In result
                ds = obj.fill_Data_set("GET_ITEM_BY_ID", "@V_ITEM_ID", element)
                Dim iRowCount As Int32
                Dim iRow As Int32
                iRowCount = grdSupplier.RowCount
                IsInsert = True

                For iRow = 0 To iRowCount - 2

                    If Trim(grdSupplier.Item("Item_Code", iRow).Value) = Trim(ds.Tables(0).Rows(0)(1)) Then
                        MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
                        IsInsert = False
                        Exit For
                    End If
                Next iRow
                Dim datatbl As New DataTable
                datatbl = ds.Tables(0)


                If IsInsert = True Then
                    Dim introw As Integer

                    introw = grdSupplier.Rows.Count - 1
                    grdSupplier.Rows.Insert(introw)
                    grdSupplier.Rows(introw).Cells("Item_ID").Value = ds.Tables(0).Rows(0)(0)
                    grdSupplier.Rows(introw).Cells("Item_CODE").Value = ds.Tables(0).Rows(0)("item_Code").ToString()
                    grdSupplier.Rows(introw).Cells("Item_Name").Value = ds.Tables(0).Rows(0)("item_Name").ToString()

                    grdSupplier.Rows(introw).Cells("UOM").Value = ds.Tables(0).Rows(0)("UM_NAME").ToString()
                    grdSupplier.Rows(introw).Cells("gst").Value = ds.Tables(0).Rows(0)("VAT_PERCENTAGE").ToString()
                    grdSupplier.Rows(introw).Cells("rate").Value = 0
                    grdSupplier.Rows(introw).Cells("Selling_Rate").Value = "0.00"

                    grdSupplier.CurrentCell = grdSupplier.Rows(introw).Cells("Selling_Rate")
                    grdSupplier.CurrentCell.Style.BackColor = Color.LimeGreen
                    grdSupplier.CurrentCell.Style.ForeColor = Color.Black

                    grdSupplier.BeginEdit(True)


                End If
            Next

            'Dim dtCustomers As New DataTable("Item")

            'dtCustomers.Columns.Add("Item_ID", GetType(String))
            'dtCustomers.Columns.Add("Item_CODE", GetType(String))
            'dtCustomers.Columns.Add("Item_Name", GetType(String))
            'dtCustomers.Columns.Add("UOM", GetType(String))
            'dtCustomers.Columns.Add("gst", GetType(String))
            'dtCustomers.Columns.Add("rate", GetType(String))
            'dtCustomers.Columns.Add("Selling_Rate", GetType(String))


            'For Each row As DataGridViewRow In grdSupplier.Rows
            '    dtCustomers.Rows.Add()
            '    For i As Integer = 0 To row.Cells.Count - 1
            '        If row.Cells(i).Value <> Nothing Then
            '            dtCustomers.Rows(row.Index)(i - 1) = row.Cells(i).Value
            '        End If


            '    Next
            'Next

            'grdSupplier.DataSource = dtCustomers
            'source1.DataSource = dtCustomers

        End If

    End Sub

    Private Sub grdSupplier_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSupplier.CellEnter
        int_ColumnIndex = e.ColumnIndex
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        obj.GridBind(grdSupplierList, "SELECT SUPPLIER_RATE_LIST.SRL_ID,SUPPLIER_RATE_LIST.SRL_NAME,SUPPLIER_RATE_LIST.SRL_DATE,SUPPLIER_RATE_LIST.SRL_DESC,SUPPLIER_RATE_LIST.ACTIVE,ACCOUNT_MASTER.ACC_NAME,ACCOUNT_MASTER.ACC_ID FROM SUPPLIER_RATE_LIST INNER JOIN ACCOUNT_MASTER ON SUPPLIER_RATE_LIST.SUPP_ID = ACCOUNT_MASTER.ACC_ID WHERE AG_ID in (1,2,3,6) AND SRL_ID NOT IN(SELECT SRL_ID FROM dbo.CUSTOMER_RATE_LIST_MAPPING) and (dbo.SUPPLIER_RATE_LIST.SRL_NAME + CONVERT(VARCHAR,SUPPLIER_RATE_LIST.SRL_DATE,101) + dbo.SUPPLIER_RATE_LIST.SRL_DESC + dbo.ACCOUNT_MASTER.ACC_NAME) LIKE '%" & Trim(txtSearch.Text) & "%'")
    End Sub

    Private Sub grdSupplier_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grdSupplier.CellEndEdit, grdSupplierList.CellEndEdit
        Dim value As String = grdSupplier.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        If Not String.IsNullOrEmpty(value) Then
            Dim sellingPrice As Decimal = value
            Dim gst As Decimal = grdSupplier.Rows(e.RowIndex).Cells("gst").Value
            grdSupplier.Rows(e.RowIndex).Cells("rate").Value = Math.Round(sellingPrice / (1 + (gst / 100)), 2)
        End If
    End Sub

    Private Sub grdSupplierList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdSupplierList.CellContentClick
        'grdSupplierList.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub grdSupplierList_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSupplierList.CellDoubleClick
        If _rights.allow_edit = "N" Then
            RightsMsg()
            Exit Sub
        End If

        If e.RowIndex <> -1 Then
            SRL_ID = grdSupplierList.Rows(e.RowIndex).Cells("SRL_ID").Value
            getSupplierDetail(SRL_ID)
            TabControl1.SelectTab(1)
        Else
            SRL_ID = 0
        End If
    End Sub

    Private Sub grdSupplierList_KeyDown(sender As Object, e As KeyEventArgs) Handles grdSupplierList.KeyDown


        If e.KeyCode = Keys.Enter Then
            If _rights.allow_edit = "N" Then
                RightsMsg()
                Exit Sub
            End If
            If grdSupplierList.SelectedRows.Count > 0 Then
                SRL_ID = grdSupplierList.SelectedRows(0).Cells("SRL_ID").Value
                getSupplierDetail(SRL_ID)
                TabControl1.SelectTab(1)
            Else
                SRL_ID = 0
            End If
        End If
    End Sub

    Private Sub txtBarcodeSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBarcodeSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrEmpty(txtBarcodeSearch.Text) Then

                Dim qry As String = "SELECT  im.item_id  FROM  Item_master im  LEFT OUTER JOIN item_detail id ON im.item_id = id.item_id  WHERE id.Is_active = 1 and    Barcode_vch = '" + txtBarcodeSearch.Text + "'"
                Dim id As Int32 = clsObj.ExecuteScalar(qry)
                If id > 0 Then
                    get_row(id)

                End If
                txtBarcodeSearch.Text = ""
                txtBarcodeSearch.Focus()
            End If
        End If
    End Sub

    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged

        'If txt_search.Text = "" Then
        '    source1.Filter = ""
        'Else
        '    source1.Filter = "[Item_CODE] like '%" & txt_search.Text & "%'  OR [Item_name] like '%" & txt_search.Text & "%' "
        'End If

        'grdSupplier.Refresh()

        '((DataTable)datagridview1.DataSource)).DefaultView.RowFilter = string.Format("name LIKE '*{0}*'",txt_search.Text)

    End Sub

    Private Sub cmbSuppSrch_Enter(sender As Object, e As EventArgs) Handles cmbSuppSrch.Enter
        If Not cmbSuppSrch.DroppedDown Then
            cmbSuppSrch.DroppedDown = True
        End If
    End Sub

    Private Sub cmbSupplier_Enter(sender As Object, e As EventArgs) Handles cmbSupplier.Enter
        If Not cmbSupplier.DroppedDown Then
            cmbSupplier.DroppedDown = True
        End If
    End Sub
End Class
