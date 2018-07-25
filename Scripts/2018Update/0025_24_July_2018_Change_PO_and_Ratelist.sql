 INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0025_24_July_2018_Change_PO_and_Ratelist' ,
          GETDATE()
        )
Go

ALTER PROCEDURE [dbo].[GET_SUPPLIER_TOTAL_DETAIL] @V_SRL_ID INT  
AS   
    BEGIN    
        SELECT  *  
        FROM    SUPPLIER_RATE_LIST  
        WHERE   SRL_ID = @V_SRL_ID    
     
        SELECT  dbo.SUPPLIER_RATE_LIST_DETAIL.SRL_ID,  
                dbo.SUPPLIER_RATE_LIST_DETAIL.ITEM_RATE,  
                dbo.SUPPLIER_RATE_LIST_DETAIL.DEL_QTY,  
                dbo.SUPPLIER_RATE_LIST_DETAIL.DEL_DAYS,  
                dbo.SUPPLIER_RATE_LIST.SRL_ID,  
                dbo.SUPPLIER_RATE_LIST.SUPP_ID,  
                dbo.ITEM_MASTER.ITEM_ID,  
                dbo.ITEM_MASTER.BarCode_vch AS ITEM_CODE,  
                dbo.ITEM_MASTER.ITEM_NAME,  
                dbo.ITEM_DETAIL.ITEM_ID,  
                dbo.UNIT_MASTER.UM_ID,  
                dbo.SUPPLIER_RATE_LIST_DETAIL.ITEM_ID,  
                dbo.UNIT_MASTER.UM_Name,  
                vm.VAT_PERCENTAGE  
        FROM    dbo.ITEM_DETAIL  
                INNER JOIN dbo.ITEM_MASTER ON dbo.ITEM_DETAIL.ITEM_ID = dbo.ITEM_MASTER.ITEM_ID  
                inner join VAT_MASTER vm on vm.VAT_ID = ITEM_MASTER.vat_id  
                INNER JOIN dbo.UNIT_MASTER ON dbo.ITEM_MASTER.UM_ID = dbo.UNIT_MASTER.UM_ID  
                INNER JOIN dbo.SUPPLIER_RATE_LIST_DETAIL  
                INNER JOIN dbo.SUPPLIER_RATE_LIST ON dbo.SUPPLIER_RATE_LIST_DETAIL.SRL_ID = dbo.SUPPLIER_RATE_LIST.SRL_ID ON dbo.ITEM_DETAIL.ITEM_ID = dbo.SUPPLIER_RATE_LIST_DETAIL.ITEM_ID  
        WHERE   SUPPLIER_RATE_LIST.SRL_ID = @V_SRL_ID  
        ORDER BY dbo.ITEM_MASTER.item_name        
             
    
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
      
      