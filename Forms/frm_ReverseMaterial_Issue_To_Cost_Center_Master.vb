Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports System.Data

Public Class frm_ReverseMaterial_Issue_To_Cost_Center_Master
    Implements IForm

    Dim obj As New CommonClass
    Dim clsObj As New Reversematerial_issue_to_cost_center_master.cls_Reversematerial_issue_to_cost_center_master
    Dim prpty As New Reversematerial_issue_to_cost_center_master.cls_Reversematerial_issue_to_cost_center_master_prop
    Dim Flag As String
    Dim dtable_Item_List As DataTable
    Dim TemporaryTable As DataTable
    Dim TemporaryRow As DataRow
    Dim Ds As New DataSet
    'Dim RMIOID As Int32
    'Dim MRSID As Int32
    Dim Query As String

    Dim MIC_Id As Integer

    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_Material_Issue_To_Cost_Center_Master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Flag = "save"
            dtpFromDate.Value = now
            dtpToDate.Value = Now
            '  obj.FormatGrid(flx_Material_Issue_List)
            '  obj.FormatGrid(flxGrd_ItemList)
            FillDetail_Grid()
            BindIssueSlipCombo()
            new_initilization()
            '  BindMRSCombo()
            ' table_style()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        Finally
        End Try
    End Sub

    Private Sub BindIssueSlipCombo()
        '
        Query = " SELECT " & _
                        " MIO_id, " & _
                        " MIO_CODE+CAST(MIO_NO AS VARCHAR(10)) AS MIO_No " & _
                " FROM " & _
                        " MATERIAL_ISSUE_TO_COST_CENTER_master " & _
         " WHERE " & _
         " DIVISION_ID=" & v_the_current_division_id

        TemporaryTable = clsObj.Fill_DataSet(Query).Tables(0)
        TemporaryRow = TemporaryTable.NewRow
        TemporaryRow("MIO_Id") = -1
        TemporaryRow("MIO_No") = "Select Issue Slip"
        TemporaryTable.Rows.InsertAt(TemporaryRow, 0)
        cmbIssueSlip.DisplayMember = "MIO_No"
        cmbIssueSlip.ValueMember = "MIO_ID"
        cmbIssueSlip.DataSource = TemporaryTable
        cmbIssueSlip.SelectedIndex = 0

    End Sub

    Private Sub cmbIssueSlip_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbIssueSlip.SelectedIndexChanged
        'BindMRSCombo()
        MIC_Id = cmbIssueSlip.SelectedValue
        FillGrid()
    End Sub

    Private Sub FillGrid(Optional ByVal iSt As Boolean = False)
        Try

            Query = " SELECT  " & _
                            " MD.ITEM_ID, " & _
                            " IM.ITEM_CODE, " & _
                            " IM.ITEM_NAME," & _
                            " UM.UM_Name, " & _
                            " MD.Item_Rate as TRANSFER_RATE, " & _
                            " dbo.fn_format(Sd.Expiry_date) as Expiry_date, " & _
                            " Sd.Batch_no, " & _
                            " MD.BalISSUED_QTY as Issued_Qty, " & _
                            " 0.0 AS ReturnQTy, " & _
                            " MD.STOCK_DETAIL_ID, " & _
                            " MD.MIO_ID " & _
                    " FROM " & _
                            " MATERIAL_ISSUE_TO_COST_CENTER_DETAIL MD " & _
                            " INNER JOIN ITEM_MASTER IM ON MD.ITEM_ID = IM.ITEM_ID " & _
                            " INNER JOIN UNIT_MASTER UM ON IM.UM_ID=UM.UM_ID " & _
                            " Inner JOIN STOCK_DETAIL SD ON MD.STOCK_DETAIL_ID=SD.STOCK_DETAIL_ID " & _
                    " WHERE " & _
                            " MD.MIO_ID =" & MIC_Id



            TemporaryTable = clsObj.Fill_DataSet(Query).Tables(0)
            flxGrd_ItemList.DataSource = TemporaryTable


        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        TbReverseIssue.SelectTab(0)
        FillDetail_Grid()

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            new_initilization()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub new_initilization()
        Flag = "save"
        obj.Clear_All_ComoBox(Me.GroupBox1.Controls)
        '  obj.Clear_All_DTPicker(Me.GroupBox1.Controls)
        obj.Clear_All_TextBox(Me.GroupBox1.Controls)
        cmbIssueSlip.Focus()
        TbReverseIssue.SelectTab(1)
    End Sub

    Private Function validation() As Boolean

        validation = True
        Dim int_RowIndex As Int32
        Dim iindex As Int32
        Dim blnIsExist As Boolean
        Dim dtCheck As New DataTable
        If cmbIssueSlip.SelectedIndex <= 0 Then
            MsgBox("Select Issue Slip No.", vbExclamation, gblMessageHeading)
            validation = False
            cmbIssueSlip.Focus()
            Exit Function
        End If

        blnIsExist = False
        dtCheck = flxGrd_ItemList.DataSource
        int_RowIndex = dtCheck.Rows.Count

        For iindex = 0 To int_RowIndex - 1
            If dtCheck.Rows(iindex)("ReturnQty") > 0 Then
                blnIsExist = True
            End If
        Next iindex

        If blnIsExist = False Then
            validation = False
            MsgBox("Enter atleast One Reverse Quantity.", vbExclamation, gblMessageHeading)
            Exit Function
        End If

    End Function

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Dim cmd As SqlCommand
        If validation() = False Then
            Exit Sub
        End If
        cmd = obj.MyCon_BeginTransaction
        Try
            If Flag = "save" Then
                Dim RMIO_ID As Integer
                Dim RMIO_Code As String
                RMIO_Code = obj.getPrefixCode("RMIO_PREFIX", "DIVISION_SETTINGS")
                RMIO_ID = Convert.ToInt32(obj.getMaxValue("RMIO_ID", "ReverseMATERIAL_ISSUE_TO_COST_CENTER_MASTER"))
                'If grdMRNDetail.Rows.Count - 1 Then
                prpty.RMIO_ID = RMIO_ID
                prpty.RMIO_CODE = RMIO_Code  ' GetRMRSCode()
                prpty.RMIO_NO = RMIO_ID
                prpty.RMIO_DATE = now 'Convert.ToDateTime(dtpMIODate.Text).ToString()
                prpty.ISSUE_ID = MIC_Id 'Convert.ToDecimal(cmbCostCenter.SelectedValue)
                prpty.RMIO_REMARKS = txtRemarks.Text
                prpty.CREATED_BY = v_the_current_logged_in_user_name
                prpty.CREATION_DATE = now
                prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                prpty.MODIFIED_DATE = now
                prpty.DIVISION_ID = v_the_current_division_id
                clsObj.insert_ReverseMATERIAL_ISSUE_TO_COST_CENTER_MASTER(prpty, cmd)

                Dim iRowCount As Int32
                Dim iRow As Int32


                Dim dtDetail As New DataTable
                dtDetail = flxGrd_ItemList.DataSource
                iRowCount = dtDetail.Rows.Count
                For iRow = 0 To iRowCount - 1
                    If dtDetail.Rows(iRow)("ReturnQty") > 0 Then
                        prpty.RMIO_ID = RMIO_ID
                        prpty.ITEM_ID = dtDetail.Rows(iRow)("Item_Id")

                        prpty.ITEM_QTY = Convert.ToDouble(dtDetail.Rows(iRow)("ReturnQty")) '.ToString()
                        prpty.AVG_RATE = dtDetail.Rows(iRow)("Transfer_Rate")
                        prpty.Stock_Detail_Id = Convert.ToInt32(dtDetail.Rows(iRow)("Stock_Detail_Id"))
                        prpty.TransDate = Now
                        prpty.TransType = Transaction_Type.MaterialReturnAgainstIssue
                        clsObj.insert_ReverseMATERIAL_ISSUE_TO_COST_CENTER_Detail(prpty, cmd)

                    End If
                Next iRow
            End If
            obj.MyCon_CommitTransaction(cmd)
            If Flag = "save" Then
                Dim Msg As String = "Reverse Material infromation saved with no. " & prpty.RMIO_CODE & prpty.RMIO_ID
                If MsgBox(Msg & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptRevMICostCenterPrint, "rmio_id", CStr(prpty.RMIO_ID), CStr(enmDataType.D_int))
                End If
                new_initilization()
            End If
        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Function GetRMRSCode() As String

        GetRMRSCode = obj.getPrefixCode("RMIO_PREFIX", "DIVISION_SETTINGS")

    End Function

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TbReverseIssue.SelectedIndex = 0 Then
                obj.RptShow(enmReportName.RptRevMICostCenterPrint, "rmio_id", CStr(flx_Material_Issue_List.Rows(flx_Material_Issue_List.CursorCell.r1).Item("RMIO_ID").ToString()), CStr(enmDataType.D_int))
            Else
                If Flag <> "save" Then
                    '    obj.RptShow(enmReportName.RptOpenPurchaseOrderPrint, "PO_ID", CStr(_po_id), CStr(enmDataType.D_int))
                    obj.RptShow(enmReportName.RptRevMICostCenterPrint, "rmio_id", CStr(flx_Material_Issue_List.Rows(flx_Material_Issue_List.CursorCell.r1).Item("RMIO_ID").ToString()), CStr(enmDataType.D_int))

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub lnkSelectItems_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        frm_MRN_ITEMS.Show()
    End Sub



    Private Sub FillDetail_Grid()
        Try
            flx_Material_Issue_List.Cols.Fixed = 1
            flx_Material_Issue_List.DataSource = clsObj.fill_Data_set("GET_ReverseMATERIAL_ISSUE_DETAIL", "@FromDate,@ToDate", dtpFromDate.Value & "," & dtpToDate.Value).Tables(0)

            flx_Material_Issue_List.Cols(0).Width = 10

            flx_Material_Issue_List.Cols("RMIO_Id").Caption = "RMIO ID"
            flx_Material_Issue_List.Cols("RMIO_Id").Visible = False

            flx_Material_Issue_List.Cols("RMIO_No").Caption = "RMIO No"
            flx_Material_Issue_List.Cols("RMIO_No").AllowEditing = False
            flx_Material_Issue_List.Cols("RMIO_No").Width = 100

            flx_Material_Issue_List.Cols("RMIO_Date").Caption = "RMIO Date"
            flx_Material_Issue_List.Cols("RMIO_Date").AllowEditing = False
            flx_Material_Issue_List.Cols("RMIO_Date").Width = 100

            flx_Material_Issue_List.Cols("Issue_No").Caption = "Issue No"
            flx_Material_Issue_List.Cols("Issue_No").AllowEditing = False
            flx_Material_Issue_List.Cols("Issue_No").Width = 100

            flx_Material_Issue_List.Cols("Issue_Id").Caption = "Issue Id"
            flx_Material_Issue_List.Cols("Issue_Id").AllowEditing = False
            flx_Material_Issue_List.Cols("Issue_Id").Visible = False

            flx_Material_Issue_List.Cols("MRS_NO").Caption = "MRS No"
            flx_Material_Issue_List.Cols("MRS_NO").AllowEditing = False
            flx_Material_Issue_List.Cols("MRS_NO").Width = 100


            flx_Material_Issue_List.Cols("RMIO_REMARKS").Caption = "Remarks"
            flx_Material_Issue_List.Cols("RMIO_REMARKS").AllowEditing = False

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub grd_MaterialIssue_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles flxGrd_ItemList.AfterDataRefresh
        generate_tree()
    End Sub

    Private Sub generate_tree()

        If flxGrd_ItemList.Rows.Count > 1 Then
            Dim strSort As String = flxGrd_ItemList.Cols(1).Name + ", " + flxGrd_ItemList.Cols(2).Name + ", " + flxGrd_ItemList.Cols(3).Name
            Dim dt As DataTable = CType(flxGrd_ItemList.DataSource, DataTable)
            If Not dt Is Nothing Then
                dt.DefaultView.Sort = strSort
            End If

            flxGrd_ItemList.Tree.Style = TreeStyleFlags.CompleteLeaf
            flxGrd_ItemList.Tree.Column = 2
            flxGrd_ItemList.AllowMerging = AllowMergingEnum.None

            Dim totalOn As Integer = flxGrd_ItemList.Cols("Issued_Qty").SafeIndex
            flxGrd_ItemList.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)

            flxGrd_ItemList.Cols(0).Width = 10
            flxGrd_ItemList.Cols(1).Visible = False

            flxGrd_ItemList.Cols("ITEM_CODE").Caption = "Item Code"
            flxGrd_ItemList.Cols("ITEM_NAME").Caption = "Item Name"
            flxGrd_ItemList.Cols("UM_Name").Caption = "UOM"
            flxGrd_ItemList.Cols("Transfer_rate").Caption = "Avg. Rate"
            flxGrd_ItemList.Cols("Expiry_date").Caption = "Expiry Date"
            flxGrd_ItemList.Cols("Batch_no").Caption = "Batch No."
            flxGrd_ItemList.Cols("Issued_Qty").Caption = "Issued Qty."
            flxGrd_ItemList.Cols("ReturnQty").Caption = "Return Qty"
            flxGrd_ItemList.Cols("Stock_Detail_Id").Caption = "Stock_Detail_Id"
            flxGrd_ItemList.Cols("MIO_ID").Caption = "MIO_ID"

            flxGrd_ItemList.Cols("Stock_Detail_Id").Visible = False
            flxGrd_ItemList.Cols("MIO_ID").Visible = False

            flxGrd_ItemList.Cols("ITEM_CODE").Width = 90
            flxGrd_ItemList.Cols("ITEM_NAME").Width = 330
            flxGrd_ItemList.Cols("UM_Name").Width = 40
            flxGrd_ItemList.Cols("Transfer_rate").Width = 60
            flxGrd_ItemList.Cols("Expiry_date").Width = 70
            flxGrd_ItemList.Cols("Batch_no").Width = 70
            flxGrd_ItemList.Cols("Issued_Qty").Width = 80
            flxGrd_ItemList.Cols("ReturnQty").Width = 80
            flxGrd_ItemList.Cols("Stock_Detail_Id").Width = 70
            flxGrd_ItemList.Cols("MIO_ID").Width = 70

            flxGrd_ItemList.Cols("ITEM_CODE").AllowEditing = False
            flxGrd_ItemList.Cols("ITEM_NAME").AllowEditing = False
            flxGrd_ItemList.Cols("UM_Name").AllowEditing = False
            flxGrd_ItemList.Cols("Transfer_rate").AllowEditing = False
            flxGrd_ItemList.Cols("Expiry_date").AllowEditing = False
            flxGrd_ItemList.Cols("Batch_no").AllowEditing = False
            flxGrd_ItemList.Cols("Issued_Qty").AllowEditing = False
            flxGrd_ItemList.Cols("ReturnQty").AllowEditing = True
            flxGrd_ItemList.Cols("Stock_Detail_Id").AllowEditing = False
            flxGrd_ItemList.Cols("MIO_ID").AllowEditing = False

        End If

    End Sub

    Private Sub flxItemList_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles flxGrd_ItemList.KeyPressEdit
        e.Handled = flxGrd_ItemList.Rows(flxGrd_ItemList.CursorCell.r1).IsNode
    End Sub

    Private Sub flxItemList_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles flxGrd_ItemList.AfterEdit

        If flxGrd_ItemList.Rows(e.Row).IsNode Then Exit Sub

        If flxGrd_ItemList.Rows(e.Row)("Issued_Qty") < flxGrd_ItemList.Rows(e.Row)("ReturnQty") Then
            flxGrd_ItemList.Rows(e.Row)("ReturnQty") = 0
        End If



    End Sub
    Private Sub btnShowList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowList.Click
        FillDetail_Grid()
    End Sub

    Private Sub flx_Material_Issue_List_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flx_Material_Issue_List.Click

    End Sub

    Private Sub flx_Material_Issue_List_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles flx_Material_Issue_List.DoubleClick
        Dim Reverse_Id As Integer
        Dim dtMaster As New DataTable
        Dim dtDetail As New DataTable
        Dim ds As New DataSet


        Reverse_Id = Convert.ToInt32(flx_Material_Issue_List.Rows(flx_Material_Issue_List.CursorCell.r1)("RMIO_ID"))
        Flag = "update"
        ds = obj.fill_Data_set("Get_ReverseMaterial_Issue_FillDetail", "@V_RMIO_ID", Reverse_Id)

        If ds.Tables.Count > 0 Then

            dtMaster = ds.Tables(0)

            cmbIssueSlip.SelectedValue = dtMaster.Rows(0)("Issue_Id")

            txtRemarks.Text = dtMaster.Rows(0)("RMIO_REMARKS")



            dtable_Item_List = ds.Tables(1).Copy
            flxGrd_ItemList.DataSource = dtable_Item_List

            TbReverseIssue.SelectTab(1)

            ds.Dispose()
        End If
    End Sub




End Class
