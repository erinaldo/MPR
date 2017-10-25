USE [MMSPlus]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

DROP TABLE Gate_Pass_Master

CREATE TABLE [dbo].[GatePass_Master](
	[GatePassId] [numeric](18, 0) NOT NULL,
	[GatePassNo] [varchar](50) NULL,
	[GatePassDate] DATETIME NULL,
	[BillNo] [varchar](50) NULL,
	[BillDate] DATETIME NULL,
	[SI_ID] [numeric](18, 0) NULL,
	[Acc_id] [numeric](18, 0) NULL,
	[VehicalNo] [varchar](50) NULL,
	[EntryDate] DATETIME NULL,
	[InTime]  [varchar](50) NULL,
	[OutTime] [varchar](50) NULL,
	[Remarks] [varchar](500) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreationDate] DATETIME NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
-----------------------------

USE [MMSPlus]
GO

/****** Object:  Table [dbo].[INVOICE_SERIES]    Script Date: 10/25/2017 2:08:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GATEPASS_SERIES](
	[DIV_ID] [numeric](18, 0) NULL,
	[PREFIX] [varchar](50) NULL,
	[START_NO] [numeric](18, 0) NULL,
	[END_NO] [numeric](18, 0) NULL,
	[CURRENT_USED] [numeric](18, 0) NULL,
	[IS_FINISHED] [char](1) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
-----------------------------------------------


CREATE PROCEDURE [dbo].[GET_GATEPASS_NO]  

(  

 @DIV_ID NUMERIC(18,0)  

)  

AS  

BEGIN  

 DECLARE @COUNT NUMERIC(18,0)

  SELECT @COUNT = COUNT(CURRENT_USED) FROM dbo.GATEPASS_SERIES WHERE   

  DIV_ID = @DIV_ID and IS_FINISHED = 'N' 
   

 IF @COUNT = 0   

  BEGIN  

   SELECT '-1','-1'  

  END  

 ELSE  

  BEGIN      

  SELECT @COUNT = COUNT(CURRENT_USED) FROM GATEPASS_SERIES WHERE IS_FINISHED = 'N'  

  AND  

  DIV_ID = @DIV_ID AND CURRENT_USED >= START_NO - 1   

  AND CURRENT_USED < END_NO     

  if @count = 0   

   begin  

    select '-2','-2'  

   end   

  else  

   begin  

    SELECT  PREFIX,CURRENT_USED FROM GATEPASS_SERIES WHERE   

     DIV_ID = @DIV_ID AND IS_FINISHED ='N'  

   END  

  end   

END  

---------------------------------------------------------------------------------------

          
            
CREATE PROCEDURE [dbo].[PROC_GATEPASS_MASTER]
    (
      @GatePassId INT ,
      @GatePassNo VARCHAR(50) ,
      @GatePassDate DATETIME ,
      @BillNo VARCHAR(50) ,
      @BillDate DATETIME ,
      @SI_ID NUMERIC(18, 0) ,
      @Acc_id NUMERIC(18, 0) ,
      @VehicalNo VARCHAR(50) ,
      @EntryDate INT ,
      @InTime VARCHAR(50) ,
      @OutTime VARCHAR(50) ,
      @Remarks VARCHAR(500) ,
      @CreatedBy varchar(50) ,
      @v_Proc_Type INT 	          
    )
AS
    BEGIN            
            
        SELECT  @GatePassId = ISNULL(MAX(GatePassId), 0) + 1
        FROM    GatePass_Master            
           
        INSERT  INTO GatePass_Master
                ( [GatePassId] ,
                  [GatePassNo] ,
                  [GatePassDate] ,
                  [BillNo] ,
                  [BillDate] ,
                  [SI_ID] ,
                  [Acc_id] ,
                  [VehicalNo] ,
                  [EntryDate] ,
                  [InTime] ,
                  [OutTime] ,
                  [Remarks] ,
                  [CreatedBy] ,
                  [CreationDate]    
                )
        VALUES  ( @GatePassId ,
                  @GatePassNo ,
                  @GatePassDate ,
                  @BillNo ,
                  @BillDate ,
                  @SI_ID ,
                  @Acc_id ,
                  @VehicalNo ,
                  @EntryDate ,
                  @InTime ,
                  @OutTime ,
                  @Remarks ,
                  @CreatedBy ,
                  GETDATE()
                )     
						            
        RETURN @GatePassId                       
            
    END 

	-----------------------------------------------------------------


