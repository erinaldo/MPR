Public Class frm_Approve_MRS
    Implements IForm

    Dim clsObj As New mrs_main_store_master.cls_mrs_main_store_master
    Dim obj As New CommonClass
    Dim ds As DataSet
    Dim ds_Items As DataSet
    Dim _from_mrs_status, _to_mrs_status As Integer

    Dim _form_rights As Form_Rights
    Public Sub New(ByVal form_heading As String, ByVal from_mrs_status As Integer, ByVal to_mrs_status As Integer, ByVal rights As Form_Rights)
        InitializeComponent()
        lblFormHeading.Text = form_heading
        _from_mrs_status = from_mrs_status
        _to_mrs_status = to_mrs_status
        _form_rights = rights

        If form_heading = "Approve MRS" Then
            btnUpdate.Text = "Approve MRS"
        Else
            btnUpdate.Text = "Cancel MRS"
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
            If grdMRSList.Rows.Count > 0 Then
                ''obj.RptShow(enmReportName.RptIndentDetailPrint, "indent_id", CStr(grdMRSList("indent_id", grdMRSList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                'obj.RptShow(enmReportName.RptIndentDetailPrint, "MRS_ID", CStr(grdMRSList("MRS_ID", grdMRSList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                obj.RptShow(enmReportName.RptMrsItemList, "MRS_ID", CStr(grdMRSList("MRS_ID", grdMRSList.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                MsgBox("No Records To Print", MsgBoxStyle.Information, gblMessageHeading)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            If rdoMRSDate.Checked Then
                ds = clsObj.get_mrs_date_wise(dtpMRSFrom.Value.Date, dtpMRSTo.Value.Date, 1, _from_mrs_status, v_the_current_division_id)
                ds.Tables(0).Columns.Add("Select", GetType(System.Boolean))
                grdMRSList.DataSource = ds.Tables(0)


                'grdMRSList.Columns(0).Visible = False
                'grdMRSList.Columns(0).ReadOnly = True
                'grdMRSList.Columns(1).Visible = False
                'grdMRSList.Columns(1).ReadOnly = True
                'grdMRSList.Columns(1).HeaderText = "MRS Code"
                'grdMRSList.Columns(2).ReadOnly = True
                'grdMRSList.Columns(2).HeaderText = "MRS Code"
                'grdMRSList.Columns(3).ReadOnly = True
                'grdMRSList.Columns(3).HeaderText = "MRS Date"
                'grdMRSList.Columns(4).ReadOnly = True
                'grdMRSList.Columns(4).HeaderText = "Required Date"
                'grdMRSList.Columns(4).Width = 350
                'grdMRSList.Columns(5).ReadOnly = True
                'grdMRSList.Columns(5).Visible = False
                'grdMRSList.Columns(6).ReadOnly = True
                'grdMRSList.Columns(6).HeaderText = "MRS Status"
                'grdMRSList.Columns(7).ReadOnly = True
                'grdMRSList.Columns(7).Visible = False


                grdMRSList.Columns(0).Visible = False
                grdMRSList.Columns(1).Visible = False
                grdMRSList.Columns(6).Visible = False
                grdMRSList.Columns(8).Visible = False
                ' grdMRSList.Columns(9).Visible = False

                grdMRSList.Columns(2).HeaderText = "MRS Code"
                grdMRSList.Columns(2).Width = 100
                grdMRSList.Columns(3).HeaderText = "MRS Date"
                grdMRSList.Columns(3).Width = 100
                grdMRSList.Columns(4).HeaderText = "Required Date"
                grdMRSList.Columns(4).Width = 120
                grdMRSList.Columns(5).HeaderText = "MRS Remarks"
                grdMRSList.Columns(5).Width = 230
                grdMRSList.Columns(7).HeaderText = "Status"
                grdMRSList.Columns(7).Width = 100
                grdMRSList.Columns(9).HeaderText = "Store Name"
                grdMRSList.Columns(9).Width = 100
                'grdMRSList.Columns(9).HeaderText = "indent_id"




                grdMRSList.Columns(2).ReadOnly = True
                grdMRSList.Columns(3).ReadOnly = True
                grdMRSList.Columns(4).ReadOnly = True
                grdMRSList.Columns(5).ReadOnly = True
                grdMRSList.Columns(7).ReadOnly = True
                grdMRSList.Columns(9).ReadOnly = True




                'grdMRSList.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                'grdMRSList.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells




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
        For i = 0 To grdMRSList.Rows.Count - 1
            grdMRSList.Rows(i).Cells(10).Value = true_false
        Next
    End Sub
    Private Sub frm_Approve_Indent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' clsObj.FormatGrid(grdMRSList)

    End Sub

    Private Sub btnApproveIndent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click


        'Int32 i;
        'String Indentno = "";
        'for (i = 0; i < grdIndents.Rows.Count; i++)
        '{
        '    if (((CheckBox)grdIndents.Rows[i].FindControl("chkSelect")).Checked == true)
        '    {
        '        Indentno = Indentno + " " + ((Label)grdIndents.Rows[i].FindControl("lblIndentID")).Text + ",";
        '    }
        '}
        '        If (Indentno.Length < 1) Then
        '{
        '    lblmsg.Text = "Checked Indent not found";
        '}
        '        Else
        '{
        '    Indentno = Indentno.Substring(0, Indentno.Length - 1);
        '    //clsObj.changeStatus(Indentno, Convert.ToInt32(IndentStatus.Pending));
        '    clsObj.changeStatus(Indentno, Convert.ToInt32(IndentStatus.Fresh));
        '    showdata();
        '    lblmsg.Text = "Selected Indent/Indent's Approved Successfully.";
        '}
        Try
            If MsgBox("Are you sure to do this?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.No Then Exit Sub
            Dim i As Integer
            Dim mrs_ids As String
            mrs_ids = ""

            For i = 0 To grdMRSList.Rows.Count - 1
                If grdMRSList.Rows(i).Cells(10).Value = True Then
                    mrs_ids = mrs_ids & grdMRSList.Rows(i).Cells(0).Value & ","
                End If
            Next
            mrs_ids = mrs_ids.Substring(0, mrs_ids.Length - 1)
            clsObj.MRS_status_change(mrs_ids, _to_mrs_status)

            MsgBox("Selected MRS are updated Successfully.", MsgBoxStyle.Information, gblMessageHeading)
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
        For i = 0 To grdMRSList.Rows.Count - 1
            grdMRSList.Rows(i).Cells(10).Value = Not grdMRSList.Rows(i).Cells(10).Value
        Next
    End Sub
End Class
