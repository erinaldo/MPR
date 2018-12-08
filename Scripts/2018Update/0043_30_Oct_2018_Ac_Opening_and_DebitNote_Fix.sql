INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0043_30_Oct_2018_Ac_Opening_and_DebitNote_Fix' ,
          GETDATE()
        )
Go

---------------------POS+ AND MMS+ -----------------------------------------------------

CREATE FUNCTION Get_Current_OPENING_Summary ( -- @AccountId NUMERIC(18, 0) ,  
  @Fromdate DATETIME )  
RETURNS TABLE  
AS  
RETURN  
    ( SELECT    AccountID ,  
                SUM(OpeningBalance) AS OpeningBalance  
      FROM      ( SELECT    FkAccountId AS AccountID ,  
                            CASE WHEN type = 1 THEN -OpeningAmount  
                                 ELSE OpeningAmount  
                            END AS OpeningBalance  
                  FROM      dbo.OpeningBalance  
                  WHERE     CAST(OpeningDate AS DATE) = CAST(@Fromdate AS DATE) --AND  FkAccountId = @AccountId  
                  UNION ALL  
                  SELECT    AccountId ,  
                            CASE WHEN AccountId <> 10073  
                                 THEN ISNULL(SUM(ISNULL(CASHIN, 0)  
                                                 - ISNULL(CASHOUT, 0)), 0)  
                                 ELSE CASE WHEN AccountId = 10073  
                                                AND ( ISNULL(SUM(ISNULL(CASHIN,  
                                                              0)  
                                                              - ISNULL(CASHOUT,  
                                                              0)), 0) ) < 0  
                                           THEN ISNULL(SUM(ISNULL(CASHIN, 0)  
                                                           - ISNULL(CASHOUT, 0)),  
                                                       0)  
                                           WHEN AccountId = 10073  
                                                AND ( ISNULL(SUM(ISNULL(CASHIN,  
                                                              0)  
                                                              - ISNULL(CASHOUT,  
                                                              0)), 0) ) > 0  
                                           THEN 0.00  
                                      END  
                            END AS OpeningBalance  
                  FROM      dbo.LedgerMaster  
                            JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId  
                                                     AND CAST(TRANSACTIONDATE AS DATE) < CAST(@FromDate AS DATE)  
                            INNER  JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.LedgerMaster.AccountId  
     -- WHERE     LedgerMaster.AccountId = @AccountId  
                  GROUP BY  AccountId  
                ) tb  GROUP BY  AccountID  
    )  
    
    GO
    
    
    --------------------------------------MMS+ ONLY----------------------------------------------------------------------------------    
ALTER PROCEDURE [dbo].[PROC_DebitNote_MASTER]    
    (    
      @v_DebitNote_ID NUMERIC(18, 0) ,    
      @v_DebitNote_No NUMERIC(18, 0) ,    
      @v_DebitNote_Code VARCHAR(20) ,    
      @v_DebitNote_Date DATETIME ,    
      @v_Remarks VARCHAR(500) ,    
      @v_MRN_Id NUMERIC(18, 0) ,    
      @v_Created_BY VARCHAR(100) ,    
      @v_Creation_Date DATETIME ,    
      @v_Modified_By VARCHAR(100) ,    
      @v_Modification_Date DATETIME ,    
      @v_Division_ID NUMERIC(18, 0) ,    
      @v_DN_Amount NUMERIC(18, 2) ,    
      @v_DN_ItemValue NUMERIC(18, 2) = 0 ,    
      @v_DN_ItemTax NUMERIC(18, 2) = 0 ,    
      @v_DN_ItemCess NUMERIC(18, 2) = 0 ,    
      @v_DN_CustId NUMERIC(18, 0) ,    
      @v_INV_No VARCHAR(100) ,    
      @v_INV_Date DATETIME ,    
      @v_DebitNote_Type VARCHAR(20) = NULL ,    
      @v_RefNo VARCHAR(50) = NULL ,    
      @v_RefDate_dt DATETIME ,    
      @V_Trans_Type NUMERIC(18, 0) ,    
      @v_RoundOff NUMERIC(18, 2) ,    
      @v_Proc_Type INT                    
    )    
AS    
    BEGIN               
        DECLARE @V_GSTType NUMERIC(18, 0)            
        DECLARE @Remarks VARCHAR(250)                    
        DECLARE @InputID NUMERIC                    
        DECLARE @CInputID NUMERIC= 0                    
        SET @CInputID = 10016                    
        DECLARE @CGST_Amount NUMERIC(18, 2)            
        DECLARE @V_MRN_TYPE NUMERIC = 0          
        DECLARE @CessInputID NUMERIC = 0                    
        SET @CessInputID = 10013           
                   
        SET @V_GSTType = ( SELECT   dbo.Get_GST_Type(@v_DN_CustId)    
                         )        
                    
        IF @V_PROC_TYPE = 1    
            BEGIN                    
                    
                INSERT  INTO DebitNote_MASTER    
                        ( DebitNote_ID ,    
                          DebitNote_No ,    
                          DebitNote_Code ,    
                          DebitNote_Date ,    
                          Remarks ,    
                          MRNId ,    
                          Created_BY ,    
                          Creation_Date ,    
                          Modified_By ,    
                          Modification_Date ,    
                          Division_ID ,    
                          DN_Amount ,    
                          DN_CustId ,    
                          INV_No ,    
                          INV_Date ,    
                          DebitNote_Type ,    
                          RefNo ,    
                          RefDate_dt ,    
                          Tax_num ,    
                          Cess_num ,    
                          RoundOff ,    
                          GSTType        
                        )    
                VALUES  ( @V_DebitNote_ID ,    
                          @V_DebitNote_No ,    
                          @V_DebitNote_Code ,    
                          @v_DebitNote_Date ,    
                          @V_Remarks ,    
                          @V_MRN_Id ,    
                          @V_Created_BY ,    
                          @V_Creation_Date ,    
                          @V_Modified_By ,    
                          @V_Modification_Date ,    
                          @V_Division_ID ,    
                          @v_DN_Amount ,    
                          @v_DN_CustId ,    
                          @v_INV_No ,    
                          @v_INV_Date ,    
                          @v_DebitNote_Type ,    
                          @v_RefNo ,    
                          @v_RefDate_dt ,    
                          @v_DN_ItemTax ,    
                          @v_DN_ItemCess ,    
                          @v_RoundOff ,    
                          @V_GSTType            
                        )                    
                    
                UPDATE  DN_SERIES    
                SET     CURRENT_USED = @V_DebitNote_No    
                WHERE   DIV_ID = @V_Division_ID                    
                    
                SET @CGST_Amount = ( @v_DN_ItemTax / 2 )             
                       
--------------------------------------------------------------------------------------------------------------------------------------------    
    
                IF @V_MRN_Id > 0    
                    BEGIN    
                        SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1    
                                                             THEN 10023    
                                                             WHEN MRN_TYPE = 2    
                                                             THEN 10020    
                                                             WHEN MRN_TYPE = 3    
                                                             THEN 10074    
                                                        END AS inputid    
                                                FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER    
                                                WHERE   MRN_NO = @V_MRN_Id    
                                              ), 0)                    
                        IF @InputID = 0    
                            BEGIN                    
                                SET @InputID = ISNULL(( SELECT    
                                                              CASE    
                                                              WHEN MRN_TYPE = 1    
                                                              THEN 10023    
                                                              WHEN MRN_TYPE = 2    
                                                              THEN 10020    
                                                              WHEN MRN_TYPE = 3    
                                                              THEN 10074    
                                                              END AS inputid    
                                                        FROM  dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER    
                                                        WHERE MRN_NO = @V_MRN_Id    
                                                      ), 0)                    
                            END              
                                  
                    
                        SET @V_MRN_TYPE = ( SELECT  MRN_TYPE    
                                            FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER    
                                            WHERE   MRN_NO = @V_MRN_Id    
                                            UNION    
                                            SELECT  MRN_TYPE    
                                            FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER    
                                            WHERE   MRN_NO = @V_MRN_Id    
                                          )       
                    END    
                        
                ELSE    
                    BEGIN    
                        
                        SET @V_MRN_TYPE = ( SELECT  dbo.Get_GST_Type(@v_DN_CustId)    
                                          )    
                                              
                                              
                        SET @InputID = ISNULL(( SELECT  CASE WHEN @V_MRN_TYPE = 1    
                                                             THEN 10023    
                                                             WHEN @V_MRN_TYPE = 2    
                                                             THEN 10020    
                                                             WHEN @V_MRN_TYPE = 3    
                                                             THEN 10074    
                                                        END AS inputid    
                                              ), 0)                    
                        
                    END    
----------------------------------------------------------------------------------------------------------------------------       
                    
                SET @Remarks = 'Debit  against DebitNote No-. -'    
                    + @V_DebitNote_Code + ' - '    
                    + CAST(@V_DebitNote_No AS VARCHAR(50))      
                    
                    
                EXECUTE Proc_Ledger_Insert @v_DN_CustId, 0, @v_DN_Amount,    
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,    
                    @v_DebitNote_Date, @V_Created_BY                    
                    
                    
                EXECUTE Proc_Ledger_Insert 10070, @v_DN_ItemValue, 0, @Remarks,    
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,    
                    @v_DebitNote_Date, @V_Created_BY                     
                    
                    
                    
                SET @Remarks = 'GST against DebitNote No- '    
                    + @V_DebitNote_Code + ' - '    
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                    
                    
                IF @V_MRN_TYPE <> 2    
                    BEGIN              
                    
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,    
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,    
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                     
                    
                    
                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,    
                            @Remarks, @v_Division_ID, @V_DebitNote_ID,    
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                     
                    END                    
                    
                    
                    
                ELSE    
                    BEGIN                    
                        EXECUTE Proc_Ledger_Insert @InputID, @v_DN_ItemTax, 0,    
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,    
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                     
                    END                       
                  
                SET @Remarks = 'Cess against DebitNote No- '    
                    + @V_DebitNote_Code + ' - '    
                    + CAST(@V_DebitNote_No AS VARCHAR(50))              
                                  
                EXECUTE Proc_Ledger_Insert @CessInputID, @v_DN_ItemCess, 0,    
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,    
                    @v_DebitNote_Date, @V_Created_BY                  
                    
                SET @Remarks = 'Stock In against Debit Note No- '    
                    + @V_DebitNote_Code + ' - '    
                    + CAST(@V_DebitNote_No AS VARCHAR(50))           
                    
                    
                EXECUTE Proc_Ledger_Insert 10073, @v_DN_Amount, 0, @Remarks,    
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,    
                    @v_DebitNote_Date, @V_Created_BY            
                            
                SET @Remarks = 'Round Off against Debit Note No- '    
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                                 
                        
                IF @v_RoundOff > 0    
                    BEGIN                        
                        
                        EXECUTE Proc_Ledger_Insert 10054, 0, @v_RoundOff,    
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,    
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                    
                        
                    END                        
                        
                ELSE    
                    BEGIN                        
                        
                        SET @v_RoundOff = -+@v_RoundOff      
                        
                        EXECUTE Proc_Ledger_Insert 10054, @v_RoundOff, 0,    
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,    
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                    
                        
                    END                    
                    
            END                    
                    
        IF @V_PROC_TYPE = 2    
            BEGIN        
                        
                        
                EXECUTE [Proc_DebitNoteUpdateDeleteLedgerEntries] @V_DebitNote_ID,    
                    @V_Trans_Type            
                              
                    
                UPDATE  DebitNote_MASTER    
                SET     DebitNote_No = @V_DebitNote_No ,    
                        DebitNote_Code = @V_DebitNote_Code ,    
                        DebitNote_Date = @v_DebitNote_Date ,    
                        Remarks = @V_Remarks ,    
                        MRNId = @V_MRN_Id ,    
                        Modified_By = @V_Modified_By ,    
                        Modification_Date = @V_Modification_Date ,    
                        DN_Amount = @v_DN_Amount ,    
                        DN_CustId = @v_DN_CustId ,    
                        Division_ID = @V_Division_ID ,    
                        INV_No = @v_INV_No ,    
                        INV_Date = @v_INV_Date ,    
                        DebitNote_Type = @v_DebitNote_Type ,    
                        RefNo = @v_RefNo ,    
                        RefDate_dt = @v_RefDate_dt ,    
                        Tax_num = @v_DN_ItemTax ,    
                        Cess_num = @v_DN_ItemCess ,    
                        RoundOff = @v_RoundOff ,    
                        GSTType = @v_GSTType    
                WHERE   DebitNote_ID = @V_DebitNote_ID               
                            
                            
                                   
                SET @CGST_Amount = ( @v_DN_ItemTax / 2 )                    
----------------------------------------------------------------------------------------------------------------------------------                                   
                IF @V_MRN_Id > 0    
                    BEGIN    
                        SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1    
                                                             THEN 10023    
                                                             WHEN MRN_TYPE = 2    
                                                             THEN 10020    
                                                             WHEN MRN_TYPE = 3    
                                                             THEN 10074    
                                                        END AS inputid    
                                                FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER    
                                                WHERE   MRN_NO = @V_MRN_Id    
                                              ), 0)                    
                        IF @InputID = 0    
                            BEGIN                    
                                SET @InputID = ISNULL(( SELECT    
                                                              CASE    
                                                              WHEN MRN_TYPE = 1    
                                                              THEN 10023    
                                                              WHEN MRN_TYPE = 2    
                                                              THEN 10020    
                                                              WHEN MRN_TYPE = 3    
                                                              THEN 10074    
                                                              END AS inputid    
                                                        FROM  dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER    
        WHERE MRN_NO = @V_MRN_Id    
                                                      ), 0)                    
                            END              
                                  
                    
                        SET @V_MRN_TYPE = ( SELECT  MRN_TYPE    
                                            FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER    
                                            WHERE   MRN_NO = @V_MRN_Id    
                                            UNION    
                                            SELECT  MRN_TYPE    
        FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER    
                                            WHERE   MRN_NO = @V_MRN_Id    
                                          )       
                    END    
                        
                ELSE    
                    BEGIN    
                        
                        SET @V_MRN_TYPE = ( SELECT  dbo.Get_GST_Type(@v_DN_CustId)    
                                          )    
                                              
                                              
                        SET @InputID = ISNULL(( SELECT  CASE WHEN @V_MRN_TYPE = 1    
                                                             THEN 10023    
                                                             WHEN @V_MRN_TYPE = 2    
                                                             THEN 10020    
                                                             WHEN @V_MRN_TYPE = 3    
                                                             THEN 10074    
                                                        END AS inputid    
                                              ), 0)                    
                        
                    END    
                                      
                                                   
----------------------------------------------------------------------------------------------------------------------------------    
    
                SET @Remarks = 'Debit  against DebitNote No-. -'    
                    + @V_DebitNote_Code + ' - '    
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                    
                    
                    
                EXECUTE Proc_Ledger_Insert @v_DN_CustId, 0, @v_DN_Amount,    
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,    
                    @v_DebitNote_Date, @V_Created_BY                    
                    
                    
                EXECUTE Proc_Ledger_Insert 10070, @v_DN_ItemValue, 0, @Remarks,    
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,    
                    @v_DebitNote_Date, @V_Created_BY                     
                    
                    
                    
                SET @Remarks = 'GST against DebitNote No- '    
                    + @V_DebitNote_Code + ' - '    
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                    
                    
                IF @V_MRN_TYPE <> 2    
                    BEGIN              
                    
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,    
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,    
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                     
                    
                    
                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,    
                            @Remarks, @v_Division_ID, @V_DebitNote_ID,    
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                     
                    END                    
                    
                    
                    
                ELSE    
                    BEGIN                    
                        EXECUTE Proc_Ledger_Insert @InputID, @v_DN_ItemTax, 0,    
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,    
      @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                     
                    END                       
                  
                SET @Remarks = 'Cess against DebitNote No- '    
                    + @V_DebitNote_Code + ' - '    
                    + CAST(@V_DebitNote_No AS VARCHAR(50))              
                                  
                EXECUTE Proc_Ledger_Insert @CessInputID, @v_DN_ItemCess, 0,    
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,    
                    @v_DebitNote_Date, @V_Created_BY                  
                    
                SET @Remarks = 'Stock In against Debit Note No- '    
                    + @V_DebitNote_Code + ' - '    
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                    
                    
                    
                    
                EXECUTE Proc_Ledger_Insert 10073, @v_DN_Amount, 0, @Remarks,    
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,    
                    @v_DebitNote_Date, @V_Created_BY                             
                                 
                SET @Remarks = 'Round Off against Debit Note No- '    
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                                 
                        
                IF @v_RoundOff > 0    
                    BEGIN                        
                        
                        EXECUTE Proc_Ledger_Insert 10054,@v_RoundOff, 0,     
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,    
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                    
                        
                    END                        
                        
                ELSE   
                    BEGIN                        
                        
                        SET @v_RoundOff = -+@v_RoundOff                        
                        
                        EXECUTE Proc_Ledger_Insert 10054, 0, @v_RoundOff,  
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,    
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                    
                        
                    END               
                    
            END                    
    END      