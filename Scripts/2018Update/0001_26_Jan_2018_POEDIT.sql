CREATE TABLE [dbo].[DBScriptUpdateLog](
	[LogFileName] [varchar](50) NOT NULL,
	[ExecuteDateTime] [datetime] NOT NULL
) ON [PRIMARY]

GO

--------------------------------------------------------------------------------------------------------------------------

insert INTO dbo.DBScriptUpdateLog
        ( LogFileName, ExecuteDateTime )
VALUES  ( '0001_26_Jan_2018_POEDIT',
          GETDATE()
          )

-----------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[GET_PO_ITEM_DETAILS] ( @V_PO_ID NUMERIC(18, 0) )
AS
    BEGIN            
        SELECT  ITEM_MASTER.ITEM_CODE ,
                ITEM_MASTER.ITEM_NAME ,
                UNIT_MASTER.UM_Name ,
                PO_STATUS.REQUIRED_QTY AS Req_Qty ,
                CAST(PO_STATUS.REQUIRED_QTY AS NUMERIC(18, 4)) AS PO_Qty ,
                PO_DETAIL.ITEM_RATE AS Item_Rate ,
                ISNULL(DType, 'P') AS DType ,
                ISNULL(DiscountValue, 0) AS DISC ,
                ITEM_DETAIL.PURCHASE_VAT_ID AS Vat_Id ,
                VAT_MASTER.VAT_NAME ,
                PO_DETAIL.EXICE_PER AS Exice_Per ,
                VAT_MASTER.VAT_PERCENTAGE AS Vat_Per ,
                0 AS Item_Value ,
                PO_STATUS.ITEM_ID AS Item_ID ,
                ITEM_MASTER.UM_ID ,
                PO_STATUS.INDENT_ID
        FROM    PO_DETAIL
                INNER JOIN ITEM_MASTER ON PO_DETAIL.ITEM_ID = ITEM_MASTER.ITEM_ID
                INNER JOIN UNIT_MASTER ON ITEM_MASTER.UM_ID = UNIT_MASTER.UM_ID
                INNER JOIN ITEM_DETAIL ON ITEM_MASTER.ITEM_ID = ITEM_DETAIL.ITEM_ID
                INNER JOIN VAT_MASTER ON ITEM_DETAIL.PURCHASE_VAT_ID = VAT_MASTER.VAT_ID
                INNER JOIN PO_STATUS ON PO_DETAIL.PO_ID = PO_STATUS.PO_ID
                                        AND PO_DETAIL.ITEM_ID = PO_STATUS.ITEM_ID
        WHERE   ( PO_STATUS.PO_ID = @V_PO_ID )    
    END  

-------------------------------------------------------------------------------------------------------------------------