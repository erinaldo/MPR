INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0041_23_Oct_2018_DebitNote_CreditNoteBugFix ' ,
          GETDATE()
        )
Go

-------------------------------------------------------------------------------------------------------------------------------
ALTER FUNCTION [dbo].[Get_GST_Type]
    (
      @AccountId NUMERIC(18, 0)
    )
RETURNS NUMERIC(18, 0)
AS
    BEGIN
    
        DECLARE @GstType NUMERIC(18, 0)
        DECLARE @DivStateId AS NUMERIC
        DECLARE @IsInUt BIT
        DECLARE @AccountStateId AS NUMERIC
        SET @DivStateId = ( SELECT  STATE_ID
                            FROM    dbo.CITY_MASTER
                            WHERE   CITY_ID IN ( SELECT CITY_ID
                                                 FROM   dbo.DIVISION_SETTINGS )
                          )
                  
        SET @AccountStateId = ( SELECT  STATE_ID
                                FROM    dbo.CITY_MASTER
                                WHERE   CITY_ID IN (
                                        SELECT  CITY_ID
                                        FROM    dbo.ACCOUNT_MASTER
                                        WHERE   ACC_ID = @AccountId )
                              )

        SET @IsInUt = ( SELECT  IsUT_Bit
                        FROM    dbo.STATE_MASTER
                        WHERE   STATE_ID = @DivStateId
                      )
 
           
        SELECT  @GstType = ( SELECT CASE WHEN @DivStateId <> @AccountStateId
                                         THEN 2
                                         ELSE CASE WHEN @IsInUt = 1 THEN 3
                                                   ELSE 1
                                              END
                                    END AS GstType
                           )
        RETURN @GstType
    
    END

Go
-----------------------Credit Note----------------------------------  
ALTER PROCEDURE [dbo].[PROC_CreditNote_MASTER]
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
      @v_CN_ItemValue NUMERIC(18, 2) = 0 ,
      @v_CN_ItemTax NUMERIC(18, 2) = 0 ,
      @v_CN_ItemCess NUMERIC(18, 2) = 0 ,
      @v_CN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
      @v_CreditNote_Type VARCHAR(20) = NULL ,
      @v_RefNo VARCHAR(50) = NULL ,
      @v_RefDate_dt DATETIME ,
      @V_Trans_Type NUMERIC(18, 0) ,
      @v_RoundOff NUMERIC(18, 2) ,
      @v_Proc_Type INT                
    )
AS
    BEGIN          
          
        DECLARE @V_GSTType NUMERIC(18, 0)       
        DECLARE @Remarks VARCHAR(250)             
        DECLARE @OutputID NUMERIC                
        DECLARE @COutputID NUMERIC                
        SET @COutputID = 10017           
        DECLARE @CGST_Amount NUMERIC(18, 2)         
        DECLARE @v_INV_TYPE VARCHAR(1)          
        DECLARE @CessOutputID NUMERIC = 0                  
        SET @CessOutputID = 10014        
                   
        SET @V_GSTType = ( SELECT   dbo.Get_GST_Type(@v_CN_CustId)
                         )      
                               
        IF @V_PROC_TYPE = 1
            BEGIN          
                     
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
                          INV_Date ,
                          CreditNote_Type ,
                          RefNo ,
                          RefDate_dt ,
                          Tax_Amt ,
                          Cess_Amt ,
                          RoundOff ,
                          GSTType                
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
                          @v_INV_Date ,
                          @v_CreditNote_Type ,
                          @v_RefNo ,
                          @v_RefDate_dt ,
                          @v_CN_ItemTax ,
                          @v_CN_ItemCess ,
                          @v_RoundOff ,
                          @V_GSTType                
                        )                
                
                UPDATE  CN_SERIES
                SET     CURRENT_USED = @V_CreditNote_No
                WHERE   DIV_ID = @V_Division_ID                  
                
                               
                SET @Remarks = 'Credit against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))            
                
                
                EXECUTE Proc_Ledger_Insert @v_CN_CustId, @v_CN_Amount, 0,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY                
                
                EXECUTE Proc_Ledger_Insert 10071, 0, @v_CN_ItemValue, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY        
                    
                               
                SET @CGST_Amount = ( @v_CN_ItemTax / 2 )          
                
                
----------------------------------------------------------------------------------------------------
                IF @v_INV_Id > 0
                    BEGIN
                        SET @OutputID = ( SELECT    CASE WHEN INV_TYPE = 'I'
                                                         THEN 10021
                                                         WHEN INV_TYPE = 'S'
                                                         THEN 10024
                                                         WHEN INV_TYPE = 'U'
                                                         THEN 10075
                                                    END AS inputid
                                          FROM      dbo.SALE_INVOICE_MASTER
                                          WHERE     SI_ID = @v_INV_Id
                                        )         
                          
                        SET @v_INV_TYPE = ( SELECT  INV_TYPE
                                            FROM    dbo.SALE_INVOICE_MASTER
                                            WHERE   SI_ID = @v_INV_Id
                                          )                
                
                    END
                ELSE
                    BEGIN
                              
                        SET @v_INV_TYPE = ( SELECT  CASE WHEN GType = 2
                                                         THEN 'I'
                                                         WHEN GType = 1
                                                         THEN 'S'
                                                         ELSE 'U'
                                                    END AS Gtype
                                            FROM    ( SELECT  dbo.Get_GST_Type(@v_CN_CustId) AS GType
                                                    ) tb
                                          )
                                          
                                          
                        SET @OutputID = ( SELECT    CASE WHEN @v_INV_TYPE = 'I'
                                                         THEN 10021
                                                         WHEN @v_INV_TYPE = 'S'
                                                         THEN 10024
                                                         WHEN @v_INV_TYPE = 'U'
                                                         THEN 10075
                                                    END AS inputid
                                        )     
                                          
                    END
                
                
                -----------------------------------------------------------------------------------------
                
                
                SET @Remarks = 'GST against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                
                
                IF @v_INV_TYPE <> 'I'
                    BEGIN                
                        EXECUTE Proc_Ledger_Insert @COutputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY               
                
                        EXECUTE Proc_Ledger_Insert @OutputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                
                    END                
                ELSE
                    BEGIN                
                        EXECUTE Proc_Ledger_Insert @OutputID, 0, @v_CN_ItemTax,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                 
                    END           
                              
                              
                SET @Remarks = 'CESS against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))            
                           
                EXECUTE Proc_Ledger_Insert @CessOutputID, 0, @v_CN_ItemCess,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY             
                              
                              
                SET @Remarks = 'Stock Out against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                
                
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_CN_Amount, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY      
                          
                SET @Remarks = 'Round Off against Credit Note No- '
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                               
            
                IF @v_RoundOff > 0
                    BEGIN                      
                      
                        EXECUTE Proc_Ledger_Insert 10054, 0, @v_RoundOff,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                  
                   
                    END                      
                      
                ELSE
                    BEGIN                      
                      
                        SET @v_RoundOff = -+@v_RoundOff                      
                      
                        EXECUTE Proc_Ledger_Insert 10054, @v_RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                  
                      
                    END           
                            
            END           
                
        IF @V_PROC_TYPE = 2
            BEGIN         
                    
                EXECUTE [Proc_CreditNoteUpdateDeleteLedgerEntries] @V_CreditNote_ID,
                    @V_Trans_Type                 
                
                UPDATE  dbo.CreditNote_Master
                SET     CreditNote_No = @V_CreditNote_No ,
                        CreditNote_Code = @V_CreditNote_Code ,
                        CreditNote_Date = @v_CreditNote_Date ,
                        Remarks = @V_Remarks ,
                        INVId = @V_INV_Id ,
                        Modified_By = @V_Modified_By ,
                        Modification_Date = @V_Modification_Date ,
                        CN_Amount = @v_CN_Amount ,
                        CN_CustId = @v_CN_CustId ,
                        Division_ID = @V_Division_ID ,
                        INV_No = @v_INV_No ,
                        INV_Date = @v_INV_Date ,
                        CreditNote_Type = @v_CreditNote_Type ,
                        RefNo = @v_RefNo ,
                        RefDate_dt = @v_RefDate_dt ,
                        Tax_Amt = @v_CN_ItemTax ,
                        Cess_Amt = @v_CN_ItemCess ,
                        RoundOff = @v_RoundOff ,
                        GSTType = @v_GSTType
                WHERE   CreditNote_ID = @V_CreditNote_ID                         
                        
                SET @Remarks = 'Credit against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))            
                
                
                EXECUTE Proc_Ledger_Insert @v_CN_CustId, @v_CN_Amount, 0,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY                
                
                EXECUTE Proc_Ledger_Insert 10071, 0, @v_CN_ItemValue, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY        
                      
                               
                SET @CGST_Amount = ( @v_CN_ItemTax / 2 )        
                
 -----------------------------------------------------------------------------------------------------------------               
                IF @v_Inv_Id > 0
                    BEGIN
                
                        SET @OutputID = ( SELECT    CASE WHEN INV_TYPE = 'I'
                                                         THEN 10021
                                                         WHEN INV_TYPE = 'S'
                                                         THEN 10024
                                                         WHEN INV_TYPE = 'U'
                                                         THEN 10075
                                                    END AS inputid
                                          FROM      dbo.SALE_INVOICE_MASTER
                                          WHERE     SI_ID = @v_INV_Id
                                        )         
                          
                        SET @v_INV_TYPE = ( SELECT  INV_TYPE
                                            FROM    dbo.SALE_INVOICE_MASTER
                                            WHERE   SI_ID = @v_INV_Id
                                          )                
                
                    END
                    
                ELSE
                    BEGIN
                    
                        SET @v_INV_TYPE = ( SELECT  CASE WHEN GType = 2
                                                         THEN 'I'
                                                         WHEN GType = 1
                                                         THEN 'S'
                                                         ELSE 'U'
                                                    END AS Gtype
                                            FROM    ( SELECT  dbo.Get_GST_Type(@v_CN_CustId) AS GType
                                                    ) tb
                                          )
                                          
                                          
                        SET @OutputID = ( SELECT    CASE WHEN @v_INV_TYPE = 'I'
                                                         THEN 10021
                                                         WHEN @v_INV_TYPE = 'S'
                                                         THEN 10024
                                                         WHEN @v_INV_TYPE = 'U'
                                                         THEN 10075
                                                    END AS inputid
                                        )
                    END
                    
-----------------------------------------------------------------------------------------------------------------                
                
                SET @Remarks = 'GST against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                
                
                IF @v_INV_TYPE <> 'I'
                    BEGIN                
                        EXECUTE Proc_Ledger_Insert @COutputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY               
                
                        EXECUTE Proc_Ledger_Insert @OutputID, 0, @CGST_Amount,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                
                    END                
                ELSE
                    BEGIN                
                        EXECUTE Proc_Ledger_Insert @OutputID, 0, @v_CN_ItemTax,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                 
                    END           
                              
                              
                SET @Remarks = 'CESS against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))            
                           
                EXECUTE Proc_Ledger_Insert @CessOutputID, 0, @v_CN_ItemCess,
                    @Remarks, @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY             
                              
                              
                SET @Remarks = 'Stock Out against CreditNote No- '
                    + @V_CreditNote_Code
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                
                
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_CN_Amount, @Remarks,
                    @V_Division_ID, @V_CreditNote_ID, @V_Trans_Type,
                    @v_CreditNote_Date, @V_Created_BY         
                          
                SET @Remarks = 'Round Off against Credit Note No- '
                    + CAST(@V_CreditNote_No AS VARCHAR(50))                               
                      
                IF @v_RoundOff > 0
                    BEGIN                      
                      
                        EXECUTE Proc_Ledger_Insert 10054, 0, @v_RoundOff,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                  
                      
                    END                      
                      
                ELSE
                    BEGIN                      
                      
                        SET @v_RoundOff = -+@v_RoundOff                      
                      
                        EXECUTE Proc_Ledger_Insert 10054, @v_RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_CreditNote_ID,
                            @V_Trans_Type, @v_CreditNote_Date, @V_Created_BY                  
                      
                    END        
             
            END                
    END
Go   
------------------------------------------------------------------------------------------------------------------------
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
      @v_DN_ItemCess NUMERIC(18, 2) = 0 ,
      @v_DN_CustId NUMERIC(18, 0) ,
      @v_INV_No VARCHAR(100) ,
      @v_INV_Date DATETIME ,
      @v_DebitNote_Type VARCHAR(20) = NULL ,
      @v_RefNo VARCHAR(50) = NULL ,
      @v_RefDate_dt DATETIME ,
      @V_Trans_Type NUMERIC(18, 0) ,
      @v_RoundOff NUMERIC(18, 2) ,
      @v_Proc_Type INT                
    )
AS
    BEGIN           
        DECLARE @V_GSTType NUMERIC(18, 0)        
        DECLARE @Remarks VARCHAR(250)                
        DECLARE @InputID NUMERIC                
        DECLARE @CInputID NUMERIC= 0                
        SET @CInputID = 10016                
        DECLARE @CGST_Amount NUMERIC(18, 2)        
        DECLARE @V_MRN_TYPE NUMERIC = 0      
        DECLARE @CessInputID NUMERIC = 0                
        SET @CessInputID = 10013       
               
        SET @V_GSTType = ( SELECT   dbo.Get_GST_Type(@v_DN_CustId)
                         )    
                
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
                          Tax_num ,
                          Cess_num ,
                          RoundOff ,
                          GSTType    
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
                          @v_DN_ItemTax ,
                          @v_DN_ItemCess ,
                          @v_RoundOff ,
                          @V_GSTType        
                        )                
                
                UPDATE  DN_SERIES
                SET     CURRENT_USED = @V_DebitNote_No
                WHERE   DIV_ID = @V_Division_ID                
                
                SET @CGST_Amount = ( @v_DN_ItemTax / 2 )         
                       
--------------------------------------------------------------------------------------------------------------------------------------------

                IF @V_MRN_Id > 0
                    BEGIN
                        SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1
                                                             THEN 10023
                                                             WHEN MRN_TYPE = 2
                                                             THEN 10020
                                                             WHEN MRN_TYPE = 3
                                                             THEN 10074
                                                        END AS inputid
                                                FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                                                WHERE   MRN_NO = @V_MRN_Id
                                              ), 0)                
                        IF @InputID = 0
                            BEGIN                
                                SET @InputID = ISNULL(( SELECT
                                                              CASE
                                                              WHEN MRN_TYPE = 1
                                                              THEN 10023
                                                              WHEN MRN_TYPE = 2
                                                              THEN 10020
                                                              WHEN MRN_TYPE = 3
                                                              THEN 10074
                                                              END AS inputid
                                                        FROM  dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
                                                        WHERE MRN_NO = @V_MRN_Id
                                                      ), 0)                
                            END          
                              
                
                        SET @V_MRN_TYPE = ( SELECT  MRN_TYPE
                                            FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                                            WHERE   MRN_NO = @V_MRN_Id
                                            UNION
                                            SELECT  MRN_TYPE
                                            FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
                                            WHERE   MRN_NO = @V_MRN_Id
                                          )   
                    END
                    
                ELSE
                    BEGIN
                    
                        SET @V_MRN_TYPE = ( SELECT  dbo.Get_GST_Type(@v_DN_CustId)
                                          )
                                          
                                          
                        SET @InputID = ISNULL(( SELECT  CASE WHEN @V_MRN_TYPE = 1
                                                             THEN 10023
                                                             WHEN @V_MRN_TYPE = 2
                                                             THEN 10020
                                                             WHEN @V_MRN_TYPE = 3
                                                             THEN 10074
                                                        END AS inputid
                                              ), 0)                
                    
                    END
----------------------------------------------------------------------------------------------------------------------------   
                
                SET @Remarks = 'Debit  against DebitNote No-. -'
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                
                
                
                EXECUTE Proc_Ledger_Insert @v_DN_CustId, 0, @v_DN_Amount,
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,
                    @v_DebitNote_Date, @V_Created_BY                
                
                
                EXECUTE Proc_Ledger_Insert 10070, @v_DN_ItemValue, 0, @Remarks,
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,
                    @v_DebitNote_Date, @V_Created_BY                 
                
                
                
                SET @Remarks = 'GST against DebitNote No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                
                
                IF @V_MRN_TYPE <> 2
                    BEGIN          
                
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                 
                
                
                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,
                            @Remarks, @v_Division_ID, @V_DebitNote_ID,
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                 
                    END                
                
                
                
                ELSE
                    BEGIN                
                        EXECUTE Proc_Ledger_Insert @InputID, @v_DN_ItemTax, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                 
                    END                   
              
                SET @Remarks = 'Cess against DebitNote No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))          
                              
                EXECUTE Proc_Ledger_Insert @CessInputID, @v_DN_ItemCess, 0,
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,
                    @v_DebitNote_Date, @V_Created_BY              
                
                SET @Remarks = 'Stock In against Debit Note No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))       
                
                
                EXECUTE Proc_Ledger_Insert 10073, @v_DN_Amount, 0, @Remarks,
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,
                    @v_DebitNote_Date, @V_Created_BY        
                        
                SET @Remarks = 'Round Off against Debit Note No- '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                             
                    
                IF @v_RoundOff > 0
                    BEGIN                    
                    
                        EXECUTE Proc_Ledger_Insert 10054, 0, @v_RoundOff,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                
                    
                    END                    
                    
                ELSE
                    BEGIN                    
                    
                        SET @v_RoundOff = -+@v_RoundOff                    
                    
                        EXECUTE Proc_Ledger_Insert 10054, @v_RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                
                    
                    END                
                
            END                
                
        IF @V_PROC_TYPE = 2
            BEGIN          
                    
                    
                EXECUTE [Proc_DebitNoteUpdateDeleteLedgerEntries] @V_DebitNote_ID,
                    @V_Trans_Type        
                          
                
                UPDATE  DebitNote_MASTER
                SET     DebitNote_No = @V_DebitNote_No ,
                        DebitNote_Code = @V_DebitNote_Code ,
                        DebitNote_Date = @v_DebitNote_Date ,
                        Remarks = @V_Remarks ,
                        MRNId = @V_MRN_Id ,
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
                        Tax_num = @v_DN_ItemTax ,
                        Cess_num = @v_DN_ItemCess ,
                        RoundOff = @v_RoundOff ,
                        GSTType = @v_GSTType
                WHERE   DebitNote_ID = @V_DebitNote_ID           
                        
                        
                               
                SET @CGST_Amount = ( @v_DN_ItemTax / 2 )                
----------------------------------------------------------------------------------------------------------------------------------                               
                IF @V_MRN_Id > 0
                    BEGIN
                        SET @InputID = ISNULL(( SELECT  CASE WHEN MRN_TYPE = 1
                                                             THEN 10023
                                                             WHEN MRN_TYPE = 2
                                                             THEN 10020
                                                             WHEN MRN_TYPE = 3
                                                             THEN 10074
                                                        END AS inputid
                                                FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                                                WHERE   MRN_NO = @V_MRN_Id
                                              ), 0)                
                        IF @InputID = 0
                            BEGIN                
                                SET @InputID = ISNULL(( SELECT
                                                              CASE
                                                              WHEN MRN_TYPE = 1
                                                              THEN 10023
                                                              WHEN MRN_TYPE = 2
                                                              THEN 10020
                                                              WHEN MRN_TYPE = 3
                                                              THEN 10074
                                                              END AS inputid
                                                        FROM  dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
                                                        WHERE MRN_NO = @V_MRN_Id
                                                      ), 0)                
                            END          
                              
                
                        SET @V_MRN_TYPE = ( SELECT  MRN_TYPE
                                            FROM    dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER
                                            WHERE   MRN_NO = @V_MRN_Id
                                            UNION
                                            SELECT  MRN_TYPE
                                            FROM    dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER
                                            WHERE   MRN_NO = @V_MRN_Id
                                          )   
                    END
                    
                ELSE
                    BEGIN
                    
                        SET @V_MRN_TYPE = ( SELECT  dbo.Get_GST_Type(@v_DN_CustId)
                                          )
                                          
                                          
                        SET @InputID = ISNULL(( SELECT  CASE WHEN @V_MRN_TYPE = 1
                                                             THEN 10023
                                                             WHEN @V_MRN_TYPE = 2
                                                             THEN 10020
                                                             WHEN @V_MRN_TYPE = 3
                                                             THEN 10074
                                                        END AS inputid
                                              ), 0)                
                    
                    END
                                  
                                               
----------------------------------------------------------------------------------------------------------------------------------

                SET @Remarks = 'Debit  against DebitNote No-. -'
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                
                
                
                EXECUTE Proc_Ledger_Insert @v_DN_CustId, 0, @v_DN_Amount,
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,
                    @v_DebitNote_Date, @V_Created_BY                
                
                
                EXECUTE Proc_Ledger_Insert 10070, @v_DN_ItemValue, 0, @Remarks,
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,
                    @v_DebitNote_Date, @V_Created_BY                 
                
                
                
                SET @Remarks = 'GST against DebitNote No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                
                
                IF @V_MRN_TYPE <> 2
                    BEGIN          
                
                        EXECUTE Proc_Ledger_Insert @CInputID, @CGST_Amount, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                 
                
                
                        EXECUTE Proc_Ledger_Insert @InputID, @CGST_Amount, 0,
                            @Remarks, @v_Division_ID, @V_DebitNote_ID,
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                 
                    END                
                
                
                
                ELSE
                    BEGIN                
                        EXECUTE Proc_Ledger_Insert @InputID, @v_DN_ItemTax, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                 
                    END                   
              
                SET @Remarks = 'Cess against DebitNote No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))          
                              
                EXECUTE Proc_Ledger_Insert @CessInputID, @v_DN_ItemCess, 0,
                    @Remarks, @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,
                    @v_DebitNote_Date, @V_Created_BY              
                
                SET @Remarks = 'Stock In against Debit Note No- '
                    + @V_DebitNote_Code + ' - '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                
                
                
                
                EXECUTE Proc_Ledger_Insert 10073, @v_DN_Amount, 0, @Remarks,
                    @V_Division_ID, @V_DebitNote_ID, @V_Trans_Type,
                    @v_DebitNote_Date, @V_Created_BY                         
                             
                SET @Remarks = 'Round Off against Debit Note No- '
                    + CAST(@V_DebitNote_No AS VARCHAR(50))                             
                    
                IF @v_RoundOff > 0
                    BEGIN                    
                    
                        EXECUTE Proc_Ledger_Insert 10054, 0, @v_RoundOff,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                
                    
                    END                    
                    
                ELSE
                    BEGIN                    
                    
                        SET @v_RoundOff = -+@v_RoundOff                    
                    
                        EXECUTE Proc_Ledger_Insert 10054, @v_RoundOff, 0,
                            @Remarks, @V_Division_ID, @V_DebitNote_ID,
                            @V_Trans_Type, @v_DebitNote_Date, @V_Created_BY                
                    
                    END           
                
            END                
    END  

 GO
 -----------------------------------------------------------------------------------------------------------------------------------------------------------------

