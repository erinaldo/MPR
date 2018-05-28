
INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0013_24_May_2018_EditFormsFunctionality' ,
          GETDATE()
        )
Go


CREATE PROCEDURE Proc_GETPaymentDetailByID_Edit ( @PaymentId AS NUMERIC )
AS
    BEGIN   
    
        SELECT  PaymentTransactionId ,
                PaymentTransactionNo ,
                PaymentTypeId ,
                AccountId ,
                PaymentDate ,
                ChequeDraftNo ,
                ChequeDraftDate ,
                BankId ,
                BankDate ,
                Remarks ,
                TotalAmountReceived ,
                UndistributedAmount ,
                CancellationCharges ,
                CreatedBy ,
                CreatedDate ,
                ModifiedBy ,
                ModifiedDate ,
                DivisionId ,
                StatusId ,
                PM_TYPE
        FROM    dbo.PaymentTransaction
                INNER JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.PaymentTransaction.AccountId
        WHERE   PaymentTransactionId = @PaymentId      
              
              
            
              
    END

Go 

  
CREATE PROCEDURE [dbo].[Proc_PaymentTransactionUpdateDeleteLedgerEntries]    
    (    
      @PaymentTransactionId NUMERIC(18, 2) ,    
      @TransactionTypeId INT      
    )    
AS    
    BEGIN        
              
        DECLARE @ACC_ID NUMERIC(18, 2)  
        DECLARE @Bank_ID NUMERIC(18, 2)      
            
        SELECT  @ACC_ID = AccountId , @Bank_ID = BankId    
        FROM    dbo.PaymentTransaction    
        WHERE   PaymentTransactionId = @PaymentTransactionId            
              
              
        DECLARE @CashOut NUMERIC(18, 2)        
        DECLARE @CashIn NUMERIC(18, 2)       
              
        SET @CashIn = 0      
              
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)    
                         FROM   dbo.LedgerDetail    
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId    
                         WHERE  TransactionId = @PaymentTransactionId    
                                AND TransactionTypeId = @TransactionTypeId    
                                AND AccountId = @ACC_ID    
                       )        
        
        UPDATE  dbo.LedgerMaster    
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn    
        WHERE   AccountId = @ACC_ID      
              
              
              
              
        SET @CashOut = 0      
              
        SET @CashIn = ( SELECT ISNULL(SUM(CashOut), 0)    
                         FROM   dbo.LedgerDetail    
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId    
                         WHERE  TransactionId = @PaymentTransactionId    
                                AND TransactionTypeId = @TransactionTypeId    
                                AND AccountId = @Bank_ID    
                       )   
        
        UPDATE  dbo.LedgerMaster    
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn    
        WHERE   AccountId = @Bank_ID     
           
              
        DELETE  FROM dbo.LedgerDetail    
        WHERE   TransactionId = @PaymentTransactionId    
                AND TransactionTypeId = @TransactionTypeId       
                 
              
    END 

Go

    
CREATE PROCEDURE [dbo].[Proc_PaymentTransactionPayableUpdateDeleteLedgerEntries]      
    (      
      @PaymentTransactionId NUMERIC(18, 2) ,      
      @TransactionTypeId INT        
    )      
AS      
    BEGIN          
                
        DECLARE @ACC_ID NUMERIC(18, 2)    
        DECLARE @Bank_ID NUMERIC(18, 2)        
              
        SELECT  @ACC_ID = AccountId , @Bank_ID = BankId      
        FROM    dbo.PaymentTransaction      
        WHERE   PaymentTransactionId = @PaymentTransactionId              
                
                
        DECLARE @CashOut NUMERIC(18, 2)          
        DECLARE @CashIn NUMERIC(18, 2)         
                
        SET @CashOut = 0        
                
        SET @CashIn = ( SELECT ISNULL(SUM(CashOut), 0)      
                         FROM   dbo.LedgerDetail      
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId      
                         WHERE  TransactionId = @PaymentTransactionId      
                                AND TransactionTypeId = @TransactionTypeId      
                                AND AccountId = @ACC_ID      
                       )          
          
        UPDATE  dbo.LedgerMaster      
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn      
        WHERE   AccountId = @ACC_ID        
                
                
                
                
        SET @CashIn = 0        
                
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)      
                         FROM   dbo.LedgerDetail      
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId      
                         WHERE  TransactionId = @PaymentTransactionId      
                                AND TransactionTypeId = @TransactionTypeId      
                                AND AccountId = @Bank_ID      
                       )     
          
        UPDATE  dbo.LedgerMaster      
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn      
        WHERE   AccountId = @Bank_ID       
             
                
        DELETE  FROM dbo.LedgerDetail      
        WHERE   TransactionId = @PaymentTransactionId      
                AND TransactionTypeId = @TransactionTypeId         
                   
                
    END 

Go


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
      @Proctype NUMERIC = 0 ,
      @TransactionId NUMERIC = NULL ,
      @ProcedureStatus INT OUTPUT         
        
    )
AS
    BEGIN 
    
        IF @Proctype = 1
            BEGIN
                   
                SELECT  @PaymentTransactionId = ISNULL(MAX(PaymentTransactionId),
                                                       0) + 1
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
        
                                SET @Remarks = 'Payment received against reference no.: '
                                    + @ChequeDraftNo          
        
                                EXECUTE Proc_Ledger_Insert @AccountId,
                                    @TotalamountReceived, 0, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy         
                
                                EXECUTE Proc_Ledger_Insert @BankId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy         
        
                            END    
                                  
                        IF @PM_TYPE = 2
                            BEGIN        
                                SET @Remarks = 'Payment released against reference no.: '
                                    + @ChequeDraftNo          
        
                                EXECUTE Proc_Ledger_Insert @AccountId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy          
        
                                EXECUTE Proc_Ledger_Insert @BankId,
                                    @TotalamountReceived, 0, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy         
        
                            END        
                            
                        IF @PM_TYPE = 3
                            BEGIN        
                                SET @Remarks = 'Journal Entry against reference no.: '
                                    + @ChequeDraftNo           
        
                                EXECUTE Proc_Ledger_Insert @AccountId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy          
        
                                EXECUTE Proc_Ledger_Insert @BankId,
                                    @TotalamountReceived, 0, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy         
        
                            END         
                            
                        IF @PM_TYPE = 4
                            BEGIN        
                                SET @Remarks = 'Contra Entry against reference no.: '
                                    + @ChequeDraftNo           
        
                                EXECUTE Proc_Ledger_Insert @AccountId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy          
        
                                EXECUTE Proc_Ledger_Insert @BankId,
                                    @TotalamountReceived, 0, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy         
        
                            END         
                            
                        IF @PM_TYPE = 5
                            BEGIN        
                                SET @Remarks = 'Expense aagainst reference no.: '
                                    + @ChequeDraftNo          
        
                                EXECUTE Proc_Ledger_Insert @AccountId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy          
        
                                EXECUTE Proc_Ledger_Insert @BankId,
                                    @TotalamountReceived, 0, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy         
        
                            END         
        
                    END  
            
            END  
            
        IF @Proctype = 2
            BEGIN    
                
                IF @PM_TYPE = 1
                    BEGIN  
                
                        EXECUTE Proc_PaymentTransactionUpdateDeleteLedgerEntries @PaymentTransactionId,
                            @TransactionId    
                    END   
                      
                IF @PM_TYPE = 2
                    BEGIN  
                
                        EXECUTE Proc_PaymentTransactionPayableUpdateDeleteLedgerEntries @PaymentTransactionId,
                            @TransactionId    
                    END   
                      
                IF @PM_TYPE = 3
                    BEGIN  
                
                        EXECUTE Proc_PaymentTransactionPayableUpdateDeleteLedgerEntries @PaymentTransactionId,
                            @TransactionId    
                    END   
                      
                IF @PM_TYPE = 4
                    BEGIN  
                
                        EXECUTE Proc_PaymentTransactionPayableUpdateDeleteLedgerEntries @PaymentTransactionId,
                            @TransactionId    
                    END   
                      
                IF @PM_TYPE = 5
                    BEGIN  
                
                        EXECUTE Proc_PaymentTransactionPayableUpdateDeleteLedgerEntries @PaymentTransactionId,
                            @TransactionId    
                    END                   
                
                DELETE  FROM dbo.SettlementDetail
                WHERE   PaymentId = @PaymentTransactionId    
                
                UPDATE  PaymentTransaction
                SET     AccountId = @AccountId ,
                        PaymentTypeId = @PaymentTypeId ,
                        BankId = @BankId ,
                        PaymentDate = @PaymentDate ,
                        ChequeDraftNo = @ChequeDraftNo ,
                        ChequeDraftDate = @ChequeDraftDate ,
                        BankDate = BankDate ,
                        Remarks = @Remarks ,
                        TotalAmountReceived = @TotalAmountReceived ,
                        UndistributedAmount = @UndistributedAmount ,
                        ModifiedBy = @CreatedBy ,
                        ModifiedDate = GETDATE() ,
                        StatusId = @StatusId
                WHERE   PaymentTransactionId = @PaymentTransactionId      
                    
                SET @ProcedureStatus = @PaymentTransactionId    
                    
                    
                IF @StatusId = 2
                    BEGIN     
                       
                        IF @PM_TYPE = 1
                            BEGIN            
          
                                SET @Remarks = 'Payment received against reference no.: '
                                    + @ChequeDraftNo            
          
                                EXECUTE Proc_Ledger_Insert @AccountId,
                                    @TotalamountReceived, 0, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy           
                  
                                EXECUTE Proc_Ledger_Insert @BankId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy           
          
                            END      
                              
                        IF @PM_TYPE = 2
                            BEGIN          
                                SET @Remarks = 'Payment released against reference no.: '
                                    + @ChequeDraftNo            
          
                                EXECUTE Proc_Ledger_Insert @AccountId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy            
          
                                EXECUTE Proc_Ledger_Insert @BankId,
                                    @TotalamountReceived, 0, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy           
          
                            END          
                              
                        IF @PM_TYPE = 3
                            BEGIN          
                                SET @Remarks = 'Journal Entry against reference no.: '
                                    + @ChequeDraftNo             
          
                                EXECUTE Proc_Ledger_Insert @AccountId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy            
          
                                EXECUTE Proc_Ledger_Insert @BankId,
                                    @TotalamountReceived, 0, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy           
          
                            END           
                              
                        IF @PM_TYPE = 4
                            BEGIN          
                                SET @Remarks = 'Contra Entry against reference no.: '
                                    + @ChequeDraftNo             
          
                                EXECUTE Proc_Ledger_Insert @AccountId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy            
          
                                EXECUTE Proc_Ledger_Insert @BankId,
                                    @TotalamountReceived, 0, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy           
          
                            END           
                              
                        IF @PM_TYPE = 5
                            BEGIN          
                                SET @Remarks = 'Expense aagainst reference no.: '
                                    + @ChequeDraftNo            
          
                                EXECUTE Proc_Ledger_Insert @AccountId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy            
          
                                EXECUTE Proc_Ledger_Insert @BankId,
                                    @TotalamountReceived, 0, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy           
          
                            END           
          
                    END     
                    
                    
            END       
        
    END 

Go  
            
Alter PROC [dbo].[Proc_UpdatePaymentStauts]  
    (  
      @PaymentTransactionId [numeric](18, 0) ,  
      @CancellationCharges [numeric](18, 2) ,  
      @StatusId NUMERIC ,  
      @PM_TYPE NUMERIC(18, 0) ,  
      @TransactionId NUMERIC = NULL          
    )  
  AS  
    BEGIN                
              
        UPDATE  PaymentTransaction  
        SET     StatusId = @StatusId ,  
                CancellationCharges = @CancellationCharges  
        WHERE   PaymentTransactionId = @PaymentTransactionId                
              
        ----declare parameters              
              
        DECLARE @AccountId NUMERIC              
        DECLARE @TotalamountReceived NUMERIC(18, 2) = 0               
        DECLARE @PaymentTransactionNo VARCHAR(100)               
        DECLARE @CreatedBy VARCHAR(50) = NULL               
        DECLARE @DivisionId NUMERIC = 0               
        DECLARE @BankId NUMERIC = 0              
        DECLARE @ChequeDraftNo VARCHAR(50) = NULL             
        DECLARE @TransactionDate DATETIME              
              
        ----set parametrs              
        SELECT  @AccountId = AccountId ,  
                @TotalamountReceived = TotalAmountReceived ,  
                @PaymentTransactionNo = PaymentTransactionNo ,  
                @CreatedBy = CreatedBy ,  
                @DivisionId = DivisionId ,  
                @BankId = BankId ,  
                @ChequeDraftNo = ChequeDraftNo ,  
                @TransactionDate = PaymentDate  
        FROM    dbo.PaymentTransaction  
        WHERE   PaymentTransactionId = @PaymentTransactionId              
              
        ---update customer ledger              
  ---if payment status approved than make entry in customer ledger              
              
        DECLARE @Remarks VARCHAR(200)               
              
        IF @StatusId = 2  
            BEGIN              
              
                IF @PM_TYPE = 1  
                    BEGIN              
              
                        SET @Remarks = 'Payment received against reference no.: '  
                            + @ChequeDraftNo                
              
                        EXECUTE Proc_Ledger_Insert @AccountId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy               
                      
                        EXECUTE Proc_Ledger_Insert @BankId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy               
              
                    END                 
              
                IF @PM_TYPE = 2  
                    BEGIN              
              
                        SET @Remarks = 'Payment released against reference no.: '  
                            + @ChequeDraftNo                
              
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy               
              
                        EXECUTE Proc_Ledger_Insert @BankId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy               
              
              
                    END              
                              
                IF @PM_TYPE = 3  
                    BEGIN              
                        SET @Remarks = 'Journal Entry against reference no.: '  
                            + @ChequeDraftNo                
              
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy               
              
                        EXECUTE Proc_Ledger_Insert @BankId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy               
              
                    END               
                                  
                IF @PM_TYPE = 4  
                    BEGIN              
                        SET @Remarks = 'Contra Entry against reference no.: '  
                            + @ChequeDraftNo                
              
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy               
              
                        EXECUTE Proc_Ledger_Insert @BankId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy               
              
                    END               
                                  
                IF @PM_TYPE = 5  
                    BEGIN              
                        SET @Remarks = 'Expense Entry against reference no.: '  
                            + @ChequeDraftNo                
              
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy               
              
                        EXECUTE Proc_Ledger_Insert @BankId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy               
              
                    END               
            END                
              
        ---if payment status bounced with cancellation charges than make entry in customer ledger               
        SET @Remarks = 'Payment Cancelation Charges against ' + @ChequeDraftNo              
              
        IF @StatusId = 4  
            AND @CancellationCharges > 0  
            BEGIN             
                       
                IF @PM_TYPE = 1  
                    BEGIN              
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,  
                            @CancellationCharges, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy               
                    END              
              
                IF @PM_TYPE = 2  
                    BEGIN              
              
                        EXECUTE Proc_Ledger_Insert @AccountId,  
                            @CancellationCharges, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy               
                    END               
              
            END           
                      
        IF @StatusId = 3  
            BEGIN         
                    
                IF @PM_TYPE = 1  
                    BEGIN          
              
                        SET @Remarks = 'Amount Deducted against Cancel reference no.: '  
                            + @ChequeDraftNo               
                    
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,  
  @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy              
                              
                        EXECUTE Proc_Ledger_Insert @BankId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy        
                            
                    END         
                            
                IF @PM_TYPE = 2  
                    BEGIN          
              
                        SET @Remarks = 'Amount Deducted against Cancel reference no.: '  
                            + @ChequeDraftNo               
                    
                        EXECUTE Proc_Ledger_Insert @AccountId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy              
                              
                        EXECUTE Proc_Ledger_Insert @BankId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy       
                                                        
                    END     
                      
                IF @PM_TYPE = 3  
                    BEGIN          
              
                        SET @Remarks = 'Amount Deducted against Cancel reference no.: '  
                            + @ChequeDraftNo               
                    
                        EXECUTE Proc_Ledger_Insert @AccountId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy              
                              
                        EXECUTE Proc_Ledger_Insert @BankId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy       
                                                        
                    END  
                      
                IF @PM_TYPE = 4  
                    BEGIN          
              
                        SET @Remarks = 'Amount Deducted against Cancel reference no.: '  
                            + @ChequeDraftNo               
                    
                        EXECUTE Proc_Ledger_Insert @AccountId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy              
                              
                        EXECUTE Proc_Ledger_Insert @BankId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy       
                                                        
                    END  
                      
                IF @PM_TYPE = 5  
                    BEGIN          
              
                        SET @Remarks = 'Amount Deducted against Cancel reference no.: '  
                            + @ChequeDraftNo               
                    
                        EXECUTE Proc_Ledger_Insert @AccountId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy              
                              
                        EXECUTE Proc_Ledger_Insert @BankId, 0,  
                     @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, @TransactionId,  
                            @TransactionDate, @CreatedBy       
                                                        
                    END      
                      
            END            
              
    END

Go

CREATE PROCEDURE Proc_GETOpeningBalanceDetailByID_Edit
    (
      @OpeningBalanceId AS NUMERIC
    )
AS
    BEGIN   
      
        SELECT  OpeningBalanceId ,
                dbo.ACCOUNT_GROUPS.AG_ID AS AG_ID ,
                FkAccountId ,
                OpeningAmount ,
                OpeningDate ,
                [TYPE]
        FROM    dbo.OpeningBalance
                INNER JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.OpeningBalance.FkAccountId
                INNER JOIN dbo.ACCOUNT_Groups ON dbo.ACCOUNT_MASTER.AG_ID = dbo.ACCOUNT_GROUPS.AG_ID
        WHERE   OpeningBalanceId = @OpeningBalanceId   
                
    END

Go

  
Create PROCEDURE [dbo].[Proc_OpeningBalDebitUpdateDeleteLedgerEntries]      
    (      
      @OpeningBalanceId NUMERIC(18, 2) ,      
      @TransactionTypeId INT        
    )      
AS      
    BEGIN          
                
        DECLARE @ACC_ID NUMERIC(18, 2)        
              
        SELECT  @ACC_ID = FkAccountId    
        FROM    dbo.OpeningBalance      
        WHERE   OpeningBalanceId = @OpeningBalanceId              
                
                
        DECLARE @CashOut NUMERIC(18, 2)          
        DECLARE @CashIn NUMERIC(18, 2)         
                
        SET @CashOut = 0        
                
        SET @CashIn = ( SELECT ISNULL(SUM(CashOut), 0)      
                         FROM   dbo.LedgerDetail      
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId      
                         WHERE  TransactionId = @OpeningBalanceId      
                                AND TransactionTypeId = @TransactionTypeId      
                                AND AccountId = @ACC_ID      
                       )          
          
        UPDATE  dbo.LedgerMaster      
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn      
        WHERE   AccountId = @ACC_ID   
            
             
                
        DELETE  FROM dbo.LedgerDetail      
        WHERE   TransactionId = @OpeningBalanceId      
                AND TransactionTypeId = @TransactionTypeId         
                   
                
    END 

Go

  
CREATE PROCEDURE [dbo].[Proc_OpeningBalCreditUpdateDeleteLedgerEntries]      
    (      
      @OpeningBalanceId NUMERIC(18, 2) ,      
      @TransactionTypeId INT        
    )      
AS      
    BEGIN          
                
        DECLARE @ACC_ID NUMERIC(18, 2)        
              
        SELECT  @ACC_ID = FkAccountId    
        FROM    dbo.OpeningBalance      
        WHERE   OpeningBalanceId = @OpeningBalanceId              
                
                
        DECLARE @CashOut NUMERIC(18, 2)          
        DECLARE @CashIn NUMERIC(18, 2)         
                
        SET @CashIn = 0        
                
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)      
                         FROM   dbo.LedgerDetail      
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId      
                         WHERE  TransactionId = @OpeningBalanceId      
                                AND TransactionTypeId = @TransactionTypeId      
                                AND AccountId = @ACC_ID      
                       )          
          
        UPDATE  dbo.LedgerMaster      
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn      
        WHERE   AccountId = @ACC_ID   
                
        DELETE  FROM dbo.LedgerDetail      
        WHERE   TransactionId = @OpeningBalanceId      
                AND TransactionTypeId = @TransactionTypeId    
                
    END 

Go
    
Alter PROC [dbo].[Proc_AddOpeningBalance]
    (
      @OpeningBalanceId NUMERIC = NULL ,
      @AccountId NUMERIC(18, 0) ,
      @Amount NUMERIC(18, 2) ,
      @DivisionId NUMERIC(18, 0) ,
      @CreatedBy NVARCHAR(50) ,
      @Openingdate DATETIME = NULL ,
      @Type BIGINT ,
      @Proctype NUMERIC = 0 ,
      @TransactionId NUMERIC = NULL  
    )
AS
    BEGIN     
      
        DECLARE @Remarks VARCHAR(200)   
      
        IF @Proctype = 1
            BEGIN    
      
                SELECT TOP 1
                        @Openingdate = financialyear_dt
                FROM    Company_Master    
                   
    
                SET @OpeningBalanceId = -( SELECT   ISNULL(COUNT(OPENINGBALANCEID),
                                                           0) + 1 AS Id
                                           FROM     OPENINGBALANCE
                                         )    
                                            
                INSERT  OPENINGBALANCE
                VALUES  ( @OpeningBalanceId, @AccountId, @Amount, @Openingdate,
                          @type )      
    
                SET @Remarks = 'Account Opening Balance as on Date: '
                    + CAST(CONVERT(VARCHAR(20), @Openingdate, 106) AS VARCHAR(30))      
    
                IF @TYPE = 1
                    BEGIN   
                      
                        UPDATE  dbo.ACCOUNT_MASTER
                        SET     OPENING_BAL = @Amount ,
                                OPENING_BAL_TYPE = 'Dr'
                        WHERE   ACC_ID = @AccountId      
    
                        EXECUTE Proc_Ledger_InsertOpening @AccountId, 0,
                            @Amount, @Remarks, @DivisionId, @OpeningBalanceId,
                            @TransactionId, @Openingdate, @CreatedBy     
                    END      
    
                IF @TYPE = 2
                    BEGIN   
                      
                        UPDATE  dbo.ACCOUNT_MASTER
                        SET     OPENING_BAL = @Amount ,
                                OPENING_BAL_TYPE = 'Cr'
                        WHERE   ACC_ID = @AccountId   
                             
                        EXECUTE Proc_Ledger_InsertOpening @AccountId, @Amount,
                            0, @Remarks, @DivisionId, @OpeningBalanceId,
                            @TransactionId, @Openingdate, @CreatedBy      
                    END    
              
            END    
              
        IF @Proctype = 2
            BEGIN    
      
                SELECT TOP 1
                        @Openingdate = financialyear_dt
                FROM    Company_Master   
                  
                DECLARE @typeopbal NUMERIC(18, 0)  
                  
                SELECT  @typeopbal = [type]
                FROM    dbo.OpeningBalance
                WHERE   OpeningBalanceId = @OpeningBalanceId  
                  
                IF ( @typeopbal = 1 )
                    BEGIN  
                        EXECUTE Proc_OpeningBalDebitUpdateDeleteLedgerEntries @OpeningBalanceId,
                            @TransactionId  
                    END  
                IF ( @typeopbal = 2 )
                    BEGIN  
                        EXECUTE Proc_OpeningBalCreditUpdateDeleteLedgerEntries @OpeningBalanceId,
                            @TransactionId                      
                    END                  
                            
                UPDATE  OPENINGBALANCE
                SET     OpeningAmount = @Amount ,
                        Type = @Type ,
                        FkAccountId = @AccountId
                WHERE   OpeningBalanceId = @OpeningBalanceId  
    
                SET @Remarks = 'Account Opening Balance as on Date: '
                    + CAST(CONVERT(VARCHAR(20), @Openingdate, 106) AS VARCHAR(30))      
    
                IF @TYPE = 1
                    BEGIN   
                      
                        UPDATE  dbo.ACCOUNT_MASTER
                        SET     OPENING_BAL = @Amount ,
                                OPENING_BAL_TYPE = 'Dr'
                        WHERE   ACC_ID = @AccountId      
    
                        EXECUTE Proc_Ledger_InsertOpening @AccountId, 0,
                            @Amount, @Remarks, @DivisionId, @OpeningBalanceId,
                            @TransactionId, @Openingdate, @CreatedBy     
                    END      
    
                IF @TYPE = 2
                    BEGIN   
                      
                        UPDATE  dbo.ACCOUNT_MASTER
                        SET     OPENING_BAL = @Amount ,
                                OPENING_BAL_TYPE = 'Cr'
                        WHERE   ACC_ID = @AccountId   
                             
                        EXECUTE Proc_Ledger_InsertOpening @AccountId, @Amount,
                            0, @Remarks, @DivisionId, @OpeningBalanceId,
                            @TransactionId, @Openingdate, @CreatedBy      
                    END    
              
            END    
    END 

Go


ALTER VIEW [dbo].[VW_PurchaseReturnRegister]
AS
    SELECT  dbo.fn_Format(DN.DebitNote_Date) AS [ReturnDate] ,
            DN.DebitNote_Code + CAST(DN.DebitNote_No AS VARCHAR(20)) AS DebitNoteNumber ,
            ISNULL(AM.ACC_NAME, 'Purchase Return') AS AccName ,
            CASE WHEN MRWPM.MRN_TYPE = 1 THEN 'IGST'            
                 WHEN MRWPM.MRN_TYPE = 2 THEN 'SGST/CGST'
                 WHEN MRWPM.MRN_TYPE = 3 THEN 'UTGST/CGST'
                 END AS [Type] , 
            SUM(DN.DN_Amount) AS TotalAmount ,
            'Purchase Return' AS TransType
    FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER AS MRWPM
            INNER JOIN dbo.DebitNote_Master AS DN ON MRWPM.MRN_NO = DN.MRNId
            INNER JOIN dbo.ACCOUNT_MASTER AS AM ON MRWPM.Vendor_ID = AM.ACC_ID
    GROUP BY MRWPM.MRN_NO ,
            dbo.fn_Format(DN.DebitNote_Date) ,
            DN.DebitNote_Code ,
            DN.DebitNote_No,
            AM.ACC_NAME,
            MRWPM.MRN_TYPE
        
	UNION ALL
	
	SELECT  dbo.fn_Format(DN.DebitNote_Date) AS [ReturnDate] ,
            DN.DebitNote_Code + CAST(DN.DebitNote_No AS VARCHAR(20)) AS DebitNoteNumber ,
            ISNULL(AM.ACC_NAME, 'Purchase Return') AS AccName ,
            CASE WHEN MRAPM.MRN_TYPE = 1 THEN 'IGST'            
                 WHEN MRAPM.MRN_TYPE = 2 THEN 'SGST/CGST'
                 WHEN MRAPM.MRN_TYPE = 3 THEN 'UTGST/CGST'
                 END AS [Type] , 
            SUM(DN.DN_Amount) AS TotalAmount ,
            'Purchase Return' AS TransType
    FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER AS MRAPM
            INNER JOIN dbo.DebitNote_Master AS DN ON MRAPM.MRN_NO = DN.MRNId
            INNER JOIN dbo.ACCOUNT_MASTER AS AM ON MRAPM.CUST_ID = AM.ACC_ID
    GROUP BY MRAPM.MRN_NO ,
            dbo.fn_Format(DN.DebitNote_Date) ,
            DN.DebitNote_Code ,
            DN.DebitNote_No,
            AM.ACC_NAME,
            MRAPM.MRN_TYPE

GO


Alter PROCEDURE Proc_GETDebitNoteDetailsByID_Edit 
    (
      @DebitNoteId NUMERIC(18,0)
    )
AS
    BEGIN
        SELECT  DN.DebitNote_Code + CAST(DN.DebitNote_No AS VARCHAR(20)) AS DebitNoteNumber ,
                dbo.fn_Format(DN.DebitNote_Date) AS DebitNote_Date ,
                DN_CustId ,
                MRWPM.MRN_NO AS MRNo,
                MRWPM.MRN_PREFIX + CAST(MRWPM.MRN_NO AS VARCHAR(20)) AS MRNNumber,
                INV_No AS InvoiceNo,
                dbo.fn_Format(DN.INV_Date) AS InvoiceDate,
                DN.Remarks AS Remarks
        FROM    dbo.DebitNote_Master DN
                INNER JOIN dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER AS MRWPM ON MRWPM.MRN_NO = DN.MRNId
        WHERE   DebitNote_Id = @DebitNoteId
        
        UNION ALL
        
        SELECT  DN.DebitNote_Code + CAST(DN.DebitNote_No AS VARCHAR(20)) AS DebitNoteNumber ,
                dbo.fn_Format(DN.DebitNote_Date) AS DebitNote_Date ,
                DN_CustId ,
                MRAPM.MRN_NO AS MRNo,
                MRAPM.MRN_PREFIX + CAST(MRAPM.MRN_NO AS VARCHAR(20)) AS MRNNumber,
                INV_No AS InvoiceNo,
                dbo.fn_Format(DN.INV_Date) AS InvoiceDate,
                DN.Remarks AS Remarks
        FROM    dbo.DebitNote_Master DN
                INNER JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER AS MRAPM ON MRAPM.MRN_NO = DN.MRNId
        WHERE   DebitNote_Id = @DebitNoteId
        
    END

Go

CREATE PROCEDURE [dbo].[GetDebitNoteDetails] 
@DebitNoteId NUMERIC(18, 0)  
AS  
    BEGIN         
        SELECT  IM.ITEM_ID AS Item_ID ,  
                IM.ITEM_CODE AS Item_Code ,  
                IM.ITEM_NAME AS Item_Name ,  
                IM.UM_Name AS UM_Name ,  
                SD.Balance_Qty AS Prev_Item_Qty ,  
                md.Item_Qty AS MRN_Qty ,  
                dd.Item_Rate ,  
                dd.Item_Tax AS Vat_Per ,  
                dd.Item_Cess AS Cess_Per ,  
                dd.Item_Qty AS Item_Qty ,  
                sd.Stock_Detail_Id AS Stock_Detail_Id  
        FROM    dbo.DebitNote_Master DN 
				INNER JOIN DebitNote_DETAIL DD ON DD.DebitNote_Id = dn.DebitNote_Id
				left JOIN MATERIAL_RECIEVED_WITHOUT_PO_MASTER MM  ON mm.MRN_NO = dn.MRNId
                INNER JOIN dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MD ON mm.Received_ID = md.Received_ID  AND md.Item_ID = dd.Item_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON DD.Item_ID = IM.ITEM_ID  
                INNER JOIN STOCK_DETAIL SD ON DD.Stock_Detail_Id = SD.STOCK_DETAIL_ID AND dd.Item_ID = sd.Item_id
        WHERE   dn.DebitNote_Id = @DebitNoteId 
        UNION ALL 
        SELECT  IM.ITEM_ID AS Item_ID ,  
                IM.ITEM_CODE AS Item_Code ,  
                IM.ITEM_NAME AS Item_Name ,  
                IM.UM_Name AS UM_Name ,  
                SD.Balance_Qty AS Prev_Item_Qty ,  
                md.Item_Qty AS MRN_Qty ,  
                dd.Item_Rate ,  
                dd.Item_Tax AS Vat_Per ,  
                dd.Item_Cess AS Cess_Per ,  
                dd.Item_Qty AS Item_Qty ,  
                sd.Stock_Detail_Id AS Stock_Detail_Id  
        FROM    dbo.DebitNote_Master DN 
				INNER JOIN DebitNote_DETAIL DD ON DD.DebitNote_Id = dn.DebitNote_Id
				left JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER MM ON mm.MRN_NO = dn.MRNId
                INNER JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL MD ON mm.Receipt_ID = md.Receipt_ID AND md.Item_ID = dd.Item_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON DD.Item_ID = IM.ITEM_ID  
                INNER JOIN STOCK_DETAIL SD ON DD.Stock_Detail_Id = SD.STOCK_DETAIL_ID AND dd.Item_ID = sd.Item_id
        WHERE   dn.DebitNote_Id = @DebitNoteId
    
    END

Go

Alter PROCEDURE [dbo].[PROC_DebitNote_DETAIL]
    (
      @v_DebitNote_ID NUMERIC(18, 0) ,
      @v_Item_ID NUMERIC(18, 0) ,
      @v_Item_Qty NUMERIC(18, 4) ,
      @v_Created_By VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Division_Id INT ,
      @v_Stock_Detail_Id NUMERIC(18, 0) ,
      @v_Item_Rate NUMERIC(18, 2) ,
      @v_Item_Tax NUMERIC(18, 2) ,
      @v_Item_Cess NUMERIC(18, 2) ,
      @V_Trans_Type NUMERIC(18, 0) ,
      @v_Proc_Type INT          
    )
AS
    BEGIN          
        IF @V_PROC_TYPE = 1
            BEGIN          
                INSERT  INTO DebitNote_DETAIL
                        ( DebitNote_Id ,
                          Item_ID ,
                          Item_Qty ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_Id ,
                          Stock_Detail_Id ,
                          Item_Rate ,
                          Item_Tax ,
                          Item_Cess       
      
                        )
                VALUES  ( @V_DebitNote_ID ,
                          @V_Item_ID ,
                          @V_Item_Qty ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_Id ,
                          @v_Stock_Detail_Id ,
                          @v_Item_Rate ,
                          @v_Item_Tax ,
                          @v_Item_Cess        
                        )          
                DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)      
                --it will insert entry in stock detail table and       
--                --return stock_detail_id.              
--                EXEC INSERT_STOCK_DETAIL @v_Item_ID, @v_Batch_no,      
--                    @v_Expiry_Date, @v_Item_Qty, @v_Received_ID, @V_Trans_Type,      
--                    @STOCK_DETAIL_ID OUTPUT      
--                            
                EXEC UPDATE_STOCK_DETAIL_ISSUE @STOCK_DETAIL_ID = @v_Stock_Detail_Id, --  numeric(18, 0)      
                    @ISSUE_QTY = @v_Item_Qty --  numeric(18, 4)      
                --it will insert entry in transaction log with stock_detail_id      
                EXEC INSERT_TRANSACTION_LOG @v_DebitNote_ID, @v_Item_ID,
                    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,
                    @v_Creation_Date, 0           
            END   
              
        IF @V_PROC_TYPE = 2
            BEGIN  
              
                DELETE  FROM DebitNote_DETAIL
                WHERE   DebitNote_Id = @v_DebitNote_ID AND  Item_ID = @V_Item_ID
                      
                INSERT  INTO DebitNote_DETAIL
                        ( DebitNote_Id ,
                          Item_ID ,
                          Item_Qty ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_Id ,
                          Stock_Detail_Id ,
                          Item_Rate ,
                          Item_Tax ,
                          Item_Cess     
                        )
                VALUES  ( @V_DebitNote_ID ,
                          @V_Item_ID ,
                          @V_Item_Qty ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_Id ,
                          @v_Stock_Detail_Id ,
                          @v_Item_Rate ,
                          @v_Item_Tax ,
                          @v_Item_Cess        
                        )          
  
                           
                EXEC UPDATE_STOCK_DETAIL_ISSUE @STOCK_DETAIL_ID = @v_Stock_Detail_Id, --  numeric(18, 0)      
                    @ISSUE_QTY = @v_Item_Qty --  numeric(18, 4)     
                      
                      
                DELETE  FROM dbo.Transaction_Log
                WHERE   Transaction_Type = @V_Trans_Type
                        AND Transaction_ID = @v_DebitNote_ID   
                  
                --it will insert entry in transaction log with stock_detail_id                      
                EXEC INSERT_TRANSACTION_LOG @v_DebitNote_ID, @v_Item_ID,
                    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,
                    @v_Creation_Date, 0           
            END          
    END 

Go

Create PROCEDURE [dbo].[GET_DebitNoteCodeByID]  
    @DebitNoteId NUMERIC(18, 0)  
AS  
    BEGIN           
        SELECT  DebitNote_Code ,  
                DebitNote_No  
        FROM    dbo.DebitNote_Master DN  
        WHERE   dn.DebitNote_Id = @DebitNoteId   
          
      
    END 

Go


Create PROCEDURE [dbo].[Proc_DebitNoteUpdateDeleteLedgerEntries]
    (
      @DebitNoteId VARCHAR(50) ,
      @TransactionTypeId INT      
    )
AS
    BEGIN        
              
        DECLARE @ACC_ID NUMERIC(18, 2) 
        DECLARE @MRNId NUMERIC(18, 2) 
        
        SELECT  @ACC_ID = DN_CustId ,
                @MRNId = MRN_NO
        FROM    ( SELECT    DN_CustId ,
                            mrwpm.MRN_NO --CASE WHEN ISNULL(mrwpm.MRN_NO,0) = 0 THEN mrapm.MRN_NO end
                  FROM      dbo.DebitNote_master dn
                            INNER JOIN dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER mrwpm ON dn.MRNId = mrwpm.MRN_NO
                  WHERE     dn.DebitNote_Id = @DebitNoteId
                  UNION ALL
                  SELECT    DN_CustId ,
                            mrapm.MRN_NO --CASE WHEN ISNULL(mrwpm.MRN_NO,0) = 0 THEN mrapm.MRN_NO end
                  FROM      dbo.DebitNote_master dn
                            INNER JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER mrapm ON dn.MRNId = mrapm.MRN_NO
                  WHERE     dn.DebitNote_Id = @DebitNoteId
                ) tb
        
        
              
              
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
        SET @V_MRN_TYPE = ( SELECT  MRN_TYPE
                            FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                            WHERE   MRN_NO = @MRNId
                            UNION
                            SELECT  MRN_TYPE
                            FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
                            WHERE   MRN_NO = @MRNId
                          )          
              
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
  
Alter PROCEDURE [dbo].[PROC_DebitNote_MASTER]  
    (  
      @v_DebitNote_ID NUMERIC(18, 0) ,  
      @v_DebitNote_No NUMERIC(18, 0) ,  
      @v_DebitNote_Code VARCHAR(20) ,  
      @v_DebitNote_Date DATETIME ,  
      @v_Remarks VARCHAR(500) ,  
      @v_MRN_Id NUMERIC(18, 0) ,  
      @v_Created_BY VARCHAR(100) ,  
      @v_Creation_Date DATETIME ,  
      @v_Modified_By VARCHAR(100) ,  
      @v_Modification_Date DATETIME ,  
      @v_Division_ID NUMERIC(18, 0) ,  
      @v_DN_Amount NUMERIC(18, 2) ,  
      @v_DN_ItemValue NUMERIC(18, 2) = 0 ,  
      @v_DN_ItemTax NUMERIC(18, 2) = 0 ,  
      @v_DN_ItemCess NUMERIC(18, 2) = 0 ,  
      @v_DN_CustId NUMERIC(18, 0) ,  
      @v_INV_No VARCHAR(100) ,  
      @v_INV_Date DATETIME ,  
      @v_DebitNote_Type VARCHAR(50) = NULL ,  
      @v_RefNo VARCHAR(50) = NULL ,  
      @v_RefDate_dt DATETIME ,  
      @V_Trans_Type NUMERIC(18, 0) ,  
      @v_Proc_Type INT          
    )  
AS  
    BEGIN     
           
        DECLARE @Remarks VARCHAR(250)          
        DECLARE @InputID NUMERIC          
        DECLARE @CInputID NUMERIC= 0          
        SET @CInputID = 10016          
        DECLARE @CGST_Amount NUMERIC(18, 2)  
        DECLARE @V_MRN_TYPE NUMERIC = 0
        DECLARE @CessInputID NUMERIC = 0          
        SET @CessInputID = 10013    
          
          
        IF @V_PROC_TYPE = 1  
            BEGIN          
          
                INSERT  INTO DebitNote_MASTER  
                        ( DebitNote_ID ,  
                          DebitNote_No ,  
                          DebitNote_Code ,  
                          DebitNote_Date ,  
                          Remarks ,  
                          MRNId ,  
                          Created_BY ,  
                          Creation_Date ,  
                          Modified_By ,  
                          Modification_Date ,  
                          Division_ID ,  
                          DN_Amount ,  
                          DN_CustId ,  
                          INV_No ,  
                          INV_Date ,  
                          DebitNote_Type ,  
                          RefNo ,  
                          RefDate_dt ,  
                          Tax_num ,  
                          Cess_num          
          
                        )  
                VALUES  ( @V_DebitNote_ID ,  
                          @V_DebitNote_No ,  
                          @V_DebitNote_Code ,  
                          @v_DebitNote_Date ,  
                          @V_Remarks ,  
                          @V_MRN_Id ,  
                          @V_Created_BY ,  
                          @V_Creation_Date ,  
                          @V_Modified_By ,  
                          @V_Modification_Date ,  
                          @V_Division_ID ,  
                          @v_DN_Amount ,  
                          @v_DN_CustId ,  
                          @v_INV_No ,  
                          @v_INV_Date ,  
                          @v_DebitNote_Type ,  
                          @v_RefNo ,  
                          @v_RefDate_dt ,  
                          @v_DN_ItemTax ,  
                          @v_DN_ItemCess     
                        )          
          
                UPDATE  DN_SERIES  
                SET     CURRENT_USED = @V_DebitNote_No  
                WHERE   DIV_ID = @V_Division_ID          
          
				SET @CGST_Amount = ( @v_DN_ItemTax / 2 )          
                         
                SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1  
                                                     THEN 10023  
                                                     WHEN MRN_TYPE = 2  
                                                     THEN 10020  
                                                     WHEN MRN_TYPE = 3  
                                                     THEN 10074  
                                                END AS inputid  
												FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER  
												WHERE   MRN_NO = @V_MRN_Id  
                                      ), 0)          
                IF @InputID = 0  
                    BEGIN          
                        SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1  
                                                             THEN 10023  
                                                             WHEN MRN_TYPE = 2  
                                                             THEN 10020  
                                                             WHEN MRN_TYPE = 3  
                                                             THEN 10074  
                                                        END AS inputid  
                                                FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER  
                                                WHERE   MRN_NO = @V_MRN_Id  
                                              ), 0)          
                    END          
          
          
                        
          
                SET @V_MRN_TYPE = ( SELECT  MRN_TYPE  
                                    FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER  
                                    WHERE   MRN_NO = @V_MRN_Id  
                                    UNION  
                                    SELECT  MRN_TYPE  
                                    FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER  
                                    WHERE   MRN_NO = @V_MRN_Id  
                                  )          
          
                SET @Remarks = 'Debit  against DebitNote No-. -'  
                    + @V_DebitNote_Code + ' - '  
                    + CAST(@V_DebitNote_No AS VARCHAR(50))          
          
          
                EXECUTE Proc_Ledger_Insert @v_DN_CustId, 0, @v_DN_Amount,  
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,  
                    @v_DebitNote_Date, @V_Created_BY          
          
          
                EXECUTE Proc_Ledger_Insert 10070, @v_DN_ItemValue, 0, @Remarks,  
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,  
                    @v_DebitNote_Date, @V_Created_BY           
          
          
          
                SET @Remarks = 'GST against DebitNote No- '  
                    + @V_DebitNote_Code + ' - '  
                    + CAST(@V_DebitNote_No AS VARCHAR(50))          
          
                IF @V_MRN_TYPE <> 2  
                    BEGIN    
          
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,  
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,  
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY           
          
          
                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,  
                            @Remarks, @v_Division_ID, @V_DebitNote_ID,  
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY           
                    END          
          
          
          
                ELSE  
                    BEGIN          
                        EXECUTE Proc_Ledger_Insert @InputID, @v_DN_ItemTax, 0,  
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,  
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY           
                    END             
        
                SET @Remarks = 'Cess against DebitNote No- '  
                    + @V_DebitNote_Code + ' - '  
                    + CAST(@V_DebitNote_No AS VARCHAR(50))    
                        
                EXECUTE Proc_Ledger_Insert @CessInputID, @v_DN_ItemCess, 0, @Remarks,  
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,  
                    @v_DebitNote_Date, @V_Created_BY        
          
                SET @Remarks = 'Stock In against Debit Note No- '  
                    + @V_DebitNote_Code + ' - '  
                    + CAST(@V_DebitNote_No AS VARCHAR(50)) 
          
          
                EXECUTE Proc_Ledger_Insert 10073, @v_DN_Amount, 0, @Remarks,  
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,  
                    @v_DebitNote_Date, @V_Created_BY            
          
            END          
          
        IF @V_PROC_TYPE = 2  
            BEGIN    
              
              
				EXECUTE [Proc_DebitNoteUpdateDeleteLedgerEntries] @V_DebitNote_ID, @V_Trans_Type  
                    
          
                UPDATE  DebitNote_MASTER  
                SET     DebitNote_No = @V_DebitNote_No ,  
                        DebitNote_Code = @V_DebitNote_Code ,  
                        DebitNote_Date = @v_DebitNote_Date ,  
                        Remarks = @V_Remarks ,  
                        MRNId = @V_MRN_Id ,  
                        Modified_By = @V_Modified_By ,  
                        Modification_Date = @V_Modification_Date ,  
                        DN_Amount = @v_DN_Amount ,  
                        DN_CustId = @v_DN_CustId ,  
                        Division_ID = @V_Division_ID ,  
                        INV_No = @v_INV_No ,  
                        INV_Date = @v_INV_Date ,  
                        DebitNote_Type = @v_DebitNote_Type ,  
                        RefNo = @v_RefNo ,  
                        RefDate_dt = @v_RefDate_dt ,  
                        Tax_num = @v_DN_ItemTax ,  
                        Cess_num = @v_DN_ItemCess  
                WHERE   DebitNote_ID = @V_DebitNote_ID     
                  
                  
                         
                SET @CGST_Amount = ( @v_DN_ItemTax / 2 )          
                         
                SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1  
                                                     THEN 10023  
                                                     WHEN MRN_TYPE = 2  
                                                     THEN 10020  
                                                     WHEN MRN_TYPE = 3  
                                                     THEN 10074  
                                                END AS inputid  
                                        FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER  
                                        WHERE   MRN_NO = @V_MRN_Id  
                                      ), 0)    
									        
                IF @InputID = 0  
                    BEGIN          
                        SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1  
                                                             THEN 10023  
                                                             WHEN MRN_TYPE = 2  
                                                             THEN 10020  
                                                             WHEN MRN_TYPE = 3  
                                                             THEN 10074  
                                                        END AS inputid  
                                                FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER  
                                                WHERE   MRN_NO = @V_MRN_Id  
                                              ), 0)          
                    END    
                        
          
                SET @V_MRN_TYPE = ( SELECT  MRN_TYPE  
                                    FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER  
                                    WHERE   MRN_NO = @V_MRN_Id  
                                    UNION  
                                    SELECT  MRN_TYPE  
                                    FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER  
                                    WHERE   MRN_NO = @V_MRN_Id  
                                  )          
          
                SET @Remarks = 'Debit  against DebitNote No-. -'  
                    + @V_DebitNote_Code + ' - '  
                    + CAST(@V_DebitNote_No AS VARCHAR(50))   
          
                EXECUTE Proc_Ledger_Insert @v_DN_CustId, 0, @v_DN_Amount,  
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,  
                    @v_DebitNote_Date, @V_Created_BY  
          
                EXECUTE Proc_Ledger_Insert 10070, @v_DN_ItemValue, 0, @Remarks,  
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,  
                    @v_DebitNote_Date, @V_Created_BY            
          
                SET @Remarks = 'GST against DebitNote No- '  
                    + @V_DebitNote_Code + ' - '  
                    + CAST(@V_DebitNote_No AS VARCHAR(50))          
          
                IF @V_MRN_TYPE <> 2  
                    BEGIN    
          
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,  
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,  
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY           
          
          
                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,  
                            @Remarks, @v_Division_ID, @V_DebitNote_ID,  
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY           
                    END          
          
          
          
                ELSE  
                    BEGIN          
                        EXECUTE Proc_Ledger_Insert @InputID, @v_DN_ItemTax, 0,  
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,  
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY           
                    END             
        
                SET @Remarks = 'Cess against DebitNote No- '  
                    + @V_DebitNote_Code + ' - '  
                    + CAST(@V_DebitNote_No AS VARCHAR(50))    
                        
                EXECUTE Proc_Ledger_Insert @CessInputID, @v_DN_ItemCess, 0, @Remarks,  
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,  
                    @v_DebitNote_Date, @V_Created_BY        
          
                SET @Remarks = 'Stock In against Debit Note No- '  
                    + @V_DebitNote_Code + ' - '  
                    + CAST(@V_DebitNote_No AS VARCHAR(50))  
          
                EXECUTE Proc_Ledger_Insert 10073, @v_DN_Amount, 0, @Remarks,  
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,  
                    @v_DebitNote_Date, @V_Created_BY                   
                       
          
            END          
    END     

Go
  
CREATE PROCEDURE Proc_GETCreditNoteDetailsByID_Edit   
    (  
      @CreditNoteId NUMERIC(18,0)  
    )  
AS  
    BEGIN  
        SELECT  CN.CreditNote_Code + CAST(CN.CreditNote_No AS VARCHAR(20)) AS CreditNoteNumber ,  
                dbo.fn_Format(CN.CreditNote_Date) AS CreditNote_Date ,  
                CN_CustId ,  
                SIM.SI_ID AS Invid,
                INV_No AS InvoiceNo,  
                dbo.fn_Format(CN.INV_Date) AS InvoiceDate,  
                CN.Remarks AS Remarks  
        FROM    dbo.CreditNote_Master CN  
                INNER JOIN dbo.SALE_INVOICE_MASTER SIM ON SIM.SI_ID = CN.INVId 
        WHERE   CreditNote_Id = 1  
          
    END

Go

Create PROCEDURE [dbo].[GetCreditNoteDetails] 
    @CreditNoteId NUMERIC(18, 0)
AS
    BEGIN           
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                SD.Balance_Qty AS Prev_Item_Qty ,
                saleID.Item_Qty AS INV_Qty ,
                cd.Item_Rate ,
                cd.Item_Tax AS Vat_Per ,
                cd.Item_Cess AS Cess_Per ,
                cd.Item_Qty AS Item_Qty ,
                SIID.Stock_Detail_Id AS Stock_Detail_Id ,
                CONVERT(VARCHAR(20), SIM.CREATION_DATE, 106) AS INvDate ,  
                SI_CODE + CAST(SI_NO AS VARCHAR(20)) AS SiNo 
        FROM    dbo.CreditNote_Master CN
                INNER JOIN dbo.CreditNote_DETAIL CD ON CD.CreditNote_Id = CN.CreditNote_Id
                LEFT JOIN dbo.SALE_INVOICE_MASTER SIM ON sim.SI_ID = CN.INVId
                INNER JOIN SALE_INVOICE_DETAIL SaleID ON sim.SI_ID = SaleID.SI_ID
                                                              AND SaleID.Item_ID = cd.Item_ID
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL SIID ON SIID.ITEM_ID = SaleID.ITEM_ID
                                                           AND SIID.SI_ID = SaleID.SI_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON SaleID.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON SIID.Stock_Detail_Id = SD.STOCK_DETAIL_ID
                
        WHERE   cn.CreditNote_Id = @CreditNoteId     
    END  

Go

CREATE PROCEDURE [dbo].[GET_CreditNoteCodeByID]
    @CreditNoteId NUMERIC(18, 0)
AS
    BEGIN           
        SELECT  CreditNote_Code ,
                CreditNote_No
        FROM    dbo.CreditNote_Master DN
        WHERE   dn.CreditNote_Id = @CreditNoteId   
          
      
    END 

Go
  
Alter PROCEDURE [dbo].[Proc_CreditNoteUpdateDeleteLedgerEntries]
    (
      @CreditNoteId VARCHAR(50) ,
      @TransactionTypeId INT        
    )
AS
    BEGIN  
    
        SELECT  *
        FROM    SALE_INVOICE_MASTER   
                
        DECLARE @ACC_ID NUMERIC(18, 2)   
        DECLARE @InvId NUMERIC(18, 2)   
          
        SELECT  @ACC_ID = CN_CustId ,
                @InvId = sim.SI_ID
        FROM    dbo.CreditNote_master cn
                INNER JOIN dbo.SALE_INVOICE_MASTER sim ON cn.INVId = sim.SI_ID
        WHERE   cn.CreditNote_Id = @CreditNoteId          
                
                
        DECLARE @CashOut NUMERIC(18,2)          
        DECLARE @CashIn NUMERIC(18,2)         
                
        SET @CashIn = 0        
                
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
                
        SET @OutputID = ( SELECT    CASE WHEN INV_TYPE = 'I' THEN 10021
                                         WHEN INV_TYPE = 'S' THEN 10024
                                         WHEN INV_TYPE = 'U' THEN 10075
                                    END AS inputid
                          FROM      dbo.SALE_INVOICE_MASTER
                          WHERE     SI_ID = @InvId
                        ) 
                  
        SET @v_INV_TYPE = ( SELECT  INV_TYPE
                            FROM    dbo.SALE_INVOICE_MASTER
                            WHERE   SI_ID = @InvId
                          )    
                               
                               
        IF @v_INV_TYPE <> 'I'
            BEGIN            
                SET @CashIn = ( SELECT ISNULL(SUM(CashOut), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @CreditNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @COutputID
                               )            
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @COutputID            
            
            
                SET @CashIn = ( SELECT ISNULL(SUM(CashOut), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @CreditNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @OutputID
                               )            
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @OutputID            
            END        
        ELSE
            BEGIN            
            
                SET @CashIn = ( SELECT ISNULL(SUM(CashOut), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @CreditNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @OutputID
                               )            
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @OutputID            
            END       
                  
                  
        ------------------Start Cess Entries Deletion ---------------      
                       
        SET @CashIn = ( SELECT ISNULL(SUM(CashOut), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @CreditNoteId
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

Alter PROCEDURE [dbo].[PROC_CreditNote_MASTER]
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
      @v_CreditNote_Type VARCHAR(50) = NULL ,
      @v_RefNo VARCHAR(50) = NULL ,
      @v_RefDate_dt DATETIME ,
      @V_Trans_Type NUMERIC(18, 0) ,
      @v_Proc_Type INT        
    )
AS
    BEGIN  
    
        DECLARE @Remarks VARCHAR(250)     
        DECLARE @OutputID NUMERIC        
        DECLARE @COutputID NUMERIC        
        SET @COutputID = 10017   
        DECLARE @CGST_Amount NUMERIC(18, 2) 
        DECLARE @v_INV_TYPE VARCHAR(1)  
        DECLARE @CessOutputID NUMERIC = 0          
        SET @CessOutputID = 10014     
        
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
                          Cess_Amt        
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
                          @v_CN_ItemCess        
                        )        
        
                UPDATE  CN_SERIES
                SET     CURRENT_USED = @V_CreditNote_No
                WHERE   DIV_ID = @V_Division_ID          
        
                       
                SET @Remarks = 'Credit against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))    
        
        
                EXECUTE Proc_Ledger_Insert @v_CN_CustId, @v_CN_Amount, 0,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY        
        
                EXECUTE Proc_Ledger_Insert 10071, 0, @v_CN_ItemValue, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY
              
                       
                SET @CGST_Amount = ( @v_CN_ItemTax / 2 )  
        
        
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
                        Cess_Amt = @v_CN_ItemCess
                WHERE   CreditNote_ID = @V_CreditNote_ID                 
                
                SET @Remarks = 'Credit against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))    
        
        
                EXECUTE Proc_Ledger_Insert @v_CN_CustId, @v_CN_Amount, 0,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY        
        
                EXECUTE Proc_Ledger_Insert 10071, 0, @v_CN_ItemValue, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY
              
                       
                SET @CGST_Amount = ( @v_CN_ItemTax / 2 )  
        
        
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
                       
            END        
    END 

Go


Alter PROCEDURE [dbo].[PROC_CreditNote_DETAIL]  
    (  
      @v_CreditNote_ID NUMERIC(18, 0) ,  
      @v_Item_ID NUMERIC(18, 0) ,  
      @v_Item_Qty NUMERIC(18, 4) ,  
      @v_Created_By VARCHAR(100) ,  
      @v_Creation_Date DATETIME ,  
      @v_Modified_By VARCHAR(100) ,  
      @v_Modification_Date DATETIME ,  
      @v_Division_Id INT ,  
      @v_Stock_Detail_Id NUMERIC(18, 0) ,  
      @v_Item_Rate NUMERIC(18, 2) ,  
      @v_Item_Tax NUMERIC(18, 2) ,  
      @v_Item_Cess NUMERIC(18, 2) ,  
      @V_Trans_Type NUMERIC(18, 0) ,  
      @v_Proc_Type INT        
    )  
AS  
    BEGIN        
        IF @V_PROC_TYPE = 1  
            BEGIN        
                INSERT  INTO CreditNote_DETAIL  
                        ( CreditNote_Id ,  
                          Item_ID ,  
                          Item_Qty ,  
                          Created_By ,  
                          Creation_Date ,  
                          Modified_By ,  
                          Modification_Date ,  
                          Division_Id ,  
                          Stock_Detail_Id ,  
                          Item_Rate ,  
                          Item_Tax,  
                          Item_Cess    
                        )  
                VALUES  ( @V_CreditNote_ID ,  
                          @V_Item_ID ,  
                          @V_Item_Qty ,  
                          @V_Created_By ,  
                          @V_Creation_Date ,  
                          @V_Modified_By ,  
                          @V_Modification_Date ,  
                          @V_Division_Id ,  
                          @v_Stock_Detail_Id ,  
                          @v_Item_Rate ,  
                          @v_Item_Tax,  
                          @v_Item_Cess    
                        )   
                               
                --DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)    
                      
                UPDATE  dbo.STOCK_DETAIL  
                SET     ISSUE_QTY = ISSUE_QTY - @v_Item_Qty ,  
                        Balance_Qty = BALANCE_QTY + @v_Item_Qty  
                WHERE   STOCK_DETAIL_ID = @v_Stock_Detail_Id        
                  
      
                EXEC INSERT_TRANSACTION_LOG @v_CreditNote_ID, @v_Item_ID,  
                    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,  
                    @v_Creation_Date, 0         
            END  
            
        IF @V_PROC_TYPE = 2
            BEGIN
            
				DELETE  FROM dbo.CreditNote_DETAIL
                WHERE   CreditNote_Id = @V_CreditNote_ID  AND  Item_ID = @V_Item_ID
                
                
				INSERT  INTO CreditNote_DETAIL  
                        ( CreditNote_Id ,  
                          Item_ID ,  
                          Item_Qty ,  
                          Created_By ,  
                          Creation_Date ,  
                          Modified_By ,  
                          Modification_Date ,  
                          Division_Id ,  
                          Stock_Detail_Id ,  
                          Item_Rate ,  
                          Item_Tax,  
                          Item_Cess    
                        )  
                VALUES  ( @V_CreditNote_ID ,  
                          @V_Item_ID ,  
                          @V_Item_Qty ,  
                          @V_Created_By ,  
                          @V_Creation_Date ,  
                          @V_Modified_By ,  
                          @V_Modification_Date ,  
                          @V_Division_Id ,  
                          @v_Stock_Detail_Id ,  
                          @v_Item_Rate ,  
                          @v_Item_Tax,  
                          @v_Item_Cess    
                        )   
                               
                --DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)    
                      
                UPDATE  dbo.STOCK_DETAIL  
                SET     ISSUE_QTY = ISSUE_QTY - @v_Item_Qty ,  
                        Balance_Qty = BALANCE_QTY + @v_Item_Qty  
                WHERE   STOCK_DETAIL_ID = @v_Stock_Detail_Id   
                
                
                DELETE  FROM dbo.Transaction_Log
                WHERE   Transaction_Type = @V_Trans_Type
                        AND Transaction_ID = @V_CreditNote_ID     
                  
      
                EXEC INSERT_TRANSACTION_LOG @v_CreditNote_ID, @v_Item_ID,  
                    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,  
                    @v_Creation_Date, 0 
            END      
    END    
     
  
Go



Alter PROC [dbo].[PROC_Cancel_Sale_Invoice]
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
                @TransactionDate = SI_DATE
        FROM    dbo.SALE_INVOICE_MASTER sim 
        WHERE   sim.SI_ID = @SI_ID
        
        SELECT @v_Add_CESS_AMOUNT = SUM(sidd.ACessAmount) FROM dbo.SALE_INVOICE_DETAIL sidd WHERE  sidd.SI_ID = @SI_ID
          
        
        SET @CGST_Amount = @V_VAT_AMOUNT / 2
        
        SET @RoundOff = @v_NET_AMOUNT - ( @v_GROSS_AMOUNT + @v_VAT_AMOUNT  
                                          + @v_CESS_AMOUNT ) 
        
        SET @InputID = ( SELECT CASE WHEN @v_INV_TYPE = 'I' THEN 10021
                                     WHEN @v_INV_TYPE = 'S' THEN 10024
                                     WHEN @v_INV_TYPE = 'U' THEN 10075
                                END AS inputid
                       ) 
          
        DECLARE @Remarks VARCHAR(250)    
        SET @Remarks = 'Amount Deducted against Cancel Invoice - ' + @V_Si_NO    
          
        EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0, @Remarks,
            @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY  
            
        EXECUTE Proc_Ledger_Insert @V_CUST_ID, 0, @V_GROSS_AMOUNT, @Remarks,
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
            
            
        
        SET @Remarks = 'Amount Deducted Cess against Cancel Invoice - ' + @V_Si_NO   
                        
                EXECUTE Proc_Ledger_Insert 10014, 0, @v_CESS_AMOUNT, @Remarks,  
                    @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY    
                      
                      
                  SET @Remarks = 'Amount Deducted Add. Cess against Cancel Invoice - ' +   @V_Si_NO
                      
                EXECUTE Proc_Ledger_Insert 10012, 0, @v_Add_CESS_AMOUNT, @Remarks,  
                    @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY    
             
                           
              
                SET @Remarks = 'Amount Deducted Round Off against Cancel Invoice -' + @V_Si_NO 
                               
              
                IF @RoundOff > 0  
                    BEGIN              
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff, 
                            @Remarks, @V_Division_ID, @SI_ID, 16, @TransactionDate,  
                            @V_Created_BY              
                    END              
                ELSE  
                    BEGIN              
                        SET @RoundOff = -+@RoundOff  
                                    
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0, 
               @Remarks, @V_Division_ID, @SI_ID, 16, @TransactionDate,  
                            @V_Created_BY              
                    END               
              
              
                SET @Remarks = 'Amount Deducted Stock In against Cancel Invoice - ' + @V_Si_NO  
              
                EXECUTE Proc_Ledger_Insert 10073, 0, @V_NET_AMOUNT, @Remarks,  
                    @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY 
        
            
    END    

Go



Alter PROCEDURE [dbo].[PROC_OUTSIDE_SALE_MASTER_SALE_NEW]
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
                        CESS_AMOUNT = ( ISNULL(@v_CESS_AMOUNT, 0) + ISNULL(@v_ACESS_AMOUNT,
                                                              0) ) ,
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

Go