  INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0024_23_July_2018_Add_Partial_PO' ,
          GETDATE()
        )
Go

ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECEIVED_AGAINST_PO_DETAIL]  
    (  
      @v_PO_ID NUMERIC(18, 0) ,  
      @v_DIV_ID NUMERIC(18, 0) ,  
      @v_Receipt_ID NUMERIC(18, 0) ,  
      @v_Item_ID NUMERIC(18, 0) ,  
      @v_Item_Qty NUMERIC(18, 4) ,  
      @v_Trans_Type NUMERIC(18, 0) ,--new added                    
      @v_Creation_Date DATETIME ,--new added                    
      @v_Expiry_Date DATETIME , -- new added                    
      @v_Item_Rate NUMERIC(18, 2) ,  
      @v_Batch_no VARCHAR(50) ,  
      @v_Item_vat_per NUMERIC(18, 2) ,  
      @v_Item_cess_per NUMERIC(18, 2) ,  
      @v_Item_Exice_per NUMERIC(18, 2) ,  
      @v_Proc_Type INT ,  
      @v_DType CHAR(1) ,  
      @v_DiscountValue NUMERIC(18, 2)  
    )  
AS  
    BEGIN                    
            
        IF @V_PROC_TYPE = 1  
            BEGIN                    
            
  --- added on 16-12-2009 for insertion in stock degail and insert transaction log    
      
                DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)      
                SELECT  @STOCK_DETAIL_ID = MAX(STOCK_DETAIL_ID)  
                FROM    dbo.STOCK_DETAIL  
                WHERE   Item_id = @v_Item_ID         
                          
                EXEC Update_Stock_Detail_Adjustment @v_Item_ID, @v_Batch_no,  
                    @v_Expiry_Date, @v_Item_Qty, @v_Receipt_ID, @v_Trans_Type,  
                    @STOCK_DETAIL_ID OUTPUT                    
            
            
            
                INSERT  INTO MATERIAL_RECEIVED_AGAINST_PO_DETAIL  
                        ( Receipt_ID ,  
                          Item_ID ,  
                          PO_ID ,  
                          Item_Qty ,  
                          Item_Rate ,  
                          Vat_Per ,  
                          Cess_Per ,  
                          Bal_Item_Qty ,  
                          bal_vat_per ,  
                          stocK_dETAIL_Id ,  
                          Exice_Per ,  
                          Bal_Exice_Per ,  
                          DType ,  
                          DiscountValue                   
                        )  
                VALUES  ( @V_Receipt_ID ,  
                          @V_Item_ID ,  
                          @v_PO_ID ,  
                          @V_Item_Qty ,  
                          @V_Item_Rate ,  
                          @v_Item_vat_per ,  
                          @v_Item_cess_per ,  
                          @V_Item_Qty ,  
                          @v_Item_vat_per ,  
                          @STOCK_DETAIL_ID ,  
                          @v_Item_Exice_per ,  
                          @v_Item_Exice_per ,  
                          @v_DType ,  
                          @v_DiscountValue                    
                        )                    
            
            
       --Inserting Transaction Log           
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
                          @V_Item_Qty ,  
                          @V_Creation_Date ,  
                          4 ,  
                          @V_Receipt_ID ,  
                          @v_DIV_ID        
                        )        
                         
                         
                UPDATE  PO_DETAIL  
                SET     BALANCE_QTY = BALANCE_QTY - @v_Item_Qty  
                WHERE   PO_ID = @v_PO_ID  
                        AND ITEM_ID = @v_Item_ID    
                        
                  UPDATE  dbo.PO_STATUS  
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

Go
------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[Fill_PO] ( @Div_ID NUMERIC(18, 0) )  
AS   
    BEGIN    
        SELECT  tb.PO_CODE AS PO_CODE,  
                tb.PO_ID AS PO_ID  
        FROM    ( SELECT    '--Select--' AS PO_CODE,  
                            0 AS PO_ID  
                  UNION ALL  
                  SELECT DISTINCT  
                            PM.PO_CODE + CAST(PM.PO_NO AS VARCHAR) AS PO_CODE,  
                            PM.PO_ID  
                  FROM      PO_MASTER PM,  
                            PO_STATUS PS  
                  WHERE     PM.PO_ID = PS.PO_ID  
                            AND INDENT_ID IN ( SELECT   INDENT_ID  
                                               FROM     INDENT_MASTER  
                                               WHERE    DIVISION_ID = 3 )  
                            AND BALANCE_QTY > 0  
                            AND PO_STATUS = 2  
                ) tb  
        ORDER BY tb.PO_ID  
    END    

Go
----------------------------------------------------------------------------------------------------

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
                ISNULL(PO_DETAIL.CESS_PER, 0.00) AS CESS_PER ,  
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
        
         AND PO_DETAIL.BALANCE_QTY >0
    --            AND ( ( CASE WHEN ( @open <> 1 ) THEN PO_DETAIL.BALANCE_QTY  
    --                         ELSE 1  
    --                    END ) > 0 )    
    END 

-------------------------------------------------------------------------------------------------------