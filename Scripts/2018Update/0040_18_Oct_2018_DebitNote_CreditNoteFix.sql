INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0040_18_Oct_2018_DebitNote_CreditNoteFix' ,
          GETDATE()
        )
Go

          
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
                        SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1
                                                             THEN 10023
                                                             WHEN MRN_TYPE = 2
                                                             THEN 10020
                                                             WHEN MRN_TYPE = 3
                                                             THEN 10074
                                                        END AS inputid
                                                FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
                                                WHERE   MRN_NO = @V_MRN_Id
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
                        SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1
                                                             THEN 10023
                                                             WHEN MRN_TYPE = 2
                                                             THEN 10020
                                                             WHEN MRN_TYPE = 3
                                                             THEN 10074
                                                        END AS inputid
                                                FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
                                                WHERE   MRN_NO = @V_MRN_Id
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
    END 
    
    GO
    
          
ALTER PROCEDURE [dbo].[PROC_DebitNote_DETAIL]
    (
      @v_DebitNote_ID NUMERIC(18, 0) ,
      @v_Item_ID NUMERIC(18, 0) ,
      @v_Item_Qty NUMERIC(18, 4) ,
      @v_Created_By VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Division_Id INT ,
      @v_Stock_Detail_Id NUMERIC(18, 0) ,
      @v_Item_Rate NUMERIC(18, 2) ,
      @v_Item_Tax NUMERIC(18, 2) ,
      @v_Item_Cess NUMERIC(18, 2) ,
      @V_Trans_Type NUMERIC(18, 0) ,
      @v_Proc_Type INT ,
      @v_TaxableAmt NUMERIC(18, 2) ,
      @v_TaxAmt NUMERIC(18, 2) ,
      @v_CessAmt NUMERIC(18, 2) ,
      @v_DebitAmt NUMERIC(18, 2)
    )
AS
    BEGIN              
        IF @V_PROC_TYPE = 1
            BEGIN              
                INSERT  INTO DebitNote_DETAIL
                        ( DebitNote_Id ,
                          Item_ID ,
                          Item_Qty ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_Id ,
                          Stock_Detail_Id ,
                          Item_Rate ,
                          Item_Tax ,
                          Item_Cess ,
                          TaxableAmt ,
                          TaxAmt ,
                          CessAmt ,
                          DebitAmt  
                        )
                VALUES  ( @V_DebitNote_ID ,
                          @V_Item_ID ,
                          @V_Item_Qty ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_Id ,
                          @v_Stock_Detail_Id ,
                          @v_Item_Rate ,
                          @v_Item_Tax ,
                          @v_Item_Cess ,
                          @v_TaxableAmt ,
                          @v_TaxAmt ,
                          @v_CessAmt ,
                          @v_DebitAmt  
                                    
                        )              
                DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)          
                --it will insert entry in stock detail table and           
--                --return stock_detail_id.                  
--                EXEC INSERT_STOCK_DETAIL @v_Item_ID, @v_Batch_no,          
--                    @v_Expiry_Date, @v_Item_Qty, @v_Received_ID, @V_Trans_Type,          
--                    @STOCK_DETAIL_ID OUTPUT          
--                                
                EXEC UPDATE_STOCK_DETAIL_ISSUE @STOCK_DETAIL_ID = @v_Stock_Detail_Id, --  numeric(18, 0)          
                    @ISSUE_QTY = @v_Item_Qty --  numeric(18, 4)          
                --it will insert entry in transaction log with stock_detail_id          
                EXEC INSERT_TRANSACTION_LOG @v_DebitNote_ID, @v_Item_ID,
                    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,
                    @v_Creation_Date, 0               
            END       
                  
        IF @V_PROC_TYPE = 2
            BEGIN      
                  
                DELETE  FROM DebitNote_DETAIL
                WHERE   DebitNote_Id = @v_DebitNote_ID
                        AND Item_ID = @V_Item_ID    
                          
                INSERT  INTO DebitNote_DETAIL
                        ( DebitNote_Id ,
                          Item_ID ,
                          Item_Qty ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_Id ,
                          Stock_Detail_Id ,
                          Item_Rate ,
                          Item_Tax ,
                          Item_Cess ,
                          TaxableAmt ,
                          TaxAmt ,
                          CessAmt ,
                          DebitAmt         
                        )
                VALUES  ( @V_DebitNote_ID ,
                          @V_Item_ID ,
                          @V_Item_Qty ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_Id ,
                          @v_Stock_Detail_Id ,
                          @v_Item_Rate ,
                          @v_Item_Tax ,
                          @v_Item_Cess ,
                          @v_TaxableAmt ,
                          @v_TaxAmt ,
                          @v_CessAmt ,
                          @v_DebitAmt          
                        )              
      
                               
                EXEC UPDATE_STOCK_DETAIL_ISSUE @STOCK_DETAIL_ID = @v_Stock_Detail_Id, --  numeric(18, 0)          
                    @ISSUE_QTY = @v_Item_Qty --  numeric(18, 4)         
                          
                          
                DELETE  FROM dbo.Transaction_Log
                WHERE   Transaction_Type = @V_Trans_Type
                        AND Transaction_ID = @v_DebitNote_ID       
                      
                --it will insert entry in transaction log with stock_detail_id                          
                EXEC INSERT_TRANSACTION_LOG @v_DebitNote_ID, @v_Item_ID,
                    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,
                    @v_Creation_Date, 0               
            END              
    END   
      
     GO
     
ALTER PROCEDURE [dbo].[Proc_DebitNoteUpdateDeleteLedgerEntries]
    (
      @DebitNoteId VARCHAR(50) ,
      @TransactionTypeId INT            
    )
AS
    BEGIN              
                    
        DECLARE @ACC_ID NUMERIC(18, 2)       
        DECLARE @MRNId NUMERIC(18, 2)       
              
        SELECT  @ACC_ID = DN_CustId ,
                @MRNId = MRN_NO
        FROM    ( SELECT    DN_CustId ,
                            mrwpm.MRN_NO --CASE WHEN ISNULL(mrwpm.MRN_NO,0) = 0 THEN mrapm.MRN_NO end      
                  FROM      dbo.DebitNote_master dn
                            INNER JOIN dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER mrwpm ON dn.MRNId = mrwpm.MRN_NO
                  WHERE     dn.DebitNote_Id = @DebitNoteId
                  UNION ALL
                  SELECT    DN_CustId ,
                            mrapm.MRN_NO --CASE WHEN ISNULL(mrwpm.MRN_NO,0) = 0 THEN mrapm.MRN_NO end      
                  FROM      dbo.DebitNote_master dn
                            INNER JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER mrapm ON dn.MRNId = mrapm.MRN_NO
                  WHERE     dn.DebitNote_Id = @DebitNoteId
                ) tb      
              
              
        UPDATE  STOCK_DETAIL
        SET     STOCK_DETAIL.Issue_Qty = ( STOCK_DETAIL.Issue_Qty
                                           - debitNote_DETAIL.ITEM_QTY ) ,
                STOCK_DETAIL.Balance_Qty = ( STOCK_DETAIL.Balance_Qty
                                             + debitNote_DETAIL.ITEM_QTY )
        FROM    dbo.STOCK_DETAIL
                JOIN dbo.debitNote_DETAIL ON debitNote_DETAIL.STOCK_DETAIL_ID = STOCK_DETAIL.STOCK_DETAIL_ID
                                             AND debitNote_DETAIL.ITEM_ID = STOCK_DETAIL.Item_id
        WHERE   DebitNote_Id = @DebitNoteId    
              
        DELETE  FROM dbo.debitNote_DETAIL
        WHERE   DebitNote_Id = @DebitNoteId     
                    
                    
        DECLARE @CashOut NUMERIC(18, 2)              
        DECLARE @CashIn NUMERIC(18, 2)             
                    
        SET @CashOut = 0            
                    
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @DebitNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = @ACC_ID
                      )              
              
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @ACC_ID            
                    
                    
                    
                    
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @DebitNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = 10073
                       )              
                 
        SET @CashIn = 0         
                
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10073                
                    
                    
                    
                    
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @DebitNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = 10070
                       )                
                
        SET @CashIn = 0     
                
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10070       
              
        SET @CashIn = 0         
        SET @CashOut = 0            
                   
        DECLARE @V_MRN_TYPE VARCHAR(1)                
        SET @V_MRN_TYPE = ( SELECT  MRN_TYPE
                            FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                            WHERE   MRN_NO = @MRNId
                            UNION
                            SELECT  MRN_TYPE
                            FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
                            WHERE   MRN_NO = @MRNId
                          )                
                    
        DECLARE @InputID NUMERIC                
        DECLARE @CInputID NUMERIC                
        SET @CInputID = 10016           
        DECLARE @CessInputID NUMERIC= 0              
        SET @CessInputID = 10013              
                    
        SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10023
                                     WHEN @V_MRN_TYPE = 2 THEN 10020
                                     WHEN @V_MRN_TYPE = 3 THEN 10074
                                END AS inputid
                       )             
                                   
                                   
                                   
        IF @V_MRN_TYPE <> 2
            BEGIN                
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @DebitNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @CInputID
                               )                
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @CInputID                
                
                
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @DebitNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @InputID
                               )                
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @InputID                
            END            
        ELSE
            BEGIN                
                
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @DebitNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @InputID
                               )                
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @InputID                
            END           
                      
                      
        -------Start Cess Entries Deletion --------          
                           
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @DebitNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = @CessInputID
                       )         
                                       
        SET @CashIn = 0         
                           
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @CessInputID           
                
                          
        -------End Cess Entries Deletion --------            
                    
        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @DebitNoteId
                AND TransactionTypeId = @TransactionTypeId             
                       
                    
    END 
    
    GO
   
        
ALTER PROCEDURE [dbo].[Get_MRN_Details_DebitNote] @V_MRN_NO NUMERIC(18, 0)
AS
    BEGIN           
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                CAST(SD.Balance_Qty AS NUMERIC(18, 2)) AS Prev_Item_Qty ,
                CAST(md.Item_Qty AS NUMERIC(18, 2)) AS MRN_Qty ,
                CAST(dbo.Get_CostRate(md.Item_Rate, IM.ITEM_ID, MM.Receipt_ID,
                                      2) AS NUMERIC(18, 2)) AS Item_Rate ,
                CAST(md.Vat_Per AS NUMERIC(18, 2)) AS Vat_Per ,
                CAST(md.Cess_Per AS NUMERIC(18, 2)) AS Cess_Per ,
                '0.00' AS Item_Qty ,
                0.00 AS TaxableAmt ,
                0.00 AS Amount ,
                0.00 AS GST ,
                0.00 AS Cess ,
                MD.Stock_Detail_Id AS Stock_Detail_Id ,
                dbo.Get_CostRate(md.Item_Rate, IM.ITEM_ID, MM.Receipt_ID, 2) AS Prv_Rate ,
                md.Vat_Per AS Prv_Vat_Per ,
                md.Cess_Per AS Prv_Cess_Per
        FROM    MATERIAL_RECEIVED_AGAINST_PO_MASTER MM
                INNER JOIN MATERIAL_RECEIVED_AGAINST_PO_DETAIL MD ON mm.Receipt_ID = md.Receipt_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON MD.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON MD.Stock_Detail_Id = SD.STOCK_DETAIL_ID
        WHERE   MM.MRN_NO = @V_MRN_NO
        UNION ALL
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                CAST(SD.Balance_Qty AS NUMERIC(18, 2)) AS Prev_Item_Qty ,
                CAST(md.Item_Qty AS NUMERIC(18, 2)) AS MRN_Qty ,
                CAST(dbo.Get_CostRate(md.Item_Rate, IM.ITEM_ID, MM.Received_ID,
                                      1) AS NUMERIC(18, 2)) AS Item_Rate ,
                CAST(md.Item_vat AS NUMERIC(18, 2)) AS Item_vat ,
                CAST(md.Item_Cess AS NUMERIC(18, 2)) AS Item_Cess ,
                '0.00' AS Item_Qty ,
                0.00 AS TaxableAmt ,
                0.00 AS Amount ,
                0.00 AS GST ,
                0.00 AS Cess ,
                MD.Stock_Detail_Id AS Stock_Detail_Id ,
                dbo.Get_CostRate(md.Item_Rate, IM.ITEM_ID, MM.Received_ID, 1) AS Prv_Rate ,
                md.Item_vat AS Prv_Vat_Per ,
                md.Item_Cess AS Prv_Cess_Per
        FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER MM
                INNER JOIN dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MD ON mm.Received_ID = md.Received_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON MD.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON MD.Stock_Detail_Id = SD.STOCK_DETAIL_ID
        WHERE   MM.MRN_NO = @V_MRN_NO        
      
    END   
      GO    
  
ALTER PROCEDURE Proc_GETDebitNoteDetailsByID_Edit
    (
      @DebitNoteId NUMERIC(18, 0)
    )
AS
    BEGIN      
        SELECT  DN.DebitNote_Code + CAST(DN.DebitNote_No AS VARCHAR(20)) AS DebitNoteNumber ,
                dbo.fn_Format(DN.DebitNote_Date) AS DebitNote_Date ,
                DN_CustId ,
                MRWPM.MRN_NO AS MRNo ,
                MRWPM.MRN_PREFIX + CAST(MRWPM.MRN_NO AS VARCHAR(20)) AS MRNNumber ,
                INV_No AS InvoiceNo ,
                dbo.fn_Format(DN.INV_Date) AS InvoiceDate ,
                DN.Remarks AS Remarks ,
                Purchase_Type AS MRN_TYPE ,
                ISNULL(DN.RoundOff, 0) AS RoundOff
        FROM    dbo.DebitNote_Master DN
                INNER JOIN dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER AS MRWPM ON MRWPM.MRN_NO = DN.MRNId
        WHERE   DebitNote_Id = @DebitNoteId
        UNION ALL
        SELECT  DN.DebitNote_Code + CAST(DN.DebitNote_No AS VARCHAR(20)) AS DebitNoteNumber ,
                dbo.fn_Format(DN.DebitNote_Date) AS DebitNote_Date ,
                DN_CustId ,
                MRAPM.MRN_NO AS MRNo ,
                MRAPM.MRN_PREFIX + CAST(MRAPM.MRN_NO AS VARCHAR(20)) AS MRNNumber ,
                INV_No AS InvoiceNo ,
                dbo.fn_Format(DN.INV_Date) AS InvoiceDate ,
                DN.Remarks AS Remarks ,
                MRN_TYPE ,
                ISNULL(DN.RoundOff, 0) AS RoundOff
        FROM    dbo.DebitNote_Master DN
                INNER JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER AS MRAPM ON MRAPM.MRN_NO = DN.MRNId
        WHERE   DebitNote_Id = @DebitNoteId
        UNION ALL
        SELECT  DN.DebitNote_Code + CAST(DN.DebitNote_No AS VARCHAR(20)) AS DebitNoteNumber ,
                dbo.fn_Format(DN.DebitNote_Date) AS DebitNote_Date ,
                DN_CustId ,
                0 AS MRNo ,
                '' AS MRNNumber ,
                INV_No AS InvoiceNo ,
                dbo.fn_Format(DN.INV_Date) AS InvoiceDate ,
                DN.Remarks AS Remarks ,
                DN.GSTType AS MRN_TYPE ,
                ISNULL(DN.RoundOff, 0) AS RoundOff
        FROM    dbo.DebitNote_Master DN
        WHERE   DebitNote_Id = @DebitNoteId
                AND DN.MRNId <= 0  
    END    
      
 GO
 
         
    --[dbo].[GetDebitNoteDetails] 1      
ALTER PROCEDURE [dbo].[GetDebitNoteDetails]
    @DebitNoteId NUMERIC(18, 0)
AS
    BEGIN               
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                SD.Balance_Qty AS Prev_Item_Qty ,
                md.Item_Qty AS MRN_Qty ,
                dd.Item_Rate ,
                dd.Item_Tax AS Vat_Per ,
                dd.Item_Cess AS Cess_Per ,
                CAST(dd.Item_Qty AS NUMERIC(18, 2)) AS Item_Qty ,
                dd.TaxableAmt AS TaxableAmt ,
                dd.TaxAmt AS GST ,
                dd.CessAmt AS Cess ,
                dd.TaxableAmt AS Amount ,
                dd.Item_Rate AS Prv_Rate ,
                dd.Item_Tax AS Prv_Vat_Per ,
                dd.Item_Cess AS Prv_Cess_Per ,
                sd.Stock_Detail_Id AS Stock_Detail_Id
        FROM    dbo.DebitNote_Master DN
                INNER JOIN DebitNote_DETAIL DD ON DD.DebitNote_Id = dn.DebitNote_Id
                LEFT JOIN MATERIAL_RECIEVED_WITHOUT_PO_MASTER MM ON mm.MRN_NO = dn.MRNId
                INNER JOIN dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MD ON mm.Received_ID = md.Received_ID
                                                              AND md.Item_ID = dd.Item_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON DD.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON DD.Stock_Detail_Id = SD.STOCK_DETAIL_ID
                                              AND dd.Item_ID = sd.Item_id
        WHERE   dn.DebitNote_Id = @DebitNoteId
        UNION ALL
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                SD.Balance_Qty AS Prev_Item_Qty ,
                md.Item_Qty AS MRN_Qty ,
                dd.Item_Rate ,
                dd.Item_Tax AS Vat_Per ,
                dd.Item_Cess AS Cess_Per ,
                CAST(dd.Item_Qty AS NUMERIC(18, 2)) AS Item_Qty ,
                dd.TaxableAmt AS TaxableAmt ,
                dd.TaxAmt AS GST ,
                dd.CessAmt AS Cess ,
                dd.TaxableAmt AS Amount ,
                dd.Item_Rate AS Prv_Rate ,
                dd.Item_Tax AS Prv_Vat_Per ,
                dd.Item_Cess AS Prv_Cess_Per ,
                sd.Stock_Detail_Id AS Stock_Detail_Id
        FROM    dbo.DebitNote_Master DN
                INNER JOIN DebitNote_DETAIL DD ON DD.DebitNote_Id = dn.DebitNote_Id
                LEFT JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER MM ON mm.MRN_NO = dn.MRNId
                INNER JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL MD ON mm.Receipt_ID = md.Receipt_ID
                                                              AND md.Item_ID = dd.Item_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON DD.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON DD.Stock_Detail_Id = SD.STOCK_DETAIL_ID
                                              AND dd.Item_ID = sd.Item_id
        WHERE   dn.DebitNote_Id = @DebitNoteId
        UNION ALL
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                SD.Balance_Qty AS Prev_Item_Qty ,
                0 AS MRN_Qty ,
                dd.Item_Rate ,
                dd.Item_Tax AS Vat_Per ,
                dd.Item_Cess AS Cess_Per ,
                CAST(dd.Item_Qty AS NUMERIC(18, 2)) AS Item_Qty ,
                dd.TaxableAmt AS TaxableAmt ,
                dd.TaxAmt AS GST ,
                dd.CessAmt AS Cess ,
                dd.TaxableAmt AS Amount ,
                0.00 AS Prv_Rate ,
                dd.Item_Tax AS Prv_Vat_Per ,
                dd.Item_Cess AS Prv_Cess_Per ,
                sd.Stock_Detail_Id AS Stock_Detail_Id
        FROM    dbo.DebitNote_Master DN
                INNER JOIN DebitNote_DETAIL DD ON DD.DebitNote_Id = dn.DebitNote_Id
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON DD.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON DD.Stock_Detail_Id = SD.STOCK_DETAIL_ID
                                              AND dd.Item_ID = sd.Item_id
        WHERE   dn.DebitNote_Id = @DebitNoteId
                AND DN.MRNId <= 0    
    END     
    GO
    
  -----------------------Credit Note----------------------------------
  
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
      @v_CN_ItemValue NUMERIC(18, 2) = 0 ,
      @v_CN_ItemTax NUMERIC(18, 2) = 0 ,
      @v_CN_ItemCess NUMERIC(18, 2) = 0 ,
      @v_CN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
      @v_CreditNote_Type VARCHAR(20) = NULL ,
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
        DECLARE @OutputID NUMERIC              
        DECLARE @COutputID NUMERIC              
        SET @COutputID = 10017         
        DECLARE @CGST_Amount NUMERIC(18, 2)       
        DECLARE @v_INV_TYPE VARCHAR(1)        
        DECLARE @CessOutputID NUMERIC = 0                
        SET @CessOutputID = 10014      
                 
        SET @V_GSTType = ( SELECT   dbo.Get_GST_Type(@v_CN_CustId)
                         )    
                             
        IF @V_PROC_TYPE = 1
            BEGIN        
                   
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
                          INV_Date ,
                          CreditNote_Type ,
                          RefNo ,
                          RefDate_dt ,
                          Tax_Amt ,
                          Cess_Amt ,
                          RoundOff ,
                          GSTType              
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
                          @v_INV_Date ,
                          @v_CreditNote_Type ,
                          @v_RefNo ,
                          @v_RefDate_dt ,
                          @v_CN_ItemTax ,
                          @v_CN_ItemCess ,
                          @v_RoundOff ,
                          @V_GSTType              
                        )              
              
                UPDATE  CN_SERIES
                SET     CURRENT_USED = @V_CreditNote_No
                WHERE   DIV_ID = @V_Division_ID                
              
                             
                SET @Remarks = 'Credit against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))          
              
              
                EXECUTE Proc_Ledger_Insert @v_CN_CustId, @v_CN_Amount, 0,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY              
              
                EXECUTE Proc_Ledger_Insert 10071, 0, @v_CN_ItemValue, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY      
                  
                             
                SET @CGST_Amount = ( @v_CN_ItemTax / 2 )        
              
              
                SET @OutputID = ( SELECT    CASE WHEN INV_TYPE = 'I'
                                                 THEN 10021
                                                 WHEN INV_TYPE = 'S'
                                                 THEN 10024
                                                 WHEN INV_TYPE = 'U'
                                                 THEN 10075
                                            END AS inputid
                                  FROM      dbo.SALE_INVOICE_MASTER
                                  WHERE     SI_ID = @v_INV_Id
                                )       
                        
                SET @v_INV_TYPE = ( SELECT  INV_TYPE
                                    FROM    dbo.SALE_INVOICE_MASTER
                                    WHERE   SI_ID = @v_INV_Id
                                  )              
              
                SET @Remarks = 'GST against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))              
              
                IF @v_INV_TYPE <> 'I'
                    BEGIN              
                        EXECUTE Proc_Ledger_Insert @COutputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY             
              
                        EXECUTE Proc_Ledger_Insert @OutputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY              
                    END              
                ELSE
                    BEGIN              
                        EXECUTE Proc_Ledger_Insert @OutputID, 0, @v_CN_ItemTax,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY               
                    END         
                            
                            
                SET @Remarks = 'CESS against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))          
                         
                EXECUTE Proc_Ledger_Insert @CessOutputID, 0, @v_CN_ItemCess,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY           
                            
                            
                SET @Remarks = 'Stock Out against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))              
              
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_CN_Amount, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY    
                        
                SET @Remarks = 'Round Off against Credit Note No- '
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                             
          
                IF @v_RoundOff > 0
                    BEGIN                    
                    
                        EXECUTE Proc_Ledger_Insert 10054, 0, @v_RoundOff,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                
                 
                    END                    
                    
                ELSE
                    BEGIN                    
                    
                        SET @v_RoundOff = -+@v_RoundOff                    
                    
                        EXECUTE Proc_Ledger_Insert 10054, @v_RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                
                    
                    END         
                          
            END         
              
        IF @V_PROC_TYPE = 2
            BEGIN       
                  
                EXECUTE [Proc_CreditNoteUpdateDeleteLedgerEntries] @V_CreditNote_ID,
                    @V_Trans_Type               
              
                UPDATE  dbo.CreditNote_Master
                SET     CreditNote_No = @V_CreditNote_No ,
                        CreditNote_Code = @V_CreditNote_Code ,
                        CreditNote_Date = @v_CreditNote_Date ,
                        Remarks = @V_Remarks ,
                        INVId = @V_INV_Id ,
                        Modified_By = @V_Modified_By ,
                        Modification_Date = @V_Modification_Date ,
                        CN_Amount = @v_CN_Amount ,
                        CN_CustId = @v_CN_CustId ,
                        Division_ID = @V_Division_ID ,
                        INV_No = @v_INV_No ,
                        INV_Date = @v_INV_Date ,
                        CreditNote_Type = @v_CreditNote_Type ,
                        RefNo = @v_RefNo ,
                        RefDate_dt = @v_RefDate_dt ,
                        Tax_Amt = @v_CN_ItemTax ,
                        Cess_Amt = @v_CN_ItemCess ,
                        RoundOff = @v_RoundOff ,
                        GSTType = @v_GSTType
                WHERE   CreditNote_ID = @V_CreditNote_ID                       
                      
                SET @Remarks = 'Credit against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))          
              
              
                EXECUTE Proc_Ledger_Insert @v_CN_CustId, @v_CN_Amount, 0,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY              
              
                EXECUTE Proc_Ledger_Insert 10071, 0, @v_CN_ItemValue, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY      
                    
                             
                SET @CGST_Amount = ( @v_CN_ItemTax / 2 )        
              
              
                SET @OutputID = ( SELECT    CASE WHEN INV_TYPE = 'I'
                                                 THEN 10021
                                                 WHEN INV_TYPE = 'S'
                                                 THEN 10024
                                                 WHEN INV_TYPE = 'U'
                                                 THEN 10075
                                            END AS inputid
                                  FROM      dbo.SALE_INVOICE_MASTER
                                  WHERE     SI_ID = @v_INV_Id
                                )       
                        
                SET @v_INV_TYPE = ( SELECT  INV_TYPE
                                    FROM    dbo.SALE_INVOICE_MASTER
                                    WHERE   SI_ID = @v_INV_Id
                                  )              
              
                SET @Remarks = 'GST against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))              
              
                IF @v_INV_TYPE <> 'I'
                    BEGIN              
                        EXECUTE Proc_Ledger_Insert @COutputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY             
              
                        EXECUTE Proc_Ledger_Insert @OutputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY              
                    END              
                ELSE
                    BEGIN              
                        EXECUTE Proc_Ledger_Insert @OutputID, 0, @v_CN_ItemTax,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY               
                    END         
                            
                            
                SET @Remarks = 'CESS against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))          
                         
                EXECUTE Proc_Ledger_Insert @CessOutputID, 0, @v_CN_ItemCess,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY           
                            
                            
                SET @Remarks = 'Stock Out against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))              
              
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_CN_Amount, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY       
                        
                SET @Remarks = 'Round Off against Credit Note No- '
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                             
                    
                IF @v_RoundOff > 0
                    BEGIN                    
                    
                        EXECUTE Proc_Ledger_Insert 10054, 0, @v_RoundOff,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                
                    
                    END                    
                    
                ELSE
                    BEGIN                    
                    
                        SET @v_RoundOff = -+@v_RoundOff                    
                    
                        EXECUTE Proc_Ledger_Insert 10054, @v_RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                
                    
                    END      
           
            END              
    END 
    GO
          
ALTER PROCEDURE [dbo].[PROC_CreditNote_DETAIL]
    (
      @v_CreditNote_ID NUMERIC(18, 0) ,
      @v_Item_ID NUMERIC(18, 0) ,
      @v_Item_Qty NUMERIC(18, 4) ,
      @v_Created_By VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Division_Id INT ,
      @v_Stock_Detail_Id NUMERIC(18, 0) ,
      @v_Item_Rate NUMERIC(18, 2) ,
      @v_Item_Tax NUMERIC(18, 2) ,
      @v_Item_Cess NUMERIC(18, 2) ,
      @V_Trans_Type NUMERIC(18, 0) ,
      @v_Proc_Type INT ,
      @v_TaxableAmt NUMERIC(18, 2) ,
      @v_TaxAmt NUMERIC(18, 2) ,
      @v_CessAmt NUMERIC(18, 2) ,
      @v_CreditAmt NUMERIC(18, 2)
    )
AS
    BEGIN     
        DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)          
        SET @STOCK_DETAIL_ID = @v_Stock_Detail_Id     
        IF @STOCK_DETAIL_ID = 0
            BEGIN  
                SET @STOCK_DETAIL_ID = ( SELECT DISTINCT
                                                STOCK_DETAIL_ID
                                         FROM   STOCK_DETAIL
                                         WHERE  Item_id = @v_Item_ID
                                       )  
            END  
            
        IF @V_PROC_TYPE = 1
            BEGIN              
                INSERT  INTO CreditNote_DETAIL
                        ( CreditNote_Id ,
                          Item_ID ,
                          Item_Qty ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_Id ,
                          Stock_Detail_Id ,
                          Item_Rate ,
                          Item_Tax ,
                          Item_Cess ,
                          TaxableAmt ,
                          TaxAmt ,
                          CessAmt ,
                          CreditAmt         
                        )
                VALUES  ( @V_CreditNote_ID ,
                          @V_Item_ID ,
                          @V_Item_Qty ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_Id ,
                          @v_Stock_Detail_Id ,
                          @v_Item_Rate ,
                          @v_Item_Tax ,
                          @v_Item_Cess ,
                          @v_TaxableAmt ,
                          @v_TaxAmt ,
                          @v_CessAmt ,
                          @v_CreditAmt    
                        )       
                                     
                       
                  
             
                UPDATE  dbo.STOCK_DETAIL
                SET     ISSUE_QTY = ISSUE_QTY - @v_Item_Qty ,
                        Balance_Qty = BALANCE_QTY + @v_Item_Qty
                WHERE   STOCK_DETAIL_ID = @STOCK_DETAIL_ID              
                        
            
                EXEC INSERT_TRANSACTION_LOG @v_CreditNote_ID, @v_Item_ID,
                    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,
                    @v_Creation_Date, 0               
            END        
                  
        IF @V_PROC_TYPE = 2
            BEGIN      
                  
    --DELETE  FROM dbo.CreditNote_DETAIL      
    --            WHERE   CreditNote_Id = @V_CreditNote_ID  AND  Item_ID = @V_Item_ID      
                      
                      
                INSERT  INTO CreditNote_DETAIL
                        ( CreditNote_Id ,
                          Item_ID ,
                          Item_Qty ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_Id ,
                          Stock_Detail_Id ,
                          Item_Rate ,
                          Item_Tax ,
                          Item_Cess ,
                          TaxableAmt ,
                          TaxAmt ,
                          CessAmt ,
                          CreditAmt          
                        )
                VALUES  ( @V_CreditNote_ID ,
                          @V_Item_ID ,
                          @V_Item_Qty ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_Id ,
                          @v_Stock_Detail_Id ,
                          @v_Item_Rate ,
                          @v_Item_Tax ,
                          @v_Item_Cess ,
                          @v_TaxableAmt ,
                          @v_TaxAmt ,
                          @v_CessAmt ,
                          @v_CreditAmt    
                        )     
                
                UPDATE  dbo.STOCK_DETAIL
                SET     ISSUE_QTY = ISSUE_QTY - @v_Item_Qty ,
                        Balance_Qty = BALANCE_QTY + @v_Item_Qty
                WHERE   STOCK_DETAIL_ID = @STOCK_DETAIL_ID    
          
            END            
    END          
    
    GO
    
CREATE FUNCTION [dbo].[Get_CostSaleRate]
    (
      @rate NUMERIC(18, 2) ,
      @ItemID NUMERIC(18, 0) ,
      @SIID NUMERIC(18, 0) ,
      @ID NUMERIC(18, 0)
    )
RETURNS NUMERIC(18, 2)
AS
    BEGIN  
      
        DECLARE @costrate NUMERIC(18, 2)  
       
        IF @ID = 1
            BEGIN             
                SELECT  @costrate = @rate
                        - ( SELECT  CAST(( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                                       THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( @rate )
                                                              - ( ROUND(( @rate
                                                              * DISCOUNT_VALUE )
                                                              / 100, 2) ) )
                                                              - ( ( ( @rate )
                                                              - ( ROUND(( @rate
                                                              * DISCOUNT_VALUE )
                                                              / 100, 2) ) )
                                                              / ( 1
                                                              + ( VAT_PER
                                                              / 100 ) ) ) )
                                                              + ROUND(( @rate
                                                              * DISCOUNT_VALUE )
                                                              / 100, 2)
                                                              ELSE ROUND(( @rate
                                                              * DISCOUNT_VALUE )
                                                              / 100, 2)
                                                            END
                                                       ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( @rate )
                                                              - ROUND(DISCOUNT_VALUE,
                                                              2) )
                                                              - ( ( ( @rate )
                                                              - ROUND(DISCOUNT_VALUE,
                                                              2) ) / ( 1
                                                              + ( VAT_PER
                                                              / 100 ) ) ) )
                                                              + ROUND(DISCOUNT_VALUE,
                                                              2)
                                                              ELSE ROUND(DISCOUNT_VALUE,
                                                              2)
                                                            END
                                                  END, 0) ) AS NUMERIC(18, 2))
                            FROM    dbo.SALE_INVOICE_DETAIL
                            WHERE   SI_ID = @SIID
                                    AND Item_ID = @ItemID
                          )  
            END   
                         
        ELSE
            BEGIN              
                SELECT  @costrate = @rate
                        - ( SELECT  ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                                  THEN ( ( @rate )
                                                         * DISCOUNT_VALUE )
                                                       / 100
                                                  ELSE DISCOUNT_VALUE
                                             END, 0) )
                            FROM    dbo.SALE_INVOICE_DETAIL
                            WHERE   SI_ID = @SIID
                                    AND Item_ID = @ItemID
                          )
            END 
                           
        RETURN @costrate  
      
    END  
    GO
ALTER PROCEDURE [dbo].[Get_INV_Details_CreditNote] @Si_ID NUMERIC(18, 0)
AS
    BEGIN             
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                CAST(SD.Balance_Qty AS NUMERIC(18, 2)) AS Prev_Item_Qty ,
                CAST(saleID.Item_Qty AS NUMERIC(18, 2)) AS INV_Qty ,
                CAST(dbo.Get_CostSaleRate(saleID.Item_Rate, IM.ITEM_ID,
                                          saleID.SI_ID, 1) AS NUMERIC(18, 2)) AS Item_Rate ,
                CAST(saleID.Vat_Per AS NUMERIC(18, 2)) AS Vat_Per ,
                CAST(saleID.CessPercentage_num AS NUMERIC(18, 2)) AS Cess_Per ,
                '0.00' AS Item_Qty ,
                0.00 AS TaxableAmt ,
                0.00 AS Amount ,
                0.00 AS GST ,
                0.00 AS Cess ,
                SIID.Stock_Detail_Id AS Stock_Detail_Id ,
                saleID.Item_Rate AS Prv_Rate ,
                saleID.Vat_Per AS Prv_Vat_Per ,
                saleID.CessPercentage_num Prv_Cess_Per ,
                CONVERT(VARCHAR(20), SIM.CREATION_DATE, 106) AS INvDate ,
                SI_CODE + CAST(SI_NO AS VARCHAR(20)) AS SiNo ,
                INV_TYPE
        FROM    dbo.SALE_INVOICE_MASTER SIM
                INNER JOIN SALE_INVOICE_DETAIL SaleID ON sim.SI_ID = SaleID.SI_ID
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL SIID ON SIID.ITEM_ID = SaleID.ITEM_ID
                                                           AND SIID.SI_ID = SaleID.SI_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON SaleID.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON SIID.Stock_Detail_Id = SD.STOCK_DETAIL_ID
        WHERE   SIM.SI_ID = @Si_ID         
    END  
      
GO
ALTER PROCEDURE [dbo].[Proc_CreditNoteUpdateDeleteLedgerEntries]
    (
      @CreditNoteId VARCHAR(50) ,
      @TransactionTypeId INT              
    )
AS
    BEGIN        
          
        --SELECT  *      
        --FROM    SALE_INVOICE_MASTER         
                      
        DECLARE @ACC_ID NUMERIC(18, 2)         
        DECLARE @InvId NUMERIC(18, 2)         
                
        SELECT  @ACC_ID = CN_CustId ,
                @InvId = sim.SI_ID
        FROM    dbo.CreditNote_master cn
                INNER JOIN dbo.SALE_INVOICE_MASTER sim ON cn.INVId = sim.SI_ID
        WHERE   cn.CreditNote_Id = @CreditNoteId                
                      
                      
            
        UPDATE  STOCK_DETAIL
        SET     STOCK_DETAIL.Issue_Qty = ( STOCK_DETAIL.Issue_Qty
                                           + CreditNote_DETAIL.ITEM_QTY ) ,
                STOCK_DETAIL.Balance_Qty = ( STOCK_DETAIL.Balance_Qty
                                             - CreditNote_DETAIL.ITEM_QTY )
        FROM    dbo.STOCK_DETAIL
                JOIN dbo.CreditNote_DETAIL ON CreditNote_DETAIL.STOCK_DETAIL_ID = STOCK_DETAIL.STOCK_DETAIL_ID
                                              AND CreditNote_DETAIL.ITEM_ID = STOCK_DETAIL.Item_id
        WHERE   CreditNote_Id = @CreditNoteId      
                   
                
                
        DELETE  FROM dbo.CreditNote_DETAIL
        WHERE   CreditNote_Id = @CreditNoteId      
                      
                      
                      
        DECLARE @CashOut NUMERIC(18, 2)                
        DECLARE @CashIn NUMERIC(18, 2)               
                      
        SET @CashIn = 0              
                      
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @CreditNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = @ACC_ID
                       )                
                
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @ACC_ID                       
                      
                      
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @CreditNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = 10073
                      )                
                   
        SET @CashOut = 0           
                  
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10073        
                      
                      
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @CreditNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = 10071
                      )                  
                  
        SET @CashOut = 0                  
                  
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10071         
                
        SET @CashIn = 0           
        SET @CashOut = 0              
                     
        DECLARE @v_INV_TYPE VARCHAR(1)                        
              
                      
        DECLARE @OutputID NUMERIC                  
        DECLARE @COutputID NUMERIC                  
        SET @COutputID = 10017             
        DECLARE @CessOutputID NUMERIC = 0                
        SET @CessOutputID = 10014               
                      
        SET @OutputID = ( SELECT    CASE WHEN INV_TYPE = 'I' THEN 10021
                                         WHEN INV_TYPE = 'S' THEN 10024
                                         WHEN INV_TYPE = 'U' THEN 10075
                                    END AS inputid
                          FROM      dbo.SALE_INVOICE_MASTER
                          WHERE     SI_ID = @InvId
                        )       
                        
        SET @v_INV_TYPE = ( SELECT  INV_TYPE
                            FROM    dbo.SALE_INVOICE_MASTER
                            WHERE   SI_ID = @InvId
                          )          
                                     
                                     
        IF @v_INV_TYPE <> 'I'
            BEGIN                  
                SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                FROM    dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                WHERE   TransactionId = @CreditNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @COutputID
                              )                  
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @COutputID                  
                  
                  
                SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                FROM    dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                WHERE   TransactionId = @CreditNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @OutputID
                              )                  
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @OutputID                  
            END              
        ELSE
            BEGIN                  
                  
                SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                                FROM    dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                WHERE   TransactionId = @CreditNoteId
                                        AND TransactionTypeId = @TransactionTypeId
                                        AND AccountId = @OutputID
                              )                  
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @OutputID                  
            END             
                        
                        
        ------------------Start Cess Entries Deletion ---------------            
                             
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @CreditNoteId
                                AND TransactionTypeId = @TransactionTypeId
                                AND AccountId = @CessOutputID
                      )           
        SET @CashIn = 0           
                             
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @CessOutputID             
                  
                            
        ------------------End Cess Entries Deletion ------------------              
                      
        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @CreditNoteId
                AND TransactionTypeId = @TransactionTypeId               
                         
                      
    END 
    
    GO
    
          
          
ALTER PROCEDURE Proc_GETCreditNoteDetailsByID_Edit
    (
      @CreditNoteId NUMERIC(18, 0)
    )
AS
    BEGIN      
        SELECT  CN.CreditNote_Code + CAST(CN.CreditNote_No AS VARCHAR(20)) AS CreditNoteNumber ,
                dbo.fn_Format(CN.CreditNote_Date) AS CreditNote_Date ,
                CN_CustId ,
                SIM.SI_ID AS Invid ,
                INV_No AS InvoiceNo ,
                dbo.fn_Format(CN.INV_Date) AS InvoiceDate ,
                CN.Remarks AS Remarks ,
                ISNULL(CN.RoundOff, 0) AS RoundOff
        FROM    dbo.CreditNote_Master CN
                INNER JOIN dbo.SALE_INVOICE_MASTER SIM ON SIM.SI_ID = CN.INVId
        WHERE   CreditNote_Id = @CreditNoteId
        UNION ALL
        SELECT  CN.CreditNote_Code + CAST(CN.CreditNote_No AS VARCHAR(20)) AS CreditNoteNumber ,
                dbo.fn_Format(CN.CreditNote_Date) AS CreditNote_Date ,
                CN_CustId ,
                CN.INVId AS Invid ,
                INV_No AS InvoiceNo ,
                dbo.fn_Format(CN.INV_Date) AS InvoiceDate ,
                CN.Remarks AS Remarks ,
                ISNULL(CN.RoundOff, 0) AS RoundOff
        FROM    dbo.CreditNote_Master CN
        WHERE   CreditNote_Id = @CreditNoteId
                AND CN.INVId <= 0  
    END  
      GO
      
              
ALTER PROCEDURE [dbo].[GetCreditNoteDetails]
    @CreditNoteId NUMERIC(18, 0)
AS
    BEGIN                       
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                SD.Balance_Qty AS Prev_Item_Qty ,
                saleID.Item_Qty AS INV_Qty ,
                cd.Item_Rate ,
                cd.Item_Tax AS Vat_Per ,
                cd.Item_Cess AS Cess_Per ,
                CAST(cd.Item_Qty AS NUMERIC(18, 2)) AS Item_Qty ,
                cd.TaxableAmt AS TaxableAmt ,
                cd.TaxAmt AS GST ,
                cd.CessAmt AS Cess ,
                cd.TaxableAmt AS Amount ,
                cd.Item_Rate AS Prv_Rate ,
                cd.Item_Tax AS Prv_Vat_Per ,
                cd.Item_Cess AS Prv_Cess_Per ,
                SIID.Stock_Detail_Id AS Stock_Detail_Id ,
                CONVERT(VARCHAR(20), SIM.CREATION_DATE, 106) AS INvDate ,
                SI_CODE + CAST(SI_NO AS VARCHAR(20)) AS SiNo ,
                CASE WHEN INV_TYPE = 'I' THEN 2
                     WHEN INV_TYPE = 'S' THEN 1
                     WHEN INV_TYPE = 'U' THEN 3
                END AS INV_TYPE
        FROM    dbo.CreditNote_Master CN
                INNER JOIN dbo.CreditNote_DETAIL CD ON CD.CreditNote_Id = CN.CreditNote_Id
                LEFT JOIN dbo.SALE_INVOICE_MASTER SIM ON sim.SI_ID = CN.INVId
                INNER JOIN SALE_INVOICE_DETAIL SaleID ON sim.SI_ID = SaleID.SI_ID
                                                         AND SaleID.Item_ID = cd.Item_ID
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL SIID ON SIID.ITEM_ID = SaleID.ITEM_ID
                                                           AND SIID.SI_ID = SaleID.SI_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON SaleID.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON SIID.Stock_Detail_Id = SD.STOCK_DETAIL_ID
        WHERE   cn.CreditNote_Id = @CreditNoteId
        UNION ALL
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                ISNULL(SD.Balance_Qty, dbo.Get_Current_Stock(IM.ITEM_ID)) AS Prev_Item_Qty ,
                0 AS INV_Qty ,
                cd.Item_Rate ,
                cd.Item_Tax AS Vat_Per ,
                cd.Item_Cess AS Cess_Per ,
                CAST(cd.Item_Qty AS NUMERIC(18, 2)) AS Item_Qty ,
                cd.TaxableAmt AS TaxableAmt ,
                cd.TaxAmt AS GST ,
                cd.CessAmt AS Cess ,
                cd.TaxableAmt AS Amount ,
                0.00 AS Prv_Rate ,
                cd.Item_Tax AS Prv_Vat_Per ,
                cd.Item_Cess AS Prv_Cess_Per ,
                ISNULL(SIID.Stock_Detail_Id, 0) AS Stock_Detail_Id ,
                CONVERT(VARCHAR(20), CN.INV_Date, 106) AS INvDate ,
                CN.INV_No AS SiNo ,
                GSTType AS INV_TYPE
        FROM    dbo.CreditNote_Master CN
                INNER JOIN dbo.CreditNote_DETAIL CD ON CD.CreditNote_Id = CN.CreditNote_Id
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON CD.Item_ID = IM.ITEM_ID
                LEFT JOIN dbo.SALE_INVOICE_STOCK_DETAIL SIID ON SIID.ITEM_ID = CD.ITEM_ID
                                                              AND SIID.SI_ID = CN.INVId
                LEFT JOIN STOCK_DETAIL SD ON SIID.Stock_Detail_Id = SD.STOCK_DETAIL_ID
        WHERE   cn.CreditNote_Id = @CreditNoteId
                AND CN.INVId <= 0        
    END 
    
    GO
    