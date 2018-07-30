INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0027_30_July_2018_PurchaseOrderCompositionChanges' ,
          GETDATE()
        )
Go

ALTER TABLE PO_MASTER ADD SpecialSchemeFlag  CHAR(10) NOT NULL DEFAULT 'Nill'

Go

--------------------------------------------------------------------------------------------------------------        

Alter PROCEDURE [dbo].[GET_PO_DETAIL]
(      
 @V_PO_ID NUMERIC(18,0)      
)      
AS      
 BEGIN      
      
      
SELECT     PO_ID, PO_CODE, PO_NO, dbo.fn_Format(PO_DATE) AS PO_DATE, dbo.fn_Format(PO_START_DATE) AS PO_START_DATE,       
                      dbo.fn_Format(PO_END_DATE) AS PO_END_DATE, PO_REMARKS, PO_SUPP_ID, PO_QUALITY_ID, PO_DELIVERY_ID, PO_STATUS, PATMENT_TERMS,       
                      TRANSPORT_MODE, NET_AMOUNT, TOTAL_AMOUNT, VAT_AMOUNT, EXICE_AMOUNT, PO_TYPE, OCTROI, PRICE_BASIS, FRIEGHT,       
                      OTHER_CHARGES, CESS_PER,DISCOUNT_AMOUNT,OPEN_PO_QTY,SpecialSchemeFlag      
FROM         PO_MASTER      
WHERE     (PO_ID = @V_PO_ID)      
      
END     
		
Go


ALTER PROCEDURE [dbo].[PROC_PO_MASTER]  
    (  
      @v_PO_ID INT ,  
      @v_PO_CODE VARCHAR(20) ,  
      @v_PO_NO DECIMAL ,  
      @v_PO_DATE DATETIME ,  
      @v_PO_START_DATE DATETIME ,  
      @v_PO_END_DATE DATETIME ,  
      @v_PO_REMARKS VARCHAR(500) ,  
      @v_PO_SUPP_ID INT ,  
      @v_PO_QUALITY_ID INT ,  
      @v_PO_DELIVERY_ID INT ,  
      @v_PO_STATUS INT ,  
      @v_PATMENT_TERMS VARCHAR(500) ,  
      @v_TRANSPORT_MODE VARCHAR(50) ,  
      @v_TOTAL_AMOUNT NUMERIC(18, 2) ,  
      @v_VAT_AMOUNT NUMERIC(18, 2) ,  
      @v_CESS_AMOUNT NUMERIC(18, 2) ,  
      @v_NET_AMOUNT NUMERIC(18, 2) ,  
      @v_EXICE_AMOUNT NUMERIC(18, 2) ,  
      @v_PO_TYPE INT ,  
      @v_OCTROI VARCHAR(50) ,  
      @v_PRICE_BASIS VARCHAR(50) ,  
      @v_FRIEGHT INT ,  
      @v_OTHER_CHARGES NUMERIC(18, 2) ,  
      --@v_CESS NUMERIC(18, 2) ,  
      @v_DISCOUNT_AMOUNT NUMERIC(18, 2) ,  
      @v_CREATED_BY VARCHAR(50) ,  
      @v_CREATION_DATE DATETIME ,  
      @v_MODIFIED_BY VARCHAR(50) ,  
      @v_MODIFIED_DATE DATETIME ,  
      @v_DIVISION_ID INT ,  
      @V_OPEN_PO_QTY BIT ,  
      @v_Proc_Type INT ,  
      @v_VAT_ON_EXICE BIT ,
      @V_Special_Scheme CHAR(10)                
    )  
AS  
    BEGIN                
                
        IF @V_PROC_TYPE = 1  
            BEGIN                
--Inderjeet: PO_NO is gentared from another procedure and send to this procedure as a parameter.                
--   select @v_PO_NO = isnull(max(PO_NO),0) + 1 from PO_MASTER                
                SELECT  @V_PO_ID = ISNULL(MAX(PO_ID), 0) + 1  
                FROM    PO_MASTER                
                
                INSERT  INTO PO_MASTER  
                        ( PO_ID ,  
                          PO_CODE ,  
                          PO_NO ,  
                          PO_DATE ,  
                          PO_START_DATE ,  
                          PO_END_DATE ,  
                          PO_REMARKS ,  
                          PO_SUPP_ID ,  
                          PO_QUALITY_ID ,  
                          PO_DELIVERY_ID ,  
                          PO_STATUS ,  
                          PATMENT_TERMS ,  
                          TRANSPORT_MODE ,  
                          TOTAL_AMOUNT ,  
                          VAT_AMOUNT ,  
                          CESS_AMOUNT ,  
                          NET_AMOUNT ,  
                          EXICE_AMOUNT ,  
                          PO_TYPE ,  
                          OCTROI ,  
                          PRICE_BASIS ,  
                          FRIEGHT ,  
                          OTHER_CHARGES ,  
                          --CESS_PER ,  
                          DISCOUNT_AMOUNT ,  
                          CREATED_BY ,  
                          CREATION_DATE ,  
                          MODIFIED_BY ,  
                          MODIFIED_DATE ,  
                          DIVISION_ID ,  
                          VAT_ON_EXICE ,  
                          OPEN_PO_QTY,
                          SpecialSchemeFlag            
                        )  
                VALUES  ( @V_PO_ID ,  
                          @V_PO_CODE ,  
                          @V_PO_NO ,  
                          @V_PO_DATE ,  
                          @V_PO_START_DATE ,  
                          @V_PO_END_DATE ,  
                          @V_PO_REMARKS ,  
                          @V_PO_SUPP_ID ,  
                          @V_PO_QUALITY_ID ,  
                          @V_PO_DELIVERY_ID ,  
                          @V_PO_STATUS ,  
                          @V_PATMENT_TERMS ,  
                          @V_TRANSPORT_MODE ,  
                          @V_TOTAL_AMOUNT ,  
                          @V_VAT_AMOUNT ,  
                          @v_CESS_AMOUNT ,  
                          @V_NET_AMOUNT ,  
                          @V_EXICE_AMOUNT ,  
                          @V_PO_TYPE ,  
                          @V_OCTROI ,  
                          @V_PRICE_BASIS ,  
                          @V_FRIEGHT ,  
                          @V_OTHER_CHARGES ,  
                          --@v_CESS ,  
						  @v_DISCOUNT_AMOUNT ,  
                          @V_CREATED_BY ,  
                          @V_CREATION_DATE ,  
                          @V_MODIFIED_BY ,  
                          @V_MODIFIED_DATE ,  
                          @V_DIVISION_ID ,  
                          @v_VAT_ON_EXICE ,  
                          @V_OPEN_PO_QTY ,
                          @V_Special_Scheme             
                        ) 
                                            
                RETURN @V_PO_ID                 
                   
            END                
                
        IF @V_PROC_TYPE = 2  
            BEGIN                  
                UPDATE  PO_MASTER  
                SET     PO_DATE = @V_PO_DATE ,  
                        PO_START_DATE = @V_PO_START_DATE ,  
                        PO_END_DATE = @V_PO_END_DATE ,  
                        PO_REMARKS = @V_PO_REMARKS ,  
                        PO_SUPP_ID = @V_PO_SUPP_ID ,  
                        PO_QUALITY_ID = @V_PO_QUALITY_ID ,  
                        PO_DELIVERY_ID = @V_PO_DELIVERY_ID ,  
                        PO_STATUS = @V_PO_STATUS ,  
                        PATMENT_TERMS = @V_PATMENT_TERMS ,  
                        TRANSPORT_MODE = @V_TRANSPORT_MODE ,  
                        TOTAL_AMOUNT = @V_TOTAL_AMOUNT ,  
                        VAT_AMOUNT = @V_VAT_AMOUNT ,  
                        CESS_AMOUNT = @V_CESS_AMOUNT ,  
                        NET_AMOUNT = @V_NET_AMOUNT ,  
                        PO_TYPE = @v_PO_TYPE ,  
                        OCTROI = @v_OCTROI ,  
                        PRICE_BASIS = @v_PRICE_BASIS ,  
                        FRIEGHT = @v_FRIEGHT ,  
                        OTHER_CHARGES = @v_OTHER_CHARGES ,  
                        --CESS_PER = @v_CESS ,  
                        DISCOUNT_AMOUNT = @v_DISCOUNT_AMOUNT ,  
                        MODIFIED_BY = @V_MODIFIED_BY ,  
                        MODIFIED_DATE = @V_MODIFIED_DATE ,  
                        DIVISION_ID = @V_DIVISION_ID ,  
                        VAT_ON_EXICE = @v_VAT_ON_EXICE ,  
                        OPEN_PO_QTY = @V_OPEN_PO_QTY ,
                        SpecialSchemeFlag = @V_Special_Scheme 
                WHERE   PO_ID = @V_PO_ID                  
            END                  
                  
        IF @V_PROC_TYPE = 3  
            BEGIN                  
                DELETE  FROM PO_MASTER  
                WHERE   PO_ID = @V_PO_ID                  
            END                  
                
    END 

Go

ALTER PROCEDURE [dbo].[PROC_MATERIAL_RECIEVED_AGAINST_PO_MASTER]    
    (    
      @v_Receipt_ID NUMERIC(18, 0) ,    
      @v_Receipt_No NUMERIC(18, 0) ,    
      @v_Receipt_Code VARCHAR(20) ,    
      @v_PO_ID NUMERIC(18, 0) ,    
      @v_Receipt_Date DATETIME ,    
      @v_Remarks VARCHAR(500) ,    
      @v_MRN_NO NUMERIC(18, 0) ,    
      @v_MRN_PREFIX VARCHAR(50) ,    
      @v_Created_BY VARCHAR(100) ,    
      @v_Creation_Date DATETIME ,    
      @v_Modified_By VARCHAR(100) ,    
      @v_Modification_Date DATETIME ,    
      @v_Division_ID NUMERIC(18, 0) ,    
      @v_Proc_Type INT ,    
      @V_mrn_status INT ,    
      @v_freight NUMERIC(18, 2) ,    
      @v_Other_Charges NUMERIC(18, 2) ,    
      @v_Discount_amt NUMERIC(18, 2) ,    
      @v_GROSS_AMOUNT NUMERIC(18, 2) ,    
      @v_GST_AMOUNT NUMERIC(18, 2) ,    
      @v_CESS_AMOUNT NUMERIC(18, 2) ,    
      @v_NET_AMOUNT NUMERIC(18, 2) ,    
      @V_MRN_TYPE INT ,    
      @V_VAT_ON_EXICE INT ,    
      @v_Invoice_No NVARCHAR(50) ,    
      @v_Invoice_Date DATETIME ,    
      @V_CUST_ID NUMERIC(18, 0) ,    
      @v_MRNCompanies_ID INT             
            
    )    
AS    
    BEGIN              
        IF @V_PROC_TYPE = 1    
            BEGIN            
            
            
            
                DECLARE @Remarks VARCHAR(250)            
                DECLARE @InputID NUMERIC            
                DECLARE @CInputID NUMERIC            
                SET @CInputID = 10016            
                DECLARE @RoundOff NUMERIC(18, 2)            
                DECLARE @CGST_Amount NUMERIC(18, 2)            
                SET @CGST_Amount = ( @v_GST_AMOUNT / 2 )            
                SET @RoundOff = ( @v_Other_Charges )            
            
                SET @InputID = ( SELECT CASE WHEN @V_MRN_TYPE = 1 THEN 10023    
                                             WHEN @V_MRN_TYPE = 2 THEN 10020    
                                             WHEN @V_MRN_TYPE = 3 THEN 10074    
                                        END AS inputid    
                               )         
            
            
                SET @Remarks = 'Purchase against party bill no.: '    
                    + @v_Invoice_No + ' - '    
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)             
            
                EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0,    
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3,    
                    @v_Invoice_Date, @V_Created_BY                
            
                EXECUTE Proc_Ledger_Insert 10070, 0, @v_GROSS_AMOUNT, @Remarks,    
                    @V_Division_ID, @V_Receipt_ID, 3, @v_Invoice_Date,    
                    @V_Created_BY             
            
				SET @Remarks = 'Freight against party bill no.: '    
                    + @v_Invoice_No + ' - '    
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)             
            
                EXECUTE Proc_Ledger_Insert 10047, @v_freight, 0,    
                    @Remarks, @V_Division_ID, @V_Receipt_ID, 3,    
                    @v_Invoice_Date, @V_Created_BY
            
                SET @Remarks = 'GST against party invoice No- '    
                    + @v_Invoice_No + ' - '    
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)             
            
                IF @V_MRN_TYPE <> 2    
                    BEGIN              
            
                        EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,    
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,    
                            @v_Invoice_Date, @V_Created_BY             
            
            
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount,    
                            @Remarks, @v_Division_ID, @v_Receipt_ID, 3,    
                            @v_Invoice_Date, @V_Created_BY             
                    END          
            
            
                ELSE    
                    BEGIN            
                        EXECUTE Proc_Ledger_Insert @InputID, 0, @v_GST_AMOUNT,    
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,    
                            @v_Invoice_Date, @V_Created_BY             
                    END     
                        
                SET @Remarks = 'Cess against party invoice No- '    
                    + @v_Invoice_No + ' - '    
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)       
                        
                EXECUTE Proc_Ledger_Insert 10013, 0, @v_CESS_AMOUNT,    
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,    
                            @v_Invoice_Date, @V_Created_BY            
            
            
                SET @Remarks = 'Round Off against party invoice No- '    
                    + @v_Invoice_No + ' - '    
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)            
            
                IF @RoundOff > 0    
                    BEGIN            
            
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,    
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,    
                            @v_Invoice_Date, @V_Created_BY                   
                    END            
            
                ELSE    
                    BEGIN            
            
                        SET @RoundOff = -+@RoundOff      
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,    
                            @Remarks, @V_Division_ID, @v_Receipt_ID, 3,    
                            @v_Invoice_Date, @V_Created_BY             
            
                    END             
            
            
                SET @Remarks = 'Stock Out against party  invoice No- '    
                    + @v_Invoice_No + ' - '    
                    + CONVERT(VARCHAR(20), @v_Invoice_Date, 106)            
            
            
            
                EXECUTE Proc_Ledger_Insert 10073, 0, @v_NET_AMOUNT, @Remarks,    
                    @V_Division_ID, @v_Receipt_ID, 3, @v_Invoice_Date,    
                    @V_Created_BY             
            
            
                SELECT  @V_Receipt_No = ISNULL(MAX(Receipt_No), 0) + 1    
                FROM    MATERIAL_RECEIVED_AGAINST_PO_MASTER                          
            
                INSERT  INTO MATERIAL_RECEIVED_AGAINST_PO_MASTER    
                        ( Receipt_ID ,    
                          Receipt_No ,    
                          Receipt_Code ,    
                          Invoice_No ,    
                          Invoice_Date ,    
                          PO_ID ,    
                          Receipt_Date ,    
                          Remarks ,    
                          MRN_NO ,    
                          MRN_PREFIX ,    
                          Created_BY ,    
                          Creation_Date ,    
                          Modified_By ,    
                          Modification_Date ,    
                          Division_ID ,    
                          MRN_STATUS ,    
                          freight ,    
                          Other_charges ,    
                          Discount_amt ,    
                          GROSS_AMOUNT ,    
                          GST_AMOUNT ,    
                          CESS_AMOUNT ,    
                          NET_AMOUNT ,    
                          MRN_TYPE ,    
                          VAT_ON_EXICE ,    
                          MRNCompanies_ID ,    
                          IsPrinted ,    
                          CUST_ID              
                        )    
                VALUES  ( @V_Receipt_ID ,    
                          @V_Receipt_No ,    
                          @V_Receipt_Code ,    
                          @v_Invoice_No ,    
                          @v_Invoice_Date ,    
                          @V_PO_ID ,    
                          @v_Receipt_Date ,    
                          @V_Remarks ,    
                          @V_MRN_NO ,    
                          @V_MRN_PREFIX ,    
                          @V_Created_BY ,    
                          @V_Creation_Date ,    
                          @V_Modified_By ,    
                          @V_Modification_Date ,    
						  @V_Division_ID ,    
                          @V_mrn_status ,    
                          @v_freight ,    
                          @v_other_charges ,    
                          @v_Discount_amt ,    
                          @v_GROSS_AMOUNT ,    
                          @v_GST_AMOUNT ,    
                          @v_CESS_AMOUNT ,    
                          @v_NET_AMOUNT ,    
                          @v_MRN_TYPE ,    
                          @V_VAT_ON_EXICE ,    
                          @v_MRNCompanies_ID ,    
                          0 ,    
                          @V_CUST_ID 
                        )                          
            
                UPDATE  MRN_SERIES    
                SET     CURRENT_USED = CURRENT_USED + 1    
                WHERE   DIV_ID = @v_Division_ID  
                
                RETURN @V_MRN_NO                 
            
            END                          
            
            
        IF @V_PROC_TYPE = 2    
            BEGIN                          
            
                UPDATE  MATERIAL_RECEIVED_AGAINST_PO_MASTER    
                SET     PO_ID = @V_PO_ID ,    
                        Receipt_Date = @v_Receipt_Date ,    
                        Remarks = @V_Remarks ,    
                        Created_BY = @V_Created_BY ,    
                        Creation_Date = @V_Creation_Date ,    
                        Modified_By = @V_Modified_By ,    
                        Modification_Date = @V_Modification_Date ,    
                        Division_ID = @V_Division_ID ,    
                        freight = @v_freight ,    
                        Other_charges = @v_Other_Charges ,    
                        Discount_amt = @v_Discount_amt ,    
                        VAT_ON_EXICE = @V_VAT_ON_EXICE ,    
                        MRNCompanies_ID = @v_MRNCompanies_ID    
                WHERE   Receipt_ID = @V_Receipt_ID    
            
            END                          
            
            
        --IF @V_PROC_TYPE = 3            
        --    BEGIN                            
            
        --        DECLARE cur CURSOR            
        --        FOR            
        --            SELECT  Item_ID ,            
        --                    Item_Qty ,            
        --                    Indent_ID            
        --            FROM    MATERIAL_RECEIVED_AGAINST_PO_DETAIL            
        --            WHERE   Receipt_ID = @V_Receipt_ID  
            
        --        DECLARE @itemid NUMERIC(18, 0)                            
            
        --        DECLARE @itemQty NUMERIC(18, 4)                            
            
        --        DECLARE @IndentID NUMERIC(18, 0)
            
        --        OPEN cur                            
            
        --        FETCH NEXT FROM cur INTO @itemid, @itemQty, @IndentID                            
            
        --        WHILE @@fetch_status = 0            
        --            BEGIN  
                               
            
        --                UPDATE  PO_STATUS            
        --                SET     RECIEVED_QTY = RECIEVED_QTY - @itemQty ,            
        --                        BALANCE_QTY = BALANCE_QTY + @itemQty            
        --                WHERE   PO_ID = @V_PO_ID            
        --                        AND ITEM_ID = @itemid            
        --                        AND INDENT_ID = @IndentID                              
            
        --                UPDATE  ITEM_DETAIL            
        --                SET     CURRENT_STOCK = CURRENT_STOCK - @itemQty            
        --                WHERE   ITEM_ID = @itemid            
        --                AND DIV_ID = @V_Division_ID            
            
        --                FETCH NEXT FROM cur INTO @itemid, @itemQty, @IndentID            
        --            END
                   
        --        CLOSE cur
                    
        --        DEALLOCATE cur            
            
        --        DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_DETAIL            
        --        WHERE   Receipt_ID = @V_Receipt_ID            
             
        --        DELETE  FROM MATERIAL_RECEIVED_AGAINST_PO_MASTER            
		--        WHERE   Receipt_ID = @V_Receipt_ID             
        --    END              
               
    END 

Go

---------------------------------------------------------------------------------------------------------------