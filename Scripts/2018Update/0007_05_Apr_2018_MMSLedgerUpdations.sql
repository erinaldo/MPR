
insert INTO dbo.DBScriptUpdateLog
        ( LogFileName, ExecuteDateTime )
VALUES  ( '0007_05_Apr_2018_MMSLedgerUpdations',
          GETDATE()
          )

  Go
Alter PROCEDURE [dbo].[Proc_Ledger_Insert]
    (
      @AccountId NUMERIC(18, 0) ,
      @CashIn NUMERIC(18, 2) ,
      @CashOut NUMERIC(18, 2) ,
      @Remarks VARCHAR(500) ,
      @DivisionId NUMERIC(18, 0) ,
      @TransactionId INT ,
      @TransactionTypeId INT ,
      @TransactionDate DATETIME ,
      @CreatedBy VARCHAR(100)
    )
AS
    BEGIN  
        DECLARE @LedgerId NUMERIC(18, 0)   
    ---------if customer ledger not exists then make entry  
        IF EXISTS ( SELECT  AccountId
                    FROM    dbo.LedgerMaster
                    WHERE   AccountId = @AccountId )
            SELECT  @LedgerId = LedgerId
            FROM    dbo.LedgerMaster
            WHERE   AccountId = @AccountId  
        ELSE
            BEGIN  
                SELECT  @LedgerId = ISNULL(MAX(LedgerId), 0) + 1
                FROM    dbo.LedgerMaster   
                INSERT  INTO dbo.LedgerMaster
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
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @AccountId  
       -------entry in ledger detail  
        DECLARE @LedgerDetailId AS NUMERIC  
        SELECT  @LedgerDetailId = ISNULL(MAX(LedgerDetailId), 0) + 1
        FROM    LedgerDetail  
        INSERT  INTO dbo.LedgerDetail
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
                  @TransactionDate , --GETDATE() , -- TransactionDate - datetime  
                  @CreatedBy  -- CreatedBy - varchar(100)  
                )  
    END  

	Go

Alter PROCEDURE [dbo].[PROC_ADJUSTMENT_DETAIL]
    (
      @v_adjustment_ID DECIMAL ,
      @v_Item_ID DECIMAL ,
      @v_Stock_Detail_Id DECIMAL ,
      @v_Item_Qty NUMERIC(18, 3) ,
      @v_Balance_Qty NUMERIC(18, 3) ,
      @v_Created_By NVARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By NVARCHAR(100) ,
      @v_Modified_Date DATETIME ,
      @v_Division_ID INT ,
      @v_Proc_Type INT ,
      @v_Item_Rate NUMERIC(18, 2)
    )
AS
    BEGIN              
  
      
  
        IF @V_PROC_TYPE = 1
            BEGIN              
  
                INSERT  INTO ADJUSTMENT_DETAIL
                        ( adjustment_ID ,
                          Item_ID ,
                          Stock_Detail_Id ,
                          Item_Qty ,
                          Item_Rate ,
                          Balance_Qty ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modified_Date ,
                          Division_ID          
  
                        )
                VALUES  ( @V_adjustment_ID ,
                          @V_Item_ID ,
                          @v_Stock_Detail_Id ,
                          @V_Item_Qty ,
                          @v_Item_Rate ,
                          @v_Balance_Qty ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modified_Date ,
                          @V_Division_ID          
  
                        )     
        
          
           
  
                IF ( @V_Item_Qty > 0 )
                    BEGIN  
  
                        INSERT  INTO dbo.TransactionLog_Master
                                ( Item_id ,
                                  Issue_Qty ,
                                  Receive_Qty ,
                                  Transaction_Date ,
                                  TransactionType_Id ,
                                  Transaction_Id ,
                                  Division_Id  
  
                                )
                        VALUES  ( @V_Item_ID ,
                                  0 ,
                                  ABS(@V_Item_Qty) ,
                                  @v_Creation_Date ,
                                  1 ,
                                  @V_adjustment_ID ,
                                  @v_Division_ID  
  
                                )   
  
                    END  
  
                ELSE
                    IF ( @V_Item_Qty < 0 )
                        BEGIN  
  
                            INSERT  INTO dbo.TransactionLog_Master
                                    ( Item_id ,
                                      Issue_Qty ,
                                      Receive_Qty ,
                                      Transaction_Date ,
                                      TransactionType_Id ,
                                      Transaction_Id ,
                                      Division_Id  
  
                                    )
                            VALUES  ( @V_Item_ID ,
                                      ABS(@V_Item_Qty) ,
                                      0 ,
                                      @v_Creation_Date ,
                                      1 ,
                                      @V_adjustment_ID ,
                                      @v_Division_ID  
  
                                    )   
  
                        END  
  
            END   
     
        IF @V_PROC_TYPE = 2
            BEGIN    
                DECLARE @Remarks NVARCHAR(250)  
  
                DECLARE @amount NUMERIC(18, 2)  
                SET @amount = ( SELECT  SUM(Item_Qty * Item_Rate)
                                FROM    dbo.ADJUSTMENT_DETAIL
                                WHERE   Adjustment_ID = @V_adjustment_ID
                              )  
  
                SET @Remarks = 'Stock IN against adjustment No- '
                    + CAST(@V_adjustment_ID AS VARCHAR(50))  
                IF @amount > 0
                    BEGIN  
                        EXECUTE Proc_Ledger_Insert 10073, @amount, 0, @Remarks,
                            @V_Division_ID, @V_adjustment_ID, 11,
                            @V_Creation_Date, @V_Created_BY  
                    END  
                ELSE
                    BEGIN  
                        SET @Remarks = 'Stock Out against adjustment No- '
                            + CAST(@V_adjustment_ID AS VARCHAR(50)) 
                             
                        SET @V_Item_Qty = -+@amount  
                        
                        EXECUTE Proc_Ledger_Insert 10073, 0, @V_Item_Qty,
                            @Remarks, @V_Division_ID, @V_adjustment_ID, 11,
                            @V_Creation_Date, @V_Created_BY  
                    END   
            END   
     
       
    END
	GO
Alter PROC [dbo].[PROC_Cancel_Sale_Invoice]
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
        DECLARE @TransactionDate AS DATETIME
        
        SELECT  @V_CUST_ID = CUST_ID ,
                @V_NET_AMOUNT = NET_AMOUNT ,
                @V_Division_ID = DIVISION_ID ,
                @V_Created_BY = CREATED_BY ,
                @V_Si_NO = SI_CODE + CAST(SI_NO AS VARCHAR(50)),
                @TransactionDate = SI_DATE
        FROM    dbo.SALE_INVOICE_MASTER
        WHERE   SI_ID = @SI_ID  
        
        DECLARE @Remarks VARCHAR(250)  
        SET @Remarks = 'Amount Deducted against Cancel Invoice - ' + @V_Si_NO  
        
        EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0, @Remarks,
            @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY   
    END  
	GO
Alter PROCEDURE [dbo].[PROC_CreditNote_MASTER]
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
      @v_CN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
      @v_CreditNote_Type VARCHAR(50) = NULL ,
      @v_RefNo VARCHAR(50) = NULL ,
      @v_RefDate_dt DATETIME = GETDATE ,
      @v_Tax_Amt NUMERIC(18, 2) = 0 ,
      @v_Proc_Type INT  
    )
AS
    BEGIN  
  
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
                          Tax_Amt  
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
                          @v_Tax_Amt  
                        )  
  
                UPDATE  CN_SERIES
                SET     CURRENT_USED = @V_CreditNote_No
                WHERE   DIV_ID = @V_Division_ID  
  
  
  
                DECLARE @Remarks VARCHAR(250)  
                SET @Remarks = 'Credit against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))  
  
  
  
                EXECUTE Proc_Ledger_Insert @v_CN_CustId, @v_CN_Amount, 0,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, 17,
                    @v_CreditNote_Date, @V_Created_BY  
  
                EXECUTE Proc_Ledger_Insert 10071, 0, @v_CN_ItemValue, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, 17, @v_CreditNote_Date, @V_Created_BY 
  
  
                DECLARE @InputID NUMERIC  
                DECLARE @CInputID NUMERIC  
                SET @CInputID = 10017  
  
        
                DECLARE @CGST_Amount NUMERIC(18, 2)  
                SET @CGST_Amount = ( @v_CN_ItemTax / 2 )  
  
         
  
  
                SET @InputID = ( SELECT CASE WHEN INV_TYPE = 'I' THEN 10021
                                             WHEN INV_TYPE = 'S' THEN 10024
                                             WHEN INV_TYPE = 'U' THEN 10075
                                        END AS inputid
                                 FROM   dbo.SALE_INVOICE_MASTER
                                 WHERE  SI_ID = @v_INV_Id
                               )  
  
                DECLARE @v_INV_TYPE VARCHAR(1)  
                SET @v_INV_TYPE = ( SELECT  INV_TYPE
                                    FROM    dbo.SALE_INVOICE_MASTER
                                    WHERE   SI_ID = @v_INV_Id
                                  )  
  
                SET @Remarks = 'GST against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))  
  
                IF @v_INV_TYPE <> 'I'
                    BEGIN  
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID, 17,
                            @v_CreditNote_Date, @V_Created_BY 
  
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID, 17,
                            @v_CreditNote_Date, @V_Created_BY  
                    END  
                ELSE
                    BEGIN  
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_CN_ItemTax,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID, 17,
                            @v_CreditNote_Date, @V_Created_BY   
                    END  
            END  
  
        SET @Remarks = 'Stock In against CreditNote No- ' + @V_CreditNote_Code
            + CAST(@V_CreditNote_No AS VARCHAR(50))  
  
        EXECUTE Proc_Ledger_Insert 10073, @v_CN_Amount, 0, @Remarks,
            @V_Division_ID, @V_CreditNote_ID, 17, @v_CreditNote_Date, @V_Created_BY 
  
  
  
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
                        INV_Date = @v_INV_Date ,
                        CreditNote_Type = @v_CreditNote_Type ,
                        RefNo = @v_RefNo ,
                        RefDate_dt = @v_RefDate_dt ,
                        Tax_Amt = @v_Tax_Amt
                WHERE   CreditNote_ID = @V_CreditNote_ID  
            END  
    END
	GO
Alter PROCEDURE [dbo].[PROC_DebitNote_MASTER]
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
      @v_DN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
      @v_DebitNote_Type VARCHAR(50) = NULL ,
      @v_RefNo VARCHAR(50) = NULL ,
      @v_RefDate_dt DATETIME = GETDATE ,
      @v_Tax_num NUMERIC(18, 2) = 0 ,
      @v_Proc_Type INT  
    )
AS
    BEGIN  
  
  
  
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
                          Tax_num  
  
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
                          @v_Tax_num  
  
                        )  
  
                UPDATE  DN_SERIES
                SET     CURRENT_USED = @V_DebitNote_No
                WHERE   DIV_ID = @V_Division_ID  
  
    --Credit against CreditNote No- MTC/CN/17-18/6  
  
  
                DECLARE @Remarks VARCHAR(250)  
                DECLARE @InputID NUMERIC  
                DECLARE @CInputID NUMERIC= 0  
                SET @CInputID = 10016  
                DECLARE @RoundOff NUMERIC(18, 2)  
                DECLARE @CGST_Amount NUMERIC(18, 2)  
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
  
  
                DECLARE @V_MRN_TYPE NUMERIC= 0  
  
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
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, 7,
                    @v_DebitNote_Date, @V_Created_BY  
  
  
                EXECUTE Proc_Ledger_Insert 10070, @v_DN_ItemValue, 0, @Remarks,
                    @V_Division_ID, @V_DebitNote_ID, 7, @v_DebitNote_Date, @V_Created_BY   
  
  
  
                SET @Remarks = 'GST against DebitNote No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))  
  
                IF @V_MRN_TYPE <> 2
                    BEGIN  
  
  
  
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID, 7,
                            @v_DebitNote_Date, @V_Created_BY   
  
  
                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,
                            @Remarks, @v_Division_ID, @V_DebitNote_ID, 7,
                            @v_DebitNote_Date, @V_Created_BY   
                    END  
  
  
  
                ELSE
                    BEGIN  
                        EXECUTE Proc_Ledger_Insert @InputID, @v_DN_ItemTax, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID, 7,
                            @v_DebitNote_Date, @V_Created_BY   
                    END     
  
  
                SET @Remarks = 'Stock Out against Debit Note No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))  
  
  
  
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_DN_Amount, @Remarks,
                    @V_Division_ID, @V_DebitNote_ID, 7, @v_DebitNote_Date, @V_Created_BY    
  
            END  
  
        IF @V_PROC_TYPE = 2
            BEGIN  
  
                UPDATE  DebitNote_MASTER
                SET     DebitNote_No = @V_DebitNote_No ,
                        DebitNote_Code = @V_DebitNote_Code ,
                        DebitNote_Date = @v_DebitNote_Date ,
                        Remarks = @V_Remarks ,
                        MRNId = @V_MRN_Id ,
                        Created_BY = @V_Created_BY ,
                        Creation_Date = @V_Creation_Date ,
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
                        Tax_num = @v_Tax_num
                WHERE   DebitNote_ID = @V_DebitNote_ID  
  
            END  
    END
	GO
Alter PROCEDURE [dbo].[PROC_ITEM_DETAIL]
    (
      @v_ITEM_ID DECIMAL ,
      @v_DIV_ID INT ,
      @v_RE_ORDER_LEVEL DECIMAL ,
      @v_RE_ORDER_QTY DECIMAL ,
      @v_PURCHASE_VAT_ID INT ,
      @v_SALE_VAT_ID INT ,
      @v_OPENING_STOCK NUMERIC(18, 3) ,
      @v_CURRENT_STOCK DECIMAL ,
      @v_IS_EXTERNAL INT ,
      @v_TRANSFER_RATE DECIMAL ,
      @v_OPENING_RATE NUMERIC(18, 2) ,
      @v_AVERAGE_RATE DECIMAL ,
      @v_IS_STOCKABLE BIT ,
      @v_IS_ACTIVE BIT ,
      @v_Proc_Type INT ,
      @v_Batch_no VARCHAR(50) ,
      @v_Expiry_Date DATETIME ,
      @V_Trans_Type INT   
  
    )
AS
    BEGIN   
  
        DECLARE @intErrorCode INT   
        
         
  
        IF @V_PROC_TYPE = 1
            BEGIN              
  
  
  
                BEGIN TRAN    
                DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0) 
                
                DECLARE @v_creation_date DATETIME  
                SELECT  @v_creation_date = GETDATE()    
  
                --it will insert entry in stock detail table and   
                --return stock_detail_id. 
  
  
                EXEC INSERT_STOCK_DETAIL @v_Item_ID, @v_Batch_no,
                    @v_Expiry_Date, @v_OPENING_STOCK, 0, @V_Trans_Type,
                    @STOCK_DETAIL_ID OUTPUT
  
  
                EXEC INSERT_TRANSACTION_LOG 0, @v_Item_ID, @V_Trans_Type,
                    @STOCK_DETAIL_ID, @V_OPENING_STOCK, @v_creation_date, 0                   
  
                INSERT  INTO ITEM_DETAIL
                        ( ITEM_ID ,
                          DIV_ID ,
                          RE_ORDER_LEVEL ,
                          RE_ORDER_QTY ,
                          PURCHASE_VAT_ID ,
                          SALE_VAT_ID ,
                          OPENING_STOCK ,
                          CURRENT_STOCK ,
                          IS_EXTERNAL ,
                          TRANSFER_RATE ,
                          AVERAGE_RATE ,
                          OPENING_RATE ,
                          IS_STOCKABLE ,
                          IS_ACTIVE ,
                          STOCK_DETAIL_ID  
                        )
                VALUES  ( @V_ITEM_ID ,
                          @V_DIV_ID ,
                          @V_RE_ORDER_LEVEL ,
                          @V_RE_ORDER_QTY ,
                          @V_PURCHASE_VAT_ID ,
                          @V_SALE_VAT_ID ,
                          @V_OPENING_STOCK ,
                          @V_CURRENT_STOCK ,
                          @V_IS_EXTERNAL ,
                          @V_TRANSFER_RATE ,
                          @V_AVERAGE_RATE ,
                          @v_OPENING_RATE ,
                          @V_IS_STOCKABLE ,
                          @v_IS_ACTIVE ,
                          @STOCK_DETAIL_ID  
                        )                
  
  
  
                DECLARE @Remarks NVARCHAR(250)  
                DECLARE @V_NET_AMOUNT NUMERIC(18, 2)      
  
                SET @Remarks = 'Stock In against Opening Item Id- '
                    + CAST(@V_ITEM_ID AS VARCHAR(50))  
  
                SET @V_NET_AMOUNT = @v_OPENING_STOCK * @v_OPENING_RATE  
  
  
  
                EXECUTE Proc_Ledger_Insert 10073, @V_NET_AMOUNT, 0, @Remarks,
                    @v_DIV_ID, @V_ITEM_ID, 10, @v_creation_date, 'user'  
  
                SELECT  @intErrorCode = @@ERROR  
  
  
                IF ( @intErrorCode <> 0 )
                    GOTO PROBLEM    
  
                COMMIT TRAN  
                PROBLEM:   
  
                IF ( @intErrorCode <> 0 )
                    BEGIN 
                        PRINT 'Unexpected error occurred!' 
                        ROLLBACK TRAN    
                    END      
            END              
  
  
        IF @V_PROC_TYPE = 2
            BEGIN                
  
  
  
                UPDATE  ITEM_DETAIL
                SET     RE_ORDER_LEVEL = @V_RE_ORDER_LEVEL ,
                        RE_ORDER_QTY = @V_RE_ORDER_QTY ,
                        PURCHASE_VAT_ID = @V_PURCHASE_VAT_ID ,
                        SALE_VAT_ID = @V_SALE_VAT_ID ,
                        OPENING_STOCK = @V_OPENING_STOCK ,
                        IS_EXTERNAL = @V_IS_EXTERNAL ,
                        TRANSFER_RATE = @V_TRANSFER_RATE ,
                        AVERAGE_RATE = @V_AVERAGE_RATE ,
                        OPENING_RATE = @v_OPENING_RATE ,
                        IS_STOCKABLE = @V_IS_STOCKABLE ,
                        IS_ACTIVE = @v_IS_ACTIVE
                WHERE   ITEM_ID = @V_ITEM_ID
                        AND DIV_ID = @V_DIV_ID                
  
  
  
                DECLARE @CashOut NUMERIC(18, 2)  
                DECLARE @CashIn NUMERIC(18, 2) 
                DECLARE @TransactionDate DATETIME 
                SET @CashIn = 0   
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @V_ITEM_ID
                                        AND TransactionTypeId = 10
                                        AND AccountId = 10073
                               ) 
                               
                SET @TransactionDate = ( SELECT TransactionDate
                                         FROM   dbo.LedgerDetail
                                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                         WHERE  TransactionId = @V_ITEM_ID
                                                AND TransactionTypeId = 10
                                                AND AccountId = 10073
                                       )
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = 10073  
            
  
  
  
                DELETE  FROM dbo.LedgerDetail
                WHERE   TransactionId = @V_ITEM_ID
                        AND TransactionTypeId = 10  
  
                         
                SET @Remarks = 'Stock In against Opening Item Id- '
                    + CAST(@V_ITEM_ID AS VARCHAR(50))  
                SET @V_NET_AMOUNT = @v_OPENING_STOCK * @v_OPENING_RATE  
  
                EXECUTE Proc_Ledger_Insert 10073, @V_NET_AMOUNT, 0, @Remarks,
                    @v_DIV_ID, @V_ITEM_ID, 10, @TransactionDate, 'user' 
  
  
  
            END     
  
  
        UPDATE  dbo.STOCK_DETAIL
        SET     Item_Qty = @V_OPENING_STOCK ,
                Balance_Qty = @V_OPENING_STOCK - Issue_Qty
        WHERE   STOCK_DETAIL_ID = ( SELECT  stock_detail_id
                                    FROM    dbo.ITEM_DETAIL
                                    WHERE   dbo.ITEM_DETAIL.ITEM_ID = @V_ITEM_ID
                                            AND item_detail.DIV_ID = @V_DIV_ID
                                  )  
    END
	GO
Alter PROCEDURE [dbo].[PROC_MATERIAL_RECIEVED_AGAINST_PO_MASTER]
    (
      @v_Receipt_ID NUMERIC(18, 0) ,
      @v_Receipt_No NUMERIC(18, 0) ,
      @v_Receipt_Code VARCHAR(20) ,
      @v_PO_ID NUMERIC(18, 0) ,
      @v_Receipt_Date DATETIME ,
      @v_Remarks VARCHAR(500) ,
      @v_MRN_NO NUMERIC(18, 0) ,
      @v_MRN_PREFIX VARCHAR(50) ,
      @v_Created_BY VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Division_ID NUMERIC(18, 0) ,
      @v_Proc_Type INT ,
      @V_mrn_status INT ,
      @v_freight NUMERIC(18, 2) ,
      @v_Other_Charges NUMERIC(18, 2) ,
      @v_Discount_amt NUMERIC(18, 2) ,
      @v_GROSS_AMOUNT NUMERIC(18, 2) ,
      @v_GST_AMOUNT NUMERIC(18, 2) ,
      @v_NET_AMOUNT NUMERIC(18, 2) ,
      @V_MRN_TYPE INT ,
      @V_VAT_ON_EXICE INT ,
      @v_Invoice_No NVARCHAR(50) ,
      @v_Invoice_Date DATETIME ,
      @V_CUST_ID NUMERIC(18, 0) ,
      @v_MRNCompanies_ID INT     
    
    )
AS
    BEGIN      
        IF @V_PROC_TYPE = 1
            BEGIN    
    
    
    
                DECLARE @Remarks VARCHAR(250)    
                DECLARE @InputID NUMERIC    
                DECLARE @CInputID NUMERIC    
                SET @CInputID = 10016    
                DECLARE @RoundOff NUMERIC(18, 2)    
                DECLARE @CGST_Amount NUMERIC(18, 2)    
                SET @CGST_Amount = ( @v_GST_AMOUNT / 2 )    
                SET @RoundOff = ( @v_freight + @v_Other_Charges )    
    
                SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10023
                                             WHEN @V_MRN_TYPE = 2 THEN 10020
                                             WHEN @V_MRN_TYPE = 3 THEN 10074
                                        END AS inputid
                               ) 
    
    
                SET @Remarks = 'Pruchase against party bill no.: '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)     
    
                EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3, @v_Receipt_Date, @V_Created_BY        
    
                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Receipt_ID, 3, @v_Receipt_Date, @V_Created_BY     
    
    
    
                SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)     
    
                IF @V_MRN_TYPE <> 2
                    BEGIN      
    
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY     
    
    
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @v_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY     
                    END  
    
    
                ELSE
                    BEGIN    
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY     
                    END       
    
    
                SET @Remarks = 'Round Off against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)    
    
                IF @RoundOff > 0
                    BEGIN    
    
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY     
    
                    END    
    
                ELSE
                    BEGIN    
    
                        SET @RoundOff = -+@RoundOff    
    
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @v_Receipt_Date, @V_Created_BY     
    
                    END     
    
    
                SET @Remarks = 'Stock In against party  invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)    
    
    
    
                EXECUTE Proc_Ledger_Insert 10073, @v_NET_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @v_Receipt_ID, 3, @v_Receipt_Date, @V_Created_BY     
    
    
                SELECT  @V_Receipt_No = ISNULL(MAX(Receipt_No), 0) + 1
                FROM    MATERIAL_RECEIVED_AGAINST_PO_MASTER                  
    
                INSERT  INTO MATERIAL_RECEIVED_AGAINST_PO_MASTER
                        ( Receipt_ID ,
                          Receipt_No ,
                          Receipt_Code ,
                          Invoice_No ,
                          Invoice_Date ,
                          PO_ID ,
                          Receipt_Date ,
                          Remarks ,
                          MRN_NO ,
                          MRN_PREFIX ,
                          Created_BY ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_ID ,
                          MRN_STATUS ,
                          freight ,
                          Other_charges ,
                          Discount_amt ,
                          GROSS_AMOUNT ,
                          GST_AMOUNT ,
                          NET_AMOUNT ,
                          MRN_TYPE ,
                          VAT_ON_EXICE ,
                          MRNCompanies_ID ,
                          IsPrinted ,
                          CUST_ID      
    
    
    
                        )
                VALUES  ( @V_Receipt_ID ,
                          @V_Receipt_No ,
                          @V_Receipt_Code ,
                          @v_Invoice_No ,
                          @v_Invoice_Date ,
                          @V_PO_ID ,
                          @v_Receipt_Date ,
                          @V_Remarks ,
                          @V_MRN_NO ,
                          @V_MRN_PREFIX ,
                          @V_Created_BY ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_ID ,
                          @V_mrn_status ,
                          @v_freight ,
                          @v_other_charges ,
                          @v_Discount_amt ,
                          @v_GROSS_AMOUNT ,
                          @v_GST_AMOUNT ,
                          @v_NET_AMOUNT ,
                          @v_MRN_TYPE ,
                          @V_VAT_ON_EXICE ,
                          @v_MRNCompanies_ID ,
                          0 ,
                          @V_CUST_ID      
    
    
    
                        )                  
    
                UPDATE  MRN_SERIES
                SET     CURRENT_USED = CURRENT_USED + 1
                WHERE   DIV_ID = @v_Division_ID                    
    
    
    
    
    
    
    
    
        
                RETURN @V_MRN_NO         
    
            END                  
    
    
        IF @V_PROC_TYPE = 2
            BEGIN                  
    
                UPDATE  MATERIAL_RECEIVED_AGAINST_PO_MASTER
                SET     PO_ID = @V_PO_ID ,
                        Receipt_Date = @v_Receipt_Date ,
                        Remarks = @V_Remarks ,
                        Created_BY = @V_Created_BY ,
                        Creation_Date = @V_Creation_Date ,
                        Modified_By = @V_Modified_By ,
                        Modification_Date = @V_Modification_Date ,
                        Division_ID = @V_Division_ID ,
                        freight = @v_freight ,
                        Other_charges = @v_Other_Charges ,
                        Discount_amt = @v_Discount_amt ,
                        VAT_ON_EXICE = @V_VAT_ON_EXICE ,
                        MRNCompanies_ID = @v_MRNCompanies_ID
                WHERE   Receipt_ID = @V_Receipt_ID                  
    
    
            END                  
    
    
        --IF @V_PROC_TYPE = 3    
        --    BEGIN                    
    
        --        DECLARE cur CURSOR    
        --        FOR    
        --            SELECT  Item_ID ,    
        --                    Item_Qty ,    
        --                    Indent_ID    
        --            FROM    MATERIAL_RECEIVED_AGAINST_PO_DETAIL    
        --            WHERE   Receipt_ID = @V_Receipt_ID                    
    
                    
    
        --        DECLARE @itemid NUMERIC(18, 0)                    
    
        --        DECLARE @itemQty NUMERIC(18, 4)                    
    
        --        DECLARE @IndentID NUMERIC(18, 0)                    
    
                    
    
                    
    
        --        OPEN cur                    
    
        --        FETCH NEXT FROM cur INTO @itemid, @itemQty, @IndentID                    
    
        --        WHILE @@fetch_status = 0    
        --            BEGIN                    
    
                       
    
        --                UPDATE  PO_STATUS    
        --                SET     RECIEVED_QTY = RECIEVED_QTY - @itemQty ,    
        --                        BALANCE_QTY = BALANCE_QTY + @itemQty    
        --                WHERE   PO_ID = @V_PO_ID    
        --                        AND ITEM_ID = @itemid    
        --                        AND INDENT_ID = @IndentID                    
    
                    
    
        --                UPDATE  ITEM_DETAIL    
        --                SET     CURRENT_STOCK = CURRENT_STOCK - @itemQty    
        --                WHERE   ITEM_ID = @itemid    
        --                        AND DIV_ID = @V_Division_ID    
    
        --                FETCH NEXT FROM cur INTO @itemid, @itemQty, @IndentID    
        --            END    
        --        CLOSE cur    
        --        DEALLOCATE cur    
    
        --        DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_DETAIL    
        --        WHERE   Receipt_ID = @V_Receipt_ID    
     
        --        DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_MASTER    
        --        WHERE   Receipt_ID = @V_Receipt_ID     
        --    END      
       
    END 


  Go
Alter PROCEDURE [dbo].[PROC_MATERIAL_RECIEVED_WITHOUT_PO_MASTER]
    (
      @v_Received_ID NUMERIC(18, 0) ,
      @v_Received_Code VARCHAR(20) ,
      @v_Received_No NUMERIC(18, 0) ,
      @v_Received_Date DATETIME ,
      @v_Purchase_Type INT ,
      @v_Vendor_ID INT ,
      @v_Remarks VARCHAR(100) ,
      @v_Po_ID NUMERIC(18, 0) ,
      @v_MRN_PREFIX VARCHAR(50) ,
      @v_MRN_NO NUMERIC(18, 0) ,
      @v_Created_By VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Invoice_No NVARCHAR(50) ,
      @v_Invoice_Date DATETIME ,
      @v_Division_ID INT ,
      @v_mrn_status INT ,
      @v_freight NUMERIC(18, 2) = NULL ,
      @v_freight_type CHAR = '' ,
      @v_Proc_Type INT ,
      @v_Other_charges NUMERIC(18, 2) = NULL ,
      @v_Discount_amt NUMERIC(18, 2) = NULL ,
      @v_GROSS_AMOUNT NUMERIC(18, 2) ,
      @v_GST_AMOUNT NUMERIC(18, 2) ,
      @v_NET_AMOUNT NUMERIC(18, 2) ,
      @V_MRN_TYPE INT ,
      @V_VAT_ON_EXICE INT ,
      @v_MRNCompanies_ID INT        
      
    )
AS
    BEGIN       
        DECLARE @Remarks VARCHAR(250)      
        DECLARE @InputID NUMERIC      
        DECLARE @CInputID NUMERIC      
        SET @CInputID = 10016      
        DECLARE @RoundOff NUMERIC(18, 2)      
        DECLARE @CGST_Amount NUMERIC(18, 2)      
        SET @CGST_Amount = ( @v_GST_AMOUNT / 2 )      
        SET @RoundOff = ( @v_freight + @v_Other_Charges )      
      
        SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10023
                                     WHEN @V_MRN_TYPE = 2 THEN 10020
                                     WHEN @V_MRN_TYPE = 3 THEN 10074
                                END AS inputid
                       )     
        IF @V_PROC_TYPE = 1
            BEGIN        
      
                INSERT  INTO MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                        ( Received_ID ,
                          Received_Code ,
                          Received_No ,
                          Received_Date ,
                          Purchase_Type ,
                          Vendor_ID ,
                          Remarks ,
                          Po_ID ,
                          MRN_PREFIX ,
                          MRN_NO ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Invoice_No ,
                          Invoice_Date ,
                          Division_ID ,
                          mrn_status ,
                          freight ,
                          Other_charges ,
                          Discount_amt ,
                          freight_type ,
                          MRNCompanies_ID ,
                          GROSS_AMOUNT ,
                          GST_AMOUNT ,
                          NET_AMOUNT ,
                          MRN_TYPE ,
                          VAT_ON_EXICE ,
                          IsPrinted      
                        )
                VALUES  ( @V_Received_ID ,
                          @V_Received_Code ,
                          @V_Received_No ,
                          @V_Received_Date ,
                          @V_Purchase_Type ,
                          @V_Vendor_ID ,
                          @V_Remarks ,
                          @V_Po_ID ,
                          @V_MRN_PREFIX ,
                          @V_MRN_NO ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @v_Invoice_No ,
                          @v_Invoice_Date ,
                          @V_Division_ID ,
                          @V_mrn_status ,
                          @v_freight ,
                          @v_Other_charges ,
                          @v_Discount_amt ,
                          @v_freight_type ,
                          @v_MRNCompanies_ID ,
                          @v_GROSS_AMOUNT ,
                          @v_GST_AMOUNT ,
                          @v_NET_AMOUNT ,
                          @v_MRN_TYPE ,
                          @V_VAT_ON_EXICE ,
                          0        
      
                        )        
      
      
                UPDATE  MRN_SERIES
                SET     CURRENT_USED = CURRENT_USED + 1
                WHERE   DIV_ID = @v_Division_ID        
      
                SET @Remarks = 'Pruchase against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)       
      
      
                EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2, @V_Received_Date, @V_Created_BY        
      
      
      
                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @V_Received_Date, @V_Created_BY        
      
      
                SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)       
      
                IF @V_MRN_TYPE <> 2
                    BEGIN      
      
      
      
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Received_Date, @V_Created_BY      
      
      
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @v_Division_ID, @V_Received_ID, 2,
                            @V_Received_Date, @V_Created_BY      
                    END      
      
      
      
                ELSE
                    BEGIN      
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Received_Date, @V_Created_BY      
                    END         
      
      
                SET @Remarks = 'Round Off against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)      
      
                IF @RoundOff > 0
                    BEGIN      
      
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Received_Date, @V_Created_BY      
      
                    END      
      
                ELSE
                    BEGIN      
      
                        SET @RoundOff = -+@RoundOff      
      
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Received_Date, @V_Created_BY      
      
                    END       
      
      
                SET @Remarks = 'Stock In against party  invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)      
      
      
      
                EXECUTE Proc_Ledger_Insert 10073, @v_NET_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @V_Received_Date, @V_Created_BY      
      
      
            END         
      
    
        
        IF @V_PROC_TYPE = 2
            BEGIN        
                
                DECLARE @TransactionDate DATETIME
                
                EXEC Proc_ReverseMRNEntry @V_Received_ID, @V_Vendor_ID  
                
                SELECT @TransactionDate = Received_Date FROM MATERIAL_RECIEVED_WITHOUT_PO_MASTER WHERE   Received_ID = @V_Received_ID
                
                UPDATE  MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                SET     Purchase_Type = @V_Purchase_Type ,
                        Vendor_ID = @V_Vendor_ID ,
                        Remarks = @V_Remarks ,
                        Modified_By = @V_Modified_By ,
                        Modification_Date = @V_Modification_Date ,
                        Invoice_No = @v_Invoice_No ,
                        Invoice_Date = @v_Invoice_Date ,
                        mrn_status = @V_mrn_status ,
                        freight = @v_freight ,
                        Other_charges = @v_Other_charges ,
                        Discount_amt = @v_Discount_amt ,
                        freight_type = @v_freight_type ,
                        MRNCompanies_ID = @v_MRNCompanies_ID ,
                        GROSS_AMOUNT = @v_GROSS_AMOUNT ,
                        GST_AMOUNT = @v_GST_AMOUNT ,
                        NET_AMOUNT = @v_NET_AMOUNT ,
                        MRN_TYPE = @v_MRN_TYPE ,
                        VAT_ON_EXICE = @V_VAT_ON_EXICE
                WHERE   Received_ID = @V_Received_ID      
      
      
      
                SET @Remarks = 'Pruchase against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)       
      
      
                EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2, @TransactionDate, @V_Created_BY        
      
      
      
                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @TransactionDate, @V_Created_BY        
      
      
                SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)       
      
                IF @V_MRN_TYPE <> 2
                    BEGIN  
      
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY      
      
      
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @v_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY      
                    END
      
                ELSE
                    BEGIN      
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY      
                    END         
      
      
                SET @Remarks = 'Round Off against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)      
      
                IF @RoundOff > 0
                    BEGIN      
      
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY      
      
                    END      
      
                ELSE
                    BEGIN      
      
                        SET @RoundOff = -+@RoundOff      
      
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @TransactionDate, @V_Created_BY      
      
                    END       
      
      
                SET @Remarks = 'Stock In against party  invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)      
      
      
      
                EXECUTE Proc_Ledger_Insert 10073, @v_NET_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @TransactionDate, @V_Created_BY    
      
            END      
    END
	Go
Alter PROCEDURE [dbo].[PROC_OUTSIDE_SALE_MASTER_SALE_NEW]
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
      @v_GROSS_AMOUNT DECIMAL(18, 2) ,
      @v_VAT_AMOUNT DECIMAL(18, 2) ,
      @v_NET_AMOUNT DECIMAL(18, 0) ,
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
      @V_MODE INT ,
      @V_Flag INT = 0  
  
  
  
    )
AS
    BEGIN 
  
        DECLARE @InputID NUMERIC 
        DECLARE @CInputID NUMERIC 
        SET @CInputID = 10017    
        DECLARE @RoundOff NUMERIC(18, 2)
        DECLARE @CGST_Amount NUMERIC(18, 2) 
        SET @CGST_Amount = ( @v_VAT_AMOUNT / 2 )   
        SET @RoundOff = @v_NET_AMOUNT - ( @v_GROSS_AMOUNT + @v_VAT_AMOUNT )    
  
        SET @InputID = ( SELECT CASE WHEN @v_INV_TYPE = 'I' THEN 10021
                                     WHEN @v_INV_TYPE = 'S' THEN 10024
                                     WHEN @v_INV_TYPE = 'U' THEN 10075
                                END AS inputid
                       )
  
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
                          TRANSPORT ,
                          SHIPP_ADD_ID ,
                          LR_NO ,
                          INV_TYPE ,
                          Flag 
  
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
                          @V_TRANSPORT ,
                          @v_SHIPP_ADD_ID ,
                          @v_LR_NO ,
                          @v_INV_TYPE ,
                          @V_Flag  
  
                        )   
  
  
  
                DECLARE @Remarks VARCHAR(250)
  
                SET @Remarks = 'Sale against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))    
  
                EXECUTE Proc_Ledger_Insert @V_CUST_ID, 0, @V_NET_AMOUNT,
                    @Remarks, @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE, @V_Created_BY   
  
       
  
                EXECUTE Proc_Ledger_Insert 10071, @V_GROSS_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE, @V_Created_BY  
  
                SET @Remarks = 'GST against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))  
  
                IF @v_INV_TYPE <> 'I'
                    BEGIN  
  
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @V_SI_DATE, @V_Created_BY  
  
                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @V_SI_DATE, @V_Created_BY  
  
                    END  
  
                ELSE
                    BEGIN  
  
                        EXECUTE Proc_Ledger_Insert @InputID, @v_VAT_AMOUNT, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @V_SI_DATE, @V_Created_BY  
  
                    END  
  
  
  
                SET @Remarks = 'Round Off against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))  
  
       
  
                IF @RoundOff > 0
                    BEGIN  
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @V_SI_DATE, @V_Created_BY  
                    END  
                ELSE
                    BEGIN  
                        SET @RoundOff = -+@RoundOff  
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @V_SI_DATE, @V_Created_BY  
                    END   
  
  
                SET @Remarks = 'Stock out against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))  
  
                EXECUTE Proc_Ledger_Insert 10073, 0, @V_NET_AMOUNT, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_SI_DATE, @V_Created_BY  
  
  
            END          
  
  
  
        IF @V_MODE = 2
            BEGIN    
				
				
				DECLARE @TransactionDate DATETIME
				
				SELECT @TransactionDate = SI_DATE FROM SALE_INVOICE_MASTER WHERE   SI_ID = @V_SI_ID  
  
  
                UPDATE  SALE_INVOICE_MASTER
                SET     CUST_ID = @V_CUST_ID ,
                        INVOICE_STATUS = @V_INVOICE_STATUS ,
                        REMARKS = @V_REMARKS ,
                        PAYMENTS_REMARKS = @V_PAYMENTS_REMARKS ,
                        SALE_TYPE = @V_SALE_TYPE ,
                        GROSS_AMOUNT = @V_GROSS_AMOUNT ,
                        VAT_AMOUNT = @V_VAT_AMOUNT ,
                        NET_AMOUNT = @V_NET_AMOUNT ,
                        IS_SAMPLE = @V_IS_SAMPLE ,
                        DELIVERY_NOTE_NO = @V_DELIVERY_NOTE_NO ,
                        VAT_CST_PER = @V_VAT_CST_PER ,
                        SAMPLE_ADDRESS = @V_SAMPLE_ADDRESS ,   
                        --CREATED_BY = @V_CREATED_BY ,
                        --CREATION_DATE = @V_CREATION_DATE ,  
                        MODIFIED_BY = @V_MODIFIED_BY ,
                        MODIFIED_DATE = @V_MODIFIED_DATE ,
                        DIVISION_ID = @V_DIVISION_ID ,
                        VEHICLE_NO = @V_VEHICLE_NO ,
                        TRANSPORT = @V_TRANSPORT ,
                        SHIPP_ADD_ID = @v_SHIPP_ADD_ID ,
                        LR_NO = @v_LR_NO ,
                        INV_TYPE = @v_INV_TYPE
                WHERE   SI_ID = @V_SI_ID       
  
  
  
                SET @Remarks = 'Sale against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))    
  
                SET @Remarks = 'Sale against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))  
  
  
  
                EXECUTE Proc_Ledger_Insert @V_CUST_ID, 0, @V_NET_AMOUNT,
                    @Remarks, @V_Division_ID, @V_SI_ID, 16, @TransactionDate, @V_Created_BY   
  
       
  
                EXECUTE Proc_Ledger_Insert 10071, @V_GROSS_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @TransactionDate, @V_Created_BY  
  
                 
  
                SET @Remarks = 'GST against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))  
  
                IF @v_INV_TYPE <> 'I'
                    BEGIN  
  
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @TransactionDate, @V_Created_BY  
  
  
  
                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @TransactionDate, @V_Created_BY  
  
                    END  
  
                ELSE
                    BEGIN  
  
                        EXECUTE Proc_Ledger_Insert @InputID, @v_VAT_AMOUNT, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @TransactionDate, @V_Created_BY  
  
                    END     
  
  
                SET @Remarks = 'Round Off against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))  
  
                IF @RoundOff > 0
                    BEGIN  
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @TransactionDate, @V_Created_BY  
                    END  
                ELSE
                    BEGIN  
                        SET @RoundOff = -+@RoundOff  
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @TransactionDate, @V_Created_BY  
                    END   
  
  
  
                SET @Remarks = 'Stock out against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))  
  
                EXECUTE Proc_Ledger_Insert 10073, 0, @V_NET_AMOUNT, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @TransactionDate, @V_Created_BY
  
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

  GO
  Alter PROC [dbo].[Proc_UpdatePaymentStauts]
    (
      @PaymentTransactionId [numeric](18, 0) ,
      @CancellationCharges [numeric](18, 2) ,
      @StatusId NUMERIC ,
      @PM_TYPE NUMERIC(18, 0)
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
        DECLARE @BankId NUMERIC = 0    
        DECLARE @ChequeDraftNo VARCHAR(50) = NULL   
        DECLARE @TransactionDate DATETIME    
    
        ----set parametrs    
        SELECT  @AccountId = AccountId ,
                @TotalamountReceived = TotalAmountReceived ,
                @PaymentTransactionNo = PaymentTransactionNo ,
                @CreatedBy = CreatedBy ,
                @DivisionId = DivisionId ,
                @BankId = BankId ,
                @ChequeDraftNo = ChequeDraftNo ,
                @TransactionDate = PaymentDate
        FROM    dbo.PaymentTransaction
        WHERE   PaymentTransactionId = @PaymentTransactionId    
    
        ---update customer ledger    
  ---if payment status approved than make entry in customer ledger    
    
        DECLARE @Remarks VARCHAR(200)     
    
        IF @StatusId = 2
            BEGIN    
    
                IF @PM_TYPE = 1
                    BEGIN    
    
                        SET @Remarks = 'Payment received against reference no.: '
                            + @ChequeDraftNo      
    
                        EXECUTE Proc_Ledger_Insert @AccountId,
                            @TotalamountReceived, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 18, @TransactionDate,
                            @CreatedBy     
            
                        EXECUTE Proc_Ledger_Insert @BankId, 0,
                            @TotalamountReceived, @Remarks, @DivisionId,
                            @PaymentTransactionId, 18, @TransactionDate,
                            @CreatedBy     
    
                    END    
    
    
    
                IF @PM_TYPE = 2
                    BEGIN    
    
                        SET @Remarks = 'Payment released against reference no.: '
                            + @ChequeDraftNo      
    
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @TotalamountReceived, @Remarks, @DivisionId,
                            @PaymentTransactionId, 19, @TransactionDate,
                            @CreatedBy     
    
                        EXECUTE Proc_Ledger_Insert @BankId,
                            @TotalamountReceived, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 19, @TransactionDate,
                            @CreatedBy     
    
    
                    END    
                IF @PM_TYPE = 3
                    BEGIN    
                        SET @Remarks = 'Journal Entry against reference no.: '
                            + @ChequeDraftNo      
    
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @TotalamountReceived, @Remarks, @DivisionId,
                            @PaymentTransactionId, 21, @TransactionDate,
                            @CreatedBy     
    
                        EXECUTE Proc_Ledger_Insert @BankId,
                            @TotalamountReceived, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 21, @TransactionDate,
                            @CreatedBy     
    
                    END     
                        
                IF @PM_TYPE = 4
                    BEGIN    
                        SET @Remarks = 'Contra Entry against reference no.: '
                            + @ChequeDraftNo      
    
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @TotalamountReceived, @Remarks, @DivisionId,
                            @PaymentTransactionId, 22, @TransactionDate,
                            @CreatedBy     
    
                        EXECUTE Proc_Ledger_Insert @BankId,
                            @TotalamountReceived, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 22, @TransactionDate,
                            @CreatedBy     
    
                    END     
                        
                IF @PM_TYPE = 5
                    BEGIN    
                        SET @Remarks = 'Expense Entry against reference no.: '
                            + @ChequeDraftNo      
    
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @TotalamountReceived, @Remarks, @DivisionId,
                            @PaymentTransactionId, 21, @TransactionDate,
                            @CreatedBy     
    
                        EXECUTE Proc_Ledger_Insert @BankId,
                            @TotalamountReceived, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 21, @TransactionDate,
                            @CreatedBy     
    
                    END     
            END    
    
    
    
    
        ---if payment status bounced with cancellation charges than make entry in customer ledger     
        SET @Remarks = 'Payment Cancelation Charges against ' + @ChequeDraftNo    
    
        IF @StatusId = 4
            AND @CancellationCharges > 0
            BEGIN    
                IF @PM_TYPE = 1
                    BEGIN    
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @CancellationCharges, @Remarks, @DivisionId,
                            @PaymentTransactionId, 18, @TransactionDate,
                            @CreatedBy     
                    END    
    
                IF @PM_TYPE = 2
                    BEGIN    
    
                        EXECUTE Proc_Ledger_Insert @AccountId,
                            @CancellationCharges, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 18, @TransactionDate,
                            @CreatedBy     
                    END     
    
            END    
    
    END

 GO
Alter PROCEDURE [dbo].[ProcPaymentTransaction_Insert]
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
      @PM_TYPE NUMERIC(18, 0) = NULL ,
      @ProcedureStatus INT OUTPUT     
    
    )
AS
    BEGIN            
        DECLARE @LedgerId NUMERIC  
        DECLARE @TransactionDate DATETIME      
    
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
                  BankDate ,
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
                  @BankDate ,
                  @PM_TYPE     
    
                )       
    
        SET @ProcedureStatus = @PaymentTransactionId    
          
        SET @TransactionDate = GETDATE()  
    
        ---increment series    
    
        UPDATE  PM_SERIES
        SET     CURRENT_USED = CURRENT_USED + 1
        WHERE   DIV_ID = @DivisionId
                AND IS_FINISHED = 'N'
                AND PM_TYPE = @PM_TYPE      
    
  ---update customer ledger     
    
  ---if payment status approved   than make entry in customer ledger    
        IF @StatusId = 2
            BEGIN      
                IF @PM_TYPE = 1
                    BEGIN      
    
                        SET @Remarks = 'Payment received against reference no.: '
                            + @ChequeDraftNo      
    
                        EXECUTE Proc_Ledger_Insert @AccountId,
                            @TotalamountReceived, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 18, @TransactionDate,
                            @CreatedBy     
            
                        EXECUTE Proc_Ledger_Insert @BankId, 0,
                            @TotalamountReceived, @Remarks, @DivisionId,
                            @PaymentTransactionId, 18, @TransactionDate,
                            @CreatedBy     
    
                    END      
                IF @PM_TYPE = 2
                    BEGIN    
                        SET @Remarks = 'Payment released against reference no.: '
                            + @ChequeDraftNo      
    
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @TotalamountReceived, @Remarks, @DivisionId,
                            @PaymentTransactionId, 19, @TransactionDate,
                            @CreatedBy      
    
                        EXECUTE Proc_Ledger_Insert @BankId,
                            @TotalamountReceived, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 19, @TransactionDate,
                            @CreatedBy     
    
                    END    
                        
                IF @PM_TYPE = 3
                    BEGIN    
                        SET @Remarks = 'Journal Entry against reference no.: '
                            + @ChequeDraftNo       
    
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @TotalamountReceived, @Remarks, @DivisionId,
                            @PaymentTransactionId, 21, @TransactionDate,
                            @CreatedBy      
    
                        EXECUTE Proc_Ledger_Insert @BankId,
                            @TotalamountReceived, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 21, @TransactionDate,
                            @CreatedBy     
    
                    END     
                        
                IF @PM_TYPE = 4
                    BEGIN    
                        SET @Remarks = 'Contra Entry against reference no.: '
                            + @ChequeDraftNo       
    
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @TotalamountReceived, @Remarks, @DivisionId,
                            @PaymentTransactionId, 22, @TransactionDate,
                            @CreatedBy      
    
                        EXECUTE Proc_Ledger_Insert @BankId,
                            @TotalamountReceived, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 22, @TransactionDate,
                            @CreatedBy     
    
                    END     
                        
                IF @PM_TYPE = 5
                    BEGIN    
                        SET @Remarks = 'Expense aagainst reference no.: '
                            + @ChequeDraftNo      
    
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @TotalamountReceived, @Remarks, @DivisionId,
                            @PaymentTransactionId, 21, @TransactionDate,
                            @CreatedBy      
    
                        EXECUTE Proc_Ledger_Insert @BankId,
                            @TotalamountReceived, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 21, @TransactionDate,
                            @CreatedBy     
    
                    END     
    
            END      
    
    END 