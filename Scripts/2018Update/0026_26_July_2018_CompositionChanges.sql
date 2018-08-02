INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0026_26_July_2018_CompositionChanges' ,
          GETDATE()
        )
Go

ALTER TABLE MATERIAL_RECIEVED_WITHOUT_PO_MASTER ADD SpecialSchemeFlag  CHAR(10) NOT NULL DEFAULT 'Nill'

Go

--------------------------------------------------------------------------------------------------------------        
Alter PROCEDURE [dbo].[PROC_MATERIAL_RECIEVED_WITHOUT_PO_MASTER]  
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
          @V_Special_Scheme CHAR(10)                         
                        
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
            SET @RoundOff = ( @v_Other_Charges )                        
                        
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
                              SpecialSchemeFlag                       
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
                              ( ISNULL(@v_CESS_AMOUNT, 0)  
                                + ISNULL(@v_ACESS_AMOUNT, 0) ) ,  
                              @v_NET_AMOUNT ,  
                              @v_MRN_TYPE ,  
                              @V_VAT_ON_EXICE ,  
                              0 ,
                              @V_Special_Scheme   
                            )                          
                        
                        
                    UPDATE  MRN_SERIES  
                    SET     CURRENT_USED = CURRENT_USED + 1  
                    WHERE   DIV_ID = @v_Division_ID                          
                        
                    SET @Remarks = 'Purchase against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                         
                        
                        
                    EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @v_Invoice_Date, @V_Created_BY                          
                        
                        
                        
                    EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @v_Invoice_Date, @V_Created_BY   
                        
                        
                    SET @Remarks = 'Freight against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)
                        
                    EXECUTE Proc_Ledger_Insert 10047, @v_freight, 0,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @v_Invoice_Date, @V_Created_BY                           
                        
                        
                    SET @Remarks = 'GST against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                         
                        
                    IF @V_MRN_TYPE <> 2  
                        BEGIN                
                        
                            EXECUTE Proc_Ledger_Insert @CInputID, 0,  
                                @CGST_Amount, @Remarks, @V_Division_ID,  
                                @V_Received_ID, 2, @v_Invoice_Date,  
                                @V_Created_BY                        
                        
                        
                            EXECUTE Proc_Ledger_Insert @InputID, 0,  
                                @CGST_Amount, @Remarks, @v_Division_ID,  
                                @V_Received_ID, 2, @v_Invoice_Date,  
                                @V_Created_BY                        
                        END                        
                        
                        
                        
                    ELSE  
                        BEGIN                        
                            EXECUTE Proc_Ledger_Insert @InputID, 0,  
                              @v_GST_AMOUNT, @Remarks, @V_Division_ID,  
                                @V_Received_ID, 2, @v_Invoice_Date,  
                                @V_Created_BY                        
                        END               
                                  
                    SET @Remarks = 'Cess against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)              
                    
                              
                    EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @v_Invoice_Date, @V_Created_BY                          
                        
                        
                        
                    SET @Remarks = 'Add. Cess against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)    
                                   
                    EXECUTE Proc_Ledger_Insert 10011, 0, @v_ACESS_AMOUNT,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @v_Invoice_Date, @V_Created_BY          
                        
                        
                        
                    SET @Remarks = 'Round Off against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                        
                        
                    IF @RoundOff > 0  
                        BEGIN                        
                        
                            EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,  
                                @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                                @v_Invoice_Date, @V_Created_BY                        
                        
                        END                        
                        
                    ELSE  
                        BEGIN                        
                        
                            SET @RoundOff = -+@RoundOff                        
               
                            EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,  
                                @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                                @v_Invoice_Date, @V_Created_BY                        
                        
                        END                         
                        
                        
                    SET @Remarks = 'Stock Out against party  invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106) 
                        
                    EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @v_Invoice_Date, @V_Created_BY  
                        
                END   
                          
            IF @V_PROC_TYPE = 2  
                BEGIN                          
                                  
                    DECLARE @TransactionDate DATETIME                  
                                  
                    EXEC Proc_ReverseMRNEntry @V_Received_ID, @V_Vendor_ID                    
                                  
                    SELECT  @TransactionDate = Invoice_Date  
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
                            SpecialSchemeFlag =  @V_Special_Scheme 
                    WHERE   Received_ID = @V_Received_ID                        
                        
                        
                        
                    SET @Remarks = 'Purchase against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                         
                        
                        
                    EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @TransactionDate, @V_Created_BY                          
                        
                       
                        
                    EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @TransactionDate, @V_Created_BY  
                        
                        
                    SET @Remarks = 'Freight against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)
                        
                    EXECUTE Proc_Ledger_Insert 10047, @v_freight, 0,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @TransactionDate, @V_Created_BY                        
                        
                        
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
                        
                        
                    SET @Remarks = 'Add. Cess against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)     
                                  
                    EXECUTE Proc_Ledger_Insert 10011, 0, @v_ACESS_AMOUNT,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @TransactionDate, @V_Created_BY          
                        
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
                        
                    EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @TransactionDate, @V_Created_BY                      
                        
                END                        
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
                ISNULL(freight,0.00) AS freight ,        
                other_charges ,        
                Discount_amt ,      
				ISNULL(CashDiscount_Amt,0) AS  CashDiscount_Amt,        
                MRNCompanies_ID ,        
                Invoice_No ,        
                Invoice_Date,  
                SpecialSchemeFlag        
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
                0.0 AS Amount,        
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
                CAST(ISNULL(NonStockable.Acess, 0) AS NUMERIC(18, 2)) AS Acess ,     
                0.0 AS Amount,          
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
        
        
        SET @CashIn = 0
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)  
                         FROM   dbo.LedgerDetail  
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId  
                         WHERE  TransactionId = @V_Received_ID  
                                AND TransactionTypeId = 2  
                                AND AccountId = 10047  
                       )            
            
        UPDATE  dbo.LedgerMaster  
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn  
        WHERE   AccountId = 10047         
                  
                  
                  
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
                          
                          
        -------------------------------Cess Entries Deletion ------------------       
                  
        DELETE  FROM dbo.LedgerDetail  
        WHERE   TransactionId = @V_Received_ID  
                AND TransactionTypeId = 2            
    END        
      
Go

---------------------------------------------------------------------------------------------------------------