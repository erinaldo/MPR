Alter table SALE_INVOICE_MASTER add Flag int null
Alter table SALE_INVOICE_DETAIL add GSTPaid varchar(5) null


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
      @v_GROSS_AMOUNT DECIMAL(18, 2) ,
      @v_VAT_AMOUNT DECIMAL(18, 2) ,
      @v_NET_AMOUNT DECIMAL(18, 0) ,
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
      @V_MODE INT,
      @V_Flag INT  =0

    )
AS
    BEGIN       
      
       
        --SET @v_NET_AMOUNT = ROUND(@v_NET_AMOUNT,0)
        DECLARE @InputID NUMERIC
        DECLARE @CInputID NUMERIC
        SET @CInputID = 10017

        DECLARE @RoundOff NUMERIC(18, 2)
        DECLARE @CGST_Amount NUMERIC(18, 2)
        SET @CGST_Amount = ( @v_VAT_AMOUNT / 2 )

        SET @RoundOff = @v_NET_AMOUNT - ( @v_GROSS_AMOUNT + @v_VAT_AMOUNT )


        SET @InputID = ( SELECT CASE WHEN @v_INV_TYPE = 'I' THEN 10021
                                     WHEN @v_INV_TYPE = 'S' THEN 10024
                                     WHEN @v_INV_TYPE = 'U' THEN 10075
                                END AS inputid
                       )

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
                          TRANSPORT ,
                          SHIPP_ADD_ID ,
                          LR_NO ,
                          INV_TYPE,
                          Flag

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
                          @V_TRANSPORT ,
                          @v_SHIPP_ADD_ID ,
                          @v_LR_NO ,
                          @v_INV_TYPE,
                          @V_Flag
                        ) 

                DECLARE @Remarks VARCHAR(250)

                SET @Remarks = 'Sale against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))

                EXECUTE Proc_Ledger_Insert @V_CUST_ID, 0, @V_NET_AMOUNT,
                    @Remarks, @V_Division_ID, @V_SI_ID, 16, @V_Created_BY	
					
                EXECUTE Proc_Ledger_Insert 10071, @V_GROSS_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_Created_BY
					
					


               
			   SET @Remarks = 'GST against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))
                IF @v_INV_TYPE <> 'I'
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @V_Created_BY

                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @V_Created_BY
                    END
                ELSE
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @InputID, @v_VAT_AMOUNT, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @V_Created_BY
                    END   



					SET @Remarks = 'Round Off against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))
                EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_Created_BY

					SET @Remarks = 'Stock out against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))
                EXECUTE Proc_Ledger_Insert 10073, 0, @V_NET_AMOUNT, @Remarks,
                   @V_Division_ID, @V_SI_ID, 16, @V_Created_BY



            END        

        IF @V_MODE = 2
            BEGIN  

                UPDATE  SALE_INVOICE_MASTER
                SET     CUST_ID = @V_CUST_ID ,
                        INVOICE_STATUS = @V_INVOICE_STATUS ,
                        REMARKS = @V_REMARKS ,
                        PAYMENTS_REMARKS = @V_PAYMENTS_REMARKS ,
                        SALE_TYPE = @V_SALE_TYPE ,
                        GROSS_AMOUNT = @V_GROSS_AMOUNT ,
                        VAT_AMOUNT = @V_VAT_AMOUNT ,
                        NET_AMOUNT = @V_NET_AMOUNT ,
                        IS_SAMPLE = @V_IS_SAMPLE ,
                        DELIVERY_NOTE_NO = @V_DELIVERY_NOTE_NO ,
                        VAT_CST_PER = @V_VAT_CST_PER ,
                        SAMPLE_ADDRESS = @V_SAMPLE_ADDRESS ,

                        --CREATED_BY = @V_CREATED_BY ,
                        --CREATION_DATE = @V_CREATION_DATE ,
                        MODIFIED_BY = @V_MODIFIED_BY ,
                        MODIFIED_DATE = @V_MODIFIED_DATE ,
                        DIVISION_ID = @V_DIVISION_ID ,
                        VEHICLE_NO = @V_VEHICLE_NO ,
                        TRANSPORT = @V_TRANSPORT ,
                        SHIPP_ADD_ID = @v_SHIPP_ADD_ID ,
                        LR_NO = @v_LR_NO ,
                        INV_TYPE = @v_INV_TYPE
                WHERE   SI_ID = @V_SI_ID     

                SET @Remarks = 'Sale against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))



                  SET @Remarks = 'Sale against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))

                EXECUTE Proc_Ledger_Insert @V_CUST_ID, 0, @V_NET_AMOUNT,
                    @Remarks, @V_Division_ID, @V_SI_ID, 16, @V_Created_BY	
					
                EXECUTE Proc_Ledger_Insert 10071, @V_GROSS_AMOUNT, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_Created_BY
					
					


               
			   SET @Remarks = 'GST against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))
                IF @v_INV_TYPE <> 'I'
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @V_Created_BY

                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @V_Created_BY
                    END
                ELSE
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @InputID, @v_VAT_AMOUNT, 0,
                            @Remarks, @V_Division_ID, @V_SI_ID, 16,
                            @V_Created_BY
                    END   



					SET @Remarks = 'Round Off against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))
                EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_Created_BY

					SET @Remarks = 'Stock out against invoice No- ' + @V_SI_CODE
                    + CAST(@V_SI_NO AS VARCHAR(50))
                EXECUTE Proc_Ledger_Insert 10073, 0, @V_NET_AMOUNT, @Remarks,
                    @V_Division_ID, @V_SI_ID, 16, @V_Created_BY


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

    
    -----------------------------------------------------------
    
    Alter PROCEDURE [dbo].[PROC_OUTSIDE_SALE_DETAIL_NEW]  
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
      @V_MODE INT,
      @v_GSTPAID VARCHAR(5) ='N'
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
                          DISCOUNT_VALUE,
                          GSTPaid  
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
                          @v_DISCOUNT_VALUE,
                          @v_GSTPAID
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
  