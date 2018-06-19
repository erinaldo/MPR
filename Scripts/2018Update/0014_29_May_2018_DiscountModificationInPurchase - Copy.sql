
INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0014_29_May_2018_DiscountModificationInPurchase' ,
          GETDATE()
        )
Go


ALTER TABLE dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL ADD DiscountValue1 NUMERIC(18,2) DEFAULT 0.00
ALTER TABLE dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL ADD GSTPAID VARCHAR(5) DEFAULT 'N'
ALTER TABLE dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO ADD DiscountValue1 NUMERIC(18,2) DEFAULT 0.00
ALTER TABLE dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO ADD GSTPAID VARCHAR(5) DEFAULT 'N'
ALTER TABLE dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER ADD CashDiscount_Amt NUMERIC(18,3) DEFAULT 0.00

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
				ISNULL(CashDiscount_Amt,0) AS  CashDiscount_Amt,  
                MRNCompanies_ID ,  
                Invoice_No ,  
                Invoice_Date  
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
      @v_A_cess NUMERIC(18, 4) ,  
      @v_Item_exice NUMERIC(18, 4) ,  
      @v_Batch_no VARCHAR(50) ,  
      @v_Expiry_Date DATETIME ,  
      @V_Trans_Type NUMERIC(18, 0) ,  
      @v_Proc_Type INT ,  
      @v_DType CHAR(1) ,  
      @v_DiscountValue NUMERIC(18, 2) , 
      @v_DiscountValue1 NUMERIC(18, 2) ,
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
                          GSTPaid                        
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
                          @v_GSTPaid                
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
                          GSTPaid                        
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
                          @v_GSTPaid                 
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
    @v_DiscountValue NUMERIC(18, 2) ,
    @v_DiscountValue1 NUMERIC(18, 2) ,
    @v_GSTPaid CHAR(1)
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
                  DiscountValue ,
                  DiscountValue1,
                  GSTPaid       
                )  
        VALUES  ( 
				  /* Received_ID - int */  
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
                  @v_DiscountValue ,
                  @v_DiscountValue1 ,
                  @v_GSTPaid       
                )           
    END 

Go

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
                              CashDiscount_Amt ,  
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
                              0                      
                    
                            )                      
                    
                    
                    UPDATE  MRN_SERIES  
                    SET     CURRENT_USED = CURRENT_USED + 1  
                    WHERE   DIV_ID = @v_Division_ID                      
                    
                    SET @Remarks = 'Purchase against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                     
                    
                    
                    EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @V_Received_Date, @V_Created_BY                      
                    
                    
                    
                    EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @V_Received_Date, @V_Created_BY                      
                    
                    
                    SET @Remarks = 'GST against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                     
                    
                    IF @V_MRN_TYPE <> 2  
                        BEGIN            
                    
                            EXECUTE Proc_Ledger_Insert @CInputID, 0,  
                                @CGST_Amount, @Remarks, @V_Division_ID,  
                                @V_Received_ID, 2, @V_Received_Date,  
                                @V_Created_BY                    
                    
                    
                            EXECUTE Proc_Ledger_Insert @InputID, 0,  
                                @CGST_Amount, @Remarks, @v_Division_ID,  
                                @V_Received_ID, 2, @V_Received_Date,  
                                @V_Created_BY                    
                        END                    
                    
                    
                    
                    ELSE  
                        BEGIN                    
                            EXECUTE Proc_Ledger_Insert @InputID, 0,  
                                @v_GST_AMOUNT, @Remarks, @V_Division_ID,  
                                @V_Received_ID, 2, @V_Received_Date,  
                                @V_Created_BY                    
                        END           
                              
                    SET @Remarks = 'Cess against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)          
                
                          
                    EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @V_Received_Date, @V_Created_BY                      
                    
                    
                    
                    SET @Remarks = 'Add. Cess against party invoice No- '  
                        + @v_Invoice_No + ' - '  
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)         
                    EXECUTE Proc_Ledger_Insert 10011, 0, @v_ACESS_AMOUNT,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @V_Received_Date, @V_Created_BY      
                    
                    
                    
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
                    
                    
                    
                    EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT,  
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                        @V_Received_Date, @V_Created_BY                    
                    
                    
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
                            CashDiscount_Amt = @v_CashDiscount_amt ,  
                            freight_type = @v_freight_type ,  
                            MRNCompanies_ID = @v_MRNCompanies_ID ,  
                            GROSS_AMOUNT = @v_GROSS_AMOUNT ,  
                            GST_AMOUNT = @v_GST_AMOUNT ,  
                            CESS_AMOUNT = ( ISNULL(@v_CESS_AMOUNT, 0)  
                                + ISNULL(@v_ACESS_AMOUNT, 0) ) , 
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
                    
                   
                    
                    EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT,  
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
                        @V_Received_Date, @V_Created_BY      
                    
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