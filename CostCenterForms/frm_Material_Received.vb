Imports MMSPlus.material_issue_to_cost_center_master
Public Class frm_Material_Received
    Implements IForm
    Dim rights As Form_Rights
    Dim obj As New CommonClass
    Dim flag As String
    Dim cmbItems As ComboBox
    Dim txtQutity As TextBox
    Dim iMIId As Int32
    Dim clsObj As New cls_material_issue_to_cost_center_master
    Dim prpty As cls_material_issue_to_cost_center_master_prop


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
        'Try
        '    DGVMarerialIssueItem_style()
        '    flag = "save"
        '    FillGridMarerialIssue()
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_Indent_Master")
        'End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        BindComboMeterialIssueNo()
        FillGridMarerialIssue()
        DGVMarerialIssueItem_style()
        FillMIInformation(iMIId)
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick

        Try
            If flag = "update" Then
                Dim iRowCount As Int32
                Dim iRow As Int32
                iRow = 0
                iRowCount = DGVMIItem.RowCount
                For iRow = 0 To iRowCount - 1
                    If DGVMIItem.Item(7, iRow).Value() IsNot Nothing Then
                        If Convert.ToString(DGVMIItem.Item(7, iRow).Value) > "0.00" Then
                            prpty = New cls_material_issue_to_cost_center_master_prop
                            prpty.MIO_ID = Convert.ToInt32(iMIId)
                            prpty.ITEM_ID = Convert.ToInt32(DGVMIItem.Item(0, iRow).Value)
                            prpty.MIO_ACCEPT_DATE = Now
                            prpty.ACCEPTED_QTY = Convert.ToDecimal(DGVMIItem.Item(7, iRow).Value)
                            prpty.MRS_ID = Convert.ToInt32(DGVMIItem.Item(8, iRow).Value)
                            clsObj.UPDATE_MATERIAL_ISSUE_TO_COST_CENTER_DETAIL(prpty)
                        End If
                    End If
                Next iRow
                FillGridMarerialIssue()
                MsgBox("Record Update", MsgBoxStyle.Information, "Save Material Issue Detail")
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error saveClick --> frm_MRS_Master")
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

   
    Private Sub frm_Material_Received_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            rights = clsObj.Get_Form_Rights(Me.Name)
            FillGridMarerialIssue()
            BindComboMeterialIssueNo()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Form Load")
        End Try
    End Sub
    Private Sub FillGridMarerialIssue()
        Try
            obj.Grid_Bind(DGVMaterialIssue, "GET_MATERIAL_ISSUE")
            DGVMaterialIssue.Columns(0).Visible = False                'MIO_ID
            DGVMaterialIssue.Columns(1).HeaderText = "MIO CODE"        'MIO_CODE
            DGVMaterialIssue.Columns(1).Width = 100
            DGVMaterialIssue.Columns(2).HeaderText = "MOI Date"        'MIO_DATE
            DGVMaterialIssue.Columns(2).Width = 150
            DGVMaterialIssue.Columns(3).HeaderText = "MIO STATUS"   'MIO_STATUS
            DGVMaterialIssue.Columns(3).Width = 120
            DGVMaterialIssue.Columns(4).HeaderText = "Division"     'Division
            DGVMaterialIssue.Columns(4).Width = 480
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error saveClick --> frm_MRS_Master")
        End Try
    End Sub
    Private Sub DGVMarerialIssueItem_style()
        Try
            DGVMIItem.Columns.Clear()
            Dim txtMIItemId = New DataGridViewTextBoxColumn
            Dim txtMIItemCode = New DataGridViewTextBoxColumn
            Dim txtMIItemName = New DataGridViewTextBoxColumn
            Dim txtMIItemUom = New DataGridViewTextBoxColumn
            Dim txtMIItemReqQuantity = New DataGridViewTextBoxColumn
            Dim txtMIItemTrsRate = New DataGridViewTextBoxColumn
            Dim txtMIItemIssueQuantity = New DataGridViewTextBoxColumn
            Dim txtMIItemAcceptQuantity = New DataGridViewTextBoxColumn
            Dim txtMIMRSID = New DataGridViewTextBoxColumn

            With txtMIItemId
                .HeaderText = "Item Id"
                .Name = "Item_ID"
                .ReadOnly = True
                .Visible = False
                .Width = 5
            End With
            DGVMIItem.Columns.Add(txtMIItemId)

            With txtMIItemCode
                .HeaderText = "Item Code"
                .Name = "Item_CODE"
                .ReadOnly = True
                .Visible = True
                .Width = 100
            End With
            DGVMIItem.Columns.Add(txtMIItemCode)

            With txtMIItemName
                .HeaderText = "Item Name"
                .Name = "Item_Name"
                .ReadOnly = True
                .Visible = True
                .Width = 250
            End With
            DGVMIItem.Columns.Add(txtMIItemName)


            With txtMIItemUom
                .HeaderText = "UOM"
                .Name = "UM_Name"
                .ReadOnly = True
                .Visible = True
                .Width = 80
            End With
            DGVMIItem.Columns.Add(txtMIItemUom)

            With txtMIItemReqQuantity
                .HeaderText = "Req Quantity"
                .Name = "REQ_QTY"
                .ReadOnly = True
                .Visible = True
                .Width = 120
            End With
            DGVMIItem.Columns.Add(txtMIItemReqQuantity)
            With txtMIItemIssueQuantity
                .HeaderText = "Issued Quantity"
                .Name = "ISSUED_QTY"
                .ReadOnly = True
                .Visible = True
                .Width = 120
            End With
            DGVMIItem.Columns.Add(txtMIItemIssueQuantity)

            With txtMIItemTrsRate
                .HeaderText = "Transfer Rate"
                .Name = "TRANSFER_RATE"
                .ReadOnly = True
                .Visible = True
                .Width = 100
            End With
            DGVMIItem.Columns.Add(txtMIItemTrsRate)

            With txtMIItemAcceptQuantity
                .HeaderText = "Accept Quantity"
                .Name = "ACCEPTED_QTY"
                .Visible = True
                .Width = 100
            End With
            DGVMIItem.Columns.Add(txtMIItemAcceptQuantity)

            With txtMIMRSID
                .HeaderText = "MRS_ID"
                .Name = "MRS_ID"
                .Visible = False
                .Width = 10
            End With
            DGVMIItem.Columns.Add(txtMIMRSID)


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error saveClick --> frm_Indent_Master")
        End Try
    End Sub
  
    Private Sub DGVMaterialIssue_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVMaterialIssue.DoubleClick
        Try
            iMIId = Convert.ToInt32(DGVMaterialIssue.SelectedRows.Item(0).Cells(0).Value)
            FillMIInformation(iMIId)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error saveClick --> frm_Indent_Master")
        End Try
    End Sub
    Private Sub FillMIInformation(ByVal iMIId As Int32)
        Try

            Dim ds As DataSet
            Dim dtMI As New DataTable
            Dim dtMIDetail As New DataTable
            'iMIId = Convert.ToInt32(DGVMaterialIssue.SelectedRows.Item(0).Cells(0).Value)
            flag = "update"
            ds = obj.fill_Data_set("GET_MATERIAL_ISSUE_MASTERANDMATERIAL_ISSUE_DETAIL", "@v_MIO_ID", iMIId)
            If ds.Tables.Count > 0 Then
                'Bind the Indent Infromation
                dtMI = ds.Tables(0)
                iMIId = Convert.ToString(dtMI.Rows(0)("MIO_ID"))
                CBMiNo.SelectedValue = Convert.ToString(dtMI.Rows(0)("MIO_ID"))
                lbl_Division.Text = Convert.ToString(dtMI.Rows(0)("Division"))
                lbl_MIStatus.Text = Convert.ToString(dtMI.Rows(0)("MIO_STATUS"))
                lbl_MiRemarks.Text = Convert.ToString(dtMI.Rows(0)("MIO_REMARKS"))
                'Bind the Indent Item Grid
                DGVMarerialIssueItem_style()
                dtMIDetail = ds.Tables(1)
                Dim iRowCount As Int32
                Dim iRow As Int32
                iRowCount = dtMIDetail.Rows.Count
                For iRow = 0 To iRowCount - 1
                    If dtMIDetail.Rows.Count > 0 Then
                        Dim rowindex As Integer = DGVMIItem.Rows.Add()
                        DGVMIItem.Rows(rowindex).Cells(0).Value = Convert.ToInt32(dtMIDetail.Rows(iRow)("ITEM_ID"))
                        DGVMIItem.Rows(rowindex).Cells(1).Value = Convert.ToString(dtMIDetail.Rows(iRow)("ITEM_CODE"))
                        DGVMIItem.Rows(rowindex).Cells(2).Value = Convert.ToString(dtMIDetail.Rows(iRow)("ITEM_NAME"))
                        DGVMIItem.Rows(rowindex).Cells(3).Value = Convert.ToString(dtMIDetail.Rows(iRow)("UM_Name"))
                        DGVMIItem.Rows(rowindex).Cells(4).Value = Convert.ToString(dtMIDetail.Rows(iRow)("REQ_QTY"))
                        DGVMIItem.Rows(rowindex).Cells(5).Value = Convert.ToString(dtMIDetail.Rows(iRow)("ISSUED_QTY"))
                        DGVMIItem.Rows(rowindex).Cells(6).Value = Convert.ToString(dtMIDetail.Rows(iRow)("TRANSFER_RATE"))
                        DGVMIItem.Rows(rowindex).Cells(7).Value = Convert.ToString(dtMIDetail.Rows(iRow)("ACCEPTED_QTY"))
                        DGVMIItem.Rows(rowindex).Cells(8).Value = Convert.ToString(dtMIDetail.Rows(iRow)("MRS_ID"))
                    End If
                Next iRow
                TBCMaretialReceived.SelectTab(1)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error saveClick --> frm_Indent_Master")
        End Try

    End Sub

    Private Sub BindComboMeterialIssueNo()
        Try
            obj.ComboBindWithSP(CBMiNo, "GET_MATERIAL_ISSUE_TO_COST_CENTER_MASTER", "MIO_ID", "MIO_CODE")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error saveClick --> frm_Indent_Master")
        End Try
    End Sub

    Private Sub CBMiNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBMiNo.SelectedIndexChanged
        Try
            iMIId = Convert.ToInt32(CBMiNo.SelectedValue)
            FillMIInformation(iMIId)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error saveClick --> frm_Indent_Master")
        End Try
    End Sub
    Private Sub DGVMIItem_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DGVMIItem.CellValidating
        If e.ColumnIndex = 7 Then
            Dim Number As String = e.FormattedValue
            If Not Decimal.TryParse(Number, Nothing) Then
                MsgBox("Number is not in right format.")
                e.Cancel = True
            End If
        End If
    End Sub
   
    Private Sub DGVMIItem_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVMIItem.CellEndEdit
        Dim Dval As Decimal
        Dval = Convert.ToDecimal(DGVMIItem.Rows(e.RowIndex).Cells("ACCEPTED_QTY").Value)
        'Dim i As Decimal
        'If Len(Dval) <= 3 Then
        '    i = Dval & ".0000"
        'Else
        '    i = Dval
        'End If
        'If Convert.ToDecimal(DGVMIItem.Rows(e.RowIndex).Cells("ACCEPTED_QTY").Value) Then
        If Convert.ToDecimal(DGVMIItem.Rows(e.RowIndex).Cells("ISSUED_QTY").Value) <= Dval Then
            MsgBox("Accepted Quantity Must Be Equal To Less than Issue Quantity", MsgBoxStyle.Exclamation, "Material Received Master")
            DGVMIItem.Rows(e.RowIndex).Cells("ACCEPTED_QTY").Value = "0.0000"
        End If
        Dim DecaccQty As Decimal
        DecaccQty = DGVMIItem.Rows(e.RowIndex).Cells("ACCEPTED_QTY").Value
        If Val(Len(DecaccQty)) >= 5 Then
            MsgBox("Length Of Accepted Quantity Must Be less Than Equal To Five", MsgBoxStyle.Exclamation, "Material Received Master")
        End If

    End Sub
   
    Sub NumericValueDGVMIItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Dim colindex As Decimal = DGVMIItem.CurrentCell.ColumnIndex
        If colindex = 7 Then
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

    Private Sub DGVMIItem_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGVMIItem.EditingControlShowing
        Try
            AddHandler e.Control.KeyPress, AddressOf NumericValueDGVMIItem

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Material Received Master")
        End Try
    End Sub
End Class
