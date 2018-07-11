
INSERT  INTO dbo.DBScriptUpdateLog
        ( LogFileName ,
          ExecuteDateTime
        )
VALUES  ( '0022_06_July_2018_CancelSaleInvoiceCorrection' ,
          GETDATE()
        )
Go


Alter PROC [dbo].[PROC_Cancel_Sale_Invoice]  
    (  
      @SI_ID AS INT ,  
      @status AS INT ,  
      @userName AS VARCHAR(50)  
    )  
AS  
    BEGIN      
        UPDATE  dbo.SALE_INVOICE_MASTER  
        SET     INVOICE_STATUS = @status ,  
                MODIFIED_DATE = GETDATE() ,  
                MODIFIED_BY = @userName  
        WHERE   SI_ID = @SI_ID      
        UPDATE  dbo.STOCK_DETAIL  
        SET     STOCK_DETAIL.Issue_Qty = ( STOCK_DETAIL.Issue_Qty  
                                           - SALE_INVOICE_STOCK_DETAIL.ITEM_QTY ) ,  
                STOCK_DETAIL.Balance_Qty = ( STOCK_DETAIL.Balance_Qty  
                                             + SALE_INVOICE_STOCK_DETAIL.ITEM_QTY )  
        FROM    dbo.STOCK_DETAIL  
                JOIN dbo.SALE_INVOICE_STOCK_DETAIL ON SALE_INVOICE_STOCK_DETAIL.STOCK_DETAIL_ID = STOCK_DETAIL.STOCK_DETAIL_ID  
                                                      AND SALE_INVOICE_STOCK_DETAIL.ITEM_ID = STOCK_DETAIL.Item_id  
        WHERE   SI_ID = @SI_ID      
        
        DECLARE @V_CUST_ID AS NUMERIC(18, 0)      
        DECLARE @V_NET_AMOUNT AS NUMERIC(18, 2)    
        DECLARE @V_GROSS_AMOUNT AS NUMERIC(18, 2)     
        DECLARE @V_VAT_AMOUNT AS NUMERIC(18, 2)   
        DECLARE @v_CESS_AMOUNT AS NUMERIC(18, 2)   
        DECLARE @v_Add_CESS_AMOUNT AS NUMERIC(18, 2)     
        DECLARE @V_Division_ID AS NUMERIC(18, 0)      
        DECLARE @V_Created_BY AS VARCHAR(50)      
        DECLARE @V_Si_NO AS VARCHAR(100)      
        DECLARE @TransactionDate AS DATETIME    
        DECLARE @v_INV_TYPE CHAR(1)  
        DECLARE @CGST_Amount AS NUMERIC(18, 2)   
        DECLARE @RoundOff NUMERIC(18, 2)   
          
        DECLARE @InputID NUMERIC               
        DECLARE @CInputID NUMERIC               
        SET @CInputID = 10017   
            
        SELECT  @V_CUST_ID = CUST_ID ,  
                @V_NET_AMOUNT = ISNULL(NET_AMOUNT, 0) ,  
                @V_GROSS_AMOUNT = ISNULL(GROSS_AMOUNT, 0) ,  
                @v_CESS_AMOUNT = ISNULL(CESS_AMOUNT, 0) ,  
                @V_VAT_AMOUNT = ISNULL(sim.VAT_AMOUNT, 0) ,  
                @v_INV_TYPE = INV_TYPE ,  
                @V_Division_ID = sim.DIVISION_ID ,  
                @V_Created_BY = sim.CREATED_BY ,  
                @V_Si_NO = SI_CODE + CAST(SI_NO AS VARCHAR(50)) ,  
                @TransactionDate = SI_DATE  
        FROM    dbo.SALE_INVOICE_MASTER sim   
        WHERE   sim.SI_ID = @SI_ID  
          
       SELECT  @v_Add_CESS_AMOUNT = SUM(ISNULL(sidd.ACessAmount, 0))FROM    dbo.SALE_INVOICE_DETAIL sidd WHERE   sidd.SI_ID = @SI_ID   
            
          
        SET @CGST_Amount = @V_VAT_AMOUNT / 2  
          
        SET @RoundOff = @v_NET_AMOUNT - ( @v_GROSS_AMOUNT + @v_VAT_AMOUNT    
                                          + @v_CESS_AMOUNT )   
          
        SET @InputID = ( SELECT CASE WHEN @v_INV_TYPE = 'I' THEN 10021  
                                     WHEN @v_INV_TYPE = 'S' THEN 10024  
                                     WHEN @v_INV_TYPE = 'U' THEN 10075  
                                END AS inputid  
                       )   
            
        DECLARE @Remarks VARCHAR(250)      
        SET @Remarks = 'Amount Deducted against Cancel Invoice - ' + @V_Si_NO      
            
        EXECUTE Proc_Ledger_Insert @V_CUST_ID, @V_NET_AMOUNT, 0, @Remarks,  
            @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY    
              
        EXECUTE Proc_Ledger_Insert 10071, 0, @V_GROSS_AMOUNT, @Remarks,  
            @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY    
              
        SET @Remarks = 'Amount Deducted GST against Cancel Invoice - '  
            + @V_Si_NO   
                             
        IF @v_INV_TYPE <> 'I'  
            BEGIN                
                
                EXECUTE Proc_Ledger_Insert @CInputID, 0, @CGST_Amount,  
                    @Remarks, @V_Division_ID, @SI_ID, 16, @TransactionDate,  
                    @V_Created_BY                
                
                EXECUTE Proc_Ledger_Insert @InputID, 0, @CGST_Amount, @Remarks,  
                    @V_Division_ID, @SI_ID, 16, @TransactionDate,  
                    @V_Created_BY                
                
            END                
                
        ELSE  
            BEGIN                
                
                EXECUTE Proc_Ledger_Insert @InputID, 0, @v_VAT_AMOUNT,  
                    @Remarks, @V_Division_ID, @SI_ID, 16, @TransactionDate,  
                    @V_Created_BY                
                
            END    
              
              
          
        SET @Remarks = 'Amount Deducted Cess against Cancel Invoice - ' + @V_Si_NO     
                          
                EXECUTE Proc_Ledger_Insert 10014, 0, @v_CESS_AMOUNT, @Remarks,    
                    @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY      
                        
                        
                  SET @Remarks = 'Amount Deducted Add. Cess against Cancel Invoice - ' +   @V_Si_NO  
                        
                EXECUTE Proc_Ledger_Insert 10012, 0, @v_Add_CESS_AMOUNT, @Remarks,    
                    @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY      
               
                             
                
                SET @Remarks = 'Amount Deducted Round Off against Cancel Invoice -' + @V_Si_NO   
                                 
                
                IF @RoundOff > 0    
                    BEGIN                
                        EXECUTE Proc_Ledger_Insert 10054, 0, @RoundOff,   
                            @Remarks, @V_Division_ID, @SI_ID, 16, @TransactionDate,    
                            @V_Created_BY                
                    END                
                ELSE    
                    BEGIN                
                        SET @RoundOff = -+@RoundOff    
                                      
                        EXECUTE Proc_Ledger_Insert 10054, @RoundOff, 0,   
               @Remarks, @V_Division_ID, @SI_ID, 16, @TransactionDate,    
                            @V_Created_BY                
                    END                 
                
                
                SET @Remarks = 'Amount Deducted Stock In against Cancel Invoice - ' + @V_Si_NO    
                
                EXECUTE Proc_Ledger_Insert 10073, 0, @V_NET_AMOUNT, @Remarks,    
                    @V_Division_ID, @SI_ID, 16, @TransactionDate, @V_Created_BY   
          
              
    END 

Go