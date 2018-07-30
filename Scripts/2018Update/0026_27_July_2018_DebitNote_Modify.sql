   INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0026_27_July_2018_DebitNote_Modify' ,
          GETDATE()
        )
Go

  
ALTER PROCEDURE [dbo].[Get_MRN_Details_DebitNote] @V_MRN_NO NUMERIC(18, 0)
AS
    BEGIN         
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                CAST(SD.Balance_Qty AS NUMERIC(18, 3)) AS Prev_Item_Qty ,
                CAST(md.Item_Qty AS NUMERIC(18, 3)) AS MRN_Qty ,
                CAST(md.Item_Rate AS NUMERIC(18, 2)) AS Item_Rate ,
                CAST(md.Vat_Per AS NUMERIC(18, 2)) AS Vat_Per ,
                CAST(md.Cess_Per AS NUMERIC(18, 2)) AS Cess_Per ,
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
                CAST(SD.Balance_Qty AS NUMERIC(18, 3)) AS Prev_Item_Qty ,
                CAST(md.Item_Qty AS NUMERIC(18, 3)) AS MRN_Qty ,
                CAST(md.Item_Rate AS NUMERIC(18, 2)) AS Item_Rate ,
                CAST(md.Item_vat AS NUMERIC(18, 2)) AS Item_vat ,
                CAST(md.Item_Cess AS NUMERIC(18, 2)) AS Item_Cess ,
                '0.00' AS Item_Qty ,
                MD.Stock_Detail_Id AS Stock_Detail_Id
        FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER MM
                INNER JOIN dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MD ON mm.Received_ID = md.Received_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON MD.Item_ID = IM.ITEM_ID
                INNER JOIN STOCK_DETAIL SD ON MD.Stock_Detail_Id = SD.STOCK_DETAIL_ID
        WHERE   MM.MRN_NO = @V_MRN_NO      
    
    END  
    
    GO
    
      
ALTER PROCEDURE [dbo].[GetDebitNoteDetails]
    @DebitNoteId NUMERIC(18, 0)
AS
    BEGIN           
        SELECT  IM.ITEM_ID AS Item_ID ,
                IM.ITEM_CODE AS Item_Code ,
                IM.ITEM_NAME AS Item_Name ,
                IM.UM_Name AS UM_Name ,
                CAST(SD.Balance_Qty AS NUMERIC(18, 3)) AS Prev_Item_Qty ,
                CAST(md.Item_Qty AS NUMERIC(18, 3)) AS MRN_Qty ,
                CAST(dd.Item_Rate AS NUMERIC(18, 2)) AS Item_Rate ,
                dd.Item_Tax AS Vat_Per ,
                dd.Item_Cess AS Cess_Per ,
                CAST(dd.Item_Qty AS NUMERIC(18, 3)) AS Item_Qty ,
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
                CAST(SD.Balance_Qty AS NUMERIC(18, 3)) AS Prev_Item_Qty ,
                CAST(md.Item_Qty AS NUMERIC(18, 3)) AS MRN_Qty ,
                CAST(dd.Item_Rate AS NUMERIC(18, 2)) AS Item_Rate ,
                dd.Item_Tax AS Vat_Per ,
                dd.Item_Cess AS Cess_Per ,
                CAST(dd.Item_Qty AS NUMERIC(18, 3)) AS Item_Qty ,
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
                DN.Remarks AS Remarks,
                MRWPM.MRN_TYPE  
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
                MRAPM.MRN_TYPE   
        FROM    dbo.DebitNote_Master DN  
                INNER JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER AS MRAPM ON MRAPM.MRN_NO = DN.MRNId  
        WHERE   DebitNote_Id = @DebitNoteId  
          
    END  
  
  GO
  
  ALTER PROCEDURE [dbo].[GET_ITEM_BY_ID] ( @V_ITEM_ID NUMERIC )  
AS  
    BEGIN          
        
        SELECT  IM.ITEM_ID ,  
                IM.BarCode_vch as ITEM_CODE ,  
                IM.ITEM_NAME ,  
                UM.UM_NAME ,  
                vm.VAT_PERCENTAGE ,  
                ISNULL(cm.CessPercentage_num, 0.00) AS CessPercentage_num ,  
                ISNULL(IM.ACess, 0.00) AS ACess ,  
                dbo.Get_Current_Stock(IM.ITEM_ID) AS Current_Stock ,  
                IC.item_cat_name ,  
                CAST(dbo.Get_item_rate_from_previous_mrn(@V_ITEM_ID) AS NUMERIC(18,  
                                                              2)) AS prev_mrn_rate  
        FROM    ITEM_MASTER IM  
                INNER JOIN VAT_MASTER vm ON vm.VAT_ID = IM.vat_id  
                LEFT JOIN dbo.CessMaster cm ON cm.pk_CessId_num = IM.fk_CessId_num  
                INNER JOIN UNIT_MASTER UM ON IM.UM_ID = UM.UM_ID  
                INNER JOIN item_category IC ON IM.item_category_id = IC.item_cat_id  
        WHERE   IM.ITEM_ID = @V_ITEM_ID       
        
    END       
      
      
      