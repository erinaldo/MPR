CREATE PROCEDURE [dbo].[ProcPaymentTransactionDetail_Insert]  
    (  
      @PaymentTransactionId NUMERIC = NULL ,  
      @StatusId NUMERIC = NULL ,  
      @PaymentTransactionCode VARCHAR(100) = NULL ,  
      @PaymentTypeId NUMERIC = NULL ,  
      @AccountId NUMERIC = NULL ,  
      @PaymentDate DATETIME = NULL ,  
      @ChequeDraftNo VARCHAR(50) = NULL ,  
      @ChequeDraftDate DATETIME = NULL ,  
      @BankId NUMERIC = 0 ,  
      @BankDate DATETIME = NULL ,  
      @Remarks VARCHAR(200) = NULL ,  
      @TotalamountReceived NUMERIC(18, 2) = 0 ,  
      @BalanceTotalAmount NUMERIC(18, 2) = 0 ,  
      @CreatedBy VARCHAR(50) = NULL ,  
      @DivisionId NUMERIC = 0 ,  
      @ProcedureStatus INT OUTPUT     
    )  
AS  
    BEGIN    
      
        DECLARE @LedgerId NUMERIC   
        SELECT  @PaymentTransactionId = ISNULL(MAX(PaymentTransactionId), 0)  
                + 1  
        FROM    dbo.PaymentTransactionLOG        
               
                
        INSERT  INTO PaymentTransactionLOG  
                ( PaymentTransactionId ,  
                  PaymentTransactionCode ,  
                  PaymentTypeId ,  
                  AccountId ,  
                  PaymentDate ,  
                  ChequeDraftNo ,  
                  ChequeDraftDate ,  
                  BankId ,  
                  Remarks ,  
                  TotalAmountReceived ,  
                  BalanceTotalAmount ,  
                  CreatedBy ,  
                  CreatedDate ,  
                  DivisionId ,  
                  StatusId ,  
                  BankDate    
                )  
        VALUES  ( @PaymentTransactionId ,  
                  @PaymentTransactionCode ,  
                  @PaymentTypeId ,  
                  @AccountId ,  
                  @PaymentDate ,  
                  @ChequeDraftNo ,  
                  @ChequeDraftDate ,  
                  @BankId ,    
                          --@BANK_NAME ,    
                  @Remarks ,  
                  @TotalamountReceived ,  
                  @BalanceTotalAmount ,  
                  @CreatedBy ,  
                  GETDATE() ,  
                  @DivisionId ,  
                  @StatusId ,  
                  @BankDate    
                )    
        IF @PaymentTypeId <> 1  
            BEGIN    
                IF @BalanceTotalAmount > 0  
                    BEGIN        
               --Ledger Entry     
               ----------------------------------------------                                             
                        IF ( SELECT COUNT(*)  
                             FROM   dbo.SaleLedgerMaster  
                             WHERE  AccountId = @AccountId  
                           ) > 0  
                            BEGIN                                                
                                SELECT  @LedgerId = LedgerId  
                                FROM    dbo.SaleLedgerMaster  
                                WHERE   AccountId = @AccountId                              
                                   --Update Ledger Master        
                                UPDATE  dbo.SaleLedgerMaster  
                                SET     TotalAmt = TotalAmt  
                                        + @BalanceTotalAmount ,  
                                        CreatedBy = @CreatedBy ,  
                                        CreatedDate = GETDATE()  
                                WHERE   LedgerId = @LedgerId    
                            END     
                        ELSE  
                            BEGIN                              
                                SELECT  @LedgerId = ISNULL(MAX(LedgerId), 0)  
                                        + 1  
                                FROM    dbo.SaleLedgerMaster    
                        --Insert Ledger Master    
                                INSERT  INTO dbo.SaleLedgerMaster  
                                        ( LedgerId ,  
                                          AccountId ,  
                                          TotalAmt ,  
                                          CreatedBy ,  
                                          CreatedDate    
                                        )  
                                VALUES  ( @LedgerId ,  
                                          @AccountId ,  
                                          @BalanceTotalAmount ,  
                                          @CreatedBy ,  
                                          GETDATE()  
                                        )    
                            END    
                        INSERT  INTO dbo.SaleLedgerDetail  
                                ( LedgerId ,  
                                  CashIn ,  
                                  CashOut ,  
                                  Remarks ,  
                                  TransactionId ,  
                                  TransactionTypeId ,  
                                  TransactionDate    
                                )  
                        VALUES  ( @LedgerId ,  
                                  @BalanceTotalAmount ,  
                                  0 ,  
                                  'Advance Payment Received' ,  
                                  @PaymentTransactionId ,  
                                  36 ,  
                                  GETDATE()  
                                )    
                    END     
                   ------------------------------------------------------    
                SET @ProcedureStatus = @PaymentTransactionId    
            END    
        ELSE  
            BEGIN    
                IF @BalanceTotalAmount > 0  
                    BEGIN        
               --Ledger Entry     
               ----------------------------------------------      
                        IF ( SELECT COUNT(*)  
                             FROM   dbo.SaleLedgerMaster  
                             WHERE  AccountId = @AccountId  
                           ) > 0  
                            BEGIN     
                                SELECT  @LedgerId = LedgerId  
                                FROM    dbo.SaleLedgerMaster  
                                WHERE   AccountId = @AccountId     
            --Update Ledger Master                               
                                UPDATE  dbo.SaleLedgerMaster  
                                SET     -- TOTAL_AMT = TOTAL_AMT    
                                                --+ @BALANCE_TOTAL_AMOUNT ,    
                                        CreatedBy = @CreatedBy ,  
                                        CreatedDate = GETDATE()  
                                WHERE   LedgerId = @LedgerId    
                            END    
                        ELSE  
                            BEGIN       
                                SELECT  @LedgerId = ISNULL(MAX(LedgerId), 0)  
                                        + 1  
                                FROM    dbo.SaleLedgerMaster    
                        --Insert Ledger Master    
                                INSERT  INTO dbo.SaleLedgerMaster  
                                        ( LedgerId ,  
                                          AccountId ,  
                                          TotalAmt ,  
                                          CreatedBy ,  
                                          CreatedDate,  
                                          DivisionId   
                                        )  
                                VALUES  ( @LedgerId ,  
                                          @AccountId ,  
                                          0.00 ,-- @BALANCE_TOTAL_AMOUNT ,    
                                          @CreatedBy ,  
                                          GETDATE(),  
                                          @DivisionId  
                                        )    
                            END    
                        INSERT  INTO dbo.SaleLedgerDetail  
                                ( LedgerId ,  
                                  CashIn ,  
                                  CashOut ,  
                                  Remarks ,  
                                  TransactionId ,  
                                  TransactionTypeId ,  
                                  TransactionDate    
                                )  
                        VALUES  ( @LedgerId ,  
                                  @BalanceTotalAmount ,  
                                  0 ,  
                                  'Advance Payment Received' ,  
                                  @PaymentTransactionId ,  
                                  18 ,  
                                  GETDATE()  
                                )    
                    END     
                   ------------------------------------------------------    
                SET @ProcedureStatus = @PaymentTransactionId    
           
      
            END    
    END 