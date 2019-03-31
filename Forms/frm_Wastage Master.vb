Imports MMSPlus.wastage_master
Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid

Public Class frm_Wastage_Master
    Implements IForm
    Dim clsObj As New cls_wastage_master
    Dim prpty As New cls_wastage_master_prop
    Dim dtWastageItem As New DataTable
    Dim flag As String
    Dim rights As Form_Rights
    Dim cmd As New SqlCommand
    Dim con As New SqlConnection
    Dim Trans As SqlTransaction
    Dim iWastageId As Int32
    Dim objComm As New CommonClass


    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Enum enmgrdWastageItem
        rowid = 0
        ItemId = 1
        ItemCode = 2
        ItemName = 3
        ItemUOM = 4
        Batch_N0 = 5
        Expiry_Date = 6
        Batch_Qty = 7
        Stock_Detail_Id = 8
        Wastage_Qty = 10
    End Enum
    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub
    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        Try
            flag = "save"
            new_initilization()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error newClick --> frm_Wastwge_Master")
        End Try
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick
        FillGridWastageMaster()
    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        If txtWatageReamrks.Text = "" Then
            MsgBox("Remarks not entered", MsgBoxStyle.OkOnly)
            Exit Sub
        End If
        If Validation() = False Then
            Exit Sub
        End If
        Try
            If _rights.allow_trans = "N" Then
                RightsMsg()
                Exit Sub
            End If
            Dim msg As String
            iWastageId = Convert.ToInt32(clsObj.getMaxValue("Wastage_ID", "WASTAGE_MASTER"))
            prpty = New cls_wastage_master_prop
            prpty.Wastage_ID = iWastageId
            prpty.Wastage_Code = clsObj.getPrefixCode("WASTAGE_PREFIX", "DIVISION_SETTINGS")
            prpty.Wastage_No = iWastageId
            prpty.Wastage_Date = Now
            prpty.Wastage_Remarks = txtWatageReamrks.Text()
            prpty.Created_BY = v_the_current_logged_in_user_name
            prpty.Creation_Date = Now
            prpty.Modified_BY = ""
            prpty.Modified_Date = NULL_DATE
            prpty.Division_ID = v_the_current_division_id
            prpty.WastageItem = grdWastageItem.DataSource
            If flag = "save" Then
                msg = clsObj.Insert_Wastage_MasterTran(prpty)
                FillGridWastageMaster()
                'MsgBox(msg, MsgBoxStyle.Information, gblMessageHeading)
                If MsgBox("Record saved successfully with code ( " + Convert.ToString(prpty.Wastage_Code) + Convert.ToString(prpty.Wastage_No) + "  )" & vbCrLf & "Do You Want to Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    objComm.RptShow(enmReportName.RptWastagePrint, "wastage_id", CStr(iWastageId), CStr(enmDataType.D_int))
                End If
            Else
                MsgBox("You can't Modify it.", MsgBoxStyle.Information, gblMessageHeading)
            End If
            new_initilization()
            TBCWastageMaster.SelectTab(0)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error saveClick --> frm_Wastage_Master")
        End Try

    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick
        Try
            If TBCWastageMaster.SelectedIndex = 0 Then
                objComm.RptShow(enmReportName.RptWastagePrint, "wastage_id", CStr(DGVWastageMaster("wastage_id", DGVWastageMaster.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))
            Else
                If flag <> "save" Then
                    '    obj.RptShow(enmReportName.RptOpenPurchaseOrderPrint, "PO_ID", CStr(_po_id), CStr(enmDataType.D_int))
                    objComm.RptShow(enmReportName.RptWastagePrint, "wastage_id", CStr(DGVWastageMaster("wastage_id", DGVWastageMaster.CurrentCell.RowIndex).Value()), CStr(enmDataType.D_int))

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
        blnRecExist = False
        txtWatageReamrks.Focus()
        
        For iRow = 1 To grdWastageItem.Rows.Count - 2
            If Convert.ToString(grdWastageItem.Item(iRow, enmgrdWastageItem.Wastage_Qty)) > "0.0" Then
                blnRecExist = True
                Exit For
            End If
        Next iRow
        If blnRecExist = True Then
            Validation = True
            Exit Function
        Else
            Validation = False
            MsgBox("Select aleast one valid item in Wastage to save Wastage information", vbExclamation, gblMessageHeading)
            Exit Function
        End If
    End Function
    Public Function GetWastageCode() As String

        Dim Pre As String
        Dim WID As String
        Dim WastageCode As String
        Pre = clsObj.getPrefixCode("WASTAGE_PREFIX", "DIVISION_SETTINGS")
        WID = clsObj.getMaxValue("Wastage_ID", "WASTAGE_MASTER")
        WastageCode = Pre & "" & WID
        Return WastageCode

    End Function
    Private Sub frm_Wastage_Master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            rights = clsObj.Get_Form_Rights(Me.Name)
            flag = "save"
            new_initilization()
            FillGridWastageMaster()
            table_style()
            ' clsObj.FormatGrid(grdWastageItem)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub new_initilization()
        lbl_WastageCode.Text = GetWastageCode()
        lbl_WastageDate.Text = now.ToString("dd-MMM-yyy")
        txtWatageReamrks.Text = ""
        dtWastageItem = grdWastageItem.DataSource
        If Not dtWastageItem Is Nothing Then dtWastageItem.Rows.Clear()
        TBCWastageMaster.SelectTab(1)
        flag = "save"
    End Sub
    Private Sub FillGridWastageMaster()
        Try
            clsObj.Grid_Bind(DGVWastageMaster, "GET_WASTAGE_MASTER")
            DGVWastageMaster.Columns(0).Visible = False                 'Wastage_ID
            DGVWastageMaster.Columns(1).HeaderText = "Wastage No"       'Wastage_No
            DGVWastageMaster.Columns(1).Width = 200
            DGVWastageMaster.Columns(2).HeaderText = "Wastage Code"     'Wastage_Code
            DGVWastageMaster.Columns(2).Visible = False
            DGVWastageMaster.Columns(3).HeaderText = "Wastage Date"     'Wastage_Date
            DGVWastageMaster.Columns(3).Width = 230
            DGVWastageMaster.Columns(4).HeaderText = "Wastage Remarks"  'Wastage_Remarks
            DGVWastageMaster.Columns(4).Width = 450
            DGVWastageMaster.Columns(5).Visible = False
            ' clsObj.FormatGrid(DGVWastageMaster)
            lblErrorMsg.Text = ""
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub
    Private Sub DGVWastageMaster_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DGVWastageMaster.DoubleClick
        Try
            If _rights.allow_edit = "N" Then
                RightsMsg()
                Exit Sub
            End If

            Dim ds As DataSet
            Dim dtWastage As New DataTable
            'Dim dtWastageDetail As New DataTable
            Dim iWastageId As Int32
            iWastageId = Convert.ToInt32(DGVWastageMaster.SelectedRows.Item(0).Cells(0).Value)
            flag = "view"
            'grdWastageItem.Rows.RemoveRange(1, grdWastageItem.Rows.Count - 1)
            ds = clsObj.fill_Data_set("GET_WASTAGE_MASTERANDWASTAGE_DETAIL", "@v_Wastage_ID", iWastageId)
            Dim dt As DataTable
            If ds.Tables.Count > 0 Then
                'Bind the Wastage Information
                dtWastage = ds.Tables(0)
                iWastageId = Convert.ToString(dtWastage.Rows(0)("Wastage_ID"))
                lbl_WastageCode.Text = Convert.ToString(dtWastage.Rows(0)("Wastage_No"))
                lbl_WastageDate.Text = Convert.ToString(dtWastage.Rows(0)("Wastage_Date"))
                txtWatageReamrks.Text = Convert.ToString(dtWastage.Rows(0)("Wastage_Remarks"))
                'Bind the Wastage Item Information
                'dt = grdWastageItem.DataSource
                'dt.Rows.Clear()
                dt = ds.Tables(1)
                If dt.Rows.Count > 0 Then
                    grdWastageItem.DataSource = dt
                End If
            End If
            TBCWastageMaster.SelectTab(1)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub DGVWastageItem_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)

    End Sub
    Private Sub grdWastageItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdWastageItem.KeyDown
        Try
            Dim iRowindex As Int32
            If e.KeyCode = Keys.Delete Then
                RemoveHandler grdWastageItem.AfterDataRefresh, AddressOf grdWastageItem_AfterDataRefresh
                Dim result As Integer
                Dim item_code As String
                result = MsgBox("Do you want to remove """ & grdWastageItem.Rows(grdWastageItem.CursorCell.r1).Item(3) & """ from the list?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
                item_code = grdWastageItem.Rows(grdWastageItem.CursorCell.r1).Item("item_code")
                If result = MsgBoxResult.Yes Then
restart:
                    Dim dt As DataTable
                    dt = TryCast(grdWastageItem.DataSource, DataTable)
                    If Not dt Is Nothing Then
                        For Each dr As DataRow In dt.Rows
                            If dr("item_code") = item_code Then
                                dr.Delete()
                                GoTo restart
                            End If
                        Next
                        '        dt.AcceptChanges()
                    End If
                End If
                AddHandler grdWastageItem.AfterDataRefresh, AddressOf grdWastageItem_AfterDataRefresh
                generate_tree()

            ElseIf flag = "save" Then
                If e.KeyCode = Keys.Space Then
                    iRowindex = grdWastageItem.Row

                    'frm_Show_search.qry = "Select * from Item_master inner join item_detail on item_master.item_id = item_detail.item_id " 'where item_detail.div_id = '" + Convert.ToString(v_the_current_logged_in_user_id) + "'"
                    'frm_Show_search.extra_condition = ""
                    'frm_Show_search.ret_column = "Item_ID"
                    'frm_Show_search.column_name = "Item_name"
                    'frm_Show_search.item_rate_column = ""
                    'frm_Show_search.ShowDialog()

                    frm_Show_search.qry = " SELECT  top 50 im.ITEM_ID ,
		                                ISNULL(im.BarCode_vch, '') AS BARCODE ,
                                        im.ITEM_NAME AS [ITEM NAME] ,
                                        im.MRP_Num AS MRP ,
                                        CAST(im.sale_rate AS NUMERIC(18, 2)) AS RATE ,
                                        ISNULL(litems.LabelItemName_vch, '') AS BRAND ,
                                        ic.ITEM_CAT_NAME AS CATEGORY
                                        FROM      Item_master im
                                        LEFT OUTER JOIN item_detail id ON im.item_id = id.item_id
                                        LEFT OUTER JOIN dbo.ITEM_CATEGORY ic ON im.ITEM_CATEGORY_ID = ic.ITEM_CAT_ID
        LEFT JOIN dbo.LabelItem_Mapping AS LIM ON LIM.Fk_ItemId_Num = IM.ITEM_ID        
                                                  AND LIM.Fk_LabelDetailId IN (
                                                  SELECT    Pk_LabelDetailId_Num
                                                  FROM      dbo.Label_Items
                                                  WHERE     fk_LabelId_num = 1 )
        LEFT JOIN dbo.Label_Items AS litems ON litems.Pk_LabelDetailId_Num = LIM.Fk_LabelDetailId
                                               AND litems.fk_LabelId_num = 1
        LEFT JOIN dbo.Label_Master AS BrandMaster ON BrandMaster.Pk_LabelId_Num = litems.fk_LabelId_num
                                                     AND BrandMaster.Pk_LabelId_Num = 1
                                        WHERE   id.Is_active = 1 "


                    frm_Show_search.column_name = "BARCODE_VCH"
                    frm_Show_search.column_name1 = "ITEM_NAME"
                    frm_Show_search.column_name2 = "MRP_Num"
                    frm_Show_search.column_name3 = "SALE_RATE"
                    frm_Show_search.column_name4 = "LABELITEMNAME_VCH"
                    frm_Show_search.column_name5 = "ITEM_CAT_NAME"
                    frm_Show_search.cols_no_for_width = "1,2,3,4,5,6"
                    frm_Show_search.cols_width = "100,350,60,60,100,100"
                    frm_Show_search.extra_condition = ""
                    frm_Show_search.ret_column = "ITEM_ID"
                    frm_Show_search.item_rate_column = ""
                    frm_Show_search.ShowDialog()

                    If Not check_item_exist(frm_Show_search.search_result) Then
                        get_row(frm_Show_search.search_result, 0)
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Function check_item_exist(ByVal item_id As Integer) As Boolean
        Dim iRow As Int32
        check_item_exist = False
        For iRow = 1 To grdWastageItem.Rows.Count - 1
            If grdWastageItem.Item(iRow, "item_id").ToString() <> "" Then
                If Convert.ToInt32(grdWastageItem.Item(iRow, "item_id")) = item_id Then
                    MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
                    check_item_exist = True
                    Exit For
                End If
            Else
                check_item_exist = False

            End If
        Next iRow
    End Function

    Public Sub get_row(ByVal item_id As Integer, ByVal Wastage_id As Integer)
        Try
            Dim ds As DataSet
            Dim sqlqry As String
            sqlqry = "SELECT  " & _
                                        " IM.ITEM_ID , " & _
                                        " IM.ITEM_CODE , " & _
                                        " IM.ITEM_NAME , " & _
                                        " UM.UM_Name , " & _
                                        " SD.Batch_no , " & _
                                        " dbo.fn_Format(SD.Expiry_date) AS Expiry_Date, " & _
                                        " dbo.Get_Average_Rate_as_on_date(IM.ITEM_ID,'" & Now.ToString("dd-MMM-yyyy") & "'," & v_the_current_division_id & ",0) as Item_Rate," & _
                                        " SD.Balance_Qty, " & _
                                        " 0.00  as Wastage_Qty, " & _
                                        " SD.STOCK_DETAIL_ID  " & _
                                " FROM " & _
                                        " ITEM_MASTER  IM " & _
                                        " INNER JOIN ITEM_DETAIL ID ON IM.ITEM_ID = ID.ITEM_ID " & _
                                        " INNER JOIN STOCK_DETAIL SD ON ID.ITEM_ID = SD.Item_id " & _
                                        " INNER JOIN UNIT_MASTER UM ON IM.UM_ID = UM.UM_ID" & _
                                " where " & _
                                        " IM.ITEM_ID = " & item_id & " and sd.balance_qty > 0 "
            ds = clsObj.Fill_DataSet(sqlqry)
            Dim dt As DataTable
            Dim i As Integer
            Dim dr As DataRow
            dt = grdWastageItem.DataSource
            objComm.RemoveBlankRow(dt, "item_id")
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow
                dr("Item_Id") = ds.Tables(0).Rows(i)("ITEM_ID")
                dr("Item_Code") = ds.Tables(0).Rows(i)("ITEM_CODE")
                dr("Item_Name") = ds.Tables(0).Rows(i)("ITEM_NAME")
                dr("UM_Name") = ds.Tables(0).Rows(i)("UM_Name")
                dr("Batch_No") = ds.Tables(0).Rows(i)("Batch_no")
                dr("Expiry_Date") = ds.Tables(0).Rows(i)("Expiry_Date")
                dr("Item_Rate") = ds.Tables(0).Rows(i)("Item_Rate")
                dr("Batch_Qty") = ds.Tables(0).Rows(i)("Balance_Qty")
                dr("Stock_Detail_Id") = ds.Tables(0).Rows(i)("STOCK_DETAIL_ID")
                dr("Wastage_Qty") = ds.Tables(0).Rows(i)("Wastage_Qty")
                dt.Rows.Add(dr)
            Next
            Dim strSort As String = grdWastageItem.Cols(1).Name + ", " + grdWastageItem.Cols(2).Name + ", " + grdWastageItem.Cols(3).Name
            dt = grdWastageItem.DataSource
            If Not dt Is Nothing Then
                dt.DefaultView.Sort = strSort
                dt = dt.DefaultView.ToTable
                dt.DefaultView.Sort = ""
            End If
            dt.Rows.Add(dt.NewRow)
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Private Sub grdWastageItem_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles grdWastageItem.AfterEdit
        If grdWastageItem.Rows(e.Row).IsNode Then Exit Sub
        If IsDBNull(grdWastageItem.Rows(e.Row)("wastage_qty")) Or IsDBNull(grdWastageItem.Rows(e.Row)("Batch_Qty")) Then Exit Sub

        If Convert.ToDecimal(grdWastageItem.Rows(e.Row)("wastage_qty")) > Convert.ToDecimal(grdWastageItem.Rows(e.Row)("Batch_Qty")) Then
            grdWastageItem.Rows(e.Row)("wastage_qty") = 0.0
        End If
    End Sub

    Private Sub table_style()

        If Not dtWastageItem Is Nothing Then dtWastageItem.Dispose()
        dtWastageItem = New DataTable()
        dtWastageItem.Columns.Add("Item_Id", GetType(System.Double))
        dtWastageItem.Columns.Add("Item_Code", GetType(System.String))
        dtWastageItem.Columns.Add("Item_Name", GetType(System.String))
        dtWastageItem.Columns.Add("UM_Name", GetType(System.String))
        dtWastageItem.Columns.Add("Batch_no", GetType(System.String))
        dtWastageItem.Columns.Add("Expiry_date", GetType(System.String))
        dtWastageItem.Columns.Add("Batch_Qty", GetType(System.Double))
        dtWastageItem.Columns.Add("Stock_Detail_Id", GetType(System.Int32))
        dtWastageItem.Columns.Add("Item_Rate", GetType(System.Double))
        dtWastageItem.Columns.Add("Wastage_Qty", GetType(System.Double))
        dtWastageItem.Rows.Add()

        grdWastageItem.DataSource = dtWastageItem
        format_grid()


    End Sub

    Private Sub format_grid()

        grdWastageItem.Cols(0).Width = 10
        grdWastageItem.Cols("Item_Id").Visible = True
        grdWastageItem.Cols("Stock_Detail_Id").Visible = True
        grdWastageItem.Cols("Item_Code").Caption = "Item Code"
        grdWastageItem.Cols("Item_Name").Caption = "Item Name"
        grdWastageItem.Cols("UM_Name").Caption = "UOM"
        grdWastageItem.Cols("Batch_no").Caption = "Batch No"
        grdWastageItem.Cols("Expiry_date").Caption = "Expiry Date"
        grdWastageItem.Cols("batch_qty").Caption = "Batch Qty"
        grdWastageItem.Cols("Wastage_Qty").Caption = "Wastage Qty"
        grdWastageItem.Cols("Item_Rate").Caption = "Item Rate"

        grdWastageItem.Cols("Item_Code").AllowEditing = False
        grdWastageItem.Cols("Item_Name").AllowEditing = False
        grdWastageItem.Cols("UM_Name").AllowEditing = False
        grdWastageItem.Cols("Batch_no").AllowEditing = False
        grdWastageItem.Cols("Expiry_date").AllowEditing = False
        grdWastageItem.Cols("batch_qty").AllowEditing = False
        grdWastageItem.Cols("Stock_Detail_Id").AllowEditing = False
        grdWastageItem.Cols("Wastage_Qty").AllowEditing = True
        grdWastageItem.Cols("Item_Rate").AllowEditing = False

        grdWastageItem.Cols("Item_Id").Width = 40
        grdWastageItem.Cols("Item_Code").Width = 70
        grdWastageItem.Cols("Item_Name").Width = 300
        grdWastageItem.Cols("UM_Name").Width = 40
        grdWastageItem.Cols("Batch_No").Width = 70
        grdWastageItem.Cols("Expiry_date").Width = 80
        grdWastageItem.Cols("Batch_Qty").Width = 60
        grdWastageItem.Cols("Stock_Detail_Id").Width = 60
        grdWastageItem.Cols("Wastage_Qty").Width = 80
        grdWastageItem.Cols("Item_Rate").Width = 80

        grdWastageItem.Cols("Stock_Detail_Id").Visible = False
    End Sub

    Private Sub grdWastageItem_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles grdWastageItem.AfterDataRefresh
        generate_tree()
    End Sub

    Private Sub generate_tree()
        If grdWastageItem.Rows.Count > 1 Then
            grdWastageItem.Tree.Style = TreeStyleFlags.CompleteLeaf
            grdWastageItem.Tree.Column = 1
            grdWastageItem.AllowMerging = AllowMergingEnum.None
            Dim totalOn As Integer = grdWastageItem.Cols("Batch_Qty").SafeIndex
            grdWastageItem.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)
            totalOn = grdWastageItem.Cols("Wastage_Qty").SafeIndex
            grdWastageItem.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)

            Dim cs As C1.Win.C1FlexGrid.CellStyle
            cs = Me.grdWastageItem.Styles.Add("Wastage_Qty")
            cs.ForeColor = Color.White
            cs.BackColor = Color.OrangeRed
            cs.Border.Style = BorderStyleEnum.Raised

            Dim i As Integer
            For i = 1 To grdWastageItem.Rows.Count - 1
                If Not grdWastageItem.Rows(i).IsNode Then grdWastageItem.SetCellStyle(i, enmgrdWastageItem.Wastage_Qty, cs)
            Next
        End If
    End Sub

    Private Sub grdWastageItem_ChangeEdit(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdWastageItem.ChangeEdit
    End Sub

    Private Sub grdWastageItem_KeyPressEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles grdWastageItem.KeyPressEdit
        e.Handled = grdWastageItem.Rows(grdWastageItem.CursorCell.r1).IsNode
    End Sub

    Private Sub txtSearch_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            clsObj.GridBind(DGVWastageMaster, "SELECT  Wastage_ID," & _
                    "Wastage_Code + CAST(Wastage_No AS VARCHAR) AS Wastage_No ," & _
                    "Wastage_Code," & _
                    "dbo.fn_Format(Wastage_Date) AS Wastage_Date," & _
                    "Wastage_Remarks," & _
            "Division_ID" & _
            " FROM    WASTAGE_MASTER where (cast(Wastage_No as varchar) + Wastage_Code + cast(Wastage_Date as varchar) + Wastage_Remarks) like '%" & txtSearch.Text & "%'")

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> FillGrid")
        End Try
    End Sub

End Class
