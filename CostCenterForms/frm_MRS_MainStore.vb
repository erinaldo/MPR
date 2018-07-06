Imports MMSPlus.mrs_main_store_master
Imports System.Data.SqlClient

Public Class frm_MRS_MainStore
    Implements IForm
    Dim _user_role As String
    Dim obj As New CommonClass
    Dim clsObj As New cls_mrs_main_store_master
    Dim prpty As cls_mrs_main_store_master_prop
    Dim flag As String

    Dim CostCenter_Code As String
    Dim clsobjItem As New item_detail.cls_item_detail
    Dim cmbItems As ComboBox
    Dim txtQutity As TextBox
    Dim iMRSId As Int32
    Dim DGVMRSItem_Rowindex As Int16
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
        Try
            TBC_MRS_Master.SelectTab(1)
            DGVMRSItem_style()
            flag = "save"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_MRS_Master")
        End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        clear_all()
        'obj.RemoveRowsFromGrid(DGVIndentItem, DGVIndentItem.Rows.Count)
        'obj.RemoveRowsFromGrid(DGVMRSItem, DGVMRSItem.Rows.Count)
    End Sub
    Public Sub clear_all()
        obj.Clear_All_DTPicker(Me.GBMRSMaster.Controls)
        obj.Clear_All_TextBox(Me.GBMRSMaster.Controls)
        Me.DGVMRSItem.Rows.Clear()
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

        DGVMRSItem.EndEdit()

        Dim val As String = TryCast(DGVMRSItem("item_code", 0).Value, String)
        If String.IsNullOrEmpty(val) Then
            MsgBox("Please Enter Atleast One Record", MsgBoxStyle.Information, gblMessageHeading)
            Exit Sub
        End If
        lbl_Status.Focus()
        Dim cmd As SqlCommand
        cmd = obj.MyCon_BeginTransaction
        Try

            If flag = "save" Then
                Dim Mdate As DateTime = Today()
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If
                prpty = New cls_mrs_main_store_master_prop
                iMRSId = Convert.ToInt32(obj.getMaxValue("MRS_ID", "MRS_MAIN_STORE_MASTER"))
                prpty.MRS_ID = iMRSId
                prpty.MRS_CODE = obj.getPrefixCode("MRSMainStorePREFIX", "DIVISION_SETTINGS")
                prpty.MRS_NO = iMRSId
                prpty.MRS_DATE = Now
                prpty.CC_ID = v_the_current_Selected_CostCenter_id
                prpty.REQ_DATE = Convert.ToDateTime(DTPMRSReqDate.Value)
                prpty.MRS_STATUS = Convert.ToInt32(GlobalModule.MRSStatus.Fresh)
                prpty.MRS_REMARKS = txtMRSRemarks.Text()
                prpty.CREATED_BY = v_the_current_logged_in_user_name
                prpty.CREATION_DATE = Now
                prpty.MODIFIED_BY = ""
                prpty.MODIFIED_DATE = NULL_DATE
                prpty.DIVISION_ID = Convert.ToInt32(obj.ExecuteScalar("select DIV_ID from DIVISION_SETTINGS"))
                prpty.APPROVE_DATETIME = NULL_DATE
                clsObj.Insert_MRS_MAIN_STORE_MASTER(prpty, cmd)
                Dim iRowCount As Int32
                Dim iRow As Int32
                iRow = 0
                iRowCount = DGVMRSItem.RowCount
                For iRow = 0 To iRowCount - 1
                    'If DGVMRSItem.Item(4, iRow).Value() IsNot Nothing Then
                    If (Not String.IsNullOrEmpty(DGVMRSItem.Item("Quantity", iRow).Value)) And IsNumeric(DGVMRSItem.Item("Quantity", iRow).Value) Then
                        prpty = New cls_mrs_main_store_master_prop
                        prpty.MRS_ID = iMRSId
                        prpty.DIVISION_ID = Convert.ToInt32(obj.ExecuteScalar("select DIV_ID from DIVISION_SETTINGS"))
                        prpty.ITEM_ID = Convert.ToInt32(DGVMRSItem.Item(0, iRow).Value)
                        prpty.ITEM_QTY = Convert.ToDecimal(DGVMRSItem.Item(6, iRow).Value)
                        prpty.Issue_QTY = 0.0
                        prpty.Bal_QTY = Convert.ToDecimal(DGVMRSItem.Item(6, iRow).Value)
                        prpty.CREATED_BY = v_the_current_logged_in_user_name
                        prpty.CREATION_DATE = Now
                        prpty.MODIFIED_BY = ""
                        prpty.MODIFIED_DATE = NULL_DATE
                        clsObj.Insert_MRS_MAIN_STORE_DETAIL(prpty, cmd)
                        ' End If
                    End If
                Next iRow

            Else
                Dim Mdate As DateTime = Today()
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If

                If clsObj.get_record_count("SELECT * FROM MRS_MAIN_STORE_DETAIL WHERE MRS_ID  =" & iMRSId) > 0 Then
                    prpty = New cls_mrs_main_store_master_prop
                    prpty.MRS_ID = iMRSId
                    prpty.MRS_CODE = ""
                    prpty.MRS_NO = 1
                    prpty.MRS_DATE = Now
                    prpty.CC_ID = v_the_current_Selected_CostCenter_id
                    prpty.REQ_DATE = Now
                    prpty.MRS_STATUS = Convert.ToInt32(GlobalModule.MRSStatus.Fresh)
                    prpty.MRS_REMARKS = txtMRSRemarks.Text()
                    prpty.CREATED_BY = v_the_current_logged_in_user_name
                    prpty.CREATION_DATE = Now
                    prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                    prpty.MODIFIED_DATE = Now
                    prpty.DIVISION_ID = Convert.ToInt32(obj.ExecuteScalar("select DIV_ID from DIVISION_SETTINGS"))
                    prpty.APPROVE_DATETIME = Now
                    clsObj.Delete_MRS_MAIN_STORE_DETAIL(prpty, cmd)
                End If
                Dim qry As String
                qry = "SELECT * FROM  MRS_MAIN_STORE_MASTER WHERE MRS_ID = " & iMRSId
                Dim ds As DataSet
                Dim dt As DataTable
                ds = obj.FillDataSet(qry)
                dt = ds.Tables(0)
                If ds.Tables(0).Rows.Count > 0 Then
                    prpty = New cls_mrs_main_store_master_prop

                    ''*************************************************
                    ''Only Remarks will update in the master table.
                    ''Other parameters are passed just to ignore error.
                    ''*************************************************

                    prpty.MRS_ID = iMRSId
                    prpty.MRS_CODE = GetMRSCode()
                    prpty.MRS_NO = iMRSId
                    prpty.MRS_DATE = Now
                    prpty.CC_ID = v_the_current_Selected_CostCenter_id
                    prpty.REQ_DATE = Convert.ToDateTime(DTPMRSReqDate.Value)
                    prpty.MRS_STATUS = Convert.ToInt32(GlobalModule.MRSStatus.Fresh)
                    prpty.MRS_REMARKS = txtMRSRemarks.Text()
                    prpty.CREATED_BY = v_the_current_logged_in_user_name
                    prpty.CREATION_DATE = Now
                    prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                    prpty.MODIFIED_DATE = Now
                    prpty.DIVISION_ID = Convert.ToInt32(obj.ExecuteScalar("select DIV_ID from DIVISION_SETTINGS"))
                    prpty.APPROVE_DATETIME = Now
                    clsObj.Update_MRS_MAIN_STORE_MASTER(prpty, cmd)
                    Dim iRowCount As Int32
                    Dim iRow As Int32
                    iRowCount = DGVMRSItem.RowCount
                    For iRow = 0 To iRowCount - 1
                        If DGVMRSItem.Item("Quantity", iRow).Value() IsNot Nothing Then
                            If (Not String.IsNullOrEmpty(DGVMRSItem.Item("Quantity", iRow).Value)) And IsNumeric(DGVMRSItem.Item("Quantity", iRow).Value) Then
                                'If Convert.ToString(DGVMRSItem.Item(5, iRow).Value) > "0.00" Then
                                prpty.MRS_ID = Convert.ToInt32(dt.Rows(0)("MRS_ID"))
                                prpty.DIVISION_ID = Convert.ToInt32(dt.Rows(0)("DIVISION_ID"))
                                prpty.ITEM_ID = Convert.ToInt32(DGVMRSItem.Item(0, iRow).Value)
                                prpty.ITEM_QTY = Convert.ToDecimal(DGVMRSItem.Item("Quantity", iRow).Value)
                                prpty.Issue_QTY = Convert.ToDecimal("0.0")
                                prpty.Bal_QTY = Convert.ToDecimal(DGVMRSItem.Item("Quantity", iRow).Value)
                                prpty.CREATED_BY = v_the_current_logged_in_user_name
                                prpty.CREATION_DATE = Now
                                prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                                prpty.MODIFIED_DATE = Now
                                clsObj.Insert_MRS_MAIN_STORE_DETAIL(prpty, cmd)
                            End If
                        End If

                    Next iRow
                End If
            End If
            obj.MyCon_CommitTransaction(cmd)
            If flag = "save" Then
                If MsgBox("MRS Has Been Saved Successfully." & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptMRSMainStorePrint, "mrs_id", CStr(iMRSId), CStr(enmDataType.D_int))

                End If
            End If
            If Not flag = "save" Then
                If MsgBox("MRS Has Been Updated Successfully." & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptMRSMainStorePrint, "mrs_id", CStr(iMRSId), CStr(enmDataType.D_int))

                End If
            End If
            new_initilization()
        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TBC_MRS_Master.SelectedIndex = 0 Then
                obj.RptShow(enmReportName.RptMRSMainStorePrint, "mrs_id", CStr(DGVMRSMaster("MRS_ID", DGVMRSMaster.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                If flag <> "save" Then
                    obj.RptShow(enmReportName.RptMRSMainStorePrint, "mrs_id", CStr(DGVMRSMaster("MRS_id", DGVMRSMaster.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillGridMRSMaster()
        Try
            obj.Bind_GridBind_Val(DGVMRSMaster, "GET_MRS_MAIN_STORE_MASTERS", "@v_cc_id", "", v_the_current_Selected_CostCenter_id, "")
            DGVMRSMaster.Columns(0).Visible = False                'MRS_ID
            DGVMRSMaster.Columns(1).HeaderText = "MRS Code"        'MRS_CODE
            DGVMRSMaster.Columns(1).Width = 120
            DGVMRSMaster.Columns(2).HeaderText = "MRS Date"        'MRS_DATE
            DGVMRSMaster.Columns(2).Width = 120
            DGVMRSMaster.Columns(3).HeaderText = "Required Date"   'MRS_REMARKS
            DGVMRSMaster.Columns(3).Width = 120
            DGVMRSMaster.Columns(4).HeaderText = "MRS Remarks"     'MRS_REMARKS
            DGVMRSMaster.Columns(4).Width = 280
            DGVMRSMaster.Columns(5).HeaderText = "STATUS"          'MRS_STATUS
            DGVMRSMaster.Columns(5).Width = 100
            DGVMRSMaster.Columns(6).Visible = False                'DIVISION_ID
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub frm_MRS_MainStore_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'obj.FormatGrid(DGVMRSItem)
            'obj.FormatGrid(DGVMRSMaster)

            FillGridMRSMaster()
            DGVMRSItem_style()
            new_initilization()

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub new_initilization()
        clear_all()

        lbl_MRSCode.Text = GetMRSCode()
        lbl_MRSDate.Text = Now
        lbl_Status.Text = GlobalModule.MRSStatus.Fresh.ToString()
        DTPMRSReqDate.Value = Now.AddDays(2)
        txtMRSRemarks.Text = ""
        TBC_MRS_Master.SelectTab(1)
        flag = "save"
    End Sub

    Private Sub DGVMRSItem_style()
        Try

            DGVMRSItem.Columns.Clear()
            DGVMRSItem.Rows.Clear()
            Dim txtMRSItemId = New DataGridViewTextBoxColumn
            Dim txtMRSItemCode = New DataGridViewTextBoxColumn
            Dim txtMRSItemName = New DataGridViewTextBoxColumn
            Dim txtMRSItemUom = New DataGridViewTextBoxColumn
            Dim txtMRSItemCategory = New DataGridViewTextBoxColumn
            Dim txtMRSItemQuantity = New DataGridViewTextBoxColumn
            Dim txtCurrentStock = New DataGridViewTextBoxColumn
            'Dim cmbColItemName As New DataGridViewComboBoxColumn

            With txtMRSItemId
                .HeaderText = "Item Id"
                .Name = "Item_ID"
                .ReadOnly = True
                .Visible = False
                .Width = 10
            End With
            DGVMRSItem.Columns.Add(txtMRSItemId)

            With txtMRSItemCode
                .HeaderText = "Item Code"
                .Name = "Item_CODE"
                .ReadOnly = True
                .Visible = True
                .Width = 100
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            DGVMRSItem.Columns.Add(txtMRSItemCode)

            With txtMRSItemName
                .HeaderText = "Item Name"
                .Name = "Item_Name"
                .ReadOnly = True
                .Visible = True
                .Width = 330
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            DGVMRSItem.Columns.Add(txtMRSItemName)

            With txtMRSItemCategory
                .HeaderText = "Category"
                .Name = "item_cat_name"
                .ReadOnly = True
                .Visible = True
                .Width = 150
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            DGVMRSItem.Columns.Add(txtMRSItemCategory)

            With txtMRSItemUom
                .HeaderText = "UOM"
                .Name = "UM_Name"
                .ReadOnly = True
                .Visible = True
                .Width = 70
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            End With
            DGVMRSItem.Columns.Add(txtMRSItemUom)

            With txtCurrentStock
                .HeaderText = "Current Stock"
                .Name = "Current_Stock"
                .ReadOnly = True
                .Visible = True
                .Width = 120
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            End With
            DGVMRSItem.Columns.Add(txtCurrentStock)

            With txtMRSItemQuantity
                .HeaderText = "Quantity"
                .Name = "Quantity"
                .ReadOnly = False
                .Visible = True
                .Width = 90
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            End With
            DGVMRSItem.Columns.Add(txtMRSItemQuantity)
            Dim CellLength As DataGridViewTextBoxColumn = TryCast(Me.DGVMRSItem.Columns("Quantity"), DataGridViewTextBoxColumn)
            CellLength.MaxInputLength = 10
            DGVMRSItem.AllowUserToResizeColumns = False
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Function GetMRSCode() As String
        ' Try
        Dim Pre As String
        Dim MRSID As String
        Dim MRSCode As String
        Pre = obj.getPrefixCode("MRSMainStorePREFIX", "DIVISION_SETTINGS")
        MRSID = obj.getMaxValue("MRS_ID", "MRS_MAIN_STORE_MASTER")
        MRSCode = Pre & "" & MRSID
        Return MRSCode
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Form Load")
        'End Try
    End Function

    Private Sub DGVMRSItem_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DGVMRSItem.DataError

    End Sub

    Private Sub DGVMRSItem_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGVMRSItem.EditingControlShowing

        Try
            'AddHandler e.Control.KeyPress, AddressOf NumericValueDGVMRSItem

            txtQutity = TryCast(e.Control, TextBox)
            cmbItems = TryCast(e.Control, ComboBox)
            If cmbItems IsNot Nothing Then
                AddHandler cmbItems.SelectedIndexChanged, AddressOf cmbItems_SelectedIndexChanged
            End If
            If txtQutity IsNot Nothing Then
                AddHandler txtQutity.KeyDown, AddressOf txtQutity_KeyDown

            End If


        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub DGVMRSItem_CellLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            If cmbItems IsNot Nothing Then
                RemoveHandler cmbItems.SelectedIndexChanged, AddressOf cmbItems_SelectedIndexChanged
                cmbItems = Nothing
            End If
            If txtQutity IsNot Nothing Then
                RemoveHandler txtQutity.KeyDown, AddressOf txtQutity_KeyDown
                txtQutity = Nothing
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub cmbItems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        fill_DGVMRSItem(sender, e)
    End Sub

    Private Sub fill_DGVMRSItem(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim drv As DataRowView
            Dim IsInsert As Boolean
            drv = TryCast(TryCast(sender, ComboBox).SelectedItem, DataRowView)
            If drv IsNot Nothing Then
                Dim iRowCount As Int32
                Dim iRow As Int32
                iRowCount = DGVMRSItem.RowCount
                IsInsert = True
                For iRow = 0 To iRowCount - 2
                    If DGVMRSItem.Item(0, iRow).Value = Convert.ToInt32(drv(0)) Then
                        MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, "MRS Main Store Item")
                        IsInsert = False
                        Exit For
                    End If
                Next iRow
                If IsInsert = True Then
                    DGVMRSItem.Rows(DGVMRSItem.CurrentCell.RowIndex).Cells(0).Value = drv(0)
                    DGVMRSItem.Rows(DGVMRSItem.CurrentCell.RowIndex).Cells(1).Value = drv(1)
                    DGVMRSItem.Rows(DGVMRSItem.CurrentCell.RowIndex).Cells(3).Value = drv(3)
                    DGVMRSItem.Rows(DGVMRSItem.CurrentCell.RowIndex).Cells(4).Value = "0.00"
                End If
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
        'MsgBox(TryCast(sender, ComboBox).SelectedValue)
    End Sub

    Public Sub get_row(ByVal item_id As String)
        'Dim drv As DataRowView
        Dim IsInsert As Boolean
        Dim ds As DataSet
        'ds = obj.fill_Data_set("GET_ITEM_BY_ID", "@V_ITEM_ID", item_id)
        ds = obj.fill_Data_set("GET_ITEM_BY_ID_WITHSTOCK", "@V_ITEM_ID,@DIV_ID,@DATE", item_id & "," & Convert.ToString(v_the_current_division_id) & "," & Convert.ToString(Now.Date))
        Dim iRowCount As Int32
        Dim iRow As Int32
        iRowCount = DGVMRSItem.RowCount
        IsInsert = True
        'DGVMRSItem_style()
        For iRow = 0 To iRowCount - 2
            If DGVMRSItem.Item(0, iRow).Value = Convert.ToInt32(ds.Tables(0).Rows(0)(0)) Then
                MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, "MRS Main Store Item")
                IsInsert = False
                Exit For
            End If
        Next iRow
        Dim datatbl As New DataTable
        datatbl = ds.Tables(0)
        'DGVMRSItem.DataSource

        If IsInsert = True Then
            Dim introw As Integer
            'For Each dr As DataRow In datatbl.Rows
            ' MsgBox(DGVMRSItem.Rows.Count)
            introw = DGVMRSItem.Rows.Count - 1
            DGVMRSItem.Rows.Insert(introw)
            DGVMRSItem.Rows(introw).Cells("Item_ID").Value = ds.Tables(0).Rows(0)(0)
            DGVMRSItem.Rows(introw).Cells("Item_CODE").Value = ds.Tables(0).Rows(0)("item_Code").ToString()
            DGVMRSItem.Rows(introw).Cells("Item_Name").Value = ds.Tables(0).Rows(0)("item_Name").ToString()
            DGVMRSItem.Rows(introw).Cells("item_cat_name").Value = ds.Tables(0).Rows(0)("item_cat_name").ToString()
            DGVMRSItem.Rows(introw).Cells("UM_Name").Value = ds.Tables(0).Rows(0)("UM_NAME").ToString()
            ''New Column Added 
            DGVMRSItem.Rows(introw).Cells("Current_Stock").Value = ds.Tables(0).Rows(0)("Current_Stock").ToString()
            ''New Column Added 
            DGVMRSItem.Rows(introw).Cells("Quantity").Value = "0.00"
            'DGVMRSItem.Rows.Add()
            DGVMRSItem.Rows(introw).Cells("Item_CODE").Selected = False
            DGVMRSItem.Rows(introw).Cells("Item_Name").Selected = False
            DGVMRSItem.Rows(introw).Cells("item_cat_name").Selected = False
            DGVMRSItem.Rows(introw).Cells("UM_Name").Selected = False
            DGVMRSItem.Rows(introw).Cells("Current_Stock").Selected = False
            DGVMRSItem.Rows(introw).Cells("Quantity").Selected = True

            DGVMRSItem.Rows(introw + 1).Cells("Item_CODE").Selected = False
            DGVMRSItem.Rows(introw + 1).Cells("Item_Name").Selected = False
            DGVMRSItem.Rows(introw + 1).Cells("item_cat_name").Selected = False
            DGVMRSItem.Rows(introw + 1).Cells("UM_Name").Selected = False
            DGVMRSItem.Rows(introw + 1).Cells("Current_Stock").Selected = False
            DGVMRSItem.Rows(introw + 1).Cells("Quantity").Selected = False
            'If Me.DGVMRSItem.Rows.Count > 0 Then DGVMRSItem.Rows.Add()
            ' If Me.DGVMRSItem.Rows.Count > 0 Then DGVMRSItem.Rows.Add(1)
            'Next
        End If
        'ds = obj.
    End Sub

    Private Sub DGVMRSMaster_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVMRSMaster.DoubleClick
        Try
            Dim ds As DataSet
            Dim dtMRS As New DataTable
            Dim dtMRSDetail As New DataTable
            iMRSId = Convert.ToInt32(DGVMRSMaster.SelectedRows.Item(0).Cells(0).Value)
            flag = "update"
            ds = obj.fill_Data_set("GET_MRS_MASTERANDMRS_DETAIL", "@V_MRS_ID", iMRSId)
            If ds.Tables.Count > 0 Then
                'Bind the MRS Infromation
                dtMRS = ds.Tables(0)
                If (Convert.ToString(dtMRS.Rows(0)("MRS_STATUS")) = "Fresh") Then
                    txtMRSRemarks.ReadOnly = False
                    DGVMRSItem.Enabled = True

                Else
                    txtMRSRemarks.ReadOnly = True
                    DGVMRSItem.Enabled = False
                End If
                iMRSId = Convert.ToString(dtMRS.Rows(0)("MRS_ID"))
                lbl_MRSCode.Text = Convert.ToString(dtMRS.Rows(0)("MRS_CODE"))
                lbl_MRSDate.Text = Convert.ToString(dtMRS.Rows(0)("MRS_DATE"))
                dtpRequiredDate.Text = Convert.ToString(dtMRS.Rows(0)("REQ_DATE"))
                lbl_Status.Text = Convert.ToString(dtMRS.Rows(0)("MRS_STATUS"))
                txtMRSRemarks.Text = Convert.ToString(dtMRS.Rows(0)("MRS_REMARKS"))
                'Bind the MRS Item Grid
                DGVMRSItem_style()
                dtMRSDetail = ds.Tables(1)
                Dim iRowCount As Int32
                Dim iRow As Int32
                iRowCount = dtMRSDetail.Rows.Count
                For iRow = 0 To iRowCount - 1
                    If dtMRSDetail.Rows.Count > 0 Then
                        Dim rowindex As Integer = DGVMRSItem.Rows.Add()
                        DGVMRSItem.Rows(rowindex).Cells(0).Value = Convert.ToInt32(dtMRSDetail.Rows(iRow)("ITEM_ID"))
                        DGVMRSItem.Rows(rowindex).Cells(1).Value = Convert.ToString(dtMRSDetail.Rows(iRow)("ITEM_CODE"))
                        DGVMRSItem.Rows(rowindex).Cells(2).Value = Convert.ToString(dtMRSDetail.Rows(iRow)("ITEM_Name"))
                        DGVMRSItem.Rows(rowindex).Cells(3).Value = Convert.ToString(dtMRSDetail.Rows(iRow)("item_cat_name"))
                        DGVMRSItem.Rows(rowindex).Cells(4).Value = Convert.ToString(dtMRSDetail.Rows(iRow)("UM_Name"))
                        DGVMRSItem.Rows(rowindex).Cells(5).Value = Convert.ToString(dtMRSDetail.Rows(iRow)("Current_Stock"))
                        DGVMRSItem.Rows(rowindex).Cells(6).Value = Convert.ToInt32(dtMRSDetail.Rows(iRow)("Quantity"))
                    End If
                Next iRow
                TBC_MRS_Master.SelectTab(1)
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub txtQutity_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case Asc(e.KeyCode)
            Case AscW(Keys.Enter) 'Enter key
                e.Handled = True
            Case Keys.Back 'Backspace
            Case Keys.Subtract, Keys.Decimal, Keys.D0 To Keys.D9 'Negative sign, Decimal and Numbers
            Case Else ' Everything else
                e.Handled = True
        End Select
    End Sub

    Private Sub OpenConnectionFile()
        Try
            Dim sr As System.IO.StreamReader
            If System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\afbsl_mms\connection.txt") Then
                sr = New System.IO.StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\afbsl_mms\connection.txt")
                DNS = sr.ReadToEnd
                sr.Close()
            Else
                MsgBox("connection file does not exist")
                frm_create_connection_file.ShowDialog()
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub DGVMRSItem_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVMRSItem.CellEndEdit, DGVMRSItem.CellLeave
        Dim introwCount = DGVMRSItem.Rows.Count - 1

        'DGVMRSItem.Rows(introwCount - 1).Cells("Item_CODE").Selected = False
        'DGVMRSItem.Rows(introwCount - 1).Cells("Item_Name").Selected = False
        'DGVMRSItem.Rows(introwCount - 1).Cells("item_cat_name").Selected = False
        'DGVMRSItem.Rows(introwCount - 1).Cells("UM_Name").Selected = False
        'DGVMRSItem.Rows(introwCount - 1).Cells("Quantity").Selected = False

        'DGVMRSItem.Rows(introwCount).Cells("Item_CODE").Selected = False
        'DGVMRSItem.Rows(introwCount).Cells("Item_Name").Selected = True
        'DGVMRSItem.Rows(introwCount).Cells("item_cat_name").Selected = False
        'DGVMRSItem.Rows(introwCount).Cells("UM_Name").Selected = False
        'DGVMRSItem.Rows(introwCount).Cells("Quantity").Selected = False


    End Sub

    Private Sub DGVMRSItem_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVMRSItem.DoubleClick


    End Sub

    Private Sub DGVMRSItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGVMRSItem.KeyDown
        Try
            Dim iRowindex As Int32
            If flag = "save" Then
                If e.KeyCode = Keys.Space Then
                    iRowindex = DGVMRSItem.CurrentRow.Index
                    'frm_Show_search.qry = "Select Item_master.ITEM_ID,Item_master.ITEM_CODE as [Item Code],Item_master.ITEM_NAME  as [Item Name] from Item_master inner join item_detail on item_master.item_id = item_detail.item_id where item_detail.Is_active=1 " 'where item_detail.div_id = '" + Convert.ToString(v_the_curre) + "'"
                    'frm_Show_search.extra_condition = ""
                    'frm_Show_search.ret_column = "Item_ID"
                    'frm_Show_search.column_name = "ITEM_NAME"
                    'frm_Show_search.cols_width = "80,300"
                    'frm_Show_search.cols_no_for_width = "1,2"
                    'frm_Show_search.item_rate_column = ""
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
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim qry As String = ""

        qry = "SELECT  MRS_ID,  " &
                " MRS_CODE + CAST(MRS_NO AS VARCHAR) AS MRS_CODE,  " &
                " REPLACE(CONVERT(VARCHAR, MRS_DATE, 106), ' ', '-') AS MRS_DATE,  " &
                " REPLACE(CONVERT(VARCHAR, REQ_DATE, 106), ' ', '-') AS REQ_DATE,  " &
                " MRS_REMARKS,  " &
                " CASE WHEN MRS_STATUS = 1 THEN 'Fresh'  " &
                     " WHEN MRS_STATUS = 2 THEN 'Pending'  " &
                     " WHEN MRS_STATUS = 3 THEN 'Clear'  " &
                     " WHEN MRS_STATUS = 4 THEN 'Cancel'  " &
                " END AS MRS_STATUS,  " &
                " DIVISION_ID,  " &
        " CC_ID" &
        " FROM mrs_main_store_master" &
        " WHERE  ((MRS_CODE + CAST(MRS_NO AS VARCHAR)) + cast(dbo.fn_Format(MRS_DATE) as varchar) +  cast(dbo.fn_Format(REQ_DATE) as varchar) + MRS_REMARKS +  cast(MRS_STATUS as VARCHAR)) like '%" & txtSearch.Text & "%'" &
        " and CC_ID =" & v_the_current_Selected_CostCenter_id & ""


        obj.GridBind(DGVMRSMaster, qry)

    End Sub

End Class
