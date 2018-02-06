insert INTO dbo.DBScriptUpdateLog
        ( LogFileName, ExecuteDateTime )
VALUES  ( '0003_05_Feb_2018_Update_Stock_Adjustment',
          GETDATE()
         )
----------------------------------------------------------------------------------------------------------------------------------------------

CREATE	PROCEDURE [dbo].[Update_Stock_Detail_Adjustment]
    @item_ID INT ,
    @Batch_no NVARCHAR(100) ,
    @Expiry_Date DATETIME ,
    @Item_Qty NUMERIC(18, 4) ,
    @Doc_ID INT ,
    @Transaction_ID INT ,
    @STOCKDETAIL_ID INT OUTPUT
AS
    BEGIN  
    
        UPDATE  STOCK_DETAIL
        SET     Item_Qty = Item_Qty + @Item_Qty ,
                Balance_Qty = Balance_Qty + @Item_Qty
        WHERE   STOCK_DETAIL_ID = @STOCKDETAIL_ID and item_id = @item_id   
    END

------------------------------------------------------------------------------------------------------------------------------------------------

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
                FROM    dbo.STOCK_DETAIL WHERE Item_id=@v_Item_ID     
                      
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

-------------------------------------------------------------------------------------------------------------------------------------

ALTER PROC [dbo].[PROC_ACCEPT_STOCK_TRANSFER_DETAIL]  
    @Transfer_ID INT,  
    @ITEM_ID INT,  
    @ITEM_QTY NUMERIC(18, 3),  
    @TRANSFER_RATE NUMERIC(18, 2),  
    @Created_By NVARCHAR(100),  
    @Creation_Date DATETIME,  
    @Modified_By NVARCHAR(100),  
    @Modification_Date DATETIME,  
    @Division_ID INT,  
    @STOCK_DETAIL_ID INT,  
    @BATCH_NO VARCHAR(20),  
    @EXPIRY_DATE DATETIME,  
    @transaction_id INT  
AS   
    BEGIN                
                
        DECLARE @ACCEPTED_STOCK_DETAIL_ID NUMERIC(18, 0)    
         --it will insert entry in stock detail table and         
        --return stock_detail_id.  
        
         
                SELECT  @ACCEPTED_STOCK_DETAIL_ID = MAX(STOCK_DETAIL_ID)
                FROM    dbo.STOCK_DETAIL WHERE Item_id=@ITEM_ID     
          
          
          
        EXEC Update_Stock_Detail_Adjustment @ITEM_ID, @BATCH_NO, @EXPIRY_DATE, @item_qty,  
            @Transfer_ID, @transaction_id, @ACCEPTED_STOCK_DETAIL_ID OUTPUT    
    
                
        INSERT  INTO STOCK_TRANSFER_DETAIL  
                (  
                  TRANSFER_ID,  
                  ITEM_ID,  
                  ITEM_QTY,  
                  ACCEPTED_QTY,  
                  RETURNED_QTY,  
                  CREATED_BY,  
                  CREATION_DATE,  
                  MODIFIED_BY,  
                  MODIFIED_DATE,  
                  DIVISION_ID,  
                  TRANSFER_RATE,  
                  STOCK_DETAIL_ID,  
                  ACCEPTED_STOCK_DETAIL_ID,  
                  BATCH_NO,  
                  EXPIRY_DATE                
                            
                )  
        VALUES  (  
                  @Transfer_ID,  
                  @ITEM_ID,  
                  @ITEM_QTY,  
                  @ITEM_QTY,  
                  0,  
                  @Created_By,  
                  @Creation_Date,  
                  @Modified_By,  
                  @Modification_Date,  
                  @Division_ID,  
                  @TRANSFER_RATE,  
                  @STOCK_DETAIL_ID,  
                  @ACCEPTED_STOCK_DETAIL_ID,  
                  @BATCH_NO,  
                  @EXPIRY_DATE                
                            
                )        
                    
                    
                  --it will insert entry in transaction log with stock_detail_id        
        EXEC INSERT_TRANSACTION_LOG @Transfer_ID, @ITEM_ID, @transaction_id,  
            @ACCEPTED_STOCK_DETAIL_ID, @ITEM_QTY, @Creation_Date, 0    
                        
        RETURN @ACCEPTED_STOCK_DETAIL_ID    
                          
    END 

--------------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECEIVED_WITHOUT_PO_DETAIL]
    (
      @v_Received_ID NUMERIC(18, 0) ,
      @v_Item_ID NUMERIC(18, 0) ,
      @v_Item_Qty NUMERIC(18, 4) ,
      @v_Item_Rate NUMERIC(18, 4) ,
      @v_Created_By VARCHAR(100) ,
      @v_Creation_Date DATETIME ,
      @v_Modified_By VARCHAR(100) ,
      @v_Modification_Date DATETIME ,
      @v_Division_Id INT ,
      @v_Item_vat NUMERIC(18, 4) ,
      @v_Item_exice NUMERIC(18, 4) ,
      @v_Batch_no VARCHAR(50) ,
      @v_Expiry_Date DATETIME ,
      @V_Trans_Type NUMERIC(18, 0) ,
      @v_Proc_Type INT ,
      @v_DType CHAR(1) ,
      @v_DiscountValue NUMERIC(18, 2)
    )
AS
    BEGIN          
          
        IF @V_PROC_TYPE = 1
            BEGIN          
                         
                         
                DECLARE @STOCK_DETAIL_ID NUMERIC(18, 0)      
                  SELECT  @STOCK_DETAIL_ID = MAX(STOCK_DETAIL_ID)
                FROM    dbo.STOCK_DETAIL WHERE Item_id=@v_Item_ID                 
                              
                --it will insert entry in stock detail table and       
                --return stock_detail_id.              
                EXEC Update_Stock_Detail_Adjustment @v_Item_ID, @v_Batch_no,
                    @v_Expiry_Date, @v_Item_Qty, @v_Received_ID, @V_Trans_Type,
                    @STOCK_DETAIL_ID OUTPUT      
                   
                   
                   
                INSERT  INTO MATERIAL_RECEIVED_WITHOUT_PO_DETAIL
                        ( Received_ID ,
                          Item_ID ,
                          Item_Qty ,
                          Item_Rate ,
                          Created_By ,
                          Creation_Date ,
                          Modified_By ,
                          Modification_Date ,
                          Division_Id ,
                          Item_vat ,
                          Item_exice ,
                          BAtch_No ,
                          Expiry_Date ,
                          Bal_Item_Qty ,
                          Bal_Item_Rate ,
                          Bal_Item_Vat ,
                          Bal_Item_Exice ,
                          Stock_Detail_Id ,
                          DType ,
                          DiscountValue                
                        )
                VALUES  ( @V_Received_ID ,
                          @V_Item_ID ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @V_Created_By ,
                          @V_Creation_Date ,
                          @V_Modified_By ,
                          @V_Modification_Date ,
                          @V_Division_Id ,
                          @V_Item_vat ,
                          @V_Item_exice ,
                          @v_Batch_no ,
                          @v_Expiry_Date ,
                          @V_Item_Qty ,
                          @V_Item_Rate ,
                          @V_Item_vat ,
                          @V_Item_exice ,
                          @STOCK_DETAIL_ID ,
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
                          2 ,
                          @V_Received_ID ,
                          @V_Division_Id  
                        )  
                      
                --it will insert entry in transaction log with stock_detail_id      
                EXEC INSERT_TRANSACTION_LOG @v_Received_ID, @v_Item_ID,
                    @V_Trans_Type, @STOCK_DETAIL_ID, @v_Item_Qty,
                    @v_Creation_Date, 0           
              
                    
           
                                  
                                  
            END          
          
               
          
    END

------------------------------------------------------------------------------------------------------------------------------------
