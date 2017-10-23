CREATE TABLE [dbo].[BankMaster](
	[BankID] [int] NOT NULL,
	[BankName] [varchar](100) NULL,
	[BankAccountNo] [nvarchar](100) NULL,
	[IsActive] [bit] NULL
) ON [PRIMARY]


CREATE TABLE [dbo].[PaymentTypeMaster](
	[PaymentTypeId] [numeric](18, 0) NOT NULL,
	[PaymentTypeName] [varchar](50) NULL,
	[PaymentTypeDescription] [varchar](255) NULL,
	[CreatedBy] [numeric](18, 0) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsActive_bit] [bit] NULL,
	[IsApprovalRequired_bit] [bit] NULL,
 CONSTRAINT [PK_PAYMENT_TYPE_MASTER] PRIMARY KEY CLUSTERED 
(
	[PaymentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[PaymentTransaction](
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
	DivisionId BIGINT NULL,
	[StatusId] [int] NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[PM_Series](
	[DIV_ID] [numeric](18, 0) NULL,
	[PREFIX] [varchar](50) NULL,
	[START_NO] [numeric](18, 0) NULL,
	[END_NO] [numeric](18, 0) NULL,
	[CURRENT_USED] [numeric](18, 0) NULL,
	[IS_FINISHED] [char](1) NULL
) ON [PRIMARY]


CREATE TABLE [dbo].[CustomerLedgerMaster](
	[LedgerId] [numeric](18, 0) NOT NULL,
	[AccountId] [numeric](18, 0) NULL,
	AmountInHand [numeric](18, 2) NULL,
	DivisionId BIGINT NULL
 CONSTRAINT [PK_LEDGER_INVOICE_MASTER] PRIMARY KEY CLUSTERED 
(
	[LedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[CustomerLedgerDetail]
    (
      [LedgerDetailId] [numeric] NOT NULL ,
      [LedgerId] [numeric](18, 0) NOT NULL ,
      [CashIn] [numeric](18, 2) ,
      [CashOut] [numeric](18, 2) ,
      [Remarks] [varchar](500) NULL ,
      [TransactionId] [int] NULL ,
      [TransactionTypeId] [int] NULL ,
      [TransactionDate] [datetime] NULL ,
      CreatedBy VARCHAR(100) NULL ,
      CONSTRAINT [PK_SaleLedgerDetail] PRIMARY KEY CLUSTERED
        ( [LedgerDetailId] ASC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
               ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY]


CREATE TABLE CustomerSettlementDetail
    (
      PaymentTransactionId NUMERIC NOT NULL ,
      InvoicePaymentId NUMERIC NOT NULL ,
      InvoiceId NUMERIC NOT NULL ,
      AmountSettled NUMERIC(18, 2) NOT NULL ,
      Remarks VARCHAR(200) ,
      CreatedBy VARCHAR(50) ,
      CreatedDate DATETIME ,
      DivisionId NUMERIC NOT NULL
    )
ON  [PRIMARY]




---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[ProcPaymentTransaction_Insert]
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
                  BankDate      
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
                  @BankDate      
                )      
                  
        SET @ProcedureStatus = @PaymentTransactionId       
        
        ---increment series
        UPDATE  PM_SERIES
        SET     CURRENT_USED = CURRENT_USED + 1
        WHERE   DIV_ID = @DivisionId
                AND IS_FINISHED = 'N'
        
        ---update customer ledger        
		---if payment status approved   than make entry in customer ledger      
        SET @Remarks = 'Payment Recieved against ' + @PaymentTransactionNo
        IF @StatusId = 2
            EXECUTE Proc_CustomerLedger_Insert @AccountId,
                @TotalamountReceived, 0, @Remarks, @DivisionId,
                @PaymentTransactionId, 18, @CreatedBy
    END 


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
create PROCEDURE [dbo].[GET_PaymentModule_No]
(
	@DIV_ID NUMERIC(18,0)
)
AS
BEGIN
	DECLARE @COUNT NUMERIC(18,0)


		SELECT @COUNT = COUNT(CURRENT_USED) FROM PM_SERIES WHERE 
		DIV_ID = @DIV_ID and IS_FINISHED = 'N'
	
	IF @COUNT = 0 
		BEGIN
			SELECT '-1','-1'
		END
	ELSE
		BEGIN
		
		SELECT @COUNT = COUNT(CURRENT_USED) FROM PM_SERIES WHERE IS_FINISHED = 'N'
		AND
		DIV_ID = @DIV_ID AND CURRENT_USED >= START_NO - 1 
		AND CURRENT_USED < END_NO
	
		if @count = 0 
			begin
				select '-2','-2'
			end 
		else
			begin
				SELECT  PREFIX,CURRENT_USED FROM PM_SERIES WHERE 
					DIV_ID = @DIV_ID AND IS_FINISHED ='N'
			END
		end 
END







---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROC Proc_UpdatePaymentStauts
    (
      @PaymentTransactionId [numeric](18, 0) ,
      @CancellationCharges [numeric](18, 2) ,
      @StatusId NUMERIC    
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
        
        ----set parametrs
        SELECT  @AccountId = AccountId ,
                @TotalamountReceived = TotalAmountReceived ,
                @PaymentTransactionNo = PaymentTransactionNo ,
                @CreatedBy = CreatedBy ,
                @DivisionId = DivisionId
        FROM    dbo.PaymentTransaction
        WHERE   PaymentTransactionId = @PaymentTransactionId
        
        ---update customer ledger
		---if payment status approved   than make entry in customer ledger
        DECLARE @Remarks VARCHAR(200) = 'Payment Recieved against '
            + @PaymentTransactionNo
        IF @StatusId = 2
            EXECUTE Proc_CustomerLedger_Insert @AccountId,
                @TotalamountReceived, 0, @Remarks, @DivisionId,
                @PaymentTransactionId, 18, @CreatedBy
        
        ---if payment status bounced with cancellation charges than make entry in customer ledger      
        SET @Remarks = 'Payment Cancelation Charges against '
            + @PaymentTransactionNo
        IF @StatusId = 4
            AND @CancellationCharges > 0
            EXECUTE Proc_CustomerLedger_Insert @AccountId, 0,
                @CancellationCharges, @Remarks, @DivisionId,
                @PaymentTransactionId, 18, @CreatedBy
                
    END


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE Proc_CustomerLedger_Insert
    (
      @AccountId NUMERIC(18, 0) ,
      @CashIn NUMERIC(18, 2) ,
      @CashOut NUMERIC(18, 2) ,
      @Remarks VARCHAR(500) ,
      @DivisionId NUMERIC(18, 0) ,
      @TransactionId INT ,
      @TransactionTypeId INT ,
      --@TransactionDate DATETIME ,
      @CreatedBy VARCHAR(100)
    )
AS
    BEGIN
    
        DECLARE @LedgerId NUMERIC(18, 0) 
        
    ---------if customer ledger not exists then make entry
        IF EXISTS ( SELECT  AccountId
                    FROM    dbo.CustomerLedgerMaster
                    WHERE   AccountId = @AccountId )
            SELECT  @LedgerId = LedgerId
            FROM    dbo.CustomerLedgerMaster
            WHERE   AccountId = @AccountId
        ELSE
            BEGIN
                SELECT  @LedgerId = ISNULL(MAX(LedgerId), 0) + 1
                FROM    dbo.CustomerLedgerMaster	
                INSERT  INTO dbo.CustomerLedgerMaster
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
        UPDATE  dbo.CustomerLedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @AccountId
       
       -------entry in ledger detail
        DECLARE @LedgerDetailId AS NUMERIC
        SELECT  @LedgerDetailId = ISNULL(MAX(LedgerDetailId), 0) + 1
        FROM    CustomerLedgerDetail
       
        INSERT  INTO dbo.CustomerLedgerDetail
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
                  GETDATE() , -- TransactionDate - datetime
                  @CreatedBy  -- CreatedBy - varchar(100)
                )
    END 


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROC Proc_UpdatePaymentStauts
    (
      @PaymentTransactionId [numeric](18, 0) ,
      @CancellationCharges [numeric](18, 2) ,
      @StatusId NUMERIC    
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
        
        ----set parametrs
        SELECT  @AccountId = AccountId ,
                @TotalamountReceived = TotalAmountReceived ,
                @PaymentTransactionNo = PaymentTransactionNo ,
                @CreatedBy = CreatedBy ,
                @DivisionId = DivisionId
        FROM    dbo.PaymentTransaction
        WHERE   PaymentTransactionId = @PaymentTransactionId
        
        ---update customer ledger
		---if payment status approved   than make entry in customer ledger
        DECLARE @Remarks VARCHAR(200) = 'Payment Recieved against '
            + @PaymentTransactionNo
        IF @StatusId = 2
            EXECUTE Proc_CustomerLedger_Insert @AccountId,
                @TotalamountReceived, 0, @Remarks, @DivisionId,
                @PaymentTransactionId, 18, @CreatedBy
        
        ---if payment status bounced with cancellation charges than make entry in customer ledger      
        SET @Remarks = 'Payment Cancelation Charges against '
            + @PaymentTransactionNo
        IF @StatusId = 4
            AND @CancellationCharges > 0
            EXECUTE Proc_CustomerLedger_Insert @AccountId, 0,
                @CancellationCharges, @Remarks, @DivisionId,
                @PaymentTransactionId, 18, @CreatedBy
                
    END


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE Proc_CustomerSettlementDetail_Insert
    (
      @PaymentTransactionId NUMERIC(18, 0) ,
      @PaymentId NUMERIC(18, 0) ,
      @InvoiceId NUMERIC(18, 0) ,
      @AmountSettled NUMERIC(18, 2) ,
      @Remarks VARCHAR(200) ,
      @CreatedBy VARCHAR(50) ,      
      @DivisionId NUMERIC(18, 0)
    )
AS
    BEGIN
        
        INSERT  INTO CustomerSettlementDetail
                ( PaymentTransactionId ,
                  PaymentId ,
                  [InvoiceId] ,
                  [AmountSettled] ,
                  [Remarks] ,
                  [CreatedBy] ,
                  [CreatedDate] ,
                  [DivisionId]
                )
        VALUES  ( @PaymentTransactionId ,
                  @PaymentId ,
                  @InvoiceId ,
                  @AmountSettled ,
                  @Remarks ,
                  @CreatedBy ,
                  GETDATE() ,
                  @DivisionId
                )
                
-----------deduct settled amount from payment transaction table                
        UPDATE  dbo.PaymentTransaction
        SET     UndistributedAmount = UndistributedAmount - @AmountSettled
        WHERE   PaymentTransactionId = @PaymentTransactionId
    END

