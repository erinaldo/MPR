Public Class frm_Approve_Indent
    Implements IForm

    Dim clsObj As New indent_master.cls_indent_master
    Dim obj As New CommonClass
    Dim ds As DataSet
    Dim ds_Items As DataSet
    Dim _from_indent_status, _to_indent_status As Integer
    Dim _form_rights As Form_Rights

    Public Sub New(ByVal form_heading As String, ByVal from_indent_status As Integer, ByVal to_indent_status As Integer, ByVal rights As Form_Rights)
        InitializeComponent()
        lblFormHeading.Text = form_heading
        _from_indent_status = from_indent_status
        _to_indent_status = to_indent_status
        _form_rights = rights

        If form_heading = "Approve Indent" Then
            btnApproveIndent.Text = "Approve Indent"
        Else
            btnApproveIndent.Text = "Cancel Indent"
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
            If grdIndentList.Rows.Count > 0 Then
                obj.RptShow(enmReportName.RptIndentDetailPrint, "indent_id", CStr(grdIndentList("indent_id", grdIndentList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                MsgBox("No Records To Print", MsgBoxStyle.Information, gblMessageHeading)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            If rdoIndentDate.Checked Then
                ds = clsObj.get_indent_date_wise(dtpIndentFrom.Value.Date, dtpIndentTo.Value.Date, 1, _from_indent_status, v_the_current_division_id)
                ds.Tables(0).Columns.Add("Select", GetType(System.Boolean))
                grdIndentList.DataSource = ds.Tables(0)

                grdIndentList.Columns(0).ReadOnly = True
                grdIndentList.Columns(0).Visible = False
                grdIndentList.Columns(1).ReadOnly = True
                grdIndentList.Columns(1).HeaderText = "Indent Code"
                grdIndentList.Columns(1).Width = 110
                grdIndentList.Columns(2).ReadOnly = True
                grdIndentList.Columns(2).HeaderText = "Indent Date"
                grdIndentList.Columns(2).Width = 110
                grdIndentList.Columns(3).ReadOnly = True
                grdIndentList.Columns(3).HeaderText = "Required Date"
                grdIndentList.Columns(3).Width = 120
                grdIndentList.Columns(4).ReadOnly = True
                grdIndentList.Columns(4).HeaderText = "Remarks"
                grdIndentList.Columns(4).Width = 360
                grdIndentList.Columns(5).ReadOnly = True
                grdIndentList.Columns(5).Visible = False
                grdIndentList.Columns(6).ReadOnly = True
                grdIndentList.Columns(6).HeaderText = "Indent Status"
                grdIndentList.Columns(6).Width = 130
                grdIndentList.Columns(7).ReadOnly = True
                grdIndentList.Columns(7).Visible = False
                grdIndentList.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                grdIndentList.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells




            Else
                'ds = clsObj.getIndentDatewiseOutlet(txtIndentFDate.Text, txtIndentTDate.Text, 1, Convert.ToInt32(IndentStatus.Created), Convert.ToInt32(Session["division_id"]));
            End If
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

    Private Sub btnApproveIndent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApproveIndent.Click

        Try
            If MsgBox("Are you sure to do this?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading_confirm) = MsgBoxResult.No Then Exit Sub
            Dim i As Integer
            Dim indent_ids As String
            indent_ids = ""

            For i = 0 To grdIndentList.Rows.Count - 1
                If grdIndentList.Rows(i).Cells(8).Value = True Then
                    indent_ids = indent_ids & grdIndentList.Rows(i).Cells(0).Value & ","
                End If
            Next
            indent_ids = indent_ids.Substring(0, indent_ids.Length - 1)
            clsObj.indent_status_change(indent_ids, _to_indent_status)

            MsgBox("Selected Indents are updated Successfully.", MsgBoxStyle.Information, gblMessageHeading)
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
