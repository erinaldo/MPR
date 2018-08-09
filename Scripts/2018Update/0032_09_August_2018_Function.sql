INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0032_09_August_2018_Function' ,
          GETDATE()
        )
Go

---------------------------------------------------------------------------------------------------------------------------------------------

ALTER FUNCTION [dbo].[Get_AllGroupOpening_as_on_date]
    (
      @agID NUMERIC ,
      @Fromdate DATETIME        
        
    )
RETURNS NUMERIC(18, 2)
AS
    BEGIN
        DECLARE @OpeningBalance NUMERIC(18, 2)        
        
        DECLARE @StartOpeningBalance NUMERIC(18, 2)  
        SET @StartOpeningBalance = 0        
        
        --SET @StartOpeningBalance = ISNULL(( SELECT  SUM(amount)
        --                                    FROM    ( SELECT  CASE
        --                                                      WHEN type = 1
        --                                                      THEN -OpeningAmount
        --                                                      ELSE OpeningAmount
        --                                                      END AS amount
        --                                              FROM    dbo.OpeningBalance
        --                                              WHERE   FkAccountId IN (
        --                                                      SELECT
        --                                                      ACC_ID
        --                                                      FROM
        --                                                      dbo.ACCOUNT_MASTER
        --                                                      WHERE
        --                                                      AG_ID = @agID )
        --                                                      AND CAST(OpeningDate AS DATE) = CAST(@Fromdate AS DATE)
        --                                            ) tb
        --                                  ), 0)       
        
        --PRINT @StartOpeningBalance
        --PRINT '1'
        
        SET @OpeningBalance = ISNULL(( SELECT  
										--ISNULL(SUM(CASHIN - CASHOUT), 0) AS OpeningBalance
                                         CASE WHEN AccountId <> 10073
                                             THEN ISNULL(SUM(ISNULL(CASHIN,0) - ISNULL(CASHOUT,0)),
                                                         0)
                                             ELSE CASE WHEN AccountId = 10073
                                                            AND ( ISNULL(SUM(ISNULL(CASHIN,0) - ISNULL(CASHOUT,0)), 0) ) < 0
                                                       THEN ISNULL(SUM(ISNULL(CASHIN,0) - ISNULL(CASHOUT,0)), 0)
                                                       WHEN AccountId = 10073
                                                            AND ( ISNULL(SUM(ISNULL(CASHIN,0) - ISNULL(CASHOUT,0)), 0) ) > 0
                                                       THEN 0.00 
                                                  END
                                        END AS OpeningBalance
                                FROM    dbo.LedgerMaster
                                        JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(TRANSACTIONDATE AS DATE) < CAST(@Fromdate AS DATE)
                                        INNER  JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.LedgerMaster.AccountId
                                WHERE   LedgerMaster.AccountId IN (
                                        SELECT  ACC_ID
                                        FROM    dbo.ACCOUNT_MASTER
                                        WHERE   AG_ID = @agID )
                                GROUP BY AccountId
                              ), 0)
                              
        --PRINT @OpeningBalance
        --PRINT '2'
        SET @OpeningBalance = ISNULL(@StartOpeningBalance,0) + ISNULL(@OpeningBalance,0)       
   
		--PRINT 'end'
        return @OpeningBalance        
    END
	Go
---------------------------------------------------------------------------------------------------------------------------------------------
ALTER FUNCTION [dbo].[Get_GroupOpening_as_on_date]
        (
         
          @Fromdate DATETIME        
        
        )
RETURNS NUMERIC(18, 2)
    AS
        BEGIN
            DECLARE @OpeningBalance NUMERIC(18, 2)        
        
            DECLARE @StartOpeningBalance NUMERIC(18, 2)  
            SET @StartOpeningBalance = 0        
        
            --SET @StartOpeningBalance = ISNULL((SELECT SUM(amount) FROM ( SELECT  CASE WHEN type = 1
            --                                                 THEN -OpeningAmount
            --                                                 ELSE OpeningAmount
            --                                            END AS amount
            --                                    FROM    dbo.OpeningBalance
            --                                    WHERE   FkAccountId IN (
            --                                            SELECT
            --                                                  ACC_ID
            --                                            FROM  dbo.ACCOUNT_MASTER
            --                                            WHERE AG_ID IN ( 3, 6 ) )
            --                                            AND CAST(OpeningDate AS DATE) = CAST(@Fromdate AS DATE)
            --                                 )tb ), 0)       
        
            SET @OpeningBalance = ( SELECT  ISNULL(SUM(CASHIN - CASHOUT), 0) AS OpeningBalance
                                    FROM    dbo.LedgerMaster
                                            JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(TRANSACTIONDATE AS DATE) < CAST(@FromDate AS DATE)
                                            INNER  JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.LedgerMaster.AccountId
                                    WHERE   LedgerMaster.AccountId IN (
                                            SELECT  ACC_ID
                                            FROM    dbo.ACCOUNT_MASTER
                                            WHERE   AG_ID IN ( 3, 6 ) )
                                  )
            SET @OpeningBalance = @StartOpeningBalance + @OpeningBalance        
   
     
            RETURN @OpeningBalance        
        END
		Go
---------------------------------------------------------------------------------------------------------------------------------------------

ALTER FUNCTION [dbo].[Get_Opening_as_on_date]
    (
      @AccountId NUMERIC(18, 0) ,
      @Fromdate DATETIME        
        
    )
RETURNS NUMERIC(18, 2)
AS
    BEGIN        
        DECLARE @OpeningBalance NUMERIC(18, 2)        
        
        DECLARE @StartOpeningBalance NUMERIC(18, 2)  
        SET @StartOpeningBalance = 0        
        
        --SET @StartOpeningBalance = ISNULL(( SELECT  CASE WHEN type = 1  
        --                                                 THEN -OpeningAmount  
        --                                                 ELSE OpeningAmount  
        --                                            END AS amount  
        --                                    FROM    dbo.OpeningBalance  
        --                                    WHERE   FkAccountId = @AccountId  
        --                                            AND CAST(OpeningDate AS DATE) = CAST(@Fromdate AS DATE)  
        --                                  ), 0)        
        
        SET @OpeningBalance = ISNULL(( SELECT  
										--ISNULL(SUM(CASHIN - CASHOUT), 0) AS OpeningBalance
										CASE WHEN AccountId <> 10073
                                             THEN ISNULL(SUM(ISNULL(CASHIN,0) - ISNULL(CASHOUT,0)),
                                                         0)
                                             ELSE CASE WHEN AccountId = 10073
                                                            AND ( ISNULL(SUM(ISNULL(CASHIN,0) - ISNULL(CASHOUT,0)), 0) ) < 0
                                                       THEN ISNULL(SUM(ISNULL(CASHIN,0) - ISNULL(CASHOUT,0)), 0)
                                                       WHEN AccountId = 10073
                                                            AND ( ISNULL(SUM(ISNULL(CASHIN,0) - ISNULL(CASHOUT,0)), 0) ) > 0
                                                       THEN 0.00 
                                                  END
                                        END AS OpeningBalance
                                FROM    dbo.LedgerMaster
                                        JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(TRANSACTIONDATE AS DATE) < CAST(@FromDate AS DATE)
                                        INNER  JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.LedgerMaster.AccountId
                                WHERE   LedgerMaster.AccountId = @AccountId
                                GROUP BY AccountId 
                              ), 0)
                              
        SET @OpeningBalance = @StartOpeningBalance + @OpeningBalance        
        RETURN @OpeningBalance        
    END
	Go

---------------------------------------------------------------------------------------------------------------------------------------------
ALTER FUNCTION [dbo].[Get_PrimaryGroupOpening_as_on_date] 
    (
      @agID NUMERIC ,
      @Fromdate DATETIME        
        
    )
RETURNS NUMERIC(18, 2)
AS
    BEGIN
        DECLARE @OpeningBalance NUMERIC(18, 2)        
        
        DECLARE @StartOpeningBalance NUMERIC(18, 2)  
        SET @StartOpeningBalance = 0        
        
        --SET @StartOpeningBalance = ISNULL(( SELECT  SUM(amount)
        --                                    FROM    ( SELECT  CASE
        --                                                      WHEN type = 1
        --                                                      THEN -OpeningAmount
        --                                                      ELSE OpeningAmount
        --                                                      END AS amount
        --                                              FROM    dbo.OpeningBalance
        --                                              WHERE   FkAccountId IN (
        --                                                      SELECT
        --                                                      ACC_ID
        --                                                      FROM
        --                                                      dbo.ACCOUNT_MASTER
        --                                                      WHERE
        --                                                      AG_ID IN (
        --                                                      SELECT
        --                                                      AG_ID
        --                                                      FROM
        --                                                      dbo.ACCOUNT_GROUPS
        --                                                      WHERE
        --                                                      Primary_AG_ID = @agID )
        --                                                       OR dbo.ACCOUNT_MASTER.AG_ID=@agID
        --                                                       )
        --                                                      AND CAST(OpeningDate AS DATE) = CAST(@Fromdate AS DATE)
        --                                            ) tb
        --                                  ), 0)       
        
        SET @OpeningBalance = ISNULL((SELECT SUM(ISNULL(OpeningBalance,0)) FROM 
									( SELECT  CASE WHEN AccountId <> 10073
                                             THEN ISNULL(SUM(ISNULL(CASHIN,0)) - SUM(ISNULL(CASHOUT,0)), 0)
                                             ELSE CASE WHEN AccountId = 10073
                                                            AND ( ISNULL(SUM(ISNULL(CASHIN,0)) - SUM(ISNULL(CASHOUT,0)), 0) ) < 0
                                                       THEN ISNULL(SUM(ISNULL(CASHIN,0)) - SUM(ISNULL(CASHOUT,0)), 0)
                                                       WHEN AccountId = 10073
                                                            AND ( ISNULL(SUM(ISNULL(CASHIN,0)) - SUM(ISNULL(CASHOUT,0)), 0) ) > 0
                                                       THEN 0.00 
                                                  END
                                        END AS OpeningBalance
                                FROM    dbo.LedgerMaster
                                        JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(TRANSACTIONDATE AS DATE) < CAST(@Fromdate AS DATE)
                                        INNER  JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.LedgerMaster.AccountId
                                WHERE   LedgerMaster.AccountId IN (
                                        SELECT  ACC_ID
                                        FROM    dbo.ACCOUNT_MASTER
                                        WHERE   AG_ID IN (
                                                SELECT  AG_ID
                                                FROM    dbo.ACCOUNT_GROUPS
                                                WHERE   Primary_AG_ID = @agID ) OR dbo.ACCOUNT_MASTER.AG_ID= @agID)
                               GROUP BY AccountId
                              )tb)
                              , 0) 
        SET @OpeningBalance = @StartOpeningBalance + @OpeningBalance      
        RETURN @OpeningBalance        
    END
Go
---------------------------------------------------------------------------------------------------------------------------------------------
