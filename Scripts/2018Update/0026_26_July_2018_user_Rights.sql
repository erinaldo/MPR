 INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0026_26_July_2018_user_Rights' ,
          GETDATE()
        )
Go

ALTER TABLE user_rights  ADD allow_edit CHAR(1)NOT NULL DEFAULT 'N',
      allow_cancel CHAR(1)NOT NULL DEFAULT 'N'