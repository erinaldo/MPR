Imports MMSPlus.indent_master
'Imports afbsl_mms.item_master
Imports System.Data.SqlClient
Imports System.Data

Public Class frm_Indent_Master
    Implements IForm
    Dim int_RowIndex As Integer
    Dim DGVIndentItem_Rowindex As Int16
    Dim _user_role As String
    Dim obj As New CommonClass
    Dim clsObj As New cls_indent_master
    Dim prpty As cls_indent_master_prop
    Dim flag As String
    Dim CostCenter_Code As String
    Dim clsobjItem As New item_detail.cls_item_detail
    Dim cmbItems As ComboBox
    Dim txtQuanity As TextBox
    Dim iIdentId As Int32
    Dim iRowCount As Int32
    Dim int_ColumnIndex As Integer
    Dim _rights As Form_Rights

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Dim qry As String
    Dim iRow As Int32
    Private Enum enmIndentInsert
        Rowid = 0
    End Enum

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick
        Dim cmd As SqlCommand = Nothing

        Try
            If TBCIndentMaster.SelectedIndex = 0 Then
                MsgBox("select indent to delete.", vbExclamation, gblMessageHeading)
                Exit Sub
            End If
            If flag = "save" Then
                Exit Sub
            End If
            If _rights.allow_trans = "N" Then
                RightsMsg()
                Exit Sub
            End If
            'Delete detail table records
            cmd = obj.MyCon_BeginTransaction
            prpty = New cls_indent_master_prop
            prpty.INDENT_ID = Convert.ToInt32(iIdentId)
            prpty.DIVISION_ID = Convert.ToInt32(-1)
            prpty.ITEM_ID = Convert.ToInt32(-1)
            prpty.ITEM_QTY_REQ = Convert.ToInt32(-1)
            prpty.ITEM_QTY_PO = Convert.ToInt32(-1)
            prpty.ITEM_QTY_BAL = Convert.ToInt32(-1)
            prpty.CREATED_BY = v_the_current_logged_in_user_name
            prpty.CREATION_DATE = now
            prpty.MODIFIED_BY = v_the_current_logged_in_user_name
            prpty.MODIFIED_DATE = now
            clsObj.Delete_INDENT_DETAILTrans(prpty, cmd)

            ' delete Master table records
            prpty = New cls_indent_master_prop
            prpty.INDENT_ID = iIdentId
            prpty.INDENT_CODE = lbl_IndentCode.Tag
            prpty.INDENT_NO = -1
            prpty.INDENT_DATE = lbl_IndentDate.Text()
            prpty.INDENT_STATUS = Convert.ToInt32(GlobalModule.IndentStatus.Fresh)
            prpty.INDENT_REMARKS = txtIndentReamrks.Text()
            prpty.CREATED_BY = v_the_current_logged_in_user_name
            prpty.CREATION_DATE = now
            prpty.MODIFIED_BY = v_the_current_logged_in_user_name
            prpty.MODIFIED_DATE = now
            prpty.REQUIRED_DATE = Convert.ToDateTime(dtpRequiredDate.Text)
            prpty.DIVISION_ID = v_the_current_division_id
            clsObj.Delete_INDENT_MASTERTrans(prpty, cmd)

            obj.MyCon_CommitTransaction(cmd)
            MsgBox("Indent Deleted successfully", vbInformation, gblMessageHeading)
            FillGridIndentMaster()
            TBCIndentMaster.SelectTab(0)
        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)

            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        NewRecord()

    End Sub
    Private Sub NewRecord()
        Try
            TBCIndentMaster.SelectTab(1)

            DGVIndentItem.DataSource = Nothing

            DGVIndentItem_style()
            DGVIndentItem.Rows.Add()
            FillIndentInfo()
            flag = "save"
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_Indent_Master")
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        FillGridIndentMaster()
        TBCIndentMaster.SelectTab(1)
    End Sub
    Private Function Validation() As Boolean
        Validation = True
        Dim blnRecExist As Boolean
        Dim blnChkQuantity As Boolean
        blnRecExist = False
        blnChkQuantity = False


        If TBCIndentMaster.SelectedIndex = 0 Then
            Validation = False
            Exit Function
        End If

        If dtpRequiredDate.Value < now Then
            MsgBox("Required date can not be less than current date.", vbExclamation, gblMessageHeading)
            dtpRequiredDate.Focus()
            Validation = False
            Exit Function
        End If

        lbl_IndentCode.Focus()
        For iRow = 0 To DGVIndentItem.RowCount - 1
            If Val(DGVIndentItem.Item("Quantity", iRow).Value) > 0 And blnRecExist = False Then
                blnRecExist = True
                'Exit For
            End If

        Next iRow

        If blnRecExist = True Then
            Validation = True
            Exit Function
        Else
            Validation = False
            MsgBox("Select aleast one valid item in indent to save indent information", vbExclamation, gblMessageHeading)
            Exit Function
        End If

    End Function
    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Dim cmd As SqlCommand

        If Validation() = False Then
            Exit Sub
        End If

        cmd = obj.MyCon_BeginTransaction
        Try
            lbl_IndentCode.Focus() ' to remove error input string is not in correct format. remove focus from grid  before saving process start
            If flag = "save" Then
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If
                prpty = New cls_indent_master_prop
                prpty.INDENT_ID = iIdentId
                ' prpty.INDENT_CODE = lbl_IndentCode.Text()
                prpty.INDENT_CODE = lbl_IndentCode.Tag
                prpty.INDENT_NO = Convert.ToInt32(obj.getMaxValueTrans("INDENT_ID", "INDENT_MASTER", cmd))
                prpty.INDENT_DATE = lbl_IndentDate.Text()
                prpty.INDENT_STATUS = Convert.ToInt32(GlobalModule.IndentStatus.Fresh)
                prpty.INDENT_REMARKS = txtIndentReamrks.Text()
                prpty.CREATED_BY = v_the_current_logged_in_user_name
                prpty.CREATION_DATE = now
                prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                prpty.MODIFIED_DATE = now
                prpty.REQUIRED_DATE = Convert.ToDateTime(dtpRequiredDate.Text)
                prpty.DIVISION_ID = v_the_current_division_id
                clsObj.Insert_INDENT_MASTERTrans(prpty, cmd)
                Dim iRowCount As Int32
                Dim iRow As Int32
                iRowCount = DGVIndentItem.RowCount
                For iRow = 0 To iRowCount - 1
                    If DGVIndentItem.Item("Quantity", iRow).Value() IsNot Nothing Then
                        If DGVIndentItem.Item("Quantity", iRow).Value() > 0 Then
                            prpty = New cls_indent_master_prop
                            prpty.INDENT_ID = iIdentId
                            prpty.DIVISION_ID = v_the_current_division_id
                            prpty.ITEM_ID = Convert.ToInt32(DGVIndentItem.Item(0, iRow).Value)
                            prpty.ITEM_QTY_REQ = Convert.ToDouble(DGVIndentItem.Item("Quantity", iRow).Value)
                            prpty.ITEM_QTY_PO = Convert.ToDouble(DGVIndentItem.Item("Quantity", iRow).Value)
                            prpty.ITEM_QTY_BAL = Convert.ToDouble(DGVIndentItem.Item("Quantity", iRow).Value)
                            prpty.CREATED_BY = v_the_current_logged_in_user_name
                            prpty.CREATION_DATE = now
                            prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                            prpty.MODIFIED_DATE = now
                            clsObj.Insert_INDENT_DETAILTrans(prpty, cmd)
                        End If
                    End If
                Next iRow
                'MsgBox("Indent information has been Saved", MsgBoxStyle.Information, gblMessageHeading)
            Else
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If

                prpty = New cls_indent_master_prop
                prpty.INDENT_ID = Convert.ToInt32(iIdentId)
                prpty.DIVISION_ID = Convert.ToInt32(-1)
                prpty.ITEM_ID = Convert.ToInt32(-1)
                prpty.ITEM_QTY_REQ = Convert.ToInt32(-1)
                prpty.ITEM_QTY_PO = Convert.ToInt32(-1)
                prpty.ITEM_QTY_BAL = Convert.ToInt32(-1)
                prpty.CREATED_BY = v_the_current_logged_in_user_name
                prpty.CREATION_DATE = now
                prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                prpty.MODIFIED_DATE = now
                clsObj.Delete_INDENT_DETAILTrans(prpty, cmd)


                prpty = New cls_indent_master_prop
                prpty.INDENT_ID = iIdentId
                prpty.INDENT_CODE = Convert.ToInt32(-1)
                prpty.INDENT_NO = Convert.ToInt32(-1)
                prpty.INDENT_DATE = lbl_IndentDate.Text()
                prpty.INDENT_STATUS = Convert.ToInt32(-1)
                prpty.INDENT_REMARKS = txtIndentReamrks.Text()
                prpty.CREATED_BY = ""
                prpty.CREATION_DATE = lbl_IndentDate.Text()
                prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                prpty.MODIFIED_DATE = now
                prpty.REQUIRED_DATE = Convert.ToDateTime(dtpRequiredDate.Text)
                prpty.DIVISION_ID = v_the_current_division_id
                clsObj.Update_INDENT_MASTERTrans(prpty, cmd)

                iRowCount = DGVIndentItem.RowCount
                For iRow = 0 To iRowCount - 1
                    If DGVIndentItem.Item("Quantity", iRow).Value() IsNot Nothing Then
                        If Val(DGVIndentItem.Item("Quantity", iRow).Value) > 0 Then
                            prpty = New cls_indent_master_prop
                            prpty.INDENT_ID = iIdentId
                            prpty.DIVISION_ID = v_the_current_division_id
                            prpty.ITEM_ID = Convert.ToInt32(DGVIndentItem.Item(0, iRow).Value)
                            prpty.ITEM_QTY_REQ = Convert.ToDouble(DGVIndentItem.Item("Quantity", iRow).Value)
                            prpty.ITEM_QTY_PO = Convert.ToDouble(DGVIndentItem.Item("Quantity", iRow).Value)
                            prpty.ITEM_QTY_BAL = Convert.ToDouble(DGVIndentItem.Item("Quantity", iRow).Value)
                            prpty.CREATED_BY = v_the_current_logged_in_user_name
                            prpty.CREATION_DATE = lbl_IndentDate.Text()
                            prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                            prpty.MODIFIED_DATE = now
                            clsObj.Insert_INDENT_DETAILTrans(prpty, cmd)
                        End If
                    End If

                Next iRow
                'MsgBox("Indent information updated.", MsgBoxStyle.Information, gblMessageHeading)
            End If
            obj.MyCon_CommitTransaction(cmd)
            FillGridIndentMaster()
            TBCIndentMaster.SelectTab(1)
            If flag = "save" Then
                If MsgBox("Indent information has been Saved" & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptIndentDetailPrint, "indent_id", CStr(prpty.INDENT_ID), CStr(enmDataType.D_int))
                End If
            Else
                If MsgBox("Indent information has been Updated" & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptIndentDetailPrint, "indent_id", CStr(prpty.INDENT_ID), CStr(enmDataType.D_int))
                End If
            End If
            NewRecord()
        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)

            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TBCIndentMaster.SelectedIndex = 0 Then
                obj.RptShow(enmReportName.RptIndentDetailPrint, "indent_id", CStr(DGVIdnetMaster("indent_id", DGVIdnetMaster.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                If flag <> "save" Then
                    obj.RptShow(enmReportName.RptIndentDetailPrint, "wastage_id", CStr(DGVIdnetMaster("wastage_id", DGVIdnetMaster.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Function GetIndentCode() As String
        ' Try
        Dim Pre As String ' Store indent prefix 
        Dim CCID As String 'store new indent no.
        Dim CostCenterCode As String
        Pre = obj.getPrefixCode("INDENT_PREFIX", "DIVISION_SETTINGS")
        CCID = obj.getMaxValue("INDENT_ID", "INDENT_MASTER")
        lbl_IndentCode.Tag = Pre
        CostCenterCode = Pre & "" & CCID
        Return CostCenterCode
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Form Load")
        'End Try
    End Function

    Private Sub frm_Indent_Master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dtpRequiredDate.Value = now

            FillIndentInfo()
            FillGridIndentMaster()
            'iIdentId = 0
            DGVIndentItem.AutoGenerateColumns = False
            DGVIndentItem_style()
            NewRecord()

        Catch ex As Exception

            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub
    Private Sub FillIndentInfo()
        iIdentId = Convert.ToInt32(obj.getMaxValue("INDENT_ID", "INDENT_MASTER"))
        lbl_IndentCode.Text = GetIndentCode()
        lbl_IndentDate.Text = now.ToString("dd/MMM/yyyy")
        lbl_IndentStatus.Text = GlobalModule.IndentStatus.Fresh.ToString()
        dtpRequiredDate.Value = now
        txtIndentReamrks.Text = ""
        flag = "save"
    End Sub
    Private Sub FillGridIndentMaster()
        Try
            obj.Grid_Bind(DGVIdnetMaster, "GET_INDENT_MASTER")
            DGVIdnetMaster.Columns(0).Visible = False                'INDENT_ID
            DGVIdnetMaster.Columns(1).HeaderText = "Indent Code"     'INDENT_CODE
            DGVIdnetMaster.Columns(1).Width = 130
            DGVIdnetMaster.Columns(2).HeaderText = "Indent Date"     'INDENT_DATE
            DGVIdnetMaster.Columns(2).Width = 100
            DGVIdnetMaster.Columns(3).HeaderText = "Required Date"   'REQUIRED_DATE
            DGVIdnetMaster.Columns(3).Width = 140
            DGVIdnetMaster.Columns(4).HeaderText = "Indent Remarks"   'INDENT_REMARKS
            DGVIdnetMaster.Columns(4).Width = 350
            DGVIdnetMaster.Columns(5).HeaderText = "Indent Status"   'INDENT_REMARKS
            DGVIdnetMaster.Columns(5).Width = 150
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub


    Private Sub DGVIndentItem_style()
        Try

            DGVIndentItem.Columns.Clear()
            Dim txtbCol As New DataGridViewTextBoxColumn
            Dim txtbCol1 As New DataGridViewTextBoxColumn
            Dim txtbCol2 As New DataGridViewTextBoxColumn
            Dim txtbCol3 As New DataGridViewTextBoxColumn
            Dim cmbCol As New DataGridViewTextBoxColumn
            Dim STOCK As New DataGridViewTextBoxColumn

            With txtbCol
                .HeaderText = "Item Id"
                .DataPropertyName = "ITEM_ID"
                .Name = "ITEM_ID"
                .ReadOnly = True
                .Visible = False
                .Width = 0
            End With

            DGVIndentItem.Columns.Add(txtbCol)

            With txtbCol1
                .HeaderText = "Item Code"
                .Name = "ITEM_CODE"
                .DataPropertyName = "ITEM_CODE"
                .ReadOnly = True
                .Visible = True
                .Width = 100
            End With
            DGVIndentItem.Columns.Add(txtbCol1)

            'txtbCol1 = New DataGridViewTextBoxColumn
            With cmbCol
                .HeaderText = "Item Name"
                .Name = "ITEM_NAME"
                .DataPropertyName = "ITEM_NAME"
                .ReadOnly = True
                .Visible = True
                .Width = 400
            End With
            'cmbCol.DataSource = clsObj.Fill_DataSet("SELECT IM.ITEM_ID,IM.ITEM_CODE,IM.ITEM_NAME,UM.UM_Name,CM.ITEM_CAT_NAME,IM.DIVISION_ID, 0.00 as Quantity FROM ITEM_MASTER  AS IM INNER JOIN  ITEM_DETAIL AS ID ON IM.ITEM_ID = ID.ITEM_ID  INNER JOIN  UNIT_MASTER AS UM ON IM.UM_ID = UM.UM_ID INNER JOIN ITEM_CATEGORY AS CM ON IM.ITEM_CATEGORY_ID = CM.ITEM_CAT_ID").Tables(0)
            'cmbCol.DisplayMember = "Item_name"
            'cmbCol.ValueMember = "Item_Id"
            DGVIndentItem.Columns.Add(cmbCol)

            With txtbCol2
                .HeaderText = "UOM"
                .Name = "UM_Name"
                .DataPropertyName = "UM_Name"
                .ReadOnly = True
                .Visible = True
                .Width = 80
            End With
            DGVIndentItem.Columns.Add(txtbCol2)

            With STOCK
                .HeaderText = "Current Stock"
                .Name = "CurrentStock"
                .DataPropertyName = "CurrentStock"
                .ReadOnly = True
                .Visible = True
                .Width = 150
            End With
            DGVIndentItem.Columns.Add(STOCK)

            With txtbCol3
                .HeaderText = "Quantity"
                .Name = "Quantity"
                .DataPropertyName = "Quantity"
                .ReadOnly = False
                .Visible = True
                .Width = 145
            End With
            DGVIndentItem.Columns.Add(txtbCol3)

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub DGVIndentItem_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGVIndentItem.EditingControlShowing
        Try
            AddHandler e.Control.KeyPress, AddressOf obj.Valid_NumberGrid
            txtQuanity = TryCast(e.Control, TextBox)
            cmbItems = TryCast(e.Control, ComboBox)
            If cmbItems IsNot Nothing Then
                AddHandler cmbItems.SelectedIndexChanged, AddressOf cmbItems_SelectedIndexChanged
            End If
            If txtQuanity IsNot Nothing Then
                AddHandler txtQuanity.KeyDown, AddressOf txtQutity_KeyDown

            End If

        Catch ex As Exception

            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub DGVIndentItem_CellLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVIndentItem.CellLeave
        Try


            If cmbItems IsNot Nothing Then
                RemoveHandler cmbItems.SelectedIndexChanged, AddressOf cmbItems_SelectedIndexChanged
                cmbItems = Nothing
            End If
            If txtQuanity IsNot Nothing Then
                RemoveHandler txtQuanity.KeyDown, AddressOf txtQutity_KeyDown
                txtQuanity = Nothing
            End If

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical + vbYesNo, gblMessageHeading)
        End Try
    End Sub
    Private Sub cmbItems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim drv As DataRowView
            Dim IsInsert As Boolean
            drv = TryCast(TryCast(sender, ComboBox).SelectedItem, DataRowView)
            If drv IsNot Nothing Then
                Dim iRowCount As Int32
                Dim iRow As Int32
                iRowCount = DGVIndentItem.RowCount
                IsInsert = True
                For iRow = 0 To iRowCount - 2
                    If DGVIndentItem.Item(0, iRow).Value = Convert.ToInt32(drv(0)) Then
                        'lblErrorMsg.Text = "Item Already Exist"
                        MsgBox("Same Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
                        IsInsert = False
                        Exit For
                    End If
                Next iRow
                If IsInsert = True Then
                    DGVIndentItem.Rows(DGVIndentItem.CurrentCell.RowIndex).Cells(0).Value = drv(0)
                    DGVIndentItem.Rows(DGVIndentItem.CurrentCell.RowIndex).Cells(1).Value = drv(1)
                    DGVIndentItem.Rows(DGVIndentItem.CurrentCell.RowIndex).Cells(3).Value = drv(3)
                    DGVIndentItem.Rows(DGVIndentItem.CurrentCell.RowIndex).Cells(4).Value = "0.00"
                End If
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
        'MsgBox(TryCast(sender, ComboBox).SelectedValue)
    End Sub

    Private Sub txtQutity_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    Private Sub DGVIndentItem_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DGVIndentItem.DataError

    End Sub
    Sub NumericValueDGVIndentItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Dim colindex As Decimal = DGVIndentItem.CurrentCell.ColumnIndex
        If colindex = 4 Then
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

    Private Sub DGVIdnetMaster_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVIdnetMaster.DoubleClick
        Try

            If _rights.allow_edit = "N" Then
                RightsMsg()
                Exit Sub
            End If

            Dim ds As DataSet
            If Convert.ToString(DGVIdnetMaster.SelectedRows.Item(0).Cells(5).Value).ToUpper <> "FRESH" Then
                MsgBox("Indent with 'FRESH' status can modify only.", vbExclamation, gblMessageHeading)
                Exit Sub
            End If


            Dim dtIndent As New DataTable
            Dim dtIndentDetail As New DataTable
            iIdentId = Convert.ToInt32(DGVIdnetMaster.SelectedRows.Item(0).Cells(0).Value)
            flag = "update"
            ds = obj.fill_Data_set("GET_INDENT_MASTERANDINDENT_DETAIL", "@V_INDENT_ID,@Div_Id,@ToDate", iIdentId & "," & v_the_current_division_id & "," & Now)
            'ds = obj.fill_Data_set("GET_INDENT_MASTERANDINDENT_DETAIL", "@V_INDENT_ID", iIdentId)
            If ds.Tables.Count > 0 Then
                'Bind the Indent Infromation
                dtIndent = ds.Tables(0)
                'iIdentId = Convert.ToString(dtIndent.Rows(0)("INDENT_ID"))
                lbl_IndentCode.Text = Convert.ToString(dtIndent.Rows(0)("INDENT_CODE"))
                lbl_IndentDate.Text = Convert.ToString(dtIndent.Rows(0)("INDENT_DATE"))
                dtpRequiredDate.Text = Convert.ToString(dtIndent.Rows(0)("REQUIRED_DATE"))
                lbl_IndentStatus.Text = Convert.ToString(dtIndent.Rows(0)("INDENT_STATUS"))
                txtIndentReamrks.Text = Convert.ToString(dtIndent.Rows(0)("INDENT_REMARKS"))
                'Bind the Indent Item Grid
                '  DGVIndentItem_style()
                dtIndentDetail = ds.Tables(1)

                'dtIndentDetail.Rows.Add(dtIndentDetail.NewRow)
                DGVIndentItem.DataSource = dtIndentDetail

                TBCIndentMaster.SelectTab(1)
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub TBCIndentMaster_Selecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles TBCIndentMaster.Selecting
        'If TBCIndentMaster.TabIndex = 1 And iIdentId = 0 Then
        '    TBCIndentMaster.SelectTab(0)
        'End If
    End Sub


    Private Sub DGVIndentItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGVIndentItem.KeyDown
        Try
            Dim iRowindex As Int32
            If e.KeyCode = Keys.Space Then
                iRowindex = Convert.ToInt32(DGVIndentItem.CurrentRow.Index)
                If int_ColumnIndex = 0 Then

                    'frm_Show_search.qry = "SELECT IM.ITEM_ID,IM.ITEM_CODE," &
                    '       " IM.ITEM_NAME,UM.UM_Name,CM.ITEM_CAT_NAME," &
                    '       " IM.DIVISION_ID, 0.00 as Quantity " &
                    '       " FROM ITEM_MASTER  AS IM INNER JOIN " &
                    '       " ITEM_DETAIL AS ID ON IM.ITEM_ID = ID.ITEM_ID  INNER JOIN  UNIT_MASTER AS UM " &
                    '       " ON IM.UM_ID = UM.UM_ID INNER JOIN ITEM_CATEGORY AS CM ON " &
                    '       " IM.ITEM_CATEGORY_ID = CM.ITEM_CAT_ID" '& _
                    ''" where " & _
                    ''" IM.division_id = '" + Convert.ToString(v_the_current_logged_in_user_id) + "'"

                    'frm_Show_search.column_name = "Item_Name"
                    'frm_Show_search.extra_condition = ""
                    'frm_Show_search.ret_column = "Item_ID"
                    'frm_Show_search.item_rate_column = ""
                    '' frm_Show_search.ret_column = "division_id"
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
                    frm_Show_search.cols_width = "100,340,70,70,100,105"
                    frm_Show_search.extra_condition = ""
                    frm_Show_search.ret_column = "ITEM_ID"
                    frm_Show_search.item_rate_column = ""
                    frm_Show_search.ShowDialog()


                    get_row(frm_Show_search.search_result)
                    frm_Show_search.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub get_row(ByVal item_id As String)

        Dim IsInsert As Boolean
        Dim ds As DataSet
        'Dim Div_ID As Integer
        If item_id <> -1 Then
            ds = obj.fill_Data_set("Get_IndentItemById", "@V_ITEM_ID,@Div_Id,@ToDate", item_id & "," & v_the_current_division_id & "," & now)
            Dim iRowCount As Int32
            Dim iRow As Int32
            iRowCount = DGVIndentItem.RowCount
            IsInsert = True
            For iRow = 0 To iRowCount - 2
                If Trim(DGVIndentItem.Item("ITEM_CODE", iRow).Value) = Trim(ds.Tables(0).Rows(0)(1)) Then
                    MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
                    IsInsert = False
                    Exit For
                End If

            Next iRow
            Dim dtable As New DataTable
            Dim dr As DataRow

            dtable = DGVIndentItem.DataSource
            If dtable Is Nothing Then
                dtable = ds.Tables(0).Copy
                DGVIndentItem.DataSource = dtable
            ElseIf IsInsert = True Then
                'Dim introw As Integer
                dr = dtable.NewRow
                dr("Item_ID") = ds.Tables(0).Rows(0)(0)
                dr("Item_CODE") = ds.Tables(0).Rows(0)("item_Code").ToString()
                dr("Item_Name") = ds.Tables(0).Rows(0)("item_Name").ToString()
                dr("UM_Name") = ds.Tables(0).Rows(0)("UM_NAME").ToString()
                dr("CurrentStock") = ds.Tables(0).Rows(0)("CurrentStock").ToString()
                dr("Quantity") = ds.Tables(0).Rows(0)("Quantity").ToString()
                dtable.Rows.Add(dr)

            End If
        End If
        'ds = obj.
    End Sub

    Private Sub DGVIndentItem_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVIndentItem.CellEndEdit
        'If DGVIndentItem.Rows(e.RowIndex).Cells("Quantity").Value > DGVIndentItem.Rows(e.RowIndex).Cells("CurrentStock").Value Then
        '    MsgBox("Quantity Must Be Less Than  Current Stock", MsgBoxStyle.Critical, gblMessageHeading)
        '    DGVIndentItem.SelectionMode = DataGridViewSelectionMode.CellSelect
        '    DGVIndentItem.CurrentCell = DGVIndentItem.Item("Quantity", e.RowIndex)
        '    DGVIndentItem.Item("Quantity", e.RowIndex).Value = 0
        'End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim qry As String = ""
        qry = "   SELECT  INDENT_ID,  " & _
                " INDENT_CODE +''+ CAST(INDENT_NO AS VARCHAR) AS INDENT_CODE,  " & _
                " dbo.fn_Format(Indent_Date) AS INDENT_DATE,  " & _
                " dbo.fn_Format(REQUIRED_DATE) AS Required_Date,  " & _
                " INDENT_REMARKS,  " & _
                " case WHEN INDENT_STATUS = 1 THEN 'FRESH'  " & _
                     " WHEN INDENT_STATUS = 2 THEN 'PENDING'  " & _
                     " WHEN INDENT_STATUS = 3 THEN 'CLEAR'  " & _
                     " when INDENT_STATUS = 4 THEN 'CANCEL'  " & _
                " END AS INDENT_STATUS  " & _
        " FROM indent_master " & _
  " WHERE ((INDENT_CODE +''+ CAST(INDENT_NO AS VARCHAR)) " & _
     " + CAST(dbo.fn_Format(Indent_Date) AS VARCHAR) " & _
     " + CAST(dbo.fn_Format(REQUIRED_DATE) AS VARCHAR) + INDENT_REMARKS " & _
     " + (case WHEN INDENT_STATUS = 1 THEN 'FRESH'  " & _
                     " WHEN INDENT_STATUS = 2 THEN 'PENDING'  " & _
                     " WHEN INDENT_STATUS = 3 THEN 'CLEAR'  " & _
                     " when INDENT_STATUS = 4 THEN 'CANCEL'  " & _
               " END)" & _
    " ) " & _
  " LIKE '%" & txtSearch.Text & "%'"

        obj.GridBind(DGVIdnetMaster, qry)
    End Sub


End Class



