ALTER TABLE dbo.PO_DETAIL ADD DType VARCHAR(10) NULL
ALTER TABLE dbo.PO_DETAIL ADD DiscountValue NUMERIC(18, 2) NULL

--------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[PROC_PO_DETAIL]
    (
      @v_PO_ID DECIMAL ,
      @v_ITEM_ID DECIMAL ,
      @v_ITEM_QTY DECIMAL(18, 4) ,
      @v_VAT_PER DECIMAL(18, 2) ,
      @v_EXICE_PER DECIMAL(18, 2) ,
      @v_ITEM_RATE DECIMAL(18, 2) ,
	  @v_DISC_VALUE NUMERIC(18, 2) ,
      @v_DTYPE NVARCHAR(10) ,
      @v_CREATED_BY VARCHAR(50) ,
      @v_CREATION_DATE DATETIME ,
      @v_MODIFIED_BY VARCHAR(50) ,
      @v_MODIFIED_DATE DATETIME ,
      @v_DIVISION_ID INT ,
      @v_Proc_Type INT      
    )
AS
    BEGIN      
      
        IF @V_PROC_TYPE = 1
            BEGIN      
                INSERT  INTO PO_DETAIL
                        ( PO_ID ,
                          ITEM_ID ,
                          ITEM_QTY ,
                          balance_qty ,
                          VAT_PER ,
                          EXICE_PER ,
                          ITEM_RATE ,
                          CREATED_BY ,
                          CREATION_DATE ,
                          MODIFIED_BY ,
                          MODIFIED_DATE ,
                          DIVISION_ID,
						  DType,
						  DiscountValue
                        )
                VALUES  ( @V_PO_ID ,
                          @V_ITEM_ID ,
                          @V_ITEM_QTY ,
                          @V_ITEM_QTY ,
                          @V_VAT_PER ,
                          @V_EXICE_PER ,
                          @V_ITEM_RATE ,
                          @V_CREATED_BY ,
                          @V_CREATION_DATE ,
                          @V_MODIFIED_BY ,
                          @V_MODIFIED_DATE ,
                          @V_DIVISION_ID,
						  @v_DTYPE,
						  @v_DISC_VALUE
                        )      
            END      
      
-- IF @V_PROC_TYPE=2      
--  BEGIN      
--   UPDATE PO_DETAIL      
--     SET      
--     POD_ID =  @V_POD_ID,       
--     ITEM_ID =  @V_ITEM_ID,       
--     INDENT_ID =  @V_INDENT_ID,       
--     ITEM_QTY =  @V_ITEM_QTY,       
--     VAT_PER =  @V_VAT_PER,       
--     EXICE_PER =  @V_EXICE_PER,       
--     ITEM_RATE =  @V_ITEM_RATE,       
--     TOTAL_AMOUNT =  @V_TOTAL_AMOUNT,       
--     CREATED_BY =  @V_CREATED_BY,       
--     CREATION_DATE =  @V_CREATION_DATE,       
--     MODIFIED_BY =  @V_MODIFIED_BY,       
--     MODIFIED_DATE =  @V_MODIFIED_DATE,       
--     DIVISION_ID =  @V_DIVISION_ID       
--     WHERE       
--     PO_ID = @V_PO_ID      
--   END      
      
        IF @V_PROC_TYPE = 3
            BEGIN      
                DELETE  FROM PO_DETAIL
                WHERE   PO_ID = @V_PO_ID      
            END      
      
    END      
      
      
      
      

