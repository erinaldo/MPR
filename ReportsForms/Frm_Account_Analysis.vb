Imports MMSPlus

Public Class Frm_Account_Analysis
    Implements IForm

    Dim objCommFunction As New CommonClass
    Dim Query As String
    Dim _rights As Form_Rights
    Private Sub Frm_Account_Analysis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BindAccountData()
    End Sub
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub dtpFromDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpFromDate.ValueChanged
        BindAccountData()
    End Sub


    Dim dt As DataTable
    Private Sub BindAccountData()

        Cursor.Current = Cursors.WaitCursor

        Query = "SELECT  Amount ,
        type ,
        SrNo
FROM    ( SELECT    ( ISNULL(SUM(CashIn), 0) - ISNULL(SUM(CashOut), 0) ) AS Amount ,
                    'Cash' AS TYPE ,
                    1 AS SrNo
          FROM      dbo.LedgerDetail
          WHERE     LedgerId IN (
                    SELECT  LedgerId
                    FROM    dbo.LedgerMaster
                    WHERE   AccountId IN ( SELECT   ACC_ID
                                           FROM     dbo.ACCOUNT_MASTER
                                           WHERE    AG_ID = 6 ) )
                    AND CAST(TransactionDate AS DATE) < CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
          UNION ALL
          SELECT    ( ISNULL(SUM(CashIn), 0) - ISNULL(SUM(CashOut), 0) ) AS Amount ,
                    'Bank' AS TYPE ,
                    2 AS SrNo
          FROM      dbo.LedgerDetail
          WHERE     LedgerId IN (
                    SELECT  LedgerId
                    FROM    dbo.LedgerMaster
                    WHERE   AccountId IN ( SELECT   ACC_ID
                                           FROM     dbo.ACCOUNT_MASTER
                                           WHERE    AG_ID = 3 ) )
                    AND CAST(TransactionDate AS DATE) < CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
          UNION ALL
          SELECT    ( ISNULL(SUM(CashIn), 0) - ISNULL(SUM(CashOut), 0) ) AS Amount ,
                    'Sale' AS TYPE ,
                    3 AS SrNo
          FROM      dbo.LedgerDetail
          WHERE     LedgerId IN (
                    SELECT  LedgerId
                    FROM    dbo.LedgerMaster
                    WHERE   AccountId IN ( SELECT   ACC_ID
                                           FROM     dbo.ACCOUNT_MASTER
                                           WHERE    AG_ID = 24 ) )
                    AND CAST(TransactionDate AS DATE) < CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
          UNION ALL
          SELECT    ( ISNULL(SUM(CashIn), 0) - ISNULL(SUM(CashOut), 0) ) AS Amount ,
                    'Purchase' AS type ,
                    4 AS SrNo
          FROM      dbo.LedgerDetail
          WHERE     LedgerId IN (
                    SELECT  LedgerId
                    FROM    dbo.LedgerMaster
                    WHERE   AccountId IN ( SELECT   ACC_ID
                                           FROM     dbo.ACCOUNT_MASTER
                                           WHERE    AG_ID = 21 ) )
                    AND CAST(TransactionDate AS DATE) < CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
          UNION ALL
          SELECT    ( ISNULL(SUM(CashIn), 0) - ISNULL(SUM(CashOut), 0) ) AS Amount ,
                    'Bill Receivable' AS type ,
                    5 AS SrNo
          FROM      dbo.LedgerDetail
          WHERE     LedgerId IN (
                    SELECT  LedgerId
                    FROM    dbo.LedgerMaster
                    WHERE   AccountId IN ( SELECT   ACC_ID
                                           FROM     dbo.ACCOUNT_MASTER
                                           WHERE    AG_ID = 1 ) )
                    AND CAST(TransactionDate AS DATE) < CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
          UNION ALL
          SELECT    ( ISNULL(SUM(CashIn), 0) - ISNULL(SUM(CashOut), 0) ) AS Amount ,
                    'Bill Payable' AS TYPE ,
                    6 AS SrNo
          FROM      dbo.LedgerDetail
          WHERE     LedgerId IN (
                    SELECT  LedgerId
                    FROM    dbo.LedgerMaster
                    WHERE   AccountId IN ( SELECT   ACC_ID
                                           FROM     dbo.ACCOUNT_MASTER
                                           WHERE    AG_ID = 2 ) )
                    AND CAST(TransactionDate AS DATE) < CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
          UNION ALL
          SELECT    ( ISNULL(SUM(SaleAmount - CnAmount), 0)
                      - ISNULL(SUM(PurchaseAmount), 0) ) AS Amount ,
                    'Profit/Loss' AS TYPE ,
                    7 AS SrNo
          FROM      ( SELECT    ITEM_ID ,
                                ( CAST(( CAST(( ISNULL(ITEM_QTY, 0)
                                                * ISNULL(ITEM_RATE, 0)
                                                - ( ISNULL(CASE
                                                              WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                           END, 0) ) ) AS DECIMAL(18,
                                                              2))
                                         - CASE WHEN GSTPaid = 'y'
                                                THEN ( ( ( ISNULL(ITEM_QTY, 0)
                                                           * ISNULL(ITEM_RATE,
                                                              0) )
                                                         - ( ISNULL(CASE
                                                              WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                              END, 0) ) )
                                                       - ( ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                             - ( ISNULL(CASE
                                                              WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                              END, 0) ) )
                                                           / ( 1 + VAT_PER
                                                              / 100 ) ) )
                                                ELSE 0
                                           END ) AS DECIMAL(18, 2)) ) AS SaleAmount ,
                                ( ISNULL(OD.ITEM_QTY, 0)
                                  * CAST(dbo.Get_Average_Rate_as_on_date(OD.ITEM_ID,
                                                              '" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "', 0,
                                                              1) AS DECIMAL(18,
                                                              2)) ) AS PurchaseAmount ,
                                0 AS CnAmount
                      FROM      dbo.SALE_INVOICE_MASTER OM
                                JOIN dbo.SALE_INVOICE_DETAIL OD ON OM.SI_ID = OD.SI_ID
                      WHERE     OM.INVOICE_STATUS <> 4
                                AND CAST(SI_DATE AS DATE) < CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
                      UNION ALL
                      SELECT    0 AS ID ,
                                0 ,
                                0 ,
                                ISNULL(SUM(CN_Amount), 0)
                                - ( ISNULL(SUM(Tax_Amt) + SUM(Cess_Amt), 0) ) AS CnAmount
                      FROM      dbo.CreditNote_Master
                      WHERE     CAST(CreditNote_Date AS DATE) < CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
                      GROUP BY  CreditNote_Id
                    ) tb
          UNION ALL
          SELECT    ( ISNULL(SUM(CashIn), 0) - ISNULL(SUM(CashOut), 0) ) AS Amount ,
                    'Day Sale' AS TYPE ,
                    8 AS SrNo
          FROM      dbo.LedgerDetail
          WHERE     LedgerId IN (
                    SELECT  LedgerId
                    FROM    dbo.LedgerMaster
                    WHERE   AccountId IN ( SELECT   ACC_ID
                                           FROM     dbo.ACCOUNT_MASTER
                                           WHERE    AG_ID = 24 ) )
                    AND CAST(TransactionDate AS DATE) = CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
          UNION ALL
          SELECT    ( ISNULL(SUM(CashIn), 0) - ISNULL(SUM(CashOut), 0) ) AS Amount ,
                    'Day Purchase' AS type ,
                    9 AS SrNo
          FROM      dbo.LedgerDetail
          WHERE     LedgerId IN (
                    SELECT  LedgerId
                    FROM    dbo.LedgerMaster
                    WHERE   AccountId IN ( SELECT   ACC_ID
                                           FROM     dbo.ACCOUNT_MASTER
                                           WHERE    AG_ID = 21 ) )
                    AND CAST(TransactionDate AS DATE) = CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
          UNION ALL
          SELECT    ( ISNULL(SUM(TotalAmountReceived), 0) ) AS Amount ,
                    'Day Receipt' AS type ,
                    10 AS SrNo
          FROM      dbo.PaymentTransaction
          WHERE     PM_TYPE = 1
                    AND StatusId <> 3
                    AND CAST(PaymentDate AS DATE) = CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
          UNION ALL
          SELECT    ISNULL(SUM(TotalAmountReceived), 0) AS Amount ,
                    'Day Payment' AS type ,
                    11 AS SrNo
          FROM      dbo.PaymentTransaction
          WHERE     PM_TYPE = 2
                    AND StatusId <> 3
                    AND CAST(PaymentDate AS DATE) = CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
          UNION ALL
          SELECT    ( ISNULL(SUM(SaleAmount - CnAmount), 0)
                      - ISNULL(SUM(PurchaseAmount), 0) ) AS Amount ,
                    'Day Profit/Loss' AS TYPE ,
                    13 AS SrNo
          FROM      ( SELECT    ITEM_ID ,
                                ( CAST(( CAST(( ISNULL(ITEM_QTY, 0)
                                                * ISNULL(ITEM_RATE, 0)
                                                - ( ISNULL(CASE
                                                              WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                           END, 0) ) ) AS DECIMAL(18,
                                                              2))
                                         - CASE WHEN GSTPaid = 'y'
                                                THEN ( ( ( ISNULL(ITEM_QTY, 0)
                                                           * ISNULL(ITEM_RATE,
                                                              0) )
                                                         - ( ISNULL(CASE
                                                              WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                              END, 0) ) )
                                                       - ( ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                             - ( ISNULL(CASE
                                                              WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                              END, 0) ) )
                                                           / ( 1 + VAT_PER
                                                              / 100 ) ) )
                                                ELSE 0
                                           END ) AS DECIMAL(18, 2)) ) AS SaleAmount ,
                                ( ISNULL(OD.ITEM_QTY, 0)
                                  * CAST(dbo.Get_Average_Rate_as_on_date(OD.ITEM_ID,
                                                              '" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "', 0,
                                                              1) AS DECIMAL(18,
                                                              2)) ) AS PurchaseAmount ,
                                0 AS CnAmount
                      FROM      dbo.SALE_INVOICE_MASTER OM
                                JOIN dbo.SALE_INVOICE_DETAIL OD ON OM.SI_ID = OD.SI_ID
                      WHERE     OM.INVOICE_STATUS <> 4
                                AND CAST(SI_DATE AS DATE) = CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
                      UNION ALL
                      SELECT    CreditNote_Id AS ID ,
                                0 ,
                                0 ,
                                ISNULL(SUM(CN_Amount), 0)
                                - ( ISNULL(SUM(Tax_Amt) + SUM(Cess_Amt), 0) ) AS CnAmount
                      FROM      dbo.CreditNote_Master
                      WHERE     CAST(CreditNote_Date AS DATE) = CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
                      GROUP BY  CreditNote_Id
                    ) tb
          UNION ALL
          SELECT    ISNULL(SUM(PurchaseAmount), 0) AS PurchaseCost ,
                    'Purchase Cost' AS TYPE ,
                    14 AS SrNo
          FROM      ( SELECT    ( ISNULL(OD.ITEM_QTY, 0)
                                  * CAST(dbo.Get_Average_Rate_as_on_date(OD.ITEM_ID,
                                                              '" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "', 0,
                                                              1) AS DECIMAL(18,
                                                              2)) ) AS PurchaseAmount
                      FROM      dbo.SALE_INVOICE_MASTER OM
                                JOIN dbo.SALE_INVOICE_DETAIL OD ON OM.SI_ID = OD.SI_ID
                      WHERE     OM.INVOICE_STATUS <> 4
                                AND CAST(SI_DATE AS DATE) = CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
                    ) tb
          UNION ALL
          SELECT    ISNULL(SUM(PurchaseAmount), 0) AS PurchaseCost ,
                    'Al Purchase Cost' AS TYPE ,
                    15 AS SrNo
          FROM      ( SELECT    ( ISNULL(OD.ITEM_QTY, 0)
                                  * CAST(dbo.Get_Average_Rate_as_on_date(OD.ITEM_ID,
                                                              '" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "', 0,
                                                              1) AS DECIMAL(18,
                                                              2)) ) AS PurchaseAmount
                      FROM      dbo.SALE_INVOICE_MASTER OM
                                JOIN dbo.SALE_INVOICE_DETAIL OD ON OM.SI_ID = OD.SI_ID
                      WHERE     OM.INVOICE_STATUS <> 4
                                AND CAST(SI_DATE AS DATE) < CAST('" & dtpFromDate.Value.ToString("MM/dd/yyyy") & "' AS DATE)
                    ) tb
        ) tb
ORDER BY SrNo"

        dt = objCommFunction.FillDataSet(Query).Tables(0)

        If dt.Rows.Count > 0 Then
            lblCashBalance.Text = "Rs. " & CrDr(dt.Rows(0)(0))
            lblBankBalance.Text = "Rs. " & CrDr(dt.Rows(1)(0))
            lblSale.Text = "Rs. " & CrDr(dt.Rows(2)(0))
            lblPurchase.Text = "Rs. " & CrDr(dt.Rows(3)(0))
            lblBillRec.Text = "Rs. " & CrDr(dt.Rows(4)(0))
            lblBillPay.Text = "Rs. " & CrDr(dt.Rows(5)(0))

            If dt.Rows(6)(0) > 0 Then
                lblProfit.Text = "Rs. " & Math.Abs(dt.Rows(6)(0))
                lblloss.Text = "Rs. 0.00"
            Else
                lblloss.Text = "Rs. " & Math.Abs(dt.Rows(6)(0))
                lblProfit.Text = "Rs. 0.00"
            End If

            lblDaySale.Text = "Rs. " & CrDr(dt.Rows(7)(0))
            lblDayPurchase.Text = "Rs. " & CrDr(dt.Rows(8)(0))
            lbldayRec.Text = "Rs. " & dt.Rows(9)(0)
            lblDayPayment.Text = "Rs. " & dt.Rows(10)(0)

            lblDayProfit_Loss.ForeColor = Color.Lime
            lblDayProfitText.Text = "Profit for the Day"

            If (dt.Rows(11)(0) < 0) Then
                lblDayProfit_Loss.ForeColor = Color.Salmon
                lblDayProfitText.Text = "Loss for the Day"
            End If

            lblDayProfit_Loss.Text = "Rs. " & Math.Abs(dt.Rows(11)(0))
            lblDayBalance.Text = "Rs. " & CrDr(Convert.ToDecimal(dt.Rows(9)(0)) - Convert.ToDecimal(dt.Rows(10)(0)))
            lbldaySaleCost.Text = "Rs. " & dt.Rows(7)(0)
            lbldayPurchaseCost.Text = "Rs. " & dt.Rows(12)(0)
            lblSaleCost.Text = "Rs. " & dt.Rows(2)(0)
            lblPurchaseCost.Text = "Rs. " & dt.Rows(13)(0)
        End If

        Cursor.Current = Cursors.Default

    End Sub

    Private Function CrDr(str As String) As String
        Dim dec As Decimal = Convert.ToDecimal(str)

        If dec > 0 Then
            str = str & " Cr"
        Else
            str = (dec * (-1)).ToString() & " Dr"
        End If
        Return str
    End Function

    Public Sub NewClick(sender As Object, e As EventArgs) Implements IForm.NewClick
        Throw New NotImplementedException()
    End Sub

    Public Sub SaveClick(sender As Object, e As EventArgs) Implements IForm.SaveClick
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseClick(sender As Object, e As EventArgs) Implements IForm.CloseClick
        Throw New NotImplementedException()
    End Sub

    Public Sub DeleteClick(sender As Object, e As EventArgs) Implements IForm.DeleteClick
        Throw New NotImplementedException()
    End Sub

    Public Sub ViewClick(sender As Object, e As EventArgs) Implements IForm.ViewClick
        Throw New NotImplementedException()
    End Sub

    Public Sub RefreshClick(sender As Object, e As EventArgs) Implements IForm.RefreshClick
        Throw New NotImplementedException()
    End Sub
End Class
