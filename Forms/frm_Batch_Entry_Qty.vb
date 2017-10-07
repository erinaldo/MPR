Public Class frm_Batch_Entry_Qty

    Public dTable_MaterialItems As DataTable
    Dim v_frm_mat_rec_against_po As frm_material_rec_against_PO

    Private Sub frm_Batch_Entry_Qty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim tbtemp As New DataTable("Table")
        'tbtemp.Columns.Add(New DataColumn("Batch_No", Type.GetType("System.String")))
        'tbtemp.Columns.Add(New DataColumn("Expiry_Date", Type.GetType("System.DateTime")))
        'tbtemp.Columns.Add(New DataColumn("Item_Quantity", Type.GetType("System.Double")))
        InitilizeGrid()
    End Sub

    Public Sub InitilizeGrid()
        dTable_MaterialItems = New DataTable("PO_Items")
        'dTable_MaterialItems.Columns.Add("Item_ID", GetType(System.Int32))
        dTable_MaterialItems.Columns.Add("Batch_No", GetType(System.String))
        dTable_MaterialItems.Columns.Add("Expiry_Date", GetType(System.DateTime))
        dTable_MaterialItems.Columns.Add("Item_Qty", GetType(System.Double))
        'dTable_MaterialItems.Rows.Add("test", DateTime.Now, 10)
        'grdMaterialQty.DataSource = dTable_MaterialItems
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If txtBatchNo.Text.Length <= 0 Then
            erp.SetError(txtBatchNo, "Please Enter Batch No.")
            Exit Sub
        Else
            erp.Clear()
        End If
        If txtQuantity.Text.Length <= 0 Then
            erp.SetError(txtQuantity, "Please Enter Quantity")
            Exit Sub
        Else
            erp.Clear()
        End If
        'v_frm_mat_rec_against_po = New frm_material_rec_against_PO()
        dTable_MaterialItems.Rows.Add(txtBatchNo.Text, dtpMaterialDate.Value, txtQuantity.Text)
        grdMaterialQty.DataSource = dTable_MaterialItems
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class