Imports Microsoft.Office.Interop
Imports MMSPlus

Public Class frm_PurchaseTaxRegister
    Implements IForm
    Dim objCommFunction As New CommonClass

    Dim Qry As String
    Dim _rights As Form_Rights


    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_GSTR_1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BindData()

        Qry = "SELECT  ROW_NUMBER() OVER ( ORDER BY Received_ID ) AS SrNo ,
        ReceivedDate AS [Received Date] ,
        BillNo AS [Bill No.] ,
        BillDate AS [Bill Date] ,
        CustomerName AS [Account Name] ,
        ADDRESS ,
        GSTNo AS [GST No.] ,
        BillAmount AS [Bill Amount] ,
        BillType AS [Bill Type] ,
        GST0 AS [Nill Rated] ,
        GST3 AS [Txbl. 3%] ,
        GST3Tax AS [Tax @3%] ,
        GST5 AS [Txbl. 5%] ,
        GST5Tax AS [Tax @5%] ,
        GST12 AS [Txbl. 12%] ,
        GST12Tax AS [Tax @12%] ,
        GST18 AS [Txbl. 18%] ,
        GST18Tax AS [Tax @18%] ,
        GST28 AS [Txbl. 28%] ,
        GST28Tax AS [Tax @28%] ,
        TaxableAmt AS [Total Txbl. GST] ,
        TotalTax AS [Total GST Tax] ,
        otherAmount
FROM    ( SELECT    MRWPM.Received_ID ,
                    CONVERT(VARCHAR(20), Received_Date, 106) ReceivedDate ,
                    Invoice_No AS BillNo ,
                    CONVERT(VARCHAR(20), Invoice_Date, 106) AS BillDate ,
                    ACM.ACC_NAME AS CustomerName ,
                    ACM.ADDRESS_PRIM AS ADDRESS ,
                    ACM.VAT_NO AS GSTNo ,
                    MRWPM.NET_AMOUNT AS BillAmount ,
                    CASE WHEN MRWPM.MRN_TYPE = 1 THEN 'SGST/CGST'
                         WHEN MRWPM.MRN_TYPE = 2 THEN 'IGST'
                         WHEN MRWPM.MRN_TYPE = 3 THEN 'UTGST/CGST'
                    END AS BillType ,
                    CAST(SUM(CASE WHEN Item_vat = 0
                                  THEN ( Item_Qty * Item_Rate )
                                       - ( ISNULL(CASE WHEN DType = 'P'
                                                       THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                            / 100
                                                       ELSE DiscountValue
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS GST0 ,
                    CAST(SUM(CASE WHEN Item_vat = 3
                                  THEN ( Item_Qty * Item_Rate )
                                       - ( ISNULL(CASE WHEN DType = 'P'
                                                       THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                            / 100
                                                       ELSE DiscountValue
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS GST3 ,
                    CAST(CAST(SUM(CASE WHEN Item_vat = 3
                                       THEN ( Item_Qty * Item_Rate )
                                            - ( ISNULL(CASE WHEN DType = 'P'
                                                            THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                            ELSE DiscountValue
                                                       END, 0) )
                                       ELSE 0
                                  END * Item_vat / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST3Tax ,
                    CAST(SUM(CASE WHEN Item_vat = 5
                                  THEN ( Item_Qty * Item_Rate )
                                       - ( ISNULL(CASE WHEN DType = 'P'
                                                       THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                            / 100
                                                       ELSE DiscountValue
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS GST5 ,
                    CAST(CAST(SUM(CASE WHEN Item_vat = 5
                                       THEN ( Item_Qty * Item_Rate )
                                            - ( ISNULL(CASE WHEN DType = 'P'
                                                            THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                            ELSE DiscountValue
                                                       END, 0) )
                                       ELSE 0
                                  END * Item_vat / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST5Tax ,
                    CAST(SUM(CASE WHEN Item_vat = 12
                                  THEN ( Item_Qty * Item_Rate )
                                       - ( ISNULL(CASE WHEN DType = 'P'
                                                       THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                            / 100
                                                       ELSE DiscountValue
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS GST12 ,
                    CAST(CAST(SUM(CASE WHEN Item_vat = 12
                                       THEN ( Item_Qty * Item_Rate )
                                            - ( ISNULL(CASE WHEN DType = 'P'
                                                            THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                            ELSE DiscountValue
                                                       END, 0) )
                                       ELSE 0
                                  END * Item_vat / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST12Tax ,
                    CAST(SUM(CASE WHEN Item_vat = 18
                                  THEN ( Item_Qty * Item_Rate )
                                       - ( ISNULL(CASE WHEN DType = 'P'
                                                       THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                            / 100
                                                       ELSE DiscountValue
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS GST18 ,
                    CAST(CAST(SUM(CASE WHEN Item_vat = 18
                                       THEN ( Item_Qty * Item_Rate )
                                            - ( ISNULL(CASE WHEN DType = 'P'
                                                            THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                            ELSE DiscountValue
                                                       END, 0) )
                                       ELSE 0
                                  END * Item_vat / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST18Tax ,
                    CAST(SUM(CASE WHEN Item_vat = 28
                                  THEN ( Item_Qty * Item_Rate )
                                       - ( ISNULL(CASE WHEN DType = 'P'
                                                       THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                            / 100
                                                       ELSE DiscountValue
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS GST28 ,
                    CAST(CAST(SUM(CASE WHEN Item_vat = 28
                                       THEN ( Item_Qty * Item_Rate )
                                            - ( ISNULL(CASE WHEN DType = 'P'
                                                            THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                            ELSE DiscountValue
                                                       END, 0) )
                                       ELSE 0
                                  END * Item_vat / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST28Tax ,
                    MAX(ISNULL(Other_Charges, 0) + ISNULL(freight, 0)) AS otherAmount ,
                    MAX(ISNULL(GROSS_AMOUNT, 0)) AS TaxableAmt ,
                    MAX(ISNULL(GST_AMOUNT, 0)) TotalTax
          FROM      dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER MRWPM
                    JOIN dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MRWPD ON MRWPD.Received_ID = MRWPM.Received_ID
                    JOIN dbo.ACCOUNT_MASTER ACM ON ACM.ACC_ID = MRWPM.Vendor_ID
                    where  MONTH(Received_Date) =" + txtFromDate.Value.Month.ToString() &
                    " AND YEAR(Received_Date) = " + txtFromDate.Value.Year.ToString() &
         " GROUP BY  MRWPM.Received_ID ,
                    Received_Date ,
                    Invoice_No ,
                    Invoice_Date ,
                    ACC_NAME ,
                    ADDRESS_PRIM ,
                    VAT_NO ,
                    NET_AMOUNT ,
                    MRN_TYPE
          UNION ALL
          SELECT    MRAPM.Receipt_ID ,
                    CONVERT(VARCHAR(20), Receipt_Date, 106) ReceivedDate ,
                    Invoice_No AS BillNo ,
                    CONVERT(VARCHAR(20), Invoice_Date, 106) AS BillDate ,
                    ACM.ACC_NAME AS CustomerName ,
                    ACM.ADDRESS_PRIM AS ADDRESS ,
                    ACM.VAT_NO AS GSTNo ,
                    MRAPM.NET_AMOUNT AS BillAmount ,
                    CASE WHEN MRAPM.MRN_TYPE = 1 THEN 'SGST/CGST'
                         WHEN MRAPM.MRN_TYPE = 2 THEN 'IGST'
                         WHEN MRAPM.MRN_TYPE = 3 THEN 'UTGST/CGST'
                    END AS BillType ,
                    CAST(SUM(CASE WHEN Vat_Per = 0
                                  THEN ( Item_Qty * Item_Rate )
                                       - ( ISNULL(CASE WHEN DType = 'P'
                                                       THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                            / 100
                                                       ELSE DiscountValue
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS GST0 ,
                    CAST(SUM(CASE WHEN Vat_Per = 3
                                  THEN ( Item_Qty * Item_Rate )
                                       - ( ISNULL(CASE WHEN DType = 'P'
                                                       THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                            / 100
                                                       ELSE DiscountValue
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS GST3 ,
                    CAST(CAST(SUM(CASE WHEN Vat_Per = 3
                                       THEN ( Item_Qty * Item_Rate )
                                            - ( ISNULL(CASE WHEN DType = 'P'
                                                            THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                            ELSE DiscountValue
                                                       END, 0) )
                                       ELSE 0
                                  END * Vat_Per / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST3Tax ,
                    CAST(SUM(CASE WHEN Vat_Per = 5
                                  THEN ( Item_Qty * Item_Rate )
                                       - ( ISNULL(CASE WHEN DType = 'P'
                                                       THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                            / 100
                                                       ELSE DiscountValue
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS GST5 ,
                    CAST(CAST(SUM(CASE WHEN Vat_Per = 5
                                       THEN ( Item_Qty * Item_Rate )
                                            - ( ISNULL(CASE WHEN DType = 'P'
                                                            THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                            ELSE DiscountValue
                                                       END, 0) )
                                       ELSE 0
                                  END * Vat_Per / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST5Tax ,
                    CAST(SUM(CASE WHEN Vat_Per = 12
                                  THEN ( Item_Qty * Item_Rate )
                                       - ( ISNULL(CASE WHEN DType = 'P'
                                                       THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                            / 100
                                                       ELSE DiscountValue
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS GST12 ,
                    CAST(CAST(SUM(CASE WHEN Vat_Per = 12
                                       THEN ( Item_Qty * Item_Rate )
                                            - ( ISNULL(CASE WHEN DType = 'P'
                                                            THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                            ELSE DiscountValue
                                                       END, 0) )
                                       ELSE 0
                                  END * Vat_Per / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST12Tax ,
                    CAST(SUM(CASE WHEN Vat_Per = 18
                                  THEN ( Item_Qty * Item_Rate )
                                       - ( ISNULL(CASE WHEN DType = 'P'
                                                       THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                            / 100
                                                       ELSE DiscountValue
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS GST18 ,
                    CAST(CAST(SUM(CASE WHEN Vat_Per = 18
                                       THEN ( Item_Qty * Item_Rate )
                                            - ( ISNULL(CASE WHEN DType = 'P'
                                                            THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                            ELSE DiscountValue
                                                       END, 0) )
                                       ELSE 0
                                  END * Vat_Per / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST18Tax ,
                    CAST(SUM(CASE WHEN Vat_Per = 28
                                  THEN ( Item_Qty * Item_Rate )
                                       - ( ISNULL(CASE WHEN DType = 'P'
                                                       THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                            / 100
                                                       ELSE DiscountValue
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS GST28 ,
                    CAST(CAST(SUM(CASE WHEN Vat_Per = 28
                                       THEN ( Item_Qty * Item_Rate )
                                            - ( ISNULL(CASE WHEN DType = 'P'
                                                            THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                            ELSE DiscountValue
                                                       END, 0) )
                                       ELSE 0
                                  END * Vat_Per / 100) AS DECIMAL(18, 2)) AS DECIMAL(18,
                                                              2)) AS GST28Tax ,
                    MAX(ISNULL(Other_Charges, 0) + ISNULL(freight, 0)) AS otherAmount ,
                    MAX(ISNULL(GROSS_AMOUNT, 0)) AS TaxableAmt ,
                    MAX(ISNULL(GST_AMOUNT, 0)) TotalTax
          FROM      dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER MRAPM
                    JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL MRAPD ON MRAPD.Receipt_ID = MRAPM.Receipt_ID
                    JOIN dbo.ACCOUNT_MASTER ACM ON ACM.ACC_ID = MRAPM.CUST_ID
                    where  MONTH(Receipt_Date) =" + txtFromDate.Value.Month.ToString() &
                    " AND YEAR(Receipt_Date) = " + txtFromDate.Value.Year.ToString() &
          " GROUP BY  MRAPM.Receipt_ID ,
                    Receipt_Date ,
                    Invoice_No ,
                    Invoice_Date ,
                    ACC_NAME ,
                    ADDRESS_PRIM ,
                    VAT_NO ,
                    NET_AMOUNT ,
                    MRN_TYPE
        ) tb
        "

        Dim dt As DataTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

        grdTaxReport.DataSource = dt


    End Sub

    Public Sub NewClick(sender As Object, e As EventArgs) Implements IForm.NewClick
    End Sub

    Public Sub SaveClick(sender As Object, e As EventArgs) Implements IForm.SaveClick
    End Sub

    Public Sub CloseClick(sender As Object, e As EventArgs) Implements IForm.CloseClick
    End Sub

    Public Sub DeleteClick(sender As Object, e As EventArgs) Implements IForm.DeleteClick
    End Sub

    Public Sub ViewClick(sender As Object, e As EventArgs) Implements IForm.ViewClick
    End Sub

    Public Sub RefreshClick(sender As Object, e As EventArgs) Implements IForm.RefreshClick
    End Sub


    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        objCommFunction.ExportGridToExcel(grdTaxReport)
    End Sub

    Private Sub btnGenerate_Click_1(sender As Object, e As EventArgs) Handles btnGenerate.Click
        BindData()
    End Sub
End Class
