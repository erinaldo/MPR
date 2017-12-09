CREATE PROCEDURE Proc_Ledger_InsertOpening
    (  
      @AccountId NUMERIC(18, 0) ,  
      @CashIn NUMERIC(18, 2) ,  
      @CashOut NUMERIC(18, 2) ,  
      @Remarks VARCHAR(500) ,  
      @DivisionId NUMERIC(18, 0) ,  
      @TransactionId INT ,  
      @TransactionTypeId INT ,  
      @Openingdate DATETIME  ,  
      --@TransactionDate DATETIME ,  
      @CreatedBy VARCHAR(100)  
    )  
AS  
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
                  @Openingdate , -- TransactionDate - datetime  
                  @CreatedBy  -- CreatedBy - varchar(100)  
                )  
    END  
  

Alter PROC Proc_AddOpeningBalance  
    (  
      @AccountId NUMERIC(18, 0) ,  
      @Amount NUMERIC(18, 2) ,  
      @DivisionId NUMERIC(18, 0) ,  
      @CreatedBy NVARCHAR(50) ,  
      @Openingdate DATETIME  ,  
      @Type BIGINT  
    )  
AS  
    BEGIN  
  
        UPDATE  dbo.ACCOUNT_MASTER  
        SET     OPENING_BAL = @Amount  
        WHERE   ACC_ID = @AccountId  
  
        DECLARE @OPENINGBALANCEID NUMERIC(18, 0)  
       
  
        SET @OPENINGBALANCEID = ( SELECT    ISNULL(COUNT(OPENINGBALANCEID), 0)  
                                            + 1 AS Id  
                                  FROM      OPENINGBALANCE  
                                )  
  
        INSERT   OPENINGBALANCE  
        VALUES  ( -@OPENINGBALANCEID, @AccountId, @Amount, @Openingdate )  
            
        DECLARE @Remarks VARCHAR(200)   
        SET @Remarks = 'Account Opening Balance as on Date: '  
            + CAST(CONVERT(varchar(20),@Openingdate,106) AS VARCHAR(30))  
        IF @TYPE = 1  
            BEGIN  
               EXECUTE Proc_Ledger_InsertOpening @AccountId, 0,  
                            @Amount, @Remarks, @DivisionId,  
                            @OPENINGBALANCEID, 20,@Openingdate, @CreatedBy  
            END  
  
        IF @TYPE = 2  
            BEGIN  
                  EXECUTE Proc_Ledger_InsertOpening @AccountId,  
                            @Amount, 0, @Remarks, @DivisionId,  
                            @OPENINGBALANCEID, 20,@Openingdate, @CreatedBy  
           END  
  
  
    END   
    
    --------------------------------------------------------------------------------------------------------------------  
  
Alter PROCEDURE [dbo].[ProcPaymentTransaction_Insert]  
    (  
      @PaymentTransactionId NUMERIC = NULL ,  
      @PaymentTransactionNo VARCHAR(100) = NULL ,  
      @PaymentTypeId NUMERIC = NULL ,  
      @AccountId NUMERIC = NULL ,  
      @PaymentDate DATETIME = NULL ,  
      @ChequeDraftNo VARCHAR(50) = NULL ,  
      @ChequeDraftDate DATETIME = NULL ,  
      @BankId NUMERIC = 0 ,  
      @BankDate DATETIME = NULL ,  
      @Remarks VARCHAR(200) = NULL ,  
      @TotalamountReceived NUMERIC(18, 2) = 0 ,  
      @UndistributedAmount NUMERIC(18, 2) = 0 ,  
      @CreatedBy VARCHAR(50) = NULL ,  
      @DivisionId NUMERIC = 0 ,  
      @StatusId NUMERIC = NULL ,  
      @PM_TYPE NUMERIC(18, 0) = NULL ,  
      @ProcedureStatus INT OUTPUT         
  
    )  
AS  
    BEGIN        
  
        DECLARE @LedgerId NUMERIC       
  
        SELECT  @PaymentTransactionId = ISNULL(MAX(PaymentTransactionId), 0)  
                + 1  
        FROM    dbo.PaymentTransaction            
  
        INSERT  INTO PaymentTransaction  
                ( PaymentTransactionId ,  
                  PaymentTransactionNo ,  
                  PaymentTypeId ,  
                  AccountId ,  
                  PaymentDate ,  
                  ChequeDraftNo ,  
                  ChequeDraftDate ,  
                  BankId ,  
                  Remarks ,  
                  TotalAmountReceived ,  
                  UndistributedAmount ,  
                  CreatedBy ,  
                  CreatedDate ,  
                  DivisionId ,  
                  StatusId ,  
                  BankDate ,  
                  PM_TYPE        
  
                )  
        VALUES  ( @PaymentTransactionId ,  
                  @PaymentTransactionNo ,  
                  @PaymentTypeId ,  
                  @AccountId ,  
                  @PaymentDate ,  
                  @ChequeDraftNo ,  
                  @ChequeDraftDate ,  
                  @BankId ,  
                  @Remarks ,  
                  @TotalamountReceived ,  
                  @UndistributedAmount ,  
                  @CreatedBy ,  
                  GETDATE() ,  
                  @DivisionId ,  
                  @StatusId ,  
                  @BankDate ,  
                  @PM_TYPE        
  
                )        
  
        SET @ProcedureStatus = @PaymentTransactionId         
  
        ---increment series  
  
        UPDATE  PM_SERIES  
        SET     CURRENT_USED = CURRENT_USED + 1  
        WHERE   DIV_ID = @DivisionId  
                AND IS_FINISHED = 'N'  
                AND PM_TYPE = @PM_TYPE  
  
        ---update customer ledger          
  
  ---if payment status approved   than make entry in customer ledger        
  
        
  
        IF @StatusId = 2  
            BEGIN  
  
                IF @PM_TYPE = 1  
                    BEGIN  
       SET @Remarks = 'Payment received against reference no.: ' + @ChequeDraftNo  
                        EXECUTE Proc_Ledger_Insert @AccountId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, 18, @CreatedBy  
                    END  
                IF @PM_TYPE = 2  
                    BEGIN  
       SET @Remarks = 'Payment released against reference no.: ' + @ChequeDraftNo  
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, 19, @CreatedBy  
  
                    END   
  
            END  
  
    END
    
    
    -------------------------------------------------------------------------------------------------------------  
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
  
                SET @Remarks = 'Pruchase against party bill no.: '  
                    + @v_Invoice_No + ' - '  
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)  
  
                EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0,  
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3, @V_Created_BY    
                   
  
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
                          NET_AMOUNT ,  
                          MRN_TYPE ,  
                          VAT_ON_EXICE ,  
                          MRNCompanies_ID ,  
                          IsPrinted,          
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
                          @v_NET_AMOUNT ,  
                          @v_MRN_TYPE ,  
                     @V_VAT_ON_EXICE ,  
                          @v_MRNCompanies_ID ,  
                          0,            
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
  
--        IF @V_PROC_TYPE = 3               
  
--            BEGIN                
  
--                DECLARE cur CURSOR              
  
--                    FOR SELECT  Item_ID,              
  
--                                Item_Qty,              
  
--                                Indent_ID              
  
--                        FROM    MATERIAL_RECEIVED_AGAINST_PO_DETAIL              
  
--                        WHERE   Receipt_ID = @V_Receipt_ID                
  
--                
  
--                DECLARE @itemid NUMERIC(18, 0)                
  
--                DECLARE @itemQty NUMERIC(18, 4)                
  
--                DECLARE @IndentID NUMERIC(18, 0)                
  
--                
  
--                
  
--                OPEN cur                
  
--                FETCH NEXT FROM cur INTO @itemid, @itemQty, @IndentID                
  
--                WHILE @@fetch_status = 0                
  
--                    BEGIN                
  
--                   
  
--                        UPDATE  PO_STATUS              
  
--                        SET     RECIEVED_QTY = RECIEVED_QTY - @itemQty,              
  
--                                BALANCE_QTY = BALANCE_QTY + @itemQty              
  
--                        WHERE   PO_ID = @V_PO_ID              
  
--                                AND ITEM_ID = @itemid              
  
--                                AND INDENT_ID = @IndentID                
  
--                
  
--                        UPDATE  ITEM_DETAIL              
  
--                        SET     CURRENT_STOCK = CURRENT_STOCK - @itemQty              
  
--                        WHERE   ITEM_ID = @itemid              
  
--                                AND DIV_ID = @V_Division_ID                 
  
--                
  
--                        FETCH NEXT FROM cur INTO @itemid, @itemQty, @IndentID                
  
--                    END                
  
--                CLOSE cur                
  
--                DEALLOCATE cur                
  
--                  
  
--                DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_DETAIL              
  
--                WHERE   Receipt_ID = @V_Receipt_ID                
  
--                
  
--                DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_MASTER              
  
--                WHERE   Receipt_ID = @V_Receipt_ID                
  
--            END               
  
    END  
  
-------------------------------------------------

  
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
      @v_GROSS_AMOUNT NUMERIC(18, 2) ,  
      @v_GST_AMOUNT NUMERIC(18, 2) ,  
      @v_NET_AMOUNT NUMERIC(18, 2) ,  
      @V_MRN_TYPE INT ,  
      @V_VAT_ON_EXICE INT ,  
      @v_MRNCompanies_ID INT          
  
    )  
AS  
    BEGIN              
  
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
                          @v_NET_AMOUNT ,  
                          @v_MRN_TYPE ,  
                          @V_VAT_ON_EXICE ,  
                          0          
  
                        )               
  
                UPDATE  MRN_SERIES  
                SET     CURRENT_USED = CURRENT_USED + 1  
                WHERE   DIV_ID = @v_Division_ID       
  
      
  
                DECLARE @Remarks VARCHAR(250)  
  
  
  
             SET @Remarks = 'Pruchase against party bill no.: '  
                    + @v_Invoice_No + ' - '  
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)  
  
                EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT,  
                    0, @Remarks, @V_Division_ID, @V_Received_ID, 2,  
                    @V_Created_BY  
  
                
  
            END   
  
    END  
    
