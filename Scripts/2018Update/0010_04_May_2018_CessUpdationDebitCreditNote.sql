
insert INTO dbo.DBScriptUpdateLog
        ( LogFileName, ExecuteDateTime )
VALUES  ( '0010_04_May_2018_CessUpdationDebitCreditNote',
          GETDATE()
          )

  Go


ALTER TABLE dbo.CreditNote_Master ADD Cess_Amt NUMERIC(18,2) DEFAULT 0.00
ALTER TABLE dbo.CreditNote_DETAIL ADD Item_Cess NUMERIC(18,2) DEFAULT 0.00
ALTER TABLE dbo.DebitNote_MASTER ADD Cess_num NUMERIC(18,2) DEFAULT 0.00
ALTER TABLE dbo.DebitNote_DETAIL ADD Item_Cess NUMERIC(18,2) DEFAULT 0.00


Go


Alter PROCEDURE [dbo].[Get_INV_Details_CreditNote] @Si_ID NUMERIC(18, 0)
AS
    BEGIN       
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                SD.Balance_Qty AS Prev_Item_Qty ,
                saleID.Item_Qty AS INV_Qty ,
                saleID.Item_Rate ,
                saleID.Vat_Per ,
                saleID.CessPercentage_num AS Cess_Per ,
                '0.00' AS Item_Qty ,
                SIID.Stock_Detail_Id AS Stock_Detail_Id ,
                CONVERT(VARCHAR(20), SIM.CREATION_DATE, 106) AS INvDate ,
                SI_CODE + CAST(SI_NO AS VARCHAR(20)) AS SiNo
        FROM    dbo.SALE_INVOICE_MASTER SIM
                INNER JOIN SALE_INVOICE_DETAIL SaleID ON sim.SI_ID = SaleID.SI_ID
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL SIID ON SIID.ITEM_ID = SaleID.ITEM_ID
                                                           AND SIID.SI_ID = SaleID.SI_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON SaleID.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON SIID.Stock_Detail_Id = SD.STOCK_DETAIL_ID
        WHERE   SIM.SI_ID = @Si_ID   
    END  
Go


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
      @v_CN_ItemCess NUMERIC(18, 2) = 0 ,
      @v_CN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
      @v_CreditNote_Type VARCHAR(50) = NULL ,
      @v_RefNo VARCHAR(50) = NULL ,
      @v_RefDate_dt DATETIME ,
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
                          Tax_Amt ,
                          Cess_Amt      
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
                          @v_CN_ItemCess      
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
                    @V_Division_ID, @V_CreditNote_ID, 17, @v_CreditNote_Date,
                    @V_Created_BY     
      
      
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
                    
                    
                SET @Remarks = 'CESS against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))  
                 
                EXECUTE Proc_Ledger_Insert 10014, 0, @v_CN_ItemCess,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID, 17,
                            @v_CreditNote_Date, @V_Created_BY   
                    
                    
                SET @Remarks = 'Stock Out against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))      
      
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_CN_Amount, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, 17, @v_CreditNote_Date,
                    @V_Created_BY 
                    
                    
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
                        INV_Date = @v_INV_Date ,
                        CreditNote_Type = @v_CreditNote_Type ,
                        RefNo = @v_RefNo ,
                        RefDate_dt = @v_RefDate_dt ,
                        Tax_Amt = @v_CN_ItemTax ,
                        Cess_Amt = @v_CN_ItemCess
                WHERE   CreditNote_ID = @V_CreditNote_ID      
            END      
    END 

Go


Alter PROCEDURE [dbo].[PROC_CreditNote_DETAIL]
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
      @v_Proc_Type INT      
    )
AS
    BEGIN      
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
                          Item_Tax,
                          Item_Cess  
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
                          @v_Item_Tax,
                          @v_Item_Cess  
                        ) 
                             
                --DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)  
                    
                UPDATE  dbo.STOCK_DETAIL
                SET     Item_Qty = Item_Qty + @v_Item_Qty ,
                        Balance_Qty = BALANCE_QTY + @v_Item_Qty
                WHERE   STOCK_DETAIL_ID = @v_Stock_Detail_Id      
                
    
                EXEC INSERT_TRANSACTION_LOG @v_CreditNote_ID, @v_Item_ID,
                    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,
                    @v_Creation_Date, 0       
            END      
    END  

Go

Alter PROCEDURE [dbo].[Get_MRN_Details_DebitNote] @V_MRN_NO NUMERIC(18, 0)
AS
    BEGIN       
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                SD.Balance_Qty AS Prev_Item_Qty ,
                md.Item_Qty AS MRN_Qty ,
                md.Item_Rate ,
                md.Vat_Per ,
                md.Cess_Per ,
                '0.00' AS Item_Qty ,
                MD.Stock_Detail_Id AS Stock_Detail_Id
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
                SD.Balance_Qty AS Prev_Item_Qty ,
                md.Item_Qty AS MRN_Qty ,
                md.Item_Rate ,
                md.Item_vat ,
                md.Item_Cess ,
                '0.00' AS Item_Qty ,
                MD.Stock_Detail_Id AS Stock_Detail_Id
        FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER MM
                INNER JOIN dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MD ON mm.Received_ID = md.Received_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON MD.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON MD.Stock_Detail_Id = SD.STOCK_DETAIL_ID
        WHERE   MM.MRN_NO = @V_MRN_NO    
  
    END  
Go

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
      @v_DN_ItemCess NUMERIC(18, 2) = 0 ,
      @v_DN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
      @v_DebitNote_Type VARCHAR(50) = NULL ,
      @v_RefNo VARCHAR(50) = NULL ,
      @v_RefDate_dt DATETIME ,
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
                          Tax_num,
                          Cess_num      
      
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
                          @v_DN_ItemTax,
                          @v_DN_ItemCess 
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
                    @V_Division_ID, @V_DebitNote_ID, 7, @v_DebitNote_Date,
                    @V_Created_BY       
      
      
      
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
				
				SET @Remarks = 'Cess against DebitNote No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))
                    
                 EXECUTE Proc_Ledger_Insert 10013, @v_DN_ItemCess, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID, 7,
                            @v_DebitNote_Date, @V_Created_BY    
      
                SET @Remarks = 'Stock In against Debit Note No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))      
      
      
      
                EXECUTE Proc_Ledger_Insert 10073, @v_DN_Amount, 0, @Remarks,
                    @V_Division_ID, @V_DebitNote_ID, 7, @v_DebitNote_Date,
                    @V_Created_BY        
      
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
                        Tax_num = @v_DN_ItemTax,
                        Cess_num = @v_DN_ItemCess
                WHERE   DebitNote_ID = @V_DebitNote_ID      
      
            END      
    END    
      
Go

Alter PROCEDURE [dbo].[PROC_DebitNote_DETAIL]
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
      @v_Proc_Type INT        
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
                          Item_Cess     
    
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
                          @v_Item_Cess      
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
    END 
Go

----------------------------------------------------------------------------------------------------------------------------------------------------------

