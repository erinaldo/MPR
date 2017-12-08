-- ================== AdminPLUS and MMSPLUS ===========================
-- Author:		<Yogesh Chandra Upreti>
-- Create date: <1-April-2010>
-- Description:	<Account Master Value Insert>
CREATE PROC [dbo].[proc_AccountMasterInsert]
    (
      @ACC_ID NUMERIC ,
      @ACC_CODE VARCHAR(20) = '' ,
      @ACC_NAME VARCHAR(100) ,
      @ACC_DESC VARCHAR(500) = '' ,
      @AG_ID NUMERIC ,
      @OPENING_BAL NUMERIC(18, 2)=0 ,
      @OPENING_BAL_TYPE VARCHAR(3)='Cr' ,
      @CONTACT_PERSON VARCHAR(100)='' ,
      @PERSON_DESIGNATION VARCHAR(100)='' ,
      @PHONE_PRIM VARCHAR(20) ,
      @PHONE_SEC VARCHAR(20)=NULL ,
      @MOBILE_NO VARCHAR(20)=NULL ,
      @ADDRESS_PRIM VARCHAR(500) ,
      @ADDRESS_SEC VARCHAR(500)=NULL ,
      @CITY_ID NUMERIC ,
      @VAT_NO VARCHAR(50) ,
      @VAT_DATE DATETIME = GETDATE,
      @IS_OUTSIDE BIT =false,
      @CREATED_BY VARCHAR(100)='Admin' ,
      @CREATION_DATE DATETIME = GETDATE,
      @MODIFIED_BY VARCHAR(100)=NULL ,
      @MODIFIED_DATE DATETIME=NULL ,
      @DIVISION_ID NUMERIC =1    
    )
AS
    BEGIN    
        SELECT  @ACC_ID = ISNULL(MAX(ACC_ID), 0) + 1
        FROM    dbo.ACCOUNT_MASTER    
        INSERT  INTO ACCOUNT_MASTER
                ( [ACC_ID] ,
                  [ACC_CODE] ,
                  [ACC_NAME] ,
                  [ACC_DESC] ,
                  [AG_ID] ,
                  [OPENING_BAL] ,
                  [OPENING_BAL_TYPE] ,
                  [CONTACT_PERSON] ,
                  [PERSON_DESIGNATION] ,
                  [PHONE_PRIM] ,
                  [PHONE_SEC] ,
                  [MOBILE_NO ] ,
                  [ADDRESS_PRIM] ,
                  [ADDRESS_SEC ] ,
                  [CITY_ID] ,
                  [VAT_NO] ,
                  [VAT_DATE] ,
                  [IS_OUTSIDE] ,
                  [CREATED_BY] ,
                  [CREATION_DATE] ,
                  [MODIFIED_BY] ,
                  [MODIFIED_DATE] ,
                  [DIVISION_ID]
		        )
        VALUES  ( @ACC_ID ,
                  @ACC_CODE ,
                  @ACC_NAME ,
                  @ACC_DESC ,
                  @AG_ID ,
                  @OPENING_BAL ,
                  @OPENING_BAL_TYPE ,
                  @CONTACT_PERSON ,
                  @PERSON_DESIGNATION ,
                  @PHONE_PRIM ,
                  @PHONE_SEC ,
                  @MOBILE_NO ,
                  @ADDRESS_PRIM ,
                  @ADDRESS_SEC ,
                  @CITY_ID ,
                  @VAT_NO ,
                  @VAT_DATE ,
                  @IS_OUTSIDE ,
                  @CREATED_BY ,
                  @CREATION_DATE
			--,DATEPART(DD,@CREATION_DATE)
			--,CONVERT(Varchar(10),@CREATION_DATE,101)
                  ,
                  NULL ,
                  NULL ,
                  @DIVISION_ID
		        )
--			,@MODIFIED_BY
--			,@MODIFIED_DATE)
    END


	--============================MMSPLUS==============

	ALTER PROCEDURE [dbo].[PROC_OUTSIDE_SALE_MASTER_SALE_NEW]
    (
      @v_SI_ID INT ,
      @v_SI_CODE VARCHAR(50) ,
      @v_SI_NO DECIMAL ,
      @v_DC_NO DECIMAL ,
      @v_SI_DATE DATETIME ,
      @v_CUST_ID INT ,
      @V_INVOICE_STATUS INT ,
      @v_REMARKS VARCHAR(500) ,
      @v_PAYMENTS_REMARKS VARCHAR(500) ,
      @v_SALE_TYPE VARCHAR(10) ,
      @v_GROSS_AMOUNT DECIMAL(18, 3) ,
      @v_VAT_AMOUNT DECIMAL(18, 3) ,
      @v_NET_AMOUNT DECIMAL(18, 3) ,
      @V_IS_SAMPLE INT ,
      @V_DELIVERY_NOTE_NO VARCHAR(50) ,
      @V_VAT_CST_PER NUMERIC(18, 2) ,
      @V_SAMPLE_ADDRESS VARCHAR(500) ,
      @v_CREATED_BY VARCHAR(50) ,
      @v_CREATION_DATE DATETIME ,
      @v_MODIFIED_BY VARCHAR(50) ,
      @v_MODIFIED_DATE DATETIME ,
      @v_DIVISION_ID INT ,
      @V_VEHICLE_NO VARCHAR(100) = NULL ,
	  @V_TRANSPORT VARCHAR(70) = NULL ,
      @v_SHIPP_ADD_ID INT = NULL ,
      @v_INV_TYPE CHAR(1) = NULL ,
      @v_LR_NO VARCHAR(100) = NULL ,
      @V_MODE INT
    )
AS
    BEGIN                          

	 Set @v_NET_AMOUNT= CEILING(@v_NET_AMOUNT)  

        IF @V_MODE = 1
            BEGIN    

                INSERT  INTO SALE_INVOICE_MASTER
                        ( SI_ID ,
                          SI_CODE ,
                          SI_NO ,
                          DC_GST_NO ,
                          SI_DATE ,
                          CUST_ID ,
                          INVOICE_STATUS ,
                          REMARKS ,
                          PAYMENTS_REMARKS ,
                          SALE_TYPE ,
                          GROSS_AMOUNT ,
                          VAT_AMOUNT ,
                          NET_AMOUNT ,
                          IS_SAMPLE ,
                          DELIVERY_NOTE_NO ,
                          VAT_CST_PER ,
                          SAMPLE_ADDRESS ,
                          CREATED_BY ,
                          CREATION_DATE ,
                          MODIFIED_BY ,
                          MODIFIED_DATE ,
                          DIVISION_ID ,
                          VEHICLE_NO ,
						  TRANSPORT,
                          SHIPP_ADD_ID ,
                          LR_NO ,
                          INV_TYPE
                        )
                VALUES  ( @V_SI_ID ,
                          @V_SI_CODE ,
                          @V_SI_NO ,
                          @v_DC_NO ,
                          @V_SI_DATE ,
                          @V_CUST_ID ,
                          @V_INVOICE_STATUS ,
                          @V_REMARKS ,
                          @V_PAYMENTS_REMARKS ,
                          @V_SALE_TYPE ,
                          @V_GROSS_AMOUNT ,
                          @V_VAT_AMOUNT ,
                          @V_NET_AMOUNT ,
                          @V_IS_SAMPLE ,
                          @V_DELIVERY_NOTE_NO ,
                          @V_VAT_CST_PER ,
                          @V_SAMPLE_ADDRESS ,
                          @V_CREATED_BY ,
                          @V_CREATION_DATE ,
                          @V_MODIFIED_BY ,
                          @V_MODIFIED_DATE ,
                          @V_DIVISION_ID ,
                          @V_VEHICLE_NO ,
						  @V_TRANSPORT,
                          @v_SHIPP_ADD_ID ,
                          @v_LR_NO ,
                          @v_INV_TYPE 
                        )                          

            END   
        IF @V_MODE = 2
            BEGIN  

                UPDATE  SALE_INVOICE_MASTER
                SET     SI_CODE = @V_SI_CODE ,
                        SI_NO = @V_SI_NO ,
                        DC_GST_NO = @v_DC_NO ,
                        SI_DATE = @V_SI_DATE ,
                        CUST_ID = @V_CUST_ID ,
                        INVOICE_STATUS = @V_INVOICE_STATUS ,
                        REMARKS = @V_REMARKS ,
                        IS_SAMPLE = @V_IS_SAMPLE ,
                        DELIVERY_NOTE_NO = @V_DELIVERY_NOTE_NO ,
                        VAT_CST_PER = @V_VAT_CST_PER ,
                        PAYMENTS_REMARKS = @V_PAYMENTS_REMARKS ,
                        SALE_TYPE = @V_SALE_TYPE ,
                        GROSS_AMOUNT = @V_GROSS_AMOUNT ,
                        VAT_AMOUNT = @V_VAT_AMOUNT ,
                        NET_AMOUNT = @V_NET_AMOUNT ,
                        SAMPLE_ADDRESS = @V_SAMPLE_ADDRESS ,
                        MODIFIED_BY = @V_MODIFIED_BY ,
                        MODIFIED_DATE = @V_MODIFIED_DATE ,
                        DIVISION_ID = @V_DIVISION_ID ,
                        VEHICLE_NO = @V_VEHICLE_NO ,
                        SHIPP_ADD_ID = @v_SHIPP_ADD_ID ,
                        INV_TYPE = @v_INV_TYPE ,
                        LR_NO = @v_LR_NO
                WHERE   SI_ID = @V_SI_ID                        



            END                      



        IF @V_MODE = 3
            BEGIN                      



                DECLARE UpdateStock CURSOR
                FOR
                    SELECT  Item_id ,
                            Item_qty ,
                            division_id
                    FROM    sale_invoice_detail
                    WHERE   si_id = @v_SI_ID                        



                OPEN UpdateStock                        



                DECLARE @DivisionId AS INT                        



                DECLARE @ItemId AS INT                        



                DECLARE @ItemQty AS NUMERIC(18, 4)                      



                FETCH NEXT FROM UpdateStock INTO @ItemId, @ItemQty,
                    @DivisionId                        



                WHILE @@FETCH_STATUS = 0
                    BEGIN                        



                        UPDATE  item_detail
                        SET     current_stock = current_stock + @ItemQty
                        WHERE   item_id = @ItemId
                                AND div_id = @DivisionId                        



                        FETCH NEXT FROM UpdateStock INTO @ItemId, @ItemQty,
                            @DivisionId                        



                    END                         



                CLOSE UpdateStock 
                DEALLOCATE UpdateStock   

                DELETE  FROM sale_invoice_Detail
                WHERE   si_id = @v_SI_ID 

                DELETE  FROM sale_invoice_master
                WHERE   si_id = @v_SI_ID                        

            END  
    END 
-----------------------------MMSPLUS ------------------------------------------

ALTER PROCEDURE [dbo].[PROC_OUTSIDE_SALE_DETAIL_NEW]
    (
      @v_SI_ID DECIMAL ,
      @v_ITEM_ID DECIMAL ,
      @v_ITEM_QTY DECIMAL(18, 3) ,
      @v_PKT DECIMAL(18, 3) ,
      @v_ITEM_RATE DECIMAL(18, 3) ,
      @v_ITEM_AMOUNT DECIMAL(18, 3) ,
      @v_VAT_PER DECIMAL(18, 3) ,
      @v_VAT_AMOUNT DECIMAL(18, 3) ,
      @v_CREATED_BY VARCHAR(50) ,
      @v_CREATION_DATE DATETIME ,
      @v_MODIFIED_BY VARCHAR(50) ,
      @v_MODIFIED_DATE DATETIME ,
      @v_DIVISION_ID INT ,
      @v_TARRIF_ID NUMERIC(18, 0) ,
	  @v_DISCOUNT_TYPE VARCHAR(10),
	  @v_DISCOUNT_VALUE DECIMAL(18, 3),
      @V_MODE INT 
    )
AS
    BEGIN      
        IF @V_MODE = 1
            BEGIN  

                INSERT  INTO SALE_INVOICE_DETAIL
                        ( SI_ID ,
                          ITEM_ID ,
                          ITEM_QTY ,
                          PKT ,
                          ITEM_RATE ,
                          ITEM_AMOUNT ,
                          VAT_PER ,
                          VAT_AMOUNT ,
                          BAL_ITEM_RATE ,
                          BAL_ITEM_QTY ,
                          CREATED_BY ,
                          CREATION_DATE ,
                          MODIFIED_BY ,
                          MODIFIED_DATE ,
                          DIVISION_ID ,
                          TARRIF_ID , 
						  DISCOUNT_TYPE,
						  DISCOUNT_VALUE

                        )
                VALUES  ( @V_SI_ID ,
                          @V_ITEM_ID ,
                          @V_ITEM_QTY ,
                          @v_PKT ,
                          @V_ITEM_RATE ,
                          @V_ITEM_AMOUNT ,
                          @V_VAT_PER ,
                          @V_VAT_AMOUNT ,
                          @V_ITEM_RATE ,
                          @V_ITEM_QTY ,
                          @V_CREATED_BY ,
                          @V_CREATION_DATE ,
                          @V_MODIFIED_BY ,
                          @V_MODIFIED_DATE ,
                          @V_DIVISION_ID ,
                          @v_TARRIF_ID ,
						  @v_DISCOUNT_TYPE,
						  @v_DISCOUNT_VALUE
                        )     
                UPDATE  item_detail
                SET     current_stock = current_stock - @v_ITEM_QTY
                WHERE   item_id = @V_ITEM_ID
                        AND div_id = @V_DIVISION_ID  

            END    
			
        IF @V_MODE = 3
            BEGIN    
			
                DECLARE UpdateStock CURSOR
                FOR
                    SELECT  Item_id ,
                            Item_qty ,
                            division_id
                    FROM    sale_invoice_detail
                    WHERE   si_id = @v_SI_ID      
					

                OPEN UpdateStock 
                DECLARE @DivisionId AS INT 
                DECLARE @ItemId AS INT   
                DECLARE @ItemQty AS NUMERIC(18, 4)  
                FETCH NEXT FROM UpdateStock INTO @ItemId, @ItemQty,
                    @DivisionId      
                WHILE @@FETCH_STATUS = 0
                    BEGIN   

                        UPDATE  item_detail
                        SET     current_stock = current_stock + @ItemQty
                        WHERE   item_id = @ItemId
                                AND div_id = @DivisionId   

                        FETCH NEXT FROM UpdateStock INTO @ItemId, @ItemQty,
                            @DivisionId     
                    END 

                CLOSE UpdateStock   
                DEALLOCATE UpdateStock 
                DELETE  FROM sale_invoice_Detail
                WHERE   si_id = @v_SI_ID 
            END  
    END 

