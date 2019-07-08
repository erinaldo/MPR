INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0044_08_Apr_2019_All_Proc_View_Function_Type' ,
          GETDATE()
        )
Go
------------------------------------------Run this Qury in AdminPlus-------------------------------------------------------
INSERT  INTO Adminplus.dbo.ACCOUNT_MASTER( ACC_ID , ACC_CODE , ACC_NAME ,ACC_DESC , AG_ID , OPENING_BAL , OPENING_BAL_TYPE ,
          CONTACT_PERSON ,PERSON_DESIGNATION , PHONE_PRIM , PHONE_SEC , MOBILE_NO ,ADDRESS_PRIM , ADDRESS_SEC ,
          CITY_ID ,VAT_NO ,VAT_DATE , IS_OUTSIDE ,CREATED_BY ,CREATION_DATE ,MODIFIED_BY ,MODIFIED_DATE ,
          DIVISION_ID ,Fk_HSN_ID ,fk_GST_ID ,FK_GST_TYPE_ID ,Is_Active
        )
VALUES  ( 10088 , 'RE' ,'RCM Expenses' ,'RCM Expenses' ,11 ,0.00 ,'Dr' , '' , '' , '' , '' , '' , '' ,
          '' , 1 ,'' ,'2018-03-26 00:00:00.000' , 0 ,'Admin' ,'2018-03-26 00:00:00.000' ,'' ,'2019-04-18 12:53:42' ,
          1 , NULL , NULL , NULL ,1  
        )
---------------------------------------------------------------------------------------------------------



ALTER TABLE dbo.SALE_INVOICE_MASTER ADD ConsumerHeadID NUMERIC(18,2) DEFAULT 0,
freight NUMERIC(18,2) DEFAULT 0,freight_type CHAR(10),FreightTaxApplied BIT DEFAULT 0,
FreightTaxValue   NUMERIC(18,2) DEFAULT 0

ALTER TABLE dbo.SALE_INVOICE_DETAIL ADD
freight NUMERIC(18,2) DEFAULT 0,freight_type CHAR(10),
FreightTaxValue NUMERIC(18,2) DEFAULT 0,FreightCessValue NUMERIC(18,2) DEFAULT 0

ALTER TABLE dbo.CreditNote_Master ADD ConsumerHeadID NUMERIC(18,2) DEFAULT 0
Go

ALTER TABLE SettlementDetail ADD OpenCrAmount NUMERIC(18,2)DEFAULT 0, OpenCrNo NVARCHAR(50),
 OpenDrAmount NUMERIC(18,2)DEFAULT 0, OpenDrNO NVARCHAR(50)

 ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL ADD
freight NUMERIC(18,2) DEFAULT 0,freight_type CHAR(10),
FreightTaxValue NUMERIC(18,2) DEFAULT 0,FreightCessValue NUMERIC(18,2) DEFAULT 0

ALTER TABLE dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL ADD
freight NUMERIC(18,2) DEFAULT 0,freight_type CHAR(10),
FreightTaxValue NUMERIC(18,2) DEFAULT 0,FreightCessValue NUMERIC(18,2) DEFAULT 0

ALTER TABLE dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER ADD
IS_RCM_Applicable BIT DEFAULT 0

ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER ADD
IS_RCM_Applicable BIT DEFAULT 0

GO
---------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------