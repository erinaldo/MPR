INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0042_24_Oct_2018_Delivery Challan' ,
          GETDATE()
        )
Go

ALTER TABLE STOCK_TRANSFER_DETAIL ADD MRP NUMERIC(18,2) DEFAULT 0
ALTER TABLE STOCK_TRANSFER_DETAIL ADD GSTID NUMERIC(18,0)DEFAULT 0

UPDATE STOCK_TRANSFER_DETAIL SET MRP=ITEM_MASTER.MRP_Num,GSTID=vat_id FROM STOCK_TRANSFER_DETAIL JOIN ITEM_MASTER ON STOCK_TRANSFER_DETAIL.ITEM_ID=ITEM_MASTER.ITEM_ID



GO
ALTER PROC [dbo].[PROC_STOCK_TRANSER_DETAIL]    
    @Transfer_ID INT,    
    @STOCK_DETAIL_ID INT,   
    @BATCH_NO VARCHAR(20),  
    @EXPIRY_DATE DATETIME,   
    @ITEM_ID INT,    
    @ITEM_QTY NUMERIC(18, 3),    
    @ACCEPTED_QTY NUMERIC(18, 3),    
    @RETURNED_QTY NUMERIC(18, 3),    
    @Created_By NVARCHAR(100),    
    @Creation_Date DATETIME,    
    @Modified_By NVARCHAR(100),    
    @Modification_Date DATETIME,    
    @Division_ID INT,    
    @TRANSFER_RATE NUMERIC(18, 2),    
    @PROC_TYPE INT    
AS     
    BEGIN  
    DECLARE @MRP NUMERIC(18, 2)=0
    DECLARE @GSTID NUMERIC(18, 0)=0
    
     SELECT @MRP=MRP_Num,@GSTID=vat_id FROM dbo.ITEM_MASTER WHERE ITEM_ID=@ITEM_ID
        
        IF @PROC_TYPE = 1 -- INSERT           
            BEGIN          
                INSERT  INTO STOCK_TRANSFER_DETAIL    
                        (    
                          TRANSFER_ID,    
                          ITEM_ID,    
                          STOCK_DETAIL_ID,   
                          BATCH_NO,  
                          EXPIRY_DATE,   
                          ITEM_QTY,    
                          ACCEPTED_STOCK_DETAIL_ID,    
                          ACCEPTED_QTY,    
                          RETURNED_QTY,    
                          CREATED_BY,    
                          CREATION_DATE,    
                          MODIFIED_BY,    
                          MODIFIED_DATE,    
                          DIVISION_ID,    
                          TRANSFER_RATE,
                          MRP,
                          GSTID        
                        )    
                VALUES  (    
                          @Transfer_ID,    
                          @ITEM_ID,    
                          @STOCK_DETAIL_ID,   
                          @BATCH_NO,  
                          @EXPIRY_DATE,   
                          @ITEM_QTY,    
                          -1,    
                          @ACCEPTED_QTY,    
                          @RETURNED_QTY,    
                          @Created_By,    
                          @Creation_Date,    
                          @Modified_By,    
                          @Modification_Date,    
                          @Division_ID,    
                          @TRANSFER_RATE,
                          @MRP,
                          @GSTID          
                        )          
            END          
    END  
    
    
