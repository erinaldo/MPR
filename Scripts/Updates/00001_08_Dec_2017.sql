ALTER	 PROCEDURE [dbo].[INSERT_DIVISION_SETTINGS]

	-- Add the parameters for the stored procedure here
    @DIV_ID INT ,
    @DIVISION_NAME VARCHAR(200) ,
    @DIVISION_ADDRESS VARCHAR(MAX) ,
    @TIN_NO VARCHAR(50) ,
    @PHONE1 VARCHAR(50) ,
    @PHONE2 VARCHAR(50) ,
    @ZIP_CODE VARCHAR(15) ,
    @MAIL_ADD VARCHAR(50) ,
    @CITY_ID NUMERIC(18, 0) ,
    @COST_CENTER_PREFIX VARCHAR(50) ,
    @INDENT_PREFIX VARCHAR(50) ,
    @WASTAGE_PREFIX VARCHAR(50) ,
    @REV_WASTAGE_PREFIX VARCHAR(50) ,
    @RECEIPT_PREFIX VARCHAR(50) ,
    @MIO_PREFIX VARCHAR(50) ,
    @RMIO_PREFIX VARCHAR(50) ,
    @MRSMainStorePREFIX VARCHAR(50) ,
    @RMRN_PREFIX VARCHAR(50) ,
    @RPMRN_PREFIX VARCHAR(50) ,
    @ADJUSTMENT_PREFIX VARCHAR(50) ,
    @TRANSFER_PREFIX VARCHAR(50) ,
    @CLOSING_PREFIX VARCHAR(50) ,
    @WASTAGE_PREFIX_CC VARCHAR(50) ,
    @REV_WASTAGE_PREFIX_CC VARCHAR(50) ,
    @BANK_NAME VARCHAR(50) ,
    @ACCOUNT_NO VARCHAR(50) ,
    @BRANCH_ADDRESS VARCHAR(50) ,
    @IFSC_CODE VARCHAR(50) ,
    @AUTH_SIGNATORY VARCHAR(50) ,
    @fk_CompanyId_num NUMERIC ,
    @TYPE INT
AS
    BEGIN

        IF @TYPE = 1 -- insert
            BEGIN

                INSERT  INTO dbo.DIVISION_SETTINGS
                        ( DIV_ID ,
                          DIVISION_NAME ,
                          DIVISION_ADDRESS ,
                          TIN_NO ,
                          PHONE1 ,
                          PHONE2 ,
                          ZIP_CODE ,
                          MAIL_ADD ,
                          CITY_ID ,
                          COST_CENTER_PREFIX ,
                          INDENT_PREFIX ,
                          WASTAGE_PREFIX ,
                          REV_WASTAGE_PREFIX ,
                          RECEIPT_PREFIX ,
                          MIO_PREFIX ,
                          RMIO_Prefix ,
                          MRSMainStorePREFIX ,
                          RMRN_Prefix ,
                          RPMRN_Prefix ,
                          CREATED_BY ,
                          CREATION_DATE ,
                          MODIFIED_BY ,
                          MODIFIED_DATE ,
                          ADJUSTMENT_PREFIX ,
                          TRANSFER_PREFIX ,
                          CLOSING_PREFIX ,
                          WASTAGE_PREFIX_CC ,
                          REV_WASTAGE_PREFIX_CC ,
                          BANK_NAME ,
                          ACCOUNT_NO ,
                          BRANCH_ADDRESS ,
                          IFSC_CODE ,
                          AUTH_SIGNATORY ,
                          fk_CompanyId_num ,
                          ITEM_RATE_MIN_PER ,
                          ITEM_RATE_MAX_PER
						  

				        )
                VALUES  ( @DIV_ID ,
                          @DIVISION_NAME ,
                          @DIVISION_ADDRESS ,
                          @TIN_NO ,
                          @PHONE1 ,
                          @PHONE2 ,
                          @ZIP_CODE ,
                          @MAIL_ADD ,
                          @CITY_ID ,
                          @COST_CENTER_PREFIX ,
                          @INDENT_PREFIX ,
                          @WASTAGE_PREFIX ,
                          @REV_WASTAGE_PREFIX ,
                          @RECEIPT_PREFIX ,
                          @MIO_PREFIX ,
                          @RMIO_Prefix ,
                          @MRSMainStorePREFIX ,
                          @RMRN_Prefix ,
                          @MIO_PREFIX ,

					/* CREATED_BY - numeric(18, 0) */
                          @DIV_ID ,

					/* CREATION_DATE - datetime */
                          GETDATE() ,

					/* MODIFIED_BY - numeric(18, 0) */
                          0 ,

					/* MODIFIED_DATE - datetime */
                          GETDATE() ,
                          @ADJUSTMENT_PREFIX ,
                          @TRANSFER_PREFIX ,
                          @CLOSING_PREFIX ,
                          @WASTAGE_PREFIX_CC ,
                          @REV_WASTAGE_PREFIX_CC ,
                          @BANK_NAME ,
                          @ACCOUNT_NO ,
                          @BRANCH_ADDRESS ,
                          @IFSC_CODE ,
                          @AUTH_SIGNATORY ,
                          @fk_CompanyId_num ,
                          0 ,
                          0
                        ) 

            END

        IF @TYPE = 2 -- update
            BEGIN

                UPDATE  dbo.DIVISION_SETTINGS
                SET     DIVISION_NAME = @DIVISION_NAME ,
                        DIVISION_ADDRESS = @DIVISION_ADDRESS ,
                        TIN_NO = @TIN_NO ,
                        PHONE1 = @PHONE1 ,
                        PHONE2 = @PHONE2 ,
                        ZIP_CODE = @ZIP_CODE ,
                        MAIL_ADD = @MAIL_ADD ,
                        CITY_ID = @CITY_ID ,
                        COST_CENTER_PREFIX = @COST_CENTER_PREFIX ,
                        INDENT_PREFIX = @INDENT_PREFIX ,
                        WASTAGE_PREFIX = @WASTAGE_PREFIX ,
                        REV_WASTAGE_PREFIX = @REV_WASTAGE_PREFIX ,
                        RECEIPT_PREFIX = @RECEIPT_PREFIX ,
                        MIO_PREFIX = @MIO_PREFIX ,
                        RMIO_Prefix = @RMIO_Prefix ,
                        MRSMainStorePREFIX = @MRSMainStorePREFIX ,
                        RMRN_Prefix = @RMRN_Prefix ,
                        RPMRN_Prefix = @RPMRN_PREFIX ,
                        MODIFIED_DATE = GETDATE() ,
                        ADJUSTMENT_PREFIX = @ADJUSTMENT_PREFIX ,
                        TRANSFER_PREFIX = @TRANSFER_PREFIX ,
                        CLOSING_PREFIX = @CLOSING_PREFIX ,
                        WASTAGE_PREFIX_CC = @WASTAGE_PREFIX_CC ,
                        REV_WASTAGE_PREFIX_CC = @REV_WASTAGE_PREFIX_CC ,
                        BANK_NAME = @BANK_NAME ,
                        ACCOUNT_NO = @ACCOUNT_NO ,
                        BRANCH_ADDRESS = @BRANCH_ADDRESS ,
                        IFSC_CODE = @IFSC_CODE ,
                        AUTH_SIGNATORY = @AUTH_SIGNATORY ,
                        fk_CompanyId_num = @fk_CompanyId_num
                WHERE   DIV_ID = @DIV_ID

            END

    END


-------------------------------------------------------------------------------------------------------------------------------