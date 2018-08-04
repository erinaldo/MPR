INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0028_01_August_2018_FreightTaxChanges' ,
          GETDATE()
        )
Go

ALTER TABLE MATERIAL_RECIEVED_WITHOUT_PO_MASTER ADD FreightTaxApplied  bit NOT NULL DEFAULT 0

Go

ALTER TABLE MATERIAL_RECIEVED_WITHOUT_PO_MASTER ADD FreightTaxValue  numeric(18,2) NOT NULL DEFAULT 0.00

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
      @V_FreightTaxValue NUMERIC(18, 2)
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
                          SpecialSchemeFlag ,
                          FreightTaxApplied ,
                          FreightTaxValue                           
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
                          @V_FreightTaxValue       
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
                            
                            
                            
                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @v_Invoice_Date,
                    @V_Created_BY       
                            
                            
                SET @Remarks = 'Freight against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)    
                            
                EXECUTE Proc_Ledger_Insert 10047, @v_freight, 0, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @v_Invoice_Date,
                    @V_Created_BY                               
                            
                            
                SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                             
                            
                IF @V_MRN_TYPE <> 2
                    BEGIN                    
                            
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @v_Invoice_Date, @V_Created_BY                            
                            
                            
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @v_Division_ID, @V_Received_ID, 2,
                            @v_Invoice_Date, @V_Created_BY                            
                    END                 
                            
                            
                            
                ELSE
                    BEGIN                            
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @v_Invoice_Date, @V_Created_BY                            
                    END                   
                                      
                SET @Remarks = 'Cess against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                  
                        
                                  
                EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @v_Invoice_Date,
                    @V_Created_BY                              
                            
                            
                            
                SET @Remarks = 'Add. Cess against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)        
                                       
                EXECUTE Proc_Ledger_Insert 10011, 0, @v_ACESS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @v_Invoice_Date,
                    @V_Created_BY              
                            
                            
                            
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
                            
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @v_Invoice_Date,
                    @V_Created_BY      
                            
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
                        SpecialSchemeFlag = @V_Special_Scheme ,
                        FreightTaxApplied = @V_FreightTaxApplied ,
                        FreightTaxValue = @V_FreightTaxValue
                WHERE   Received_ID = @V_Received_ID                            
                            
                            
                            
                SET @Remarks = 'Purchase against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)   
                            
                EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2,
                    @TransactionDate, @V_Created_BY    
                            
                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @TransactionDate,
                    @V_Created_BY      
                            
                            
                SET @Remarks = 'Freight against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)    
                            
                EXECUTE Proc_Ledger_Insert 10047, @v_freight, 0, @Remarks,
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
                FreightTaxValue
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

Go

-----Purchase Order----


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
                --ITEM_DETAIL.PURCHASE_VAT_ID AS Vat_Id ,
                CASE WHEN PO_DETAIL.VAT_PER = 0 THEN 0 ELSE ITEM_DETAIL.PURCHASE_VAT_ID End AS Vat_Id ,    
                --VAT_MASTER.VAT_NAME , 
                CASE WHEN PO_DETAIL.VAT_PER = 0 THEN '0% GST' ELSE VAT_MASTER.VAT_NAME End AS VAT_NAME ,  
                PO_DETAIL.EXICE_PER AS Exice_Per ,
                CASE WHEN PO_DETAIL.VAT_PER = 0 THEN 0 ELSE VAT_MASTER.VAT_PERCENTAGE End AS Vat_Per , 
                0 AS Item_Value ,  
                PO_STATUS.ITEM_ID AS Item_ID ,  
                ITEM_MASTER.UM_ID ,  
                PO_STATUS.INDENT_ID,  
                --ISNULL(CessMaster.pk_CessId_num,0) AS Cess_Id, 
                CASE WHEN PO_DETAIL.CESS_PER = 0 THEN 0 ELSE ISNULL(CessMaster.pk_CessId_num,0) End AS Cess_Id ,   
                --ISNULL(CessMaster.CessName_vch,'0%') + ' CESS'  AS Cess_Name, 
                CASE WHEN PO_DETAIL.CESS_PER = 0 THEN '0% CESS' ELSE ISNULL(CessMaster.CessName_vch,'0%') + ' CESS' End AS Cess_Name ,  
                --ISNULL(CessMaster.CessPercentage_num,0.00) AS Cess_Per
                CASE WHEN PO_DETAIL.CESS_PER = 0 THEN 0 ELSE ISNULL(CessMaster.CessPercentage_num,0.00) End AS Cess_Per 
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

---MRAPM---


ALTER TABLE MATERIAL_RECEIVED_AGAINST_PO_MASTER ADD FreightTaxApplied  bit NOT NULL DEFAULT 0

Go

ALTER TABLE MATERIAL_RECEIVED_AGAINST_PO_MASTER ADD FreightTaxValue  numeric(18,2) NOT NULL DEFAULT 0.00

GO


ALTER TABLE MATERIAL_RECEIVED_AGAINST_PO_MASTER ADD CashDiscount_Amt  numeric(18,2) NOT NULL DEFAULT 0.00

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
      @v_MRNCompanies_ID INT ,
      @v_CashDiscount_amt NUMERIC(18, 2) = NULL ,               
      @V_FreightTaxApplied INT ,
      @V_FreightTaxValue NUMERIC(18, 2)        
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
                SET @RoundOff = ( @v_Other_Charges )              
              
                SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10023      
                                             WHEN @V_MRN_TYPE = 2 THEN 10020      
                                             WHEN @V_MRN_TYPE = 3 THEN 10074      
                                        END AS inputid      
                               )           
              
              
                SET @Remarks = 'Purchase against party bill no.: '      
                    + @v_Invoice_No + ' - '      
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)               
              
                EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0,      
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3,      
                    @v_Invoice_Date, @V_Created_BY                  
              
                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,      
                    @V_Division_ID, @V_Receipt_ID, 3, @v_Invoice_Date,      
                    @V_Created_BY               
              
    SET @Remarks = 'Freight against party bill no.: '      
                    + @v_Invoice_No + ' - '      
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)               
              
                EXECUTE Proc_Ledger_Insert 10047, @v_freight, 0,      
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3,      
                    @v_Invoice_Date, @V_Created_BY  
              
                SET @Remarks = 'GST against party invoice No- '      
                    + @v_Invoice_No + ' - '      
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)               
              
                IF @V_MRN_TYPE <> 2      
                    BEGIN                
              
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,      
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,      
                            @v_Invoice_Date, @V_Created_BY               
              
              
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,      
                            @Remarks, @v_Division_ID, @v_Receipt_ID, 3,      
                            @v_Invoice_Date, @V_Created_BY               
                    END            
              
              
                ELSE      
                    BEGIN              
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,      
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,      
                            @v_Invoice_Date, @V_Created_BY               
                    END       
                          
                SET @Remarks = 'Cess against party invoice No- '      
                    + @v_Invoice_No + ' - '      
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)         
                          
                EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT,      
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,      
                            @v_Invoice_Date, @V_Created_BY              
              
              
                SET @Remarks = 'Round Off against party invoice No- '      
                    + @v_Invoice_No + ' - '      
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)              
              
                IF @RoundOff > 0      
                    BEGIN              
              
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,      
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,      
                            @v_Invoice_Date, @V_Created_BY                     
                    END              
              
                ELSE      
                    BEGIN              
              
                        SET @RoundOff = -+@RoundOff        
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,      
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,      
                            @v_Invoice_Date, @V_Created_BY               
              
                    END               
              
              
                SET @Remarks = 'Stock Out against party  invoice No- '      
                    + @v_Invoice_No + ' - '      
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)              
              
              
              
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT, @Remarks,      
                    @V_Division_ID, @v_Receipt_ID, 3, @v_Invoice_Date,      
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
                          CUST_ID ,
                          CashDiscount_Amt ,
                          FreightTaxApplied ,
                          FreightTaxValue                
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
                          @V_CUST_ID,
                          @v_CashDiscount_amt ,
                          @V_FreightTaxApplied ,
                          @V_FreightTaxValue   
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
                        MRNCompanies_ID = @v_MRNCompanies_ID ,
                        CashDiscount_Amt = @v_CashDiscount_amt,
                        FreightTaxApplied = @V_FreightTaxApplied,
                        FreightTaxValue = @V_FreightTaxValue    
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

Go
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
                IM.barcode_vch as ITEM_CODE ,
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
                AND PO_DETAIL.BALANCE_QTY > 0  
    --            AND ( ( CASE WHEN ( @open <> 1 ) THEN PO_DETAIL.BALANCE_QTY    
    --                         ELSE 1    
    --                    END ) > 0 )      
    END 