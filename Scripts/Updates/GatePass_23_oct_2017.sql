USE [MMSPlus]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[GatePass_Master](
	[GatePassId] [numeric](18, 0) NOT NULL,
	[GatePassNo] [varchar](50) NULL,
	[GatePassDate] DATETIME NULL,
	[BillNo] [varchar](50) NULL,
	[BillDate] DATETIME NULL,
	[Acc_id] [numeric](18, 0) NULL,
	[VehicalNo] [varchar](50) NULL,
	[EntryDate] DATETIME NULL,
	[InTime]  [varchar](50) NULL,
	[OutTime] [varchar](50) NULL,
	[Remarks] [varchar](500) NULL,
	[CreatedBy] [numeric](18, 0) NULL,
	[CreationDate] DATETIME NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


