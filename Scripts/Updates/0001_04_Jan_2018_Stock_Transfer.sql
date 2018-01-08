USE AdminPlus
ALTER TABLE	 STOCK_TRANSFER_MASTER ADD TYPE CHAR(1) NULL

--------------------------------------------------------------------------------------------------------

USE MMSPlus
ALTER TABLE	 STOCK_TRANSFER_MASTER ADD TYPE CHAR(1) NULL

--------------------------------------------------------------------------------------------------------

USE MMSPlus

ALTER PROC [dbo].[PROC_STOCK_TRANSFER_MASTER]
    @Transfer_ID INT ,
    @DC_Code NVARCHAR(50) ,
    @DC_No INT ,
    @Transfer_Date DATETIME ,
    @Transfer_Outlet_Id INT ,
    @Transfer_Status_Id INT ,
    @TRANSFER_REMARKS VARCHAR(500) ,
    @Received_Date DATETIME ,
    @Freezed_Date DATETIME ,
    @Created_By NVARCHAR(100) ,
    @Creation_Date DATETIME ,
    @Modified_By NVARCHAR(100) ,
    @Modification_Date DATETIME ,
    @Division_ID INT ,
	@TYPE CHAR(1),
    @PROC_TYPE INT
AS
    BEGIN    

        IF @PROC_TYPE = 1 -- INSERT     
            BEGIN    

                INSERT  INTO STOCK_TRANSFER_MASTER
                        ( TRANSFER_ID ,
                          DC_CODE ,
                          DC_NO ,
                          TRANSFER_DATE ,
                          TRANSFER_OUTLET_ID ,
                          TRANSFER_STATUS ,
                          TRANSFER_REMARKS ,
                          RECEIVED_DATE ,
                          FREEZED_DATE ,
                          MRN_NO ,
                          MRN_PREFIX ,
                          CREATED_BY ,
                          CREATION_DATE ,
                          MODIFIED_BY ,
                          MODIFIED_DATE ,
                          DIVISION_ID,
						  TYPE    

                        )
                VALUES  ( @Transfer_ID ,
                          @DC_Code ,
                          @DC_No ,
                          @Transfer_Date ,
                          @Transfer_Outlet_Id ,
                          @Transfer_Status_Id ,
                          @TRANSFER_REMARKS ,
                          @Received_Date ,
                          @Freezed_Date ,
                          -1 ,
                          '' ,
                          @Created_By ,
                          @Creation_Date ,
                          @Modified_By ,
                          @Modification_Date ,
                          @Division_ID,
						  @TYPE    

                        )  
            END  
    END
------------------------------------------------------------------------------------------------------------------------------

USE AdminPlus

ALTER PROC [dbo].[PROC_STOCK_TRANSFER_MASTER]
    @Transfer_ID INT ,
    @DC_Code NVARCHAR(50) ,
    @DC_No INT ,
    @Transfer_Date DATETIME ,
    @Transfer_Outlet_Id INT ,
    @Transfer_Status_Id INT ,
    @TRANSFER_REMARKS VARCHAR(500) ,
    @Received_Date DATETIME ,
    @Freezed_Date DATETIME ,
    @Created_By NVARCHAR(100) ,
    @Creation_Date DATETIME ,
    @Modified_By NVARCHAR(100) ,
    @Modification_Date DATETIME ,
    @Division_ID INT ,
    @TYPE CHAR(1) ,
    @PROC_TYPE INT
AS
    BEGIN    

        IF @PROC_TYPE = 1 -- INSERT     
            BEGIN    

                INSERT  INTO STOCK_TRANSFER_MASTER
                        ( TRANSFER_ID ,
                          DC_CODE ,
                          DC_NO ,
                          TRANSFER_DATE ,
                          TRANSFER_OUTLET_ID ,
                          TRANSFER_STATUS ,
                          TRANSFER_REMARKS ,
                          RECEIVED_DATE ,
                          FREEZED_DATE ,
                          MRN_NO ,
                          MRN_PREFIX ,
                          CREATED_BY ,
                          CREATION_DATE ,
                          MODIFIED_BY ,
                          MODIFIED_DATE ,
                          DIVISION_ID ,
                          TYPE    

                        )
                VALUES  ( @Transfer_ID ,
                          @DC_Code ,
                          @DC_No ,
                          @Transfer_Date ,
                          @Transfer_Outlet_Id ,
                          @Transfer_Status_Id ,
                          @TRANSFER_REMARKS ,
                          @Received_Date ,
                          @Freezed_Date ,
                          -1 ,
                          '' ,
                          @Created_By ,
                          @Creation_Date ,
                          @Modified_By ,
                          @Modification_Date ,
                          @Division_ID ,
                          @TYPE 
                        ) 
            END  
    END

---------------------------------------------------------------------------------------------------------------------------------

ALTER PROC [dbo].[PROC_ACCEPT_STOCK_TRANSFER_MASTER]
    @Transfer_ID INT ,
    @DC_Code NVARCHAR(50) ,
    @DC_No NUMERIC(18, 0) ,
    @Transfer_Date DATETIME ,
    @Transfer_Outlet_Id INT ,
    @Transfer_Status_Id INT ,
    @TRANSFER_REMARKS VARCHAR(500) ,
    @MRN_REMARKS VARCHAR(500) ,
    @Received_Date DATETIME ,
    @Freezed_Date DATETIME ,
    @Created_By NVARCHAR(100) ,
    @Creation_Date DATETIME ,
    @Modified_By NVARCHAR(100) ,
    @Modification_Date DATETIME ,
    @Division_ID INT ,
    @MRN_NO NUMERIC(18, 0) ,
    @MRN_PREFIX NVARCHAR(50) ,
    @TYPE CHAR(1)
AS
    BEGIN  
        INSERT  INTO STOCK_TRANSFER_MASTER
                ( TRANSFER_ID ,
                  DC_CODE ,
                  DC_NO ,
                  TRANSFER_DATE ,
                  TRANSFER_OUTLET_ID ,
                  TRANSFER_STATUS ,
                  TRANSFER_REMARKS ,
                  MRN_REMARKS ,
                  RECEIVED_DATE ,
                  FREEZED_DATE ,
                  MRN_NO ,
                  MRN_PREFIX ,
                  CREATED_BY ,
                  CREATION_DATE ,
                  MODIFIED_BY ,
                  MODIFIED_DATE ,
                  DIVISION_ID,
				  TYPE
                )
        VALUES  ( @Transfer_ID ,
                  @DC_Code ,
                  @DC_No ,
                  @Transfer_Date ,
                  @Transfer_Outlet_Id ,
                  @Transfer_Status_Id ,
                  @TRANSFER_REMARKS ,
                  @MRN_REMARKS ,
                  @Received_Date ,
                  @Freezed_Date ,
                  @MRN_NO ,
                  @MRN_PREFIX ,
                  @Created_By ,
                  @Creation_Date ,
                  @Modified_By ,
                  @Modification_Date ,
                  @Division_ID,       
				  @TYPE
                )        

               

    END
--------------------------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[GET_ACCEPT_STOCK_TRANSFER_DETAIL] @DIV_ID INT
AS
    BEGIN
        SELECT  dbo.fn_Format(CREATION_DATE) AS CREATION_DATE ,
                dbo.fn_Format(TRANSFER_DATE) AS TRANSFER_DATE ,
                STM.MRN_PREFIX + CAST(STM.MRN_NO AS VARCHAR) AS MRNCODE ,
                STM.CREATED_BY --(SELECT DIVISION_NAME FROM DIVISION_SETTINGS DM WHERE DM.DIV_ID=STM.DIVISION_ID) AS 
                ,
                DC_CODE + CAST(DC_NO AS VARCHAR) AS DC_NO ,
                CASE WHEN STM.TRANSFER_STATUS = 1 THEN 'Fresh'
                     WHEN STM.TRANSFER_STATUS = 2 THEN 'Accepted'
                     WHEN STM.TRANSFER_STATUS = 3 THEN 'Freezed'
                     ELSE 'Cancel'
                END AS TRANSFER_STATUS
        FROM    dbo.STOCK_TRANSFER_MASTER STM
        WHERE   STM.TRANSFER_OUTLET_ID = @DIV_ID 
		AND TYPE='W'
    END
---------------------------------------------------------------------------------------------------------------------------------------------