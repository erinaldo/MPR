Imports MMSPlus.stock_transfer_outlet
Imports System.Data.SqlClient
Imports System.Data
Imports C1.Win.C1FlexGrid

Public Class frm_Accept_stock_transfer
    Implements IForm

    Dim obj As New CommonClass
    Dim _rights As Form_Rights
    Dim dtable_Item_List As DataTable
    Dim dtableItem_List As DataTable
    Dim data_table As DataTable
    Dim prpty As cls_Stock_Transfer_prop
    Dim clsObj As New cls_Stock_Transfer_master
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
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
        Try
            If (cmb_DcNo.SelectedValue > 0) Then

                Dim dt As DataTable
                Dim i As Integer = 0
                dt = data_table
                prpty = New cls_Stock_Transfer_prop

                Dim ds As New DataSet()
                ds = obj.fill_Data_set("GET_MRN_NO", "@DIV_ID", v_the_current_division_id)
                If ds.Tables(0).Rows.Count = 0 Then
                    MsgBox("MRN NO. does not exists", MsgBoxStyle.Information, gblMessageHeading)
                    ds.Dispose()
                    Exit Sub
                Else
                    If ds.Tables(0).Rows(0)(0).ToString() = "-1" Then
                        MsgBox("MRN NO. does not exists", MsgBoxStyle.Information, gblMessageHeading)
                        ds.Dispose()
                        Exit Sub
                    ElseIf ds.Tables(0).Rows(0)(0).ToString() = "-2" Then
                        MsgBox("MRN NO. has been completed", MsgBoxStyle.Information, gblMessageHeading)
                        ds.Dispose()
                        Exit Sub
                    Else
                        prpty.MRN_PREFIX = ds.Tables(0).Rows(0)(0).ToString()
                        prpty.MRN_NO = Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString()) + 1
                        ds.Dispose()
                    End If
                End If

                prpty.TRANSFER_ID = dt.Rows(0)("Transfer_ID")
                prpty.DC_CODE = Convert.ToString(dt.Rows(0)("DC_CODE"))
                prpty.DC_NO = Convert.ToDouble(dt.Rows(0)("DC_NO"))
                prpty.TRANSFER_DATE = Convert.ToDateTime(dt.Rows(0)("TRANSFER_DATE"))
                prpty.TRANSFER_OUTLET_ID = Convert.ToInt32(dt.Rows(0)("TRANSFER_OUTLET_ID"))
                prpty.TRANSFER_STATUS = Convert.ToInt32(dt.Rows(0)("TRANSFER_STATUS"))

               
                prpty.TRANSFER_REMARKS = dt.Rows(0)("TRANSFER_REMARKS")
                prpty.MRN_REMARKS = txt_MrnRemarks.Text

                prpty.RECEIVED_DATE = Now
                prpty.FREEZED_DATE = NULL_DATE
                prpty.CREATED_BY = Convert.ToString(dt.Rows(0)("CREATED_BY"))
                prpty.CREATION_DATE = Convert.ToDateTime(dt.Rows(0)("CREATION_DATE"))
                prpty.MODIFIED_DATE = Now
                prpty.MODIFIED_BY = v_the_current_logged_in_user_name
                prpty.DIVISION_ID = Convert.ToInt32(dt.Rows(0)("DIVISION_ID"))

                prpty.Data_Table = dt

                clsObj.Update_Stock_Transfer(prpty)

                new_initilization()
                MsgBox("Stock Accepted Successfully.", MsgBoxStyle.Information, gblMessageHeading)
            Else
                MsgBox("Select DC NO.", MsgBoxStyle.Information, gblMessageHeading)
            End If

        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

    Public Sub ViewClick(ByVal sender As Object, ByVal e As System.EventArgs) Implements IForm.ViewClick

    End Sub

    Private Sub frm_Accept_stock_transfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            table_style()
            new_initilization()
            clsObj.FormatGrid(flxItems)
            clsObj.FormatGrid(flxGridItems)
            table_style_new()
            GetItemList()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub new_initilization()

        lbl_MrnDate.Text = Now.ToString("dd-MMM-yyyy")
        lbl_DcRemarks.Text = ""
        lbl_DcRemarks.Text = ""
        lbl_DCDate.Text = ""
        txt_MrnRemarks.Text = ""
        lbl_DivName.Text = ""

        dtable_Item_List.Rows.Clear()
        dtable_Item_List.Rows.Add(dtable_Item_List.NewRow)

        'dtableItem_List.Rows.Clear()
        'dtableItem_List.Rows.Add(dtableItem_List.NewRow)
        TabControl2.SelectTab(1)
        GetMrnNo()
        obj.ComboBind_Remote(cmb_DcNo, "select TRANSFER_ID,(DC_CODE + CONVERT(nvarchar(10),DC_NO)) as DCNO from STOCK_TRANSFER_MASTER where Transfer_Status=1 and TRANSFER_OUTLET_ID = " & v_the_current_division_id, "DCNO", "TRANSFER_ID", True)
        'flag = "save"
    End Sub
    ''' <summary>
    ''' TO GET MRN NO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetMrnNo()
        Try


            Dim ds As New DataSet()
            ds = obj.fill_Data_set("GET_MRN_NO", "@DIV_ID", v_the_current_division_id)
            If ds.Tables(0).Rows.Count = 0 Then
                lbl_MrnNo.Text = "MRN series does not exists"
                ds.Dispose()
                Exit Sub
            Else
                If ds.Tables(0).Rows(0)(0).ToString() = "-1" Then
                    lbl_MrnNo.Text = "MRN series does not exists"
                    ds.Dispose()
                    Exit Sub
                ElseIf ds.Tables(0).Rows(0)(0).ToString() = "-2" Then
                    lbl_MrnNo.Text = "MRN series has been completed"
                    ds.Dispose()
                    Exit Sub
                Else
                    lbl_MrnNo.Text = ds.Tables(0).Rows(0)(0).ToString() & (Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString()) + 1).ToString
                    ds.Dispose()
                End If
            End If
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub


    Private Sub cmb_DcNo_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmb_DcNo.SelectionChangeCommitted
        Try
            If (cmb_DcNo.SelectedValue > 0) Then
                Dim ds As DataSet = clsObj.GetDCDetail_remote("SELECT  STOCK_TRANSFER_MASTER.TRANSFER_ID, " & _
                                                                "STOCK_TRANSFER_MASTER.TRANSFER_DATE, " & _
                                                                "STOCK_TRANSFER_MASTER.TRANSFER_REMARKS, " & _
                                                                 "STOCK_TRANSFER_MASTER.DC_NO, " & _
                                                                  "STOCK_TRANSFER_MASTER.DC_CODE, " & _
                                                                  "STOCK_TRANSFER_MASTER.TRANSFER_OUTLET_ID, " & _
                                                                    "STOCK_TRANSFER_MASTER.TRANSFER_STATUS, " & _
                                                                    "STOCK_TRANSFER_MASTER.RECEIVED_DATE, " & _
                                                                    "STOCK_TRANSFER_MASTER.DIVISION_ID, " & _
                                                                    "STOCK_TRANSFER_MASTER.CREATED_BY, " & _
                                                                "dbo.fn_format(STOCK_TRANSFER_MASTER.CREATION_DATE) CREATION_DATE, " & _
                                                                "STOCK_TRANSFER_DETAIL.ITEM_ID, " & _
                                                                "STOCK_TRANSFER_DETAIL.TRANSFER_RATE AS ITEM_RATE, " & _
        "STOCK_TRANSFER_DETAIL.ITEM_QTY AS TRANSFER_QTY, " & _
        "STOCK_TRANSFER_DETAIL.STOCK_DETAIL_ID, " & _
        "STOCK_TRANSFER_DETAIL.ACCEPTED_STOCK_DETAIL_ID, " & _
        "STOCK_TRANSFER_DETAIL.BATCH_NO, " & _
        "dbo.fn_format(STOCK_TRANSFER_DETAIL.EXPIRY_DATE) EXPIRY_DATE, " & _
        "STOCK_TRANSFER_DETAIL.ITEM_QTY AS BATCH_QTY, " & _
        "dbo.ITEM_MASTER.ITEM_CODE, " & _
        "dbo.ITEM_MASTER.ITEM_NAME, " & _
        "dbo.UNIT_MASTER.UM_Name, " & _
                "DIVISIONMASTER.DIVISIONNAME_VCH " & _
                "FROM STOCK_TRANSFER_MASTER " & _
        "INNER JOIN STOCK_TRANSFER_DETAIL ON STOCK_TRANSFER_DETAIL.TRANSFER_ID = STOCK_TRANSFER_MASTER.TRANSFER_ID " & _
        "INNER JOIN dbo.ITEM_MASTER ON dbo.ITEM_MASTER.ITEM_ID = STOCK_TRANSFER_DETAIL.ITEM_ID " & _
        "INNER JOIN dbo.UNIT_MASTER ON dbo.UNIT_MASTER.UM_ID = dbo.ITEM_MASTER.issue_um_id " & _
        "INNER JOIN DIVISIONMASTER ON DivisionMaster.Pk_DivisionId_num = STOCK_TRANSFER_MASTER.TRANSFER_OUTLET_ID " & _
        "WHERE STOCK_TRANSFER_MASTER.TRANSFER_STATUS=1 AND STOCK_TRANSFER_DETAIL.TRANSFER_ID=" & cmb_DcNo.SelectedValue)

                If ds.Tables(0).Rows.Count = 0 Then
                    lbl_DcRemarks.Text = ""
                    lbl_DCDate.Text = ""
                    txtMRNRemarks.Text = ""
                    lbl_DivName.Text = ""
                Else
                    data_table = New DataTable
                    data_table.Rows.Clear()
                    data_table = ds.Tables(0)
                    lbl_DivName.Text = ds.Tables(0).Rows(0)("DIVISIONNAME_VCH").ToString()
                    lbl_DCDate.Text = ds.Tables(0).Rows(0)("CREATION_DATE").ToString()
                    lbl_DcRemarks.Text = ds.Tables(0).Rows(0)("TRANSFER_REMARKS").ToString()
                    dtable_Item_List.Rows.Clear()
                    Dim dr As DataRow
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        dr = dtable_Item_List.NewRow

                        With ds.Tables(0)
                            dr("ITEM_ID") = .Rows(i)("ITEM_ID")
                            dr("STOCK_DETAIL_ID") = .Rows(i)("STOCK_DETAIL_ID")
                            dr("Item_code") = .Rows(i)("item_code")
                            dr("Item_name") = .Rows(i)("item_name")
                            dr("UM_NAME") = .Rows(i)("UM_NAME")
                            dr("BATCH_NO") = .Rows(i)("BATCH_NO")
                            dr("Expiry_Date") = .Rows(i)("Expiry_Date")
                            dr("Item_Rate") = .Rows(i)("Item_Rate")
                            'dr("Batch_Qty") = .Rows(i)("Batch_Qty")
                            dr("transfer_Qty") = .Rows(i)("transfer_Qty")
                        End With

                        dtable_Item_List.Rows.Add(dr)

                    Next
                    Dim strSort As String = flxItems.Cols(1).Name + ", " + flxItems.Cols(2).Name + ", " + flxItems.Cols(3).Name
                    dtable_Item_List.DefaultView.Sort = strSort
                    generate_tree()
                End If
            Else
                MsgBox("Select DC NO.", MsgBoxStyle.Information, gblMessageHeading)
                lbl_DcRemarks.Text = ""
                lbl_DCDate.Text = ""
                txtMRNRemarks.Text = ""
                lbl_DivName.Text = ""
                dtable_Item_List.Rows.Clear()
            End If


        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub

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
            'dtable_Item_List.Columns.Add("Batch_Qty", GetType(System.Double))
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

    'Private Sub table_style()
    '    Try


    '        If dtable_Item_List Is Nothing Then dtable_Item_List = New DataTable()

    '        dtable_Item_List.Columns.Add("Item_ID", GetType(System.String))
    '        dtable_Item_List.Columns.Add("Transfer_ID", GetType(System.String))
    '        dtable_Item_List.Columns.Add("Item_code", GetType(System.String))
    '        dtable_Item_List.Columns.Add("Item_Name", GetType(System.String))
    '        dtable_Item_List.Columns.Add("UM_Name", GetType(System.String))
    '        dtable_Item_List.Columns.Add("Transfer_Rate", GetType(System.Double))
    '        dtable_Item_List.Columns.Add("Item_Qty", GetType(System.Int32))


    '        flxItems.DataSource = dtable_Item_List


    '        flxItems.Cols(0).Width = 18
    '        flxItems.Cols("Item_ID").Visible = False

    '        flxItems.Cols("Transfer_ID").Width = 18
    '        flxItems.Cols("Transfer_ID").Visible = False


    '        flxItems.Cols("item_code").Width = 120
    '        flxItems.Cols("item_name").Width = 250
    '        flxItems.Cols("um_name").Width = 100
    '        flxItems.Cols("transfer_rate").Width = 120
    '        flxItems.Cols("item_qty").Width = 120


    '        flxItems.Cols("item_code").Caption = "Item Code"
    '        flxItems.Cols("item_name").Caption = "Item Name"
    '        flxItems.Cols("um_name").Caption = "UOM"
    '        flxItems.Cols("transfer_rate").Caption = "Transfer Rate"
    '        flxItems.Cols("item_qty").Caption = "Transfered Qty"
    '    Catch ex As Exception
    '        MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
    '    End Try

    'End Sub

    Private Sub format_grid()

        flxItems.Cols(0).Width = 10
        flxItems.Cols("Item_Id").Visible = False
        flxItems.Cols("Stock_Detail_Id").Visible = True
        flxItems.Cols("Item_Code").Caption = "Item Code"
        flxItems.Cols("Item_Name").Caption = "Item Name"
        flxItems.Cols("UM_Name").Caption = "UOM"
        flxItems.Cols("Batch_no").Caption = "Batch No"
        flxItems.Cols("Expiry_date").Caption = "Expiry Date"
        'flxItems.Cols("batch_qty").Caption = "Batch Qty"
        flxItems.Cols("transfer_Qty").Caption = "Transfered Qty"
        flxItems.Cols("Item_Rate").Caption = "Item Rate"

        flxItems.Cols("Item_Code").AllowEditing = False
        flxItems.Cols("Item_Name").AllowEditing = False
        flxItems.Cols("UM_Name").AllowEditing = False
        flxItems.Cols("Batch_no").AllowEditing = False
        flxItems.Cols("Expiry_date").AllowEditing = False
        'flxItems.Cols("batch_qty").AllowEditing = False
        flxItems.Cols("Stock_Detail_Id").AllowEditing = False
        flxItems.Cols("transfer_Qty").AllowEditing = True
        flxItems.Cols("Item_Rate").AllowEditing = False

        flxItems.Cols("Item_Id").Width = 40
        flxItems.Cols("Item_Code").Width = 70
        flxItems.Cols("Item_Name").Width = 250
        flxItems.Cols("UM_Name").Width = 40
        flxItems.Cols("Batch_No").Width = 70
        flxItems.Cols("Expiry_date").Width = 80
        'flxItems.Cols("Batch_Qty").Width = 60
        flxItems.Cols("Stock_Detail_Id").Width = 60
        flxItems.Cols("transfer_Qty").Width = 80
        flxItems.Cols("Item_Rate").Width = 80


        flxItems.Cols("Stock_Detail_Id").Visible = False

    End Sub

    Private Sub generate_tree()
        flxItems.DataSource = Nothing
        flxItems.DataSource = dtable_Item_List
        format_grid()

        If flxItems.Rows.Count > 1 Then
            flxItems.Tree.Style = TreeStyleFlags.CompleteLeaf
            flxItems.Tree.Column = 2
            flxItems.AllowMerging = AllowMergingEnum.None
            'Dim totalOn As Integer = flxItems.Cols("Batch_Qty").SafeIndex
            'flxItems.Subtotal(AggregateEnum.Sum, 0, 3, totalOn)
            Dim totalOn As Integer = flxItems.Cols("transfer_Qty").SafeIndex
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


    Private Sub GetItemList()
        Try

            Dim dsListItems As New DataSet
            dsListItems = obj.fill_Data_set("GET_ACCEPT_STOCK_TRANSFER_DETAIL", "@DIV_ID", v_the_current_division_id)
            
            dtableItem_List.Rows.Clear()
            Dim dr As DataRow
            For i As Integer = 0 To dsListItems.Tables(0).Rows.Count - 1
                dr = dtableItem_List.NewRow

                With dsListItems.Tables(0)

                    dr("MRNCODE") = .Rows(i)("MRNCODE")
                    dr("Transfer_Date") = .Rows(i)("Transfer_Date")
                    dr("DC_NO") = .Rows(i)("DC_NO")
                    dr("CREATION_DATE") = .Rows(i)("CREATION_DATE")
                    dr("CREATED_BY") = .Rows(i)("CREATED_BY")
                    dr("transfer_status") = .Rows(i)("transfer_status")
                End With

                dtableItem_List.Rows.Add(dr)

            Next


        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try

    End Sub
    Private Sub table_style_new()
        Try

       
            If dtableItem_List Is Nothing Then dtableItem_List = New DataTable()


            dtableItem_List.Columns.Add("MRNCODE", GetType(System.String))
            dtableItem_List.Columns.Add("Transfer_Date", GetType(System.String))
            dtableItem_List.Columns.Add("DC_NO", GetType(System.String))
            dtableItem_List.Columns.Add("CREATION_DATE", GetType(System.String))
            dtableItem_List.Columns.Add("CREATED_BY", GetType(System.String))
            dtableItem_List.Columns.Add("transfer_status", GetType(System.String))

            flxGridItems.DataSource = dtableItem_List


            flxGridItems.Cols("MRNCODE").Width = 120
            flxGridItems.Cols("DC_NO").Width = 120
            flxGridItems.Cols("CREATED_BY").Width = 100
            flxGridItems.Cols("CREATION_DATE").Width = 120


            flxGridItems.Cols("MRNCODE").Caption = "MRN NO"
            flxGridItems.Cols("DC_NO").Caption = "DC NO"
            flxGridItems.Cols("CREATED_BY").Caption = "Transfered By"
            flxGridItems.Cols("CREATION_DATE").Caption = "DC Date"
            flxGridItems.Cols("Transfer_Date").Caption = "MRN Date"
            flxGridItems.Cols("transfer_status").Caption = "Transfer Status"
        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try


    End Sub

    Private Sub txt_Search_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Search.TextChanged
        Try
            If Not (String.IsNullOrEmpty(txt_Search.Text)) Then

                Dim qry As String = ""
                qry = " SELECT  dbo.fn_Format(CREATION_DATE) AS CREATION_DATE, " & _
                        "dbo.fn_Format(TRANSFER_DATE) AS TRANSFER_DATE ," & _
                        "STM.MRN_PREFIX+CAST(STM.MRN_NO AS VARCHAR) AS MRNCODE,CREATED_BY " & _
                        ",DC_CODE + CAST(DC_NO AS VARCHAR) AS DC_NO," & _
                        "CASE WHEN STM.TRANSFER_STATUS = 1 THEN 'Fresh'" & _
                        " WHEN STM.TRANSFER_STATUS = 2 THEN 'Accepted'" & _
                        "WHEN STM.TRANSFER_STATUS = 3 THEN 'Freezed'" & _
                        "ELSE 'Cancel'  " & _
                        "END AS TRANSFER_STATUS" & _
                        " FROM dbo.STOCK_TRANSFER_MASTER STM " & _
                        "WHERE STM.TRANSFER_OUTLET_ID=" & v_the_current_division_id & " AND (CAST(dbo.fn_Format(CREATION_DATE) AS VARCHAR) " & _
                        "+ CAST(dbo.fn_Format(TRANSFER_DATE) AS VARCHAR)" & _
                        "+ (CREATED_BY)" & _
                        "+ (DC_CODE + CAST(DC_NO AS VARCHAR)) + (CASE WHEN TRANSFER_STATUS = 1 THEN 'Fresh'" & _
                        "WHEN TRANSFER_STATUS = 2 THEN 'Accepted'" & _
                        "WHEN TRANSFER_STATUS = 3 THEN 'Freezed'" & _
                        " END ))" & _
                        " LIKE '%" & txt_Search.Text & "%'"
                Dim dsListItems As DataSet = obj.FillDataSet(qry)
                If (dsListItems.Tables.Count > 0 AndAlso dsListItems.Tables(0).Rows.Count > 0) Then
                    dtableItem_List.Rows.Clear()
                    Dim dr As DataRow
                    For i As Integer = 0 To dsListItems.Tables(0).Rows.Count - 1
                        dr = dtableItem_List.NewRow

                        With dsListItems.Tables(0)

                            dr("MRNCODE") = .Rows(i)("MRNCODE")
                            dr("Transfer_Date") = .Rows(i)("Transfer_Date")
                            dr("DC_NO") = .Rows(i)("DC_NO")
                            dr("CREATION_DATE") = .Rows(i)("CREATION_DATE")
                            dr("CREATED_BY") = .Rows(i)("CREATED_BY")
                            dr("transfer_status") = .Rows(i)("transfer_status")
                        End With

                        dtableItem_List.Rows.Add(dr)

                    Next
                    dtableItem_List.Rows.Clear()
                    dtableItem_List.Rows.Add(dtable_Item_List.NewRow)
                End If
                GetItemList()
            End If


        Catch ex As Exception
            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
        End Try
    End Sub
End Class
