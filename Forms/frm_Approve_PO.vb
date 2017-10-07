Public Class frm_Approve_PO
    Implements IForm
    Dim obj As New CommonClass
    Dim clsObj As New Purchase_Order.cls_Purchase_Order_Master
    Dim ds As DataSet
    Dim ds_Items As DataSet
    Dim _from_PO_status, _to_PO_status As Integer
    Dim _form_rights As Form_Rights

    Public Sub New(ByVal form_heading As String, ByVal from_indent_status As Integer, ByVal to_indent_status As Integer, ByVal rights As Form_Rights)
        InitializeComponent()
        lblFormHeading.Text = form_heading
        _from_PO_status = from_indent_status
        _to_PO_status = to_indent_status
        _form_rights = rights
        If form_heading = "Approve PO" Then
            btnUpdatePO.Text = "Approve PO"
        Else
            btnUpdatePO.Text = "Cancel PO"
        End If
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

    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If grdPOList.Rows.Count > 0 Then
                obj.RptShow(enmReportName.RptPurchaseOrderPrint, "PO_ID", CStr(grdPOList("po_id", grdPOList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                MsgBox("No Records To Print", MsgBoxStyle.Information, gblMessageHeading)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            'If rdoIndentDate.Checked Then
            ds = clsObj.get_PO_date_wise("-1", "-1", dtpIndentFrom.Value.ToString("dd-MMM-yyyy"), dtpIndentTo.Value.ToString("dd-MMM-yyyy"), 1, _from_PO_status, v_the_current_division_id)
            ds.Tables(0).Columns.Add("Select", GetType(System.Boolean))
            grdPOList.DataSource = ds.Tables(0)

            grdPOList.Columns(0).ReadOnly = True
            grdPOList.Columns(0).Visible = False
            grdPOList.Columns(1).ReadOnly = True
            grdPOList.Columns(1).HeaderText = "PO Code"
            grdPOList.Columns(7).Width = 80
            grdPOList.Columns(2).ReadOnly = True
            grdPOList.Columns(2).HeaderText = "PO Date"
            grdPOList.Columns(7).Width = 80
            grdPOList.Columns(3).Visible = False
            grdPOList.Columns(4).ReadOnly = True
            grdPOList.Columns(4).HeaderText = "Supplier"
            grdPOList.Columns(4).Width = 360
            grdPOList.Columns(5).ReadOnly = True
            grdPOList.Columns(5).HeaderText = "Start Date"
            grdPOList.Columns(7).Width = 80
            grdPOList.Columns(6).ReadOnly = True
            grdPOList.Columns(6).HeaderText = "End Date"
            grdPOList.Columns(7).Width = 80
            grdPOList.Columns(7).ReadOnly = True
            grdPOList.Columns(7).HeaderText = "Status"
            grdPOList.Columns(7).Width = 80
            grdPOList.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            grdPOList.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells




            'Else
            'ds = clsObj.getIndentDatewiseOutlet(txtIndentFDate.Text, txtIndentTDate.Text, 1, Convert.ToInt32(IndentStatus.Created), Convert.ToInt32(Session["division_id"]));
            'End If
            select_deselectAll(False)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ->Frm_Approve_Indent.btnShow_Click")
        End Try
    End Sub
    Private Sub select_deselectAll(ByVal true_false As Boolean)
        Dim i As Integer
        For i = 0 To grdPOList.Rows.Count - 1
            grdPOList.Rows(i).Cells(8).Value = true_false
        Next
    End Sub
    Private Sub frm_Approve_Indent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' clsObj.FormatGrid(grdPOList)

    End Sub

    Private Sub btnApproveIndent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePO.Click
        Try
            If MsgBox("Are you sure to do this?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.No Then Exit Sub
            Dim i As Integer
            Dim po_ids As String
            po_ids = ""

            For i = 0 To grdPOList.Rows.Count - 1
                If grdPOList.Rows(i).Cells(8).Value = True Then
                    po_ids = po_ids & grdPOList.Rows(i).Cells(0).Value & ","
                End If
            Next
            po_ids = po_ids.Substring(0, po_ids.Length - 1)
            clsObj.po_status_change(po_ids, _to_PO_status)

            MsgBox("Selected Purchase Orders are updated Successfully.", MsgBoxStyle.Information, gblMessageHeading)
            btnShow_Click(Nothing, Nothing)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDeselectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselectAll.Click
        select_deselectAll(False)
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        select_deselectAll(True)
    End Sub

    Private Sub btInverseSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btInverseSelect.Click
        Dim i As Integer
        For i = 0 To grdPOList.Rows.Count - 1
            grdPOList.Rows(i).Cells(8).Value = Not grdPOList.Rows(i).Cells(8).Value
        Next
    End Sub
End Class
