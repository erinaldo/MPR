
INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0012_11_May_2018_ACESS' ,
          GETDATE()
        )
          go
--ADMIN + MMS------
ALTER TABLE dbo.ITEM_MASTER ADD ACess NUMERIC(18,2) DEFAULT 0
ALTER TABLE dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL ADD ACess NUMERIC(18,2) DEFAULT 0 
ALTER TABLE dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO ADD ACess NUMERIC(18,2) DEFAULT 0 
ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL ADD ACess NUMERIC(18,2) DEFAULT 0 
ALTER TABLE dbo.SALE_INVOICE_DETAIL ADD ACess NUMERIC(18,2) DEFAULT 0 
ALTER TABLE dbo.SALE_INVOICE_DETAIL ADD ACessAmount NUMERIC(18,2) DEFAULT 0 

go
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
                CAST(dbo.Get_item_rate_from_previous_mrn(@V_ITEM_ID) AS NUMERIC(18,
                                                              2)) AS prev_mrn_rate
        FROM    ITEM_MASTER IM
                INNER JOIN VAT_MASTER vm ON vm.VAT_ID = IM.vat_id
                LEFT JOIN dbo.CessMaster cm ON cm.pk_CessId_num = IM.fk_CessId_num
                INNER JOIN UNIT_MASTER UM ON IM.UM_ID = UM.UM_ID
                INNER JOIN item_category IC ON IM.item_category_id = IC.item_cat_id
        WHERE   IM.ITEM_ID = @V_ITEM_ID     
      
    END     
    
    go
  -----------------------------
  
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
                CAST(MD.Item_Qty AS NUMERIC(18, 3)) AS BATCH_QTY ,
                CAST(MD.Item_Rate AS NUMERIC(18, 2)) AS Item_Rate ,
                ISNULL(DType, 'P') AS DType ,
                CAST(ISNULL(DiscountValue, 0) AS NUMERIC(18, 2)) AS DISC ,
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
                ISNULL(DiscountValue, 0) AS DISC ,
                NonStockable.Item_Exice AS exe_Per ,
                NonStockable.batch_no AS BATCH_NO ,
                NonStockable.batch_date AS EXPIRY_DATE
        FROM    dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO NonStockable
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON NonStockable.Item_ID = IM.ITEM_ID
                INNER JOIN dbo.COST_CENTER_MASTER ON NonStockable.CostCenter_ID = dbo.COST_CENTER_MASTER.CostCenter_Id
        WHERE   Received_ID = @V_Receive_ID              
              
    END        
    
    ------------------------------
GO

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
                          @v_DiscountValue               
                        )                    
            END                  
                
    END 
    GO
    --------------------------------------------------
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
                          ( ISNULL(@v_CESS_AMOUNT, 0) + ISNULL(@v_ACESS_AMOUNT,
                                                              0) ) ,
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
                
                
                
                SET @Remarks = 'Add. Cess against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)     
                EXECUTE Proc_Ledger_Insert 10011, 0, @v_ACESS_AMOUNT, @Remarks,
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
                
                
                SET @Remarks = 'Add. Cess against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)     
                EXECUTE Proc_Ledger_Insert 10011, 0, @v_ACESS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @V_Received_Date,
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
    
    --------------------------------------
    go
    
      
        ALTER PROC [dbo].[Proc_ReverseMRNEntry]
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
                        
                        
        -------------------------------Cess Entries Deletion -----------------------     
                
        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @V_Received_ID
                AND TransactionTypeId = 2          
    END      
    
    ----------------------------------------------------------
    GO
    
    ALTER PROCEDURE [dbo].[GET_INV_ITEM_DETAILS] ( @V_SI_ID NUMERIC(18, 0) )    
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
                ISNULL(SID.CessPercentage_num,0) AS Cess ,   
                ISNULL(SID.ACess,0) AS ACess,    
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
      
  -----------------------------------------
  GO
  
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
                          ACess,  
                          ACessAmount        
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
                          @v_ACESS  ,  
                          ISNULL(@v_ACESS,0)*@V_ITEM_QTY      
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
    -------------------------------
    GO
    
    CREATE PROCEDURE [dbo].[PROC_OUTSIDE_SALE_MASTER_SALE_NEW]  
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
                      
                      
                   SET @Remarks = 'Add. Cess against invoice No- ' + @V_SI_CODE  
                    + CAST(@V_SI_NO AS VARCHAR(50))  
                      
                EXECUTE Proc_Ledger_Insert 10012, @v_ACESS_AMOUNT, 0, @Remarks,  
                    @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE, @V_Created_BY  
                       
              
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
    
    -----------------------------------------------------
    
    ALTER PROCEDURE [dbo].[PROC_OUTSIDE_SALE_MASTER_SALE_BillBook]  
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
                        SI_DATE = @V_SI_DATE ,  
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
                      
                      
                      
                SET @Remarks = 'Add. Cess against invoice No- ' + @V_SI_CODE  
                    + CAST(@V_SI_NO AS VARCHAR(50))  
                      
                EXECUTE Proc_Ledger_Insert 10012, @v_ACESS_AMOUNT, 0, @Remarks,  
                    @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE, @V_Created_BY      
                      
                      
              
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
    go
    -----------------------------------
    