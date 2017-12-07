ALTER PROC Proc_AddOpeningBalance
    (
      @AccountId NUMERIC(18, 0) ,
      @Amount NUMERIC(18, 2) ,
      @DivisionId NUMERIC(18, 0) ,
      @CreatedBy NVARCHAR(50) ,
      @Openingdate DATETIME  ,
      @Type BIGINT
    )
AS
    BEGIN

        UPDATE  dbo.ACCOUNT_MASTER
        SET     OPENING_BAL = @Amount
        WHERE   ACC_ID = @AccountId

        DECLARE @OPENINGBALANCEID NUMERIC(18, 0)
     

        SET @OPENINGBALANCEID = ( SELECT    ISNULL(COUNT(OPENINGBALANCEID), 0)
                                            + 1 AS Id
                                  FROM      OPENINGBALANCE
                                )

        INSERT   OPENINGBALANCE
        VALUES  ( -@OPENINGBALANCEID, @AccountId, @Amount, @Openingdate )
          
        DECLARE @Remarks VARCHAR(200) 
        SET @Remarks = 'Account Opening Balance Against Opening Balance Id='
            + CAST(@OPENINGBALANCEID AS VARCHAR(100))


	



        IF @TYPE = 1
            BEGIN
               EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @Amount, @Remarks, @DivisionId,
                            @OPENINGBALANCEID, 20, @CreatedBy
            END

        IF @TYPE = 2
            BEGIN
                  EXECUTE Proc_Ledger_Insert @AccountId,
                            @Amount, 0, @Remarks, @DivisionId,
                            @OPENINGBALANCEID, 20, @CreatedBy
           END


    END 

------------------------------------------------------------------------------------------------------------------------------------------------------

