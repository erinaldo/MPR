
INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0018_26_June_2018_DataTransferLinkedServer' ,
          GETDATE()
        )
Go

--- Use ADMINPLUS ---

Alter table MATERIAL_RECEIVED_AGAINST_PO_DETAIL add Cess_Per numeric(18, 2)

GO

Alter table MATERIAL_RECEIVED_AGAINST_PO_DETAIL add ACess numeric(18, 2)

GO

Alter table MATERIAL_RECEIVED_AGAINST_PO_MASTER add VAT_ON_EXICE bit

GO

Alter table MATERIAL_RECEIVED_AGAINST_PO_MASTER add IsPrinted bit

GO

Alter table MATERIAL_RECEIVED_AGAINST_PO_MASTER add CUST_ID numeric(18, 0)

GO

Alter table MATERIAL_RECEIVED_AGAINST_PO_MASTER add REFERENCE_ID numeric(18, 0) NOT NULL DEFAULT(10070)

GO

Alter table MATERIAL_RECEIVED_AGAINST_PO_MASTER add CESS_AMOUNT numeric(18, 2)

GO

Alter table MATERIAL_RECEIVED_WITHOUT_PO_DETAIL add Item_cess numeric(18, 4)

GO

Alter table MATERIAL_RECEIVED_WITHOUT_PO_DETAIL add Acess numeric(18, 2)

GO

Alter table MATERIAL_RECEIVED_WITHOUT_PO_DETAIL add DiscountValue1 numeric(18, 2)

GO


Alter table MATERIAL_RECEIVED_WITHOUT_PO_DETAIL add GSTPAID varchar

GO

Alter table MATERIAL_RECIEVED_WITHOUT_PO_MASTER add VAT_ON_EXICE bit

GO

Alter table MATERIAL_RECIEVED_WITHOUT_PO_MASTER add IsPrinted bit

GO

Alter table MATERIAL_RECIEVED_WITHOUT_PO_MASTER add REFERENCE_ID numeric(18, 0) NOT NULL DEFAULT(10070)

GO

Alter table MATERIAL_RECIEVED_WITHOUT_PO_MASTER add CESS_AMOUNT numeric(18, 2) NOT NULL DEFAULT(0.00)

GO

Alter table MATERIAL_RECIEVED_WITHOUT_PO_MASTER add CashDiscount_Amt numeric(18, 3)   

GO

Alter table NON_STOCKABLE_ITEMS_MAT_REC_WO_PO add DiscountValue1 numeric(18, 2)

GO

Alter table NON_STOCKABLE_ITEMS_MAT_REC_WO_PO add GSTPAID varchar(5)

GO

Alter table SALE_INVOICE_STOCK_DETAIL add DIVISION_ID int

Go

--proc_TransferMMSData '1','173.249.49.87\SQL2K12,1986].[ADMINPLUS','MMSPLUSBunty','2018-06-24','2018-06-25'     

Alter PROCEDURE proc_TransferMMSData      
    (      
      @DivisionId VARCHAR(50) ,     
      @destDB VARCHAR(100),    
      @sourceDB VARCHAR(100),      
      @fromdate VARCHAR(50),
      @todate VARCHAR(50)       
    )      
AS      
    BEGIN    
        
    DECLARE @deleteDynamicQuery1 NVARCHAR(MAX)     
    DECLARE @deleteDynamicQuery2 NVARCHAR(MAX)
    DECLARE @deleteDynamicQuery3 NVARCHAR(MAX)
    DECLARE @deleteDynamicQuery4 NVARCHAR(MAX)
            
    DECLARE @insertDynamicQuery1 NVARCHAR(MAX)
    DECLARE @insertDynamicQuery2 nVARCHAR(MAX)
	DECLARE @insertDynamicQuery3 NVARCHAR(MAX)    
    DECLARE @insertDynamicQuery4 nVARCHAR(MAX) 
    DECLARE @insertDynamicQuery5 NVARCHAR(MAX)    
    DECLARE @insertDynamicQuery6 nVARCHAR(MAX) 
    DECLARE @insertDynamicQuery7 NVARCHAR(MAX)    
    DECLARE @insertDynamicQuery8 nVARCHAR(MAX)  
    DECLARE @insertDynamicQuery9 nVARCHAR(MAX) 
    DECLARE @insertDynamicQuery10 nVARCHAR(MAX)  
          
            
  -----------Delete All Data from MMSPLUS-----------        
        
  SET @deleteDynamicQuery1 = '
		 DELETE FROM [' + @destDB + '].dbo.CLOSING_STOCK_AVG_RATE where division_id = ''' + @DivisionId + ''' 
         DELETE  FROM [' + @destDB + '].dbo.ITEM_DETAIL where Div_ID = ''' + @DivisionId + '''    
         DELETE  FROM [' + @destDB + '].dbo.Recipe_master where division_id = ''' + @DivisionId + '''       
         DELETE  FROM [' + @destDB + '].dbo.STOCK_DETAIL where division_id = ''' + @DivisionId + '''
         DELETE  FROM [' + @destDB + '].dbo.INDENT_DETAIL  where division_id = ''' + @DivisionId + ''' and Indent_Id in ( select indent_id from [' + @sourceDB + '].dbo.INDENT_MASTER where cast(indent_date as date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')       
         DELETE  FROM [' + @destDB + '].dbo.INDENT_MASTER  where division_id = ''' + @DivisionId + ''' and Indent_Id in ( select indent_id from [' + @sourceDB + '].dbo.INDENT_MASTER where cast(indent_date as date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
         DELETE  FROM [' + @destDB + '].dbo.MATERIAL_ISSUE_TO_COST_CENTER_DETAIL where division_id = ''' + @DivisionId + ''' and MIO_ID in ( select MIO_ID from [' + @sourceDB + '].dbo.MATERIAL_ISSUE_TO_COST_CENTER_MASTER where cast(mio_date as date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')       
         DELETE  FROM [' + @destDB + '].dbo.MATERIAL_ISSUE_TO_COST_CENTER_Master where division_id = ''' + @DivisionId + ''' and MIO_ID in ( select MIO_ID from [' + @sourceDB + '].dbo.MATERIAL_ISSUE_TO_COST_CENTER_MASTER where cast(mio_date as date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')  
         DELETE  FROM [' + @destDB + '].dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL where division_id = ''' + @DivisionId + ''' and Receipt_Id in ( select receipt_Id from [' + @sourceDB + '].dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER where cast(receipt_date as date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')              
         DELETE  FROM [' + @destDB + '].dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER where division_id = ''' + @DivisionId + ''' and Receipt_Id in ( select receipt_Id from [' + @sourceDB + '].dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER where cast(receipt_date as date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')  
         DELETE  FROM [' + @destDB + '].dbo.NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO where Div_ID = ''' + @DivisionId + ''' and Received_Id in ( select receipt_Id from [' + @sourceDB + '].dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER where cast(receipt_date as date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
         DELETE  FROM [' + @destDB + '].dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL  where division_id = ''' + @DivisionId + ''' and Received_Id in ( select received_id from [' + @sourceDB + '].dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER where cast(received_date as date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')                     
         DELETE  FROM [' + @destDB + '].dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER  where division_id = ''' + @DivisionId + ''' and Received_Id in ( select received_id from [' + @sourceDB + '].dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER where cast(received_date as date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')'                     
    
         --print @deleteDynamicQuery1     
         EXEC sys.[sp_executesql] @deleteDynamicQuery1  
         
         SET @deleteDynamicQuery2 = '
         DELETE  FROM [' + @destDB + '].dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO where division_id = ''' + @DivisionId + ''' and Received_Id in ( select received_id from [' + @sourceDB + '].dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER where cast(received_date as date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
         DELETE  FROM [' + @destDB + '].dbo.MRS_MAIN_STORE_Detail  where division_id = ''' + @DivisionId + ''' and MRS_Id in ( select MRS_ID from [' + @sourceDB + '].dbo.MRS_MAIN_STORE_MASTER where cast(MRS_DATE as date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')                     
         DELETE  FROM [' + @destDB + '].dbo.MRS_MAIN_STORE_MASTER  where division_id = ''' + @DivisionId + ''' and MRS_Id in ( select MRS_ID from [' + @sourceDB + '].dbo.MRS_MAIN_STORE_MASTER where cast(MRS_DATE as date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')                     
         DELETE  FROM [' + @destDB + '].dbo.OPEN_PO_Detail where division_id = ''' + @DivisionId + ''' and PO_Id in ( select PO_ID from [' + @sourceDB + '].dbo.OPEN_PO_MASTER where cast(PO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')                      
         DELETE  FROM [' + @destDB + '].dbo.OPEN_PO_MASTER where division_id = ''' + @DivisionId + ''' and PO_Id in ( select PO_ID from [' + @sourceDB + '].dbo.OPEN_PO_MASTER where cast(PO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')   
         DELETE  FROM [' + @destDB + '].dbo.PO_DETAIL  where division_id = ''' + @DivisionId + ''' and PO_Id in ( select PO_ID from [' + @sourceDB + '].dbo.PO_MASTER where cast(PO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')                     
         DELETE  FROM [' + @destDB + '].dbo.PO_Master  where division_id = ''' + @DivisionId + ''' and PO_Id in ( select PO_ID from [' + @sourceDB + '].dbo.PO_MASTER where cast(PO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')                     
         DELETE  FROM [' + @destDB + '].dbo.PO_STATUS  where division_id = ''' + @DivisionId + ''' and PO_Id in ( select PO_ID from [' + @sourceDB + '].dbo.PO_MASTER where cast(PO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
         DELETE  FROM [' + @destDB + '].dbo.ReverseMaterial_Issue_To_Cost_Center_Detail  where division_id = ''' + @DivisionId + ''' and RMIO_Id in ( select RMIO_ID from [' + @sourceDB + '].dbo.ReverseMaterial_Issue_To_Cost_Center_Master where cast(RMIO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')                     
         DELETE  FROM [' + @destDB + '].dbo.ReverseMaterial_Issue_To_Cost_Center_Master  where division_id = ''' + @DivisionId + ''' and RMIO_Id in ( select RMIO_ID from [' + @sourceDB + '].dbo.ReverseMaterial_Issue_To_Cost_Center_Master where cast(RMIO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
         DELETE  FROM [' + @destDB + '].dbo.ReverseMATERIAL_RECEIVED_Against_PO_DETAIL   where division_id = ''' + @DivisionId + ''' and Reverse_Id in ( select Reverse_ID from [' + @sourceDB + '].dbo.ReverseMATERIAL_RECIEVED_Against_PO_MASTER where cast(Reverse_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
         DELETE  FROM [' + @destDB + '].dbo.ReverseMATERIAL_RECIEVED_Against_PO_MASTER   where division_id = ''' + @DivisionId + ''' and Reverse_Id in ( select Reverse_ID from [' + @sourceDB + '].dbo.ReverseMATERIAL_RECIEVED_Against_PO_MASTER where cast(Reverse_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')'
          
         --print @deleteDynamicQuery2     
         EXEC sys.[sp_executesql] @deleteDynamicQuery2
                           
         SET @deleteDynamicQuery3 = ' 
          
         DELETE  FROM [' + @destDB + '].dbo.ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL   where division_id = ''' + @DivisionId + ''' and Reverse_Id in ( select Reverse_ID from [' + @sourceDB + '].dbo.ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER where cast(Reverse_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
         DELETE  FROM [' + @destDB + '].dbo.REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO		 where division_id = ''' + @DivisionId + ''' and Reverse_Id in ( select Reverse_ID from [' + @sourceDB + '].dbo.ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER where cast(Reverse_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
         DELETE  FROM [' + @destDB + '].dbo.REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO	 where division_id = ''' + @DivisionId + ''' and Reverse_Id in ( select Reverse_ID from [' + @sourceDB + '].dbo.ReverseMATERIAL_RECIEVED_Against_PO_MASTER where cast(Reverse_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')                                  
         DELETE  FROM [' + @destDB + '].dbo.ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER   where division_id = ''' + @DivisionId + ''' and Reverse_Id in ( select Reverse_ID from [' + @sourceDB + '].dbo.ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER where cast(Reverse_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
         DELETE  FROM [' + @destDB + '].dbo.ReverseWASTAGE_Detail  where division_id = ''' + @DivisionId + ''' and F_ReverseWastage_Id in ( select ReverseWastage_ID from [' + @sourceDB + '].dbo.ReverseWASTAGE_MASTER where cast(ReverseWastage_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')                      
         DELETE  FROM [' + @destDB + '].dbo.ReverseWASTAGE_MASTER where division_id = ''' + @DivisionId + ''' and reverseWastage_id in ( select ReverseWastage_ID from [' + @sourceDB + '].dbo.ReverseWASTAGE_MASTER where cast(ReverseWastage_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')  
         DELETE  FROM [' + @destDB + '].dbo.SUPPLIER_RATE_LIST			where division_id = ''' + @DivisionId + ''' and SRL_Id in ( select SRL_ID from [' + @sourceDB + '].dbo.SUPPLIER_RATE_LIST where cast(SRL_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')                     
         DELETE  FROM [' + @destDB + '].dbo.SUPPLIER_RATE_LIST_DETAIL   where division_id = ''' + @DivisionId + ''' and SRL_Id in ( select SRL_ID from [' + @sourceDB + '].dbo.SUPPLIER_RATE_LIST where cast(SRL_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
         DELETE  FROM [' + @destDB + '].dbo.WASTAGE_Detail where division_id = ''' + @DivisionId + ''' and Wastage_Id in ( select Wastage_ID from [' + @sourceDB + '].dbo.WASTAGE_MASTER where cast(Wastage_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
         DELETE  FROM [' + @destDB + '].dbo.WASTAGE_MASTER where division_id = ''' + @DivisionId + ''' and Wastage_Id in ( select Wastage_ID from [' + @sourceDB + '].dbo.WASTAGE_MASTER where cast(Wastage_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')'
         
         
         --print @deleteDynamicQuery3     
         EXEC sys.[sp_executesql] @deleteDynamicQuery3
         
         SET @deleteDynamicQuery4 = ' 
         DELETE  FROM [' + @destDB + '].dbo.ADJUSTMENT_MASTER  where division_id = ''' + @DivisionId + ''' and Adjustment_ID in ( select Adjustment_ID from [' + @sourceDB + '].dbo.ADJUSTMENT_MASTER where cast(Adjustment_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')      
         DELETE  FROM [' + @destDB + '].dbo.ADJUSTMENT_Detail where division_id = ''' + @DivisionId + ''' and Adjustment_ID in ( select Adjustment_ID from [' + @sourceDB + '].dbo.ADJUSTMENT_MASTER where cast(Adjustment_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
         DELETE  FROM [' + @destDB + '].dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER where division_id = ''' + @DivisionId + ''' and TRANSFER_ID in ( select TRANSFER_ID from [' + @sourceDB + '].dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER where cast(TRANSFER_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')       
         DELETE  FROM [' + @destDB + '].dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL where division_id = ''' + @DivisionId + ''' and TRANSFER_ID in ( select TRANSFER_ID from [' + @sourceDB + '].dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER where cast(TRANSFER_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
         DELETE  FROM [' + @destDB + '].dbo.WASTAGE_Detail_cc  where division_id = ''' + @DivisionId + ''' and Wastage_Id in ( select Wastage_id from [' + @sourceDB + '].dbo.WASTAGE_MASTER_CC where cast(WASTAGE_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')      
         DELETE  FROM [' + @destDB + '].dbo.WASTAGE_MASTER_CC where division_id = ''' + @DivisionId + ''' and Wastage_Id in ( select Wastage_id from [' + @sourceDB + '].dbo.WASTAGE_MASTER_CC where cast(WASTAGE_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
         DELETE  FROM [' + @destDB + '].dbo.Closing_cc_master where division_id = ''' + @DivisionId + ''' and Closing_Id in ( select Closing_id from [' + @sourceDB + '].dbo.CLOSING_CC_MASTER where cast(CLOSING_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')       
         DELETE  FROM [' + @destDB + '].dbo.Closing_cc_detail where division_id = ''' + @DivisionId + ''' and Closing_Id in ( select Closing_id from [' + @sourceDB + '].dbo.CLOSING_CC_MASTER where cast(CLOSING_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
         
         DELETE  FROM [' + @destDB + '].dbo.SALE_INVOICE_DETAIL WHERE cast(CREATION_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + '''    
         DELETE  FROM [' + @destDB + '].dbo.SALE_INVOICE_MASTER  WHERE  cast(CREATION_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and DIVISION_ID = ''' + @DivisionId + '''
         DELETE  FROM [' + @destDB + '].dbo.SALE_INVOICE_STOCK_DETAIL 
         DELETE  FROM [' + @destDB + '].dbo.PaymentTransaction  where cast(PaymentDate AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) 
         DELETE  FROM [' + @destDB + '].dbo.SettlementDetail  WHERE  cast(CreatedDate AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) AND DivisionId = ''' + @DivisionId + '''        
         DELETE  FROM [' + @destDB + '].dbo.DebitNote_DETAIL  WHERE DIVISION_ID = ''' + @DivisionId + ''' and cast(Creation_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) 
         DELETE  FROM [' + @destDB + '].dbo.DebitNote_Master  where Division_Id = ''' + @DivisionId + ''' and cast(DebitNote_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE)         
         DELETE  FROM [' + @destDB + '].dbo.CreditNote_DETAIL  where DIVISION_ID = ''' + @DivisionId + ''' and cast(Creation_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE)   
         DELETE  FROM [' + @destDB + '].dbo.CreditNote_Master  where Division_Id = ''' + @DivisionId + ''' and cast(CreditNote_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE)'  
         
            
        --print @deleteDynamicQuery4     
        EXEC sys.[sp_executesql] @deleteDynamicQuery4
               
        
		-----------Insert Data into MMSPLUS from ADMINPLUS based on selected outlet-----------        
        
        SET @insertDynamicQuery1 = 'INSERT  INTO [' + @destDB + '].dbo.CLOSING_STOCK_AVG_RATE      
                Select ID,Closing_DATE,ITEM_ID,CLOSING_STOCK,AVG_RATE, ''' + @DivisionId + ''' as division_id       
        FROM    [' + @sourceDB + '].dbo.CLOSING_STOCK_AVG_RATE
        INSERT  INTO [' + @destDB + '].dbo.ITEM_DETAIL      
                SELECT  ITEM_ID,RE_ORDER_LEVEL,RE_ORDER_QTY,PURCHASE_VAT_ID,SALE_VAT_ID,OPENING_STOCK,CURRENT_STOCK,IS_EXTERNAL,TRANSFER_RATE,AVERAGE_RATE,OPENING_RATE,IS_ACTIVE,IS_STOCKABLE,STOCK_DETAIL_ID,''' + @DivisionId + ''' as DIV_ID     
                FROM    [' + @sourceDB + '].dbo.ITEM_DETAIL
        INSERT  INTO [' + @destDB + '].dbo.Recipe_master      
                SELECT  Recipe_Id,Menu_Id,Item_Id,Item_uom,Item_qty, Item_yield_qty,creation_date, created_by, Modification_date, Modified_by, ''' + @DivisionId + ''' as division_id     
                FROM    [' + @sourceDB + '].dbo.Recipe_master
        INSERT  INTO [' + @destDB + '].dbo.STOCK_DETAIL      
                SELECT  STOCK_DETAIL_ID,Item_id,Batch_no,Expiry_date,Item_Qty,Issue_Qty,Balance_Qty,DOC_ID,Transaction_ID, ''' + @DivisionId + ''' as division_id     
                FROM    [' + @sourceDB + '].dbo.STOCK_DETAIL  
        INSERT  INTO [' + @destDB + '].dbo.INDENT_MASTER      
                SELECT  INDENT_ID,INDENT_CODE,INDENT_NO,INDENT_DATE,REQUIRED_DATE,INDENT_REMARKS,INDENT_STATUS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE, ''' + @DivisionId + ''' as DIVISION_ID      
                FROM    [' + @sourceDB + '].dbo.INDENT_MASTER  As im   
                where im.Indent_Id  in ( select indent_id from [' + @sourceDB + '].dbo.INDENT_MASTER where cast(indent_date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.INDENT_DETAIL      
                SELECT  INDENT_ID,ITEM_ID,ITEM_QTY_REQ,ITEM_QTY_PO,ITEM_QTY_BAL,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,  ''' + @DivisionId + ''' as DIVISION_ID        
                FROM    [' + @sourceDB + '].dbo.INDENT_DETAIL AS id         
                where id.Indent_Id  in ( select indent_id from [' + @sourceDB + '].dbo.INDENT_MASTER where cast(indent_date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')  
        INSERT  INTO [' + @destDB + '].dbo.MATERIAL_ISSUE_TO_COST_CENTER_Master      
                SELECT  MIO_ID,MIO_CODE,MIO_NO,MIO_DATE,CS_ID,MIO_REMARKS,MIO_ACCEPT_DATE,MIO_STATUS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,''' + @DivisionId + ''' as DIVISION_ID      
                FROM    [' + @sourceDB + '].dbo.MATERIAL_ISSUE_TO_COST_CENTER_Master mitccm         
                WHERE mitccm.MIO_ID in ( select MIO_ID from [' + @sourceDB + '].dbo.MATERIAL_ISSUE_TO_COST_CENTER_MASTER where cast(mio_date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')'
                      
       
        --print @insertDynamicQuery1     
		EXEC sys.[sp_executesql] @insertDynamicQuery1  
		
		SET @insertDynamicQuery2 = 'INSERT  INTO [' + @destDB + '].dbo.MATERIAL_ISSUE_TO_COST_CENTER_DETAIL      
                SELECT  MIO_ID,MRS_ID,ITEM_ID,REQ_QTY,ISSUED_QTY,ACCEPTED_QTY,RETURNED_QTY,BalIssued_Qty,IS_WASTAGE,ITEM_RATE,STOCK_DETAIL_ID, ''' + @DivisionId + ''' as DIVISION_ID      
                FROM    [' + @sourceDB + '].dbo.MATERIAL_ISSUE_TO_COST_CENTER_DETAIL mitccd                         
				WHERE mitccd.MIO_ID in ( select MIO_ID from [' + @sourceDB + '].dbo.MATERIAL_ISSUE_TO_COST_CENTER_MASTER where cast(mio_date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL      
                SELECT  Receipt_ID,Item_ID,PO_ID,Item_Qty,Item_Rate,Bal_Item_Qty,Stock_Detail_iD,Vat_Per,Bal_Vat_Per,Exice_per,Bal_Exice_Per, ''' + @DivisionId + ''' as DIVISION_ID,DType,DiscountValue,Cess_Per,ACess     
                FROM    [' + @sourceDB + '].dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL mrapd        
                WHERE mrapd.Receipt_Id IN ( select receipt_Id from [' + @sourceDB + '].dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER where cast(receipt_date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER      
                SELECT  Receipt_ID,Receipt_No,Receipt_Code,PO_ID,Receipt_Date,Remarks,MRN_NO,MRN_PREFIX,Created_BY,Creation_Date,Modified_By,Modification_Date,MRN_STATUS,MRNCompanies_ID,Invoice_no,Invoice_date,freight,Other_Charges,Discount_Amt , ''' + @DivisionId + ''' as DIVISION_ID,VAT_ON_EXICE,IsPrinted,CUST_ID,REFERENCE_ID,CESS_AMOUNT,GROSS_AMOUNT,GST_AMOUNT,NET_AMOUNT,MRN_TYPE     
                FROM    [' + @sourceDB + '].dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER mrapm      
                WHERE mrapm.Receipt_Id IN ( select receipt_Id from [' + @sourceDB + '].dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER where cast(receipt_date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO      
                SELECT  Received_ID,PO_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,batch_no,batch_date,Item_Rate,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice, ''' + @DivisionId + ''' as Div_ID,DType,DiscountValue         
                FROM    [' + @sourceDB + '].dbo.NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO  nsimrap              
				WHERE nsimrap.Received_Id IN ( select receipt_Id from [' + @sourceDB + '].dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER where cast(receipt_date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
		INSERT  INTO [' + @destDB + '].dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL      
                SELECT  Received_ID,Item_ID,Item_Qty,Item_Rate,Created_By,Creation_Date,Modified_By,Modification_Date,Item_vat,Item_exice,Batch_No,Expiry_Date,Stock_Detail_Id,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice, ''' + @DivisionId + ''' as Division_Id,DType,DiscountValue,Item_cess,Acess,DiscountValue1,GSTPAID      
                FROM    [' + @sourceDB + '].dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL mrwpd         
                WHERE mrwpd.Received_Id in ( select received_id from [' + @sourceDB + '].dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER where cast(received_date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')'      
                     
        --print @insertDynamicQuery2     
		EXEC sys.[sp_executesql] @insertDynamicQuery2
        
        SET @insertDynamicQuery3 = '
        INSERT  INTO [' + @destDB + '].dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER      
                SELECT  Received_ID,Received_Code,Received_No,Received_Date,Purchase_Type,Vendor_ID,Remarks,Po_ID,MRN_PREFIX,MRN_NO,Created_By,Creation_Date,Modified_By,Modification_Date,Invoice_No,Invoice_Date,mrn_status,freight,freight_type,MRNCompanies_ID,Other_Charges,Discount_Amt, ''' + @DivisionId + ''' as Division_Id,VAT_ON_EXICE,IsPrinted,CashDiscount_Amt,REFERENCE_ID,CESS_AMOUNT,GROSS_AMOUNT,GST_AMOUNT,NET_AMOUNT,MRN_TYPE      
                FROM    [' + @sourceDB + '].dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER mrwpm        
                WHERE mrwpm.Received_Id in ( select received_id from [' + @sourceDB + '].dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER where cast(received_date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO      
                SELECT  Received_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,batch_no,batch_date,Item_Rate,Bal_Item_Qty,Bal_Item_Rate,Bal_Item_Vat,Bal_Item_Exice,''' + @DivisionId + ''' as DIVISION_ID,DType,DiscountValue,DiscountValue1,GSTPAID,Item_cess      
                FROM    [' + @sourceDB + '].dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO nsimrwp         
                WHERE nsimrwp.Received_Id in ( select received_id from [' + @sourceDB + '].dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER where cast(received_date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.MRS_MAIN_STORE_DETAIL      
                SELECT  MRS_ID,ITEM_ID,ITEM_QTY,Issue_QTY,Bal_QTY,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,''' + @DivisionId + ''' as DIVISION_ID      
                FROM    [' + @sourceDB + '].dbo.MRS_MAIN_STORE_DETAIL AS mmsd      
                WHERE mmsd.MRS_Id in ( select MRS_ID from [' + @sourceDB + '].dbo.MRS_MAIN_STORE_MASTER where cast(MRS_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')     
        INSERT  INTO [' + @destDB + '].dbo.MRS_MAIN_STORE_MASTER      
                SELECT  MRS_ID,MRS_CODE,MRS_NO,MRS_DATE,CC_ID,REQ_DATE,MRS_REMARKS,MRS_STATUS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE, ''' + @DivisionId + ''' as DIVISION_ID,APPROVE_DATETIME      
                FROM    [' + @sourceDB + '].dbo.MRS_MAIN_STORE_MASTER mmsm         
                WHERE mmsm.MRS_Id in ( select MRS_ID from [' + @sourceDB + '].dbo.MRS_MAIN_STORE_MASTER where cast(MRS_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
        INSERT  INTO [' + @destDB + '].dbo.OPEN_PO_Detail      
                SELECT  PO_ID,ITEM_NAME,UOM,ITEM_QTY,VAT_PER,EXICE_PER,ITEM_RATE,AMOUNT,TOTAL_AMOUNT,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,''' + @DivisionId + ''' as DIVISION_ID      
                FROM    [' + @sourceDB + '].dbo.OPEN_PO_Detail opd
                WHERE opd.PO_Id in ( select PO_ID from [' + @sourceDB + '].dbo.OPEN_PO_MASTER where cast(PO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')          
        INSERT  INTO [' + @destDB + '].dbo.OPEN_PO_MASTER      
                SELECT  PO_ID,PO_CODE,PO_NO,PO_TYPE,PO_DATE,PO_START_DATE,PO_END_DATE,PO_REMARKS,PO_SUPP_ID,PO_QUALITY_ID,PO_DELIVERY_ID,PO_STATUS,PATMENT_TERMS,TRANSPORT_MODE,TOTAL_AMOUNT,VAT_AMOUNT,NET_AMOUNT,EXICE_AMOUNT,OCTROI,PRICE_BASIS,FRIEGHT,OTHER_CHARGES,CESS_PER,ALREADY_RECVD,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,''' + @DivisionId + ''' as DIVISION_ID      
                FROM    [' + @sourceDB + '].dbo.OPEN_PO_MASTER opm        
                WHERE opm.PO_Id in ( select PO_ID from [' + @sourceDB + '].dbo.OPEN_PO_MASTER where cast(PO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')'          
                       
        
        --print @insertDynamicQuery3     
		EXEC sys.[sp_executesql] @insertDynamicQuery3
        
        
        SET @insertDynamicQuery4 = 'INSERT  INTO [' + @destDB + '].dbo.PO_DETAIL      
                SELECT  PO_ID,ITEM_ID,ITEM_QTY,Balance_Qty,VAT_PER,EXICE_PER,ITEM_RATE,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,''' + @DivisionId + ''' as DIVISION_ID      
                FROM    [' + @sourceDB + '].dbo.PO_DETAIL pd
                WHERE pd.PO_Id in ( select PO_ID from [' + @sourceDB + '].dbo.PO_MASTER where cast(PO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')        
		INSERT  INTO [' + @destDB + '].dbo.PO_MASTER      
                SELECT  PO_ID,PO_CODE,PO_NO,PO_DATE,PO_START_DATE,PO_END_DATE,PO_REMARKS,PO_SUPP_ID,PO_QUALITY_ID,PO_DELIVERY_ID,PO_STATUS,PATMENT_TERMS,TRANSPORT_MODE,TOTAL_AMOUNT,VAT_AMOUNT,NET_AMOUNT,EXICE_AMOUNT,PO_TYPE,OCTROI,PRICE_BASIS,FRIEGHT,OTHER_CHARGES,CESS_PER,ALREADY_RECVD,DISCOUNT_AMOUNT,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,OPEN_PO_QTY, ''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.PO_MASTER pm        
                WHERE pm.PO_Id in ( select PO_ID from [' + @sourceDB + '].dbo.PO_MASTER where cast(PO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
		INSERT  INTO [' + @destDB + '].dbo.PO_STATUS      
                SELECT  POS_ID,PO_ID,ITEM_ID,INDENT_ID,REQUIRED_QTY,RECIEVED_QTY,BALANCE_QTY,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,''' + @DivisionId + ''' as DIVISION_ID      
                FROM    [' + @sourceDB + '].dbo.PO_STATUS ps     
                WHERE ps.PO_Id in ( select PO_ID from [' + @sourceDB + '].dbo.PO_MASTER where cast(PO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
		INSERT  INTO [' + @destDB + '].dbo.ReverseMaterial_Issue_To_Cost_Center_Detail      
                SELECT  RMIO_ID,Stock_Detail_Id,ITEM_ID,Item_QTY,Avg_RATE, ''' + @DivisionId + ''' as DIVISION_ID     
                FROM    [' + @sourceDB + '].dbo.ReverseMaterial_Issue_To_Cost_Center_Detail rmitccd       
                WHERE rmitccd.RMIO_Id in ( select RMIO_ID from [' + @sourceDB + '].dbo.ReverseMaterial_Issue_To_Cost_Center_Master where cast(RMIO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
		INSERT  INTO [' + @destDB + '].dbo.Reversematerial_issue_to_cost_center_master      
                SELECT  RMIO_ID,RMIO_CODE,RMIO_NO,RMIO_DATE,Issue_Id,RMIO_REMARKS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,''' + @DivisionId + ''' as DIVISION_ID      
                FROM    [' + @sourceDB + '].dbo.Reversematerial_issue_to_cost_center_master rmitccm     
                WHERE rmitccm.RMIO_Id in ( select RMIO_ID from [' + @sourceDB + '].dbo.ReverseMaterial_Issue_To_Cost_Center_Master where cast(RMIO_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
		INSERT  INTO [' + @destDB + '].dbo.ReverseMATERIAL_RECEIVED_Against_PO_DETAIL      
                SELECT  Reverse_ID,Item_ID,Prev_Item_Qty,Item_Qty,Prev_Item_Rate,Item_Rate,Created_By,Creation_Date,Modified_By,Modification_Date,Prev_Item_vat,Prev_Item_exice,Item_vat,Item_exice,Batch_No,Expiry_Date,Stock_Detail_Id, ''' + @DivisionId + ''' as DIVISION_ID      
                FROM    [' + @sourceDB + '].dbo.ReverseMATERIAL_RECEIVED_Against_PO_DETAIL rmrapd
                WHERE rmrapd.Reverse_Id in ( select Reverse_ID from [' + @sourceDB + '].dbo.ReverseMATERIAL_RECIEVED_Against_PO_MASTER where cast(Reverse_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')'
              
        --print @insertDynamicQuery4     
		EXEC sys.[sp_executesql] @insertDynamicQuery4        
        
        
        SET @insertDynamicQuery5 = '
        
        INSERT  INTO [' + @destDB + '].dbo.ReverseMaterial_Recieved_Against_Po_Master      
                SELECT  Reverse_ID,Reverse_Code,Reverse_No,Reverse_Date,Remarks,received_ID,Created_By,Creation_Date,Modified_By,Modification_Date,''' + @DivisionId + ''' as DIVISION_ID     
                FROM    [' + @sourceDB + '].dbo.ReverseMaterial_Recieved_Against_Po_Master rmrapm
                WHERE rmrapm.Reverse_Id in ( select Reverse_ID from [' + @sourceDB + '].dbo.ReverseMATERIAL_RECIEVED_Against_PO_MASTER where cast(Reverse_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL      
                SELECT  Reverse_ID,Item_ID,Prev_Item_Qty,Item_Qty,Prev_Item_Rate,Item_Rate,Created_By,Creation_Date,Modified_By,Modification_Date,Prev_Item_vat,Prev_Item_exice,Item_vat,Item_exice,Batch_No,Expiry_Date,Stock_Detail_Id, ''' + @DivisionId + ''' as DIVISION_ID      
                FROM    [' + @sourceDB + '].dbo.ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL rmrwpd 
                WHERE rmrwpd.Reverse_Id in ( select Reverse_ID from [' + @sourceDB + '].dbo.ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER where cast(Reverse_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
        INSERT  INTO [' + @destDB + '].dbo.REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO      
                SELECT  Reverse_ID,Item_ID,CostCenter_ID,Item_Qty,Item_vat,Item_Exice,Item_Rate,batch_no,batch_date,prev_item_qty,prev_item_Rate,prev_item_vat,prev_item_exice,''' + @DivisionId + ''' as DIVISION_ID      
                FROM    [' + @sourceDB + '].dbo.REV_NON_STOCKABLE_ITEMS_MAT_REC_WO_PO rnsimrwp        
                WHERE rnsimrwp.Reverse_Id in ( select Reverse_ID from [' + @sourceDB + '].dbo.ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER where cast(Reverse_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.ReverseMaterial_Recieved_Without_Po_Master      
                SELECT  Reverse_ID,Reverse_Code,Reverse_No,Reverse_Date,Remarks,received_ID,Created_By,Creation_Date,Modified_By,Modification_Date,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.ReverseMaterial_Recieved_Without_Po_Master rmrwpm        
                WHERE rmrwpm.Reverse_Id in ( select Reverse_ID from [' + @sourceDB + '].dbo.ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER where cast(Reverse_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO      
                SELECT  Reverse_id, Item_id, Costcenter_id, po_id,Item_qty, Item_vat, Item_exice, Item_rate, Prev_Item_qty, Prev_item_rate, Prev_item_vat, Prev_item_exice, batch_no, batch_date,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.REV_NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO rnsimrap        
                WHERE rnsimrap.Reverse_Id in ( select Reverse_ID from [' + @sourceDB + '].dbo.ReverseMATERIAL_RECIEVED_Against_PO_MASTER where cast(Reverse_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')'
       
       --print @insertDynamicQuery5     
	  EXEC sys.[sp_executesql] @insertDynamicQuery5
        
        
      SET @insertDynamicQuery6 = '
        INSERT  INTO [' + @destDB + '].dbo.ReverseWASTAGE_Detail      
                SELECT  p_ReverseWastage_ID,f_ReverseWastage_ID,Item_ID,Stock_Detail_Id,Item_Qty,item_Rate,Actual_Qty,Created_By,Creation_Date,Modified_By,Modified_Date,WastageId,''' + @DivisionId + ''' as Division_ID          
                FROM    [' + @sourceDB + '].dbo.ReverseWASTAGE_Detail rwd        
                WHERE rwd.F_ReverseWastage_Id in ( select ReverseWastage_ID from [' + @sourceDB + '].dbo.ReverseWASTAGE_MASTER where cast(ReverseWastage_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.ReverseWASTAGE_MASTER      
                SELECT  ReverseWastage_ID,WastageId,ReverseWastage_Code,ReverseWastage_No,ReverseWastage_Date,ReverseWastage_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.ReverseWASTAGE_MASTER rwm        
                WHERE rwm.reverseWastage_id in ( select ReverseWastage_ID from [' + @sourceDB + '].dbo.ReverseWASTAGE_MASTER where cast(ReverseWastage_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
		INSERT  INTO [' + @destDB + '].dbo.SUPPLIER_RATE_LIST      
                SELECT  SRL_ID,SRL_NAME,SRL_DATE,SRL_DESC,SUPP_ID,ACTIVE,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.SUPPLIER_RATE_LIST srl        
                WHERE srl.SRL_Id in ( select SRL_ID from [' + @sourceDB + '].dbo.SUPPLIER_RATE_LIST where cast(SRL_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.SUPPLIER_RATE_LIST_DETAIL      
                SELECT  SRL_ID,ITEM_ID,ITEM_RATE,DEL_QTY,DEL_DAYS,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.SUPPLIER_RATE_LIST_DETAIL srld        
                WHERE srld.SRL_Id in ( select SRL_ID from [' + @sourceDB + '].dbo.SUPPLIER_RATE_LIST where cast(SRL_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
        INSERT  INTO [' + @destDB + '].dbo.WASTAGE_Detail      
                SELECT  Wastage_ID,Item_ID,Stock_Detail_Id,Item_Qty,Item_Rate,Balance_Qty,Created_By,Creation_Date,Modified_By,Modified_Date,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.WASTAGE_Detail wd        
                WHERE wd.Wastage_Id in ( select Wastage_ID from [' + @sourceDB + '].dbo.WASTAGE_MASTER where cast(Wastage_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')'
        
         --print @insertDynamicQuery6     
		EXEC sys.[sp_executesql] @insertDynamicQuery6
        
        SET @insertDynamicQuery7 = '
        INSERT  INTO [' + @destDB + '].dbo.WASTAGE_MASTER      
                SELECT  Wastage_ID,Wastage_Code,Wastage_No,Wastage_Date,Wastage_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.WASTAGE_MASTER wm        
                WHERE wm.Wastage_Id in ( select Wastage_ID from [' + @sourceDB + '].dbo.WASTAGE_MASTER where cast(Wastage_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''') 
        INSERT  INTO [' + @destDB + '].dbo.ADJUSTMENT_MASTER      
                SELECT  Adjustment_ID,Adjustment_Code,Adjustment_No,Adjustment_Date,Adjustment_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.ADJUSTMENT_MASTER am        
                WHERE am.Adjustment_ID in ( select Adjustment_ID from [' + @sourceDB + '].dbo.ADJUSTMENT_MASTER where cast(Adjustment_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.ADJUSTMENT_Detail      
                SELECT  Adjustment_ID,Item_ID,Stock_Detail_Id,Item_Qty,Item_Rate,Balance_Qty,Created_By,Creation_Date,Modified_By,Modified_Date,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.ADJUSTMENT_Detail ad        
                WHERE ad.Adjustment_ID in ( select Adjustment_ID from [' + @sourceDB + '].dbo.ADJUSTMENT_MASTER where cast(Adjustment_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER      
                SELECT  TRANSFER_ID,TRANSFER_CODE,TRANSFER_NO,TRANSFER_DATE,TRANSFER_CC_ID,TRANSFER_REMARKS,TRANSFER_STATUS,RECEIVED_DATE,Created_By,Creation_Date,Modified_By,Modified_Date,COSTCENTER_ID,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER stotom        
                WHERE stotom.TRANSFER_ID in ( select TRANSFER_ID from [' + @sourceDB + '].dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER where cast(TRANSFER_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL      
                SELECT  TRANSFER_ID,ITEM_ID,ITEM_QTY,ACCEPTED_QTY,RETURNED_QTY,Created_By,Creation_Date,Modified_By,Modified_Date,COSTCENTER_ID,TRANSFER_RATE,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_DETAIL stotod        
                WHERE stotod.TRANSFER_ID in ( select TRANSFER_ID from [' + @sourceDB + '].dbo.STOCK_TRANSFER_OUTLET_TO_OUTLET_MASTER where cast(TRANSFER_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')'
         
       
       --print @insertDynamicQuery7     
	   EXEC sys.[sp_executesql] @insertDynamicQuery7
	   
	   
	   SET @insertDynamicQuery8 = 'INSERT  INTO [' + @destDB + '].dbo.WASTAGE_Detail_cc      
                SELECT  Wastage_ID,Item_ID,Item_Qty,Item_Rate,Balance_Qty,Created_By,Creation_Date,Modified_By,Modified_Date,CostCenter_id,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.WASTAGE_Detail_cc wdcc        
                WHERE wdcc.wastage_id in ( select Wastage_id from [' + @sourceDB + '].dbo.WASTAGE_MASTER_CC where cast(WASTAGE_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.WASTAGE_MASTER_CC      
                SELECT  Wastage_ID,Wastage_Code,Wastage_No,Wastage_Date,Wastage_Remarks,Created_BY,Creation_Date,Modified_BY,Modified_Date,CostCenter_id,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.WASTAGE_MASTER_CC wmcc        
                WHERE wmcc.wastage_id in ( select Wastage_id from [' + @sourceDB + '].dbo.WASTAGE_MASTER_CC where cast(WASTAGE_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.Closing_cc_master      
                SELECT  Closing_id,closing_code,Closing_no,closing_date,closing_remarks,closing_status,Created_By,Creation_Date,Modified_By,Modified_Date,CostCenter_id,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.Closing_cc_master ccm        
                WHERE ccm.Closing_Id in ( select Closing_id from [' + @sourceDB + '].dbo.CLOSING_CC_MASTER where cast(CLOSING_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')
        INSERT  INTO [' + @destDB + '].dbo.Closing_cc_detail      
                SELECT  Closing_id,item_id,item_qty,item_rate,Current_stock,Consumption,Created_BY,Creation_Date,Modified_BY,Modified_Date,CostCenter_id,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.Closing_cc_detail ccd        
                WHERE ccd.Closing_Id in ( select Closing_id from [' + @sourceDB + '].dbo.CLOSING_CC_MASTER where cast(CLOSING_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and division_id = ''' + @DivisionId + ''')'
       
       --print @insertDynamicQuery8     
	   EXEC sys.[sp_executesql] @insertDynamicQuery8
       
       SET @insertDynamicQuery9 = 'INSERT  INTO [' + @destDB + '].dbo.SALE_INVOICE_DETAIL      
                SELECT  SI_ID,ITEM_ID,ITEM_QTY,PKT,ITEM_RATE,ITEM_AMOUNT,VAT_PER,VAT_AMOUNT,BAL_ITEM_RATE,BAL_ITEM_QTY,
						CREATED_BY,	CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,CAST(''' + @DivisionId + ''' AS INT) as DIVISION_ID,TARRIF_ID,ITEM_MRP,ITEM_MRP_DESC,
						ASSESIBLE_PER,ASSESIBLE_VALUE,EXCISE_PER,EXCISE_AMOUNT,EDU_CESS_PER,EDU_CESS_VALUE,SHE_CESS_PER,	
						SHE_CESS_VALUE,ITEM_DISCOUNT,DISCOUNT_TYPE,DISCOUNT_VALUE,GSTPaid,CessPercentage_num,CessAmount_num,	
						MRP, ACess,ACessAmount           
                FROM    [' + @sourceDB + '].dbo.SALE_INVOICE_DETAIL sid        
       WHERE  cast(sid.CREATION_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and sid.division_id = ''' + @DivisionId + '''
         
       INSERT  INTO [' + @destDB + '].dbo.SALE_INVOICE_MASTER      
                SELECT SI_ID,SI_CODE,SI_NO,DC_GST_NO,SI_DATE,CUST_ID,REMARKS,PAYMENTS_REMARKS,SALE_TYPE,
					   GROSS_AMOUNT,VAT_AMOUNT,NET_AMOUNT,IS_SAMPLE,SAMPLE_ADDRESS,VAT_CST_PER,INVOICE_STATUS,
					   DELIVERY_NOTE_NO,CREATED_BY,CREATION_DATE,MODIFIED_BY,MODIFIED_DATE,CAST(''' + @DivisionId + ''' AS INT) as DIVISION_ID,is_InvCancel,
					   VEHICLE_NO,SHIPP_ADD_ID,INV_TYPE,LR_NO,SaleExecutiveId,REQ_ID,DELIVERED_BY,TempInvoiceId,
					   PAY_STATUS,TRANSPORT,REFERENCE_ID,FLAG,CESS_AMOUNT          
       FROM    [' + @sourceDB + '].dbo.SALE_INVOICE_MASTER sim        
       WHERE  cast(sim.CREATION_DATE AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) and sim.DIVISION_ID = ''' + @DivisionId + '''
       
       INSERT  INTO [' + @destDB + '].dbo.SALE_INVOICE_STOCK_DETAIL(SI_ID,ITEM_ID,STOCK_DETAIL_ID,ITEM_QTY,NOOFCASES,ITEM_PKT_WEIGHT,DIVISION_ID)      
                SELECT  SI_ID,ITEM_ID,STOCK_DETAIL_ID,ITEM_QTY,NOOFCASES,ITEM_PKT_WEIGHT,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.SALE_INVOICE_STOCK_DETAIL sisd
                
       INSERT  INTO [' + @destDB + '].dbo.PaymentTransaction      
                SELECT  PaymentTransactionId,PaymentTransactionNo,PaymentTypeId,AccountId,PaymentDate,ChequeDraftNo ,
						ChequeDraftDate,BankId,BankDate,Remarks,TotalAmountReceived,UndistributedAmount,CancellationCharges ,
						CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,StatusId,PM_TYPE,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.PaymentTransaction pt
       WHERE  cast(pt.PaymentDate AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE)
       
       INSERT  INTO [' + @destDB + '].dbo.SettlementDetail      
                SELECT  PaymentTransactionId,InvoiceId,AmountSettled,Remarks,CreatedBy,CreatedDate,PaymentId,''' + @DivisionId + ''' as DIVISION_ID          
                FROM    [' + @sourceDB + '].dbo.SettlementDetail sd        
       WHERE  cast(sd.CreatedDate AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) AND sd.DivisionId = ''' + @DivisionId + ''''
       
       --print @insertDynamicQuery9     
	   EXEC sys.[sp_executesql] @insertDynamicQuery9
       
       
       SET @insertDynamicQuery10 = 'INSERT  INTO [' + @destDB + '].dbo.DebitNote_DETAIL      
                SELECT  DebitNote_Id,Item_ID,Item_Qty,Created_By,Creation_Date,Modified_By,Modification_Date,Stock_Detail_Id,Item_Rate,
						Item_Tax,Item_Cess,''' + @DivisionId + ''' as Division_Id          
                FROM    [' + @sourceDB + '].dbo.DebitNote_DETAIL dnd        
       WHERE dnd.DIVISION_ID = ''' + @DivisionId + ''' and cast(dnd.Creation_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) 
       
       INSERT  INTO [' + @destDB + '].dbo.DebitNote_Master      
                SELECT  DebitNote_Id,DebitNote_Code,DebitNote_No,DebitNote_Date,MRNId,Remarks,Created_by,
						Creation_Date,Modified_By,Modification_Date,DN_Amount,DN_CustId,INV_No,INV_Date,
						REFERENCE_ID,DebitNote_Type,RefNo,RefDate_dt,Tax_num,Cess_num,''' + @DivisionId + ''' as Division_Id          
                FROM    [' + @sourceDB + '].dbo.DebitNote_Master dnm        
       WHERE dnm.Division_Id = ''' + @DivisionId + ''' and cast(dnm.DebitNote_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE) 
       
       INSERT  INTO [' + @destDB + '].dbo.CreditNote_DETAIL      
                SELECT  CreditNote_Id,Item_ID,Item_Qty,Created_By,Creation_Date,Modified_By,
						Modification_Date,Stock_Detail_Id,Item_Rate,Item_Tax,Item_Cess,''' + @DivisionId + ''' as Division_Id          
                FROM    [' + @sourceDB + '].dbo.CreditNote_DETAIL cnd        
       WHERE cnd.DIVISION_ID = ''' + @DivisionId + ''' and cast(cnd.Creation_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE)  
       
       INSERT  INTO [' + @destDB + '].dbo.CreditNote_Master      
                SELECT  CreditNote_Id,CreditNote_Code,CreditNote_No,CreditNote_Date,INVId,
						Remarks,Created_by,Creation_Date,Modified_By,Modification_Date,
						CN_Amount,CN_CustId,INV_No,INV_Date,REFERENCE_ID,CreditNote_Type,
						RefNo,RefDate_dt,Tax_Amt,Cess_Amt,''' + @DivisionId + ''' as Division_Id          
                FROM    [' + @sourceDB + '].dbo.CreditNote_Master cnm        
       WHERE cnm.Division_Id = ''' + @DivisionId + ''' and cast(cnm.CreditNote_Date AS date) between CAST(''' + @fromdate + ''' AS DATE) and  CAST(''' + @todate + ''' AS DATE)'      
                    
       --print @insertDynamicQuery10     
	  EXEC sys.[sp_executesql] @insertDynamicQuery10   
        
END

Go




/****** Object:  Table [dbo].[SettlementDetail]    Script Date: 06/23/2018 13:08:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SettlementDetail](
	[PaymentTransactionId] [numeric](18, 0) NOT NULL,
	[InvoiceId] [numeric](18, 0) NOT NULL,
	[AmountSettled] [numeric](18, 2) NOT NULL,
	[Remarks] [varchar](200) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[DivisionId] [numeric](18, 0) NOT NULL,
	[PaymentId] [numeric](18, 0) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SALE_INVOICE_STOCK_DETAIL]    Script Date: 06/23/2018 13:08:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SALE_INVOICE_STOCK_DETAIL](
	[TABLE_ID] [int] IDENTITY(1,1) NOT NULL,
	[SI_ID] [int] NOT NULL,
	[ITEM_ID] [int] NOT NULL,
	[STOCK_DETAIL_ID] [int] NOT NULL,
	[ITEM_QTY] [numeric](18, 4) NULL,
	[NOOFCASES] [numeric](18, 4) NULL,
	[ITEM_PKT_WEIGHT] [numeric](18, 4) NULL,
 CONSTRAINT [PK_SALE_INVOICE_STOCK_DETAIL] PRIMARY KEY CLUSTERED 
(
	[TABLE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SALE_INVOICE_MASTER]    Script Date: 06/23/2018 13:08:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SALE_INVOICE_MASTER](
	[SI_ID] [numeric](18, 0) NOT NULL,
	[SI_CODE] [varchar](50) NULL,
	[SI_NO] [numeric](18, 0) NULL,
	[DC_GST_NO] [numeric](18, 0) NULL,
	[SI_DATE] [datetime] NULL,
	[CUST_ID] [int] NULL,
	[REMARKS] [varchar](500) NULL,
	[PAYMENTS_REMARKS] [varchar](500) NULL,
	[SALE_TYPE] [char](10) NULL,
	[GROSS_AMOUNT] [numeric](18, 2) NULL,
	[VAT_AMOUNT] [numeric](18, 2) NULL,
	[NET_AMOUNT] [numeric](18, 2) NULL,
	[IS_SAMPLE] [int] NULL,
	[SAMPLE_ADDRESS] [varchar](500) NULL,
	[VAT_CST_PER] [numeric](18, 2) NULL,
	[INVOICE_STATUS] [int] NULL,
	[DELIVERY_NOTE_NO] [varchar](50) NULL,
	[CREATED_BY] [varchar](50) NULL,
	[CREATION_DATE] [datetime] NULL,
	[MODIFIED_BY] [varchar](50) NULL,
	[MODIFIED_DATE] [datetime] NULL,
	[DIVISION_ID] [int] NULL,
	[is_InvCancel] [char](1) NULL,
	[VEHICLE_NO] [varchar](100) NULL,
	[SHIPP_ADD_ID] [int] NULL,
	[INV_TYPE] [char](1) NULL,
	[LR_NO] [varchar](100) NULL,
	[SaleExecutiveId] [numeric](18, 0) NULL,
	[REQ_ID] [numeric](18, 0) NULL,
	[DELIVERED_BY] [nvarchar](100) NULL,
	[TempInvoiceId] [numeric](18, 0) NULL,
	[PAY_STATUS] [char](10) NULL,
	[TRANSPORT] [varchar](50) NULL,
	[REFERENCE_ID] [numeric](18, 0) NOT NULL,
	[FLAG] [int] NULL,
	[CESS_AMOUNT] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_SALE_INVOICE_MASTER] PRIMARY KEY CLUSTERED 
(
	[SI_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SALE_INVOICE_DETAIL]    Script Date: 06/23/2018 13:08:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SALE_INVOICE_DETAIL](
	[SI_ID] [numeric](18, 0) NULL,
	[ITEM_ID] [numeric](18, 0) NULL,
	[ITEM_QTY] [numeric](18, 3) NULL,
	[PKT] [numeric](18, 3) NULL,
	[ITEM_RATE] [numeric](18, 2) NULL,
	[ITEM_AMOUNT] [numeric](18, 2) NULL,
	[VAT_PER] [numeric](18, 2) NULL,
	[VAT_AMOUNT] [numeric](18, 2) NULL,
	[BAL_ITEM_RATE] [numeric](18, 2) NULL,
	[BAL_ITEM_QTY] [numeric](18, 3) NULL,
	[CREATED_BY] [varchar](50) NULL,
	[CREATION_DATE] [datetime] NULL,
	[MODIFIED_BY] [varchar](50) NULL,
	[MODIFIED_DATE] [datetime] NULL,
	[DIVISION_ID] [int] NULL,
	[TARRIF_ID] [numeric](18, 0) NULL,
	[ITEM_MRP] [numeric](18, 2) NULL,
	[ITEM_MRP_DESC] [varchar](20) NULL,
	[ASSESIBLE_PER] [numeric](18, 2) NULL,
	[ASSESIBLE_VALUE] [numeric](18, 2) NULL,
	[EXCISE_PER] [numeric](18, 2) NULL,
	[EXCISE_AMOUNT] [numeric](18, 2) NULL,
	[EDU_CESS_PER] [numeric](18, 2) NULL,
	[EDU_CESS_VALUE] [numeric](18, 2) NULL,
	[SHE_CESS_PER] [numeric](18, 2) NULL,
	[SHE_CESS_VALUE] [numeric](18, 2) NULL,
	[ITEM_DISCOUNT] [numeric](18, 2) NULL,
	[DISCOUNT_TYPE] [varchar](5) NULL,
	[DISCOUNT_VALUE] [numeric](18, 2) NULL,
	[GSTPaid] [varchar](5) NULL,
	[CessPercentage_num] [numeric](18, 2) NOT NULL,
	[CessAmount_num] [numeric](18, 2) NOT NULL,
	[MRP] [numeric](18, 2) NOT NULL,
	[ACess] [numeric](18, 2) NULL,
	[ACessAmount] [numeric](18, 2) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaymentTransaction]    Script Date: 06/23/2018 13:08:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PaymentTransaction](
	[PaymentTransactionId] [numeric](18, 0) NOT NULL,
	[PaymentTransactionNo] [varchar](100) NOT NULL,
	[PaymentTypeId] [numeric](18, 0) NOT NULL,
	[AccountId] [numeric](18, 0) NULL,
	[PaymentDate] [datetime] NULL,
	[ChequeDraftNo] [varchar](50) NULL,
	[ChequeDraftDate] [datetime] NULL,
	[BankId] [numeric](18, 0) NULL,
	[BankDate] [datetime] NULL,
	[Remarks] [varchar](200) NULL,
	[TotalAmountReceived] [numeric](18, 2) NULL,
	[UndistributedAmount] [numeric](18, 2) NULL,
	[CancellationCharges] [numeric](18, 2) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[DivisionId] [bigint] NULL,
	[StatusId] [int] NULL,
	[PM_TYPE] [numeric](18, 0) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DebitNote_Master]    Script Date: 06/23/2018 13:08:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DebitNote_Master](
	[DebitNote_Id] [numeric](18, 0) NOT NULL,
	[DebitNote_Code] [varchar](20) NULL,
	[DebitNote_No] [numeric](18, 0) NULL,
	[DebitNote_Date] [datetime] NULL,
	[MRNId] [numeric](18, 0) NULL,
	[Remarks] [varchar](255) NULL,
	[Created_by] [varchar](100) NULL,
	[Creation_Date] [datetime] NULL,
	[Modified_By] [varchar](100) NULL,
	[Modification_Date] [datetime] NULL,
	[Division_Id] [numeric](18, 0) NULL,
	[DN_Amount] [numeric](18, 2) NULL,
	[DN_CustId] [numeric](18, 0) NULL,
	[INV_No] [varchar](100) NULL,
	[INV_Date] [datetime] NULL,
	[REFERENCE_ID] [numeric](18, 0) NOT NULL,
	[DebitNote_Type] [varchar](50) NULL,
	[RefNo] [varchar](50) NULL,
	[RefDate_dt] [datetime] NULL,
	[Tax_num] [numeric](18, 2) NULL,
	[Cess_num] [numeric](18, 2) NULL,
 CONSTRAINT [PK_DebitNote_Master] PRIMARY KEY CLUSTERED 
(
	[DebitNote_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DebitNote_DETAIL]    Script Date: 06/23/2018 13:08:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DebitNote_DETAIL](
	[DebitNote_Id] [numeric](18, 0) NULL,
	[Item_ID] [numeric](18, 0) NULL,
	[Item_Qty] [numeric](18, 4) NULL,
	[Created_By] [varchar](100) NULL,
	[Creation_Date] [datetime] NULL,
	[Modified_By] [varchar](100) NULL,
	[Modification_Date] [datetime] NULL,
	[Division_Id] [int] NULL,
	[Stock_Detail_Id] [numeric](18, 0) NULL,
	[Item_Rate] [numeric](18, 2) NOT NULL,
	[Item_Tax] [numeric](18, 2) NOT NULL,
	[Item_Cess] [numeric](18, 2) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CreditNote_Master]    Script Date: 06/23/2018 13:08:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CreditNote_Master](
	[CreditNote_Id] [numeric](18, 0) NOT NULL,
	[CreditNote_Code] [varchar](20) NULL,
	[CreditNote_No] [numeric](18, 0) NULL,
	[CreditNote_Date] [datetime] NULL,
	[INVId] [numeric](18, 0) NULL,
	[Remarks] [varchar](255) NULL,
	[Created_by] [varchar](100) NULL,
	[Creation_Date] [datetime] NULL,
	[Modified_By] [varchar](100) NULL,
	[Modification_Date] [datetime] NULL,
	[Division_Id] [numeric](18, 0) NULL,
	[CN_Amount] [numeric](18, 2) NOT NULL,
	[CN_CustId] [numeric](18, 0) NOT NULL,
	[INV_No] [varchar](100) NULL,
	[INV_Date] [datetime] NULL,
	[REFERENCE_ID] [numeric](18, 0) NOT NULL,
	[CreditNote_Type] [varchar](50) NULL,
	[RefNo] [varchar](50) NULL,
	[RefDate_dt] [datetime] NULL,
	[Tax_Amt] [numeric](18, 2) NULL,
	[Cess_Amt] [numeric](18, 2) NULL,
 CONSTRAINT [PK_CreditNote_Master] PRIMARY KEY CLUSTERED 
(
	[CreditNote_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CreditNote_DETAIL]    Script Date: 06/23/2018 13:08:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CreditNote_DETAIL](
	[CreditNote_Id] [numeric](18, 0) NULL,
	[Item_ID] [numeric](18, 0) NULL,
	[Item_Qty] [numeric](18, 4) NULL,
	[Created_By] [varchar](100) NULL,
	[Creation_Date] [datetime] NULL,
	[Modified_By] [varchar](100) NULL,
	[Modification_Date] [datetime] NULL,
	[Division_Id] [int] NULL,
	[Stock_Detail_Id] [numeric](18, 0) NULL,
	[Item_Rate] [numeric](18, 2) NOT NULL,
	[Item_Tax] [numeric](18, 2) NOT NULL,
	[Item_Cess] [numeric](18, 2) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF__CreditNot__Item___0F824689]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[CreditNote_DETAIL] ADD  DEFAULT ((0.00)) FOR [Item_Cess]
GO
/****** Object:  Default [DF__CreditNot__REFER__0CDAE408]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[CreditNote_Master] ADD  DEFAULT ((10071)) FOR [REFERENCE_ID]
GO
/****** Object:  Default [DF__CreditNot__Cess___0E8E2250]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[CreditNote_Master] ADD  DEFAULT ((0.00)) FOR [Cess_Amt]
GO
/****** Object:  Default [DF__DebitNote__Item___116A8EFB]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[DebitNote_DETAIL] ADD  DEFAULT ((0.00)) FOR [Item_Cess]
GO
/****** Object:  Default [DF__DebitNote__REFER__0AF29B96]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[DebitNote_Master] ADD  DEFAULT ((10070)) FOR [REFERENCE_ID]
GO
/****** Object:  Default [DF__DebitNote__Cess___10766AC2]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[DebitNote_Master] ADD  DEFAULT ((0.00)) FOR [Cess_num]
GO
/****** Object:  Default [DF__PaymentTr__PM_TY__5F141958]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[PaymentTransaction] ADD  DEFAULT ((0)) FOR [PM_TYPE]
GO
/****** Object:  Default [DF_SALE_INVOICE_DETAIL_PKT]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[SALE_INVOICE_DETAIL] ADD  CONSTRAINT [DF_SALE_INVOICE_DETAIL_PKT]  DEFAULT ((0)) FOR [PKT]
GO
/****** Object:  Default [DF_SALE_INVOICE_DETAIL_CessPercentage_num]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[SALE_INVOICE_DETAIL] ADD  CONSTRAINT [DF_SALE_INVOICE_DETAIL_CessPercentage_num]  DEFAULT ((0)) FOR [CessPercentage_num]
GO
/****** Object:  Default [DF_SALE_INVOICE_DETAIL_CessAmount_num]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[SALE_INVOICE_DETAIL] ADD  CONSTRAINT [DF_SALE_INVOICE_DETAIL_CessAmount_num]  DEFAULT ((0)) FOR [CessAmount_num]
GO
/****** Object:  Default [DF_SALE_INVOICE_DETAIL_MRP]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[SALE_INVOICE_DETAIL] ADD  CONSTRAINT [DF_SALE_INVOICE_DETAIL_MRP]  DEFAULT ((0.00)) FOR [MRP]
GO
/****** Object:  Default [DF__SALE_INVO__ACess__19FFD4FC]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[SALE_INVOICE_DETAIL] ADD  DEFAULT ((0)) FOR [ACess]
GO
/****** Object:  Default [DF__SALE_INVO__ACess__1AF3F935]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[SALE_INVOICE_DETAIL] ADD  DEFAULT ((0)) FOR [ACessAmount]
GO
/****** Object:  Default [DF_SALE_INVOICE_MASTER_INVOICE_STATUS]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[SALE_INVOICE_MASTER] ADD  CONSTRAINT [DF_SALE_INVOICE_MASTER_INVOICE_STATUS]  DEFAULT ((1)) FOR [INVOICE_STATUS]
GO
/****** Object:  Default [DF_SALE_INVOICE_MASTER_is_InvCancel]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[SALE_INVOICE_MASTER] ADD  CONSTRAINT [DF_SALE_INVOICE_MASTER_is_InvCancel]  DEFAULT ('N') FOR [is_InvCancel]
GO
/****** Object:  Default [DF__SALE_INVO__REFER__0BE6BFCF]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[SALE_INVOICE_MASTER] ADD  DEFAULT ((10071)) FOR [REFERENCE_ID]
GO
/****** Object:  Default [DF__SALE_INVO__CESS___05F8DC4F]    Script Date: 06/23/2018 13:08:29 ******/
ALTER TABLE [dbo].[SALE_INVOICE_MASTER] ADD  DEFAULT ((0.00)) FOR [CESS_AMOUNT]
GO

