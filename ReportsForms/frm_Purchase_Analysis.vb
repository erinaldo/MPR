Imports Microsoft.Office.Interop
Imports MMSPlus
Imports System.Linq
Imports System.Data.DataSetExtensions
Public Class frm_Purchase_Analysis
    Implements IForm
    Dim objCommFunction As New CommonClass

    Dim Query As String
    Dim _rights As Form_Rights
    Dim dtBrand As DataTable
    Dim dtColor As DataTable
    Dim dtSize As DataTable
    Dim dtCompany As DataTable
    Dim dtDepartment As DataTable
    Dim dtType As DataTable
    Dim dtCategory As DataTable
    Dim dt As DataTable

    Dim allBrandFlag As Boolean
    Dim BrandIds As String = ""
    Dim allColorFlag As Boolean
    Dim ColorIds As String = ""
    Dim allSizeFlag As Boolean
    Dim SizeIds As String = ""
    Dim allCompanyFlag As Boolean
    Dim CompanyIds As String = ""
    Dim allDepartmentFlag As Boolean
    Dim DepartmentIds As String = ""
    Dim allTypeFlag As Boolean
    Dim TypeIds As String = ""
    Dim allCategoryFlag As Boolean
    Dim CategoryIds As String = ""
    Public Sub New(ByVal rights As Form_Rights)
        _rights = rights
        InitializeComponent()
    End Sub
    Dim itemId As Integer = 0
    Private Sub txtSearchedItem_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearchedItem.KeyDown

        If e.KeyCode = Keys.Enter Then
            Dim dr() As DataRow = itemTable.Select("Name = '" & txtSearchedItem.Text.Replace("'", "''") & "'")
            If dr.Length > 0 Then
                itemId = dr(0)("Id")
                'SendKeys.Send("{TAB}")
            End If
        End If
    End Sub

    Dim AccountId As Integer = 0
    Private Sub txtBoxCustomer_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBoxCustomer.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim dr() As DataRow = AccountTable.Select("Name = '" & txtBoxCustomer.Text.Replace("'", "''") & "'")
            If dr.Length > 0 Then
                AccountId = dr(0)("ACC_ID")
                'SendKeys.Send("{TAB}")
            End If
        End If
    End Sub
    Private Sub BindData()

        Dim Filter As String = ""
        Cursor.Current = Cursors.WaitCursor

        Dim FROMDATE As String = dtpFromDate.Value.ToString("dd-MMM-yyyy")
        Dim TODATE As String = dtpToDate.Value.ToString("dd-MMM-yyyy")

        Query = "SELECT ROW_NUMBER() OVER (ORDER BY [Received_ID]) as SrNo,convert(varchar(20), Received_Date,106) as [Rec. Date], Invoice_No as [Bill No.],convert(varchar(20), Invoice_Date,106) as [Bill Date], BillAmount as [Bill Amount], ACC_NAME as [Account Name], ADDRESS_PRIM as [Address], VAT_NO as [GST No.], Mobile_no as [Mobile No.], BrandName as [Brand], CategoryName_vch as [Category], ITEM_NAME as [Item], BarCode_vch as [BAR Code], HsnCode_vch as [HSN Code],Cast(MRP_Num as Numeric(18,2)) as [MRP], Cast(BasePrice as Numeric(18,2)) AS [Base Price], Cast(Item_Qty as Numeric(18,2)) as [Quantity] ,um_name as [UOM], Cast(LandingRate as Numeric(18,2)) as [Purc. Rate], Cast(GrossAmount as Numeric(18,2)) AS [Taxable Value], VAT_PERCENTAGE as [GST%], Cast(GST_Amt as Numeric(18,2)) as [Tax], CessPercentage_num as [CESS%], Cast(Cess_Amount as Numeric(18,2)) AS [CESS], Cast(ACess_Amount as Numeric(18,2)) AS [ACESS],Cast(FreightValue as Numeric(18,2)) AS [Freight],Cast(FreightTax as Numeric(18,2)) AS [Fret.Tax], Cast(ItemTotalAmount as Numeric(18,2)) as [Total], SizeName as [Size], ColorName as [Color],CompanyName as [Make], DepartmentName as [Department], TypeName as [Type]  FROM Purchase_Analysis WHERE CAST(Received_Date AS DATE)BETWEEN CAST('" & FROMDATE & "' AS DATE) AND CAST('" & TODATE & "' AS DATE)"

        If BrandIds <> "" Then
            BrandIds = BrandIds.Substring(0, BrandIds.Length() - 1)
            If BrandIds.Length > 0 Then
                Filter = Filter & " AND BrandId IN(" & BrandIds & ") "
            End If
            BrandIds = BrandIds + ","
        End If

        If ColorIds <> "" Then
            ColorIds = ColorIds.Substring(0, ColorIds.Length() - 1)
            If ColorIds.Length > 0 Then
                Filter = Filter & " AND ColorId IN(" & ColorIds & ") "
            End If
            ColorIds = ColorIds + ","
        End If

        If SizeIds <> "" Then
            SizeIds = SizeIds.Substring(0, SizeIds.Length() - 1)
            If SizeIds.Length > 0 Then
                Filter = Filter & " AND SizeId IN(" & SizeIds & ") "
            End If
            SizeIds = SizeIds + ","
        End If

        If CompanyIds <> "" Then
            CompanyIds = CompanyIds.Substring(0, CompanyIds.Length() - 1)
            If CompanyIds.Length > 0 Then
                Filter = Filter & " AND CompanyId IN(" & CompanyIds & ") "
            End If
            CompanyIds = CompanyIds + ","
        End If

        If DepartmentIds <> "" Then
            DepartmentIds = DepartmentIds.Substring(0, DepartmentIds.Length() - 1)
            If DepartmentIds.Length > 0 Then
                Filter = Filter & " AND DepartmentId IN(" & DepartmentIds & ") "
            End If
            DepartmentIds = DepartmentIds + ","
        End If

        If TypeIds <> "" Then
            TypeIds = TypeIds.Substring(0, TypeIds.Length() - 1)
            If TypeIds.Length > 0 Then
                Filter = Filter & " AND TypeId IN(" & TypeIds & ") "
            End If
            TypeIds = TypeIds + ","
        End If

        If CategoryIds <> "" Then
            CategoryIds = CategoryIds.Substring(0, CategoryIds.Length() - 1)
            If CategoryIds.Length > 0 Then
                Filter = Filter & " AND pk_CategoryID_num IN(" & CategoryIds & ") "
            End If
            CategoryIds = CategoryIds + ","
        End If

        If Not String.IsNullOrEmpty(txtSearchedItem.Text) Then
            If itemId <> 0 Then
                Filter = Filter & " AND ITEM_ID IN(" & itemId & ") "
            End If
        End If


        If Not String.IsNullOrEmpty(txtMrpFrom.Text) Then
            Filter = Filter & " AND MRP_num >=" & txtMrpFrom.Text
        End If

        If Not String.IsNullOrEmpty(txtMrpTo.Text) Then
            Filter = Filter & " AND MRP_num <=" & txtMrpTo.Text
        End If


        If Not String.IsNullOrEmpty(txtSaleRateFrom.Text) Then
            Filter = Filter & " AND LandingRate >=" & txtSaleRateFrom.Text
        End If

        If Not String.IsNullOrEmpty(txtSaleRateTo.Text) Then
            Filter = Filter & " AND LandingRate <=" & txtSaleRateTo.Text
        End If


        If Not String.IsNullOrEmpty(txtBasePriceFrom.Text) Then
            Filter = Filter & " AND BasePrice >=" & txtBasePriceFrom.Text
        End If

        If Not String.IsNullOrEmpty(txtBasePriceTo.Text) Then
            Filter = Filter & " AND BasePrice <=" & txtBasePriceTo.Text
        End If

        If Not String.IsNullOrEmpty(txtHSNcode.Text) Then
            Filter = Filter & " AND HsnCode_vch in ( '" & txtHSNcode.Text & "' )"
        End If

        If cmbGST.SelectedIndex > 0 Then
            Filter = Filter & " AND vat_id in ( " & cmbGST.SelectedValue & " )"
        End If

        If cmbCESS.SelectedIndex > 0 Then
            Filter = Filter & " AND fk_CessId_num in ( " & cmbCESS.SelectedValue & " )"
        End If

        If Not String.IsNullOrEmpty(txtBoxCustomer.Text) Then
            If AccountId <> 0 Then
                Filter = Filter & " AND ACC_ID IN(" & AccountId & ") "
            End If
        End If



        If Not String.IsNullOrEmpty(txtBillNo.Text) Then
            Filter = Filter & " AND Invoice_No = '" & txtBillNo.Text & "'"
        End If

        If Not String.IsNullOrEmpty(txtBillAmt.Text) Then
            Filter = Filter & " AND BillAmount = " & txtBillAmt.Text
        End If

        Query = Query & Filter

        Query = Query & " Order By Received_ID"

        If rbItemWise.Checked = True Then
            Query = " SELECT  ROW_NUMBER() OVER ( ORDER BY [ITEM_NAME] ) AS SrNo ,
        ITEM_NAME AS [Item] ,
        um_name AS [UOM] ,
        CAST(PurchaseQty AS NUMERIC(18, 2)) AS [Quantity] ,
        CAST(GrossAmount AS NUMERIC(18, 2)) AS [Gross Amount] ,
        CAST(( GrossAmount / PurchaseQty ) AS NUMERIC(18, 2)) AS [AvgPrice] ,
        CAST(GstAmount AS NUMERIC(18, 2)) AS [Tax] ,
        CAST(CessAmount AS NUMERIC(18, 2)) AS [CESS] ,
        CAST(ACessAmount AS NUMERIC(18, 2)) AS [ACESS] ,
        CAST(Freight AS NUMERIC(18, 2)) AS [Freight] ,
        CAST(FreightTax AS NUMERIC(18, 2)) AS [Fret.Tax] ,
        CAST(TotalBill AS NUMERIC(18, 0)) AS [Total Bills]
        FROM    ( SELECT    ITEM_NAME ,
                    ITEM_ID ,
                    um_name ,
                    ISNULL(SUM(Item_Qty), 0) AS PurchaseQty ,
                    ISNULL(SUM(GrossAmount), 0) AS GrossAmount ,
                    ISNULL(SUM(GST_Amt), 0) AS GstAmount ,
                    ISNULL(SUM(Cess_Amount), 0) AS CessAmount ,
                    ISNULL(SUM(ACess_Amount), 0) AS ACessAmount ,
                    ISNULL(SUM(FreightValue), 0) AS Freight ,
                    ISNULL(SUM(FreightTax), 0) AS FreightTax ,
                    COUNT(Distinct Invoice_No) AS TotalBill
           FROM      dbo.Purchase_Analysis
           WHERE     CAST(Received_Date AS DATE) BETWEEN CAST('" & FROMDATE & "' AS DATE)
                                               AND     CAST('" & TODATE & "' AS DATE)" & Filter &
          " GROUP BY  ITEM_ID ,
                    ITEM_NAME ,
                    um_name
        ) tb"

        End If

        If rbAccWise.Checked = True Then
            Query = "SELECT  ROW_NUMBER() OVER ( ORDER BY [ACC_NAME] ) AS SrNo ,
        ACC_NAME AS [Account Name] ,
        VAT_NO AS [GST No.] ,
        'Units' AS [Units] ,
        CAST(PurchaseQty AS NUMERIC(18, 2)) AS [Quantity] ,
        CAST(GrossAmount AS NUMERIC(18, 2)) AS [Gross Amount] ,
        CAST(( GrossAmount / PurchaseQty ) AS NUMERIC(18, 2)) AS [AvgPrice] ,
        CAST(GstAmount AS NUMERIC(18, 2)) AS [Tax] ,
        CAST(CessAmount AS NUMERIC(18, 2)) AS [CESS] ,
        CAST(ACessAmount AS NUMERIC(18, 2)) AS [ACESS] ,
        CAST(Freight AS NUMERIC(18, 2)) AS [Freight] ,
        CAST(FreightTax AS NUMERIC(18, 2)) AS [Fret.Tax] ,
        CAST(TotalBill AS NUMERIC(18, 0)) AS [Total Bills]
        FROM    ( SELECT    ACC_NAME ,
                    ISNULL(VAT_NO, '') AS VAT_NO ,
                    ISNULL(SUM(Item_Qty), 0) AS PurchaseQty ,
                    ISNULL(SUM(GrossAmount), 0) AS GrossAmount ,
                    ISNULL(SUM(GST_Amt), 0) AS GstAmount ,
                    ISNULL(SUM(Cess_Amount), 0) AS CessAmount ,
                    ISNULL(SUM(ACess_Amount), 0) AS ACessAmount ,
                    ISNULL(SUM(FreightValue), 0) AS Freight ,
                    ISNULL(SUM(FreightTax), 0) AS FreightTax ,
                    COUNT(Distinct Invoice_No) AS TotalBill
           FROM      dbo.Purchase_Analysis
           WHERE     CAST(Received_Date AS DATE) BETWEEN CAST('" & FROMDATE & "' AS DATE)
                                               AND     CAST('" & TODATE & "' AS DATE)" & Filter &
          " Group BY  ACC_NAME ,
                    VAT_NO
        ) tb"

        End If

        dt = objCommFunction.FillDataSet(Query).Tables(0)

        dgvReport.Columns.Clear()

        If rbDall.Checked = True Then
            dgvReport.DataSource = dt

            dgvReport.Columns(0).Width = 50
            dgvReport.Columns(1).Width = 80
            dgvReport.Columns(2).Width = 100
            dgvReport.Columns(3).Width = 80
            dgvReport.Columns(4).Width = 100
            dgvReport.Columns(5).Width = 280
            dgvReport.Columns(7).Width = 115
            dgvReport.Columns(8).Width = 90
            dgvReport.Columns(9).Width = 180
            dgvReport.Columns(10).Width = 180
            dgvReport.Columns(11).Width = 280
            dgvReport.Columns(12).Width = 100
            dgvReport.Columns(13).Width = 80
            dgvReport.Columns(14).Width = 80
            dgvReport.Columns(15).Width = 80
            dgvReport.Columns(16).Width = 80
            dgvReport.Columns(17).Width = 40
            dgvReport.Columns(18).Width = 80
            dgvReport.Columns(19).Width = 80
            dgvReport.Columns(20).Width = 50
            dgvReport.Columns(21).Width = 80
            dgvReport.Columns(22).Width = 50
            dgvReport.Columns(23).Width = 100
        Else

            Dim TotalQty As Decimal = dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Quantity")))
            Dim TotalGrossAmount As Decimal = dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Gross Amount")))
            Dim TotalTax As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Tax"))))
            Dim TotalCess As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("CESS"))))
            Dim TotalACess As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("ACESS"))))
            Dim TotalFreight As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Freight"))))
            Dim TotalFreightTax As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Fret.Tax"))))
            Dim TotalBills As Decimal = Math.Abs(dt.AsEnumerable().Sum(Function(x) Convert.ToDecimal(x.Field(Of Decimal)("Total Bills"))))

            If rbAccWise.Checked = True Then

                Dim footerRow As DataRow
                footerRow = dt.NewRow
                footerRow(1) = "Total"
                footerRow(4) = TotalQty
                footerRow(5) = TotalGrossAmount
                footerRow(7) = TotalTax
                footerRow(8) = TotalCess
                footerRow(9) = TotalACess
                footerRow(10) = TotalFreight
                footerRow(11) = TotalFreightTax
                footerRow(12) = TotalBills
                dt.Rows.Add(footerRow)

                'Dim rcount = dt.Rows.Count
                'Dim rw As DataRow
                'Dim i = 0
                'While (i <= 26 - rcount)
                '    rw = dt.NewRow
                '    dt.Rows.Add(rw)
                '    i = i + 1
                'End While

                dgvReport.DataSource = dt

                dgvReport.Columns(0).Width = 40
                dgvReport.Columns(1).Width = 200
                dgvReport.Columns(2).Width = 120
                dgvReport.Columns(3).Width = 35
                dgvReport.Columns(4).Width = 60
                dgvReport.Columns(5).Width = 95
                dgvReport.Columns(6).Width = 65
                dgvReport.Columns(8).Width = 65
                dgvReport.Columns(7).Width = 65
                dgvReport.Columns(8).Width = 65
                dgvReport.Columns(9).Width = 65
                dgvReport.Columns(10).Width = 65
                dgvReport.Columns(11).Width = 55
                dgvReport.Columns(12).Width = 65

                dgvReport.Rows(dgvReport.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Lime
                dgvReport.Rows(dgvReport.Rows.Count - 1).DefaultCellStyle.Font = New Font("Tahoma", 8, FontStyle.Bold)

            End If

            If rbItemWise.Checked = True Then

                Dim footerRow As DataRow
                footerRow = dt.NewRow
                footerRow(1) = "Total"
                footerRow(3) = TotalQty
                footerRow(4) = TotalGrossAmount
                footerRow(6) = TotalTax
                footerRow(7) = TotalCess
                footerRow(8) = TotalACess
                footerRow(9) = TotalFreight
                footerRow(10) = TotalFreightTax
                footerRow(11) = TotalBills
                dt.Rows.Add(footerRow)

                'Dim rcount = dt.Rows.Count
                'Dim rw As DataRow
                'Dim i = 0
                'While (i <= 26 - rcount)
                '    rw = dt.NewRow
                '    dt.Rows.Add(rw)
                '    i = i + 1
                'End While

                dgvReport.DataSource = dt

                dgvReport.Columns(0).Width = 40
                dgvReport.Columns(1).Width = 200
                dgvReport.Columns(2).Width = 35
                dgvReport.Columns(3).Width = 60
                dgvReport.Columns(4).Width = 95
                dgvReport.Columns(5).Width = 65
                dgvReport.Columns(6).Width = 65
                dgvReport.Columns(8).Width = 65
                dgvReport.Columns(7).Width = 65
                dgvReport.Columns(8).Width = 65
                dgvReport.Columns(9).Width = 60
                dgvReport.Columns(10).Width = 55
                dgvReport.Columns(11).Width = 65

                dgvReport.Rows(dgvReport.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Lime
                dgvReport.Rows(dgvReport.Rows.Count - 1).DefaultCellStyle.Font = New Font("Tahoma", 8, FontStyle.Bold)

            End If

        End If

        pnlShow.Visible = True
        pnlShow.BringToFront()

        Cursor.Current = Cursors.Default


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

    Private Sub frm_Sale_Analysis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'pdsTaxReportDates.FromDateDTP = dtpFromDate
        'pdsTaxReportDates.ToDateDTP = dtpToDate
        FillBrand()
        FillColor()
        FillSize()
        FillCompany()
        FillDepartment()
        FillType()
        FillCategory()
        BindGST()
        BindCESS()
        BindItemAutoCompleteTextBox()
        BindAccountAutoCompleteTextBox()
        dgvReport.Columns.Clear()
    End Sub


    Private Sub FillBrand()
        dtBrand = objCommFunction.FillDataSet("SELECT Pk_LabelDetailId_Num As BrandId,LabelItemName_vch As Brand FROM dbo.Label_Items WHERE fk_LabelId_num=1" &
                    " order by LabelItemName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtBrand.Rows.Count > 0 Then
            ChkdLB_Brand.DataSource = dtBrand
            ChkdLB_Brand.DisplayMember = "Brand"
            ChkdLB_Brand.ValueMember = "BrandId"
        End If
    End Sub

    Private Sub FillCategory()
        dtCategory = objCommFunction.FillDataSet("SELECT pk_CategoryID_num As CategoryId,CategoryName_vch As Category FROM dbo.CategoryMaster" &
                    " order by CategoryName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtCategory.Rows.Count > 0 Then
            ChkdLB_Category.DataSource = dtCategory
            ChkdLB_Category.DisplayMember = "Category"
            ChkdLB_Category.ValueMember = "CategoryId"
        End If
    End Sub

    Private Sub BindGST()

        Query = "SELECT VAT_ID As GSTId,VAT_NAME As GST FROM dbo.VAT_MASTER order by VAT_NAME"
        Dim dtTable As DataTable = objCommFunction.FillDataSet(Query).Tables(0)
        dtTable.Rows.InsertAt(dtTable.NewRow, 0)
        dtTable.Rows(0)(0) = 0
        dtTable.Rows(0)(1) = "--All--"

        cmbGST.ValueMember = "GSTId"
        cmbGST.DisplayMember = "GST"
        cmbGST.DataSource = dtTable
        cmbGST.SelectedIndex = 0

    End Sub

    Private Sub BindCESS()

        Query = "SELECT pk_CessId_num As CESSId,CessName_vch As CESS FROM dbo.CessMaster order by CessName_vch"
        Dim dtTable As DataTable = objCommFunction.FillDataSet(Query).Tables(0)
        dtTable.Rows.InsertAt(dtTable.NewRow, 0)
        dtTable.Rows(0)(0) = 0
        dtTable.Rows(0)(1) = "--All--"

        cmbCESS.ValueMember = "CESSId"
        cmbCESS.DisplayMember = "CESS"
        cmbCESS.DataSource = dtTable
        cmbCESS.SelectedIndex = 0

    End Sub

    Dim itemTable As DataTable = Nothing
    Private Sub BindItemAutoCompleteTextBox()
        Query = " select ITEM_CODE + ' - ' + ITEM_NAME + ' MRP - ' + cast(MRP_num as varchar(20)) + ' - ' + cast(BasePrice_num as varchar(20)) + ' - ' + BarCode_vch  as name, ITEM_ID as Id, 'MM' as Type from ITEM_MASTER ORDER BY ITEM_NAME "
        itemTable = objCommFunction.FillDataSet(Query).Tables(0)
        Try

            Dim MyCollection As AutoCompleteStringCollection = New AutoCompleteStringCollection()

            Dim source As New List(Of String)
            For Each row As DataRow In itemTable.Rows
                source.Add(row("Name"))
                MyCollection.Add(row("Name"))
            Next
            txtSearchedItem.AutoCompleteList = source
            txtMrpFrom.AutoCompleteCustomSource = MyCollection

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Dim AccountTable As DataTable = Nothing
    Private Sub BindAccountAutoCompleteTextBox()
        Query = " SELECT ISNULL(ACC_NAME,'')+ ' -  GST NO. -  ' + ISNULL(VAT_NO,'')+' - ADD. -  '+ ISNULL(ADDRESS_PRIM,'')+ ' ' +ISNULL(ADDRESS_SEC,'')+' - MOB. - '+ ISNULL(MOBILE_NO,'')   AS Name,ACC_ID,'AA' AS Type FROM dbo.ACCOUNT_MASTER"

        AccountTable = objCommFunction.FillDataSet(Query).Tables(0)
        Try
            Dim source As New List(Of String)
            For Each row As DataRow In AccountTable.Rows
                source.Add(row("Name"))
            Next
            txtBoxCustomer.AutoCompleteList = source
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub FillColor()
        dtColor = objCommFunction.FillDataSet("SELECT Pk_LabelDetailId_Num As ColorId,LabelItemName_vch As Color FROM dbo.Label_Items WHERE fk_LabelId_num=3" &
                    " order by LabelItemName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtColor.Rows.Count > 0 Then
            ChkdLB_Color.DataSource = dtColor
            ChkdLB_Color.DisplayMember = "Color"
            ChkdLB_Color.ValueMember = "ColorId"
        End If
    End Sub


    Private Sub FillSize()
        dtSize = objCommFunction.FillDataSet("SELECT Pk_LabelDetailId_Num As SizeId,LabelItemName_vch As Size FROM dbo.Label_Items WHERE fk_LabelId_num=2" &
                    " order by LabelItemName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtSize.Rows.Count > 0 Then
            ChkdLB_Size.DataSource = dtSize
            ChkdLB_Size.DisplayMember = "Size"
            ChkdLB_Size.ValueMember = "SizeId"
        End If
    End Sub

    Private Sub FillCompany()
        dtCompany = objCommFunction.FillDataSet("SELECT Pk_LabelDetailId_Num As CompanyId,LabelItemName_vch As Company FROM dbo.Label_Items WHERE fk_LabelId_num=4" &
                    " order by LabelItemName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtCompany.Rows.Count > 0 Then
            ChkdLB_Company.DataSource = dtCompany
            ChkdLB_Company.DisplayMember = "Company"
            ChkdLB_Company.ValueMember = "CompanyId"
        End If
    End Sub

    Private Sub FillDepartment()
        dtDepartment = objCommFunction.FillDataSet("SELECT Pk_LabelDetailId_Num As DepartmentId,LabelItemName_vch As Department FROM dbo.Label_Items WHERE fk_LabelId_num=5" &
                    " order by LabelItemName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtDepartment.Rows.Count > 0 Then
            ChkdLB_Department.DataSource = dtDepartment
            ChkdLB_Department.DisplayMember = "Department"
            ChkdLB_Department.ValueMember = "DepartmentId"
        End If
    End Sub

    Private Sub FillType()
        dtType = objCommFunction.FillDataSet("SELECT Pk_LabelDetailId_Num As TypeId,LabelItemName_vch As Type FROM dbo.Label_Items WHERE fk_LabelId_num=6" &
                    " order by LabelItemName_vch").Tables(0)

        Dim i As Int32 = 0
        If dtType.Rows.Count > 0 Then
            ChkdLB_Type.DataSource = dtType
            ChkdLB_Type.DisplayMember = "Type"
            ChkdLB_Type.ValueMember = "TypeId"
        End If
    End Sub


    Private Sub btn_AllBrand_Click(sender As Object, e As EventArgs) Handles btn_AllBrand.Click
        Dim u As Integer
        If btn_AllBrand.Text = "Select All" Then
            For u = 0 To ChkdLB_Brand.Items.Count() - 1
                ChkdLB_Brand.SetItemChecked(u, True)
                btn_AllBrand.Text = "UnSelect"
            Next u
            allBrandFlag = True
        ElseIf btn_AllBrand.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Brand.Items.Count() - 1
                ChkdLB_Brand.SetItemChecked(u, False)
                btn_AllBrand.Text = "Select All"
            Next
            allBrandFlag = False
            BrandIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Brand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Brand.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView
        For i = 0 To ChkdLB_Brand.Items.Count - 1
            If ChkdLB_Brand.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Brand.Items(i)
                BrandIds = BrandIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                BrandIds &= "'" & drv(0).ToString().Trim & "',"



            Else
                If BrandIds <> "" Then
                    drv = ChkdLB_Brand.Items(i)
                    BrandIds = BrandIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If

            End If
        Next
    End Sub

    Private Sub btn_AllColor_Click(sender As Object, e As EventArgs) Handles btn_AllColor.Click
        Dim u As Integer
        If btn_AllColor.Text = "Select All" Then
            For u = 0 To ChkdLB_Color.Items.Count() - 1
                ChkdLB_Color.SetItemChecked(u, True)
                btn_AllColor.Text = "UnSelect"
            Next u
            allColorFlag = True
        ElseIf btn_AllColor.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Color.Items.Count() - 1
                ChkdLB_Color.SetItemChecked(u, False)
                btn_AllColor.Text = "Select All"
            Next
            allColorFlag = False
            ColorIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Color_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Color.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView

        For i = 0 To ChkdLB_Color.Items.Count - 1
            If ChkdLB_Color.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Color.Items(i)
                ColorIds = ColorIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                ColorIds &= "'" & drv(0).ToString().Trim & "',"
            Else
                If ColorIds <> "" Then
                    drv = ChkdLB_Color.Items(i)
                    ColorIds = ColorIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If
            End If
        Next

    End Sub

    Private Sub btn_AllSize_Click(sender As Object, e As EventArgs) Handles btn_AllSize.Click
        Dim u As Integer
        If btn_AllSize.Text = "Select All" Then
            For u = 0 To ChkdLB_Size.Items.Count() - 1
                ChkdLB_Size.SetItemChecked(u, True)
                btn_AllSize.Text = "UnSelect"
            Next u
            allSizeFlag = True
        ElseIf btn_AllSize.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Size.Items.Count() - 1
                ChkdLB_Size.SetItemChecked(u, False)
                btn_AllSize.Text = "Select All"
            Next
            allSizeFlag = False
            SizeIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Size_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Size.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView

        For i = 0 To ChkdLB_Size.Items.Count - 1
            If ChkdLB_Size.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Size.Items(i)
                SizeIds = SizeIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                SizeIds &= "'" & drv(0).ToString().Trim & "',"

            Else
                If SizeIds <> "" Then
                    drv = ChkdLB_Size.Items(i)
                    SizeIds = SizeIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If
            End If
        Next
    End Sub

    Private Sub btn_AllCompany_Click(sender As Object, e As EventArgs) Handles btn_AllCompany.Click
        Dim u As Integer
        If btn_AllCompany.Text = "Select All" Then
            For u = 0 To ChkdLB_Company.Items.Count() - 1
                ChkdLB_Company.SetItemChecked(u, True)
                btn_AllCompany.Text = "UnSelect"
            Next u
            allCompanyFlag = True
        ElseIf btn_AllCompany.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Company.Items.Count() - 1
                ChkdLB_Company.SetItemChecked(u, False)
                btn_AllCompany.Text = "Select All"
            Next
            allCompanyFlag = False
            CompanyIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Company_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Company.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView

        For i = 0 To ChkdLB_Company.Items.Count - 1
            If ChkdLB_Company.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Company.Items(i)

                CompanyIds = CompanyIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                CompanyIds &= "'" & drv(0).ToString().Trim & "',"


            Else
                If CompanyIds <> "" Then
                    drv = ChkdLB_Company.Items(i)
                    CompanyIds = CompanyIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If
            End If
        Next
    End Sub

    Private Sub btn_AllDepartment_Click(sender As Object, e As EventArgs) Handles btn_AllDepartment.Click
        Dim u As Integer
        If btn_AllDepartment.Text = "Select All" Then
            For u = 0 To ChkdLB_Department.Items.Count() - 1
                ChkdLB_Department.SetItemChecked(u, True)
                btn_AllDepartment.Text = "UnSelect"
            Next u
            allDepartmentFlag = True
        ElseIf btn_AllDepartment.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Department.Items.Count() - 1
                ChkdLB_Department.SetItemChecked(u, False)
                btn_AllDepartment.Text = "Select All"
            Next
            allDepartmentFlag = False
            DepartmentIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Department_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Department.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView

        For i = 0 To ChkdLB_Department.Items.Count - 1
            If ChkdLB_Department.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Department.Items(i)

                DepartmentIds = DepartmentIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                DepartmentIds &= "'" & drv(0).ToString().Trim & "',"

            Else
                If DepartmentIds <> "" Then
                    drv = ChkdLB_Department.Items(i)
                    DepartmentIds = DepartmentIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If
            End If
        Next
    End Sub

    Private Sub btn_AllType_Click(sender As Object, e As EventArgs) Handles btn_AllType.Click
        Dim u As Integer
        If btn_AllType.Text = "Select All" Then
            For u = 0 To ChkdLB_Type.Items.Count() - 1
                ChkdLB_Type.SetItemChecked(u, True)
                btn_AllType.Text = "UnSelect"
            Next u
            allTypeFlag = True
        ElseIf btn_AllType.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Type.Items.Count() - 1
                ChkdLB_Type.SetItemChecked(u, False)
                btn_AllType.Text = "Select All"
            Next
            allTypeFlag = False
            TypeIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Type.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView

        For i = 0 To ChkdLB_Type.Items.Count - 1
            If ChkdLB_Type.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Type.Items(i)

                TypeIds = TypeIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                TypeIds &= "'" & drv(0).ToString().Trim & "',"

            Else
                If TypeIds <> "" Then
                    drv = ChkdLB_Type.Items(i)
                    TypeIds = TypeIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If
            End If
        Next
    End Sub

    Private Sub txtBrand_TextChanged(sender As Object, e As EventArgs) Handles txtBrand.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvBrand As DataView = dtBrand.DefaultView
        dvBrand.RowFilter = String.Format("Brand LIKE '{0}%'", txtBrand.Text)


        If BrandIds <> "" Then
            Dim NewBrandIds As String = BrandIds.Substring(0, BrandIds.Length() - 1)
            Dim CheckedBrandId() As String = NewBrandIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedBrandId
                For i = 0 To ChkdLB_Brand.Items.Count - 1
                    drv = ChkdLB_Brand.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Brand.SetItemChecked(i, True)
                    End If
                Next
            Next

        End If

    End Sub

    Private Sub btn_AllCategory_Click(sender As Object, e As EventArgs) Handles btn_AllCategory.Click
        Dim u As Integer
        If btn_AllCategory.Text = "Select All" Then
            For u = 0 To ChkdLB_Category.Items.Count() - 1
                ChkdLB_Category.SetItemChecked(u, True)
                btn_AllCategory.Text = "UnSelect"
            Next u
            allCategoryFlag = True
        ElseIf btn_AllCategory.Text = "UnSelect" Then
            For u = 0 To ChkdLB_Category.Items.Count() - 1
                ChkdLB_Category.SetItemChecked(u, False)
                btn_AllCategory.Text = "Select All"
            Next
            allCategoryFlag = False
            CategoryIds = ""
        End If
    End Sub

    Private Sub ChkdLB_Category_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChkdLB_Category.SelectedIndexChanged
        Dim i As Integer
        Dim drv As DataRowView

        For i = 0 To ChkdLB_Category.Items.Count - 1
            If ChkdLB_Category.GetItemCheckState(i) = CheckState.Checked Then
                drv = ChkdLB_Category.Items(i)

                CategoryIds = CategoryIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                CategoryIds &= "'" & drv(0).ToString().Trim & "',"
            Else
                If CategoryIds <> "" Then
                    drv = ChkdLB_Category.Items(i)
                    CategoryIds = CategoryIds.Replace("'" & drv(0).ToString().Trim & "',", "")
                End If
            End If
        Next
    End Sub

    Private Sub txtColor_TextChanged(sender As Object, e As EventArgs) Handles txtColor.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvColor As DataView = dtColor.DefaultView
        dvColor.RowFilter = String.Format("Color LIKE '{0}%'", txtColor.Text)


        If ColorIds <> "" Then
            Dim NewColorIds As String = ColorIds.Substring(0, ColorIds.Length() - 1)
            Dim CheckedColorId() As String = NewColorIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedColorId
                For i = 0 To ChkdLB_Color.Items.Count - 1
                    drv = ChkdLB_Color.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Color.SetItemChecked(i, True)
                    End If
                Next
            Next

        End If

    End Sub

    Private Sub txtSize_TextChanged(sender As Object, e As EventArgs) Handles txtSize.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvSize As DataView = dtSize.DefaultView
        dvSize.RowFilter = String.Format("Size LIKE '{0}%'", txtSize.Text)


        If SizeIds <> "" Then
            Dim NewSizeIds As String = SizeIds.Substring(0, SizeIds.Length() - 1)
            Dim CheckedSizeId() As String = NewSizeIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedSizeId
                For i = 0 To ChkdLB_Size.Items.Count - 1
                    drv = ChkdLB_Size.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Size.SetItemChecked(i, True)
                    End If
                Next
            Next

        End If
    End Sub

    Private Sub txtCompany_TextChanged(sender As Object, e As EventArgs) Handles txtCompany.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvCompany As DataView = dtCompany.DefaultView
        dvCompany.RowFilter = String.Format("Company LIKE '{0}%'", txtCompany.Text)


        If CompanyIds <> "" Then
            Dim NewCompanyIds As String = CompanyIds.Substring(0, CompanyIds.Length() - 1)
            Dim CheckedCompanyId() As String = NewCompanyIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedCompanyId
                For i = 0 To ChkdLB_Company.Items.Count - 1
                    drv = ChkdLB_Company.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Company.SetItemChecked(i, True)
                    End If
                Next
            Next

        End If
    End Sub

    Private Sub txtDepartment_TextChanged(sender As Object, e As EventArgs) Handles txtDepartment.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvDepartment As DataView = dtDepartment.DefaultView
        dvDepartment.RowFilter = String.Format("Department LIKE '{0}%'", txtDepartment.Text)

        If DepartmentIds <> "" Then
            Dim NewDepartmentIds As String = DepartmentIds.Substring(0, DepartmentIds.Length() - 1)
            Dim CheckedDepartmentId() As String = NewDepartmentIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedDepartmentId
                For i = 0 To ChkdLB_Department.Items.Count - 1
                    drv = ChkdLB_Department.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Department.SetItemChecked(i, True)
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub txtType_TextChanged(sender As Object, e As EventArgs) Handles txtType.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvType As DataView = dtType.DefaultView
        dvType.RowFilter = String.Format("Type LIKE '{0}%'", txtType.Text)


        If TypeIds <> "" Then
            Dim NewTypeIds As String = TypeIds.Substring(0, TypeIds.Length() - 1)
            Dim CheckedTypeId() As String = NewTypeIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedTypeId
                For i = 0 To ChkdLB_Type.Items.Count - 1
                    drv = ChkdLB_Type.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Type.SetItemChecked(i, True)
                    End If
                Next
            Next

        End If
    End Sub

    Private Sub txtCategory_TextChanged(sender As Object, e As EventArgs) Handles txtCategory.TextChanged

        Dim i As Integer
        Dim drv As DataRowView

        Dim dvCategory As DataView = dtCategory.DefaultView
        dvCategory.RowFilter = String.Format("Category LIKE '{0}%'", txtCategory.Text)


        If CategoryIds <> "" Then
            Dim NewCategoryIds As String = CategoryIds.Substring(0, CategoryIds.Length() - 1)
            Dim CheckedCategoryId() As String = NewCategoryIds.Split(",")

            Dim arrayItem As String
            For Each arrayItem In CheckedCategoryId
                For i = 0 To ChkdLB_Category.Items.Count - 1
                    drv = ChkdLB_Category.Items(i)
                    If drv(0).ToString() = arrayItem.Replace("'", "") Then
                        ChkdLB_Category.SetItemChecked(i, True)
                    End If
                Next
            Next

        End If

    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If rbDall.Checked = True Then
            TryCast(dgvReport.DataSource, DataTable).DefaultView.RowFilter = String.Format("[Bill Date] LIKE '{0}%' OR [Account Name] LIKE '{0}%' OR [Brand] LIKE '{0}%' OR [Category] LIKE '{0}%' OR [Item] LIKE '{0}%' OR [Color] LIKE '{0}%' OR [Size] LIKE '{0}%' OR [GST No.] LIKE '{0}%' OR [Address] LIKE '{0}%' OR [Bill No.] LIKE '{0}%' OR [BAR Code] LIKE '{0}%' OR [HSN Code] LIKE '{0}%' OR [Make] LIKE '{0}%' OR [Department] LIKE '{0}%' OR [Type] LIKE '{0}%' OR [Mobile No.] LIKE '{0}%'", txtSearch.Text)
        Else
            If rbAccWise.Checked = True Then
                TryCast(dgvReport.DataSource, DataTable).DefaultView.RowFilter = String.Format("[Account Name] LIKE '{0}%' OR [GST No.] LIKE '{0}%'", txtSearch.Text)
            End If
            If rbItemWise.Checked = True Then
                TryCast(dgvReport.DataSource, DataTable).DefaultView.RowFilter = String.Format("[Item] LIKE '{0}%'", txtSearch.Text)
            End If
        End If
    End Sub

    Private Sub dgvReport_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReport.CellClick
        txtSearch.Focus()
    End Sub

    Private Sub dgvReport_SelectionChanged(sender As Object, e As EventArgs) Handles dgvReport.SelectionChanged
        txtSearch.Focus()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If dgvReport.Rows.Count > 0 Then
            If dgvReport.SelectedRows.Count = 0 Then
                dgvReport.Rows(0).Selected = True
            Else
                Dim selectedRowIndex As Int32 = dgvReport.SelectedRows(0).Index

                If (e.KeyCode = Keys.Up) Then
                    If selectedRowIndex > 0 Then
                        dgvReport.Rows(selectedRowIndex - 1).Selected = True
                        e.Handled = True
                    End If
                End If

                If (e.KeyCode = Keys.Down) Then
                    If selectedRowIndex < dgvReport.Rows.Count - 1 Then
                        dgvReport.Rows(selectedRowIndex + 1).Selected = True
                        e.Handled = True
                    End If
                End If
                selectedRowIndex = dgvReport.SelectedRows(0).Index
                dgvReport.CurrentCell = dgvReport.Rows(selectedRowIndex).Cells(1)
            End If
        End If
    End Sub
    Dim n As Integer = 0

    Private Sub txtMrpFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMrpFrom.KeyPress, txtMrpTo.KeyPress, txtBasePriceFrom.KeyPress, txtBasePriceTo.KeyPress, txtSaleRateFrom.KeyPress, txtSaleRateTo.KeyPress, txtBillAmt.KeyPress
        Dim DecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        e.Handled = Not (Char.IsDigit(e.KeyChar) Or
                         Asc(e.KeyChar) = 8 Or
                         (e.KeyChar = DecimalSeparator And sender.Text.IndexOf(DecimalSeparator) = -1))

    End Sub

    Private Sub txtHSNcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHSNcode.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then e.KeyChar = ""
    End Sub

    Private Sub btnShowBack_Click(sender As Object, e As EventArgs) Handles btnShowBack.Click
        pnlShow.Visible = False
        pnlShow.SendToBack()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        objCommFunction.ExportGridToExcel(dgvReport)
        MsgBox("Sale Analysis export successfully.", MsgBoxStyle.Information, gblMessageHeading)
    End Sub

    Private Sub btnLoadReport_Click(sender As Object, e As EventArgs) Handles btnLoadReport.Click
        BindData()
    End Sub


End Class
