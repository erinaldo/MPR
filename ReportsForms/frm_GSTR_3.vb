Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms

Public Class frm_GSTR_3
    Implements IForm

    Dim objCommFunction As New CommonClass
    Dim row As DataRow
    Private stateId As String
    Dim Qry As String
    Dim _rights As Form_Rights

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_GSTR_3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        stateId = objCommFunction.ExecuteScalar("SELECT STATE_ID FROM dbo.CITY_MASTER WHERE CITY_ID IN(SELECT TOP 1 CITY_ID FROM dbo.DIVISION_SETTINGS)")
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        ImportData()
    End Sub

    Private Sub ImportData()
        Dim path As String = System.Windows.Forms.Application.StartupPath + "\ExcelTemplate\GSTR3_Template.xlsx"

        Dim sfd As New SaveFileDialog
        sfd.CheckFileExists = False
        If sfd.ShowDialog <> System.Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        'Dim xlApp As Excel.Application
        'Dim xlWorkBook As Excel.Workbook
        'xlApp = New Excel.ApplicationClass

        Dim xlApp As Object 'Excel.Application
        Dim xlWorkBook As Object 'Excel.Workbook

        xlApp = CreateObject("Excel.Application")

        xlWorkBook = xlApp.Workbooks.Open(path)

        Try
            WriteData(xlWorkBook)
            xlWorkBook.SaveAs(sfd.FileName)
        Finally
            xlWorkBook.Close(SaveChanges:=False)
            xlApp.Quit()
            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            MsgBox("GSTR 3 exported sucessfully", MsgBoxStyle.Information, gblMessageHeading)
        End Try
    End Sub

    Private Sub WriteData(xlWorkBook As Object)
        ' Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("GSTR3")
        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("GSTR3")

        xlWorkSheet = AddBasicData(xlWorkSheet)
        xlWorkSheet = Add3_1SectionData(xlWorkSheet)
        xlWorkSheet = Add3_2SectionData(xlWorkSheet)
        xlWorkSheet = Add4SectionData(xlWorkSheet)
        xlWorkSheet = Add5SectionData(xlWorkSheet)
        xlWorkSheet = Add5SectionDataNNGST(xlWorkSheet)
    End Sub

    Private Function AddBasicData(xlWorkSheet As Object) As Object
        row = GetBasicData()
        If (txtFromDate.Value.ToString("yyyy") = txtToDate.Value.ToString("yyyy")) Then
            xlWorkSheet.Cells(4, 15) = txtFromDate.Value.ToString("yyyy")
        Else
            xlWorkSheet.Cells(4, 15) = txtFromDate.Value.ToString("yyyy") & "-" & txtToDate.Value.ToString("yyyy")
        End If

        If (txtFromDate.Value.ToString("MMMM") = txtToDate.Value.ToString("MMMM")) Then
            xlWorkSheet.Cells(5, 15) = txtFromDate.Value.ToString("MMMM")
        Else
            xlWorkSheet.Cells(5, 15) = txtFromDate.Value.ToString("MMMM") & "-" & txtToDate.Value.ToString("MMMM")
        End If

        Dim colIndex As Int16 = 2
        For Each ch As Char In row("TIN_NO").ToString
            xlWorkSheet.Cells(6, colIndex) = ch.ToString
            colIndex += 1
        Next
        xlWorkSheet.Cells(7, 2) = row("DIVISION_NAME")
        Return xlWorkSheet
    End Function

    Private Function Add3_1SectionData(xlWorkSheet As Object) As Object
        row = Get3_1_SectionData(" AND(VAT_PER)>0")
        xlWorkSheet.Cells(11, 2) = row("Taxable_Value")
        xlWorkSheet.Cells(11, 5) = row("integrated_tax")
        xlWorkSheet.Cells(11, 8) = row("non_integrated_tax") / 2
        xlWorkSheet.Cells(11, 11) = row("non_integrated_tax") / 2
        xlWorkSheet.Cells(11, 14) = row("Cess_Amount")

        row = Get3_1_SectionData(" AND(VAT_PER)=0")
        xlWorkSheet.Cells(13, 2) = row("Taxable_Value")
        xlWorkSheet.Cells(13, 5) = row("integrated_tax")
        xlWorkSheet.Cells(13, 8) = row("non_integrated_tax") / 2
        xlWorkSheet.Cells(13, 11) = row("non_integrated_tax") / 2
        xlWorkSheet.Cells(13, 14) = row("Cess_Amount")

        'row = Get3_1_SectionData(" And LEN(ISNUll(am.VAT_NO,'')) = 0 ")
        'xlWorkSheet.Cells(15, 2) = row("Taxable_Value")
        'xlWorkSheet.Cells(15, 5) = row("integrated_tax")
        'xlWorkSheet.Cells(15, 8) = row("non_integrated_tax") / 2
        'xlWorkSheet.Cells(15, 11) = row("non_integrated_tax") / 2
        'xlWorkSheet.Cells(15, 14) = row("Cess_Amount")

        row = Get3_1_SectionData("")
        xlWorkSheet.Cells(16, 2) = row("Taxable_Value")
        xlWorkSheet.Cells(16, 5) = row("integrated_tax")
        xlWorkSheet.Cells(16, 8) = row("non_integrated_tax") / 2
        xlWorkSheet.Cells(16, 11) = row("non_integrated_tax") / 2
        xlWorkSheet.Cells(16, 14) = row("Cess_Amount")
        Return xlWorkSheet
    End Function

    Dim noOfRowsInserted As Int32 = 0
    Private Function Add3_2SectionData(xlWorkSheet As Object) As Object
        Dim table As System.Data.DataTable = Get3_2_SectionData()
        Dim rowIndex As Int16 = 21
        If table.Rows.Count > 0 Then
            noOfRowsInserted = table.Rows.Count
            For Each row As DataRow In table.Rows
                'CType(xlWorkSheet.Rows(20), Range).Select()
                ' CType(xlWorkSheet.Rows(rowIndex), Excel.Range).Insert(Excel.XlInsertShiftDirection.xlShiftDown, True)
                xlWorkSheet.Range("B" & rowIndex.ToString & ":F" & rowIndex.ToString).MergeCells = True
                xlWorkSheet.Range("G" & rowIndex.ToString & ":K" & rowIndex.ToString).MergeCells = True
                xlWorkSheet.Range("L" & rowIndex.ToString & ":P" & rowIndex.ToString).MergeCells = True
                xlWorkSheet.Cells(rowIndex, 2) = row("STATE_CODE") & "-" & row("STATE_NAME")
                xlWorkSheet.Cells(rowIndex, 7) = row("Taxable_Value")
                xlWorkSheet.Cells(rowIndex, 12) = row("integrated_tax")
                rowIndex += 1
            Next
            xlWorkSheet.Cells(rowIndex, 7) = table.Compute("sum(Taxable_Value)", Nothing)
            xlWorkSheet.Cells(rowIndex, 12) = table.Compute("sum(integrated_tax)", Nothing)
        End If
        Return xlWorkSheet
    End Function

    Dim rowIndex As Int16 = 34
    Private Function Add4SectionData(xlWorkSheet As Object) As Object
        rowIndex = 34 + noOfRowsInserted
        row = Get4_SectionDataForPurchase()
        Dim drrow As DataRow = Get4_SectionDataForPurchaseReturn()

        xlWorkSheet.Cells(rowIndex, 2) = row("integrated_tax") - drrow("integrated_tax")
        xlWorkSheet.Cells(rowIndex, 6) = (row("non_integrated_tax") / 2) - (drrow("non_integrated_tax") / 2)
        xlWorkSheet.Cells(rowIndex, 10) = (row("non_integrated_tax") / 2) - (drrow("non_integrated_tax") / 2)
        xlWorkSheet.Cells(rowIndex, 14) = row("CessAmt") - drrow("CessAmt")

        rowIndex += 3
        'row = Get4_SectionDataForPurchaseReturn()
        'xlWorkSheet.Cells(rowIndex, 2) = row("integrated_tax")
        'xlWorkSheet.Cells(rowIndex, 6) = row("non_integrated_tax") / 2
        'xlWorkSheet.Cells(rowIndex, 10) = row("non_integrated_tax") / 2
        'xlWorkSheet.Cells(rowIndex, 14) = 0

        Return xlWorkSheet
    End Function

    Private Function Add5SectionData(xlWorkSheet As Object) As Object
        rowIndex = 45 + noOfRowsInserted
        row = Get5SectionData()
        xlWorkSheet.Cells(rowIndex, 5) = row("InterState_TaxableValue")
        xlWorkSheet.Cells(rowIndex, 11) = row("IntraState_TaxableValue")
        Return xlWorkSheet
    End Function

    Private Function Add5SectionDataNNGST(xlWorkSheet As Object) As Object
        rowIndex = 46 + noOfRowsInserted
        row = Get5SectionDataNONGST()
        xlWorkSheet.Cells(rowIndex, 5) = row("InterState_TaxableValue")
        xlWorkSheet.Cells(rowIndex, 11) = row("IntraState_TaxableValue")
        Return xlWorkSheet
    End Function

    Private Function Get5SectionDataNONGST() As DataRow

        Qry = "SELECT ISNULL(SUM(CASE WHEN INV_TYPE <> 'I' THEN Taxable_Value
                        ELSE 0
                   END), 0) AS IntraState_TaxableValue ,
                    ISNULL(SUM(CASE WHEN INV_TYPE = 'I' THEN Taxable_Value
                        ELSE 0
                   END), 0) AS InterState_TaxableValue
          FROM   ( SELECT   CASE WHEN pt.fk_GST_ID = 1 THEN 0.00
                         WHEN pt.fk_GST_ID = 2 THEN 5.00
                         WHEN pt.fk_GST_ID = 3 THEN 12.00
                         WHEN pt.fk_GST_ID = 4 THEN 18.00
                         WHEN pt.fk_GST_ID = 5 THEN 28.00
                         WHEN pt.fk_GST_ID = 6 THEN 3.00
                    END AS VAT_PER ,
                    SUM(( ISNULL(pt.TotalAmountReceived, 0) )) AS Taxable_Value ,
                    CASE WHEN cm.STATE_ID <> " & stateId & " THEN 'I'
                         WHEN cm.STATE_ID =  " & stateId & " THEN 'S'
                    END AS Inv_Type
          FROM      dbo.PaymentTransaction pt
                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = pt.AccountId
                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
          WHERE     pt.FK_GST_TYPE_ID = 3 AND pt.StatusId <> 3
                    AND CAST(pt.PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
          GROUP BY  cm.STATE_ID, sm.IsUT_Bit, pt.fk_GST_ID
        ) tb"

        Return objCommFunction.Fill_DataSet(Qry).Tables(0).Rows(0)
    End Function

    Private Function Get5SectionData() As DataRow

        Qry = "SELECT  SUM(ISNULL(IntraState_TaxableValue, 0)) AS IntraState_TaxableValue ,
        SUM(ISNULL(InterState_TaxableValue, 0)) AS InterState_TaxableValue
FROM    ( SELECT    SUM(ISNULL(IntraState_TaxableValue, 0)) AS IntraState_TaxableValue ,
                    SUM(ISNULL(InterState_TaxableValue, 0)) AS InterState_TaxableValue
          FROM      ( SELECT    CASE WHEN MRN_TYPE <> 2
                                     THEN ROUND(( ( totalamt - ( discountamt
                                                              + gpaid ) )
                                                  * Bal_Item_Vat / 100 ), 2)
                                     ELSE 0
                                END AS IntraState_TaxableValue ,
                                CASE WHEN MRN_TYPE = 2
                                     THEN ROUND(( ( totalamt - ( discountamt
                                                              + gpaid ) )
                                                  * Bal_Item_Vat / 100 ), 2)
                                     ELSE 0
                                END AS InterState_TaxableValue ,
                                totalamt ,
                                discountamt ,
                                gpaid ,
                                Bal_Item_Vat ,
                                MRN_TYPE
                      FROM      ( SELECT    MRWPM.MRN_TYPE ,
                                            MRWPD.Bal_Item_Vat ,
                                            ROUND(MRWPD.Bal_Item_Rate
                                                  * MRWPD.Bal_Item_Qty, 2) AS totalamt ,
                                            CASE WHEN MRWPD.Dtype = 'P'
                                                 THEN ROUND(( MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty
                                                              * MRWPD.DiscountValue )
                                                            / 100, 2)
                                                      + ( ( ROUND(MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty,
                                                              2)
                                                            - ROUND(( MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty
                                                              * MRWPD.DiscountValue )
                                                              / 100, 2) )
                                                          * MRWPD.DiscountValue1
                                                          / 100 )
                                                 WHEN MRWPD.Dtype = 'A'
                                                 THEN ROUND(MRWPD.DiscountValue,
                                                            2)
                                                      + ROUND(MRWPD.DiscountValue1,
                                                              2)
                                            END AS discountamt ,
                                            CASE WHEN MRWPD.Dtype = 'P'
                                                 THEN ROUND(MRWPD.Bal_Item_Rate
                                                            * MRWPD.Bal_Item_Qty,
                                                            2)
                                                      - ( ROUND(( MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty
                                                              * MRWPD.DiscountValue )
                                                              / 100, 2)
                                                          + ( ( ROUND(MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty,
                                                              2)
                                                              - ROUND(( MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty
                                                              * MRWPD.DiscountValue )
                                                              / 100, 2) )
                                                              * MRWPD.DiscountValue1
                                                              / 100 ) )
                                                 WHEN MRWPD.Dtype = 'A'
                                                 THEN ROUND(MRWPD.Bal_Item_Rate
                                                            * MRWPD.Bal_Item_Qty,
                                                            2)
                                                      - ( ROUND(MRWPD.DiscountValue,
                                                              2)
                                                          + ROUND(MRWPD.DiscountValue1,
                                                              2) )
                                            END AS itemvalue ,
                                            CASE WHEN MRWPD.GSTPAID = 'Y'
                                                 THEN CASE WHEN MRWPD.Dtype = 'P'
                                                           THEN ( ( ROUND(MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty,
                                                              2)
                                                              - ( ROUND(( MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty
                                                              * MRWPD.DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty,
                                                              2)
                                                              - ROUND(( MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty
                                                              * MRWPD.DiscountValue )
                                                              / 100, 2) )
                                                              * MRWPD.DiscountValue1
                                                              / 100 ) ) )
                                                              - ( ( ROUND(MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty,
                                                              2)
                                                              - ( ROUND(( MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty
                                                              * MRWPD.DiscountValue )
                                                              / 100, 2)
                                                              + ( ( ROUND(MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty,
                                                              2)
                                                              - ROUND(( MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty
                                                              * MRWPD.DiscountValue )
                                                              / 100, 2) )
                                                              * MRWPD.DiscountValue1
                                                              / 100 ) ) )
                                                              / ( 1
                                                              + ( MRWPD.Bal_Item_Vat
                                                              / 100 ) ) ) )
                                                           WHEN MRWPD.Dtype = 'A'
                                                           THEN ( ( ROUND(MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty,
                                                              2)
                                                              - ( ROUND(MRWPD.DiscountValue,
                                                              2)
                                                              + ROUND(MRWPD.DiscountValue1,
                                                              2) ) )
                                                              - ( ( ROUND(MRWPD.Bal_Item_Rate
                                                              * MRWPD.Bal_Item_Qty,
                                                              2)
                                                              - ( ROUND(MRWPD.DiscountValue,
                                                              2)
                                                              + ROUND(MRWPD.DiscountValue1,
                                                              2) ) ) / ( 1
                                                              + ( MRWPD.Bal_Item_Vat
                                                              / 100 ) ) ) )
                                                      END
                                                 ELSE 0.00
                                            END AS gpaid
                                  FROM      dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER MRWPM
                                            JOIN dbo.MATERIAL_RECEIVED_WITHOUT_PO_Detail MRWPD ON MRWPD.Received_ID = MRWPM.Received_ID
                                            INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = MRWPM.Vendor_ID
                                            INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                                            INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                                  WHERE     MRWPD.Item_vat = 0
                                            AND cast(RECEIVED_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                                ) purchase
                    ) main
          UNION ALL
          SELECT    SUM(ISNULL(IntraState_TaxableValue, 0)) AS IntraState_TaxableValue ,
                    SUM(ISNULL(InterState_TaxableValue, 0)) AS InterState_TaxableValue
          FROM      ( SELECT    SUM(CASE WHEN MRAPM.MRN_TYPE <> 2
                                         THEN ( ( Item_Qty * Item_Rate )
                                                - ISNULL(CASE WHEN DTYPE = 'P'
                                                              THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                              ELSE DiscountValue
                                                         END, 0) )
                                         ELSE 0
                                    END) AS IntraState_TaxableValue ,
                                SUM(CASE WHEN MRAPM.MRN_TYPE = 2
                                         THEN ( ( Item_Qty * Item_Rate )
                                                - ISNULL(CASE WHEN DTYPE = 'P'
                                                              THEN ( ( Item_Qty
                                                              * Item_Rate )
                                                              * DiscountValue )
                                                              / 100
                                                              ELSE DiscountValue
                                                         END, 0) )
                                         ELSE 0
                                    END) AS InterState_TaxableValue
                      FROM      dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER MRAPM
                                JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_Detail MRAPD ON MRAPD.Receipt_ID = MRAPM.Receipt_ID
                                INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = MRAPM.CUST_ID
                                INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                                INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                      WHERE     MRAPD.Vat_Per = 0
                                AND cast(RECEIPT_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                    ) main1
          UNION ALL
          SELECT    SUM(IntraState_TaxableValue) AS IntraState_TaxableValue ,
                    SUM(InterState_TaxableValue) AS InterState_TaxableValue 
          FROM      ( SELECT    CASE WHEN sm.STATE_ID <> " & stateId & "
                                                THEN SUM(( ISNULL(pt.TotalAmountReceived, 0) ))
                                                ELSE 0
                                           END  AS IntraState_TaxableValue ,
                                CASE WHEN sm.STATE_ID = " & stateId & "
                                                THEN SUM(( ISNULL(pt.TotalAmountReceived, 0) ))
                                                ELSE 0
                                           END  AS InterState_TaxableValue 
                      FROM      dbo.PaymentTransaction pt
								INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = pt.BankId
								INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
								INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                      WHERE     pt.GST_Applicable_Acc = 'Dr.'
								and pt.FK_GST_TYPE_ID = 2 and pt.StatusId <> 3
								AND pt.fk_GST_ID = 1
                                AND cast(pt.PaymentDate AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
								GROUP BY sm.STATE_ID 
                    ) tb 

          UNION ALL
          SELECT    SUM(IntraState_TaxableValue) AS IntraState_TaxableValue ,
                    SUM(InterState_TaxableValue) AS InterState_TaxableValue
          FROM      ( SELECT    ISNULL(SUM(CASE WHEN MRN_TYPE <> 2
                                                THEN CAST(( d.Item_Qty
                                                            * d.Item_Rate ) AS NUMERIC(18,
                                                              2))
                                                ELSE 0
                                           END), 0) * -1 AS IntraState_TaxableValue ,
                                ISNULL(SUM(CASE WHEN MRN_TYPE = 2
                                                THEN CAST(( d.Item_Qty
                                                            * d.Item_Rate ) AS NUMERIC(18,
                                                              2))
                                                ELSE 0
                                           END), 0) * -1 AS InterState_TaxableValue
                      FROM      dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER MRWPM
                                JOIN dbo.DebitNote_Master M ON M.MRNId = MRWPM.MRN_NO
                                JOIN dbo.DebitNote_Detail D ON M.DebitNote_Id = D.DebitNote_Id
                      WHERE     cast(DebitNote_Date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                                AND Item_Tax = 0
                      UNION ALL
                      SELECT    ISNULL(SUM(CASE WHEN MRN_TYPE <> 2
                                                THEN CAST(( d.Item_Qty
                                                            * d.Item_Rate ) AS NUMERIC(18,
                                                              2))
                                                ELSE 0
                                           END), 0) * -1 AS IntraState_TaxableValue ,
                                ISNULL(SUM(CASE WHEN MRN_TYPE = 2
                                                THEN CAST(( d.Item_Qty
                                                            * d.Item_Rate ) AS NUMERIC(18,
                                                              2))
                                                ELSE 0
                                           END), 0) * -1 AS InterState_TaxableValue
                      FROM      dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER MRAPM
                                JOIN dbo.DebitNote_Master M ON M.MRNId = MRAPM.MRN_NO
                                JOIN dbo.DebitNote_Detail D ON M.DebitNote_Id = D.DebitNote_Id
                      WHERE     cast(DebitNote_Date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                                AND Item_Tax = 0
                    ) tb1
        ) TB"

        Return objCommFunction.Fill_DataSet(Qry).Tables(0).Rows(0)
    End Function

    Private Function Get4_SectionDataForPurchaseReturn() As DataRow

        Qry = " SELECT ISNULL(SUM(non_integrated_tax), 0) AS non_integrated_tax ,
                        ISNULL(SUM(integrated_tax), 0) AS integrated_tax, 
                        ISNULL(SUM(CessAmt), 0) AS CessAmt
                FROM   ( SELECT    CASE WHEN sm.STATE_ID =" & stateId & "
                         THEN ISNULL(Tax_num, 0)
                         ELSE 0
                    END AS non_integrated_tax ,
                    CASE WHEN sm.STATE_ID <> " & stateId & "
                         THEN ISNULL(Tax_num, 0)
                         ELSE 0
                    END AS integrated_tax,
                    ISNULL(Cess_num, 0) As CessAmt
          FROM      dbo.DebitNote_Master dnm
                    --INNER JOIN dbo.DebitNote_DETAIL dnd ON dnm.DebitNote_Id = dnd.DebitNote_Id
                    INNER JOIN dbo.ACCOUNT_MASTER am ON ACC_ID = dnm.DN_CustId
                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
          WHERE     cast(dnm.Creation_date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
          ) tb"


        Return objCommFunction.Fill_DataSet(Qry).Tables(0).Rows(0)
    End Function

    Private Function Get4_SectionDataForPurchase() As DataRow
        Qry = " SELECT SUM(ISNULL(non_integrated_tax,0)) AS non_integrated_tax, " &
            " SUM(ISNULL(integrated_tax,0)) As integrated_tax, SUM(ISNULL(CessAmt, 0)) AS CessAmt FROM (" &
            " SELECT  SUM(CASE WHEN mrn.MRN_TYPE <> 2 THEN mrn.GST_AMOUNT ELSE 0 END) AS non_integrated_tax," &
            "         SUM(CASE WHEN mrn.MRN_TYPE = 2 THEN mrn.GST_AMOUNT ELSE 0 END) AS integrated_tax, " &
            "         SUM(mrn.CESS_AMOUNT) AS CessAmt " &
            " FROM dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER  AS mrn " &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON mrn.Vendor_ID = am.ACC_ID" &
            " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID" &
            " WHERE MRN_STATUS <> 2 AND LEN(am.VAT_NO) > 0 and cast(Received_Date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
            " UNION ALL" &
            " SELECT  SUM(CASE WHEN mrn.MRN_TYPE <> 2 THEN mrn.GST_AMOUNT ELSE 0 END) AS non_integrated_tax," &
            "         SUM(CASE WHEN mrn.MRN_TYPE = 2 THEN mrn.GST_AMOUNT ELSE 0 END) AS integrated_tax, " &
            "         SUM(mrn.CESS_AMOUNT) AS CessAmt " &
            " FROM dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER AS mrn " &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON mrn.CUST_ID = am.ACC_ID" &
            " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID" &
            " WHERE MRN_STATUS <> 2 AND LEN(am.VAT_NO) > 0 and cast(Receipt_Date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
           
            UNION ALL
            SELECT 	CASE WHEN cm.STATE_ID = " & stateId & " THEN pt.GSTPerAmt END AS non_integrated_tax ,                    
                    CASE WHEN cm.STATE_ID <> " & stateId & " THEN pt.GSTPerAmt END AS integrated_tax ,
                    0.00 AS CessAmt
            FROM    dbo.PaymentTransaction AS pt
                    INNER JOIN dbo.ACCOUNT_MASTER am ON pt.BankId = am.ACC_ID
                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
            WHERE   CAST(pt.PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
					AND pt.GST_Applicable_Acc = 'Dr.' and pt.FK_GST_TYPE_ID = 2 and pt.StatusId <> 3 AND LEN(am.VAT_NO) > 0


) As temp"
        Return objCommFunction.Fill_DataSet(Qry).Tables(0).Rows(0)
    End Function

    Private Function Get3_2_SectionData() As System.Data.DataTable

        Qry = " Select  STATE_CODE ,
        STATE_NAME ,
        SUM(Taxable_Value) AS Taxable_Value ,
        SUM(integrated_tax) AS integrated_tax
FROM    ( SELECT    STATE_CODE ,
                    STATE_NAME ,
                    CAST(SUM(CASE WHEN GSTPaid = 'Y'
                                  THEN Taxable_Value - ( Taxable_Value
                                                         - ( Taxable_Value
                                                             / ( 1 + VAT_PER
                                                              / 100 ) ) )
                                  ELSE Taxable_Value
                             END) AS DECIMAL(18, 2)) AS Taxable_Value ,
                    SUM(integrated_tax) AS integrated_tax
          FROM      ( SELECT    STATE_CODE ,
                                STATE_NAME ,
                                SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                                      - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                                    THEN ( ( BAL_ITEM_QTY
                                                             * BAL_ITEM_RATE )
                                                           * DISCOUNT_VALUE )
                                                         / 100
                                                    ELSE DISCOUNT_VALUE
                                               END, 0) )) AS Taxable_Value ,
                                SUM(invd.VAT_AMOUNT) AS integrated_tax ,
                                GSTPaid ,
                                VAT_PER
                      FROM      dbo.SALE_INVOICE_MASTER inv
                                INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
                                INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
                                INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                                INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                      WHERE     INV.INVOICE_STATUS <> 4
                                 AND cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
                                " AND inv.INV_TYPE = 'I'
                                AND LEN(ISNULL(VAT_NO, '')) = 0
                      GROUP BY  STATE_CODE ,
                                STATE_NAME ,
                                GSTPaid ,
                                VAT_PER

                      UNION ALL

                      SELECT    STATE_CODE ,
                                STATE_NAME ,
                                Taxable_Value ,
                                CASE WHEN Inv_Type = 'I' THEN GSTPerAmt
                                END AS integrated_tax ,
                                GSTPaid ,
                                VAT_PER
                      FROM      ( SELECT    STATE_CODE ,
                                            STATE_NAME ,
                                            pt.GSTPerAmt ,
                                            pt.TotalAmountReceived AS Taxable_Value ,
                                            '' AS GSTPaid ,
                                            CASE WHEN pt.fk_GST_ID = 1
                                                 THEN 0.00
                                                 WHEN pt.fk_GST_ID = 2
                                                 THEN 5.00
                                                 WHEN pt.fk_GST_ID = 3
                                                 THEN 12.00
                                                 WHEN pt.fk_GST_ID = 4 
                                                 THEN 18.00
                                                 WHEN pt.fk_GST_ID = 5
                                                 THEN 28.00
                                                 WHEN pt.fk_GST_ID = 6
                                                 THEN 3.00
                                            END AS VAT_PER ,
                                            CASE WHEN cm.STATE_ID <> " & stateId & " THEN 'I'
                                                 WHEN cm.STATE_ID =  " & stateId & " THEN 'S'          
                                            END AS Inv_Type
                                  FROM      dbo.PaymentTransaction AS pt
                                            INNER JOIN dbo.ACCOUNT_MASTER am ON pt.AccountId = am.ACC_ID
                                            INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                                            INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                                  WHERE     CAST(pt.PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                                            And pt.GST_Applicable_Acc = 'Cr.' and pt.FK_GST_TYPE_ID = 2 and pt.StatusId <> 3
                                            And Len(ISNULL(VAT_NO, '')) = 0
                      ) subquery
                      WHERE     Inv_Type = 'I'


                    ) tb
          GROUP BY  STATE_CODE ,
                    STATE_NAME
          UNION ALL
          SELECT    STATE_CODE ,
                    STATE_NAME ,
                    SUM(CAST(( d.Item_Qty * d.Item_Rate ) AS NUMERIC(18, 2)))
                    * -1 AS Taxable_Value ,
                    ISNULL(SUM(CASE WHEN INV_TYPE = 'I'
                                    THEN CAST(( d.Item_Qty * d.Item_Rate )
                                         * Item_Tax / 100 AS NUMERIC(18, 2))
                                    ELSE 0
                               END), 0) * -1 AS integrated_tax
          FROM      dbo.SALE_INVOICE_MASTER AS inv
                    JOIN dbo.CreditNote_Master M ON M.INVId = inv.SI_ID
                    JOIN dbo.CreditNote_DETAIL D ON M.CreditNote_Id = D.CreditNote_Id
                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
          WHERE     cast(CreditNote_Date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
                    " AND INV_TYPE = 'I'
                     AND LEN(ISNULL(VAT_NO, '')) = 0
          GROUP BY  STATE_CODE ,
                    STATE_NAME
        ) tb
        GROUP BY STATE_CODE ,
        STATE_NAME "

        Return objCommFunction.Fill_DataSet(Qry).Tables(0)
    End Function

    Private Function Get3_1_SectionData(condition As String) As DataRow

        Qry = "SELECT SUM(ISNULL(Taxable_Value, 0)) AS Taxable_Value ,
        SUM(ISNULL(Cess_Amount, 0)) AS Cess_Amount ,
        SUM(ISNULL(non_integrated_tax, 0)) AS non_integrated_tax ,
        SUM(ISNULL(integrated_tax, 0)) AS integrated_tax
        FROM   ( SELECT CAST(SUM(CASE WHEN GSTPaid = 'Y'
                      THEN Taxable_Value - ( Taxable_Value - ( Taxable_Value
                                                              / ( 1 + VAT_PER
                                                              / 100 ) ) )
                      ELSE Taxable_Value
                 END) AS DECIMAL(18, 2)) AS Taxable_Value ,
        ISNULL(SUM(Cess_Amount), 0) AS Cess_Amount ,
        ISNULL(SUM(non_integrated_tax), 0) AS non_integrated_tax ,
        ISNULL(SUM(integrated_tax), 0) AS integrated_tax
        FROM   ( SELECT    ISNULL(SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                                 - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                               THEN ( ( BAL_ITEM_QTY
                                                        * BAL_ITEM_RATE )
                                                      * DISCOUNT_VALUE ) / 100
                                               ELSE DISCOUNT_VALUE
                                          END, 0) )), 0) AS Taxable_Value ,
                    SUM(invd.CessAmount_num) + SUM(invd.ACessAmount) As Cess_Amount ,
                    ISNULL(SUM(CASE WHEN inv.INV_TYPE <> 'I'
                                    THEN invd.VAT_AMOUNT
                                    ELSE 0
                               END), 0) AS non_integrated_tax ,
                    ISNULL(SUM(CASE WHEN inv.INV_TYPE = 'I'
                                    THEN invd.VAT_AMOUNT
                                    ELSE 0
                               END), 0) AS integrated_tax ,
                    GSTPaid ,
                    VAT_PER
          FROM      dbo.SALE_INVOICE_MASTER inv
                    INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
          WHERE     INV.INVOICE_STATUS <> 4 
                    AND cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) 
                    AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) "

        Qry = Qry & condition & " GROUP BY GSTPaid, VAT_PER) tb "

        Qry = Qry & "
        UNION ALL 
              Select Taxable_Value ,
                        CessAmt ,
                        non_integrated_tax ,
                        integrated_tax 
              from(
              SELECT * From (
              SELECT    pt.TotalAmountReceived AS Taxable_Value , 
						0.00 AS CessAmt ,
						CASE WHEN cm.STATE_ID = " & stateId & " THEN pt.GSTPerAmt
                        END AS non_integrated_tax ,
                        CASE WHEN cm.STATE_ID <> " & stateId & " THEN pt.GSTPerAmt
                        END AS integrated_tax,
                         CASE WHEN pt.fk_GST_ID = 1 THEN 0.00
                                         WHEN pt.fk_GST_ID = 2 THEN 5.00
                                         WHEN pt.fk_GST_ID = 3 THEN 12.00
                                         WHEN pt.fk_GST_ID = 4 THEN 18.00
                                         WHEN pt.fk_GST_ID = 5 THEN 28.00
                                         WHEN pt.fk_GST_ID = 6 THEN 3.00
                                    END AS Vat_per ,
                        pt.PaymentDate ,
                        pt.GST_Applicable_Acc ,
                        pt.FK_GST_TYPE_ID ,
                        pt.StatusId ,
                        am.VAT_NO 
              FROM      dbo.PaymentTransaction AS pt
                        INNER JOIN dbo.ACCOUNT_MASTER am ON pt.AccountId = am.ACC_ID
                        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID ) subquery
              WHERE     CAST(PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                        AND GST_Applicable_Acc = 'Cr.' and FK_GST_TYPE_ID = 2 and StatusId <> 3 " & condition & " 
               ) tb " & "

        UNION ALL
        SELECT    SUM(CAST(( d.Item_Qty * d.Item_Rate ) AS NUMERIC(18, 2)))
                    * (-1) AS Taxable_Value ,
                    ISNULL(SUM(CAST(( d.Item_Qty * d.Item_Rate )
                                         * Item_Cess / 100 AS NUMERIC(18, 2))), 0) * (-1) As Cess_Amount ,
                    ISNULL(SUM(CASE WHEN INV_TYPE <> 'I'
                                    THEN CAST(( d.Item_Qty * d.Item_Rate )
                                         * Item_Tax / 100 AS NUMERIC(18, 2))
                                    ELSE 0
                               END), 0) * (-1) AS non_integrated_tax ,
                    ISNULL(SUM(CASE WHEN INV_TYPE = 'I'
                                    THEN CAST(( d.Item_Qty * d.Item_Rate )
                                         * Item_Tax / 100 AS NUMERIC(18, 2))
                                    ELSE 0
                               END), 0) * (-1) AS integrated_tax
          FROM      dbo.SALE_INVOICE_MASTER
                    JOIN dbo.CreditNote_Master M ON M.INVId = dbo.SALE_INVOICE_MASTER.SI_ID
                    JOIN dbo.CreditNote_DETAIL D ON M.CreditNote_Id = D.CreditNote_Id
          WHERE     cast(CreditNote_Date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) "


        If condition = " AND(VAT_PER)>0" Then
            Qry = Qry & " AND ITEM_TAX > 0 "

        ElseIf condition = " AND(VAT_PER)=0" Then
            Qry = Qry & " AND ITEM_TAX = 0"
        End If

        Qry = Qry & " ) tb"

        Return objCommFunction.Fill_DataSet(Qry).Tables(0).Rows(0)
    End Function

    Private Function GetBasicData() As DataRow
        Qry = "SELECT DIVISION_NAME, TIN_NO  FROM dbo.DIVISION_SETTINGS"
        Return objCommFunction.Fill_DataSet(Qry).Tables(0).Rows(0)
    End Function

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
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

End Class
