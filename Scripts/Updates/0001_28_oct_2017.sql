ALTER PROCEDURE [dbo].[PROC_CreditNote_MASTER]
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
      @v_CN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
      @v_Proc_Type INT
	
    )
AS
    BEGIN
        IF @V_PROC_TYPE = 1
            BEGIN
			
--                select  @v_CreditNote_No = isnull(max(CreditNote_No), 0) + 1
--                from    CreditNote_Master
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
                          INV_Date
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
                          @v_INV_Date
                        )
                UPDATE  CN_SERIES
                SET     CURRENT_USED = @V_CreditNote_No
                WHERE   DIV_ID = @V_Division_ID


				DECLARE @Remarks VARCHAR(250)

				  SET @Remarks = 'Credit against CreditNote No- ' +  @V_CreditNote_Code+CAST(@V_CreditNote_No AS VARCHAR(50))
        
            EXECUTE Proc_CustomerLedger_Insert @v_CN_CustId,
                @v_CN_Amount, 0, @Remarks, @V_Division_ID,
                @V_CreditNote_ID, 17, @V_Created_BY


            END
        IF @V_PROC_TYPE = 2
            BEGIN
                UPDATE  dbo.CreditNote_Master
                SET     CreditNote_No = @V_CreditNote_No ,
                        CreditNote_Code = @V_CreditNote_Code ,
                        CreditNote_Date = @v_CreditNote_Date ,
                        Remarks = @V_Remarks ,
                        INVId = @V_INV_Id ,
                        Created_BY = @V_Created_BY ,
                        Creation_Date = @V_Creation_Date ,
                        Modified_By = @V_Modified_By ,
                        Modification_Date = @V_Modification_Date ,
                        CN_Amount = @v_CN_Amount ,
                        CN_CustId = @v_CN_CustId ,
                        Division_ID = @V_Division_ID ,
                        INV_No = @v_INV_No ,
                        INV_Date = @v_INV_Date
                WHERE   CreditNote_ID = @V_CreditNote_ID
            END
    END

-------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------

	ALTER PROCEDURE [dbo].[PROC_OUTSIDE_SALE_MASTER_SALE_NEW]
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
      @v_GROSS_AMOUNT DECIMAL(18, 3) ,
      @v_VAT_AMOUNT DECIMAL(18, 3) ,
      @v_NET_AMOUNT DECIMAL(18, 3) ,
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
      @V_MODE INT
    )
AS
    BEGIN                          
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
						  TRANSPORT,
                          SHIPP_ADD_ID ,
                          LR_NO ,
                          INV_TYPE
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
						  @V_TRANSPORT,
                          @v_SHIPP_ADD_ID ,
                          @v_LR_NO ,
                          @v_INV_TYPE 
                        ) 
						
						DECLARE @Remarks VARCHAR(250)
					  SET @Remarks = 'Sale against invoice No- ' +  @V_SI_CODE+CAST(@V_SI_NO AS VARCHAR(50))
            EXECUTE Proc_CustomerLedger_Insert @V_CUST_ID,
                0, @V_NET_AMOUNT, @Remarks, @V_Division_ID,
                @V_SI_ID, 16, @V_Created_BY	
						                         
            END   
        IF @V_MODE = 2
            BEGIN  
                UPDATE  SALE_INVOICE_MASTER
                SET     SI_CODE = @V_SI_CODE ,
                        SI_NO = @V_SI_NO ,
                        DC_GST_NO = @v_DC_NO ,
                        SI_DATE = @V_SI_DATE ,
                        CUST_ID = @V_CUST_ID ,
                        INVOICE_STATUS = @V_INVOICE_STATUS ,
                        REMARKS = @V_REMARKS ,
                        IS_SAMPLE = @V_IS_SAMPLE ,
                        DELIVERY_NOTE_NO = @V_DELIVERY_NOTE_NO ,
                        VAT_CST_PER = @V_VAT_CST_PER ,
                        PAYMENTS_REMARKS = @V_PAYMENTS_REMARKS ,
                        SALE_TYPE = @V_SALE_TYPE ,
                        GROSS_AMOUNT = @V_GROSS_AMOUNT ,
                        VAT_AMOUNT = @V_VAT_AMOUNT ,
                        NET_AMOUNT = @V_NET_AMOUNT ,
                        SAMPLE_ADDRESS = @V_SAMPLE_ADDRESS ,
                        MODIFIED_BY = @V_MODIFIED_BY ,
                        MODIFIED_DATE = @V_MODIFIED_DATE ,
                        DIVISION_ID = @V_DIVISION_ID ,
                        VEHICLE_NO = @V_VEHICLE_NO ,
                        SHIPP_ADD_ID = @v_SHIPP_ADD_ID ,
                        INV_TYPE = @v_INV_TYPE ,
                        LR_NO = @v_LR_NO
                WHERE   SI_ID = @V_SI_ID                        
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

-----------------------------------------------------------------------------------------------------------------------------------------------------------
ALTER PROC PROC_Cancel_Sale_Invoice
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
        DECLARE @V_Division_ID AS NUMERIC(18, 0)
        DECLARE @V_Created_BY AS VARCHAR(50)
        DECLARE @V_Si_NO AS VARCHAR(100)

        SELECT  @V_CUST_ID = CUST_ID ,
                @V_NET_AMOUNT = NET_AMOUNT ,
                @V_Division_ID = DIVISION_ID ,
                @V_Created_BY = CREATED_BY ,
                @V_Si_NO = SI_CODE + CAST(SI_NO AS VARCHAR(50))
        FROM    dbo.SALE_INVOICE_MASTER
        WHERE   SI_ID = @SI_ID

        DECLARE @Remarks VARCHAR(250)
        SET @Remarks = 'Amount Deducted against Cancel Invoice - ' + @V_Si_NO

        EXECUTE Proc_CustomerLedger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0,
            @Remarks, @V_Division_ID, @SI_ID, 16, @V_Created_BY	

    END

-----------------------------------------------------------------------Supplier Payment------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[SupplierPaymentTransaction](
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

--------------------------------------------------------

CREATE TABLE [dbo].[SupplierPM_Series](
	[DIV_ID] [numeric](18, 0) NULL,
	[PREFIX] [varchar](50) NULL,
	[START_NO] [numeric](18, 0) NULL,
	[END_NO] [numeric](18, 0) NULL,
	[CURRENT_USED] [numeric](18, 0) NULL,
	[IS_FINISHED] [char](1) NULL
) ON [PRIMARY]
---------------------------------------------------------

CREATE TABLE [dbo].[SupplierLedgerMaster](
	[LedgerId] [numeric](18, 0) NOT NULL,
	[AccountId] [numeric](18, 0) NULL,
	AmountInHand [numeric](18, 2) NULL,
	DivisionId BIGINT NULL
 CONSTRAINT [PK_SupplierLedgerMaster] PRIMARY KEY CLUSTERED 
(
	[LedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
-------------------------------------------------------------


CREATE TABLE SupplierSettlementDetail
    (
      PaymentTransactionId NUMERIC NOT NULL ,
      InvoicePaymentId NUMERIC NOT NULL ,
      MrnNo NUMERIC NOT NULL ,
      AmountSettled NUMERIC(18, 2) NOT NULL ,
      Remarks VARCHAR(200) ,
      CreatedBy VARCHAR(50) ,
      CreatedDate DATETIME ,
      DivisionId NUMERIC NOT NULL,
	  paymentId NUMERIC NOT NULL
    )
ON  [PRIMARY]

---------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[GET_SuppPaymentModule_No]
(
	@DIV_ID NUMERIC(18,0)
)
AS
BEGIN
	DECLARE @COUNT NUMERIC(18,0)


		SELECT @COUNT = COUNT(CURRENT_USED) FROM SupplierPM_Series WHERE 
		DIV_ID = @DIV_ID and IS_FINISHED = 'N'
	
	IF @COUNT = 0 
		BEGIN
			SELECT '-1','-1'
		END
	ELSE
		BEGIN
		
		SELECT @COUNT = COUNT(CURRENT_USED) FROM SupplierPM_Series WHERE IS_FINISHED = 'N'
		AND
		DIV_ID = @DIV_ID AND CURRENT_USED >= START_NO - 1 
		AND CURRENT_USED < END_NO
	
		if @count = 0 
			begin
				select '-2','-2'
			end 
		else
			begin
				SELECT  PREFIX,CURRENT_USED FROM SupplierPM_Series WHERE 
					DIV_ID = @DIV_ID AND IS_FINISHED ='N'
			END
		end 
END

---------------------------------------------------------------------------------------------------

CREATE PROCEDURE Proc_SupplierLedger_Insert
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
                    FROM    dbo.SupplierLedgerMaster
                    WHERE   AccountId = @AccountId )
            SELECT  @LedgerId = LedgerId
            FROM    dbo.SupplierLedgerMaster
            WHERE   AccountId = @AccountId
        ELSE
            BEGIN
                SELECT  @LedgerId = ISNULL(MAX(LedgerId), 0) + 1
                FROM    dbo.SupplierLedgerMaster	
                INSERT  INTO dbo.SupplierLedgerMaster
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
        UPDATE  dbo.SupplierLedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @AccountId
       
       -------entry in ledger detail
        DECLARE @LedgerDetailId AS NUMERIC
        SELECT  @LedgerDetailId = ISNULL(MAX(LedgerDetailId), 0) + 1
        FROM    SupplierLedgerDetail
       
        INSERT  INTO dbo.SupplierLedgerDetail
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

-------------------------------------------------------------------------------------------------------------

CREATE	 PROCEDURE [dbo].[ProcSupplierPaymentTransaction_Insert]
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
        FROM    dbo.SupplierPaymentTransaction          
        INSERT  INTO SupplierPaymentTransaction
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
        UPDATE  SupplierPM_Series
        SET     CURRENT_USED = CURRENT_USED + 1
        WHERE   DIV_ID = @DivisionId
                AND IS_FINISHED = 'N'
        ---update supplier ledger        
		---if payment status approved   than make entry in customer ledger      
        SET @Remarks = 'Payment release against -' + @PaymentTransactionNo
        IF @StatusId = 2
            EXECUTE Proc_SupplierLedger_Insert @AccountId,
                0,@TotalamountReceived,  @Remarks, @DivisionId,
                @PaymentTransactionId, 19, @CreatedBy
    END 


---------------------------------------------------------------------------------------------------------

CREATE PROC Proc_SupplierUpdatePaymentStauts
    (
      @PaymentTransactionId [numeric](18, 0) ,
      @CancellationCharges [numeric](18, 2) ,
      @StatusId NUMERIC    
    )
AS
    BEGIN  
        UPDATE  SupplierPaymentTransaction
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
        FROM    dbo.SupplierPaymentTransaction
        WHERE   PaymentTransactionId = @PaymentTransactionId
        
        ---update customer ledger
		---if payment status approved   than make entry in customer ledger
        DECLARE @Remarks VARCHAR(200) = 'Payment transfer against '
            + @PaymentTransactionNo
        IF @StatusId = 2
            EXECUTE Proc_SupplierLedger_Insert @AccountId,
                 0,@TotalamountReceived, @Remarks, @DivisionId,
                @PaymentTransactionId, 18, @CreatedBy
        
        ---if payment status bounced with cancellation charges than make entry in customer ledger      
        SET @Remarks = 'Payment Cancelation Charges against '
            + @PaymentTransactionNo
        IF @StatusId = 4
            AND @CancellationCharges > 0
            EXECUTE Proc_SupplierLedger_Insert @AccountId, 
                @CancellationCharges,0, @Remarks, @DivisionId,
                @PaymentTransactionId, 18, @CreatedBy
                
    END

------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE Proc_SupplierSettlementDetail_Insert
    (
      @PaymentTransactionId NUMERIC(18, 0) ,
      @PaymentId NUMERIC(18, 0) ,
      @MrnNo NUMERIC(18, 0) ,
      @AmountSettled NUMERIC(18, 2) ,
      @Remarks VARCHAR(200) ,
      @CreatedBy VARCHAR(50) ,      
      @DivisionId NUMERIC(18, 0)
    )
AS
    BEGIN
        
        INSERT  INTO SupplierSettlementDetail
                ( PaymentTransactionId ,
                  PaymentId ,
                  [MrnNo] ,
                  [AmountSettled] ,
                  [Remarks] ,
                  [CreatedBy] ,
                  [CreatedDate] ,
                  [DivisionId],
				  InvoicePaymentId
                )
        VALUES  ( @PaymentTransactionId ,
                  @PaymentId ,
                  @MrnNo ,
                  @AmountSettled ,
                  @Remarks ,
                  @CreatedBy ,
                  GETDATE() ,
                  @DivisionId
				   @PaymentId 
                )
                
-----------deduct settled amount from payment transaction table                
        UPDATE  dbo.SupplierPaymentTransaction
        SET     UndistributedAmount = UndistributedAmount - @AmountSettled
        WHERE   PaymentTransactionId = @PaymentTransactionId
    END

------------------------------------------------------------------------------------------------------------------

