insert INTO dbo.DBScriptUpdateLog
        ( LogFileName, ExecuteDateTime )
VALUES  ( '0003_05_Feb_2018_Update_Stock_Adjustment',
          GETDATE()
         )
------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE	PROCEDURE Update_Stock_Detail_Adjustment
    @item_ID INT ,
    @Batch_no NVARCHAR(100) ,
    @Expiry_Date DATETIME ,
    @Item_Qty NUMERIC(18, 4) ,
    @Doc_ID INT ,
    @Transaction_ID INT ,
    @STOCKDETAIL_ID INT OUTPUT
AS
    BEGIN  
    
        UPDATE  STOCK_DETAIL
        SET     Item_Qty = Item_Qty + @Item_Qty ,
                Balance_Qty = Balance_Qty + @Item_Qty
        WHERE   STOCK_DETAIL_ID = @STOCKDETAIL_ID
                AND item_id = @item_id   
    END

-----------------------------------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECEIVED_WITHOUT_PO_DETAIL]
    (
      @v_Received_ID NUMERIC(18, 0) ,
      @v_Item_ID NUMERIC(18, 0) ,
      @v_Item_Qty NUMERIC(18, 4) ,
      @v_Item_Rate NUMERIC(18, 4) ,
      @v_Created_By VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Division_Id INT ,
      @v_Item_vat NUMERIC(18, 4) ,
      @v_Item_exice NUMERIC(18, 4) ,
      @v_Batch_no VARCHAR(50) ,
      @v_Expiry_Date DATETIME ,
      @V_Trans_Type NUMERIC(18, 0) ,
      @v_Proc_Type INT ,
      @v_DType CHAR(1) ,
      @v_DiscountValue NUMERIC(18, 2)
    )
AS
    BEGIN            
            
            
        DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)       
        IF @V_PROC_TYPE = 1
            BEGIN            
                           
                           
                  
                SELECT  @STOCK_DETAIL_ID = MAX(STOCK_DETAIL_ID)
                FROM    dbo.STOCK_DETAIL
                WHERE   Item_id = @v_Item_ID                   
                                
                --it will insert entry in stock detail table and         
                --return stock_detail_id.                
                EXEC Update_Stock_Detail_Adjustment @v_Item_ID, @v_Batch_no,
                    @v_Expiry_Date, @v_Item_Qty, @v_Received_ID, @V_Trans_Type,
                    @STOCK_DETAIL_ID OUTPUT        
                     
                     
                     
                INSERT  INTO MATERIAL_RECEIVED_WITHOUT_PO_DETAIL
                        ( Received_ID ,
                          Item_ID ,
                          Item_Qty ,
                          Item_Rate ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_Id ,
                          Item_vat ,
                          Item_exice ,
                          BAtch_No ,
                          Expiry_Date ,
                          Bal_Item_Qty ,
                          Bal_Item_Rate ,
                          Bal_Item_Vat ,
                          Bal_Item_Exice ,
                          Stock_Detail_Id ,
                          DType ,
                          DiscountValue                  
                        )
                VALUES  ( @V_Received_ID ,
                          @V_Item_ID ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_Id ,
                          @V_Item_vat ,
                          @V_Item_exice ,
                          @v_Batch_no ,
                          @v_Expiry_Date ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @V_Item_vat ,
                          @V_Item_exice ,
                          @STOCK_DETAIL_ID ,
                          @v_DType ,
                          @v_DiscountValue           
                        )                     
                       
                    
                --Inserting Transaction Log       
                INSERT  INTO dbo.TransactionLog_Master
                        ( Item_id ,
                          Issue_Qty ,
                          Receive_Qty ,
                          Transaction_Date ,
                          TransactionType_Id ,
                          Transaction_Id ,
                          Division_Id    
                        )
                VALUES  ( @V_Item_ID ,
                          0 ,
                          @V_Item_Qty ,
                          @V_Creation_Date ,
                          2 ,
                          @V_Received_ID ,
                          @V_Division_Id    
                        )    
                        
                --it will insert entry in transaction log with stock_detail_id        
                EXEC INSERT_TRANSACTION_LOG @v_Received_ID, @v_Item_ID,
                    @V_Trans_Type, @STOCK_DETAIL_ID, @v_Item_Qty,
                    @v_Creation_Date, 0  
                                    
            END            
            
        IF @V_PROC_TYPE = 2
            BEGIN    
                SELECT  @STOCK_DETAIL_ID = MAX(STOCK_DETAIL_ID)
                FROM    dbo.STOCK_DETAIL
                WHERE   Item_id = @v_Item_ID                   
                                
                --it will insert entry in stock detail table and         
                --return stock_detail_id.                
                EXEC Update_Stock_Detail_Adjustment @v_Item_ID, @v_Batch_no,
                    @v_Expiry_Date, @v_Item_Qty, @v_Received_ID, @V_Trans_Type,
                    @STOCK_DETAIL_ID OUTPUT        
                     
                     
                     
                INSERT  INTO MATERIAL_RECEIVED_WITHOUT_PO_DETAIL
                        ( Received_ID ,
                          Item_ID ,
                          Item_Qty ,
                          Item_Rate ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_Id ,
                          Item_vat ,
                          Item_exice ,
                          BAtch_No ,
                          Expiry_Date ,
                          Bal_Item_Qty ,
                          Bal_Item_Rate ,
                          Bal_Item_Vat ,
                          Bal_Item_Exice ,
                          Stock_Detail_Id ,
                          DType ,
                          DiscountValue                  
                        )
                VALUES  ( @V_Received_ID ,
                          @V_Item_ID ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_Id ,
                          @V_Item_vat ,
                          @V_Item_exice ,
                          @v_Batch_no ,
                          @v_Expiry_Date ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @V_Item_vat ,
                          @V_Item_exice ,
                          @STOCK_DETAIL_ID ,
                          @v_DType ,
                          @v_DiscountValue           
                        )                
            END              
            
    END 

-------------------------------------------------------------------------------------------------------------------------------------

ALTER PROC [dbo].[PROC_ACCEPT_STOCK_TRANSFER_DETAIL]  
    @Transfer_ID INT,  
    @ITEM_ID INT,  
    @ITEM_QTY NUMERIC(18, 3),  
    @TRANSFER_RATE NUMERIC(18, 2),  
    @Created_By NVARCHAR(100),  
    @Creation_Date DATETIME,  
    @Modified_By NVARCHAR(100),  
    @Modification_Date DATETIME,  
    @Division_ID INT,  
    @STOCK_DETAIL_ID INT,  
    @BATCH_NO VARCHAR(20),  
    @EXPIRY_DATE DATETIME,  
    @transaction_id INT  
AS   
    BEGIN                
                
        DECLARE @ACCEPTED_STOCK_DETAIL_ID NUMERIC(18, 0)    
         --it will insert entry in stock detail table and         
        --return stock_detail_id.  
        
         
                SELECT  @ACCEPTED_STOCK_DETAIL_ID = MAX(STOCK_DETAIL_ID)
                FROM    dbo.STOCK_DETAIL WHERE Item_id=@ITEM_ID     
          
          
          
        EXEC Update_Stock_Detail_Adjustment @ITEM_ID, @BATCH_NO, @EXPIRY_DATE, @item_qty,  
            @Transfer_ID, @transaction_id, @ACCEPTED_STOCK_DETAIL_ID OUTPUT    
    
                
        INSERT  INTO STOCK_TRANSFER_DETAIL  
                (  
                  TRANSFER_ID,  
                  ITEM_ID,  
                  ITEM_QTY,  
                  ACCEPTED_QTY,  
                  RETURNED_QTY,  
                  CREATED_BY,  
                  CREATION_DATE,  
                  MODIFIED_BY,  
                  MODIFIED_DATE,  
                  DIVISION_ID,  
                  TRANSFER_RATE,  
                  STOCK_DETAIL_ID,  
                  ACCEPTED_STOCK_DETAIL_ID,  
                  BATCH_NO,  
                  EXPIRY_DATE                
                            
                )  
        VALUES  (  
                  @Transfer_ID,  
                  @ITEM_ID,  
                  @ITEM_QTY,  
                  @ITEM_QTY,  
                  0,  
                  @Created_By,  
                  @Creation_Date,  
                  @Modified_By,  
                  @Modification_Date,  
                  @Division_ID,  
                  @TRANSFER_RATE,  
                  @STOCK_DETAIL_ID,  
                  @ACCEPTED_STOCK_DETAIL_ID,  
                  @BATCH_NO,  
                  @EXPIRY_DATE                
                            
                )        
                    
                    
                  --it will insert entry in transaction log with stock_detail_id        
        EXEC INSERT_TRANSACTION_LOG @Transfer_ID, @ITEM_ID, @transaction_id,  
            @ACCEPTED_STOCK_DETAIL_ID, @ITEM_QTY, @Creation_Date, 0    
                        
        RETURN @ACCEPTED_STOCK_DETAIL_ID    
                          
    END 

-------------------------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECEIVED_WITHOUT_PO_DETAIL]
    (
      @v_Received_ID NUMERIC(18, 0) ,
      @v_Item_ID NUMERIC(18, 0) ,
      @v_Item_Qty NUMERIC(18, 4) ,
      @v_Item_Rate NUMERIC(18, 4) ,
      @v_Created_By VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Division_Id INT ,
      @v_Item_vat NUMERIC(18, 4) ,
      @v_Item_exice NUMERIC(18, 4) ,
      @v_Batch_no VARCHAR(50) ,
      @v_Expiry_Date DATETIME ,
      @V_Trans_Type NUMERIC(18, 0) ,
      @v_Proc_Type INT ,
      @v_DType CHAR(1) ,
      @v_DiscountValue NUMERIC(18, 2)
    )
AS
    BEGIN            
            
            
        DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)       
        IF @V_PROC_TYPE = 1
            BEGIN            
                           
                           
                  
                SELECT  @STOCK_DETAIL_ID = MAX(STOCK_DETAIL_ID)
                FROM    dbo.STOCK_DETAIL
                WHERE   Item_id = @v_Item_ID                   
                                
                --it will insert entry in stock detail table and         
                --return stock_detail_id.                
                EXEC Update_Stock_Detail_Adjustment @v_Item_ID, @v_Batch_no,
                    @v_Expiry_Date, @v_Item_Qty, @v_Received_ID, @V_Trans_Type,
                    @STOCK_DETAIL_ID OUTPUT        
                     
                     
                     
                INSERT  INTO MATERIAL_RECEIVED_WITHOUT_PO_DETAIL
                        ( Received_ID ,
                          Item_ID ,
                          Item_Qty ,
                          Item_Rate ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_Id ,
                          Item_vat ,
                          Item_exice ,
                          BAtch_No ,
                          Expiry_Date ,
                          Bal_Item_Qty ,
                          Bal_Item_Rate ,
                          Bal_Item_Vat ,
                          Bal_Item_Exice ,
                          Stock_Detail_Id ,
                          DType ,
                          DiscountValue                  
                        )
                VALUES  ( @V_Received_ID ,
                          @V_Item_ID ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_Id ,
                          @V_Item_vat ,
                          @V_Item_exice ,
                          @v_Batch_no ,
                          @v_Expiry_Date ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @V_Item_vat ,
                          @V_Item_exice ,
                          @STOCK_DETAIL_ID ,
                          @v_DType ,
                          @v_DiscountValue           
                        )                     
                       
                    
                --Inserting Transaction Log       
                INSERT  INTO dbo.TransactionLog_Master
                        ( Item_id ,
                          Issue_Qty ,
                          Receive_Qty ,
                          Transaction_Date ,
                          TransactionType_Id ,
                          Transaction_Id ,
                          Division_Id    
                        )
                VALUES  ( @V_Item_ID ,
                          0 ,
                          @V_Item_Qty ,
                          @V_Creation_Date ,
                          2 ,
                          @V_Received_ID ,
                          @V_Division_Id    
                        )    
                        
                --it will insert entry in transaction log with stock_detail_id        
                EXEC INSERT_TRANSACTION_LOG @v_Received_ID, @v_Item_ID,
                    @V_Trans_Type, @STOCK_DETAIL_ID, @v_Item_Qty,
                    @v_Creation_Date, 0  
                                    
            END            
            
        IF @V_PROC_TYPE = 2
            BEGIN    
                SELECT  @STOCK_DETAIL_ID = MAX(STOCK_DETAIL_ID)
                FROM    dbo.STOCK_DETAIL
                WHERE   Item_id = @v_Item_ID                   
                                
                --it will insert entry in stock detail table and         
                --return stock_detail_id.                
                EXEC Update_Stock_Detail_Adjustment @v_Item_ID, @v_Batch_no,
                    @v_Expiry_Date, @v_Item_Qty, @v_Received_ID, @V_Trans_Type,
                    @STOCK_DETAIL_ID OUTPUT        
                     
                     
                     
                INSERT  INTO MATERIAL_RECEIVED_WITHOUT_PO_DETAIL
                        ( Received_ID ,
                          Item_ID ,
                          Item_Qty ,
                          Item_Rate ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_Id ,
                          Item_vat ,
                          Item_exice ,
                          BAtch_No ,
                          Expiry_Date ,
                          Bal_Item_Qty ,
                          Bal_Item_Rate ,
                          Bal_Item_Vat ,
                          Bal_Item_Exice ,
                          Stock_Detail_Id ,
                          DType ,
                          DiscountValue                  
                        )
                VALUES  ( @V_Received_ID ,
                          @V_Item_ID ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_Id ,
                          @V_Item_vat ,
                          @V_Item_exice ,
                          @v_Batch_no ,
                          @v_Expiry_Date ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @V_Item_vat ,
                          @V_Item_exice ,
                          @STOCK_DETAIL_ID ,
                          @v_DType ,
                          @v_DiscountValue           
                        )                
            END              
            
    END 

-------------------------------------------------------------------------------------------------------------------------------------------------

ALTER PROC [dbo].[ProcReverseInvoiceEntry]
    (
      @V_SI_ID NUMERIC(18, 0) ,
      @V_CUST_ID NUMERIC(18, 0)
    )
AS
    BEGIN    
    
    
        SET @V_CUST_ID = ( SELECT   CUST_ID
                           FROM     dbo.SALE_INVOICE_MASTER
                           WHERE    SI_ID = @V_SI_ID
                         )
        UPDATE  STOCK_DETAIL
        SET     STOCK_DETAIL.Issue_Qty = ( STOCK_DETAIL.Issue_Qty
                                           - SALE_INVOICE_STOCK_DETAIL.ITEM_QTY ) ,
                STOCK_DETAIL.Balance_Qty = ( STOCK_DETAIL.Balance_Qty
                                             + SALE_INVOICE_STOCK_DETAIL.ITEM_QTY )
        FROM    dbo.STOCK_DETAIL
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL ON SALE_INVOICE_STOCK_DETAIL.STOCK_DETAIL_ID = STOCK_DETAIL.STOCK_DETAIL_ID
                                                      AND SALE_INVOICE_STOCK_DETAIL.ITEM_ID = STOCK_DETAIL.Item_id
        WHERE   SI_ID = @V_SI_ID    
    
    
        DELETE  FROM dbo.SALE_INVOICE_DETAIL
        WHERE   SI_ID = @V_SI_ID    
    
    
        DELETE  FROM dbo.SALE_INVOICE_STOCK_DETAIL
        WHERE   SI_ID = @V_SI_ID    
    
    
    
        DECLARE @CashOut NUMERIC(18, 2)    
        DECLARE @CashIn NUMERIC(18, 2)    
        SET @CashOut = 0    
    
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = @V_CUST_ID
                      )    
    
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @V_CUST_ID    
    
    
    
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = 10073
                      )
    
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10073    
    
    
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = 10071
                       )    
    
        SET @CashIn = 0    
    
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10071    
    
    
    
    
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = 10054
                       )    
    
    
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10054    
        
        
    
    
        SET @CashOut = 0    
    
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = 10054
                      )    
    
    
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10054    
    
    
    
    
        DECLARE @v_INV_TYPE VARCHAR(1)    
        SET @v_INV_TYPE = ( SELECT  INV_TYPE
                            FROM    dbo.SALE_INVOICE_MASTER
                            WHERE   SI_ID = @v_SI_ID
                          )    
    
        DECLARE @CInputID NUMERIC    
        SET @CInputID = 10017    
        DECLARE @InputID NUMERIC    
        DECLARE @CGST_Amount NUMERIC(18, 2)    
      
     SET @CashOut = 0
     SET @CashIn = 0
    
        SET @InputID = ( SELECT CASE WHEN @v_INV_TYPE = 'I' THEN 10021
                                     WHEN @v_INV_TYPE = 'S' THEN 10024
                                     WHEN @v_INV_TYPE = 'U' THEN 10075
                                END AS inputid
                       )    
    
        IF @v_INV_TYPE <> 'I'
            BEGIN    
    
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @v_SI_ID
                                        AND TransactionTypeId = 16
                                        AND AccountId = @CInputID
                               )    
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @CInputID    
    
    
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @v_SI_ID
                                        AND TransactionTypeId = 16
                                        AND AccountId = @InputID
                               )    
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @InputID    
            END    
        ELSE
            BEGIN    
    
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @v_SI_ID
                                        AND TransactionTypeId = 16
                                        AND AccountId = @InputID
                               )    
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @InputID    
            END     
    
    
    
        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @v_SI_ID
                AND TransactionTypeId = 16    
    END

-----------------------------------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECIEVED_WITHOUT_PO_MASTER]
    (
      @v_Received_ID NUMERIC(18, 0) ,
      @v_Received_Code VARCHAR(20) ,
      @v_Received_No NUMERIC(18, 0) ,
      @v_Received_Date DATETIME ,
      @v_Purchase_Type INT ,
      @v_Vendor_ID INT ,
      @v_Remarks VARCHAR(100) ,
      @v_Po_ID NUMERIC(18, 0) ,
      @v_MRN_PREFIX VARCHAR(50) ,
      @v_MRN_NO NUMERIC(18, 0) ,
      @v_Created_By VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Invoice_No NVARCHAR(50) ,
      @v_Invoice_Date DATETIME ,
      @v_Division_ID INT ,
      @v_mrn_status INT ,
      @v_freight NUMERIC(18, 2) = NULL ,
      @v_freight_type CHAR = '' ,
      @v_Proc_Type INT ,
      @v_Other_charges NUMERIC(18, 2) = NULL ,
      @v_Discount_amt NUMERIC(18, 2) = NULL ,
      @v_GROSS_AMOUNT NUMERIC(18, 2) ,
      @v_GST_AMOUNT NUMERIC(18, 2) ,
      @v_NET_AMOUNT NUMERIC(18, 2) ,
      @V_MRN_TYPE INT ,
      @V_VAT_ON_EXICE INT ,
      @v_MRNCompanies_ID INT      
    
    )
AS
    BEGIN     
        DECLARE @Remarks VARCHAR(250)    
        DECLARE @InputID NUMERIC    
        DECLARE @CInputID NUMERIC    
        SET @CInputID = 10016    
        DECLARE @RoundOff NUMERIC(18, 2)    
        DECLARE @CGST_Amount NUMERIC(18, 2)    
        SET @CGST_Amount = ( @v_GST_AMOUNT / 2 )    
        SET @RoundOff = ( @v_freight + @v_Other_Charges )    
    
        SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10020
                                     WHEN @V_MRN_TYPE = 2 THEN 10023
                                     WHEN @V_MRN_TYPE = 3 THEN 10074
                                END AS inputid
                       )   
        IF @V_PROC_TYPE = 1
            BEGIN      
    
                INSERT  INTO MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                        ( Received_ID ,
                          Received_Code ,
                          Received_No ,
                          Received_Date ,
                          Purchase_Type ,
                          Vendor_ID ,
                          Remarks ,
                          Po_ID ,
                          MRN_PREFIX ,
                          MRN_NO ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Invoice_No ,
                          Invoice_Date ,
                          Division_ID ,
                          mrn_status ,
                          freight ,
                          Other_charges ,
                          Discount_amt ,
                          freight_type ,
                          MRNCompanies_ID ,
                          GROSS_AMOUNT ,
                          GST_AMOUNT ,
                          NET_AMOUNT ,
                          MRN_TYPE ,
                          VAT_ON_EXICE ,
                          IsPrinted    
                        )
                VALUES  ( @V_Received_ID ,
                          @V_Received_Code ,
                          @V_Received_No ,
                          @V_Received_Date ,
                          @V_Purchase_Type ,
                          @V_Vendor_ID ,
                          @V_Remarks ,
                          @V_Po_ID ,
                          @V_MRN_PREFIX ,
                          @V_MRN_NO ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @v_Invoice_No ,
                          @v_Invoice_Date ,
                          @V_Division_ID ,
                          @V_mrn_status ,
                          @v_freight ,
                          @v_Other_charges ,
                          @v_Discount_amt ,
                          @v_freight_type ,
                          @v_MRNCompanies_ID ,
                          @v_GROSS_AMOUNT ,
                          @v_GST_AMOUNT ,
                          @v_NET_AMOUNT ,
                          @v_MRN_TYPE ,
                          @V_VAT_ON_EXICE ,
                          0      
    
                        )      
    
    
                UPDATE  MRN_SERIES
                SET     CURRENT_USED = CURRENT_USED + 1
                WHERE   DIV_ID = @v_Division_ID      
    
                SET @Remarks = 'Pruchase against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)     
    
    
                EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2, @V_Created_BY      
    
    
    
                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @V_Created_BY      
    
    
                SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)     
    
                IF @V_MRN_TYPE <> 2
                    BEGIN    
    
    
    
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY    
    
    
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @v_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY    
                    END    
    
    
    
                ELSE
                    BEGIN    
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY    
                    END       
    
    
                SET @Remarks = 'Round Off against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)    
    
                IF @RoundOff > 0
                    BEGIN    
    
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY    
    
                    END    
    
                ELSE
                    BEGIN    
    
                        SET @RoundOff = -+@RoundOff    
    
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY    
    
                    END     
    
    
                SET @Remarks = 'Stock In against party  invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)    
    
    
    
                EXECUTE Proc_Ledger_Insert 10073, @v_NET_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @V_Created_BY    
    
    
            END       
    
  
      
        IF @V_PROC_TYPE = 2
            BEGIN      
              
              
               EXEC Proc_ReverseMRNEntry @V_Received_ID,@V_Vendor_ID
              
              
                UPDATE  MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                SET     Purchase_Type = @V_Purchase_Type ,
                        Vendor_ID = @V_Vendor_ID ,
                        Remarks = @V_Remarks ,
                        Modified_By = @V_Modified_By ,
                        Modification_Date = @V_Modification_Date ,
                        Invoice_No = @v_Invoice_No ,
                        Invoice_Date = @v_Invoice_Date ,
                        mrn_status = @V_mrn_status ,
                        freight = @v_freight ,
                        Other_charges = @v_Other_charges ,
                        Discount_amt = @v_Discount_amt ,
                        freight_type = @v_freight_type ,
                        MRNCompanies_ID = @v_MRNCompanies_ID ,
                        GROSS_AMOUNT = @v_GROSS_AMOUNT ,
                        GST_AMOUNT = @v_GST_AMOUNT ,
                        NET_AMOUNT = @v_NET_AMOUNT ,
                        MRN_TYPE = @v_MRN_TYPE ,
                        VAT_ON_EXICE = @V_VAT_ON_EXICE
                WHERE   Received_ID = @V_Received_ID   
              
            
                 
    
    
    
    
                SET @Remarks = 'Pruchase against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)     
    
    
                EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2, @V_Created_BY      
    
    
    
                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @V_Created_BY      
    
    
                SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)     
    
                IF @V_MRN_TYPE <> 2
                    BEGIN    
    
    
    
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY    
    
    
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @v_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY    
                    END    
    
    
    
                ELSE
                    BEGIN    
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY    
                    END       
    
    
                SET @Remarks = 'Round Off against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)    
    
                IF @RoundOff > 0
                    BEGIN    
    
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY    
    
                    END    
    
                ELSE
                    BEGIN    
    
                        SET @RoundOff = -+@RoundOff    
    
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY    
    
                    END     
    
    
                SET @Remarks = 'Stock In against party  invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)    
    
    
    
                EXECUTE Proc_Ledger_Insert 10073, @v_NET_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @V_Created_BY  
    
            END    
    END
-------------------------------------------------------------------------------------------------------------------------------------------
ALTER PROC Proc_ReverseMRNEntry
    (
      @V_Received_ID NUMERIC(18, 0) ,
      @V_Vendor_ID NUMERIC(18, 0)
    )
AS
    BEGIN
    
        SET @V_Vendor_ID = ( SELECT Vendor_ID
                             FROM   dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                             WHERE  Received_ID = @V_Received_ID
                           )
                           
                                                                                 
       
       
       
        UPDATE  STOCK_DETAIL
        SET     STOCK_DETAIL.Item_Qty = ( STOCK_DETAIL.Item_Qty
                                          - MATERIAL_RECEIVED_WITHOUT_PO_DETAIL.ITEM_QTY ) ,
                STOCK_DETAIL.Balance_Qty = ( STOCK_DETAIL.Balance_Qty
                                             - MATERIAL_RECEIVED_WITHOUT_PO_DETAIL.ITEM_QTY )
        FROM    dbo.STOCK_DETAIL
                JOIN dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL ON MATERIAL_RECEIVED_WITHOUT_PO_DETAIL.STOCK_DETAIL_ID = STOCK_DETAIL.STOCK_DETAIL_ID
                                                              AND MATERIAL_RECEIVED_WITHOUT_PO_DETAIL.ITEM_ID = STOCK_DETAIL.Item_id
        WHERE   Received_ID = @V_Received_ID  
        
          
  
        DELETE  FROM dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL
        WHERE   Received_ID = @V_Received_ID
        
        
        
        DECLARE @CashOut NUMERIC(18, 2)  
        DECLARE @CashIn NUMERIC(18, 2) 
        
        SET @CashIn=0
        
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @V_Received_ID
                                AND TransactionTypeId = 2
                                AND AccountId = @V_Vendor_ID
                       )  
  
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @V_Vendor_ID
        
        
        
        
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @V_Received_ID
                                AND TransactionTypeId = 2
                                AND AccountId = 10073
                       )  
     
    
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10073    
        
        
        
        
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @V_Received_ID
                                AND TransactionTypeId = 2
                                AND AccountId = 10070
                      )    
    
        SET @CashOut = 0    
    
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10070
        
           
           
           
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @V_Received_ID
                                AND TransactionTypeId = 2
                                AND AccountId = 10054
                      )    
    
        SET @CashOut = 0    
    
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10054
           
        SET @CashIn = 0
        SET @CashOut = ( SELECT ISNULL(SUM(Cashin), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @V_Received_ID
                                AND TransactionTypeId = 2
                                AND AccountId = 10054
                       )    
    
   
    
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10054
        
        SET @CashIn = 0
        SET @CashOut = 0
       
          DECLARE @V_MRN_TYPE VARCHAR(1)    
        SET @V_MRN_TYPE = ( SELECT  MRN_TYPE
                            FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                            WHERE   Received_ID = @V_Received_ID
                          )    
        
        DECLARE @InputID NUMERIC    
        DECLARE @CInputID NUMERIC    
        SET @CInputID = 10016  
        
           SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10020
                                     WHEN @V_MRN_TYPE = 2 THEN 10023
                                     WHEN @V_MRN_TYPE = 3 THEN 10074
                                END AS inputid
                       ) 
                       
                       
                       
            IF @V_MRN_TYPE <> 2
                    BEGIN    
                     SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @V_Received_ID
                                AND TransactionTypeId = 2
                                AND AccountId= @CInputID
                               )    
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @CInputID    
    
    
                SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @V_Received_ID
                                AND TransactionTypeId = 2
                                AND AccountId = @InputID
                               )    
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @InputID    
                    end
         ELSE
            BEGIN    
    
                SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @V_Received_ID
                                AND TransactionTypeId = 2
                                AND AccountId = @InputID
                               )    
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @InputID    
            END 
        
         DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId =@V_Received_ID
                AND TransactionTypeId = 2  
    END

----------------------------------------------------------------------------------------------------------------------------------------------------

