Imports Microsoft.Office.Interop
Imports MMSPlus

Public Class frm_SaleTaxRegister
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

    '    Private Sub BindData()

    '        Qry = "SELECT  [S. No.] ,
    '        Date ,
    '        [Bill NO.] ,
    '        [Account Name] ,
    '        ADDRESS ,
    '        [GST NO.] ,
    '        [Credit Amount] ,
    '        [Cash Amount] ,
    '        [Bill Amount] ,
    '        [Bill Type] ,
    '        [Nill Rated] ,
    '        [Txbl. 3%] ,
    '        [Tax @3%] ,
    '        [Txbl. 5%] ,
    '        [Tax @5%] ,
    '        [Txbl. 12%] ,
    '        [Tax @12%] ,
    '        [Txbl. 18%] ,
    '        [Tax @18%] ,
    '        [Txbl. 28%] ,
    '        [Tax @28%] ,
    '        ( ISNULL([Nill Rated], 0) + ISNULL([Txbl. 3%], 0) + ISNULL([Txbl. 5%],
    '                                                              0)
    '          + ISNULL([Txbl. 12%], 0) + ISNULL([Txbl. 18%], 0)
    '          + ISNULL([Txbl. 28%], 0) ) AS [Total Txbl. GST] ,
    '        ( ISNULL([Tax @3%], 0) + ISNULL([Tax @5%], 0) + ISNULL([Tax @12%], 0)
    '          + ISNULL([Tax @18%], 0) + ISNULL([Tax @28%], 0) ) AS [Total GST Tax] ,
    '        [Round Off] ,
    '        [CESS %] ,
    '        [Cess Amount] ,
    '        [ACess Amount]
    'FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY SIM.SI_ID ) AS [S. No.] ,
    '                    CONVERT(VARCHAR(20), SI_DATE, 106) AS Date ,
    '                    SI_CODE + CAST(si_no AS VARCHAR) AS [Bill NO.] ,
    '                    ACM.ACC_NAME AS [Account Name] ,
    '                    ISNULL(ACM.ADDRESS_PRIM, '') AS ADDRESS ,
    '                    ISNULL(ACM.VAT_NO, '') AS [GST NO.] ,
    '                    CASE WHEN sim.sale_type = 'Credit'
    '                         THEN ISNULL(NET_AMOUNT, 0)
    '                         ELSE 0
    '                    END AS [Credit Amount] ,
    '                    CASE WHEN sim.sale_type = 'Cash'
    '                         THEN ISNULL(NET_AMOUNT, 0)
    '                         ELSE 0
    '                    END AS [Cash Amount] ,
    '                    ISNULL(NET_AMOUNT, 0) AS [Bill Amount] ,
    '                    CASE WHEN INV_TYPE = 'I' THEN 'IGST'
    '                         WHEN INV_TYPE = 'S' THEN 'SGST/CGST'
    '                         WHEN INV_TYPE = 'U' THEN 'UTGST/CGST'
    '                    END AS [Bill Type] ,
    '                    CAST(SUM(CASE WHEN VAT_PER = 0
    '                                  THEN ( ISNULL(ITEM_QTY, 0)
    '                                         * ISNULL(ITEM_RATE, 0) )
    '                                       - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '                                                       THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                            / 100
    '                                                       ELSE DISCOUNT_VALUE
    '                                                  END, 0) )
    '                                  ELSE 0
    '                             END) AS DECIMAL(18, 2)) AS [Nill Rated] ,
    '                    CAST(( CAST(SUM(CASE WHEN VAT_PER = 3
    '                                         THEN ( ISNULL(ITEM_QTY, 0)
    '                                                * ISNULL(ITEM_RATE, 0) )
    '                                              - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                         END, 0) )
    '                                         ELSE 0
    '                                    END) AS DECIMAL(18, 2))
    '                           - SUM(CASE WHEN GSTPaid = 'y'
    '                                      THEN ( ( CASE WHEN VAT_PER = 3
    '                                                    THEN ( ISNULL(ITEM_QTY, 0)
    '                                                           * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                         - ( ISNULL(CASE
    '                                                              WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                              END, 0) )
    '                                                    ELSE 0
    '                                               END )
    '                                             - ( ( CASE WHEN VAT_PER = 3
    '                                                        THEN ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                             - ( ISNULL(CASE
    '                                                              WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                              END, 0) )
    '                                                        ELSE 0
    '                                                   END ) / ( 1 + VAT_PER / 100 ) ) )
    '                                      ELSE 0
    '                                 END) ) AS DECIMAL(18, 2)) AS [Txbl. 3%] ,
    '                    CAST(SUM(CASE WHEN VAT_PER = 3 THEN SID.VAT_AMOUNT
    '                                  ELSE 0
    '                             END) AS DECIMAL(18, 2)) AS [Tax @3%] ,
    '                    CAST(( CAST(SUM(CASE WHEN VAT_PER = 5
    '                                         THEN ( ISNULL(ITEM_QTY, 0)
    '                                                * ISNULL(ITEM_RATE, 0) )
    '                                              - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                         END, 0) )
    '                                         ELSE 0
    '                                    END) AS DECIMAL(18, 2))
    '                           - SUM(CASE WHEN GSTPaid = 'y'
    '                                      THEN ( ( CASE WHEN VAT_PER = 5
    '                                                    THEN ( ISNULL(ITEM_QTY, 0)
    '                                                           * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                         - ( ISNULL(CASE
    '                                                              WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                              END, 0) )
    '                                                    ELSE 0
    '                                               END )
    '                                             - ( ( CASE WHEN VAT_PER = 5
    '                                                        THEN ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                             - ( ISNULL(CASE
    '                                                              WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                              END, 0) )
    '                                                        ELSE 0
    '                                                   END ) / ( 1 + VAT_PER / 100 ) ) )
    '                                      ELSE 0
    '                                 END) ) AS DECIMAL(18, 2)) AS [Txbl. 5%] ,
    '                    CAST(SUM(CASE WHEN VAT_PER = 5 THEN SID.VAT_AMOUNT
    '                                  ELSE 0
    '                             END) AS DECIMAL(18, 2)) AS [Tax @5%] ,
    '                    CAST(( CAST(SUM(CASE WHEN VAT_PER = 12
    '                                         THEN ( ISNULL(ITEM_QTY, 0)
    '                                                * ISNULL(ITEM_RATE, 0) )
    '                                              - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                         END, 0) )
    '                                         ELSE 0
    '                                    END) AS DECIMAL(18, 2))
    '                           - SUM(CASE WHEN GSTPaid = 'y'
    '                                      THEN ( ( CASE WHEN VAT_PER = 12
    '                                                    THEN ( ISNULL(ITEM_QTY, 0)
    '                                                           * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                         - ( ISNULL(CASE
    '                                                              WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                              END, 0) )
    '                                                    ELSE 0
    '                                               END )
    '                                             - ( ( CASE WHEN VAT_PER = 12
    '                                                        THEN ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                             - ( ISNULL(CASE
    '                                                              WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                              END, 0) )
    '                                                        ELSE 0
    '                                                   END ) / ( 1 + VAT_PER / 100 ) ) )
    '                                      ELSE 0
    '                                 END) ) AS DECIMAL(18, 2)) AS [Txbl. 12%] ,
    '                    CAST(SUM(CASE WHEN VAT_PER = 12 THEN SID.VAT_AMOUNT
    '                                  ELSE 0
    '                             END) AS DECIMAL(18, 2)) AS [Tax @12%] ,
    '                    CAST(( CAST(SUM(CASE WHEN VAT_PER = 18
    '                                         THEN ( ISNULL(ITEM_QTY, 0)
    '                                                * ISNULL(ITEM_RATE, 0) )
    '                                              - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                         END, 0) )
    '                                         ELSE 0
    '                                    END) AS DECIMAL(18, 2))
    '                           - SUM(CASE WHEN GSTPaid = 'y'
    '                                      THEN ( ( CASE WHEN VAT_PER = 18
    '                                                    THEN ( ISNULL(ITEM_QTY, 0)
    '                                                           * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                         - ( ISNULL(CASE
    '                                                              WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                              END, 0) )
    '                                                    ELSE 0
    '                                               END )
    '                                             - ( ( CASE WHEN VAT_PER = 18
    '                                                        THEN ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                             - ( ISNULL(CASE
    '                                                              WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                              END, 0) )
    '                                                        ELSE 0
    '                                                   END ) / ( 1 + VAT_PER / 100 ) ) )
    '                                      ELSE 0
    '                                 END) ) AS DECIMAL(18, 2)) AS [Txbl. 18%] ,
    '                    CAST (SUM(CASE WHEN VAT_PER = 18 THEN SID.VAT_AMOUNT
    '                                   ELSE 0
    '                              END) AS DECIMAL(18, 2)) AS [Tax @18%] ,
    '                    CAST(( CAST(SUM(CASE WHEN VAT_PER = 28
    '                                         THEN ( ISNULL(ITEM_QTY, 0)
    '                                                * ISNULL(ITEM_RATE, 0) )
    '                                              - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                         END, 0) )
    '                                         ELSE 0
    '                                    END) AS DECIMAL(18, 2))
    '                           - SUM(CASE WHEN GSTPaid = 'y'
    '                                      THEN ( ( CASE WHEN VAT_PER = 28
    '                                                    THEN ( ISNULL(ITEM_QTY, 0)
    '                                                           * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                         - ( ISNULL(CASE
    '                                                              WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                              END, 0) )
    '                                                    ELSE 0
    '                                               END )
    '                                             - ( ( CASE WHEN VAT_PER = 28
    '                                                        THEN ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                             - ( ISNULL(CASE
    '                                                              WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( ISNULL(ITEM_QTY,
    '                                                              0)
    '                                                              * ISNULL(ITEM_RATE,
    '                                                              0) )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                              END, 0) )
    '                                                        ELSE 0
    '                                                   END ) / ( 1 + VAT_PER / 100 ) ) )
    '                                      ELSE 0
    '                                 END) ) AS DECIMAL(18, 2)) AS [Txbl. 28%] ,
    '                    CAST (SUM(CASE WHEN VAT_PER = 28 THEN SID.VAT_AMOUNT
    '                                   ELSE 0
    '                              END) AS DECIMAL(18, 2)) AS [Tax @28%] ,
    '                    ISNULL(NET_AMOUNT, 0) - ( ISNULL(GROSS_AMOUNT, 0)
    '                                              + ISNULL(SIM.VAT_AMOUNT, 0)
    '                                              + SUM(ISNULL(SID.CessAmount_num,
    '                                                           0))
    '                                              + CAST(SUM(ISNULL(SID.ACess, 0)
    '                                                         * ISNULL(SID.ITEM_QTY,
    '                                                              0)) AS NUMERIC(18,
    '                                                              2)) ) AS [Round Off] ,
    '                    MAX(SID.CessPercentage_num) AS [CESS %] ,
    '                    SUM(ISNULL(SID.CessAmount_num, 0)) AS [Cess Amount] ,
    '                    CAST(SUM(ISNULL(SID.ACess, 0) * ISNULL(SID.ITEM_QTY, 0)) AS NUMERIC(18,
    '                                                              2)) AS [ACess Amount]
    '          FROM      dbo.SALE_INVOICE_MASTER SIM
    '                    JOIN dbo.SALE_INVOICE_DETAIL SID ON SIM.SI_ID = SID.SI_ID
    '                    JOIN dbo.ACCOUNT_MASTER ACM ON ACM.ACC_ID = SIM.CUST_ID
    '          WHERE     INVOICE_STATUS <> 4
    '                    AND MONTH(SI_DATE) = " + txtFromDate.Value.Month.ToString() &
    '                    "
    '                    AND YEAR(SI_DATE) = " + txtFromDate.Value.Year.ToString() &
    '          "GROUP BY SIM.SI_ID ,
    '                    SI_DATE ,
    '                    SI_CODE ,
    '                    SI_NO ,
    '                    ACM.ACC_NAME ,
    '                    ADDRESS_PRIM ,
    '                    VAT_NO ,
    '                    NET_AMOUNT ,
    '                    GROSS_AMOUNT ,
    '                    SIM.VAT_AMOUNT ,
    '                    INV_TYPE ,
    '                    SALE_TYPE
    '        ) AS tbb
    '        "

    '        Dim dt As DataTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

    '        grdTaxReport.DataSource = dt


    '    End Sub

    Private Sub BindData()

        Qry = "SELECT  [S. No.] ,
        Date ,
        [Bill NO.] ,
        [Account Name] ,
        ADDRESS ,
        [GST NO.] ,
        [Credit Amount] ,
        [Cash Amount] ,
        [Bill Amount] ,
        [Bill Type] ,
        [Nill Rated] ,
        [Txbl. 3%] ,
        [Tax @3%] ,
        [Txbl. 5%] ,
        [Tax @5%] ,
        [Txbl. 12%] ,
        [Tax @12%] ,
        [Txbl. 18%] ,
        [Tax @18%] ,
        [Txbl. 28%] ,
        [Tax @28%] ,
        ( ISNULL([Nill Rated], 0) + ISNULL([Txbl. 3%], 0) + ISNULL([Txbl. 5%],
                                                              0)
          + ISNULL([Txbl. 12%], 0) + ISNULL([Txbl. 18%], 0)
          + ISNULL([Txbl. 28%], 0) ) AS [Total Txbl. GST] ,
        ( ISNULL([Tax @3%], 0) + ISNULL([Tax @5%], 0) + ISNULL([Tax @12%], 0)
          + ISNULL([Tax @18%], 0) + ISNULL([Tax @28%], 0) ) AS [Total GST Tax] ,
        [Round Off] ,
        [CESS %] ,
        [Cess Amount] ,
        [ACess Amount] ,
        Freight ,
        [Freight Tax]
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY SIM.SI_ID ) AS [S. No.] ,
                    CONVERT(VARCHAR(20), SI_DATE, 106) AS Date ,
                    SI_CODE + CAST(si_no AS VARCHAR) AS [Bill NO.] ,
                    ACM.ACC_NAME AS [Account Name] ,
                    ISNULL(ACM.ADDRESS_PRIM, '') AS ADDRESS ,
                    ISNULL(ACM.VAT_NO, '') AS [GST NO.] ,
                    CASE WHEN sim.sale_type = 'Credit'
                         THEN ISNULL(NET_AMOUNT, 0)
                         ELSE 0
                    END AS [Credit Amount] ,
                    CASE WHEN sim.sale_type = 'Cash'
                         THEN ISNULL(NET_AMOUNT, 0)
                         ELSE 0
                    END AS [Cash Amount] ,
                    ISNULL(NET_AMOUNT, 0) AS [Bill Amount] ,
                    CASE WHEN INV_TYPE = 'I' THEN 'IGST'
                         WHEN INV_TYPE = 'S' THEN 'SGST/CGST'
                         WHEN INV_TYPE = 'U' THEN 'UTGST/CGST'
                    END AS [Bill Type] ,
                    CAST(SUM(CASE WHEN VAT_PER = 0
                                  THEN ( ISNULL(ITEM_QTY, 0)
                                         * ISNULL(ITEM_RATE, 0) )
                                       - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                                       THEN ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                              * DISCOUNT_VALUE )
                                                            / 100
                                                       ELSE DISCOUNT_VALUE
                                                  END, 0) )
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS [Nill Rated] ,
                    CAST(( CAST(SUM(CASE WHEN VAT_PER = 3
                                         THEN ( ISNULL(ITEM_QTY, 0)
                                                * ISNULL(ITEM_RATE, 0) )
                                              - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                         END, 0) )
                                         ELSE 0
                                    END) AS DECIMAL(18, 2))
                           - SUM(CASE WHEN GSTPaid = 'y'
                                      THEN ( ( CASE WHEN VAT_PER = 3
                                                    THEN ( ISNULL(ITEM_QTY, 0)
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
                                                              END, 0) )
                                                    ELSE 0
                                               END )
                                             - ( ( CASE WHEN VAT_PER = 3
                                                        THEN ( ISNULL(ITEM_QTY,
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
                                                              END, 0) )
                                                        ELSE 0
                                                   END ) / ( 1 + VAT_PER / 100 ) ) )
                                      ELSE 0
                                 END) ) AS DECIMAL(18, 2)) AS [Txbl. 3%] ,
                    CAST(SUM(CASE WHEN VAT_PER = 3 THEN SID.VAT_AMOUNT
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS [Tax @3%] ,
                    CAST(( CAST(SUM(CASE WHEN VAT_PER = 5
                                         THEN ( ISNULL(ITEM_QTY, 0)
                                                * ISNULL(ITEM_RATE, 0) )
                                              - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                         END, 0) )
                                         ELSE 0
                                    END) AS DECIMAL(18, 2))
                           - SUM(CASE WHEN GSTPaid = 'y'
                                      THEN ( ( CASE WHEN VAT_PER = 5
                                                    THEN ( ISNULL(ITEM_QTY, 0)
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
                                                              END, 0) )
                                                    ELSE 0
                                               END )
                                             - ( ( CASE WHEN VAT_PER = 5
                                                        THEN ( ISNULL(ITEM_QTY,
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
                                                              END, 0) )
                                                        ELSE 0
                                                   END ) / ( 1 + VAT_PER / 100 ) ) )
                                      ELSE 0
                                 END) ) AS DECIMAL(18, 2)) AS [Txbl. 5%] ,
                    CAST(SUM(CASE WHEN VAT_PER = 5 THEN SID.VAT_AMOUNT
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS [Tax @5%] ,
                    CAST(( CAST(SUM(CASE WHEN VAT_PER = 12
                                         THEN ( ISNULL(ITEM_QTY, 0)
                                                * ISNULL(ITEM_RATE, 0) )
                                              - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                         END, 0) )
                                         ELSE 0
                                    END) AS DECIMAL(18, 2))
                           - SUM(CASE WHEN GSTPaid = 'y'
                                      THEN ( ( CASE WHEN VAT_PER = 12
                                                    THEN ( ISNULL(ITEM_QTY, 0)
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
                                                              END, 0) )
                                                    ELSE 0
                                               END )
                                             - ( ( CASE WHEN VAT_PER = 12
                                                        THEN ( ISNULL(ITEM_QTY,
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
                                                              END, 0) )
                                                        ELSE 0
                                                   END ) / ( 1 + VAT_PER / 100 ) ) )
                                      ELSE 0
                                 END) ) AS DECIMAL(18, 2)) AS [Txbl. 12%] ,
                    CAST(SUM(CASE WHEN VAT_PER = 12 THEN SID.VAT_AMOUNT
                                  ELSE 0
                             END) AS DECIMAL(18, 2)) AS [Tax @12%] ,
                    CAST(( CAST(SUM(CASE WHEN VAT_PER = 18
                                         THEN ( ISNULL(ITEM_QTY, 0)
                                                * ISNULL(ITEM_RATE, 0) )
                                              - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                         END, 0) )
                                         ELSE 0
                                    END) AS DECIMAL(18, 2))
                           - SUM(CASE WHEN GSTPaid = 'y'
                                      THEN ( ( CASE WHEN VAT_PER = 18
                                                    THEN ( ISNULL(ITEM_QTY, 0)
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
                                                              END, 0) )
                                                    ELSE 0
                                               END )
                                             - ( ( CASE WHEN VAT_PER = 18
                                                        THEN ( ISNULL(ITEM_QTY,
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
                                                              END, 0) )
                                                        ELSE 0
                                                   END ) / ( 1 + VAT_PER / 100 ) ) )
                                      ELSE 0
                                 END) ) AS DECIMAL(18, 2)) AS [Txbl. 18%] ,
                    CAST (SUM(CASE WHEN VAT_PER = 18 THEN SID.VAT_AMOUNT
                                   ELSE 0
                              END) AS DECIMAL(18, 2)) AS [Tax @18%] ,
                    CAST(( CAST(SUM(CASE WHEN VAT_PER = 28
                                         THEN ( ISNULL(ITEM_QTY, 0)
                                                * ISNULL(ITEM_RATE, 0) )
                                              - ( ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( ISNULL(ITEM_QTY,
                                                              0)
                                                              * ISNULL(ITEM_RATE,
                                                              0) )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                         END, 0) )
                                         ELSE 0
                                    END) AS DECIMAL(18, 2))
                           - SUM(CASE WHEN GSTPaid = 'y'
                                      THEN ( ( CASE WHEN VAT_PER = 28
                                                    THEN ( ISNULL(ITEM_QTY, 0)
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
                                                              END, 0) )
                                                    ELSE 0
                                               END )
                                             - ( ( CASE WHEN VAT_PER = 28
                                                        THEN ( ISNULL(ITEM_QTY,
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
                                                              END, 0) )
                                                        ELSE 0
                                                   END ) / ( 1 + VAT_PER / 100 ) ) )
                                      ELSE 0
                                 END) ) AS DECIMAL(18, 2)) AS [Txbl. 28%] ,
                    CAST (SUM(CASE WHEN VAT_PER = 28 THEN SID.VAT_AMOUNT
                                   ELSE 0
                              END) AS DECIMAL(18, 2)) AS [Tax @28%] ,
                    ISNULL(NET_AMOUNT, 0) - ( ISNULL(GROSS_AMOUNT, 0)
                                              + ISNULL(SIM.VAT_AMOUNT, 0)
                                              + SUM(ISNULL(SID.CessAmount_num,
                                                           0))
                                              + CAST(SUM(ISNULL(SID.ACess, 0)
                                                         * ISNULL(SID.ITEM_QTY,
                                                              0)) AS NUMERIC(18,
                                                              2)) ) AS [Round Off] ,
                    MAX(SID.CessPercentage_num) AS [CESS %] ,
                    SUM(ISNULL(SID.CessAmount_num, 0)) AS [Cess Amount] ,
                    CAST(SUM(ISNULL(SID.ACess, 0) * ISNULL(SID.ITEM_QTY, 0)) AS NUMERIC(18,
                                                              2)) AS [ACess Amount] ,
                    MAX(ISNULL(SIM.freight, 0)) AS Freight ,
                    MAX(ISNULL(SIM.FreightTaxValue, 0)) AS [Freight Tax]
          FROM      dbo.SALE_INVOICE_MASTER SIM
                    JOIN dbo.SALE_INVOICE_DETAIL SID ON SIM.SI_ID = SID.SI_ID
                    JOIN dbo.ACCOUNT_MASTER ACM ON ACM.ACC_ID = SIM.CUST_ID
          WHERE     INVOICE_STATUS <> 4
                    AND CAST(SI_DATE AS DATE) BETWEEN CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS DATE)
                                              AND     CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS DATE)
          GROUP BY  SIM.SI_ID ,
                    SI_DATE ,
                    SI_CODE ,
                    SI_NO ,
                    ACM.ACC_NAME ,
                    ADDRESS_PRIM ,
                    VAT_NO ,
                    NET_AMOUNT ,
                    GROSS_AMOUNT ,
                    SIM.VAT_AMOUNT ,
                    INV_TYPE ,
                    SALE_TYPE
        ) AS tbb"

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

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        BindData()
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        objCommFunction.ExportGridToExcel(grdTaxReport)
        MsgBox("Sale Register export successfully.", MsgBoxStyle.Information, gblMessageHeading)
    End Sub
End Class
