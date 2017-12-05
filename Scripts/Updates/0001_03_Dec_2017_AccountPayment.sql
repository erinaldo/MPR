ALTER TABLE dbo.PM_Series ADD PM_TYPE numeric(18,0)  NULL DEFAULT 0
ALTER TABLE dbo.PaymentTransaction ADD PM_TYPE numeric(18,0)  NULL DEFAULT 0
---------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[GET_PaymentModule_No]
(
	@DIV_ID NUMERIC(18,0),
	@PM_TYPE  numeric(18,0)
)
AS
BEGIN
	DECLARE @COUNT NUMERIC(18,0)
		SELECT @COUNT = COUNT(CURRENT_USED) FROM PM_SERIES WHERE 
		DIV_ID = @DIV_ID and IS_FINISHED = 'N' AND @PM_TYPE=PM_TYPE
	
	IF @COUNT = 0 
		BEGIN
			SELECT '-1','-1'
		END
	ELSE
		BEGIN
		
		SELECT @COUNT = COUNT(CURRENT_USED) FROM PM_SERIES WHERE IS_FINISHED = 'N'
		AND
		DIV_ID = @DIV_ID AND CURRENT_USED >= START_NO - 1 
		AND CURRENT_USED < END_NO AND @PM_TYPE=PM_TYPE
	
		if @count = 0 
			begin
				select '-2','-2'
			end 
		else
			begin
				SELECT  PREFIX,CURRENT_USED FROM PM_SERIES WHERE 
					DIV_ID = @DIV_ID AND IS_FINISHED ='N' AND @PM_TYPE=PM_TYPE
			END
		end 
END

---------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[ProcPaymentTransaction_Insert]
    (
      @PaymentTransactionId NUMERIC = NULL ,
      @PaymentTransactionNo VARCHAR(100) = NULL ,
      @PaymentTypeId NUMERIC = NULL ,
      @AccountId NUMERIC = NULL ,
      @PaymentDate DATETIME = NULL ,
      @ChequeDraftNo VARCHAR(50) = NULL ,
      @ChequeDraftDate DATETIME = NULL ,
      @BankId NUMERIC = 0 ,
      @BankDate DATETIME = NULL ,
      @Remarks VARCHAR(200) = NULL ,
      @TotalamountReceived NUMERIC(18, 2) = 0 ,
      @UndistributedAmount NUMERIC(18, 2) = 0 ,
      @CreatedBy VARCHAR(50) = NULL ,
      @DivisionId NUMERIC = 0 ,
      @StatusId NUMERIC = NULL ,
	  @PM_TYPE  numeric(18,0)=NULL,
      @ProcedureStatus INT OUTPUT       
    )
AS
    BEGIN      
        DECLARE @LedgerId NUMERIC     
        SELECT  @PaymentTransactionId = ISNULL(MAX(PaymentTransactionId), 0)
                + 1
        FROM    dbo.PaymentTransaction          
        INSERT  INTO PaymentTransaction
                ( PaymentTransactionId ,
                  PaymentTransactionNo ,
                  PaymentTypeId ,
                  AccountId ,
                  PaymentDate ,
                  ChequeDraftNo ,
                  ChequeDraftDate ,
                  BankId ,
                  Remarks ,
                  TotalAmountReceived ,
                  UndistributedAmount ,
                  CreatedBy ,
                  CreatedDate ,
                  DivisionId ,
                  StatusId ,
                  BankDate,
				  PM_TYPE      
                )
        VALUES  ( @PaymentTransactionId ,
                  @PaymentTransactionNo ,
                  @PaymentTypeId ,
                  @AccountId ,
                  @PaymentDate ,
                  @ChequeDraftNo ,
                  @ChequeDraftDate ,
                  @BankId ,
                  @Remarks ,
                  @TotalamountReceived ,
                  @UndistributedAmount ,
                  @CreatedBy ,
                  GETDATE() ,
                  @DivisionId ,
                  @StatusId ,
                  @BankDate,
				  @PM_TYPE      
                )      
        SET @ProcedureStatus = @PaymentTransactionId       
        ---increment series
        UPDATE  PM_SERIES
        SET     CURRENT_USED = CURRENT_USED + 1
        WHERE   DIV_ID = @DivisionId
                AND IS_FINISHED = 'N' AND PM_TYPE=@PM_TYPE
        ---update customer ledger        
		---if payment status approved   than make entry in customer ledger      
        SET @Remarks = 'Payment Recieved against ' + @PaymentTransactionNo
        IF @StatusId = 2
            EXECUTE Proc_Ledger_Insert @AccountId,
                @TotalamountReceived, 0, @Remarks, @DivisionId,
                @PaymentTransactionId, 18, @CreatedBy
    END 

---------------------------------------------------------------------------------------------------------
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

                EXECUTE Proc_Ledger_Insert @v_DN_CustId, 0, @v_DN_Amount,
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
--------------------------------------------------------------------------------------------------------------
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
                          IsPrinted        

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
                          0          

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

-----------------------------------------------------------------------------------------------------------------------------

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



                SET @Remarks = 'Pruchase  WPO party invoice no. -'
                    + @v_Invoice_No + ' - '
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)

                EXECUTE Proc_Ledger_Insert @v_Vendor_ID, @V_NET_AMOUNT,
                    0, @Remarks, @V_Division_ID, @V_Received_ID, 2,
                    @V_Created_BY

				          

            END 

    END

-------------------------------------------------------------------------------------------------------------------------------------------------
ALTER PROC [dbo].[Proc_UpdatePaymentStauts]
    (
      @PaymentTransactionId [numeric](18, 0) ,
      @CancellationCharges [numeric](18, 2) ,
      @StatusId NUMERIC ,
      @PM_TYPE NUMERIC(18, 0)
    )
AS
    BEGIN  



        UPDATE  PaymentTransaction
        SET     StatusId = @StatusId ,
                CancellationCharges = @CancellationCharges
        WHERE   PaymentTransactionId = @PaymentTransactionId  

        ----declare parameters

        DECLARE @AccountId NUMERIC
        DECLARE @TotalamountReceived NUMERIC(18, 2) = 0 
        DECLARE @PaymentTransactionNo VARCHAR(100) 
        DECLARE @CreatedBy VARCHAR(50) = NULL 
        DECLARE @DivisionId NUMERIC = 0 

        ----set parametrs

        SELECT  @AccountId = AccountId ,
                @TotalamountReceived = TotalAmountReceived ,
                @PaymentTransactionNo = PaymentTransactionNo ,
                @CreatedBy = CreatedBy ,
                @DivisionId = DivisionId
        FROM    dbo.PaymentTransaction
        WHERE   PaymentTransactionId = @PaymentTransactionId


        ---update customer ledger
		---if payment status approved   than make entry in customer ledger



        DECLARE @Remarks VARCHAR(200) 
		



        IF @StatusId = 2
            BEGIN
                IF @PM_TYPE = 1
                    BEGIN
					SET @Remarks= 'Payment Recieved against ' + @PaymentTransactionNo
                        EXECUTE Proc_Ledger_Insert @AccountId,
                            @TotalamountReceived, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 18, @CreatedBy

                    END
                IF @PM_TYPE = 2
                    BEGIN
					
       
		             SET @Remarks= 'Payment release against ' + @PaymentTransactionNo
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @TotalamountReceived, @Remarks, @DivisionId,
                            @PaymentTransactionId, 18, @CreatedBy

                    END

            END



        ---if payment status bounced with cancellation charges than make entry in customer ledger      



        SET @Remarks = 'Payment Cancelation Charges against '
            + @PaymentTransactionNo



        IF @StatusId = 4
            AND @CancellationCharges > 0
            BEGIN
                IF @PM_TYPE = 1
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @CancellationCharges, @Remarks, @DivisionId,
                            @PaymentTransactionId, 18, @CreatedBy
                    END

                IF @PM_TYPE = 2
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @AccountId,
                            @CancellationCharges, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 18, @CreatedBy
                    END 
            END
    END
---------------------------------------------------------------------------------------------------------------------
ALTER PROC [dbo].[Proc_SupplierUpdatePaymentStauts]
    (
      @PaymentTransactionId [numeric](18, 0) ,
      @CancellationCharges [numeric](18, 2) ,
      @StatusId NUMERIC    

    )
AS
    BEGIN  

        UPDATE  SupplierPaymentTransaction
        SET     StatusId = @StatusId ,
                CancellationCharges = @CancellationCharges
        WHERE   PaymentTransactionId = @PaymentTransactionId  

        

        ----declare parameters        

        DECLARE @AccountId NUMERIC

        DECLARE @TotalamountReceived NUMERIC(18, 2) = 0 

        DECLARE @PaymentTransactionNo VARCHAR(100) 

        DECLARE @CreatedBy VARCHAR(50) = NULL 

        DECLARE @DivisionId NUMERIC = 0 

        

        ----set parametrs

        SELECT  @AccountId = AccountId ,
                @TotalamountReceived = TotalAmountReceived ,
                @PaymentTransactionNo = PaymentTransactionNo ,
                @CreatedBy = CreatedBy ,
                @DivisionId = DivisionId
        FROM    dbo.SupplierPaymentTransaction
        WHERE   PaymentTransactionId = @PaymentTransactionId

        

        ---update customer ledger

		---if payment status approved   than make entry in customer ledger

        DECLARE @Remarks VARCHAR(200) = 'Payment release against '
            + @PaymentTransactionNo

        IF @StatusId = 2
            EXECUTE Proc_Ledger_Insert @AccountId, 0,
                @TotalamountReceived, @Remarks, @DivisionId,
                @PaymentTransactionId, 18, @CreatedBy

        

        ---if payment status bounced with cancellation charges than make entry in customer ledger      

        SET @Remarks = 'Payment Cancelation Charges against '
            + @PaymentTransactionNo

        IF @StatusId = 4
            AND @CancellationCharges > 0
            EXECUTE Proc_Ledger_Insert @AccountId,
                @CancellationCharges, 0, @Remarks, @DivisionId,
                @PaymentTransactionId, 18, @CreatedBy

                

    END
---------------------------------------------------------------------------------------------------------------------------------------
ALTER	 PROCEDURE [dbo].[ProcSupplierPaymentTransaction_Insert]
    (
      @PaymentTransactionId NUMERIC = NULL ,
      @PaymentTransactionNo VARCHAR(100) = NULL ,
      @PaymentTypeId NUMERIC = NULL ,
      @AccountId NUMERIC = NULL ,
      @PaymentDate DATETIME = NULL ,
      @ChequeDraftNo VARCHAR(50) = NULL ,
      @ChequeDraftDate DATETIME = NULL ,
      @BankId NUMERIC = 0 ,
      @BankDate DATETIME = NULL ,
      @Remarks VARCHAR(200) = NULL ,
      @TotalamountReceived NUMERIC(18, 2) = 0 ,
      @UndistributedAmount NUMERIC(18, 2) = 0 ,
      @CreatedBy VARCHAR(50) = NULL ,
      @DivisionId NUMERIC = 0 ,
      @StatusId NUMERIC = NULL ,
      @ProcedureStatus INT OUTPUT       

    )
AS
    BEGIN      

        DECLARE @LedgerId NUMERIC     

        SELECT  @PaymentTransactionId = ISNULL(MAX(PaymentTransactionId), 0)
                + 1
        FROM    dbo.SupplierPaymentTransaction          

        INSERT  INTO SupplierPaymentTransaction
                ( PaymentTransactionId ,
                  PaymentTransactionNo ,
                  PaymentTypeId ,
                  AccountId ,
                  PaymentDate ,
                  ChequeDraftNo ,
                  ChequeDraftDate ,
                  BankId ,
                  Remarks ,
                  TotalAmountReceived ,
                  UndistributedAmount ,
                  CreatedBy ,
                  CreatedDate ,
                  DivisionId ,
                  StatusId ,
                  BankDate      

                )
        VALUES  ( @PaymentTransactionId ,
                  @PaymentTransactionNo ,
                  @PaymentTypeId ,
                  @AccountId ,
                  @PaymentDate ,
                  @ChequeDraftNo ,
                  @ChequeDraftDate ,
                  @BankId ,
                  @Remarks ,
                  @TotalamountReceived ,
                  @UndistributedAmount ,
                  @CreatedBy ,
                  GETDATE() ,
                  @DivisionId ,
                  @StatusId ,
                  @BankDate      

                )      

        SET @ProcedureStatus = @PaymentTransactionId       

        ---increment series

        UPDATE  SupplierPM_Series
        SET     CURRENT_USED = CURRENT_USED + 1
        WHERE   DIV_ID = @DivisionId
                AND IS_FINISHED = 'N'

        ---update customer ledger        

		---if payment status approved   than make entry in customer ledger      

        SET @Remarks = 'Payment release against -' + @PaymentTransactionNo

        IF @StatusId = 2
            EXECUTE Proc_Ledger_Insert @AccountId, 0,
                @TotalamountReceived, @Remarks, @DivisionId,
                @PaymentTransactionId, 19, @CreatedBy

    END

---------------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[ProcPaymentTransaction_Insert]
    (
      @PaymentTransactionId NUMERIC = NULL ,
      @PaymentTransactionNo VARCHAR(100) = NULL ,
      @PaymentTypeId NUMERIC = NULL ,
      @AccountId NUMERIC = NULL ,
      @PaymentDate DATETIME = NULL ,
      @ChequeDraftNo VARCHAR(50) = NULL ,
      @ChequeDraftDate DATETIME = NULL ,
      @BankId NUMERIC = 0 ,
      @BankDate DATETIME = NULL ,
      @Remarks VARCHAR(200) = NULL ,
      @TotalamountReceived NUMERIC(18, 2) = 0 ,
      @UndistributedAmount NUMERIC(18, 2) = 0 ,
      @CreatedBy VARCHAR(50) = NULL ,
      @DivisionId NUMERIC = 0 ,
      @StatusId NUMERIC = NULL ,
      @PM_TYPE NUMERIC(18, 0) = NULL ,
      @ProcedureStatus INT OUTPUT       

    )
AS
    BEGIN      

        DECLARE @LedgerId NUMERIC     

        SELECT  @PaymentTransactionId = ISNULL(MAX(PaymentTransactionId), 0)
                + 1
        FROM    dbo.PaymentTransaction          

        INSERT  INTO PaymentTransaction
                ( PaymentTransactionId ,
                  PaymentTransactionNo ,
                  PaymentTypeId ,
                  AccountId ,
                  PaymentDate ,
                  ChequeDraftNo ,
                  ChequeDraftDate ,
                  BankId ,
                  Remarks ,
                  TotalAmountReceived ,
                  UndistributedAmount ,
                  CreatedBy ,
                  CreatedDate ,
                  DivisionId ,
                  StatusId ,
                  BankDate ,
                  PM_TYPE      

                )
        VALUES  ( @PaymentTransactionId ,
                  @PaymentTransactionNo ,
                  @PaymentTypeId ,
                  @AccountId ,
                  @PaymentDate ,
                  @ChequeDraftNo ,
                  @ChequeDraftDate ,
                  @BankId ,
                  @Remarks ,
                  @TotalamountReceived ,
                  @UndistributedAmount ,
                  @CreatedBy ,
                  GETDATE() ,
                  @DivisionId ,
                  @StatusId ,
                  @BankDate ,
                  @PM_TYPE      

                )      

        SET @ProcedureStatus = @PaymentTransactionId       

        ---increment series

        UPDATE  PM_SERIES
        SET     CURRENT_USED = CURRENT_USED + 1
        WHERE   DIV_ID = @DivisionId
                AND IS_FINISHED = 'N'
                AND PM_TYPE = @PM_TYPE

        ---update customer ledger        

		---if payment status approved   than make entry in customer ledger      

      

        IF @StatusId = 2
            BEGIN

                IF @PM_TYPE = 1
                    BEGIN
					  SET @Remarks = 'Payment Recieved against ' + @PaymentTransactionNo
                        EXECUTE Proc_Ledger_Insert @AccountId,
                            @TotalamountReceived, 0, @Remarks, @DivisionId,
                            @PaymentTransactionId, 18, @CreatedBy
                    END
                IF @PM_TYPE = 2
                    BEGIN
					  SET @Remarks = 'Payment release against -' + @PaymentTransactionNo
                        EXECUTE Proc_Ledger_Insert @AccountId, 0,
                            @TotalamountReceived, @Remarks, @DivisionId,
                            @PaymentTransactionId, 19, @CreatedBy

                    END 

            END

    END
----------------------------------------------------------------------------------------------------
--EXEC PROC_GET_SUPPLIER_LEDGER_DETAILS '2017-08-01','2017-11-13',23
ALTER PROC [dbo].[PROC_GET_SUPPLIER_LEDGER_DETAILS]
    (
      @Fromdate AS DATETIME = NULL ,
      @ToDate AS DATETIME = NULL ,
      @AccountId AS INT = 0

    )
AS
    BEGIN

-----------------------------------Party Details With Opening Balance-------------------------------------------------------------------- 



        DECLARE @OpeningBalance NUMERIC(18, 2)

        SET @OpeningBalance = ( SELECT  ISNULL(SUM(CASHIN - CASHOUT), 0) AS OpeningBalance
                                FROM    dbo.LedgerMaster
                                        LEFT OUTER  JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TRANSACTIONDATE, 101) AS DATETIME) < CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                        INNER  JOIN dbo.ACCOUNT_MASTER ON dbo.ACCOUNT_MASTER.ACC_ID = dbo.LedgerMaster.AccountId
                                WHERE   LedgerMaster.AccountId = @AccountId
                              )   

     

-----------------------------------Party Details With Opening Balance-------------------------------------------------------------------- 

        IF OBJECT_ID('tempdb..#Ledgertemp') IS NOT NULL
            DROP TABLE #Ledgertemp        

        CREATE TABLE #Ledgertemp
            (
              LedgerId NUMERIC(18, 0) ,
              TRANSACTIONDATE DATETIME ,
              AccId NUMERIC(18, 0) ,
              AmountInHand NUMERIC(18, 2) ,
              CASHIN NUMERIC(18, 2) ,
              CASHOUT NUMERIC(18, 2) ,
              REMARKS NVARCHAR(500) ,
              TransactionTypeId NUMERIC(18, 0) ,
              ClosingBalance NUMERIC(18, 2) ,
              CheckgBalance NVARCHAR(500)
            ) 



        DECLARE @LedgerId INT ,
            @TRANSACTIONDATE DATETIME ,
            @AccId NUMERIC(18, 0) ,
            @AmountInHand NUMERIC(18, 2) ,
            @CASHIN NUMERIC(18, 2) ,
            @CASHOUT NUMERIC(18, 2) ,
            @REMARKS NVARCHAR(500) ,
            @TransactionTypeId NUMERIC(18, 0)

					 

        DECLARE Ledger_cursor CURSOR
        FOR
            SELECT  LedgerId ,
                    TRANSACTIONDATE ,
                    AccountId ,
                    AmountInHand ,
                    CASH_IN ,
                    CASH_OUT ,
                    REMARKS ,
                    TransactionTypeId
            FROM    ( SELECT    dbo.LedgerMaster.LedgerId ,
                                TRANSACTIONDATE ,
                                AccountId ,
                                AmountInHand ,
                                ISNULL(CASHIN, 0) AS CASH_IN ,
                                ISNULL(CASHOUT, 0) AS CASH_OUT ,
                                REMARKS ,
                                TransactionTypeId
                      FROM      dbo.LedgerMaster
                                INNER JOIN dbo.LedgerDetail ON dbo.LedgerMaster.LedgerId = dbo.LedgerDetail.LedgerId
                                                              AND CAST(CONVERT(VARCHAR, TransactionDate, 101) AS DATETIME) BETWEEN CAST(CONVERT(VARCHAR, @FromDate, 101) AS DATETIME)
                                                              AND
                                                              CAST(CONVERT(VARCHAR, @ToDate, 101) AS DATETIME)
                      WHERE     dbo.LedgerMaster.AccountId = @AccountId
                    ) tb
            ORDER BY TRANSACTIONDATE





        OPEN Ledger_cursor



        FETCH NEXT FROM Ledger_cursor INTO @LedgerId, @TRANSACTIONDATE, @AccId,
            @AmountInHand, @CASHIN, @CASHOUT, @REMARKS, @TransactionTypeId

        WHILE @@FETCH_STATUS = 0
            BEGIN

                INSERT  INTO #Ledgertemp
                        SELECT  @LedgerId ,
                                @TRANSACTIONDATE ,
                                @AccId ,
                                @AmountInHand ,
                                @CASHIN ,
                                @CASHOUT ,
                                @REMARKS ,
                                @TransactionTypeId ,
                                @OpeningBalance - @CASHOUT + @CASHIN ,
                                ( CAST(@OpeningBalance AS VARCHAR(50)) + '  -'
                                  + CAST(@CASHOUT AS VARCHAR(50)) + '  +'
                                  + CAST(@CASHIN AS VARCHAR(50)) )





                SET @OpeningBalance = @OpeningBalance - @CASHOUT + @CASHIN   

                FETCH NEXT FROM Ledger_cursor INTO @LedgerId, @TRANSACTIONDATE,
                    @AccId, @AmountInHand, @CASHIN, @CASHOUT, @REMARKS,
                    @TransactionTypeId 





            END

        CLOSE Ledger_cursor

        DEALLOCATE Ledger_cursor



        SELECT  LedgerId ,
                TRANSACTIONDATE ,
                AccId ,
                AmountInHand ,
                CASHIN ,
                CASHOUT ,
                REMARKS ,
                ClosingBalance AS ClosingBalance
        FROM    #Ledgertemp
        ORDER BY TRANSACTIONDATE

    END
------------------------------------------------------------------------------------------------------