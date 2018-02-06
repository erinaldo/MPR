
insert INTO dbo.DBScriptUpdateLog
        ( LogFileName, ExecuteDateTime )
VALUES  ( '0002_27_Jan_2018_MRN_WITHOUTPO_EDIT',
          GETDATE()
         )

-----------------------------------------------------------------------------------------------------------------------------
ALTER PROC ProcReverseInvoiceEntry
    (
      @V_SI_ID NUMERIC(18, 0) ,
      @V_CUST_ID NUMERIC(18, 0)
    )
AS
    BEGIN  
  
  
        SET @V_CUST_ID = ( SELECT   CUST_ID
                           FROM     dbo.SALE_INVOICE_MASTER
                           WHERE    SI_ID = @V_SI_ID
                         )
        UPDATE  STOCK_DETAIL
        SET     STOCK_DETAIL.Issue_Qty = ( STOCK_DETAIL.Issue_Qty
                                           - SALE_INVOICE_STOCK_DETAIL.ITEM_QTY ) ,
                STOCK_DETAIL.Balance_Qty = ( STOCK_DETAIL.Balance_Qty
                                             + SALE_INVOICE_STOCK_DETAIL.ITEM_QTY )
        FROM    dbo.STOCK_DETAIL
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL ON SALE_INVOICE_STOCK_DETAIL.STOCK_DETAIL_ID = STOCK_DETAIL.STOCK_DETAIL_ID
                                                      AND SALE_INVOICE_STOCK_DETAIL.ITEM_ID = STOCK_DETAIL.Item_id
        WHERE   SI_ID = @V_SI_ID  
  
  
        DELETE  FROM dbo.SALE_INVOICE_DETAIL
        WHERE   SI_ID = @V_SI_ID  
  
  
        DELETE  FROM dbo.SALE_INVOICE_STOCK_DETAIL
        WHERE   SI_ID = @V_SI_ID  
  
  
  
        DECLARE @CashOut NUMERIC(18, 2)  
        DECLARE @CashIn NUMERIC(18, 2)  
        SET @CashOut = 0  
  
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = @V_CUST_ID
                      )  
  
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @V_CUST_ID  
  
  
  
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = 10073
                      )  
  
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10073  
  
  
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = 10071
                       )  
  
        SET @CashIn = 0  
  
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10071  
  
  
  
  
        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = 10054
                       )  
  
  
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10054  
      
      
  
  
        SET @CashOut = 0  
  
        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                                JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                                AND AccountId = 10054
                      )  
  
  
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10054  
  
  
  
  
        DECLARE @v_INV_TYPE VARCHAR(1)  
        SET @v_INV_TYPE = ( SELECT  INV_TYPE
                            FROM    dbo.SALE_INVOICE_MASTER
                            WHERE   SI_ID = @v_SI_ID
                          )  
  
        DECLARE @CInputID NUMERIC  
        SET @CInputID = 10017  
        DECLARE @InputID NUMERIC  
        DECLARE @CGST_Amount NUMERIC(18, 2)  
    
   
  
  
        SET @InputID = ( SELECT CASE WHEN @v_INV_TYPE = 'I' THEN 10021
                                     WHEN @v_INV_TYPE = 'S' THEN 10024
                                     WHEN @v_INV_TYPE = 'U' THEN 10075
                                END AS inputid
                       )  
  
        IF @v_INV_TYPE <> 'I'
            BEGIN  
  
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @v_SI_ID
                                        AND TransactionTypeId = 16
                                        AND AccountId = @CInputID
                               )  
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @CInputID  
  
  
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @v_SI_ID
                                        AND TransactionTypeId = 16
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
                                 WHERE  TransactionId = @v_SI_ID
                                        AND TransactionTypeId = 16
                                        AND AccountId = @InputID
                               )  
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @InputID  
            END   
  
  
  
        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @v_SI_ID
                AND TransactionTypeId = 16  
    END


---------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[Get_MRN_WithOutPO_Detail]
    @V_Receive_ID NUMERIC(18, 0)
AS
    BEGIN        
        
        SELECT  Received_ID ,
                Received_Code ,
                Received_No ,
               CONVERT(VARCHAR(20),Received_Date,106) Received_Date ,
                Purchase_Type ,
                Vendor_ID ,
                Remarks ,
                Division_ID ,
                mrn_status ,
                freight ,
                other_charges ,
                Discount_amt ,
                MRNCompanies_ID,
                Invoice_No,
                Invoice_Date
        FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER
        WHERE   Received_ID = @V_Receive_ID  
        
        
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                MD.Item_Qty AS BATCH_QTY ,
                MD.Item_Rate AS Item_Rate ,
                ISNULL(DType, 'P') AS DType ,
                ISNULL(DiscountValue, 0) AS DISC ,
                MD.Item_vat AS Vat_Per ,
                MD.Item_exice AS exe_Per ,
                MD.Batch_No AS BATCH_NO ,
                MD.Expiry_Date AS EXPIRY_DATE
        FROM    MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MD
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON MD.Item_ID = IM.ITEM_ID
        WHERE   MD.Received_ID = @V_Receive_ID        
        
        
        
        SELECT  dbo.COST_CENTER_MASTER.CostCenter_Id ,
                dbo.COST_CENTER_MASTER.CostCenter_Code ,
                IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                COST_CENTER_MASTER.CostCenter_Name ,
                NonStockable.Item_Qty AS BATCH_QTY ,
                NonStockable.Item_Rate AS Item_Rate ,
                NonStockable.Item_vat AS Vat_Per ,
                ISNULL(DType, 'P') AS DType ,
                ISNULL(DiscountValue, 0) AS DISC ,
                NonStockable.Item_Exice AS exe_Per ,
                NonStockable.batch_no AS BATCH_NO ,
                NonStockable.batch_date AS EXPIRY_DATE
        FROM    dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO NonStockable
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON NonStockable.Item_ID = IM.ITEM_ID
                INNER JOIN dbo.COST_CENTER_MASTER ON NonStockable.CostCenter_ID = dbo.COST_CENTER_MASTER.CostCenter_Id
        WHERE   Received_ID = @V_Receive_ID        
        
    END  
-------------------------------------------------------------------------------------------------------------------------
