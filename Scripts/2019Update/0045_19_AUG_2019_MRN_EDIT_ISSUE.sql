
INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0045_19_Aug_2019_MRN_EDIT_ISSUE' ,
          GETDATE()
        )

GO

ALTER VIEW [dbo].[vw_ItemMaster_Detail_Unit]    
AS    
SELECT  DISTINCT   dbo.ITEM_MASTER.ITEM_ID, dbo.ITEM_MASTER.BarCode_vch as ITEM_CODE, dbo.ITEM_MASTER.ITEM_NAME, dbo.ITEM_MASTER.ITEM_DESC,     
                      dbo.ITEM_MASTER.UM_ID, dbo.ITEM_MASTER.ITEM_CATEGORY_ID, dbo.UNIT_MASTER.UM_Name, dbo.UNIT_MASTER.UM_DESC,     
                      dbo.ITEM_DETAIL.DIV_ID, dbo.ITEM_DETAIL.RE_ORDER_LEVEL, dbo.ITEM_DETAIL.RE_ORDER_QTY, dbo.ITEM_DETAIL.PURCHASE_VAT_ID,     
                      dbo.ITEM_DETAIL.SALE_VAT_ID, dbo.ITEM_DETAIL.OPENING_STOCK, dbo.ITEM_DETAIL.CURRENT_STOCK, dbo.ITEM_DETAIL.IS_EXTERNAL,     
                      dbo.ITEM_DETAIL.TRANSFER_RATE, dbo.ITEM_DETAIL.AVERAGE_RATE, dbo.ITEM_DETAIL.IS_STOCKABLE    
FROM         dbo.ITEM_MASTER INNER JOIN    
                      dbo.ITEM_DETAIL ON dbo.ITEM_MASTER.ITEM_ID = dbo.ITEM_DETAIL.ITEM_ID INNER JOIN    
                      dbo.UNIT_MASTER ON dbo.ITEM_MASTER.UM_ID = dbo.UNIT_MASTER.UM_ID  
                      
                      
GO

ALTER PROCEDURE [dbo].[Get_MRN_WithOutPO_Detail]    
    @V_Receive_ID NUMERIC(18, 0)    
AS    
    BEGIN                                
                                
        SELECT DISTINCT Received_ID ,    
                Received_Code ,    
                Received_No ,    
                CONVERT(VARCHAR(20), Received_Date, 106) Received_Date ,    
                Purchase_Type ,    
                Vendor_ID ,    
                Remarks ,    
                Division_ID ,    
                mrn_status ,    
                ISNULL(freight, 0.00) AS freight ,    
                other_charges ,    
                Discount_amt ,    
                ISNULL(CashDiscount_Amt, 0) AS CashDiscount_Amt ,    
                MRNCompanies_ID ,    
                Invoice_No ,    
                Invoice_Date ,    
                SpecialSchemeFlag ,    
                FreightTaxApplied ,    
                FreightTaxValue ,    
                FK_ITCEligibility_ID ,    
                REFERENCE_ID ,    
                ISNULL(IS_RCM_Applicable, 0) AS IS_RCM_Applicable    
        FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER    
        WHERE   Received_ID = @V_Receive_ID             
                                
        SELECT  DISTINCT IM.ITEM_ID AS Item_ID ,    
                IM.ITEM_CODE AS Item_Code ,    
                IM.ITEM_NAME AS Item_Name ,    
                IM.UM_Name AS UM_Name ,    
                CAST(MD.Item_Qty AS NUMERIC(18, 3)) AS BATCH_QTY ,    
                CAST(MD.Item_Rate AS NUMERIC(18, 2)) AS Item_Rate ,    
                ISNULL(DType, 'P') AS DType ,    
                CAST(ISNULL(DiscountValue, 0) AS NUMERIC(18, 2)) AS DISC ,    
                CAST(ISNULL(DiscountValue1, 0) AS NUMERIC(18, 2)) AS DISC1 ,    
                ISNULL(GSTPAID, 'N') AS GPAID ,    
                CAST(MD.Item_vat AS NUMERIC(18, 2)) AS Vat_Per ,    
                CAST(ISNULL(MD.Item_cess, 0) AS NUMERIC(18, 2)) AS Cess_Per ,    
                CAST(ISNULL(MD.Acess, 0) AS NUMERIC(18, 2)) AS Acess ,    
                0.0 AS Amount ,    
                MD.Item_exice AS exe_Per ,    
                MD.Batch_No AS BATCH_NO ,    
                MD.Expiry_Date AS EXPIRY_DATE ,    
                ISNULL(Freight, 0.00) AS Freight ,    
                ISNULL(Freight_type, 'A') AS Freight_type ,    
                ISNULL(FreightTaxValue, 0.00) AS FreightTaxValue ,    
                ISNULL(FreightCessValue, 0.00) AS FreightCessValue    
        FROM    MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MD    
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON MD.Item_ID = IM.ITEM_ID    
        WHERE   MD.Received_ID = @V_Receive_ID                             
                                
        SELECT DISTINCT dbo.COST_CENTER_MASTER.CostCenter_Id ,    
                dbo.COST_CENTER_MASTER.CostCenter_Code ,    
                IM.ITEM_ID AS Item_ID ,    
                IM.ITEM_CODE AS Item_Code ,    
                IM.ITEM_NAME AS Item_Name ,    
                IM.UM_Name AS UM_Name ,    
                COST_CENTER_MASTER.CostCenter_Name ,    
                NonStockable.Item_Qty AS BATCH_QTY ,    
                NonStockable.Item_Rate AS Item_Rate ,    
                NonStockable.Item_vat AS Vat_Per ,    
                NonStockable.Item_cess AS Cess_Per ,    
                CAST(ISNULL(NonStockable.Acess, 0) AS NUMERIC(18, 2)) AS Acess ,    
                0.0 AS Amount ,    
                ISNULL(DType, 'P') AS DType ,    
                CAST(ISNULL(DiscountValue, 0) AS NUMERIC(18, 2)) AS DISC ,    
                CAST(ISNULL(DiscountValue1, 0) AS NUMERIC(18, 2)) AS DISC1 ,    
                ISNULL(GSTPAID, 'N') AS GPAID ,    
                NonStockable.Item_Exice AS exe_Per ,    
                NonStockable.batch_no AS BATCH_NO ,    
                NonStockable.batch_date AS EXPIRY_DATE    
        FROM    dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO NonStockable    
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON NonStockable.Item_ID = IM.ITEM_ID    
                INNER JOIN dbo.COST_CENTER_MASTER ON NonStockable.CostCenter_ID = dbo.COST_CENTER_MASTER.CostCenter_Id    
        WHERE   Received_ID = @V_Receive_ID          
                                
    END 