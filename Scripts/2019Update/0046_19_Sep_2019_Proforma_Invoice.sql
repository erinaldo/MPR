INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0046_19_Sep_2019_Proforma_Invoice' ,
          GETDATE()
        )

GO

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
      @v_TempInvoiceId DECIMAL(18, 0)= NULL , 
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
                          ConsumerHeadID  ,
                          TempInvoiceId
                                                 
                        
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
                          @V_ConsumerHeadID,  
                          @V_TempInvoiceId                      
                        
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

CREATE PROCEDURE [dbo].[PROC_PROFORMA_INVOICE_MASTER]  
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
                                          + ISNULL(@v_ACESS_AMOUNT, 0)  
                                          + ISNULL(@v_freight, 0) )                            
                          
        SET @InputID = ( SELECT CASE WHEN @v_INV_TYPE = 'I' THEN 10021  
                                     WHEN @v_INV_TYPE = 'S' THEN 10024  
                                     WHEN @v_INV_TYPE = 'U' THEN 10075  
                                END AS inputid  
                       )                        
                          
        IF @V_MODE = 1  
            BEGIN                         
                                      
                INSERT  INTO PROFORMA_INVOICE_MASTER  
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
                             
            END  
                          
        IF @V_MODE = 2  
            BEGIN                            
                            
                            
                DECLARE @TransactionDate DATETIME                        
                            
                SELECT  @TransactionDate = SI_DATE  
                FROM    PROFORMA_INVOICE_MASTER  
                WHERE   SI_ID = @V_SI_ID                          
                          
                          
                UPDATE  PROFORMA_INVOICE_MASTER  
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
                                 
                          
            END                    
                          
    END 
    
    GO
    
    CREATE PROCEDURE [dbo].[PROC_PROFORMA_INVOICE_DETAIL]    
    (    
      @v_SI_ID DECIMAL ,    
      @v_ITEM_ID DECIMAL ,    
      @v_ITEM_QTY DECIMAL(18, 3) ,    
      @v_PKT DECIMAL(18, 3) ,    
      @v_ITEM_RATE DECIMAL(18, 3) ,    
      @v_ITEM_AMOUNT DECIMAL(18, 3) ,    
      @v_VAT_PER DECIMAL(18, 3) ,    
      @v_VAT_AMOUNT DECIMAL(18, 3) ,    
      @v_MRP DECIMAL(18, 3) ,    
      @v_CESS_PER DECIMAL(18, 3) ,    
      @v_ACESS DECIMAL(18, 3) ,    
      @v_CESS_AMOUNT DECIMAL(18, 3) ,    
      @v_CREATED_BY VARCHAR(50) ,    
      @v_CREATION_DATE DATETIME ,    
      @v_MODIFIED_BY VARCHAR(50) ,    
      @v_MODIFIED_DATE DATETIME ,    
      @v_DIVISION_ID INT ,    
      @v_TARRIF_ID NUMERIC(18, 0) ,    
      @v_DISCOUNT_TYPE VARCHAR(10) ,    
      @v_DISCOUNT_VALUE DECIMAL(18, 3) ,    
      @V_MODE INT ,    
      @V_Freight NUMERIC(18, 2) = 0.00 ,    
      @V_Freight_type CHAR(10) = 'A' ,    
      @V_FreightTaxValue NUMERIC(18, 2) = 0.00 ,    
      @V_FreightCessValue NUMERIC(18, 2) = 0.00 ,    
      @v_GSTPAID VARCHAR(5) = 'N'              
    )    
AS    
    BEGIN                    
        IF @V_MODE = 1    
            BEGIN           
                             
              
                INSERT  INTO PROFORMA_INVOICE_DETAIL    
                        ( SI_ID ,    
                          ITEM_ID ,    
                          ITEM_QTY ,    
                          PKT ,    
                          ITEM_RATE ,    
                          ITEM_AMOUNT ,    
                          VAT_PER ,    
                          VAT_AMOUNT ,    
                          BAL_ITEM_RATE ,    
                          BAL_ITEM_QTY ,    
                          CREATED_BY ,    
                          CREATION_DATE ,    
                          MODIFIED_BY ,    
                          MODIFIED_DATE ,    
                          DIVISION_ID ,    
                          TARRIF_ID ,    
                          DISCOUNT_TYPE ,    
                          DISCOUNT_VALUE ,    
                          GSTPaid ,    
                          MRP ,    
                          CessPercentage_num ,    
                          CessAmount_num ,    
                          ACess ,    
                          ACessAmount ,    
                          Freight ,    
                          Freight_type ,    
                          FreightTaxValue ,    
                          FreightCessValue           
                        )    
                VALUES  ( @V_SI_ID ,    
                          @V_ITEM_ID ,    
                          @V_ITEM_QTY ,    
                          @v_PKT ,    
                          @V_ITEM_RATE ,    
                          @V_ITEM_AMOUNT ,    
                          @V_VAT_PER ,    
                          @V_VAT_AMOUNT ,    
                          @V_ITEM_RATE ,    
                          @V_ITEM_QTY ,    
                          @V_CREATED_BY ,    
                          @V_CREATION_DATE ,    
                          @V_MODIFIED_BY ,    
                          @V_MODIFIED_DATE ,    
                          @V_DIVISION_ID ,    
                          @v_TARRIF_ID ,    
                          @v_DISCOUNT_TYPE ,    
                          @v_DISCOUNT_VALUE ,    
                          @v_GSTPAID ,    
                          @v_MRP ,    
                          @v_CESS_PER ,    
                          @v_CESS_AMOUNT ,    
                          @v_ACESS ,    
                          ISNULL(@v_ACESS, 0) * @V_ITEM_QTY ,    
                          @V_Freight ,    
                          @V_Freight_type ,    
                          @V_FreightTaxValue ,    
                          @V_FreightCessValue            
                        )                   
                        
              
            END                  
                 
    END 
    
    GO
    
    CREATE PROC [dbo].[GET_PROFORMA_INV_DETAIL] ( @V_SI_ID NUMERIC(18, 0) )    
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
                Remarks,  
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
        FROM    dbo.PROFORMA_INVOICE_MASTER    
        WHERE   SI_ID = @V_SI_ID          
          
    END 
    
    GO
    
    CREATE PROCEDURE [dbo].[GET_PROFORMA_INV_ITEM_DETAILS] (@V_SI_ID NUMERIC(18, 0) )      
AS      
    BEGIN                     
                    
        SELECT  IM.ITEM_ID ,      
                IM.BarCode_vch AS ITEM_CODE ,      
                ITEM_NAME ,      
                UM_Name ,      
                '' AS Batch_no ,              
                sd.Balance_Qty AS Batch_Qty ,            
               -- 0.00 AS Batch_Qty ,          
                --CAST(Balance_Qty AS NUMERIC(18,2)) AS Batch_Qty ,              
                CAST(SID.Item_Qty AS NUMERIC(18, 2)) AS transfer_Qty ,      
                GETDATE() AS  Expiry_date ,      
                ISNULL(sd.STOCK_DETAIL_ID,0) AS  STOCK_DETAIL_ID,      
                ITEM_RATE ,      
                ISNULL(DISCOUNT_TYPE, 'P') AS DType ,      
                ISNULL(DISCOUNT_VALUE, 0) AS DISC ,      
                ISNULL(GSTPAID, 'N') AS GPAID ,      
                CAST(ISNULL(SID.Item_Qty, 0) * ISNULL(ITEM_RATE, 0) AS DECIMAL(18,      
                                                              2)) AS Amount ,      
                VAT_PER AS GST ,      
                VAT_AMOUNT AS GST_Amount ,      
                fk_HsnId_num AS HsnCodeId ,      
                SID.MRP ,      
                ISNULL(SID.CessPercentage_num, 0) AS Cess ,      
                ISNULL(SID.ACess, 0) AS ACess ,      
                SID.CessAmount_num AS Cess_Amount ,      
                0 AS LandingAmt ,      
                ISNULL(SID.Freight, 0) AS Freight ,      
                ISNULL(SID.Freight_type, 'A') AS Freight_type ,      
                ISNULL(SID.FreightTaxValue, 0) AS FreightTaxValue ,      
                ISNULL(SID.FreightCessValue, 0) AS FreightCessValue            
                --,              
                --SISD.ITEM_QTY              
        FROM    dbo.PROFORMA_INVOICE_DETAIL SID      
                JOIN dbo.ITEM_MASTER IM ON IM.ITEM_ID = SID.ITEM_ID      
                JOIN dbo.UNIT_MASTER UM ON UM.UM_ID = IM.UM_ID     
                JOIN dbo.STOCK_DETAIL sd ON sd.Item_id = SID.ITEM_ID                     
        WHERE   SID.SI_ID = @V_SI_ID      
        ORDER BY SID.SI_ID ASC            
                    
    END     

	GO

	ALTER PROCEDURE [dbo].[GET_ITEM_BY_ID] ( @V_ITEM_ID NUMERIC )  
AS  
    BEGIN          
        
        SELECT  IM.ITEM_ID ,  
                IM.ITEM_CODE ,  
                IM.ITEM_NAME ,  
                UM.UM_NAME ,  
                vm.VAT_PERCENTAGE ,  
                ISNULL(cm.CessPercentage_num, 0.00) AS CessPercentage_num ,  
                ISNULL(IM.ACess, 0.00) AS ACess ,  
                dbo.Get_Current_Stock(IM.ITEM_ID) AS Current_Stock ,  
                IC.item_cat_name ,  
                --CAST(dbo.Get_item_rate_from_previous_mrn(@V_ITEM_ID) AS NUMERIC(18,  
                --                                              2)) AS prev_mrn_rate,
                CAST(ISNULL(IM.Purchase_rate ,DBO.Get_Average_Rate_as_on_date(IM.ITEM_ID,
                                                    GETDATE(),
                                                    IM.DIVISION_ID, 1)) AS NUMERIC(18,  
                                                              2)) AS prev_mrn_rate
        FROM    ITEM_MASTER IM  
                INNER JOIN VAT_MASTER vm ON vm.VAT_ID = IM.vat_id  
                LEFT JOIN dbo.CessMaster cm ON cm.pk_CessId_num = IM.fk_CessId_num  
                INNER JOIN UNIT_MASTER UM ON IM.UM_ID = UM.UM_ID  
                INNER JOIN item_category IC ON IM.item_category_id = IC.item_cat_id  
        WHERE   IM.ITEM_ID = @V_ITEM_ID       
        
    END       


    
    