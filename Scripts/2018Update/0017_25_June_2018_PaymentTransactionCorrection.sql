
INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0017_25_June_2018_PaymentTransactionCorrection' ,
          GETDATE()
        )
Go

CREATE TABLE [dbo].[PaymentTransactionLog](
	[TransactionLog_Id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[PaymentTransactionId] [numeric](18, 0) NOT NULL,
	[PaymentTransactionNo] [varchar](100) NOT NULL,
	[PaymentTypeId] [numeric](18, 0) NOT NULL,
	[AccountId] [numeric](18, 0) NULL,
	[PaymentDate] [datetime] NULL,
	[ChequeDraftNo] [varchar](50) NULL,
	[ChequeDraftDate] [datetime] NULL,
	[BankId] [numeric](18, 0) NULL,
	[BankDate] [datetime] NULL,
	[Remarks] [varchar](200) NULL,
	[TotalAmountReceived] [numeric](18, 2) NULL,
	[UndistributedAmount] [numeric](18, 2) NULL,
	[CancellationCharges] [numeric](18, 2) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[DivisionId] [bigint] NULL,
	[StatusId] [int] NULL,
	[PM_TYPE] [numeric](18, 0) NULL,
	
 CONSTRAINT [PK_PaymentTransactionLog] PRIMARY KEY CLUSTERED 
(
	[TransactionLog_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


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
                
                INSERT  INTO PaymentTransactionLog 
						(PaymentTransactionId ,  
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
                          PM_TYPE)  
                SELECT PaymentTransactionId ,  
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
                   FROM PaymentTransaction WHERE PaymentTransactionId = @PaymentTransactionId
          
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
                        BankDate = @BankDate ,  
                        Remarks = @Remarks ,  
                        TotalAmountReceived = @TotalAmountReceived ,  
                        UndistributedAmount = @UndistributedAmount ,  
                        ModifiedBy = @CreatedBy ,  
                        ModifiedDate = GETDATE() ,  
                        StatusId = @StatusId  
                WHERE   PaymentTransactionId = @PaymentTransactionId        
                      
                SET @ProcedureStatus = @PaymentTransactionId  
                
                
                INSERT  INTO PaymentTransactionLog 
						(PaymentTransactionId ,  
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
                          PM_TYPE)  
                SELECT PaymentTransactionId ,  
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
                          PM_TYPE
                   FROM PaymentTransaction WHERE PaymentTransactionId = @PaymentTransactionId    
                      
                      
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

GO
