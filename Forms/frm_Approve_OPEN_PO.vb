Public Class frm_Approve_OPEN_PO
    Implements IForm
    Dim obj As New CommonClass
    Dim clsObj As New open_po_master.cls_open_po_master
    Dim ds As DataSet
    Dim ds_Items As DataSet
    Dim _from_open_po_status, _to_open_po_status As Integer
    Dim _form_rights As Form_Rights

    Public Sub New(ByVal form_heading As String, ByVal from_indent_status As Integer, ByVal to_indent_status As Integer, ByVal rights As Form_Rights)
        InitializeComponent()
        lblFormHeading.Text = form_heading
        _from_open_po_status = from_indent_status
        _to_open_po_status = to_indent_status
        _form_rights = rights
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
            If grdIndentList.Rows.Count > 0 Then
                obj.RptShow(enmReportName.RptOpenPurchaseOrderPrint, "PO_ID", CStr(grdIndentList("po_id", grdIndentList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
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
            ds = clsObj.get_OPEN_PO_date_wise("-1", "-1", dtpIndentFrom.Value.Date, dtpIndentTo.Value.Date, 1, _from_open_po_status, v_the_current_division_id)
            ds.Tables(0).Columns.Add("Select", GetType(System.Boolean))
            grdIndentList.DataSource = ds.Tables(0)

            grdIndentList.Columns(0).ReadOnly = True
            grdIndentList.Columns(0).Visible = False
            grdIndentList.Columns(1).ReadOnly = True
            grdIndentList.Columns(1).HeaderText = "PO Code"
            grdIndentList.Columns(2).ReadOnly = True
            grdIndentList.Columns(2).HeaderText = "PO Date"
            grdIndentList.Columns(3).ReadOnly = True
            grdIndentList.Columns(3).Visible = False
            grdIndentList.Columns(4).ReadOnly = True
            grdIndentList.Columns(4).HeaderText = "Supplier"
            grdIndentList.Columns(4).Width = 450
            grdIndentList.Columns(5).ReadOnly = True
            grdIndentList.Columns(5).HeaderText = "Start Date"
            grdIndentList.Columns(6).ReadOnly = True
            grdIndentList.Columns(6).HeaderText = "End Date"
            grdIndentList.Columns(7).ReadOnly = True
            grdIndentList.Columns(7).HeaderText = "PO Status"
            grdIndentList.Columns(7).Width = 80

            grdIndentList.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            grdIndentList.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells




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
        For i = 0 To grdIndentList.Rows.Count - 1
            grdIndentList.Rows(i).Cells(8).Value = true_false
        Next
    End Sub

    Private Sub frm_Approve_Indent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' clsObj.FormatGrid(grdIndentList)

    End Sub

    Private Sub btnApproveIndent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdatePO.Click
        Try
            If MsgBox("Are you sure to do this?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.No Then Exit Sub
            Dim i As Integer
            Dim PO_ids As String
            PO_ids = ""

            For i = 0 To grdIndentList.Rows.Count - 1
                If grdIndentList.Rows(i).Cells(8).Value = True Then
                    PO_ids = PO_ids & grdIndentList.Rows(i).Cells(0).Value & ","
                End If
            Next
            PO_ids = PO_ids.Substring(0, PO_ids.Length - 1)
            clsObj.OPEN_po_status_change(PO_ids, _to_open_po_status)

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
        For i = 0 To grdIndentList.Rows.Count - 1
            grdIndentList.Rows(i).Cells(8).Value = Not grdIndentList.Rows(i).Cells(8).Value
        Next
    End Sub

End Class
