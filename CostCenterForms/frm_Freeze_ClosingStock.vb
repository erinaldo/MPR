Public Class frm_Freeze_ClosingStock
    Implements IForm

    Dim clsObj As New Closing_master.cls_Closing_master
    Dim prpty As New Closing_master.cls_Closing_master_prop
    Dim obj As New CommonClass
    Dim ds As DataSet
    Dim ds_Items As DataSet


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

    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If grdclstklist.Rows.Count > 0 Then
                ''obj.RptShow(enmReportName.RptIndentDetailPrint, "indent_id", CStr(grdclstklist("indent_id", grdclstklist.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                'obj.RptShow(enmReportName.RptIndentDetailPrint, "MRS_ID", CStr(grdclstklist("MRS_ID", grdclstklist.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                'obj.RptShow(enmReportName.RptMrsItemList, "MRS_ID", CStr(grdclstklist("MRS_ID", grdclstklist.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                MsgBox("No Records To Print", MsgBoxStyle.Information, gblMessageHeading)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try

            ds = clsObj.GET_CLOSING_MASTER_DATEWISE(dtpMRSFrom.Value.Date, dtpMRSTo.Value.Date, v_the_current_Selected_CostCenter_id)
            ds.Tables(0).Columns.Add("Select", GetType(System.Boolean))
            grdclstklist.DataSource = ds.Tables(0)


            grdclstklist.Columns(0).Visible = False
            grdclstklist.Columns(0).ReadOnly = True
            'grdclstklist.Columns(1).Visible = False
            grdclstklist.Columns(1).ReadOnly = True
            grdclstklist.Columns(1).HeaderText = "Closing Code"
            grdclstklist.Columns(1).Name = "Closing_Code"
            grdclstklist.Columns(2).ReadOnly = True
            grdclstklist.Columns(2).HeaderText = "Closing Date"
            grdclstklist.Columns(2).Name = "Closing_Date"
            grdclstklist.Columns(3).ReadOnly = True
            grdclstklist.Columns(3).HeaderText = "Remarks"
            grdclstklist.Columns(3).Name = "Closing_Remarks"
            grdclstklist.Columns(4).ReadOnly = True
            grdclstklist.Columns(4).HeaderText = "Status"
            grdclstklist.Columns(4).Name = "Closing_status"
            grdclstklist.Columns(5).Visible = False
            grdclstklist.Columns(5).ReadOnly = True


            'grdclstklist.Columns(0).Visible = False
            'grdclstklist.Columns(1).Visible = False
            'grdclstklist.Columns(6).Visible = False
            'grdclstklist.Columns(8).Visible = False
            '' grdclstklist.Columns(9).Visible = False


            'grdclstklist.Columns(5).Width = 200

            'grdclstklist.Columns(2).HeaderText = "Closing Code"
            'grdclstklist.Columns(3).HeaderText = "Closing Date"
            'grdclstklist.Columns(5).HeaderText = "Remarks"
            'grdclstklist.Columns(7).HeaderText = "Status"
            ''grdclstklist.Columns(9).HeaderText = "indent_id"




            'grdclstklist.Columns(2).ReadOnly = True
            'grdclstklist.Columns(3).ReadOnly = True
            'grdclstklist.Columns(4).ReadOnly = True
            'grdclstklist.Columns(5).ReadOnly = True
            'grdclstklist.Columns(7).ReadOnly = True
            'grdclstklist.Columns(9).ReadOnly = True




            'grdclstklist.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            'grdclstklist.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells




            
            select_deselectAll(False)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ->Frm_Freeze_Closing_Stock.btnShow_Click")
        End Try
    End Sub
    Private Sub select_deselectAll(ByVal true_false As Boolean)
        Dim i As Integer
        For i = 0 To grdclstklist.Rows.Count - 1
            grdclstklist.Rows(i).Cells(6).Value = true_false
        Next
    End Sub
    Private Sub frm_Approve_Indent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'clsObj.FormatGrid(grdclstklist)

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

            For i = 0 To grdclstklist.Rows.Count - 1
                If grdclstklist.Rows(i).Cells(6).Value = True Then
                    prpty.Closing_ID = Convert.ToInt32(grdclstklist.Rows(i).Cells(0).Value)

                    clsObj.Freeze_Closing_Stock(prpty)
                End If
            Next

            MsgBox("Selected Closing Stock has been freezed Successfully.", MsgBoxStyle.Information, gblMessageHeading)
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
        For i = 0 To grdclstklist.Rows.Count - 1
            grdclstklist.Rows(i).Cells(6).Value = Not grdclstklist.Rows(i).Cells(6).Value
        Next
    End Sub
End Class
