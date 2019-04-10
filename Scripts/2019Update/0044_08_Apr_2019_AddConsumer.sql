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
ALTER VIEW [dbo].[VWPurchaseRegister]
AS
    SELECT       
  --ROW_NUMBER() OVER ( ORDER BY Received_ID ) AS SrNo ,      
            BillDate AS [Bill Date] ,
            BillNo AS [Bill No.] ,
            ReceivedDate AS [Received Date] ,
            CustomerName AS [Account Name] ,
            ADDRESS ,
            GSTNo AS [GST No.] ,
            BillAmount AS [Bill Amount] ,
            BillType AS [Bill Type] ,
            GST0 AS [Nill Rated] ,
            GST3 AS [Txbl. 3%] ,
            GST3Tax AS [Tax @3%] ,
            GST5 AS [Txbl. 5%] ,
            GST5Tax AS [Tax @5%] ,
            GST12 AS [Txbl. 12%] ,
            GST12Tax AS [Tax @12%] ,
            GST18 AS [Txbl. 18%] ,
            GST18Tax AS [Tax @18%] ,
            GST28 AS [Txbl. 28%] ,
            GST28Tax AS [Tax @28%] ,
            TaxableAmt AS [Total Txbl. GST] ,
            TotalTax AS [Total GST Tax] ,
            CAST([CESS %] AS INTEGER) AS [CESS %] ,
            CAST([Cess Amount] AS NUMERIC(18, 2)) AS [Cess Amount] ,
            CAST([ACess Amount] AS NUMERIC(18, 2)) AS [ACess Amount] ,
            CAST(otherAmount AS NUMERIC(18, 2)) AS [Other Amount] ,
            CAST(CashDiscount AS NUMERIC(18, 2)) AS [Cash Discount] ,
            CAST(Freight AS NUMERIC(18, 2)) AS [Freight] ,
            CAST(FreightTax AS NUMERIC(18, 2)) AS [Freight Tax]
    FROM    ( SELECT    MRWPM.Received_ID ,
                        CONVERT(VARCHAR(20), Received_Date, 106) ReceivedDate ,
                        Invoice_No AS BillNo ,
                        CONVERT(VARCHAR(20), Invoice_Date, 106) AS BillDate ,
                        ACM.ACC_NAME AS CustomerName ,
                        ACM.ADDRESS_PRIM AS ADDRESS ,
                        ACM.VAT_NO AS GSTNo ,
                        MRWPM.NET_AMOUNT AS BillAmount ,
                        CASE WHEN MRWPM.MRN_TYPE = 1 THEN 'SGST/CGST'
                             WHEN MRWPM.MRN_TYPE = 2 THEN 'IGST'
                             WHEN MRWPM.MRN_TYPE = 3 THEN 'UTGST/CGST'
                        END AS BillType ,
                        CAST(SUM(CASE WHEN Item_vat = 0
                                      THEN ( Item_Qty * Item_Rate )
                                           - ( ISNULL(CASE WHEN DType = 'P'
                                                           THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              ELSE ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              END
                                                           ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) ) / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              ELSE ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              END
                                                      END, 0) )
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) AS GST0 ,
                        CAST(SUM(CASE WHEN Item_vat = 3
                                      THEN ( Item_Qty * Item_Rate )
                                           - ( ISNULL(CASE WHEN DType = 'P'
                                                           THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              ELSE ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              END
                                                           ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) ) / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              ELSE ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              END
                                                      END, 0) )
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) AS GST3 ,
                        CAST(CAST(SUM(CASE WHEN Item_vat = 3
                                           THEN ( Item_Qty * Item_Rate )
                                                - ( ISNULL(CASE
                                                              WHEN DType = 'P'
                                                              THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              ELSE ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              END
                                                              ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) ) / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              ELSE ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              END
                                                           END, 0) )
                                           ELSE 0
                                      END * Item_vat / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST3Tax ,
                        CAST(SUM(CASE WHEN Item_vat = 5
                                      THEN ( Item_Qty * Item_Rate )
                                           - ( ISNULL(CASE WHEN DType = 'P'
                                                           THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              ELSE ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              END
                                                           ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) ) / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              ELSE ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              END
                                                      END, 0) )
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) AS GST5 ,
                        CAST(CAST(SUM(CASE WHEN Item_vat = 5
                                           THEN ( Item_Qty * Item_Rate )
                                                - ( ISNULL(CASE
                                                              WHEN DType = 'P'
                                                              THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              ELSE ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              END
                                                              ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) ) / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              ELSE ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              END
                                                           END, 0) )
                                           ELSE 0
                                      END * Item_vat / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST5Tax ,
                        CAST(SUM(CASE WHEN Item_vat = 12
                                      THEN ( Item_Qty * Item_Rate )
                                           - ( ISNULL(CASE WHEN DType = 'P'
                                                           THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              ELSE ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              END
                                                           ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) ) / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              ELSE ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              END
                                                      END, 0) )
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) AS GST12 ,
                        CAST(CAST(SUM(CASE WHEN Item_vat = 12
                                           THEN ( Item_Qty * Item_Rate )
                                                - ( ISNULL(CASE
                                                              WHEN DType = 'P'
                                                              THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              ELSE ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              END
                                                              ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) ) / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              ELSE ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              END
                                                           END, 0) )
                                           ELSE 0
                                      END * Item_vat / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST12Tax ,
                        CAST(SUM(CASE WHEN Item_vat = 18
                                      THEN ( Item_Qty * Item_Rate )
                                           - ( ISNULL(CASE WHEN DType = 'P'
                                                           THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              ELSE ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              END
                                                           ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) ) / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              ELSE ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              END
                                                      END, 0) )
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) AS GST18 ,
                        CAST(CAST(SUM(CASE WHEN Item_vat = 18
                                           THEN ( Item_Qty * Item_Rate )
                                                - ( ISNULL(CASE
                                                              WHEN DType = 'P'
                                                              THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              ELSE ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              END
                                                              ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) ) / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              ELSE ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              END
                                                           END, 0) )
                                           ELSE 0
                                      END * Item_vat / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST18Tax ,
                        CAST(SUM(CASE WHEN Item_vat = 28
                                      THEN ( Item_Qty * Item_Rate )
                                           - ( ISNULL(CASE WHEN DType = 'P'
                                                           THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              ELSE ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              END
                                                           ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) ) / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              ELSE ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              END
                                                      END, 0) )
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) AS GST28 ,
                        CAST(CAST(SUM(CASE WHEN Item_vat = 28
                                           THEN ( Item_Qty * Item_Rate )
                                                - ( ISNULL(CASE
                                                              WHEN DType = 'P'
                                                              THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ( ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              ELSE ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(Item_Rate
                                                              * Item_Qty, 2)
                                                              - ROUND(( Item_Rate
                                                              * Item_Qty
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              END
                                                              ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) )
                                                              - ( ( ( Item_Qty
                                                              * Item_Rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) ) / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              ELSE ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              END
                                                           END, 0) )
                                           ELSE 0
                                      END * Item_vat / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST28Tax ,
                        MAX(ISNULL(GROSS_AMOUNT, 0)) AS TaxableAmt ,
                        MAX(ISNULL(GST_AMOUNT, 0)) TotalTax ,
                        MAX(ISNULL(MRWPD.Item_cess, 0)) AS [CESS %] ,
                        CAST(SUM(( ISNULL(MRWPD.Item_Qty, 0)
                                   * ISNULL(MRWPD.Item_Rate, 0) )
                                 * ISNULL(MRWPD.Item_cess, 0) / 100) AS NUMERIC(18,
                                                              2)) AS [Cess Amount] ,
                        CAST(SUM(ISNULL(MRWPD.Item_Qty, 0)
                                 * ISNULL(MRWPD.ACess, 0)) AS NUMERIC(18, 2)) AS [ACess Amount] ,
                        MAX(ISNULL(Other_Charges, 0)) AS otherAmount ,
                        MAX(ISNULL(CashDiscount_Amt, 0)) CashDiscount ,
                        MAX(ISNULL(MRWPM.freight, 0)) AS Freight ,
                        MAX(ISNULL(MRWPM.FreightTaxValue, 0)) AS FreightTax
              FROM      dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER MRWPM
                        LEFT JOIN dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MRWPD ON MRWPD.Received_ID = MRWPM.Received_ID
                        INNER JOIN dbo.ACCOUNT_MASTER ACM ON ACM.ACC_ID = MRWPM.Vendor_ID
              GROUP BY  MRWPM.Received_ID ,
                        Received_Date ,
                        Invoice_No ,
                        Invoice_Date ,
                        ACC_NAME ,
                        ADDRESS_PRIM ,
                        VAT_NO ,
                        NET_AMOUNT ,
                        MRN_TYPE
              UNION ALL
              SELECT    MRAPM.Receipt_ID ,
                        CONVERT(VARCHAR(20), Receipt_Date, 106) ReceivedDate ,
                        Invoice_No AS BillNo ,
                        CONVERT(VARCHAR(20), Invoice_Date, 106) AS BillDate ,
                        ACM.ACC_NAME AS CustomerName ,
                        ACM.ADDRESS_PRIM AS ADDRESS ,
                        ACM.VAT_NO AS GSTNo ,
                        MRAPM.NET_AMOUNT AS BillAmount ,
                        CASE WHEN MRAPM.MRN_TYPE = 1 THEN 'SGST/CGST'
                             WHEN MRAPM.MRN_TYPE = 2 THEN 'IGST'
                             WHEN MRAPM.MRN_TYPE = 3 THEN 'UTGST/CGST'
                        END AS BillType ,
                        CAST(SUM(CASE WHEN Vat_Per = 0
                                      THEN ( Item_Qty * Item_Rate )
                                           - ( ISNULL(CASE WHEN DType = 'P'
                                                           THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                           ELSE DiscountValue
                                                      END, 0) )
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) AS GST0 ,
                        CAST(SUM(CASE WHEN Vat_Per = 3
                                      THEN ( Item_Qty * Item_Rate )
                                           - ( ISNULL(CASE WHEN DType = 'P'
                                                           THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                           ELSE DiscountValue
                                                      END, 0) )
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) AS GST3 ,
                        CAST(CAST(SUM(CASE WHEN Vat_Per = 3
                                           THEN ( Item_Qty * Item_Rate )
                                                - ( ISNULL(CASE
                                                              WHEN DType = 'P'
                                                              THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                              ELSE DiscountValue
                                                           END, 0) )
                                           ELSE 0
                                      END * Vat_Per / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST3Tax ,
                        CAST(SUM(CASE WHEN Vat_Per = 5
                                      THEN ( Item_Qty * Item_Rate )
                                           - ( ISNULL(CASE WHEN DType = 'P'
                                                           THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                           ELSE DiscountValue
                                                      END, 0) )
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) AS GST5 ,
                        CAST(CAST(SUM(CASE WHEN Vat_Per = 5
                                           THEN ( Item_Qty * Item_Rate )
                                                - ( ISNULL(CASE
                                                              WHEN DType = 'P'
                                                              THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                              ELSE DiscountValue
                                                           END, 0) )
                                           ELSE 0
                                      END * Vat_Per / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST5Tax ,
                        CAST(SUM(CASE WHEN Vat_Per = 12
                                      THEN ( Item_Qty * Item_Rate )
                                           - ( ISNULL(CASE WHEN DType = 'P'
                                                           THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                           ELSE DiscountValue
                                                      END, 0) )
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) AS GST12 ,
                        CAST(CAST(SUM(CASE WHEN Vat_Per = 12
                                           THEN ( Item_Qty * Item_Rate )
                                                - ( ISNULL(CASE
                                                              WHEN DType = 'P'
                                                              THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                              ELSE DiscountValue
                                                           END, 0) )
                                           ELSE 0
                                      END * Vat_Per / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST12Tax ,
                        CAST(SUM(CASE WHEN Vat_Per = 18
                                      THEN ( Item_Qty * Item_Rate )
                                           - ( ISNULL(CASE WHEN DType = 'P'
                                                           THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                           ELSE DiscountValue
                                                      END, 0) )
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) AS GST18 ,
                        CAST(CAST(SUM(CASE WHEN Vat_Per = 18
                                           THEN ( Item_Qty * Item_Rate )
                                                - ( ISNULL(CASE
                                                              WHEN DType = 'P'
                                                              THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                              ELSE DiscountValue
                                                           END, 0) )
                                           ELSE 0
                                      END * Vat_Per / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST18Tax ,
                        CAST(SUM(CASE WHEN Vat_Per = 28
                                      THEN ( Item_Qty * Item_Rate )
                                           - ( ISNULL(CASE WHEN DType = 'P'
                                                           THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                           ELSE DiscountValue
                                                      END, 0) )
                                      ELSE 0
                                 END) AS DECIMAL(18, 2)) AS GST28 ,
                        CAST(CAST(SUM(CASE WHEN Vat_Per = 28
                                           THEN ( Item_Qty * Item_Rate )
                                                - ( ISNULL(CASE
                                                              WHEN DType = 'P'
                                                              THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                              ELSE DiscountValue
                                                           END, 0) )
                                           ELSE 0
                                      END * Vat_Per / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST28Tax ,
                        MAX(ISNULL(GROSS_AMOUNT, 0)) AS TaxableAmt ,
                        MAX(ISNULL(GST_AMOUNT, 0)) TotalTax ,
                        0 AS [CESS %] ,
                        0 AS [Cess Amount] ,
                        0 AS [ACess Amount] ,
                        MAX(ISNULL(Other_Charges, 0)) AS otherAmount ,
                        MAX(ISNULL(CashDiscount_Amt, 0)) CashDiscount ,
                        MAX(ISNULL(MRAPM.freight, 0)) AS Freight ,
                        MAX(ISNULL(MRAPM.FreightTaxValue, 0)) AS FreightTax
              FROM      dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER MRAPM
                        LEFT JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL MRAPD ON MRAPD.Receipt_ID = MRAPM.Receipt_ID
                        INNER JOIN dbo.ACCOUNT_MASTER ACM ON ACM.ACC_ID = MRAPM.CUST_ID
              GROUP BY  MRAPM.Receipt_ID ,
                        Receipt_Date ,
                        Invoice_No ,
                        Invoice_Date ,
                        ACC_NAME ,
                        ADDRESS_PRIM ,
                        VAT_NO ,
                        NET_AMOUNT ,
                        MRN_TYPE
            ) tb  

			GO
			-----------------------------------------

			ALTER VIEW [dbo].[Sales_Analysis]  
AS  
    SELECT  ACC_ID ,  
            ACC_NAME ,  
            VAT_NO ,  
            ADDRESS_PRIM ,  
            MOBILE_NO ,  
            SI_ID ,  
            BillNo ,  
            BillAmount ,  
            fk_CessId_num ,  
            CessPercentage_num ,  
            ITEM_QTY ,  
            CAST(( ISNULL(GrossAmount, 0) / ITEM_QTY ) AS DECIMAL(18, 2)) AS Price_num ,  
            GrossAmount ,  
            VAT_AMOUNT ,  
            CessAmount_num ,  
            ACessAmount ,  
            MRP_num ,  
            ( ISNULL(GrossAmount, 0) + ISNULL(VAT_AMOUNT, 0)  
              + ISNULL(CessAmount_num, 0) + ISNULL(ACessAmount, 0) ) AS ItemTotalAmount_num ,  
            ITEM_ID ,  
            ITEM_NAME ,  
            BarCode_vch ,  
            ITEM_CODE ,  
            UM_Name ,  
            fk_hsnid_num ,  
            HsnCode_vch ,  
            ITEM_CAT_ID ,  
            ITEM_CAT_NAME ,  
            TaxID_num ,  
            TaxPercentage_num ,  
            OrderDate ,  
            BrandId ,  
            BrandName ,  
            SizeId ,  
            SizeName ,  
            ColorId ,  
            ColorName ,  
            CompanyId ,  
            CompanyName ,  
            DepartmentId ,  
            DepartmentName ,  
            TypeId ,  
            TypeName,
            Freight,
            FreightTaxValue
    FROM    ( SELECT    ACC_ID ,  
                        ACC_NAME ,  
                        VAT_NO ,  
                        dbo.ACCOUNT_MASTER.ADDRESS_PRIM ,  
                        dbo.ACCOUNT_MASTER.MOBILE_NO ,  
                        dbo.SALE_INVOICE_MASTER.SI_ID ,  
                        SI_CODE + CAST(SI_NO AS VARCHAR(20)) AS BillNo ,  
                        NET_AMOUNT AS BillAmount ,  
                        fk_CessId_num ,  
                        dbo.CessMaster.CessPercentage_num ,  
                        dbo.SALE_INVOICE_DETAIL.ITEM_QTY ,            
          --orderdetails.Price_num ,    
                        CAST(( CAST(( ISNULL(ITEM_QTY, 0) * ISNULL(ITEM_RATE,  
                                                              0)  
                                      - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'  
                                                      THEN ( ( ISNULL(ITEM_QTY,  
                                                              0)  
                                                              * ISNULL(ITEM_RATE,  
                                                              0) )  
                                                             * DISCOUNT_VALUE )  
                                                           / 100  
                                                      ELSE DISCOUNT_VALUE  
                                                 END, 0) ) ) AS DECIMAL(18, 2))  
                               - CASE WHEN GSTPaid = 'y'  
                                      THEN ( ( ( ISNULL(ITEM_QTY, 0)  
                                                 * ISNULL(ITEM_RATE, 0) )  
                                               - ( ISNULL(CASE  
                                                              WHEN DISCOUNT_TYPE = 'P'  
                                                              THEN ( ( ISNULL(ITEM_QTY,  
                                                              0)  
                                                              * ISNULL(ITEM_RATE,  
                                                              0) )  
                                                              * DISCOUNT_VALUE )  
                                                              / 100  
                                                              ELSE DISCOUNT_VALUE  
                                                          END, 0) ) )  
                                             - ( ( ( ISNULL(ITEM_QTY, 0)  
                                                     * ISNULL(ITEM_RATE, 0) )  
                                                   - ( ISNULL(CASE  
                                                              WHEN DISCOUNT_TYPE = 'P'  
                                                              THEN ( ( ISNULL(ITEM_QTY,  
                                                              0)  
                                                              * ISNULL(ITEM_RATE,  
                                                              0) )  
                                                              * DISCOUNT_VALUE )  
                                                              / 100  
                                                              ELSE DISCOUNT_VALUE  
                                                              END, 0) ) )  
                                                 / ( 1 + VAT_PER / 100 ) ) )  
                                      ELSE 0  
                                 END ) AS DECIMAL(18, 2)) AS GrossAmount ,  
                        dbo.SALE_INVOICE_DETAIL.VAT_AMOUNT ,  
                        CessAmount_num ,  
                        ACessAmount ,  
                        dbo.SALE_INVOICE_DETAIL.MRP AS MRP_num ,    
       --SellingPrice_num ,    
                        dbo.ITEM_MASTER.ITEM_ID ,  
                        ITEM_NAME ,  
                        BarCode_vch ,  
                        ITEM_CODE ,  
                        UM_Name ,  
                        dbo.ITEM_MASTER.fk_hsnid_num ,  
                        HsnCode_vch ,  
                        ITEM_CAT_ID ,  
                        ITEM_CAT_NAME ,  
                        VAT_MASTER.vat_id AS TaxID_num ,  
                        VAT_PER AS TaxPercentage_num ,  
                        SI_DATE AS OrderDate ,  
                        BrandItems.Pk_LabelDetailId_Num AS BrandId ,  
                        BrandItems.LabelItemName_vch AS BrandName ,  
                        SizeItems.Pk_LabelDetailId_Num AS SizeId ,  
                        SizeItems.LabelItemName_vch AS SizeName ,  
                        ColorItems.Pk_LabelDetailId_Num AS ColorId ,  
                        ColorItems.LabelItemName_vch AS ColorName ,  
                        CompanyItems.Pk_LabelDetailId_Num AS CompanyId ,  
                        CompanyItems.LabelItemName_vch AS CompanyName ,  
                        DepartmentItems.Pk_LabelDetailId_Num AS DepartmentId ,  
                        DepartmentItems.LabelItemName_vch AS DepartmentName ,  
                        TypeItems.Pk_LabelDetailId_Num AS TypeId ,  
                        TypeItems.LabelItemName_vch AS TypeName,
                        SALE_INVOICE_MASTER.freight,
                        SALE_INVOICE_MASTER.FreightTaxValue
              FROM      dbo.SALE_INVOICE_MASTER  
                        JOIN dbo.SALE_INVOICE_DETAIL ON SALE_INVOICE_DETAIL.SI_ID = dbo.SALE_INVOICE_MASTER.SI_ID  
                        JOIN dbo.ITEM_MASTER ON ITEM_MASTER.ITEM_ID = dbo.SALE_INVOICE_DETAIL.ITEM_ID  
                        JOIN dbo.ITEM_CATEGORY ON ITEM_CATEGORY.ITEM_CAT_ID = dbo.ITEM_MASTER.ITEM_CATEGORY_ID  
                        LEFT JOIN dbo.CessMaster ON cessmaster.pk_CessId_num = ITEM_MASTER.fk_CessId_num  
                        JOIN dbo.HsnCode_Master ON Pk_HsnId_num = fk_hsnid_num  
                        JOIN dbo.UNIT_MASTER ON dbo.UNIT_MASTER.UM_ID = ITEM_MASTER.UM_ID  
                        JOIN dbo.VAT_MASTER ON VAT_MASTER.VAT_PERCENTAGE = dbo.SALE_INVOICE_DETAIL.VAT_PER          
                  
------------------------------------------Brand-------------------------------------------------------------------------------------          
                        LEFT JOIN dbo.LabelItem_Mapping AS Brand ON Brand.Fk_ItemId_Num = dbo.ITEM_MASTER.ITEM_ID  
                                                              AND Brand.Fk_LabelDetailId IN (  
                                                              SELECT  
                                                              Pk_LabelDetailId_Num  
                                                              FROM  
                                                              dbo.Label_Items  
                                                              WHERE  
                                            fk_LabelId_num = 1 )  
                        LEFT JOIN dbo.Label_Items AS BrandItems ON BrandItems.Pk_LabelDetailId_Num = Brand.Fk_LabelDetailId  
                                                              AND BrandItems.fk_LabelId_num = 1  
                        LEFT JOIN dbo.Label_Master AS BrandMaster ON BrandMaster.Pk_LabelId_Num = BrandItems.fk_LabelId_num  
                                                              AND BrandMaster.Pk_LabelId_Num = 1          
                                                               
---------------------------------------------Size-----------------------------------------------------------------------------------            
                        LEFT JOIN dbo.LabelItem_Mapping AS Size ON Size.Fk_ItemId_Num = dbo.ITEM_MASTER.ITEM_ID  
                                                              AND Size.Fk_LabelDetailId IN (  
                                                              SELECT  
                                                              Pk_LabelDetailId_Num  
                                                              FROM  
                                                              dbo.Label_Items  
                                                              WHERE  
                                                              fk_LabelId_num = 2 )  
                        LEFT JOIN dbo.Label_Items AS SizeItems ON SizeItems.Pk_LabelDetailId_Num = Size.Fk_LabelDetailId  
                                                              AND SizeItems.fk_LabelId_num = 2  
                        LEFT JOIN dbo.Label_Master AS SizeMaster ON SizeMaster.Pk_LabelId_Num = SizeItems.fk_LabelId_num  
                                                              AND SizeMaster.Pk_LabelId_Num = 2          
                                                              
---------------------------------------------Color-----------------------------------------------------------------------------------          
                        LEFT JOIN dbo.LabelItem_Mapping AS Color ON Color.Fk_ItemId_Num = dbo.ITEM_MASTER.ITEM_ID  
                                                              AND Color.Fk_LabelDetailId IN (  
                                                              SELECT  
                                                              Pk_LabelDetailId_Num  
                                                              FROM  
                                                              dbo.Label_Items  
                                                              WHERE  
                                                              fk_LabelId_num = 3 )  
                        LEFT JOIN dbo.Label_Items AS ColorItems ON ColorItems.Pk_LabelDetailId_Num = Color.Fk_LabelDetailId  
                                                              AND ColorItems.fk_LabelId_num = 3  
                        LEFT JOIN dbo.Label_Master AS ColorMaster ON ColorMaster.Pk_LabelId_Num = ColorItems.fk_LabelId_num  
                                                              AND ColorMaster.Pk_LabelId_Num = 3          
                                                              
---------------------------------------------Company-----------------------------------------------------------------------------------          
                        LEFT JOIN dbo.LabelItem_Mapping AS Company ON Company.Fk_ItemId_Num = dbo.ITEM_MASTER.ITEM_ID  
                                                              AND Company.Fk_LabelDetailId IN (  
                                                              SELECT  
                                                              Pk_LabelDetailId_Num  
                                                              FROM  
                                                              dbo.Label_Items  
                                                              WHERE  
         fk_LabelId_num = 4 )  
                        LEFT JOIN dbo.Label_Items AS CompanyItems ON CompanyItems.Pk_LabelDetailId_Num = Company.Fk_LabelDetailId  
                                                              AND CompanyItems.fk_LabelId_num = 4  
                        LEFT JOIN dbo.Label_Master AS CompanyMaster ON CompanyMaster.Pk_LabelId_Num = CompanyItems.fk_LabelId_num  
                                                              AND CompanyMaster.Pk_LabelId_Num = 4          
                                                              
---------------------------------------------Department-----------------------------------------------------------------------------------          
                        LEFT JOIN dbo.LabelItem_Mapping AS Department ON Department.Fk_ItemId_Num = dbo.ITEM_MASTER.ITEM_ID  
                                                              AND Department.Fk_LabelDetailId IN (  
                                                              SELECT  
                                                              Pk_LabelDetailId_Num  
                                                              FROM  
                                                              dbo.Label_Items  
                                                              WHERE  
                                                              fk_LabelId_num = 5 )  
                        LEFT JOIN dbo.Label_Items AS DepartmentItems ON DepartmentItems.Pk_LabelDetailId_Num = Department.Fk_LabelDetailId  
                                                              AND DepartmentItems.fk_LabelId_num = 5  
                        LEFT JOIN dbo.Label_Master AS DepartmentMaster ON DepartmentMaster.Pk_LabelId_Num = DepartmentItems.fk_LabelId_num  
                                                              AND DepartmentMaster.Pk_LabelId_Num = 5          
                                                              
---------------------------------------------Type-----------------------------------------------------------------------------------------          
                        LEFT JOIN dbo.LabelItem_Mapping AS Type ON Type.Fk_ItemId_Num = dbo.ITEM_MASTER.ITEM_ID  
                                                              AND Type.Fk_LabelDetailId IN (  
                                                              SELECT  
                                                              Pk_LabelDetailId_Num  
                                                              FROM  
                                                              dbo.Label_Items  
                                                              WHERE  
                                                              fk_LabelId_num = 6 )  
                        LEFT JOIN dbo.Label_Items AS TypeItems ON TypeItems.Pk_LabelDetailId_Num = Type.Fk_LabelDetailId  
                                                              AND TypeItems.fk_LabelId_num = 6  
                        LEFT JOIN dbo.Label_Master AS TypeMaster ON TypeMaster.Pk_LabelId_Num = TypeItems.fk_LabelId_num  
                                                              AND TypeMaster.Pk_LabelId_Num = 6  
                        JOIN dbo.ACCOUNT_MASTER ON ACC_ID = SALE_INVOICE_MASTER.cust_ID  
              WHERE     dbo.SALE_INVOICE_MASTER.INVOICE_STATUS <> 4  
            ) tb    
---------------------------------------------End-----------------------------------------------------------------------------------------          
            GO
			----------------------------------------
