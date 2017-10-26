ALTER TABLE DebitNote_Master
  ADD  DN_Amount NUMERIC(18,2) NULL,
    DN_CustId NUMERIC(18,0) NULL,
    INV_No varchar(100)  NULL,
    INV_Date DATETIME  NULL;


ALTER TABLE dbo.DebitNote_DETAIL
  ADD  Item_Rate NUMERIC(18,2) NOT NULL,
    Item_Tax NUMERIC(18,2) NOT NULL;



ALTER PROCEDURE [dbo].[Get_MRN_Details_DebitNote]
    @V_MRN_NO NUMERIC(18, 0)
AS 
    BEGIN     
        SELECT  IM.ITEM_ID AS Item_ID,
                IM.ITEM_CODE AS Item_Code,
                IM.ITEM_NAME AS Item_Name,
                IM.UM_Name AS UM_Name,
                SD.Balance_Qty AS Prev_Item_Qty,
				md.Item_Qty AS MRN_Qty,
				md.Item_Rate,
				md.Vat_Per,
                '0.00' AS Item_Qty,
				
                MD.Stock_Detail_Id AS Stock_Detail_Id
        FRoM    MATERIAL_RECEIVED_AGAINST_PO_MASTER MM INNER JOIN   MATERIAL_RECEIVED_AGAINST_PO_DETAIL MD
		ON mm.Receipt_ID=md.Receipt_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON MD.Item_ID = IM.ITEM_ID
                  INNER JOIN STOCK_DETAIL SD ON MD.Stock_Detail_Id=SD.STOCK_DETAIL_ID
        where   MM.MRN_NO = @V_MRN_NO  
		UNION ALL
		  SELECT  IM.ITEM_ID AS Item_ID,
                IM.ITEM_CODE AS Item_Code,
                IM.ITEM_NAME AS Item_Name,
                IM.UM_Name AS UM_Name,
                SD.Balance_Qty AS Prev_Item_Qty,
				md.Item_Qty AS MRN_Qty,
				md.Item_Rate,
				md.Item_vat,
                '0.00' AS Item_Qty,
				
                MD.Stock_Detail_Id AS Stock_Detail_Id
        FRoM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER MM INNER JOIN   dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MD
		ON mm.Received_ID=md.Received_ID
                INNER JOIN vw_ItemMaster_Detail_Unit IM ON MD.Item_ID = IM.ITEM_ID
                  INNER JOIN STOCK_DETAIL SD ON MD.Stock_Detail_Id=SD.STOCK_DETAIL_ID
        where   MM.MRN_NO = @V_MRN_NO  
    END   



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
			
--                select  @V_DebitNote_No = isnull(max(DebitNote_No), 0) + 1
--                from    DebitNote_MASTER
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



ALTER PROCEDURE [dbo].[PROC_DebitNote_DETAIL]
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
                EXEC INSERT_TRANSACTION_LOG @v_DebitNote_ID, @v_Item_ID,
                    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,
                    @v_Creation_Date, 0     
            END    
    END    




CREATE TABLE [dbo].[CreditNote_Master](
	[CreditNote_Id] [numeric](18, 0) NOT NULL,
	[CreditNote_Code] [varchar](20) NULL,
	[CreditNote_No] [numeric](18, 0) NULL,
	[CreditNote_Date] [datetime] NULL,
	[INVId] [numeric](18, 0) NULL,
	[Remarks] [varchar](255) NULL,
	[Created_by] [varchar](100) NULL,
	[Creation_Date] [datetime] NULL,
	[Modified_By] [varchar](100) NULL,
	[Modification_Date] [datetime] NULL,
	[Division_Id] [numeric](18, 0) NULL,
	[CN_Amount] [numeric](18, 2) NOT NULL,
	[CN_CustId] [numeric](18, 0) NOT NULL,
	[INV_No] [varchar](100) NULL,
	[INV_Date] [datetime] NULL,
 CONSTRAINT [PK_CreditNote_Master] PRIMARY KEY CLUSTERED 
(
	[CreditNote_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


CREATE TABLE [dbo].[CreditNote_DETAIL](
	[CreditNote_Id] [numeric](18, 0) NULL,
	[Item_ID] [numeric](18, 0) NULL,
	[Item_Qty] [numeric](18, 4) NULL,
	[Created_By] [varchar](100) NULL,
	[Creation_Date] [datetime] NULL,
	[Modified_By] [varchar](100) NULL,
	[Modification_Date] [datetime] NULL,
	[Division_Id] [int] NULL,
	[Stock_Detail_Id] [numeric](18, 0) NULL,
	[Item_Rate] [numeric](18, 2) NOT NULL,
	[Item_Tax] [numeric](18, 2) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


CREATE PROCEDURE [dbo].[GET_CreditNote_No]
(
	@DIV_ID NUMERIC(18,0)
)
AS
BEGIN
	DECLARE @COUNT NUMERIC(18,0)
		SELECT @COUNT = COUNT(CURRENT_USED) FROM CN_SERIES WHERE 
		DIV_ID = @DIV_ID and IS_FINISHED = 'N'
	
	IF @COUNT = 0 
		BEGIN
			SELECT '-1','-1'
		END
	ELSE
		BEGIN
		
		SELECT @COUNT = COUNT(CURRENT_USED) FROM CN_SERIES WHERE IS_FINISHED = 'N'
		AND
		DIV_ID = @DIV_ID AND CURRENT_USED >= START_NO - 1 
		AND CURRENT_USED < END_NO
	
		if @count = 0 
			begin
				select '-2','-2'
			end 
		else
			begin
				SELECT  PREFIX,CURRENT_USED FROM CN_SERIES WHERE 
					DIV_ID = @DIV_ID AND IS_FINISHED ='N'
			END
		end 
END





CREATE PROCEDURE [dbo].[Get_INV_Details_CreditNote]
    @Si_ID NUMERIC(18, 0)
AS 
    BEGIN     
        SELECT  IM.ITEM_ID AS Item_ID,
                IM.ITEM_CODE AS Item_Code,
                IM.ITEM_NAME AS Item_Name,
                IM.UM_Name AS UM_Name,
                SD.Balance_Qty AS Prev_Item_Qty,
				saleID.Item_Qty AS INV_Qty,
				saleID.Item_Rate,
				saleID.Vat_Per,
                '0.00' AS Item_Qty,				
                SIID.Stock_Detail_Id AS Stock_Detail_Id,
				CONVERT(VARCHAR(20),SIM.CREATION_DATE,106)AS INvDate,
				SI_CODE+CAST(SI_NO as varchar(20)) AS SiNo
         FRoM    dbo.SALE_INVOICE_MASTER SIM INNER JOIN SALE_INVOICE_DETAIL SaleID 
		ON sim.SI_ID=SaleID.SI_ID
		JOIN dbo.SALE_INVOICE_STOCK_DETAIL SIID ON SIID.ITEM_ID = SaleID.ITEM_ID
		AND SIID.SI_ID = SaleID.SI_ID	
            INNER JOIN vw_ItemMaster_Detail_Unit IM ON SaleID.Item_ID = IM.ITEM_ID
               INNER JOIN STOCK_DETAIL SD ON SIID.Stock_Detail_Id=SD.STOCK_DETAIL_ID
        where   SIM.SI_ID = @Si_ID 
		END



CREATE PROCEDURE [dbo].[PROC_CreditNote_MASTER]
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



CREATE PROCEDURE [dbo].[PROC_CreditNote_DETAIL]
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
							       
		
                EXEC INSERT_TRANSACTION_LOG @v_CreditNote_ID, @v_Item_ID,
                    @V_Trans_Type, @v_Stock_Detail_Id, @v_Item_Qty,
                    @v_Creation_Date, 0     
            END    
    END
	





ALTER VIEW [dbo].[vw_ItemRecieveIssue_Details]
AS
    SELECT TOP ( 100 ) PERCENT
            trans_Date ,
            doc_no ,
            Item_Id ,
            Item_Qty ,
            Item_Rate ,
            EntryType ,
            DivId
    FROM    (       
--Material Issued to CostCenter Table      
              SELECT    MM.MIO_DATE AS trans_Date ,
                        MM.MIO_CODE + CAST(MM.MIO_NO AS VARCHAR) AS doc_no ,
                        MD.ITEM_ID AS Item_Id ,
                        MD.ISSUED_QTY AS Item_Qty ,
                        MD.ITEM_RATE AS Item_Rate ,
                        'I' AS EntryType ,
                        MM.DIVISION_ID AS DivId
              FROM      MATERIAL_ISSUE_TO_COST_CENTER_MASTER AS MM
                        INNER JOIN MATERIAL_ISSUE_TO_COST_CENTER_DETAIL AS MD ON MM.MIO_ID = MD.MIO_ID
              UNION ALL    
-- Reverse Material Issued to CostCenter             
              SELECT    RM.RMIO_DATE AS trans_Date ,
                        RM.RMIO_CODE + CAST(RM.RMIO_NO AS VARCHAR) AS doc_no ,
                        RD.ITEM_ID AS Item_Id ,
                        RD.Item_QTY AS Item_Qty ,
                        RD.Avg_RATE AS Item_Rate ,
                        'R' AS EntryType ,
                        RM.DIVISION_ID AS DivId
              FROM      ReverseMaterial_Issue_To_Cost_Center_Master AS RM
                        INNER JOIN ReverseMaterial_Issue_To_Cost_Center_Detail
                        AS RD ON RM.RMIO_ID = RD.RMIO_ID
              UNION     ALL     
--Material Recieved without PO       
              SELECT    MM.Received_Date AS trans_Date ,
                        MM.Received_Code + CAST(MM.Received_No AS VARCHAR) AS doc_no ,
                        MD.Item_ID AS Item_Id ,
                        MD.Item_Qty ,
                        MD.Item_Rate ,
                        'R' AS EntryType ,
                        MM.Division_ID AS DivId
              FROM      MATERIAL_RECIEVED_WITHOUT_PO_MASTER AS MM
                        INNER JOIN MATERIAL_RECEIVED_WITHOUT_PO_DETAIL AS MD ON MM.Received_ID = MD.Received_ID
              UNION     ALL    
--REVERSE Material recieved without PO          
              SELECT    RM.Reverse_Date AS trans_Date ,
                        RM.Reverse_Code + CAST(RM.Reverse_No AS VARCHAR) ,
                        RD.Item_ID AS Item_Id ,
                        RD.Item_Qty - RD.Prev_Item_Qty AS Item_Qty_diff ,
                        CASE WHEN RD.Item_Rate - RD.Prev_Item_Rate = 0
                                  AND RD.Item_Qty - RD.Prev_Item_Qty <> 0
                             THEN RD.Item_Rate
                             ELSE ( ( RD.Item_Qty * RD.Item_Rate )
                                    - ( RD.Prev_Item_Qty * RD.Prev_Item_Rate ) )
                                  / CASE WHEN ( RD.Item_Qty - RD.Prev_Item_Qty ) = 0
                                         THEN 1
                                         ELSE ( RD.Item_Qty - RD.Prev_Item_Qty )
                                    END
                        END AS Item_Rate_new ,
                        'RV' AS EntryType ,
                        RM.Division_ID AS DivId
              FROM      ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER AS RM
                        INNER JOIN ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL
                        AS RD ON RM.Reverse_ID = RD.Reverse_ID
              UNION    ALL    
              --Material Wastage Master    
              SELECT    WM.Wastage_Date AS trans_Date ,
                        WM.Wastage_Code + CAST(WM.Wastage_No AS VARCHAR) AS doc_no ,
                        WD.Item_ID AS Item_Id ,
                        WD.Item_Qty ,
                        WD.Item_Rate ,
                        'I' AS EntryType ,
                        WM.Division_ID AS DivId
              FROM      WASTAGE_MASTER AS WM
                        INNER JOIN WASTAGE_DETAIL AS WD ON WM.Wastage_ID = WD.Wastage_ID
              UNION   ALL    
              --Reverse Material wastage Master    
              SELECT    RM.ReverseWastage_Date AS trans_Date ,
                        RM.ReverseWastage_Code
                        + CAST(RM.ReverseWastage_No AS VARCHAR) AS doc_no ,
                        RD.Item_ID AS Item_Id ,
                        RD.Item_Qty - RD.Actual_Qty AS Item_Qty ,
                        RD.item_Rate AS Item_Rate ,
                        'R' AS EntryType ,
                        RM.Division_ID AS DivId
              FROM      ReverseWASTAGE_MASTER AS RM
                        INNER JOIN ReverseWASTAGE_DETAIL AS RD ON RM.ReverseWastage_ID = RD.f_ReverseWastage_ID
                                                              AND RD.Item_Qty
                                                              - RD.Actual_Qty <> 0
              UNION  ALL    
              --Material Recieved against PO    
              SELECT    MM.Receipt_Date AS trans_Date ,
                        MM.MRN_PREFIX + CAST(MM.MRN_NO AS VARCHAR) AS doc_no ,
                        MD.Item_ID AS Item_Id ,
                        MD.Item_Qty ,
                        ISNULL(MD.Item_Rate, 0) AS Expr1 ,
                        'R' AS EnteryType ,
                        MM.Division_ID AS DivId
              FROM      MATERIAL_RECEIVED_AGAINST_PO_MASTER AS MM
                        INNER JOIN MATERIAL_RECEIVED_AGAINST_PO_DETAIL AS MD ON MM.Receipt_ID = MD.Receipt_ID
              UNION ALL    
    --Reverse Material Recieved agaist PO    
              SELECT    RM.Reverse_Date ,
                        RM.Reverse_Code + CAST(RM.Reverse_No AS VARCHAR) AS doc_no ,
                        RD.Item_ID ,
                        ABS(RD.Prev_Item_Qty - RD.Item_Qty) AS Expr1 ,
                        ISNULL(RD.Item_Rate, 0) AS Expr2 ,
                        CASE WHEN rd.Prev_Item_Qty - rd.Item_Qty >= 0 THEN 'I'
                             WHEN rd.Prev_Item_Qty - rd.Item_Qty < 0 THEN 'R'
                        END AS EnteryType ,
                        RM.Division_ID
              FROM      ReverseMATERIAL_RECIEVED_Against_PO_MASTER AS RM
                        INNER JOIN ReverseMATERIAL_RECEIVED_Against_PO_DETAIL
                        AS RD ON RM.Reverse_ID = RD.Reverse_ID
              WHERE     ( RD.Prev_Item_Qty - RD.Item_Qty <> 0 )
              UNION ALL
              SELECT    AM.Adjustment_Date ,
                        AM.Adjustment_Code + CAST(AM.Adjustment_No AS VARCHAR) AS doc_no ,
                        AD.Item_ID ,
                        ABS(AD.Item_Qty) AS Expr1 ,
                        ISNULL(AD.Item_Rate, 0) AS Expr2 ,
                        CASE WHEN ad.item_qty >= 0 THEN 'R'
                             ELSE 'I'
                        END AS ENTRY_type ,
                        AM.Division_ID
              FROM      ADJUSTMENT_MASTER AS AM
                        INNER JOIN ADJUSTMENT_DETAIL AS AD ON AD.Adjustment_ID = AM.Adjustment_ID
              WHERE     ad.item_qty <> 0
              UNION ALL
              SELECT    M.TRANSFER_DATE ,
                        M.DC_CODE + CAST(M.DC_NO AS VARCHAR) AS doc_no ,
                        D.ITEM_ID ,
                        D.ITEM_QTY ,
                        D.TRANSFER_RATE ,
                        'I' AS entery_type ,
                        M.DIVISION_ID
              FROM      STOCK_TRANSFER_MASTER AS M
                        INNER JOIN STOCK_TRANSFER_DETAIL AS D ON M.TRANSFER_ID = D.TRANSFER_ID
              UNION ALL
              SELECT    M.RECEIVED_DATE ,
                        M.DC_CODE + CAST(M.DC_NO AS VARCHAR) AS doc_no ,
                        D.ITEM_ID ,
                        D.ACCEPTED_QTY ,
                        D.TRANSFER_RATE ,
                        'R' AS entery_type ,
                        M.TRANSFER_OUTLET_ID
              FROM      STOCK_TRANSFER_MASTER AS M
                        INNER JOIN STOCK_TRANSFER_DETAIL AS D ON M.TRANSFER_ID = D.TRANSFER_ID
              UNION ALL
              SELECT    M.DebitNote_Date ,
                        M.DebitNote_Code + CAST(M.DebitNote_No AS VARCHAR) AS doc_no ,
                        D.ITEM_ID ,
                        D.Item_Qty ,
                        D.Item_Rate ,
                        'I' AS entery_type ,
                        M.Division_Id
              FROM      dbo.DebitNote_Master AS M
                        INNER JOIN dbo.DebitNote_DETAIL AS D ON M.DebitNote_Id = D.DebitNote_Id
              UNION ALL
              SELECT    M.CreditNote_Date ,
                        M.CreditNote_Code + CAST(M.CreditNote_No AS VARCHAR) AS doc_no ,
                        D.ITEM_ID ,
                        D.Item_Qty ,
                        D.Item_Rate ,
                        'R' AS entery_type ,
                        M.Division_Id
              FROM      dbo.CreditNote_Master AS M
                        INNER JOIN dbo.CreditNote_DETAIL AS D ON M.CreditNote_Id = D.CreditNote_Id
            ) AS kk
    ORDER BY trans_Date ,
            Item_Id








--sp_helptext Get_Stock_as_on_date 
alter FUNCTION [dbo].[Get_Stock_as_on_date]
    (
      @Item_ID NUMERIC(18, 0) ,
      @Div_ID NUMERIC(18, 0) ,
      @ToDate VARCHAR(13) ,
      @calculate_all_data BIT          
    )
RETURNS NUMERIC(18, 4)
AS
    BEGIN          
        DECLARE @Sum NUMERIC(18, 4)          
        DECLARE @InnerSum NUMERIC(18, 4)          
        DECLARE @FromDate DATETIME          
        SET @Sum = 0          
        SET @InnerSum = 0          
--CLOSING_STOCK_AVG_RATE is used to save the daily basis closing stock and average rate          
--To get the maximum date, whoose closing stock and average rate is saved.          
-- with condition Closing_DATE<=@todate          
        SELECT  @FromDate = ISNULL(MAX(Closing_DATE), '1/1/1900')
        FROM    CLOSING_STOCK_AVG_RATE
        WHERE   CAST(dbo.fn_format(Closing_DATE) AS DATETIME) <= CAST(@ToDate AS DATETIME)
                AND ITEM_ID = @Item_ID          
        IF @FromDate = '1/1/1900'
            BEGIN         
                SET @InnerSum = ( SELECT    OPENING_STOCK
                                  FROM      dbo.ITEM_DETAIL
                                  WHERE     ITEM_ID = @Item_ID
                                )        
            END        
        ELSE
            BEGIN        
--To get the CLOSING_STOCK from CLOSING_STOCK_AVG_RATE with condition Closing_DATE<=@todate          
                SELECT  @InnerSum = ISNULL(CLOSING_STOCK, 0)
                FROM    CLOSING_STOCK_AVG_RATE
                WHERE   CAST(dbo.fn_format(Closing_DATE) AS DATETIME) <= CAST(@ToDate AS DATETIME)
                        AND ITEM_ID = @Item_ID         
            END        
  --if there is no entry in the closing_stock_avg_rate table        
  --then we have to consider the opening stock of that item.        
        IF @calculate_all_data = 1
            BEGIN          
                SET @FromDate = '1/1/1900'          
                SET @Sum = 0          
                SELECT  @InnerSum = OPENING_STOCK
                FROM    dbo.ITEM_DETAIL
                WHERE   ITEM_ID = @Item_ID        
            END          
        SET @Sum = @InnerSum          
        IF dbo.fn_format(@FromDate) != @ToDate
            BEGIN          
   --to get the total recieved qty of specified item from without po           
   --with Received_Date> @FromDate AND Received_Date<=@todate           
                SELECT  @InnerSum = ISNULL(SUM(Item_Qty), 0)
                FROM    MATERIAL_RECIEVED_WITHOUT_PO_MASTER M ,
                        MATERIAL_RECEIVED_WITHOUT_PO_DETAIL D
                WHERE   M.Received_ID = D.Received_ID
                        AND CAST(dbo.fn_format(Received_Date) AS DATETIME) > CAST(dbo.fn_format(@FromDate) AS DATETIME)
                        AND CAST(dbo.fn_format(Received_Date) AS DATETIME) <= CAST(@todate AS DATETIME)
                        AND Item_ID = @Item_ID
                        AND M.Division_ID = @Div_ID                              
                SET @Sum = @Sum + @InnerSum          
            --to get the total modified recieved qty of specified item from without po           
   --with Reverse_Date> @FromDate AND Reverse_Date<=@todate           
                SELECT  @InnerSum = ISNULL(SUM(D.Prev_Item_Qty - D.Item_Qty),
                                           0)
                FROM    dbo.ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER M ,
                        ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL D
                WHERE   M.Reverse_ID = D.Reverse_ID
                        AND CAST(dbo.fn_Format(M.Reverse_Date) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(M.Reverse_Date) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND D.Item_ID = @Item_ID
                        AND M.Division_ID = @Div_ID          
                SET @Sum = @Sum - @InnerSum          
            --to get the total recieved qty of specified item from with po           
   --with Received_Date> @FromDate AND Received_Date<=@todate           
                SELECT  @InnerSum = ISNULL(SUM(item_qty), 0)
                FROM    MATERIAL_RECEIVED_AGAINST_PO_MASTER M ,
                        MATERIAL_RECEIVED_AGAINST_PO_DETAIL D
                WHERE   M.Receipt_ID = D.Receipt_ID
                        AND CAST(dbo.fn_Format(Receipt_Date) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(M.Receipt_Date) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND D.Item_ID = @Item_ID
                        AND M.Division_ID = @Div_ID          
                SET @Sum = @Sum + @InnerSum          
            --Code of Reverse Material Recieved Against PO goes here          
                SELECT  @InnerSum = ISNULL(SUM(D.Prev_Item_Qty - D.Item_Qty),
                                           0)
                FROM    dbo.ReverseMATERIAL_RECIEVED_Against_PO_MASTER M ,
                        ReverseMATERIAL_RECEIVED_Against_PO_DETAIL D
                WHERE   M.Reverse_ID = D.Reverse_ID
                        AND CAST(dbo.fn_Format(M.Reverse_Date) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(M.Reverse_Date) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND D.Item_ID = @Item_ID
                        AND M.Division_ID = @Div_ID          
                SET @Sum = @Sum - @InnerSum                
   --to get the total wastage qty of specified item           
   --with Wastage_Date> @FromDate AND Wastage_Date<=@todate           
                SELECT  @InnerSum = ISNULL(SUM(item_qty), 0)
                FROM    WASTAGE_MASTER M ,
                        WASTAGE_DETAIL D
                WHERE   M.Wastage_ID = D.Wastage_ID
                        AND CAST(dbo.fn_Format(Wastage_Date) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(Wastage_Date) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND Item_ID = @Item_ID
                        AND M.Division_ID = @Div_ID          
                SET @Sum = @Sum - @InnerSum          
       --to get the total reverse wastage qty of specified item           
   --with ReverseWastage_Date> @FromDate AND ReverseWastage_Date<=@todate           
                SELECT  @InnerSum = ISNULL(SUM(item_qty - actual_qty), 0)
                FROM    ReverseWASTAGE_MASTER M ,
                        ReverseWASTAGE_DETAIL D
                WHERE   M.ReverseWastage_ID = D.f_ReverseWastage_ID
                        AND CAST(dbo.fn_Format(ReverseWastage_Date) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(ReverseWastage_Date) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND Item_ID = @Item_ID
                        AND M.Division_ID = @Div_ID          
                SET @Sum = @Sum + @InnerSum                                  
    --to get the total adjusted qty of specified item           
   --with Wastage_Date> @FromDate AND Wastage_Date<=@todate           
                SELECT  @InnerSum = ISNULL(SUM(item_qty), 0)
                FROM    ADJUSTMENT_MASTER M ,
                        ADJUSTMENT_DETAIL D
                WHERE   M.Adjustment_ID = D.Adjustment_ID
                        AND CAST(dbo.fn_Format(Adjustment_Date) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(Adjustment_Date) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND Item_ID = @Item_ID
                        AND M.Division_ID = @Div_ID          
                SET @Sum = @Sum + @InnerSum        
            --to get the total Issue qty of specified item to Cost Center          
   --with MIO_DATE> @FromDate AND MIO_DATE<=@todate           
                SELECT  @InnerSum = ISNULL(SUM(ISSUED_QTY), 0)
                FROM    MATERIAL_ISSUE_TO_COST_CENTER_MASTER M ,
                        MATERIAL_ISSUE_TO_COST_CENTER_DETAIL D
                WHERE   M.MIO_ID = d.MIO_ID
                        AND CAST(dbo.fn_Format(MIO_DATE) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(MIO_DATE) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND Item_ID = @Item_ID
                        AND M.Division_ID = @Div_ID          
                SET @Sum = @Sum - @InnerSum          
            --to get the total reverse Issue qty of specified item from Cost Center          
   --with RMIO_DATE> @FromDate AND RMIO_DATE<=@todate           
                SELECT  @InnerSum = ISNULL(SUM(item_qty), 0)
                FROM    ReverseMaterial_Issue_To_Cost_Center_Master M ,
                        ReverseMaterial_Issue_To_Cost_Center_Detail D
                WHERE   M.RMIO_ID = D.RMIO_ID
                        AND CAST(dbo.fn_Format(RMIO_DATE) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(RMIO_DATE) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND Item_ID = @Item_ID
                        AND M.Division_ID = @Div_ID          
                SET @Sum = @Sum + @InnerSum          
             -- to get the total transfered qty INTER DIVISION  
                SELECT  @InnerSum = ISNULL(SUM(item_qty), 0)
                FROM    STOCK_TRANSFER_MASTER M ,
                        STOCK_TRANSFER_DETAIL D
                WHERE   M.transfer_id = D.transfer_id
                        AND CAST(dbo.fn_Format(TRANSFER_DATE) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(TRANSFER_DATE) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND Item_ID = @Item_ID
                        AND M.Division_ID = @Div_ID   
                SET @Sum = @Sum - @InnerSum        
           -- to get the total accepted qty INTER DIVISION  
                SELECT  @InnerSum = ISNULL(SUM(accepted_qty), 0)
                FROM    STOCK_TRANSFER_MASTER M ,
                        STOCK_TRANSFER_DETAIL D
                WHERE   M.transfer_id = D.transfer_id
                        AND CAST(dbo.fn_Format(RECEIVED_DATE) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(RECEIVED_DATE) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND Item_ID = @Item_ID
                        AND M.TRANSFER_OUTLET_ID = @Div_ID   
                SET @Sum = @Sum + @InnerSum  
				   
	 -- to get the total Invoice qty   
                SELECT  @InnerSum = ISNULL(SUM(item_qty), 0)
                FROM    dbo.SALE_INVOICE_MASTER M ,
                        dbo.SALE_INVOICE_DETAIL D
                WHERE   M.SI_ID = D.SI_ID
                        AND CAST(dbo.fn_Format(D.CREATION_DATE) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(D.CREATION_DATE) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND Item_ID = @Item_ID
                        AND INVOICE_STATUS <> 4
                        AND M.Division_ID = @Div_ID                      
                SET @Sum = @Sum - @InnerSum  
					

					
		  -- To Get the total Debit qty
                
                SELECT  @InnerSum = ISNULL(SUM(ITEM_QTY), 0)
                FROM    dbo.DebitNote_Master M ,
                        dbo.DebitNote_DETAIL D
                WHERE   M.DebitNote_Id = D.DebitNote_Id
                        AND CAST(dbo.fn_Format(DebitNote_Date) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(DebitNote_Date) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND Item_ID = @Item_ID
                        AND M.Division_ID = @Div_ID   
                SET @Sum = @Sum - @InnerSum 


           -- To Get the total Credit qty

                SELECT  @InnerSum = ISNULL(SUM(ITEM_QTY), 0)
                FROM    dbo.CreditNote_Master M ,
                        dbo.CreditNote_DETAIL D
                WHERE   M.CreditNote_Id = D.CreditNote_Id
                        AND CAST(dbo.fn_Format(CreditNote_Date) AS DATETIME) > CAST(dbo.fn_format(@fromdate) AS DATETIME)
                        AND CAST(dbo.fn_Format(CreditNote_Date) AS DATETIME) <= CAST(@Todate AS DATETIME)
                        AND Item_ID = @Item_ID
                        AND M.Division_Id = @Div_ID   

                SET @Sum = @Sum + @InnerSum

            END    
        RETURN @sum    
    END





--sp_helptext GENERATE_ITEM_LEDGER '-1,10,15,116,206,307,322,330,331,341,342,451,1000,1049,1060,1336,1498,1930,2074,2316',  3,  '01-Apr-2010' ,'30-Sep-2010'  
alter PROCEDURE [dbo].[GENERATE_ITEM_LEDGER]
    (
      @Item_ID VARCHAR(MAX) ,
      @Div_ID NUMERIC(18, 0) ,
      @FromDate DATETIME ,
      @ToDate DATETIME                
    )
AS
    BEGIN                
        SELECT  IM.ITEM_ID ,
                IM.ITEM_NAME ,
                um.UM_Name ,
                INN.Transaction_No ,
                dbo.Get_Stock_as_on_date(IM.ITEM_ID, @Div_ID,
                                         dbo.fn_Format(DATEADD(day, -1,
                                                              @FromDate)), 1) AS OPENING_STOCK ,
                dbo.Get_Stock_as_on_date(IM.ITEM_ID, @Div_ID,
                                         dbo.fn_Format(@ToDate), 1) AS Closing_STOCK ,
                INN.TRANSACTION_DATE ,
                INN.TRANSACTION_TYPE ,
                ISNULL(INN.Issue, 0) AS Issue ,
                ISNULL(INN.Received, 0) AS Received ,
                ( SELECT    DIVISION_NAME
                  FROM      DIVISION_SETTINGS
                  WHERE     DIV_ID = @div_id
                ) AS Division_name ,
                CAST(( SELECT   DIVISION_address
                       FROM     DIVISION_SETTINGS
                       WHERE    DIV_ID = @div_id
                     ) AS VARCHAR(MAX)) AS address
        FROM    UNIT_MASTER AS um
                INNER JOIN ( SELECT *
                             FROM   ITEM_MASTER
                             WHERE  item_id IN (
                                    SELECT  [value]
                                    FROM    dbo.fn_split(@ITEM_ID, ',') )
                           ) AS IM ON um.UM_ID = IM.UM_ID
                LEFT OUTER JOIN ( SELECT    MATERIAL_RECEIVED_WITHOUT_PO_DETAIL.Item_ID ,
                                            MATERIAL_RECIEVED_WITHOUT_PO_MASTER.MRN_PREFIX
                                            + CAST(MATERIAL_RECIEVED_WITHOUT_PO_MASTER.MRN_NO AS VARCHAR) AS Transaction_No ,
                                            MATERIAL_RECIEVED_WITHOUT_PO_MASTER.Received_Date AS TRANSACTION_DATE ,
                                            'Material Received Without PO' AS TRANSACTION_TYPE ,
                                            0 AS Issue ,
                                            ISNULL(MATERIAL_RECEIVED_WITHOUT_PO_DETAIL.Item_Qty,
                                                   0) AS Received
                                  FROM      MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                                            INNER JOIN MATERIAL_RECEIVED_WITHOUT_PO_DETAIL ON MATERIAL_RECIEVED_WITHOUT_PO_MASTER.Received_ID = MATERIAL_RECEIVED_WITHOUT_PO_DETAIL.Received_ID
                                  WHERE     ( CAST(dbo.fn_Format(MATERIAL_RECIEVED_WITHOUT_PO_MASTER.Received_Date) AS DATETIME) >= CAST(dbo.fn_Format(@FromDate) AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(MATERIAL_RECIEVED_WITHOUT_PO_MASTER.Received_Date) AS DATETIME) <= CAST(@todate AS DATETIME) )
                                            AND ( MATERIAL_RECEIVED_WITHOUT_PO_DETAIL.Item_ID IN (
                                                  SELECT    [value]
                                                  FROM      dbo.fn_split(@ITEM_ID,
                                                              ',') ) )
                                            AND ( MATERIAL_RECIEVED_WITHOUT_PO_MASTER.Division_ID = @Div_ID )
                                  UNION ALL
                                  SELECT    D.Item_ID ,
                                            M.Reverse_Code
                                            + CAST(M.Reverse_No AS VARCHAR) AS Transaction_No ,
                                            M.Reverse_Date AS TRANSACTION_DATE ,
                                            CASE WHEN ISNULL(d.Prev_Item_Qty,
                                                             0) > ISNULL(d.Item_Qty,
                                                              0)
                                                 THEN 'Rev against MRN '
                                                      + MR.mrn_prefix
                                                      + CAST(MR.mrn_no AS VARCHAR)
                                                 WHEN ISNULL(d.Prev_Item_Qty,
                                                             0) < ISNULL(d.Item_Qty,
                                                              0)
                                                 THEN 'Rev against MRN '
                                                      + MR.mrn_prefix
                                                      + CAST(MR.mrn_no AS VARCHAR)
                                            END AS TRANSACTION_TYPE ,
                                            CASE WHEN ISNULL(d.Prev_Item_Qty,
                                                             0) > ISNULL(d.Item_Qty,
                                                              0)
                                                 THEN ISNULL(d.Prev_Item_Qty
                                                             - d.Item_Qty, 0)
                                                 ELSE 0
                                            END AS issue ,
                                            CASE WHEN ISNULL(d.Prev_Item_Qty,
                                                             0) < ISNULL(d.Item_Qty,
                                                              0)
                                                 THEN ISNULL(d.Item_Qty
                                                             - d.Prev_Item_Qty,
                                                             0)
                                                 ELSE 0
                                            END AS received
                                  FROM      ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER
                                            AS M
                                            INNER JOIN ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL
                                            AS D ON M.Reverse_ID = D.Reverse_ID
                                            INNER JOIN MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                                            AS MR ON M.received_ID = MR.Received_ID
                                  WHERE     ( D.Prev_Item_Qty <> D.Item_Qty )
                                            AND ( CAST(dbo.fn_Format(M.Reverse_Date) AS DATETIME) >= CAST(dbo.fn_Format(@FromDate) AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(M.Reverse_Date) AS DATETIME) <= CAST(@todate AS DATETIME) )
                                            AND ( D.Item_ID IN (
                                                  SELECT    [value]
                                                  FROM      dbo.fn_split(@ITEM_ID,
                                                              ',') ) )
                                            AND ( M.Division_ID = @Div_ID )
                                  UNION ALL
                                  SELECT    MATERIAL_RECEIVED_AGAINST_PO_DETAIL.Item_ID ,
                                            MATERIAL_RECEIVED_AGAINST_PO_MASTER.MRN_PREFIX
                                            + CAST(MATERIAL_RECEIVED_AGAINST_PO_MASTER.MRN_NO AS VARCHAR) AS Transaction_No ,
                                            MATERIAL_RECEIVED_AGAINST_PO_MASTER.Receipt_Date AS TRANSACTION_DATE ,
                                            'Material Received Against PO' AS TRANSACTION_TYPE ,
                                            0 AS Issue ,
                                            ISNULL(MATERIAL_RECEIVED_AGAINST_PO_DETAIL.Item_Qty,
                                                   0) AS Received
                                  FROM      MATERIAL_RECEIVED_AGAINST_PO_MASTER
                                            INNER JOIN MATERIAL_RECEIVED_AGAINST_PO_DETAIL ON MATERIAL_RECEIVED_AGAINST_PO_MASTER.Receipt_ID = MATERIAL_RECEIVED_AGAINST_PO_DETAIL.Receipt_ID
                                  WHERE     ( CAST(dbo.fn_Format(MATERIAL_RECEIVED_AGAINST_PO_MASTER.Receipt_Date) AS DATETIME) >= CAST(dbo.fn_Format(@FromDate) AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(MATERIAL_RECEIVED_AGAINST_PO_MASTER.Receipt_Date) AS DATETIME) <= CAST(@todate AS DATETIME) )
                                            AND ( MATERIAL_RECEIVED_AGAINST_PO_DETAIL.Item_ID IN (
                                                  SELECT    [value]
                                                  FROM      dbo.fn_split(@ITEM_ID,
                                                              ',') ) )
                                            AND ( MATERIAL_RECEIVED_AGAINST_PO_MASTER.Division_ID = @Div_ID )
                                  UNION ALL
                                  SELECT    D.Item_ID ,
                                            M.Reverse_Code
                                            + CAST(M.Reverse_No AS VARCHAR) AS Transaction_No ,
                                            M.Reverse_Date AS TRANSACTION_DATE ,
                                            CASE WHEN ISNULL(d.Prev_Item_Qty,
                                                             0) > ISNULL(d.Item_Qty,
                                                              0)
                                                 THEN 'Rev against POMRN '
                                                      + MR.MRN_PREFIX
                                                      + CAST(MR.MRN_NO AS VARCHAR)
                                                 WHEN ISNULL(d.Prev_Item_Qty,
                                                             0) < ISNULL(d.Item_Qty,
                                                              0)
                                                 THEN 'Rev against POMRN '
                                                      + MR.MRN_PREFIX
                                                      + CAST(MR.MRN_NO AS VARCHAR)
                                            END AS TRANSACTION_TYPE ,
                                            CASE WHEN ISNULL(d.Prev_Item_Qty,
                                                             0) > ISNULL(d.Item_Qty,
                                                              0)
                                                 THEN ISNULL(d.Prev_Item_Qty
                                                             - d.Item_Qty, 0)
                                                 ELSE 0
                                            END AS issue ,
                                            CASE WHEN ISNULL(d.Prev_Item_Qty,
                                                             0) < ISNULL(d.Item_Qty,
                                                              0)
                                                 THEN ISNULL(d.Item_Qty
                                                             - d.Prev_Item_Qty,
                                                             0)
                                                 ELSE 0
                                            END AS received
                                  FROM      ReverseMATERIAL_RECIEVED_Against_PO_MASTER
                                            AS M
                                            INNER JOIN ReverseMATERIAL_RECEIVED_Against_PO_DETAIL
                                            AS D ON M.Reverse_ID = D.Reverse_ID
                                            INNER JOIN MATERIAL_RECEIVED_AGAINST_PO_MASTER
                                            AS MR ON M.received_ID = MR.Receipt_ID
                                  WHERE     ( D.Prev_Item_Qty <> D.Item_Qty )
                                            AND ( CAST(dbo.fn_Format(M.Reverse_Date) AS DATETIME) >= CAST(dbo.fn_Format(@FromDate) AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(M.Reverse_Date) AS DATETIME) <= CAST(@todate AS DATETIME) )
                                            AND ( D.Item_ID IN (
                                                  SELECT    [value]
                                                  FROM      dbo.fn_split(@ITEM_ID,
                                                              ',') ) )
                                            AND ( M.Division_ID = @Div_ID )
                                  UNION ALL
                                  SELECT    WASTAGE_DETAIL.Item_ID ,
                                            WASTAGE_MASTER.Wastage_Code
                                            + CAST(WASTAGE_MASTER.Wastage_No AS VARCHAR) AS Transaction_No ,
                                            WASTAGE_MASTER.Wastage_Date AS TRANSACTION_DATE ,
                                            'Wastage' AS TRANSACTION_TYPE ,
                                            ISNULL(WASTAGE_DETAIL.Item_Qty, 0) AS Issue ,
                                            0 AS Received
                                  FROM      WASTAGE_MASTER
                                            INNER JOIN WASTAGE_DETAIL ON WASTAGE_MASTER.Wastage_ID = WASTAGE_DETAIL.Wastage_ID
                                  WHERE     ( CAST(dbo.fn_Format(WASTAGE_MASTER.Wastage_Date) AS DATETIME) >= CAST(dbo.fn_Format(@fromdate) AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(WASTAGE_MASTER.Wastage_Date) AS DATETIME) <= CAST(@Todate AS DATETIME) )
                                            AND ( WASTAGE_DETAIL.Item_ID IN (
                                                  SELECT    [value]
                                                  FROM      dbo.fn_split(@ITEM_ID,
                                                              ',') ) )
                                            AND ( WASTAGE_MASTER.Division_ID = @Div_ID )
                                  UNION ALL
                                  SELECT    ReverseWASTAGE_DETAIL.Item_ID ,
                                            ReverseWASTAGE_MASTER.ReverseWastage_Code
                                            + CAST(ReverseWASTAGE_MASTER.ReverseWastage_No AS VARCHAR) AS Transaction_No ,
                                            ReverseWASTAGE_MASTER.ReverseWastage_Date AS TRANSACTION_DATE ,
                                            'Rev Wastg for '
                                            + WASTAGE_MASTER_1.Wastage_Code
                                            + CAST(WASTAGE_MASTER_1.Wastage_No AS VARCHAR) AS TRANSACTION_TYPE ,
                                            CASE WHEN ISNULL(ReverseWASTAGE_DETAIL.actual_qty,
                                                             0) > ISNULL(ReverseWASTAGE_DETAIL.Item_Qty,
                                                              0)
                                                 THEN ISNULL(ReverseWASTAGE_DETAIL.actual_qty
                                                             - ReverseWASTAGE_DETAIL.Item_Qty,
                                                             0)
                                                 ELSE 0
                                            END AS issue ,
                                            CASE WHEN ISNULL(ReverseWASTAGE_DETAIL.actual_qty,
                                                             0) < ISNULL(ReverseWASTAGE_DETAIL.Item_Qty,
                                                              0)
                                                 THEN ISNULL(ReverseWASTAGE_DETAIL.Item_Qty
                                                             - ReverseWASTAGE_DETAIL.actual_qty,
                                                             0)
                                                 ELSE 0
                                            END AS received
                                  FROM      ReverseWASTAGE_DETAIL
                                            INNER JOIN ReverseWASTAGE_MASTER ON ReverseWASTAGE_DETAIL.f_ReverseWastage_ID = ReverseWASTAGE_MASTER.ReverseWastage_ID
                                            INNER JOIN WASTAGE_MASTER AS WASTAGE_MASTER_1 ON ReverseWASTAGE_MASTER.WastageId = WASTAGE_MASTER_1.Wastage_ID
                                                              AND CAST(dbo.fn_Format(ReverseWASTAGE_MASTER.ReverseWastage_Date) AS DATETIME) >= CAST(dbo.fn_Format(@fromdate) AS DATETIME)
                                                              AND CAST(dbo.fn_Format(ReverseWASTAGE_MASTER.ReverseWastage_Date) AS DATETIME) <= CAST(@Todate AS DATETIME)
                                                              AND ReverseWASTAGE_DETAIL.Item_ID IN (
                                                              SELECT
                                                              [value]
                                                              FROM
                                                              dbo.fn_split(@ITEM_ID,
                                                              ',') )
                                                              AND ReverseWASTAGE_MASTER.Division_ID = @Div_ID
                                  UNION ALL
                                  SELECT    MATERIAL_ISSUE_TO_COST_CENTER_DETAIL.ITEM_ID ,
                                            MATERIAL_ISSUE_TO_COST_CENTER_MASTER.MIO_CODE
                                            + CAST(MATERIAL_ISSUE_TO_COST_CENTER_MASTER.MIO_NO AS VARCHAR) AS Transaction_No ,
                                            MATERIAL_ISSUE_TO_COST_CENTER_MASTER.MIO_DATE AS TRANSACTION_DATE ,
                                            'Material Issue to Cost Center' AS TRANSACTION_TYPE ,
                                            ISNULL(MATERIAL_ISSUE_TO_COST_CENTER_DETAIL.ISSUED_QTY,
                                                   0) AS Issue ,
                                            0 AS Received
                                  FROM      MATERIAL_ISSUE_TO_COST_CENTER_DETAIL
                                            INNER JOIN MATERIAL_ISSUE_TO_COST_CENTER_MASTER ON MATERIAL_ISSUE_TO_COST_CENTER_DETAIL.MIO_ID = MATERIAL_ISSUE_TO_COST_CENTER_MASTER.MIO_ID
                                  WHERE     ( MATERIAL_ISSUE_TO_COST_CENTER_DETAIL.ITEM_ID IN (
                                              SELECT    [value]
                                              FROM      dbo.fn_split(@ITEM_ID,
                                                              ',') ) )
                                            AND ( MATERIAL_ISSUE_TO_COST_CENTER_MASTER.DIVISION_ID = @Div_ID )
                                            AND ( CAST(dbo.fn_Format(MATERIAL_ISSUE_TO_COST_CENTER_MASTER.MIO_DATE) AS DATETIME) >= CAST(@FromDate AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(MATERIAL_ISSUE_TO_COST_CENTER_MASTER.MIO_DATE) AS DATETIME) <= CAST(@ToDate AS DATETIME) )
                                  UNION ALL
                                  SELECT    ADJUSTMENT_DETAIL.Item_ID ,
                                            ADJUSTMENT_MASTER.Adjustment_Code
                                            + CAST(ADJUSTMENT_MASTER.Adjustment_No AS VARCHAR) AS Transaction_No ,
                                            ADJUSTMENT_MASTER.Adjustment_Date AS TRANSACTION_DATE ,
                                            'Stock Adjustment' AS TRANSACTION_TYPE ,
                                            CASE WHEN ISNULL(( ADJUSTMENT_DETAIL.Item_Qty ),
                                                             0) <= 0
                                                 THEN ABS(ISNULL(( ADJUSTMENT_DETAIL.Item_Qty ),
                                                              0))
                                                 ELSE 0
                                            END AS Issue ,
                                            CASE WHEN ISNULL(( ADJUSTMENT_DETAIL.Item_Qty ),
                                                             0) > 0
                                                 THEN ABS(ISNULL(( ADJUSTMENT_DETAIL.Item_Qty ),
                                                              0))
                                                 ELSE 0
                                            END AS Received
                                  FROM      ADJUSTMENT_DETAIL
                                            INNER JOIN ADJUSTMENT_MASTER ON ADJUSTMENT_DETAIL.Adjustment_ID = ADJUSTMENT_MASTER.Adjustment_ID
                                  WHERE     ( ADJUSTMENT_DETAIL.Item_ID IN (
                                              SELECT    [value]
                                              FROM      dbo.fn_split(@ITEM_ID,
                                                              ',') ) )
                                            AND ( ADJUSTMENT_MASTER.Division_ID = @Div_ID )
                                            AND ( CAST(dbo.fn_Format(ADJUSTMENT_MASTER.Adjustment_Date) AS DATETIME) >= CAST(@FromDate AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(ADJUSTMENT_MASTER.Adjustment_Date) AS DATETIME) <= CAST(@ToDate AS DATETIME) )
                                            AND ADJUSTMENT_DETAIL.Item_Qty <> 0
                                  UNION ALL
                                  SELECT    D.ITEM_ID ,
                                            M.DC_CODE
                                            + CAST(M.DC_NO AS VARCHAR) ,
                                            M.TRANSFER_DATE ,
                                            'Stock Transfer to another Division' AS transaction_type ,
                                            d.item_qty AS issued ,
                                            0 AS recieved
                                  FROM      STOCK_TRANSFER_DETAIL AS D
                                            INNER JOIN STOCK_TRANSFER_MASTER
                                            AS M ON D.TRANSFER_ID = M.TRANSFER_ID
                                  WHERE     m.division_id = @div_id
                                            AND d.item_id IN (
                                            SELECT  [value]
                                            FROM    dbo.fn_split(@ITEM_ID, ',') )
                                            AND ( CAST(dbo.fn_Format(M.TRANSFER_DATE) AS DATETIME) >= CAST(@FromDate AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(M.TRANSFER_DATE) AS DATETIME) <= CAST(@ToDate AS DATETIME) )
                                  UNION ALL
                                  SELECT    D.ITEM_ID ,
                                            M.MRN_PREFIX
                                            + CAST(M.MRN_NO AS VARCHAR) ,
                                            M.RECEIVED_DATE ,
                                            'Stock recieved from another Division' AS transaction_type ,
                                            0 AS issued ,
                                            d.accepted_qty AS recieved
                                  FROM      STOCK_TRANSFER_DETAIL AS D
                                            INNER JOIN STOCK_TRANSFER_MASTER
                                            AS M ON D.TRANSFER_ID = M.TRANSFER_ID
                                  WHERE     m.TRANSFER_OUTLET_ID = @div_id
                                            AND d.item_id IN (
                                            SELECT  [value]
                                            FROM    dbo.fn_split(@ITEM_ID, ',') )
                                            AND ( CAST(dbo.fn_Format(M.RECEIVED_DATE) AS DATETIME) >= CAST(@FromDate AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(M.RECEIVED_DATE) AS DATETIME) <= CAST(@ToDate AS DATETIME) )
                                  UNION ALL
                                  SELECT    D.ITEM_ID ,
                                            M.DebitNote_Code
                                            + CAST(M.DebitNote_No AS VARCHAR) ,
                                            M.DebitNote_Date ,
                                            'Debit Note' AS transaction_type ,
                                            Item_Qty AS issued ,
                                            0 AS recieved
                                  FROM      dbo.DebitNote_DETAIL AS D
                                            INNER JOIN dbo.DebitNote_Master AS M ON D.DebitNote_Id = M.DebitNote_Id
                                  WHERE     m.Division_Id = @div_id
                                            AND d.item_id IN (
                                            SELECT  [value]
                                            FROM    dbo.fn_split(@ITEM_ID, ',') )
                                            AND ( CAST(dbo.fn_Format(M.DebitNote_Date) AS DATETIME) >= CAST(@FromDate AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(M.DebitNote_Date) AS DATETIME) <= CAST(@ToDate AS DATETIME) )

											  UNION ALL
                                  SELECT    D.ITEM_ID ,
                                            M.CreditNote_Code
                                            + CAST(M.CreditNote_No AS VARCHAR) ,
                                            M.CreditNote_Date ,
                                            'Credit Note' AS transaction_type ,
                                            0 AS issued ,
                                            Item_Qty AS recieved
                                  FROM      dbo.CreditNote_DETAIL AS D
                                            INNER JOIN dbo.CreditNote_Master AS M ON D.CreditNote_Id = M.CreditNote_Id
                                  WHERE     m.Division_Id = @div_id
                                            AND d.item_id IN (
                                            SELECT  [value]
                                            FROM    dbo.fn_split(@ITEM_ID, ',') )
                                            AND ( CAST(dbo.fn_Format(M.CreditNote_Date) AS DATETIME) >= CAST(@FromDate AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(M.CreditNote_Date) AS DATETIME) <= CAST(@ToDate AS DATETIME) )
                                  UNION ALL
                                  SELECT    ITEM_ID ,
                                            M.SI_CODE
                                            + CAST(M.SI_NO AS VARCHAR) ,
                                            m.CREATION_DATE ,
                                            'Invoice Qty' AS transaction_type ,
                                            d.ITEM_QTY AS issued ,
                                            0 AS recieved
                                  FROM      dbo.SALE_INVOICE_MASTER AS M
                                            JOIN dbo.SALE_INVOICE_DETAIL AS D ON M.SI_ID = d.SI_ID
                                  WHERE     D.ITEM_ID IN (
                                            SELECT  [value]
                                            FROM    dbo.fn_split(@ITEM_ID, ',') )
                                            AND ( CAST(dbo.fn_Format(M.CREATION_DATE) AS DATETIME) >= CAST(@FromDate AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(M.CREATION_DATE) AS DATETIME) <= CAST(@ToDate AS DATETIME) )
                                  UNION ALL
                                  SELECT    D.ITEM_ID ,
                                            M.RMIO_CODE
                                            + CAST(M.RMIO_NO AS VARCHAR) AS transaction_no ,
                                            M.RMIO_DATE ,
                                            'Rev Issue against '
                                            + MATERIAL_ISSUE_TO_COST_CENTER_MASTER_1.MIO_CODE
                                            + CAST(MATERIAL_ISSUE_TO_COST_CENTER_MASTER_1.MIO_NO AS VARCHAR) AS transaction_type ,
                                            0.0 AS issue ,
                                            D.Item_QTY AS received
                                  FROM      ReverseMaterial_Issue_To_Cost_Center_Master
                                            AS M
                                            INNER JOIN ReverseMaterial_Issue_To_Cost_Center_Detail
                                            AS D ON M.RMIO_ID = D.RMIO_ID
                                            INNER JOIN MATERIAL_ISSUE_TO_COST_CENTER_MASTER
                                            AS MATERIAL_ISSUE_TO_COST_CENTER_MASTER_1 ON M.Issue_Id = MATERIAL_ISSUE_TO_COST_CENTER_MASTER_1.MIO_ID
                                  WHERE     ( CAST(dbo.fn_Format(M.RMIO_DATE) AS DATETIME) >= CAST(dbo.fn_Format(@fromdate) AS DATETIME) )
                                            AND ( CAST(dbo.fn_Format(M.RMIO_DATE) AS DATETIME) <= CAST(@Todate AS DATETIME) )
                                            AND ( D.ITEM_ID IN (
                                                  SELECT    [value]
                                                  FROM      dbo.fn_split(@ITEM_ID,
                                                              ',') ) )
                                            AND ( M.DIVISION_ID = @Div_ID )
                                ) AS INN ON IM.ITEM_ID = INN.Item_ID        
--        WHERE   ( INN.Issue <> 0 )        
--                OR ( INN.Received <> 0 )        
        ORDER BY INN.item_id ,
                INN.TRANSACTION_DATE ,
                INN.Received     
    END   




