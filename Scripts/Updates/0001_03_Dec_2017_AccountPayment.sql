ALTER TABLE dbo.PM_Series ADD PM_TYPE numeric(18,0)  NULL DEFAULT 0
ALTER TABLE dbo.PaymentTransaction ADD PM_TYPE numeric(18,0)  NULL DEFAULT 0
---------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[GET_PaymentModule_No]
(
	@DIV_ID NUMERIC(18,0),
	@PM_TYPE  numeric(18,0)
)
AS
BEGIN
	DECLARE @COUNT NUMERIC(18,0)
		SELECT @COUNT = COUNT(CURRENT_USED) FROM PM_SERIES WHERE 
		DIV_ID = @DIV_ID and IS_FINISHED = 'N' AND @PM_TYPE=PM_TYPE
	
	IF @COUNT = 0 
		BEGIN
			SELECT '-1','-1'
		END
	ELSE
		BEGIN
		
		SELECT @COUNT = COUNT(CURRENT_USED) FROM PM_SERIES WHERE IS_FINISHED = 'N'
		AND
		DIV_ID = @DIV_ID AND CURRENT_USED >= START_NO - 1 
		AND CURRENT_USED < END_NO AND @PM_TYPE=PM_TYPE
	
		if @count = 0 
			begin
				select '-2','-2'
			end 
		else
			begin
				SELECT  PREFIX,CURRENT_USED FROM PM_SERIES WHERE 
					DIV_ID = @DIV_ID AND IS_FINISHED ='N' AND @PM_TYPE=PM_TYPE
			END
		end 
END

---------------------------------------------------------------------------------------------------------------------
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
	  @PM_TYPE  numeric(18,0)=NULL,
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
                  BankDate,
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
                  @BankDate,
				  @PM_TYPE      
                )      
        SET @ProcedureStatus = @PaymentTransactionId       
        ---increment series
        UPDATE  PM_SERIES
        SET     CURRENT_USED = CURRENT_USED + 1
        WHERE   DIV_ID = @DivisionId
                AND IS_FINISHED = 'N' AND PM_TYPE=@PM_TYPE
        ---update customer ledger        
		---if payment status approved   than make entry in customer ledger      
        SET @Remarks = 'Payment Recieved against ' + @PaymentTransactionNo
        IF @StatusId = 2
            EXECUTE Proc_Ledger_Insert @AccountId,
                @TotalamountReceived, 0, @Remarks, @DivisionId,
                @PaymentTransactionId, 18, @CreatedBy
    END 