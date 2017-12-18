CREATE TABLE [dbo].[DepreciationDetail](
	[AccountId] [numeric](18, 0) NOT NULL,
	[Rate] [numeric](18, 2) NOT NULL,
	[OpeningBalance] [numeric](18, 2) NOT NULL,
	[Add1stHalf] [numeric](18, 2) NOT NULL,
	[Add2ndHalf] [numeric](18, 2) NOT NULL,
	[SoldValue] [numeric](18, 2) NOT NULL,
	[DepreciationAmount] [numeric](18, 2) NOT NULL,
	[FYBeginDate] [date] NOT NULL,
	[DepreciationId] [numeric](18, 0) NULL
) ON [PRIMARY]