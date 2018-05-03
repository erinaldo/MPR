Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
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

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        xlApp = New Excel.ApplicationClass
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

    Private Sub WriteData(xlWorkBook As Excel.Workbook)
        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("GSTR3")

        xlWorkSheet = AddBasicData(xlWorkSheet)
        xlWorkSheet = Add3_1SectionData(xlWorkSheet)
        xlWorkSheet = Add3_2SectionData(xlWorkSheet)
        xlWorkSheet = Add4SectionData(xlWorkSheet)
        xlWorkSheet = Add5SectionData(xlWorkSheet)
    End Sub

    Private Function AddBasicData(xlWorkSheet As Excel.Worksheet) As Excel.Worksheet
        row = GetBasicData()
        xlWorkSheet.Cells(4, 15) = txtFromDate.Value.ToString("yyyy")
        xlWorkSheet.Cells(5, 15) = txtFromDate.Value.ToString("MMMM")
        Dim colIndex As Int16 = 2
        For Each ch As Char In row("TIN_NO").ToString
            xlWorkSheet.Cells(6, colIndex) = ch.ToString
            colIndex += 1
        Next
        xlWorkSheet.Cells(7, 2) = row("DIVISION_NAME")
        Return xlWorkSheet
    End Function

    Private Function Add3_1SectionData(xlWorkSheet As Excel.Worksheet) As Excel.Worksheet
        row = Get3_1_SectionData("  AND (invd.VAT_PER)>0")
        xlWorkSheet.Cells(11, 2) = row("Taxable_Value")
        xlWorkSheet.Cells(11, 5) = row("integrated_tax")
        xlWorkSheet.Cells(11, 8) = row("non_integrated_tax") / 2
        xlWorkSheet.Cells(11, 11) = row("non_integrated_tax") / 2
        xlWorkSheet.Cells(11, 14) = row("Cess_Amount")

        row = Get3_1_SectionData("  AND (invd.VAT_PER)= 0")
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
    Private Function Add3_2SectionData(xlWorkSheet As Excel.Worksheet) As Excel.Worksheet
        Dim table As System.Data.DataTable = Get3_2_SectionData()
        Dim rowIndex As Int16 = 21
        If table.Rows.Count > 0 Then
            noOfRowsInserted = table.Rows.Count
            For Each row As DataRow In table.Rows
                'CType(xlWorkSheet.Rows(20), Range).Select()
                CType(xlWorkSheet.Rows(rowIndex), Range).Insert(XlInsertShiftDirection.xlShiftDown, True)
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
    Private Function Add4SectionData(xlWorkSheet As Excel.Worksheet) As Excel.Worksheet
        rowIndex = 34 + noOfRowsInserted
        row = Get4_SectionDataForPurchase()
        Dim drrow As DataRow = Get4_SectionDataForPurchaseReturn()

        xlWorkSheet.Cells(rowIndex, 2) = row("integrated_tax") - drrow("integrated_tax")
        xlWorkSheet.Cells(rowIndex, 6) = (row("non_integrated_tax") / 2) - (drrow("non_integrated_tax") / 2)
        xlWorkSheet.Cells(rowIndex, 10) = (row("non_integrated_tax") / 2) - (drrow("non_integrated_tax") / 2)
        xlWorkSheet.Cells(rowIndex, 14) = 0

        rowIndex += 3
        'row = Get4_SectionDataForPurchaseReturn()
        'xlWorkSheet.Cells(rowIndex, 2) = row("integrated_tax")
        'xlWorkSheet.Cells(rowIndex, 6) = row("non_integrated_tax") / 2
        'xlWorkSheet.Cells(rowIndex, 10) = row("non_integrated_tax") / 2
        'xlWorkSheet.Cells(rowIndex, 14) = 0

        Return xlWorkSheet
    End Function

    Private Function Add5SectionData(xlWorkSheet As Worksheet) As Worksheet
        rowIndex = 45 + noOfRowsInserted
        row = Get5SectionData()
        xlWorkSheet.Cells(rowIndex, 5) = row("InterState_TaxableValue")
        xlWorkSheet.Cells(rowIndex, 11) = row("IntraState_TaxableValue")
        Return xlWorkSheet
    End Function

    Private Function Get5SectionData() As DataRow

        Qry = "SELECT  SUM(ISNULL(IntraState_TaxableValue, 0)) AS IntraState_TaxableValue ,
        SUM(ISNULL(InterState_TaxableValue, 0)) AS InterState_TaxableValue
FROM    ( SELECT    SUM(CASE WHEN MRWPM.MRN_TYPE <> 2
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
                    SUM(CASE WHEN MRWPM.MRN_TYPE = 2
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
          FROM      dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER MRWPM
                    JOIN dbo.MATERIAL_RECEIVED_WITHOUT_PO_DETAIL MRWPD ON MRWPD.Received_ID = MRWPM.Received_ID
                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = MRWPM.Vendor_ID
                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
          WHERE     MRWPD.Item_vat = 0
                    AND MONTH(RECEIVED_DATE) = " & txtFromDate.Value.Month &
                   " And Year(RECEIVED_DATE) = " & txtFromDate.Value.Year &
          "UNION ALL
          SELECT    SUM(CASE WHEN MRAPM.MRN_TYPE <> 2
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
                    JOIN dbo.MATERIAL_RECEIVED_AGAINST_PO_DETAIL MRAPD ON MRAPD.Receipt_ID = MRAPM.Receipt_ID
                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = MRAPM.CUST_ID
                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
          WHERE     MRAPD.Vat_Per = 0
                    AND MONTH(Receipt_DATE) = " & txtFromDate.Value.Month &
                   " AND YEAR(Receipt_DATE) = " & txtFromDate.Value.Year &
          "UNION ALL
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
                                JOIN dbo.DebitNote_DETAIL D ON M.DebitNote_Id = D.DebitNote_Id
                      WHERE     MONTH(DebitNote_Date) = " & txtFromDate.Value.Month &
                               " AND YEAR(DebitNote_Date) = " & txtFromDate.Value.Year &
                                "AND Item_Tax = 0
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
                                JOIN dbo.DebitNote_DETAIL D ON M.DebitNote_Id = D.DebitNote_Id
                      WHERE     MONTH(DebitNote_Date) = " & txtFromDate.Value.Month &
                                "AND YEAR(DebitNote_Date) = " & txtFromDate.Value.Year &
                                "AND Item_Tax = 0
                    ) tb
        ) TB"



        Return objCommFunction.Fill_DataSet(Qry).Tables(0).Rows(0)
    End Function

    Private Function Get4_SectionDataForPurchaseReturn() As DataRow


        'Qry = " SELECT SUM(ISNULL(non_integrated_tax,0)) AS non_integrated_tax, " &
        '    " SUM(ISNULL(integrated_tax,0)) As integrated_tax FROM (" &
        '    " SELECT  SUM(CASE WHEN cm.STATE_ID = " & stateId & " THEN Item_vat ELSE 0 END) AS non_integrated_tax," &
        '    "         SUM(CASE WHEN cm.STATE_ID != " & stateId & " THEN Item_vat ELSE 0 END) AS integrated_tax " &
        '    " From dbo.ReverseMATERIAL_RECIEVED_Against_PO_MASTER ret" &
        '    " inner Join dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER mrn ON ret.received_ID = mrn.Receipt_ID" &
        '    " INNER Join dbo.ACCOUNT_MASTER am ON mrn.CUST_ID = am.ACC_ID" &
        '    " INNER Join dbo.CITY_MASTER cm ON am.CITY_ID = cm.CITY_ID" &
        '    " INNER Join dbo.ReverseMATERIAL_RECEIVED_Against_PO_DETAIL retd ON ret.Reverse_ID = retd.Reverse_ID" &
        '    " WHERE MRN_STATUS <> 2 and MONTH(Reverse_Date) =  " & txtFromDate.Value.Month &
        '    " And YEAR(Reverse_Date)=  " & txtFromDate.Value.Year &
        '    " UNION" &
        '    " SELECT  SUM(CASE WHEN cm.STATE_ID = " & stateId & " THEN Item_vat ELSE 0 END) AS non_integrated_tax," &
        '    "         SUM(CASE WHEN cm.STATE_ID != " & stateId & " THEN Item_vat ELSE 0 END) AS integrated_tax " &
        '    " From dbo.ReverseMATERIAL_RECIEVED_WITHOUT_PO_MASTER ret" &
        '    " inner Join dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER mrn ON ret.received_ID = mrn.Received_ID" &
        '    " INNER Join dbo.ACCOUNT_MASTER am ON mrn.Vendor_ID = am.ACC_ID" &
        '    " INNER Join dbo.CITY_MASTER cm ON am.CITY_ID = cm.CITY_ID" &
        '    " INNER Join dbo.ReverseMATERIAL_RECEIVED_WITHOUT_PO_DETAIL retd ON ret.Reverse_ID = retd.Reverse_ID" &
        '    " WHERE MRN_STATUS <> 2 and MONTH(Reverse_Date) =  " & txtFromDate.Value.Month &
        '    " And YEAR(Reverse_Date)=  " & txtFromDate.Value.Year &
        '    " ) AS temp"



        Qry = " SELECT ISNULL(SUM(non_integrated_tax), 0) AS non_integrated_tax ,
       ISNULL(SUM(integrated_tax), 0) AS integrated_tax
 FROM   ( SELECT    CASE WHEN sm.STATE_ID =" & stateId & "
                         THEN ISNULL(Item_Tax, 0)
                         ELSE 0
                    END AS non_integrated_tax ,
                    CASE WHEN sm.STATE_ID <> " & stateId & "
                         THEN ISNULL(Item_Tax, 0)
                         ELSE 0
                    END AS integrated_tax
          FROM      dbo.DebitNote_Master
                    JOIN dbo.DebitNote_DETAIL ON dbo.DebitNote_Master.DebitNote_Id = dbo.DebitNote_DETAIL.DebitNote_Id
                    JOIN dbo.ACCOUNT_MASTER ON ACC_ID = DN_CustId
                    JOIN dbo.CITY_MASTER ON dbo.CITY_MASTER.CITY_ID = dbo.ACCOUNT_MASTER.CITY_ID
                    JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = dbo.CITY_MASTER.STATE_ID
          WHERE     MONTH(DebitNote_Master.Creation_Date) =" & txtFromDate.Value.Month & "
                    AND YEAR(DebitNote_Master.Creation_Date) = " & txtFromDate.Value.Year & "
          AND item_tax>0) tb"


        Return objCommFunction.Fill_DataSet(Qry).Tables(0).Rows(0)
    End Function

    Private Function Get4_SectionDataForPurchase() As DataRow
        Qry = " SELECT SUM(ISNULL(non_integrated_tax,0)) AS non_integrated_tax, " &
            " SUM(ISNULL(integrated_tax,0)) As integrated_tax FROM (" &
            " SELECT  SUM(CASE WHEN mrn.MRN_TYPE <> 2 THEN mrn.GST_AMOUNT ELSE 0 END) AS non_integrated_tax," &
            "         SUM(CASE WHEN mrn.MRN_TYPE = 2 THEN mrn.GST_AMOUNT ELSE 0 END) AS integrated_tax " &
            " FROM dbo.MATERIAL_RECIEVED_WITHOUT_PO_MASTER  AS mrn " &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON mrn.Vendor_ID = am.ACC_ID" &
            " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID" &
            " WHERE MRN_STATUS <> 2 and MONTH(Received_Date) =  " & txtFromDate.Value.Month &
            " And YEAR(Received_Date)=  " & txtFromDate.Value.Year &
            " union" &
            " SELECT  SUM(CASE WHEN mrn.MRN_TYPE <> 2 THEN mrn.GST_AMOUNT ELSE 0 END) AS non_integrated_tax," &
            "         SUM(CASE WHEN mrn.MRN_TYPE = 2 THEN mrn.GST_AMOUNT ELSE 0 END) AS integrated_tax " &
            " FROM dbo.MATERIAL_RECEIVED_AGAINST_PO_MASTER AS mrn " &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON mrn.CUST_ID = am.ACC_ID" &
            " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID" &
            " WHERE MRN_STATUS <> 2 and MONTH(Receipt_Date) =  " & txtFromDate.Value.Month &
            " And YEAR(Receipt_Date)=  " & txtFromDate.Value.Year &
            " ) AS temp"
        Return objCommFunction.Fill_DataSet(Qry).Tables(0).Rows(0)
    End Function

    Private Function Get3_2_SectionData() As System.Data.DataTable

        Qry = " SELECT  STATE_CODE ,
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
                                AND MONTH(SI_DATE) = " & txtFromDate.Value.Month &
                                "AND YEAR(SI_DATE) = " & txtFromDate.Value.Year &
                                "AND inv.INV_TYPE = 'I'
                                AND LEN(ISNULL(VAT_NO, '')) = 0
                      GROUP BY  STATE_CODE ,
                                STATE_NAME ,
                                GSTPaid ,
                                VAT_PER
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
          WHERE     MONTH(CreditNote_Date) =" & txtFromDate.Value.Month &
                    "AND YEAR(CreditNote_Date) =" & txtFromDate.Value.Year &
                    "AND INV_TYPE = 'I'
                    AND LEN(ISNULL(VAT_NO, '')) = 0
          GROUP BY  STATE_CODE ,
                    STATE_NAME
        ) tb
GROUP BY STATE_CODE ,
        STATE_NAME "

        'Qry = " SELECT STATE_CODE, STATE_NAME, " &
        '     " SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) As Taxable_Value, " &
        '     " SUM(invd.VAT_AMOUNT) as integrated_tax " &
        '     " FROM dbo.SALE_INVOICE_MASTER inv " &
        '     " INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID" &
        '     " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID " &
        '     " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID " &
        '     " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID " &
        '     " WHERE MONTH(SI_DATE) =  " & txtFromDate.Value.Month &
        '     " And YEAR(SI_DATE) =  " & txtFromDate.Value.Year &
        '     " AND inv.INV_TYPE='I' " &
        '     " AND len(isnull(VAT_NO,''))=0 Group By STATE_CODE, STATE_NAME"
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
                    SUM(0) Cess_Amount ,
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
          WHERE     INV.INVOICE_STATUS <> 4 AND MONTH(SI_DATE) = " & txtFromDate.Value.Month &
                    " AND YEAR(SI_DATE) =" & txtFromDate.Value.Year

        ''Qry = " SELECT " &
        ''     " isnull(SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))),0) As Taxable_Value, SUM(0) Cess_Amount, " &
        ''     " isnull(SUM(CASE WHEN inv.INV_TYPE <>'I' THEN invd.VAT_AMOUNT ELSE 0 END),0) AS non_integrated_tax, " &
        ''     " isnull(SUM(CASE WHEN inv.INV_TYPE = 'I' THEN invd.VAT_AMOUNT ELSE 0 END),0) AS integrated_tax " &
        ''     " FROM    dbo.SALE_INVOICE_MASTER inv " &
        ''     " INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID" &
        ''     " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID " &
        ''     " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID " &
        ''     " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID " &
        ''     " WHERE MONTH(SI_DATE) =  " & txtFromDate.Value.Month &
        ''     " And YEAR(SI_DATE)=  " & txtFromDate.Value.Year
        Qry = Qry + condition + " GROUP BY  GSTPaid ,VAT_PER) tb UNION ALL
          SELECT    SUM(CAST(( d.Item_Qty * d.Item_Rate ) AS NUMERIC(18, 2)))
                    * -1 AS Taxable_Value ,
                    ISNULL(SUM(0), 0) Cess_Amount ,
                    ISNULL(SUM(CASE WHEN INV_TYPE <> 'I'
                                    THEN CAST(( d.Item_Qty * d.Item_Rate )
                                         * Item_Tax / 100 AS NUMERIC(18, 2))
                                    ELSE 0
                               END), 0) * -1 AS non_integrated_tax ,
                    ISNULL(SUM(CASE WHEN INV_TYPE = 'I'
                                    THEN CAST(( d.Item_Qty * d.Item_Rate )
                                         * Item_Tax / 100 AS NUMERIC(18, 2))
                                    ELSE 0
                               END), 0) * -1 AS integrated_tax
          FROM      dbo.SALE_INVOICE_MASTER
                    JOIN dbo.CreditNote_Master M ON M.INVId = dbo.SALE_INVOICE_MASTER.SI_ID
                    JOIN dbo.CreditNote_DETAIL D ON M.CreditNote_Id = D.CreditNote_Id
          WHERE     MONTH(CreditNote_Date) =" & txtFromDate.Value.Month &
                    "AND YEAR(CreditNote_Date) = " & txtFromDate.Value.Year
        If condition = "  AND (invd.VAT_PER)>0" Then
            Qry = Qry + " AND ITEM_TAX > 0 "

        ElseIf condition = "  AND (invd.VAT_PER)= 0" Then
            Qry = Qry + " AND ITEM_TAX = 0"

        End If

        Qry = Qry + " ) tb"

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
