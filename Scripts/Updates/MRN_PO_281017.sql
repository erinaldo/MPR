-- Fill_PO_ITEMS 19,1  
ALTER PROCEDURE [dbo].[Fill_PO_ITEMS]
    (
      @PO_ID NUMERIC(18, 0) ,
      @DIV_ID NUMERIC(18, 0)
    )
AS
    BEGIN   
        DECLARE @con VARCHAR(100)  
        DECLARE @open INT  
  
        SELECT  @open = open_po_qty
        FROM    po_master
        WHERE   po_id = @po_id  
   
-- if (@open=1)  
-- begin  
--  @con= ''  
-- else         
--  @con='AND (PO_DETAIL.BALANCE_QTY > 0)'  
-- end  
    
        SELECT  PO_DETAIL.PO_ID ,
                IM.ITEM_ID ,
                UNIT_MASTER.UM_ID ,
                IM.ITEM_CODE ,
                IM.ITEM_NAME ,
                UNIT_MASTER.UM_Name ,
                PO_DETAIL.ITEM_RATE ,
                ISNULL(PO_DETAIL.DType, 'P') AS DType ,
                ISNULL(PO_DETAIL.DiscountValue, 0.00) AS DISC ,
                ISNULL(PO_DETAIL.VAT_PER, 0.00) AS VAT_PER ,
                ISNULL(PO_DETAIL.EXICE_PER, 0.00) AS EXICE_PER ,
                '' AS BATCH_NO ,
                DATEADD(yy, 2, GETDATE()) AS EXPIRY_DATE ,
                CASE WHEN ISNULL(PO_DETAIL.DType, 'P') = 'P'
                     THEN ISNULL(CAST(PO_DETAIL.Balance_qty
                                 * PO_DETAIL.ITEM_RATE AS NUMERIC(18, 2)),
                                 0.00)
                          - ISNULL(CAST(PO_DETAIL.Balance_qty
                                   * PO_DETAIL.ITEM_RATE AS NUMERIC(18, 2)),
                                   0.00) * ISNULL(PO_DETAIL.DiscountValue,
                                                  0.00) / 100
                     ELSE ISNULL(CAST(PO_DETAIL.Balance_qty
                                 * PO_DETAIL.ITEM_RATE AS NUMERIC(18, 2)),
                                 0.00) - ISNULL(PO_DETAIL.DiscountValue, 0.00)
                END AS Net_Amount ,
                CASE WHEN ISNULL(PO_DETAIL.DType, 'P') = 'P'
                     THEN ISNULL(CAST(( ( PO_DETAIL.Balance_qty
                                          * PO_DETAIL.ITEM_RATE
                                          - ISNULL(CAST(PO_DETAIL.Balance_qty
                                                   * PO_DETAIL.ITEM_RATE AS NUMERIC(18,
                                                              2)), 0.00)
                                          * ISNULL(PO_DETAIL.DiscountValue,
                                                   0.00) / 100 )
                                        + ( PO_DETAIL.Balance_qty
                                            * PO_DETAIL.ITEM_RATE
                                            - ISNULL(CAST(PO_DETAIL.Balance_qty
                                                     * PO_DETAIL.ITEM_RATE AS NUMERIC(18,
                                                              2)), 0.00)
                                            * ISNULL(PO_DETAIL.DiscountValue,
                                                     0.00) / 100 )
                                        * PO_DETAIL.EXICE_PER / 100 )
                                 + ( ( PO_DETAIL.Balance_qty
                                       * PO_DETAIL.ITEM_RATE
                                       - ISNULL(CAST(PO_DETAIL.Balance_qty
                                                * PO_DETAIL.ITEM_RATE AS NUMERIC(18,
                                                              2)), 0.00)
                                       * ISNULL(PO_DETAIL.DiscountValue, 0.00)
                                       / 100 ) + ( PO_DETAIL.Balance_qty
                                                   * PO_DETAIL.ITEM_RATE
                                                   - ISNULL(CAST(PO_DETAIL.Balance_qty
                                                            * PO_DETAIL.ITEM_RATE AS NUMERIC(18,
                                                              2)), 0.00)
                                                   * ISNULL(PO_DETAIL.DiscountValue,
                                                            0.00) / 100 )
                                     * PO_DETAIL.EXICE_PER / 100 )
                                 * PO_DETAIL.VAT_PER / 100 AS NUMERIC(18, 2)),
                                 0.00)
                     ELSE ISNULL(CAST(( ( PO_DETAIL.Balance_qty
                                          * PO_DETAIL.ITEM_RATE
                                          - ISNULL(PO_DETAIL.DiscountValue,
                                                   0.00) )
                                        + ( PO_DETAIL.Balance_qty
                                            * PO_DETAIL.ITEM_RATE
                                            - ISNULL(PO_DETAIL.DiscountValue,
                                                     0.00) )
                                        * PO_DETAIL.EXICE_PER / 100 )
                                 + ( ( PO_DETAIL.Balance_qty
                                       * PO_DETAIL.ITEM_RATE
                                       - ISNULL(PO_DETAIL.DiscountValue, 0.00) )
                                     + ( PO_DETAIL.Balance_qty
                                         * PO_DETAIL.ITEM_RATE
                                         - ISNULL(PO_DETAIL.DiscountValue,
                                                  0.00) )
                                     * PO_DETAIL.EXICE_PER / 100 )
                                 * PO_DETAIL.VAT_PER / 100 AS NUMERIC(18, 2)),
                                 0.00)
                END AS Gross_Amount ,
                ITEM_DETAIL.IS_STOCKABLE ,
                0 AS CostCenter_Id ,
                '' AS CostCenter_Code ,
                '' AS CostCenter_Name ,
                PO_DETAIL.Balance_qty AS PO_QTY ,
                0.00 AS BATCH_QTY ,
                PM.OPEN_PO_QTY
        FROM    ITEM_MASTER AS IM
                INNER JOIN UNIT_MASTER ON IM.UM_ID = UNIT_MASTER.UM_ID
                INNER JOIN PO_DETAIL ON IM.ITEM_ID = PO_DETAIL.ITEM_ID
                INNER JOIN PO_MASTER AS PM ON PO_DETAIL.PO_ID = PM.PO_ID
                INNER JOIN ITEM_DETAIL ON IM.ITEM_ID = ITEM_DETAIL.ITEM_ID
        WHERE   ( PO_DETAIL.PO_ID = @PO_ID )
                AND ( ( CASE WHEN ( @open <> 1 ) THEN PO_DETAIL.BALANCE_QTY
                             ELSE 1
                        END ) > 0 )
    END   

	------------------------------------------------------------------------------

	ALTER PROCEDURE [dbo].[Fill_PO] ( @Div_ID NUMERIC(18, 0) )
AS
    BEGIN  
        SELECT  tb.PO_CODE AS PO_CODE ,
                tb.PO_ID AS PO_ID
        FROM    ( SELECT    '--Select--' AS PO_CODE ,
                            0 AS PO_ID
                  UNION ALL
                  SELECT DISTINCT
                            PM.PO_CODE + CAST(PM.PO_NO AS VARCHAR) AS PO_CODE ,
                            PM.PO_ID
                  FROM      PO_MASTER PM ,
                            PO_STATUS PS
                  WHERE     PM.PO_ID = PS.PO_ID
                            AND INDENT_ID IN ( SELECT   INDENT_ID
                                               FROM     INDENT_MASTER
                                               WHERE    DIVISION_ID = @div_id )
                            AND BALANCE_QTY > 0
                            AND PO_STATUS = 2
                            AND PM.PO_ID NOT IN (
                            SELECT  PO_ID
                            FROM    [dbo].[MATERIAL_RECEIVED_AGAINST_PO_MASTER] )
                ) tb
        ORDER BY tb.PO_ID DESC
    END  

	------------------------------------------------------------------------------

ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER ADD GROSS_AMOUNT numeric(18, 2) NULL
ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER ADD GST_AMOUNT numeric(18, 2) NULL
ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER ADD NET_AMOUNT numeric(18, 2) NULL
ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER ADD MRN_TYPE INT NULL

---------------------------------------------------------------------------------------

ALTER TABLE dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER ADD GROSS_AMOUNT numeric(18, 2) NULL
ALTER TABLE dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER ADD GST_AMOUNT numeric(18, 2) NULL
ALTER TABLE dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER ADD NET_AMOUNT numeric(18, 2) NULL
ALTER TABLE dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER ADD MRN_TYPE INT NULL

--------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECIEVED_AGAINST_PO_MASTER]
    (
      @v_Receipt_ID NUMERIC(18, 0),
      @v_Receipt_No NUMERIC(18, 0),
      @v_Receipt_Code VARCHAR(20),
      @v_PO_ID NUMERIC(18, 0),
      @v_Receipt_Date DATETIME,
      @v_Remarks VARCHAR(500),
      @v_MRN_NO NUMERIC(18, 0),
      @v_MRN_PREFIX VARCHAR(50),
      @v_Created_BY VARCHAR(100),
      @v_Creation_Date DATETIME,
      @v_Modified_By VARCHAR(100),
      @v_Modification_Date DATETIME,
      @v_Division_ID NUMERIC(18, 0),
      @v_Proc_Type INT,
      @V_mrn_status INT,
      @v_freight NUMERIC(18, 2),
      @v_Other_Charges NUMERIC(18, 2),
      @v_Discount_amt NUMERIC(18, 2),
	  @v_GROSS_AMOUNT NUMERIC(18, 2),
      @v_GST_AMOUNT NUMERIC(18, 2),
      @v_NET_AMOUNT NUMERIC(18, 2),
      @V_MRN_TYPE INT,
      @V_VAT_ON_EXICE INT,
      @v_Invoice_No NVARCHAR(50),
      @v_Invoice_Date DATETIME,
      @v_MRNCompanies_ID INT            
    )
AS 
    BEGIN            
        
        IF @V_PROC_TYPE = 1 
            BEGIN            
        
                SELECT  @V_Receipt_No = ISNULL(MAX(Receipt_No), 0) + 1
                FROM    MATERIAL_RECEIVED_AGAINST_PO_MASTER            
        
                INSERT  INTO MATERIAL_RECEIVED_AGAINST_PO_MASTER
                        (
                          Receipt_ID,
                          Receipt_No,
                          Receipt_Code,
                          Invoice_No,
                          Invoice_Date,
                          PO_ID,
                          Receipt_Date,
                          Remarks,
                          MRN_NO,
                          MRN_PREFIX,
                          Created_BY,
                          Creation_Date,
                          Modified_By,
                          Modification_Date,
                          Division_ID,
                          MRN_STATUS,
                          freight,
                          Other_charges,
                          Discount_amt,
						  GROSS_AMOUNT,
						  GST_AMOUNT ,
						  NET_AMOUNT ,
						  MRN_TYPE,
                          VAT_ON_EXICE,
                          MRNCompanies_ID,
                          IsPrinted        
                        )
                VALUES  (
                          @V_Receipt_ID,
                          @V_Receipt_No,
                          @V_Receipt_Code,
                          @v_Invoice_No,
                          @v_Invoice_Date,
                          @V_PO_ID,
                          @v_Receipt_Date,
                          @V_Remarks,
                          @V_MRN_NO,
                          @V_MRN_PREFIX,
                          @V_Created_BY,
                          @V_Creation_Date,
                          @V_Modified_By,
                          @V_Modification_Date,
                          @V_Division_ID,
                          @V_mrn_status,
                          @v_freight,
                          @v_other_charges,
                          @v_Discount_amt,
						  @v_GROSS_AMOUNT,
						  @v_GST_AMOUNT ,
						  @v_NET_AMOUNT ,
						  @v_MRN_TYPE,
                          @V_VAT_ON_EXICE,
                          @v_MRNCompanies_ID,
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
                SET     PO_ID = @V_PO_ID,
                        Receipt_Date = @v_Receipt_Date,
                        Remarks = @V_Remarks,
                        Created_BY = @V_Created_BY,
                        Creation_Date = @V_Creation_Date,
                        Modified_By = @V_Modified_By,
                        Modification_Date = @V_Modification_Date,
                        Division_ID = @V_Division_ID,
                        freight = @v_freight,
                        Other_charges = @v_Other_Charges,
                        Discount_amt = @v_Discount_amt,
                        VAT_ON_EXICE = @V_VAT_ON_EXICE,
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
-----------------------------------------------------------------------------------------------------------

ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL ADD DType char(1) NULL
ALTER TABLE dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL ADD DiscountValue numeric(18, 2) NULL


-------------------------------------------------------------------------------------------------------------

ALTER TABLE dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL ADD DType char(1) NULL
ALTER TABLE dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL ADD DiscountValue numeric(18, 2) NULL


-------------------------------------------------------------------------------------------------------------

ALTER TABLE dbo.NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO ADD DType char(1) NULL
ALTER TABLE dbo.NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO ADD DiscountValue numeric(18, 2) NULL

------------------------------------------------------------------------------------------------------------

ALTER TABLE dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO ADD DType char(1) NULL
ALTER TABLE dbo.NON_STOCKABLE_ITEMS_MAT_REC_WO_PO ADD DiscountValue numeric(18, 2) NULL

----------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECEIVED_AGAINST_PO_DETAIL]
    (
      @v_PO_ID NUMERIC(18, 0),
      @v_DIV_ID NUMERIC(18, 0),
      @v_Receipt_ID NUMERIC(18, 0),
      @v_Item_ID NUMERIC(18, 0),
      @v_Item_Qty NUMERIC(18, 4),
      @v_Trans_Type NUMERIC(18, 0),--new added              
      @v_Creation_Date DATETIME,--new added              
      @v_Expiry_Date DATETIME, -- new added              
      @v_Item_Rate NUMERIC(18, 2),
      @v_Batch_no VARCHAR(50),
      @v_Item_vat_per NUMERIC(18, 2),
      @v_Item_Exice_per NUMERIC(18, 2),
      @v_Proc_Type INT  ,
	  @v_DType CHAR(1),
      @v_DiscountValue NUMERIC(18, 2)           
    )
AS 
    BEGIN              
      
        IF @V_PROC_TYPE = 1 
            BEGIN              
      
  --- added on 16-12-2009 for insertion in stock degail and insert transaction log              
                DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)              
      
      
                EXEC INSERT_STOCK_DETAIL @v_Item_ID, @v_Batch_no,
                    @v_Expiry_Date, @v_Item_Qty, @v_Receipt_ID, @v_Trans_Type,
                    @STOCK_DETAIL_ID OUTPUT              
      
      
      
                INSERT  INTO MATERIAL_RECEIVED_AGAINST_PO_DETAIL
                        (
                          Receipt_ID,
                          Item_ID,
                          PO_ID,
                          Item_Qty,
                          Item_Rate,
                          Vat_Per,
                          Bal_Item_Qty,
                          bal_vat_per,
                          stocK_dETAIL_Id,
                          Exice_Per,
                          Bal_Exice_Per ,
						  DType,
						  DiscountValue             
                        )
                VALUES  (
                          @V_Receipt_ID,
                          @V_Item_ID,
                          @v_PO_ID,
                          @V_Item_Qty,
                          @V_Item_Rate,
                          @v_Item_vat_per,
                          @V_Item_Qty,
                          @v_Item_vat_per,
                          @STOCK_DETAIL_ID,
                          @v_Item_Exice_per,
                          @v_Item_Exice_per,
						  @v_DType,
						  @v_DiscountValue              
                        )              
      
      
       --Inserting Transaction Log     
                INSERT  INTO dbo.TransactionLog_Master
                        (
                          Item_id,
                          Issue_Qty,
                          Receive_Qty,
                          Transaction_Date,
                          TransactionType_Id,
                          Transaction_Id,
                          Division_Id  
                        )
                VALUES  (
                          @V_Item_ID,
                          0,
                          @V_Item_Qty,
                          @V_Creation_Date,
                          4,
                          @V_Receipt_ID,
                          @v_DIV_ID  
                        )  
                   
                   
                UPDATE  PO_DETAIL
                SET     BALANCE_QTY = BALANCE_QTY - @v_Item_Qty
                WHERE   PO_ID = @v_PO_ID
                        AND ITEM_ID = @v_Item_ID            
      
                UPDATE  ITEM_DETAIL
                SET     CURRENT_STOCK = CURRENT_STOCK + @v_Item_Qty
                WHERE   ITEM_ID = @v_Item_ID
                        AND DIV_ID = @v_DIV_ID              
      
      
      
      
      
      
                --it will insert entry in transaction log with stock_detail_id              
                EXEC INSERT_TRANSACTION_LOG @v_Receipt_ID, @v_Item_ID,
                    @V_Trans_Type, @STOCK_DETAIL_ID, @v_Item_Qty,
                    @v_Creation_Date, 0                   
  -----------------------------------------------------------------------------              
            END              
      
--        IF @V_PROC_TYPE = 2     
--            BEGIN              
--                UPDATE  MATERIAL_RECEIVED_AGAINST_PO_DETAIL        
--                SET     Item_ID = @V_Item_ID,        
--                        Item_Qty = @V_Item_Qty        
--                WHERE   Receipt_ID = @V_Receipt_ID              
--            END              
--              
--        IF @V_PROC_TYPE = 3         
--            BEGIN              
--              
--                DECLARE cur CURSOR        
--                    FOR SELECT  Item_ID,        
--                                Item_Qty        
--                        FROM    MATERIAL_RECEIVED_AGAINST_PO_DETAIL        
--                        WHERE   Receipt_ID = @V_Receipt_ID              
--              
-- DECLARE @itemid NUMERIC(18, 0)              
--                DECLARE @itemQty NUMERIC(18, 4)              
--              
--              
--                OPEN cur              
--                FETCH NEXT FROM cur INTO @itemid, @itemQty             
--                WHILE @@fetch_status = 0              
--                    BEGIN     
--                 
--                        UPDATE  PO_STATUS        
--                        SET     RECIEVED_QTY = RECIEVED_QTY - @itemQty,        
--                                BALANCE_QTY = BALANCE_QTY + @itemQty        
--                        WHERE   PO_ID = @V_PO_ID        
--                    AND ITEM_ID = @itemid              
--              
--                        UPDATE  ITEM_DETAIL        
--                        SET     CURRENT_STOCK = CURRENT_STOCK - @itemQty        
--                        WHERE   ITEM_ID = @itemid        
--                  AND DIV_ID = @v_DIV_ID               
--              
--                        FETCH NEXT FROM cur INTO @itemid, @itemQty            
--                    END              
--                CLOSE cur              
--                DEALLOCATE cur              
--              
--                DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_DETAIL        
--                WHERE   Receipt_ID = @V_Receipt_ID              
--            END              
      
    END     

-----------------------------------------------------------------------------------------------------------

  
ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECEIVED_AGAINST_PO_DETAIL_NON_STOCKABLE]
    (
      @v_PO_ID NUMERIC(18, 0) ,
      @v_DIV_ID NUMERIC(18, 0) ,
      @v_Receipt_ID NUMERIC(18, 0) ,
      @v_Item_ID NUMERIC(18, 0) ,
      @v_Item_Qty NUMERIC(18, 4) ,
      @v_Item_Rate NUMERIC(18, 2) ,
      @v_Item_Vat NUMERIC(18, 2) ,
      @v_Item_Exice NUMERIC(18, 2) ,
      @v_Batch_no VARCHAR(50) ,
      @v_Expiry_Date DATETIME , -- new added  
      @v_CostCenter_ID INT ,
      @v_Bal_Item_Qty NUMERIC(18, 4) ,
      @v_Bal_Item_Rate NUMERIC(18, 2) ,
      @v_Bal_Item_Vat NUMERIC(18, 2) ,
      @v_Bal_Item_Exice NUMERIC(18, 2) ,
      @v_DType CHAR(1) ,
      @v_DiscountValue NUMERIC(18, 2)
    )
AS
    BEGIN  
  
        INSERT  INTO dbo.NON_STOCKABLE_ITEMS_MAT_REC_AGAINST_PO
                ( Received_ID ,
                  PO_ID ,
                  Item_ID ,
                  CostCenter_ID ,
                  Item_Qty ,
                  Item_vat ,
                  Item_Exice ,
                  batch_no ,
                  batch_date ,
                  Item_Rate ,
                  Bal_Item_Qty ,
                  Bal_Item_Rate ,
                  Bal_Item_Vat ,
                  Bal_Item_Exice ,
                  Div_ID ,
                  DType ,
                  DiscountValue   
                )
        VALUES  ( /* Received_ID - int */
                  @v_Receipt_ID ,
 	/* PO_ID - int */
                  @v_PO_ID ,
 	/* Item_ID - int */
                  @v_Item_ID ,
 	/* CostCenter_ID - int */
                  @v_CostCenter_ID ,
 	/* Item_Qty - numeric(18, 4) */
                  @v_Item_Qty ,
 	/* Item_vat - numeric(18, 2) */
                  @v_Item_Vat ,
 	/* Item_Exice - numeric(18, 2) */
                  @v_Item_Exice ,
 	/* batch_no - varchar(50) */
                  @v_Batch_no ,
 	/* batch_date - datetime */
                  @v_Expiry_Date ,
 	/* Item_Rate - numeric(18, 2) */
                  @v_Item_Rate ,
 	/* Bal_Item_Qty - numeric(18, 4) */
                  @v_Bal_Item_Qty ,
 	/* Bal_Item_Rate - numeric(18, 2) */
                  @v_Bal_Item_Rate ,
 	/* Bal_Item_Vat - numeric(18, 4) */
                  @v_Bal_Item_Vat ,
 	/* Bal_Item_Exice - numeric(18, 4) */
                  @v_Bal_Item_Exice ,
 	/* Div_ID - int */
                  @v_DIV_ID ,
                  @v_DType ,
                  @v_DiscountValue  	
	            ) 
 	
 	
        UPDATE  PO_DETAIL
        SET     BALANCE_QTY = BALANCE_QTY - @v_Item_Qty
        WHERE   PO_ID = @v_PO_ID
                AND ITEM_ID = @v_Item_ID   
  
    END  
  
  ------------------------------------------------------------------------------------------------------




