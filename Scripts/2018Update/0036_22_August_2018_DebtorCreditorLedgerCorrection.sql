INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0036_22_August_2018_DebtorCreditorLedgerCorrection' ,
          GETDATE()
        )
Go

Alter PROC [dbo].[PROC_GET_CUSTOMER_LEDGER_DETAILS]
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
                                FROM    dbo.LedgerMaster
                                        LEFT OUTER  JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TRANSACTIONDATE, 101) AS DATETIME) < CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                        INNER  JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.LedgerMaster.AccountId
                                WHERE   LedgerMaster.AccountId = @AccountId
                              )     
-----------------------------------Party Details With Opening Balance--------------------------------------------------------------------       
      
        IF OBJECT_ID('tempdb..#Ledgertemp') IS NOT NULL
            DROP TABLE #Ledgertemp     
      
        CREATE TABLE #Ledgertemp
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
      
      
        DECLARE Ledger_cursor CURSOR
        FOR
            SELECT  LedgerId ,
                    TRANSACTIONDATE ,
                    AccountId ,
                    AmountInHand ,
                    CASH_IN ,
                    CASH_OUT ,
                    REMARKS ,
                    TransactionTypeId
            FROM    ( SELECT    dbo.LedgerMaster.LedgerId ,
                                TRANSACTIONDATE ,
                                LedgerMaster.AccountId ,    
                                --LedgerMaster.AmountInHand ,  
                                ( SELECT    SUM(ISNULL(CASHIN, 0))
                                            - SUM(ISNULL(CASHOUT, 0))
                                  FROM      dbo.LedgerDetail
                                  WHERE     LedgerId = dbo.LedgerMaster.LedgerId
                                ) AS AmountInHand ,
                                ISNULL(CASHIN, 0) AS CASH_IN ,
                                ISNULL(CASHOUT, 0) AS CASH_OUT ,
                                LedgerDetail.REMARKS ,
                                TransactionTypeId
                      FROM      dbo.LedgerMaster
                                INNER JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TransactionDate, 101) AS DATETIME) 
                                                              BETWEEN CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                                              AND CAST(CONVERT(VARCHAR, @ToDate, 101) AS DATETIME)
                      WHERE     dbo.LedgerMaster.AccountId = @AccountId
                      EXCEPT
                      SELECT    dbo.LedgerMaster.LedgerId ,
                                TRANSACTIONDATE ,
                                LedgerMaster.AccountId ,    
                                --LedgerMaster.AmountInHand ,  
                                ( SELECT    SUM(ISNULL(CASHIN, 0))
                                            - SUM(ISNULL(CASHOUT, 0))
                                  FROM      dbo.LedgerDetail
                                  WHERE     LedgerId = dbo.LedgerMaster.LedgerId
                                ) AS AmountInHand ,
                                ISNULL(CASHIN, 0) AS CASH_IN ,
                                ISNULL(CASHOUT, 0) AS CASH_OUT ,
                                LedgerDetail.REMARKS ,
                                TransactionTypeId
                      FROM      dbo.LedgerMaster
                                INNER JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TransactionDate, 101) AS DATETIME) 
                                                              BETWEEN CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                                              AND CAST(CONVERT(VARCHAR, @ToDate, 101) AS DATETIME)
								LEFT OUTER JOIN dbo.SALE_INVOICE_MASTER sim ON sim.SI_ID = dbo.LedgerDetail.TransactionId
                                                              AND dbo.LedgerDetail.TransactionTypeId = 16
                      WHERE     dbo.LedgerMaster.AccountId = @AccountId
								AND sim.SI_ID IN (
                                SELECT  SI_ID
                                FROM    dbo.SALE_INVOICE_MASTER
                                WHERE   INVOICE_STATUS = 4 )
                      EXCEPT
                      SELECT    dbo.LedgerMaster.LedgerId ,
                                TRANSACTIONDATE ,
                                LedgerMaster.AccountId ,    
                                --LedgerMaster.AmountInHand ,  
                                ( SELECT    SUM(ISNULL(CASHIN, 0))
                                            - SUM(ISNULL(CASHOUT, 0))
                                  FROM      dbo.LedgerDetail
                                  WHERE     LedgerId = dbo.LedgerMaster.LedgerId
                                ) AS AmountInHand ,
                                ISNULL(CASHIN, 0) AS CASH_IN ,
                                ISNULL(CASHOUT, 0) AS CASH_OUT ,
                                LedgerDetail.REMARKS ,
                                TransactionTypeId
                      FROM      dbo.LedgerMaster
                                INNER JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TransactionDate, 101) AS DATETIME) 
                                                              BETWEEN CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                                              AND CAST(CONVERT(VARCHAR, @ToDate, 101) AS DATETIME)
                                LEFT OUTER JOIN dbo.PaymentTransaction pt ON pt.PaymentTransactionId = dbo.LedgerDetail.TransactionId
                                                              AND dbo.LedgerDetail.TransactionTypeId IN (18,19,21,22,23)
                      WHERE     dbo.LedgerMaster.AccountId = @AccountId
                                AND pt.PaymentTransactionId IN (
                                SELECT  PaymentTransactionId
                                FROM    dbo.PaymentTransaction
                                WHERE   statusid = 3 )
                    ) tb
            ORDER BY TRANSACTIONDATE      
      
        OPEN Ledger_cursor      
      
        FETCH NEXT FROM Ledger_cursor INTO @LedgerId, @TRANSACTIONDATE, @AccId,
            @AmountInHand, @CASHIN, @CASHOUT, @REMARKS, @TransactionTypeId     
      
        WHILE @@FETCH_STATUS = 0
            BEGIN      
      
                INSERT  INTO #Ledgertemp
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
      
                FETCH NEXT FROM Ledger_cursor INTO @LedgerId, @TRANSACTIONDATE,
                    @AccId, @AmountInHand, @CASHIN, @CASHOUT, @REMARKS,
                    @TransactionTypeId    
      
            END     
      
        CLOSE Ledger_cursor      
      
        DEALLOCATE Ledger_cursor     
      
        SELECT  LedgerId ,
                TRANSACTIONDATE ,
                AccId ,
                AmountInHand AS AmountInHand ,
                CASHIN ,
                CASHOUT ,
                REMARKS ,
                ClosingBalance AS ClosingBalance
        FROM    #Ledgertemp
        ORDER BY TRANSACTIONDATE     
           
END 

Go


Alter PROC [dbo].[PROC_GET_SUPPLIER_LEDGER_DETAILS]
    (
      @Fromdate AS DATETIME = NULL ,
      @ToDate AS DATETIME = NULL ,
      @AccountId AS INT = 0        
    )
AS
    BEGIN        
 -----------------Party Details With Opening Balance--------------------------------------------------------------------         
        
        DECLARE @OpeningBalance NUMERIC(18, 2)        
        
        SET @OpeningBalance = ( SELECT  ISNULL(SUM(CASHIN - CASHOUT), 0) AS OpeningBalance
                                FROM    dbo.LedgerMaster
                                        LEFT OUTER  JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TRANSACTIONDATE, 101) AS DATETIME) < CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                        INNER  JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.LedgerMaster.AccountId
                                WHERE   LedgerMaster.AccountId = @AccountId
                              )           
        
-----------------------------------Party Details With Opening Balance--------------------------------------------------------------------         
       
        IF OBJECT_ID('tempdb..#Ledgertemp') IS NOT NULL
            DROP TABLE #Ledgertemp          
        
        CREATE TABLE #Ledgertemp
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
        
        DECLARE Ledger_cursor CURSOR
        FOR
            SELECT  LedgerId ,
                    TRANSACTIONDATE ,
                    AccountId ,
                    AmountInHand ,
                    CASH_IN ,
                    CASH_OUT ,
                    REMARKS ,
                    TransactionTypeId
            FROM    ( SELECT    dbo.LedgerMaster.LedgerId ,
                                CASE WHEN dbo.LedgerDetail.TransactionTypeId = 2
                                     THEN mrwpm.Received_Date
                                     WHEN dbo.LedgerDetail.TransactionTypeId = 3
                                     THEN mrapm.Receipt_Date
                                     ELSE TRANSACTIONDATE
                                END AS TRANSACTIONDATE ,
                                LedgerMaster.AccountId ,  
                                --LedgerMaster.AmountInHand ,
                                (SELECT sum(ISNULL(CASHIN, 0)) - sum(ISNULL(CASHOUT, 0)) FROM dbo.LedgerDetail WHERE LedgerId = dbo.LedgerMaster.LedgerId) AS AmountInHand,
                                ISNULL(CASHIN, 0) AS CASH_IN ,
                                ISNULL(CASHOUT, 0) AS CASH_OUT ,
                                dbo.LedgerDetail.REMARKS ,
                                TransactionTypeId
                      FROM      dbo.LedgerMaster
                                INNER JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TransactionDate, 101) AS DATETIME) BETWEEN CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                                              AND
                                                              CAST(CONVERT(VARCHAR, @ToDate, 101) AS DATETIME)
                                LEFT JOIN MATERIAL_RECIEVED_WITHOUT_PO_MASTER mrwpm ON mrwpm.Received_ID = dbo.LedgerDetail.TransactionId
                                                              AND dbo.LedgerDetail.TransactionTypeId = 2
                                LEFT JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER mrapm ON mrapm.Receipt_ID = dbo.LedgerDetail.TransactionId
                                                              AND dbo.LedgerDetail.TransactionTypeId = 3
                      WHERE     dbo.LedgerMaster.AccountId = @AccountId
                      EXCEPT
                      SELECT    dbo.LedgerMaster.LedgerId ,
                                dbo.LedgerDetail.TRANSACTIONDATE ,
                                LedgerMaster.AccountId ,  
                                --LedgerMaster.AmountInHand ,
                                (SELECT sum(ISNULL(CASHIN, 0)) - sum(ISNULL(CASHOUT, 0)) FROM dbo.LedgerDetail WHERE LedgerId = dbo.LedgerMaster.LedgerId) AS AmountInHand,
                                ISNULL(CASHIN, 0) AS CASH_IN ,
                                ISNULL(CASHOUT, 0) AS CASH_OUT ,
                                dbo.LedgerDetail.REMARKS ,
                                TransactionTypeId
                      FROM      dbo.LedgerMaster
                                INNER JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TransactionDate, 101) AS DATETIME) BETWEEN CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                                              AND
                                                              CAST(CONVERT(VARCHAR, @ToDate, 101) AS DATETIME)
                                LEFT OUTER JOIN dbo.PaymentTransaction pt ON pt.PaymentTransactionId = dbo.LedgerDetail.TransactionId
                                                              AND dbo.LedgerDetail.TransactionTypeId IN (18,19,21,22,23)
                      WHERE     dbo.LedgerMaster.AccountId = @AccountId
								AND pt.PaymentTransactionId IN (
                                SELECT  PaymentTransactionId
                                FROM    dbo.PaymentTransaction
                                WHERE   statusid = 3 )
                    ) tb
            ORDER BY TRANSACTIONDATE        
        
        OPEN Ledger_cursor            
        
        FETCH NEXT FROM Ledger_cursor INTO @LedgerId, @TRANSACTIONDATE, @AccId,
            @AmountInHand, @CASHIN, @CASHOUT, @REMARKS, @TransactionTypeId        
        
        WHILE @@FETCH_STATUS = 0
            BEGIN        
        
                INSERT  INTO #Ledgertemp
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
        
                FETCH NEXT FROM Ledger_cursor INTO @LedgerId, @TRANSACTIONDATE,
                    @AccId, @AmountInHand, @CASHIN, @CASHOUT, @REMARKS,
                    @TransactionTypeId         
        
            END  
        
        CLOSE Ledger_cursor        
        DEALLOCATE Ledger_cursor        
        
        SELECT  LedgerId ,
                TRANSACTIONDATE ,
                AccId ,
                AmountInHand ,
                CASHIN ,
                CASHOUT ,
                REMARKS ,
                ClosingBalance AS ClosingBalance
        FROM    #Ledgertemp
        ORDER BY TRANSACTIONDATE 
        
    END

Go