INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0044_08_Apr_2019_AddConsumer' ,
          GETDATE()
        )
Go
---------------------------------------------------------------------------------------------------------------------------
ALTER TABLE dbo.SALE_INVOICE_MASTER ADD ConsumerHeadID NUMERIC(18,2) DEFAULT 0,
freight NUMERIC(18,2) DEFAULT 0,freight_type CHAR(10),FreightTaxApplied BIT DEFAULT 0,
FreightTaxValue   NUMERIC(18,2) DEFAULT 0

Go
---------------------------------------------------------------------------------------------------------------------------
ALTER PROC [dbo].[PROC_Cancel_Sale_Invoice]
    (
      @SI_ID AS INT ,
      @status AS INT ,
      @userName AS VARCHAR(50)
    )
AS
    BEGIN        
        UPDATE  dbo.SALE_INVOICE_MASTER
        SET     INVOICE_STATUS = @status ,
                MODIFIED_DATE = GETDATE() ,
                MODIFIED_BY = @userName
        WHERE   SI_ID = @SI_ID        
        UPDATE  dbo.STOCK_DETAIL
        SET     STOCK_DETAIL.Issue_Qty = ( STOCK_DETAIL.Issue_Qty
                                           - SALE_INVOICE_STOCK_DETAIL.ITEM_QTY ) ,
                STOCK_DETAIL.Balance_Qty = ( STOCK_DETAIL.Balance_Qty
                                             + SALE_INVOICE_STOCK_DETAIL.ITEM_QTY )
        FROM    dbo.STOCK_DETAIL
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL ON SALE_INVOICE_STOCK_DETAIL.STOCK_DETAIL_ID = STOCK_DETAIL.STOCK_DETAIL_ID
                                                      AND SALE_INVOICE_STOCK_DETAIL.ITEM_ID = STOCK_DETAIL.Item_id
        WHERE   SI_ID = @SI_ID        
          
        DECLARE @V_CUST_ID AS NUMERIC(18, 0)        
        DECLARE @V_NET_AMOUNT AS NUMERIC(18, 2)      
        DECLARE @V_GROSS_AMOUNT AS NUMERIC(18, 2)       
        DECLARE @V_VAT_AMOUNT AS NUMERIC(18, 2)     
        DECLARE @v_CESS_AMOUNT AS NUMERIC(18, 2)     
        DECLARE @v_Add_CESS_AMOUNT AS NUMERIC(18, 2)       
        DECLARE @V_Division_ID AS NUMERIC(18, 0)        
        DECLARE @V_Created_BY AS VARCHAR(50)        
        DECLARE @V_Si_NO AS VARCHAR(100)        
        DECLARE @TransactionDate AS DATETIME      
        DECLARE @v_INV_TYPE CHAR(1)    
        DECLARE @CGST_Amount AS NUMERIC(18, 2)     
        DECLARE @RoundOff NUMERIC(18, 2)  
        DECLARE @V_Freight AS NUMERIC(18, 2)    
            
        DECLARE @InputID NUMERIC                 
        DECLARE @CInputID NUMERIC                 
        SET @CInputID = 10017     
              
        SELECT  @V_CUST_ID = CUST_ID ,
                @V_NET_AMOUNT = NET_AMOUNT ,
                @V_GROSS_AMOUNT = GROSS_AMOUNT ,
                @v_CESS_AMOUNT = CESS_AMOUNT ,
                @V_VAT_AMOUNT = sim.VAT_AMOUNT ,
                @v_INV_TYPE = INV_TYPE ,
                @V_Division_ID = sim.DIVISION_ID ,
                @V_Created_BY = sim.CREATED_BY ,
                @V_Si_NO = SI_CODE + CAST(SI_NO AS VARCHAR(50)) ,
                @TransactionDate = SI_DATE ,
                @V_Freight = ISNULL(freight, 0)
        FROM    dbo.SALE_INVOICE_MASTER sim
        WHERE   sim.SI_ID = @SI_ID    
            
        SELECT  @v_Add_CESS_AMOUNT = SUM(sidd.ACessAmount)
        FROM    dbo.SALE_INVOICE_DETAIL sidd
        WHERE   sidd.SI_ID = @SI_ID    
              
            
        SET @CGST_Amount = @V_VAT_AMOUNT / 2    
            
        SET @RoundOff = @v_NET_AMOUNT - ( @v_GROSS_AMOUNT + @v_VAT_AMOUNT
                                          + @v_CESS_AMOUNT + ISNULL(@V_Freight, 0) )     
            
        SET @InputID = ( SELECT CASE WHEN @v_INV_TYPE = 'I' THEN 10021
                                     WHEN @v_INV_TYPE = 'S' THEN 10024
                                     WHEN @v_INV_TYPE = 'U' THEN 10075
                                END AS inputid
                       )     
              
        DECLARE @Remarks VARCHAR(250)        
        SET @Remarks = 'Amount Deducted against Cancel Invoice - ' + @V_Si_NO        
       
       
        DECLARE @ConsumerHeadID NUMERIC(18, 2)    
         
        SET @ConsumerHeadID = ( SELECT  ISNULL(ConsumerHeadID, 0)
                                FROM    SALE_INVOICE_MASTER
                                WHERE   SI_ID = @SI_ID
                              )
       
        IF @ConsumerHeadID > 0
            BEGIN
                EXECUTE Proc_Ledger_Insert @ConsumerHeadID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @SI_ID, 16, @TransactionDate,
                    @V_Created_BY    
            END
            
        ELSE
            BEGIN
                EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @SI_ID, 16, @TransactionDate,
                    @V_Created_BY    
            END
            
            
            
             
                
        EXECUTE Proc_Ledger_Insert 10071, 0, @V_GROSS_AMOUNT, @Remarks,
            @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY    
            
            
            
        SET @Remarks = 'Freight Deducted against Cancel Invoice - ' + @V_Si_NO   
        EXECUTE Proc_Ledger_Insert 10047, 0, @V_Freight, @Remarks,
            @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY   
              
                
        SET @Remarks = 'Amount Deducted GST against Cancel Invoice - '
            + @V_Si_NO     
                               
        IF @v_INV_TYPE <> 'I'
            BEGIN                  
                  
                EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                    @Remarks, @V_Division_ID, @SI_ID, 16, @TransactionDate,
                    @V_Created_BY                  
                  
                EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount, @Remarks,
                    @V_Division_ID, @SI_ID, 16, @TransactionDate,
                    @V_Created_BY                  
                  
            END                  
                  
        ELSE
            BEGIN                  
                  
                EXECUTE Proc_Ledger_Insert @InputID, 0, @v_VAT_AMOUNT,
                    @Remarks, @V_Division_ID, @SI_ID, 16, @TransactionDate,
                    @V_Created_BY                  
                  
            END      
                
                
            
        SET @Remarks = 'Amount Deducted Cess against Cancel Invoice - '
            + @V_Si_NO       
                            
        EXECUTE Proc_Ledger_Insert 10014, 0, @v_CESS_AMOUNT, @Remarks,
            @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY        
                          
                          
        SET @Remarks = 'Amount Deducted Add. Cess against Cancel Invoice - '
            + @V_Si_NO    
                          
        EXECUTE Proc_Ledger_Insert 10012, 0, @v_Add_CESS_AMOUNT, @Remarks,
            @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY        
                 
                               
                  
        SET @Remarks = 'Amount Deducted Round Off against Cancel Invoice -'
            + @V_Si_NO     
                                   
                  
        IF @RoundOff > 0
            BEGIN                  
                EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff, @Remarks,
                    @V_Division_ID, @SI_ID, 16, @TransactionDate,
                    @V_Created_BY                  
            END                  
        ELSE
            BEGIN                  
                SET @RoundOff = -+@RoundOff      
                                        
                EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0, @Remarks,
                    @V_Division_ID, @SI_ID, 16, @TransactionDate,
                    @V_Created_BY                  
            END                   
                  
                  
        SET @Remarks = 'Amount Deducted Stock In against Cancel Invoice - '
            + @V_Si_NO      
                  
        EXECUTE Proc_Ledger_Insert 10073, 0, @V_NET_AMOUNT, @Remarks,
            @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY     
            
                
    END
	Go
---------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[PROC_OUTSIDE_SALE_MASTER_SALE_NEW]
    (
      @v_SI_ID INT ,
      @v_SI_CODE VARCHAR(50) ,
      @v_SI_NO DECIMAL ,
      @v_DC_NO DECIMAL ,
      @v_SI_DATE DATETIME ,
      @v_CUST_ID INT ,
      @V_INVOICE_STATUS INT ,
      @v_REMARKS VARCHAR(500) ,
      @v_PAYMENTS_REMARKS VARCHAR(500) ,
      @v_SALE_TYPE VARCHAR(10) ,
      @v_GROSS_AMOUNT DECIMAL(18, 2) ,
      @v_VAT_AMOUNT DECIMAL(18, 2) ,
      @v_CESS_AMOUNT DECIMAL(18, 2) ,
      @v_ACESS_AMOUNT DECIMAL(18, 2) ,
      @v_NET_AMOUNT DECIMAL(18, 0) ,
      @V_IS_SAMPLE INT ,
      @V_DELIVERY_NOTE_NO VARCHAR(50) ,
      @V_VAT_CST_PER NUMERIC(18, 2) ,
      @V_SAMPLE_ADDRESS VARCHAR(500) ,
      @v_CREATED_BY VARCHAR(50) ,
      @v_CREATION_DATE DATETIME ,
      @v_MODIFIED_BY VARCHAR(50) ,
      @v_MODIFIED_DATE DATETIME ,
      @v_DIVISION_ID INT ,
      @V_VEHICLE_NO VARCHAR(100) = NULL ,
      @V_TRANSPORT VARCHAR(70) = NULL ,
      @v_SHIPP_ADD_ID INT = NULL ,
      @v_INV_TYPE CHAR(1) = NULL ,
      @v_LR_NO VARCHAR(100) = NULL ,
      @V_MODE INT ,
      @V_Flag INT = 0 ,
      @v_EwayBill_NO VARCHAR(50) = NULL ,
      @v_freight NUMERIC(18, 2) = 0 ,
      @v_freight_type CHAR = 'A' ,
      @V_FreightTaxApplied INT = 0 ,
      @V_FreightTaxValue NUMERIC(18, 2) = 0 ,
      @V_ConsumerHeadID INT = 0
    )
AS
    BEGIN                     
                      
        DECLARE @InputID NUMERIC                     
        DECLARE @CInputID NUMERIC                     
        SET @CInputID = 10017                        
        DECLARE @RoundOff NUMERIC(18, 2)                    
        DECLARE @CGST_Amount NUMERIC(18, 2)                     
        SET @CGST_Amount = ( @v_VAT_AMOUNT / 2 )                       
        SET @RoundOff = @v_NET_AMOUNT - ( @v_GROSS_AMOUNT + @v_VAT_AMOUNT
                                          + @v_CESS_AMOUNT
                                          + ISNULL(@v_ACESS_AMOUNT, 0)+ ISNULL(@v_freight, 0) )                        
                      
        SET @InputID = ( SELECT CASE WHEN @v_INV_TYPE = 'I' THEN 10021
                                     WHEN @v_INV_TYPE = 'S' THEN 10024
                                     WHEN @v_INV_TYPE = 'U' THEN 10075
                                END AS inputid
                       )                    
                      
        IF @V_MODE = 1
            BEGIN                     
                                  
                INSERT  INTO SALE_INVOICE_MASTER
                        ( SI_ID ,
                          SI_CODE ,
                          SI_NO ,
                          DC_GST_NO ,
                          SI_DATE ,
                          CUST_ID ,
                          INVOICE_STATUS ,
                          REMARKS ,
                          PAYMENTS_REMARKS ,
                          SALE_TYPE ,
                          GROSS_AMOUNT ,
                          VAT_AMOUNT ,
                          CESS_AMOUNT ,
                          NET_AMOUNT ,
                          IS_SAMPLE ,
                          DELIVERY_NOTE_NO ,
                          VAT_CST_PER ,
                          SAMPLE_ADDRESS ,
                          CREATED_BY ,
                          CREATION_DATE ,
                          MODIFIED_BY ,
                          MODIFIED_DATE ,
                          DIVISION_ID ,
                          VEHICLE_NO ,
                          TRANSPORT ,
                          SHIPP_ADD_ID ,
                          LR_NO ,
                          INV_TYPE ,
                          Flag ,
                          freight ,
                          freight_type ,
                          FreightTaxApplied ,
                          FreightTaxValue ,
                          ConsumerHeadID
                                               
                      
                        )
                VALUES  ( @V_SI_ID ,
                          @V_SI_CODE ,
                          @V_SI_NO ,
                          @v_DC_NO ,
                          @V_SI_DATE ,
                          @V_CUST_ID ,
                          @V_INVOICE_STATUS ,
                          @V_REMARKS ,
                          @V_PAYMENTS_REMARKS ,
                          @V_SALE_TYPE ,
                          @V_GROSS_AMOUNT ,
                          @V_VAT_AMOUNT ,
                          ( ISNULL(@v_CESS_AMOUNT, 0) + ISNULL(@v_ACESS_AMOUNT,
                                                              0) ) ,
                          @V_NET_AMOUNT ,
                          @V_IS_SAMPLE ,
                          @V_DELIVERY_NOTE_NO ,
                          @V_VAT_CST_PER ,
                          @V_SAMPLE_ADDRESS ,
                          @V_CREATED_BY ,
                          @V_CREATION_DATE ,
                          @V_MODIFIED_BY ,
                          @V_MODIFIED_DATE ,
                          @V_DIVISION_ID ,
                          @V_VEHICLE_NO ,
                          @V_TRANSPORT ,
                          @v_SHIPP_ADD_ID ,
                          @v_LR_NO ,
                          @v_INV_TYPE ,
                          @V_Flag ,
                          @V_freight ,
                          @V_freight_type ,
                          @V_FreightTaxApplied ,
                          @V_FreightTaxValue ,
                          @V_ConsumerHeadID
                                               
                      
                        )       
                      
                      
                DECLARE @Remarks VARCHAR(250) 
                SET @Remarks = 'Sale against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))                        
                      
                           
                IF @V_ConsumerHeadID > 0
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @V_ConsumerHeadID, 0,
                            @V_NET_AMOUNT, @Remarks, @V_Division_ID, @V_SI_ID,
                            16, @V_SI_DATE, @V_Created_BY   
                    END
                ELSE
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @V_CUST_ID, 0,
                            @V_NET_AMOUNT, @Remarks, @V_Division_ID, @V_SI_ID,
                            16, @V_SI_DATE, @V_Created_BY  
                    END 
                      
                EXECUTE Proc_Ledger_Insert 10071, @V_GROSS_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE, @V_Created_BY     
                    
                    
                    
                SET @Remarks = 'Freight against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))                 
                                        
                EXECUTE Proc_Ledger_Insert 10047, @v_freight, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE, @V_Created_BY  
                                     
                      
                SET @Remarks = 'GST against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))                      
                      
                IF @v_INV_TYPE <> 'I'
                    BEGIN                      
                      
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE,
                            @V_Created_BY                      
                      
                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE,
                            @V_Created_BY                      
                      
                    END                      
                      
                ELSE
                    BEGIN                      
                      
                        EXECUTE Proc_Ledger_Insert @InputID, @v_VAT_AMOUNT, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE,
                            @V_Created_BY                      
                      
                    END                      
                      
                SET @Remarks = 'Cess against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))                      
                                
                EXECUTE Proc_Ledger_Insert 10014, @v_CESS_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE, @V_Created_BY            
                              
                              
                SET @Remarks = 'Add. Cess against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))          
                              
                EXECUTE Proc_Ledger_Insert 10012, @v_ACESS_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE, @V_Created_BY            
                     
                                   
                      
                SET @Remarks = 'Round Off against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))                      
                                       
                      
                IF @RoundOff > 0
                    BEGIN                      
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE,
                            @V_Created_BY                      
                    END                      
                ELSE
                    BEGIN                      
                        SET @RoundOff = -+@RoundOff                      
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE,
                            @V_Created_BY                      
                    END                       
                      
                      
                SET @Remarks = 'Stock In against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))                      
                      
                EXECUTE Proc_Ledger_Insert 10073, @V_NET_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE, @V_Created_BY                      
                
                      
            END                              
                      
                      
                      
        IF @V_MODE = 2
            BEGIN                        
                        
                        
                DECLARE @TransactionDate DATETIME                    
                        
                SELECT  @TransactionDate = SI_DATE
                FROM    SALE_INVOICE_MASTER
                WHERE   SI_ID = @V_SI_ID                      
                      
                      
                UPDATE  SALE_INVOICE_MASTER
                SET     CUST_ID = @V_CUST_ID ,
                        INVOICE_STATUS = @V_INVOICE_STATUS ,
                        REMARKS = @V_REMARKS ,
                        PAYMENTS_REMARKS = @V_PAYMENTS_REMARKS ,
                        SALE_TYPE = @V_SALE_TYPE ,
                        GROSS_AMOUNT = @V_GROSS_AMOUNT ,
                        VAT_AMOUNT = @V_VAT_AMOUNT ,
                        CESS_AMOUNT = ( ISNULL(@v_CESS_AMOUNT, 0)
                                        + ISNULL(@v_ACESS_AMOUNT, 0) ) ,
                        NET_AMOUNT = @V_NET_AMOUNT ,
                        IS_SAMPLE = @V_IS_SAMPLE ,
                        DELIVERY_NOTE_NO = @V_DELIVERY_NOTE_NO ,
                        VAT_CST_PER = @V_VAT_CST_PER ,
                        SAMPLE_ADDRESS = @V_SAMPLE_ADDRESS ,                       
                        --CREATED_BY = @V_CREATED_BY ,                    
                        --CREATION_DATE = @V_CREATION_DATE ,                      
                        MODIFIED_BY = @V_MODIFIED_BY ,
                        MODIFIED_DATE = @V_MODIFIED_DATE ,
                        DIVISION_ID = @V_DIVISION_ID ,
                        VEHICLE_NO = @V_VEHICLE_NO ,
                        TRANSPORT = @V_TRANSPORT ,
                        SHIPP_ADD_ID = @v_SHIPP_ADD_ID ,
                        LR_NO = @v_LR_NO ,
                        INV_TYPE = @v_INV_TYPE ,
                        EwayBillNo = @v_EwayBill_NO ,
                        freight = @V_freight ,
                        freight_type = @V_freight_type ,
                        FreightTaxApplied = @V_FreightTaxApplied ,
                        FreightTaxValue = @V_FreightTaxValue ,
                        ConsumerHeadID = @V_ConsumerHeadID
                WHERE   SI_ID = @V_SI_ID      
                      
                      
                SET @Remarks = 'Sale against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))                        
                      
                SET @Remarks = 'Sale against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))                      
                      
                   
                      
                IF @V_ConsumerHeadID > 0
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @V_ConsumerHeadID, 0,
                            @V_NET_AMOUNT, @Remarks, @V_Division_ID, @V_SI_ID,
                            16, @TransactionDate, @V_Created_BY  
                    END
                ELSE
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @V_CUST_ID, 0,
                            @V_NET_AMOUNT, @Remarks, @V_Division_ID, @V_SI_ID,
                            16, @TransactionDate, @V_Created_BY  
                    END      
                      
                EXECUTE Proc_Ledger_Insert 10071, @V_GROSS_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @TransactionDate,
                    @V_Created_BY                      
                      
                
                SET @Remarks = 'Freight against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))                 
                                        
                EXECUTE Proc_Ledger_Insert 10047, @v_freight, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE, @V_Created_BY 
                
                                     
                      
                SET @Remarks = 'GST against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))                      
                      
                IF @v_INV_TYPE <> 'I'
                    BEGIN                      
                      
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @TransactionDate, @V_Created_BY                      
                      
                      
                      
                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @TransactionDate, @V_Created_BY                      
                      
                    END                      
                      
                ELSE
                    BEGIN                      
                      
                        EXECUTE Proc_Ledger_Insert @InputID, @v_VAT_AMOUNT, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @TransactionDate, @V_Created_BY                      
                      
                    END                         
                    
                SET @Remarks = 'Cess against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))          
                                
                EXECUTE Proc_Ledger_Insert 10014, @v_CESS_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @TransactionDate,
                    @V_Created_BY              
                              
                              
                SET @Remarks = 'Add. Cess against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))          
                              
                EXECUTE Proc_Ledger_Insert 10012, @v_ACESS_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @TransactionDate,
                    @V_Created_BY          
                               
                      
                SET @Remarks = 'Round Off against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))                      
                      
                IF @RoundOff > 0
                    BEGIN               
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @TransactionDate, @V_Created_BY                      
                    END                      
                ELSE
                    BEGIN                      
                        SET @RoundOff = -+@RoundOff                      
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @TransactionDate, @V_Created_BY                      
                    END                       
                      
                      
                     
                SET @Remarks = 'Stock In against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))                      
                      
                EXECUTE Proc_Ledger_Insert 10073, @V_NET_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @TransactionDate,
                    @V_Created_BY                    
                      
            END                     
                      
                      
        IF @V_MODE = 3
            BEGIN                     
                      
                DECLARE UpdateStock CURSOR
                FOR
                    SELECT  Item_id ,
                            Item_qty ,
                            division_id
                    FROM    sale_invoice_detail
                    WHERE   si_id = @v_SI_ID                                              
                      
                      
                      
                OPEN UpdateStock                     
                DECLARE @DivisionId AS INT                     
                DECLARE @ItemId AS INT                        
                DECLARE @ItemQty AS NUMERIC(18, 4)                     
                      
                FETCH NEXT FROM UpdateStock INTO @ItemId, @ItemQty,
                    @DivisionId            
                      
                      
                WHILE @@FETCH_STATUS = 0
                    BEGIN                      
                      
                        UPDATE  item_detail
                        SET     current_stock = current_stock + @ItemQty
                        WHERE   item_id = @ItemId
                                AND div_id = @DivisionId                     
                      
                      
                        FETCH NEXT FROM UpdateStock INTO @ItemId, @ItemQty,
                            @DivisionId                     
                      
                    END                      
                      
                      
                CLOSE UpdateStock                        
                DEALLOCATE UpdateStock                    
                      
                DELETE  FROM sale_invoice_Detail
                WHERE   si_id = @v_SI_ID                     
                      
                DELETE  FROM sale_invoice_master
                WHERE   si_id = @v_SI_ID                       
                      
            END                        
                      
    END 
	GO
---------------------------------------------------------------------------------------------------------------------------
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
            
            
        DECLARE @ConsumerHeadID NUMERIC(18, 2)    
         
        SET @ConsumerHeadID = ( SELECT  ISNULL(ConsumerHeadID, 0)
                                FROM    SALE_INVOICE_MASTER
                                WHERE   SI_ID = @V_SI_ID
                              )
            
        DECLARE @CashOut NUMERIC(18, 2)            
        DECLARE @CashIn NUMERIC(18, 2)            
        SET @CashOut = 0            
            
            
            
        IF @ConsumerHeadID > 0
            BEGIN
            
                SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                FROM    dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                WHERE   TransactionId = @v_SI_ID
                                        AND TransactionTypeId = 16
                                        AND AccountId = @ConsumerHeadID
                              )            
            
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @ConsumerHeadID 
            
            END
            
        ELSE
            BEGIN
            
            
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
            
            END
            
            
        SET @CashIn = 0        
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = 10047
                       )                    
                    
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10047   
        
        SET @CashIn = 0      
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
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
            
            
            
         -------Cess Entries Deletion --------     
             
                         
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = 10014
                       )     
                           
        SET @CashIn = 0     
                         
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10014         
                        
                        
                        
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = 10012
                       )     
                           
        SET @CashIn = 0     
                         
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10012          
                        
        -------Cess Entries Deletion --------       
            
        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @v_SI_ID
                AND TransactionTypeId = 16            
    END
GO
---------------------------------------------------------------------------------------------------------------------------
ALTER PROC [dbo].[GET_INV_DETAIL] ( @V_SI_ID NUMERIC(18, 0) )
AS
    BEGIN      
      
        SELECT  SI_ID ,
                SI_CODE + CAST(SI_NO AS VARCHAR(20)) AS SI_NO ,
                CONVERT(VARCHAR(20), SI_DATE, 106) AS InvDate ,
                CUST_ID ,
                SALE_TYPE ,
                VEHICLE_NO ,
                ISNULL(TRANSPORT, 0) AS TRANSPORT ,
                ISNULL(LR_NO, 0) AS LR_NO ,
                CASE INV_TYPE
                  WHEN 'U' THEN 'UGST'
                  WHEN 'S' THEN 'SGST'
                  ELSE 'IGST'
                END AS INV_TYPE ,
                SI_NO AS INVNO ,
                ISNULL(EwayBillNo, '') AS EwayBillNo ,
                ISNULL(freight, 0) AS freight ,
                ISNULL(freight_type, 'A') AS freight_type ,
                ISNULL(FreightTaxApplied, 0) AS FreightTaxApplied ,
                ISNULL(FreightTaxValue, 0) AS FreightTaxValue ,
                ISNULL(ConsumerHeadID, 0) AS ConsumerHeadID ,
                CASE WHEN ISNULL(ConsumerHeadID, 0) > 0
                     THEN ( SELECT  ACC_NAME
                            FROM    dbo.ACCOUNT_MASTER
                            WHERE   ACC_ID = ConsumerHeadID
                          )
                     ELSE ''
                END ConsumerHeadName
        FROM    dbo.SALE_INVOICE_MASTER
        WHERE   SI_ID = @V_SI_ID      
      
    END 

GO
---------------------------------------------------------------------------------------------------------------------------

ALTER TABLE dbo.CreditNote_Master ADD ConsumerHeadID NUMERIC(18,2) DEFAULT 0
GO
---------------------------------------------------------------------------------------------------------------------------
-----------------------Credit Note----------------------------------    
ALTER PROCEDURE [dbo].[PROC_CreditNote_MASTER]
    (
      @v_CreditNote_ID NUMERIC(18, 0) ,
      @v_CreditNote_No NUMERIC(18, 0) ,
      @v_CreditNote_Code VARCHAR(20) ,
      @v_CreditNote_Date DATETIME ,
      @v_Remarks VARCHAR(500) ,
      @v_INV_Id NUMERIC(18, 0) ,
      @v_Created_BY VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Division_ID NUMERIC(18, 0) ,
      @v_CN_Amount NUMERIC(18, 2) ,
      @v_CN_ItemValue NUMERIC(18, 2) = 0 ,
      @v_CN_ItemTax NUMERIC(18, 2) = 0 ,
      @v_CN_ItemCess NUMERIC(18, 2) = 0 ,
      @v_CN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
      @v_CreditNote_Type VARCHAR(20) = NULL ,
      @v_RefNo VARCHAR(50) = NULL ,
      @v_RefDate_dt DATETIME ,
      @V_Trans_Type NUMERIC(18, 0) ,
      @v_RoundOff NUMERIC(18, 2) ,
      @V_ConsumerHeadID INT = 0 ,
      @v_Proc_Type INT
                        
    )
AS
    BEGIN            
            
        DECLARE @V_GSTType NUMERIC(18, 0)         
        DECLARE @Remarks VARCHAR(250)               
        DECLARE @OutputID NUMERIC                  
        DECLARE @COutputID NUMERIC                  
        SET @COutputID = 10017             
        DECLARE @CGST_Amount NUMERIC(18, 2)           
        DECLARE @v_INV_TYPE VARCHAR(1)            
        DECLARE @CessOutputID NUMERIC = 0                    
        SET @CessOutputID = 10014          
                     
        SET @V_GSTType = ( SELECT   dbo.Get_GST_Type(@v_CN_CustId)
                         )        
                                 
        IF @V_PROC_TYPE = 1
            BEGIN            
                       
                INSERT  INTO CreditNote_Master
                        ( CreditNote_ID ,
                          CreditNote_No ,
                          CreditNote_Code ,
                          CreditNote_Date ,
                          Remarks ,
                          INVId ,
                          Created_BY ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_ID ,
                          CN_Amount ,
                          CN_CustId ,
                          INV_No ,
                          INV_Date ,
                          CreditNote_Type ,
                          RefNo ,
                          RefDate_dt ,
                          Tax_Amt ,
                          Cess_Amt ,
                          RoundOff ,
                          GSTType ,
                          ConsumerHeadID                
                        )
                VALUES  ( @V_CreditNote_ID ,
                          @V_CreditNote_No ,
                          @V_CreditNote_Code ,
                          @v_CreditNote_Date ,
                          @V_Remarks ,
                          @V_INV_Id ,
                          @V_Created_BY ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_ID ,
                          @v_CN_Amount ,
                          @v_CN_CustId ,
                          @v_INV_No ,
                          @v_INV_Date ,
                          @v_CreditNote_Type ,
                          @v_RefNo ,
                          @v_RefDate_dt ,
                          @v_CN_ItemTax ,
                          @v_CN_ItemCess ,
                          @v_RoundOff ,
                          @V_GSTType ,
                          @V_ConsumerHeadID                  
                        )                  
                  
                UPDATE  CN_SERIES
                SET     CURRENT_USED = @V_CreditNote_No
                WHERE   DIV_ID = @V_Division_ID                    
                  
                                 
                SET @Remarks = 'Credit against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))              
                  
                  
                  
                IF @V_ConsumerHeadID > 0
                    BEGIN 
                        EXECUTE Proc_Ledger_Insert @V_ConsumerHeadID,
                            @v_CN_Amount, 0, @Remarks, @V_Division_ID,
                            @V_CreditNote_ID, @V_Trans_Type,
                            @v_CreditNote_Date, @V_Created_BY  
                 
                    END
                ELSE
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @v_CN_CustId, @v_CN_Amount,
                            0, @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY  
                    END
                                 
                  
                EXECUTE Proc_Ledger_Insert 10071, 0, @v_CN_ItemValue, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY          
                      
                                 
                SET @CGST_Amount = ( @v_CN_ItemTax / 2 )            
                  
                  
----------------------------------------------------------------------------------------------------  
                IF @v_INV_Id > 0
                    BEGIN  
                        SET @OutputID = ( SELECT    CASE WHEN INV_TYPE = 'I'
                                                         THEN 10021
                                                         WHEN INV_TYPE = 'S'
                                                         THEN 10024
                                                         WHEN INV_TYPE = 'U'
                                                         THEN 10075
                                                    END AS inputid
                                          FROM      dbo.SALE_INVOICE_MASTER
                                          WHERE     SI_ID = @v_INV_Id
                                        )           
                            
                        SET @v_INV_TYPE = ( SELECT  INV_TYPE
                                            FROM    dbo.SALE_INVOICE_MASTER
                                            WHERE   SI_ID = @v_INV_Id
                                          )                  
                  
                    END  
                ELSE
                    BEGIN  
                                
                        SET @v_INV_TYPE = ( SELECT  CASE WHEN GType = 2
                                                         THEN 'I'
                                                         WHEN GType = 1
                                                         THEN 'S'
                                                         ELSE 'U'
                                                    END AS Gtype
                                            FROM    ( SELECT  dbo.Get_GST_Type(@v_CN_CustId) AS GType
                                                    ) tb
                                          )  
                                            
                                            
                        SET @OutputID = ( SELECT    CASE WHEN @v_INV_TYPE = 'I'
                                                         THEN 10021
                                                         WHEN @v_INV_TYPE = 'S'
                                                         THEN 10024
                                                         WHEN @v_INV_TYPE = 'U'
                                                         THEN 10075
                                                    END AS inputid
                                        )       
                                            
                    END  
                  
                  
                -----------------------------------------------------------------------------------------  
                  
                  
                SET @Remarks = 'GST against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                  
                  
                IF @v_INV_TYPE <> 'I'
                    BEGIN                  
                        EXECUTE Proc_Ledger_Insert @COutputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                 
                  
                        EXECUTE Proc_Ledger_Insert @OutputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                  
                    END                  
                ELSE
                    BEGIN                  
                        EXECUTE Proc_Ledger_Insert @OutputID, 0, @v_CN_ItemTax,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                   
                    END             
                                
                                
                SET @Remarks = 'CESS against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))              
                             
                EXECUTE Proc_Ledger_Insert @CessOutputID, 0, @v_CN_ItemCess,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY               
                                
                                
                SET @Remarks = 'Stock Out against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                  
                  
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_CN_Amount, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY        
                            
                SET @Remarks = 'Round Off against Credit Note No- '
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                                 
              
                IF @v_RoundOff > 0
                    BEGIN                        
                        
                        EXECUTE Proc_Ledger_Insert 10054, 0, @v_RoundOff,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                    
                     
                    END                        
                        
                ELSE
                    BEGIN                        
                        
                        SET @v_RoundOff = -+@v_RoundOff                        
                        
                        EXECUTE Proc_Ledger_Insert 10054, @v_RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                    
                        
                    END             
                              
            END             
                  
        IF @V_PROC_TYPE = 2
            BEGIN           
                      
                EXECUTE [Proc_CreditNoteUpdateDeleteLedgerEntries] @V_CreditNote_ID,
                    @V_Trans_Type                   
                  
                UPDATE  dbo.CreditNote_Master
                SET     CreditNote_No = @V_CreditNote_No ,
                        CreditNote_Code = @V_CreditNote_Code ,
                        CreditNote_Date = @v_CreditNote_Date ,
                        Remarks = @V_Remarks ,
                        INVId = @V_INV_Id ,
                        Modified_By = @V_Modified_By ,
                        Modification_Date = @V_Modification_Date ,
                        CN_Amount = @v_CN_Amount ,
                        CN_CustId = @v_CN_CustId ,
                        Division_ID = @V_Division_ID ,
                        INV_No = @v_INV_No ,
                        INV_Date = @v_INV_Date ,
                        CreditNote_Type = @v_CreditNote_Type ,
                        RefNo = @v_RefNo ,
                        RefDate_dt = @v_RefDate_dt ,
                        Tax_Amt = @v_CN_ItemTax ,
                        Cess_Amt = @v_CN_ItemCess ,
                        RoundOff = @v_RoundOff ,
                        GSTType = @v_GSTType ,
                        ConsumerHeadID = @V_ConsumerHeadID
                WHERE   CreditNote_ID = @V_CreditNote_ID                           
                          
                SET @Remarks = 'Credit against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))              
                  
                  
                 
                    
                    
                    
                IF @V_ConsumerHeadID > 0
                    BEGIN 
                        EXECUTE Proc_Ledger_Insert @V_ConsumerHeadID,
                            @v_CN_Amount, 0, @Remarks, @V_Division_ID,
                            @V_CreditNote_ID, @V_Trans_Type,
                            @v_CreditNote_Date, @V_Created_BY  
                 
                    END
                ELSE
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @v_CN_CustId, @v_CN_Amount,
                            0, @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY   
                    END
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                                  
                  
                EXECUTE Proc_Ledger_Insert 10071, 0, @v_CN_ItemValue, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY          
                        
                                 
                SET @CGST_Amount = ( @v_CN_ItemTax / 2 )          
                  
 -----------------------------------------------------------------------------------------------------------------                 
                IF @v_Inv_Id > 0
                    BEGIN  
                  
                        SET @OutputID = ( SELECT    CASE WHEN INV_TYPE = 'I'
                                                         THEN 10021
                                                         WHEN INV_TYPE = 'S'
                                                         THEN 10024
                                                         WHEN INV_TYPE = 'U'
                                                         THEN 10075
                                                    END AS inputid
                                          FROM      dbo.SALE_INVOICE_MASTER
                                          WHERE     SI_ID = @v_INV_Id
                                        )           
                            
                        SET @v_INV_TYPE = ( SELECT  INV_TYPE
                                            FROM    dbo.SALE_INVOICE_MASTER
                                            WHERE   SI_ID = @v_INV_Id
                                          )                  
                  
                    END  
                      
                ELSE
                    BEGIN  
                      
                        SET @v_INV_TYPE = ( SELECT  CASE WHEN GType = 2
                                                         THEN 'I'
                                                         WHEN GType = 1
                                                         THEN 'S'
                                                         ELSE 'U'
                                                    END AS Gtype
                                            FROM    ( SELECT  dbo.Get_GST_Type(@v_CN_CustId) AS GType
                                                    ) tb
                                          )  
                                            
                                            
                        SET @OutputID = ( SELECT    CASE WHEN @v_INV_TYPE = 'I'
                                                         THEN 10021
                                                         WHEN @v_INV_TYPE = 'S'
                                                         THEN 10024
                                                         WHEN @v_INV_TYPE = 'U'
                                                         THEN 10075
                                                    END AS inputid
                                        )  
                    END  
                      
-----------------------------------------------------------------------------------------------------------------                  
                  
                SET @Remarks = 'GST against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                  
                  
                IF @v_INV_TYPE <> 'I'
                    BEGIN                  
                        EXECUTE Proc_Ledger_Insert @COutputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                 
                  
                        EXECUTE Proc_Ledger_Insert @OutputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                  
                    END                  
                ELSE
                    BEGIN                  
                        EXECUTE Proc_Ledger_Insert @OutputID, 0, @v_CN_ItemTax,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                   
                    END             
                                
                                
                SET @Remarks = 'CESS against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))              
                             
                EXECUTE Proc_Ledger_Insert @CessOutputID, 0, @v_CN_ItemCess,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY               
                                
                                
                SET @Remarks = 'Stock Out against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                  
                  
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_CN_Amount, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY           
                            
                SET @Remarks = 'Round Off against Credit Note No- '
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                                 
                        
                IF @v_RoundOff > 0
                    BEGIN                        
                        
                        EXECUTE Proc_Ledger_Insert 10054, 0, @v_RoundOff,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                    
                        
                    END                        
                        
                ELSE
                    BEGIN                        
                        
                        SET @v_RoundOff = -+@v_RoundOff                        
                        
                        EXECUTE Proc_Ledger_Insert 10054, @v_RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                    
                        
                    END          
               
            END                  
    END 
Go
---------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[Proc_CreditNoteUpdateDeleteLedgerEntries]
    (
      @CreditNoteId VARCHAR(50) ,
      @TransactionTypeId INT                
    )
AS
    BEGIN          
            
        --SELECT  *        
        --FROM    SALE_INVOICE_MASTER           
        DECLARE @ConsumerHeadID NUMERIC(18, 2)      
           
                     
        DECLARE @ACC_ID NUMERIC(18, 2)           
        DECLARE @InvId NUMERIC(18, 2)           
                  
        SELECT  @ACC_ID = CN_CustId ,
                @ConsumerHeadID = ISNULL(ConsumerHeadID, 0)
        FROM    dbo.CreditNote_master cn
        WHERE   cn.CreditNote_Id = @CreditNoteId                  
                        
                        
              
        UPDATE  STOCK_DETAIL
        SET     STOCK_DETAIL.Issue_Qty = ( STOCK_DETAIL.Issue_Qty
                                           + CreditNote_DETAIL.ITEM_QTY ) ,
                STOCK_DETAIL.Balance_Qty = ( STOCK_DETAIL.Balance_Qty
                                             - CreditNote_DETAIL.ITEM_QTY )
        FROM    dbo.STOCK_DETAIL
                JOIN dbo.CreditNote_DETAIL ON CreditNote_DETAIL.STOCK_DETAIL_ID = STOCK_DETAIL.STOCK_DETAIL_ID
                                              AND CreditNote_DETAIL.ITEM_ID = STOCK_DETAIL.Item_id
        WHERE   CreditNote_Id = @CreditNoteId        
                     
                  
                  
        DELETE  FROM dbo.CreditNote_DETAIL
        WHERE   CreditNote_Id = @CreditNoteId        
                        
                        
                        
        DECLARE @CashOut NUMERIC(18, 2)                  
        DECLARE @CashIn NUMERIC(18, 2)                 
                        
        SET @CashIn = 0                
        IF @ConsumerHeadID > 0
            BEGIN        
                        
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @CreditNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @ConsumerHeadID
                               )                  
                  
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @ConsumerHeadID                         
                        
            END
        ELSE
            BEGIN
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @CreditNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @ACC_ID
                               )                  
                  
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @ACC_ID   
            END   
                 
                 
                 
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @CreditNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = 10073
                      )                  
                     
        SET @CashOut = 0             
                    
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10073          
                        
                        
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @CreditNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = 10071
                      )                    
                    
        SET @CashOut = 0                    
                    
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10071           
                  
        SET @CashIn = 0             
        SET @CashOut = 0                
                       
        DECLARE @v_INV_TYPE VARCHAR(1)                          
                
                        
        DECLARE @OutputID NUMERIC                    
        DECLARE @COutputID NUMERIC                    
        SET @COutputID = 10017               
        DECLARE @CessOutputID NUMERIC = 0                  
        SET @CessOutputID = 10014                 
                        
        SET @OutputID = ( SELECT    CASE WHEN GSTType = 2 THEN 10021
                                         WHEN GSTType = 1 THEN 10024
                                         WHEN GSTType = 3 THEN 10075
                                    END AS inputid
                          FROM      dbo.CreditNote_Master
                          WHERE     CreditNote_Id =@CreditNoteId 
                        )         
                          
        SET @v_INV_TYPE = ( SELECT  GSTType
                            FROM    dbo.CreditNote_Master
                            WHERE   CreditNote_Id =@CreditNoteId 
                          )            
                                       
                                       
        IF @v_INV_TYPE <> '2'
            BEGIN                    
                SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                FROM    dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                WHERE   TransactionId = @CreditNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @COutputID
                              )                    
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @COutputID                    
                    
                    
                SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                FROM    dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                WHERE   TransactionId = @CreditNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @OutputID
                              )                    
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @OutputID                    
            END                
        ELSE
            BEGIN                    
                    
                SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                FROM    dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                WHERE   TransactionId = @CreditNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @OutputID
                              )                    
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @OutputID                    
            END               
                          
                          
        ------------------Start Cess Entries Deletion ---------------              
                               
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @CreditNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = @CessOutputID
                      )             
        SET @CashIn = 0             
                               
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @CessOutputID               
                    
                              
        ------------------End Cess Entries Deletion ------------------                
                        
        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @CreditNoteId
                AND TransactionTypeId = @TransactionTypeId                 
                           
                        
    END   
      
Go
---------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE Proc_GETCreditNoteDetailsByID_Edit
    (
      @CreditNoteId NUMERIC(18, 0)
    )
AS
    BEGIN        
        SELECT  CN.CreditNote_Code + CAST(CN.CreditNote_No AS VARCHAR(20)) AS CreditNoteNumber ,
                dbo.fn_Format(CN.CreditNote_Date) AS CreditNote_Date ,
                CN_CustId ,
                SIM.SI_ID AS Invid ,
                INV_No AS InvoiceNo ,
                dbo.fn_Format(CN.INV_Date) AS InvoiceDate ,
                CN.Remarks AS Remarks ,
                ISNULL(CN.RoundOff, 0) AS RoundOff ,
                ISNULL(CN.ConsumerHeadID, 0) AS ConsumerHeadID ,
                CASE WHEN ISNULL(CN.ConsumerHeadID, 0) > 0
                     THEN ( SELECT  ACC_NAME
                            FROM    dbo.ACCOUNT_MASTER
                            WHERE   ACC_ID = CN.ConsumerHeadID
                          )
                     ELSE ''
                END ConsumerHeadName
        FROM    dbo.CreditNote_Master CN
                INNER JOIN dbo.SALE_INVOICE_MASTER SIM ON SIM.SI_ID = CN.INVId
        WHERE   CreditNote_Id = @CreditNoteId
        UNION ALL
        SELECT  CN.CreditNote_Code + CAST(CN.CreditNote_No AS VARCHAR(20)) AS CreditNoteNumber ,
                dbo.fn_Format(CN.CreditNote_Date) AS CreditNote_Date ,
                CN_CustId ,
                CN.INVId AS Invid ,
                INV_No AS InvoiceNo ,
                dbo.fn_Format(CN.INV_Date) AS InvoiceDate ,
                CN.Remarks AS Remarks ,
                ISNULL(CN.RoundOff, 0) AS RoundOff ,
                ISNULL(ConsumerHeadID, 0) AS ConsumerHeadID ,
                CASE WHEN ISNULL(ConsumerHeadID, 0) > 0
                     THEN ( SELECT  ACC_NAME
                            FROM    dbo.ACCOUNT_MASTER
                            WHERE   ACC_ID = ConsumerHeadID
                          )
                     ELSE ''
                END ConsumerHeadName
        FROM    dbo.CreditNote_Master CN
        WHERE   CreditNote_Id = @CreditNoteId
                AND CN.INVId <= 0    
    END    
Go
---------------------------------------------------------------------------------------------------------------------------
