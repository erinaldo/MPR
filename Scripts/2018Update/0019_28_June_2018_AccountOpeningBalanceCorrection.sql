
INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0019_28_June_2018_AccountOpeningBalanceCorrection' ,
          GETDATE()
        )
Go

CREATE PROC [dbo].Proc_CheckOpeningBalance
    (
      @AccountId NUMERIC(18, 0)
    )
AS
    BEGIN
        SELECT  *
        FROM    OPENINGBALANCE
        WHERE   FkAccountId = @AccountId
    
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
            
                DECLARE @openingCount INT = 0 
            
                SELECT  @openingCount = COUNT(*)
                FROM    OPENINGBALANCE
                WHERE   FkAccountId = @AccountId
				
                IF ( @openingCount = 0 )
                    BEGIN
                        SELECT TOP 1
                                @Openingdate = financialyear_dt
                        FROM    Company_Master      
                     
      
                        SET @OpeningBalanceId = -( SELECT   ISNULL(COUNT(OPENINGBALANCEID),
                                                              0) + 1 AS Id
                                                   FROM     OPENINGBALANCE
                                                 )      
                                              
                        INSERT  OPENINGBALANCE
                        VALUES  ( @OpeningBalanceId, @AccountId, @Amount,
                                  @Openingdate, @type )        
      
                        SET @Remarks = 'Account Opening Balance as on Date: '
                            + CAST(CONVERT(VARCHAR(20), @Openingdate, 106) AS VARCHAR(30))        
      
                        IF @TYPE = 1
                            BEGIN     
                        
                                UPDATE  dbo.ACCOUNT_MASTER
                                SET     OPENING_BAL = @Amount ,
                                        OPENING_BAL_TYPE = 'Dr'
                                WHERE   ACC_ID = @AccountId        
      
                                EXECUTE Proc_Ledger_InsertOpening @AccountId,
                                    0, @Amount, @Remarks, @DivisionId,
                                    @OpeningBalanceId, @TransactionId,
                                    @Openingdate, @CreatedBy       
                            END        
      
                        IF @TYPE = 2
                            BEGIN     
                        
                                UPDATE  dbo.ACCOUNT_MASTER
                                SET     OPENING_BAL = @Amount ,
                                        OPENING_BAL_TYPE = 'Cr'
                                WHERE   ACC_ID = @AccountId     
                               
                                EXECUTE Proc_Ledger_InsertOpening @AccountId,
                                    @Amount, 0, @Remarks, @DivisionId,
                                    @OpeningBalanceId, @TransactionId,
                                    @Openingdate, @CreatedBy        
                            END 
                        
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