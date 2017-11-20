--EXEC PROC_GET_SUPPLIER_LEDGER_DETAILS '2017-08-01','2017-11-13',23

ALTER PROC PROC_GET_SUPPLIER_LEDGER_DETAILS
    (
      @Fromdate AS DATETIME = NULL ,
      @ToDate AS DATETIME = NULL ,
      @AccountId AS INT = 0
    )
AS
    BEGIN
-----------------------------------Party Details With Opening Balance-------------------------------------------------------------------- 

        DECLARE @OpeningBalance NUMERIC(18, 2)
        SET @OpeningBalance = ( SELECT  ISNULL(SUM(CASHIN - CASHOUT), 0) AS OpeningBalance
                                FROM    dbo.SupplierLedgerMaster
                                        LEFT OUTER  JOIN dbo.SupplierLedgerDetail ON dbo.SupplierLedgerMaster.LedgerId = dbo.SupplierLedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TRANSACTIONDATE, 101) AS DATETIME) < CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                        INNER  JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.SupplierLedgerMaster.AccountId
                                WHERE   SupplierLedgerMaster.AccountId = @AccountId
                              )   
     
-----------------------------------Party Details With Opening Balance-------------------------------------------------------------------- 
        IF OBJECT_ID('tempdb..#SupplierLedgertemp') IS NOT NULL
            DROP TABLE #SupplierLedgertemp        
        CREATE TABLE #SupplierLedgertemp
            (
              LedgerId NUMERIC(18, 0) ,
              TRANSACTIONDATE DATETIME ,
              AccId NUMERIC(18, 0) ,
              AmountInHand NUMERIC(18, 2) ,
              CASHIN NUMERIC(18, 2) ,
              CASHOUT NUMERIC(18, 2) ,
              REMARKS NVARCHAR(500) ,
              TransactionTypeId NUMERIC(18, 0) ,
              ClosingBalance NUMERIC(18, 2) ,
              CheckgBalance NVARCHAR(500)
            ) 

        DECLARE @LedgerId INT ,
            @TRANSACTIONDATE DATETIME ,
            @AccId NUMERIC(18, 0) ,
            @AmountInHand NUMERIC(18, 2) ,
            @CASHIN NUMERIC(18, 2) ,
            @CASHOUT NUMERIC(18, 2) ,
            @REMARKS NVARCHAR(500) ,
            @TransactionTypeId NUMERIC(18, 0)
					 
        DECLARE SupplierLedger_cursor CURSOR
        FOR
            SELECT  LedgerId ,
                    TRANSACTIONDATE ,
                    AccountId ,
                    AmountInHand ,
                    CASH_IN ,
                    CASH_OUT ,
                    REMARKS ,
                    TransactionTypeId
            FROM    ( SELECT    dbo.SupplierLedgerMaster.LedgerId ,
                                TRANSACTIONDATE ,
                                AccountId ,
                                AmountInHand ,
                                ISNULL(CASHIN, 0) AS CASH_IN ,
                                ISNULL(CASHOUT, 0) AS CASH_OUT ,
                                REMARKS ,
                                TransactionTypeId
                      FROM      dbo.SupplierLedgerMaster
                                INNER JOIN dbo.SupplierLedgerDetail ON dbo.SupplierLedgerMaster.LedgerId = dbo.SupplierLedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TransactionDate, 101) AS DATETIME) BETWEEN CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                                              AND
                                                              CAST(CONVERT(VARCHAR, @ToDate, 101) AS DATETIME)
                      WHERE     dbo.SupplierLedgerMaster.AccountId = @AccountId
                    ) tb
            ORDER BY TRANSACTIONDATE


        OPEN SupplierLedger_cursor

        FETCH NEXT FROM SupplierLedger_cursor INTO @LedgerId, @TRANSACTIONDATE,
            @AccId, @AmountInHand, @CASHIN, @CASHOUT, @REMARKS,
            @TransactionTypeId
        WHILE @@FETCH_STATUS = 0
            BEGIN




                INSERT  INTO #SupplierLedgertemp
                        SELECT  @LedgerId ,
                                @TRANSACTIONDATE ,
                                @AccId ,
                                @AmountInHand ,
                                @CASHIN ,
                                @CASHOUT ,
                                @REMARKS ,
                                @TransactionTypeId ,
                                @OpeningBalance - @CASHOUT + @CASHIN ,
                                ( CAST(@OpeningBalance AS VARCHAR(50)) + '  -'
                                  + CAST(@CASHOUT AS VARCHAR(50)) + '  +'
                                  + CAST(@CASHIN AS VARCHAR(50)) )


                SET @OpeningBalance = @OpeningBalance - @CASHOUT + @CASHIN   
                FETCH NEXT FROM SupplierLedger_cursor INTO @LedgerId,
                    @TRANSACTIONDATE, @AccId, @AmountInHand, @CASHIN, @CASHOUT,
                    @REMARKS, @TransactionTypeId 


            END
        CLOSE SupplierLedger_cursor
        DEALLOCATE SupplierLedger_cursor

        SELECT  LedgerId ,
                TRANSACTIONDATE ,
                AccId ,
                AmountInHand ,
                CASHIN ,
                CASHOUT ,
                REMARKS ,
                ClosingBalance AS ClosingBalance
        FROM    #SupplierLedgertemp
        ORDER BY TRANSACTIONDATE


    END