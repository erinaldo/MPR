Imports MMSPlus.stock_transfer_outlet
Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid


Public Class frm_Stock_Transfer

    Implements IForm
    Dim obj As New CommonClass
    Dim clsObj As New cls_Stock_Transfer_master
    Dim prpty As cls_Stock_Transfer_prop
    Dim flag As String
    Dim Transfer_ID As Int16

    Dim dtable_Item_List As DataTable

    Dim _rights As Form_Rights
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub


    Private Sub fill_grid(Optional ByVal condition As String = "")
        Try

            Dim strsql As String
            '
          
            strsql = "  select * from (SELECT    STOCK_TRANSFER_MASTER.TRANSFER_ID," & _
                        " STOCK_TRANSFER_MASTER.DC_CODE " & _
                        " + CAST(STOCK_TRANSFER_MASTER.DC_NO AS VARCHAR) AS [DC No]," & _
                        " dbo.fn_Format(STOCK_TRANSFER_MASTER.TRANSFER_DATE) AS [DC DATE]," & _
                        " DivisionMaster.DivisionName_vch AS [Transfer to]," & _
                        " ISNULL(STOCK_TRANSFER_MASTER.MRN_PREFIX" & _
                        " + CAST(CASE WHEN STOCK_TRANSFER_MASTER.MRN_NO <>-1 THEN STOCK_TRANSFER_MASTER.MRN_NO END  AS VARCHAR),'') [MRN No]," & _
                        " ISNULL(CASE WHEN STOCK_TRANSFER_MASTER.MRN_NO <>-1 THEN dbo.fn_Format(STOCK_TRANSFER_MASTER.RECEIVED_DATE) end,'') AS [MRN Date]," & _
                        " STOCK_TRANSFER_MASTER.TRANSFER_REMARKS AS Remarks " & _
                    " FROM STOCK_TRANSFER_MASTER" & _
                        " INNER JOIN DivisionMaster ON STOCK_TRANSFER_MASTER.TRANSFER_OUTLET_ID = DivisionMaster.Pk_DivisionId_num" & _
                        " where STOCK_TRANSFER_MASTER.division_id=" & v_the_current_division_id & ") tb  where ([DC No] + [DC DATE] + [Transfer to] + [MRN No] + [MRN Date] + Remarks) like '%" & condition & "%'"

            Dim dt As DataTable = obj.Fill_DataSet(strsql).Tables(0)

            flxList.DataSource = dt

            flxList.Cols(1).Visible = False
            flxList.Cols(2).Width = 100
            flxList.Cols(3).Width = 100
            flxList.Cols(4).Width = 170
            flxList.Cols(5).Width = 100
            flxList.Cols(6).Width = 100
            flxList.Cols(7).Width = 200

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub

    Private Sub frm_Stock_Transfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            obj.FormatGrid(flxItems)
            obj.FormatGrid(flxList)
            table_style()
            obj.ComboBind_Remote(cmbOutlet, "Select 'W'+convert(varchar(10),Pk_DivisionId_num)as Pk_DivisionId_num,DivisionName_vch from DivisionMaster where Pk_DivisionId_num<>" & v_the_current_division_id & " UNION ALL select 'O'+convert(varchar(10),Pk_OutletId_num) AS Pk_DivisionId_num,OutletName_vch AS DivisionName_vch from OutletMaster", "DivisionName_vch", "Pk_DivisionId_num", True)

            new_initilization()
            fill_grid()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, gblMessageHeading_Error)
        End Try
    End Sub

    Public Sub CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.CloseClick

    End Sub

    Public Sub DeleteClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.DeleteClick

    End Sub

    Public Sub NewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.NewClick
        new_initilization()
    End Sub

    Public Sub RefreshClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.RefreshClick

    End Sub

    Public Sub SaveClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.SaveClick
        Dim cmd As SqlCommand
        Try

            If Validation() = False Then
                Exit Sub
            End If

            If flag = "save" Then
                If _rights.allow_trans = "N" Then
                    RightsMsg()
                    Exit Sub
                End If


                prpty = New cls_Stock_Transfer_prop
                
                Dim ds1 As DataSet = obj.FillDataSet_Remote("Select isnull(max(Transfer_ID),0) + 1 from STOCK_TRANSFER_MASTER") '( getMaxValue("Transfer_ID", "STOCK_TRANSFER_MASTER"))
                Transfer_ID = Convert.ToInt32(ds1.Tables(0).Rows(0)(0))
                prpty.TRANSFER_ID = Transfer_ID


                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''''''''''''''''''''''''''TO GET DC NO'''''''''''''''''''''''''''''
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Dim ds As New DataSet()
                ds = obj.fill_Data_set("GET_DC_NO", "@DIV_ID", v_the_current_division_id)
                If ds.Tables(0).Rows.Count = 0 Then
                    MsgBox("DC series does Not exists", MsgBoxStyle.Information, gblMessageHeading)
                    ds.Dispose()
                    Exit Sub
                Else
                    If ds.Tables(0).Rows(0)(0).ToString() = "-1" Then
                        MsgBox("DC series does Not exists", MsgBoxStyle.Information, gblMessageHeading)
                        ds.Dispose()
                        Exit Sub
                    ElseIf ds.Tables(0).Rows(0)(0).ToString() = "-2" Then
                        MsgBox("DC series has been completed", MsgBoxStyle.Information, gblMessageHeading)
                        ds.Dispose()
                        Exit Sub
                    Else
                        prpty.DC_CODE = ds.Tables(0).Rows(0)(0).ToString()
                        prpty.DC_NO = Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString()) + 1
                        ds.Dispose()
                    End If
                End If

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''''''''''''''''''''''''''TO GET DC NO'''''''''''''''''''''''''''''
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                prpty.TRANSFER_DATE = Now
                prpty.TRANSFER_STATUS = Convert.ToInt32(GlobalModule.TRANSFER_STATUS.Fresh)
                prpty.RECEIVED_DATE = NULL_DATE
                prpty.FREEZED_DATE = NULL_DATE
                prpty.TRANSFER_REMARKS = txtDCRemarks.Text()
                prpty.CREATED_BY = v_the_current_logged_in_user_name
                prpty.CREATION_DATE = Now
                prpty.MODIFIED_BY = ""
                prpty.MODIFIED_DATE = NULL_DATE
                prpty.DIVISION_ID = v_the_current_division_id
                prpty.TRANSFER_OUTLET_ID = Convert.ToInt32(cmbOutlet.SelectedValue)

                prpty.dtable_Item_List = dtable_Item_List
                clsObj.Insert_STOCK_TRANSFER(prpty)


            End If


            If flag = "save" Then
                If MsgBox("Transfer information has been Saved." & vbCrLf & "Do You Want To Print Preview.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, gblMessageHeading) = MsgBoxResult.Yes Then
                    obj.RptShow(enmReportName.RptStockTransferCCPrint, "Transfer_ID", CStr(prpty.TRANSFER_ID), CStr(enmDataType.D_int))
                End If
            Else
                MsgBox("You Can't edit this.")
            End If

            new_initilization()
        Catch ex As Exception
            obj.MyCon_RollBackTransaction(cmd)

            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

    Private Sub new_initilization()


        lbl_TransferDate.Text = Now.ToString("dd-MMM-yyyy")
        lbl_Status.Text = GlobalModule.TRANSFER_STATUS.Fresh.ToString()

        txtDCRemarks.Text = ""

        dtable_Item_List.Rows.Clear()
        dtable_Item_List.Rows.Add()

        TabControl1.SelectTab(1)


        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''TO GET DC NO'''''''''''''''''''''''''''''
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim ds As New DataSet()
        ds = obj.fill_Data_set("GET_DC_NO", "@DIV_ID", v_the_current_division_id)
        If ds.Tables(0).Rows.Count = 0 Then
            lbl_DCNo.Text = "DC series does not exists"
            ds.Dispose()
            Exit Sub
        Else
            If ds.Tables(0).Rows(0)(0).ToString() = "-1" Then
                lbl_DCNo.Text = "DC series does not exists"
                ds.Dispose()
                Exit Sub
            ElseIf ds.Tables(0).Rows(0)(0).ToString() = "-2" Then
                lbl_DCNo.Text = "DC series has been completed"
                ds.Dispose()
                Exit Sub
            Else
                lbl_DCNo.Text = ds.Tables(0).Rows(0)(0).ToString() & (Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString()) + 1).ToString
                ds.Dispose()
            End If
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''TO GET DC NO'''''''''''''''''''''''''''''
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        flag = "save"
    End Sub
    Private Function Validation() As Boolean
        
        If cmbOutlet.SelectedIndex = 0 Then
            MsgBox("Select Outlet/Warehouse to Transfer.", MsgBoxStyle.Information, gblMessageHeading)
            Return False
        End If

        Dim dt As DataTable
        dt = dtable_Item_List.Copy
        For j As Integer = 0 To dt.Rows.Count
            Dim i As Integer
again:
            For i = 0 To dt.Rows.Count - 1
                If IsNumeric(dt.Rows(i)("transfer_qty")) Then
                    If Convert.ToDouble(dt.Rows(i)("transfer_qty")) <= 0 Then
                        dt.Rows.RemoveAt(i)
                        GoTo again
                    End If
                End If
            Next
        Next

        If dt.Rows.Count <= 0 Then
            MsgBox("Please transfer atleast one item.", MsgBoxStyle.Information)
            Return False
        End If

        Return True

    End Function
  
    
    Private Sub table_style()
        Try
            If Not dtable_Item_List Is Nothing Then dtable_Item_List.Dispose()
            dtable_Item_List = New DataTable()
            dtable_Item_List.Columns.Add("Item_Id", GetType(System.Double))
            dtable_Item_List.Columns.Add("Item_Code", GetType(System.String))
            dtable_Item_List.Columns.Add("Item_Name", GetType(System.String))
            dtable_Item_List.Columns.Add("UM_Name", GetType(System.String))
            dtable_Item_List.Columns.Add("Batch_no", GetType(System.String))
            dtable_Item_List.Columns.Add("Expiry_date", GetType(System.String))
            dtable_Item_List.Columns.Add("Batch_Qty", GetType(System.Double))
            dtable_Item_List.Columns.Add("Stock_Detail_Id", GetType(System.Int32))
            dtable_Item_List.Columns.Add("Item_Rate", GetType(System.Double))
            dtable_Item_List.Columns.Add("transfer_Qty", GetType(System.Double))
            dtable_Item_List.Rows.Add()

            flxItems.DataSource = dtable_Item_List

            format_grid()

         
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub
    

    Private Sub format_grid()

        flxItems.Cols(0).Width = 10
        flxItems.Cols("Item_Id").Visible = False
        flxItems.Cols("Stock_Detail_Id").Visible = True
        flxItems.Cols("Item_Code").Caption = "Item Code"
        flxItems.Cols("Item_Name").Caption = "Item Name"
        flxItems.Cols("UM_Name").Caption = "UOM"
        flxItems.Cols("Batch_no").Caption = "Batch No"
        flxItems.Cols("Expiry_date").Caption = "Expiry Date"
        flxItems.Cols("batch_qty").Caption = "Batch Qty"
        flxItems.Cols("transfer_Qty").Caption = "Transfer Qty"
        flxItems.Cols("Item_Rate").Caption = "Item Rate"

        flxItems.Cols("Item_Code").AllowEditing = False
        flxItems.Cols("Item_Name").AllowEditing = False
        flxItems.Cols("UM_Name").AllowEditing = False
        flxItems.Cols("Batch_no").AllowEditing = False
        flxItems.Cols("Expiry_date").AllowEditing = False
        flxItems.Cols("batch_qty").AllowEditing = False
        flxItems.Cols("Stock_Detail_Id").AllowEditing = False
        flxItems.Cols("transfer_Qty").AllowEditing = True
        flxItems.Cols("Item_Rate").AllowEditing = False

        flxItems.Cols("Item_Id").Width = 40
        flxItems.Cols("Item_Code").Width = 70
        flxItems.Cols("Item_Name").Width = 250
        flxItems.Cols("UM_Name").Width = 40
        flxItems.Cols("Batch_No").Width = 70
        flxItems.Cols("Expiry_date").Width = 80
        flxItems.Cols("Batch_Qty").Width = 60
        flxItems.Cols("Stock_Detail_Id").Width = 60
        flxItems.Cols("transfer_Qty").Width = 80
        flxItems.Cols("Item_Rate").Width = 80

        flxItems.Cols("Stock_Detail_Id").Visible = False

    End Sub


    Private Sub flxItems_AfterDataRefresh(ByVal sender As System.Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles flxItems.AfterDataRefresh

    End Sub
    Private Sub generate_tree()
        flxItems.DataSource = Nothing
        flxItems.DataSource = dtable_Item_List
        format_grid()

        If flxItems.Rows.Count > 1 Then
            flxItems.Tree.Style = TreeStyleFlags.CompleteLeaf
            flxItems.Tree.Column = 2
            flxItems.AllowMerging = AllowMergingEnum.None
            Dim totalOn As Integer = flxItems.Cols("Batch_Qty").SafeIndex
            flxItems.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)
            totalOn = flxItems.Cols("transfer_Qty").SafeIndex
            flxItems.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)

            Dim cs As C1.Win.C1FlexGrid.CellStyle
            cs = Me.flxItems.Styles.Add("transfer_Qty")
            cs.ForeColor = Color.White
            cs.BackColor = Color.Green
            cs.Border.Style = BorderStyleEnum.Raised

            Dim i As Integer
            For i = 1 To flxItems.Rows.Count - 1
                If Not flxItems.Rows(i).IsNode Then flxItems.SetCellStyle(i, flxItems.Cols("transfer_Qty").SafeIndex, cs)
            Next
        End If
    End Sub

    Private Sub flxItems_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles flxItems.KeyDown
        Try
            Dim iRowindex As Int32
            If flag = "save" Then
                If e.KeyCode = Keys.Space Then
                    iRowindex = flxItems.Row

                    frm_Show_search.qry = " SELECT " & _
                                       " ITEM_MASTER.ITEM_ID,   " & _
                                       " ITEM_MASTER.ITEM_CODE, " & _
                                       " ITEM_MASTER.ITEM_NAME, " & _
                                       " ITEM_MASTER.ITEM_DESC, " & _
                                       " UNIT_MASTER.UM_Name,   " & _
                                       " ITEM_CATEGORY.ITEM_CAT_NAME, " & _
                                       " ITEM_MASTER.IS_STOCKABLE " & _
                               " FROM " & _
                                       " ITEM_MASTER " & _
                                       " INNER JOIN UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID " & _
                                       " INNER JOIN ITEM_CATEGORY ON ITEM_MASTER.ITEM_CATEGORY_ID = ITEM_CATEGORY.ITEM_CAT_ID " & _
                                        "INNER JOIN ITEM_DETAIL ON ITEM_MASTER.ITEM_ID = ITEM_DETAIL.ITEM_ID "


                    frm_Show_search.column_name = "Item_Name"
                    frm_Show_search.extra_condition = ""
                    frm_Show_search.ret_column = "Item_ID"
                    frm_Show_search.ShowDialog()
                    If Not check_item_exist(frm_Show_search.search_result) Then
                        get_row(frm_Show_search.search_result, 0)
                    End If
                End If

            End If
            If e.KeyCode = Keys.Delete Then

                Dim result As Integer
                Dim item_code As String
                result = MsgBox("Do you want to remove """ & flxItems.Rows(flxItems.CursorCell.r1).Item(3) & """ from the list?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)
                item_code = flxItems.Rows(flxItems.CursorCell.r1).Item("item_code")
                If result = MsgBoxResult.Yes Then
restart:
                    Dim dt As DataTable
                    dt = TryCast(flxItems.DataSource, DataTable)
                    If Not dt Is Nothing Then
                        For Each dr As DataRow In dt.Rows
                            If Convert.ToString(dr("item_code")) = item_code Then
                                dr.Delete()
                                GoTo restart
                            End If
                        Next
                        '        dt.AcceptChanges()
                    End If
                End If
            End If
        Catch ex As Exception
        End Try

    End Sub

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
                                        " 0.00  as transfer_qty, " & _
                                        " SD.STOCK_DETAIL_ID  " & _
                                " FROM " & _
                                        " ITEM_MASTER  IM " & _
                                        " INNER JOIN ITEM_DETAIL ID ON IM.ITEM_ID = ID.ITEM_ID " & _
                                        " INNER JOIN STOCK_DETAIL SD ON ID.ITEM_ID = SD.Item_id " & _
                                        " INNER JOIN UNIT_MASTER UM ON IM.UM_ID = UM.UM_ID" & _
                                " where " & _
                                        " IM.ITEM_ID = " & item_id & " and SD.Balance_Qty > 0"
            ds = clsObj.Fill_DataSet(sqlqry)

            Dim i As Integer
            Dim dr As DataRow

            If ds.Tables(0).Rows.Count > 0 Then
                obj.RemoveBlankRow(dtable_Item_List, "item_id")
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = dtable_Item_List.NewRow
                    dr("Item_Id") = ds.Tables(0).Rows(i)("ITEM_ID")
                    dr("Item_Code") = ds.Tables(0).Rows(i)("ITEM_CODE")
                    dr("Item_Name") = ds.Tables(0).Rows(i)("ITEM_NAME")
                    dr("UM_Name") = ds.Tables(0).Rows(i)("UM_Name")
                    dr("Batch_No") = ds.Tables(0).Rows(i)("Batch_no")
                    dr("Expiry_Date") = ds.Tables(0).Rows(i)("Expiry_Date")
                    dr("Item_Rate") = ds.Tables(0).Rows(i)("Item_Rate")
                    dr("Batch_Qty") = ds.Tables(0).Rows(i)("Balance_Qty")
                    dr("Stock_Detail_Id") = ds.Tables(0).Rows(i)("STOCK_DETAIL_ID")
                    dr("transfer_Qty") = ds.Tables(0).Rows(i)("transfer_Qty")
                    dtable_Item_List.Rows.Add(dr)
                Next
                Dim strSort As String = flxItems.Cols(1).Name + ", " + flxItems.Cols(2).Name + ", " + flxItems.Cols(3).Name



                dtable_Item_List.DefaultView.Sort = strSort

                If dtable_Item_List.Rows.Count = 0 Then dtable_Item_List.Rows.Add(dtable_Item_List.NewRow)

                generate_tree()
            Else
                MsgBox("Stock is not avaialable for this Item.", MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Function check_item_exist(ByVal item_id As Integer) As Boolean
        Dim iRow As Int32
        check_item_exist = False
        For iRow = 1 To flxItems.Rows.Count - 1
            If flxItems.Item(iRow, "item_id").ToString() <> "" Then
                If Convert.ToInt32(flxItems.Item(iRow, "item_id")) = item_id Then
                    MsgBox("Item Already Exist", MsgBoxStyle.Exclamation, gblMessageHeading)
                    check_item_exist = True
                    Exit For
                End If
            Else
                check_item_exist = False
            End If
        Next iRow
    End Function

    Private Sub flxItems_AfterEdit(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles flxItems.AfterEdit
        If flxItems.Rows(e.Row).IsNode Then Exit Sub
        If Convert.ToDecimal(flxItems.Rows(e.Row)("transfer_Qty")) > Convert.ToDecimal(flxItems.Rows(e.Row)("Batch_Qty")) Then
            flxItems.Rows(e.Row)("transfer_Qty") = 0.0
            generate_tree()
        Else
            generate_tree()
        End If
    End Sub

   
    Private Sub txtSearch_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp
        fill_grid(txtSearch.Text)
    End Sub

    Private Sub flxList_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles flxList.DoubleClick
        MsgBox("You Can't Edit this transfer." & vbCrLf & "Please click in print to view/print this transfer DC.", MsgBoxStyle.Information)
    End Sub

    Private Sub cmbOutlet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOutlet.SelectedIndexChanged
        ' String A = cmbOutlet.SelectedValue.ToString().Substring(1, 1)
    End Sub
End Class
