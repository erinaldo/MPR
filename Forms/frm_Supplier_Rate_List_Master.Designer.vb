<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Supplier_Rate_List_Master
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Supplier_Rate_List_Master))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.grdSupplierList = New System.Windows.Forms.DataGridView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbSuppSrch = New MMSPlus.AutoCompleteCombo()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbSupplier = New MMSPlus.AutoCompleteCombo()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.txtBarcodeSearch = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.lblCap1 = New System.Windows.Forms.Label()
        Me.lblCap4 = New System.Windows.Forms.Label()
        Me.txtSupName = New System.Windows.Forms.TextBox()
        Me.lblCap3 = New System.Windows.Forms.Label()
        Me.lblCap2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grdSupplier = New System.Windows.Forms.DataGridView()
        Me.txt_search = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCap5 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.grdSupplierList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.ImageList = Me.ImageList1
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(910, 630)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.DimGray
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.ForeColor = System.Drawing.Color.White
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(902, 600)
        Me.TabPage1.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.grdSupplierList)
        Me.GroupBox4.ForeColor = System.Drawing.Color.White
        Me.GroupBox4.Location = New System.Drawing.Point(22, 95)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(861, 485)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "List of Supplier Rate List"
        '
        'grdSupplierList
        '
        Me.grdSupplierList.AllowUserToAddRows = False
        Me.grdSupplierList.AllowUserToDeleteRows = False
        Me.grdSupplierList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSupplierList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdSupplierList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdSupplierList.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdSupplierList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSupplierList.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSupplierList.Location = New System.Drawing.Point(3, 16)
        Me.grdSupplierList.Name = "grdSupplierList"
        Me.grdSupplierList.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdSupplierList.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdSupplierList.RowHeadersVisible = False
        Me.grdSupplierList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSupplierList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSupplierList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdSupplierList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.grdSupplierList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdSupplierList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdSupplierList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSupplierList.Size = New System.Drawing.Size(855, 466)
        Me.grdSupplierList.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbSuppSrch)
        Me.GroupBox3.Controls.Add(Me.txtSearch)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.chkActive)
        Me.GroupBox3.ForeColor = System.Drawing.Color.White
        Me.GroupBox3.Location = New System.Drawing.Point(22, 10)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(861, 79)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Search Options"
        '
        'cmbSuppSrch
        '
        Me.cmbSuppSrch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbSuppSrch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSuppSrch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSuppSrch.ForeColor = System.Drawing.Color.White
        Me.cmbSuppSrch.FormattingEnabled = True
        Me.cmbSuppSrch.Location = New System.Drawing.Point(150, 16)
        Me.cmbSuppSrch.Name = "cmbSuppSrch"
        Me.cmbSuppSrch.ResetOnClear = False
        Me.cmbSuppSrch.Size = New System.Drawing.Size(623, 24)
        Me.cmbSuppSrch.TabIndex = 1
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(150, 48)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(623, 18)
        Me.txtSearch.TabIndex = 3
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(22, 50)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(62, 13)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "Search By :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Item Rate List Search"
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Location = New System.Drawing.Point(794, 20)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(56, 17)
        Me.chkActive.TabIndex = 2
        Me.chkActive.Text = "Active"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.DimGray
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.lblCap5)
        Me.TabPage2.ImageIndex = 1
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(902, 600)
        Me.TabPage2.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbSupplier)
        Me.GroupBox1.Controls.Add(Me.lblFormHeading)
        Me.GroupBox1.Controls.Add(Me.txtBarcodeSearch)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtpDate)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.txtDesc)
        Me.GroupBox1.Controls.Add(Me.lblCap1)
        Me.GroupBox1.Controls.Add(Me.lblCap4)
        Me.GroupBox1.Controls.Add(Me.txtSupName)
        Me.GroupBox1.Controls.Add(Me.lblCap3)
        Me.GroupBox1.Controls.Add(Me.lblCap2)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(-4, -9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(910, 242)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cmbSupplier
        '
        Me.cmbSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSupplier.ForeColor = System.Drawing.Color.White
        Me.cmbSupplier.FormattingEnabled = True
        Me.cmbSupplier.Location = New System.Drawing.Point(130, 81)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.ResetOnClear = False
        Me.cmbSupplier.Size = New System.Drawing.Size(699, 24)
        Me.cmbSupplier.TabIndex = 2
        '
        'lblFormHeading
        '
        Me.lblFormHeading.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(88, Byte), Integer))
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.OrangeRed
        Me.lblFormHeading.Location = New System.Drawing.Point(891, 10)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(14, 230)
        Me.lblFormHeading.TabIndex = 9
        Me.lblFormHeading.Text = "Supplier  RateList"
        '
        'txtBarcodeSearch
        '
        Me.txtBarcodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtBarcodeSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBarcodeSearch.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcodeSearch.ForeColor = System.Drawing.Color.White
        Me.txtBarcodeSearch.Location = New System.Drawing.Point(130, 202)
        Me.txtBarcodeSearch.MaxLength = 100
        Me.txtBarcodeSearch.Name = "txtBarcodeSearch"
        Me.txtBarcodeSearch.Size = New System.Drawing.Size(699, 19)
        Me.txtBarcodeSearch.TabIndex = 5
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(31, 203)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(62, 15)
        Me.Label15.TabIndex = 63
        Me.Label15.Text = "BarCode :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(851, 131)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 15)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Active"
        '
        'dtpDate
        '
        Me.dtpDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(691, 36)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(138, 21)
        Me.dtpDate.TabIndex = 1
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(837, 133)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox1.TabIndex = 4
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'txtDesc
        '
        Me.txtDesc.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.ForeColor = System.Drawing.Color.White
        Me.txtDesc.Location = New System.Drawing.Point(130, 133)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(699, 42)
        Me.txtDesc.TabIndex = 3
        '
        'lblCap1
        '
        Me.lblCap1.AutoSize = True
        Me.lblCap1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap1.Location = New System.Drawing.Point(31, 38)
        Me.lblCap1.Name = "lblCap1"
        Me.lblCap1.Size = New System.Drawing.Size(99, 15)
        Me.lblCap1.TabIndex = 0
        Me.lblCap1.Text = "Rate List Name :"
        '
        'lblCap4
        '
        Me.lblCap4.AutoSize = True
        Me.lblCap4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap4.Location = New System.Drawing.Point(31, 133)
        Me.lblCap4.Name = "lblCap4"
        Me.lblCap4.Size = New System.Drawing.Size(76, 15)
        Me.lblCap4.TabIndex = 3
        Me.lblCap4.Text = "Description :"
        '
        'txtSupName
        '
        Me.txtSupName.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSupName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSupName.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.txtSupName.ForeColor = System.Drawing.Color.White
        Me.txtSupName.Location = New System.Drawing.Point(130, 36)
        Me.txtSupName.Name = "txtSupName"
        Me.txtSupName.Size = New System.Drawing.Size(454, 19)
        Me.txtSupName.TabIndex = 0
        '
        'lblCap3
        '
        Me.lblCap3.AutoSize = True
        Me.lblCap3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap3.Location = New System.Drawing.Point(31, 85)
        Me.lblCap3.Name = "lblCap3"
        Me.lblCap3.Size = New System.Drawing.Size(96, 15)
        Me.lblCap3.TabIndex = 2
        Me.lblCap3.Text = "Supplier Name :"
        '
        'lblCap2
        '
        Me.lblCap2.AutoSize = True
        Me.lblCap2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap2.Location = New System.Drawing.Point(597, 38)
        Me.lblCap2.Name = "lblCap2"
        Me.lblCap2.Size = New System.Drawing.Size(91, 15)
        Me.lblCap2.TabIndex = 1
        Me.lblCap2.Text = "Rate List Date :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdSupplier)
        Me.GroupBox2.Controls.Add(Me.txt_search)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(-4, 214)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(907, 390)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'grdSupplier
        '
        Me.grdSupplier.BackgroundColor = System.Drawing.Color.DarkGray
        Me.grdSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSupplier.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSupplier.Location = New System.Drawing.Point(5, 20)
        Me.grdSupplier.Name = "grdSupplier"
        Me.grdSupplier.RowHeadersVisible = False
        Me.grdSupplier.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSupplier.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSupplier.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdSupplier.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdSupplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSupplier.Size = New System.Drawing.Size(898, 360)
        Me.grdSupplier.TabIndex = 0
        '
        'txt_search
        '
        Me.txt_search.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_search.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_search.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_search.ForeColor = System.Drawing.Color.White
        Me.txt_search.Location = New System.Drawing.Point(177, 34)
        Me.txt_search.MaxLength = 100
        Me.txt_search.Name = "txt_search"
        Me.txt_search.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_search.Size = New System.Drawing.Size(679, 19)
        Me.txt_search.TabIndex = 66
        Me.txt_search.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(160, 15)
        Me.Label3.TabIndex = 65
        Me.Label3.Text = "Serach By Name / Barcode :"
        Me.Label3.Visible = False
        '
        'lblCap5
        '
        Me.lblCap5.AutoSize = True
        Me.lblCap5.Location = New System.Drawing.Point(720, 158)
        Me.lblCap5.Name = "lblCap5"
        Me.lblCap5.Size = New System.Drawing.Size(37, 13)
        Me.lblCap5.TabIndex = 4
        Me.lblCap5.Text = "Active"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'frm_Supplier_Rate_List_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frm_Supplier_Rate_List_Master"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.grdSupplierList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCap1 As System.Windows.Forms.Label
    Friend WithEvents txtSupName As System.Windows.Forms.TextBox
    Friend WithEvents lblCap2 As System.Windows.Forms.Label
    Friend WithEvents lblCap5 As System.Windows.Forms.Label
    Friend WithEvents lblCap4 As System.Windows.Forms.Label
    Friend WithEvents lblCap3 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdSupplier As System.Windows.Forms.DataGridView
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents grdSupplierList As System.Windows.Forms.DataGridView
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents txtBarcodeSearch As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txt_search As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbSupplier As AutoCompleteCombo
    Friend WithEvents cmbSuppSrch As AutoCompleteCombo
End Class
