INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0039_4_Oct_2018_OpenCreditNote' ,
          GETDATE()
        )
Go

ALTER TABLE dbo.CreditNote_Master ALTER COLUMN CreditNote_Type varchar(10)
ALTER TABLE dbo.CreditNote_DETAIL ADD TaxableAmt NUMERIC(18,2)
ALTER TABLE dbo.CreditNote_DETAIL ADD TaxAmt NUMERIC(18,2)
ALTER TABLE dbo.CreditNote_DETAIL ADD CessAmt NUMERIC(18,2)
ALTER TABLE dbo.CreditNote_DETAIL ADD CreditAmt NUMERIC(18,2)
ALTER TABLE dbo.CreditNote_Master ADD GSTType NUMERIC(18,0)
ALTER TABLE dbo.CreditNote_Master ADD RoundOff NUMERIC(18,2)

go

ALTER PROCEDURE [dbo].[Get_INV_Details_CreditNote] @Si_ID NUMERIC(18, 0)
AS
    BEGIN           
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                CAST(SD.Balance_Qty AS NUMERIC(18, 2)) AS Prev_Item_Qty ,
                CAST(saleID.Item_Qty AS NUMERIC(18, 2)) AS INV_Qty ,
                CAST(saleID.Item_Rate AS NUMERIC(18, 2)) AS Item_Rate ,
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
                          Cess_Amt,                         
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
                          @v_CN_ItemCess,
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
                        Cess_Amt = @v_CN_ItemCess,
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
      @v_Proc_Type INT,
      @v_TaxableAmt NUMERIC(18, 2) ,
      @v_TaxAmt NUMERIC(18, 2) ,
      @v_CessAmt NUMERIC(18, 2) ,
      @v_CreditAmt NUMERIC(18, 2)
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
                          Item_Cess,
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
                          @v_Item_Tax,    
                          @v_Item_Cess,
                          @v_TaxableAmt ,
                          @v_TaxAmt ,
                          @v_CessAmt ,
                          @v_CreditAmt
                        )     
                                 
                --DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)      
                        
                UPDATE  dbo.STOCK_DETAIL    
                SET     ISSUE_QTY = ISSUE_QTY - @v_Item_Qty ,    
                        Balance_Qty = BALANCE_QTY + @v_Item_Qty    
                WHERE   STOCK_DETAIL_ID = @v_Stock_Detail_Id          
                    
        
                EXEC INSERT_TRANSACTION_LOG @v_CreditNote_ID, @v_Item_ID,    
                    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,    
                    @v_Creation_Date, 0           
            END    
              
        IF @V_PROC_TYPE = 2  
            BEGIN  
              
    DELETE  FROM dbo.CreditNote_DETAIL  
                WHERE   CreditNote_Id = @V_CreditNote_ID  AND  Item_ID = @V_Item_ID  
                  
                  
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
                          Item_Cess,
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
                          @v_Item_Tax,    
                          @v_Item_Cess,
                          @v_TaxableAmt ,
                          @v_TaxAmt ,
                          @v_CessAmt ,
                          @v_CreditAmt
                        )     
                                 
                --DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)      
                        
        UPDATE  dbo.STOCK_DETAIL    
                SET     ISSUE_QTY = ISSUE_QTY - @v_Item_Qty ,    
                        Balance_Qty = BALANCE_QTY + @v_Item_Qty    
                WHERE   STOCK_DETAIL_ID = @v_Stock_Detail_Id     
                  
                  
                DELETE  FROM dbo.Transaction_Log  
                WHERE   Transaction_Type = @V_Trans_Type  
                        AND Transaction_ID = @V_CreditNote_ID       
                    
        
                EXEC INSERT_TRANSACTION_LOG @v_CreditNote_ID, @v_Item_ID,    
                    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,    
                    @v_Creation_Date, 0   
            END        
    END      
    
    Go
    
        
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
                cd.Item_Qty AS Item_Qty ,
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
                INV_TYPE
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
                SD.Balance_Qty AS Prev_Item_Qty ,
                CD.Item_Qty AS INV_Qty ,
                cd.Item_Rate ,
                cd.Item_Tax AS Vat_Per ,
                cd.Item_Cess AS Cess_Per ,
                cd.Item_Qty AS Item_Qty ,
                cd.TaxableAmt AS TaxableAmt ,
                cd.TaxAmt AS GST ,
                cd.CessAmt AS Cess ,
                cd.TaxableAmt AS Amount ,
                0.00 AS Prv_Rate ,
                cd.Item_Tax AS Prv_Vat_Per ,
                cd.Item_Cess AS Prv_Cess_Per ,
                SIID.Stock_Detail_Id AS Stock_Detail_Id ,
                CONVERT(VARCHAR(20), CN.INV_Date, 106) AS INvDate ,
                CN.INV_No AS SiNo ,
                GSTType AS INV_TYPE
        FROM    dbo.CreditNote_Master CN
                INNER JOIN dbo.CreditNote_DETAIL CD ON CD.CreditNote_Id = CN.CreditNote_Id
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL SIID ON SIID.ITEM_ID = CD.ITEM_ID
                                                           AND SIID.SI_ID = CN.INVId
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON CD.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON SIID.Stock_Detail_Id = SD.STOCK_DETAIL_ID
        WHERE   cn.CreditNote_Id = @CreditNoteId
                AND CN.INVId <= 0
    END 
    
    GO
    
     