Public Class frm_freeze_stock_transfer
    Implements IForm

    Dim clsobj As New Stock_Transfer.cls_Stock_Transfer_CC_To_CC
    Dim prpty As New Stock_Transfer.cls_Stock_Transfer_CC_To_CC_Prop
    'Dim clsObj As New mrs_main_store_master.cls_mrs_main_store_master
    Dim obj As New CommonClass
    Dim ds As DataSet
    Dim ds_Items As DataSet
    Dim _from_mrs_status, _to_mrs_status As Integer


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
            If grdstocklist.Rows.Count > 0 Then
                obj.RptShow(enmReportName.RptAcceptStockCCPrint, "Transfer_ID", CStr(grdstocklist("Transfer_ID", grdstocklist.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                MsgBox("No Records To Print", MsgBoxStyle.Information, gblMessageHeading)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            'If rdoMRSDate.Checked Then
            Dim STATUS As Integer
            If rdbFresh.Checked = True Then
                STATUS = 1
                btnUpdate.Enabled = True
            Else
                STATUS = 2
                btnUpdate.Enabled = False
            End If

            ds = clsobj.get_stocktransfered_date_wise(dtpfrmdt.Value.Date, dtptodt.Value.Date, v_the_current_Selected_CostCenter_id, STATUS)
            ds.Tables(0).Columns.Add("Select", GetType(System.Boolean))
            grdstocklist.DataSource = ds.Tables(0)


            grdstocklist.Columns(0).Visible = False
            grdstocklist.Columns(0).ReadOnly = True
            grdstocklist.Columns(1).Visible = True
            grdstocklist.Columns(1).ReadOnly = True
            grdstocklist.Columns(1).HeaderText = "Transfer Code"
            grdstocklist.Columns(2).ReadOnly = True
            grdstocklist.Columns(2).HeaderText = "Transfer Date"
            grdstocklist.Columns(3).ReadOnly = True
            grdstocklist.Columns(3).HeaderText = "Cost Center Name"
            grdstocklist.Columns(4).ReadOnly = True
            grdstocklist.Columns(4).HeaderText = "Transfer Status"
            grdstocklist.Columns(5).ReadOnly = True
            grdstocklist.Columns(5).Visible = True
            grdstocklist.Columns(5).HeaderText = "Transfer Remarks"

            select_deselectAll(False)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error ->Frm_Accept_Stock.btnShow_Click")
        End Try
    End Sub
    Private Sub select_deselectAll(ByVal true_false As Boolean)
        Dim i As Integer
        For i = 0 To grdstocklist.Rows.Count - 1
            grdstocklist.Rows(i).Cells(6).Value = true_false
        Next
    End Sub
    Private Sub frm_Approve_Indent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        clsObj.FormatGrid(grdstocklist)

    End Sub

    Private Sub btnApproveIndent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click


        Try
            If MsgBox("Are you sure to do this?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.No Then Exit Sub
            Dim i As Integer

            For i = 0 To grdstocklist.Rows.Count - 1
                If grdstocklist.Rows(i).Cells(6).Value = True Then
                    prpty.Received_Date = Now()
                    prpty.Transfer_ID = Convert.ToInt32(grdstocklist.Rows(i).Cells(0).Value)
                    clsobj.Accept_Stock(prpty)
                End If
            Next


            MsgBox("Selected Stock has been transfered Successfully.", MsgBoxStyle.Information, gblMessageHeading)
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
        For i = 0 To grdstocklist.Rows.Count - 1
            grdstocklist.Rows(i).Cells(6).Value = Not grdstocklist.Rows(i).Cells(6).Value
        Next
    End Sub
End Class
