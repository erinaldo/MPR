
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