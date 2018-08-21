Imports Microsoft.Office.Interop
Imports MMSPlus

Public Class frm_GSTR_1
    Implements IForm
    Dim objCommFunction As New CommonClass
    Private stateId As String
    Private IsInUT As String
    Dim Qry As String
    Dim _rights As Form_Rights

    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub

    Private Sub frm_GSTR_1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        stateId = objCommFunction.ExecuteScalar("SELECT STATE_ID FROM dbo.CITY_MASTER WHERE CITY_ID IN(SELECT TOP 1 CITY_ID FROM dbo.DIVISION_SETTINGS)")
        'IsInUT = objCommFunction.ExecuteScalar("SELECT isUT_bit FROM dbo.STATE_MASTER WHERE STATE_ID IN (SELECT STATE_ID FROM dbo.CITY_MASTER WHERE CITY_ID IN ( SELECT CITY_ID FROM dbo.DIVISION_SETTINGS)) ")
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        BindData()
        ImportData()

    End Sub

    Dim b2bTable As DataTable
    Dim b2clTable As DataTable
    Dim b2csTable As DataTable
    Dim hsnTable As DataTable
    Dim cdnrTable As DataTable
    Dim cdnurTable As DataTable
    Dim exempTable As DataTable
    Dim docsTable As DataTable

    Private Sub ImportData()
        Dim path As String = Application.StartupPath + "\ExcelTemplate\GSTR1_Template.xlsx"

        Dim sfd As New SaveFileDialog
        sfd.CheckFileExists = False
        If sfd.ShowDialog <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        'Dim xlApp As Excel.Application
        'Dim xlWorkBook As Excel.Workbook

        Dim xlApp As Object 'Excel.Application
        Dim xlWorkBook As Object 'Excel.Workbook

        xlApp = CreateObject("Excel.Application")

        ' xlApp = New Excel.ApplicationClass


        xlWorkBook = xlApp.Workbooks.Open(path)

        Try
            WriteB2BData(xlWorkBook)
            WriteB2CLData(xlWorkBook)
            WriteB2CSData(xlWorkBook)
            WriteHsnData(xlWorkBook)
            WriteCDNRData(xlWorkBook)
            WriteCDNURData(xlWorkBook)
            WriteEXEMPData(xlWorkBook)
            WriteDocsData(xlWorkBook)
            xlWorkBook.SaveAs(sfd.FileName)
        Finally
            xlWorkBook.Close(SaveChanges:=False)
            xlApp.Quit()
            releaseObject(xlApp)
            releaseObject(xlWorkBook)

            MsgBox("GSTR 1 exported sucessfully", MsgBoxStyle.Information, gblMessageHeading)
        End Try
    End Sub

    Private Sub WriteB2BData(xlWorkBook As Object)
        If b2bTable.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim rowIndex As Int32 = 5
        ' Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("b2b")

        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("b2b")

        For Each row As DataRow In b2bTable.Rows
            Dim state As String = row("STATE_CODE") + "-" + row("STATE_NAME")
            Dim invoiceNo As String = row("SI_CODE") + row("SI_NO").ToString()
            xlWorkSheet.Cells(rowIndex, 1) = row("VAT_NO")
            xlWorkSheet.Cells(rowIndex, 2) = invoiceNo
            xlWorkSheet.Cells(rowIndex, 3) = Convert.ToDateTime(row("SI_DATE")).ToString("dd-MMM-yy")
            xlWorkSheet.Cells(rowIndex, 4) = row("NET_AMOUNT")
            xlWorkSheet.Cells(rowIndex, 5) = state
            xlWorkSheet.Cells(rowIndex, 6) = "N"
            xlWorkSheet.Cells(rowIndex, 7) = "Regular"
            xlWorkSheet.Cells(rowIndex, 8) = ""
            xlWorkSheet.Cells(rowIndex, 9) = row("VAT_PER")
            xlWorkSheet.Cells(rowIndex, 10) = row("Taxable_Value")
            xlWorkSheet.Cells(rowIndex, 11) = row("Cess_Amount")
            rowIndex += 1
        Next

        ''set global values
        Dim noOfRecipients As Int32 = b2bTable.DefaultView.ToTable(True, "VAT_NO").Rows.Count
        Dim noOfInvoices As Int32 = b2bTable.DefaultView.ToTable(True, "SI_CODE", "SI_NO").Rows.Count
        Dim sumOfInvoices As Decimal = b2bTable.DefaultView.ToTable(True, "SI_CODE", "SI_NO", "NET_AMOUNT").Compute("sum(NET_AMOUNT)", Nothing)

        xlWorkSheet.Cells(3, 1) = noOfRecipients
        xlWorkSheet.Cells(3, 2) = noOfInvoices
        xlWorkSheet.Cells(3, 4) = sumOfInvoices
    End Sub

    Private m_colIndex As Integer
    Public Property colIndex As Integer
        Get
            m_colIndex += 1
            Return m_colIndex
        End Get
        Set(value As Integer)
            m_colIndex = value
        End Set
    End Property

    Private Sub WriteB2CLData(xlWorkBook As Object)
        If b2clTable.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim rowIndex As Int32 = 5

        ' Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("b2cl")
        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("b2cl")

        For Each row As DataRow In b2clTable.Rows
            Dim invoiceNo As String = row("SI_CODE") + row("SI_NO").ToString()
            Dim state As String = row("STATE_CODE") + "-" + row("STATE_NAME")
            xlWorkSheet.Cells(rowIndex, 1) = invoiceNo
            xlWorkSheet.Cells(rowIndex, 2) = Convert.ToDateTime(row("SI_DATE")).ToString("dd-MMM-yy")
            xlWorkSheet.Cells(rowIndex, 3) = row("NET_AMOUNT")
            xlWorkSheet.Cells(rowIndex, 4) = state
            xlWorkSheet.Cells(rowIndex, 5) = row("VAT_PER")
            xlWorkSheet.Cells(rowIndex, 6) = row("Taxable_Value")
            xlWorkSheet.Cells(rowIndex, 7) = row("Cess_Amount")
            xlWorkSheet.Cells(rowIndex, 8) = ""
            rowIndex += 1
        Next

        ''set global values
        Dim noOfInvoices As Int32 = b2clTable.DefaultView.ToTable(True, "SI_CODE", "SI_NO").Rows.Count
        Dim sumOfInvoices As Decimal = b2clTable.DefaultView.ToTable(True, "SI_CODE", "SI_NO", "NET_AMOUNT").Compute("sum(NET_AMOUNT)", Nothing)

        xlWorkSheet.Cells(3, 1) = noOfInvoices
        xlWorkSheet.Cells(3, 3) = sumOfInvoices
    End Sub

    Private Sub WriteB2CSData(xlWorkBook As Object)
        Dim rowIndex As Int32 = 5
        'Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("b2cs")
        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("b2cs")

        For Each row As DataRow In b2csTable.Rows
            Dim state As String = row("STATE_CODE") + "-" + row("STATE_NAME")
            xlWorkSheet.Cells(rowIndex, 1) = "OE"
            xlWorkSheet.Cells(rowIndex, 2) = state
            xlWorkSheet.Cells(rowIndex, 3) = row("VAT_PER")
            xlWorkSheet.Cells(rowIndex, 4) = row("Taxable_Value")
            xlWorkSheet.Cells(rowIndex, 5) = row("Cess_Amount")
            xlWorkSheet.Cells(rowIndex, 6) = ""
            rowIndex += 1
        Next
    End Sub

    Private Sub WriteHsnData(xlWorkBook As Object)
        If hsnTable.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim rowIndex As Int32 = 5
        '  Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("hsn")

        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("hsn")

        For Each row As DataRow In hsnTable.Rows
            xlWorkSheet.Cells(rowIndex, 1) = row("HsnCode_vch")
            'xlWorkSheet.Cells(rowIndex, 2) = row("ITEM_NAME")
            'xlWorkSheet.Cells(rowIndex, 3) = row("UM_Name") + "-" + row("UM_DESC")
            xlWorkSheet.Cells(rowIndex, 4) = row("Qty")
            xlWorkSheet.Cells(rowIndex, 5) = Convert.ToDecimal(row("Taxable_Value")) + Convert.ToDecimal(row("Cess_Amount")) + Convert.ToDecimal(row("non_integrated_tax")) + Convert.ToDecimal(row("integrated_tax"))
            xlWorkSheet.Cells(rowIndex, 6) = row("Taxable_Value")
            xlWorkSheet.Cells(rowIndex, 7) = row("integrated_tax")
            xlWorkSheet.Cells(rowIndex, 8) = row("non_integrated_tax") / 2
            xlWorkSheet.Cells(rowIndex, 9) = row("non_integrated_tax") / 2
            xlWorkSheet.Cells(rowIndex, 10) = row("Cess_Amount")
            rowIndex += 1
        Next

        ''set global values 
        Dim noOfHSNC As Int32 = hsnTable.DefaultView.ToTable(True, "HsnCode_vch").Rows.Count

        xlWorkSheet.Cells(3, 1) = noOfHSNC
    End Sub

    Private Sub WriteCDNRData(xlWorkBook As Object)
        If cdnrTable.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim rowIndex As Int32 = 5
        'Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("cdnr")

        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("cdnr")

        For Each row As DataRow In cdnrTable.Rows
            colIndex = 0
            xlWorkSheet.Cells(rowIndex, colIndex) = row("VAT_NO")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("ACC_NAME")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("SI_CODE") + row("SI_NO").ToString()
            xlWorkSheet.Cells(rowIndex, colIndex) = Convert.ToDateTime(row("SI_DATE")).ToString("dd-MMM-yy")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("CreditNote_Code") + row("CreditNote_No").ToString()
            xlWorkSheet.Cells(rowIndex, colIndex) = Convert.ToDateTime(row("CreditNote_Date")).ToString("dd-MMM-yy")
            xlWorkSheet.Cells(rowIndex, colIndex) = "C"
            xlWorkSheet.Cells(rowIndex, colIndex) = "01-Sales Return"
            xlWorkSheet.Cells(rowIndex, colIndex) = row("STATE_CODE") + "-" + row("STATE_NAME")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("CN_Amount")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("Item_Tax")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("Taxable_Value")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("Cess_Amount")
            xlWorkSheet.Cells(rowIndex, colIndex) = "N"
            rowIndex += 1
        Next

        ''set global values
        Dim noOfRecipients As Int32 = cdnrTable.DefaultView.ToTable(True, "VAT_NO").Rows.Count
        Dim noOfInvoices As Int32 = cdnrTable.DefaultView.ToTable(True, "SI_CODE", "SI_NO").Rows.Count
        Dim noOfVouchers As Int32 = cdnrTable.DefaultView.ToTable(True, "CreditNote_Code", "CreditNote_No").Rows.Count
        Dim sumOfVouchers As Decimal = cdnrTable.DefaultView.ToTable(True, "CreditNote_Code", "CreditNote_No", "CN_Amount").Compute("sum(CN_Amount)", Nothing)

        xlWorkSheet.Cells(3, 1) = noOfRecipients
        xlWorkSheet.Cells(3, 3) = noOfInvoices
        xlWorkSheet.Cells(3, 5) = noOfVouchers
        xlWorkSheet.Cells(3, 10) = sumOfVouchers
    End Sub

    Private Sub WriteCDNURData(xlWorkBook As Object)
        If cdnurTable.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim rowIndex As Int32 = 5
        ' Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("cdnur")

        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("cdnur")

        For Each row As DataRow In cdnurTable.Rows
            colIndex = 0
            xlWorkSheet.Cells(rowIndex, colIndex) = "B2CL"
            'xlWorkSheet.Cells(rowIndex, colIndex) = row("ACC_NAME")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("CreditNote_Code") + row("CreditNote_No").ToString()
            xlWorkSheet.Cells(rowIndex, colIndex) = Convert.ToDateTime(row("CreditNote_Date")).ToString("dd-MMM-yy")
            xlWorkSheet.Cells(rowIndex, colIndex) = "C"
            xlWorkSheet.Cells(rowIndex, colIndex) = row("SI_CODE") + row("SI_NO").ToString()
            xlWorkSheet.Cells(rowIndex, colIndex) = Convert.ToDateTime(row("SI_DATE")).ToString("dd-MMM-yy")
            xlWorkSheet.Cells(rowIndex, colIndex) = "01-Sales Return"
            xlWorkSheet.Cells(rowIndex, colIndex) = row("STATE_CODE") + "-" + row("STATE_NAME")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("CN_Amount")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("Item_Tax")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("Taxable_Value")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("Cess_Amount")
            xlWorkSheet.Cells(rowIndex, colIndex) = "N"
            rowIndex += 1
        Next

        ''set global values
        Dim noOfRecipients As Int32 = cdnurTable.DefaultView.ToTable(True, "VAT_NO").Rows.Count
        Dim noOfInvoices As Int32 = cdnurTable.DefaultView.ToTable(True, "SI_CODE", "SI_NO").Rows.Count
        Dim noOfVouchers As Int32 = cdnurTable.DefaultView.ToTable(True, "CreditNote_Code", "CreditNote_No").Rows.Count
        Dim sumOfVouchers As Decimal = cdnurTable.DefaultView.ToTable(True, "CreditNote_Code", "CreditNote_No", "CN_Amount").Compute("sum(CN_Amount)", Nothing)

        xlWorkSheet.Cells(3, 2) = noOfVouchers
        xlWorkSheet.Cells(3, 5) = noOfInvoices
        'xlWorkSheet.Cells(3, 5) = noOfVouchers
        xlWorkSheet.Cells(3, 9) = sumOfVouchers
    End Sub

    Private Sub WriteEXEMPData(xlWorkBook As Object)
        If exempTable.Rows.Count = 0 Then
            Exit Sub
        End If

        'Dim rowIndex As Int32 = 5
        'Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("exemp")
        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("exemp")
        'For Each row As DataRow In exempTable.Rows
        '    colIndex = 1
        '    xlWorkSheet.Cells(rowIndex, colIndex) = "B2CL"
        '    xlWorkSheet.Cells(rowIndex, colIndex) = row("CreditNote_Code") + row("CreditNote_No").ToString()
        '    rowIndex += 1
        'Next

        ''set global values

        Dim dataViewinterStateRVouchers As DataView = exempTable.DefaultView
        dataViewinterStateRVouchers.RowFilter = "Description = 'Inter-State supplies to registered persons'"

        Dim interStateRVouchers As Decimal = 0
        If dataViewinterStateRVouchers.Count > 0 Then
            interStateRVouchers = dataViewinterStateRVouchers.ToTable(True, "Taxable_Value").Compute("sum(Taxable_Value)", Nothing)
        End If



        Dim dataViewintraStateRVouchers As DataView = exempTable.DefaultView
        dataViewintraStateRVouchers.RowFilter = "Description = 'Intra-State supplies to registered persons'"
        Dim intraStateRVouchers As Decimal = 0

        If dataViewintraStateRVouchers.Count > 0 Then
            intraStateRVouchers = dataViewintraStateRVouchers.ToTable(True, "Taxable_Value").Compute("sum(Taxable_Value)", Nothing)
        End If




        Dim dataViewinterStateURVouchers As DataView = exempTable.DefaultView
        dataViewinterStateURVouchers.RowFilter = "Description = 'Inter-State supplies to unregistered persons'"
        Dim interStateURVouchers As Decimal = 0
        If (dataViewinterStateURVouchers.Count > 0) Then
            interStateURVouchers = dataViewinterStateURVouchers.ToTable(True, "Taxable_Value").Compute("sum(Taxable_Value)", Nothing)
        End If



        Dim dataViewintraStateURVouchers As DataView = exempTable.DefaultView
        dataViewintraStateURVouchers.RowFilter = "Description = 'Intra-State supplies to unregistered persons'"
        Dim intraStateURVouchers As Decimal = 0
        If dataViewintraStateURVouchers.Count > 0 Then
            intraStateURVouchers = dataViewintraStateURVouchers.ToTable(True, "Taxable_Value").Compute("sum(Taxable_Value)", Nothing)
        End If


        xlWorkSheet.Cells(5, 2) = interStateRVouchers
        xlWorkSheet.Cells(6, 2) = intraStateRVouchers
        xlWorkSheet.Cells(7, 2) = interStateURVouchers
        xlWorkSheet.Cells(8, 2) = intraStateURVouchers
    End Sub

    Private Sub WriteDocsData(xlWorkBook As Object)
        If docsTable.Rows.Count = 0 Then
            Exit Sub
        End If

        ''set global values
        Dim dataViewOutwardSupply As DataView = docsTable.DefaultView
        dataViewOutwardSupply.RowFilter = "Description = 'Invoice for outward supply'"

        ' Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("docs")
        Dim xlWorkSheet As Object = xlWorkBook.Worksheets("docs")
        Dim rowIndex As Int32 = 5
        For Each row As DataRow In dataViewOutwardSupply.ToTable.Rows
            colIndex = 1
            xlWorkSheet.Cells(rowIndex, colIndex) = row("Sr. No. From")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("Sr. No. To")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("Total Number")
            xlWorkSheet.Cells(rowIndex, colIndex) = row("Cancelled")
        Next

        Dim dataViewCreditNote As DataView = docsTable.DefaultView
        dataViewCreditNote.RowFilter = "Description = 'Credit Note'"

        Dim rowIndex1 As Int32 = 6
        For Each row As DataRow In dataViewCreditNote.ToTable.Rows
            colIndex = 1
            xlWorkSheet.Cells(rowIndex1, colIndex) = row("Sr. No. From")
            xlWorkSheet.Cells(rowIndex1, colIndex) = row("Sr. No. To")
            xlWorkSheet.Cells(rowIndex1, colIndex) = row("Total Number")
            xlWorkSheet.Cells(rowIndex1, colIndex) = row("Cancelled")
        Next

    End Sub

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

    '    Private Sub BindData()

    '        Dim QryTemplate = "SELECT  NET_AMOUNT ,
    '        VAT_NO ,
    '        SI_CODE ,
    '        SI_NO ,
    '        SI_DATE ,
    '        STATE_CODE ,
    '        STATE_NAME ,
    '        VAT_PER ,
    '        CAST(SUM(CASE WHEN GSTPaid = 'Y'
    '                      THEN Taxable_Value - ( Taxable_Value - ( Taxable_Value
    '                                                              / ( 1 + VAT_PER
    '                                                              / 100 ) ) )
    '                      ELSE Taxable_Value
    '                 END) AS DECIMAL(18, 2)) AS Taxable_Value ,
    '        Cess_Amount ,
    '        SUM(VAT_AMOUNT) AS VAT_AMOUNT
    'FROM    ( SELECT    inv.NET_AMOUNT ,
    '                    ISNULL(VAT_NO, '') AS VAT_NO ,
    '                    SI_CODE ,
    '                    SI_NO ,
    '                    SI_DATE ,
    '                    STATE_CODE ,
    '                    STATE_NAME ,
    '                    VAT_PER ,
    '                    SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '                          - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '                                        THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '                                               * DISCOUNT_VALUE ) / 100
    '                                        ELSE DISCOUNT_VALUE
    '                                   END, 0) )) AS Taxable_Value ,
    '                    SUM(invd.CessAmount_num) + SUM(invd.ACessAmount) As Cess_Amount ,
    '                    SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT ,
    '                    DISCOUNT_TYPE ,
    '                    DISCOUNT_VALUE ,
    '                    GSTPaid
    '          FROM      dbo.SALE_INVOICE_MASTER inv
    '                    INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
    '                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
    '                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    '          WHERE     inv.INVOICE_STATUS <> 4
    '                    AND MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
    '                    " AND YEAR(SI_DATE) = " + txtFromDate.Value.Year.ToString() &
    '                   " And invd.VAT_AMOUNT > 0                     
    '                    AND 1=1
    '          GROUP BY  inv.NET_AMOUNT ,
    '                    VAT_NO ,
    '                    SI_CODE ,
    '                    SI_NO ,
    '                    SI_DATE ,
    '                    STATE_CODE ,
    '                    STATE_NAME ,
    '                    VAT_PER ,
    '                    DISCOUNT_TYPE ,
    '                    DISCOUNT_VALUE ,
    '                    GSTPaid
    '        ) tb
    'GROUP BY NET_AMOUNT ,
    '        VAT_NO ,
    '        SI_CODE ,
    '        SI_NO ,
    '        SI_DATE ,
    '        STATE_CODE ,
    '        STATE_NAME ,
    '        VAT_PER ,
    '        Cess_Amount
    'ORDER BY SI_NO"


    '        Qry = QryTemplate.Replace("1=1", "LEN(ISNULL(VAT_NO,''))>0")
    '        b2bTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

    '        Qry = QryTemplate.Replace("1=1", "LEN(ISNULL(VAT_NO,''))=0 and inv.NET_AMOUNT > 250000 and inv.Inv_type='I'")
    '        b2clTable = objCommFunction.Fill_DataSet(Qry).Tables(0)


    '        'Qry = "SELECT 
    '        'STATE_CODE ,
    '        '        STATE_NAME ,
    '        '        VAT_PER ,
    '        '        CAST(SUM(CASE WHEN GSTPaid = 'Y'
    '        '                      THEN Taxable_Value - ( Taxable_Value - ( Taxable_Value
    '        '                                                              / ( 1 + VAT_PER
    '        '                                                              / 100 ) ) )
    '        '                      ELSE Taxable_Value
    '        '                 END) AS DECIMAL(18, 2)) AS Taxable_Value ,
    '        '        SUM(Cess_Amount) AS Cess_Amount
    '        'FROM    ( SELECT    inv.SI_ID ,
    '        '                    STATE_CODE ,
    '        '                    STATE_NAME ,
    '        '                    VAT_PER ,
    '        '                    SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '        '                          - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '        '                                        THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '        '                                               * DISCOUNT_VALUE ) / 100
    '        '                                        ELSE DISCOUNT_VALUE
    '        '                                   END, 0) )) AS Taxable_Value ,
    '        '                    SUM(invd.CessAmount_num) Cess_Amount ,
    '        '                    GSTPaid
    '        '          FROM      dbo.SALE_INVOICE_MASTER inv
    '        '                    INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
    '        '                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
    '        '                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '        '                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    '        '          WHERE     inv.INVOICE_STATUS <> 4
    '        '                     AND MONTH(SI_DATE) = " & txtFromDate.Value.Month.ToString() &
    '        '            " AND YEAR(SI_DATE) = " & txtFromDate.Value.Year.ToString() &
    '        '            " AND LEN(ISNULL(VAT_NO, '')) = 0
    '        '                     AND inv.NET_AMOUNT <= 250000
    '        '                     AND inv.Inv_type = 'I'
    '        '                     AND invd.VAT_AMOUNT > 0
    '        '          GROUP BY  STATE_CODE ,
    '        '                    STATE_NAME ,
    '        '                    VAT_PER ,
    '        '                    GSTPaid ,
    '        '                    inv.SI_ID
    '        '          UNION ALL
    '        '          SELECT    inv.SI_ID ,
    '        '                    STATE_CODE ,
    '        '                    STATE_NAME ,
    '        '                    VAT_PER ,
    '        '                    SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '        '                          - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '        '                                        THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '        '                                               * DISCOUNT_VALUE ) / 100
    '        '                                        ELSE DISCOUNT_VALUE
    '        '                                   END, 0) )) AS Taxable_Value ,
    '        '                    SUM(invd.CessAmount_num) Cess_Amount ,
    '        '                    GSTPaid
    '        '          FROM      dbo.SALE_INVOICE_MASTER inv
    '        '                    INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
    '        '                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
    '        '                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '        '                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    '        '          WHERE     inv.INVOICE_STATUS <> 4
    '        '                    AND MONTH(SI_DATE) = " & txtFromDate.Value.Month.ToString() &
    '        '            " And Year(SI_DATE) = " & txtFromDate.Value.Year.ToString() &
    '        '           " AND LEN(ISNULL(VAT_NO, '')) = 0
    '        '                    AND inv.Inv_type <> 'I'
    '        '                    AND invd.VAT_AMOUNT > 0
    '        '          GROUP BY  STATE_CODE ,
    '        '                    STATE_NAME ,
    '        '                    VAT_PER ,
    '        '                    GSTPaid ,
    '        '                    inv.SI_ID
    '        '        ) TB
    '        'GROUP BY STATE_CODE ,
    '        '        STATE_NAME ,
    '        '        VAT_PER  
    '        'ORDER BY TB.STATE_CODE"

    '        Qry = "SELECT  STATE_CODE ,
    '        STATE_NAME ,
    '        VAT_PER ,
    '        SUM(Taxable_Value) AS Taxable_Value ,
    '        SUM(Cess_Amount) AS Cess_Amount
    'FROM    ( SELECT    STATE_CODE ,
    '                    STATE_NAME ,
    '                    VAT_PER ,
    '                    SUM(Taxable_Value) AS Taxable_Value ,
    '                    SUM(Cess_Amount) AS Cess_Amount
    '          FROM      ( SELECT    STATE_CODE ,
    '                                STATE_NAME ,
    '                                VAT_PER ,
    '                                CAST(CASE WHEN GSTPaid = 'Y'
    '                                          THEN Taxable_Value - ( Taxable_Value
    '                                                              - ( Taxable_Value
    '                                                              / ( 1 + VAT_PER
    '                                                              / 100 ) ) )
    '                                          ELSE Taxable_Value
    '                                     END AS DECIMAL(18, 2)) AS Taxable_Value ,
    '                                Cess_Amount AS Cess_Amount
    '                      FROM      ( SELECT    inv.SI_ID ,
    '                                            STATE_CODE ,
    '                                            STATE_NAME ,
    '                                            VAT_PER ,
    '                                            SUM(( ( BAL_ITEM_QTY
    '                                                    * BAL_ITEM_RATE )
    '                                                  - ISNULL(CASE
    '                                                              WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( BAL_ITEM_QTY
    '                                                              * BAL_ITEM_RATE )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                           END, 0) )) AS Taxable_Value ,
    '                                            SUM(invd.CessAmount_num) + SUM(invd.ACessAmount) Cess_Amount ,
    '                                            GSTPaid
    '                                  FROM      dbo.SALE_INVOICE_MASTER inv
    '                                            INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
    '                                            INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
    '                                            INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '                                            INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    '                                  WHERE     inv.INVOICE_STATUS <> 4
    '                                            AND MONTH(SI_DATE) = " & txtFromDate.Value.Month.ToString() & "
    '                                            AND YEAR(SI_DATE) = " & txtFromDate.Value.Year.ToString() & "
    '                                            AND LEN(ISNULL(VAT_NO, '')) = 0
    '                                            AND inv.NET_AMOUNT <= 250000
    '                                            AND inv.Inv_type = 'I'
    '                                            AND invd.VAT_AMOUNT > 0
    '                                  GROUP BY  STATE_CODE ,
    '                                            STATE_NAME ,
    '                                            VAT_PER ,
    '                                            GSTPaid ,
    '                                            inv.SI_ID
    '                                ) tb
    '                      UNION ALL
    '                      SELECT    STATE_CODE ,
    '                                STATE_NAME ,
    '                                VAT_PER ,
    '                                Taxable_Value AS Taxable_Value ,
    '                                Cess_Amount AS Cess_Amount
    '                      FROM      ( SELECT    STATE_CODE ,
    '                                            STATE_NAME ,
    '                                            cnd.Item_Tax AS VAT_PER ,
    '                                            ( CAST(( Item_Rate * Item_Qty ) AS DECIMAL(18,
    '                                                              2)) ) * ( -1 ) AS Taxable_Value ,
    '                                            ( CAST(( cnd.Item_Qty
    '                                                     * cnd.Item_Rate )
    '                                              * cnd.Item_Tax / 100 AS NUMERIC(18,
    '                                                              2)) ) * ( -1 ) AS VAT_AMOUNT ,
    '                                            ( CAST(( cnd.Item_Qty
    '                                                     * cnd.Item_Rate )
    '                                              * cnd.Item_Cess / 100 AS NUMERIC(18,
    '                                                              2)) ) * ( -1 ) AS Cess_Amount
    '                                  FROM      dbo.CreditNote_Master cnm
    '                                            INNER JOIN dbo.SALE_INVOICE_MASTER sim ON sim.SI_ID = cnm.INVId
    '                                            INNER JOIN dbo.CreditNote_DETAIL cnd ON cnd.CreditNote_Id = cnm.CreditNote_Id
    '                                            INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = cnm.CN_CustId
    '                                            INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '                                            INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    '                                  WHERE     MONTH(CreditNote_Date) = " & txtFromDate.Value.Month.ToString() & "
    '                                            AND YEAR(CreditNote_Date) = " & txtFromDate.Value.Year.ToString() & "
    '                                            AND LEN(ISNULL(VAT_NO, '')) = 0
    '                                            AND sim.Inv_type = 'I'
    '                                ) tb
    '                      WHERE     ( -1 ) * VAT_AMOUNT > 0
    '                    ) tbmain
    '          GROUP BY  tbmain.STATE_CODE ,
    '                    tbmain.STATE_NAME ,
    '                    tbmain.VAT_PER
    '          UNION ALL
    '          SELECT    STATE_CODE ,
    '                    STATE_NAME ,
    '                    VAT_PER ,
    '                    SUM(Taxable_Value) AS Taxable_Value ,
    '                    SUM(Cess_Amount) AS Cess_Amount
    '          FROM      ( SELECT    STATE_CODE ,
    '                                STATE_NAME ,
    '                                VAT_PER ,
    '                                CAST(CASE WHEN GSTPaid = 'Y'
    '                                          THEN Taxable_Value - ( Taxable_Value
    '                                                              - ( Taxable_Value
    '                                                              / ( 1 + VAT_PER
    '                                                              / 100 ) ) )
    '                                          ELSE Taxable_Value
    '                                     END AS DECIMAL(18, 2)) AS Taxable_Value ,
    '                                Cess_Amount AS Cess_Amount
    '                      FROM      ( SELECT    inv.SI_ID ,
    '                                            STATE_CODE ,
    '                                            STATE_NAME ,
    '                                            VAT_PER ,
    '                                            SUM(( ( BAL_ITEM_QTY
    '                                                    * BAL_ITEM_RATE )
    '                                                  - ISNULL(CASE
    '                                                              WHEN DISCOUNT_TYPE = 'P'
    '                                                              THEN ( ( BAL_ITEM_QTY
    '                                                              * BAL_ITEM_RATE )
    '                                                              * DISCOUNT_VALUE )
    '                                                              / 100
    '                                                              ELSE DISCOUNT_VALUE
    '                                                           END, 0) )) AS Taxable_Value ,
    '                                            SUM(invd.CessAmount_num) + SUM(invd.ACessAmount) Cess_Amount ,
    '                                            GSTPaid
    '                                  FROM      dbo.SALE_INVOICE_MASTER inv
    '                                            INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
    '                                            INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
    '                                            INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '                                            INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    '                                  WHERE     inv.INVOICE_STATUS <> 4
    '                                            AND MONTH(SI_DATE) = " & txtFromDate.Value.Month.ToString() & "
    '                                            AND YEAR(SI_DATE) = " & txtFromDate.Value.Year.ToString() & "
    '                                            AND LEN(ISNULL(VAT_NO, '')) = 0
    '                                            AND inv.Inv_type <> 'I'
    '                                            AND invd.VAT_AMOUNT > 0
    '                                  GROUP BY  STATE_CODE ,
    '                                            STATE_NAME ,
    '                                            VAT_PER ,
    '                                            GSTPaid ,
    '                                            inv.SI_ID
    '                                ) tb
    '                      UNION ALL
    '                      SELECT    STATE_CODE ,
    '                                STATE_NAME ,
    '                                VAT_PER ,
    '                                Taxable_Value AS Taxable_Value ,
    '                                Cess_Amount AS Cess_Amount
    '                      FROM      ( SELECT    STATE_CODE ,
    '                                            STATE_NAME ,
    '                                            cnd.Item_Tax AS VAT_PER ,
    '                                            ( CAST(( Item_Rate * Item_Qty ) AS DECIMAL(18,
    '                                                              2)) ) * ( -1 ) AS Taxable_Value ,
    '                                            ( CAST(( cnd.Item_Qty
    '                                                     * cnd.Item_Rate )
    '                                              * cnd.Item_Tax / 100 AS NUMERIC(18,
    '                                                              2)) ) * ( -1 ) AS VAT_AMOUNT ,
    '                                            ( CAST(( cnd.Item_Qty
    '                                                     * cnd.Item_Rate )
    '                                              * cnd.Item_Cess / 100 AS NUMERIC(18,
    '                                                              2)) ) * ( -1 ) AS Cess_Amount
    '                                  FROM      dbo.CreditNote_Master cnm
    '                                            INNER JOIN dbo.SALE_INVOICE_MASTER sim ON sim.SI_ID = cnm.INVId
    '                                            INNER JOIN dbo.CreditNote_DETAIL cnd ON cnd.CreditNote_Id = cnm.CreditNote_Id
    '                                            INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = cnm.CN_CustId
    '                                            INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '                                            INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    '                                  WHERE     MONTH(CreditNote_Date) = " & txtFromDate.Value.Month.ToString() & "
    '                                            AND YEAR(CreditNote_Date) = " & txtFromDate.Value.Year.ToString() & "
    '                                            AND LEN(ISNULL(VAT_NO, '')) = 0
    '                                            AND sim.Inv_type <> 'I'
    '                                ) tb
    '                      WHERE     ( -1 ) * VAT_AMOUNT > 0
    '                    ) tbmain
    '          GROUP BY  tbmain.STATE_CODE ,
    '                    tbmain.STATE_NAME ,
    '                    tbmain.VAT_PER
    '        ) main
    'GROUP BY main.STATE_CODE ,
    '        main.STATE_NAME ,
    '        main.VAT_PER"

    '        b2csTable = objCommFunction.Fill_DataSet(Qry).Tables(0)


    '        Qry = " SELECT CAST(HsnCode_vch AS INT) AS HsnCode_vch ,
    '        SUM(Qty) AS Qty ,
    '        SUM(Taxable_Value)  AS Taxable_Value ,
    '        Isnull(SUM(Cess_Amount),0.00)  AS Cess_Amount ,
    '        SUM(non_integrated_tax) AS non_integrated_tax ,
    '        SUM(integrated_tax)  AS integrated_tax 
    ' FROM (



    'SELECT HsnCode_vch ,
    '        SUM(Qty) AS Qty ,
    '        CAST(SUM(CASE WHEN GSTPaid = 'Y'
    '                      THEN Taxable_Value - ( Taxable_Value - ( Taxable_Value
    '                                                              / ( 1 + VAT_PER
    '                                                              / 100 ) ) )
    '                      ELSE Taxable_Value
    '                 END) AS DECIMAL(18, 2)) AS Taxable_Value ,
    '        SUM(Cess_Amount) AS Cess_Amount ,
    '        SUM(non_integrated_tax) AS non_integrated_tax ,
    '        SUM(integrated_tax) AS integrated_tax
    '        FROM   ( SELECT    HsnCode_vch ,
    '                    SUM(invd.BAL_ITEM_QTY) AS Qty ,
    '                    SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '                          - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '                                        THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '                                               * DISCOUNT_VALUE ) / 100
    '                                        ELSE DISCOUNT_VALUE
    '                                   END, 0) )) AS Taxable_Value ,
    '                    SUM(invd.CessAmount_num) + SUM(invd.ACessAmount) Cess_Amount ,
    '                    SUM(CASE WHEN inv.INV_TYPE <> 'I' THEN invd.VAT_AMOUNT
    '                             ELSE 0
    '                        END) AS non_integrated_tax ,
    '                    SUM(CASE WHEN inv.INV_TYPE = 'I' THEN invd.VAT_AMOUNT
    '                             ELSE 0
    '                        END) AS integrated_tax ,
    '                    GSTPaid ,
    '                    VAT_PER
    '          FROM      dbo.SALE_INVOICE_MASTER inv
    '                    INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
    '                    INNER JOIN dbo.ITEM_MASTER im ON invd.ITEM_ID = im.ITEM_ID
    '                    INNER JOIN dbo.UNIT_MASTER um ON um.UM_ID = im.UM_ID
    '                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
    '                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    '                    INNER JOIN dbo.HsnCode_Master hsn ON hsn.Pk_HsnId_num = im.fk_HsnId_num
    '          WHERE     inv.INVOICE_STATUS <> 4
    '                      AND MONTH(SI_DATE) = " & txtFromDate.Value.Month.ToString() &
    '                    " AND YEAR(SI_DATE) = " & txtFromDate.Value.Year.ToString() &
    '          " GROUP BY  HsnCode_vch ,
    '                    GSTPaid ,
    '                    VAT_PER
    '        ) tb GROUP BY HsnCode_vch 

    'UNION ALL

    ' SELECT HsnCode_vch ,
    '        SUM(Qty) * (-1) AS Qty ,
    '        SUM(Taxable_Value) * (-1) AS Taxable_Value ,
    '        SUM(Cess_Amount) * (-1) AS Cess_Amount ,
    '        SUM(non_integrated_tax) * (-1) AS non_integrated_tax ,
    '        SUM(integrated_tax) * (-1) AS integrated_tax
    '        FROM   ( SELECT    HsnCode_vch ,
    '                    SUM(cnd.Item_Qty) AS Qty ,
    '                    SUM(CAST(( cnd.Item_Qty * cnd.Item_Rate ) AS NUMERIC(18, 2))) AS Taxable_Value,
    '                    SUM(CAST(((cnd.Item_Qty * cnd.Item_Rate * cnd.Item_Cess)/100) AS DECIMAL(18,2)))  As Cess_Amount ,
    '                    SUM(CASE WHEN inv.INV_TYPE <> 'I' THEN CAST(((cnd.Item_Qty * cnd.Item_Rate * cnd.Item_Tax)/100) AS DECIMAL(18,2))
    '                             ELSE 0
    '                        END) AS non_integrated_tax ,
    '                    SUM(CASE WHEN inv.INV_TYPE = 'I' THEN CAST(((cnd.Item_Qty * cnd.Item_Rate * cnd.Item_Tax)/100) AS DECIMAL(18,2))
    '                             ELSE 0
    '                        END) AS integrated_tax

    '          FROM   dbo.CreditNote_Master cnm
    '					INNER JOIN dbo.SALE_INVOICE_MASTER inv ON inv.SI_ID = cnm.INVId
    '					INNER JOIN dbo.CreditNote_DETAIL cnd ON cnd.CreditNote_Id = cnm.CreditNote_Id
    '                    INNER JOIN dbo.ITEM_MASTER im ON cnd.ITEM_ID = im.ITEM_ID
    '                    INNER JOIN dbo.UNIT_MASTER um ON um.UM_ID = im.UM_ID
    '                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = cnm.CN_CustId
    '					INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '					INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    '                    INNER JOIN dbo.HsnCode_Master hsn ON hsn.Pk_HsnId_num = im.fk_HsnId_num
    '          WHERE     inv.INVOICE_STATUS <> 4
    '                    AND MONTH(CreditNote_Date) = " & txtFromDate.Value.Month.ToString() & " AND YEAR(CreditNote_Date) = " & txtFromDate.Value.Year.ToString() &
    '                    " GROUP BY  HsnCode_vch

    '        ) tb GROUP BY HsnCode_vch 

    '        )main GROUP BY HsnCode_vch ORDER BY HsnCode_vch


    '"

    '        'Qry = " SELECT HsnCode_vch, SUM(invd.BAL_ITEM_QTY) AS Qty, " &
    '        '    " SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value, SUM(0) Cess_Amount," &
    '        '    " SUM(CASE WHEN inv.INV_TYPE <> 'I' THEN invd.VAT_AMOUNT ELSE 0 END) AS non_integrated_tax," &
    '        '    " SUM(CASE WHEN inv.INV_TYPE = 'I' THEN invd.VAT_AMOUNT ELSE 0 END) AS integrated_tax" &
    '        '    " FROM    dbo.SALE_INVOICE_MASTER inv" &
    '        '    " INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID" &
    '        '    " INNER JOIN dbo.ITEM_MASTER im ON invd.ITEM_ID = im.ITEM_ID" &
    '        '    " INNER JOIN dbo.UNIT_MASTER um ON um.UM_ID = im.UM_ID" &
    '        '    " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
    '        '    " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID" &
    '        '    " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
    '        '    " INNER JOIN dbo.HsnCode_Master hsn ON hsn.Pk_HsnId_num = im.fk_HsnId_num" &
    '        '    " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" & txtFromDate.Value.Month.ToString() &
    '        '    " And YEAR(SI_DATE)=" & txtFromDate.Value.Year.ToString() &
    '        '    " GROUP BY HsnCode_vch order by HsnCode_vch"

    '        hsnTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

    '        Qry = " SELECT  ISNULL(VAT_NO,'') As VAT_NO, ACC_NAME, CreditNote_Code, CreditNote_No, CreditNote_Date, cnm.CN_Amount, SI_CODE, SI_NO," &
    '        " SI_DATE, STATE_CODE , STATE_NAME , Tax_Amt As Item_Tax, CAST(SUM(( Item_Rate * Item_Qty )) AS DECIMAL(18,2)) AS Taxable_Value , Cess_Amt As Cess_Amount " &
    '        " FROM    dbo.CreditNote_Master cnm " &
    '        " INNER JOIN dbo.SALE_INVOICE_MASTER sim ON sim.SI_ID = cnm.INVId " &
    '        " INNER JOIN dbo.CreditNote_DETAIL cnd ON cnd.CreditNote_Id = cnm.CreditNote_Id " &
    '        " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = cnm.CN_CustId " &
    '        " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID " &
    '        " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID " &
    '        " WHERE MONTH(CreditNote_Date) =" & txtFromDate.Value.Month.ToString() &
    '        " And YEAR(CreditNote_Date)=" & txtFromDate.Value.Year.ToString() &
    '        " AND LEN(ISNULL(VAT_NO,'')) > 0" &
    '        " GROUP BY VAT_NO, ACC_NAME, CreditNote_Code, CreditNote_No, CreditNote_Date, cnm.CN_Amount, SI_CODE, SI_NO," &
    '        " SI_DATE, STATE_CODE, STATE_NAME, Tax_Amt, Cess_Amt ORDER BY cnm.CreditNote_No"

    '        cdnrTable = objCommFunction.Fill_DataSet(Qry).Tables(0)


    '        Qry = " SELECT ISNULL(VAT_NO,'') As VAT_NO, ACC_NAME ,CreditNote_Code ,CreditNote_No,CreditNote_Date ,cnm.CN_Amount,SI_CODE, SI_NO," &
    '        " SI_DATE,STATE_CODE ,STATE_NAME , Tax_Amt As Item_Tax, CAST(SUM(( Item_Rate * Item_Qty )) AS DECIMAL(18,2)) AS Taxable_Value , Cess_Amt As Cess_Amount " &
    '        " FROM    dbo.CreditNote_Master cnm " &
    '        " INNER JOIN dbo.SALE_INVOICE_MASTER sim ON sim.SI_ID = cnm.INVId " &
    '        " INNER JOIN dbo.CreditNote_DETAIL cnd ON cnd.CreditNote_Id = cnm.CreditNote_Id " &
    '        " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = cnm.CN_CustId " &
    '        " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID " &
    '        " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID " &
    '        " WHERE MONTH(CreditNote_Date) =" & txtFromDate.Value.Month.ToString() &
    '        " And YEAR(CreditNote_Date)=" & txtFromDate.Value.Year.ToString() &
    '        " AND LEN(ISNULL(VAT_NO,''))= 0 and sim.NET_AMOUNT > 250000 and sim.Inv_type='I' " &
    '         " GROUP BY  VAT_NO, ACC_NAME ,CreditNote_Code ,CreditNote_No,CreditNote_Date ,cnm.CN_Amount,SI_CODE, SI_NO," &
    '        " SI_DATE,STATE_CODE ,STATE_NAME ,Tax_Amt, Cess_Amt ORDER BY cnm.CreditNote_No"


    '        cdnurTable = objCommFunction.Fill_DataSet(Qry).Tables(0)


    '        Qry = "SELECT  'Inter-State supplies to registered persons' AS Description ,
    '        ISNULL(SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '                     - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '                                   THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '                                          * DISCOUNT_VALUE ) / 100
    '                                   ELSE DISCOUNT_VALUE
    '                              END, 0) )), 0) AS Taxable_Value
    'FROM    dbo.SALE_INVOICE_MASTER inv
    '        INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
    '        INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
    '        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    'WHERE   inv.INVOICE_STATUS <> 4
    '        AND MONTH(SI_DATE) = " + txtFromDate.Value.Month.ToString() &
    '        " AND YEAR(SI_DATE) = " + txtFromDate.Value.Year.ToString() &
    '        " AND LEN(ISNULL(VAT_NO, '')) > 0
    '        AND invd.VAT_AMOUNT = 0
    '        AND inv.Inv_type = 'I'
    'UNION ALL
    'SELECT  'Intra-State supplies to registered persons' AS Description ,
    '        ISNULL(SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '                     - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '                                   THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '                                          * DISCOUNT_VALUE ) / 100
    '                                   ELSE DISCOUNT_VALUE
    '                              END, 0) )), 0) AS Taxable_Value
    'FROM    dbo.SALE_INVOICE_MASTER inv
    '        INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
    '        INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
    '        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    'WHERE   inv.INVOICE_STATUS <> 4
    '         AND MONTH(SI_DATE) = " + txtFromDate.Value.Month.ToString() &
    '        " AND YEAR(SI_DATE) = " + txtFromDate.Value.Year.ToString() &
    '        " AND LEN(ISNULL(VAT_NO, '')) > 0
    '        AND invd.VAT_AMOUNT = 0
    '        AND inv.Inv_type <> 'I'
    'UNION ALL
    'SELECT  'Inter-State supplies to unregistered persons' AS Description ,
    '        ISNULL(SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '                     - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '                                   THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '                                          * DISCOUNT_VALUE ) / 100
    '                                   ELSE DISCOUNT_VALUE
    '                              END, 0) )), 0) AS Taxable_Value
    'FROM    dbo.SALE_INVOICE_MASTER inv
    '        INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
    '        INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
    '        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    'WHERE   inv.INVOICE_STATUS <> 4
    '        AND MONTH(SI_DATE) = " + txtFromDate.Value.Month.ToString() &
    '        " AND YEAR(SI_DATE) = " + txtFromDate.Value.Year.ToString() &
    '        " AND LEN(ISNULL(VAT_NO, '')) = 0
    '        AND invd.VAT_AMOUNT = 0
    '        AND inv.Inv_type = 'I'
    'UNION ALL
    'SELECT  'Intra-State supplies to unregistered persons' AS Description ,
    '        ISNULL(SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '                     - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
    '                                   THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
    '                                          * DISCOUNT_VALUE ) / 100
    '                                   ELSE DISCOUNT_VALUE
    '                              END, 0) )), 0) AS Taxable_Value
    'FROM    dbo.SALE_INVOICE_MASTER inv
    '        INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
    '        INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
    '        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
    '        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
    'WHERE   inv.INVOICE_STATUS <> 4
    '         AND MONTH(SI_DATE) = " + txtFromDate.Value.Month.ToString() &
    '        " AND YEAR(SI_DATE) = " + txtFromDate.Value.Year.ToString() &
    '        " AND LEN(ISNULL(VAT_NO, '')) = 0
    '        AND invd.VAT_AMOUNT = 0
    '        AND inv.Inv_type <> 'I'"

    '        'Qry = "SELECT 'Inter-State supplies to registered persons' AS Description, inv.NET_AMOUNT, ISNULL(VAT_NO,'') As VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,  " &
    '        '    " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
    '        '    " SUM(0) Cess_Amount, SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT" &
    '        '    " FROM    dbo.SALE_INVOICE_MASTER inv" &
    '        '    " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
    '        '    " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
    '        '    " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
    '        '    " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
    '        '    " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
    '        '    " And YEAR(SI_DATE)=" + txtFromDate.Value.Year.ToString() + " AND LEN(ISNULL(VAT_NO,'')) > 0 AND invd.VAT_AMOUNT = 0 AND inv.Inv_type = 'I' " &
    '        '    " GROUP BY inv.NET_AMOUNT, VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,VAT_PER " &
    '        '    " Union All " &
    '        '    "SELECT 'Intra-State supplies to registered persons' AS Description, inv.NET_AMOUNT, ISNULL(VAT_NO,'') As VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,  " &
    '        '    " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
    '        '    " SUM(0) Cess_Amount, SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT" &
    '        '    " FROM    dbo.SALE_INVOICE_MASTER inv" &
    '        '    " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
    '        '    " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
    '        '    " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
    '        '    " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
    '        '    " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
    '        '    " And YEAR(SI_DATE)=" + txtFromDate.Value.Year.ToString() + " AND LEN(ISNULL(VAT_NO,'')) > 0 AND invd.VAT_AMOUNT = 0 AND inv.Inv_type <> 'I' " &
    '        '    " GROUP BY inv.NET_AMOUNT, VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,VAT_PER " &
    '        '    " Union All " &
    '        '    "SELECT 'Inter-State supplies to unregistered persons' AS Description, inv.NET_AMOUNT, ISNULL(VAT_NO,'') As VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,  " &
    '        '    " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
    '        '    " SUM(0) Cess_Amount, SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT" &
    '        '    " FROM    dbo.SALE_INVOICE_MASTER inv" &
    '        '    " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
    '        '    " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
    '        '    " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
    '        '    " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
    '        '    " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
    '        '    " And YEAR(SI_DATE)=" + txtFromDate.Value.Year.ToString() + " AND LEN(ISNULL(VAT_NO,'')) = 0 AND invd.VAT_AMOUNT = 0 AND inv.Inv_type = 'I' " &
    '        '    " GROUP BY inv.NET_AMOUNT, VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,VAT_PER " &
    '        '    " Union All " &
    '        '    "SELECT 'Intra-State supplies to unregistered persons' AS Description, inv.NET_AMOUNT, ISNULL(VAT_NO,'') As VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,  " &
    '        '    " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
    '        '    " SUM(0) Cess_Amount, SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT" &
    '        '    " FROM    dbo.SALE_INVOICE_MASTER inv" &
    '        '    " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
    '        '    " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
    '        '    " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
    '        '    " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
    '        '    " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
    '        '    " And YEAR(SI_DATE)=" + txtFromDate.Value.Year.ToString() + " AND LEN(ISNULL(VAT_NO,'')) = 0 AND invd.VAT_AMOUNT = 0 AND inv.Inv_type <> 'I' " &
    '        '    " GROUP BY inv.NET_AMOUNT, VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,VAT_PER  "

    '        exempTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

    '        Qry = "SELECT 'Invoice for outward supply' AS Description,  Isnull(MIN(SI_NO),0) AS 'Sr. No. From', Isnull(MAX(SI_NO),0) AS 'Sr. No. To', COUNT(SI_No) AS 'Total Number', " &
    '        "  ( SELECT  COUNT(SI_NO) FROM  dbo.SALE_INVOICE_MASTER WHERE MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
    '        "   AND YEAR(SI_DATE) = " + txtFromDate.Value.Year.ToString() &
    '        "  AND INVOICE_STATUS = 4) AS Cancelled FROM  dbo.SALE_INVOICE_MASTER WHERE MONTH(SI_DATE) = " + txtFromDate.Value.Month.ToString() &
    '        "  AND YEAR(SI_DATE) = " + txtFromDate.Value.Year.ToString() &
    '        "  UNION ALL " &
    '        "  SELECT 'Credit Note' AS Description, Isnull(MIN(CreditNote_No),0) AS 'Sr. No. From', Isnull(MAX(CreditNote_No),0) AS 'Sr. No. To', COUNT(CreditNote_No) AS 'Total Number', " &
    '        "  0 AS Cancelled FROM  dbo.CreditNote_Master WHERE MONTH(CreditNote_Date) = " + txtFromDate.Value.Month.ToString() &
    '        "  AND YEAR(CreditNote_Date) = " + txtFromDate.Value.Year.ToString()

    '        docsTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

    '    End Sub

    Private Sub BindData()

        Qry = "SELECT  NET_AMOUNT ,
        VAT_NO ,
        SI_CODE ,
        SI_NO ,
        SI_DATE ,
        STATE_CODE ,
        STATE_NAME ,
        VAT_PER ,
        CAST(SUM(CASE WHEN GSTPaid = 'Y'
                      THEN Taxable_Value - ( Taxable_Value - ( Taxable_Value
                                                              / ( 1 + VAT_PER
                                                              / 100 ) ) )
                      ELSE Taxable_Value
                 END) AS DECIMAL(18, 2)) AS Taxable_Value ,
        Cess_Amount ,
        SUM(VAT_AMOUNT) AS VAT_AMOUNT
FROM    ( SELECT    inv.NET_AMOUNT ,
                    ISNULL(VAT_NO, '') AS VAT_NO ,
                    SI_CODE ,
                    SI_NO ,
                    SI_DATE ,
                    STATE_CODE ,
                    STATE_NAME ,
                    VAT_PER ,
                    SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                          - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                        THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                                               * DISCOUNT_VALUE ) / 100
                                        ELSE DISCOUNT_VALUE
                                   END, 0) )) AS Taxable_Value ,
                    SUM(invd.CessAmount_num) + SUM(invd.ACessAmount) As Cess_Amount ,
                    SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT ,
                    DISCOUNT_TYPE ,
                    DISCOUNT_VALUE ,
                    GSTPaid
          FROM      dbo.SALE_INVOICE_MASTER inv
                    INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
          WHERE     inv.INVOICE_STATUS <> 4
                    AND cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                    And invd.VAT_AMOUNT > 0
                    And LEN(ISNULL(VAT_NO,'')) > 0
          GROUP BY  inv.NET_AMOUNT ,
                    VAT_NO ,
                    SI_CODE ,
                    SI_NO ,
                    SI_DATE ,
                    STATE_CODE ,
                    STATE_NAME ,
                    VAT_PER ,
                    DISCOUNT_TYPE ,
                    DISCOUNT_VALUE ,
                    GSTPaid

           UNION ALL
          
           SELECT    ( SUM(ISNULL(pt.TotalAmountReceived, 0))
                      + SUM(ISNULL(pt.GSTPerAmt, 0)) ) AS NET_AMOUNT ,
                    ISNULL(am.VAT_NO, '') AS VAT_NO ,
                    pt.ChequeDraftNo AS SI_CODE ,
                    0 AS SI_NO ,
                    cast(pt.PaymentDate as date) AS SI_DATE ,
                    STATE_CODE ,
                    STATE_NAME ,
                    CASE WHEN pt.fk_GST_ID = 1 THEN 0.00
                         WHEN pt.fk_GST_ID = 2 THEN 5.00
                         WHEN pt.fk_GST_ID = 3 THEN 12.00
                         WHEN pt.fk_GST_ID = 4 THEN 18.00
                         WHEN pt.fk_GST_ID = 5 THEN 28.00
                         WHEN pt.fk_GST_ID = 6 THEN 3.00
                    END AS VAT_PER ,
                    SUM(ISNULL(pt.TotalAmountReceived, 0)) AS Taxable_Value ,
                    0.00 AS Cess_Amount ,
                    SUM(ISNULL(pt.GSTPerAmt, 0)) AS VAT_AMOUNT ,
                    '' AS DISCOUNT_TYPE ,
                    0 AS DISCOUNT_VALUE ,
                    '' AS GSTPaid
          FROM      dbo.PaymentTransaction AS pt
                    INNER JOIN dbo.ACCOUNT_MASTER am ON pt.AccountId = am.ACC_ID
                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
          WHERE     CAST(pt.PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                    And pt.GST_Applicable_Acc = 'Cr.' and pt.StatusId <> 3
                    AND pt.GSTPerAmt > 0
                    And Len(ISNULL(VAT_NO, '')) > 0
          GROUP BY  VAT_NO ,
                    pt.ChequeDraftNo ,
                    cast(pt.PaymentDate as date) ,
                    STATE_CODE ,
                    STATE_NAME ,
                    cm.STATE_ID ,
                    pt.fk_GST_ID
        ) tb
GROUP BY NET_AMOUNT ,
        VAT_NO ,
        SI_CODE ,
        SI_NO ,
        SI_DATE ,
        STATE_CODE ,
        STATE_NAME ,
        VAT_PER ,
        Cess_Amount
ORDER BY SI_NO"


        'Qry = QryTemplate.Replace("1=1", "LEN(ISNULL(VAT_NO,''))>0")
        b2bTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

        Qry = "SELECT  NET_AMOUNT ,
        VAT_NO ,
        SI_CODE ,
        SI_NO ,
        SI_DATE ,
        STATE_CODE ,
        STATE_NAME ,
        VAT_PER ,
        CAST(SUM(CASE WHEN GSTPaid = 'Y'
                      THEN Taxable_Value - ( Taxable_Value - ( Taxable_Value
                                                              / ( 1 + VAT_PER
                                                              / 100 ) ) )
                      ELSE Taxable_Value
                 END) AS DECIMAL(18, 2)) AS Taxable_Value ,
        Cess_Amount ,
        SUM(VAT_AMOUNT) AS VAT_AMOUNT
FROM    ( SELECT    inv.NET_AMOUNT ,
                    ISNULL(VAT_NO, '') AS VAT_NO ,
                    SI_CODE ,
                    SI_NO ,
                    SI_DATE ,
                    STATE_CODE ,
                    STATE_NAME ,
                    VAT_PER ,
                    SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                          - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                        THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                                               * DISCOUNT_VALUE ) / 100
                                        ELSE DISCOUNT_VALUE
                                   END, 0) )) AS Taxable_Value ,
                    SUM(invd.CessAmount_num) + SUM(invd.ACessAmount) As Cess_Amount ,
                    SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT ,
                    DISCOUNT_TYPE ,
                    DISCOUNT_VALUE ,
                    GSTPaid
          FROM      dbo.SALE_INVOICE_MASTER inv
                    INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
          WHERE     inv.INVOICE_STATUS <> 4
                    AND cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                    And invd.VAT_AMOUNT > 0
                    And LEN(ISNULL(VAT_NO,'')) = 0 and inv.NET_AMOUNT > 250000 and inv.Inv_type='I'
          GROUP BY  inv.NET_AMOUNT ,
                    VAT_NO ,
                    SI_CODE ,
                    SI_NO ,
                    SI_DATE ,
                    STATE_CODE ,
                    STATE_NAME ,
                    VAT_PER ,
                    DISCOUNT_TYPE ,
                    DISCOUNT_VALUE ,
                    GSTPaid

          UNION ALL
          
          Select NET_AMOUNT , VAT_NO , SI_CODE, SI_NO , SI_DATE , STATE_CODE, STATE_NAME , VAT_PER , Taxable_Value,
                    Cess_Amount , VAT_AMOUNT ,DISCOUNT_TYPE, DISCOUNT_VALUE , GSTPaid  from (

          SELECT    ( SUM(ISNULL(pt.TotalAmountReceived, 0))
                      + SUM(ISNULL(pt.GSTPerAmt, 0)) ) AS NET_AMOUNT ,
                    ISNULL(am.VAT_NO, '') AS VAT_NO ,
                    pt.ChequeDraftNo AS SI_CODE ,
                    0 AS SI_NO ,
                    cast(pt.PaymentDate as date) AS SI_DATE ,
                    STATE_CODE ,
                    STATE_NAME ,
                    CASE WHEN pt.fk_GST_ID = 1 THEN 0.00
                         WHEN pt.fk_GST_ID = 2 THEN 5.00
                         WHEN pt.fk_GST_ID = 3 THEN 12.00
                         WHEN pt.fk_GST_ID = 4 THEN 18.00
                         WHEN pt.fk_GST_ID = 5 THEN 28.00
                         WHEN pt.fk_GST_ID = 6 THEN 3.00
                    END AS VAT_PER ,
                    SUM(ISNULL(pt.TotalAmountReceived, 0)) AS Taxable_Value ,
                    0.00 AS Cess_Amount ,
                    SUM(ISNULL(pt.GSTPerAmt, 0)) AS VAT_AMOUNT ,
                    '' AS DISCOUNT_TYPE ,
                    0 AS DISCOUNT_VALUE ,
                    '' AS GSTPaid ,
                    CASE WHEN cm.STATE_ID <> " & stateId & " THEN 'I'
                         WHEN cm.STATE_ID =  " & stateId & " THEN 'S'
                    END AS Inv_Type
          FROM      dbo.PaymentTransaction AS pt
                    INNER JOIN dbo.ACCOUNT_MASTER am ON pt.AccountId = am.ACC_ID
                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
          WHERE     CAST(pt.PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                    And pt.GST_Applicable_Acc = 'Cr.' and pt.StatusId <> 3
                    And LEN(ISNULL(VAT_NO,'')) = 0 
                    AND pt.GSTPerAmt > 0
                    and ((ISNULL(pt.TotalAmountReceived, 0)) + (ISNULL(pt.GSTPerAmt, 0))) > 250000
          GROUP BY  VAT_NO ,
                    pt.ChequeDraftNo ,
                    cast(pt.PaymentDate as date) ,
                    STATE_CODE ,
                    STATE_NAME ,
                    cm.STATE_ID ,
                    sm.IsUT_Bit ,
                    pt.fk_GST_ID

            )subquery where Inv_type='I'

        ) tb
GROUP BY NET_AMOUNT ,
        VAT_NO ,
        SI_CODE ,
        SI_NO ,
        SI_DATE ,
        STATE_CODE ,
        STATE_NAME ,
        VAT_PER ,
        Cess_Amount
ORDER BY SI_NO"

        'Qry = QryTemplate.Replace("1=1", "LEN(ISNULL(VAT_NO,''))=0 and inv.NET_AMOUNT > 250000 and inv.Inv_type='I'")
        b2clTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

        Qry = "SELECT  STATE_CODE ,
        STATE_NAME ,
        VAT_PER ,
        SUM(Taxable_Value) AS Taxable_Value ,
        SUM(Cess_Amount) AS Cess_Amount
FROM    ( SELECT    STATE_CODE ,
                    STATE_NAME ,
                    VAT_PER ,
                    SUM(Taxable_Value) AS Taxable_Value ,
                    SUM(Cess_Amount) AS Cess_Amount
          FROM      ( SELECT    STATE_CODE ,
                                STATE_NAME ,
                                VAT_PER ,
                                CAST(CASE WHEN GSTPaid = 'Y'
                                          THEN Taxable_Value - ( Taxable_Value
                                                              - ( Taxable_Value
                                                              / ( 1 + VAT_PER
                                                              / 100 ) ) )
                                          ELSE Taxable_Value
                                     END AS DECIMAL(18, 2)) AS Taxable_Value ,
                                Cess_Amount AS Cess_Amount
                      FROM      ( SELECT    inv.SI_ID ,
                                            STATE_CODE ,
                                            STATE_NAME ,
                                            VAT_PER ,
                                            SUM(( ( BAL_ITEM_QTY
                                                    * BAL_ITEM_RATE )
                                                  - ISNULL(CASE
                                                              WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( BAL_ITEM_QTY
                                                              * BAL_ITEM_RATE )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                           END, 0) )) AS Taxable_Value ,
                                            SUM(invd.CessAmount_num) + SUM(invd.ACessAmount) Cess_Amount ,
                                            GSTPaid
                                  FROM      dbo.SALE_INVOICE_MASTER inv
                                            INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
                                            INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
                                            INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                                            INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                                  WHERE     inv.INVOICE_STATUS <> 4
                                            AND cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                                            AND LEN(ISNULL(VAT_NO, '')) = 0
                                            AND inv.NET_AMOUNT <= 250000
                                            AND inv.Inv_type = 'I'
                                            AND invd.VAT_AMOUNT > 0
                                  GROUP BY  STATE_CODE ,
                                            STATE_NAME ,
                                            VAT_PER ,
                                            GSTPaid ,
                                            inv.SI_ID


                                  UNION ALL

                                  SELECT    SI_ID ,
                                            STATE_CODE ,
                                            STATE_NAME ,
                                            VAT_PER ,
                                            Taxable_Value ,
                                            Cess_Amount ,
                                            GSTPaid 
                                  FROM      ( SELECT    pt.PaymentTransactionId AS SI_ID ,
                                                        STATE_CODE ,
                                                        STATE_NAME ,
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
                                                        SUM(ISNULL(pt.TotalAmountReceived,
                                                              0)) AS Taxable_Value ,
                                                        0.00 AS Cess_Amount ,
                                                        '' AS GSTPaid ,
                                                        CASE WHEN cm.STATE_ID <> " & stateId & " THEN 'I'
                                                             WHEN cm.STATE_ID =  " & stateId & " THEN 'S'
                                                        END AS Inv_Type
                                              FROM      dbo.PaymentTransaction
                                                        AS pt
                                                        INNER JOIN dbo.ACCOUNT_MASTER am ON pt.AccountId = am.ACC_ID
                                                        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                                                        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                                              WHERE     CAST(pt.PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                                                        AND pt.GST_Applicable_Acc = 'Cr.' and pt.StatusId <> 3
                                                        AND LEN(ISNULL(VAT_NO,
                                                              '')) = 0
                                                        AND ( ( ISNULL(pt.TotalAmountReceived,
                                                              0) )
                                                              + ( ISNULL(pt.GSTPerAmt,
                                                              0) ) ) <= 250000
                                                        AND pt.GSTPerAmt > 0
                                              GROUP BY  STATE_CODE ,
                                                        STATE_NAME ,
                                                        cm.STATE_ID ,
                                                        sm.IsUT_Bit ,
                                                        pt.fk_GST_ID ,
                                                        pt.PaymentTransactionId
                                            ) subquery
                                  WHERE     Inv_Type = 'I' 

                                ) tb
                      UNION ALL
                      SELECT    STATE_CODE ,
                                STATE_NAME ,
                                VAT_PER ,
                                Taxable_Value AS Taxable_Value ,
                                Cess_Amount AS Cess_Amount
                      FROM      ( SELECT    STATE_CODE ,
                                            STATE_NAME ,
                                            cnd.Item_Tax AS VAT_PER ,
                                            ( CAST(( Item_Rate * Item_Qty ) AS DECIMAL(18,
                                                              2)) ) * ( -1 ) AS Taxable_Value ,
                                            ( CAST(( cnd.Item_Qty
                                                     * cnd.Item_Rate )
                                              * cnd.Item_Tax / 100 AS NUMERIC(18,
                                                              2)) ) * ( -1 ) AS VAT_AMOUNT ,
                                            ( CAST(( cnd.Item_Qty
                                                     * cnd.Item_Rate )
                                              * cnd.Item_Cess / 100 AS NUMERIC(18,
                                                              2)) ) * ( -1 ) AS Cess_Amount
                                  FROM      dbo.CreditNote_Master cnm
                                            INNER JOIN dbo.SALE_INVOICE_MASTER sim ON sim.SI_ID = cnm.INVId
                                            INNER JOIN dbo.CreditNote_DETAIL cnd ON cnd.CreditNote_Id = cnm.CreditNote_Id
                                            INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = cnm.CN_CustId
                                            INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                                            INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                                            where cast(CreditNote_Date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                                            AND LEN(ISNULL(VAT_NO, '')) = 0
                                            AND sim.Inv_type = 'I'
                                ) tb
                      WHERE     ( -1 ) * VAT_AMOUNT > 0
                    ) tbmain
          GROUP BY  tbmain.STATE_CODE ,
                    tbmain.STATE_NAME ,
                    tbmain.VAT_PER
          UNION ALL
          SELECT    STATE_CODE ,
                    STATE_NAME ,
                    VAT_PER ,
                    SUM(Taxable_Value) AS Taxable_Value ,
                    SUM(Cess_Amount) AS Cess_Amount
          FROM      ( SELECT    STATE_CODE ,
                                STATE_NAME ,
                                VAT_PER ,
                                CAST(CASE WHEN GSTPaid = 'Y'
                                          THEN Taxable_Value - ( Taxable_Value
                                                              - ( Taxable_Value
                                                              / ( 1 + VAT_PER
                                                              / 100 ) ) )
                                          ELSE Taxable_Value
                                     END AS DECIMAL(18, 2)) AS Taxable_Value ,
                                Cess_Amount AS Cess_Amount
                      FROM      ( SELECT    inv.SI_ID ,
                                            STATE_CODE ,
                                            STATE_NAME ,
                                            VAT_PER ,
                                            SUM(( ( BAL_ITEM_QTY
                                                    * BAL_ITEM_RATE )
                                                  - ISNULL(CASE
                                                              WHEN DISCOUNT_TYPE = 'P'
                                                              THEN ( ( BAL_ITEM_QTY
                                                              * BAL_ITEM_RATE )
                                                              * DISCOUNT_VALUE )
                                                              / 100
                                                              ELSE DISCOUNT_VALUE
                                                           END, 0) )) AS Taxable_Value ,
                                            SUM(invd.CessAmount_num) + SUM(invd.ACessAmount) Cess_Amount ,
                                            GSTPaid
                                  FROM      dbo.SALE_INVOICE_MASTER inv
                                            INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
                                            INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
                                            INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                                            INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                                  WHERE     inv.INVOICE_STATUS <> 4
                                            AND cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                                            AND LEN(ISNULL(VAT_NO, '')) = 0
                                            AND inv.Inv_type <> 'I'
                                            AND invd.VAT_AMOUNT > 0
                                  GROUP BY  STATE_CODE ,
                                            STATE_NAME ,
                                            VAT_PER ,
                                            GSTPaid ,
                                            inv.SI_ID

                                  UNION ALL
                                  SELECT    SI_ID ,
                                            STATE_CODE ,
                                            STATE_NAME ,
                                            VAT_PER ,
                                            Taxable_Value ,
                                            Cess_Amount ,
                                            GSTPaid 
                                  FROM      ( SELECT    pt.PaymentTransactionId AS SI_ID ,
                                                        STATE_CODE ,
                                                        STATE_NAME ,
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
                                                        SUM(ISNULL(pt.TotalAmountReceived,
                                                              0)) AS Taxable_Value ,
                                                        0.00 AS Cess_Amount ,
                                                        '' AS GSTPaid ,
                                                        CASE WHEN cm.STATE_ID <> " & stateId & " THEN 'I'
                                                             WHEN cm.STATE_ID =  " & stateId & " THEN 'S'
                                                        END AS Inv_Type
                                              FROM      dbo.PaymentTransaction
                                                        AS pt
                                                        INNER JOIN dbo.ACCOUNT_MASTER am ON pt.AccountId = am.ACC_ID
                                                        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                                                        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                                              WHERE     CAST(pt.PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                                                        AND pt.GST_Applicable_Acc = 'Cr.' and pt.StatusId <> 3
                                                        AND LEN(ISNULL(VAT_NO,
                                                              '')) = 0
                                                        AND pt.GSTPerAmt > 0
                                              GROUP BY  STATE_CODE ,
                                                        STATE_NAME ,
                                                        cm.STATE_ID ,
                                                        sm.IsUT_Bit ,
                                                        pt.fk_GST_ID ,
                                                        pt.PaymentTransactionId
                                            ) subquery
                                  WHERE     Inv_Type <> 'I' 


                                ) tb
                      UNION ALL
                      SELECT    STATE_CODE ,
                                STATE_NAME ,
                                VAT_PER ,
                                Taxable_Value AS Taxable_Value ,
                                Cess_Amount AS Cess_Amount
                      FROM      ( SELECT    STATE_CODE ,
                                            STATE_NAME ,
                                            cnd.Item_Tax AS VAT_PER ,
                                            ( CAST(( Item_Rate * Item_Qty ) AS DECIMAL(18,
                                                              2)) ) * ( -1 ) AS Taxable_Value ,
                                            ( CAST(( cnd.Item_Qty
                                                     * cnd.Item_Rate )
                                              * cnd.Item_Tax / 100 AS NUMERIC(18,
                                                              2)) ) * ( -1 ) AS VAT_AMOUNT ,
                                            ( CAST(( cnd.Item_Qty
                                                     * cnd.Item_Rate )
                                              * cnd.Item_Cess / 100 AS NUMERIC(18,
                                                              2)) ) * ( -1 ) AS Cess_Amount
                                  FROM      dbo.CreditNote_Master cnm
                                            INNER JOIN dbo.SALE_INVOICE_MASTER sim ON sim.SI_ID = cnm.INVId
                                            INNER JOIN dbo.CreditNote_DETAIL cnd ON cnd.CreditNote_Id = cnm.CreditNote_Id
                                            INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = cnm.CN_CustId
                                            INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                                            INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                                  WHERE     cast(CreditNote_Date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                                            AND LEN(ISNULL(VAT_NO, '')) = 0
                                            AND sim.Inv_type <> 'I'
                                ) tb
                      WHERE     ( -1 ) * VAT_AMOUNT > 0
                    ) tbmain
          GROUP BY  tbmain.STATE_CODE ,
                    tbmain.STATE_NAME ,
                    tbmain.VAT_PER
        ) main
GROUP BY main.STATE_CODE ,
        main.STATE_NAME ,
        main.VAT_PER"

        b2csTable = objCommFunction.Fill_DataSet(Qry).Tables(0)


        Qry = " SELECT CAST(HsnCode_vch AS INT) AS HsnCode_vch ,
        SUM(Qty) AS Qty ,
        SUM(Taxable_Value)  AS Taxable_Value ,
        Isnull(SUM(Cess_Amount),0.00)  AS Cess_Amount ,
        SUM(non_integrated_tax) AS non_integrated_tax ,
        SUM(integrated_tax)  AS integrated_tax 
        FROM (

        SELECT HsnCode_vch ,
        SUM(Qty) AS Qty ,
        CAST(SUM(CASE WHEN GSTPaid = 'Y'
                      THEN Taxable_Value - ( Taxable_Value - ( Taxable_Value
                                                              / ( 1 + VAT_PER
                                                              / 100 ) ) )
                      ELSE Taxable_Value
                 END) AS DECIMAL(18, 2)) AS Taxable_Value ,
        SUM(Cess_Amount) AS Cess_Amount ,
        SUM(non_integrated_tax) AS non_integrated_tax ,
        SUM(integrated_tax) AS integrated_tax
        FROM   ( SELECT    HsnCode_vch ,
                    SUM(invd.BAL_ITEM_QTY) AS Qty ,
                    SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                          - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                        THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                                               * DISCOUNT_VALUE ) / 100
                                        ELSE DISCOUNT_VALUE
                                   END, 0) )) AS Taxable_Value ,
                    SUM(invd.CessAmount_num) + SUM(invd.ACessAmount) Cess_Amount ,
                    SUM(CASE WHEN inv.INV_TYPE <> 'I' THEN invd.VAT_AMOUNT
                             ELSE 0
                        END) AS non_integrated_tax ,
                    SUM(CASE WHEN inv.INV_TYPE = 'I' THEN invd.VAT_AMOUNT
                             ELSE 0
                        END) AS integrated_tax ,
                    GSTPaid ,
                    VAT_PER
          FROM      dbo.SALE_INVOICE_MASTER inv
                    INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
                    INNER JOIN dbo.ITEM_MASTER im ON invd.ITEM_ID = im.ITEM_ID
                    INNER JOIN dbo.UNIT_MASTER um ON um.UM_ID = im.UM_ID
                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
                    INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                    INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                    INNER JOIN dbo.HsnCode_Master hsn ON hsn.Pk_HsnId_num = im.fk_HsnId_num
          WHERE     inv.INVOICE_STATUS <> 4
                    AND cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
          " GROUP BY  HsnCode_vch ,
                    GSTPaid ,
                    VAT_PER



          UNION ALL
                      SELECT    HsnCode_vch ,
                                Qty ,
                                Taxable_Value ,
                                Cess_Amount ,
                                CASE WHEN INV_TYPE <> 'I' THEN VAT_AMOUNT
                                     ELSE 0
                                END AS non_integrated_tax ,
                                CASE WHEN INV_TYPE = 'I' THEN VAT_AMOUNT
                                     ELSE 0
                                END AS integrated_tax ,
                                GSTPaid ,
                                VAT_PER
                      FROM      ( SELECT    HsnCode_vch ,
                                            '1' AS Qty ,
                                            SUM(ISNULL(pt.TotalAmountReceived,
                                                       0)) AS Taxable_Value ,
                                            0.00 AS Cess_Amount ,
                                            SUM(ISNULL(pt.GSTPerAmt, 0)) AS VAT_AMOUNT ,
                                            CASE WHEN cm.STATE_ID <> " & stateId & " THEN 'I'
                                                 WHEN cm.STATE_ID =  " & stateId & " THEN 'S'
                                            END AS Inv_Type ,
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
                                            END AS VAT_PER
                                  FROM      dbo.PaymentTransaction AS pt
                                            INNER JOIN dbo.ACCOUNT_MASTER am ON pt.AccountId = am.ACC_ID
                                            INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
                                            INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                                            INNER JOIN dbo.HsnCode_Master hsn ON hsn.Pk_HsnId_num = pt.Fk_HSN_ID
                                  WHERE     CAST(pt.PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
                                            And pt.GST_Applicable_Acc = 'Cr.' and pt.StatusId <> 3
                                  GROUP BY  VAT_NO ,
                                            STATE_CODE ,
                                            STATE_NAME ,
                                            cm.STATE_ID ,
                                            sm.IsUT_Bit ,
                                            hsn.HsnCode_vch,
                                            pt.fk_GST_ID
                                ) subquery


        ) tb GROUP BY HsnCode_vch 

UNION ALL

 SELECT HsnCode_vch ,
        SUM(Qty) * (-1) AS Qty ,
        SUM(Taxable_Value) * (-1) AS Taxable_Value ,
        SUM(Cess_Amount) * (-1) AS Cess_Amount ,
        SUM(non_integrated_tax) * (-1) AS non_integrated_tax ,
        SUM(integrated_tax) * (-1) AS integrated_tax
        FROM   ( SELECT    HsnCode_vch ,
                    SUM(cnd.Item_Qty) AS Qty ,
                    SUM(CAST(( cnd.Item_Qty * cnd.Item_Rate ) AS NUMERIC(18, 2))) AS Taxable_Value,
                    SUM(CAST(((cnd.Item_Qty * cnd.Item_Rate * cnd.Item_Cess)/100) AS DECIMAL(18,2)))  As Cess_Amount ,
                    SUM(CASE WHEN inv.INV_TYPE <> 'I' THEN CAST(((cnd.Item_Qty * cnd.Item_Rate * cnd.Item_Tax)/100) AS DECIMAL(18,2))
                             ELSE 0
                        END) AS non_integrated_tax ,
                    SUM(CASE WHEN inv.INV_TYPE = 'I' THEN CAST(((cnd.Item_Qty * cnd.Item_Rate * cnd.Item_Tax)/100) AS DECIMAL(18,2))
                             ELSE 0
                        END) AS integrated_tax

          FROM   dbo.CreditNote_Master cnm
					INNER JOIN dbo.SALE_INVOICE_MASTER inv ON inv.SI_ID = cnm.INVId
					INNER JOIN dbo.CreditNote_DETAIL cnd ON cnd.CreditNote_Id = cnm.CreditNote_Id
                    INNER JOIN dbo.ITEM_MASTER im ON cnd.ITEM_ID = im.ITEM_ID
                    INNER JOIN dbo.UNIT_MASTER um ON um.UM_ID = im.UM_ID
                    INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = cnm.CN_CustId
					INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
					INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
                    INNER JOIN dbo.HsnCode_Master hsn ON hsn.Pk_HsnId_num = im.fk_HsnId_num
          WHERE     inv.INVOICE_STATUS <> 4
                    AND cast(CreditNote_Date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
                    " GROUP BY  HsnCode_vch

        ) tb GROUP BY HsnCode_vch 
        
        )main GROUP BY HsnCode_vch ORDER BY HsnCode_vch"

        'Qry = " SELECT HsnCode_vch, SUM(invd.BAL_ITEM_QTY) AS Qty, " &
        '    " SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value, SUM(0) Cess_Amount," &
        '    " SUM(CASE WHEN inv.INV_TYPE <> 'I' THEN invd.VAT_AMOUNT ELSE 0 END) AS non_integrated_tax," &
        '    " SUM(CASE WHEN inv.INV_TYPE = 'I' THEN invd.VAT_AMOUNT ELSE 0 END) AS integrated_tax" &
        '    " FROM    dbo.SALE_INVOICE_MASTER inv" &
        '    " INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID" &
        '    " INNER JOIN dbo.ITEM_MASTER im ON invd.ITEM_ID = im.ITEM_ID" &
        '    " INNER JOIN dbo.UNIT_MASTER um ON um.UM_ID = im.UM_ID" &
        '    " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
        '    " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID" &
        '    " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
        '    " INNER JOIN dbo.HsnCode_Master hsn ON hsn.Pk_HsnId_num = im.fk_HsnId_num" &
        '    " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" & txtFromDate.Value.Month.ToString() &
        '    " And YEAR(SI_DATE)=" & txtFromDate.Value.Year.ToString() &
        '    " GROUP BY HsnCode_vch order by HsnCode_vch"

        hsnTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

        Qry = " SELECT  ISNULL(VAT_NO,'') As VAT_NO, ACC_NAME, CreditNote_Code, CreditNote_No, CreditNote_Date, cnm.CN_Amount, SI_CODE, SI_NO," &
        " SI_DATE, STATE_CODE , STATE_NAME , Tax_Amt As Item_Tax, CAST(SUM(( Item_Rate * Item_Qty )) AS DECIMAL(18,2)) AS Taxable_Value , Cess_Amt As Cess_Amount " &
        " FROM    dbo.CreditNote_Master cnm " &
        " INNER JOIN dbo.SALE_INVOICE_MASTER sim ON sim.SI_ID = cnm.INVId " &
        " INNER JOIN dbo.CreditNote_DETAIL cnd ON cnd.CreditNote_Id = cnm.CreditNote_Id " &
        " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = cnm.CN_CustId " &
        " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID " &
        " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID " &
        " WHERE cast(CreditNote_Date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
        " AND LEN(ISNULL(VAT_NO,'')) > 0" &
        " GROUP BY VAT_NO, ACC_NAME, CreditNote_Code, CreditNote_No, CreditNote_Date, cnm.CN_Amount, SI_CODE, SI_NO," &
        " SI_DATE, STATE_CODE, STATE_NAME, Tax_Amt, Cess_Amt ORDER BY cnm.CreditNote_No"

        cdnrTable = objCommFunction.Fill_DataSet(Qry).Tables(0)


        Qry = " SELECT ISNULL(VAT_NO,'') As VAT_NO, ACC_NAME ,CreditNote_Code ,CreditNote_No,CreditNote_Date ,cnm.CN_Amount,SI_CODE, SI_NO," &
        " SI_DATE,STATE_CODE ,STATE_NAME , Tax_Amt As Item_Tax, CAST(SUM(( Item_Rate * Item_Qty )) AS DECIMAL(18,2)) AS Taxable_Value , Cess_Amt As Cess_Amount " &
        " FROM    dbo.CreditNote_Master cnm " &
        " INNER JOIN dbo.SALE_INVOICE_MASTER sim ON sim.SI_ID = cnm.INVId " &
        " INNER JOIN dbo.CreditNote_DETAIL cnd ON cnd.CreditNote_Id = cnm.CreditNote_Id " &
        " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = cnm.CN_CustId " &
        " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID " &
        " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID " &
        " WHERE cast(CreditNote_Date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
        " AND LEN(ISNULL(VAT_NO,''))= 0 and sim.NET_AMOUNT > 250000 and sim.Inv_type='I' " &
         " GROUP BY  VAT_NO, ACC_NAME ,CreditNote_Code ,CreditNote_No,CreditNote_Date ,cnm.CN_Amount,SI_CODE, SI_NO," &
        " SI_DATE,STATE_CODE ,STATE_NAME ,Tax_Amt, Cess_Amt ORDER BY cnm.CreditNote_No"


        cdnurTable = objCommFunction.Fill_DataSet(Qry).Tables(0)


        Qry = "


SELECT [DESCRIPTION] AS [DESCRIPTION] , SUM(Taxable_Value) AS Taxable_Value FROM (
SELECT  'Inter-State supplies to registered persons' AS Description ,
        ISNULL(SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                     - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                   THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                                          * DISCOUNT_VALUE ) / 100
                                   ELSE DISCOUNT_VALUE
                              END, 0) )), 0) AS Taxable_Value
FROM    dbo.SALE_INVOICE_MASTER inv
        INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
        INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
WHERE   inv.INVOICE_STATUS <> 4
        AND cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
        " AND LEN(ISNULL(VAT_NO, '')) > 0
        AND invd.VAT_AMOUNT = 0
        AND inv.Inv_type = 'I'


UNION ALL

SELECT [DESCRIPTION] AS [DESCRIPTION] , Taxable_Value AS Taxable_Value FROM 
(SELECT  'Inter-State supplies to registered persons' AS Description ,
        SUM(ISNULL(pt.TotalAmountReceived, 0)) AS Taxable_Value ,
        CASE WHEN cm.STATE_ID <> " & stateId & " THEN 'I'
             WHEN cm.STATE_ID =  " & stateId & " THEN 'S'
        END AS Inv_Type
FROM    dbo.PaymentTransaction AS pt
        INNER JOIN dbo.ACCOUNT_MASTER am ON pt.AccountId = am.ACC_ID
        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
WHERE   CAST(pt.PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
         And pt.GST_Applicable_Acc = 'Cr.'
         And pt.StatusId <> 3
         And LEN(ISNULL(VAT_NO, '')) > 0
         AND pt.GSTPerAmt = 0
GROUP BY cm.STATE_ID, sm.IsUT_Bit ) tb WHERE Inv_Type = 'I'
) main GROUP BY [DESCRIPTION]


UNION ALL

SELECT [DESCRIPTION] AS [DESCRIPTION] , Sum(Taxable_Value) AS Taxable_Value FROM (
SELECT  'Intra-State supplies to registered persons' AS Description ,
        ISNULL(SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                     - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                   THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                                          * DISCOUNT_VALUE ) / 100
                                   ELSE DISCOUNT_VALUE
                              END, 0) )), 0) AS Taxable_Value
FROM    dbo.SALE_INVOICE_MASTER inv
        INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
        INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
WHERE   inv.INVOICE_STATUS <> 4 
        AND cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
        " AND LEN(ISNULL(VAT_NO, '')) > 0
        AND invd.VAT_AMOUNT = 0
        AND inv.Inv_type <> 'I'

UNION ALL

SELECT [DESCRIPTION] AS [DESCRIPTION] , (Taxable_Value) AS Taxable_Value FROM 
(SELECT  'Intra-State supplies to registered persons' AS Description ,
        SUM(ISNULL(pt.TotalAmountReceived, 0)) AS Taxable_Value ,
        CASE WHEN cm.STATE_ID <> " & stateId & " THEN 'I'
             WHEN cm.STATE_ID =  " & stateId & " THEN 'S'
        END AS Inv_Type
FROM    dbo.PaymentTransaction AS pt
        INNER JOIN dbo.ACCOUNT_MASTER am ON pt.AccountId = am.ACC_ID
        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
WHERE   CAST(pt.PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
         AND pt.GST_Applicable_Acc = 'Cr.'
         AND pt.StatusId <> 3
         AND LEN(ISNULL(VAT_NO, '')) > 0
         AND pt.GSTPerAmt = 0
GROUP BY cm.STATE_ID, sm.IsUT_Bit ) tb WHERE Inv_Type <> 'I'          
) main1 GROUP BY [DESCRIPTION]


UNION ALL



SELECT [DESCRIPTION] AS [DESCRIPTION] , SUM(Taxable_Value) AS Taxable_Value FROM (
SELECT  'Inter-State supplies to unregistered persons' AS Description ,
        ISNULL(SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                     - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                   THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                                          * DISCOUNT_VALUE ) / 100
                                   ELSE DISCOUNT_VALUE
                              END, 0) )), 0) AS Taxable_Value
FROM    dbo.SALE_INVOICE_MASTER inv
        INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
        INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
WHERE   inv.INVOICE_STATUS <> 4
        AND cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
        " AND LEN(ISNULL(VAT_NO, '')) = 0
        AND invd.VAT_AMOUNT = 0
        AND inv.Inv_type = 'I'

UNION ALL

SELECT [DESCRIPTION] AS [DESCRIPTION] , (Taxable_Value) AS Taxable_Value FROM 
(SELECT  'Inter-State supplies to unregistered persons' AS Description ,
        SUM(ISNULL(pt.TotalAmountReceived, 0)) AS Taxable_Value ,
        CASE WHEN cm.STATE_ID <> " & stateId & " THEN 'I'
             WHEN cm.STATE_ID =  " & stateId & " THEN 'S'
        END AS Inv_Type
FROM    dbo.PaymentTransaction AS pt
        INNER JOIN dbo.ACCOUNT_MASTER am ON pt.AccountId = am.ACC_ID
        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
WHERE   CAST(pt.PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
         And pt.GST_Applicable_Acc = 'Cr.'
         And pt.StatusId <> 3
         And LEN(ISNULL(VAT_NO, '')) = 0
         AND pt.GSTPerAmt = 0
GROUP BY cm.STATE_ID, sm.IsUT_Bit ) tb WHERE Inv_Type = 'I'          
) main2 GROUP BY [DESCRIPTION]


UNION ALL

SELECT [DESCRIPTION] AS [DESCRIPTION] , SUM(Taxable_Value) AS Taxable_Value FROM (
SELECT  'Intra-State supplies to unregistered persons' AS Description ,
        ISNULL(SUM(( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                     - ISNULL(CASE WHEN DISCOUNT_TYPE = 'P'
                                   THEN ( ( BAL_ITEM_QTY * BAL_ITEM_RATE )
                                          * DISCOUNT_VALUE ) / 100
                                   ELSE DISCOUNT_VALUE
                              END, 0) )), 0) AS Taxable_Value
FROM    dbo.SALE_INVOICE_MASTER inv
        INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID
        INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID
        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
WHERE   inv.INVOICE_STATUS <> 4
         AND cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
        " AND LEN(ISNULL(VAT_NO, '')) = 0
        AND invd.VAT_AMOUNT = 0
        AND inv.Inv_type <> 'I'

UNION ALL

SELECT [DESCRIPTION] AS [DESCRIPTION] , (Taxable_Value) AS Taxable_Value FROM 
(SELECT  'Intra-State supplies to unregistered persons' AS Description ,
        SUM(ISNULL(pt.TotalAmountReceived, 0)) AS Taxable_Value ,
        CASE WHEN cm.STATE_ID <> " & stateId & " THEN 'I'
             WHEN cm.STATE_ID =  " & stateId & " THEN 'S'
        END AS Inv_Type
FROM    dbo.PaymentTransaction AS pt
        INNER JOIN dbo.ACCOUNT_MASTER am ON pt.AccountId = am.ACC_ID
        INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID
        INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID
WHERE   CAST(pt.PaymentDate AS DATE) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " & "
        And pt.GST_Applicable_Acc = 'Cr.'
        And pt.StatusId <> 3
        And LEN(ISNULL(VAT_NO, '')) = 0
        AND pt.GSTPerAmt = 0
GROUP BY cm.STATE_ID, sm.IsUT_Bit ) tb WHERE Inv_Type <> 'I'
) main3 GROUP BY [DESCRIPTION]"

        exempTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

        Qry = "SELECT 'Invoice for outward supply' AS Description,  Isnull(MIN(SI_NO),0) AS 'Sr. No. From', Isnull(MAX(SI_NO),0) AS 'Sr. No. To', COUNT(SI_No) AS 'Total Number', " &
        "  ( SELECT  COUNT(SI_NO) FROM  dbo.SALE_INVOICE_MASTER WHERE cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
        "  AND INVOICE_STATUS = 4) AS Cancelled FROM  dbo.SALE_INVOICE_MASTER WHERE cast(SI_DATE AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) " &
        "  UNION ALL " &
        "  SELECT 'Credit Note' AS Description, Isnull(MIN(CreditNote_No),0) AS 'Sr. No. From', Isnull(MAX(CreditNote_No),0) AS 'Sr. No. To', COUNT(CreditNote_No) AS 'Total Number', " &
        "  0 AS Cancelled FROM  dbo.CreditNote_Master WHERE cast(CreditNote_Date AS date) between CAST('" & txtFromDate.Value.ToString("dd-MMM-yyyy") & "' AS date) AND CAST('" & txtToDate.Value.ToString("dd-MMM-yyyy") & "' AS date) "

        docsTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

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
