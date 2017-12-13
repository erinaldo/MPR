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

                SET @Remarks = 'Pruchase against party bill no.: '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)  

  

                EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3, @V_Created_BY    

                   
                EXECUTE Proc_Ledger_Insert 10070, 0, @V_NET_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Receipt_ID, 3, @V_Created_BY
  

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
                          IsPrinted ,
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
                          0 ,
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

-----------------------------------------------------------------------------------------------------------------------------------------
    ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECIEVED_WITHOUT_PO_MASTER]
        (
          @v_Received_ID NUMERIC(18, 0) ,
          @v_Received_Code VARCHAR(20) ,
          @v_Received_No NUMERIC(18, 0) ,
          @v_Received_Date DATETIME ,
          @v_Purchase_Type INT ,
          @v_Vendor_ID INT ,
          @v_Remarks VARCHAR(100) ,
          @v_Po_ID NUMERIC(18, 0) ,
          @v_MRN_PREFIX VARCHAR(50) ,
          @v_MRN_NO NUMERIC(18, 0) ,
          @v_Created_By VARCHAR(100) ,
          @v_Creation_Date DATETIME ,
          @v_Modified_By VARCHAR(100) ,
          @v_Modification_Date DATETIME ,
          @v_Invoice_No NVARCHAR(50) ,
          @v_Invoice_Date DATETIME ,
          @v_Division_ID INT ,
          @v_mrn_status INT ,
          @v_freight NUMERIC(18, 2) = NULL ,
          @v_freight_type CHAR = '' ,
          @v_Proc_Type INT ,
          @v_Other_charges NUMERIC(18, 2) = NULL ,
          @v_Discount_amt NUMERIC(18, 2) = NULL ,
          @v_GROSS_AMOUNT NUMERIC(18, 2) ,
          @v_GST_AMOUNT NUMERIC(18, 2) ,
          @v_NET_AMOUNT NUMERIC(18, 2) ,
          @V_MRN_TYPE INT ,
          @V_VAT_ON_EXICE INT ,
          @v_MRNCompanies_ID INT  
        )
    AS
        BEGIN              

  

            IF @V_PROC_TYPE = 1
                BEGIN              

  

                    INSERT  INTO MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                            ( Received_ID ,
                              Received_Code ,
                              Received_No ,
                              Received_Date ,
                              Purchase_Type ,
                              Vendor_ID ,
                              Remarks ,
                              Po_ID ,
                              MRN_PREFIX ,
                              MRN_NO ,
                              Created_By ,
                              Creation_Date ,
                              Modified_By ,
                              Modification_Date ,
                              Invoice_No ,
                              Invoice_Date ,
                              Division_ID ,
                              mrn_status ,
                              freight ,
                              Other_charges ,
                              Discount_amt ,
                              freight_type ,
                              MRNCompanies_ID ,
                              GROSS_AMOUNT ,
                              GST_AMOUNT ,
                              NET_AMOUNT ,
                              MRN_TYPE ,
                              VAT_ON_EXICE ,
                              IsPrinted         
  

                            )
                    VALUES  ( @V_Received_ID ,
                              @V_Received_Code ,
                              @V_Received_No ,
                              @V_Received_Date ,
                              @V_Purchase_Type ,
                              @V_Vendor_ID ,
                              @V_Remarks ,
                              @V_Po_ID ,
                              @V_MRN_PREFIX ,
                              @V_MRN_NO ,
                              @V_Created_By ,
                              @V_Creation_Date ,
                              @V_Modified_By ,
                              @V_Modification_Date ,
                              @v_Invoice_No ,
                              @v_Invoice_Date ,
                              @V_Division_ID ,
                              @V_mrn_status ,
                              @v_freight ,
                              @v_Other_charges ,
                              @v_Discount_amt ,
                              @v_freight_type ,
                              @v_MRNCompanies_ID ,
                              @v_GROSS_AMOUNT ,
                              @v_GST_AMOUNT ,
                              @v_NET_AMOUNT ,
                              @v_MRN_TYPE ,
                              @V_VAT_ON_EXICE ,
                              0   
                            )               

  

                    UPDATE  MRN_SERIES
                    SET     CURRENT_USED = CURRENT_USED + 1
                    WHERE   DIV_ID = @v_Division_ID  


                    DECLARE @Remarks VARCHAR(250)  

                    SET @Remarks = 'Pruchase against party bill no.: '
                        + @v_Invoice_No + ' - '
                        + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)  

  

                    EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,
                        @V_Created_BY  

                    EXECUTE Proc_Ledger_Insert 10070, 0, @V_NET_AMOUNT,
                        @Remarks, @V_Division_ID, @V_Received_ID, 2,
                        @V_Created_BY  

                END   
        END  

-----------------------------------------------------------------------------------------------------------------------------------------

        ALTER PROCEDURE [dbo].[PROC_DebitNote_MASTER]
            (
              @v_DebitNote_ID NUMERIC(18, 0) ,
              @v_DebitNote_No NUMERIC(18, 0) ,
              @v_DebitNote_Code VARCHAR(20) ,
              @v_DebitNote_Date DATETIME ,
              @v_Remarks VARCHAR(500) ,
              @v_MRN_Id NUMERIC(18, 0) ,
              @v_Created_BY VARCHAR(100) ,
              @v_Creation_Date DATETIME ,
              @v_Modified_By VARCHAR(100) ,
              @v_Modification_Date DATETIME ,
              @v_Division_ID NUMERIC(18, 0) ,
              @v_DN_Amount NUMERIC(18, 2) ,
              @v_DN_CustId NUMERIC(18, 0) ,
              @v_INV_No VARCHAR(100) ,
              @v_INV_Date DATETIME ,
              @v_Proc_Type INT
            )
        AS
            BEGIN

                IF @V_PROC_TYPE = 1
                    BEGIN
                        INSERT  INTO DebitNote_MASTER
                                ( DebitNote_ID ,
                                  DebitNote_No ,
                                  DebitNote_Code ,
                                  DebitNote_Date ,
                                  Remarks ,
                                  MRNId ,
                                  Created_BY ,
                                  Creation_Date ,
                                  Modified_By ,
                                  Modification_Date ,
                                  Division_ID ,
                                  DN_Amount ,
                                  DN_CustId ,
                                  INV_No ,
                                  INV_Date
                                )
                        VALUES  ( @V_DebitNote_ID ,
                                  @V_DebitNote_No ,
                                  @V_DebitNote_Code ,
                                  @v_DebitNote_Date ,
                                  @V_Remarks ,
                                  @V_MRN_Id ,
                                  @V_Created_BY ,
                                  @V_Creation_Date ,
                                  @V_Modified_By ,
                                  @V_Modification_Date ,
                                  @V_Division_ID ,
                                  @v_DN_Amount ,
                                  @v_DN_CustId ,
                                  @v_INV_No ,
                                  @v_INV_Date
                                )
                        UPDATE  DN_SERIES
                        SET     CURRENT_USED = @V_DebitNote_No
                        WHERE   DIV_ID = @V_Division_ID



				--Credit against CreditNote No- MTC/CN/17-18/6

                        DECLARE @Remarks VARCHAR(250)

                        SET @Remarks = 'Debit  against DebitNote No-. -'
                            + @V_DebitNote_Code + ' - '
                            + CAST(@V_DebitNote_No AS VARCHAR(50))



                        EXECUTE Proc_Ledger_Insert @v_DN_CustId, 0,
                            @v_DN_Amount, @Remarks, @V_Division_ID,
                            @V_DebitNote_ID, 7, @V_Created_BY


                        EXECUTE Proc_Ledger_Insert 10070, @v_DN_Amount, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID, 7,
                            @V_Created_BY
               -- return @V_DebitNote_No
                    END
                IF @V_PROC_TYPE = 2
                    BEGIN



                        UPDATE  DebitNote_MASTER
                        SET     DebitNote_No = @V_DebitNote_No ,
                                DebitNote_Code = @V_DebitNote_Code ,
                                DebitNote_Date = @v_DebitNote_Date ,
                                Remarks = @V_Remarks ,
                                MRNId = @V_MRN_Id ,
                                Created_BY = @V_Created_BY ,
                                Creation_Date = @V_Creation_Date ,
                                Modified_By = @V_Modified_By ,
                                Modification_Date = @V_Modification_Date ,
                                DN_Amount = @v_DN_Amount ,
                                DN_CustId = @v_DN_CustId ,
                                Division_ID = @V_Division_ID ,
                                INV_No = @v_INV_No ,
                                INV_Date = @v_INV_Date
                        WHERE   DebitNote_ID = @V_DebitNote_ID
                    END

            END

--------------------------------------------------------------------------------------------------------------------------------------------------------------
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
                  @V_MODE INT



                )
            AS
                BEGIN                          

                    SET @v_NET_AMOUNT = CEILING(@v_NET_AMOUNT)

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
                                      @V_TRANSPORT ,
                                      @v_SHIPP_ADD_ID ,
                                      @v_LR_NO ,
                                      @v_INV_TYPE 
                                    ) 


                            DECLARE @Remarks VARCHAR(250)
                            SET @Remarks = 'Sale against invoice No- '
                                + @V_SI_CODE + CAST(@V_SI_NO AS VARCHAR(50))


                            EXECUTE Proc_Ledger_Insert @V_CUST_ID, 0,
                                @V_NET_AMOUNT, @Remarks, @V_Division_ID,
                                @V_SI_ID, 16, @V_Created_BY	


                            EXECUTE Proc_Ledger_Insert 10071, @V_NET_AMOUNT, 0,
                                @Remarks, @V_Division_ID, @V_SI_ID, 16,
                                @V_Created_BY	

					

                        END   



                    IF @V_MODE = 2
                        BEGIN  



                --UPDATE  STOCK_DETAIL



                --SET     STOCK_DETAIL.Issue_Qty = ( STOCK_DETAIL.Issue_Qty



                --                                   - SALE_INVOICE_STOCK_DETAIL.ITEM_QTY ) ,



                --        STOCK_DETAIL.Balance_Qty = ( STOCK_DETAIL.Balance_Qty



                --                                     + SALE_INVOICE_STOCK_DETAIL.ITEM_QTY )



                --FROM    dbo.STOCK_DETAIL



                --        JOIN dbo.SALE_INVOICE_STOCK_DETAIL ON SALE_INVOICE_STOCK_DETAIL.STOCK_DETAIL_ID = STOCK_DETAIL.STOCK_DETAIL_ID



                --                                              AND SALE_INVOICE_STOCK_DETAIL.ITEM_ID = STOCK_DETAIL.Item_id



                --WHERE   SI_ID = @V_SI_ID



		



                --DELETE  FROM dbo.SALE_INVOICE_DETAIL



                --WHERE   SI_ID = @V_SI_ID



                --DELETE  FROM dbo.SALE_INVOICE_STOCK_DETAIL



                --WHERE   SI_ID = @V_SI_ID



                --DECLARE @CashOut NUMERIC(18, 2)



                --DECLARE @CashIn NUMERIC(18, 2)



                --SET @CashOut = 0



                --SET @CashIn = ( SELECT  CashOut



                --                FROM    dbo.CustomerLedgerDetail



                --                WHERE   TransactionId = @v_SI_ID



                --                        AND TransactionTypeId = 16



                --              )



                --UPDATE  dbo.CustomerLedgerMaster



                --SET     AmountInHand = AmountInHand - @CashOut + @CashIn



                --WHERE   AccountId = @V_CUST_ID



                --DELETE  FROM dbo.CustomerLedgerDetail



                --WHERE   TransactionId = @v_SI_ID



                --        AND TransactionTypeId = 16



	



                            UPDATE  SALE_INVOICE_MASTER
                            SET     -- SI_CODE = @V_SI_CODE ,



                --        SI_NO = @V_SI_NO ,



                --        DC_GST_NO = @v_DC_NO ,



                --        SI_DATE = @V_SI_DATE ,
                                    CUST_ID = @V_CUST_ID ,
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


                            SET @Remarks = 'Sale against invoice No- '
                                + @V_SI_CODE + CAST(@V_SI_NO AS VARCHAR(50))



                            EXECUTE Proc_Ledger_Insert @V_CUST_ID, 0,
                                @V_NET_AMOUNT, @Remarks, @V_Division_ID,
                                @V_SI_ID, 16, @V_Created_BY	                   

                            EXECUTE Proc_Ledger_Insert 10071, @V_NET_AMOUNT, 0,
                                @Remarks, @V_Division_ID, @V_SI_ID, 16,
                                @V_Created_BY	

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
                                    SET     current_stock = current_stock
                                            + @ItemQty
                                    WHERE   item_id = @ItemId
                                            AND div_id = @DivisionId                        



                                    FETCH NEXT FROM UpdateStock INTO @ItemId,
                                        @ItemQty, @DivisionId                        



                                END                         



                            CLOSE UpdateStock 



                            DEALLOCATE UpdateStock   



                            DELETE  FROM sale_invoice_Detail
                            WHERE   si_id = @v_SI_ID 



                            DELETE  FROM sale_invoice_master
                            WHERE   si_id = @v_SI_ID                        



                        END  
                END
---------------------------------------------------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[PROC_CreditNote_MASTER]
    (
      @v_CreditNote_ID NUMERIC(18, 0) ,
      @v_CreditNote_No NUMERIC(18, 0) ,
      @v_CreditNote_Code VARCHAR(20) ,
      @v_CreditNote_Date DATETIME ,
      @v_Remarks VARCHAR(500) ,
      @v_INV_Id NUMERIC(18, 0) ,
      @v_Created_BY VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Division_ID NUMERIC(18, 0) ,
      @v_CN_Amount NUMERIC(18, 2) ,
      @v_CN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
      @v_Proc_Type INT

	

    )
AS
    BEGIN

        IF @V_PROC_TYPE = 1
            BEGIN

			

--                select  @v_CreditNote_No = isnull(max(CreditNote_No), 0) + 1

--                from    CreditNote_Master



                INSERT  INTO CreditNote_Master
                        ( CreditNote_ID ,
                          CreditNote_No ,
                          CreditNote_Code ,
                          CreditNote_Date ,
                          Remarks ,
                          INVId ,
                          Created_BY ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_ID ,
                          CN_Amount ,
                          CN_CustId ,
                          INV_No ,
                          INV_Date

                        )
                VALUES  ( @V_CreditNote_ID ,
                          @V_CreditNote_No ,
                          @V_CreditNote_Code ,
                          @v_CreditNote_Date ,
                          @V_Remarks ,
                          @V_INV_Id ,
                          @V_Created_BY ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_ID ,
                          @v_CN_Amount ,
                          @v_CN_CustId ,
                          @v_INV_No ,
                          @v_INV_Date

                        )

                UPDATE  CN_SERIES
                SET     CURRENT_USED = @V_CreditNote_No
                WHERE   DIV_ID = @V_Division_ID

                DECLARE @Remarks VARCHAR(250)

                SET @Remarks = 'Credit against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))

                EXECUTE Proc_Ledger_Insert @v_CN_CustId, @v_CN_Amount, 0,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, 17,
                    @V_Created_BY


					 EXECUTE Proc_Ledger_Insert 10071,  0,@v_CN_Amount,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, 17,
                    @V_Created_BY

            END

        IF @V_PROC_TYPE = 2
            BEGIN

                UPDATE  dbo.CreditNote_Master
                SET     CreditNote_No = @V_CreditNote_No ,
                        CreditNote_Code = @V_CreditNote_Code ,
                        CreditNote_Date = @v_CreditNote_Date ,
                        Remarks = @V_Remarks ,
                        INVId = @V_INV_Id ,
                        Created_BY = @V_Created_BY ,
                        Creation_Date = @V_Creation_Date ,
                        Modified_By = @V_Modified_By ,
                        Modification_Date = @V_Modification_Date ,
                        CN_Amount = @v_CN_Amount ,
                        CN_CustId = @v_CN_CustId ,
                        Division_ID = @V_Division_ID ,
                        INV_No = @v_INV_No ,
                        INV_Date = @v_INV_Date
                WHERE   CreditNote_ID = @V_CreditNote_ID

            END

    END
-------------------------------------------------------------------------------------------------------------------------------------------------------

ALTER PROC [dbo].[ProcReverseInvoiceEntry]
    (
      @V_SI_ID NUMERIC(18, 0) ,
      @V_CUST_ID NUMERIC(18, 0)
    )
AS
    BEGIN

  	

        UPDATE  STOCK_DETAIL
        SET     STOCK_DETAIL.Issue_Qty = ( STOCK_DETAIL.Issue_Qty
                                           - SALE_INVOICE_STOCK_DETAIL.ITEM_QTY ) ,
                STOCK_DETAIL.Balance_Qty = ( STOCK_DETAIL.Balance_Qty
                                             + SALE_INVOICE_STOCK_DETAIL.ITEM_QTY )
        FROM    dbo.STOCK_DETAIL
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL ON SALE_INVOICE_STOCK_DETAIL.STOCK_DETAIL_ID = STOCK_DETAIL.STOCK_DETAIL_ID
                                                      AND SALE_INVOICE_STOCK_DETAIL.ITEM_ID = STOCK_DETAIL.Item_id
        WHERE   SI_ID = @V_SI_ID

		

        DELETE  FROM dbo.SALE_INVOICE_DETAIL
        WHERE   SI_ID = @V_SI_ID

        DELETE  FROM dbo.SALE_INVOICE_STOCK_DETAIL
        WHERE   SI_ID = @V_SI_ID

        DECLARE @CashOut NUMERIC(18, 2)

        DECLARE @CashIn NUMERIC(18, 2)

        SET @CashOut = 0

        SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
                        WHERE   TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                      )

        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @V_CUST_ID


        SET @CashOut = ( SELECT ISNULL(SUM(CashOut), 0)
                         FROM   dbo.LedgerDetail
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
                       )

        SET @CashIn = 0

        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10071


        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @v_SI_ID
                AND TransactionTypeId = 16

    END

------------------------------------------------------------------------------------------------------------------------------------------
ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER 
ADD REFERENCE_ID NUMERIC DEFAULT 10070 NOT NULL

ALTER TABLE dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER 
ADD REFERENCE_ID NUMERIC DEFAULT 10070 NOT NULL

ALTER TABLE dbo.DebitNote_Master 
ADD REFERENCE_ID NUMERIC DEFAULT 10070 NOT NULL


ALTER TABLE dbo.SALE_INVOICE_MASTER 
ADD REFERENCE_ID NUMERIC DEFAULT 10071 NOT NULL



ALTER TABLE dbo.CreditNote_Master 
ADD REFERENCE_ID NUMERIC DEFAULT 10071 NOT NULL