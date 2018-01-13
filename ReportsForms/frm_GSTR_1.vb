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
        
        xlWorkBook.SaveAs(sfd.FileName)
        Finally        
        xlWorkBook.Close(SaveChanges:=False)
        xlApp.Quit()
        releaseObject(xlApp)
        releaseObject(xlWorkBook)	
        End Try               
    End Sub
    
    Private Sub WriteB2BData(xlWorkBook As Excel.Workbook)
    	Dim rowIndex As Int32 = 5
    	Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("b2b")
    	For Each row As DataRow In b2bTable.Rows 
    		Dim invoiceAmount As Decimal =row("VAT_AMOUNT") + row("Taxable_Value")
    		Dim  state As String = row("STATE_CODE") + "-" + row("STATE_NAME")
    		Dim invoiceNo As String = row("SI_CODE") + row("SI_NO").ToString()
    		xlWorkSheet.Cells(rowIndex , 1) = row("VAT_NO")
    		xlWorkSheet.Cells(rowIndex , 2) = invoiceNo
    		xlWorkSheet.Cells(rowIndex , 3) = Convert.ToDateTime(row("SI_DATE")).ToString("dd-MMM-yy")
    		xlWorkSheet.Cells(rowIndex , 4) = invoiceAmount
    		xlWorkSheet.Cells(rowIndex , 5) = state
    		xlWorkSheet.Cells(rowIndex , 6) = "N"
    		xlWorkSheet.Cells(rowIndex , 7) = "Regular"
    		xlWorkSheet.Cells(rowIndex , 8) = ""
    		xlWorkSheet.Cells(rowIndex , 9) = row("VAT_PER")
    		xlWorkSheet.Cells(rowIndex , 10) = row("Taxable_Value")
    		xlWorkSheet.Cells(rowIndex , 11) = row("Cess_Amount")    
    		rowIndex+=1
    	Next
    End Sub
    
    Private Sub WriteB2CLData(xlWorkBook As Excel.Workbook)
    	Dim rowIndex As Int32 = 5
    	Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets("b2cl")
    	For Each row As DataRow In b2clTable.Rows 
    		Dim invoiceAmount As Decimal =row("VAT_AMOUNT") + row("Taxable_Value")    		
    		Dim invoiceNo As String = row("SI_CODE") + row("SI_NO").ToString()
    		Dim  state As String = row("STATE_CODE") + "-" + row("STATE_NAME")
    		xlWorkSheet.Cells(rowIndex , 1) = invoiceNo
    		xlWorkSheet.Cells(rowIndex , 2) = row("SI_DATE")
    		xlWorkSheet.Cells(rowIndex , 3) = invoiceAmount
    		xlWorkSheet.Cells(rowIndex , 4) = state
    		xlWorkSheet.Cells(rowIndex , 5) = row("VAT_PER")
    		xlWorkSheet.Cells(rowIndex , 6) = row("Taxable_Value")
    		xlWorkSheet.Cells(rowIndex , 7) = row("Cess_Amount")
    		xlWorkSheet.Cells(rowIndex , 8) = ""  
    		rowIndex+=1
    	Next
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
	    Dim QryTemplate As String = "SELECT VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,  " &
            " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
            " SUM(0) Cess_Amount, SUM(invd.VAT_AMOUNT) AS VAT_AMOUNT" &
            " FROM    dbo.SALE_INVOICE_MASTER inv" &
            " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
            " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
            " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
            " WHERE MONTH(SI_DATE) =" + txtFromDate.Value.Month.ToString() &
            " And YEAR(SI_DATE)=" + txtFromDate.Value.Year.ToString() + " AND 1=1 " &
            " GROUP BY VAT_NO,SI_CODE, SI_NO, SI_DATE, STATE_CODE,STATE_NAME,VAT_PER"
    	    
        Qry = QryTemplate.Replace ("1=1","LEN(VAT_NO)>0")
        b2bTable = objCommFunction.Fill_DataSet(Qry).Tables(0)
        
        Qry = QryTemplate.Replace ("1=1","LEN(VAT_NO)=0 and inv.NET_AMOUNT>=250000")
        b2clTable = objCommFunction.Fill_DataSet(Qry).Tables(0)
        
        Qry = "SELECT STATE_CODE,STATE_NAME,  " &
            " VAT_PER, SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value," &
            " SUM(0) Cess_Amount" &
            " FROM    dbo.SALE_INVOICE_MASTER inv" &
            " INNER JOIN dbo.SALE_INVOICE_DETAIL invd On invd.SI_ID = inv.SI_ID" &
            " INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
            " INNER JOIN dbo.CITY_MASTER cm On cm.CITY_ID = am.CITY_ID" &
            " INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
            " WHERE MONTH(SI_DATE) =" & txtFromDate.Value.Month.ToString() &
            " And YEAR(SI_DATE)=" & txtFromDate.Value.Year.ToString() &
            " AND LEN(VAT_NO)=0 and inv.NET_AMOUNT<250000 " &
            " GROUP BY STATE_CODE,STATE_NAME,VAT_PER"                
        b2csTable = objCommFunction.Fill_DataSet(Qry).Tables(0)
        
        Qry = " SELECT HsnCode_vch, ITEM_NAME, UM_Name, UM_DESC, SUM(invd.BAL_ITEM_QTY) AS Qty, " &
			" SUM(((BAL_ITEM_QTY * BAL_ITEM_RATE) - ISNULL(ITEM_DISCOUNT,0))) AS Taxable_Value, SUM(0) Cess_Amount," &
			" SUM(CASE WHEN sm.STATE_ID =4 THEN invd.VAT_AMOUNT ELSE 0 END) AS non_integrated_tax," &
			" SUM(CASE WHEN sm.STATE_ID !=4 THEN invd.VAT_AMOUNT ELSE 0 END) AS integrated_tax" &
			" FROM    dbo.SALE_INVOICE_MASTER inv" &
			" INNER JOIN dbo.SALE_INVOICE_DETAIL invd ON invd.SI_ID = inv.SI_ID" &
			" INNER JOIN dbo.ITEM_MASTER im ON invd.ITEM_ID = im.ITEM_ID" &
			" INNER JOIN dbo.UNIT_MASTER um ON um.UM_ID = im.UM_ID" &
			" INNER JOIN dbo.ACCOUNT_MASTER am ON am.ACC_ID = inv.CUST_ID" &
			" INNER JOIN dbo.CITY_MASTER cm ON cm.CITY_ID = am.CITY_ID" &
			" INNER JOIN dbo.STATE_MASTER sm ON sm.STATE_ID = cm.STATE_ID" &
			" INNER JOIN dbo.HsnCode_Master hsn ON hsn.Pk_HsnId_num = im.fk_HsnId_num" &
			" WHERE MONTH(SI_DATE) =9 AND YEAR(SI_DATE)=2017 " &
			" GROUP BY HsnCode_vch,ITEM_NAME, UM_Name,UM_DESC"
        hsnTable = objCommFunction.Fill_DataSet(Qry).Tables(0)
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
