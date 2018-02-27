insert INTO dbo.DBScriptUpdateLog
        ( LogFileName, ExecuteDateTime )
VALUES  ( '0004_27_Feb_2018_DBbackup',
          GETDATE()
          )

go
 
	
CREATE TABLE [dbo].[DBBackupDetails](
	[FileName_vch] [varchar](100) NOT NULL,
	[OnLocaLoaded_bit] [bit] NOT NULL,
	[OnOnlineLoaded_bit] [bit] NOT NULL,
	[BackupStatus_vch] [varchar](500) NOT NULL,
	[TimeStamp_dt] [datetime] NOT NULL
) ON [PRIMARY]
