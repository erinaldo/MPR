ALTER TABLE MATERIAL_RECEIVED_AGAINST_PO_MASTER ADD CUST_ID NUMERIC(18, 0) NULL 
  
  
-------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECIEVED_AGAINST_PO_MASTER]
    (
      @v_Receipt_ID NUMERIC(18, 0) ,
      @v_Receipt_No NUMERIC(18, 0) ,
      @v_Receipt_Code VARCHAR(20) ,
      @v_PO_ID NUMERIC(18, 0) ,
      @v_Receipt_Date DATETIME ,
      @v_Remarks VARCHAR(500) ,
      @v_MRN_NO NUMERIC(18, 0) ,
      @v_MRN_PREFIX VARCHAR(50) ,
      @v_Created_BY VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Division_ID NUMERIC(18, 0) ,
      @v_Proc_Type INT ,
      @V_mrn_status INT ,
      @v_freight NUMERIC(18, 2) ,
      @v_Other_Charges NUMERIC(18, 2) ,
      @v_Discount_amt NUMERIC(18, 2) ,
      @v_GROSS_AMOUNT NUMERIC(18, 2) ,
      @v_GST_AMOUNT NUMERIC(18, 2) ,
      @v_NET_AMOUNT NUMERIC(18, 2) ,
      @V_MRN_TYPE INT ,
      @V_VAT_ON_EXICE INT ,
      @v_Invoice_No NVARCHAR(50) ,
      @v_Invoice_Date DATETIME ,
      @V_CUST_ID NUMERIC(18, 0) ,
      @v_MRNCompanies_ID INT            

    )
AS
    BEGIN            

        IF @V_PROC_TYPE = 1
            BEGIN

			

			

                DECLARE @Remarks VARCHAR(250)

                SET @Remarks = 'Pruchase against PO party invoice no. -'
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)

                EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3, @V_Created_BY

					

					            

                SELECT  @V_Receipt_No = ISNULL(MAX(Receipt_No), 0) + 1
                FROM    MATERIAL_RECEIVED_AGAINST_PO_MASTER            

                INSERT  INTO MATERIAL_RECEIVED_AGAINST_PO_MASTER
                        ( Receipt_ID ,
                          Receipt_No ,
                          Receipt_Code ,
                          Invoice_No ,
                          Invoice_Date ,
                          PO_ID ,
                          Receipt_Date ,
                          Remarks ,
                          MRN_NO ,
                          MRN_PREFIX ,
                          Created_BY ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_ID ,
                          MRN_STATUS ,
                          freight ,
                          Other_charges ,
                          Discount_amt ,
                          GROSS_AMOUNT ,
                          GST_AMOUNT ,
                          NET_AMOUNT ,
                          MRN_TYPE ,
                          VAT_ON_EXICE ,
                          MRNCompanies_ID ,
                          IsPrinted,        
                          CUST_ID
                        )
                VALUES  ( @V_Receipt_ID ,
                          @V_Receipt_No ,
                          @V_Receipt_Code ,
                          @v_Invoice_No ,
                          @v_Invoice_Date ,
                          @V_PO_ID ,
                          @v_Receipt_Date ,
                          @V_Remarks ,
                          @V_MRN_NO ,
                          @V_MRN_PREFIX ,
                          @V_Created_BY ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_ID ,
                          @V_mrn_status ,
                          @v_freight ,
                          @v_other_charges ,
                          @v_Discount_amt ,
                          @v_GROSS_AMOUNT ,
                          @v_GST_AMOUNT ,
                          @v_NET_AMOUNT ,
                          @v_MRN_TYPE ,
                          @V_VAT_ON_EXICE ,
                          @v_MRNCompanies_ID ,
                          0,          
                          @V_CUST_ID
                        )            

                UPDATE  MRN_SERIES
                SET     CURRENT_USED = CURRENT_USED + 1
                WHERE   DIV_ID = @v_Division_ID              

                RETURN @V_MRN_NO   
            END            

        IF @V_PROC_TYPE = 2
            BEGIN            

                UPDATE  MATERIAL_RECEIVED_AGAINST_PO_MASTER
                SET     PO_ID = @V_PO_ID ,
                        Receipt_Date = @v_Receipt_Date ,
                        Remarks = @V_Remarks ,
                        Created_BY = @V_Created_BY ,
                        Creation_Date = @V_Creation_Date ,
                        Modified_By = @V_Modified_By ,
                        Modification_Date = @V_Modification_Date ,
                        Division_ID = @V_Division_ID ,
                        freight = @v_freight ,
                        Other_charges = @v_Other_Charges ,
                        Discount_amt = @v_Discount_amt ,
                        VAT_ON_EXICE = @V_VAT_ON_EXICE ,
                        MRNCompanies_ID = @v_MRNCompanies_ID
                WHERE   Receipt_ID = @V_Receipt_ID            

            END            

--        IF @V_PROC_TYPE = 3             

--            BEGIN              

--                DECLARE cur CURSOR            

--                    FOR SELECT  Item_ID,            

--                                Item_Qty,            

--                                Indent_ID            

--                        FROM    MATERIAL_RECEIVED_AGAINST_PO_DETAIL            

--                        WHERE   Receipt_ID = @V_Receipt_ID              

--              

--                DECLARE @itemid NUMERIC(18, 0)              

--                DECLARE @itemQty NUMERIC(18, 4)              

--                DECLARE @IndentID NUMERIC(18, 0)              

--              

--              

--                OPEN cur              

--                FETCH NEXT FROM cur INTO @itemid, @itemQty, @IndentID              

--                WHILE @@fetch_status = 0              

--                    BEGIN              

--                 

--                        UPDATE  PO_STATUS            

--                        SET     RECIEVED_QTY = RECIEVED_QTY - @itemQty,            

--                                BALANCE_QTY = BALANCE_QTY + @itemQty            

--                        WHERE   PO_ID = @V_PO_ID            

--                                AND ITEM_ID = @itemid            

--                                AND INDENT_ID = @IndentID              

--              

--                        UPDATE  ITEM_DETAIL            

--                        SET     CURRENT_STOCK = CURRENT_STOCK - @itemQty            

--                        WHERE   ITEM_ID = @itemid            

--                                AND DIV_ID = @V_Division_ID               

--              

--                        FETCH NEXT FROM cur INTO @itemid, @itemQty, @IndentID              

--                    END              

--                CLOSE cur              

--                DEALLOCATE cur              

--                

--                DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_DETAIL            

--                WHERE   Receipt_ID = @V_Receipt_ID              

--              

--                DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_MASTER            

--                WHERE   Receipt_ID = @V_Receipt_ID              

--            END             

    END

-------------------------------------------------

-------------------------------------------------