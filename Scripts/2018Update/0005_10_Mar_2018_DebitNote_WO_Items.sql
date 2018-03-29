insert INTO dbo.DBScriptUpdateLog
        ( LogFileName, ExecuteDateTime )
VALUES  ( '0005_10_Mar_2018_DebitNote_WO_Items',
          GETDATE()
          )

go
 ALTER TABLE dbo.DebitNote_Master ADD DebitNote_Type varchar(50) Null , RefNo varchar(50) Null,RefDate_dt DateTime Null,Tax_num numeric(18,2) Null
	
GO

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
      @v_DN_ItemValue NUMERIC(18, 2) = 0 ,
      @v_DN_ItemTax NUMERIC(18, 2) = 0 ,
      @v_DN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
      @v_DebitNote_Type VARCHAR(50) = NULL ,
      @v_RefNo VARCHAR(50) = NULL ,
      @v_RefDate_dt DATETIME = GETDATE ,
      @v_Tax_num NUMERIC(18, 2) = 0 ,
      @v_Proc_Type INT
    )
AS
    BEGIN



        IF @V_PROC_TYPE = 1
            BEGIN

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
                          INV_Date ,
                          DebitNote_Type ,
                          RefNo ,
                          RefDate_dt ,
                          Tax_num

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
                          @v_INV_Date ,
                          @v_DebitNote_Type ,
                          @v_RefNo ,
                          @v_RefDate_dt ,
                          @v_Tax_num

                        )

                UPDATE  DN_SERIES
                SET     CURRENT_USED = @V_DebitNote_No
                WHERE   DIV_ID = @V_Division_ID

				--Credit against CreditNote No- MTC/CN/17-18/6


                DECLARE @Remarks VARCHAR(250)
                DECLARE @InputID NUMERIC
                DECLARE @CInputID NUMERIC= 0
                SET @CInputID = 10016
                DECLARE @RoundOff NUMERIC(18, 2)
                DECLARE @CGST_Amount NUMERIC(18, 2)
                SET @CGST_Amount = ( @v_DN_ItemTax / 2 )
               
                SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1
                                                     THEN 10020
                                                     WHEN MRN_TYPE = 2
                                                     THEN 10023
                                                     WHEN MRN_TYPE = 3
                                                     THEN 10074
                                                END AS inputid
                                        FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                                        WHERE   MRN_NO = @V_MRN_Id
                                      ), 0)
                IF @InputID = 0
                    BEGIN
                        SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1
                                                             THEN 10020
                                                             WHEN MRN_TYPE = 2
                                                             THEN 10023
                                                             WHEN MRN_TYPE = 3
                                                             THEN 10074
                                                        END AS inputid
                                                FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
                                                WHERE   MRN_NO = @V_MRN_Id
                                              ), 0)
                    END


                DECLARE @V_MRN_TYPE NUMERIC= 0

                SET @V_MRN_TYPE = ( SELECT  MRN_TYPE
                                    FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                                    WHERE   MRN_NO = @V_MRN_Id
                                    UNION
                                    SELECT  MRN_TYPE
                                    FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
                                    WHERE   MRN_NO = @V_MRN_Id
                                  )

                SET @Remarks = 'Debit  against DebitNote No-. -'
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))


                EXECUTE Proc_Ledger_Insert @v_DN_CustId, 0, @v_DN_Amount,
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, 7,
                    @V_Created_BY


                EXECUTE Proc_Ledger_Insert 10070, @v_DN_ItemValue, 0, @Remarks,
                    @V_Division_ID, @V_DebitNote_ID, 7, @V_Created_BY



                SET @Remarks = 'GST against DebitNote No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))

                IF @V_MRN_TYPE <> 2
                    BEGIN



                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID, 7,
                            @V_Created_BY


                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,
                            @Remarks, @v_Division_ID, @V_DebitNote_ID, 7,
                            @V_Created_BY
                    END



                ELSE
                    BEGIN
                        EXECUTE Proc_Ledger_Insert @InputID, @v_DN_ItemTax, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID, 7,
                            @V_Created_BY
                    END   


                SET @Remarks = 'Stock Out against Debit Note No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))



                EXECUTE Proc_Ledger_Insert 10073, 0, @v_DN_Amount, @Remarks,
                    @V_Division_ID, @V_DebitNote_ID, 7, @V_Created_BY

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
                        INV_Date = @v_INV_Date ,
                        DebitNote_Type = @v_DebitNote_Type ,
                        RefNo = @v_RefNo ,
                        RefDate_dt = @v_RefDate_dt ,
                        Tax_num = @v_Tax_num
                WHERE   DebitNote_ID = @V_DebitNote_ID

            END
    END
GO
