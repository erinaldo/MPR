
INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0029_02_August_2018_GSTNature' ,
          GETDATE()
        )
Go

---------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[GST_Nature](
	[Id] [numeric](18, 0) NULL,
	[Type] [nvarchar](100) NULL,
	[Is_Active] [bit] NULL
) ON [PRIMARY]

GO


-------------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[ITC_Eligibility](
	[ID] [numeric](18, 0) NULL,
	[Type] [nvarchar](100) NULL,
	[Is_Active] [bit] NULL
) ON [PRIMARY]

 --------------------------------------------------------------------------------------------
 CREATE TABLE [dbo].[GST_Type](
	[Id] [numeric](18, 0) NULL,
	[Type] [nvarchar](100) NULL,
	[Is_Active] [bit] NULL
) ON [PRIMARY]

GO
-----------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Account_Item_Mapping](
	[Fk_AccountId] [numeric](18, 0) NULL,
	[Fk_ItemId] [numeric](18, 0) NULL
) ON [PRIMARY]

GO
-----------------------------------------------------------------------------------------------
ALTER TABLE ACCOUNT_MASTER ADD Fk_HSN_ID NUMERIC(18,0) NULL,
fk_GST_ID NUMERIC(18,0)NULL,FK_GST_TYPE_ID NUMERIC(18,0)NULL

------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[DepreciationRate](
	[Depn_Id] [numeric](18, 0) NULL,
	[AnualRate] [numeric](18, 2) NULL,
	[Half_Year] [numeric](18, 2) NULL,
	[Fk_AccountId] [numeric](18, 0) NULL
) ON [PRIMARY]

GO
--------------------------------------------------------------------------------------------------

ALTER TABLE PaymentTransaction ADD FK_GST_TYPE_ID  Numeric(18,0) 
Go
ALTER TABLE PaymentTransaction ADD fk_GST_ID Numeric(18,0) 
Go
ALTER TABLE PaymentTransaction ADD Fk_HSN_ID Numeric(18,0)
Go

ALTER TABLE PaymentTransaction ADD Fk_GSTNature_ID Numeric(18,0) 
Go
ALTER TABLE PaymentTransaction ADD GSTPerAmt Numeric(18,2) NOT NULL Default 0.00
GO
ALTER TABLE PaymentTransactionLog ADD FK_GST_TYPE_ID  Numeric(18,0)
Go
ALTER TABLE PaymentTransactionLog ADD fk_GST_ID Numeric(18,0) 
Go
ALTER TABLE PaymentTransactionLog ADD Fk_HSN_ID Numeric(18,0) 
Go
ALTER TABLE PaymentTransactionLog ADD Fk_GSTNature_ID Numeric(18,0) 
Go
ALTER TABLE PaymentTransactionLog ADD GSTPerAmt Numeric(18,2) NOT NULL Default 0.00
Go
----------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[ProcPaymentTransaction_Insert]
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
      @ProcedureStatus INT OUTPUT             
            
    )
AS
    BEGIN     
        
        
        
         
        DECLARE @IsInUT BIT             
        DECLARE @DivisionStateId INT            
        DECLARE @CustStateid INT   
        DECLARE @InputID NUMERIC  
                  
        DECLARE @CInputID NUMERIC             
        SET @CInputID = 10016     
        
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
                          GSTPerAmt              
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
                          @PM_TYPE,
                          @FK_GST_TYPE_ID ,
                          @fk_GST_ID ,
                          @Fk_HSN_ID ,
                          @Fk_GSTNature_ID ,
                          @GSTPerAmt   
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
                          GSTPerAmt 
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
                                GSTPerAmt
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
                                    
                                    
                                   
                                IF ISNULL(@FK_GST_TYPE_ID, 0) = 2
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
            
                                EXECUTE Proc_Ledger_Insert @AccountId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy              
            
                                EXECUTE Proc_Ledger_Insert @BankId,
                                    @TotalAmtPlusGST, 0, @Remarks, @DivisionId,
                                    @PaymentTransactionId, @TransactionId,
                                    @PaymentDate, @CreatedBy   
                                    
                                    
                                    
                                    
                                IF ISNULL(@FK_GST_TYPE_ID, 0) = 2
                                    BEGIN  
           
                                        SET @Remarks = 'GST against Journal reference no.: '
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
                                
                        IF @PM_TYPE = 4
                            BEGIN            
                                SET @Remarks = 'Contra Entry against reference no.: '
                                    + @ChequeDraftNo               
            
                                EXECUTE Proc_Ledger_Insert @AccountId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy              
            
                                EXECUTE Proc_Ledger_Insert @BankId,
                                    @TotalAmtPlusGST, 0, @Remarks, @DivisionId,
                                    @PaymentTransactionId, @TransactionId,
                                    @PaymentDate, @CreatedBy       
                                    
                                    
                                IF ISNULL(@FK_GST_TYPE_ID, 0) = 2
                                    BEGIN  
           
                                        SET @Remarks = 'GST against Journal reference no.: '
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
                                
                        IF @PM_TYPE = 5
                            BEGIN            
                                SET @Remarks = 'Expense aagainst reference no.: '
                                    + @ChequeDraftNo              
            
                                EXECUTE Proc_Ledger_Insert @AccountId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy              
            
                                EXECUTE Proc_Ledger_Insert @BankId,
                                    @TotalAmtPlusGST, 0, @Remarks, @DivisionId,
                                    @PaymentTransactionId, @TransactionId,
                                    @PaymentDate, @CreatedBy 
                                    
                                    
                                IF ISNULL(@FK_GST_TYPE_ID, 0) = 2
                                    BEGIN  
           
                                        SET @Remarks = 'GST against Journal reference no.: '
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
                        GSTPerAmt = @GSTPerAmt
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
                          GSTPerAmt 
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
                                GSTPerAmt
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
                                    
                                IF ISNULL(@FK_GST_TYPE_ID, 0) = 2
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
              
                                EXECUTE Proc_Ledger_Insert @AccountId, 0,
                                    @TotalamountReceived, @Remarks,
                                    @DivisionId, @PaymentTransactionId,
                                    @TransactionId, @PaymentDate, @CreatedBy                
              
                                EXECUTE Proc_Ledger_Insert @BankId,
                                    @TotalAmtPlusGST, 0, @Remarks, @DivisionId,
                                    @PaymentTransactionId, @TransactionId,
                                    @PaymentDate, @CreatedBy
                                    
                                    
                                IF ISNULL(@FK_GST_TYPE_ID, 0) = 2
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
                                    @TotalAmtPlusGST, 0, @Remarks, @DivisionId,
                                    @PaymentTransactionId, @TransactionId,
                                    @PaymentDate, @CreatedBy
                                    
                                IF ISNULL(@FK_GST_TYPE_ID, 0) = 2
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
              
                    END         
                        
                        
            END           
            
    END 

	GO
---------------------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[Proc_PaymentTransactionPayableUpdateDeleteLedgerEntries]
    (
      @PaymentTransactionId NUMERIC(18, 2) ,
      @TransactionTypeId INT          
    )
AS
    BEGIN            
                  
        DECLARE @ACC_ID NUMERIC(18, 2)      
        DECLARE @Bank_ID NUMERIC(18, 2)          
                
        SELECT  @ACC_ID = AccountId ,
                @Bank_ID = BankId
        FROM    dbo.PaymentTransaction
        WHERE   PaymentTransactionId = @PaymentTransactionId  
        DECLARE @CashOut NUMERIC(18, 2)            
        DECLARE @CashIn NUMERIC(18, 2)           
        
        
                  
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
        
        DECLARE @FK_GST_TYPE_ID NUMERIC(18, 0)  
        DECLARE @IsInUT BIT             
        DECLARE @DivisionStateId INT            
        DECLARE @CustStateid INT   
        DECLARE @InputID NUMERIC  
                  
        DECLARE @CInputID NUMERIC             
        SET @CInputID = 10016     
        
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
        
        
        
        
        IF ISNULL(@FK_GST_TYPE_ID, 0) = 2
            BEGIN  
     
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

---------------------------------------------------------------------------------------------------------------------------------------
  
--exec Proc_GETPaymentDetailByID_Edit 1        
ALTER PROCEDURE Proc_GETPaymentDetailByID_Edit ( @PaymentId AS NUMERIC )
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
                PM_TYPE ,
                PaymentTransaction.FK_GST_TYPE_ID ,
                PaymentTransaction.fk_GST_ID ,
                PaymentTransaction.Fk_HSN_ID ,
                PaymentTransaction.Fk_GSTNature_ID ,
                PaymentTransaction.GSTPerAmt
        FROM    dbo.PaymentTransaction
                INNER JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.PaymentTransaction.AccountId
        WHERE   PaymentTransactionId = @PaymentId 
                
    END
	Go
--------------------------------------------------------------------------------------------------------------------
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
        
        DECLARE @AccountId NUMERIC                    
        DECLARE @TotalamountReceived NUMERIC(18, 2) = 0                     
        DECLARE @PaymentTransactionNo VARCHAR(100)                     
        DECLARE @CreatedBy VARCHAR(50) = NULL                     
        DECLARE @DivisionId NUMERIC = 0                     
        DECLARE @BankId NUMERIC = 0                    
        DECLARE @ChequeDraftNo VARCHAR(50) = NULL                   
        DECLARE @TransactionDate DATETIME        
        DECLARE @GSTPerAmt NUMERIC(18, 2) = 0           
        DECLARE @FK_GST_TYPE_ID NUMERIC(18, 0) = NULL       
        DECLARE @fk_GST_ID NUMERIC(18, 0) = NULL       
        DECLARE @Fk_HSN_ID NUMERIC(18, 0) = NULL       
        DECLARE @Fk_GSTNature_ID NUMERIC(18, 0) = NULL        
              
              
        SELECT  @AccountId = AccountId ,
                @TotalamountReceived = TotalAmountReceived ,
                @PaymentTransactionNo = PaymentTransactionNo ,
                @CreatedBy = CreatedBy ,
                @DivisionId = DivisionId ,
                @BankId = BankId ,
                @ChequeDraftNo = ChequeDraftNo ,
                @TransactionDate = PaymentDate ,
                @GSTPerAmt = GSTPerAmt ,
                @FK_GST_TYPE_ID = FK_GST_TYPE_ID ,
                @fk_GST_ID = fk_GST_ID ,
                @Fk_HSN_ID = Fk_HSN_ID ,
                @Fk_GSTNature_ID = Fk_GSTNature_ID
        FROM    dbo.PaymentTransaction
        WHERE   PaymentTransactionId = @PaymentTransactionId      
        
        DECLARE @IsInUT BIT                 
        DECLARE @DivisionStateId INT                
        DECLARE @CustStateid INT       
        DECLARE @InputID NUMERIC      
                      
        DECLARE @CInputID NUMERIC                 
        SET @CInputID = 10016         
            
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
        
                       
                    
        UPDATE  PaymentTransaction
        SET     StatusId = @StatusId ,
                CancellationCharges = @CancellationCharges
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
                    
                        EXECUTE Proc_Ledger_Insert @BankId, @TotalAmtPlusGST,
                            0, @Remarks, @DivisionId, @PaymentTransactionId,
                            @TransactionId, @TransactionDate, @CreatedBy    
                                
                        IF ISNULL(@FK_GST_TYPE_ID, 0) = 2
                            BEGIN      
               
                                SET @Remarks = 'GST against Payment released reference no.: '
                                    + @ChequeDraftNo               
                  
                                IF @InputID <> 10020
                                    BEGIN                  
                
                                        EXECUTE Proc_Ledger_Insert @CInputID,
                                            0, @CGST_Amount, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @TransactionDate,
                                            @CreatedBy             
                
                                        EXECUTE Proc_Ledger_Insert @InputID, 0,
                                            @CGST_Amount, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @TransactionDate,
                                            @CreatedBy                 
                                    END                  
                
                                ELSE
                                    BEGIN                  
                
                                        EXECUTE Proc_Ledger_Insert @InputID, 0,
                                            @GSTPerAmt, @Remarks, @DivisionId,
                                            @PaymentTransactionId,
                                            @TransactionId, @TransactionDate,
                                            @CreatedBy            
                                    END        
                                      
                            END                                      
                    
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
                                
                        DELETE  FROM dbo.SettlementDetail
                        WHERE   PaymentTransactionId = @TransactionId              
                                  
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
                            @TotalAmtPlusGST, @Remarks, @DivisionId,
                            @PaymentTransactionId, @TransactionId,
                            @TransactionDate, @CreatedBy   
                              
                          
                        IF ISNULL(@FK_GST_TYPE_ID, 0) = 2
                            BEGIN      
               
                                SET @Remarks = 'GST Amount Deducted against Cancel reference no.: '
                                    + @ChequeDraftNo               
                  
                                IF @InputID <> 10020
                                    BEGIN                  
                
                                        EXECUTE Proc_Ledger_Insert @CInputID,
                                            @CGST_Amount, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @TransactionDate,
                                            @CreatedBy             
                
                                        EXECUTE Proc_Ledger_Insert @InputID,
                                            @CGST_Amount, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @TransactionDate,
                                            @CreatedBy                 
                                    END                  
                
                                ELSE
                                    BEGIN                  
                
                                        EXECUTE Proc_Ledger_Insert @InputID,
                                            @GSTPerAmt, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @TransactionDate,
                                            @CreatedBy            
                                    END        
                                      
                            END     
                                
                        DELETE  FROM dbo.SettlementDetail
                        WHERE   PaymentTransactionId = @TransactionId             
                                                              
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
                            @TotalAmtPlusGST, @Remarks, @DivisionId,
                            @PaymentTransactionId, @TransactionId,
                            @TransactionDate, @CreatedBy  
                              
                        IF ISNULL(@FK_GST_TYPE_ID, 0) = 2
                            BEGIN      
               
                                SET @Remarks = 'GST Amount Deducted against Cancel reference no.: '
                                    + @ChequeDraftNo               
                  
                                IF @InputID <> 10020
                                    BEGIN                  
                
                                        EXECUTE Proc_Ledger_Insert @CInputID,
                                            @CGST_Amount, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @TransactionDate,
                                            @CreatedBy             
                
                                        EXECUTE Proc_Ledger_Insert @InputID,
                                            @CGST_Amount, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @TransactionDate,
                                            @CreatedBy                 
                                    END                  
                
                                ELSE
                                    BEGIN                  
                
                                        EXECUTE Proc_Ledger_Insert @InputID,
                                            @GSTPerAmt, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @TransactionDate,
                                            @CreatedBy            
                                    END        
                                      
                            END   
                                
                        DELETE  FROM dbo.SettlementDetail
                        WHERE   PaymentTransactionId = @TransactionId            
                                                              
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
                                
                        DELETE  FROM dbo.SettlementDetail
                        WHERE   PaymentTransactionId = @TransactionId              
                                                              
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
                            @TotalAmtPlusGST, @Remarks, @DivisionId,
                            @PaymentTransactionId, @TransactionId,
                            @TransactionDate, @CreatedBy   
                              
                              
                        IF ISNULL(@FK_GST_TYPE_ID, 0) = 2
                            BEGIN      
               
                                SET @Remarks = 'GST Amount Deducted against Cancel reference no.: '
                                    + @ChequeDraftNo               
                  
                                IF @InputID <> 10020
                                    BEGIN                  
                
                                        EXECUTE Proc_Ledger_Insert @CInputID,
                                            @CGST_Amount, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @TransactionDate,
                                            @CreatedBy             
                
                                        EXECUTE Proc_Ledger_Insert @InputID,
                                            @CGST_Amount, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @TransactionDate,
                                            @CreatedBy                 
                                    END                  
                
                                ELSE
                                    BEGIN                  
                
                                        EXECUTE Proc_Ledger_Insert @InputID,
                                            @GSTPerAmt, 0, @Remarks,
                                            @DivisionId, @PaymentTransactionId,
                                            @TransactionId, @TransactionDate,
                                            @CreatedBy            
                                    END        
                                      
                            END     
                                
                        DELETE  FROM dbo.SettlementDetail
                        WHERE   PaymentTransactionId = @TransactionId             
                                                              
                    END            
                            
            END         
                    
    END

Go

--------------------------------------------------------------------------------------------------------------------
