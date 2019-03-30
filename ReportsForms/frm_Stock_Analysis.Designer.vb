<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_Stock_Analysis
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Stock_Analysis))
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtType = New System.Windows.Forms.TextBox()
        Me.CESSAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBtnRemove = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.txtDepartment = New System.Windows.Forms.TextBox()
        Me.txtCompany = New System.Windows.Forms.TextBox()
        Me.txtSize = New System.Windows.Forms.TextBox()
        Me.txtColor = New System.Windows.Forms.TextBox()
        Me.txtCategory = New System.Windows.Forms.TextBox()
        Me.ChkdLB_Category = New System.Windows.Forms.CheckedListBox()
        Me.btn_AllCategory = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtBrand = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.ChkdLB_Type = New System.Windows.Forms.CheckedListBox()
        Me.btn_AllType = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ChkdLB_Department = New System.Windows.Forms.CheckedListBox()
        Me.btn_AllDepartment = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ChkdLB_Company = New System.Windows.Forms.CheckedListBox()
        Me.btn_AllCompany = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ChkdLB_Size = New System.Windows.Forms.CheckedListBox()
        Me.btn_AllSize = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ChkdLB_Color = New System.Windows.Forms.CheckedListBox()
        Me.btn_AllColor = New System.Windows.Forms.Button()
        Me.ChkdLB_Brand = New System.Windows.Forms.CheckedListBox()
        Me.btn_AllBrand = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.lblHeading = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.pnlShow = New System.Windows.Forms.Panel()
        Me.btnShowBack = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dgvReport = New System.Windows.Forms.DataGridView()
        Me.SNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CESS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HSNCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MRP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Rate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DiscPer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Discount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TAX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnLoadReport = New System.Windows.Forms.Button()
        Me.txtSearchedItem = New MMSPlus.AutoCompleteTextBoxSample.AutoCompleteTextbox()
        Me.rbTypeWise = New System.Windows.Forms.RadioButton()
        Me.rbDepartmentWise = New System.Windows.Forms.RadioButton()
        Me.rbCompanyWise = New System.Windows.Forms.RadioButton()
        Me.rbSizeWise = New System.Windows.Forms.RadioButton()
        Me.rbColorWise = New System.Windows.Forms.RadioButton()
        Me.rbBrandWise = New System.Windows.Forms.RadioButton()
        Me.rbCategoryWise = New System.Windows.Forms.RadioButton()
        Me.rbItemWise = New System.Windows.Forms.RadioButton()
        Me.cbxSingleLedger = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.pnlShow.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(88, Byte), Integer))
        Me.Label22.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label22.Location = New System.Drawing.Point(894, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(15, 200)
        Me.Label22.TabIndex = 58
        Me.Label22.Text = "Stock  Analysis"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Total
        '
        Me.Total.HeaderText = "Total"
        Me.Total.Name = "Total"
        Me.Total.ReadOnly = True
        Me.Total.Width = 70
        '
        'txtType
        '
        Me.txtType.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtType.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtType.ForeColor = System.Drawing.Color.White
        Me.txtType.Location = New System.Drawing.Point(631, 269)
        Me.txtType.Multiline = True
        Me.txtType.Name = "txtType"
        Me.txtType.Size = New System.Drawing.Size(160, 28)
        Me.txtType.TabIndex = 26
        '
        'CESSAmount
        '
        Me.CESSAmount.HeaderText = "CESS Amt"
        Me.CESSAmount.Name = "CESSAmount"
        Me.CESSAmount.ReadOnly = True
        Me.CESSAmount.Width = 50
        '
        'colBtnRemove
        '
        Me.colBtnRemove.HeaderText = "Action"
        Me.colBtnRemove.Name = "colBtnRemove"
        Me.colBtnRemove.ReadOnly = True
        Me.colBtnRemove.Width = 70
        '
        'txtDepartment
        '
        Me.txtDepartment.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDepartment.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.ForeColor = System.Drawing.Color.White
        Me.txtDepartment.Location = New System.Drawing.Point(326, 269)
        Me.txtDepartment.Multiline = True
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(160, 28)
        Me.txtDepartment.TabIndex = 23
        '
        'txtCompany
        '
        Me.txtCompany.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtCompany.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompany.ForeColor = System.Drawing.Color.White
        Me.txtCompany.Location = New System.Drawing.Point(16, 269)
        Me.txtCompany.Multiline = True
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Size = New System.Drawing.Size(160, 28)
        Me.txtCompany.TabIndex = 20
        '
        'txtSize
        '
        Me.txtSize.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSize.ForeColor = System.Drawing.Color.White
        Me.txtSize.Location = New System.Drawing.Point(631, 147)
        Me.txtSize.Multiline = True
        Me.txtSize.Name = "txtSize"
        Me.txtSize.Size = New System.Drawing.Size(160, 28)
        Me.txtSize.TabIndex = 17
        '
        'txtColor
        '
        Me.txtColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtColor.ForeColor = System.Drawing.Color.White
        Me.txtColor.Location = New System.Drawing.Point(326, 147)
        Me.txtColor.Multiline = True
        Me.txtColor.Name = "txtColor"
        Me.txtColor.Size = New System.Drawing.Size(160, 28)
        Me.txtColor.TabIndex = 14
        '
        'txtCategory
        '
        Me.txtCategory.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.ForeColor = System.Drawing.Color.White
        Me.txtCategory.Location = New System.Drawing.Point(16, 401)
        Me.txtCategory.Multiline = True
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.Size = New System.Drawing.Size(160, 28)
        Me.txtCategory.TabIndex = 29
        '
        'ChkdLB_Category
        '
        Me.ChkdLB_Category.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ChkdLB_Category.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ChkdLB_Category.CheckOnClick = True
        Me.ChkdLB_Category.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkdLB_Category.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkdLB_Category.ForeColor = System.Drawing.Color.White
        Me.ChkdLB_Category.FormattingEnabled = True
        Me.ChkdLB_Category.Location = New System.Drawing.Point(16, 435)
        Me.ChkdLB_Category.Name = "ChkdLB_Category"
        Me.ChkdLB_Category.ScrollAlwaysVisible = True
        Me.ChkdLB_Category.Size = New System.Drawing.Size(245, 68)
        Me.ChkdLB_Category.Sorted = True
        Me.ChkdLB_Category.TabIndex = 30
        Me.ChkdLB_Category.ThreeDCheckBoxes = True
        Me.ChkdLB_Category.UseTabStops = False
        '
        'btn_AllCategory
        '
        Me.btn_AllCategory.BackColor = System.Drawing.Color.DodgerBlue
        Me.btn_AllCategory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_AllCategory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btn_AllCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_AllCategory.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.btn_AllCategory.ForeColor = System.Drawing.Color.White
        Me.btn_AllCategory.Location = New System.Drawing.Point(180, 401)
        Me.btn_AllCategory.Name = "btn_AllCategory"
        Me.btn_AllCategory.Size = New System.Drawing.Size(82, 28)
        Me.btn_AllCategory.TabIndex = 31
        Me.btn_AllCategory.Text = "Select All"
        Me.btn_AllCategory.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(13, 380)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(198, 28)
        Me.Label13.TabIndex = 354
        Me.Label13.Text = "Category"
        '
        'txtBrand
        '
        Me.txtBrand.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtBrand.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBrand.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBrand.ForeColor = System.Drawing.Color.White
        Me.txtBrand.Location = New System.Drawing.Point(16, 147)
        Me.txtBrand.Multiline = True
        Me.txtBrand.Name = "txtBrand"
        Me.txtBrand.Size = New System.Drawing.Size(160, 28)
        Me.txtBrand.TabIndex = 11
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Silver
        Me.Label26.Location = New System.Drawing.Point(11, 24)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(312, 16)
        Me.Label26.TabIndex = 351
        Me.Label26.Text = "Select View Template for Comulative Ledger :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Silver
        Me.Label14.Location = New System.Drawing.Point(13, 524)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(244, 16)
        Me.Label14.TabIndex = 339
        Me.Label14.Text = "Type Item Name/Barcode to search"
        '
        'ChkdLB_Type
        '
        Me.ChkdLB_Type.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ChkdLB_Type.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ChkdLB_Type.CheckOnClick = True
        Me.ChkdLB_Type.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkdLB_Type.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkdLB_Type.ForeColor = System.Drawing.Color.White
        Me.ChkdLB_Type.FormattingEnabled = True
        Me.ChkdLB_Type.Location = New System.Drawing.Point(631, 300)
        Me.ChkdLB_Type.Name = "ChkdLB_Type"
        Me.ChkdLB_Type.ScrollAlwaysVisible = True
        Me.ChkdLB_Type.Size = New System.Drawing.Size(245, 68)
        Me.ChkdLB_Type.Sorted = True
        Me.ChkdLB_Type.TabIndex = 27
        Me.ChkdLB_Type.ThreeDCheckBoxes = True
        Me.ChkdLB_Type.UseTabStops = False
        '
        'btn_AllType
        '
        Me.btn_AllType.BackColor = System.Drawing.Color.DodgerBlue
        Me.btn_AllType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_AllType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btn_AllType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_AllType.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.btn_AllType.ForeColor = System.Drawing.Color.White
        Me.btn_AllType.Location = New System.Drawing.Point(795, 269)
        Me.btn_AllType.Name = "btn_AllType"
        Me.btn_AllType.Size = New System.Drawing.Size(82, 28)
        Me.btn_AllType.TabIndex = 28
        Me.btn_AllType.Text = "Select All"
        Me.btn_AllType.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(628, 252)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(198, 28)
        Me.Label9.TabIndex = 338
        Me.Label9.Text = "Type"
        '
        'ChkdLB_Department
        '
        Me.ChkdLB_Department.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ChkdLB_Department.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ChkdLB_Department.CheckOnClick = True
        Me.ChkdLB_Department.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkdLB_Department.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkdLB_Department.ForeColor = System.Drawing.Color.White
        Me.ChkdLB_Department.FormattingEnabled = True
        Me.ChkdLB_Department.Location = New System.Drawing.Point(326, 300)
        Me.ChkdLB_Department.Name = "ChkdLB_Department"
        Me.ChkdLB_Department.ScrollAlwaysVisible = True
        Me.ChkdLB_Department.Size = New System.Drawing.Size(245, 68)
        Me.ChkdLB_Department.Sorted = True
        Me.ChkdLB_Department.TabIndex = 24
        Me.ChkdLB_Department.ThreeDCheckBoxes = True
        Me.ChkdLB_Department.UseTabStops = False
        '
        'btn_AllDepartment
        '
        Me.btn_AllDepartment.BackColor = System.Drawing.Color.DodgerBlue
        Me.btn_AllDepartment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_AllDepartment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btn_AllDepartment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_AllDepartment.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.btn_AllDepartment.ForeColor = System.Drawing.Color.White
        Me.btn_AllDepartment.Location = New System.Drawing.Point(490, 269)
        Me.btn_AllDepartment.Name = "btn_AllDepartment"
        Me.btn_AllDepartment.Size = New System.Drawing.Size(82, 28)
        Me.btn_AllDepartment.TabIndex = 25
        Me.btn_AllDepartment.Text = "Select All"
        Me.btn_AllDepartment.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(323, 252)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(198, 28)
        Me.Label10.TabIndex = 337
        Me.Label10.Text = "Department"
        '
        'ChkdLB_Company
        '
        Me.ChkdLB_Company.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ChkdLB_Company.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ChkdLB_Company.CheckOnClick = True
        Me.ChkdLB_Company.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkdLB_Company.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkdLB_Company.ForeColor = System.Drawing.Color.White
        Me.ChkdLB_Company.FormattingEnabled = True
        Me.ChkdLB_Company.Location = New System.Drawing.Point(16, 300)
        Me.ChkdLB_Company.Name = "ChkdLB_Company"
        Me.ChkdLB_Company.ScrollAlwaysVisible = True
        Me.ChkdLB_Company.Size = New System.Drawing.Size(245, 68)
        Me.ChkdLB_Company.Sorted = True
        Me.ChkdLB_Company.TabIndex = 21
        Me.ChkdLB_Company.ThreeDCheckBoxes = True
        Me.ChkdLB_Company.UseTabStops = False
        '
        'btn_AllCompany
        '
        Me.btn_AllCompany.BackColor = System.Drawing.Color.DodgerBlue
        Me.btn_AllCompany.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_AllCompany.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btn_AllCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_AllCompany.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.btn_AllCompany.ForeColor = System.Drawing.Color.White
        Me.btn_AllCompany.Location = New System.Drawing.Point(180, 269)
        Me.btn_AllCompany.Name = "btn_AllCompany"
        Me.btn_AllCompany.Size = New System.Drawing.Size(82, 28)
        Me.btn_AllCompany.TabIndex = 22
        Me.btn_AllCompany.Text = "Select All"
        Me.btn_AllCompany.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(13, 251)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(198, 28)
        Me.Label11.TabIndex = 336
        Me.Label11.Text = "Company"
        '
        'ChkdLB_Size
        '
        Me.ChkdLB_Size.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ChkdLB_Size.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ChkdLB_Size.CheckOnClick = True
        Me.ChkdLB_Size.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkdLB_Size.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkdLB_Size.ForeColor = System.Drawing.Color.White
        Me.ChkdLB_Size.FormattingEnabled = True
        Me.ChkdLB_Size.Location = New System.Drawing.Point(631, 178)
        Me.ChkdLB_Size.Name = "ChkdLB_Size"
        Me.ChkdLB_Size.ScrollAlwaysVisible = True
        Me.ChkdLB_Size.Size = New System.Drawing.Size(245, 68)
        Me.ChkdLB_Size.Sorted = True
        Me.ChkdLB_Size.TabIndex = 18
        Me.ChkdLB_Size.ThreeDCheckBoxes = True
        Me.ChkdLB_Size.UseTabStops = False
        '
        'btn_AllSize
        '
        Me.btn_AllSize.BackColor = System.Drawing.Color.DodgerBlue
        Me.btn_AllSize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_AllSize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btn_AllSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_AllSize.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.btn_AllSize.ForeColor = System.Drawing.Color.White
        Me.btn_AllSize.Location = New System.Drawing.Point(795, 147)
        Me.btn_AllSize.Name = "btn_AllSize"
        Me.btn_AllSize.Size = New System.Drawing.Size(82, 28)
        Me.btn_AllSize.TabIndex = 19
        Me.btn_AllSize.Text = "Select All"
        Me.btn_AllSize.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(628, 129)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(198, 28)
        Me.Label8.TabIndex = 335
        Me.Label8.Text = "Size"
        '
        'ChkdLB_Color
        '
        Me.ChkdLB_Color.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ChkdLB_Color.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ChkdLB_Color.CheckOnClick = True
        Me.ChkdLB_Color.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkdLB_Color.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkdLB_Color.ForeColor = System.Drawing.Color.White
        Me.ChkdLB_Color.FormattingEnabled = True
        Me.ChkdLB_Color.Location = New System.Drawing.Point(326, 178)
        Me.ChkdLB_Color.Name = "ChkdLB_Color"
        Me.ChkdLB_Color.ScrollAlwaysVisible = True
        Me.ChkdLB_Color.Size = New System.Drawing.Size(245, 68)
        Me.ChkdLB_Color.Sorted = True
        Me.ChkdLB_Color.TabIndex = 15
        Me.ChkdLB_Color.ThreeDCheckBoxes = True
        Me.ChkdLB_Color.UseTabStops = False
        '
        'btn_AllColor
        '
        Me.btn_AllColor.BackColor = System.Drawing.Color.DodgerBlue
        Me.btn_AllColor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_AllColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btn_AllColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_AllColor.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.btn_AllColor.ForeColor = System.Drawing.Color.White
        Me.btn_AllColor.Location = New System.Drawing.Point(490, 147)
        Me.btn_AllColor.Name = "btn_AllColor"
        Me.btn_AllColor.Size = New System.Drawing.Size(82, 28)
        Me.btn_AllColor.TabIndex = 16
        Me.btn_AllColor.Text = "Select All"
        Me.btn_AllColor.UseVisualStyleBackColor = False
        '
        'ChkdLB_Brand
        '
        Me.ChkdLB_Brand.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ChkdLB_Brand.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ChkdLB_Brand.CheckOnClick = True
        Me.ChkdLB_Brand.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ChkdLB_Brand.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkdLB_Brand.ForeColor = System.Drawing.Color.White
        Me.ChkdLB_Brand.FormattingEnabled = True
        Me.ChkdLB_Brand.Location = New System.Drawing.Point(16, 178)
        Me.ChkdLB_Brand.Name = "ChkdLB_Brand"
        Me.ChkdLB_Brand.ScrollAlwaysVisible = True
        Me.ChkdLB_Brand.Size = New System.Drawing.Size(245, 68)
        Me.ChkdLB_Brand.Sorted = True
        Me.ChkdLB_Brand.TabIndex = 12
        Me.ChkdLB_Brand.ThreeDCheckBoxes = True
        Me.ChkdLB_Brand.UseTabStops = False
        '
        'btn_AllBrand
        '
        Me.btn_AllBrand.BackColor = System.Drawing.Color.DodgerBlue
        Me.btn_AllBrand.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btn_AllBrand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btn_AllBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_AllBrand.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.btn_AllBrand.ForeColor = System.Drawing.Color.White
        Me.btn_AllBrand.Location = New System.Drawing.Point(180, 147)
        Me.btn_AllBrand.Name = "btn_AllBrand"
        Me.btn_AllBrand.Size = New System.Drawing.Size(82, 28)
        Me.btn_AllBrand.TabIndex = 13
        Me.btn_AllBrand.Text = "Select All"
        Me.btn_AllBrand.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(13, 129)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(198, 28)
        Me.Label6.TabIndex = 333
        Me.Label6.Text = "Brand"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Agency FB", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Silver
        Me.Label5.Location = New System.Drawing.Point(418, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 20)
        Me.Label5.TabIndex = 332
        Me.Label5.Text = "Attributes"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox3.Location = New System.Drawing.Point(1, 122)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(880, 1)
        Me.GroupBox3.TabIndex = 331
        Me.GroupBox3.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Agency FB", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Silver
        Me.Label3.Location = New System.Drawing.Point(415, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 20)
        Me.Label3.TabIndex = 329
        Me.Label3.Text = "Date Range"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(-5, 376)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(890, 1)
        Me.GroupBox2.TabIndex = 328
        Me.GroupBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Silver
        Me.Label2.Location = New System.Drawing.Point(322, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 16)
        Me.Label2.TabIndex = 327
        Me.Label2.Text = "To Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Silver
        Me.Label7.Location = New System.Drawing.Point(13, 101)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 16)
        Me.Label7.TabIndex = 326
        Me.Label7.Text = "From Date"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(-7, 514)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(890, 1)
        Me.GroupBox1.TabIndex = 325
        Me.GroupBox1.TabStop = False
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpToDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(410, 96)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(162, 22)
        Me.dtpToDate.TabIndex = 10
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFromDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(101, 96)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(160, 22)
        Me.dtpFromDate.TabIndex = 9
        '
        'lblHeading
        '
        Me.lblHeading.AutoSize = True
        Me.lblHeading.BackColor = System.Drawing.Color.Transparent
        Me.lblHeading.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.ForeColor = System.Drawing.Color.Silver
        Me.lblHeading.Location = New System.Drawing.Point(369, 2)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(177, 23)
        Me.lblHeading.TabIndex = 312
        Me.lblHeading.Text = "STOCK ANALYSIS"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.White
        Me.Label29.Location = New System.Drawing.Point(323, 129)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(198, 28)
        Me.Label29.TabIndex = 334
        Me.Label29.Text = "Color"
        '
        'pnlShow
        '
        Me.pnlShow.Controls.Add(Me.btnShowBack)
        Me.pnlShow.Controls.Add(Me.txtSearch)
        Me.pnlShow.Controls.Add(Me.Label33)
        Me.pnlShow.Controls.Add(Me.btnExport)
        Me.pnlShow.Controls.Add(Me.dgvReport)
        Me.pnlShow.Location = New System.Drawing.Point(1, 2)
        Me.pnlShow.Name = "pnlShow"
        Me.pnlShow.Size = New System.Drawing.Size(890, 629)
        Me.pnlShow.TabIndex = 353
        Me.pnlShow.Visible = False
        '
        'btnShowBack
        '
        Me.btnShowBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnShowBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnShowBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.btnShowBack.FlatAppearance.BorderSize = 0
        Me.btnShowBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowBack.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnShowBack.ForeColor = System.Drawing.Color.White
        Me.btnShowBack.Location = New System.Drawing.Point(633, 592)
        Me.btnShowBack.Name = "btnShowBack"
        Me.btnShowBack.Size = New System.Drawing.Size(120, 30)
        Me.btnShowBack.TabIndex = 3
        Me.btnShowBack.TabStop = False
        Me.btnShowBack.Text = "Back"
        Me.btnShowBack.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(2, 20)
        Me.txtSearch.MaxLength = 50
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(887, 22)
        Me.txtSearch.TabIndex = 1
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Silver
        Me.Label33.Location = New System.Drawing.Point(0, 2)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(236, 16)
        Me.Label33.TabIndex = 259
        Me.Label33.Text = "Filter Stock By specific attribute:-"
        '
        'btnExport
        '
        Me.btnExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.btnExport.FlatAppearance.BorderSize = 0
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnExport.ForeColor = System.Drawing.Color.White
        Me.btnExport.Location = New System.Drawing.Point(769, 592)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(120, 30)
        Me.btnExport.TabIndex = 2
        Me.btnExport.Text = "Export"
        Me.btnExport.UseVisualStyleBackColor = False
        '
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.AllowUserToOrderColumns = True
        Me.dgvReport.AllowUserToResizeRows = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.Gray
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Verdana", 7.0!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvReport.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvReport.BackgroundColor = System.Drawing.Color.Gray
        Me.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReport.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvReport.ColumnHeadersHeight = 20
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvReport.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SNo, Me.ItemName, Me.GST, Me.CESS, Me.HSNCode, Me.MRP, Me.Qty, Me.Rate, Me.DiscPer, Me.Discount, Me.TAX, Me.CESSAmount, Me.Total, Me.colBtnRemove})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvReport.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgvReport.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvReport.GridColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(15, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.dgvReport.Location = New System.Drawing.Point(2, 46)
        Me.dgvReport.MultiSelect = False
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReport.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvReport.RowHeadersVisible = False
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Verdana", 7.0!)
        DataGridViewCellStyle10.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.DarkOrange
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvReport.RowsDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvReport.Size = New System.Drawing.Size(888, 540)
        Me.dgvReport.TabIndex = 0
        '
        'SNo
        '
        Me.SNo.HeaderText = "S.No"
        Me.SNo.Name = "SNo"
        Me.SNo.ReadOnly = True
        Me.SNo.Width = 40
        '
        'ItemName
        '
        Me.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ItemName.HeaderText = " Name"
        Me.ItemName.Name = "ItemName"
        Me.ItemName.ReadOnly = True
        '
        'GST
        '
        Me.GST.HeaderText = "GST%"
        Me.GST.Name = "GST"
        Me.GST.ReadOnly = True
        Me.GST.Width = 50
        '
        'CESS
        '
        Me.CESS.HeaderText = "CESS%"
        Me.CESS.Name = "CESS"
        Me.CESS.ReadOnly = True
        Me.CESS.Width = 60
        '
        'HSNCode
        '
        Me.HSNCode.HeaderText = "HSNC"
        Me.HSNCode.Name = "HSNCode"
        Me.HSNCode.ReadOnly = True
        Me.HSNCode.Width = 50
        '
        'MRP
        '
        Me.MRP.HeaderText = "MRP"
        Me.MRP.Name = "MRP"
        Me.MRP.ReadOnly = True
        Me.MRP.Width = 50
        '
        'Qty
        '
        Me.Qty.HeaderText = "Qty."
        Me.Qty.Name = "Qty"
        Me.Qty.ReadOnly = True
        Me.Qty.Width = 50
        '
        'Rate
        '
        Me.Rate.HeaderText = "Rate"
        Me.Rate.Name = "Rate"
        Me.Rate.ReadOnly = True
        Me.Rate.Width = 50
        '
        'DiscPer
        '
        Me.DiscPer.HeaderText = "Disc%"
        Me.DiscPer.Name = "DiscPer"
        Me.DiscPer.ReadOnly = True
        Me.DiscPer.Width = 50
        '
        'Discount
        '
        Me.Discount.HeaderText = "Disc."
        Me.Discount.Name = "Discount"
        Me.Discount.ReadOnly = True
        Me.Discount.Width = 50
        '
        'TAX
        '
        Me.TAX.HeaderText = "GST Amt."
        Me.TAX.Name = "TAX"
        Me.TAX.ReadOnly = True
        Me.TAX.Width = 50
        '
        'btnLoadReport
        '
        Me.btnLoadReport.BackColor = System.Drawing.Color.Green
        Me.btnLoadReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLoadReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.btnLoadReport.FlatAppearance.BorderSize = 0
        Me.btnLoadReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoadReport.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnLoadReport.ForeColor = System.Drawing.Color.White
        Me.btnLoadReport.Location = New System.Drawing.Point(757, 586)
        Me.btnLoadReport.Name = "btnLoadReport"
        Me.btnLoadReport.Size = New System.Drawing.Size(118, 32)
        Me.btnLoadReport.TabIndex = 34
        Me.btnLoadReport.Text = "Load Report"
        Me.btnLoadReport.UseVisualStyleBackColor = False
        '
        'txtSearchedItem
        '
        Me.txtSearchedItem.AutoCompleteList = CType(resources.GetObject("txtSearchedItem.AutoCompleteList"), System.Collections.Generic.List(Of String))
        Me.txtSearchedItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearchedItem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchedItem.CaseSensitive = False
        Me.txtSearchedItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtSearchedItem.ForeColor = System.Drawing.Color.White
        Me.txtSearchedItem.Location = New System.Drawing.Point(16, 544)
        Me.txtSearchedItem.MinTypedCharacters = 2
        Me.txtSearchedItem.Multiline = True
        Me.txtSearchedItem.Name = "txtSearchedItem"
        Me.txtSearchedItem.SelectedIndex = -1
        Me.txtSearchedItem.Size = New System.Drawing.Size(556, 20)
        Me.txtSearchedItem.TabIndex = 32
        '
        'rbTypeWise
        '
        Me.rbTypeWise.AutoSize = True
        Me.rbTypeWise.BackColor = System.Drawing.Color.Transparent
        Me.rbTypeWise.Font = New System.Drawing.Font("Verdana", 7.25!, System.Drawing.FontStyle.Bold)
        Me.rbTypeWise.ForeColor = System.Drawing.Color.White
        Me.rbTypeWise.Location = New System.Drawing.Point(804, 49)
        Me.rbTypeWise.Name = "rbTypeWise"
        Me.rbTypeWise.Size = New System.Drawing.Size(81, 16)
        Me.rbTypeWise.TabIndex = 8
        Me.rbTypeWise.Tag = "1"
        Me.rbTypeWise.Text = "Type Wise"
        Me.rbTypeWise.UseVisualStyleBackColor = False
        '
        'rbDepartmentWise
        '
        Me.rbDepartmentWise.AutoSize = True
        Me.rbDepartmentWise.BackColor = System.Drawing.Color.Transparent
        Me.rbDepartmentWise.Font = New System.Drawing.Font("Verdana", 7.25!, System.Drawing.FontStyle.Bold)
        Me.rbDepartmentWise.ForeColor = System.Drawing.Color.White
        Me.rbDepartmentWise.Location = New System.Drawing.Point(663, 49)
        Me.rbDepartmentWise.Name = "rbDepartmentWise"
        Me.rbDepartmentWise.Size = New System.Drawing.Size(124, 16)
        Me.rbDepartmentWise.TabIndex = 6
        Me.rbDepartmentWise.Tag = "3"
        Me.rbDepartmentWise.Text = "Department Wise"
        Me.rbDepartmentWise.UseVisualStyleBackColor = False
        '
        'rbCompanyWise
        '
        Me.rbCompanyWise.AutoSize = True
        Me.rbCompanyWise.BackColor = System.Drawing.Color.Transparent
        Me.rbCompanyWise.Font = New System.Drawing.Font("Verdana", 7.25!, System.Drawing.FontStyle.Bold)
        Me.rbCompanyWise.ForeColor = System.Drawing.Color.White
        Me.rbCompanyWise.Location = New System.Drawing.Point(539, 49)
        Me.rbCompanyWise.Name = "rbCompanyWise"
        Me.rbCompanyWise.Size = New System.Drawing.Size(107, 16)
        Me.rbCompanyWise.TabIndex = 5
        Me.rbCompanyWise.Tag = "1"
        Me.rbCompanyWise.Text = "Company Wise"
        Me.rbCompanyWise.UseVisualStyleBackColor = False
        '
        'rbSizeWise
        '
        Me.rbSizeWise.AutoSize = True
        Me.rbSizeWise.BackColor = System.Drawing.Color.Transparent
        Me.rbSizeWise.Font = New System.Drawing.Font("Verdana", 7.25!, System.Drawing.FontStyle.Bold)
        Me.rbSizeWise.ForeColor = System.Drawing.Color.White
        Me.rbSizeWise.Location = New System.Drawing.Point(444, 49)
        Me.rbSizeWise.Name = "rbSizeWise"
        Me.rbSizeWise.Size = New System.Drawing.Size(77, 16)
        Me.rbSizeWise.TabIndex = 4
        Me.rbSizeWise.Tag = "3"
        Me.rbSizeWise.Text = "Size Wise"
        Me.rbSizeWise.UseVisualStyleBackColor = False
        '
        'rbColorWise
        '
        Me.rbColorWise.AutoSize = True
        Me.rbColorWise.BackColor = System.Drawing.Color.Transparent
        Me.rbColorWise.Font = New System.Drawing.Font("Verdana", 7.25!, System.Drawing.FontStyle.Bold)
        Me.rbColorWise.ForeColor = System.Drawing.Color.White
        Me.rbColorWise.Location = New System.Drawing.Point(342, 49)
        Me.rbColorWise.Name = "rbColorWise"
        Me.rbColorWise.Size = New System.Drawing.Size(83, 16)
        Me.rbColorWise.TabIndex = 3
        Me.rbColorWise.Tag = "2"
        Me.rbColorWise.Text = "Color Wise"
        Me.rbColorWise.UseVisualStyleBackColor = False
        '
        'rbBrandWise
        '
        Me.rbBrandWise.AutoSize = True
        Me.rbBrandWise.BackColor = System.Drawing.Color.Transparent
        Me.rbBrandWise.Font = New System.Drawing.Font("Verdana", 7.25!, System.Drawing.FontStyle.Bold)
        Me.rbBrandWise.ForeColor = System.Drawing.Color.White
        Me.rbBrandWise.Location = New System.Drawing.Point(236, 49)
        Me.rbBrandWise.Name = "rbBrandWise"
        Me.rbBrandWise.Size = New System.Drawing.Size(88, 16)
        Me.rbBrandWise.TabIndex = 2
        Me.rbBrandWise.Tag = "1"
        Me.rbBrandWise.Text = "Brand Wise"
        Me.rbBrandWise.UseVisualStyleBackColor = False
        '
        'rbCategoryWise
        '
        Me.rbCategoryWise.AutoSize = True
        Me.rbCategoryWise.BackColor = System.Drawing.Color.Transparent
        Me.rbCategoryWise.Font = New System.Drawing.Font("Verdana", 7.25!, System.Drawing.FontStyle.Bold)
        Me.rbCategoryWise.ForeColor = System.Drawing.Color.White
        Me.rbCategoryWise.Location = New System.Drawing.Point(113, 49)
        Me.rbCategoryWise.Name = "rbCategoryWise"
        Me.rbCategoryWise.Size = New System.Drawing.Size(106, 16)
        Me.rbCategoryWise.TabIndex = 1
        Me.rbCategoryWise.Tag = "3"
        Me.rbCategoryWise.Text = "Category Wise"
        Me.rbCategoryWise.UseVisualStyleBackColor = False
        '
        'rbItemWise
        '
        Me.rbItemWise.AutoSize = True
        Me.rbItemWise.BackColor = System.Drawing.Color.Transparent
        Me.rbItemWise.Checked = True
        Me.rbItemWise.Font = New System.Drawing.Font("Verdana", 7.25!, System.Drawing.FontStyle.Bold)
        Me.rbItemWise.ForeColor = System.Drawing.Color.White
        Me.rbItemWise.Location = New System.Drawing.Point(14, 49)
        Me.rbItemWise.Name = "rbItemWise"
        Me.rbItemWise.Size = New System.Drawing.Size(83, 16)
        Me.rbItemWise.TabIndex = 0
        Me.rbItemWise.TabStop = True
        Me.rbItemWise.Tag = "2"
        Me.rbItemWise.Text = "Item Wise"
        Me.rbItemWise.UseVisualStyleBackColor = False
        '
        'cbxSingleLedger
        '
        Me.cbxSingleLedger.AutoSize = True
        Me.cbxSingleLedger.BackColor = System.Drawing.Color.Transparent
        Me.cbxSingleLedger.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.cbxSingleLedger.ForeColor = System.Drawing.Color.White
        Me.cbxSingleLedger.Location = New System.Drawing.Point(630, 544)
        Me.cbxSingleLedger.Name = "cbxSingleLedger"
        Me.cbxSingleLedger.Size = New System.Drawing.Size(201, 20)
        Me.cbxSingleLedger.TabIndex = 33
        Me.cbxSingleLedger.Text = "Display Single Item Ledger"
        Me.cbxSingleLedger.UseVisualStyleBackColor = False
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.GroupBox4.Location = New System.Drawing.Point(2, 68)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(890, 1)
        Me.GroupBox4.TabIndex = 369
        Me.GroupBox4.TabStop = False
        '
        'frm_Stock_Analysis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.pnlShow)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.cbxSingleLedger)
        Me.Controls.Add(Me.rbTypeWise)
        Me.Controls.Add(Me.rbDepartmentWise)
        Me.Controls.Add(Me.rbCompanyWise)
        Me.Controls.Add(Me.rbSizeWise)
        Me.Controls.Add(Me.rbColorWise)
        Me.Controls.Add(Me.rbBrandWise)
        Me.Controls.Add(Me.rbCategoryWise)
        Me.Controls.Add(Me.rbItemWise)
        Me.Controls.Add(Me.txtSearchedItem)
        Me.Controls.Add(Me.btnLoadReport)
        Me.Controls.Add(Me.txtType)
        Me.Controls.Add(Me.txtDepartment)
        Me.Controls.Add(Me.txtCompany)
        Me.Controls.Add(Me.txtSize)
        Me.Controls.Add(Me.txtColor)
        Me.Controls.Add(Me.txtCategory)
        Me.Controls.Add(Me.ChkdLB_Category)
        Me.Controls.Add(Me.btn_AllCategory)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtBrand)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.ChkdLB_Type)
        Me.Controls.Add(Me.btn_AllType)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ChkdLB_Department)
        Me.Controls.Add(Me.btn_AllDepartment)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.ChkdLB_Company)
        Me.Controls.Add(Me.btn_AllCompany)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.ChkdLB_Size)
        Me.Controls.Add(Me.btn_AllSize)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.ChkdLB_Color)
        Me.Controls.Add(Me.btn_AllColor)
        Me.Controls.Add(Me.ChkdLB_Brand)
        Me.Controls.Add(Me.btn_AllBrand)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.lblHeading)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label26)
        Me.Name = "frm_Stock_Analysis"
        Me.Size = New System.Drawing.Size(910, 645)
        Me.pnlShow.ResumeLayout(False)
        Me.pnlShow.PerformLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label22 As Label
    Friend WithEvents Total As DataGridViewTextBoxColumn
    Friend WithEvents txtType As TextBox
    Friend WithEvents CESSAmount As DataGridViewTextBoxColumn
    Friend WithEvents colBtnRemove As DataGridViewButtonColumn
    Friend WithEvents txtDepartment As TextBox
    Friend WithEvents txtCompany As TextBox
    Friend WithEvents txtSize As TextBox
    Friend WithEvents txtColor As TextBox
    Friend WithEvents txtCategory As TextBox
    Friend WithEvents ChkdLB_Category As CheckedListBox
    Friend WithEvents btn_AllCategory As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents txtBrand As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents ChkdLB_Type As CheckedListBox
    Friend WithEvents btn_AllType As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents ChkdLB_Department As CheckedListBox
    Friend WithEvents btn_AllDepartment As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents ChkdLB_Company As CheckedListBox
    Friend WithEvents btn_AllCompany As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents ChkdLB_Size As CheckedListBox
    Friend WithEvents btn_AllSize As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents ChkdLB_Color As CheckedListBox
    Friend WithEvents btn_AllColor As Button
    Friend WithEvents ChkdLB_Brand As CheckedListBox
    Friend WithEvents btn_AllBrand As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dtpToDate As DateTimePicker
    Friend WithEvents dtpFromDate As DateTimePicker
    Friend WithEvents lblHeading As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents pnlShow As Panel
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents btnExport As Button
    Friend WithEvents dgvReport As DataGridView
    Friend WithEvents SNo As DataGridViewTextBoxColumn
    Friend WithEvents ItemName As DataGridViewTextBoxColumn
    Friend WithEvents GST As DataGridViewTextBoxColumn
    Friend WithEvents CESS As DataGridViewTextBoxColumn
    Friend WithEvents HSNCode As DataGridViewTextBoxColumn
    Friend WithEvents MRP As DataGridViewTextBoxColumn
    Friend WithEvents Qty As DataGridViewTextBoxColumn
    Friend WithEvents Rate As DataGridViewTextBoxColumn
    Friend WithEvents DiscPer As DataGridViewTextBoxColumn
    Friend WithEvents Discount As DataGridViewTextBoxColumn
    Friend WithEvents TAX As DataGridViewTextBoxColumn
    Friend WithEvents btnShowBack As Button
    Friend WithEvents btnLoadReport As Button
    Friend WithEvents txtSearchedItem As AutoCompleteTextBoxSample.AutoCompleteTextbox
    Friend WithEvents rbTypeWise As RadioButton
    Friend WithEvents rbDepartmentWise As RadioButton
    Friend WithEvents rbCompanyWise As RadioButton
    Friend WithEvents rbSizeWise As RadioButton
    Friend WithEvents rbColorWise As RadioButton
    Friend WithEvents rbBrandWise As RadioButton
    Friend WithEvents rbCategoryWise As RadioButton
    Friend WithEvents rbItemWise As RadioButton
    Friend WithEvents cbxSingleLedger As CheckBox
    Friend WithEvents GroupBox4 As GroupBox
End Class
