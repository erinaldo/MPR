Imports Microsoft.Office.Interop
Imports MMSPlus

Public Class frm_GSTR_1
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

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        xlApp = New Excel.ApplicationClass
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
        End Try               
    End Sub
    
    Private Sub WriteB2BData(xlWorkBook As Excel.Workbook)
		If b2bTable.Rows .Count=0 Then
			Exit Sub 
		End If
    			
    	Dim rowIndex As Int32 = 5
    	Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("b2b")
    	For Each row As DataRow In b2bTable.Rows     		
    		Dim  state As String = row("STATE_CODE") + "-" + row("STATE_NAME")
    		Dim invoiceNo As String = row("SI_CODE") + row("SI_NO").ToString()
    		xlWorkSheet.Cells(rowIndex , 1) = row("VAT_NO")
    		xlWorkSheet.Cells(rowIndex , 2) = invoiceNo
    		xlWorkSheet.Cells(rowIndex , 3) = Convert.ToDateTime(row("SI_DATE")).ToString("dd-MMM-yy")
    		xlWorkSheet.Cells(rowIndex , 4) = row("NET_AMOUNT")
    		xlWorkSheet.Cells(rowIndex , 5) = state
    		xlWorkSheet.Cells(rowIndex , 6) = "N"
    		xlWorkSheet.Cells(rowIndex , 7) = "Regular"
    		xlWorkSheet.Cells(rowIndex , 8) = ""
    		xlWorkSheet.Cells(rowIndex , 9) = row("VAT_PER")
    		xlWorkSheet.Cells(rowIndex , 10) = row("Taxable_Value")
    		xlWorkSheet.Cells(rowIndex , 11) = row("Cess_Amount")    
    		rowIndex+=1
    	Next
    	
    	''set global values
    	Dim noOfRecipients As Int32 = b2bTable.DefaultView.ToTable(True,"VAT_NO").Rows.Count 
    	Dim noOfInvoices As Int32 = b2bTable.DefaultView.ToTable(True,"SI_CODE", "SI_NO").Rows.Count 
    	Dim sumOfInvoices As Decimal = b2bTable.DefaultView.ToTable(True,"SI_CODE", "SI_NO","NET_AMOUNT").Compute("sum(NET_AMOUNT)", Nothing)
    	
    	xlWorkSheet.Cells(3 , 1) =noOfRecipients
    	xlWorkSheet.Cells(3 , 2) =noOfInvoices
    	xlWorkSheet.Cells(3 , 4) =sumOfInvoices
    End Sub
    
    Private m_colIndex As Integer
    Public Property colIndex As Integer
    	Get
    		m_colIndex+=1
    		Return m_colIndex
    	End Get
    	Set(value As Integer)
    		m_colIndex = value
    	End Set
    End Property
    
    Private Sub WriteB2CLData(xlWorkBook As Excel.Workbook)
		If b2clTable.Rows .Count=0 Then
			Exit Sub 
		End If
    	    	
    	Dim rowIndex As Int32 = 5
    	Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("b2cl")
    	For Each row As DataRow In b2clTable.Rows     		    	
    		Dim invoiceNo As String = row("SI_CODE") + row("SI_NO").ToString()
    		Dim  state As String = row("STATE_CODE") + "-" + row("STATE_NAME")
    		xlWorkSheet.Cells(rowIndex , 1) = invoiceNo
            xlWorkSheet.Cells(rowIndex, 2) = Convert.ToDateTime(row("SI_DATE")).ToString("dd-MMM-yy")
            xlWorkSheet.Cells(rowIndex , 3) = row("NET_AMOUNT")
    		xlWorkSheet.Cells(rowIndex , 4) = state
    		xlWorkSheet.Cells(rowIndex , 5) = row("VAT_PER")
    		xlWorkSheet.Cells(rowIndex , 6) = row("Taxable_Value")
    		xlWorkSheet.Cells(rowIndex , 7) = row("Cess_Amount")
    		xlWorkSheet.Cells(rowIndex , 8) = ""  
    		rowIndex+=1
    	Next
    	
    	''set global values
    	Dim noOfInvoices As Int32 = b2clTable.DefaultView.ToTable(True,"SI_CODE", "SI_NO").Rows.Count 
    	Dim sumOfInvoices As Decimal = b2clTable.DefaultView.ToTable(True,"SI_CODE", "SI_NO","NET_AMOUNT").Compute("sum(NET_AMOUNT)", Nothing)
    	
    	xlWorkSheet.Cells(3 , 1) =noOfInvoices
    	xlWorkSheet.Cells(3 , 3) =sumOfInvoices
    End Sub
    
    Private Sub WriteB2CSData(xlWorkBook As Excel.Workbook)
    	Dim rowIndex As Int32 = 5
    	Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("b2cs")    	
    	For Each row As DataRow In b2csTable.Rows 
    		Dim  state As String = row("STATE_CODE") + "-" + row("STATE_NAME")
    		xlWorkSheet.Cells(rowIndex , 1) = "OE"
    		xlWorkSheet.Cells(rowIndex , 2) = state
    		xlWorkSheet.Cells(rowIndex , 3) = row("VAT_PER")
    		xlWorkSheet.Cells(rowIndex , 4) = row("Taxable_Value")
    		xlWorkSheet.Cells(rowIndex , 5) = row("Cess_Amount")
    		xlWorkSheet.Cells(rowIndex , 6) = ""  
    		rowIndex+=1
    	Next
    End Sub
    
    Private Sub WriteHsnData(xlWorkBook As Excel.Workbook)
    	If hsnTable.Rows .Count=0 Then
    		Exit Sub 
    	End If
    	
    	Dim rowIndex As Int32 = 5
    	Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("hsn")    	
    	For Each row As DataRow In hsnTable.Rows 
    		xlWorkSheet.Cells(rowIndex , 1) = row("HsnCode_vch")
    		xlWorkSheet.Cells(rowIndex , 2) = row("ITEM_NAME")
    		xlWorkSheet.Cells(rowIndex , 3) = row("UM_Name") + "-" + row("UM_DESC")
    		xlWorkSheet.Cells(rowIndex , 4) = row("Qty")
    		xlWorkSheet.Cells(rowIndex , 5) = row("Taxable_Value") + row("Cess_Amount") + row("non_integrated_tax") + row("integrated_tax")
    		xlWorkSheet.Cells(rowIndex , 6) = row("Taxable_Value")
    		xlWorkSheet.Cells(rowIndex , 7) = row("integrated_tax")
    		xlWorkSheet.Cells(rowIndex , 8) = row("non_integrated_tax")/2
    		xlWorkSheet.Cells(rowIndex , 9) = row("non_integrated_tax")/2
    		xlWorkSheet.Cells(rowIndex , 10) = row("Cess_Amount")
    		rowIndex+=1
    	Next
    	
    	''set global values 
    	Dim noOfHSNC As Int32 = hsnTable.DefaultView.ToTable(True,"HsnCode_vch").Rows.Count
    	
    	xlWorkSheet.Cells(3 , 1) =noOfHSNC
    End Sub

    Private Sub WriteCDNRData(xlWorkBook As Excel.Workbook)
        If cdnrTable.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim rowIndex As Int32 = 5
        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("cdnr")
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

    Private Sub WriteCDNURData(xlWorkBook As Excel.Workbook)
        If cdnurTable.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim rowIndex As Int32 = 5
        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("cdnur")
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

    Private Sub WriteEXEMPData(xlWorkBook As Excel.Workbook)
        If exempTable.Rows.Count = 0 Then
            Exit Sub
        End If

        'Dim rowIndex As Int32 = 5
        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("exemp")
        'For Each row As DataRow In exempTable.Rows
        '    colIndex = 1
        '    xlWorkSheet.Cells(rowIndex, colIndex) = "B2CL"
        '    xlWorkSheet.Cells(rowIndex, colIndex) = row("CreditNote_Code") + row("CreditNote_No").ToString()
        '    rowIndex += 1
        'Next

        ''set global values

        Dim dataViewinterStateRVouchers As DataView = exempTable.DefaultView
        dataViewinterStateRVouchers.RowFilter = "Description = 'Inter-State supplies to registered persons'"
        Dim interStateRVouchers As Decimal = dataViewinterStateRVouchers.ToTable(True, "Taxable_Value").Compute("sum(Taxable_Value)", Nothing)

        Dim dataViewintraStateRVouchers As DataView = exempTable.DefaultView
        dataViewintraStateRVouchers.RowFilter = "Description = 'Intra-State supplies to registered persons'"
        Dim intraStateRVouchers As Decimal = dataViewintraStateRVouchers.ToTable(True, "Taxable_Value").Compute("sum(Taxable_Value)", Nothing)

        Dim dataViewinterStateURVouchers As DataView = exempTable.DefaultView
        dataViewinterStateURVouchers.RowFilter = "Description = 'Inter-State supplies to unregistered persons'"
        Dim interStateURVouchers As Decimal = dataViewinterStateURVouchers.ToTable(True, "Taxable_Value").Compute("sum(Taxable_Value)", Nothing)

        Dim dataViewintraStateURVouchers As DataView = exempTable.DefaultView
        dataViewintraStateURVouchers.RowFilter = "Description = 'Intra-State supplies to unregistered persons'"
        Dim intraStateURVouchers As Decimal = dataViewintraStateURVouchers.ToTable(True, "Taxable_Value").Compute("sum(Taxable_Value)", Nothing)

        xlWorkSheet.Cells(5, 2) = interStateRVouchers
        xlWorkSheet.Cells(6, 2) = intraStateRVouchers
        xlWorkSheet.Cells(7, 2) = interStateURVouchers
        xlWorkSheet.Cells(8, 2) = intraStateURVouchers
    End Sub

    Private Sub WriteDocsData(xlWorkBook As Excel.Workbook)
        If docsTable.Rows.Count = 0 Then
            Exit Sub
        End If

        ''set global values
        Dim dataViewOutwardSupply As DataView = docsTable.DefaultView
        dataViewOutwardSupply.RowFilter = "Description = 'Invoice for outward supply'"

        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("docs")

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
    
    Private Sub BindData()
        Dim QryTemplate As String = "SELECT inv.NET_AMOUNT, ISNULL(VAT_NO,'') As VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,  " &
            " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
            " SUM(0) Cess_Amount, SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT" &
            " FROM    dbo.SALE_INVOICE_MASTER inv" &
            " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
            " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
            " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
            " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
            " And YEAR(SI_DATE)=" + txtFromDate.Value.Year.ToString() + " AND invd.VAT_AMOUNT > 0 AND 1=1 " &
            " GROUP BY inv.NET_AMOUNT, VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,VAT_PER"

        Qry = QryTemplate.Replace("1=1", "LEN(ISNULL(VAT_NO,''))>0")
        b2bTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

        Qry = QryTemplate.Replace("1=1", "LEN(ISNULL(VAT_NO,''))=0 and inv.NET_AMOUNT > 250000 and inv.Inv_type='I'")
        b2clTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

        Qry = "SELECT STATE_CODE,STATE_NAME,  " &
            " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
            " SUM(0) Cess_Amount" &
            " FROM    dbo.SALE_INVOICE_MASTER inv" &
            " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
            " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
            " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
            " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" & txtFromDate.Value.Month.ToString() &
            " And YEAR(SI_DATE)=" & txtFromDate.Value.Year.ToString() &
            " AND LEN(ISNULL(VAT_NO,''))=0 and inv.NET_AMOUNT<=250000 and inv.Inv_type='I' AND invd.VAT_AMOUNT > 0 " &
            " GROUP BY STATE_CODE,STATE_NAME,VAT_PER" &
            " Union All " &
            " SELECT STATE_CODE,STATE_NAME,  " &
            " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
            " SUM(0) Cess_Amount" &
            " FROM    dbo.SALE_INVOICE_MASTER inv" &
            " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
            " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
            " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
            " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" & txtFromDate.Value.Month.ToString() &
            " And YEAR(SI_DATE)=" & txtFromDate.Value.Year.ToString() &
            " AND LEN(ISNULL(VAT_NO,''))=0 and inv.Inv_type <> 'I' AND invd.VAT_AMOUNT > 0 " &
            " GROUP BY STATE_CODE,STATE_NAME,VAT_PER"
        b2csTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

        Qry = " SELECT HsnCode_vch, ITEM_NAME, UM_Name, UM_DESC, SUM(invd.BAL_ITEM_QTY) AS Qty, " &
            " SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value, SUM(0) Cess_Amount," &
            " SUM(CASE WHEN inv.INV_TYPE <> 'I' THEN invd.VAT_AMOUNT ELSE 0 END) AS non_integrated_tax," &
            " SUM(CASE WHEN inv.INV_TYPE = 'I' THEN invd.VAT_AMOUNT ELSE 0 END) AS integrated_tax" &
            " FROM    dbo.SALE_INVOICE_MASTER inv" &
            " INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID" &
            " INNER JOIN dbo.ITEM_MASTER im ON invd.ITEM_ID = im.ITEM_ID" &
            " INNER JOIN dbo.UNIT_MASTER um ON um.UM_ID = im.UM_ID" &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
            " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID" &
            " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
            " INNER JOIN dbo.HsnCode_Master hsn ON hsn.Pk_HsnId_num = im.fk_HsnId_num" &
            " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" & txtFromDate.Value.Month.ToString() &
            " And YEAR(SI_DATE)=" & txtFromDate.Value.Year.ToString() &
            " GROUP BY HsnCode_vch,ITEM_NAME, UM_Name,UM_DESC"
        hsnTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

        Qry = " SELECT  ISNULL(VAT_NO,'') As VAT_NO, ACC_NAME ,CreditNote_Code ,CreditNote_No,CreditNote_Date ,cnm.CN_Amount,SI_CODE, SI_NO," &
        " SI_DATE,STATE_CODE ,STATE_NAME ,Item_Tax ,SUM(( Item_Rate * Item_Qty )) AS Taxable_Value ,SUM(0) Cess_Amount " &
        " FROM    dbo.CreditNote_Master cnm " &
        " INNER JOIN dbo.SALE_INVOICE_MASTER sim ON sim.SI_ID = cnm.INVId " &
        " INNER JOIN dbo.CreditNote_DETAIL cnd ON cnd.CreditNote_Id = cnm.CreditNote_Id " &
        " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = cnm.CN_CustId " &
        " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID " &
        " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID " &
        " WHERE MONTH(CreditNote_Date) =" & txtFromDate.Value.Month.ToString() &
        " And YEAR(CreditNote_Date)=" & txtFromDate.Value.Year.ToString() &
        " AND LEN(ISNULL(VAT_NO,'')) > 0" &
         " GROUP BY  VAT_NO, ACC_NAME ,CreditNote_Code ,CreditNote_No,CreditNote_Date ,cnm.CN_Amount,SI_CODE, SI_NO," &
        " SI_DATE,STATE_CODE ,STATE_NAME ,Item_Tax   ORDER BY cnm.CreditNote_No"
        cdnrTable = objCommFunction.Fill_DataSet(Qry).Tables(0)


        Qry = " SELECT ISNULL(VAT_NO,'') As VAT_NO, ACC_NAME ,CreditNote_Code ,CreditNote_No,CreditNote_Date ,cnm.CN_Amount,SI_CODE, SI_NO," &
        " SI_DATE,STATE_CODE ,STATE_NAME ,Item_Tax ,SUM(( Item_Rate * Item_Qty )) AS Taxable_Value ,SUM(0) Cess_Amount " &
        " FROM    dbo.CreditNote_Master cnm " &
        " INNER JOIN dbo.SALE_INVOICE_MASTER sim ON sim.SI_ID = cnm.INVId " &
        " INNER JOIN dbo.CreditNote_DETAIL cnd ON cnd.CreditNote_Id = cnm.CreditNote_Id " &
        " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = cnm.CN_CustId " &
        " INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID " &
        " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID " &
        " WHERE MONTH(CreditNote_Date) =" & txtFromDate.Value.Month.ToString() &
        " And YEAR(CreditNote_Date)=" & txtFromDate.Value.Year.ToString() &
        " AND LEN(ISNULL(VAT_NO,''))= 0 and sim.NET_AMOUNT > 250000 and sim.Inv_type='I' " &
         " GROUP BY  VAT_NO, ACC_NAME ,CreditNote_Code ,CreditNote_No,CreditNote_Date ,cnm.CN_Amount,SI_CODE, SI_NO," &
        " SI_DATE,STATE_CODE ,STATE_NAME ,Item_Tax  ORDER BY cnm.CreditNote_No"
        cdnurTable = objCommFunction.Fill_DataSet(Qry).Tables(0)


        Qry = "SELECT 'Inter-State supplies to registered persons' AS Description, inv.NET_AMOUNT, ISNULL(VAT_NO,'') As VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,  " &
            " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
            " SUM(0) Cess_Amount, SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT" &
            " FROM    dbo.SALE_INVOICE_MASTER inv" &
            " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
            " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
            " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
            " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
            " And YEAR(SI_DATE)=" + txtFromDate.Value.Year.ToString() + " AND LEN(ISNULL(VAT_NO,'')) > 0 AND invd.VAT_AMOUNT = 0 AND inv.Inv_type = 'I' " &
            " GROUP BY inv.NET_AMOUNT, VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,VAT_PER " &
            " Union All " &
            "SELECT 'Intra-State supplies to registered persons' AS Description, inv.NET_AMOUNT, ISNULL(VAT_NO,'') As VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,  " &
            " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
            " SUM(0) Cess_Amount, SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT" &
            " FROM    dbo.SALE_INVOICE_MASTER inv" &
            " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
            " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
            " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
            " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
            " And YEAR(SI_DATE)=" + txtFromDate.Value.Year.ToString() + " AND LEN(ISNULL(VAT_NO,'')) > 0 AND invd.VAT_AMOUNT = 0 AND inv.Inv_type <> 'I' " &
            " GROUP BY inv.NET_AMOUNT, VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,VAT_PER " &
            " Union All " &
            "SELECT 'Inter-State supplies to unregistered persons' AS Description, inv.NET_AMOUNT, ISNULL(VAT_NO,'') As VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,  " &
            " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
            " SUM(0) Cess_Amount, SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT" &
            " FROM    dbo.SALE_INVOICE_MASTER inv" &
            " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
            " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
            " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
            " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
            " And YEAR(SI_DATE)=" + txtFromDate.Value.Year.ToString() + " AND LEN(ISNULL(VAT_NO,'')) = 0 AND invd.VAT_AMOUNT = 0 AND inv.Inv_type = 'I' " &
            " GROUP BY inv.NET_AMOUNT, VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,VAT_PER " &
            " Union All " &
            "SELECT 'Intra-State supplies to unregistered persons' AS Description, inv.NET_AMOUNT, ISNULL(VAT_NO,'') As VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,  " &
            " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
            " SUM(0) Cess_Amount, SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT" &
            " FROM    dbo.SALE_INVOICE_MASTER inv" &
            " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
            " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
            " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
            " WHERE inv.INVOICE_STATUS <> 4 And MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
            " And YEAR(SI_DATE)=" + txtFromDate.Value.Year.ToString() + " AND LEN(ISNULL(VAT_NO,'')) = 0 AND invd.VAT_AMOUNT = 0 AND inv.Inv_type <> 'I' " &
            " GROUP BY inv.NET_AMOUNT, VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,VAT_PER "

        exempTable = objCommFunction.Fill_DataSet(Qry).Tables(0)

        Qry = "SELECT 'Invoice for outward supply' AS Description,  Isnull(MIN(SI_NO),0) AS 'Sr. No. From', Isnull(MAX(SI_NO),0) AS 'Sr. No. To', COUNT(SI_No) AS 'Total Number', " &
        "  ( SELECT  COUNT(SI_NO) FROM  dbo.SALE_INVOICE_MASTER WHERE MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
        "   AND YEAR(SI_DATE) = " + txtFromDate.Value.Year.ToString() &
        "  AND INVOICE_STATUS = 4) AS Cancelled FROM  dbo.SALE_INVOICE_MASTER WHERE MONTH(SI_DATE) = " + txtFromDate.Value.Month.ToString() &
        "  AND YEAR(SI_DATE) = " + txtFromDate.Value.Year.ToString() &
        "  UNION ALL " &
        "  SELECT 'Credit Note' AS Description, Isnull(MIN(CreditNote_No),0) AS 'Sr. No. From', Isnull(MAX(CreditNote_No),0) AS 'Sr. No. To', COUNT(CreditNote_No) AS 'Total Number', " &
        "  0 AS Cancelled FROM  dbo.CreditNote_Master WHERE MONTH(CreditNote_Date) = " + txtFromDate.Value.Month.ToString() &
        "  AND YEAR(CreditNote_Date) = " + txtFromDate.Value.Year.ToString()

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
