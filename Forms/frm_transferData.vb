Imports System.Data.SqlClient
Public Class frm_transferData
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
    Dim DataTransferIds As String

    Dim _rights As Form_Rights

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub btnDataTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataTransfer.Click
        Query = " Select isnull(CONVERT(VARCHAR,max(datatransferDate_dt),101),'01/01/1900') as Lasttransfer from Datatransfer"
        Dim LastDate As Date = Convert.ToString(obj.ExecuteScalar(Query))
        If (LastDate = "01/01/1900") Then
            FillTables()
        ElseIf (Convert.ToInt32(dtpDate.Value.Date.Subtract(LastDate).TotalDays) > 1) Then
            MsgBox("Please Transfer Yesterday's Data", MsgBoxStyle.OkOnly, "Alert?")
            Exit Sub
        Else
            FillTables()
        End If
    End Sub

    Private Sub GetLocalRecordIds()
        ''set variables
        ''Indent_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(indent_id as varchar) from INDENT_MASTER" & _
            " where convert(varchar,indent_date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & _
            "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        indentIdsInQuery = obj.ExecuteScalar(Query)


        ''MIO _ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(MIO_ID as varchar) from MATERIAL_ISSUE_TO_COST_CENTER_MASTER" & _
            " where  Division_id = " & v_the_current_division_id & " " & _
            " and convert(varchar,mio_date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & _
            "'; select '(' + @ret + ')';"
        MIOIdsInQuery = obj.ExecuteScalar(Query)

        ''Receipt_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(receipt_Id as varchar) from MATERIAL_RECEIVED_AGAINST_PO_MASTER" & _
            " where Division_id = " & v_the_current_division_id & "" & _
            " and convert(varchar,receipt_date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & _
            "'; select '(' + @ret + ')';"
        ReceiptIdsInQuery = obj.ExecuteScalar(Query)

        ''received_id
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(received_id as varchar) from MATERIAL_RECIEVED_WITHOUT_PO_MASTER" & _
            " where Division_id = " & v_the_current_division_id & "" & _
            " and convert(varchar,received_date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & _
            "'; select '(' + @ret + ')';"
        ReceivedIdsInQuery = obj.ExecuteScalar(Query)

        ''MRS_DATE
        Query = " declare @ret varchar(max);set @ret='0';" & _
           " select @ret= @ret + ',' +  cast(MRS_ID as varchar) from MRS_MAIN_STORE_MASTER" & _
           " where convert(varchar,MRS_DATE,101) = '" & _
           dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
           " select '(' + @ret + ')';"
        MRSIdsInQuery = obj.ExecuteScalar(Query)

        ''OPEN---PO_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(PO_ID as varchar) from OPEN_PO_MASTER" & _
            " where convert(varchar,PO_DATE,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        OpenPoIdsInQuery = obj.ExecuteScalar(Query)

        ''PO_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(PO_ID as varchar) from PO_MASTER" & _
            " where convert(varchar,PO_DATE,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        PoIdsInQuery = obj.ExecuteScalar(Query)

        ''RMIO_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(RMIO_ID as varchar) from ReverseMaterial_Issue_To_Cost_Center_Master" & _
            " where convert(varchar,RMIO_DATE,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        RmioIdsInQuery = obj.ExecuteScalar(Query)

        ''ReverseMATERIAL_RECIEVED_Against_PO_MASTER------Reverse_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(Reverse_ID as varchar) from ReverseMATERIAL_RECIEVED_Against_PO_MASTER" & _
            " where convert(varchar,Reverse_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        ReverseAgainstIdsInQuery = obj.ExecuteScalar(Query)

        ''ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER------Reverse_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(Reverse_ID as varchar) from ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER" & _
            " where convert(varchar,Reverse_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        ReverseWithoutIdsInQuery = obj.ExecuteScalar(Query)

        ''ReverseWastage_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(ReverseWastage_ID as varchar) from ReverseWASTAGE_MASTER" & _
            " where convert(varchar,ReverseWastage_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        ReverseWastageIdsInQuery = obj.ExecuteScalar(Query)


        ''SRL_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(SRL_ID as varchar) from SUPPLIER_RATE_LIST" & _
            " where convert(varchar,SRL_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        SrlIdsInQuery = obj.ExecuteScalar(Query)

        ''Wastage_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(Wastage_ID as varchar) from WASTAGE_MASTER" & _
            " where convert(varchar,Wastage_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        WastageIdsInQuery = obj.ExecuteScalar(Query)

        ''AdjustmentIds
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(Adjustment_ID as varchar) from ADJUSTMENT_MASTER" & _
            " where convert(varchar,Adjustment_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        AdjustmentIdsInQuery = obj.ExecuteScalar(Query)

        ''Stock_transfer_cc
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(TRANSFER_ID as varchar) from STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER" & _
            " where convert(varchar,TRANSFER_DATE,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        StocktransferccInQuery = obj.ExecuteScalar(Query)

        ''Wastage_cc
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(Wastage_id as varchar) from WASTAGE_MASTER_CC" & _
            " where convert(varchar,WASTAGE_DATE,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        WastageIdsInQuerycc = obj.ExecuteScalar(Query)

        ''Closing_cc_master
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(Closing_id as varchar) from CLOSING_CC_MASTER" & _
            " where convert(varchar,CLOSING_DATE,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        Closingidscc = obj.ExecuteScalar(Query)
    End Sub

    Private Sub GetGlobalRecordIds()
        con = New SqlConnection(gblDNS_Online)
        ''set variables
        ''Indent_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(indent_id as varchar) from INDENT_MASTER" & _
            " where convert(varchar,indent_date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & _
            "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        indentIdsInQuery = obj.ExecuteScalar(Query, con)

        ''MIO _ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(MIO_ID as varchar) from MATERIAL_ISSUE_TO_COST_CENTER_MASTER" & _
            " where  Division_id = " & v_the_current_division_id & " " & _
            " and convert(varchar,mio_date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & _
            "'; select '(' + @ret + ')';"
        MIOIdsInQuery = obj.ExecuteScalar(Query, con)

        ''Receipt_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(receipt_Id as varchar) from MATERIAL_RECEIVED_AGAINST_PO_MASTER" & _
            " where Division_id = " & v_the_current_division_id & "" & _
            " and convert(varchar,receipt_date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & _
            "'; select '(' + @ret + ')';"
        ReceiptIdsInQuery = obj.ExecuteScalar(Query, con)

        ''received_id
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(received_id as varchar) from MATERIAL_RECIEVED_WITHOUT_PO_MASTER" & _
            " where Division_id = " & v_the_current_division_id & "" & _
            " and convert(varchar,received_date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & _
            "'; select '(' + @ret + ')';"
        ReceivedIdsInQuery = obj.ExecuteScalar(Query, con)

        ''MRS_DATE
        Query = " declare @ret varchar(max);set @ret='0';" & _
           " select @ret= @ret + ',' +  cast(MRS_ID as varchar) from MRS_MAIN_STORE_MASTER" & _
           " where convert(varchar,MRS_DATE,101) = '" & _
           dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
           " select '(' + @ret + ')';"
        MRSIdsInQuery = obj.ExecuteScalar(Query, con)

        ''OPEN---PO_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(PO_ID as varchar) from OPEN_PO_MASTER" & _
            " where convert(varchar,PO_DATE,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        OpenPoIdsInQuery = obj.ExecuteScalar(Query, con)

        ''PO_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(PO_ID as varchar) from PO_MASTER" & _
            " where convert(varchar,PO_DATE,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        PoIdsInQuery = obj.ExecuteScalar(Query, con)

        ''RMIO_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(RMIO_ID as varchar) from ReverseMaterial_Issue_To_Cost_Center_Master" & _
            " where convert(varchar,RMIO_DATE,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        RmioIdsInQuery = obj.ExecuteScalar(Query, con)

        ''ReverseMATERIAL_RECIEVED_Against_PO_MASTER------Reverse_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(Reverse_ID as varchar) from ReverseMATERIAL_RECIEVED_Against_PO_MASTER" & _
            " where convert(varchar,Reverse_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        ReverseAgainstIdsInQuery = obj.ExecuteScalar(Query, con)

        ''ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER------Reverse_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(Reverse_ID as varchar) from ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER" & _
            " where convert(varchar,Reverse_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        ReverseWithoutIdsInQuery = obj.ExecuteScalar(Query, con)

        ''ReverseWastage_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(ReverseWastage_ID as varchar) from ReverseWASTAGE_MASTER" & _
            " where convert(varchar,ReverseWastage_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        ReverseWastageIdsInQuery = obj.ExecuteScalar(Query, con)


        ''SRL_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(SRL_ID as varchar) from SUPPLIER_RATE_LIST" & _
            " where convert(varchar,SRL_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        SrlIdsInQuery = obj.ExecuteScalar(Query, con)

        ''Wastage_ID
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(Wastage_ID as varchar) from WASTAGE_MASTER" & _
            " where convert(varchar,wastage_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        WastageIdsInQuery = obj.ExecuteScalar(Query, con)

        ''AdjustmentIds
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(Adjustment_ID as varchar) from ADJUSTMENT_MASTER" & _
            " where convert(varchar,Adjustment_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        AdjustmentIdsInQuery = obj.ExecuteScalar(Query)

        ''Stock_transfer_cc
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(TRANSFER_ID as varchar) from STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER" & _
            " where convert(varchar,TRANSFER_DATE,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        StocktransferccInQuery = obj.ExecuteScalar(Query)

        ''Wastage_cc
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(Wastage_ID as varchar) from WASTAGE_MASTER_CC" & _
            " where convert(varchar,wastage_Date,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        WastageIdsInQuerycc = obj.ExecuteScalar(Query, con)

        ''Closing_cc_master
        Query = " declare @ret varchar(max);set @ret='0';" & _
            " select @ret= @ret + ',' +  cast(Closing_id as varchar) from CLOSING_CC_MASTER" & _
            " where convert(varchar,CLOSING_DATE,101) = '" & _
            dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' and Division_id = " & v_the_current_division_id & ";" & _
            " select '(' + @ret + ')';"
        Closingidscc = obj.ExecuteScalar(Query)
    End Sub

    Private Sub FillTables()

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
        Query = " Select ITEM_ID,DIV_ID,RE_ORDER_LEVEL,RE_ORDER_QTY,PURCHASE_VAT_ID,SALE_VAT_ID,OPENING_STOCK,CURRENT_STOCK,IS_EXTERNAL,TRANSFER_RATE,AVERAGE_RATE,OPENING_RATE,IS_ACTIVE,IS_STOCKABLE,STOCK_DETAIL_ID from item_detail "
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "item_detail"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '3)STOCK_DETAIL
        Query = " Select STOCK_DETAIL_ID,Item_id,Batch_no,Expiry_date,Item_Qty,Issue_Qty,Balance_Qty,DOC_ID,Transaction_ID, " & v_the_current_division_id & " as Division_Id from STOCK_DETAIL "
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "STOCK_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '4)indent_master
        Query = " Select INDENT_ID,INDENT_CODE,INDENT_NO,INDENT_DATE,REQUIRED_DATE,INDENT_REMARKS,INDENT_STATUS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from indent_master where Indent_Id in " & indentIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "indent_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '5)indent_detail
        Query = " Select INDENT_ID,ITEM_ID,ITEM_QTY_REQ,ITEM_QTY_PO,ITEM_QTY_BAL,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from INDENT_DETAIL where Indent_Id in " & indentIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "INDENT_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())



        '6)MATERIAL_ISSUE_TO_COST_CENTER_DETAIL
        Query = " Select MIO_ID,MRS_ID,ITEM_ID,REQ_QTY,ISSUED_QTY,ACCEPTED_QTY,RETURNED_QTY,BalIssued_Qty,IS_WASTAGE,ITEM_RATE,STOCK_DETAIL_ID, " & v_the_current_division_id & " as Division_ID from MATERIAL_ISSUE_TO_COST_CENTER_DETAIL where MIO_ID in" & MIOIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MATERIAL_ISSUE_TO_COST_CENTER_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '7)material_issue_to_cost_center_master
        Query = " Select MIO_ID,MIO_CODE,MIO_NO,MIO_DATE,CS_ID,MIO_REMARKS,MIO_ACCEPT_DATE,MIO_STATUS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from material_issue_to_cost_center_master where MIO_ID in" & MIOIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "material_issue_to_cost_center_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '8)MATERIAL_RECEIVED_AGAINST_PO_DETAIL
        Query = " Select Receipt_ID,Item_ID,PO_ID,Item_Qty,Item_Rate,Bal_Item_Qty,Stock_Detail_iD,Vat_Per,Bal_Vat_Per,Exice_per,Bal_Exice_Per, " & v_the_current_division_id & " as Division_ID from MATERIAL_RECEIVED_AGAINST_PO_DETAIL where Receipt_Id in " & ReceiptIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MATERIAL_RECEIVED_AGAINST_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '9)MATERIAL_RECEIVED_AGAINST_PO_MASTER
        Query = " Select Receipt_ID,Receipt_No,Receipt_Code,PO_ID,Receipt_Date,Remarks,MRN_NO,MRN_PREFIX,Created_BY,Creation_Date,Modified_By,Modification_Date,Division_ID,MRN_STATUS,MRNCompanies_ID,Invoice_no,Invoice_date,freight,Other_Charges,Discount_Amt from MATERIAL_RECEIVED_AGAINST_PO_MASTER where Receipt_Id in " & ReceiptIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MATERIAL_RECEIVED_AGAINST_PO_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '10)NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO
        Query = " Select Received_ID,PO_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,batch_no,batch_date,Item_Rate,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice,Div_ID from NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where Received_Id in " & ReceiptIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '11)MATERIAL_RECEIVED_WITHOUT_PO_DETAIL
        Query = " Select Received_ID,Item_ID,Item_Qty,Item_Rate,Created_By,Creation_Date,Modified_By,Modification_Date,Division_Id,Item_vat,Item_exice,Batch_No,Expiry_Date,Stock_Detail_Id,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice from MATERIAL_RECEIVED_WITHOUT_PO_DETAIL where Received_id in " & ReceivedIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MATERIAL_RECEIVED_WITHOUT_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '12)material_recieved_without_po_master
        Query = " Select Received_ID,Received_Code,Received_No,Received_Date,Purchase_Type,Vendor_ID,Remarks,Po_ID,MRN_PREFIX,MRN_NO,Created_By,Creation_Date,Modified_By,Modification_Date,Invoice_No,Invoice_Date,Division_ID,mrn_status,freight,freight_type,MRNCompanies_ID,Other_Charges,Discount_Amt from material_recieved_without_po_master where Received_id in " & ReceivedIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "material_recieved_without_po_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '13)NON_STOCKABLE_ITEMS_MAT_REC_WO_PO
        Query = " Select Received_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,batch_no,batch_date,Item_Rate,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice, " & v_the_current_division_id & " as Division_Id from NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where Received_id in " & ReceivedIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "NON_STOCKABLE_ITEMS_MAT_REC_WO_PO"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '14)MRS_MAIN_STORE_DETAIL
        Query = " Select MRS_ID,ITEM_ID,ITEM_QTY,Issue_QTY,Bal_QTY,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from MRS_MAIN_STORE_DETAIL where MRS_ID in " & MRSIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "MRS_MAIN_STORE_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '15)mrs_main_store_master
        Query = " Select MRS_ID,MRS_CODE,MRS_NO,MRS_DATE,CC_ID,REQ_DATE,MRS_REMARKS,MRS_STATUS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID,APPROVE_DATETIME from mrs_main_store_master where MRS_ID in " & MRSIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "mrs_main_store_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '16)OPEN_PO_DETAIL
        Query = " Select PO_ID,ITEM_NAME,UOM,ITEM_QTY,VAT_PER,EXICE_PER,ITEM_RATE,AMOUNT,TOTAL_AMOUNT,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from OPEN_PO_DETAIL where PO_ID in " & OpenPoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "OPEN_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '17)open_po_master
        Query = " Select PO_ID,PO_CODE,PO_NO,PO_TYPE,PO_DATE,PO_START_DATE,PO_END_DATE,PO_REMARKS,PO_SUPP_ID,PO_QUALITY_ID,PO_DELIVERY_ID,PO_STATUS,PATMENT_TERMS,TRANSPORT_MODE,TOTAL_AMOUNT,VAT_AMOUNT,NET_AMOUNT,EXICE_AMOUNT,OCTROI,PRICE_BASIS,FRIEGHT,OTHER_CHARGES,CESS_PER,ALREADY_RECVD,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from open_po_master where PO_ID in " & OpenPoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "open_po_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '18)PO_DETAIL
        Query = " Select PO_ID,ITEM_ID,ITEM_QTY,Balance_Qty,VAT_PER,EXICE_PER,ITEM_RATE,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from PO_DETAIL where PO_ID in " & PoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '19)PO_MASTER
        Query = " Select PO_ID,PO_CODE,PO_NO,PO_DATE,PO_START_DATE,PO_END_DATE,PO_REMARKS,PO_SUPP_ID,PO_QUALITY_ID,PO_DELIVERY_ID,PO_STATUS,PATMENT_TERMS,TRANSPORT_MODE,TOTAL_AMOUNT,VAT_AMOUNT,NET_AMOUNT,EXICE_AMOUNT,PO_TYPE,OCTROI,PRICE_BASIS,FRIEGHT,OTHER_CHARGES,CESS_PER,ALREADY_RECVD,DISCOUNT_AMOUNT,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID,OPEN_PO_QTY from PO_MASTER where PO_ID in " & PoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PO_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '20)PO_STATUS
        Query = " Select POS_ID,PO_ID,ITEM_ID,INDENT_ID,REQUIRED_QTY,RECIEVED_QTY,BALANCE_QTY,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from PO_STATUS where PO_ID in " & PoIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "PO_STATUS"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '21)ReverseMaterial_Issue_To_Cost_Center_Detail
        Query = " Select RMIO_ID,Stock_Detail_Id,ITEM_ID,Item_QTY,Avg_RATE, " & v_the_current_division_id & " as Division_ID from ReverseMaterial_Issue_To_Cost_Center_Detail where RMIO_id in " & RmioIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMaterial_Issue_To_Cost_Center_Detail"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '22)Reversematerial_issue_to_cost_center_master
        Query = " Select RMIO_ID,RMIO_CODE,RMIO_NO,RMIO_DATE,Issue_Id,RMIO_REMARKS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from Reversematerial_issue_to_cost_center_master where RMIO_id in " & RmioIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Reversematerial_issue_to_cost_center_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())



        '23)ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL
        Query = " Select Reverse_ID,Item_ID,Prev_Item_Qty,Item_Qty,Prev_Item_Rate,Item_Rate,Created_By,Creation_Date,Modified_By,Modification_Date,Division_Id,Prev_Item_vat,Prev_Item_exice,Item_vat,Item_exice,Batch_No,Expiry_Date,Stock_Detail_Id from ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL where Reverse_Id in" & ReverseWithoutIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '24)ReverseMaterial_Recieved_Without_Po_Master
        Query = " Select Reverse_ID,Reverse_Code,Reverse_No,Reverse_Date,Remarks,received_ID,Created_By,Creation_Date,Modified_By,Modification_Date,Division_ID from ReverseMaterial_Recieved_Without_Po_Master where  Reverse_Id in" & ReverseWithoutIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMaterial_Recieved_Without_Po_Master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '25)REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO
        Query = " Select Reverse_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,Item_Rate,batch_no,batch_date,prev_item_qty,prev_item_Rate,prev_item_vat,prev_item_exice, " & v_the_current_division_id & " as Division_ID from REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where Reverse_Id in " & ReverseWithoutIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '26)ReverseMATERIAL_RECEIVED_Against_PO_DETAIL
        Query = " Select Reverse_ID,Item_ID,Prev_Item_Qty,Item_Qty,Prev_Item_Rate,Item_Rate,Created_By,Creation_Date,Modified_By,Modification_Date,Division_Id,Prev_Item_vat,Prev_Item_exice,Item_vat,Item_exice,Batch_No,Expiry_Date,Stock_Detail_Id from ReverseMATERIAL_RECEIVED_Against_PO_DETAIL where Reverse_Id in" & ReverseAgainstIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMATERIAL_RECEIVED_Against_PO_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '27)ReverseMaterial_Recieved_Against_Po_Master
        Query = " Select Reverse_ID,Reverse_Code,Reverse_No,Reverse_Date,Remarks,received_ID,Created_By,Creation_Date,Modified_By,Modification_Date,Division_ID from ReverseMaterial_Recieved_Against_Po_Master where  Reverse_Id in" & ReverseAgainstIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseMaterial_Recieved_Against_Po_Master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '28)ReverseWASTAGE_DETAIL
        Query = " Select p_ReverseWastage_ID,f_ReverseWastage_ID,Item_ID,Stock_Detail_Id,Item_Qty,item_Rate,Actual_Qty,Created_By,Creation_Date,Modified_By,Modified_Date,Division_ID,WastageId from ReverseWASTAGE_DETAIL where f_reverseWastage_id in " & ReverseWastageIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseWASTAGE_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '29)ReverseWASTAGE_MASTER
        Query = " Select ReverseWastage_ID,WastageId,ReverseWastage_Code,ReverseWastage_No,ReverseWastage_Date,ReverseWastage_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date,Division_ID from ReverseWASTAGE_MASTER where reverseWastage_id in " & ReverseWastageIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ReverseWASTAGE_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '30)supplier_rate_list
        Query = " Select SRL_ID,SRL_NAME,SRL_DATE,SRL_DESC,SUPP_ID,ACTIVE,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from supplier_rate_list where SRL_id in " & SrlIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "supplier_rate_list"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '31)SUPPLIER_RATE_LIST_DETAIL
        Query = " Select SRL_ID,ITEM_ID,ITEM_RATE,DEL_QTY,DEL_DAYS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,DIVISION_ID from SUPPLIER_RATE_LIST_DETAIL where SRL_id in " & SrlIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "SUPPLIER_RATE_LIST_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '32)Transaction_Log
        Query = " Select Transaction_ID,Item_ID,STOCK_DETAIL_ID,Transaction_Type,Quantity,Trans_Date,Current_Stock, " & v_the_current_division_id & " as Division_Id from Transaction_Log where convert(varchar,trans_date,101) = '" & dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' "
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Transaction_Log"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '33)WASTAGE_DETAIL
        Query = " Select Wastage_ID,Item_ID,Stock_Detail_Id,Item_Qty,Item_Rate,Balance_Qty,Created_By,Creation_Date,Modified_By,Modified_Date,Division_ID from WASTAGE_DETAIL where wastage_id in " & WastageIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "WASTAGE_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '34)wastage_master
        Query = " Select Wastage_ID,Wastage_Code,Wastage_No,Wastage_Date,Wastage_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date,Division_ID from wastage_master where wastage_id in " & WastageIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "wastage_master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '35)User Master
        Query = " Select user_id,user_name,password,user_role,division_id,CostCenter_id from User_master where division_id = " & v_the_current_division_id
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "User_master_mms"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '36)ADJUSTMENT MASTER
        Query = " SELECT  Adjustment_ID,Adjustment_Code,Adjustment_No,Adjustment_Date,Adjustment_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date,Division_ID FROM ADJUSTMENT_MASTER where Adjustment_ID in " & AdjustmentIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ADJUSTMENT_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '37)ADJUSTMENT DETAIL
        Query = " SELECT Adjustment_ID,Item_ID,Stock_Detail_Id,Item_Qty,Item_Rate,Balance_Qty,Created_By,Creation_Date,Modified_By,Modified_Date,Division_ID FROM dbo.ADJUSTMENT_DETAIL where Adjustment_ID in " & AdjustmentIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "ADJUSTMENT_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '38)STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER
        Query = " SELECT TRANSFER_ID,TRANSFER_CODE,TRANSFER_NO,TRANSFER_DATE,TRANSFER_CC_ID,TRANSFER_REMARKS,TRANSFER_STATUS,RECEIVED_DATE,Created_By,Creation_Date,Modified_By,Modified_Date,COSTCENTER_ID,Division_ID FROM dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER where TRANSFER_ID in " & StocktransferccInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '39)STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL
        Query = " SELECT TRANSFER_ID,ITEM_ID,ITEM_QTY,ACCEPTED_QTY,RETURNED_QTY,Created_By,Creation_Date,Modified_By,Modified_Date,COSTCENTER_ID,TRANSFER_RATE,Division_ID FROM dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL where TRANSFER_ID in " & StocktransferccInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '40)WASTAGE_DETAIL_CC
        Query = " Select Wastage_ID,Item_ID,Item_Qty,Item_Rate,Balance_Qty,Created_By,Creation_Date,Modified_By,Modified_Date,CostCenter_id,Division_ID from WASTAGE_DETAIL_CC where wastage_id in " & WastageIdsInQuerycc
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "WASTAGE_DETAIL_CC"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '41)wastage_master_cc
        Query = " Select Wastage_ID,Wastage_Code,Wastage_No,Wastage_Date,Wastage_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date,CostCenter_id,Division_ID from wastage_master_cc where wastage_id in " & WastageIdsInQuerycc
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "wastage_master_cc"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '42)Closing_cc_master
        Query = " Select Closing_id,closing_code,Closing_no,closing_date,closing_remarks,closing_status,Created_By,Creation_Date,Modified_By,Modified_Date,CostCenter_id,Division_ID from Closing_cc_master where Closing_id in " & Closingidscc
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "CLOSING_CC_MASTER"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        '43)Closing_cc_Detail
        Query = " Select Closing_id,item_id,item_qty,item_rate,Current_stock,Consumption,Created_BY,Creation_Date,Modified_BY,Modified_Date,CostCenter_id,Division_ID from Closing_cc_detail where closing_id in " & Closingidscc
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Closing_cc_detail"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '44)REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO
        Query = " SELECT Reverse_id, Item_id, Costcenter_id, po_id,Item_qty, Item_vat, Item_exice, Item_rate, Prev_Item_qty, Prev_item_rate, Prev_item_vat, Prev_item_exice, batch_no, batch_date, " & v_the_current_division_id & " AS division_id FROM REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where Reverse_Id in " & ReverseAgainstIdsInQuery
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO"
        TableToTransferDataSet.Tables.Add(Table.Copy())


        '45)Recipe_Master
        Query = " Select Recipe_Id,Menu_Id,Item_Id,Item_uom,Item_qty, Item_yield_qty,creation_date, created_by, Modification_date, Modified_by, " & v_the_current_division_id & " from Recipe_Master "
        Table = Get_Remote_DataSet(Query).Tables(0)
        Table.TableName = "Recipe_Master"
        TableToTransferDataSet.Tables.Add(Table.Copy())

        Bulk_Copy(TableToTransferDataSet)
    End Sub

    Public Sub Bulk_Copy(ByVal _Table As DataSet)
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

            cmd = New SqlCommand("delete from Transaction_Log where division_id=" & v_the_current_division_id & " and  convert(varchar,trans_Date,101) = '" & dtpDate.Value.Date.ToString("MM/dd/yyyy") & "' ")
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
                Query = "select isnull(max(DataTransferId_num),0)+1 from DataTransfer"
                Dim TransferId As Int32 = obj.ExecuteScalar(Query)
                cmd = New SqlCommand("insert into DataTransfer(DataTransferId_num,DataTransferDate_dt,IsCompleted_bit,fk_CreatedBy_num,CreatedDate_dt,fk_ModifiedBy_num,ModifiedDate_dt) values (" & TransferId & ",'" & dtpDate.Value.Date.ToString() & "',1,1,GETDATE(),1,GETDATE())")
                'cmd.Transaction = tran
                cmd.Connection = con
                cmd.ExecuteNonQuery()

                con = New SqlConnection(gblDNS_Online)
                If con.State = ConnectionState.Open Then con.Close()
                con.Open()
                'Query = "select isnull(max(DataTransferId_num),0)+1 from DataTransfer"
                'TransferId = obj.ExecuteScalar(Query)
                cmd = New SqlCommand("insert into DataTransfer(DataTransferId_num,DataTransferDate_dt,IsCompleted_bit,fk_CreatedBy_num,CreatedDate_dt,fk_ModifiedBy_num,ModifiedDate_dt,Division_id) values (" & TransferId & ",'" & dtpDate.Value.Date.ToString() & "',1,1,GETDATE(),1,GETDATE(), " & v_the_current_division_id & ")")
                'cmd.Transaction = tran
                cmd.Connection = con
                cmd.ExecuteNonQuery()
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

    End Sub

    Private Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        Me.Close()

    End Sub

End Class