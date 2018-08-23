INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0037_23_August_2018_DebitandCreditNoteNullErrorCorrection' ,
          GETDATE()
        )
Go

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
		       
    END 

Go

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
                Purchase_Type AS MRN_TYPE
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
                MRN_TYPE
        FROM    dbo.DebitNote_Master DN
                INNER JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER AS MRAPM ON MRAPM.MRN_NO = DN.MRNId
        WHERE   DebitNote_Id = @DebitNoteId  
          
    END

Go