
INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0015_19_June_2018_SynchronizationCorrection' ,
          GETDATE()
        )
Go

CREATE PROCEDURE proc_SyncMMSData
    (
      @OutletId INT = NULL ,
      @NewOutlet BIT = '0'
    )
AS
    BEGIN  
      
  -----------Delete All Data from MMSPLUS-----------  
  
        DELETE  FROM MMSPLUS.dbo.city_master  
        DELETE  FROM MMSPLUS.dbo.Item_Master   
        DELETE  FROM MMSPLUS.dbo.ACCOUNT_GROUPS  
        DELETE  FROM MMSPLUS.dbo.ACCOUNT_MASTER  
        DELETE  FROM MMSPLUS.dbo.COST_CENTER_MASTER  
        DELETE  FROM MMSPLUS.dbo.COUNTRY_MASTER   
        DELETE  FROM MMSPLUS.dbo.DELIVERY_RATING_MASTER  
        DELETE  FROM MMSPLUS.dbo.EXCISE_MASTER  
        DELETE  FROM MMSPLUS.dbo.ITEM_CATEGORY  
        DELETE  FROM MMSPLUS.dbo.ITEM_CATEGORY_HEAD_MASTER  
        DELETE  FROM MMSPLUS.dbo.PO_TYPE_MASTER   
        DELETE  FROM MMSPLUS.dbo.PurchaseType_Master  
        DELETE  FROM MMSPLUS.dbo.QUALITY_RATING_MASTER  
        DELETE  FROM MMSPLUS.dbo.UNIT_MASTER  
        DELETE  FROM MMSPLUS.dbo.USER_MASTER  
        DELETE  FROM MMSPLUS.dbo.VAT_MASTER  
        DELETE  FROM MMSPLUS.dbo.STATE_MASTER  
        DELETE  FROM MMSPLUS.dbo.CategoryMaster  
        DELETE  FROM MMSPLUS.dbo.MenuMaster  
        DELETE  FROM MMSPLUS.dbo.MenuMapping  
        DELETE  FROM MMSPLUS.dbo.OutletMaster  
        DELETE  FROM MMSPLUS.dbo.Hsncode_master  
        DELETE  FROM MMSPLUS.dbo.Label_Master  
        DELETE  FROM MMSPLUS.dbo.Label_Items  
        DELETE  FROM MMSPLUS.dbo.LabelItem_Mapping  
        DELETE  FROM MMSPLUS.dbo.Company_Master  
        DELETE  FROM MMSPLUS.dbo.CESSMASTER
        
        IF @NewOutlet = '1'
            BEGIN
                DELETE  FROM MMSPLUS.dbo.CN_SERIES  
                DELETE  FROM MMSPLUS.dbo.MRN_SERIES  
                DELETE  FROM MMSPLUS.dbo.INVOICE_SERIES
                DELETE  FROM MMSPLUS.dbo.DN_SERIES  
                DELETE  FROM MMSPLUS.dbo.DC_SERIES  
                DELETE  FROM MMSPLUS.dbo.PM_Series  
                DELETE  FROM MMSPLUS.dbo.PO_SERIES  
            END
  
  
  -----------Insert Data into MMSPLUS from ADMINPLUS based on selected outlet-----------  
  
        INSERT  INTO MMSPLUS.dbo.city_master
                SELECT  pk_CityId_num as CITY_ID,'C' as CITY_CODE,CityName_vch as CITY_NAME,'' as CITY_DESC, fk_StateId_num as STATE_ID,fk_CreatedBy_num as CREATED_BY,CreatedDate_dt as CREATION_DATE,fk_ModifiedBy_num as MODIFIED_BY,ModifiedDate_dt MODIFIED_DATE, @OutletId as DIVISION_ID 
                FROM    ADMINPLUS.dbo.CityMaster   
                  
        INSERT  INTO MMSPLUS.dbo.Item_Master
                SELECT  *
                FROM    ADMINPLUS.dbo.Item_Master 
        
        INSERT  INTO MMSPLUS.dbo.ACCOUNT_GROUPS
                SELECT  *
                FROM    ADMINPLUS.dbo.ACCOUNT_GROUPS 
                  
        INSERT  INTO MMSPLUS.dbo.ACCOUNT_MASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.ACCOUNT_MASTER   
                
        INSERT  INTO MMSPLUS.dbo.COST_CENTER_MASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.COST_CENTER_MASTER where ADMINPLUS.dbo.COST_CENTER_MASTER.Division_Id = @OutletId 
                 
        INSERT  INTO MMSPLUS.dbo.COUNTRY_MASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.COUNTRY_MASTER   
                
        INSERT  INTO MMSPLUS.dbo.DELIVERY_RATING_MASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.DELIVERY_RATING_MASTER   
                
        INSERT  INTO MMSPLUS.dbo.EXCISE_MASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.EXCISE_MASTER                   
        
        INSERT  INTO MMSPLUS.dbo.ITEM_CATEGORY
                SELECT  *
                FROM    ADMINPLUS.dbo.ITEM_CATEGORY  
                 
        INSERT  INTO MMSPLUS.dbo.ITEM_CATEGORY_HEAD_MASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.ITEM_CATEGORY_HEAD_MASTER   
                
        INSERT  INTO MMSPLUS.dbo.PO_TYPE_MASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.PO_TYPE_MASTER          
        
        INSERT  INTO MMSPLUS.dbo.PurchaseType_Master
                SELECT  *
                FROM    ADMINPLUS.dbo.PurchaseType_Master   
                
        INSERT  INTO MMSPLUS.dbo.QUALITY_RATING_MASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.QUALITY_RATING_MASTER  
                 
        INSERT  INTO MMSPLUS.dbo.UNIT_MASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.UNIT_MASTER   
                
        INSERT  INTO MMSPLUS.dbo.USER_MASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.USER_MASTER_MMS where ADMINPLUS.dbo.USER_MASTER_MMS.division_id = @OutletId
         
        INSERT  INTO MMSPLUS.dbo.VAT_MASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.VAT_MASTER   
                
        INSERT  INTO MMSPLUS.dbo.STATE_MASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.STATE_MASTER   
                
        INSERT  INTO MMSPLUS.dbo.CategoryMaster
                SELECT  *
                FROM    ADMINPLUS.dbo.CategoryMaster  
                 
        INSERT  INTO MMSPLUS.dbo.MenuMaster
                SELECT  *
                FROM    ADMINPLUS.dbo.MenuMaster  
                
		INSERT  INTO MMSPLUS.dbo.MenuMapping
                SELECT  *
                FROM    ADMINPLUS.dbo.MenuMapping  
                
        INSERT  INTO MMSPLUS.dbo.OutletMaster
                SELECT  *
                FROM    ADMINPLUS.dbo.OutletMaster   
                
                
        INSERT  INTO MMSPLUS.dbo.Hsncode_master
                SELECT  *
                FROM    ADMINPLUS.dbo.Hsncode_master  
                
        INSERT  INTO MMSPLUS.dbo.Label_Master
                SELECT  *
                FROM    ADMINPLUS.dbo.Label_Master  
                
                
         INSERT  INTO MMSPLUS.dbo.Label_Items
                SELECT  *
                FROM    ADMINPLUS.dbo.Label_Items  
                
          INSERT  INTO MMSPLUS.dbo.LabelItem_Mapping
                SELECT  *
                FROM    ADMINPLUS.dbo.LabelItem_Mapping  
                
          INSERT  INTO MMSPLUS.dbo.Company_Master
                SELECT  *
                FROM    ADMINPLUS.dbo.Company_Master  
                
          INSERT  INTO MMSPLUS.dbo.CESSMASTER
                SELECT  *
                FROM    ADMINPLUS.dbo.CESSMASTER 
                
                
          IF @NewOutlet = '1'
            BEGIN
            
				INSERT  INTO MMSPLUS.dbo.CN_SERIES
                SELECT  *
                FROM    ADMINPLUS.dbo.CN_SERIES where ADMINPLUS.dbo.CN_SERIES.DIV_ID = @OutletId
                
                INSERT  INTO MMSPLUS.dbo.MRN_SERIES
                SELECT  *
                FROM    ADMINPLUS.dbo.MRN_SERIES where ADMINPLUS.dbo.MRN_SERIES.DIV_ID = @OutletId
                
                INSERT  INTO MMSPLUS.dbo.INVOICE_SERIES
                SELECT  *
                FROM    ADMINPLUS.dbo.INVOICE_SERIES where ADMINPLUS.dbo.INVOICE_SERIES.DIV_ID = @OutletId
                
                INSERT  INTO MMSPLUS.dbo.DN_SERIES
                SELECT  *
                FROM    ADMINPLUS.dbo.DN_SERIES where ADMINPLUS.dbo.DN_SERIES.DIV_ID = @OutletId
                
                INSERT  INTO MMSPLUS.dbo.DC_SERIES
                SELECT  *
                FROM    ADMINPLUS.dbo.DC_SERIES where ADMINPLUS.dbo.DC_SERIES.DIV_ID = @OutletId
                
                INSERT  INTO MMSPLUS.dbo.PM_Series
                SELECT  *
                FROM    ADMINPLUS.dbo.PM_Series where ADMINPLUS.dbo.PM_Series.DIV_ID = @OutletId
                
                INSERT  INTO MMSPLUS.dbo.PO_SERIES
                SELECT  *
                FROM    ADMINPLUS.dbo.PO_SERIES where ADMINPLUS.dbo.PO_SERIES.DIV_ID = @OutletId
            
            End
  
  
    END    

Go