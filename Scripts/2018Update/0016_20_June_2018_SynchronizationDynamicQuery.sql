
INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0016_20_June_2018_SynchronizationDynamicQuery' ,
          GETDATE()
        )
Go

-----------------------------------------------------------------------------------------------------------------------------------------
 

 --proc_SyncMMSData '1','MMSPLUS','ADMINPLUS'   
      
Alter PROCEDURE proc_SyncMMSData    
    (    
      @OutletId VARCHAR(50) ,  
      @destDB VARCHAR(100) ,  
      @sourceDB VARCHAR(100) ,    
      @NewOutlet BIT = '0'    
    )    
AS    
    BEGIN  
      
    --   DECLARE @OutletId VARCHAR(50)  
 --   SET @OutletId = 1  
      
 --   DECLARE @sourceDB VARCHAR(100)  
 --   SET @sourceDB = 'ADMINPLUS'  
   
 --DECLARE @destDB VARCHAR(100)  
 --SET @destDB = 'POSPLUS'  
      
    DECLARE @deleteDynamicQuery1 NVARCHAR(MAX)   
    DECLARE @deleteDynamicQuery2 NVARCHAR(MAX)      
    DECLARE @insertDynamicQuery1 NVARCHAR(MAX)  
    DECLARE @insertDynamicQuery2 nVARCHAR(MAX)     
          
  -----------Delete All Data from MMSPLUS-----------      
      
    SET @deleteDynamicQuery1 = '    DELETE  FROM [' + @destDB + '].dbo.city_master      
         DELETE  FROM [' + @destDB + '].dbo.Item_Master       
         DELETE  FROM [' + @destDB + '].dbo.ACCOUNT_GROUPS      
         DELETE  FROM [' + @destDB + '].dbo.ACCOUNT_MASTER      
         DELETE  FROM [' + @destDB + '].dbo.COST_CENTER_MASTER      
         DELETE  FROM [' + @destDB + '].dbo.COUNTRY_MASTER       
         DELETE  FROM [' + @destDB + '].dbo.DELIVERY_RATING_MASTER      
         DELETE  FROM [' + @destDB + '].dbo.EXCISE_MASTER      
         DELETE  FROM [' + @destDB + '].dbo.ITEM_CATEGORY      
         DELETE  FROM [' + @destDB + '].dbo.ITEM_CATEGORY_HEAD_MASTER      
         DELETE  FROM [' + @destDB + '].dbo.PO_TYPE_MASTER       
         DELETE  FROM [' + @destDB + '].dbo.PurchaseType_Master      
         DELETE  FROM [' + @destDB + '].dbo.QUALITY_RATING_MASTER      
         DELETE  FROM [' + @destDB + '].dbo.UNIT_MASTER      
         DELETE  FROM [' + @destDB + '].dbo.USER_MASTER      
         DELETE  FROM [' + @destDB + '].dbo.VAT_MASTER      
         DELETE  FROM [' + @destDB + '].dbo.STATE_MASTER      
         DELETE  FROM [' + @destDB + '].dbo.CategoryMaster      
         DELETE  FROM [' + @destDB + '].dbo.MenuMaster      
         DELETE  FROM [' + @destDB + '].dbo.MenuMapping      
         DELETE  FROM [' + @destDB + '].dbo.OutletMaster      
         DELETE  FROM [' + @destDB + '].dbo.Hsncode_master      
         DELETE  FROM [' + @destDB + '].dbo.Label_Master      
         DELETE  FROM [' + @destDB + '].dbo.Label_Items      
         DELETE  FROM [' + @destDB + '].dbo.LabelItem_Mapping      
         DELETE  FROM [' + @destDB + '].dbo.Company_Master      
         DELETE  FROM [' + @destDB + '].dbo.CESSMASTER '    
          
        --print @deleteDynamicQuery   
        EXEC sys.[sp_executesql] @deleteDynamicQuery1   
            
        IF @NewOutlet = '1'    
            BEGIN    
                SET @deleteDynamicQuery2 = 'DELETE  FROM [' + @destDB + '].dbo.CN_SERIES      
           DELETE  FROM [' + @destDB + '].dbo.MRN_SERIES      
           DELETE  FROM [' + @destDB + '].dbo.INVOICE_SERIES    
           DELETE  FROM [' + @destDB + '].dbo.DN_SERIES      
           DELETE  FROM [' + @destDB + '].dbo.DC_SERIES      
           DELETE  FROM [' + @destDB + '].dbo.PM_Series      
           DELETE  FROM [' + @destDB + '].dbo.PO_SERIES '   
                  
                --print @deleteDynamicQuery   
    EXEC sys.[sp_executesql] @deleteDynamicQuery2      
      
            END      
      
  -----------Insert Data into MMSPLUS from ADMINPLUS based on selected outlet-----------      
      
        SET @insertDynamicQuery1 = 'INSERT  INTO [' + @destDB + '].dbo.city_master    
                SELECT  pk_CityId_num as CITY_ID,''C'' as CITY_CODE,CityName_vch as CITY_NAME,'''' as CITY_DESC,   
                fk_StateId_num as STATE_ID,fk_CreatedBy_num as CREATED_BY,CreatedDate_dt as CREATION_DATE,  
                fk_ModifiedBy_num as MODIFIED_BY,  
                ModifiedDate_dt MODIFIED_DATE, ' + @OutletId + ' as DIVISION_ID     
                FROM    [' + @sourceDB + '].dbo.CityMaster       
                      
        INSERT  INTO [' + @destDB + '].dbo.Item_Master    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.Item_Master     
            
        INSERT  INTO [' + @destDB + '].dbo.ACCOUNT_GROUPS    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.ACCOUNT_GROUPS     
                      
        INSERT  INTO [' + @destDB + '].dbo.ACCOUNT_MASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.ACCOUNT_MASTER       
                    
        INSERT  INTO [' + @destDB + '].dbo.COST_CENTER_MASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.COST_CENTER_MASTER  As ccm 
                where ccm.Division_Id = ' + @OutletId + '     
                     
        INSERT  INTO [' + @destDB + '].dbo.COUNTRY_MASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.COUNTRY_MASTER       
                    
        INSERT  INTO [' + @destDB + '].dbo.DELIVERY_RATING_MASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.DELIVERY_RATING_MASTER       
                    
        INSERT  INTO [' + @destDB + '].dbo.EXCISE_MASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.EXCISE_MASTER                       
            
        INSERT  INTO [' + @destDB + '].dbo.ITEM_CATEGORY    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.ITEM_CATEGORY      
                     
        INSERT  INTO [' + @destDB + '].dbo.ITEM_CATEGORY_HEAD_MASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.ITEM_CATEGORY_HEAD_MASTER    
                     
        INSERT  INTO [' + @destDB + '].dbo.PO_TYPE_MASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.PO_TYPE_MASTER              
            
        INSERT  INTO [' + @destDB + '].dbo.PurchaseType_Master    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.PurchaseType_Master       
                    
        INSERT  INTO [' + @destDB + '].dbo.QUALITY_RATING_MASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.QUALITY_RATING_MASTER      
                     
        INSERT  INTO [' + @destDB + '].dbo.UNIT_MASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.UNIT_MASTER       
                    
        INSERT  INTO [' + @destDB + '].dbo.USER_MASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.USER_MASTER_MMS AS umaster    
                WHERE umaster.division_id = ' + @OutletId + '    
             
        INSERT  INTO [' + @destDB + '].dbo.VAT_MASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.VAT_MASTER       
                    
        INSERT  INTO [' + @destDB + '].dbo.STATE_MASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.STATE_MASTER       
                    
        INSERT  INTO [' + @destDB + '].dbo.CategoryMaster    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.CategoryMaster      
                     
        INSERT  INTO [' + @destDB + '].dbo.MenuMaster    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.MenuMaster      
                    
  INSERT  INTO [' + @destDB + '].dbo.MenuMapping    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.MenuMapping      
                    
        INSERT  INTO [' + @destDB + '].dbo.OutletMaster    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.OutletMaster   
                    
        INSERT  INTO [' + @destDB + '].dbo.Hsncode_master    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.Hsncode_master      
                    
        INSERT  INTO [' + @destDB + '].dbo.Label_Master    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.Label_Master   
                    
        INSERT  INTO [' + @destDB + '].dbo.Label_Items    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.Label_Items      
                    
        INSERT  INTO [' + @destDB + '].dbo.LabelItem_Mapping    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.LabelItem_Mapping      
                    
        INSERT  INTO [' + @destDB + '].dbo.Company_Master    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.Company_Master      
                    
        INSERT  INTO [' + @destDB + '].dbo.CESSMASTER    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.CESSMASTER '    
                  
         --print @insertDynamicQuery1   
  EXEC sys.[sp_executesql] @insertDynamicQuery1    
                    
                    
          IF @NewOutlet = '1'    
            BEGIN    
                
    SET @insertDynamicQuery2 = ' INSERT  INTO [' + @destDB + '].dbo.CN_SERIES    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.CN_SERIES AS cnseries where cnseries.DIV_ID = ' + @OutletId + '    
                    
                INSERT  INTO [' + @destDB + '].dbo.MRN_SERIES    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.MRN_SERIES AS mrnseries where mrnseries.DIV_ID = ' + @OutletId + '    
                    
                INSERT  INTO [' + @destDB + '].dbo.INVOICE_SERIES    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.INVOICE_SERIES AS invSeries where invSeries.DIV_ID = ' + @OutletId + '    
                    
                INSERT  INTO [' + @destDB + '].dbo.DN_SERIES    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.DN_SERIES AS dnseries where dnseries.DIV_ID = ' + @OutletId + '    
                    
                INSERT  INTO [' + @destDB + '].dbo.DC_SERIES    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.DC_SERIES AS dcseries where dcseries.DIV_ID = ' + @OutletId + '    
                    
                INSERT  INTO [' + @destDB + '].dbo.PM_Series    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.PM_Series AS pmseries where pmseries.DIV_ID = ' + @OutletId + '    
                    
                INSERT  INTO [' + @destDB + '].dbo.PO_SERIES    
                SELECT  *    
                FROM    [' + @sourceDB + '].dbo.PO_SERIES AS poseries where poseries.DIV_ID = ' + @OutletId + ''   
                
     --print @insertDynamicQuery2   
    EXEC sys.[sp_executesql] @insertDynamicQuery2   
              
            End   
      
    END

Go
