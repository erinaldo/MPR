insert INTO dbo.DBScriptUpdateLog
        ( LogFileName, ExecuteDateTime )
VALUES  ( '0006_21_Mar_2018_CreditNote_WO_Items',
          GETDATE()
          )

go
 ALTER TABLE dbo.CreditNote_Master ADD CreditNote_Type varchar(50) Null , RefNo varchar(50) Null,RefDate_dt DateTime Null,Tax_Amt numeric(18,2) Null
	
GO
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
      @v_CN_ItemValue NUMERIC(18, 2)=0 ,
      @v_CN_ItemTax NUMERIC(18, 2)=0 ,
      @v_CN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
     @v_CreditNote_Type VARCHAR(50) = NULL ,
      @v_RefNo VARCHAR(50) = NULL ,
      @v_RefDate_dt DATETIME = GETDATE ,
      @v_Tax_Amt NUMERIC(18, 2) = 0 ,
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
                          INV_Date,
						   CreditNote_Type ,
                          RefNo ,
                          RefDate_dt ,
                          Tax_Amt
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
                          @v_INV_Date,
						   @v_CreditNote_Type ,
                          @v_RefNo ,
                          @v_RefDate_dt ,
                          @v_Tax_Amt
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
                        INV_Date = @v_INV_Date,
						CreditNote_Type = @v_CreditNote_Type ,
                        RefNo = @v_RefNo ,
                        RefDate_dt = @v_RefDate_dt ,
                        Tax_Amt = @v_Tax_Amt
                WHERE   CreditNote_ID = @V_CreditNote_ID
            END
    END

	GO
CREATE PROCEDURE [dbo].[PROC_CreditNote_WO_Items_DETAIL]
    (
      @v_CreditNote_ID NUMERIC(18, 0) ,
      @v_Item_ID NUMERIC(18, 0) ,
      @v_Item_Qty NUMERIC(18, 4) ,
      @v_Created_By VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Division_Id INT ,
      @v_Stock_Detail_Id NUMERIC(18, 0) ,
      @v_Item_Rate NUMERIC(18, 2) ,
      @v_Item_Tax NUMERIC(18, 2) ,
      @V_Trans_Type NUMERIC(18, 0) ,
      @v_Proc_Type INT    
    )
AS
    BEGIN    
        IF @V_PROC_TYPE = 1
            BEGIN    
                INSERT  INTO CreditNote_DETAIL
                        ( CreditNote_Id ,
                          Item_ID ,
                          Item_Qty ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_Id ,
                          Stock_Detail_Id ,
                          Item_Rate ,
                          Item_Tax
				        )
                VALUES  ( @V_CreditNote_ID ,
                          @V_Item_ID ,
                          @V_Item_Qty ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_Id ,
                          @v_Stock_Detail_Id ,
                          @v_Item_Rate ,
                          @v_Item_Tax
                        )    
                DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)
				              
                UPDATE  dbo.STOCK_DETAIL
                SET     Item_Qty = Item_Qty + @v_Item_Qty ,
                        Balance_Qty = BALANCE_QTY + @v_Item_Qty
                WHERE   STOCK_DETAIL_ID = @STOCK_DETAIL_ID    
							       
		
                --EXEC INSERT_TRANSACTION_LOG @v_CreditNote_ID, @v_Item_ID,
                --    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,
                --    @v_Creation_Date, 0     
            END    
    END

	
GO
CREATE PROCEDURE [dbo].[PROC_DebitNote_WO_Items_DETAIL]
    (
      @v_DebitNote_ID numeric(18, 0),
      @v_Item_ID numeric(18, 0),
      @v_Item_Qty numeric(18, 4),
      @v_Created_By varchar(100),
      @v_Creation_Date datetime,
      @v_Modified_By varchar(100),
      @v_Modification_Date datetime,
      @v_Division_Id int,
      @v_Stock_Detail_Id numeric(18, 0),
	  @v_Item_Rate numeric(18, 2),
	  @v_Item_Tax numeric(18, 2),
      @V_Trans_Type NUMERIC(18, 0),
      @v_Proc_Type int    
    )
AS 


    BEGIN    
        IF @V_PROC_TYPE = 1 
            BEGIN    
                INSERT  INTO DebitNote_DETAIL (
					DebitNote_Id,
					Item_ID,
					Item_Qty,
					Created_By,
					Creation_Date,
					Modified_By,
					Modification_Date,
					Division_Id,
					Stock_Detail_Id,
					Item_Rate,
					Item_Tax

				)
				VALUES  (
                          @V_DebitNote_ID,
                          @V_Item_ID,
                          @V_Item_Qty,
                          @V_Created_By,
                          @V_Creation_Date,
                          @V_Modified_By,
                          @V_Modification_Date,
                          @V_Division_Id,
                          @v_Stock_Detail_Id,
						  @v_Item_Rate,
						  @v_Item_Tax
                        )    
                DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)
                --it will insert entry in stock detail table and 
--                --return stock_detail_id.        
--                EXEC INSERT_STOCK_DETAIL @v_Item_ID, @v_Batch_no,
--                    @v_Expiry_Date, @v_Item_Qty, @v_Received_ID, @V_Trans_Type,
--                    @STOCK_DETAIL_ID OUTPUT
--                      
				EXEC UPDATE_STOCK_DETAIL_ISSUE
	@STOCK_DETAIL_ID = @v_Stock_Detail_Id, --  numeric(18, 0)
	@ISSUE_QTY = @v_Item_Qty --  numeric(18, 4)
                --it will insert entry in transaction log with stock_detail_id
                --EXEC INSERT_TRANSACTION_LOG @v_DebitNote_ID, @v_Item_ID,
                --    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,
                --    @v_Creation_Date, 0     
            END    
    END

