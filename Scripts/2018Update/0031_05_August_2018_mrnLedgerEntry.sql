﻿INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0031_05_August_2018_mrnLedgerEntry' ,
          GETDATE()
        )
Go

------------------------------------------------------------------------------------------------------------------------------------
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
      @V_Reference_ID NUMERIC(18, 0)
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
                    @v_Receipt_Date, @V_Created_BY                      
                  
                EXECUTE Proc_Ledger_Insert @V_Reference_ID, 0, @v_GROSS_AMOUNT,
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3,
                    @v_Receipt_Date, @V_Created_BY                   
                  
                SET @Remarks = 'Freight against party bill no.: '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                   
                  
                EXECUTE Proc_Ledger_Insert 10047, @v_freight, 0, @Remarks,
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
                              
                EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT, @Remarks,
                    @V_Division_ID, @v_Receipt_ID, 3, @v_Receipt_Date,
                    @V_Created_BY                  
                  
                  
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
-----------------------------------------------------------------------------------------------------------------------------------------------------
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
      @V_Reference_ID NUMERIC(18, 0)
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
                    @v_Receipt_Date, @V_Created_BY                      
                  
                EXECUTE Proc_Ledger_Insert @V_Reference_ID, 0, @v_GROSS_AMOUNT,
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3,
                    @v_Receipt_Date, @V_Created_BY                   
                  
                SET @Remarks = 'Freight against party bill no.: '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                   
                  
                EXECUTE Proc_Ledger_Insert 10047, @v_freight, 0, @Remarks,
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
                              
                EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT, @Remarks,
                    @V_Division_ID, @v_Receipt_ID, 3, @v_Receipt_Date,
                    @V_Created_BY                  
                  
                  
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

-------------------------------------------------------------------------------------------------------------------------------------------------------------------

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
      @V_Reference_ID NUMERIC(18, 0)
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
                          FreightTaxValue ,
                          FK_ITCEligibility_ID ,
                          Reference_ID                            
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
                          @V_Reference_ID        
                        )                                
                              
                              
                UPDATE  MRN_SERIES
                SET     CURRENT_USED = CURRENT_USED + 1
                WHERE   DIV_ID = @v_Division_ID                                
                              
                SET @Remarks = 'Purchase against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                               
                              
                              
                EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2,
                    @v_Received_Date, @V_Created_BY                                
                              
                              
                              
                EXECUTE Proc_Ledger_Insert @V_Reference_ID, 0, @v_GROSS_AMOUNT,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2,
                    @v_Received_Date, @V_Created_BY         
                              
                              
                SET @Remarks = 'Freight against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)      
                              
                EXECUTE Proc_Ledger_Insert 10047, @v_freight, 0, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @v_Received_Date,
                    @V_Created_BY                                 
                              
                              
                SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                               
                              
                IF @V_MRN_TYPE <> 2
                    BEGIN                      
                              
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @v_Received_Date, @V_Created_BY                              
                              
                              
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @v_Division_ID, @V_Received_ID, 2,
                            @v_Received_Date, @V_Created_BY                              
                    END                   
                              
                              
                              
                ELSE
                    BEGIN                              
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @v_Received_Date, @V_Created_BY                              
                    END                     
                                        
                SET @Remarks = 'Cess against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                    
                          
                                    
                EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @v_Received_Date,
                    @V_Created_BY                                
                              
                              
                              
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
                              
                              
                SET @Remarks = 'Stock Out against party  invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)       
                              
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @v_Received_Date,
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
                        Reference_ID = @V_Reference_ID
                WHERE   Received_ID = @V_Received_ID   
                              
                              
                SET @Remarks = 'Purchase against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)     
                              
                EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2,
                    @TransactionDate, @V_Created_BY      
                              
                EXECUTE Proc_Ledger_Insert @V_Reference_ID, 0, @v_GROSS_AMOUNT,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2,
                    @TransactionDate, @V_Created_BY        
                              
                              
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

----------------------------------------------------------------------------------------------------------------------------------------------------
--EXEC PROC_GET_SUPPLIER_LEDGER_DETAILS '2018-04-01','2018-07-03',84      
ALTER PROC [dbo].[PROC_GET_SUPPLIER_LEDGER_DETAILS]
    (
      @Fromdate AS DATETIME = NULL ,
      @ToDate AS DATETIME = NULL ,
      @AccountId AS INT = 0      
    )
AS
    BEGIN      
 -----------------Party Details With Opening Balance--------------------------------------------------------------------       
      
        DECLARE @OpeningBalance NUMERIC(18, 2)      
      
        SET @OpeningBalance = ( SELECT  ISNULL(SUM(CASHIN - CASHOUT), 0) AS OpeningBalance
                                FROM    dbo.LedgerMaster
                                        LEFT OUTER  JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TRANSACTIONDATE, 101) AS DATETIME) < CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                        INNER  JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.LedgerMaster.AccountId
                                WHERE   LedgerMaster.AccountId = @AccountId
                              )         
      
-----------------------------------Party Details With Opening Balance--------------------------------------------------------------------       
      
      
      
      
      
      
      
        IF OBJECT_ID('tempdb..#Ledgertemp') IS NOT NULL
            DROP TABLE #Ledgertemp        
      
        CREATE TABLE #Ledgertemp
            (
              LedgerId NUMERIC(18, 0) ,
              TRANSACTIONDATE DATETIME ,
              AccId NUMERIC(18, 0) ,
              AmountInHand NUMERIC(18, 2) ,
              CASHIN NUMERIC(18, 2) ,
              CASHOUT NUMERIC(18, 2) ,
              REMARKS NVARCHAR(500) ,
              TransactionTypeId NUMERIC(18, 0) ,
              ClosingBalance NUMERIC(18, 2) ,
              CheckgBalance NVARCHAR(500)
            )       
        DECLARE @LedgerId INT ,
            @TRANSACTIONDATE DATETIME ,
            @AccId NUMERIC(18, 0) ,
            @AmountInHand NUMERIC(18, 2) ,
            @CASHIN NUMERIC(18, 2) ,
            @CASHOUT NUMERIC(18, 2) ,
            @REMARKS NVARCHAR(500) ,
            @TransactionTypeId NUMERIC(18, 0)      
      
        DECLARE Ledger_cursor CURSOR
        FOR
            SELECT  LedgerId ,
                    TRANSACTIONDATE ,
                    AccountId ,
                    AmountInHand ,
                    CASH_IN ,
                    CASH_OUT ,
                    REMARKS ,
                    TransactionTypeId
            FROM    ( SELECT    dbo.LedgerMaster.LedgerId ,
                                CASE WHEN dbo.LedgerDetail.TransactionTypeId = 2
                                     THEN mrwpm.Received_Date
                                     WHEN dbo.LedgerDetail.TransactionTypeId = 3
                                     THEN mrapm.Receipt_Date
                                     ELSE TRANSACTIONDATE
                                END AS TRANSACTIONDATE ,
                                AccountId ,
                                AmountInHand ,
                                ISNULL(CASHIN, 0) AS CASH_IN ,
                                ISNULL(CASHOUT, 0) AS CASH_OUT ,
                                dbo.LedgerDetail.REMARKS ,
                                TransactionTypeId
                      FROM      dbo.LedgerMaster
                                INNER JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TransactionDate, 101) AS DATETIME) BETWEEN CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                                              AND
                                                              CAST(CONVERT(VARCHAR, @ToDate, 101) AS DATETIME)
                                LEFT JOIN MATERIAL_RECIEVED_WITHOUT_PO_MASTER mrwpm ON mrwpm.Received_ID = dbo.LedgerDetail.TransactionId
                                                              AND dbo.LedgerDetail.TransactionTypeId = 2
                                LEFT JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER mrapm ON mrapm.Receipt_ID = dbo.LedgerDetail.TransactionId
                                                              AND dbo.LedgerDetail.TransactionTypeId = 3
                      WHERE     dbo.LedgerMaster.AccountId = @AccountId
                    ) tb
            ORDER BY TRANSACTIONDATE      
      
        OPEN Ledger_cursor      
      
      
      
        FETCH NEXT FROM Ledger_cursor INTO @LedgerId, @TRANSACTIONDATE, @AccId,
            @AmountInHand, @CASHIN, @CASHOUT, @REMARKS, @TransactionTypeId      
      
        WHILE @@FETCH_STATUS = 0
            BEGIN      
      
                INSERT  INTO #Ledgertemp
                        SELECT  @LedgerId ,
                                @TRANSACTIONDATE ,
                                @AccId ,
                                @AmountInHand ,
                                @CASHIN ,
                                @CASHOUT ,
                                @REMARKS ,
                                @TransactionTypeId ,
                                @OpeningBalance - @CASHOUT + @CASHIN ,
                                ( CAST(@OpeningBalance AS VARCHAR(50)) + '  -'
                                  + CAST(@CASHOUT AS VARCHAR(50)) + '  +'
                                  + CAST(@CASHIN AS VARCHAR(50)) )      
      
      
      
                SET @OpeningBalance = @OpeningBalance - @CASHOUT + @CASHIN        
      
                FETCH NEXT FROM Ledger_cursor INTO @LedgerId, @TRANSACTIONDATE,
                    @AccId, @AmountInHand, @CASHIN, @CASHOUT, @REMARKS,
                    @TransactionTypeId       
      
            END      
      
      
      
        CLOSE Ledger_cursor      
        DEALLOCATE Ledger_cursor      
      
        SELECT  LedgerId ,
                TRANSACTIONDATE ,
                AccId ,
                AmountInHand ,
                CASHIN ,
                CASHOUT ,
                REMARKS ,
                ClosingBalance AS ClosingBalance
        FROM    #Ledgertemp
        ORDER BY TRANSACTIONDATE      
      
      
      
    END