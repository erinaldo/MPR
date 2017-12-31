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
      @V_MODE INT ,
      @V_Flag INT = 0



    )
AS
    BEGIN       

      

       

       

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
                          INV_TYPE ,
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
                          @v_INV_TYPE ,
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

------------------------------------------------------------------------------------------------------------------------------------------------

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
						JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
								AND AccountId=@V_CUST_ID
                      )

        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = @V_CUST_ID



		 SET @CashIn = ( SELECT  ISNULL(SUM(CashOut), 0)
                        FROM    dbo.LedgerDetail
						JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                        WHERE   TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
								AND AccountId=10073
                      )

		UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10073


        SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
						 JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
								AND AccountId=10071
                       )

        SET @CashIn = 0

        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10071




		 SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
						 JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
								AND AccountId=10054
                       )
        UPDATE  dbo.LedgerMaster
        SET     AmountInHand = AmountInHand - @CashOut + @CashIn
        WHERE   AccountId = 10054
	   
	   
        DECLARE @v_INV_TYPE VARCHAR(1)
        SET @v_INV_TYPE = ( SELECT  INV_TYPE
                            FROM    dbo.SALE_INVOICE_MASTER
                            WHERE   SI_ID = @v_SI_ID
                          )

        DECLARE @CInputID NUMERIC
        SET @CInputID = 10017
        DECLARE @InputID NUMERIC
        DECLARE @CGST_Amount NUMERIC(18, 2)
		


        SET @InputID = ( SELECT CASE WHEN @v_INV_TYPE = 'I' THEN 10021
                                     WHEN @v_INV_TYPE = 'S' THEN 10024
                                     WHEN @v_INV_TYPE = 'U' THEN 10075
                                END AS inputid
                       )

        IF @v_INV_TYPE <> 'I'
            BEGIN

			SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
						 JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
								AND AccountId=@CInputID
                       )
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @CInputID


				SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
						 JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
								AND AccountId=@InputID
                       )
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @InputID
            END
        ELSE
            BEGIN

			SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                         FROM   dbo.LedgerDetail
						 JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                         WHERE  TransactionId = @v_SI_ID
                                AND TransactionTypeId = 16
								AND AccountId=@InputID)
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = @InputID
            END 



        DELETE  FROM dbo.LedgerDetail
        WHERE   TransactionId = @v_SI_ID
                AND TransactionTypeId = 16
    END

-----------------------------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE PROC_CreditNote_MASTER
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
      @v_CN_ItemValue NUMERIC(18, 2)=0 ,
      @v_CN_ItemTax NUMERIC(18, 2)=0 ,
      @v_CN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
      @v_Proc_Type INT
    )
AS
    BEGIN

        IF @V_PROC_TYPE = 1
            BEGIN

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

                EXECUTE Proc_Ledger_Insert 10071, 0, @v_CN_ItemValue, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, 17, @V_Created_BY


		DECLARE @InputID NUMERIC
        DECLARE @CInputID NUMERIC
        SET @CInputID = 10017

      
        DECLARE @CGST_Amount NUMERIC(18, 2)
        SET @CGST_Amount = ( @v_CN_ItemTax / 2 )

       


        SET @InputID = ( SELECT CASE WHEN INV_TYPE = 'I' THEN 10021
                                     WHEN INV_TYPE = 'S' THEN 10024
                                     WHEN INV_TYPE = 'U' THEN 10075
                                END AS inputid
								FROM dbo.SALE_INVOICE_MASTER WHERE SI_ID=@v_INV_Id
                       )

DECLARE @v_INV_TYPE VARCHAR(1)
SET @v_INV_TYPE=(SELECT INV_TYPE FROM dbo.SALE_INVOICE_MASTER WHERE SI_ID=@v_INV_Id)

  SET @Remarks = 'GST against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))

                IF @v_INV_TYPE <> 'I'
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @CInputID,0, @CGST_Amount, 
                            @Remarks, @V_Division_ID, @V_CreditNote_ID, 17,
                            @V_Created_BY

                        EXECUTE Proc_Ledger_Insert @InputID,0, @CGST_Amount, 
                            @Remarks, @V_Division_ID, @V_CreditNote_ID, 17,
                            @V_Created_BY
                    END
                ELSE
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @InputID,0, @v_CN_ItemTax, 
                            @Remarks, @V_Division_ID, @V_CreditNote_ID, 17,
                            @V_Created_BY
                    END
            END

				SET @Remarks = 'Stock In against CreditNote No- '  + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))

                EXECUTE Proc_Ledger_Insert 10073, @v_CN_Amount,0,  @Remarks,
                   @V_Division_ID, @V_CreditNote_ID, 17, @V_Created_BY



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

