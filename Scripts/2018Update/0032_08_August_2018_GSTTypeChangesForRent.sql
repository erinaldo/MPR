INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0032_08_August_2018_GSTTypeChangesForRent' ,
          GETDATE()
        )
Go


ALTER TABLE PaymentTransaction ADD GST_Applicable_Acc  char(10)

Go

ALTER TABLE PaymentTransactionLog ADD GST_Applicable_Acc  char(10)

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
      @FK_GST_TYPE_ID NUMERIC(18, 0) = NULL ,
      @fk_GST_ID NUMERIC(18, 0) = NULL ,
      @Fk_HSN_ID NUMERIC(18, 0) = NULL ,
      @Fk_GSTNature_ID NUMERIC(18, 0) = NULL ,
      @GSTPerAmt NUMERIC(18, 2) = 0 ,
      @GST_Applicable_Acc CHAR(10) = NULL ,
      @ProcedureStatus INT OUTPUT               
              
    )
AS
    BEGIN             
           
        DECLARE @IsInUT BIT               
        DECLARE @DivisionStateId INT  
                    
        DECLARE @CustStateid INT
        DECLARE @BankStateid INT
             
        DECLARE @InputID NUMERIC 
        DECLARE @OutputID NUMERIC   
                    
        DECLARE @CInputID NUMERIC               
        SET @CInputID = 10016 
        
        DECLARE @COutputID NUMERIC               
        SET @COutputID = 10017      
          
        SELECT  @DivisionStateId = STATE_ID ,
                @IsInUT = isUT_bit
        FROM    dbo.STATE_MASTER
        WHERE   STATE_ID IN (
                SELECT  STATE_ID
                FROM    dbo.CITY_MASTER
                WHERE   CITY_ID IN ( SELECT CITY_ID
                                     FROM   dbo.DIVISION_SETTINGS ) )  
        SELECT  @CustStateid = STATE_ID
        FROM    dbo.STATE_MASTER
        WHERE   STATE_ID IN (
                SELECT  STATE_ID
                FROM    dbo.CITY_MASTER
                WHERE   CITY_ID IN ( SELECT CITY_ID
                                     FROM   dbo.ACCOUNT_MASTER
                                     WHERE  ACC_ID = @AccountId ) ) 
                                     
        SELECT  @BankStateid = STATE_ID
        FROM    dbo.STATE_MASTER
        WHERE   STATE_ID IN (
                SELECT  STATE_ID
                FROM    dbo.CITY_MASTER
                WHERE   CITY_ID IN ( SELECT CITY_ID
                                     FROM   dbo.ACCOUNT_MASTER
                                     WHERE  ACC_ID = @BankId ) )      
               
                    
        DECLARE @CGST_Amount NUMERIC(18, 2)                
        SET @CGST_Amount = ( @GSTPerAmt / 2 )
           
        DECLARE @TotalAmtPlusGST NUMERIC(18, 2)        
        SET @TotalAmtPlusGST = @TotalamountReceived + @GSTPerAmt      
          
        SET @InputID = ( SELECT CASE WHEN @DivisionStateId <> @CustStateid
                                     THEN 10020 --'I'              
                                     WHEN @DivisionStateId = @CustStateid
                                          AND @IsInUT = '1' THEN 10074 --'U'              
                                     ELSE 10023 --'S'              
                                END AS inputid
                       )    
        
        SET @OutputID = ( SELECT CASE WHEN @DivisionStateId <> @BankStateid
                                     THEN 10021 --'I'              
                                     WHEN @DivisionStateId = @BankStateid
                                          AND @IsInUT = '1' THEN 10075--'U'              
                                     ELSE 10024 --'S'              
                                END AS outputid
                       )   
          
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
                          PM_TYPE ,
                          FK_GST_TYPE_ID ,
                          fk_GST_ID ,
                          Fk_HSN_ID ,
                          Fk_GSTNature_ID ,
                          GSTPerAmt ,
                          GST_Applicable_Acc                
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
                          @PM_TYPE ,
                          @FK_GST_TYPE_ID ,
                          @fk_GST_ID ,
                          @Fk_HSN_ID ,
                          @Fk_GSTNature_ID ,
                          @GSTPerAmt ,
                          @GST_Applicable_Acc     
                        )                 
              
                SET @ProcedureStatus = @PaymentTransactionId      
                    
                INSERT  INTO PaymentTransactionLog
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
                          PM_TYPE ,
                          FK_GST_TYPE_ID ,
                          fk_GST_ID ,
                          Fk_HSN_ID ,
                          Fk_GSTNature_ID ,
                          GSTPerAmt ,
                          GST_Applicable_Acc   
                        )
                        SELECT  PaymentTransactionId ,
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
                                PM_TYPE ,
                                FK_GST_TYPE_ID ,
                                fk_GST_ID ,
                                Fk_HSN_ID ,
                                Fk_GSTNature_ID ,
                                GSTPerAmt ,
                                GST_Applicable_Acc
                        FROM    PaymentTransaction
                        WHERE   PaymentTransactionId = @PaymentTransactionId    
              
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
                                    @TotalAmtPlusGST, 0, @Remarks, @DivisionId,
                                    @PaymentTransactionId, @TransactionId,
                                    @PaymentDate, @CreatedBy      
                                        
                                IF ISNULL(@FK_GST_TYPE_ID, 0) = 2 AND ISNULL(@GST_Applicable_Acc, '') = 'Dr.'
                                    BEGIN    
             
                                        SET @Remarks = 'GST against Payment released reference no.: '
                                            + @ChequeDraftNo             
                
                                        IF @InputID <> 10020
                                            BEGIN                
              
                                                EXECUTE Proc_Ledger_Insert @CInputID,
                                                    0, @CGST_Amount, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy           
              
                                                EXECUTE Proc_Ledger_Insert @InputID,
                                                    0, @CGST_Amount, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy               
                                            END                
              
                                        ELSE
                                            BEGIN                
              
                                                EXECUTE Proc_Ledger_Insert @InputID,
                                                    0, @GSTPerAmt, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy          
                                            END      
                                    
                                    END               
                
                            END                
                                    
                        IF @PM_TYPE = 3
                            BEGIN   
                                           
                                SET @Remarks = 'Journal Entry against reference no.: '
                                    + @ChequeDraftNo  
                                      
                                IF ISNULL(@GST_Applicable_Acc, '') = 'Cr.'
                                    AND ISNULL(@FK_GST_TYPE_ID, 0) = 2
                                    BEGIN  
                                      
                                        EXECUTE Proc_Ledger_Insert @AccountId,
                                            0, @TotalAmtPlusGST, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @PaymentDate,
                                            @CreatedBy                  
                
                                        EXECUTE Proc_Ledger_Insert @BankId,
                                            @TotalamountReceived, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @PaymentDate,
                                            @CreatedBy   
             
                                        SET @Remarks = 'GST against Journal reference no.: '
                                            + @ChequeDraftNo             
                
                                        IF @OutputID <> 10021
                                            BEGIN                
              
                                                EXECUTE Proc_Ledger_Insert @COutputID,
                                                    @CGST_Amount, 0, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy           
              
                                                EXECUTE Proc_Ledger_Insert @OutputID,
                                                    @CGST_Amount, 0, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy     
                                                                
                                            END                
              
                                        ELSE
                                            BEGIN                
              
                                                EXECUTE Proc_Ledger_Insert @OutputID,
                                                    @GSTPerAmt, 0, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy    
                                                            
                                            END   
                                  
                                    END  
                                      
                                ELSE
                                    IF ISNULL(@GST_Applicable_Acc, '') = 'Dr.'
                                        AND ISNULL(@FK_GST_TYPE_ID, 0) = 2
                                        BEGIN  
            
                                            EXECUTE Proc_Ledger_Insert @AccountId,
                                                0, @TotalamountReceived,
                                                @Remarks, @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy                  
                
                                            EXECUTE Proc_Ledger_Insert @BankId,
                                                @TotalAmtPlusGST, 0, @Remarks,
                                                @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy   
             
                                            SET @Remarks = 'GST against Journal reference no.: '
                                                + @ChequeDraftNo             
                
                                            IF @InputID <> 10020
                                                BEGIN                
              
                                                    EXECUTE Proc_Ledger_Insert @CInputID,
                                                        0, @CGST_Amount,
                                                        @Remarks, @DivisionId,
                                                        @PaymentTransactionId,
                                                        @TransactionId,
                                                        @PaymentDate,
                                                        @CreatedBy           
              
                                                    EXECUTE Proc_Ledger_Insert @InputID,
                                                        0, @CGST_Amount,
                                                        @Remarks, @DivisionId,
                                                        @PaymentTransactionId,
                                                        @TransactionId,
                                                        @PaymentDate,
                                                        @CreatedBy               
                                                END                
              
                                            ELSE
                                                BEGIN   
                                                      
                                                    EXECUTE Proc_Ledger_Insert @InputID,
                                                        0, @GSTPerAmt,
                                                        @Remarks, @DivisionId,
                                                        @PaymentTransactionId,
                                                        @TransactionId,
                                                        @PaymentDate,
                                                        @CreatedBy   
                                                                     
                                                END   
                                                 
                                        END  
                                      
                                    ELSE
                                        BEGIN  
                                          
                                            EXECUTE Proc_Ledger_Insert @AccountId,
                                                0, @TotalamountReceived,
                                                @Remarks, @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy                  
                
                                            EXECUTE Proc_Ledger_Insert @BankId,
                                                @TotalamountReceived, 0,
                                                @Remarks, @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy      
                                                                                 
                                        END                                   
                
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
                                      
                                IF ISNULL(@GST_Applicable_Acc, '') = 'Cr.'
                                    AND ISNULL(@FK_GST_TYPE_ID, 0) = 2
                                    BEGIN  
                                      
                                        EXECUTE Proc_Ledger_Insert @AccountId,
                                            0, @TotalAmtPlusGST, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @PaymentDate,
                                            @CreatedBy                  
                
                                        EXECUTE Proc_Ledger_Insert @BankId,
                                            @TotalamountReceived, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @PaymentDate,
                                            @CreatedBy  
             
                                        SET @Remarks = 'GST against Expense reference no.: '
                                            + @ChequeDraftNo             
                
                                        IF @OutputID <> 10021
                                            BEGIN                
              
                                                EXECUTE Proc_Ledger_Insert @COutputID,
                                                    @CGST_Amount, 0, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy           
              
                                                EXECUTE Proc_Ledger_Insert @OutputID,
                                                    @CGST_Amount, 0, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy     
                                                                
                                            END                
              
                                        ELSE
                                            BEGIN                
              
                                                EXECUTE Proc_Ledger_Insert @OutputID,
                                                    @GSTPerAmt, 0, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy    
                                                            
                                            END   
                                  
                                    END  
                                      
                                ELSE
                                    IF ISNULL(@GST_Applicable_Acc, '') = 'Dr.'
                                        AND ISNULL(@FK_GST_TYPE_ID, 0) = 2
                                        BEGIN  
            
                                            EXECUTE Proc_Ledger_Insert @AccountId,
                                                0, @TotalamountReceived,
                                                @Remarks, @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy                  
                
                                            EXECUTE Proc_Ledger_Insert @BankId,
                                                @TotalAmtPlusGST, 0, @Remarks,
                                                @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy   
             
                                            SET @Remarks = 'GST against Expense reference no.: '
                                                + @ChequeDraftNo             
                
                                            IF @InputID <> 10020
                                                BEGIN                
              
                                                    EXECUTE Proc_Ledger_Insert @CInputID,
                                                        0, @CGST_Amount,
                                                        @Remarks, @DivisionId,
                                                        @PaymentTransactionId,
                                                        @TransactionId,
                                                        @PaymentDate,
                                                        @CreatedBy           
              
                                                    EXECUTE Proc_Ledger_Insert @InputID,
                                                        0, @CGST_Amount,
                                                        @Remarks, @DivisionId,
                                                        @PaymentTransactionId,
                                                        @TransactionId,
                                                        @PaymentDate,
                                                        @CreatedBy               
                                                END                
              
                                            ELSE
                                                BEGIN   
                                                      
                                                    EXECUTE Proc_Ledger_Insert @InputID,
                                                        0, @GSTPerAmt,
                                                        @Remarks, @DivisionId,
                                                        @PaymentTransactionId,
                                                        @TransactionId,
                                                        @PaymentDate,
                                                        @CreatedBy   
                                                                     
                                                END   
                                                 
                                        END  
                                      
                                    ELSE
                                        BEGIN  
                                          
                                            EXECUTE Proc_Ledger_Insert @AccountId,
                                                0, @TotalamountReceived,
                                                @Remarks, @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy                  
                
                                            EXECUTE Proc_Ledger_Insert @BankId,
                                                @TotalamountReceived, 0,
                                                @Remarks, @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy      
                                                                                 
                                        END                                           
                
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
                        BankDate = @BankDate ,
                        Remarks = @Remarks ,
                        TotalAmountReceived = @TotalAmountReceived ,
                        UndistributedAmount = @UndistributedAmount ,
                        ModifiedBy = @CreatedBy ,
                        ModifiedDate = GETDATE() ,
                        StatusId = @StatusId ,
                        FK_GST_TYPE_ID = @FK_GST_TYPE_ID ,
                        fk_GST_ID = @fk_GST_ID ,
                        Fk_HSN_ID = @Fk_HSN_ID ,
                        Fk_GSTNature_ID = @Fk_GSTNature_ID ,
                        GSTPerAmt = @GSTPerAmt ,
                        GST_Applicable_Acc = @GST_Applicable_Acc
                WHERE   PaymentTransactionId = @PaymentTransactionId            
                          
                SET @ProcedureStatus = @PaymentTransactionId   
                    
                INSERT  INTO PaymentTransactionLog
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
                          ModifiedBy ,
                          ModifiedDate ,
                          DivisionId ,
                          StatusId ,
                          BankDate ,
                          PM_TYPE ,
                          FK_GST_TYPE_ID ,
                          fk_GST_ID ,
                          Fk_HSN_ID ,
                          Fk_GSTNature_ID ,
                          GSTPerAmt ,
                          GST_Applicable_Acc   
                        )
                        SELECT  PaymentTransactionId ,
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
                                ModifiedBy ,
                                ModifiedDate ,
                                DivisionId ,
                                StatusId ,
                                BankDate ,
                                PM_TYPE ,
                                FK_GST_TYPE_ID ,
                                fk_GST_ID ,
                                Fk_HSN_ID ,
                                Fk_GSTNature_ID ,
                                GSTPerAmt ,
                                GST_Applicable_Acc
                        FROM    PaymentTransaction
                        WHERE   PaymentTransactionId = @PaymentTransactionId     
                          
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
                                    @TotalAmtPlusGST, 0, @Remarks, @DivisionId,
                                    @PaymentTransactionId, @TransactionId,
                                    @PaymentDate, @CreatedBy      
                                        
                                IF ISNULL(@FK_GST_TYPE_ID, 0) = 2 AND ISNULL(@GST_Applicable_Acc, '') = 'Dr.'
                                    BEGIN    
             
                                        SET @Remarks = 'GST against Payment released reference no.: '
                                            + @ChequeDraftNo             
                
                                        IF @InputID <> 10020
                                            BEGIN                
              
                                                EXECUTE Proc_Ledger_Insert @CInputID,
                                                    0, @CGST_Amount, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy           
              
                                                EXECUTE Proc_Ledger_Insert @InputID,
                                                    0, @CGST_Amount, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy               
                                            END                
              
                                        ELSE
                                            BEGIN                
              
                                                EXECUTE Proc_Ledger_Insert @InputID,
                                                    0, @GSTPerAmt, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy          
                                            END      
                                    
                                    END               
                
                            END                
                                    
                        IF @PM_TYPE = 3
                            BEGIN   
                                           
                                SET @Remarks = 'Journal Entry against reference no.: '
                                    + @ChequeDraftNo  
                                      
                                IF ISNULL(@GST_Applicable_Acc, '') = 'Cr.'
                                    AND ISNULL(@FK_GST_TYPE_ID, 0) = 2
                                    BEGIN  
                                      
                                        EXECUTE Proc_Ledger_Insert @AccountId,
                                            0, @TotalAmtPlusGST, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @PaymentDate,
                                            @CreatedBy                  
                
                                        EXECUTE Proc_Ledger_Insert @BankId,
                                            @TotalamountReceived, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @PaymentDate,
                                            @CreatedBy   
             
                                        SET @Remarks = 'GST against Journal reference no.: '
                                            + @ChequeDraftNo             
                
                                        IF @OutputID <> 10021
                                            BEGIN                
              
                                                EXECUTE Proc_Ledger_Insert @COutputID,
                                                    @CGST_Amount, 0, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy           
              
                                                EXECUTE Proc_Ledger_Insert @OutputID,
                                                    @CGST_Amount, 0, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy     
                                                                
                                            END                
              
                                        ELSE
                                            BEGIN                
              
                                                EXECUTE Proc_Ledger_Insert @OutputID,
                                                    @GSTPerAmt, 0, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy    
                                                            
                                            END   
                                  
                                    END  
                                      
                                ELSE
                                    IF ISNULL(@GST_Applicable_Acc, '') = 'Dr.'
                                        AND ISNULL(@FK_GST_TYPE_ID, 0) = 2
                                        BEGIN  
            
                                            EXECUTE Proc_Ledger_Insert @AccountId,
                                                0, @TotalamountReceived,
                                                @Remarks, @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy                  
                
                                            EXECUTE Proc_Ledger_Insert @BankId,
                                                @TotalAmtPlusGST, 0, @Remarks,
                                                @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy   
             
                                            SET @Remarks = 'GST against Journal reference no.: '
                                                + @ChequeDraftNo             
                
                                            IF @InputID <> 10020
                                                BEGIN                
              
                                                    EXECUTE Proc_Ledger_Insert @CInputID,
                                                        0, @CGST_Amount,
                                                        @Remarks, @DivisionId,
                                                        @PaymentTransactionId,
                                                        @TransactionId,
                                                        @PaymentDate,
                                                        @CreatedBy           
              
                                                    EXECUTE Proc_Ledger_Insert @InputID,
                                                        0, @CGST_Amount,
                                                        @Remarks, @DivisionId,
                                                        @PaymentTransactionId,
                                                        @TransactionId,
                                                        @PaymentDate,
                                                        @CreatedBy               
                                                END                
              
                                            ELSE
                                                BEGIN   
                                                      
                                                    EXECUTE Proc_Ledger_Insert @InputID,
                                                        0, @GSTPerAmt,
                                                        @Remarks, @DivisionId,
                                                        @PaymentTransactionId,
                                                        @TransactionId,
                                                        @PaymentDate,
                                                        @CreatedBy   
                                                                     
                                                END   
                                                 
                                        END  
                                      
                                    ELSE
                                        BEGIN  
                                          
                                            EXECUTE Proc_Ledger_Insert @AccountId,
                                                0, @TotalamountReceived,
                                                @Remarks, @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy                  
                
                                            EXECUTE Proc_Ledger_Insert @BankId,
                                                @TotalamountReceived, 0,
                                                @Remarks, @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy      
                                                                                 
                                        END                                   
                
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
                                      
                                IF ISNULL(@GST_Applicable_Acc, '') = 'Cr.'
                                    AND ISNULL(@FK_GST_TYPE_ID, 0) = 2
                                    BEGIN  
                                      
                                        EXECUTE Proc_Ledger_Insert @AccountId,
                                            0, @TotalAmtPlusGST, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @PaymentDate,
                                            @CreatedBy                  
                
                                        EXECUTE Proc_Ledger_Insert @BankId,
                                            @TotalamountReceived, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @PaymentDate,
                                            @CreatedBy   
             
                                        SET @Remarks = 'GST against Expense reference no.: '
                                            + @ChequeDraftNo             
                
                                        IF @OutputID <> 10021
                                            BEGIN                
              
                                                EXECUTE Proc_Ledger_Insert @COutputID,
                                                    @CGST_Amount, 0, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy           
              
                                                EXECUTE Proc_Ledger_Insert @OutputID,
                                                    @CGST_Amount, 0, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy     
                                                                
                                            END                
              
                                        ELSE
                                            BEGIN                
              
                                                EXECUTE Proc_Ledger_Insert @OutputID,
                                                    @GSTPerAmt, 0, @Remarks,
                                                    @DivisionId,
                                                    @PaymentTransactionId,
                                                    @TransactionId,
                                                    @PaymentDate, @CreatedBy    
                                                            
                                            END   
                                  
                                    END  
                                      
                                ELSE
                                    IF ISNULL(@GST_Applicable_Acc, '') = 'Dr.'
                                        AND ISNULL(@FK_GST_TYPE_ID, 0) = 2
                                        BEGIN  
            
                                            EXECUTE Proc_Ledger_Insert @AccountId,
                                                0, @TotalamountReceived,
                                                @Remarks, @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy                  
                
                                            EXECUTE Proc_Ledger_Insert @BankId,
                                                @TotalAmtPlusGST, 0, @Remarks,
                                                @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy   
             
                                            SET @Remarks = 'GST against Expense reference no.: '
                                                + @ChequeDraftNo             
                
                                            IF @InputID <> 10020
                                                BEGIN                
              
                                                    EXECUTE Proc_Ledger_Insert @CInputID,
                                                        0, @CGST_Amount,
                                                        @Remarks, @DivisionId,
                                                        @PaymentTransactionId,
                                                        @TransactionId,
                                                        @PaymentDate,
                                                        @CreatedBy           
              
                                                    EXECUTE Proc_Ledger_Insert @InputID,
                                                        0, @CGST_Amount,
                                                        @Remarks, @DivisionId,
                                                        @PaymentTransactionId,
                                                        @TransactionId,
                                                        @PaymentDate,
                                                        @CreatedBy               
                                                END                
              
                                            ELSE
                                                BEGIN   
                                                      
                                                    EXECUTE Proc_Ledger_Insert @InputID,
                                                        0, @GSTPerAmt,
                                                        @Remarks, @DivisionId,
                                                        @PaymentTransactionId,
                                                        @TransactionId,
                                                        @PaymentDate,
                                                        @CreatedBy   
                                                                     
                                                END   
                                                 
                                        END  
                                      
                                    ELSE
                                        BEGIN  
                                          
                                            EXECUTE Proc_Ledger_Insert @AccountId,
                                                0, @TotalamountReceived,
                                                @Remarks, @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy                  
                
                                            EXECUTE Proc_Ledger_Insert @BankId,
                                                @TotalamountReceived, 0,
                                                @Remarks, @DivisionId,
                                                @PaymentTransactionId,
                                                @TransactionId, @PaymentDate,
                                                @CreatedBy      
                                                                                 
                                        END                                           
                
                            END                  
                
                    END           
                          
                          
            END             
              
    END 

Go

ALTER PROCEDURE [dbo].[Proc_PaymentTransactionPayableUpdateDeleteLedgerEntries]  
    (  
      @PaymentTransactionId NUMERIC(18, 2) ,  
      @TransactionTypeId INT            
    )  
AS  
    BEGIN              
                    
        DECLARE @ACC_ID NUMERIC(18, 2)        
        DECLARE @Bank_ID NUMERIC(18, 2)
        DECLARE @GST_Applicable_Acc CHAR(10)
        DECLARE @FK_GST_TYPE_ID NUMERIC(18, 0)             
                  
        SELECT  @ACC_ID = AccountId ,  
                @Bank_ID = BankId ,
                @FK_GST_TYPE_ID = FK_GST_TYPE_ID ,
                @GST_Applicable_Acc = GST_Applicable_Acc 
        FROM    dbo.PaymentTransaction  
        WHERE   PaymentTransactionId = @PaymentTransactionId             
          
        DECLARE @IsInUT BIT               
        DECLARE @DivisionStateId INT              
        DECLARE @CustStateid INT
        DECLARE @BankStateid INT
             
        DECLARE @InputID NUMERIC 
        DECLARE @OutputID NUMERIC    
                    
        DECLARE @CInputID NUMERIC               
        SET @CInputID = 10016  
        
        DECLARE @COutputID NUMERIC               
        SET @COutputID = 10017      
          
        SELECT  @DivisionStateId = STATE_ID ,  
                @IsInUT = isUT_bit  
        FROM    dbo.STATE_MASTER  
        WHERE   STATE_ID IN (  
                SELECT  STATE_ID  
                FROM    dbo.CITY_MASTER  
                WHERE   CITY_ID IN ( SELECT CITY_ID  
                                     FROM   dbo.DIVISION_SETTINGS ) )  
                                       
        SELECT  @CustStateid = STATE_ID  
        FROM    dbo.STATE_MASTER  
        WHERE   STATE_ID IN (  
                SELECT  STATE_ID  
                FROM    dbo.CITY_MASTER  
                WHERE   CITY_ID IN ( SELECT CITY_ID  
                                     FROM   dbo.ACCOUNT_MASTER  
                                     WHERE  ACC_ID = @ACC_ID )) 
                                     
        SELECT  @BankStateid = STATE_ID
        FROM    dbo.STATE_MASTER
        WHERE   STATE_ID IN (
                SELECT  STATE_ID
                FROM    dbo.CITY_MASTER
                WHERE   CITY_ID IN ( SELECT CITY_ID
                                     FROM   dbo.ACCOUNT_MASTER
                                     WHERE  ACC_ID = @Bank_ID ) )
                                     
                                     
        SET @InputID = ( SELECT CASE WHEN @DivisionStateId <> @CustStateid
                                     THEN 10020 --'I'              
                                     WHEN @DivisionStateId = @CustStateid
                                          AND @IsInUT = '1' THEN 10074 --'U'              
                                     ELSE 10023 --'S'              
                                END AS inputid
                       )    
        
        SET @OutputID = ( SELECT CASE WHEN @DivisionStateId <> @BankStateid
                                     THEN 10021 --'I'              
                                     WHEN @DivisionStateId = @BankStateid
                                          AND @IsInUT = '1' THEN 10075--'U'              
                                     ELSE 10024 --'S'              
                                END AS outputid
                       ) 
                       
                       
        DECLARE @CashOut NUMERIC(18, 2)              
        DECLARE @CashIn NUMERIC(18, 2)                                                 
          
          
        IF ISNULL(@GST_Applicable_Acc, '') = 'Cr.'
            AND ISNULL(@FK_GST_TYPE_ID, 0) = 2
            BEGIN  
              
                SET @CashOut = 0            
                    
                SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                FROM    dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                WHERE   TransactionId = @PaymentTransactionId
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
          
                IF @OutputID <> 10021
                    BEGIN                
              
                        SET @CashIn = 0            
                    
                        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                         FROM   dbo.LedgerDetail
                                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                         WHERE  TransactionId = @PaymentTransactionId
                                                AND TransactionTypeId = @TransactionTypeId
                                                AND AccountId = @COutputID
                                       )              
              
                        UPDATE  dbo.LedgerMaster
                        SET     AmountInHand = AmountInHand - @CashOut
                                + @CashIn
                        WHERE   AccountId = @COutputID           
              
                        SET @CashIn = 0            
                    
                        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                         FROM   dbo.LedgerDetail
                                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                         WHERE  TransactionId = @PaymentTransactionId
                                                AND TransactionTypeId = @TransactionTypeId
                                                AND AccountId = @OutputID
                                       )              
              
                        UPDATE  dbo.LedgerMaster
                        SET     AmountInHand = AmountInHand - @CashOut
                                + @CashIn
                        WHERE   AccountId = @OutputID              
                    END                
              
                ELSE
                    BEGIN                
              
                        SET @CashIn = 0            
                    
                        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                         FROM   dbo.LedgerDetail
                                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                         WHERE  TransactionId = @PaymentTransactionId
                                                AND TransactionTypeId = @TransactionTypeId
                                                AND AccountId = @OutputID
                                       )              
              
                        UPDATE  dbo.LedgerMaster
                        SET     AmountInHand = AmountInHand - @CashOut
                                + @CashIn
                        WHERE   AccountId = @OutputID          
                    END   
          
            END  
        ELSE
            IF ISNULL(@GST_Applicable_Acc, '') = 'Dr.'
                AND ISNULL(@FK_GST_TYPE_ID, 0) = 2
                BEGIN   
                  
                    SET @CashOut = 0            
                    
                    SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                    FROM    dbo.LedgerDetail
                                            JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                    WHERE   TransactionId = @PaymentTransactionId
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
       
                    IF @InputID <> 10020
                        BEGIN                
              
                            SET @CashOut = 0            
                    
                            SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                            FROM    dbo.LedgerDetail
                                                    JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                            WHERE   TransactionId = @PaymentTransactionId
                                                    AND TransactionTypeId = @TransactionTypeId
                                                    AND AccountId = @CInputID
                                          )              
              
                            UPDATE  dbo.LedgerMaster
                            SET     AmountInHand = AmountInHand - @CashOut
                                    + @CashIn
                            WHERE   AccountId = @CInputID           
              
                            SET @CashOut = 0            
                    
                            SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                            FROM    dbo.LedgerDetail
                                                    JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                            WHERE   TransactionId = @PaymentTransactionId
                                                    AND TransactionTypeId = @TransactionTypeId
                                                    AND AccountId = @InputID
                                          )              
              
                            UPDATE  dbo.LedgerMaster
                            SET     AmountInHand = AmountInHand - @CashOut
                                    + @CashIn
                            WHERE   AccountId = @InputID              
                        END                
              
                    ELSE
                        BEGIN                
              
                            SET @CashOut = 0            
                    
                            SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                            FROM    dbo.LedgerDetail
                                                    JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                            WHERE   TransactionId = @PaymentTransactionId
                                                    AND TransactionTypeId = @TransactionTypeId
                                                    AND AccountId = @InputID
                                          )              
              
                            UPDATE  dbo.LedgerMaster
                            SET     AmountInHand = AmountInHand - @CashOut
                                    + @CashIn
                            WHERE   AccountId = @InputID          
                        END    
            
                END     
            ELSE
                BEGIN  
                    SET @CashOut = 0            
                    
                    SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                    FROM    dbo.LedgerDetail
                                            JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                    WHERE   TransactionId = @PaymentTransactionId
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
                END                       
                    
        DELETE  FROM dbo.LedgerDetail  
        WHERE   TransactionId = @PaymentTransactionId  
                AND TransactionTypeId = @TransactionTypeId
    
    END   

Go