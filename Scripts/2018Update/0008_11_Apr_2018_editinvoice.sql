ALTER PROCEDURE [dbo].[GET_INV_ITEM_DETAILS] ( @V_SI_ID NUMERIC(18, 0) )      
AS      
    BEGIN       
      
        SELECT  IM.ITEM_ID ,      
                IM.ITEM_CODE ,      
                ITEM_NAME ,      
                UM_Name ,      
                Batch_no ,      
                Balance_Qty + SISD.Item_Qty AS Batch_Qty ,      
                SISD.Item_Qty AS transfer_Qty ,      
                Expiry_date ,      
                sd.STOCK_DETAIL_ID ,      
                ITEM_RATE ,      
                ISNULL(DISCOUNT_TYPE, 'P') AS DType ,      
                ISNULL(DISCOUNT_VALUE, 0) AS DISC ,     
                 ISNULL(GSTPAID,'N') AS GPAID,      
                cast(  ISNULL(SISD.Item_Qty ,0)*   
                 ISNULL(ITEM_RATE ,0)AS decimal(18,2))AS Amount ,   
        
                VAT_PER AS GST ,      
                VAT_AMOUNT AS GST_Amount ,      
                fk_HsnId_num AS HsnCodeId ,      
                0 AS LandingAmt       
                   
        FROM    dbo.SALE_INVOICE_DETAIL SID      
                JOIN dbo.ITEM_MASTER IM ON IM.ITEM_ID = SID.ITEM_ID      
                JOIN dbo.UNIT_MASTER UM ON UM.UM_ID = IM.UM_ID      
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL SISD ON SISD.ITEM_ID = SID.Item_id      
                                                           AND SISD.SI_ID = SID.SI_ID      
                JOIN dbo.STOCK_DETAIL SD ON SD.Item_id = SID.ITEM_ID      
                                            AND SD.STOCK_DETAIL_ID = SISD.STOCK_DETAIL_ID      
        WHERE   SID.SI_ID = @V_SI_ID      
      
    END  