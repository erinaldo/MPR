Imports MMSPlus.Reverse_wastage_master
Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid

Public Class frm_Reverse_Wastage_Master
    Implements IForm

    Dim clsObj As New cls_Reverse_wastage_master
    Dim prpty As New cls_Reverse_wastage_master_prop
    Dim dtWastageItem As New DataTable
    Dim flag As String
    Dim rights As Form_Rights
    Dim cmd As New SqlCommand
    Dim con As New SqlConnection
    Dim Trans As SqlTransaction
    Dim iReverseWastageId As Int32
    Dim objComm As New CommonClass

    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Enum enmgrdRWastageItem
        rowid = 0
        ItemId = 1
        ItemCode = 2
        ItemName = 3
        ItemUOM = 4
        Wastage_Qty = 5
        Actual_Qty = 6
        Stock_Detail_Id = 7
    End Enum

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            set_new_initilize()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_Wastwge_Master")
        End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        FillGridWastageMaster()
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        If Validation() = False Then
            Exit Sub
        End If
        Try
            If _rights.allow_trans = "N" Then
                RightsMsg()
                Exit Sub
            End If
            Dim msg As String
            iReverseWastageId = Convert.ToInt32(clsObj.getMaxValue("ReverseWastage_ID", "ReverseWASTAGE_MASTER"))
            prpty = New cls_Reverse_wastage_master_prop
            prpty.ReverseWastage_ID = iReverseWastageId
            prpty.Wastage_ID = Convert.ToInt32(cmdBoxWastageCode.SelectedValue)
            prpty.Wastage_Code = clsObj.getPrefixCode("REV_WASTAGE_PREFIX", "DIVISION_SETTINGS")
            prpty.Wastage_No = iReverseWastageId
            prpty.Wastage_Date = Now
            prpty.Wastage_Remarks = txtWatageReamrks.Text()
            prpty.Created_BY = v_the_current_logged_in_user_name
            prpty.Creation_Date = now
            prpty.Modified_BY = v_the_current_logged_in_user_name
            prpty.Modified_Date = now
            prpty.Division_ID = v_the_current_division_id
            prpty.WastageItem = grdRWastageItem.DataSource
            If flag = "save" Then
                msg = clsObj.Insert_Reverse_Wastage_Master(prpty)
                'MsgBox(msg, MsgBoxStyle.Information, gblMessageHeading)
                If MsgBox(msg & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    objComm.RptShow(enmReportName.RptRevWastagePrint, "wastage_id", CStr(iReverseWastageId), CStr(enmDataType.D_int))
                End If
                FillGridWastageMaster()
            Else
                MsgBox("You can't Modify it.", MsgBoxStyle.Information, gblMessageHeading)
            End If
            set_new_initilize()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error saveClick --> frm_Wastage_Master")
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TBCWastageMaster.SelectedIndex = 0 Then
                objComm.RptShow(enmReportName.RptRevWastagePrint, "rwastage_id", CStr(DGVRWastageMaster("ReverseWastage_ID", DGVRWastageMaster.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                If flag <> "save" Then
                    objComm.RptShow(enmReportName.RptRevWastagePrint, "rwastage_id", CStr(DGVRWastageMaster("ReverseWastage_ID", DGVRWastageMaster.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function Validation() As Boolean
        Dim iRow As Int32
        Validation = True
        Dim blnRecExist As Boolean
        Dim dt As DataTable
        blnRecExist = False
        txtWatageReamrks.Focus()
        dt = grdRWastageItem.DataSource
        For iRow = 0 To dt.Rows.Count - 1
            If Convert.ToDouble(dt.Rows(iRow)(enmgrdRWastageItem.Actual_Qty)) > 0 Then
                blnRecExist = True
                Exit For
            End If
        Next iRow
        If blnRecExist = True Then
            Validation = True
            Exit Function
        Else
            Validation = False
            MsgBox("Select aleast one valid item in Wastage to save Reverse Wastage information", vbExclamation, gblMessageHeading)
            Exit Function
        End If
    End Function

    Public Function GetWastageCode() As String
        Dim Pre As String
        Dim WID As String
        Dim WastageCode As String
        Pre = clsObj.getPrefixCode("REV_WASTAGE_PREFIX", "DIVISION_SETTINGS")
        WID = clsObj.getMaxValue("ReverseWastage_ID", "ReverseWASTAGE_MASTER")
        WastageCode = Pre & "" & WID
        Return WastageCode

    End Function

    Private Sub frm_Wastage_Master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            rights = clsObj.Get_Form_Rights(Me.Name)
            flag = "save"
            clsObj.ComboBindWithSP(cmdBoxWastageCode, "GET_WASTAGE_MASTERFORREVERSEWASTAGE", "Wastage_ID", "Wastage_Code", True)
            ' clsObj.FormatGrid(grdRWastageItem)
            FillGridWastageMaster()
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub set_new_initilize()
        lbl_WastageCode.Text = GetWastageCode()
        lbl_WastageDate.Text = now.ToString("dd-MMM-yyy")
        txtWatageReamrks.Text = ""
        dtWastageItem = grdRWastageItem.DataSource
        If Not dtWastageItem Is Nothing Then dtWastageItem.Rows.Clear()

        TBCWastageMaster.SelectTab(1)
        flag = "save"
    End Sub

    Private Sub FillGridWastageMaster()
        Try
            clsObj.Grid_Bind(DGVRWastageMaster, "GET_Reverse_WASTAGE_MASTER")
            DGVRWastageMaster.Columns(0).Visible = False                 'Wastage_ID
            DGVRWastageMaster.Columns(1).HeaderText = "Reverse Wastage No"       'ReverseWastage_No
            DGVRWastageMaster.Columns(1).Width = 170
            DGVRWastageMaster.Columns(2).HeaderText = "Reverse Wastage Code"     'ReverseWastage_Code
            DGVRWastageMaster.Columns(2).Width = 175
            DGVRWastageMaster.Columns(3).HeaderText = "Reverse Wastage Date"     'ReverseWastage_Date
            DGVRWastageMaster.Columns(3).Width = 185
            DGVRWastageMaster.Columns(4).HeaderText = "Reverse Wastage Remarks"  'ReverseWastage_Remarks
            DGVRWastageMaster.Columns(4).Width = 320
            DGVRWastageMaster.Columns(5).Visible = False
            DGVRWastageMaster.Columns(6).Visible = False
            ' clsObj.FormatGrid(DGVRWastageMaster)
            lblErrorMsg.Text = ""
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub DGVWastageItem_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)

    End Sub

    Private Sub grdRWastageItem_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdRWastageItem.AfterEdit
        If grdRWastageItem.Rows(e.Row).IsNode Then Exit Sub
        If Convert.ToDecimal(grdRWastageItem.Rows(e.Row)("Actual_Qty")) > Convert.ToDecimal(grdRWastageItem.Rows(e.Row)("wastage_qty")) Then
            grdRWastageItem.Rows(e.Row)("Actual_Qty") = 0.0
        End If

    End Sub
    Private Sub table_style()

        If Not dtWastageItem Is Nothing Then dtWastageItem.Dispose()
        dtWastageItem = New DataTable()
        dtWastageItem.Columns.Add("Item_Id", GetType(System.Double))
        dtWastageItem.Columns.Add("Item_Code", GetType(System.String))
        dtWastageItem.Columns.Add("Item_Name", GetType(System.String))
        dtWastageItem.Columns.Add("UM_Name", GetType(System.String))
        dtWastageItem.Columns.Add("Batch_No", GetType(System.String))
        dtWastageItem.Columns.Add("Expiry_Date", GetType(System.String))
        dtWastageItem.Columns.Add("Wastage_Qty", GetType(System.Double))
        dtWastageItem.Columns.Add("Actual_Qty", GetType(System.Double))
        dtWastageItem.Columns.Add("Item_rate", GetType(System.Double))
        dtWastageItem.Columns.Add("Stock_Detail_Id", GetType(System.Int32))
        dtWastageItem.Rows.Add()
        grdRWastageItem.DataSource = dtWastageItem
        format_grid()
    End Sub

    Private Sub format_grid()

        grdRWastageItem.Cols(0).Width = 10
        grdRWastageItem.Cols("Item_Id").Visible = True
        grdRWastageItem.Cols("Stock_Detail_Id").Visible = True
        grdRWastageItem.Cols("Item_Code").Caption = "Item Code"
        grdRWastageItem.Cols("Item_Name").Caption = "Item Name"
        grdRWastageItem.Cols("UOM").Caption = "UOM"
        grdRWastageItem.Cols("Batch_No").Caption = "Batch No"
        grdRWastageItem.Cols("Expiry_Date").Caption = "Expiry Date"
        grdRWastageItem.Cols("Wastage_Qty").Caption = "Wastage Qty"
        grdRWastageItem.Cols("Actual_Qty").Caption = "Actual Qty"
        grdRWastageItem.Cols("Item_Rate").Caption = "Item Rate"
        grdRWastageItem.Cols("Stock_Detail_Id").Caption = "Stock Detail Id"


        grdRWastageItem.Cols("Item_Code").AllowEditing = False
        grdRWastageItem.Cols("Item_Name").AllowEditing = False
        grdRWastageItem.Cols("UOM").AllowEditing = False
        grdRWastageItem.Cols("Batch_No").AllowEditing = False
        grdRWastageItem.Cols("Expiry_Date").AllowEditing = False
        grdRWastageItem.Cols("Wastage_Qty").AllowEditing = False
        grdRWastageItem.Cols("Actual_Qty").AllowEditing = True
        grdRWastageItem.Cols("Item_Rate").AllowEditing = False
        grdRWastageItem.Cols("Stock_Detail_Id").AllowEditing = True


        grdRWastageItem.Cols("Item_Id").Width = 60
        grdRWastageItem.Cols("Item_Code").Width = 60
        grdRWastageItem.Cols("Item_Name").Width = 325
        grdRWastageItem.Cols("UOM").Width = 60
        grdRWastageItem.Cols("Batch_No").Width = 90
        grdRWastageItem.Cols("Expiry_Date").Width = 75
        grdRWastageItem.Cols("Stock_Detail_Id").Width = 100
        grdRWastageItem.Cols("Wastage_Qty").Width = 90
        grdRWastageItem.Cols("Actual_Qty").Width = 80
        grdRWastageItem.Cols("Item_Rate").Width = 80

        grdRWastageItem.Cols("Item_Id").Visible = False
        grdRWastageItem.Cols("Stock_Detail_Id").Visible = False
    End Sub

    Private Sub grdRWastageItem_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles grdRWastageItem.AfterDataRefresh
        generate_tree()
    End Sub
    Private Sub generate_tree()
        If grdRWastageItem.Rows.Count > 1 Then
            grdRWastageItem.Tree.Style = TreeStyleFlags.CompleteLeaf
            grdRWastageItem.Tree.Column = 1
            grdRWastageItem.AllowMerging = AllowMergingEnum.None
            Dim totalOn As Integer = grdRWastageItem.Cols("Wastage_Qty").SafeIndex
            grdRWastageItem.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)
            totalOn = grdRWastageItem.Cols("Actual_Qty").SafeIndex
            grdRWastageItem.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)

            Dim cs As C1.Win.C1FlexGrid.CellStyle
            cs = Me.grdRWastageItem.Styles.Add("Actual_Qty")
            cs.ForeColor = Color.White
            cs.BackColor = Color.OrangeRed
            cs.Border.Style = BorderStyleEnum.Raised

            Dim i As Integer
            For i = 1 To grdRWastageItem.Rows.Count - 1
                If Not grdRWastageItem.Rows(i).IsNode Then grdRWastageItem.SetCellStyle(i, enmgrdRWastageItem.Actual_Qty, cs)
            Next
        End If
    End Sub

    Private Sub grdRWastageItem_ChangeEdit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdRWastageItem.ChangeEdit
    End Sub

    Private Sub grdRWastageItem_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles grdRWastageItem.KeyPressEdit
        e.Handled = grdRWastageItem.Rows(grdRWastageItem.CursorCell.r1).IsNode
    End Sub

    Private Sub cmdBoxWastageCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBoxWastageCode.SelectedIndexChanged
        Try
            set_new_initilize()
            Dim ds As DataSet
            Dim iWastageId As Int32
            iWastageId = Convert.ToInt32(cmdBoxWastageCode.SelectedValue)
            Dim strsql As String
            strsql = "SELECT  IM.ITEM_ID," & _
                            " IM.ITEM_CODE," & _
                            " IM.ITEM_NAME," & _
                            " UM.UM_Name AS UOM," & _
                            " SD.Batch_no AS Batch_No," & _
                            " dbo.fn_Format(SD.Expiry_date) AS Expiry_Date," & _
                            " dbo.Get_Average_Rate_as_on_date(IM.ITEM_ID,'" & Now.ToString("dd-MMM-yyyy") & "'," & v_the_current_division_id & ",0) as Item_Rate," & _
                            " WD.Balance_Qty AS Wastage_Qty," & _
                            " 0.00 AS Actual_Qty," & _
                            " WD.Stock_Detail_Id " & _
                    " FROM    WASTAGE_MASTER AS WM" & _
                            " INNER JOIN WASTAGE_DETAIL AS WD ON WD.Wastage_ID = WM.Wastage_ID  AND WD.Division_ID = WM.Division_ID" & _
                            " INNER JOIN ITEM_MASTER AS IM ON IM.ITEM_ID = WD.Item_ID" & _
                            " INNER JOIN UNIT_MASTER AS UM ON IM.UM_ID = UM.UM_ID" & _
                            " INNER JOIN ITEM_CATEGORY AS CM ON IM.ITEM_CATEGORY_ID = CM.ITEM_CAT_ID" & _
                            " INNER JOIN ITEM_DETAIL AS ID ON WD.Item_ID = ID.ITEM_ID" & _
                            " INNER JOIN STOCK_DETAIL SD ON SD.STOCK_DETAIL_ID = WD.Stock_Detail_Id" & _
                    " WHERE   WM.Wastage_ID =  " & iWastageId

            'ds = clsObj.fill_Data_set("GET_WASTAGE_DETAILFORREVERSEWASTAGE", "@v_Wastage_ID", iWastageId)
            ds = clsObj.Fill_DataSet(strsql)

            If ds.Tables.Count > 0 Then
                'Bind the Wastage Detail InformationGET_WASTAGE_DETAILFORREVERSEWASTAGE
                dtWastageItem = ds.Tables(0)
                If dtWastageItem.Rows.Count > 0 Then
                    grdRWastageItem.DataSource = dtWastageItem
                    format_grid()
                End If
            End If
            TBCWastageMaster.SelectTab(1)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub DGVRWastageMaster_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVRWastageMaster.DoubleClick
        Try
            Dim ds As DataSet
            Dim dtWastage As New DataTable
            'Dim dtWastageDetail As New DataTable
            Dim iWastageId As Int32
            iWastageId = Convert.ToInt32(DGVRWastageMaster.SelectedRows.Item(0).Cells(0).Value)
            flag = "view"
            ds = clsObj.fill_Data_set("GET_REVERSE_WASTAGE_MASTERANDRREVERSE_WASTAGE_DETAIL", "@v_ReverseWastage_ID", iWastageId)
            'Dim dt As DataTableGET_REVERSE_WASTAGE_MASTERANDRREVERSE_WASTAGE_DETAIL
            If ds.Tables.Count > 0 Then
                'Bind the Wastage Information
                dtWastage = ds.Tables(0)
                iWastageId = Convert.ToString(dtWastage.Rows(0)("ReverseWastage_ID"))
                lbl_WastageCode.Text = Convert.ToString(dtWastage.Rows(0)("ReverseWastage_Code"))
                lbl_WastageDate.Text = Convert.ToString(dtWastage.Rows(0)("ReverseWastage_Date"))
                txtWatageReamrks.Text = Convert.ToString(dtWastage.Rows(0)("ReverseWastage_Remarks"))
                'Bind the Wastage Item Information               
                dtWastageItem = ds.Tables(1)
                If dtWastageItem.Rows.Count > 0 Then
                    grdRWastageItem.DataSource = dtWastageItem
                    format_grid()
                End If
            End If
            TBCWastageMaster.SelectTab(1)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim qry As String = ""

        qry = "SELECT ReverseWastage_ID," & _
                " ReverseWastage_No, " & _
      " (ReverseWastage_Code + Cast(ReverseWastage_No as Varchar)) as ReverseWastage_Code, " & _
      " dbo.fn_Format(ReverseWastage_Date) AS Wastage_Date  , " & _
        " ReverseWastage_Remarks," & _
     " Division_ID ," & _
        " WastageID" & _
        " FROM ReverseWASTAGE_MASTER " & _
" WHERE (cast(ReverseWastage_No as varchar) " & _
  " + (ReverseWastage_Code + Cast(ReverseWastage_No as Varchar))" & _
  " + cast(dbo.fn_Format(ReverseWastage_Date) as varchar)" & _
  " + ReverseWastage_Remarks)" & _
  " like '% " & txtSearch.Text & "%'"

        objComm.GridBind(DGVRWastageMaster, qry)
    End Sub
End Class
