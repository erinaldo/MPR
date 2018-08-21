INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0034_14_August_2018_DeliveryNoteCorrectionScript' ,
          GETDATE()
        )
Go


-------- Drop Table Division Master in MMSPLUS---------------

USE [MMSPLUS]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DivisionMaster_is_mms_used]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DivisionMaster] DROP CONSTRAINT [DF_DivisionMaster_is_mms_used]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__DivisionM__fk_Co__6F4A8121]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DivisionMaster] DROP CONSTRAINT [DF__DivisionM__fk_Co__6F4A8121]
END

GO

USE [MMSPLUS]
GO

/****** Object:  Table [dbo].[DivisionMaster]    Script Date: 08/14/2018 12:47:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DivisionMaster]') AND type in (N'U'))
DROP TABLE [dbo].[DivisionMaster]
GO

-------- Create New Table Division Master in MMSPLUS From ADMINPLUS---------------

CREATE TABLE [dbo].[DivisionMaster](
	[Pk_DivisionId_num] [numeric](18, 0) NOT NULL,
	[DivisionName_vch] [varchar](100) NOT NULL,
	[fk_CityId_num] [numeric](18, 0) NOT NULL,
	[Address_vch] [varchar](255) NOT NULL,
	[Phone1_vch] [varchar](15) NOT NULL,
	[Phone2_vch] [varchar](15) NOT NULL,
	[TinNo_vch] [varchar](25) NOT NULL,
	[Division_prefix] [varchar](50) NULL,
	[Indent_prefix] [varchar](50) NULL,
	[Wastage_prefix] [varchar](50) NULL,
	[Rev_wastage_prefix] [varchar](50) NULL,
	[Receipt_prefix] [varchar](50) NULL,
	[MIO_prefix] [varchar](50) NULL,
	[RMIO_prefix] [varchar](50) NULL,
	[MRSMainStorePrefix] [varchar](50) NULL,
	[RMRN_prefix] [varchar](50) NULL,
	[RPMRN_prefix] [varchar](50) NULL,
	[fk_CreatedBy_num] [numeric](18, 0) NOT NULL,
	[CreatedDate_dt] [datetime] NOT NULL,
	[fk_ModifiedBy_num] [numeric](18, 0) NOT NULL,
	[ModifiedDate_dt] [datetime] NOT NULL,
	[is_mms_used] [bit] NULL,
	[ACCOUNT_NO] [varchar](50) NULL,
	[BANK_NAME] [varchar](70) NULL,
	[BRANCH_ADDRESS] [varchar](250) NULL,
	[IFSC_CODE] [varchar](50) NULL,
	[AUTH_SIGNATORY] [varchar](80) NULL,
	[fk_CompanyId_num] [numeric](18, 0) NULL,
	[ADJUSTMENT_PREFIX] [varchar](50) NULL,
	[TRANSFER_PREFIX] [varchar](50) NULL,
	[CLOSING_PREFIX] [varchar](50) NULL,
	[WASTAGE_PREFIX_CC] [varchar](50) NULL,
	[REV_WASTAGE_PREFIX_CC] [varchar](50) NULL,
 CONSTRAINT [PK_DivisionMaster] PRIMARY KEY CLUSTERED 
(
	[Pk_DivisionId_num] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DivisionMaster] ADD  CONSTRAINT [DF_DivisionMaster_is_mms_used]  DEFAULT ((0)) FOR [is_mms_used]
GO

ALTER TABLE [dbo].[DivisionMaster] ADD  DEFAULT ((0)) FOR [fk_CompanyId_num]
GO
