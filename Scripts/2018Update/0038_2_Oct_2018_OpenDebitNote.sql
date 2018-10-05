
INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0038_2_Oct_2018_OpenDebitNote' ,
          GETDATE()
        )
Go

ALTER TABLE dbo.DebitNote_Master ALTER COLUMN DebitNote_Type varchar(10)
ALTER TABLE dbo.DebitNote_DETAIL ADD TaxableAmt NUMERIC(18,2)
ALTER TABLE dbo.DebitNote_DETAIL ADD TaxAmt NUMERIC(18,2)
ALTER TABLE dbo.DebitNote_DETAIL ADD CessAmt NUMERIC(18,2)
ALTER TABLE dbo.DebitNote_DETAIL ADD DebitAmt NUMERIC(18,2)
ALTER TABLE dbo.DebitNote_Master ADD GSTType NUMERIC(18,0)
ALTER TABLE dbo.DebitNote_Master ADD RoundOff NUMERIC(18,2)

go

CREATE FUNCTION [dbo].[Get_GST_Type]  
    (  
      @AccountId NUMERIC(18, 0)  
    )  
RETURNS NUMERIC(18, 0)  
AS  
    BEGIN  
      
        DECLARE @GstType NUMERIC(18, 0)  
        DECLARE @DivStateId AS NUMERIC  
        DECLARE @IsInUt BIT  
        DECLARE @AccountStateId AS NUMERIC  
        SET @DivStateId = ( SELECT  STATE_ID  
                            FROM    dbo.CITY_MASTER  
                            WHERE   CITY_ID IN ( SELECT CITY_ID  
                                                 FROM   dbo.DIVISION_SETTINGS )  
                          )  
                    
        SET @AccountStateId = ( SELECT  CITY_ID  
                                FROM    dbo.CITY_MASTER  
                                WHERE   CITY_ID IN (  
                                        SELECT  CITY_ID  
                                        FROM    dbo.ACCOUNT_MASTER  
                                        WHERE   ACC_ID = @AccountId )  
                              )  
  
        SET @IsInUt = ( SELECT  IsUT_Bit  
                        FROM    dbo.STATE_MASTER  
                        WHERE   STATE_ID = @DivStateId  
                      )  
   
             
        SELECT  @GstType = ( SELECT CASE WHEN @DivStateId <> @AccountStateId  
                                         THEN 2  
                                         ELSE CASE WHEN @IsInUt = 1 THEN 3  
                                                   ELSE 1  
                                              END  
                                    END AS GstType  
                           )  
        RETURN @GstType  
      
    END  
    
    go
    
        
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
    
    CREATE FUNCTION [dbo].[Get_CostRate]
    (
      @rate NUMERIC(18, 2) ,
      @ItemID NUMERIC(18, 0),
      @ReceivedID NUMERIC(18, 0),
      @ID NUMERIC(18, 0)
    )
RETURNS NUMERIC(18, 2)
AS
    BEGIN  
      
        DECLARE @costrate NUMERIC(18, 2)  
       
        IF @ID = 1
            BEGIN             
                SELECT  @costrate = @rate
                        - ( SELECT  CAST(( ISNULL(CASE WHEN DType = 'P'
                                                       THEN CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( @rate )
                                                              - ( ROUND(( @rate
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(@rate,
                                                              2)
                                                              - ROUND(( @rate
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ( @rate )
                                                              - ( ROUND(( @rate
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(@rate,
                                                              2)
                                                              - ROUND(( @rate
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(( @rate
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(@rate,
                                                              2)
                                                              - ROUND(( @rate
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                              ELSE ROUND(( @rate
                                                              * DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(@rate,
                                                              2)
                                                              - ROUND(( @rate
                                                              * DiscountValue )
                                                              / 100, 2) )
                                                              * DiscountValue1
                                                              / 100 )
                                                            END
                                                       ELSE CASE
                                                              WHEN GSTPAID = 'Y'
                                                              THEN ( ( ( @rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) )
                                                              - ( ( ( @rate )
                                                              - ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2) ) / ( 1
                                                              + ( Item_vat
                                                              / 100 ) ) ) )
                                                              + ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                              ELSE ROUND(DiscountValue,
                                                              2)
                                                              + ROUND(DiscountValue1,
                                                              2)
                                                            END
                                                  END, 0) ) AS NUMERIC(18, 2))
                            FROM    MATERIAL_RECEIVED_WITHOUT_PO_DETAIL WHERE Received_ID=@ReceivedID AND Item_ID=@ItemID
                          )  
            END   
                         
        ELSE
            BEGIN              
                SELECT  @costrate = @rate
                        - ( SELECT  ( ISNULL(CASE WHEN DType = 'P'
                                                  THEN ( ( @rate )
                                                         * DiscountValue )
                                                       / 100
                                                  ELSE DiscountValue
                                             END, 0) )
                            FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL WHERE Receipt_ID=@ReceivedID AND Item_ID=@ItemID
                          )
            END 
                           
        RETURN @costrate  
      
    END  
    
   GO
    
    ALTER PROCEDURE [dbo].[Get_MRN_Details_DebitNote] @V_MRN_NO NUMERIC(18, 0)  
AS  
    BEGIN         
        SELECT  IM.ITEM_ID AS Item_ID ,  
                IM.ITEM_CODE AS Item_Code ,  
                IM.ITEM_NAME AS Item_Name ,  
                IM.UM_Name AS UM_Name ,  
                CAST(SD.Balance_Qty AS NUMERIC(18,2)) AS Prev_Item_Qty ,  
               CAST(md.Item_Qty AS NUMERIC(18,2)) AS MRN_Qty ,  
               CAST(dbo.Get_CostRate(md.Item_Rate,IM.ITEM_ID,MM.Receipt_ID,2)  AS NUMERIC(18,2)) AS Item_Rate,  
               CAST( md.Vat_Per AS NUMERIC(18,2))AS Vat_Per ,  
               CAST( md.Cess_Per AS NUMERIC(18,2)) AS Cess_Per,  
                '0.00' AS Item_Qty , 
                0.00 AS TaxableAmt,    
                0.00 AS Amount,   
                 0.00 AS GST,  
                  0.00 AS Cess,               
                MD.Stock_Detail_Id AS Stock_Detail_Id,
                dbo.Get_CostRate(md.Item_Rate,IM.ITEM_ID,MM.Receipt_ID,2) AS Prv_Rate,
                md.Vat_Per as Prv_Vat_Per,
                md.Cess_Per as Prv_Cess_Per 
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
                CAST(SD.Balance_Qty AS NUMERIC(18,2)) AS Prev_Item_Qty ,  
               CAST(md.Item_Qty AS NUMERIC(18,2)) AS MRN_Qty ,  
               CAST( dbo.Get_CostRate(md.Item_Rate,IM.ITEM_ID,MM.Received_ID,1) AS NUMERIC(18,2)) AS Item_Rate,  
               CAST( md.Item_vat AS NUMERIC(18,2))AS Item_vat ,  
               CAST( md.Item_Cess AS NUMERIC(18,2)) AS Item_Cess,             
                '0.00' AS Item_Qty , 
                0.00 AS TaxableAmt,  
                 0.00 AS Amount,
                  0.00 AS GST,  
                  0.00 AS Cess, 
                MD.Stock_Detail_Id AS Stock_Detail_Id,
                dbo.Get_CostRate(md.Item_Rate,IM.ITEM_ID,MM.Received_ID,1) AS Prv_Rate,
                md.Item_vat as Prv_Vat_Per,
                md.Item_Cess as Prv_Cess_Per
        FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER MM  
                INNER JOIN dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MD ON mm.Received_ID = md.Received_ID  
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON MD.Item_ID = IM.ITEM_ID  
                INNER JOIN STOCK_DETAIL SD ON MD.Stock_Detail_Id = SD.STOCK_DETAIL_ID  
        WHERE   MM.MRN_NO = @V_MRN_NO      
    
    END 
    
    GO
    
    ALTER PROCEDURE Proc_GETDebitNoteDetailsByID_Edit     
    (    
      @DebitNoteId NUMERIC(18,0)    
    )    
AS    
    BEGIN    
        SELECT  DN.DebitNote_Code + CAST(DN.DebitNote_No AS VARCHAR(20)) AS DebitNoteNumber ,    
                dbo.fn_Format(DN.DebitNote_Date) AS DebitNote_Date ,    
                DN_CustId ,    
                MRWPM.MRN_NO AS MRNo,    
                MRWPM.MRN_PREFIX + CAST(MRWPM.MRN_NO AS VARCHAR(20)) AS MRNNumber,    
                INV_No AS InvoiceNo,    
                dbo.fn_Format(DN.INV_Date) AS InvoiceDate,    
                DN.Remarks AS Remarks ,  
                Purchase_Type AS MRN_TYPE,
               ISNULL(DN.RoundOff,0) AS RoundOff
        FROM    dbo.DebitNote_Master DN    
                INNER JOIN dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER AS MRWPM ON MRWPM.MRN_NO = DN.MRNId    
        WHERE   DebitNote_Id = @DebitNoteId    
            
        UNION ALL    
            
        SELECT  DN.DebitNote_Code + CAST(DN.DebitNote_No AS VARCHAR(20)) AS DebitNoteNumber ,    
                dbo.fn_Format(DN.DebitNote_Date) AS DebitNote_Date ,    
                DN_CustId ,    
                MRAPM.MRN_NO AS MRNo,    
                MRAPM.MRN_PREFIX + CAST(MRAPM.MRN_NO AS VARCHAR(20)) AS MRNNumber,    
                INV_No AS InvoiceNo,    
                dbo.fn_Format(DN.INV_Date) AS InvoiceDate,    
                DN.Remarks AS Remarks,  
                MRN_TYPE,
                ISNULL(DN.RoundOff,0) AS RoundOff
        FROM    dbo.DebitNote_Master DN    
                INNER JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER AS MRAPM ON MRAPM.MRN_NO = DN.MRNId    
        WHERE   DebitNote_Id = @DebitNoteId    
        
        UNION ALL            
        SELECT  DN.DebitNote_Code + CAST(DN.DebitNote_No AS VARCHAR(20)) AS DebitNoteNumber ,    
                dbo.fn_Format(DN.DebitNote_Date) AS DebitNote_Date ,    
                DN_CustId ,    
                0 AS MRNo,    
                '' AS MRNNumber,    
                INV_No AS InvoiceNo,    
                dbo.fn_Format(DN.INV_Date) AS InvoiceDate,    
                DN.Remarks AS Remarks,  
                DN.GSTType AS MRN_TYPE,
                ISNULL(DN.RoundOff,0) AS RoundOff
        FROM    dbo.DebitNote_Master DN                  
        WHERE   DebitNote_Id =@DebitNoteId    
            AND DN.MRNId<=0
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
                dd.Item_Qty AS Item_Qty ,
                dd.TaxableAmt AS TaxableAmt ,
                dd.TaxAmt AS GST ,
                dd.CessAmt AS Cess ,
                dd.TaxableAmt AS Amount ,
                dd.Item_Rate as Prv_Rate,
                dd.Item_Tax as Prv_Vat_Per,
                dd.Item_Cess as Prv_Cess_Per,
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
                dd.Item_Qty AS Item_Qty ,
                dd.TaxableAmt AS TaxableAmt ,
                dd.TaxAmt AS GST ,
                dd.CessAmt AS Cess ,
                dd.TaxableAmt AS Amount ,
                dd.Item_Rate as Prv_Rate,
                dd.Item_Tax as Prv_Vat_Per,
                dd.Item_Cess as Prv_Cess_Per,
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
                dd.Item_Qty AS MRN_Qty ,
                dd.Item_Rate ,
                dd.Item_Tax AS Vat_Per ,
                dd.Item_Cess AS Cess_Per ,
                dd.Item_Qty AS Item_Qty ,
                dd.TaxableAmt AS TaxableAmt ,
                dd.TaxAmt AS GST ,
                dd.CessAmt AS Cess ,
                dd.TaxableAmt AS Amount ,
                0.00 as Prv_Rate,
                dd.Item_Tax as Prv_Vat_Per,
                dd.Item_Cess as Prv_Cess_Per,
                sd.Stock_Detail_Id AS Stock_Detail_Id
        FROM    dbo.DebitNote_Master DN
                INNER JOIN DebitNote_DETAIL DD ON DD.DebitNote_Id = dn.DebitNote_Id                
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON DD.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON DD.Stock_Detail_Id = SD.STOCK_DETAIL_ID
                                              AND dd.Item_ID = sd.Item_id
        WHERE   dn.DebitNote_Id = @DebitNoteId  AND DN.MRNId<=0
    END 
    
    GO
