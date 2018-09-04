<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_BillBook
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_BillBook))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.List = New System.Windows.Forms.TabPage()
        Me.BtnCancelInv = New System.Windows.Forms.Button()
        Me.BtnInvoice = New System.Windows.Forms.Button()
        Me.BtnDc = New System.Windows.Forms.Button()
        Me.GBMRSDetail = New System.Windows.Forms.GroupBox()
        Me.flxList = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GBDCMASTER = New System.Windows.Forms.GroupBox()
        Me.cmbSupplier = New MMSPlus.AutoCompleteCombo()
        Me.cmbBillBook = New System.Windows.Forms.ComboBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dtDate = New System.Windows.Forms.DateTimePicker()
        Me.txtBarcodeSearch = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtShippingAddress = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbCity = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnAddNew = New System.Windows.Forms.Button()
        Me.txtGstNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.rbtn_Cash = New System.Windows.Forms.RadioButton()
        Me.rdbtn_credit = New System.Windows.Forms.RadioButton()
        Me.txtcustomer_name = New System.Windows.Forms.TextBox()
        Me.cmbinvtype = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_txtphoneNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCap2 = New System.Windows.Forms.Label()
        Me.lblCap1 = New System.Windows.Forms.Label()
        Me.lbl_INVNo = New System.Windows.Forms.Label()
        Me.lblMRSDate = New System.Windows.Forms.Label()
        Me.lblMRSCode = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.GBItemInfo = New System.Windows.Forms.GroupBox()
        Me.txtEwayBillNo = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblGSTDetail = New System.Windows.Forms.Label()
        Me.lblGST3 = New System.Windows.Forms.Label()
        Me.lblGST28 = New System.Windows.Forms.Label()
        Me.lblGST18 = New System.Windows.Forms.Label()
        Me.lblGST12 = New System.Windows.Forms.Label()
        Me.lblGST5 = New System.Windows.Forms.Label()
        Me.lblGST0 = New System.Windows.Forms.Label()
        Me.lblGSTHeader = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblACessAmount = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblCessAmount = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblTotalQty = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblTotalDisc = New System.Windows.Forms.Label()
        Me.lnkCalculatePOAmt = New System.Windows.Forms.LinkLabel()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtLRNO = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblVatAmount = New System.Windows.Forms.Label()
        Me.lblItemValue = New System.Windows.Forms.Label()
        Me.txtTransport = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.flxItems = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.txtvechicle_no = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.List.SuspendLayout()
        Me.GBMRSDetail.SuspendLayout()
        CType(Me.flxList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GBDCMASTER.SuspendLayout()
        Me.GBItemInfo.SuspendLayout()
        CType(Me.flxItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.List)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.ImageList1
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(910, 630)
        Me.TabControl1.TabIndex = 1
        '
        'List
        '
        Me.List.BackColor = System.Drawing.Color.DimGray
        Me.List.Controls.Add(Me.BtnCancelInv)
        Me.List.Controls.Add(Me.BtnInvoice)
        Me.List.Controls.Add(Me.BtnDc)
        Me.List.Controls.Add(Me.GBMRSDetail)
        Me.List.Controls.Add(Me.GroupBox2)
        Me.List.ForeColor = System.Drawing.Color.White
        Me.List.ImageIndex = 0
        Me.List.Location = New System.Drawing.Point(4, 26)
        Me.List.Name = "List"
        Me.List.Padding = New System.Windows.Forms.Padding(3)
        Me.List.Size = New System.Drawing.Size(902, 600)
        Me.List.TabIndex = 0
        '
        'BtnCancelInv
        '
        Me.BtnCancelInv.BackColor = System.Drawing.Color.Tomato
        Me.BtnCancelInv.Location = New System.Drawing.Point(473, 545)
        Me.BtnCancelInv.Name = "BtnCancelInv"
        Me.BtnCancelInv.Size = New System.Drawing.Size(141, 30)
        Me.BtnCancelInv.TabIndex = 2
        Me.BtnCancelInv.Text = "Cancel Bill"
        Me.BtnCancelInv.UseVisualStyleBackColor = False
        '
        'BtnInvoice
        '
        Me.BtnInvoice.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnInvoice.Location = New System.Drawing.Point(621, 545)
        Me.BtnInvoice.Name = "BtnInvoice"
        Me.BtnInvoice.Size = New System.Drawing.Size(128, 30)
        Me.BtnInvoice.TabIndex = 3
        Me.BtnInvoice.Text = "Print Bill"
        Me.BtnInvoice.UseVisualStyleBackColor = False
        '
        'BtnDc
        '
        Me.BtnDc.BackColor = System.Drawing.Color.Green
        Me.BtnDc.Location = New System.Drawing.Point(755, 545)
        Me.BtnDc.Name = "BtnDc"
        Me.BtnDc.Size = New System.Drawing.Size(128, 30)
        Me.BtnDc.TabIndex = 4
        Me.BtnDc.Text = "Print Dc"
        Me.BtnDc.UseVisualStyleBackColor = False
        '
        'GBMRSDetail
        '
        Me.GBMRSDetail.Controls.Add(Me.flxList)
        Me.GBMRSDetail.Location = New System.Drawing.Point(19, 91)
        Me.GBMRSDetail.Name = "GBMRSDetail"
        Me.GBMRSDetail.Size = New System.Drawing.Size(864, 441)
        Me.GBMRSDetail.TabIndex = 1
        Me.GBMRSDetail.TabStop = False
        '
        'flxList
        '
        Me.flxList.AllowUserToAddRows = False
        Me.flxList.AllowUserToDeleteRows = False
        Me.flxList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxList.Location = New System.Drawing.Point(3, 16)
        Me.flxList.Name = "flxList"
        Me.flxList.RowHeadersVisible = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.flxList.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.flxList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxList.Size = New System.Drawing.Size(858, 422)
        Me.flxList.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtSearch)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(19, 9)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(864, 76)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(89, 32)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(741, 18)
        Me.txtSearch.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 15)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Search By :"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.DimGray
        Me.TabPage2.Controls.Add(Me.GBDCMASTER)
        Me.TabPage2.Controls.Add(Me.GBItemInfo)
        Me.TabPage2.ForeColor = System.Drawing.Color.White
        Me.TabPage2.ImageIndex = 1
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(902, 600)
        Me.TabPage2.TabIndex = 1
        '
        'GBDCMASTER
        '
        Me.GBDCMASTER.Controls.Add(Me.cmbSupplier)
        Me.GBDCMASTER.Controls.Add(Me.cmbBillBook)
        Me.GBDCMASTER.Controls.Add(Me.Label17)
        Me.GBDCMASTER.Controls.Add(Me.dtDate)
        Me.GBDCMASTER.Controls.Add(Me.txtBarcodeSearch)
        Me.GBDCMASTER.Controls.Add(Me.Label15)
        Me.GBDCMASTER.Controls.Add(Me.txtShippingAddress)
        Me.GBDCMASTER.Controls.Add(Me.Label14)
        Me.GBDCMASTER.Controls.Add(Me.cmbCity)
        Me.GBDCMASTER.Controls.Add(Me.Label13)
        Me.GBDCMASTER.Controls.Add(Me.btnAddNew)
        Me.GBDCMASTER.Controls.Add(Me.txtGstNo)
        Me.GBDCMASTER.Controls.Add(Me.Label9)
        Me.GBDCMASTER.Controls.Add(Me.txtAddress)
        Me.GBDCMASTER.Controls.Add(Me.Label8)
        Me.GBDCMASTER.Controls.Add(Me.rbtn_Cash)
        Me.GBDCMASTER.Controls.Add(Me.rdbtn_credit)
        Me.GBDCMASTER.Controls.Add(Me.txtcustomer_name)
        Me.GBDCMASTER.Controls.Add(Me.cmbinvtype)
        Me.GBDCMASTER.Controls.Add(Me.Label4)
        Me.GBDCMASTER.Controls.Add(Me.txt_txtphoneNo)
        Me.GBDCMASTER.Controls.Add(Me.Label3)
        Me.GBDCMASTER.Controls.Add(Me.lblCap2)
        Me.GBDCMASTER.Controls.Add(Me.lblCap1)
        Me.GBDCMASTER.Controls.Add(Me.lbl_INVNo)
        Me.GBDCMASTER.Controls.Add(Me.lblMRSDate)
        Me.GBDCMASTER.Controls.Add(Me.lblMRSCode)
        Me.GBDCMASTER.Controls.Add(Me.lblFormHeading)
        Me.GBDCMASTER.Location = New System.Drawing.Point(-4, -7)
        Me.GBDCMASTER.Name = "GBDCMASTER"
        Me.GBDCMASTER.Size = New System.Drawing.Size(910, 180)
        Me.GBDCMASTER.TabIndex = 0
        Me.GBDCMASTER.TabStop = False
        '
        'cmbSupplier
        '
        Me.cmbSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSupplier.ForeColor = System.Drawing.Color.White
        Me.cmbSupplier.FormattingEnabled = True
        Me.cmbSupplier.Location = New System.Drawing.Point(97, 45)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.ResetOnClear = False
        Me.cmbSupplier.Size = New System.Drawing.Size(738, 24)
        Me.cmbSupplier.TabIndex = 4
        '
        'cmbBillBook
        '
        Me.cmbBillBook.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbBillBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbBillBook.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBillBook.ForeColor = System.Drawing.Color.White
        Me.cmbBillBook.FormattingEnabled = True
        Me.cmbBillBook.Location = New System.Drawing.Point(97, 16)
        Me.cmbBillBook.Name = "cmbBillBook"
        Me.cmbBillBook.Size = New System.Drawing.Size(96, 23)
        Me.cmbBillBook.TabIndex = 0
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(235, 18)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(52, 15)
        Me.Label17.TabIndex = 62
        Me.Label17.Text = "Bill No. :"
        '
        'dtDate
        '
        Me.dtDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDate.Location = New System.Drawing.Point(457, 16)
        Me.dtDate.Name = "dtDate"
        Me.dtDate.Size = New System.Drawing.Size(100, 20)
        Me.dtDate.TabIndex = 1
        '
        'txtBarcodeSearch
        '
        Me.txtBarcodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtBarcodeSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBarcodeSearch.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcodeSearch.ForeColor = System.Drawing.Color.White
        Me.txtBarcodeSearch.Location = New System.Drawing.Point(97, 153)
        Me.txtBarcodeSearch.MaxLength = 100
        Me.txtBarcodeSearch.Name = "txtBarcodeSearch"
        Me.txtBarcodeSearch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBarcodeSearch.Size = New System.Drawing.Size(738, 19)
        Me.txtBarcodeSearch.TabIndex = 10
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(18, 153)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(62, 15)
        Me.Label15.TabIndex = 59
        Me.Label15.Text = "BarCode :"
        '
        'txtShippingAddress
        '
        Me.txtShippingAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtShippingAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtShippingAddress.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShippingAddress.ForeColor = System.Drawing.Color.White
        Me.txtShippingAddress.Location = New System.Drawing.Point(97, 99)
        Me.txtShippingAddress.MaxLength = 0
        Me.txtShippingAddress.Multiline = True
        Me.txtShippingAddress.Name = "txtShippingAddress"
        Me.txtShippingAddress.ReadOnly = True
        Me.txtShippingAddress.Size = New System.Drawing.Size(738, 25)
        Me.txtShippingAddress.TabIndex = 6
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(18, 100)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(62, 15)
        Me.Label14.TabIndex = 57
        Me.Label14.Text = "Shipping :"
        '
        'cmbCity
        '
        Me.cmbCity.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCity.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCity.ForeColor = System.Drawing.Color.White
        Me.cmbCity.FormattingEnabled = True
        Me.cmbCity.Items.AddRange(New Object() {"---Select---", "SCGST", "IGST", "UGST"})
        Me.cmbCity.Location = New System.Drawing.Point(837, 102)
        Me.cmbCity.Name = "cmbCity"
        Me.cmbCity.Size = New System.Drawing.Size(54, 23)
        Me.cmbCity.TabIndex = 11
        Me.cmbCity.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(834, 84)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(33, 15)
        Me.Label13.TabIndex = 56
        Me.Label13.Text = "City :"
        Me.Label13.Visible = False
        '
        'btnAddNew
        '
        Me.btnAddNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddNew.Location = New System.Drawing.Point(837, 49)
        Me.btnAddNew.Name = "btnAddNew"
        Me.btnAddNew.Size = New System.Drawing.Size(54, 23)
        Me.btnAddNew.TabIndex = 0
        Me.btnAddNew.Text = "Add New"
        Me.btnAddNew.UseVisualStyleBackColor = False
        Me.btnAddNew.Visible = False
        '
        'txtGstNo
        '
        Me.txtGstNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtGstNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGstNo.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.txtGstNo.ForeColor = System.Drawing.Color.White
        Me.txtGstNo.Location = New System.Drawing.Point(97, 130)
        Me.txtGstNo.MaxLength = 0
        Me.txtGstNo.Name = "txtGstNo"
        Me.txtGstNo.ReadOnly = True
        Me.txtGstNo.Size = New System.Drawing.Size(180, 19)
        Me.txtGstNo.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(18, 131)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 15)
        Me.Label9.TabIndex = 52
        Me.Label9.Text = "GST NO :"
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAddress.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.ForeColor = System.Drawing.Color.White
        Me.txtAddress.Location = New System.Drawing.Point(97, 72)
        Me.txtAddress.MaxLength = 0
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.Size = New System.Drawing.Size(738, 25)
        Me.txtAddress.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(603, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 15)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "Payment Mode :"
        '
        'rbtn_Cash
        '
        Me.rbtn_Cash.AutoSize = True
        Me.rbtn_Cash.Location = New System.Drawing.Point(703, 19)
        Me.rbtn_Cash.Name = "rbtn_Cash"
        Me.rbtn_Cash.Size = New System.Drawing.Size(54, 17)
        Me.rbtn_Cash.TabIndex = 2
        Me.rbtn_Cash.Text = "CASH"
        Me.rbtn_Cash.UseVisualStyleBackColor = True
        '
        'rdbtn_credit
        '
        Me.rdbtn_credit.AutoSize = True
        Me.rdbtn_credit.Checked = True
        Me.rdbtn_credit.Location = New System.Drawing.Point(776, 19)
        Me.rdbtn_credit.Name = "rdbtn_credit"
        Me.rdbtn_credit.Size = New System.Drawing.Size(65, 17)
        Me.rdbtn_credit.TabIndex = 3
        Me.rdbtn_credit.TabStop = True
        Me.rdbtn_credit.Text = "CREDIT"
        Me.rdbtn_credit.UseVisualStyleBackColor = True
        '
        'txtcustomer_name
        '
        Me.txtcustomer_name.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtcustomer_name.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtcustomer_name.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcustomer_name.ForeColor = System.Drawing.Color.White
        Me.txtcustomer_name.Location = New System.Drawing.Point(97, 50)
        Me.txtcustomer_name.MaxLength = 0
        Me.txtcustomer_name.Name = "txtcustomer_name"
        Me.txtcustomer_name.Size = New System.Drawing.Size(738, 19)
        Me.txtcustomer_name.TabIndex = 7
        '
        'cmbinvtype
        '
        Me.cmbinvtype.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbinvtype.Enabled = False
        Me.cmbinvtype.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbinvtype.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbinvtype.ForeColor = System.Drawing.Color.White
        Me.cmbinvtype.FormattingEnabled = True
        Me.cmbinvtype.Items.AddRange(New Object() {"---Select---", "SGST", "IGST", "UGST"})
        Me.cmbinvtype.Location = New System.Drawing.Point(685, 127)
        Me.cmbinvtype.Name = "cmbinvtype"
        Me.cmbinvtype.Size = New System.Drawing.Size(150, 23)
        Me.cmbinvtype.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(604, 130)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 15)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "Invoice Type :"
        '
        'txt_txtphoneNo
        '
        Me.txt_txtphoneNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_txtphoneNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_txtphoneNo.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.txt_txtphoneNo.ForeColor = System.Drawing.Color.White
        Me.txt_txtphoneNo.Location = New System.Drawing.Point(394, 130)
        Me.txt_txtphoneNo.MaxLength = 0
        Me.txt_txtphoneNo.Name = "txt_txtphoneNo"
        Me.txt_txtphoneNo.ReadOnly = True
        Me.txt_txtphoneNo.Size = New System.Drawing.Size(150, 19)
        Me.txt_txtphoneNo.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(322, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 15)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Phone NO :"
        '
        'lblCap2
        '
        Me.lblCap2.AutoSize = True
        Me.lblCap2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap2.Location = New System.Drawing.Point(18, 73)
        Me.lblCap2.Name = "lblCap2"
        Me.lblCap2.Size = New System.Drawing.Size(59, 15)
        Me.lblCap2.TabIndex = 12
        Me.lblCap2.Text = "Address :"
        '
        'lblCap1
        '
        Me.lblCap1.AutoSize = True
        Me.lblCap1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap1.Location = New System.Drawing.Point(18, 50)
        Me.lblCap1.Name = "lblCap1"
        Me.lblCap1.Size = New System.Drawing.Size(68, 15)
        Me.lblCap1.TabIndex = 10
        Me.lblCap1.Text = "Customer :"
        '
        'lbl_INVNo
        '
        Me.lbl_INVNo.AutoSize = True
        Me.lbl_INVNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_INVNo.ForeColor = System.Drawing.Color.Orange
        Me.lbl_INVNo.Location = New System.Drawing.Point(285, 19)
        Me.lbl_INVNo.Name = "lbl_INVNo"
        Me.lbl_INVNo.Size = New System.Drawing.Size(45, 15)
        Me.lbl_INVNo.TabIndex = 3
        Me.lbl_INVNo.Text = "INV No"
        '
        'lblMRSDate
        '
        Me.lblMRSDate.AutoSize = True
        Me.lblMRSDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRSDate.Location = New System.Drawing.Point(395, 19)
        Me.lblMRSDate.Name = "lblMRSDate"
        Me.lblMRSDate.Size = New System.Drawing.Size(59, 15)
        Me.lblMRSDate.TabIndex = 7
        Me.lblMRSDate.Text = "Bill Date :"
        '
        'lblMRSCode
        '
        Me.lblMRSCode.AutoSize = True
        Me.lblMRSCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRSCode.Location = New System.Drawing.Point(19, 20)
        Me.lblMRSCode.Name = "lblMRSCode"
        Me.lblMRSCode.Size = New System.Drawing.Size(61, 15)
        Me.lblMRSCode.TabIndex = 5
        Me.lblMRSCode.Text = "Bill Book :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(88, Byte), Integer))
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.OrangeRed
        Me.lblFormHeading.Location = New System.Drawing.Point(891, 9)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(14, 170)
        Me.lblFormHeading.TabIndex = 4
        Me.lblFormHeading.Text = "Bill  Book"
        Me.lblFormHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'GBItemInfo
        '
        Me.GBItemInfo.Controls.Add(Me.txtEwayBillNo)
        Me.GBItemInfo.Controls.Add(Me.Label20)
        Me.GBItemInfo.Controls.Add(Me.lblGSTDetail)
        Me.GBItemInfo.Controls.Add(Me.lblGST3)
        Me.GBItemInfo.Controls.Add(Me.lblGST28)
        Me.GBItemInfo.Controls.Add(Me.lblGST18)
        Me.GBItemInfo.Controls.Add(Me.lblGST12)
        Me.GBItemInfo.Controls.Add(Me.lblGST5)
        Me.GBItemInfo.Controls.Add(Me.lblGST0)
        Me.GBItemInfo.Controls.Add(Me.lblGSTHeader)
        Me.GBItemInfo.Controls.Add(Me.Panel1)
        Me.GBItemInfo.Controls.Add(Me.Panel15)
        Me.GBItemInfo.Controls.Add(Me.Label18)
        Me.GBItemInfo.Controls.Add(Me.lblACessAmount)
        Me.GBItemInfo.Controls.Add(Me.Label19)
        Me.GBItemInfo.Controls.Add(Me.lblCessAmount)
        Me.GBItemInfo.Controls.Add(Me.Label16)
        Me.GBItemInfo.Controls.Add(Me.lblTotalQty)
        Me.GBItemInfo.Controls.Add(Me.Label10)
        Me.GBItemInfo.Controls.Add(Me.lblTotalDisc)
        Me.GBItemInfo.Controls.Add(Me.lnkCalculatePOAmt)
        Me.GBItemInfo.Controls.Add(Me.lblNetAmount)
        Me.GBItemInfo.Controls.Add(Me.Label5)
        Me.GBItemInfo.Controls.Add(Me.txtLRNO)
        Me.GBItemInfo.Controls.Add(Me.Label12)
        Me.GBItemInfo.Controls.Add(Me.lblVatAmount)
        Me.GBItemInfo.Controls.Add(Me.lblItemValue)
        Me.GBItemInfo.Controls.Add(Me.txtTransport)
        Me.GBItemInfo.Controls.Add(Me.Label11)
        Me.GBItemInfo.Controls.Add(Me.Label6)
        Me.GBItemInfo.Controls.Add(Me.Label7)
        Me.GBItemInfo.Controls.Add(Me.flxItems)
        Me.GBItemInfo.Controls.Add(Me.txtvechicle_no)
        Me.GBItemInfo.Controls.Add(Me.Label2)
        Me.GBItemInfo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBItemInfo.ForeColor = System.Drawing.Color.White
        Me.GBItemInfo.Location = New System.Drawing.Point(-4, 164)
        Me.GBItemInfo.Name = "GBItemInfo"
        Me.GBItemInfo.Size = New System.Drawing.Size(910, 440)
        Me.GBItemInfo.TabIndex = 1
        Me.GBItemInfo.TabStop = False
        '
        'txtEwayBillNo
        '
        Me.txtEwayBillNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtEwayBillNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtEwayBillNo.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.txtEwayBillNo.ForeColor = System.Drawing.Color.White
        Me.txtEwayBillNo.Location = New System.Drawing.Point(109, 310)
        Me.txtEwayBillNo.MaxLength = 0
        Me.txtEwayBillNo.Name = "txtEwayBillNo"
        Me.txtEwayBillNo.ReadOnly = True
        Me.txtEwayBillNo.Size = New System.Drawing.Size(268, 19)
        Me.txtEwayBillNo.TabIndex = 2
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(18, 311)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(90, 15)
        Me.Label20.TabIndex = 291
        Me.Label20.Text = "E-Way Bill No. :"
        '
        'lblGSTDetail
        '
        Me.lblGSTDetail.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGSTDetail.ForeColor = System.Drawing.Color.Orange
        Me.lblGSTDetail.Location = New System.Drawing.Point(717, 310)
        Me.lblGSTDetail.Name = "lblGSTDetail"
        Me.lblGSTDetail.Size = New System.Drawing.Size(182, 37)
        Me.lblGSTDetail.TabIndex = 290
        Me.lblGSTDetail.Text = "UTGST - 0.00" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SGST - 0.00"
        Me.lblGSTDetail.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblGST3
        '
        Me.lblGST3.AutoSize = True
        Me.lblGST3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST3.ForeColor = System.Drawing.Color.White
        Me.lblGST3.Location = New System.Drawing.Point(428, 317)
        Me.lblGST3.Name = "lblGST3"
        Me.lblGST3.Size = New System.Drawing.Size(107, 14)
        Me.lblGST3.TabIndex = 289
        Me.lblGST3.Text = "3% - Amt @ Tax"
        '
        'lblGST28
        '
        Me.lblGST28.AutoSize = True
        Me.lblGST28.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST28.ForeColor = System.Drawing.Color.White
        Me.lblGST28.Location = New System.Drawing.Point(428, 411)
        Me.lblGST28.Name = "lblGST28"
        Me.lblGST28.Size = New System.Drawing.Size(115, 14)
        Me.lblGST28.TabIndex = 288
        Me.lblGST28.Text = "28% - Amt @ Tax"
        '
        'lblGST18
        '
        Me.lblGST18.AutoSize = True
        Me.lblGST18.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST18.ForeColor = System.Drawing.Color.White
        Me.lblGST18.Location = New System.Drawing.Point(428, 388)
        Me.lblGST18.Name = "lblGST18"
        Me.lblGST18.Size = New System.Drawing.Size(115, 14)
        Me.lblGST18.TabIndex = 287
        Me.lblGST18.Text = "18% - Amt @ Tax"
        '
        'lblGST12
        '
        Me.lblGST12.AutoSize = True
        Me.lblGST12.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST12.ForeColor = System.Drawing.Color.White
        Me.lblGST12.Location = New System.Drawing.Point(428, 364)
        Me.lblGST12.Name = "lblGST12"
        Me.lblGST12.Size = New System.Drawing.Size(115, 14)
        Me.lblGST12.TabIndex = 286
        Me.lblGST12.Text = "12% - Amt @ Tax"
        '
        'lblGST5
        '
        Me.lblGST5.AutoSize = True
        Me.lblGST5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST5.ForeColor = System.Drawing.Color.White
        Me.lblGST5.Location = New System.Drawing.Point(428, 341)
        Me.lblGST5.Name = "lblGST5"
        Me.lblGST5.Size = New System.Drawing.Size(107, 14)
        Me.lblGST5.TabIndex = 285
        Me.lblGST5.Text = "5% - Amt @ Tax"
        '
        'lblGST0
        '
        Me.lblGST0.AutoSize = True
        Me.lblGST0.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGST0.ForeColor = System.Drawing.Color.White
        Me.lblGST0.Location = New System.Drawing.Point(428, 294)
        Me.lblGST0.Name = "lblGST0"
        Me.lblGST0.Size = New System.Drawing.Size(107, 14)
        Me.lblGST0.TabIndex = 284
        Me.lblGST0.Text = "0% - Amt @ Tax"
        '
        'lblGSTHeader
        '
        Me.lblGSTHeader.AutoSize = True
        Me.lblGSTHeader.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGSTHeader.ForeColor = System.Drawing.Color.White
        Me.lblGSTHeader.Location = New System.Drawing.Point(428, 271)
        Me.lblGSTHeader.Name = "lblGSTHeader"
        Me.lblGSTHeader.Size = New System.Drawing.Size(116, 14)
        Me.lblGSTHeader.TabIndex = 283
        Me.lblGSTHeader.Text = "GST Summary :-"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Silver
        Me.Panel1.Location = New System.Drawing.Point(394, 264)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1, 170)
        Me.Panel1.TabIndex = 282
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.Silver
        Me.Panel15.Location = New System.Drawing.Point(669, 265)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(1, 170)
        Me.Panel15.TabIndex = 281
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(224, 272)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(90, 15)
        Me.Label18.TabIndex = 27
        Me.Label18.Text = "Total Quantity :"
        '
        'lblACessAmount
        '
        Me.lblACessAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblACessAmount.ForeColor = System.Drawing.Color.Orange
        Me.lblACessAmount.Location = New System.Drawing.Point(766, 365)
        Me.lblACessAmount.Name = "lblACessAmount"
        Me.lblACessAmount.Size = New System.Drawing.Size(133, 20)
        Me.lblACessAmount.TabIndex = 25
        Me.lblACessAmount.Text = "0.00"
        Me.lblACessAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(687, 369)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(50, 15)
        Me.Label19.TabIndex = 24
        Me.Label19.Text = "ACess :"
        '
        'lblCessAmount
        '
        Me.lblCessAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCessAmount.ForeColor = System.Drawing.Color.Orange
        Me.lblCessAmount.Location = New System.Drawing.Point(741, 345)
        Me.lblCessAmount.Name = "lblCessAmount"
        Me.lblCessAmount.Size = New System.Drawing.Size(158, 20)
        Me.lblCessAmount.TabIndex = 23
        Me.lblCessAmount.Text = "0.00"
        Me.lblCessAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(687, 349)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(43, 15)
        Me.Label16.TabIndex = 22
        Me.Label16.Text = "Cess :"
        '
        'lblTotalQty
        '
        Me.lblTotalQty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalQty.ForeColor = System.Drawing.Color.Lime
        Me.lblTotalQty.Location = New System.Drawing.Point(313, 270)
        Me.lblTotalQty.Name = "lblTotalQty"
        Me.lblTotalQty.Size = New System.Drawing.Size(64, 20)
        Me.lblTotalQty.TabIndex = 18
        Me.lblTotalQty.Text = "0.00"
        Me.lblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(687, 390)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(62, 15)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Discount :"
        '
        'lblTotalDisc
        '
        Me.lblTotalDisc.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalDisc.ForeColor = System.Drawing.Color.Orange
        Me.lblTotalDisc.Location = New System.Drawing.Point(763, 386)
        Me.lblTotalDisc.Name = "lblTotalDisc"
        Me.lblTotalDisc.Size = New System.Drawing.Size(136, 20)
        Me.lblTotalDisc.TabIndex = 0
        Me.lblTotalDisc.Text = "0.00"
        Me.lblTotalDisc.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lnkCalculatePOAmt
        '
        Me.lnkCalculatePOAmt.ActiveLinkColor = System.Drawing.Color.Lime
        Me.lnkCalculatePOAmt.BackColor = System.Drawing.Color.Gainsboro
        Me.lnkCalculatePOAmt.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkCalculatePOAmt.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.lnkCalculatePOAmt.LinkColor = System.Drawing.Color.OrangeRed
        Me.lnkCalculatePOAmt.Location = New System.Drawing.Point(18, 271)
        Me.lnkCalculatePOAmt.Name = "lnkCalculatePOAmt"
        Me.lnkCalculatePOAmt.Size = New System.Drawing.Size(200, 25)
        Me.lnkCalculatePOAmt.TabIndex = 1
        Me.lnkCalculatePOAmt.TabStop = True
        Me.lnkCalculatePOAmt.Text = "Calculate  Amount"
        Me.lnkCalculatePOAmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNetAmount
        '
        Me.lblNetAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.Lime
        Me.lblNetAmount.Location = New System.Drawing.Point(798, 408)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.Size = New System.Drawing.Size(101, 20)
        Me.lblNetAmount.TabIndex = 0
        Me.lblNetAmount.Text = "0.00"
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(687, 411)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 15)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Net Amount :"
        '
        'txtLRNO
        '
        Me.txtLRNO.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLRNO.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLRNO.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.txtLRNO.ForeColor = System.Drawing.Color.White
        Me.txtLRNO.Location = New System.Drawing.Point(109, 401)
        Me.txtLRNO.MaxLength = 0
        Me.txtLRNO.Name = "txtLRNO"
        Me.txtLRNO.Size = New System.Drawing.Size(268, 19)
        Me.txtLRNO.TabIndex = 5
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(18, 402)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(56, 15)
        Me.Label12.TabIndex = 55
        Me.Label12.Text = "LR. NO. :"
        '
        'lblVatAmount
        '
        Me.lblVatAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVatAmount.ForeColor = System.Drawing.Color.Orange
        Me.lblVatAmount.Location = New System.Drawing.Point(766, 289)
        Me.lblVatAmount.Name = "lblVatAmount"
        Me.lblVatAmount.Size = New System.Drawing.Size(133, 20)
        Me.lblVatAmount.TabIndex = 0
        Me.lblVatAmount.Text = "0.00"
        Me.lblVatAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblItemValue
        '
        Me.lblItemValue.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemValue.ForeColor = System.Drawing.Color.Orange
        Me.lblItemValue.Location = New System.Drawing.Point(783, 269)
        Me.lblItemValue.Name = "lblItemValue"
        Me.lblItemValue.Size = New System.Drawing.Size(116, 20)
        Me.lblItemValue.TabIndex = 0
        Me.lblItemValue.Text = "0.00"
        Me.lblItemValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTransport
        '
        Me.txtTransport.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtTransport.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTransport.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.txtTransport.ForeColor = System.Drawing.Color.White
        Me.txtTransport.Location = New System.Drawing.Point(109, 370)
        Me.txtTransport.MaxLength = 0
        Me.txtTransport.Name = "txtTransport"
        Me.txtTransport.Size = New System.Drawing.Size(268, 19)
        Me.txtTransport.TabIndex = 4
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(19, 375)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(66, 15)
        Me.Label11.TabIndex = 53
        Me.Label11.Text = "Transport :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(687, 293)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 15)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "GST Total :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(687, 271)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(94, 15)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Gross Amount :"
        '
        'flxItems
        '
        Me.flxItems.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.flxItems.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.flxItems.BackColor = System.Drawing.Color.Silver
        Me.flxItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.flxItems.ColumnInfo = "1,1,0,0,0,90,Columns:0{Width:26;AllowSorting:False;AllowDragging:False;AllowResiz" &
    "ing:False;AllowMerging:True;AllowEditing:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.flxItems.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.flxItems.Location = New System.Drawing.Point(7, 10)
        Me.flxItems.Name = "flxItems"
        Me.flxItems.Rows.Count = 2
        Me.flxItems.Rows.DefaultSize = 18
        Me.flxItems.Size = New System.Drawing.Size(896, 253)
        Me.flxItems.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("flxItems.Styles"))
        Me.flxItems.TabIndex = 0
        '
        'txtvechicle_no
        '
        Me.txtvechicle_no.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtvechicle_no.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtvechicle_no.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.txtvechicle_no.ForeColor = System.Drawing.Color.White
        Me.txtvechicle_no.Location = New System.Drawing.Point(109, 340)
        Me.txtvechicle_no.MaxLength = 0
        Me.txtvechicle_no.Name = "txtvechicle_no"
        Me.txtvechicle_no.Size = New System.Drawing.Size(268, 19)
        Me.txtvechicle_no.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(18, 340)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 15)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Vehicle NO :"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'frm_BillBook
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frm_BillBook"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TabControl1.ResumeLayout(False)
        Me.List.ResumeLayout(False)
        Me.GBMRSDetail.ResumeLayout(False)
        CType(Me.flxList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GBDCMASTER.ResumeLayout(False)
        Me.GBDCMASTER.PerformLayout()
        Me.GBItemInfo.ResumeLayout(False)
        Me.GBItemInfo.PerformLayout()
        CType(Me.flxItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents List As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GBDCMASTER As System.Windows.Forms.GroupBox
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents GBMRSDetail As System.Windows.Forms.GroupBox
    Friend WithEvents lblMRSCode As System.Windows.Forms.Label
    Friend WithEvents lblMRSDate As System.Windows.Forms.Label
    Friend WithEvents lbl_INVNo As System.Windows.Forms.Label
    Friend WithEvents GBItemInfo As System.Windows.Forms.GroupBox
    Friend WithEvents flxList As DataGridView
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents lblCap1 As System.Windows.Forms.Label
    Friend WithEvents lblCap2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtvechicle_no As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_txtphoneNo As System.Windows.Forms.TextBox
    Friend WithEvents flxItems As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lblNetAmount As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblVatAmount As System.Windows.Forms.Label
    Friend WithEvents lblItemValue As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lnkCalculatePOAmt As System.Windows.Forms.LinkLabel
    Friend WithEvents cmbinvtype As System.Windows.Forms.ComboBox
    Friend WithEvents BtnInvoice As System.Windows.Forms.Button
    Friend WithEvents BtnDc As System.Windows.Forms.Button
    Friend WithEvents BtnCancelInv As System.Windows.Forms.Button
    Friend WithEvents txtcustomer_name As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents rbtn_Cash As System.Windows.Forms.RadioButton
    Friend WithEvents rdbtn_credit As System.Windows.Forms.RadioButton
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtGstNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As Label
    Friend WithEvents lblTotalDisc As Label
    Friend WithEvents txtLRNO As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents txtTransport As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents btnAddNew As Button
    Friend WithEvents cmbCity As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents txtShippingAddress As TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtBarcodeSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblTotalQty As Label
    Friend WithEvents lblCessAmount As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents cmbBillBook As ComboBox
    Friend WithEvents Label17 As Label
    Friend WithEvents dtDate As DateTimePicker
    Friend WithEvents lblACessAmount As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents txtEwayBillNo As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents lblGSTDetail As Label
    Friend WithEvents lblGST3 As Label
    Friend WithEvents lblGST28 As Label
    Friend WithEvents lblGST18 As Label
    Friend WithEvents lblGST12 As Label
    Friend WithEvents lblGST5 As Label
    Friend WithEvents lblGST0 As Label
    Friend WithEvents lblGSTHeader As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel15 As Panel
    Friend WithEvents cmbSupplier As AutoCompleteCombo
End Class
