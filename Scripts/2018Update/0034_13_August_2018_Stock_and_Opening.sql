INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0034_13_August_2018_Stock_and_Opening' ,
          GETDATE()
        )
Go



ALTER PROCEDURE [dbo].[Proc_Ledger_Insert]
    (
      @AccountId NUMERIC(18, 0) ,
      @CashIn NUMERIC(18, 2) ,
      @CashOut NUMERIC(18, 2) ,
      @Remarks VARCHAR(500) ,
      @DivisionId NUMERIC(18, 0) ,
      @TransactionId INT ,
      @TransactionTypeId INT ,
      @TransactionDate DATETIME ,
      @CreatedBy VARCHAR(100)
    )
AS
    BEGIN    
        IF @AccountId <> 10073
            BEGIN 
                DECLARE @LedgerId NUMERIC(18, 0)     
    ---------if customer ledger not exists then make entry    
                IF EXISTS ( SELECT  AccountId
                            FROM    dbo.LedgerMaster
                            WHERE   AccountId = @AccountId )
                    SELECT  @LedgerId = LedgerId
                    FROM    dbo.LedgerMaster
                    WHERE   AccountId = @AccountId    
                ELSE
                    BEGIN    
                        SELECT  @LedgerId = ISNULL(MAX(LedgerId), 0) + 1
                        FROM    dbo.LedgerMaster     
                        INSERT  INTO dbo.LedgerMaster
                                ( LedgerId ,
                                  AccountId ,
                                  AmountInHand ,
                                  DivisionId    
                                )
                        VALUES  ( @LedgerId , -- LedgerId - numeric    
                                  @AccountId , -- AccountId - numeric    
                                  0 , -- AmountInHand - numeric    
                                  @DivisionId -- DivisionId - bigint    
                                )    
                    END     
       -------update amount in ledger    
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @AccountId    
       -------entry in ledger detail    
                DECLARE @LedgerDetailId AS NUMERIC    
                SELECT  @LedgerDetailId = ISNULL(MAX(LedgerDetailId), 0) + 1
                FROM    LedgerDetail    
                INSERT  INTO dbo.LedgerDetail
                        ( LedgerDetailId ,
                          LedgerId ,
                          CashIn ,
                          CashOut ,
                          Remarks ,
                          TransactionId ,
                          TransactionTypeId ,
                          TransactionDate ,
                          CreatedBy    
                        )
                VALUES  ( @LedgerDetailId , -- LedgerDetailId - numeric    
                          @LedgerId , -- LedgerId - numeric    
                          @CashIn , -- CashIn - numeric    
                          @CashOut , -- CashOut - numeric    
                          @Remarks , -- Remarks - varchar(500)    
                          @TransactionId , -- TransactionId - int    
                          @TransactionTypeId , -- TransactionTypeId - int    
                          @TransactionDate , --GETDATE() , -- TransactionDate - datetime    
                          @CreatedBy  -- CreatedBy - varchar(100)    
                        )    
            END 
    END    
  
  GO

CREATE FUNCTION [dbo].[Get_Opening_as_on_date_opening]
    (
      @AccountId NUMERIC(18, 0) ,
      @Fromdate DATETIME        
        
    )
RETURNS NUMERIC(18, 2)
AS
    BEGIN        
        DECLARE @OpeningBalance NUMERIC(18, 2)        
        
        DECLARE @StartOpeningBalance NUMERIC(18, 2)  
        SET @StartOpeningBalance = 0        
        
        SET @StartOpeningBalance = ISNULL(( SELECT  CASE WHEN type = 1  
                                                         THEN -OpeningAmount  
                                                         ELSE OpeningAmount  
                                                    END AS amount  
                                            FROM    dbo.OpeningBalance  
                                            WHERE   FkAccountId = @AccountId  
                                                    AND CAST(OpeningDate AS DATE) = CAST(@Fromdate AS DATE)  
                                          ), 0)        
        
        SET @OpeningBalance = ISNULL(( SELECT  
										--ISNULL(SUM(CASHIN - CASHOUT), 0) AS OpeningBalance
										CASE WHEN AccountId <> 10073
                                             THEN ISNULL(SUM(ISNULL(CASHIN,0) - ISNULL(CASHOUT,0)),
                                                         0)
                                             ELSE CASE WHEN AccountId = 10073
                                                            AND ( ISNULL(SUM(ISNULL(CASHIN,0) - ISNULL(CASHOUT,0)), 0) ) < 0
                                                       THEN ISNULL(SUM(ISNULL(CASHIN,0) - ISNULL(CASHOUT,0)), 0)
                                                       WHEN AccountId = 10073
                                                            AND ( ISNULL(SUM(ISNULL(CASHIN,0) - ISNULL(CASHOUT,0)), 0) ) > 0
                                                       THEN 0.00 
                                                  END
                                        END AS OpeningBalance
                                FROM    dbo.LedgerMaster
                                        JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(TRANSACTIONDATE AS DATE) < CAST(@FromDate AS DATE)
                                        INNER  JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.LedgerMaster.AccountId
                                WHERE   LedgerMaster.AccountId = @AccountId
                                GROUP BY AccountId 
                              ), 0)
                              
        SET @OpeningBalance = @StartOpeningBalance + @OpeningBalance        
        RETURN @OpeningBalance        
    END
    
    GO

CREATE FUNCTION [dbo].[Get_PrimaryGroupOpening_as_on_date_opening] 
    (
      @agID NUMERIC ,
      @Fromdate DATETIME        
        
    )
RETURNS NUMERIC(18, 2)
AS
    BEGIN
        DECLARE @OpeningBalance NUMERIC(18, 2)        
        
        DECLARE @StartOpeningBalance NUMERIC(18, 2)  
        SET @StartOpeningBalance = 0        
        
        SET @StartOpeningBalance = ISNULL(( SELECT  SUM(amount)
                                            FROM    ( SELECT  CASE
                                                              WHEN type = 1
                                                              THEN -OpeningAmount
                                                              ELSE OpeningAmount
                                                              END AS amount
                                                      FROM    dbo.OpeningBalance
                                                      WHERE   FkAccountId IN (
                                                              SELECT
                                                              ACC_ID
                                                              FROM
                                                              dbo.ACCOUNT_MASTER
                                                              WHERE
                                                              AG_ID IN (
                                                              SELECT
                                                              AG_ID
                                                              FROM
                                                              dbo.ACCOUNT_GROUPS
                                                              WHERE
                                                              Primary_AG_ID = @agID )
                                                               OR dbo.ACCOUNT_MASTER.AG_ID=@agID
                                                               )
                                                              AND CAST(OpeningDate AS DATE) = CAST(@Fromdate AS DATE)
                                                    ) tb
                                          ), 0)       
        
        SET @OpeningBalance = ISNULL((SELECT SUM(ISNULL(OpeningBalance,0)) FROM 
									( SELECT  CASE WHEN AccountId <> 10073
                                             THEN ISNULL(SUM(ISNULL(CASHIN,0)) - SUM(ISNULL(CASHOUT,0)), 0)
                                             ELSE CASE WHEN AccountId = 10073
                                                            AND ( ISNULL(SUM(ISNULL(CASHIN,0)) - SUM(ISNULL(CASHOUT,0)), 0) ) < 0
                                                       THEN ISNULL(SUM(ISNULL(CASHIN,0)) - SUM(ISNULL(CASHOUT,0)), 0)
                                                       WHEN AccountId = 10073
                                                            AND ( ISNULL(SUM(ISNULL(CASHIN,0)) - SUM(ISNULL(CASHOUT,0)), 0) ) > 0
                                                       THEN 0.00 
                                                  END
                                        END AS OpeningBalance
                                FROM    dbo.LedgerMaster
                                        JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(TRANSACTIONDATE AS DATE) < CAST(@Fromdate AS DATE)
                                        INNER  JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.LedgerMaster.AccountId
                                WHERE   LedgerMaster.AccountId IN (
                                        SELECT  ACC_ID
                                        FROM    dbo.ACCOUNT_MASTER
                                        WHERE   AG_ID IN (
                                                SELECT  AG_ID
                                                FROM    dbo.ACCOUNT_GROUPS
                                                WHERE   Primary_AG_ID = @agID ) OR dbo.ACCOUNT_MASTER.AG_ID= @agID)
                               GROUP BY AccountId
                              )tb)
                              , 0) 
        SET @OpeningBalance = @StartOpeningBalance + @OpeningBalance      
        RETURN @OpeningBalance        
    END
GO
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
                                    
                EXECUTE Proc_Ledger_Insert 10047, 0, @v_freight, @Remarks,      
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
                                    
                    
                IF @V_Reference_ID = 10070    
                BEGIN    
                    
    SET @Remarks = 'Stock Out against party  invoice No- '      
                    + @v_Invoice_No + ' - '      
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)             
                                    
    EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT, @Remarks,      
                    @V_Division_ID, @V_Received_ID, 2, @v_Received_Date,      
                    @V_Created_BY    
                        
                END          
                                    
            END               
                                      
        IF @V_PROC_TYPE = 2      
            BEGIN                     
                                              
                DECLARE @TransactionDate DATETIME  =@V_Received_Date                            
                                              
                EXEC Proc_ReverseMRNEntry @V_Received_ID, @V_Vendor_ID                                
                                              
                --SELECT  @TransactionDate = Received_Date      
                --FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER      
                --WHERE   Received_ID = @V_Received_ID                              
                                              
                UPDATE  MATERIAL_RECIEVED_WITHOUT_PO_MASTER      
                SET     Purchase_Type = @V_Purchase_Type ,      
                        Vendor_ID = @V_Vendor_ID ,      
                        Received_Date=@V_Received_Date,
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
                                    
                EXECUTE Proc_Ledger_Insert 10047, 0, @v_freight, @Remarks,      
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
                    
                    
                    
                IF @V_Reference_ID = 10070    
                BEGIN    
     
    SET @Remarks = 'Stock Out against party  invoice No- '      
                    + @v_Invoice_No + ' - '      
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)                                       
                                    
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT, @Remarks,      
                    @V_Division_ID, @V_Received_ID, 2, @TransactionDate,      
                    @V_Created_BY     
                        
                END                           
                                    
            END                                    
    END 