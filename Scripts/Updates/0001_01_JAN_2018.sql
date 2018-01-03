ALTER PROCEDURE [dbo].[PROC_ITEM_DETAIL]
    (
      @v_ITEM_ID DECIMAL ,
      @v_DIV_ID INT ,
      @v_RE_ORDER_LEVEL DECIMAL ,
      @v_RE_ORDER_QTY DECIMAL ,
      @v_PURCHASE_VAT_ID INT ,
      @v_SALE_VAT_ID INT ,
      @v_OPENING_STOCK NUMERIC(18, 3) ,
      @v_CURRENT_STOCK DECIMAL ,
      @v_IS_EXTERNAL INT ,
      @v_TRANSFER_RATE DECIMAL ,
      @v_OPENING_RATE NUMERIC(18, 2) ,
      @v_AVERAGE_RATE DECIMAL ,
      @v_IS_STOCKABLE BIT ,
      @v_IS_ACTIVE BIT ,
      @v_Proc_Type INT ,
      @v_Batch_no VARCHAR(50) ,
      @v_Expiry_Date DATETIME ,
      @V_Trans_Type INT 

    )
AS
    BEGIN 

        DECLARE @intErrorCode INT    

        IF @V_PROC_TYPE = 1
            BEGIN            



                BEGIN TRAN  
                DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)

                --it will insert entry in stock detail table and 
                --return stock_detail_id.                        



                EXEC INSERT_STOCK_DETAIL @v_Item_ID, @v_Batch_no,
                    @v_Expiry_Date, @v_OPENING_STOCK, 0, @V_Trans_Type,
                    @STOCK_DETAIL_ID OUTPUT


                DECLARE @v_creation_date DATETIME
                SELECT  @v_creation_date = GETDATE()





                EXEC INSERT_TRANSACTION_LOG 0, @v_Item_ID, @V_Trans_Type,
                    @STOCK_DETAIL_ID, @V_OPENING_STOCK, @v_creation_date, 0                 

                INSERT  INTO ITEM_DETAIL
                        ( ITEM_ID ,
                          DIV_ID ,
                          RE_ORDER_LEVEL ,
                          RE_ORDER_QTY ,
                          PURCHASE_VAT_ID ,
                          SALE_VAT_ID ,
                          OPENING_STOCK ,
                          CURRENT_STOCK ,
                          IS_EXTERNAL ,
                          TRANSFER_RATE ,
                          AVERAGE_RATE ,
                          OPENING_RATE ,
                          IS_STOCKABLE ,
                          IS_ACTIVE ,
                          STOCK_DETAIL_ID
                        )
                VALUES  ( @V_ITEM_ID ,
                          @V_DIV_ID ,
                          @V_RE_ORDER_LEVEL ,
                          @V_RE_ORDER_QTY ,
                          @V_PURCHASE_VAT_ID ,
                          @V_SALE_VAT_ID ,
                          @V_OPENING_STOCK ,
                          @V_CURRENT_STOCK ,
                          @V_IS_EXTERNAL ,
                          @V_TRANSFER_RATE ,
                          @V_AVERAGE_RATE ,
                          @v_OPENING_RATE ,
                          @V_IS_STOCKABLE ,
                          @v_IS_ACTIVE ,
                          @STOCK_DETAIL_ID
                        )              



                DECLARE @Remarks NVARCHAR(250)
                DECLARE @V_NET_AMOUNT NUMERIC(18, 2)    

                SET @Remarks = 'Stock In against Opening Item Id- '
                    + CAST(@V_ITEM_ID AS VARCHAR(50))

                SET @V_NET_AMOUNT = @v_OPENING_STOCK * @v_OPENING_RATE



                EXECUTE Proc_Ledger_Insert 10073, @V_NET_AMOUNT, 0, @Remarks,
                    @v_DIV_ID, @V_ITEM_ID, 10, 'user'

                SELECT  @intErrorCode = @@ERROR            



                IF ( @intErrorCode <> 0 )
                    GOTO PROBLEM  

                COMMIT TRAN
                PROBLEM: 

                IF ( @intErrorCode <> 0 )
                    BEGIN            



                        PRINT 'Unexpected error occurred!'            



                        ROLLBACK TRAN            



                    END    
            END            


        IF @V_PROC_TYPE = 2
            BEGIN              



                UPDATE  ITEM_DETAIL
                SET     RE_ORDER_LEVEL = @V_RE_ORDER_LEVEL ,
                        RE_ORDER_QTY = @V_RE_ORDER_QTY ,
                        PURCHASE_VAT_ID = @V_PURCHASE_VAT_ID ,
                        SALE_VAT_ID = @V_SALE_VAT_ID ,
                        OPENING_STOCK = @V_OPENING_STOCK ,
                        IS_EXTERNAL = @V_IS_EXTERNAL ,
                        TRANSFER_RATE = @V_TRANSFER_RATE ,
                        AVERAGE_RATE = @V_AVERAGE_RATE ,
                        OPENING_RATE = @v_OPENING_RATE ,
                        IS_STOCKABLE = @V_IS_STOCKABLE ,
                        IS_ACTIVE = @v_IS_ACTIVE
                WHERE   ITEM_ID = @V_ITEM_ID
                        AND DIV_ID = @V_DIV_ID              



                DECLARE @CashOut NUMERIC(18, 2)
                DECLARE @CashIn NUMERIC(18, 2)
                SET @CashIn = 0 
                SET @CashOut = ( SELECT ISNULL(SUM(CashIn), 0)
                                 FROM   dbo.LedgerDetail
                                        JOIN dbo.LedgerMaster ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                 WHERE  TransactionId = @V_ITEM_ID
                                        AND TransactionTypeId = 10
                                        AND AccountId = 10073
                               )
                UPDATE  dbo.LedgerMaster
                SET     AmountInHand = AmountInHand - @CashOut + @CashIn
                WHERE   AccountId = 10073
          



                DELETE  FROM dbo.LedgerDetail
                WHERE   TransactionId = @V_ITEM_ID
                        AND TransactionTypeId = 10

			                    
                SET @Remarks = 'Stock In against Opening Item Id- '
                    + CAST(@V_ITEM_ID AS VARCHAR(50))
                SET @V_NET_AMOUNT = @v_OPENING_STOCK * @v_OPENING_RATE

                EXECUTE Proc_Ledger_Insert 10073, @V_NET_AMOUNT, 0, @Remarks,
                    @v_DIV_ID, @V_ITEM_ID, 10, 'user'	 



         



            END   


        UPDATE  dbo.STOCK_DETAIL
        SET     Item_Qty = @V_OPENING_STOCK ,
                Balance_Qty = @V_OPENING_STOCK - Issue_Qty
        WHERE   STOCK_DETAIL_ID = ( SELECT  stock_detail_id
                                    FROM    dbo.ITEM_DETAIL
                                    WHERE   dbo.ITEM_DETAIL.ITEM_ID = @V_ITEM_ID
                                            AND item_detail.DIV_ID = @V_DIV_ID
                                  )
    END

-----------------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[PROC_ADJUSTMENT_DETAIL]
    (
      @v_adjustment_ID DECIMAL ,
      @v_Item_ID DECIMAL ,
      @v_Stock_Detail_Id DECIMAL ,
      @v_Item_Qty NUMERIC(18, 3) ,
      @v_Balance_Qty NUMERIC(18, 3) ,
      @v_Created_By NVARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By NVARCHAR(100) ,
      @v_Modified_Date DATETIME ,
      @v_Division_ID INT ,
      @v_Proc_Type INT ,
      @v_Item_Rate NUMERIC(18, 2)
    )
AS
    BEGIN            

    

        IF @V_PROC_TYPE = 1
            BEGIN            

                INSERT  INTO ADJUSTMENT_DETAIL
                        ( adjustment_ID ,
                          Item_ID ,
                          Stock_Detail_Id ,
                          Item_Qty ,
                          Item_Rate ,
                          Balance_Qty ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modified_Date ,
                          Division_ID        

                        )
                VALUES  ( @V_adjustment_ID ,
                          @V_Item_ID ,
                          @v_Stock_Detail_Id ,
                          @V_Item_Qty ,
                          @v_Item_Rate ,
                          @v_Balance_Qty ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modified_Date ,
                          @V_Division_ID        

                        )   
						
					   
					    

                IF ( @V_Item_Qty > 0 )
                    BEGIN

                        INSERT  INTO dbo.TransactionLog_Master
                                ( Item_id ,
                                  Issue_Qty ,
                                  Receive_Qty ,
                                  Transaction_Date ,
                                  TransactionType_Id ,
                                  Transaction_Id ,
                                  Division_Id

					            )
                        VALUES  ( @V_Item_ID ,
                                  0 ,
                                  ABS(@V_Item_Qty) ,
                                  @v_Creation_Date ,
                                  1 ,
                                  @V_adjustment_ID ,
                                  @v_Division_ID

                                )	

                    END

                ELSE
                    IF ( @V_Item_Qty < 0 )
                        BEGIN

                            INSERT  INTO dbo.TransactionLog_Master
                                    ( Item_id ,
                                      Issue_Qty ,
                                      Receive_Qty ,
                                      Transaction_Date ,
                                      TransactionType_Id ,
                                      Transaction_Id ,
                                      Division_Id

					                )
                            VALUES  ( @V_Item_ID ,
                                      ABS(@V_Item_Qty) ,
                                      0 ,
                                      @v_Creation_Date ,
                                      1 ,
                                      @V_adjustment_ID ,
                                      @v_Division_ID

                                    )	

                        END

            END 
			
			IF @V_PROC_TYPE = 2
            BEGIN  
			DECLARE @Remarks NVARCHAR(250)

			DECLARE @amount NUMERIC(18,2)
			SET @amount=(SELECT SUM(Item_Qty*Item_Rate)   FROM dbo.ADJUSTMENT_DETAIL WHERE Adjustment_ID=@V_adjustment_ID)

                          SET @Remarks = 'Stock IN against adjustment No- ' + CAST(@V_adjustment_ID AS VARCHAR(50))
					    IF @amount > 0
                    BEGIN
                        EXECUTE Proc_Ledger_Insert 10073, @amount, 0,
                            @Remarks, @V_Division_ID, @V_adjustment_ID, 11,
                            @V_Created_BY
                    END
                ELSE
                    BEGIN
					 SET @Remarks = 'Stock Out against adjustment No- ' + CAST(@V_adjustment_ID AS VARCHAR(50))
                        SET @V_Item_Qty = -+@amount
                        EXECUTE Proc_Ledger_Insert 10073, 0, @V_Item_Qty,
                            @Remarks, @V_Division_ID, @V_adjustment_ID, 11,
                            @V_Created_BY
                    END 
			END
    END
---------------------------------------------------------------------------------------------------------------------------------------------------------------
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
                DECLARE @InputID NUMERIC
                DECLARE @CInputID NUMERIC
                SET @CInputID = 10016
                DECLARE @RoundOff NUMERIC(18, 2)
                DECLARE @CGST_Amount NUMERIC(18, 2)
                SET @CGST_Amount = ( @v_GST_AMOUNT / 2 )
                SET @RoundOff = ( @v_freight + @v_Other_Charges )

                SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10020
                                             WHEN @V_MRN_TYPE = 2 THEN 10023
                                             WHEN @V_MRN_TYPE = 3 THEN 10074
                                        END AS inputid
                               )




                SET @Remarks = 'Pruchase against party bill no.: '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106) 

                EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3, @V_Created_BY    

                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Receipt_ID, 3, @V_Created_BY



                SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106) 

                IF @V_MRN_TYPE <> 2
                    BEGIN



                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @V_Created_BY


                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @v_Division_ID, @v_Receipt_ID, 3,
                            @V_Created_BY
                    END



                ELSE
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @V_Created_BY
                    END   


                SET @Remarks = 'Round Off against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)

                IF @RoundOff > 0
                    BEGIN

                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @V_Created_BY

                    END

                ELSE
                    BEGIN

                        SET @RoundOff = -+@RoundOff

                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,
                            @V_Created_BY

                    END 


					 SET @Remarks = 'Stock In against party  invoice No- '  + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)



                EXECUTE Proc_Ledger_Insert 10073, @v_NET_AMOUNT,0,  @Remarks,
                    @V_Division_ID, @v_Receipt_ID, 3, @V_Created_BY


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


        --IF @V_PROC_TYPE = 3
        --    BEGIN                

        --        DECLARE cur CURSOR
        --        FOR
        --            SELECT  Item_ID ,
        --                    Item_Qty ,
        --                    Indent_ID
        --            FROM    MATERIAL_RECEIVED_AGAINST_PO_DETAIL
        --            WHERE   Receipt_ID = @V_Receipt_ID                

                

        --        DECLARE @itemid NUMERIC(18, 0)                

        --        DECLARE @itemQty NUMERIC(18, 4)                

        --        DECLARE @IndentID NUMERIC(18, 0)                

                

                

        --        OPEN cur                

        --        FETCH NEXT FROM cur INTO @itemid, @itemQty, @IndentID                

        --        WHILE @@fetch_status = 0
        --            BEGIN                

                   

        --                UPDATE  PO_STATUS
        --                SET     RECIEVED_QTY = RECIEVED_QTY - @itemQty ,
        --                        BALANCE_QTY = BALANCE_QTY + @itemQty
        --                WHERE   PO_ID = @V_PO_ID
        --                        AND ITEM_ID = @itemid
        --                        AND INDENT_ID = @IndentID                

                

        --                UPDATE  ITEM_DETAIL
        --                SET     CURRENT_STOCK = CURRENT_STOCK - @itemQty
        --                WHERE   ITEM_ID = @itemid
        --                        AND DIV_ID = @V_Division_ID

        --                FETCH NEXT FROM cur INTO @itemid, @itemQty, @IndentID
        --            END
        --        CLOSE cur
        --        DEALLOCATE cur

        --        DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_DETAIL
        --        WHERE   Receipt_ID = @V_Receipt_ID
 
        --        DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_MASTER
        --        WHERE   Receipt_ID = @V_Receipt_ID 
        --    END  
   
    END 

-------------------------------------------------------------------------------------------------------------------------------------------

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
                DECLARE @InputID NUMERIC
                DECLARE @CInputID NUMERIC
                SET @CInputID = 10016
                DECLARE @RoundOff NUMERIC(18, 2)
                DECLARE @CGST_Amount NUMERIC(18, 2)
                SET @CGST_Amount = ( @v_GST_AMOUNT / 2 )
                SET @RoundOff = ( @v_freight + @v_Other_Charges )

                SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10020
                                             WHEN @V_MRN_TYPE = 2 THEN 10023
                                             WHEN @V_MRN_TYPE = 3 THEN 10074
                                        END AS inputid
                               )




                SET @Remarks = 'Pruchase against party bill no.: '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106) 


                EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT, 0,
                    @Remarks, @V_Division_ID, @V_Received_ID, 2, @V_Created_BY  



                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @V_Created_BY  


					 SET @Remarks = 'GST against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106) 

                IF @V_MRN_TYPE <> 2
                    BEGIN



                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY


                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,
                            @Remarks, @v_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY
                    END



                ELSE
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY
                    END   


                SET @Remarks = 'Round Off against party invoice No- '
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)

                IF @RoundOff > 0
                    BEGIN

                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY

                    END

                ELSE
                    BEGIN

                        SET @RoundOff = -+@RoundOff

                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,
                            @Remarks, @V_Division_ID, @V_Received_ID, 2,
                            @V_Created_BY

                    END 


					 SET @Remarks = 'Stock In against party  invoice No- '  + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)



                EXECUTE Proc_Ledger_Insert 10073, @v_NET_AMOUNT,0,  @Remarks,
                    @V_Division_ID, @V_Received_ID, 2, @V_Created_BY


            END   

    END 

-------------------------------------------------------------------------------------------------------------------------------------------------------------

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
      @v_DN_ItemValue NUMERIC(18, 2) = 0 ,
      @v_DN_ItemTax NUMERIC(18, 2) = 0 ,
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
                DECLARE @InputID NUMERIC
                DECLARE @CInputID NUMERIC= 0
                SET @CInputID = 10016
                DECLARE @RoundOff NUMERIC(18, 2)
                DECLARE @CGST_Amount NUMERIC(18, 2)
                SET @CGST_Amount = ( @v_DN_ItemTax / 2 )
               
                SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1
                                                     THEN 10020
                                                     WHEN MRN_TYPE = 2
                                                     THEN 10023
                                                     WHEN MRN_TYPE = 3
                                                     THEN 10074
                                                END AS inputid
                                        FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                                        WHERE   MRN_NO = @V_MRN_Id
                                      ), 0)
                IF @InputID = 0
                    BEGIN
                        SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1
                                                             THEN 10020
                                                             WHEN MRN_TYPE = 2
                                                             THEN 10023
                                                             WHEN MRN_TYPE = 3
                                                             THEN 10074
                                                        END AS inputid
                                                FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
                                                WHERE   MRN_NO = @V_MRN_Id
                                              ), 0)
                    END


                DECLARE @V_MRN_TYPE NUMERIC= 0

                SET @V_MRN_TYPE = ( SELECT  MRN_TYPE
                                    FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                                    WHERE   MRN_NO = @V_MRN_Id
                                    UNION
                                    SELECT  MRN_TYPE
                                    FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
                                    WHERE   MRN_NO = @V_MRN_Id
                                  )

                SET @Remarks = 'Debit  against DebitNote No-. -'
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))


                EXECUTE Proc_Ledger_Insert @v_DN_CustId, 0, @v_DN_Amount,
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, 7,
                    @V_Created_BY


                EXECUTE Proc_Ledger_Insert 10070, @v_DN_ItemValue, 0, @Remarks,
                    @V_Division_ID, @V_DebitNote_ID, 7, @V_Created_BY



                SET @Remarks = 'GST against DebitNote No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))

                IF @V_MRN_TYPE <> 2
                    BEGIN



                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID, 7,
                            @V_Created_BY


                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,
                            @Remarks, @v_Division_ID, @V_DebitNote_ID, 7,
                            @V_Created_BY
                    END



                ELSE
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @InputID, @v_DN_ItemTax, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID, 7,
                            @V_Created_BY
                    END   


                SET @Remarks = 'Stock Out against Debit Note No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))



                EXECUTE Proc_Ledger_Insert 10073, 0, @v_DN_Amount, @Remarks,
                    @V_Division_ID, @V_DebitNote_ID, 7, @V_Created_BY

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

-------------------------------------------------------------------------------------------------------------------------------------
