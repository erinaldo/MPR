Imports System.Data.SqlClient
Public Class frm_transferData_btwn_dates
    Dim TableToTransferDataSet As New DataSet("TablesToTransfer")
    Dim TotalNumberOfRows As Int32 = 0
    Dim GlobalTables As DataSet
    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim Query As String
    Dim RecordIds As String
    Dim obj As New CommonClass

    Dim indentIdsInQuery As String
    Dim MIOIdsInQuery As String
    Dim ReceiptIdsInQuery As String
    Dim ReceivedIdsInQuery As String
    Dim MRSIdsInQuery As String
    Dim OpenPoIdsInQuery As String
    Dim PoIdsInQuery As String
    Dim RmioIdsInQuery As String
    Dim ReverseAgainstIdsInQuery As String
    Dim ReverseWithoutIdsInQuery As String
    Dim ReverseWastageIdsInQuery As String
    Dim SrlIdsInQuery As String
    Dim WastageIdsInQuery As String
    Dim AdjustmentIdsInQuery As String
    Dim StocktransferccInQuery As String
    Dim WastageIdsInQuerycc As String
    Dim Closingidscc As String
    Dim _rights As Form_Rights
    Dim fromdate As String
    Dim todate As String

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub btnDataTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataTransfer.Click
        Query = " Select isnull(CONVERT(VARCHAR,max(datatransferDate_dt),101),'01/01/1900') as Lasttransfer from Datatransfer"
        Dim LastDate As Date = Convert.ToString(obj.ExecuteScalar(Query))
        If (LastDate = "01/01/1900") Then
            FillTables()
        ElseIf (Convert.ToInt32(dtpfrmdate.Value.Date.Subtract(LastDate).TotalDays) > 1) Then
            MsgBox("Please Transfer Yesterday's Data", MsgBoxStyle.OkOnly, "Alert?")
            Exit Sub
        Else
            FillTables()
        End If
    End Sub

    Private Sub GetLocalRecordIds()
        ''set variables
        ''Indent_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(indent_id as varchar) from INDENT_MASTER" &
            " where (convert(varchar,indent_date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") &
            "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "') and Division_id = " & v_the_current_division_id & ";" &
            " select '(' + @ret + ')';"
        indentIdsInQuery = obj.ExecuteScalar(Query)

        ''MIO _ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(MIO_ID as varchar) from MATERIAL_ISSUE_TO_COST_CENTER_MASTER" &
            " where  Division_id = " & v_the_current_division_id & " " &
            " and convert(varchar,mio_date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            "; select '(' + @ret + ')';"
        MIOIdsInQuery = obj.ExecuteScalar(Query)

        ''Receipt_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(receipt_Id as varchar) from MATERIAL_RECEIVED_AGAINST_PO_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,receipt_date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            "; select '(' + @ret + ')';"
        ReceiptIdsInQuery = obj.ExecuteScalar(Query)

        ''received_id
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(received_id as varchar) from MATERIAL_RECIEVED_WITHOUT_PO_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,received_date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            "; select '(' + @ret + ')';"
        ReceivedIdsInQuery = obj.ExecuteScalar(Query)

        ''MRS_DATE
        Query = " declare @ret varchar(max);set @ret='0';" &
           " select @ret= @ret + ',' +  cast(MRS_ID as varchar) from MRS_MAIN_STORE_MASTER" &
           " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,MRS_DATE,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
           ";select '(' + @ret + ')';"
        MRSIdsInQuery = obj.ExecuteScalar(Query)

        ''OPEN---PO_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(PO_ID as varchar) from OPEN_PO_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,PO_DATE,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        OpenPoIdsInQuery = obj.ExecuteScalar(Query)

        ''PO_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(PO_ID as varchar) from PO_MASTER" &
             " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,PO_DATE,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        PoIdsInQuery = obj.ExecuteScalar(Query)

        ''RMIO_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(RMIO_ID as varchar) from ReverseMaterial_Issue_To_Cost_Center_Master" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,RMIO_DATE,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        RmioIdsInQuery = obj.ExecuteScalar(Query)

        ''ReverseMATERIAL_RECIEVED_Against_PO_MASTER------Reverse_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(Reverse_ID as varchar) from ReverseMATERIAL_RECIEVED_Against_PO_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,Reverse_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        ReverseAgainstIdsInQuery = obj.ExecuteScalar(Query)

        ''ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER------Reverse_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(Reverse_ID as varchar) from ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,Reverse_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        ReverseWithoutIdsInQuery = obj.ExecuteScalar(Query)

        ''ReverseWastage_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(ReverseWastage_ID as varchar) from ReverseWASTAGE_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,ReverseWastage_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        ReverseWastageIdsInQuery = obj.ExecuteScalar(Query)


        ''SRL_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(SRL_ID as varchar) from SUPPLIER_RATE_LIST" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,SRL_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        SrlIdsInQuery = obj.ExecuteScalar(Query)

        ''Wastage_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(Adjustment_ID as varchar) from ADJUSTMENT_MASTER" &
             " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,Adjustment_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        WastageIdsInQuery = obj.ExecuteScalar(Query)

        ''AdjustmentIds
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(Adjustment_ID as varchar) from ADJUSTMENT_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,Adjustment_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        AdjustmentIdsInQuery = obj.ExecuteScalar(Query)

        ''Stock_transfer_cc
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(TRANSFER_ID as varchar) from STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER" &
            " where convert(varchar,TRANSFER_DATE,101) between '" &
            dtpfrmDate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" &
            " select '(' + @ret + ')';"
        StocktransferccInQuery = obj.ExecuteScalar(Query)

        ''Wastage_cc
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(Wastage_id as varchar) from WASTAGE_MASTER_CC" &
            " where convert(varchar,WASTAGE_DATE,101) between '" &
            dtpfrmDate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" &
            " select '(' + @ret + ')';"
        WastageIdsInQuerycc = obj.ExecuteScalar(Query)

        ''Closing_cc_master
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(Closing_id as varchar) from CLOSING_CC_MASTER" &
            " where convert(varchar,CLOSING_DATE,101) between '" &
            dtpfrmDate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" &
            " select '(' + @ret + ')';"
        Closingidscc = obj.ExecuteScalar(Query)

    End Sub

    Private Sub GetGlobalRecordIds()
        con = New SqlConnection(gblDNS_Online)
        ''set variables
        ''Indent_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(indent_id as varchar) from INDENT_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,indent_date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        indentIdsInQuery = obj.ExecuteScalar(Query, con)

        ''MIO _ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(MIO_ID as varchar) from MATERIAL_ISSUE_TO_COST_CENTER_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,mio_date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        MIOIdsInQuery = obj.ExecuteScalar(Query, con)

        ''Receipt_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(receipt_Id as varchar) from MATERIAL_RECEIVED_AGAINST_PO_MASTER" &
             " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,receipt_date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        ReceiptIdsInQuery = obj.ExecuteScalar(Query, con)

        ''received_id
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(received_id as varchar) from MATERIAL_RECIEVED_WITHOUT_PO_MASTER" &
              " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,received_date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        ReceivedIdsInQuery = obj.ExecuteScalar(Query, con)

        ''MRS_DATE
        Query = " declare @ret varchar(max);set @ret='0';" &
           " select @ret= @ret + ',' +  cast(MRS_ID as varchar) from MRS_MAIN_STORE_MASTER" &
           " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,MRS_DATE,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        MRSIdsInQuery = obj.ExecuteScalar(Query, con)

        ''OPEN---PO_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(PO_ID as varchar) from OPEN_PO_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,PO_DATE,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        OpenPoIdsInQuery = obj.ExecuteScalar(Query, con)

        ''PO_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(PO_ID as varchar) from PO_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,PO_DATE,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        PoIdsInQuery = obj.ExecuteScalar(Query, con)

        ''RMIO_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(RMIO_ID as varchar) from ReverseMaterial_Issue_To_Cost_Center_Master" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,RMIO_DATE,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        RmioIdsInQuery = obj.ExecuteScalar(Query, con)

        ''ReverseMATERIAL_RECIEVED_Against_PO_MASTER------Reverse_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(Reverse_ID as varchar) from ReverseMATERIAL_RECIEVED_Against_PO_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,Reverse_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        ReverseAgainstIdsInQuery = obj.ExecuteScalar(Query, con)

        ''ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER------Reverse_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(Reverse_ID as varchar) from ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,Reverse_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        ReverseWithoutIdsInQuery = obj.ExecuteScalar(Query, con)

        ''ReverseWastage_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(ReverseWastage_ID as varchar) from ReverseWASTAGE_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,ReverseWastage_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        ReverseWastageIdsInQuery = obj.ExecuteScalar(Query, con)


        ''SRL_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(SRL_ID as varchar) from SUPPLIER_RATE_LIST" &
             " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,SRL_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        SrlIdsInQuery = obj.ExecuteScalar(Query, con)

        ''Wastage_ID
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(Wastage_ID as varchar) from WASTAGE_MASTER" &
              " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,wastage_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        WastageIdsInQuery = obj.ExecuteScalar(Query, con)


        ''AdjustmentIds
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(Adjustment_ID as varchar) from ADJUSTMENT_MASTER" &
            " where Division_id = " & v_the_current_division_id & "" &
            " and convert(varchar,Adjustment_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'" &
            ";select '(' + @ret + ')';"
        AdjustmentIdsInQuery = obj.ExecuteScalar(Query)

        ''Stock_transfer_cc
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(TRANSFER_ID as varchar) from STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER" &
            " where convert(varchar,TRANSFER_DATE,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" &
            " select '(' + @ret + ')';"
        StocktransferccInQuery = obj.ExecuteScalar(Query)

        ''Wastage_cc
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(Wastage_ID as varchar) from WASTAGE_MASTER_CC" &
            " where convert(varchar,wastage_Date,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" &
            " select '(' + @ret + ')';"
        WastageIdsInQuerycc = obj.ExecuteScalar(Query, con)

        ''Closing_cc_master
        Query = " declare @ret varchar(max);set @ret='0';" &
            " select @ret= @ret + ',' +  cast(Closing_id as varchar) from CLOSING_CC_MASTER" &
            " where convert(varchar,CLOSING_DATE,101) between '" &
            dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" &
            " select '(' + @ret + ')';"
        Closingidscc = obj.ExecuteScalar(Query)
    End Sub

    Private Sub FillTables()

        GetLocalRecordIds()
        Dim Table As DataTable
        TotalNumberOfRows = 0
        TableToTransferDataSet.Tables.Clear()

        ''1) CLOSING_STOCK_AVG_RATE
        'Query = " Select ID,Closing_DATE,ITEM_ID,CLOSING_STOCK,AVG_RATE, " & v_the_current_division_id & " as Division_ID from CLOSING_STOCK_AVG_RATE "
        'Table = Get_Remote_DataSet(Query).Tables(0)
        'Table.TableName = "CLOSING_STOCK_AVG_RATE"
        'TableToTransferDataSet.Tables.Add(Table.Copy())

        '1) CLOSING_STOCK_AVG_RATE
        Query = " Select ID,Closing_DATE,ITEM_ID,CLOSING_STOCK,AVG_RATE from CLOSING_STOCK_AVG_RATE "
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "CLOSING_STOCK_AVG_RATE"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '2)item_detail
        Query = " Select ITEM_ID," & v_the_current_division_id & " as DIV_ID,RE_ORDER_LEVEL,RE_ORDER_QTY,PURCHASE_VAT_ID,SALE_VAT_ID,OPENING_STOCK,CURRENT_STOCK,IS_EXTERNAL,TRANSFER_RATE,AVERAGE_RATE,OPENING_RATE,IS_ACTIVE,IS_STOCKABLE,STOCK_DETAIL_ID from item_detail "
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "item_detail"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        ''2)recipe_master
        'Query = " Select Recipe_Id,Menu_Id,Item_Id,Item_uom,Item_qty, Item_yield_qty,creation_date, created_by, Modification_date, Modified_by,  " & v_the_current_division_id & " as division_id from Recipe_master "
        'Table = Get_Remote_DataSet(Query).Tables(0)
        'Table.TableName = "Recipe_master"
        'TableToTransferDataSet.Tables.Add(Table.Copy())

        '2)recipe_master
        Query = " Select Recipe_Id,Menu_Id,Item_Id,Item_uom,Item_qty, Item_yield_qty,creation_date, created_by, Modification_date, Modified_by from Recipe_master "
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Recipe_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        ''3)STOCK_DETAIL
        'Query = " Select STOCK_DETAIL_ID,Item_id,Batch_no,Expiry_date,Item_Qty,Issue_Qty,Balance_Qty,DOC_ID,Transaction_ID, " & v_the_current_division_id & " as Division_Id from STOCK_DETAIL "
        'Table = Get_Remote_DataSet(Query).Tables(0)
        'Table.TableName = "STOCK_DETAIL"
        'TableToTransferDataSet.Tables.Add(Table.Copy())

        '3)STOCK_DETAIL
        Query = " Select STOCK_DETAIL_ID,Item_id,Batch_no,Expiry_date,Item_Qty,Issue_Qty,Balance_Qty,DOC_ID,Transaction_ID from STOCK_DETAIL "
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "STOCK_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '4)indent_master
        Query = " Select INDENT_ID,INDENT_CODE,INDENT_NO,INDENT_DATE,REQUIRED_DATE,INDENT_REMARKS,INDENT_STATUS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as Division_Id from indent_master where Indent_Id in " & indentIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "indent_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '5)indent_detail
        Query = " Select INDENT_ID,ITEM_ID,ITEM_QTY_REQ,ITEM_QTY_PO,ITEM_QTY_BAL,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as Division_Id from INDENT_DETAIL where Indent_Id in " & indentIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "INDENT_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())



        ''6)MATERIAL_ISSUE_TO_COST_CENTER_DETAIL
        'Query = " Select MIO_ID,MRS_ID,ITEM_ID,REQ_QTY,ISSUED_QTY,ACCEPTED_QTY,RETURNED_QTY,BalIssued_Qty,IS_WASTAGE,ITEM_RATE,STOCK_DETAIL_ID, " & v_the_current_division_id & " as Division_ID from MATERIAL_ISSUE_TO_COST_CENTER_DETAIL where MIO_ID in" & MIOIdsInQuery
        'Table = Get_Remote_DataSet(Query).Tables(0)
        'Table.TableName = "MATERIAL_ISSUE_TO_COST_CENTER_DETAIL"
        'TableToTransferDataSet.Tables.Add(Table.Copy())


        '6)MATERIAL_ISSUE_TO_COST_CENTER_DETAIL
        Query = " Select MIO_ID,MRS_ID,ITEM_ID,REQ_QTY,ISSUED_QTY,ACCEPTED_QTY,RETURNED_QTY,BalIssued_Qty,IS_WASTAGE,ITEM_RATE,STOCK_DETAIL_ID from MATERIAL_ISSUE_TO_COST_CENTER_DETAIL where MIO_ID in" & MIOIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MATERIAL_ISSUE_TO_COST_CENTER_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '7)material_issue_to_cost_center_master
        Query = " Select MIO_ID,MIO_CODE,MIO_NO,MIO_DATE,CS_ID,MIO_REMARKS,MIO_ACCEPT_DATE,MIO_STATUS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as Division_ID from material_issue_to_cost_center_master where MIO_ID in" & MIOIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "material_issue_to_cost_center_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        ''8)MATERIAL_RECEIVED_AGAINST_PO_DETAIL
        'Query = " Select Receipt_ID,Item_ID,PO_ID,Item_Qty,Item_Rate,Bal_Item_Qty,Stock_Detail_iD,Vat_Per,Bal_Vat_Per,Exice_per,Bal_Exice_Per, " & v_the_current_division_id & " as Division_ID, DType, DiscountValue, Cess_Per,ACess from MATERIAL_RECEIVED_AGAINST_PO_DETAIL where Receipt_Id in " & ReceiptIdsInQuery
        'Table = Get_Remote_DataSet(Query).Tables(0)
        'Table.TableName = "MATERIAL_RECEIVED_AGAINST_PO_DETAIL"
        'TableToTransferDataSet.Tables.Add(Table.Copy())

        '8)MATERIAL_RECEIVED_AGAINST_PO_DETAIL
        Query = " Select Receipt_ID,Item_ID,PO_ID,Item_Qty,Item_Rate,Bal_Item_Qty,Stock_Detail_iD,Vat_Per,Bal_Vat_Per,Exice_per,Bal_Exice_Per, DType, DiscountValue, Cess_Per,ACess from MATERIAL_RECEIVED_AGAINST_PO_DETAIL where Receipt_Id in " & ReceiptIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MATERIAL_RECEIVED_AGAINST_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '9)MATERIAL_RECEIVED_AGAINST_PO_MASTER
        Query = " Select Receipt_ID,Receipt_No,Receipt_Code,PO_ID,Receipt_Date,Remarks,MRN_NO,MRN_PREFIX,Created_BY,Creation_Date,Modified_By,Modification_Date," & v_the_current_division_id & " as Division_ID,MRN_STATUS,MRNCompanies_ID,Invoice_no,Invoice_date,freight,Other_Charges,Discount_Amt,GROSS_AMOUNT,GST_AMOUNT,NET_AMOUNT,MRN_TYPE,VAT_ON_EXICE,IsPrinted,CUST_ID,REFERENCE_ID,CESS_AMOUNT from MATERIAL_RECEIVED_AGAINST_PO_MASTER where Receipt_Id in " & ReceiptIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MATERIAL_RECEIVED_AGAINST_PO_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '10)NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO
        Query = " Select Received_ID,PO_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,batch_no,batch_date,Item_Rate,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice," & v_the_current_division_id & " as Div_ID,DType,DiscountValue from NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where Received_Id in " & ReceiptIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '11)MATERIAL_RECEIVED_WITHOUT_PO_DETAIL
        Query = " Select Received_ID,Item_ID,Item_Qty,Item_Rate,Created_By,Creation_Date,Modified_By,Modification_Date, " & v_the_current_division_id & " as Division_Id,Item_vat,Item_exice,Batch_No,Expiry_Date,Stock_Detail_Id,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice,DType,DiscountValue,Item_cess,Acess,DiscountValue1,GSTPAID from MATERIAL_RECEIVED_WITHOUT_PO_DETAIL where Received_id in " & ReceivedIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MATERIAL_RECEIVED_WITHOUT_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '12)material_recieved_without_po_master
        Query = " Select Received_ID,Received_Code,Received_No,Received_Date,Purchase_Type,Vendor_ID,Remarks,Po_ID,MRN_PREFIX,MRN_NO,Created_By,Creation_Date,Modified_By,Modification_Date,Invoice_No,Invoice_Date," & v_the_current_division_id & " as Division_ID,mrn_status,freight,freight_type,MRNCompanies_ID,Other_Charges,Discount_Amt,GROSS_AMOUNT,GST_AMOUNT,NET_AMOUNT,MRN_TYPE,VAT_ON_EXICE,IsPrinted,REFERENCE_ID,CESS_AMOUNT,CashDiscount_Amt from material_recieved_without_po_master where Received_id in " & ReceivedIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "material_recieved_without_po_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        ''13)NON_STOCKABLE_ITEMS_MAT_REC_WO_PO
        'Query = " Select Received_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,batch_no,batch_date,Item_Rate,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice, " & v_the_current_division_id & " as Division_Id,DType,DiscountValue,DiscountValue1,GSTPAID,Item_cess,Acess from NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where Received_id in " & ReceivedIdsInQuery
        'Table = Get_Remote_DataSet(Query).Tables(0)
        'Table.TableName = "NON_STOCKABLE_ITEMS_MAT_REC_WO_PO"
        'TableToTransferDataSet.Tables.Add(Table.Copy())

        '13)NON_STOCKABLE_ITEMS_MAT_REC_WO_PO
        Query = " Select Received_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,batch_no,batch_date,Item_Rate,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice,DType,DiscountValue,Item_cess,Acess,DiscountValue1,GSTPAID from NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where Received_id in " & ReceivedIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "NON_STOCKABLE_ITEMS_MAT_REC_WO_PO"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '14)MRS_MAIN_STORE_DETAIL
        Query = " Select MRS_ID,ITEM_ID,ITEM_QTY,Issue_QTY,Bal_QTY,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from MRS_MAIN_STORE_DETAIL where MRS_ID in " & MRSIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MRS_MAIN_STORE_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '15)mrs_main_store_master
        Query = " Select MRS_ID,MRS_CODE,MRS_NO,MRS_DATE,CC_ID,REQ_DATE,MRS_REMARKS,MRS_STATUS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE, " & v_the_current_division_id & " as DIVISION_ID, APPROVE_DATETIME from mrs_main_store_master where MRS_ID in " & MRSIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "mrs_main_store_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '16)OPEN_PO_DETAIL
        Query = " Select PO_ID,ITEM_NAME,UOM,ITEM_QTY,VAT_PER,EXICE_PER,ITEM_RATE,AMOUNT,TOTAL_AMOUNT,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE, " & v_the_current_division_id & " as DIVISION_ID from OPEN_PO_DETAIL where PO_ID in " & OpenPoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "OPEN_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '17)open_po_master
        Query = " Select PO_ID,PO_CODE,PO_NO,PO_TYPE,PO_DATE,PO_START_DATE,PO_END_DATE,PO_REMARKS,PO_SUPP_ID,PO_QUALITY_ID,PO_DELIVERY_ID,PO_STATUS,PATMENT_TERMS,TRANSPORT_MODE,TOTAL_AMOUNT,VAT_AMOUNT,NET_AMOUNT,EXICE_AMOUNT,OCTROI,PRICE_BASIS,FRIEGHT,OTHER_CHARGES,CESS_PER,ALREADY_RECVD,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID from open_po_master where PO_ID in " & OpenPoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "open_po_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '18)PO_DETAIL
        Query = " Select PO_ID,ITEM_ID,ITEM_QTY,Balance_Qty,VAT_PER,EXICE_PER,ITEM_RATE,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID from PO_DETAIL where PO_ID in " & PoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '19)PO_MASTER
        Query = " Select PO_ID,PO_CODE,PO_NO,PO_DATE,PO_START_DATE,PO_END_DATE,PO_REMARKS,PO_SUPP_ID,PO_QUALITY_ID,PO_DELIVERY_ID,PO_STATUS,PATMENT_TERMS,TRANSPORT_MODE,TOTAL_AMOUNT,VAT_AMOUNT,NET_AMOUNT,EXICE_AMOUNT,PO_TYPE,OCTROI,PRICE_BASIS,FRIEGHT,OTHER_CHARGES,CESS_PER,ALREADY_RECVD,DISCOUNT_AMOUNT,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID,OPEN_PO_QTY from PO_MASTER where PO_ID in " & PoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PO_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '20)PO_STATUS
        Query = " Select POS_ID,PO_ID,ITEM_ID,INDENT_ID,REQUIRED_QTY,RECIEVED_QTY,BALANCE_QTY,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID from PO_STATUS where PO_ID in " & PoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PO_STATUS"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        ''21)ReverseMaterial_Issue_To_Cost_Center_Detail
        'Query = " Select RMIO_ID,Stock_Detail_Id,ITEM_ID,Item_QTY,Avg_RATE, " & v_the_current_division_id & " as Division_ID from ReverseMaterial_Issue_To_Cost_Center_Detail where RMIO_id in " & RmioIdsInQuery
        'Table = Get_Remote_DataSet(Query).Tables(0)
        'Table.TableName = "ReverseMaterial_Issue_To_Cost_Center_Detail"
        'TableToTransferDataSet.Tables.Add(Table.Copy())

        '21)ReverseMaterial_Issue_To_Cost_Center_Detail
        Query = " Select RMIO_ID,Stock_Detail_Id,ITEM_ID,Item_QTY,Avg_RATE from ReverseMaterial_Issue_To_Cost_Center_Detail where RMIO_id in " & RmioIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMaterial_Issue_To_Cost_Center_Detail"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '22)Reversematerial_issue_to_cost_center_master
        Query = " Select RMIO_ID,RMIO_CODE,RMIO_NO,RMIO_DATE,Issue_Id,RMIO_REMARKS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID from Reversematerial_issue_to_cost_center_master where RMIO_id in " & RmioIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Reversematerial_issue_to_cost_center_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '23)ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL
        Query = " Select Reverse_ID,Item_ID,Prev_Item_Qty,Item_Qty,Prev_Item_Rate,Item_Rate,Created_By,Creation_Date,Modified_By,Modification_Date," & v_the_current_division_id & " as Division_Id,Prev_Item_vat,Prev_Item_exice,Item_vat,Item_exice,Batch_No,Expiry_Date,Stock_Detail_Id from ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL where Reverse_Id in" & ReverseWithoutIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '24)ReverseMaterial_Recieved_Without_Po_Master
        Query = " Select Reverse_ID,Reverse_Code,Reverse_No,Reverse_Date,Remarks,received_ID,Created_By,Creation_Date,Modified_By,Modification_Date," & v_the_current_division_id & " as Division_ID from ReverseMaterial_Recieved_Without_Po_Master where  Reverse_Id in" & ReverseWithoutIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMaterial_Recieved_Without_Po_Master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        ''25)REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO
        'Query = " Select Reverse_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,Item_Rate,batch_no,batch_date,prev_item_qty,prev_item_Rate,prev_item_vat,prev_item_exice, " & v_the_current_division_id & " as Division_ID from REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where Reverse_Id in " & ReverseWithoutIdsInQuery
        'Table = Get_Remote_DataSet(Query).Tables(0)
        'Table.TableName = "REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO"
        'TableToTransferDataSet.Tables.Add(Table.Copy())

        '25)REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO
        Query = " Select Reverse_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,Item_Rate,batch_no,batch_date,prev_item_qty,prev_item_Rate,prev_item_vat,prev_item_exice from REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where Reverse_Id in " & ReverseWithoutIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '26)ReverseMATERIAL_RECEIVED_Against_PO_DETAIL
        Query = " Select Reverse_ID,Item_ID,Prev_Item_Qty,Item_Qty,Prev_Item_Rate,Item_Rate,Created_By,Creation_Date,Modified_By,Modification_Date," & v_the_current_division_id & " as Division_Id,Prev_Item_vat,Prev_Item_exice,Item_vat,Item_exice,Batch_No,Expiry_Date,Stock_Detail_Id from ReverseMATERIAL_RECEIVED_Against_PO_DETAIL where Reverse_Id in" & ReverseAgainstIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMATERIAL_RECEIVED_Against_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '27)ReverseMaterial_Recieved_Against_Po_Master
        Query = " Select Reverse_ID,Reverse_Code,Reverse_No,Reverse_Date,Remarks,received_ID,Created_By,Creation_Date,Modified_By,Modification_Date," & v_the_current_division_id & " as Division_ID from ReverseMaterial_Recieved_Against_Po_Master where  Reverse_Id in" & ReverseAgainstIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMaterial_Recieved_Against_Po_Master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '28)ReverseWASTAGE_DETAIL
        Query = " Select p_ReverseWastage_ID,f_ReverseWastage_ID,Item_ID,Stock_Detail_Id,Item_Qty,item_Rate,Actual_Qty,Created_By,Creation_Date,Modified_By,Modified_Date," & v_the_current_division_id & " as Division_ID,WastageId from ReverseWASTAGE_DETAIL where f_reverseWastage_id in " & ReverseWastageIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseWASTAGE_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '29)ReverseWASTAGE_MASTER
        Query = " Select ReverseWastage_ID,WastageId,ReverseWastage_Code,ReverseWastage_No,ReverseWastage_Date,ReverseWastage_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date," & v_the_current_division_id & " as Division_ID from ReverseWASTAGE_MASTER where reverseWastage_id in " & ReverseWastageIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseWASTAGE_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '30)supplier_rate_list
        Query = " Select SRL_ID,SRL_NAME,SRL_DATE,SRL_DESC,SUPP_ID,ACTIVE,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID from supplier_rate_list where SRL_id in " & SrlIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "supplier_rate_list"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '31)SUPPLIER_RATE_LIST_DETAIL
        Query = " Select SRL_ID,ITEM_ID,ITEM_RATE,DEL_QTY,DEL_DAYS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID from SUPPLIER_RATE_LIST_DETAIL where SRL_id in " & SrlIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "SUPPLIER_RATE_LIST_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        ''32)Transaction_Log
        'Query = " Select Transaction_ID,Item_ID,STOCK_DETAIL_ID,Transaction_Type,Quantity,Trans_Date,Current_Stock, " & v_the_current_division_id & " as Division_Id from Transaction_Log where convert(varchar,trans_date,101) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        'Table = Get_Remote_DataSet(Query).Tables(0)
        'Table.TableName = "Transaction_Log"
        'TableToTransferDataSet.Tables.Add(Table.Copy())

        '33)WASTAGE_DETAIL
        Query = " Select Wastage_ID,Item_ID,Stock_Detail_Id,Item_Qty,Item_Rate,Balance_Qty,Created_By,Creation_Date,Modified_By,Modified_Date," & v_the_current_division_id & " as Division_ID from WASTAGE_DETAIL where wastage_id in " & WastageIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "WASTAGE_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '34)wastage_master
        Query = " Select Wastage_ID,Wastage_Code,Wastage_No,Wastage_Date,Wastage_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date," & v_the_current_division_id & " as Division_ID from wastage_master where wastage_id in " & WastageIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "wastage_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        ''35)User Master
        'Query = " Select user_id,user_name,password,user_role," & v_the_current_division_id & " as  division_id,CostCenter_id from User_master where division_id = " & v_the_current_division_id
        'Table = Get_Remote_DataSet(Query).Tables(0)
        'Table.TableName = "User_master_mms"
        'TableToTransferDataSet.Tables.Add(Table.Copy())

        '35)User Master
        Query = " Select user_id,user_name,password,user_role," & v_the_current_division_id & " as  division_id,CostCenter_id from User_master where division_id = " & v_the_current_division_id
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "User_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '36)ADJUSTMENT MASTER
        Query = " SELECT  Adjustment_ID,Adjustment_Code,Adjustment_No,Adjustment_Date,Adjustment_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date," & v_the_current_division_id & " as Division_ID FROM ADJUSTMENT_MASTER where Adjustment_ID in " & AdjustmentIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ADJUSTMENT_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '37)ADJUSTMENT DETAIL
        Query = " SELECT Adjustment_ID,Item_ID,Stock_Detail_Id,Item_Qty,Item_Rate,Balance_Qty,Created_By,Creation_Date,Modified_By,Modified_Date," & v_the_current_division_id & " as Division_ID FROM dbo.ADJUSTMENT_DETAIL where Adjustment_ID in " & AdjustmentIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ADJUSTMENT_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '38)STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER
        Query = " SELECT TRANSFER_ID,TRANSFER_CODE,TRANSFER_NO,TRANSFER_DATE,TRANSFER_CC_ID,TRANSFER_REMARKS,TRANSFER_STATUS,RECEIVED_DATE,Created_By,Creation_Date,Modified_By,Modified_Date,COSTCENTER_ID," & v_the_current_division_id & " as Division_ID FROM dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER where TRANSFER_ID in " & StocktransferccInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '39)STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL
        Query = " SELECT TRANSFER_ID,ITEM_ID,ITEM_QTY,ACCEPTED_QTY,RETURNED_QTY,Created_By,Creation_Date,Modified_By,Modified_Date,COSTCENTER_ID,TRANSFER_RATE," & v_the_current_division_id & " as Division_ID FROM dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL where TRANSFER_ID in " & StocktransferccInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '40)WASTAGE_DETAIL_CC
        Query = " Select Wastage_ID,Item_ID,Item_Qty,Item_Rate,Balance_Qty,Created_By,Creation_Date,Modified_By,Modified_Date,CostCenter_id," & v_the_current_division_id & " as Division_ID from WASTAGE_DETAIL_CC where wastage_id in " & WastageIdsInQuerycc
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "WASTAGE_DETAIL_CC"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '41)wastage_master_cc
        Query = " Select Wastage_ID,Wastage_Code,Wastage_No,Wastage_Date,Wastage_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date,CostCenter_id," & v_the_current_division_id & " as Division_ID from wastage_master_cc where wastage_id in " & WastageIdsInQuerycc
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "wastage_master_cc"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '42)Closing_cc_master
        Query = " Select Closing_id,closing_code,Closing_no,closing_date,closing_remarks,closing_status,Created_By,Creation_Date,Modified_By,Modified_Date,CostCenter_id," & v_the_current_division_id & " as Division_ID from Closing_cc_master where Closing_id in " & Closingidscc
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "CLOSING_CC_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '43)Closing_cc_Detail
        Query = " Select Closing_id,item_id,item_qty,item_rate,Current_stock,Consumption,Created_BY,Creation_Date,Modified_BY,Modified_Date,CostCenter_id," & v_the_current_division_id & " as Division_ID from Closing_cc_detail where closing_id in " & Closingidscc
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Closing_cc_detail"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        ''44)REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO
        'Query = " SELECT Reverse_id, Item_id, Costcenter_id, po_id,Item_qty, Item_vat, Item_exice, Item_rate, Prev_Item_qty, Prev_item_rate, Prev_item_vat, Prev_item_exice, batch_no, batch_date, " & v_the_current_division_id & " AS division_id FROM REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where Reverse_Id in " & ReverseAgainstIdsInQuery
        'Table = Get_Remote_DataSet(Query).Tables(0)
        'Table.TableName = "REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO"
        'TableToTransferDataSet.Tables.Add(Table.Copy())

        '44)REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO
        Query = " SELECT Reverse_id, Item_id, Costcenter_id, po_id,Item_qty, Item_vat, Item_exice, Item_rate, Prev_Item_qty, Prev_item_rate, Prev_item_vat, Prev_item_exice, batch_no, batch_date FROM REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where Reverse_Id in " & ReverseAgainstIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        'New Tables Added

        '45)SALE_INVOICE_DETAIL
        Query = " SELECT  SI_ID,ITEM_ID,ITEM_QTY,PKT,ITEM_RATE,ITEM_AMOUNT,VAT_PER,VAT_AMOUNT,BAL_ITEM_RATE,BAL_ITEM_QTY,
						 CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " AS DIVISION_ID,TARRIF_ID,ITEM_MRP,ITEM_MRP_DESC,
						 ASSESIBLE_PER,ASSESIBLE_VALUE,EXCISE_PER,EXCISE_AMOUNT,EDU_CESS_PER,EDU_CESS_VALUE,SHE_CESS_PER,	
						 SHE_CESS_VALUE,ITEM_DISCOUNT,DISCOUNT_TYPE,DISCOUNT_VALUE,GSTPaid,CessPercentage_num,CessAmount_num,	
						 MRP, ACess,ACessAmount  FROM SALE_INVOICE_DETAIL sid
                         WHERE sid.DIVISION_ID = " & v_the_current_division_id & " and cast(sid.CREATION_DATE AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "SALE_INVOICE_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '46)SALE_INVOICE_MASTER
        Query = " SELECT SI_ID,SI_CODE,SI_NO,DC_GST_NO,SI_DATE,CUST_ID,REMARKS,PAYMENTS_REMARKS,SALE_TYPE,
					   GROSS_AMOUNT,VAT_AMOUNT,NET_AMOUNT,IS_SAMPLE,SAMPLE_ADDRESS,VAT_CST_PER,INVOICE_STATUS,
					   DELIVERY_NOTE_NO,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,
                       " & v_the_current_division_id & " AS DIVISION_ID, is_InvCancel,
					   VEHICLE_NO,SHIPP_ADD_ID,INV_TYPE,LR_NO,SaleExecutiveId,REQ_ID,DELIVERED_BY,TempInvoiceId,
					   PAY_STATUS,TRANSPORT,REFERENCE_ID,FLAG,CESS_AMOUNT FROM SALE_INVOICE_MASTER sim
                       WHERE sim.DIVISION_ID = " & v_the_current_division_id & " and cast(sim.CREATION_DATE AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "SALE_INVOICE_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '47)SALE_INVOICE_STOCK_DETAIL
        Query = " Select SI_ID,ITEM_ID,STOCK_DETAIL_ID,ITEM_QTY,NOOFCASES,ITEM_PKT_WEIGHT,
                " & v_the_current_division_id & " AS DIVISION_ID FROM SALE_INVOICE_STOCK_DETAIL sisd"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "SALE_INVOICE_STOCK_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '48)PaymentTransaction
        Query = " SELECT PaymentTransactionId,PaymentTransactionNo,PaymentTypeId,AccountId,PaymentDate,ChequeDraftNo ,
						ChequeDraftDate,BankId,BankDate,Remarks,TotalAmountReceived,UndistributedAmount,CancellationCharges ,
						CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,StatusId,PM_TYPE,
                        " & v_the_current_division_id & " AS DIVISION_ID FROM PaymentTransaction pt
                        WHERE cast(pt.PaymentDate AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PaymentTransaction"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '49)SettlementDetail
        Query = " SELECT PaymentTransactionId,InvoiceId,AmountSettled,Remarks,CreatedBy,CreatedDate,PaymentId,
                " & v_the_current_division_id & " AS DivisionId FROM SettlementDetail sd
                WHERE sd.DivisionId = " & v_the_current_division_id & " and cast(sd.CreatedDate AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "SettlementDetail"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '50)DebitNote_DETAIL
        Query = " SELECT DebitNote_Id,Item_ID,Item_Qty,Created_By,Creation_Date,Modified_By,Modification_Date,Stock_Detail_Id,Item_Rate,
				  Item_Tax,Item_Cess," & v_the_current_division_id & " AS Division_Id FROM DebitNote_DETAIL dnd
                  WHERE dnd.Division_Id = " & v_the_current_division_id & " and cast(dnd.Creation_Date As Date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "DebitNote_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '51)DebitNote_Master
        Query = " SELECT DebitNote_Id,DebitNote_Code,DebitNote_No,DebitNote_Date,MRNId,Remarks,Created_by,
					Creation_Date,Modified_By,Modification_Date," & v_the_current_division_id & " AS Division_Id,DN_Amount,DN_CustId,INV_No,INV_Date,
					REFERENCE_ID,DebitNote_Type,RefNo,RefDate_dt,Tax_num,Cess_num
                    FROM DebitNote_Master dnm
                    WHERE dnm.Division_Id = " & v_the_current_division_id & " and cast(dnm.DebitNote_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "DebitNote_Master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '52)CreditNote_DETAIL
        Query = " SELECT CreditNote_Id,Item_ID,Item_Qty,Created_By,Creation_Date,Modified_By,
						Modification_Date, " & v_the_current_division_id & " AS Division_Id,
                        Stock_Detail_Id,Item_Rate,Item_Tax,Item_Cess
                         FROM CreditNote_DETAIL cnd
                        WHERE cnd.Division_Id = " & v_the_current_division_id & " and cast(cnd.Creation_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "CreditNote_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '53)CreditNote_Master
        Query = " SELECT CreditNote_Id,CreditNote_Code,CreditNote_No,CreditNote_Date,INVId,
						Remarks,Created_by,Creation_Date,Modified_By,Modification_Date,
                        " & v_the_current_division_id & " AS Division_Id,
                        CN_Amount,CN_CustId,INV_No,INV_Date,REFERENCE_ID,CreditNote_Type,
						RefNo,RefDate_dt,Tax_Amt,Cess_Amt FROM CreditNote_Master cnm
                        WHERE cnm.Division_Id = " & v_the_current_division_id & " and cast(cnm.CreditNote_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "CreditNote_Master"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        Bulk_Copy(TableToTransferDataSet)

    End Sub

    Public Sub Bulk_Copy(ByVal _Table As DataSet)
        Dim Table1 As DataTable = Nothing


        GetGlobalRecordIds()
        con = New SqlConnection(gblDNS_OnlineMMS)
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        Dim tran As SqlTransaction
        tran = con.BeginTransaction
        Try

            Dim cmd As SqlCommand
            'cmd = New SqlCommand("delete from CLOSING_STOCK_AVG_RATE where division_id=" & v_the_current_division_id & " ")
            cmd = New SqlCommand("delete from CLOSING_STOCK_AVG_RATE ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ITEM_DETAIL where div_id=" & v_the_current_division_id & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from Recipe_master where division_id=" & v_the_current_division_id & " ")
            cmd = New SqlCommand("delete from Recipe_master ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from STOCK_DETAIL where division_id=" & v_the_current_division_id & " ")
            cmd = New SqlCommand("delete from STOCK_DETAIL ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from INDENT_DETAIL where division_id=" & v_the_current_division_id & " and Indent_Id in " & indentIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from INDENT_MASTER where division_id=" & v_the_current_division_id & " and Indent_Id in " & indentIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from MATERIAL_ISSUE_TO_COST_CENTER_DETAIL where division_id=" & v_the_current_division_id & " and MIO_ID in " & MIOIdsInQuery & " ")
            cmd = New SqlCommand("delete from MATERIAL_ISSUE_TO_COST_CENTER_DETAIL where MIO_ID in " & MIOIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from MATERIAL_ISSUE_TO_COST_CENTER_Master where division_id=" & v_the_current_division_id & " and MIO_ID in " & MIOIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from MATERIAL_RECEIVED_AGAINST_PO_DETAIL where division_id=" & v_the_current_division_id & " and Receipt_Id in " & ReceiptIdsInQuery & " ")
            cmd = New SqlCommand("delete from MATERIAL_RECEIVED_AGAINST_PO_DETAIL where Receipt_Id in " & ReceiptIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from MATERIAL_RECEIVED_AGAINST_PO_MASTER where division_id=" & v_the_current_division_id & " and  Receipt_Id in " & ReceiptIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where div_id=" & v_the_current_division_id & " and  Received_Id in " & ReceiptIdsInQuery & " ")
            'cmd = New SqlCommand("delete from NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where Received_Id in " & ReceiptIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from MATERIAL_RECEIVED_WITHOUT_PO_DETAIL where division_id=" & v_the_current_division_id & " and  Received_Id in " & ReceivedIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from MATERIAL_RECIEVED_WITHOUT_PO_MASTER where division_id=" & v_the_current_division_id & " and  Received_Id in " & ReceivedIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            'cmd = New SqlCommand("delete from NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where division_id=" & v_the_current_division_id & " and  Received_Id in " & ReceivedIdsInQuery & " ")
            cmd = New SqlCommand("delete from NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where Received_Id in " & ReceivedIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            '''''''''''''''''''''''''''''''''''''''


            cmd = New SqlCommand("delete from MRS_MAIN_STORE_Detail where division_id=" & v_the_current_division_id & " and  MRS_Id in " & MRSIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            cmd = New SqlCommand("delete from MRS_MAIN_STORE_MASTER where division_id=" & v_the_current_division_id & " and  MRS_Id in " & MRSIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''
            cmd = New SqlCommand("delete from OPEN_PO_Detail where division_id=" & v_the_current_division_id & " and  PO_Id in " & OpenPoIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            cmd = New SqlCommand("delete from OPEN_PO_MASTER where division_id=" & v_the_current_division_id & " and  PO_Id in " & OpenPoIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            '''''''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from PO_DETAIL where division_id=" & v_the_current_division_id & " and  PO_Id in " & PoIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            cmd = New SqlCommand("delete from PO_Master where division_id=" & v_the_current_division_id & " and  PO_Id in " & PoIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from PO_STATUS where division_id=" & v_the_current_division_id & " and  PO_Id in " & PoIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            '''''''''''''''''''''''''''''''''''''''''

            'cmd = New SqlCommand("delete from ReverseMaterial_Issue_To_Cost_Center_Detail where division_id=" & v_the_current_division_id & " and  RMIO_Id in " & RmioIdsInQuery & " ")
            cmd = New SqlCommand("delete from ReverseMaterial_Issue_To_Cost_Center_Detail where RMIO_Id in " & RmioIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ReverseMaterial_Issue_To_Cost_Center_Master where division_id=" & v_the_current_division_id & " and  RMIO_Id in " & RmioIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from ReverseMATERIAL_RECEIVED_Against_PO_DETAIL where division_id=" & v_the_current_division_id & " and  Reverse_Id in " & ReverseAgainstIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ReverseMATERIAL_RECIEVED_Against_PO_MASTER where division_id=" & v_the_current_division_id & " and  Reverse_Id in " & ReverseAgainstIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''''''


            cmd = New SqlCommand("delete from ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL where division_id=" & v_the_current_division_id & " and  Reverse_Id in " & ReverseWithoutIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            'cmd = New SqlCommand("delete from REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where division_id=" & v_the_current_division_id & " and  Reverse_Id in " & ReverseWithoutIdsInQuery & " ")
            cmd = New SqlCommand("delete from REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where Reverse_Id in " & ReverseWithoutIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where division_id=" & v_the_current_division_id & " and  Reverse_Id in " & ReverseAgainstIdsInQuery & " ")
            cmd = New SqlCommand("delete from REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where Reverse_Id in " & ReverseAgainstIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER where division_id=" & v_the_current_division_id & " and  Reverse_Id in " & ReverseWithoutIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from ReverseWASTAGE_Detail where division_id=" & v_the_current_division_id & " and  F_ReverseWastage_Id in " & ReverseWastageIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ReverseWASTAGE_MASTER where division_id=" & v_the_current_division_id & " and  ReverseWastage_Id in " & ReverseWastageIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from SUPPLIER_RATE_LIST where division_id=" & v_the_current_division_id & " and  SRL_Id in " & SrlIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from SUPPLIER_RATE_LIST_DETAIL where division_id=" & v_the_current_division_id & " and  SRL_Id in " & SrlIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''''

            'cmd = New SqlCommand("delete from Transaction_Log where division_id=" & v_the_current_division_id & " and  convert(varchar,trans_Date,101) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd = New SqlCommand("delete from Transaction_Log where convert(varchar,trans_Date,101) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from WASTAGE_Detail where division_id=" & v_the_current_division_id & " and Wastage_Id in " & WastageIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from WASTAGE_MASTER where division_id=" & v_the_current_division_id & " and Wastage_Id in " & WastageIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            'cmd = New SqlCommand("delete from user_master_mms where division_id=" & v_the_current_division_id)
            'cmd.Transaction = tran
            'cmd.Connection = con
            'cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from user_master where division_id=" & v_the_current_division_id)
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ADJUSTMENT_MASTER where division_id=" & v_the_current_division_id & " and Adjustment_ID in " & AdjustmentIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ADJUSTMENT_Detail where division_id=" & v_the_current_division_id & " and Adjustment_ID in " & AdjustmentIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER where DIVISION_ID=" & v_the_current_division_id & " and TRANSFER_ID in " & StocktransferccInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL where DIVISION_ID=" & v_the_current_division_id & " and TRANSFER_ID in " & StocktransferccInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from WASTAGE_Detail_cc where division_id=" & v_the_current_division_id & " and Wastage_Id in " & WastageIdsInQuerycc & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from WASTAGE_MASTER_CC where division_id=" & v_the_current_division_id & " and Wastage_Id in " & WastageIdsInQuerycc & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from Closing_cc_master where division_id=" & v_the_current_division_id & " and Closing_Id in " & Closingidscc & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from Closing_cc_detail where division_id=" & v_the_current_division_id & " and Closing_Id in " & Closingidscc & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            '--------------- Newly added tables -----------------

            cmd = New SqlCommand("delete from SALE_INVOICE_DETAIL where division_id=" & v_the_current_division_id & " and cast(CREATION_DATE AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from SALE_INVOICE_MASTER where division_id=" & v_the_current_division_id & " and cast(CREATION_DATE AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from SALE_INVOICE_STOCK_DETAIL ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from PaymentTransaction where cast(PaymentDate AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from SettlementDetail where divisionid=" & v_the_current_division_id & " and cast(CreatedDate AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from DebitNote_DETAIL where division_id=" & v_the_current_division_id & " and cast(Creation_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from DebitNote_Master where division_id=" & v_the_current_division_id & " and cast(DebitNote_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from CreditNote_DETAIL where division_id=" & v_the_current_division_id & " and cast(Creation_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from CreditNote_Master where division_id=" & v_the_current_division_id & " and cast(CreditNote_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            Dim BulkCopy As New SqlBulkCopy(con, SqlBulkCopyOptions.Default, tran)

            pbardatatransfer.Value = 0
            pbardatatransfer.Minimum = 0
            pbardatatransfer.Maximum = _Table.Tables.Count
            If (lstTablesName.Items.Count > 0) Then
                lstTablesName.Items.Clear()
            End If
            For Each Table1 In _Table.Tables
                BulkCopy.DestinationTableName = Table1.TableName
                BulkCopy.WriteToServer(Table1)
                lstTablesName.Items.Add(Table1.TableName)
                pbardatatransfer.Value += 1
                System.Windows.Forms.Application.DoEvents()
            Next
            If Not IsNothing(pbardatatransfer) Then
                pbardatatransfer.Value = pbardatatransfer.Maximum
            End If

            BulkCopy.Close()

            tran.Commit()
            con.Close()

            Dim res As Int32 = MsgBox("Data Transfer Successfully Completed", MsgBoxStyle.OkOnly, "Data Transfer")
            If (res = 1) Then
                con = New SqlConnection(DNS)
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()

                Dim congbl As SqlConnection = New SqlConnection(gblDNS_Online)
                If congbl.State = ConnectionState.Open Then congbl.Close()
                congbl.Open()

                Dim frmdate As DateTime = dtpfrmdate.Value.Date
                While frmdate <= dtptodate.Value.Date
                    Query = "select isnull(max(DataTransferId_num),0)+1 from DataTransfer"
                    Dim TransferId As Int32 = obj.ExecuteScalar(Query)
                    cmd = New SqlCommand("insert into DataTransfer(DataTransferId_num,DataTransferDate_dt,IsCompleted_bit,fk_CreatedBy_num,CreatedDate_dt,fk_ModifiedBy_num,ModifiedDate_dt) values (" & TransferId & ",'" & frmdate.ToString() & "',1,1,GETDATE(),1,GETDATE())")
                    'cmd.Transaction = tran
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()

                    'cmd = New SqlCommand("insert into DataTransfer(DataTransferId_num,DataTransferDate_dt,IsCompleted_bit,fk_CreatedBy_num,CreatedDate_dt,fk_ModifiedBy_num,ModifiedDate_dt,division_id) values (" & TransferId & ",'" & frmdate.ToString() & "',1,1,GETDATE(),1,GETDATE()," & v_the_current_division_id & ")")
                    'cmd.Connection = congbl
                    'cmd.ExecuteNonQuery()
                    frmdate = frmdate.AddDays(1)
                End While


                'Query = "select isnull(max(DataTransferId_num),0)+1 from DataTransfer"
                'Dim TransferId As Int32 = obj.ExecuteScalar(Query)
                'cmd = New SqlCommand("insert into DataTransfer(DataTransferId_num,DataTransferDate_dt,IsCompleted_bit,fk_CreatedBy_num,CreatedDate_dt,fk_ModifiedBy_num,ModifiedDate_dt) values (" & TransferId & ",'" & dtptodate.Value.Date.ToString() & "',1,1,GETDATE(),1,GETDATE())")
                ''cmd.Transaction = tran
                'cmd.Connection = con
                'cmd.ExecuteNonQuery()
            End If
            'Me.Close()

            ''********************************************************************''

            ''********************************************************************''  


        Catch ex As Exception
            tran.Rollback()
            MsgBox(ex.Message & vbCrLf & Table1.TableName)
        End Try
    End Sub

    Public Function Get_Remote_DataSet(ByVal qry As String) As DataSet
        cmd = New SqlCommand()
        Dim ds_new As New DataSet()
        Try
            con = New SqlConnection(DNS)
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error --> Class Connection")
        End Try
        cmd.Connection = con
        'cmd.CommandText = "SELECT *,CityMaster.CityName_vch FROM OutletMaster INNER JOIN outletSettings ON OutletMaster.Pk_OutletId_num = outletSettings.Outlet_id INNER JOIN dbo.CityMaster ON OutletMaster.fk_CityId_num = dbo.CityMaster.pk_CityId_num WHERE outletSettings.Outlet_id = " + outlet_id.ToString()
        cmd.CommandText = qry
        cmd.CommandType = CommandType.Text
        Dim adp As New SqlDataAdapter(cmd)

        adp.Fill(ds_new)

        Return ds_new
    End Function

    Private Sub frm_transferData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        fromdate = dtpfrmdate.Value.ToString("yyyy-MM-dd")
        todate = dtptodate.Value.ToString("yyyy-MM-dd")

    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub

    Private Sub btnDataTransferAdmin_Click(sender As Object, e As EventArgs) Handles btnDataTransferAdmin.Click

        Query = " Select isnull(CONVERT(VARCHAR,max(datatransferDate_dt),101),'01/01/1900') as Lasttransfer from Datatransfer"
        Dim LastDate As Date = Convert.ToString(obj.ExecuteScalar(Query))
        If (LastDate = "01/01/1900") Then
            FillTablesAdmin()
        ElseIf (Convert.ToInt32(dtpfrmdate.Value.Date.Subtract(LastDate).TotalDays) > 1) Then
            MsgBox("Please Transfer Yesterday's Data", MsgBoxStyle.OkOnly, "Alert?")
            Exit Sub
        Else
            FillTablesAdmin()
        End If

    End Sub

    Private Sub FillTablesAdmin()

        GetLocalRecordIds()
        Dim Table As DataTable
        TotalNumberOfRows = 0
        TableToTransferDataSet.Tables.Clear()

        '1) CLOSING_STOCK_AVG_RATE
        Query = " Select ID,Closing_DATE,ITEM_ID,CLOSING_STOCK,AVG_RATE, " & v_the_current_division_id & " as Division_ID from CLOSING_STOCK_AVG_RATE "
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "CLOSING_STOCK_AVG_RATE"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '2)item_detail
        Query = " Select ITEM_ID," & v_the_current_division_id & " as DIV_ID,RE_ORDER_LEVEL,RE_ORDER_QTY,PURCHASE_VAT_ID,SALE_VAT_ID,OPENING_STOCK,CURRENT_STOCK,IS_EXTERNAL,TRANSFER_RATE,AVERAGE_RATE,OPENING_RATE,IS_ACTIVE,IS_STOCKABLE,STOCK_DETAIL_ID from item_detail "
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "item_detail"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '2)recipe_master
        Query = " Select Recipe_Id,Menu_Id,Item_Id,Item_uom,Item_qty, Item_yield_qty,creation_date, created_by, Modification_date, Modified_by,  " & v_the_current_division_id & " as division_id from Recipe_master "
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Recipe_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '3)STOCK_DETAIL
        Query = " Select STOCK_DETAIL_ID,Item_id,Batch_no,Expiry_date,Item_Qty,Issue_Qty,Balance_Qty,DOC_ID,Transaction_ID, " & v_the_current_division_id & " as Division_Id from STOCK_DETAIL "
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "STOCK_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '4)indent_master
        Query = " Select INDENT_ID,INDENT_CODE,INDENT_NO,INDENT_DATE,REQUIRED_DATE,INDENT_REMARKS,INDENT_STATUS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as Division_Id from indent_master where Indent_Id in " & indentIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "indent_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '5)indent_detail
        Query = " Select INDENT_ID,ITEM_ID,ITEM_QTY_REQ,ITEM_QTY_PO,ITEM_QTY_BAL,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as Division_Id from INDENT_DETAIL where Indent_Id in " & indentIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "INDENT_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())



        '6)MATERIAL_ISSUE_TO_COST_CENTER_DETAIL
        Query = " Select MIO_ID,MRS_ID,ITEM_ID,REQ_QTY,ISSUED_QTY,ACCEPTED_QTY,RETURNED_QTY,BalIssued_Qty,IS_WASTAGE,ITEM_RATE,STOCK_DETAIL_ID, " & v_the_current_division_id & " as Division_ID from MATERIAL_ISSUE_TO_COST_CENTER_DETAIL where MIO_ID in" & MIOIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MATERIAL_ISSUE_TO_COST_CENTER_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '7)material_issue_to_cost_center_master
        Query = " Select MIO_ID,MIO_CODE,MIO_NO,MIO_DATE,CS_ID,MIO_REMARKS,MIO_ACCEPT_DATE,MIO_STATUS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as Division_ID from material_issue_to_cost_center_master where MIO_ID in" & MIOIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "material_issue_to_cost_center_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '8)MATERIAL_RECEIVED_AGAINST_PO_DETAIL
        Query = " Select Receipt_ID,Item_ID,PO_ID,Item_Qty,Item_Rate,Bal_Item_Qty,Stock_Detail_iD,Vat_Per,Bal_Vat_Per,Exice_per,Bal_Exice_Per, " & v_the_current_division_id & " as Division_ID, DType, DiscountValue, Cess_Per,ACess from MATERIAL_RECEIVED_AGAINST_PO_DETAIL where Receipt_Id in " & ReceiptIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MATERIAL_RECEIVED_AGAINST_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '9)MATERIAL_RECEIVED_AGAINST_PO_MASTER
        Query = " Select Receipt_ID,Receipt_No,Receipt_Code,PO_ID,Receipt_Date,Remarks,MRN_NO,MRN_PREFIX,Created_BY,Creation_Date,Modified_By,Modification_Date," & v_the_current_division_id & " as Division_ID,MRN_STATUS,MRNCompanies_ID,Invoice_no,Invoice_date,freight,Other_Charges,Discount_Amt,GROSS_AMOUNT,GST_AMOUNT,NET_AMOUNT,MRN_TYPE,VAT_ON_EXICE,IsPrinted,CUST_ID,REFERENCE_ID,CESS_AMOUNT from MATERIAL_RECEIVED_AGAINST_PO_MASTER where Receipt_Id in " & ReceiptIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MATERIAL_RECEIVED_AGAINST_PO_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '10)NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO
        Query = " Select Received_ID,PO_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,batch_no,batch_date,Item_Rate,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice," & v_the_current_division_id & " as Div_ID,DType,DiscountValue from NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where Received_Id in " & ReceiptIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '11)MATERIAL_RECEIVED_WITHOUT_PO_DETAIL
        Query = " Select Received_ID,Item_ID,Item_Qty,Item_Rate,Created_By,Creation_Date,Modified_By,Modification_Date, " & v_the_current_division_id & " as Division_Id,Item_vat,Item_exice,Batch_No,Expiry_Date,Stock_Detail_Id,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice,DType,DiscountValue,Item_cess,Acess,DiscountValue1,GSTPAID from MATERIAL_RECEIVED_WITHOUT_PO_DETAIL where Received_id in " & ReceivedIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MATERIAL_RECEIVED_WITHOUT_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '12)material_recieved_without_po_master
        Query = " Select Received_ID,Received_Code,Received_No,Received_Date,Purchase_Type,Vendor_ID,Remarks,Po_ID,MRN_PREFIX,MRN_NO,Created_By,Creation_Date,Modified_By,Modification_Date,Invoice_No,Invoice_Date," & v_the_current_division_id & " as Division_ID,mrn_status,freight,freight_type,MRNCompanies_ID,Other_Charges,Discount_Amt,GROSS_AMOUNT,GST_AMOUNT,NET_AMOUNT,MRN_TYPE,VAT_ON_EXICE,IsPrinted,REFERENCE_ID,CESS_AMOUNT,CashDiscount_Amt from material_recieved_without_po_master where Received_id in " & ReceivedIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "material_recieved_without_po_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '13)NON_STOCKABLE_ITEMS_MAT_REC_WO_PO
        Query = " Select Received_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,batch_no,batch_date,Item_Rate,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice, " & v_the_current_division_id & " as Division_Id,DType,DiscountValue,DiscountValue1,GSTPAID,Item_cess,Acess from NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where Received_id in " & ReceivedIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "NON_STOCKABLE_ITEMS_MAT_REC_WO_PO"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '14)MRS_MAIN_STORE_DETAIL
        Query = " Select MRS_ID,ITEM_ID,ITEM_QTY,Issue_QTY,Bal_QTY,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from MRS_MAIN_STORE_DETAIL where MRS_ID in " & MRSIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MRS_MAIN_STORE_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '15)mrs_main_store_master
        Query = " Select MRS_ID,MRS_CODE,MRS_NO,MRS_DATE,CC_ID,REQ_DATE,MRS_REMARKS,MRS_STATUS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE, " & v_the_current_division_id & " as DIVISION_ID, APPROVE_DATETIME from mrs_main_store_master where MRS_ID in " & MRSIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "mrs_main_store_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '16)OPEN_PO_DETAIL
        Query = " Select PO_ID,ITEM_NAME,UOM,ITEM_QTY,VAT_PER,EXICE_PER,ITEM_RATE,AMOUNT,TOTAL_AMOUNT,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE, " & v_the_current_division_id & " as DIVISION_ID from OPEN_PO_DETAIL where PO_ID in " & OpenPoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "OPEN_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '17)open_po_master
        Query = " Select PO_ID,PO_CODE,PO_NO,PO_TYPE,PO_DATE,PO_START_DATE,PO_END_DATE,PO_REMARKS,PO_SUPP_ID,PO_QUALITY_ID,PO_DELIVERY_ID,PO_STATUS,PATMENT_TERMS,TRANSPORT_MODE,TOTAL_AMOUNT,VAT_AMOUNT,NET_AMOUNT,EXICE_AMOUNT,OCTROI,PRICE_BASIS,FRIEGHT,OTHER_CHARGES,CESS_PER,ALREADY_RECVD,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID from open_po_master where PO_ID in " & OpenPoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "open_po_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '18)PO_DETAIL
        Query = " Select PO_ID,ITEM_ID,ITEM_QTY,Balance_Qty,VAT_PER,EXICE_PER,ITEM_RATE,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID from PO_DETAIL where PO_ID in " & PoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '19)PO_MASTER
        Query = " Select PO_ID,PO_CODE,PO_NO,PO_DATE,PO_START_DATE,PO_END_DATE,PO_REMARKS,PO_SUPP_ID,PO_QUALITY_ID,PO_DELIVERY_ID,PO_STATUS,PATMENT_TERMS,TRANSPORT_MODE,TOTAL_AMOUNT,VAT_AMOUNT,NET_AMOUNT,EXICE_AMOUNT,PO_TYPE,OCTROI,PRICE_BASIS,FRIEGHT,OTHER_CHARGES,CESS_PER,ALREADY_RECVD,DISCOUNT_AMOUNT,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID,OPEN_PO_QTY from PO_MASTER where PO_ID in " & PoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PO_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '20)PO_STATUS
        Query = " Select POS_ID,PO_ID,ITEM_ID,INDENT_ID,REQUIRED_QTY,RECIEVED_QTY,BALANCE_QTY,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID from PO_STATUS where PO_ID in " & PoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PO_STATUS"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '21)ReverseMaterial_Issue_To_Cost_Center_Detail
        Query = " Select RMIO_ID,Stock_Detail_Id,ITEM_ID,Item_QTY,Avg_RATE, " & v_the_current_division_id & " as Division_ID from ReverseMaterial_Issue_To_Cost_Center_Detail where RMIO_id in " & RmioIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMaterial_Issue_To_Cost_Center_Detail"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '22)Reversematerial_issue_to_cost_center_master
        Query = " Select RMIO_ID,RMIO_CODE,RMIO_NO,RMIO_DATE,Issue_Id,RMIO_REMARKS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID from Reversematerial_issue_to_cost_center_master where RMIO_id in " & RmioIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Reversematerial_issue_to_cost_center_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '23)ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL
        Query = " Select Reverse_ID,Item_ID,Prev_Item_Qty,Item_Qty,Prev_Item_Rate,Item_Rate,Created_By,Creation_Date,Modified_By,Modification_Date," & v_the_current_division_id & " as Division_Id,Prev_Item_vat,Prev_Item_exice,Item_vat,Item_exice,Batch_No,Expiry_Date,Stock_Detail_Id from ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL where Reverse_Id in" & ReverseWithoutIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '24)ReverseMaterial_Recieved_Without_Po_Master
        Query = " Select Reverse_ID,Reverse_Code,Reverse_No,Reverse_Date,Remarks,received_ID,Created_By,Creation_Date,Modified_By,Modification_Date," & v_the_current_division_id & " as Division_ID from ReverseMaterial_Recieved_Without_Po_Master where  Reverse_Id in" & ReverseWithoutIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMaterial_Recieved_Without_Po_Master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '25)REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO
        Query = " Select Reverse_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,Item_Rate,batch_no,batch_date,prev_item_qty,prev_item_Rate,prev_item_vat,prev_item_exice, " & v_the_current_division_id & " as Division_ID from REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where Reverse_Id in " & ReverseWithoutIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '26)ReverseMATERIAL_RECEIVED_Against_PO_DETAIL
        Query = " Select Reverse_ID,Item_ID,Prev_Item_Qty,Item_Qty,Prev_Item_Rate,Item_Rate,Created_By,Creation_Date,Modified_By,Modification_Date," & v_the_current_division_id & " as Division_Id,Prev_Item_vat,Prev_Item_exice,Item_vat,Item_exice,Batch_No,Expiry_Date,Stock_Detail_Id from ReverseMATERIAL_RECEIVED_Against_PO_DETAIL where Reverse_Id in" & ReverseAgainstIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMATERIAL_RECEIVED_Against_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '27)ReverseMaterial_Recieved_Against_Po_Master
        Query = " Select Reverse_ID,Reverse_Code,Reverse_No,Reverse_Date,Remarks,received_ID,Created_By,Creation_Date,Modified_By,Modification_Date," & v_the_current_division_id & " as Division_ID from ReverseMaterial_Recieved_Against_Po_Master where  Reverse_Id in" & ReverseAgainstIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMaterial_Recieved_Against_Po_Master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '28)ReverseWASTAGE_DETAIL
        Query = " Select p_ReverseWastage_ID,f_ReverseWastage_ID,Item_ID,Stock_Detail_Id,Item_Qty,item_Rate,Actual_Qty,Created_By,Creation_Date,Modified_By,Modified_Date," & v_the_current_division_id & " as Division_ID,WastageId from ReverseWASTAGE_DETAIL where f_reverseWastage_id in " & ReverseWastageIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseWASTAGE_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '29)ReverseWASTAGE_MASTER
        Query = " Select ReverseWastage_ID,WastageId,ReverseWastage_Code,ReverseWastage_No,ReverseWastage_Date,ReverseWastage_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date," & v_the_current_division_id & " as Division_ID from ReverseWASTAGE_MASTER where reverseWastage_id in " & ReverseWastageIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseWASTAGE_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '30)supplier_rate_list
        Query = " Select SRL_ID,SRL_NAME,SRL_DATE,SRL_DESC,SUPP_ID,ACTIVE,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID from supplier_rate_list where SRL_id in " & SrlIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "supplier_rate_list"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '31)SUPPLIER_RATE_LIST_DETAIL
        Query = " Select SRL_ID,ITEM_ID,ITEM_RATE,DEL_QTY,DEL_DAYS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " as DIVISION_ID from SUPPLIER_RATE_LIST_DETAIL where SRL_id in " & SrlIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "SUPPLIER_RATE_LIST_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '32)Transaction_Log
        Query = " Select Transaction_ID,Item_ID,STOCK_DETAIL_ID,Transaction_Type,Quantity,Trans_Date,Current_Stock, " & v_the_current_division_id & " as Division_Id from Transaction_Log where convert(varchar,trans_date,101) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Transaction_Log"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '33)WASTAGE_DETAIL
        Query = " Select Wastage_ID,Item_ID,Stock_Detail_Id,Item_Qty,Item_Rate,Balance_Qty,Created_By,Creation_Date,Modified_By,Modified_Date," & v_the_current_division_id & " as Division_ID from WASTAGE_DETAIL where wastage_id in " & WastageIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "WASTAGE_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '34)wastage_master
        Query = " Select Wastage_ID,Wastage_Code,Wastage_No,Wastage_Date,Wastage_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date," & v_the_current_division_id & " as Division_ID from wastage_master where wastage_id in " & WastageIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "wastage_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '35)User Master
        Query = " Select user_id,user_name,password,user_role," & v_the_current_division_id & " as  division_id,CostCenter_id from User_master where division_id = " & v_the_current_division_id
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "User_master_mms"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '36)ADJUSTMENT MASTER
        Query = " SELECT  Adjustment_ID,Adjustment_Code,Adjustment_No,Adjustment_Date,Adjustment_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date," & v_the_current_division_id & " as Division_ID FROM ADJUSTMENT_MASTER where Adjustment_ID in " & AdjustmentIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ADJUSTMENT_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '37)ADJUSTMENT DETAIL
        Query = " SELECT Adjustment_ID,Item_ID,Stock_Detail_Id,Item_Qty,Item_Rate,Balance_Qty,Created_By,Creation_Date,Modified_By,Modified_Date," & v_the_current_division_id & " as Division_ID FROM dbo.ADJUSTMENT_DETAIL where Adjustment_ID in " & AdjustmentIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ADJUSTMENT_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '38)STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER
        Query = " SELECT TRANSFER_ID,TRANSFER_CODE,TRANSFER_NO,TRANSFER_DATE,TRANSFER_CC_ID,TRANSFER_REMARKS,TRANSFER_STATUS,RECEIVED_DATE,Created_By,Creation_Date,Modified_By,Modified_Date,COSTCENTER_ID," & v_the_current_division_id & " as Division_ID FROM dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER where TRANSFER_ID in " & StocktransferccInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '39)STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL
        Query = " SELECT TRANSFER_ID,ITEM_ID,ITEM_QTY,ACCEPTED_QTY,RETURNED_QTY,Created_By,Creation_Date,Modified_By,Modified_Date,COSTCENTER_ID,TRANSFER_RATE," & v_the_current_division_id & " as Division_ID FROM dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL where TRANSFER_ID in " & StocktransferccInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '40)WASTAGE_DETAIL_CC
        Query = " Select Wastage_ID,Item_ID,Item_Qty,Item_Rate,Balance_Qty,Created_By,Creation_Date,Modified_By,Modified_Date,CostCenter_id," & v_the_current_division_id & " as Division_ID from WASTAGE_DETAIL_CC where wastage_id in " & WastageIdsInQuerycc
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "WASTAGE_DETAIL_CC"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '41)wastage_master_cc
        Query = " Select Wastage_ID,Wastage_Code,Wastage_No,Wastage_Date,Wastage_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date,CostCenter_id," & v_the_current_division_id & " as Division_ID from wastage_master_cc where wastage_id in " & WastageIdsInQuerycc
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "wastage_master_cc"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '42)Closing_cc_master
        Query = " Select Closing_id,closing_code,Closing_no,closing_date,closing_remarks,closing_status,Created_By,Creation_Date,Modified_By,Modified_Date,CostCenter_id," & v_the_current_division_id & " as Division_ID from Closing_cc_master where Closing_id in " & Closingidscc
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "CLOSING_CC_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '43)Closing_cc_Detail
        Query = " Select Closing_id,item_id,item_qty,item_rate,Current_stock,Consumption,Created_BY,Creation_Date,Modified_BY,Modified_Date,CostCenter_id," & v_the_current_division_id & " as Division_ID from Closing_cc_detail where closing_id in " & Closingidscc
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Closing_cc_detail"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '44)REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO
        Query = " SELECT Reverse_id, Item_id, Costcenter_id, po_id,Item_qty, Item_vat, Item_exice, Item_rate, Prev_Item_qty, Prev_item_rate, Prev_item_vat, Prev_item_exice, batch_no, batch_date, " & v_the_current_division_id & " AS division_id FROM REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where Reverse_Id in " & ReverseAgainstIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        'New Tables Added

        '45)SALE_INVOICE_DETAIL
        Query = " SELECT  SI_ID,ITEM_ID,ITEM_QTY,PKT,ITEM_RATE,ITEM_AMOUNT,VAT_PER,VAT_AMOUNT,BAL_ITEM_RATE,BAL_ITEM_QTY,
						 CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE," & v_the_current_division_id & " AS DIVISION_ID,TARRIF_ID,ITEM_MRP,ITEM_MRP_DESC,
						 ASSESIBLE_PER,ASSESIBLE_VALUE,EXCISE_PER,EXCISE_AMOUNT,EDU_CESS_PER,EDU_CESS_VALUE,SHE_CESS_PER,	
						 SHE_CESS_VALUE,ITEM_DISCOUNT,DISCOUNT_TYPE,DISCOUNT_VALUE,GSTPaid,CessPercentage_num,CessAmount_num,	
						 MRP, ACess,ACessAmount  FROM SALE_INVOICE_DETAIL sid
                         WHERE sid.DIVISION_ID = " & v_the_current_division_id & " and cast(sid.CREATION_DATE AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "SALE_INVOICE_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '46)SALE_INVOICE_MASTER
        Query = " SELECT SI_ID,SI_CODE,SI_NO,DC_GST_NO,SI_DATE,CUST_ID,REMARKS,PAYMENTS_REMARKS,SALE_TYPE,
					   GROSS_AMOUNT,VAT_AMOUNT,NET_AMOUNT,IS_SAMPLE,SAMPLE_ADDRESS,VAT_CST_PER,INVOICE_STATUS,
					   DELIVERY_NOTE_NO,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,
                       " & v_the_current_division_id & " AS DIVISION_ID, is_InvCancel,
					   VEHICLE_NO,SHIPP_ADD_ID,INV_TYPE,LR_NO,SaleExecutiveId,REQ_ID,DELIVERED_BY,TempInvoiceId,
					   PAY_STATUS,TRANSPORT,REFERENCE_ID,FLAG,CESS_AMOUNT FROM SALE_INVOICE_MASTER sim
                       WHERE sim.DIVISION_ID = " & v_the_current_division_id & " and cast(sim.CREATION_DATE AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "SALE_INVOICE_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '47)SALE_INVOICE_STOCK_DETAIL
        Query = " Select SI_ID,ITEM_ID,STOCK_DETAIL_ID,ITEM_QTY,NOOFCASES,ITEM_PKT_WEIGHT,
                " & v_the_current_division_id & " AS DIVISION_ID FROM SALE_INVOICE_STOCK_DETAIL sisd"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "SALE_INVOICE_STOCK_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '48)PaymentTransaction
        Query = " SELECT PaymentTransactionId,PaymentTransactionNo,PaymentTypeId,AccountId,PaymentDate,ChequeDraftNo ,
						ChequeDraftDate,BankId,BankDate,Remarks,TotalAmountReceived,UndistributedAmount,CancellationCharges ,
						CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,StatusId,PM_TYPE,
                        " & v_the_current_division_id & " AS DIVISION_ID FROM PaymentTransaction pt
                        WHERE cast(pt.PaymentDate AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PaymentTransaction"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '49)SettlementDetail
        Query = " SELECT PaymentTransactionId,InvoiceId,AmountSettled,Remarks,CreatedBy,CreatedDate,PaymentId,
                " & v_the_current_division_id & " AS DivisionId FROM SettlementDetail sd
                WHERE sd.DivisionId = " & v_the_current_division_id & " and cast(sd.CreatedDate AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "SettlementDetail"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '50)DebitNote_DETAIL
        Query = " SELECT DebitNote_Id,Item_ID,Item_Qty,Created_By,Creation_Date,Modified_By,Modification_Date,Stock_Detail_Id,Item_Rate,
				  Item_Tax,Item_Cess," & v_the_current_division_id & " AS Division_Id FROM DebitNote_DETAIL dnd
                  WHERE dnd.Division_Id = " & v_the_current_division_id & " and cast(dnd.Creation_Date As Date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "DebitNote_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '51)DebitNote_Master
        Query = " SELECT DebitNote_Id,DebitNote_Code,DebitNote_No,DebitNote_Date,MRNId,Remarks,Created_by,
					Creation_Date,Modified_By,Modification_Date," & v_the_current_division_id & " AS Division_Id,DN_Amount,DN_CustId,INV_No,INV_Date,
					REFERENCE_ID,DebitNote_Type,RefNo,RefDate_dt,Tax_num,Cess_num
                    FROM DebitNote_Master dnm
                    WHERE dnm.Division_Id = " & v_the_current_division_id & " and cast(dnm.DebitNote_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "DebitNote_Master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '52)CreditNote_DETAIL
        Query = " SELECT CreditNote_Id,Item_ID,Item_Qty,Created_By,Creation_Date,Modified_By,
						Modification_Date, " & v_the_current_division_id & " AS Division_Id,
                        Stock_Detail_Id,Item_Rate,Item_Tax,Item_Cess
                         FROM CreditNote_DETAIL cnd
                        WHERE cnd.Division_Id = " & v_the_current_division_id & " and cast(cnd.Creation_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "CreditNote_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '53)CreditNote_Master
        Query = " SELECT CreditNote_Id,CreditNote_Code,CreditNote_No,CreditNote_Date,INVId,
						Remarks,Created_by,Creation_Date,Modified_By,Modification_Date,
                        " & v_the_current_division_id & " AS Division_Id,
                        CN_Amount,CN_CustId,INV_No,INV_Date,REFERENCE_ID,CreditNote_Type,
						RefNo,RefDate_dt,Tax_Amt,Cess_Amt FROM CreditNote_Master cnm
                        WHERE cnm.Division_Id = " & v_the_current_division_id & " and cast(cnm.CreditNote_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'"
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "CreditNote_Master"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        Bulk_CopyAdmin(TableToTransferDataSet)

    End Sub

    Public Sub Bulk_CopyAdmin(ByVal _Table As DataSet)
        Dim Table1 As DataTable = Nothing


        GetGlobalRecordIds()
        con = New SqlConnection(gblDNS_Online)
        If con.State = ConnectionState.Open Then con.Close()
        con.Open()
        Dim tran As SqlTransaction
        tran = con.BeginTransaction
        Try

            Dim cmd As SqlCommand
            cmd = New SqlCommand("delete from CLOSING_STOCK_AVG_RATE where division_id=" & v_the_current_division_id & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ITEM_DETAIL where div_id=" & v_the_current_division_id & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from Recipe_master where division_id=" & v_the_current_division_id & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from STOCK_DETAIL where division_id=" & v_the_current_division_id & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from INDENT_DETAIL where division_id=" & v_the_current_division_id & " and Indent_Id in " & indentIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from INDENT_MASTER where division_id=" & v_the_current_division_id & " and Indent_Id in " & indentIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from MATERIAL_ISSUE_TO_COST_CENTER_DETAIL where division_id=" & v_the_current_division_id & " and MIO_ID in " & MIOIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from MATERIAL_ISSUE_TO_COST_CENTER_Master where division_id=" & v_the_current_division_id & " and MIO_ID in " & MIOIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from MATERIAL_RECEIVED_AGAINST_PO_DETAIL where division_id=" & v_the_current_division_id & " and Receipt_Id in " & ReceiptIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from MATERIAL_RECEIVED_AGAINST_PO_MASTER where division_id=" & v_the_current_division_id & " and  Receipt_Id in " & ReceiptIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where div_id=" & v_the_current_division_id & " and  Received_Id in " & ReceiptIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from MATERIAL_RECEIVED_WITHOUT_PO_DETAIL where division_id=" & v_the_current_division_id & " and  Received_Id in " & ReceivedIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from MATERIAL_RECIEVED_WITHOUT_PO_MASTER where division_id=" & v_the_current_division_id & " and  Received_Id in " & ReceivedIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            cmd = New SqlCommand("delete from NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where division_id=" & v_the_current_division_id & " and  Received_Id in " & ReceivedIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            '''''''''''''''''''''''''''''''''''''''


            cmd = New SqlCommand("delete from MRS_MAIN_STORE_Detail where division_id=" & v_the_current_division_id & " and  MRS_Id in " & MRSIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            cmd = New SqlCommand("delete from MRS_MAIN_STORE_MASTER where division_id=" & v_the_current_division_id & " and  MRS_Id in " & MRSIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''
            cmd = New SqlCommand("delete from OPEN_PO_Detail where division_id=" & v_the_current_division_id & " and  PO_Id in " & OpenPoIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            cmd = New SqlCommand("delete from OPEN_PO_MASTER where division_id=" & v_the_current_division_id & " and  PO_Id in " & OpenPoIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            '''''''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from PO_DETAIL where division_id=" & v_the_current_division_id & " and  PO_Id in " & PoIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            cmd = New SqlCommand("delete from PO_Master where division_id=" & v_the_current_division_id & " and  PO_Id in " & PoIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from PO_STATUS where division_id=" & v_the_current_division_id & " and  PO_Id in " & PoIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            '''''''''''''''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from ReverseMaterial_Issue_To_Cost_Center_Detail where division_id=" & v_the_current_division_id & " and  RMIO_Id in " & RmioIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ReverseMaterial_Issue_To_Cost_Center_Master where division_id=" & v_the_current_division_id & " and  RMIO_Id in " & RmioIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from ReverseMATERIAL_RECEIVED_Against_PO_DETAIL where division_id=" & v_the_current_division_id & " and  Reverse_Id in " & ReverseAgainstIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ReverseMATERIAL_RECIEVED_Against_PO_MASTER where division_id=" & v_the_current_division_id & " and  Reverse_Id in " & ReverseAgainstIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''''''


            cmd = New SqlCommand("delete from ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL where division_id=" & v_the_current_division_id & " and  Reverse_Id in " & ReverseWithoutIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()


            cmd = New SqlCommand("delete from REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where division_id=" & v_the_current_division_id & " and  Reverse_Id in " & ReverseWithoutIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where division_id=" & v_the_current_division_id & " and  Reverse_Id in " & ReverseAgainstIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER where division_id=" & v_the_current_division_id & " and  Reverse_Id in " & ReverseWithoutIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from ReverseWASTAGE_Detail where division_id=" & v_the_current_division_id & " and  F_ReverseWastage_Id in " & ReverseWastageIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ReverseWASTAGE_MASTER where division_id=" & v_the_current_division_id & " and  ReverseWastage_Id in " & ReverseWastageIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from SUPPLIER_RATE_LIST where division_id=" & v_the_current_division_id & " and  SRL_Id in " & SrlIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from SUPPLIER_RATE_LIST_DETAIL where division_id=" & v_the_current_division_id & " and  SRL_Id in " & SrlIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from Transaction_Log where division_id=" & v_the_current_division_id & " and  convert(varchar,trans_Date,101) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            ''''''''''''''''''''''''''''''''''

            cmd = New SqlCommand("delete from WASTAGE_Detail where division_id=" & v_the_current_division_id & " and Wastage_Id in " & WastageIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from WASTAGE_MASTER where division_id=" & v_the_current_division_id & " and Wastage_Id in " & WastageIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from user_master_mms where division_id=" & v_the_current_division_id)
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ADJUSTMENT_MASTER where division_id=" & v_the_current_division_id & " and Adjustment_ID in " & AdjustmentIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from ADJUSTMENT_Detail where division_id=" & v_the_current_division_id & " and Adjustment_ID in " & AdjustmentIdsInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER where DIVISION_ID=" & v_the_current_division_id & " and TRANSFER_ID in " & StocktransferccInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL where DIVISION_ID=" & v_the_current_division_id & " and TRANSFER_ID in " & StocktransferccInQuery & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from WASTAGE_Detail_cc where division_id=" & v_the_current_division_id & " and Wastage_Id in " & WastageIdsInQuerycc & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from WASTAGE_MASTER_CC where division_id=" & v_the_current_division_id & " and Wastage_Id in " & WastageIdsInQuerycc & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from Closing_cc_master where division_id=" & v_the_current_division_id & " and Closing_Id in " & Closingidscc & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from Closing_cc_detail where division_id=" & v_the_current_division_id & " and Closing_Id in " & Closingidscc & " ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            '--------------- Newly added tables -----------------

            cmd = New SqlCommand("delete from SALE_INVOICE_DETAIL where division_id=" & v_the_current_division_id & " and cast(CREATION_DATE AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from SALE_INVOICE_MASTER where division_id=" & v_the_current_division_id & " and cast(CREATION_DATE AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from SALE_INVOICE_STOCK_DETAIL ")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from PaymentTransaction where cast(PaymentDate AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from SettlementDetail where divisionid=" & v_the_current_division_id & " and cast(CreatedDate AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from DebitNote_DETAIL where division_id=" & v_the_current_division_id & " and cast(Creation_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from DebitNote_Master where division_id=" & v_the_current_division_id & " and cast(DebitNote_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from CreditNote_DETAIL where division_id=" & v_the_current_division_id & " and cast(Creation_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            cmd = New SqlCommand("delete from CreditNote_Master where division_id=" & v_the_current_division_id & " and cast(CreditNote_Date AS date) between '" & dtpfrmdate.Value.Date.ToString("MM/dd/yyyy") & "' and '" & dtptodate.Value.Date.ToString("MM/dd/yyyy") & "'")
            cmd.Transaction = tran
            cmd.Connection = con
            cmd.ExecuteNonQuery()

            Dim BulkCopy As New SqlBulkCopy(con, SqlBulkCopyOptions.Default, tran)

            pbardatatransfer.Value = 0
            pbardatatransfer.Minimum = 0
            pbardatatransfer.Maximum = _Table.Tables.Count
            If (lstTablesName.Items.Count > 0) Then
                lstTablesName.Items.Clear()
            End If
            For Each Table1 In _Table.Tables
                BulkCopy.DestinationTableName = Table1.TableName
                BulkCopy.WriteToServer(Table1)
                lstTablesName.Items.Add(Table1.TableName)
                pbardatatransfer.Value += 1
                System.Windows.Forms.Application.DoEvents()
            Next
            If Not IsNothing(pbardatatransfer) Then
                pbardatatransfer.Value = pbardatatransfer.Maximum
            End If

            BulkCopy.Close()

            tran.Commit()
            con.Close()

            Dim res As Int32 = MsgBox("Data Transfer To Admin Successfully Completed", MsgBoxStyle.OkOnly, "Data Transfer")
            If (res = 1) Then
                con = New SqlConnection(DNS)
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()

                Dim congbl As SqlConnection = New SqlConnection(gblDNS_Online)
                If congbl.State = ConnectionState.Open Then congbl.Close()
                congbl.Open()

                Dim frmdate As DateTime = dtpfrmdate.Value.Date
                While frmdate <= dtptodate.Value.Date
                    Query = "select isnull(max(DataTransferId_num),0)+1 from DataTransfer"
                    Dim TransferId As Int32 = obj.ExecuteScalar(Query)
                    cmd = New SqlCommand("insert into DataTransfer(DataTransferId_num,DataTransferDate_dt,IsCompleted_bit,fk_CreatedBy_num,CreatedDate_dt,fk_ModifiedBy_num,ModifiedDate_dt) values (" & TransferId & ",'" & frmdate.ToString() & "',1,1,GETDATE(),1,GETDATE())")
                    'cmd.Transaction = tran
                    cmd.Connection = con
                    cmd.ExecuteNonQuery()

                    cmd = New SqlCommand("insert into DataTransfer(DataTransferId_num,DataTransferDate_dt,IsCompleted_bit,fk_CreatedBy_num,CreatedDate_dt,fk_ModifiedBy_num,ModifiedDate_dt,division_id) values (" & TransferId & ",'" & frmdate.ToString() & "',1,1,GETDATE(),1,GETDATE()," & v_the_current_division_id & ")")
                    cmd.Connection = congbl
                    cmd.ExecuteNonQuery()
                    frmdate = frmdate.AddDays(1)
                End While


                'Query = "select isnull(max(DataTransferId_num),0)+1 from DataTransfer"
                'Dim TransferId As Int32 = obj.ExecuteScalar(Query)
                'cmd = New SqlCommand("insert into DataTransfer(DataTransferId_num,DataTransferDate_dt,IsCompleted_bit,fk_CreatedBy_num,CreatedDate_dt,fk_ModifiedBy_num,ModifiedDate_dt) values (" & TransferId & ",'" & dtptodate.Value.Date.ToString() & "',1,1,GETDATE(),1,GETDATE())")
                ''cmd.Transaction = tran
                'cmd.Connection = con
                'cmd.ExecuteNonQuery()
            End If
            'Me.Close()

            ''********************************************************************''

            ''********************************************************************''  


        Catch ex As Exception
            tran.Rollback()
            MsgBox(ex.Message & vbCrLf & Table1.TableName)
        End Try
    End Sub

    'Private Sub btnQuickDataTransfer_Click(sender As Object, e As EventArgs) Handles btnQuickDataTransfer.Click
    '    Query = " Select isnull(CONVERT(VARCHAR,max(datatransferDate_dt),101),'01/01/1900') as Lasttransfer from Datatransfer"

    '    pbardatatransfer.Value = 0
    '    pbardatatransfer.Minimum = 0

    '    Dim LastDate As Date = Convert.ToString(obj.ExecuteScalar(Query))
    '    If (LastDate = "01/01/1900") Then
    '        Try
    '            'Dim tran1 As SqlTransaction
    '            con = New SqlConnection(DNS)
    '            If con.State = ConnectionState.Open Then con.Close()
    '            con.Open()

    '            pbardatatransfer.Value += 1
    '            System.Windows.Forms.Application.DoEvents()

    '            'tran1 = con.BeginTransaction()
    '            cmd = New SqlCommand
    '            cmd.CommandTimeout = 0
    '            cmd.Connection = con
    '            'cmd.Transaction = tran1
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.CommandText = "proc_TransferMMSData"
    '            cmd.Parameters.AddWithValue("@DivisionId", v_the_current_division_id)
    '            'cmd.Parameters.AddWithValue("@destDB", gblCentraliseServer_Name + "].[" + gblCentraliseDataBase_Name)
    '            cmd.Parameters.AddWithValue("@destDB", gblCentraliseDataBase_Name)
    '            cmd.Parameters.AddWithValue("@sourceDB", gblDataBase_Name)
    '            cmd.Parameters.AddWithValue("@fromdate", dtpfrmdate.Value.ToString("yyyy-MM-dd"))
    '            cmd.Parameters.AddWithValue("@todate", dtptodate.Value.ToString("yyyy-MM-dd"))
    '            cmd.ExecuteNonQuery()
    '            cmd.Dispose()
    '            'tran1.Commit()
    '            con.Close()

    '            If Not IsNothing(pbardatatransfer) Then
    '                pbardatatransfer.Value = 44
    '            End If

    '            Dim res As Int32 = MsgBox("Data Transfer Successfully Completed", MsgBoxStyle.OkOnly, "Data Transfer")
    '            If (res = 1) Then
    '                con = New SqlConnection(DNS)
    '                If con.State = ConnectionState.Open Then con.Close()
    '                con.Open()

    '                Dim congbl As SqlConnection = New SqlConnection(gblDNS_Online)
    '                If congbl.State = ConnectionState.Open Then congbl.Close()
    '                congbl.Open()

    '                Dim frmdate As DateTime = dtpfrmdate.Value.Date
    '                While frmdate <= dtptodate.Value.Date
    '                    Query = "select isnull(max(DataTransferId_num),0)+1 from DataTransfer"
    '                    Dim TransferId As Int32 = obj.ExecuteScalar(Query)
    '                    cmd = New SqlCommand("insert into DataTransfer(DataTransferId_num,DataTransferDate_dt,IsCompleted_bit,fk_CreatedBy_num,CreatedDate_dt,fk_ModifiedBy_num,ModifiedDate_dt) values (" & TransferId & ",'" & frmdate.ToString() & "',1,1,GETDATE(),1,GETDATE())")
    '                    'cmd.Transaction = tran
    '                    cmd.Connection = con
    '                    cmd.ExecuteNonQuery()

    '                    cmd = New SqlCommand("insert into DataTransfer(DataTransferId_num,DataTransferDate_dt,IsCompleted_bit,fk_CreatedBy_num,CreatedDate_dt,fk_ModifiedBy_num,ModifiedDate_dt,division_id) values (" & TransferId & ",'" & frmdate.ToString() & "',1,1,GETDATE(),1,GETDATE()," & v_the_current_division_id & ")")
    '                    cmd.Connection = congbl
    '                    cmd.ExecuteNonQuery()
    '                    frmdate = frmdate.AddDays(1)
    '                End While
    '            End If

    '        Catch ex As Exception
    '            'obj.MyCon_RollBackTransaction(cmd)
    '            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
    '        End Try

    '    ElseIf (Convert.ToInt32(dtpfrmdate.Value.Date.Subtract(LastDate).TotalDays) > 1) Then
    '        MsgBox("Please Transfer Yesterday's Data", MsgBoxStyle.OkOnly, "Alert?")
    '        Exit Sub
    '    Else
    '        Try
    '            'Dim tran2 As SqlTransaction
    '            con = New SqlConnection(DNS)
    '            If con.State = ConnectionState.Open Then con.Close()
    '            con.Open()

    '            'tran2 = con.BeginTransaction()

    '            cmd = New SqlCommand
    '            cmd.CommandTimeout = 0
    '            cmd.Connection = con
    '            'cmd.Transaction = tran2
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.CommandText = "proc_TransferMMSData"
    '            cmd.Parameters.AddWithValue("@DivisionId", v_the_current_division_id)
    '            'cmd.Parameters.AddWithValue("@destDB", gblCentraliseServer_Name + "].[" + gblCentraliseDataBase_Name)
    '            cmd.Parameters.AddWithValue("@destDB", gblCentraliseDataBase_Name)
    '            cmd.Parameters.AddWithValue("@sourceDB", gblDataBase_Name)
    '            cmd.Parameters.AddWithValue("@fromdate", dtpfrmdate.Value.ToString("yyyy-MM-dd"))
    '            cmd.Parameters.AddWithValue("@todate", dtptodate.Value.ToString("yyyy-MM-dd"))
    '            cmd.ExecuteNonQuery()
    '            cmd.Dispose()

    '            'tran2.Commit()
    '            con.Close()

    '            If Not IsNothing(pbardatatransfer) Then
    '                pbardatatransfer.Value = 100
    '            End If

    '            Dim res As Int32 = MsgBox("Data Transfer Successfully Completed", MsgBoxStyle.OkOnly, "Data Transfer")
    '            If (res = 1) Then
    '                con = New SqlConnection(DNS)
    '                If con.State = ConnectionState.Open Then con.Close()
    '                con.Open()

    '                Dim congbl As SqlConnection = New SqlConnection(gblDNS_Online)
    '                If congbl.State = ConnectionState.Open Then congbl.Close()
    '                congbl.Open()

    '                Dim frmdate As DateTime = dtpfrmdate.Value.Date
    '                While frmdate <= dtptodate.Value.Date
    '                    Query = "select isnull(max(DataTransferId_num),0)+1 from DataTransfer"
    '                    Dim TransferId As Int32 = obj.ExecuteScalar(Query)
    '                    cmd = New SqlCommand("insert into DataTransfer(DataTransferId_num,DataTransferDate_dt,IsCompleted_bit,fk_CreatedBy_num,CreatedDate_dt,fk_ModifiedBy_num,ModifiedDate_dt) values (" & TransferId & ",'" & frmdate.ToString() & "',1,1,GETDATE(),1,GETDATE())")
    '                    'cmd.Transaction = tran
    '                    cmd.Connection = con
    '                    cmd.ExecuteNonQuery()

    '                    cmd = New SqlCommand("insert into DataTransfer(DataTransferId_num,DataTransferDate_dt,IsCompleted_bit,fk_CreatedBy_num,CreatedDate_dt,fk_ModifiedBy_num,ModifiedDate_dt,division_id) values (" & TransferId & ",'" & frmdate.ToString() & "',1,1,GETDATE(),1,GETDATE()," & v_the_current_division_id & ")")
    '                    cmd.Connection = congbl
    '                    cmd.ExecuteNonQuery()
    '                    frmdate = frmdate.AddDays(1)
    '                End While
    '            End If

    '        Catch ex As Exception
    '            'obj.MyCon_RollBackTransaction(cmd)
    '            MsgBox(gblMessageHeading_Error & vbCrLf & gblMessage_ContactInfo & vbCrLf & ex.Message, MsgBoxStyle.Critical, gblMessageHeading)
    '        End Try

    '    End If
    'End Sub

End Class