
insert INTO dbo.DBScriptUpdateLog
        ( LogFileName, ExecuteDateTime )
VALUES  ( '0011_05_May_2018_PaymentTransactionProcedureCorrection',
          GETDATE()
          )

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
      
                        SET @Remarks = 'Payment received against reference no.: '  
                            + @ChequeDraftNo        
      
                        EXECUTE Proc_Ledger_Insert @AccountId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, 18, @PaymentDate,  
                            @CreatedBy       
              
                        EXECUTE Proc_Ledger_Insert @BankId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, 18, @PaymentDate,  
                            @CreatedBy       
      
                    END        
                IF @PM_TYPE = 2  
                    BEGIN      
                        SET @Remarks = 'Payment released against reference no.: '  
                            + @ChequeDraftNo        
      
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, 19, @PaymentDate,  
                            @CreatedBy        
      
                        EXECUTE Proc_Ledger_Insert @BankId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, 19, @PaymentDate,  
                            @CreatedBy       
      
                    END      
                          
                IF @PM_TYPE = 3  
                    BEGIN      
                        SET @Remarks = 'Journal Entry against reference no.: '  
                            + @ChequeDraftNo         
      
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, 21, @PaymentDate,  
                            @CreatedBy        
      
                        EXECUTE Proc_Ledger_Insert @BankId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, 21, @PaymentDate,  
                            @CreatedBy       
      
                    END       
                          
                IF @PM_TYPE = 4  
                    BEGIN      
                        SET @Remarks = 'Contra Entry against reference no.: '  
                            + @ChequeDraftNo         
      
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, 22, @PaymentDate,  
                            @CreatedBy        
      
                        EXECUTE Proc_Ledger_Insert @BankId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, 22, @PaymentDate,  
                            @CreatedBy       
      
                    END       
                          
                IF @PM_TYPE = 5  
                    BEGIN      
                        SET @Remarks = 'Expense aagainst reference no.: '  
                            + @ChequeDraftNo        
      
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,  
                            @TotalamountReceived, @Remarks, @DivisionId,  
                            @PaymentTransactionId, 21, @PaymentDate,  
                            @CreatedBy        
      
                        EXECUTE Proc_Ledger_Insert @BankId,  
                            @TotalamountReceived, 0, @Remarks, @DivisionId,  
                            @PaymentTransactionId, 21, @PaymentDate,  
                            @CreatedBy       
      
                    END       
      
            END        
      
    END 
Go

