
insert INTO dbo.DBScriptUpdateLog
        ( LogFileName, ExecuteDateTime )
VALUES  ( '0010_03_May_2018_CessUpdationSalePurchase',
          GETDATE()
          )

  Go


ALTER TABLE SALE_INVOICE_DETAIL ADD MRP NUMERIC(18,2) NOT NULL DEFAULT 0.00
ALTER TABLE SALE_INVOICE_DETAIL ADD CessPercentage_num NUMERIC(18,2) NOT NULL DEFAULT 0.00
ALTER TABLE SALE_INVOICE_DETAIL ADD CessAmount_num NUMERIC(18,2) NOT NULL DEFAULT 0.00
ALTER TABLE dbo.SALE_INVOICE_MASTER ADD CESS_AMOUNT NUMERIC(18,2) NOT NULL DEFAULT 0.00
ALTER TABLE dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER ADD CESS_AMOUNT NUMERIC(18,2) NOT NULL DEFAULT 0.00
ALTER TABLE dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL ADD Item_cess NUMERIC(18,4) DEFAULT 0.00
ALTER TABLE dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO ADD Item_cess NUMERIC(18,2) DEFAULT 0.00
ALTER TABLE dbo.PO_MASTER ADD CESS_AMOUNT NUMERIC(18,2) DEFAULT 0.00
ALTER TABLE dbo.PO_DETAIL ADD CESS_PER NUMERIC(18,2) DEFAULT 0.00
ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER ADD CESS_AMOUNT NUMERIC(18,2) DEFAULT 0.00
ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL ADD Cess_Per NUMERIC(18,2) DEFAULT 0.00

Go

Alter PROCEDURE [dbo].[GET_INV_ITEM_DETAILS] ( @V_SI_ID NUMERIC(18, 0) )    
AS    
    BEGIN           
          
        SELECT  IM.ITEM_ID ,    
                IM.ITEM_CODE ,    
                ITEM_NAME ,    
                UM_Name ,    
                Batch_no ,    
                --Balance_Qty + SISD.Item_Qty AS Batch_Qty ,    
                CAST(Balance_Qty AS NUMERIC(18,2)) AS Batch_Qty ,    
                CAST(SISD.Item_Qty  AS NUMERIC(18,2))  AS transfer_Qty ,    
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
                SID.MRP,    
                SID.CessPercentage_num AS Cess ,    
                SID.CessAmount_num AS Cess_Amount,    
                0 AS LandingAmt  
                --,    
                --SISD.ITEM_QTY    
        FROM    dbo.SALE_INVOICE_DETAIL SID    
                JOIN dbo.ITEM_MASTER IM ON IM.ITEM_ID = SID.ITEM_ID    
                JOIN dbo.UNIT_MASTER UM ON UM.UM_ID = IM.UM_ID    
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL SISD ON SISD.ITEM_ID = SID.Item_id    
                                                           AND SISD.SI_ID = SID.SI_ID    
                JOIN dbo.STOCK_DETAIL SD ON SD.Item_id = SID.ITEM_ID    
                                            AND SD.STOCK_DETAIL_ID = SISD.STOCK_DETAIL_ID    
        WHERE SID.SI_ID = @V_SI_ID        
        ORDER BY SID.SI_ID asc  
          
    END   

Go


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
      @V_Flag INT = 0      
      
      
      
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
                                          + @v_CESS_AMOUNT )        
      
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
                          Flag     
      
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
                          @v_CESS_AMOUNT ,
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
                          @V_Flag      
      
                        )       
      
      
      
                DECLARE @Remarks VARCHAR(250)    
      
                SET @Remarks = 'Sale against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))        
      
                EXECUTE Proc_Ledger_Insert @V_CUST_ID, 0, @V_NET_AMOUNT,
                    @Remarks, @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE,
                    @V_Created_BY       
      
           
      
                EXECUTE Proc_Ledger_Insert 10071, @V_GROSS_AMOUNT, 0, @Remarks,
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
                        CESS_AMOUNT = @V_CESS_AMOUNT ,
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
                        INV_TYPE = @v_INV_TYPE
                WHERE   SI_ID = @V_SI_ID           
      
      
      
                SET @Remarks = 'Sale against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))        
      
                SET @Remarks = 'Sale against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))      
      
      
      
                EXECUTE Proc_Ledger_Insert @V_CUST_ID, 0, @V_NET_AMOUNT,
                    @Remarks, @V_Division_ID, @V_SI_ID, 16, @TransactionDate,
                    @V_Created_BY       
      
           
      
                EXECUTE Proc_Ledger_Insert 10071, @V_GROSS_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @TransactionDate,
                    @V_Created_BY      
      
                     
      
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
      
Go


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
                          CessAmount_num   
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
                          @v_CESS_AMOUNT  
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
    
Go

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
                    
                    
        -------Cess Entries Deletion --------   
        
        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @v_SI_ID
                AND TransactionTypeId = 16        
    END
Go


----------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------Purchase -------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------

Alter PROCEDURE [dbo].[GET_ITEM_BY_ID] ( @V_ITEM_ID NUMERIC )
AS
    BEGIN    
  
        SELECT  IM.ITEM_ID ,
                IM.ITEM_CODE ,
                IM.ITEM_NAME ,
                UM.UM_NAME ,
                vm.VAT_PERCENTAGE ,
                ISNULL(cm.CessPercentage_num,0.00) AS CessPercentage_num,
                dbo.Get_Current_Stock(IM.ITEM_ID) AS Current_Stock ,
                IC.item_cat_name ,
                dbo.Get_item_rate_from_previous_mrn(@V_ITEM_ID) AS prev_mrn_rate
        FROM    ITEM_MASTER IM
                INNER JOIN VAT_MASTER vm ON vm.VAT_ID = IM.vat_id
                LEFT JOIN dbo.CessMaster cm ON cm.pk_CessId_num = IM.fk_CessId_num
                INNER JOIN UNIT_MASTER UM ON IM.UM_ID = UM.UM_ID
                INNER JOIN item_category IC ON IM.item_category_id = IC.item_cat_id
        WHERE   IM.ITEM_ID = @V_ITEM_ID 
  
    END 
Go


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
      @v_CESS_AMOUNT NUMERIC(18, 2) ,
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
          
        SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10023
                                     WHEN @V_MRN_TYPE = 2 THEN 10020
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
                          CESS_AMOUNT ,
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
                          @v_CESS_AMOUNT ,
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
                    @Remarks, @V_Division_ID, @V_Received_ID, 2,
                    @V_Received_Date, @V_Created_BY            
          
          
          
                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @V_Received_Date,
                    @V_Created_BY            
          
          
                SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)           
          
                IF @V_MRN_TYPE <> 2
                    BEGIN  
          
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Received_Date, @V_Created_BY          
          
          
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @v_Division_ID, @V_Received_ID, 2,
                            @V_Received_Date, @V_Created_BY          
                    END          
          
          
          
                ELSE
                    BEGIN          
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Received_Date, @V_Created_BY          
                    END 
                    
                SET @Remarks = 'Cess against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)
      
                
                EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @V_Received_Date,
                    @V_Created_BY            
          
          
                SET @Remarks = 'Round Off against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)          
          
                IF @RoundOff > 0
                    BEGIN          
          
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Received_Date, @V_Created_BY          
          
                    END          
          
                ELSE
                    BEGIN          
          
                        SET @RoundOff = -+@RoundOff          
          
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Received_Date, @V_Created_BY          
          
                    END           
          
          
                SET @Remarks = 'Stock Out against party  invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)          
          
          
          
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @V_Received_Date,
                    @V_Created_BY          
          
          
            END             
          
        
            
        IF @V_PROC_TYPE = 2
            BEGIN            
                    
                DECLARE @TransactionDate DATETIME    
                    
                EXEC Proc_ReverseMRNEntry @V_Received_ID, @V_Vendor_ID      
                    
                SELECT  @TransactionDate = Received_Date
                FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                WHERE   Received_ID = @V_Received_ID    
                    
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
                        CESS_AMOUNT = @v_CESS_AMOUNT ,
                        NET_AMOUNT = @v_NET_AMOUNT ,
                        MRN_TYPE = @v_MRN_TYPE ,
                        VAT_ON_EXICE = @V_VAT_ON_EXICE
                WHERE   Received_ID = @V_Received_ID          
          
          
          
                SET @Remarks = 'Pruchase against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)           
          
          
                EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2,
                    @TransactionDate, @V_Created_BY            
          
          
          
                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @TransactionDate,
                    @V_Created_BY            
          
          
                SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)           
          
                IF @V_MRN_TYPE <> 2
                    BEGIN      
          
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY          
          
          
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @v_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY          
                    END    
          
                ELSE
                    BEGIN          
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY          
                    END
                    
                SET @Remarks = 'Cess against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)
      
                
                EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT, @Remarks,
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
          
          
                SET @Remarks = 'Stock Out against party  invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)          
          
          
          
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @TransactionDate,
                    @V_Created_BY        
          
            END          
    END 
Go


  Alter PROC [dbo].[Proc_ReverseMRNEntry]
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
            
        SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10023
                                     WHEN @V_MRN_TYPE = 2 THEN 10020
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
                                        AND AccountId = @CInputID
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
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @InputID        
            END  
            
            
        --------------------------------Cess Entries Deletion --------------------
         
                     
        SET @CashIn = ( SELECT ISNULL(SUM(CashOut), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @V_Received_ID
                                AND TransactionTypeId = 2
                                AND AccountId = 10013
                       ) 
                       
        SET @CashOut = 0 
                     
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10013    
                    
                    
        -------------------------------Cess Entries Deletion ----------------------- 
            
        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @V_Received_ID
                AND TransactionTypeId = 2      
    END  
      
Go


Alter PROCEDURE [dbo].[PROC_MATERIAL_RECEIVED_WITHOUT_PO_DETAIL]
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
                          Item_cess ,
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
                          @V_Item_cess ,
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
                          Item_cess ,
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
                          @V_Item_cess ,
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
Go

Alter PROCEDURE [dbo].[PROC_INSERT_NON_STOCKABLE_ITEMS]         
 -- Add the parameters for the stored procedure here        
    @V_Received_ID INT ,
    @V_CostCenter_Id INT ,
    @V_Item_ID INT ,
    @V_Item_Qty NUMERIC(18, 4) ,
    @v_Item_vat NUMERIC(18, 4) ,
    @v_Item_cess NUMERIC(18, 4) ,
    @v_Item_Exice NUMERIC(18, 4) ,
    @v_batch_no VARCHAR(50) ,
    @v_batch_date DATETIME ,
    @v_Item_Rate NUMERIC(18, 4) ,
    @v_DType CHAR(1) ,
    @v_DiscountValue NUMERIC(18, 2)
AS
    BEGIN        
        INSERT  INTO dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO
                ( Received_ID ,
                  Item_ID ,
                  CostCenter_ID ,
                  Item_Qty ,
                  Item_vat ,
                  Item_cess ,
                  Item_Exice ,
                  batch_no ,
                  batch_date ,
                  Item_Rate ,
                  Bal_Item_Qty ,
                  Bal_Item_Rate ,
                  Bal_Item_Vat ,
                  Bal_Item_Exice ,
                  DType ,
                  DiscountValue     
                )
        VALUES  ( /* Received_ID - int */
                  @V_Received_ID ,        
				  /* Item_ID - int */
                  @V_Item_ID ,        
				  /* CostCenter_ID - int */
                  @V_CostCenter_Id ,        
					/* Item_Qty - numeric(18, 4) */
                  @V_Item_Qty ,
                  @v_Item_vat ,
                  @v_Item_cess ,
                  @v_Item_Exice ,
                  @v_batch_no ,
                  @v_batch_date ,
                  @v_Item_Rate ,
                  @V_Item_Qty ,
                  @v_Item_Rate ,
                  @v_Item_vat ,
                  @v_Item_Exice ,
                  @v_DType ,
                  @v_DiscountValue      
                )         
    END  
Go

Alter PROCEDURE [dbo].[Get_MRN_WithOutPO_Detail]
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
                freight ,
                other_charges ,
                Discount_amt ,
                MRNCompanies_ID ,
                Invoice_No ,
                Invoice_Date
        FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER
        WHERE   Received_ID = @V_Receive_ID    
          
          
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                MD.Item_Qty AS BATCH_QTY ,
                MD.Item_Rate AS Item_Rate ,
                ISNULL(DType, 'P') AS DType ,
                ISNULL(DiscountValue, 0) AS DISC ,
                MD.Item_vat AS Vat_Per ,
                MD.Item_cess AS Cess_Per ,
                MD.Item_exice AS exe_Per ,
                MD.Batch_No AS BATCH_NO ,
                MD.Expiry_Date AS EXPIRY_DATE
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
                ISNULL(DType, 'P') AS DType ,
                ISNULL(DiscountValue, 0) AS DISC ,
                NonStockable.Item_Exice AS exe_Per ,
                NonStockable.batch_no AS BATCH_NO ,
                NonStockable.batch_date AS EXPIRY_DATE
        FROM    dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO NonStockable
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON NonStockable.Item_ID = IM.ITEM_ID
                INNER JOIN dbo.COST_CENTER_MASTER ON NonStockable.CostCenter_ID = dbo.COST_CENTER_MASTER.CostCenter_Id
        WHERE   Received_ID = @V_Receive_ID          
          
    END    
      
Go


Alter PROCEDURE [dbo].[PROC_PO_MASTER]
    (
      @v_PO_ID INT ,
      @v_PO_CODE VARCHAR(20) ,
      @v_PO_NO DECIMAL ,
      @v_PO_DATE DATETIME ,
      @v_PO_START_DATE DATETIME ,
      @v_PO_END_DATE DATETIME ,
      @v_PO_REMARKS VARCHAR(500) ,
      @v_PO_SUPP_ID INT ,
      @v_PO_QUALITY_ID INT ,
      @v_PO_DELIVERY_ID INT ,
      @v_PO_STATUS INT ,
      @v_PATMENT_TERMS VARCHAR(500) ,
      @v_TRANSPORT_MODE VARCHAR(50) ,
      @v_TOTAL_AMOUNT NUMERIC(18, 2) ,
      @v_VAT_AMOUNT NUMERIC(18, 2) ,
      @v_CESS_AMOUNT NUMERIC(18, 2) ,
      @v_NET_AMOUNT NUMERIC(18, 2) ,
      @v_EXICE_AMOUNT NUMERIC(18, 2) ,
      @v_PO_TYPE INT ,
      @v_OCTROI VARCHAR(50) ,
      @v_PRICE_BASIS VARCHAR(50) ,
      @v_FRIEGHT INT ,
      @v_OTHER_CHARGES NUMERIC(18, 2) ,
      --@v_CESS NUMERIC(18, 2) ,
      @v_DISCOUNT_AMOUNT NUMERIC(18, 2) ,
      @v_CREATED_BY VARCHAR(50) ,
      @v_CREATION_DATE DATETIME ,
      @v_MODIFIED_BY VARCHAR(50) ,
      @v_MODIFIED_DATE DATETIME ,
      @v_DIVISION_ID INT ,
      @V_OPEN_PO_QTY BIT ,
      @v_Proc_Type INT ,
      @v_VAT_ON_EXICE BIT               
    )
AS
    BEGIN              
              
        IF @V_PROC_TYPE = 1
            BEGIN              
--Inderjeet: PO_NO is gentared from another procedure and send to this procedure as a parameter.              
--   select @v_PO_NO = isnull(max(PO_NO),0) + 1 from PO_MASTER              
                SELECT  @V_PO_ID = ISNULL(MAX(PO_ID), 0) + 1
                FROM    PO_MASTER              
              
                INSERT  INTO PO_MASTER
                        ( PO_ID ,
                          PO_CODE ,
                          PO_NO ,
                          PO_DATE ,
                          PO_START_DATE ,
                          PO_END_DATE ,
                          PO_REMARKS ,
                          PO_SUPP_ID ,
                          PO_QUALITY_ID ,
                          PO_DELIVERY_ID ,
                          PO_STATUS ,
                          PATMENT_TERMS ,
                          TRANSPORT_MODE ,
                          TOTAL_AMOUNT ,
                          VAT_AMOUNT ,
                          CESS_AMOUNT ,
                          NET_AMOUNT ,
                          EXICE_AMOUNT ,
                          PO_TYPE ,
                          OCTROI ,
                          PRICE_BASIS ,
                          FRIEGHT ,
                          OTHER_CHARGES ,
                          --CESS_PER ,
                          DISCOUNT_AMOUNT ,
                          CREATED_BY ,
                          CREATION_DATE ,
                          MODIFIED_BY ,
                          MODIFIED_DATE ,
                          DIVISION_ID ,
                          VAT_ON_EXICE ,
                          OPEN_PO_QTY          
                        )
                VALUES  ( @V_PO_ID ,
                          @V_PO_CODE ,
                          @V_PO_NO ,
                          @V_PO_DATE ,
                          @V_PO_START_DATE ,
                          @V_PO_END_DATE ,
                          @V_PO_REMARKS ,
                          @V_PO_SUPP_ID ,
                          @V_PO_QUALITY_ID ,
                          @V_PO_DELIVERY_ID ,
                          @V_PO_STATUS ,
                          @V_PATMENT_TERMS ,
                          @V_TRANSPORT_MODE ,
                          @V_TOTAL_AMOUNT ,
                          @V_VAT_AMOUNT ,
                          @v_CESS_AMOUNT ,
                          @V_NET_AMOUNT ,
                          @V_EXICE_AMOUNT ,
                          @V_PO_TYPE ,
                          @V_OCTROI ,
                          @V_PRICE_BASIS ,
                          @V_FRIEGHT ,
                          @V_OTHER_CHARGES ,
                          --@v_CESS ,
                          @v_DISCOUNT_AMOUNT ,
                          @V_CREATED_BY ,
                          @V_CREATION_DATE ,
                          @V_MODIFIED_BY ,
                          @V_MODIFIED_DATE ,
                          @V_DIVISION_ID ,
                          @v_VAT_ON_EXICE ,
                          @V_OPEN_PO_QTY            
                        )                   
                RETURN @V_PO_ID               
                 
            END              
              
        IF @V_PROC_TYPE = 2
            BEGIN                
                UPDATE  PO_MASTER
                SET     PO_DATE = @V_PO_DATE ,
                        PO_START_DATE = @V_PO_START_DATE ,
                        PO_END_DATE = @V_PO_END_DATE ,
                        PO_REMARKS = @V_PO_REMARKS ,
                        PO_SUPP_ID = @V_PO_SUPP_ID ,
                        PO_QUALITY_ID = @V_PO_QUALITY_ID ,
                        PO_DELIVERY_ID = @V_PO_DELIVERY_ID ,
                        PO_STATUS = @V_PO_STATUS ,
                        PATMENT_TERMS = @V_PATMENT_TERMS ,
                        TRANSPORT_MODE = @V_TRANSPORT_MODE ,
                        TOTAL_AMOUNT = @V_TOTAL_AMOUNT ,
                        VAT_AMOUNT = @V_VAT_AMOUNT ,
                        CESS_AMOUNT = @V_CESS_AMOUNT ,
                        NET_AMOUNT = @V_NET_AMOUNT ,
                        PO_TYPE = @v_PO_TYPE ,
                        OCTROI = @v_OCTROI ,
                        PRICE_BASIS = @v_PRICE_BASIS ,
                        FRIEGHT = @v_FRIEGHT ,
                        OTHER_CHARGES = @v_OTHER_CHARGES ,
                        --CESS_PER = @v_CESS ,
                        DISCOUNT_AMOUNT = @v_DISCOUNT_AMOUNT ,
                        MODIFIED_BY = @V_MODIFIED_BY ,
                        MODIFIED_DATE = @V_MODIFIED_DATE ,
                        DIVISION_ID = @V_DIVISION_ID ,
                        VAT_ON_EXICE = @v_VAT_ON_EXICE ,
                        OPEN_PO_QTY = @V_OPEN_PO_QTY
                WHERE   PO_ID = @V_PO_ID                
            END                
                
        IF @V_PROC_TYPE = 3
            BEGIN                
                DELETE  FROM PO_MASTER
                WHERE   PO_ID = @V_PO_ID                
            END                
              
    END  
Go

Alter PROCEDURE [dbo].[PROC_PO_DETAIL]  
    (  
      @v_PO_ID DECIMAL ,  
      @v_ITEM_ID DECIMAL ,  
      @v_ITEM_QTY DECIMAL(18, 4) ,  
      @v_VAT_PER DECIMAL(18, 2) , 
      @v_CESS_PER DECIMAL(18, 2) , 
      @v_EXICE_PER DECIMAL(18, 2) ,  
      @v_ITEM_RATE DECIMAL(18, 2) ,  
	  @v_DISC_VALUE NUMERIC(18, 2) ,  
      @v_DTYPE NVARCHAR(10) ,  
      @v_CREATED_BY VARCHAR(50) ,  
      @v_CREATION_DATE DATETIME ,  
      @v_MODIFIED_BY VARCHAR(50) ,  
      @v_MODIFIED_DATE DATETIME ,  
      @v_DIVISION_ID INT ,  
      @v_Proc_Type INT        
    )  
AS  
    BEGIN        
        
        IF @V_PROC_TYPE = 1  
            BEGIN        
                INSERT  INTO PO_DETAIL  
                        ( PO_ID ,  
                          ITEM_ID ,  
                          ITEM_QTY ,  
                          balance_qty ,  
                          VAT_PER , 
                          CESS_PER , 
                          EXICE_PER ,  
                          ITEM_RATE ,  
                          CREATED_BY ,  
                          CREATION_DATE ,  
                          MODIFIED_BY ,  
                          MODIFIED_DATE ,  
                          DIVISION_ID,  
        DType,  
        DiscountValue  
                        )  
                VALUES  ( @V_PO_ID ,  
                          @V_ITEM_ID ,  
                          @V_ITEM_QTY ,  
                          @V_ITEM_QTY ,  
                          @V_VAT_PER ,
                          @v_CESS_PER ,  
                          @V_EXICE_PER ,  
                          @V_ITEM_RATE ,  
                          @V_CREATED_BY ,  
                          @V_CREATION_DATE ,  
                          @V_MODIFIED_BY ,  
                          @V_MODIFIED_DATE ,  
                          @V_DIVISION_ID,  
        @v_DTYPE,  
        @v_DISC_VALUE  
                        )        
            END        
        
-- IF @V_PROC_TYPE=2        
--  BEGIN        
--   UPDATE PO_DETAIL        
--     SET        
--     POD_ID =  @V_POD_ID,         
--     ITEM_ID =  @V_ITEM_ID,         
--     INDENT_ID =  @V_INDENT_ID,         
--     ITEM_QTY =  @V_ITEM_QTY,         
--     VAT_PER =  @V_VAT_PER,         
--     EXICE_PER =  @V_EXICE_PER,         
--     ITEM_RATE =  @V_ITEM_RATE,         
--     TOTAL_AMOUNT =  @V_TOTAL_AMOUNT,         
--     CREATED_BY =  @V_CREATED_BY,         
--     CREATION_DATE =  @V_CREATION_DATE,         
--     MODIFIED_BY =  @V_MODIFIED_BY,         
--     MODIFIED_DATE =  @V_MODIFIED_DATE,         
--     DIVISION_ID =  @V_DIVISION_ID         
--     WHERE         
--     PO_ID = @V_PO_ID        
--   END        
        
        IF @V_PROC_TYPE = 3  
            BEGIN        
                DELETE  FROM PO_DETAIL  
                WHERE   PO_ID = @V_PO_ID        
            END        
        
    END  
Go

Alter PROCEDURE [dbo].[GET_PO_ITEM_DETAILS] ( @V_PO_ID NUMERIC(18, 0) )
AS
    BEGIN              
        SELECT  ITEM_MASTER.ITEM_CODE ,
                ITEM_MASTER.ITEM_NAME ,
                UNIT_MASTER.UM_Name ,
                PO_STATUS.REQUIRED_QTY AS Req_Qty ,
                CAST(PO_STATUS.REQUIRED_QTY AS NUMERIC(18, 4)) AS PO_Qty ,
                PO_DETAIL.ITEM_RATE AS Item_Rate ,
                ISNULL(DType, 'P') AS DType ,
                ISNULL(DiscountValue, 0) AS DISC ,
                ITEM_DETAIL.PURCHASE_VAT_ID AS Vat_Id ,
                VAT_MASTER.VAT_NAME ,
                PO_DETAIL.EXICE_PER AS Exice_Per ,
                VAT_MASTER.VAT_PERCENTAGE AS Vat_Per ,
                0 AS Item_Value ,
                PO_STATUS.ITEM_ID AS Item_ID ,
                ITEM_MASTER.UM_ID ,
                PO_STATUS.INDENT_ID,
                ISNULL(CessMaster.pk_CessId_num,0) AS Cess_Id, 
                ISNULL(CessMaster.CessName_vch,'0%') + ' CESS'  AS Cess_Name, 
                ISNULL(CessMaster.CessPercentage_num,0.00) AS Cess_Per
        FROM    PO_DETAIL
                INNER JOIN ITEM_MASTER ON PO_DETAIL.ITEM_ID = ITEM_MASTER.ITEM_ID
                LEFT JOIN dbo.CessMaster ON ITEM_MASTER.fk_CessId_num = CessMaster.pk_CessId_num
                INNER JOIN UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID
                INNER JOIN ITEM_DETAIL ON ITEM_MASTER.ITEM_ID = ITEM_DETAIL.ITEM_ID
                INNER JOIN VAT_MASTER ON ITEM_DETAIL.PURCHASE_VAT_ID = VAT_MASTER.VAT_ID
                INNER JOIN PO_STATUS ON PO_DETAIL.PO_ID = PO_STATUS.PO_ID
                                        AND PO_DETAIL.ITEM_ID = PO_STATUS.ITEM_ID
        WHERE   ( PO_STATUS.PO_ID = @V_PO_ID )      
    END 
Go

Alter PROCEDURE [dbo].[Fill_PO_ITEMS]
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
                IM.ITEM_CODE ,
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
                CASE WHEN ISNULL(PO_DETAIL.DType, 'P') = 'P'
                     THEN ISNULL(CAST(PO_DETAIL.Balance_qty
                                 * PO_DETAIL.ITEM_RATE AS NUMERIC(18, 2)),
                                 0.00)
                          - ISNULL(CAST(PO_DETAIL.Balance_qty
                                   * PO_DETAIL.ITEM_RATE AS NUMERIC(18, 2)),
                                   0.00) * ISNULL(PO_DETAIL.DiscountValue,
                                                  0.00) / 100
                     ELSE ISNULL(CAST(PO_DETAIL.Balance_qty
                                 * PO_DETAIL.ITEM_RATE AS NUMERIC(18, 2)),
                                 0.00) - ISNULL(PO_DETAIL.DiscountValue, 0.00)
                END AS Net_Amount ,
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
                PM.OPEN_PO_QTY
        FROM    ITEM_MASTER AS IM
                INNER JOIN UNIT_MASTER ON IM.UM_ID = UNIT_MASTER.UM_ID
                INNER JOIN PO_DETAIL ON IM.ITEM_ID = PO_DETAIL.ITEM_ID
                INNER JOIN PO_MASTER AS PM ON PO_DETAIL.PO_ID = PM.PO_ID
                INNER JOIN ITEM_DETAIL ON IM.ITEM_ID = ITEM_DETAIL.ITEM_ID
        WHERE   ( PO_DETAIL.PO_ID = @PO_ID )
                AND ( ( CASE WHEN ( @open <> 1 ) THEN PO_DETAIL.BALANCE_QTY
                             ELSE 1
                        END ) > 0 )  
    END  
Go

Alter PROCEDURE [dbo].[PROC_MATERIAL_RECIEVED_AGAINST_PO_MASTER]
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
      @v_MRNCompanies_ID INT         
        
    )
AS
    BEGIN          
        IF @V_PROC_TYPE = 1
            BEGIN        
        
        
        
                DECLARE @Remarks VARCHAR(250)        
                DECLARE @InputID NUMERIC        
                DECLARE @CInputID NUMERIC        
                SET @CInputID = 10016        
                DECLARE @RoundOff NUMERIC(18, 2)        
                DECLARE @CGST_Amount NUMERIC(18, 2)        
                SET @CGST_Amount = ( @v_GST_AMOUNT / 2 )        
                SET @RoundOff = ( @v_freight + @v_Other_Charges )        
        
                SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10023
                                             WHEN @V_MRN_TYPE = 2 THEN 10020
                                             WHEN @V_MRN_TYPE = 3 THEN 10074
                                        END AS inputid
                               )     
        
        
                SET @Remarks = 'Pruchase against party bill no.: '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)         
        
                EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3,
                    @v_Receipt_Date, @V_Created_BY            
        
                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Receipt_ID, 3, @v_Receipt_Date,
                    @V_Created_BY         
        
        
        
                SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)         
        
                IF @V_MRN_TYPE <> 2
                    BEGIN          
        
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY         
        
        
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @v_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY         
                    END      
        
        
                ELSE
                    BEGIN        
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY         
                    END 
                    
                SET @Remarks = 'Cess against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)   
                    
                EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY        
        
        
                SET @Remarks = 'Round Off against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)        
        
                IF @RoundOff > 0
                    BEGIN        
        
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY               
                    END        
        
                ELSE
                    BEGIN        
        
                        SET @RoundOff = -+@RoundOff  
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY         
        
                    END         
        
        
                SET @Remarks = 'Stock Out against party  invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)        
        
        
        
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT, @Remarks,
                    @V_Division_ID, @v_Receipt_ID, 3, @v_Receipt_Date,
                    @V_Created_BY         
        
        
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
                          CUST_ID          
        
        
        
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
                          @V_CUST_ID          
        
        
        
                        )                      
        
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
                        MRNCompanies_ID = @v_MRNCompanies_ID
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
        --                        AND DIV_ID = @V_Division_ID        
        
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
    
Go

Alter PROCEDURE [dbo].[PROC_MATERIAL_RECEIVED_AGAINST_PO_DETAIL]
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
                          DiscountValue                 
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
                          4 ,
                          @V_Receipt_ID ,
                          @v_DIV_ID      
                        )      
                       
                       
                UPDATE  PO_DETAIL
                SET     BALANCE_QTY = BALANCE_QTY - @v_Item_Qty
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
Go

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
                freight ,
                other_charges ,
                Discount_amt ,
                MRNCompanies_ID ,
                Invoice_No ,
                Invoice_Date
        FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER
        WHERE   Received_ID = @V_Receive_ID      
            
            
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                CAST(MD.Item_Qty AS NUMERIC(18, 2)) AS BATCH_QTY ,
                CAST(MD.Item_Rate AS NUMERIC(18, 2)) AS Item_Rate ,
                ISNULL(DType, 'P') AS DType ,
                CAST(ISNULL(DiscountValue, 0) AS NUMERIC(18, 2)) AS DISC ,
                CAST(MD.Item_vat AS NUMERIC(18, 2)) AS Vat_Per ,
                CAST(MD.Item_cess AS NUMERIC(18, 2)) AS Cess_Per ,
                MD.Item_exice AS exe_Per ,
                MD.Batch_No AS BATCH_NO ,
                MD.Expiry_Date AS EXPIRY_DATE
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
                ISNULL(DType, 'P') AS DType ,
                ISNULL(DiscountValue, 0) AS DISC ,
                NonStockable.Item_Exice AS exe_Per ,
                NonStockable.batch_no AS BATCH_NO ,
                NonStockable.batch_date AS EXPIRY_DATE
        FROM    dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO NonStockable
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON NonStockable.Item_ID = IM.ITEM_ID
                INNER JOIN dbo.COST_CENTER_MASTER ON NonStockable.CostCenter_ID = dbo.COST_CENTER_MASTER.CostCenter_Id
        WHERE   Received_ID = @V_Receive_ID            
            
    END      
        
----------------------------------------------------------------------------------------------------------------------------------------------------------
go
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
                freight ,
                other_charges ,
                Discount_amt ,
                MRNCompanies_ID ,
                Invoice_No ,
                Invoice_Date
        FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER
        WHERE   Received_ID = @V_Receive_ID      
            
            
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                CAST(MD.Item_Qty AS NUMERIC(18, 2)) AS BATCH_QTY ,
                CAST(MD.Item_Rate AS NUMERIC(18, 2)) AS Item_Rate ,
                ISNULL(DType, 'P') AS DType ,
                CAST(ISNULL(DiscountValue, 0) AS NUMERIC(18, 2)) AS DISC ,
                CAST(MD.Item_vat AS NUMERIC(18, 2)) AS Vat_Per ,
                CAST(MD.Item_cess AS NUMERIC(18, 2)) AS Cess_Per ,
                MD.Item_exice AS exe_Per ,
                MD.Batch_No AS BATCH_NO ,
                MD.Expiry_Date AS EXPIRY_DATE
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
                ISNULL(DType, 'P') AS DType ,
                ISNULL(DiscountValue, 0) AS DISC ,
                NonStockable.Item_Exice AS exe_Per ,
                NonStockable.batch_no AS BATCH_NO ,
                NonStockable.batch_date AS EXPIRY_DATE
        FROM    dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO NonStockable
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON NonStockable.Item_ID = IM.ITEM_ID
                INNER JOIN dbo.COST_CENTER_MASTER ON NonStockable.CostCenter_ID = dbo.COST_CENTER_MASTER.CostCenter_Id
        WHERE   Received_ID = @V_Receive_ID            
            
    END      
        
go

ALTER PROCEDURE [dbo].[GET_ITEM_BY_ID] ( @V_ITEM_ID NUMERIC )  
AS  
    BEGIN      
    
        SELECT  IM.ITEM_ID ,  
                IM.ITEM_CODE ,  
                IM.ITEM_NAME ,  
                UM.UM_NAME ,  
                vm.VAT_PERCENTAGE ,  
                ISNULL(cm.CessPercentage_num,0.00) AS CessPercentage_num,  
                dbo.Get_Current_Stock(IM.ITEM_ID) AS Current_Stock ,  
                IC.item_cat_name ,  
                CAST(dbo.Get_item_rate_from_previous_mrn(@V_ITEM_ID)AS NUMERIC(18, 2)) AS prev_mrn_rate  
        FROM    ITEM_MASTER IM  
                INNER JOIN VAT_MASTER vm ON vm.VAT_ID = IM.vat_id  
                LEFT JOIN dbo.CessMaster cm ON cm.pk_CessId_num = IM.fk_CessId_num  
                INNER JOIN UNIT_MASTER UM ON IM.UM_ID = UM.UM_ID  
                INNER JOIN item_category IC ON IM.item_category_id = IC.item_cat_id  
        WHERE   IM.ITEM_ID = @V_ITEM_ID   
    
    END   
      