INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0044_08_Apr_2019_AddConsumer' ,
          GETDATE()
        )
Go
---------------------------------------------------------------------------------------------------------------------------

------------------------------------------Run this Qury in AdminPlus-------------------------------------------------------
INSERT  INTO Adminplus.dbo.ACCOUNT_MASTER( ACC_ID , ACC_CODE , ACC_NAME ,ACC_DESC , AG_ID , OPENING_BAL , OPENING_BAL_TYPE ,
          CONTACT_PERSON ,PERSON_DESIGNATION , PHONE_PRIM , PHONE_SEC , MOBILE_NO ,ADDRESS_PRIM , ADDRESS_SEC ,
          CITY_ID ,VAT_NO ,VAT_DATE , IS_OUTSIDE ,CREATED_BY ,CREATION_DATE ,MODIFIED_BY ,MODIFIED_DATE ,
          DIVISION_ID ,Fk_HSN_ID ,fk_GST_ID ,FK_GST_TYPE_ID ,Is_Active
        )
VALUES  ( 10088 , 'RE' ,'RCM Expenses' ,'RCM Expenses' ,11 ,0.00 ,'Dr' , '' , '' , '' , '' , '' , '' ,
          '' , 1 ,'' ,'2018-03-26 00:00:00.000' , 0 ,'Admin' ,'2018-03-26 00:00:00.000' ,'' ,'2019-04-18 12:53:42' ,
          1 , NULL , NULL , NULL ,1  
        )
---------------------------------------------------------------------------------------------------------



ALTER TABLE dbo.SALE_INVOICE_MASTER ADD ConsumerHeadID NUMERIC(18,2) DEFAULT 0,
freight NUMERIC(18,2) DEFAULT 0,freight_type CHAR(10),FreightTaxApplied BIT DEFAULT 0,
FreightTaxValue   NUMERIC(18,2) DEFAULT 0

ALTER TABLE dbo.SALE_INVOICE_DETAIL ADD
freight NUMERIC(18,2) DEFAULT 0,freight_type CHAR(10),
FreightTaxValue NUMERIC(18,2) DEFAULT 0,FreightCessValue NUMERIC(18,2) DEFAULT 0


ALTER TABLE SettlementDetail ADD OpenCrAmount NUMERIC(18,2)DEFAULT 0, OpenCrNo NVARCHAR(50),
 OpenDrAmount NUMERIC(18,2)DEFAULT 0, OpenDrNO NVARCHAR(50)

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
---------------------------------------------------------------------------------------------------------------------------

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
 GO
---------------------------------------------End-----------------------------------------------------------------------------------------          
ALTER TABLE dbo.CreditNote_Master ADD ConsumerHeadID NUMERIC(18,2) DEFAULT 0
Go
---------------------------------------------------------------------------------------------------------------------------
-----------------------Credit Note-----------------------------------------------------------------------------------------    
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
                          WHERE     CreditNote_Id = @CreditNoteId
                        )         
                          
        SET @v_INV_TYPE = ( SELECT  GSTType
                            FROM    dbo.CreditNote_Master
                            WHERE   CreditNote_Id = @CreditNoteId
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
 ALTER PROCEDURE [dbo].[Proc_DebitNoteUpdateDeleteLedgerEntries]
    (
      @DebitNoteId VARCHAR(50) ,
      @TransactionTypeId INT              
    )
AS
    BEGIN                
                      
        DECLARE @ACC_ID NUMERIC(18, 2)         
        DECLARE @MRNId NUMERIC(18, 2)         
                
        SELECT  @ACC_ID = DN_CustId
        FROM    ( SELECT    DN_CustId  --CASE WHEN ISNULL(mrwpm.MRN_NO,0) = 0 THEN mrapm.MRN_NO end        
                  FROM      dbo.DebitNote_master dn
                  WHERE     dn.DebitNote_Id = @DebitNoteId
                  UNION ALL
                  SELECT    DN_CustId
                  FROM      dbo.DebitNote_master dn
                  WHERE     dn.DebitNote_Id = @DebitNoteId
                ) tb        
                
                
        UPDATE  STOCK_DETAIL
        SET     STOCK_DETAIL.Issue_Qty = ( STOCK_DETAIL.Issue_Qty
                                           - debitNote_DETAIL.ITEM_QTY ) ,
                STOCK_DETAIL.Balance_Qty = ( STOCK_DETAIL.Balance_Qty
                                             + debitNote_DETAIL.ITEM_QTY )
        FROM    dbo.STOCK_DETAIL
                JOIN dbo.debitNote_DETAIL ON debitNote_DETAIL.STOCK_DETAIL_ID = STOCK_DETAIL.STOCK_DETAIL_ID
                                             AND debitNote_DETAIL.ITEM_ID = STOCK_DETAIL.Item_id
        WHERE   DebitNote_Id = @DebitNoteId      
                
        DELETE  FROM dbo.debitNote_DETAIL
        WHERE   DebitNote_Id = @DebitNoteId       
                      
                      
        DECLARE @CashOut NUMERIC(18, 2)                
        DECLARE @CashIn NUMERIC(18, 2)               
                      
        SET @CashOut = 0              
                      
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @DebitNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = @ACC_ID
                      )                
                
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @ACC_ID              
                      
                      
                      
                      
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @DebitNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = 10073
                       )                
                   
        SET @CashIn = 0           
                  
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10073                  
                      
                      
                      
                      
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @DebitNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = 10070
                       )                  
                  
        SET @CashIn = 0       
                  
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10070         
                
        SET @CashIn = 0           
        SET @CashOut = 0              
                     
        DECLARE @V_MRN_TYPE VARCHAR(1)                  
        --SET @V_MRN_TYPE = ( SELECT  MRN_TYPE
        --                    FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
        --                    WHERE   MRN_NO = @MRNId
        --                    UNION
        --                    SELECT  MRN_TYPE
        --                    FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
        --                    WHERE   MRN_NO = @MRNId
        --                  )      
        SET @V_MRN_TYPE = ( SELECT GSTType from dbo.DebitNote_Master WHERE DebitNote_Id=@DebitNoteId)
                                                           
                      
        DECLARE @InputID NUMERIC                  
        DECLARE @CInputID NUMERIC                  
        SET @CInputID = 10016             
        DECLARE @CessInputID NUMERIC= 0                
        SET @CessInputID = 10013                
                      
        SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10023
                                     WHEN @V_MRN_TYPE = 2 THEN 10020
                                     WHEN @V_MRN_TYPE = 3 THEN 10074
                                END AS inputid
                       )               
                                     
                                     
                                     
        IF @V_MRN_TYPE <> 2
            BEGIN                  
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @DebitNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @CInputID
                               )                  
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @CInputID                  
                  
                  
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @DebitNoteId
                                        AND TransactionTypeId = @TransactionTypeId
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
                                 WHERE  TransactionId = @DebitNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @InputID
                               )                  
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @InputID                  
            END             
                        
                        
        -------Start Cess Entries Deletion --------            
                             
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @DebitNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = @CessInputID
                       )           
                                         
        SET @CashIn = 0           
                             
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @CessInputID             
                  
                            
        -------End Cess Entries Deletion --------              
                      
        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @DebitNoteId
                AND TransactionTypeId = @TransactionTypeId               
                         
                      
    END   
 Go
---------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[GET_INV_ITEM_DETAILS] (@V_SI_ID NUMERIC(18, 0) )
AS
    BEGIN               
              
        SELECT  IM.ITEM_ID ,
                IM.ITEM_CODE ,
                ITEM_NAME ,
                UM_Name ,
                Batch_no ,        
                --Balance_Qty + SISD.Item_Qty AS Batch_Qty ,      
                Balance_Qty + SISD.Item_Qty AS Batch_Qty ,    
                --CAST(Balance_Qty AS NUMERIC(18,2)) AS Batch_Qty ,        
                CAST(SISD.Item_Qty AS NUMERIC(18, 2)) AS transfer_Qty ,
                Expiry_date ,
                sd.STOCK_DETAIL_ID ,
                ITEM_RATE ,
                ISNULL(DISCOUNT_TYPE, 'P') AS DType ,
                ISNULL(DISCOUNT_VALUE, 0) AS DISC ,
                ISNULL(GSTPAID, 'N') AS GPAID ,
                CAST(ISNULL(SISD.Item_Qty, 0) * ISNULL(ITEM_RATE, 0) AS DECIMAL(18,
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
        FROM    dbo.SALE_INVOICE_DETAIL SID
                JOIN dbo.ITEM_MASTER IM ON IM.ITEM_ID = SID.ITEM_ID
                JOIN dbo.UNIT_MASTER UM ON UM.UM_ID = IM.UM_ID
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL SISD ON SISD.ITEM_ID = SID.Item_id
                                                           AND SISD.SI_ID = SID.SI_ID
                JOIN dbo.STOCK_DETAIL SD ON SD.Item_id = SID.ITEM_ID
                                            AND SD.STOCK_DETAIL_ID = SISD.STOCK_DETAIL_ID
        WHERE   SID.SI_ID = @V_SI_ID
        ORDER BY SID.SI_ID ASC      
              
    END 
GO
---------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[PROC_OUTSIDE_SALE_DETAIL_NEW]
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
          
                INSERT  INTO SALE_INVOICE_DETAIL
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
                UPDATE  item_detail
                SET     current_stock = current_stock - @v_ITEM_QTY
                WHERE   item_id = @V_ITEM_ID
                        AND div_id = @V_DIVISION_ID            
          
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
            END            
    END 
GO
------------------------------------------------------Purchase-------------------------------------------------------------
ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL ADD
freight NUMERIC(18,2) DEFAULT 0,freight_type CHAR(10),
FreightTaxValue NUMERIC(18,2) DEFAULT 0,FreightCessValue NUMERIC(18,2) DEFAULT 0

ALTER TABLE dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL ADD
freight NUMERIC(18,2) DEFAULT 0,freight_type CHAR(10),
FreightTaxValue NUMERIC(18,2) DEFAULT 0,FreightCessValue NUMERIC(18,2) DEFAULT 0

ALTER TABLE dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER ADD
IS_RCM_Applicable BIT DEFAULT 0

ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER ADD
IS_RCM_Applicable BIT DEFAULT 0

GO

---------------------------------------------------------------------------------------------------------------------------
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
      @v_CashDiscount_amt NUMERIC(18, 2) = NULL ,
      @v_GROSS_AMOUNT NUMERIC(18, 2) ,
      @v_GST_AMOUNT NUMERIC(18, 2) ,
      @v_CESS_AMOUNT NUMERIC(18, 2) ,
      @v_ACESS_AMOUNT NUMERIC(18, 2) ,
      @v_NET_AMOUNT NUMERIC(18, 2) ,
      @V_MRN_TYPE INT ,
      @V_VAT_ON_EXICE INT ,
      @v_MRNCompanies_ID INT ,
      @V_Special_Scheme CHAR(10) ,
      @V_FreightTaxApplied INT ,
      @V_FreightTaxValue NUMERIC(18, 2) ,
      @V_FK_ITCEligibility_ID NUMERIC(18, 0) ,
      @V_IS_RCM_Applicable BIT = 0 ,
      @V_Reference_ID NUMERIC(18, 0)
    )
AS
    BEGIN                                           
        DECLARE @Remarks VARCHAR(250)                                          
        DECLARE @InputID NUMERIC     
        DECLARE @RInputID NUMERIC  
                                               
        DECLARE @CInputID NUMERIC                                          
        SET @CInputID = 10016    
          
        DECLARE @RCInputID NUMERIC                                          
        SET @RCInputID = 10018                                            
          
        DECLARE @RoundOff NUMERIC(18, 2)                                          
        DECLARE @CGST_Amount NUMERIC(18, 2)                                          
        SET @CGST_Amount = ( @v_GST_AMOUNT / 2 )                                          
        SET @RoundOff = ( @v_Other_Charges )                                          
                                          
        SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10023
                                     WHEN @V_MRN_TYPE = 2 THEN 10020
                                     WHEN @V_MRN_TYPE = 3 THEN 10074
                                END AS inputid
                       )  
                                  
                                  
        SET @RInputID = ( SELECT    CASE WHEN @V_MRN_TYPE = 1 THEN 10025
                                         WHEN @V_MRN_TYPE = 2 THEN 10022
                                         WHEN @V_MRN_TYPE = 3 THEN 10076
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
                          CashDiscount_Amt ,
                          freight_type ,
                          MRNCompanies_ID ,
                          GROSS_AMOUNT ,
                          GST_AMOUNT ,
                          CESS_AMOUNT ,
                          NET_AMOUNT ,
                          MRN_TYPE ,
                          VAT_ON_EXICE ,
                          IsPrinted ,
                          SpecialSchemeFlag ,
                          FreightTaxApplied ,
                          FreightTaxValue ,
                          FK_ITCEligibility_ID ,
                          Reference_ID ,
                          IS_RCM_Applicable                                        
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
                          @v_CashDiscount_amt ,
                          @v_freight_type ,
                          @v_MRNCompanies_ID ,
                          @v_GROSS_AMOUNT ,
                          @v_GST_AMOUNT ,
                          ( ISNULL(@v_CESS_AMOUNT, 0) + ISNULL(@v_ACESS_AMOUNT,
                                                              0) ) ,
                          @v_NET_AMOUNT ,
                          @v_MRN_TYPE ,
                          @V_VAT_ON_EXICE ,
                          0 ,
                          @V_Special_Scheme ,
                          @V_FreightTaxApplied ,
                          @V_FreightTaxValue ,
                          @V_FK_ITCEligibility_ID ,
                          @V_Reference_ID ,
                          @V_IS_RCM_Applicable                    
                        )                                            
                                          
                                          
                UPDATE  MRN_SERIES
                SET     CURRENT_USED = CURRENT_USED + 1
                WHERE   DIV_ID = @v_Division_ID       
                    
                    
                --Minus Cash Discount---    
                    
                SET @v_GROSS_AMOUNT = ( @v_GROSS_AMOUNT
                                        - ISNULL(@v_CashDiscount_amt, 0) )                                         
                                          
                SET @Remarks = 'Purchase against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                                           
                                          
                  
                IF ( @V_IS_RCM_Applicable = 0 )
                    BEGIN 
                                          
                        EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT,
                            0, @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @v_Received_Date, @V_Created_BY                            
                    END
                  
                ELSE
                    BEGIN
                        DECLARE @GAmount NUMERIC(18, 2)
                        SET @GAmount = ( ISNULL(@v_GROSS_AMOUNT, 0)
                                         + ISNULL(@v_freight, 0) )
                        EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @GAmount, 0,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @v_Received_Date, @V_Created_BY  
                    END
                  
                  
                                          
                EXECUTE Proc_Ledger_Insert @V_Reference_ID, 0, @v_GROSS_AMOUNT,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2,
                    @v_Received_Date, @V_Created_BY                     
                                          
                                          
                SET @Remarks = 'Freight against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                  
                                          
                EXECUTE Proc_Ledger_Insert 10047, 0, @v_freight, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @v_Received_Date,
                    @V_Created_BY                                             
                                          
                                          
                SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                                           
                                          
                  
                IF ( @V_IS_RCM_Applicable = 0 )
                    BEGIN  
                  
                        IF @V_MRN_TYPE <> 2
                            BEGIN                                  
                                          
                                EXECUTE Proc_Ledger_Insert @CInputID, 0,
                                    @CGST_Amount, @Remarks, @V_Division_ID,
                                    @V_Received_ID, 2, @v_Received_Date,
                                    @V_Created_BY                                          
                                          
                                          
                                EXECUTE Proc_Ledger_Insert @InputID, 0,
                                    @CGST_Amount, @Remarks, @v_Division_ID,
                                    @V_Received_ID, 2, @v_Received_Date,
                                    @V_Created_BY                                          
                            END                               
                                          
                                          
                                          
                        ELSE
                            BEGIN                                          
                                EXECUTE Proc_Ledger_Insert @InputID, 0,
                                    @v_GST_AMOUNT, @Remarks, @V_Division_ID,
                                    @V_Received_ID, 2, @v_Received_Date,
                                    @V_Created_BY                                          
                            END                                 
                   
                   
                   
                                             
                        SET @Remarks = 'Cess against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                                
                                      
                                                
                        EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @v_Received_Date, @V_Created_BY  
                    END  
                ELSE
                    BEGIN  
               
                        IF @V_MRN_TYPE <> 2
                            BEGIN                                  
                                          
                                EXECUTE Proc_Ledger_Insert @RCInputID,
                                    @CGST_Amount, 0, @Remarks, @V_Division_ID,
                                    @V_Received_ID, 2, @v_Received_Date,
                                    @V_Created_BY                                          
                                          
           
                                EXECUTE Proc_Ledger_Insert @RInputID,
                                    @CGST_Amount, 0, @Remarks, @v_Division_ID,
                                    @V_Received_ID, 2, @v_Received_Date,
                                    @V_Created_BY                                          
                            END                               
                                          
                        ELSE
                            BEGIN                                          
                                EXECUTE Proc_Ledger_Insert @RInputID,
                                    @v_GST_AMOUNT, 0, @Remarks, @V_Division_ID,
                                    @V_Received_ID, 2, @v_Received_Date,
                                    @V_Created_BY                                          
                                                             
                            END  
                   
                   
                                             
                        SET @Remarks = 'Cess against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                                
                                      
                                                
                        EXECUTE Proc_Ledger_Insert 10015, @v_CESS_AMOUNT, 0,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @v_Received_Date, @V_Created_BY
                            
                            
                            
                        SET @Remarks = 'Reverse charge against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)
                            
                        DECLARE @RcmExpense NUMERIC(18, 2)
                            
                        SET @RcmExpense = ( ISNULL(@v_GST_AMOUNT, 0)
                                            + ISNULL(@v_CESS_AMOUNT, 0) )
                           
                        EXECUTE Proc_Ledger_Insert 10088, 0, @RcmExpense,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @v_Received_Date, @V_Created_BY           
               
                    END  
                      
                                          
                SET @Remarks = 'Add. Cess against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                      
                                                     
                EXECUTE Proc_Ledger_Insert 10011, 0, @v_ACESS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @v_Received_Date,
                    @V_Created_BY                            
                                          
                                          
                                          
                SET @Remarks = 'Round Off against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                                          
                                          
                IF @RoundOff > 0
                    BEGIN                                          
                                          
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @v_Received_Date, @V_Created_BY                                          
       
                    END                                          
                                          
                ELSE
                    BEGIN                                          
                                          
                        SET @RoundOff = -+@RoundOff                                          
                                 
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @v_Received_Date, @V_Created_BY                                          
                                          
                    END                                           
                                          
                          
                IF @V_Reference_ID = 10070
                    BEGIN          
                          
                        SET @Remarks = 'Stock Out against party  invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                   
                                          
                        EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @v_Received_Date, @V_Created_BY          
                              
                    END                
                                          
            END                     
                                            
        IF @V_PROC_TYPE = 2
            BEGIN                           
                                                    
                DECLARE @TransactionDate DATETIME  = @V_Received_Date                                  
                                                    
                EXEC Proc_ReverseMRNEntry @V_Received_ID, @V_Vendor_ID                                      
                                                    
                --SELECT  @TransactionDate = Received_Date            
                --FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER            
                --WHERE   Received_ID = @V_Received_ID                                    
                                                    
                UPDATE  MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                SET     Purchase_Type = @V_Purchase_Type ,
                        Vendor_ID = @V_Vendor_ID ,
                        Received_Date = @V_Received_Date ,
                        Remarks = @V_Remarks ,
                        Modified_By = @V_Modified_By ,
                        Modification_Date = @V_Modification_Date ,
                        Invoice_No = @v_Invoice_No ,
                        Invoice_Date = @v_Invoice_Date ,
                        mrn_status = @V_mrn_status ,
                        freight = @v_freight ,
                        Other_charges = @v_Other_charges ,
                        Discount_amt = @v_Discount_amt ,
                        CashDiscount_Amt = @v_CashDiscount_amt ,
                        freight_type = @v_freight_type ,
                        MRNCompanies_ID = @v_MRNCompanies_ID ,
                        GROSS_AMOUNT = @v_GROSS_AMOUNT ,
                        GST_AMOUNT = @v_GST_AMOUNT ,
                        CESS_AMOUNT = ( ISNULL(@v_CESS_AMOUNT, 0)
                                        + ISNULL(@v_ACESS_AMOUNT, 0) ) ,
                        NET_AMOUNT = @v_NET_AMOUNT ,
                        MRN_TYPE = @v_MRN_TYPE ,
                        VAT_ON_EXICE = @V_VAT_ON_EXICE ,
                        SpecialSchemeFlag = @V_Special_Scheme ,
                        FreightTaxApplied = @V_FreightTaxApplied ,
                        FreightTaxValue = @V_FreightTaxValue ,
                        FK_ITCEligibility_ID = @V_FK_ITCEligibility_ID ,
                        Reference_ID = @V_Reference_ID ,
                        IS_RCM_Applicable = @V_IS_RCM_Applicable
                WHERE   Received_ID = @V_Received_ID       
                    
                    
                --Minus Cash Discount---    
                    
                SET @v_GROSS_AMOUNT = ( @v_GROSS_AMOUNT
                                        - ISNULL(@v_CashDiscount_amt, 0) )              
                                          
                                          
                SET @Remarks = 'Purchase against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                 
                    
                    
                IF ( @V_IS_RCM_Applicable = 0 )
                    BEGIN
                                           
                        EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT,
                            0, @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY                  
                    
                    END
                ELSE
                    BEGIN
                    
                        SET @GAmount = ( ISNULL(@v_GROSS_AMOUNT, 0)
                                         + ISNULL(@v_freight, 0) )
                        EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @GAmount, 0,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY   
                    
                    END 
                    
                    
                    
                                          
                EXECUTE Proc_Ledger_Insert @V_Reference_ID, 0, @v_GROSS_AMOUNT,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2,
                    @TransactionDate, @V_Created_BY                    
                                          
                                          
                SET @Remarks = 'Freight against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                  
                                          
                EXECUTE Proc_Ledger_Insert 10047, 0, @v_freight, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @TransactionDate,
                    @V_Created_BY                                          
                 
                 
                 
                  
                  
                IF ( @V_IS_RCM_Applicable = 0 )
                    BEGIN                          
                                          
                        SET @Remarks = 'GST against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                                           
                                          
                        IF @V_MRN_TYPE <> 2
                            BEGIN                                      
                                          
                                EXECUTE Proc_Ledger_Insert @CInputID, 0,
                                    @CGST_Amount, @Remarks, @V_Division_ID,
                                    @V_Received_ID, 2, @TransactionDate,
                                    @V_Created_BY                                          
                                          
                                          
                                EXECUTE Proc_Ledger_Insert @InputID, 0,
                                    @CGST_Amount, @Remarks, @v_Division_ID,
                                    @V_Received_ID, 2, @TransactionDate,
                                    @V_Created_BY                                          
                            END                                    
                                          
                        ELSE
                            BEGIN                                          
                                EXECUTE Proc_Ledger_Insert @InputID, 0,
                                    @v_GST_AMOUNT, @Remarks, @V_Division_ID,
                                    @V_Received_ID, 2, @TransactionDate,
                                    @V_Created_BY                                          
                            END                 
                                                    
                        SET @Remarks = 'Cess against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                                
                                      
                                          
                        EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY                                             
                    
                    END  
                ELSE
                    BEGIN  
                        SET @Remarks = 'GST against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                                           
                                          
                        IF @V_MRN_TYPE <> 2
                            BEGIN                                      
                                          
                                EXECUTE Proc_Ledger_Insert @RCInputID,
                                    @CGST_Amount, 0, @Remarks, @V_Division_ID,
                                    @V_Received_ID, 2, @TransactionDate,
                                    @V_Created_BY                                         
                                          
                                          
                                EXECUTE Proc_Ledger_Insert @RInputID,
                                    @CGST_Amount, 0, @Remarks, @v_Division_ID,
                                    @V_Received_ID, 2, @TransactionDate,
                                    @V_Created_BY                                          
                            END                                    
                                          
                        ELSE
                            BEGIN                                          
                                EXECUTE Proc_Ledger_Insert @RInputID,
                                    @v_GST_AMOUNT, 0, @Remarks, @V_Division_ID,
                                    @V_Received_ID, 2, @TransactionDate,
                                    @V_Created_BY                                          
                            END                 
                                                    
                        SET @Remarks = 'Cess against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                                
                                      
                                          
                        EXECUTE Proc_Ledger_Insert 10015, @v_CESS_AMOUNT, 0,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY    
                            
                            
                             
                             
                        SET @Remarks = 'Reverse charge against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)
                            
                        
                            
                        SET @RcmExpense = ( ISNULL(@v_GST_AMOUNT, 0)
                                            + ISNULL(@v_CESS_AMOUNT, 0) )
                           
                        EXECUTE Proc_Ledger_Insert 10088, 0, @RcmExpense,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY
                             
                             
                    
                    END  
                    
                    
                    
                                          
                                          
                SET @Remarks = 'Add. Cess against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                       
                                                    
                EXECUTE Proc_Ledger_Insert 10011, 0, @v_ACESS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @TransactionDate,
                    @V_Created_BY                            
                                          
                SET @Remarks = 'Round Off against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                                          
                                          
                IF @RoundOff > 0
                    BEGIN                                          
                                          
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY                 
                    END                                          
                                   
                ELSE
                    BEGIN                                          
                                          
                        SET @RoundOff = -+@RoundOff                                          
                                          
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY                  
                    END                  
                          
                          
                          
                IF @V_Reference_ID = 10070
                    BEGIN          
           
                        SET @Remarks = 'Stock Out against party  invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                                             
                     
                        EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY           
                              
                    END                                 
                                          
            END                                          
    END 
GO
---------------------------------------------------------------------------------------------------------------------------
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
      @v_Item_cess NUMERIC(18, 4) ,
      @v_A_cess NUMERIC(18, 4) ,
      @v_Item_exice NUMERIC(18, 4) ,
      @v_Batch_no VARCHAR(50) ,
      @v_Expiry_Date DATETIME ,
      @V_Trans_Type NUMERIC(18, 0) ,
      @v_Proc_Type INT ,
      @v_DType CHAR(1) ,
      @v_DiscountValue NUMERIC(18, 2) ,
      @v_DiscountValue1 NUMERIC(18, 2) ,
      @V_Freight NUMERIC(18, 2) = 0.00 ,
      @V_Freight_type CHAR(10) = 'A' ,
      @V_FreightTaxValue NUMERIC(18, 2) = 0.00 ,
      @V_FreightCessValue NUMERIC(18, 2) = 0.00 ,
      @v_GSTPaid CHAR(1)
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
                          Item_cess ,
                          Acess ,
                          Item_exice ,
                          BAtch_No ,
                          Expiry_Date ,
                          Bal_Item_Qty ,
                          Bal_Item_Rate ,
                          Bal_Item_Vat ,
                          Bal_Item_Exice ,
                          Stock_Detail_Id ,
                          DType ,
                          DiscountValue ,
                          DiscountValue1 ,
                          GSTPaid ,
                          Freight ,
                          Freight_type ,
                          FreightTaxValue ,
                          FreightCessValue                              
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
                          @V_Item_cess ,
                          @V_A_cess ,
                          @V_Item_exice ,
                          @v_Batch_no ,
                          @v_Expiry_Date ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @V_Item_vat ,
                          @V_Item_exice ,
                          @STOCK_DETAIL_ID ,
                          @v_DType ,
                          @v_DiscountValue ,
                          @v_DiscountValue1 ,
                          @v_GSTPaid ,
                          @V_Freight ,
                          @V_Freight_type ,
                          @V_FreightTaxValue ,
                          @V_FreightCessValue                    
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
                          Item_cess ,
                          Acess ,
                          Item_exice ,
                          BAtch_No ,
                          Expiry_Date ,
                          Bal_Item_Qty ,
                          Bal_Item_Rate ,
                          Bal_Item_Vat ,
                          Bal_Item_Exice ,
                          Stock_Detail_Id ,
                          DType ,
                          DiscountValue ,
                          DiscountValue1 ,
                          GSTPaid ,
                          Freight ,
                          Freight_type ,
                          FreightTaxValue ,
                          FreightCessValue                              
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
                          @V_Item_cess ,
                          @V_A_cess ,
                          @V_Item_exice ,
                          @v_Batch_no ,
                          @v_Expiry_Date ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @V_Item_vat ,
                          @V_Item_exice ,
                          @STOCK_DETAIL_ID ,
                          @v_DType ,
                          @v_DiscountValue ,
                          @v_DiscountValue1 ,
                          @v_GSTPaid ,
                          @V_Freight ,
                          @V_Freight_type ,
                          @V_FreightTaxValue ,
                          @V_FreightCessValue                        
                        )                          
            END                        
                      
    END 
Go
---------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[Get_MRN_WithOutPO_Detail]
    @V_Receive_ID NUMERIC(18, 0)
AS
    BEGIN                            
                            
        SELECT  Received_ID ,
                Received_Code ,
                Received_No ,
                CONVERT(VARCHAR(20), Received_Date, 106) Received_Date ,
                Purchase_Type ,
                Vendor_ID ,
                Remarks ,
                Division_ID ,
                mrn_status ,
                ISNULL(freight, 0.00) AS freight ,
                other_charges ,
                Discount_amt ,
                ISNULL(CashDiscount_Amt, 0) AS CashDiscount_Amt ,
                MRNCompanies_ID ,
                Invoice_No ,
                Invoice_Date ,
                SpecialSchemeFlag ,
                FreightTaxApplied ,
                FreightTaxValue ,
                FK_ITCEligibility_ID ,
                REFERENCE_ID ,
                ISNULL(IS_RCM_Applicable, 0) AS IS_RCM_Applicable
        FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER
        WHERE   Received_ID = @V_Receive_ID         
                            
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                CAST(MD.Item_Qty AS NUMERIC(18, 3)) AS BATCH_QTY ,
                CAST(MD.Item_Rate AS NUMERIC(18, 2)) AS Item_Rate ,
                ISNULL(DType, 'P') AS DType ,
                CAST(ISNULL(DiscountValue, 0) AS NUMERIC(18, 2)) AS DISC ,
                CAST(ISNULL(DiscountValue1, 0) AS NUMERIC(18, 2)) AS DISC1 ,
                ISNULL(GSTPAID, 'N') AS GPAID ,
                CAST(MD.Item_vat AS NUMERIC(18, 2)) AS Vat_Per ,
                CAST(ISNULL(MD.Item_cess, 0) AS NUMERIC(18, 2)) AS Cess_Per ,
                CAST(ISNULL(MD.Acess, 0) AS NUMERIC(18, 2)) AS Acess ,
                0.0 AS Amount ,
                MD.Item_exice AS exe_Per ,
                MD.Batch_No AS BATCH_NO ,
                MD.Expiry_Date AS EXPIRY_DATE ,
                ISNULL(Freight, 0) AS Freight ,
                ISNULL(Freight_type, 0) AS Freight_type ,
                ISNULL(FreightTaxValue, 0) AS FreightTaxValue ,
                ISNULL(FreightCessValue, 0) AS FreightCessValue
        FROM    MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MD
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON MD.Item_ID = IM.ITEM_ID
        WHERE   MD.Received_ID = @V_Receive_ID                         
                            
        SELECT  dbo.COST_CENTER_MASTER.CostCenter_Id ,
                dbo.COST_CENTER_MASTER.CostCenter_Code ,
                IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                COST_CENTER_MASTER.CostCenter_Name ,
                NonStockable.Item_Qty AS BATCH_QTY ,
                NonStockable.Item_Rate AS Item_Rate ,
                NonStockable.Item_vat AS Vat_Per ,
                NonStockable.Item_cess AS Cess_Per ,
                CAST(ISNULL(NonStockable.Acess, 0) AS NUMERIC(18, 2)) AS Acess ,
                0.0 AS Amount ,
                ISNULL(DType, 'P') AS DType ,
                CAST(ISNULL(DiscountValue, 0) AS NUMERIC(18, 2)) AS DISC ,
                CAST(ISNULL(DiscountValue1, 0) AS NUMERIC(18, 2)) AS DISC1 ,
                ISNULL(GSTPAID, 'N') AS GPAID ,
                NonStockable.Item_Exice AS exe_Per ,
                NonStockable.batch_no AS BATCH_NO ,
                NonStockable.batch_date AS EXPIRY_DATE
        FROM    dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO NonStockable
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON NonStockable.Item_ID = IM.ITEM_ID
                INNER JOIN dbo.COST_CENTER_MASTER ON NonStockable.CostCenter_ID = dbo.COST_CENTER_MASTER.CostCenter_Id
        WHERE   Received_ID = @V_Receive_ID                            
                            
    END  
GO
---------------------------------------------------------------------------------------------------------------------------
ALTER PROC [dbo].[Proc_ReverseMRNEntry]
    (
      @V_Received_ID NUMERIC(18, 0) ,
      @V_Vendor_ID NUMERIC(18, 0)
    )
AS
    BEGIN        
      
      
            
        DECLARE @ReferenceID NUMERIC(18, 0)                                         
        SET @V_Vendor_ID = ( SELECT Vendor_ID
                             FROM   dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                             WHERE  Received_ID = @V_Received_ID
                           )                   
                                               
                                                                                                     
        SET @ReferenceID = ( SELECT REFERENCE_ID
                             FROM   dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                             WHERE  Received_ID = @V_Received_ID
                           )  
                             
                             
                             
        DECLARE @V_IS_RCM_Applicable BIT                                      
          
        SET @V_IS_RCM_Applicable = ( SELECT ISNULL(IS_RCM_Applicable, 0)
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
                            
        SET @CashIn = 0                    
                            
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
                  
                  
        SET @CashOut = 0          
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @V_Received_ID
                                AND TransactionTypeId = 2
                                AND AccountId = 10047
                      )                      
                      
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10047                   
                            
             
              
        IF @ReferenceID = 10070
            BEGIN           
                            
                SET @CashOut = 0                  
                SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                FROM    dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                WHERE   TransactionId = @V_Received_ID
                                        AND TransactionTypeId = 2
                                        AND AccountId = 10073
                              )                      
                         
                        
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = 10073                        
                            
            END                    
                            
                            
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @V_Received_ID
                                AND TransactionTypeId = 2
                                AND AccountId = @ReferenceID
                      )                        
                        
        SET @CashOut = 0                        
                        
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @ReferenceID                    
                            
                               
                               
                               
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
        DECLARE @RInputID NUMERIC                        
        DECLARE @RCInputID NUMERIC      
                            
        SET @CInputID = 10016                      
        SET @RCInputID = 10018   
          
              
        SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10023
                                     WHEN @V_MRN_TYPE = 2 THEN 10020
                                     WHEN @V_MRN_TYPE = 3 THEN 10074
                                END AS inputid
                       )  
                                          
        SET @RInputID = ( SELECT    CASE WHEN @V_MRN_TYPE = 1 THEN 10025
                                         WHEN @V_MRN_TYPE = 2 THEN 10022
                                         WHEN @V_MRN_TYPE = 3 THEN 10076
                                    END AS inputid
                        )   
                                                           
   
   
        IF ( @V_IS_RCM_Applicable = 0 )
            BEGIN  
                            
                IF @V_MRN_TYPE <> 2
                    BEGIN                        
                        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                        FROM    dbo.LedgerDetail
                                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                        WHERE   TransactionId = @V_Received_ID
                                                AND TransactionTypeId = 2
                                                AND AccountId = @CInputID
                                      )                        
                        UPDATE  dbo.LedgerMaster
                        SET     AmountInHand = AmountInHand - @CashOut
                                + @CashIn
                        WHERE   AccountId = @CInputID       
                      
                                        
                        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                        FROM    dbo.LedgerDetail
                                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                        WHERE   TransactionId = @V_Received_ID
                                                AND TransactionTypeId = 2
                                                AND AccountId = @InputID
                                      )        
                                                    
                        UPDATE  dbo.LedgerMaster
                        SET     AmountInHand = AmountInHand - @CashOut
                                + @CashIn
                        WHERE   AccountId = @InputID                        
                    END                    
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
                        SET     AmountInHand = AmountInHand - @CashOut
                                + @CashIn
                        WHERE   AccountId = @InputID                        
                    END                  
                     
                         
                                     
                SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                FROM    dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                WHERE   TransactionId = @V_Received_ID
                                        AND TransactionTypeId = 2
                                        AND AccountId = 10013
                              )                 
                                       
                SET @CashOut = 0                 
                                     
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = 10013                    
            END  
           
           
        ELSE
            BEGIN  
                IF @V_MRN_TYPE <> 2
                    BEGIN                        
                        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                         FROM   dbo.LedgerDetail
                                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                         WHERE  TransactionId = @V_Received_ID
                                                AND TransactionTypeId = 2
                                                AND AccountId = @RCInputID
                                       )                        
                        UPDATE  dbo.LedgerMaster
                        SET     AmountInHand = AmountInHand - @CashOut
                                + @CashIn
                        WHERE   AccountId = @RCInputID       
                      
                                        
                        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                         FROM   dbo.LedgerDetail
                                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                         WHERE  TransactionId = @V_Received_ID
                                                AND TransactionTypeId = 2
                                                AND AccountId = @RInputID
                                       )        
                                                    
                        UPDATE  dbo.LedgerMaster
                        SET     AmountInHand = AmountInHand - @CashOut
                                + @CashIn
                        WHERE   AccountId = @RInputID                        
                    END                    
                ELSE
                    BEGIN                        
                        
                        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                         FROM   dbo.LedgerDetail
                                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                         WHERE  TransactionId = @V_Received_ID
                                                AND TransactionTypeId = 2
                                                AND AccountId = @RInputID
                                       )                        
                        UPDATE  dbo.LedgerMaster
                        SET     AmountInHand = AmountInHand - @CashOut
                                + @CashIn
                        WHERE   AccountId = @RInputID                        
                    END                  
                     
                         
                                     
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @V_Received_ID
                                        AND TransactionTypeId = 2
                                        AND AccountId = 10015
                               )    
                                     
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = 10015    
                
                
                
                 SET @CashOut = 0 
                 SET @Cashin = ( SELECT ISNULL(SUM(CashOut), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @V_Received_ID
                                        AND TransactionTypeId = 2
                                        AND AccountId = 10088
                               )    
                                     
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = 10088   
                
            END  
           
                      
           
           
             
             
        SET @CashOut = 0  
        SET @CashIn = 0  
             
 --------------------------------A Cess Entries Deletion --------------------   
             
                      
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @V_Received_ID
                                AND TransactionTypeId = 2
                                AND AccountId = 10011
                      )                 
                                       
        SET @CashOut = 0                 
                                     
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10011               
                                    
                                    
        -------------------------------A Cess Entries Deletion -----------------------                 
                            
        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @V_Received_ID
                AND TransactionTypeId = 2                      
    END                  
                
    --------------------------------------------------------------------------------              
 Go             
---------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECIEVED_AGAINST_PO_MASTER]
    (
      @v_Receipt_ID NUMERIC(18, 0) ,
      @v_Receipt_No NUMERIC(18, 0) ,
      @v_Receipt_Code VARCHAR(20) ,
      @v_PO_ID NUMERIC(18, 0) ,
      @v_Receipt_Date DATETIME ,
      @v_Remarks VARCHAR(500) ,
      @v_MRN_NO NUMERIC(18, 0) ,
      @v_MRN_PREFIX VARCHAR(50) ,
      @v_Created_BY VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Division_ID NUMERIC(18, 0) ,
      @v_Proc_Type INT ,
      @V_mrn_status INT ,
      @v_freight NUMERIC(18, 2) ,
      @v_Other_Charges NUMERIC(18, 2) ,
      @v_Discount_amt NUMERIC(18, 2) ,
      @v_GROSS_AMOUNT NUMERIC(18, 2) ,
      @v_GST_AMOUNT NUMERIC(18, 2) ,
      @v_CESS_AMOUNT NUMERIC(18, 2) ,
      @v_NET_AMOUNT NUMERIC(18, 2) ,
      @V_MRN_TYPE INT ,
      @V_VAT_ON_EXICE INT ,
      @v_Invoice_No NVARCHAR(50) ,
      @v_Invoice_Date DATETIME ,
      @V_CUST_ID NUMERIC(18, 0) ,
      @v_MRNCompanies_ID INT ,
      @v_CashDiscount_amt NUMERIC(18, 2) = NULL ,
      @V_FreightTaxApplied INT ,
      @V_FreightTaxValue NUMERIC(18, 2) ,
      @V_FK_ITCEligibility_ID NUMERIC(18, 0) ,
      @V_IS_RCM_Applicable BIT = 0 ,
      @V_Reference_ID NUMERIC(18, 0)
    )
AS
    BEGIN                              
        IF @V_PROC_TYPE = 1
            BEGIN                            
                            
                            
                            
                DECLARE @Remarks VARCHAR(250)                            
                DECLARE @InputID NUMERIC                            
                DECLARE @CInputID NUMERIC   
                  
                DECLARE @RInputID NUMERIC                            
                DECLARE @RCInputID NUMERIC  
                SET @RCInputID = 10018    
                     
                                           
                SET @CInputID = 10016                            
                DECLARE @RoundOff NUMERIC(18, 2)                            
                DECLARE @CGST_Amount NUMERIC(18, 2)                            
                SET @CGST_Amount = ( @v_GST_AMOUNT / 2 )                            
                SET @RoundOff = ( @v_Other_Charges )                            
                            
                SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10023
                                             WHEN @V_MRN_TYPE = 2 THEN 10020
                                             WHEN @V_MRN_TYPE = 3 THEN 10074
                                        END AS inputid
                               )                                            
                            
                            
                SET @RInputID = ( SELECT    CASE WHEN @V_MRN_TYPE = 1
                                                 THEN 10025
                                                 WHEN @V_MRN_TYPE = 2
                                                 THEN 10022
                                                 WHEN @V_MRN_TYPE = 3
                                                 THEN 10076
                                            END AS inputid
                                )                   
                            
                SELECT  @V_Receipt_No = ISNULL(MAX(Receipt_No), 0) + 1
                FROM    MATERIAL_RECEIVED_AGAINST_PO_MASTER                                          
                            
                INSERT  INTO MATERIAL_RECEIVED_AGAINST_PO_MASTER
                        ( Receipt_ID ,
                          Receipt_No ,
                          Receipt_Code ,
                          Invoice_No ,
                          Invoice_Date ,
                          PO_ID ,
                          Receipt_Date ,
                          Remarks ,
                          MRN_NO ,
                          MRN_PREFIX ,
                          Created_BY ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_ID ,
                          MRN_STATUS ,
                          freight ,
                          Other_charges ,
                          Discount_amt ,
                          GROSS_AMOUNT ,
                          GST_AMOUNT ,
                          CESS_AMOUNT ,
                          NET_AMOUNT ,
                          MRN_TYPE ,
                          VAT_ON_EXICE ,
                          MRNCompanies_ID ,
                          IsPrinted ,
                          CUST_ID ,
                          CashDiscount_Amt ,
                          FreightTaxApplied ,
                          FreightTaxValue ,
                          FK_ITCEligibility_ID ,
                          Reference_ID                             
                        )
                VALUES  ( @V_Receipt_ID ,
                          @V_Receipt_No ,
                          @V_Receipt_Code ,
                          @v_Invoice_No ,
                          @v_Invoice_Date ,
                          @V_PO_ID ,
                          @v_Receipt_Date ,
                          @V_Remarks ,
                          @V_MRN_NO ,
                          @V_MRN_PREFIX ,
                          @V_Created_BY ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_ID ,
                          @V_mrn_status ,
                          @v_freight ,
                          @v_other_charges ,
                          @v_Discount_amt ,
                          @v_GROSS_AMOUNT ,
                          @v_GST_AMOUNT ,
                          @v_CESS_AMOUNT ,
                          @v_NET_AMOUNT ,
                          @v_MRN_TYPE ,
                          @V_VAT_ON_EXICE ,
                          @v_MRNCompanies_ID ,
                          0 ,
                          @V_CUST_ID ,
                          @v_CashDiscount_amt ,
                          @V_FreightTaxApplied ,
                          @V_FreightTaxValue ,
                          @V_FK_ITCEligibility_ID ,
                          @V_Reference_ID                 
                        )      
                            
                            
                --Minus Cash Discount---    
                    
                SET @v_GROSS_AMOUNT = ( @v_GROSS_AMOUNT
                                        - ISNULL(@v_CashDiscount_amt, 0) )                        
                            
                            
                SET @Remarks = 'Purchase against party bill no.: '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                             
                       
                       
                IF ( @V_IS_RCM_Applicable = 0 )
                    BEGIN            
                        EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT,
                            0, @Remarks, @V_Division_ID, @V_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY                                
                    END 
                ELSE
                    BEGIN
                        DECLARE @GAmount NUMERIC(18, 2)
                        SET @GAmount = ( ISNULL(@v_GROSS_AMOUNT, 0)
                                         + ISNULL(@v_freight, 0) )
                        EXECUTE Proc_Ledger_Insert @V_CUST_ID, @GAmount, 0,
                            @Remarks, @V_Division_ID, @V_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY  
                    END
                    
                    
                            
                EXECUTE Proc_Ledger_Insert @V_Reference_ID, 0, @v_GROSS_AMOUNT,
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3,
                    @v_Receipt_Date, @V_Created_BY                             
                            
                SET @Remarks = 'Freight against party bill no.: '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                             
                            
                EXECUTE Proc_Ledger_Insert 10047, 0, @v_freight, @Remarks,
                    @V_Division_ID, @V_Receipt_ID, 3, @v_Receipt_Date,
                    @V_Created_BY                
         
              
              
              
              
                IF ( @V_IS_RCM_Applicable = 0 )
                    BEGIN    
              
              
              
                        SET @Remarks = 'GST against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                             
                            
                        IF @V_MRN_TYPE <> 2
                            BEGIN                  
                            
                                EXECUTE Proc_Ledger_Insert @CInputID, 0,
                                    @CGST_Amount, @Remarks, @V_Division_ID,
                                    @v_Receipt_ID, 3, @v_Receipt_Date,
                                    @V_Created_BY                             
                            
               
                                EXECUTE Proc_Ledger_Insert @InputID, 0,
                                    @CGST_Amount, @Remarks, @v_Division_ID,
                                    @v_Receipt_ID, 3, @v_Receipt_Date,
                                    @V_Created_BY                             
                            END                          
                            
                            
                        ELSE
                            BEGIN                            
                                EXECUTE Proc_Ledger_Insert @InputID, 0,
                                    @v_GST_AMOUNT, @Remarks, @V_Division_ID,
                                    @v_Receipt_ID, 3, @v_Receipt_Date,
                                    @V_Created_BY                             
                            END                     
                                        
                        SET @Remarks = 'Cess against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                       
                                        
                        EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY                            
                            
                   
                    END  
                   
                ELSE
                    BEGIN  
                        SET @Remarks = 'GST against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                             
                            
                        IF @V_MRN_TYPE <> 2
                            BEGIN                  
                            
                                EXECUTE Proc_Ledger_Insert @RCInputID,
                                    @CGST_Amount, 0, @Remarks, @V_Division_ID,
                                    @v_Receipt_ID, 3, @v_Receipt_Date,
                                    @V_Created_BY                             
                            
               
                                EXECUTE Proc_Ledger_Insert @RInputID,
                                    @CGST_Amount, 0, @Remarks, @v_Division_ID,
                                    @v_Receipt_ID, 3, @v_Receipt_Date,
                                    @V_Created_BY                             
                            END                          
                            
                            
                        ELSE
                            BEGIN                            
                                EXECUTE Proc_Ledger_Insert @RInputID,
                                    @v_GST_AMOUNT, 0, @Remarks, @V_Division_ID,
                                    @v_Receipt_ID, 3, @v_Receipt_Date,
                                    @V_Created_BY                             
                            END                     
                                        
                        SET @Remarks = 'Cess against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                       
                                        
                        EXECUTE Proc_Ledger_Insert 10013, @v_CESS_AMOUNT, 0,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY
                            
                            
                           
                                            
                        SET @Remarks = 'Reverse charge against party invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)
                            
                        DECLARE @RcmExpense NUMERIC(18, 2)
                            
                        SET @RcmExpense = ( ISNULL(@v_GST_AMOUNT, 0)
                                            + ISNULL(@v_CESS_AMOUNT, 0) )
                           
                        EXECUTE Proc_Ledger_Insert 10088, 0, @RcmExpense,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY                             
                   
                    END  
                   
                   
                   
                   
                            
                SET @Remarks = 'Round Off against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                            
                            
                --IF @RoundOff > 0        
                --    BEGIN                            
                            
                --        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,        
                --            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,        
                --            @v_Receipt_Date, @V_Created_BY                                   
                --    END                            
                            
                --ELSE        
                --    BEGIN                            
                            
                --        SET @RoundOff = -+@RoundOff                      
                --        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,        
                --            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,        
                --            @v_Receipt_Date, @V_Created_BY                             
                            
                --    END     
                    
                IF @RoundOff > 0
                    BEGIN                                          
                                          
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY                                          
       
                    END                                          
                                          
                ELSE
                    BEGIN                                          
                                          
                        SET @RoundOff = -+@RoundOff                                          
                                 
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY                                          
                                          
                    END        
                            
                        
                IF @V_Reference_ID = 10070
                    BEGIN                            
                            
                            
                        SET @Remarks = 'Stock Out against party  invoice No- '
                            + @v_Invoice_No + ' - '
                            + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                            
                            
                            
                            
                        EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY        
                            
                    END                                           
                            
                UPDATE  MRN_SERIES
                SET     CURRENT_USED = CURRENT_USED + 1
                WHERE   DIV_ID = @v_Division_ID                  
                                
                RETURN @V_MRN_NO                                 
                            
            END                                          
                            
                            
        IF @V_PROC_TYPE = 2
            BEGIN                                          
                            
                UPDATE  MATERIAL_RECEIVED_AGAINST_PO_MASTER
                SET     PO_ID = @V_PO_ID ,
                        Receipt_Date = @v_Receipt_Date ,
                        Remarks = @V_Remarks ,
                        Created_BY = @V_Created_BY ,
                        Creation_Date = @V_Creation_Date ,
                        Modified_By = @V_Modified_By ,
                        Modification_Date = @V_Modification_Date ,
                        Division_ID = @V_Division_ID ,
                        freight = @v_freight ,
                        Other_charges = @v_Other_Charges ,
                        Discount_amt = @v_Discount_amt ,
                        VAT_ON_EXICE = @V_VAT_ON_EXICE ,
                        MRNCompanies_ID = @v_MRNCompanies_ID ,
                        CashDiscount_Amt = @v_CashDiscount_amt ,
                        FreightTaxApplied = @V_FreightTaxApplied ,
                        FreightTaxValue = @V_FreightTaxValue ,
                        FK_ITCEligibility_ID = @V_FK_ITCEligibility_ID ,
                        Reference_ID = @V_Reference_ID
                WHERE   Receipt_ID = @V_Receipt_ID                    
                        
            END                                          
                            
                            
        --IF @V_PROC_TYPE = 3                            
        --    BEGIN                                            
                            
        --        DECLARE cur CURSOR                   
        --        FOR                            
        --            SELECT  Item_ID ,                            
        --                    Item_Qty ,                            
        --                    Indent_ID                            
        --            FROM    MATERIAL_RECEIVED_AGAINST_PO_DETAIL                            
        --            WHERE   Receipt_ID = @V_Receipt_ID                  
                            
        --        DECLARE @itemid NUMERIC(18, 0)                                            
                            
        --        DECLARE @itemQty NUMERIC(18, 4)                                            
                            
        --        DECLARE @IndentID NUMERIC(18, 0)                
                            
        --        OPEN cur                                            
                            
        --        FETCH NEXT FROM cur INTO @itemid, @itemQty, @IndentID                                            
                            
        --        WHILE @@fetch_status = 0                            
        --            BEGIN                  
                                               
                            
        --                UPDATE  PO_STATUS                            
        --                SET     RECIEVED_QTY = RECIEVED_QTY - @itemQty ,                            
  --                        BALANCE_QTY = BALANCE_QTY + @itemQty                            
        --                WHERE   PO_ID = @V_PO_ID                            
        --                        AND ITEM_ID = @itemid                            
        --                        AND INDENT_ID = @IndentID                                              
                            
        --                UPDATE  ITEM_DETAIL                            
        --                SET     CURRENT_STOCK = CURRENT_STOCK - @itemQty                            
        --                WHERE   ITEM_ID = @itemid                            
        --                AND DIV_ID = @V_Division_ID                
                            
        --                FETCH NEXT FROM cur INTO @itemid, @itemQty, @IndentID                            
        --            END                
                                   
        --        CLOSE cur                
                                    
        --        DEALLOCATE cur                            
                            
        --        DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_DETAIL                            
        --        WHERE   Receipt_ID = @V_Receipt_ID                            
                             
        --        DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_MASTER                            
  --        WHERE   Receipt_ID = @V_Receipt_ID                             
        --    END                              
                               
    END 
GO


---------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECEIVED_AGAINST_PO_DETAIL]
    (
      @v_PO_ID NUMERIC(18, 0) ,
      @v_DIV_ID NUMERIC(18, 0) ,
      @v_Receipt_ID NUMERIC(18, 0) ,
      @v_Item_ID NUMERIC(18, 0) ,
      @v_Item_Qty NUMERIC(18, 4) ,
      @v_Trans_Type NUMERIC(18, 0) ,--new added                      
      @v_Creation_Date DATETIME ,--new added                      
      @v_Expiry_Date DATETIME , -- new added                      
      @v_Item_Rate NUMERIC(18, 2) ,
      @v_Batch_no VARCHAR(50) ,
      @v_Item_vat_per NUMERIC(18, 2) ,
      @v_Item_cess_per NUMERIC(18, 2) ,
      @v_Item_Exice_per NUMERIC(18, 2) ,
      @v_Proc_Type INT ,
      @v_DType CHAR(1) ,
      @V_Freight NUMERIC(18, 2) = 0.00 ,
      @V_Freight_type CHAR(10) = 'A' ,
      @V_FreightTaxValue NUMERIC(18, 2) = 0.00 ,
      @V_FreightCessValue NUMERIC(18, 2) = 0.00 ,
      @v_DiscountValue NUMERIC(18, 2)
    )
AS
    BEGIN                      
              
        IF @V_PROC_TYPE = 1
            BEGIN                      
              
  --- added on 16-12-2009 for insertion in stock degail and insert transaction log      
        
                DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)        
                SELECT  @STOCK_DETAIL_ID = MAX(STOCK_DETAIL_ID)
                FROM    dbo.STOCK_DETAIL
                WHERE   Item_id = @v_Item_ID           
                            
                EXEC Update_Stock_Detail_Adjustment @v_Item_ID, @v_Batch_no,
                    @v_Expiry_Date, @v_Item_Qty, @v_Receipt_ID, @v_Trans_Type,
                    @STOCK_DETAIL_ID OUTPUT                      
              
              
              
                INSERT  INTO MATERIAL_RECEIVED_AGAINST_PO_DETAIL
                        ( Receipt_ID ,
                          Item_ID ,
                          PO_ID ,
                          Item_Qty ,
                          Item_Rate ,
                          Vat_Per ,
                          Cess_Per ,
                          Bal_Item_Qty ,
                          bal_vat_per ,
                          stocK_dETAIL_Id ,
                          Exice_Per ,
                          Bal_Exice_Per ,
                          DType ,
                          DiscountValue,
                          Freight ,
                          Freight_type ,
                          FreightTaxValue ,
                          FreightCessValue                       
                        )
                VALUES  ( @V_Receipt_ID ,
                          @V_Item_ID ,
                          @v_PO_ID ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @v_Item_vat_per ,
                          @v_Item_cess_per ,
                          @V_Item_Qty ,
                          @v_Item_vat_per ,
                          @STOCK_DETAIL_ID ,
                          @v_Item_Exice_per ,
                          @v_Item_Exice_per ,
                          @v_DType ,
                          @v_DiscountValue,
                           @V_Freight ,
                          @V_Freight_type ,
                          @V_FreightTaxValue ,
                          @V_FreightCessValue                        
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
                          4 ,
                          @V_Receipt_ID ,
                          @v_DIV_ID          
                        )          
                           
                           
                UPDATE  PO_DETAIL
                SET     BALANCE_QTY = BALANCE_QTY - @v_Item_Qty
                WHERE   PO_ID = @v_PO_ID
                        AND ITEM_ID = @v_Item_ID      
                          
                UPDATE  dbo.PO_STATUS
                SET     BALANCE_QTY = BALANCE_QTY - @v_Item_Qty ,
                        RECIEVED_QTY = RECIEVED_QTY + @v_Item_Qty
                WHERE   PO_ID = @v_PO_ID
                        AND ITEM_ID = @v_Item_ID                        
              
                UPDATE  ITEM_DETAIL
                SET     CURRENT_STOCK = CURRENT_STOCK + @v_Item_Qty
                WHERE   ITEM_ID = @v_Item_ID
                        AND DIV_ID = @v_DIV_ID                      
              
              
              
              
              
              
                --it will insert entry in transaction log with stock_detail_id                      
                EXEC INSERT_TRANSACTION_LOG @v_Receipt_ID, @v_Item_ID,
                    @V_Trans_Type, @STOCK_DETAIL_ID, @v_Item_Qty,
                    @v_Creation_Date, 0                           
  -----------------------------------------------------------------------------                      
            END                      
              
--        IF @V_PROC_TYPE = 2             
--            BEGIN                      
--                UPDATE  MATERIAL_RECEIVED_AGAINST_PO_DETAIL                
--                SET     Item_ID = @V_Item_ID,                
--                        Item_Qty = @V_Item_Qty                
--                WHERE   Receipt_ID = @V_Receipt_ID                      
--            END                      
--                      
--        IF @V_PROC_TYPE = 3                 
--            BEGIN                      
--                      
--                DECLARE cur CURSOR                
--                    FOR SELECT  Item_ID,                
--                                Item_Qty                
--                        FROM    MATERIAL_RECEIVED_AGAINST_PO_DETAIL                
--                        WHERE   Receipt_ID = @V_Receipt_ID                      
--                      
-- DECLARE @itemid NUMERIC(18, 0)                      
--                DECLARE @itemQty NUMERIC(18, 4)                      
--                      
--                      
--                OPEN cur                      
--                FETCH NEXT FROM cur INTO @itemid, @itemQty                     
--                WHILE @@fetch_status = 0                      
--                    BEGIN             
--                         
--                        UPDATE  PO_STATUS                
--                        SET     RECIEVED_QTY = RECIEVED_QTY - @itemQty,                
--                                BALANCE_QTY = BALANCE_QTY + @itemQty                
--                        WHERE   PO_ID = @V_PO_ID                
--                    AND ITEM_ID = @itemid                      
--                      
--                        UPDATE  ITEM_DETAIL                
--                        SET     CURRENT_STOCK = CURRENT_STOCK - @itemQty                
--                        WHERE   ITEM_ID = @itemid                
--                  AND DIV_ID = @v_DIV_ID                       
--                      
--                        FETCH NEXT FROM cur INTO @itemid, @itemQty                    
--                    END                      
--                CLOSE cur                      
--                DEALLOCATE cur                      
--                      
--                DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_DETAIL                
--                WHERE   Receipt_ID = @V_Receipt_ID                      
--            END                      
              
    END   

GO
---------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[Fill_PO_ITEMS]
    (
      @PO_ID NUMERIC(18, 0) ,
      @DIV_ID NUMERIC(18, 0)
    )
AS
    BEGIN             
        DECLARE @con VARCHAR(100)            
        DECLARE @open INT            
            
        SELECT  @open = open_po_qty
        FROM    po_master
        WHERE   po_id = @po_id            
             
-- if (@open=1)            
-- begin            
--  @con= ''            
-- else                   
--  @con='AND (PO_DETAIL.BALANCE_QTY > 0)'            
-- end            
              
        SELECT  PO_DETAIL.PO_ID ,
                IM.ITEM_ID ,
                UNIT_MASTER.UM_ID ,
                IM.barcode_vch AS ITEM_CODE ,
                IM.ITEM_NAME ,
                UNIT_MASTER.UM_Name ,
                PO_DETAIL.ITEM_RATE ,
                ISNULL(PO_DETAIL.DType, 'P') AS DType ,
                ISNULL(PO_DETAIL.DiscountValue, 0.00) AS DISC ,
                ISNULL(PO_DETAIL.VAT_PER, 0.00) AS VAT_PER ,
                ISNULL(PO_DETAIL.CESS_PER, 0.00) AS CESS_PER ,
                ISNULL(PO_DETAIL.EXICE_PER, 0.00) AS EXICE_PER ,
                '' AS BATCH_NO ,
                DATEADD(yy, 2, GETDATE()) AS EXPIRY_DATE ,
                CAST(CASE WHEN ISNULL(PO_DETAIL.DType, 'P') = 'P'
                          THEN ISNULL(CAST(PO_DETAIL.Balance_qty
                                      * PO_DETAIL.ITEM_RATE AS NUMERIC(18, 2)),
                                      0.00)
                               - ISNULL(CAST(PO_DETAIL.Balance_qty
                                        * PO_DETAIL.ITEM_RATE AS NUMERIC(18, 2)),
                                        0.00) * ISNULL(PO_DETAIL.DiscountValue,
                                                       0.00) / 100
                          ELSE ISNULL(CAST(PO_DETAIL.Balance_qty
                                      * PO_DETAIL.ITEM_RATE AS NUMERIC(18, 2)),
                                      0.00) - ISNULL(PO_DETAIL.DiscountValue,
                                                     0.00)
                     END AS NUMERIC(18, 2)) AS Net_Amount ,
                CASE WHEN ISNULL(PO_DETAIL.DType, 'P') = 'P'
                     THEN ISNULL(CAST(( ( PO_DETAIL.Balance_qty
                                          * PO_DETAIL.ITEM_RATE
                                          - ISNULL(CAST(PO_DETAIL.Balance_qty
                                                   * PO_DETAIL.ITEM_RATE AS NUMERIC(18,
                                                              2)), 0.00)
                                          * ISNULL(PO_DETAIL.DiscountValue,
                                                   0.00) / 100 )
                                        + ( PO_DETAIL.Balance_qty
                                            * PO_DETAIL.ITEM_RATE
                                            - ISNULL(CAST(PO_DETAIL.Balance_qty
                                                     * PO_DETAIL.ITEM_RATE AS NUMERIC(18,
                                                              2)), 0.00)
                                            * ISNULL(PO_DETAIL.DiscountValue,
                                                     0.00) / 100 )
                                        * PO_DETAIL.EXICE_PER / 100 )
                                 + ( ( PO_DETAIL.Balance_qty
                                       * PO_DETAIL.ITEM_RATE
                                       - ISNULL(CAST(PO_DETAIL.Balance_qty
                                                * PO_DETAIL.ITEM_RATE AS NUMERIC(18,
                                                              2)), 0.00)
                                       * ISNULL(PO_DETAIL.DiscountValue, 0.00)
                                       / 100 ) + ( PO_DETAIL.Balance_qty
                                                   * PO_DETAIL.ITEM_RATE
                                                   - ISNULL(CAST(PO_DETAIL.Balance_qty
                                                            * PO_DETAIL.ITEM_RATE AS NUMERIC(18,
                                                              2)), 0.00)
                                                   * ISNULL(PO_DETAIL.DiscountValue,
                                                            0.00) / 100 )
                                     * PO_DETAIL.EXICE_PER / 100 )
                                 * PO_DETAIL.VAT_PER / 100 AS NUMERIC(18, 2)),
                                 0.00)
                     ELSE ISNULL(CAST(( ( PO_DETAIL.Balance_qty
                                          * PO_DETAIL.ITEM_RATE
                                          - ISNULL(PO_DETAIL.DiscountValue,
                                                   0.00) )
                                        + ( PO_DETAIL.Balance_qty
                                            * PO_DETAIL.ITEM_RATE
                                            - ISNULL(PO_DETAIL.DiscountValue,
                                                     0.00) )
                                        * PO_DETAIL.EXICE_PER / 100 )
                                 + ( ( PO_DETAIL.Balance_qty
                                       * PO_DETAIL.ITEM_RATE
                                       - ISNULL(PO_DETAIL.DiscountValue, 0.00) )
                                     + ( PO_DETAIL.Balance_qty
                                         * PO_DETAIL.ITEM_RATE
                                         - ISNULL(PO_DETAIL.DiscountValue,
                                                  0.00) )
                                     * PO_DETAIL.EXICE_PER / 100 )
                                 * PO_DETAIL.VAT_PER / 100 AS NUMERIC(18, 2)),
                                 0.00)
                END AS Gross_Amount ,
                ITEM_DETAIL.IS_STOCKABLE ,
                0 AS CostCenter_Id ,
                '' AS CostCenter_Code ,
                '' AS CostCenter_Name ,
                PO_DETAIL.Balance_qty AS PO_QTY ,
                0.00 AS BATCH_QTY ,
                PM.OPEN_PO_QTY ,
                (0.00) AS Freight ,
                ('A') AS Freight_type ,
                (0.00) AS FreightTaxValue ,
                (0.00) AS FreightCessValue
        FROM    ITEM_MASTER AS IM
                INNER JOIN UNIT_MASTER ON IM.UM_ID = UNIT_MASTER.UM_ID
                INNER JOIN PO_DETAIL ON IM.ITEM_ID = PO_DETAIL.ITEM_ID
                INNER JOIN PO_MASTER AS PM ON PO_DETAIL.PO_ID = PM.PO_ID
                INNER JOIN ITEM_DETAIL ON IM.ITEM_ID = ITEM_DETAIL.ITEM_ID
        WHERE   ( PO_DETAIL.PO_ID = @PO_ID )
                AND PO_DETAIL.BALANCE_QTY > 0      
    --            AND ( ( CASE WHEN ( @open <> 1 ) THEN PO_DETAIL.BALANCE_QTY        
    --                         ELSE 1        
    --                    END ) > 0 )          
    END 
GO
---------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------