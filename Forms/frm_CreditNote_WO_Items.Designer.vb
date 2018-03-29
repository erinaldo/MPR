<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_CreditNote_WO_Items
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_CreditNote_WO_Items))
        Me.TbRMRN = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GBRMWPM = New System.Windows.Forms.GroupBox()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblTotalTaxAmt = New System.Windows.Forms.Label()
        Me.txt28Per = New System.Windows.Forms.TextBox()
        Me.lbl28Per = New System.Windows.Forms.Label()
        Me.txt18Per = New System.Windows.Forms.TextBox()
        Me.lbl18Per = New System.Windows.Forms.Label()
        Me.txt12Per = New System.Windows.Forms.TextBox()
        Me.lbl12Per = New System.Windows.Forms.Label()
        Me.txt5Per = New System.Windows.Forms.TextBox()
        Me.lblTaxAmt = New System.Windows.Forms.Label()
        Me.lbl5Per = New System.Windows.Forms.Label()
        Me.lbl3Per = New System.Windows.Forms.Label()
        Me.txt3Per = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtCreditAmt = New System.Windows.Forms.TextBox()
        Me.lblCreditAmt = New System.Windows.Forms.Label()
        Me.dtpRefDate = New System.Windows.Forms.DateTimePicker()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.lblCap2 = New System.Windows.Forms.Label()
        Me.cmbCustomer = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_CNDate = New System.Windows.Forms.Label()
        Me.lblCN_Code = New System.Windows.Forms.Label()
        Me.lblReverseCode = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.lbl_Remarks = New System.Windows.Forms.Label()
        Me.lblReceivedDate = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.lblSelectMRNNO = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TbRMRN.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GBRMWPM.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TbRMRN
        '
        Me.TbRMRN.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TbRMRN.Controls.Add(Me.TabPage1)
        Me.TbRMRN.Controls.Add(Me.TabPage2)
        Me.TbRMRN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TbRMRN.ImageList = Me.ImageList1
        Me.TbRMRN.Location = New System.Drawing.Point(0, 0)
        Me.TbRMRN.Name = "TbRMRN"
        Me.TbRMRN.SelectedIndex = 0
        Me.TbRMRN.Size = New System.Drawing.Size(910, 630)
        Me.TbRMRN.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.DimGray
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GBRMWPM)
        Me.TabPage1.ForeColor = System.Drawing.Color.White
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(902, 600)
        Me.TabPage1.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtSearch)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(17, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(864, 76)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
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
        Me.txtSearch.TabIndex = 17
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
        'GBRMWPM
        '
        Me.GBRMWPM.Controls.Add(Me.dgvList)
        Me.GBRMWPM.ForeColor = System.Drawing.Color.White
        Me.GBRMWPM.Location = New System.Drawing.Point(17, 108)
        Me.GBRMWPM.Name = "GBRMWPM"
        Me.GBRMWPM.Size = New System.Drawing.Size(867, 471)
        Me.GBRMWPM.TabIndex = 0
        Me.GBRMWPM.TabStop = False
        Me.GBRMWPM.Text = "List of Credit Notes"
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeColumns = False
        Me.dgvList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvList.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgvList.Location = New System.Drawing.Point(3, 16)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgvList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.dgvList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkOrange
        Me.dgvList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(861, 452)
        Me.dgvList.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.DimGray
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.ForeColor = System.Drawing.Color.White
        Me.TabPage2.ImageIndex = 1
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(902, 600)
        Me.TabPage2.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblTotalTaxAmt)
        Me.GroupBox2.Controls.Add(Me.txt28Per)
        Me.GroupBox2.Controls.Add(Me.lbl28Per)
        Me.GroupBox2.Controls.Add(Me.txt18Per)
        Me.GroupBox2.Controls.Add(Me.lbl18Per)
        Me.GroupBox2.Controls.Add(Me.txt12Per)
        Me.GroupBox2.Controls.Add(Me.lbl12Per)
        Me.GroupBox2.Controls.Add(Me.txt5Per)
        Me.GroupBox2.Controls.Add(Me.lblTaxAmt)
        Me.GroupBox2.Controls.Add(Me.lbl5Per)
        Me.GroupBox2.Controls.Add(Me.lbl3Per)
        Me.GroupBox2.Controls.Add(Me.txt3Per)
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(6, 319)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(890, 169)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tax Details"
        '
        'lblTotalTaxAmt
        '
        Me.lblTotalTaxAmt.AutoSize = True
        Me.lblTotalTaxAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalTaxAmt.Location = New System.Drawing.Point(157, 92)
        Me.lblTotalTaxAmt.Name = "lblTotalTaxAmt"
        Me.lblTotalTaxAmt.Size = New System.Drawing.Size(32, 13)
        Me.lblTotalTaxAmt.TabIndex = 288
        Me.lblTotalTaxAmt.Text = "0.00"
        '
        'txt28Per
        '
        Me.txt28Per.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt28Per.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt28Per.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt28Per.ForeColor = System.Drawing.Color.White
        Me.txt28Per.Location = New System.Drawing.Point(749, 50)
        Me.txt28Per.MaxLength = 0
        Me.txt28Per.Name = "txt28Per"
        Me.txt28Per.Size = New System.Drawing.Size(101, 19)
        Me.txt28Per.TabIndex = 287
        '
        'lbl28Per
        '
        Me.lbl28Per.AutoSize = True
        Me.lbl28Per.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl28Per.Location = New System.Drawing.Point(708, 51)
        Me.lbl28Per.Name = "lbl28Per"
        Me.lbl28Per.Size = New System.Drawing.Size(35, 15)
        Me.lbl28Per.TabIndex = 286
        Me.lbl28Per.Text = "28% "
        '
        'txt18Per
        '
        Me.txt18Per.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt18Per.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt18Per.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt18Per.ForeColor = System.Drawing.Color.White
        Me.txt18Per.Location = New System.Drawing.Point(586, 50)
        Me.txt18Per.MaxLength = 0
        Me.txt18Per.Name = "txt18Per"
        Me.txt18Per.Size = New System.Drawing.Size(101, 19)
        Me.txt18Per.TabIndex = 285
        '
        'lbl18Per
        '
        Me.lbl18Per.AutoSize = True
        Me.lbl18Per.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl18Per.Location = New System.Drawing.Point(550, 53)
        Me.lbl18Per.Name = "lbl18Per"
        Me.lbl18Per.Size = New System.Drawing.Size(35, 15)
        Me.lbl18Per.TabIndex = 284
        Me.lbl18Per.Text = "18% "
        '
        'txt12Per
        '
        Me.txt12Per.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt12Per.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt12Per.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt12Per.ForeColor = System.Drawing.Color.White
        Me.txt12Per.Location = New System.Drawing.Point(430, 49)
        Me.txt12Per.MaxLength = 0
        Me.txt12Per.Name = "txt12Per"
        Me.txt12Per.Size = New System.Drawing.Size(101, 19)
        Me.txt12Per.TabIndex = 283
        '
        'lbl12Per
        '
        Me.lbl12Per.AutoSize = True
        Me.lbl12Per.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl12Per.Location = New System.Drawing.Point(389, 49)
        Me.lbl12Per.Name = "lbl12Per"
        Me.lbl12Per.Size = New System.Drawing.Size(35, 15)
        Me.lbl12Per.TabIndex = 282
        Me.lbl12Per.Text = "12% "
        '
        'txt5Per
        '
        Me.txt5Per.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt5Per.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt5Per.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt5Per.ForeColor = System.Drawing.Color.White
        Me.txt5Per.Location = New System.Drawing.Point(285, 49)
        Me.txt5Per.MaxLength = 0
        Me.txt5Per.Name = "txt5Per"
        Me.txt5Per.Size = New System.Drawing.Size(101, 19)
        Me.txt5Per.TabIndex = 281
        '
        'lblTaxAmt
        '
        Me.lblTaxAmt.AutoSize = True
        Me.lblTaxAmt.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxAmt.Location = New System.Drawing.Point(38, 90)
        Me.lblTaxAmt.Name = "lblTaxAmt"
        Me.lblTaxAmt.Size = New System.Drawing.Size(104, 15)
        Me.lblTaxAmt.TabIndex = 277
        Me.lblTaxAmt.Text = "Total Tax Amount :"
        '
        'lbl5Per
        '
        Me.lbl5Per.AutoSize = True
        Me.lbl5Per.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl5Per.Location = New System.Drawing.Point(260, 49)
        Me.lbl5Per.Name = "lbl5Per"
        Me.lbl5Per.Size = New System.Drawing.Size(28, 15)
        Me.lbl5Per.TabIndex = 280
        Me.lbl5Per.Text = "5% "
        '
        'lbl3Per
        '
        Me.lbl3Per.AutoSize = True
        Me.lbl3Per.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl3Per.Location = New System.Drawing.Point(123, 51)
        Me.lbl3Per.Name = "lbl3Per"
        Me.lbl3Per.Size = New System.Drawing.Size(28, 15)
        Me.lbl3Per.TabIndex = 278
        Me.lbl3Per.Text = "3% "
        '
        'txt3Per
        '
        Me.txt3Per.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt3Per.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt3Per.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt3Per.ForeColor = System.Drawing.Color.White
        Me.txt3Per.Location = New System.Drawing.Point(153, 50)
        Me.txt3Per.MaxLength = 0
        Me.txt3Per.Name = "txt3Per"
        Me.txt3Per.Size = New System.Drawing.Size(101, 19)
        Me.txt3Per.TabIndex = 279
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtCreditAmt)
        Me.GroupBox1.Controls.Add(Me.lblCreditAmt)
        Me.GroupBox1.Controls.Add(Me.dtpRefDate)
        Me.GroupBox1.Controls.Add(Me.txtRefNo)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblAddress)
        Me.GroupBox1.Controls.Add(Me.lblCap2)
        Me.GroupBox1.Controls.Add(Me.cmbCustomer)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lbl_CNDate)
        Me.GroupBox1.Controls.Add(Me.lblCN_Code)
        Me.GroupBox1.Controls.Add(Me.lblReverseCode)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.lbl_Remarks)
        Me.GroupBox1.Controls.Add(Me.lblReceivedDate)
        Me.GroupBox1.Controls.Add(Me.lblFormHeading)
        Me.GroupBox1.Controls.Add(Me.lblSelectMRNNO)
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(890, 296)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Credit Note Detail"
        '
        'txtCreditAmt
        '
        Me.txtCreditAmt.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtCreditAmt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCreditAmt.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditAmt.ForeColor = System.Drawing.Color.White
        Me.txtCreditAmt.Location = New System.Drawing.Point(154, 258)
        Me.txtCreditAmt.MaxLength = 0
        Me.txtCreditAmt.Name = "txtCreditAmt"
        Me.txtCreditAmt.Size = New System.Drawing.Size(587, 19)
        Me.txtCreditAmt.TabIndex = 265
        '
        'lblCreditAmt
        '
        Me.lblCreditAmt.AutoSize = True
        Me.lblCreditAmt.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditAmt.Location = New System.Drawing.Point(34, 258)
        Me.lblCreditAmt.Name = "lblCreditAmt"
        Me.lblCreditAmt.Size = New System.Drawing.Size(90, 15)
        Me.lblCreditAmt.TabIndex = 264
        Me.lblCreditAmt.Text = "Credit Amount :"
        '
        'dtpRefDate
        '
        Me.dtpRefDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpRefDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpRefDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpRefDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRefDate.Location = New System.Drawing.Point(536, 90)
        Me.dtpRefDate.Name = "dtpRefDate"
        Me.dtpRefDate.Size = New System.Drawing.Size(205, 20)
        Me.dtpRefDate.TabIndex = 262
        '
        'txtRefNo
        '
        Me.txtRefNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRefNo.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.Color.White
        Me.txtRefNo.Location = New System.Drawing.Point(154, 91)
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.Size = New System.Drawing.Size(222, 19)
        Me.txtRefNo.TabIndex = 260
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(392, 92)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 15)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Reference Date :"
        '
        'lblAddress
        '
        Me.lblAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.Location = New System.Drawing.Point(154, 62)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(587, 22)
        Me.lblAddress.TabIndex = 32
        '
        'lblCap2
        '
        Me.lblCap2.AutoSize = True
        Me.lblCap2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap2.Location = New System.Drawing.Point(37, 65)
        Me.lblCap2.Name = "lblCap2"
        Me.lblCap2.Size = New System.Drawing.Size(59, 15)
        Me.lblCap2.TabIndex = 31
        Me.lblCap2.Text = "Address :"
        '
        'cmbCustomer
        '
        Me.cmbCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCustomer.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCustomer.ForeColor = System.Drawing.Color.White
        Me.cmbCustomer.FormattingEnabled = True
        Me.cmbCustomer.Location = New System.Drawing.Point(154, 35)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(587, 23)
        Me.cmbCustomer.TabIndex = 30
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(37, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 15)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Select Customer :"
        '
        'lbl_CNDate
        '
        Me.lbl_CNDate.AutoSize = True
        Me.lbl_CNDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CNDate.Location = New System.Drawing.Point(417, 10)
        Me.lbl_CNDate.Name = "lbl_CNDate"
        Me.lbl_CNDate.Size = New System.Drawing.Size(86, 13)
        Me.lbl_CNDate.TabIndex = 26
        Me.lbl_CNDate.Text = "Credit Note Date"
        '
        'lblCN_Code
        '
        Me.lblCN_Code.AutoSize = True
        Me.lblCN_Code.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCN_Code.Location = New System.Drawing.Point(151, 12)
        Me.lblCN_Code.Name = "lblCN_Code"
        Me.lblCN_Code.Size = New System.Drawing.Size(88, 13)
        Me.lblCN_Code.TabIndex = 16
        Me.lblCN_Code.Text = "Credit Note Code"
        '
        'lblReverseCode
        '
        Me.lblReverseCode.AutoSize = True
        Me.lblReverseCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReverseCode.Location = New System.Drawing.Point(37, 16)
        Me.lblReverseCode.Name = "lblReverseCode"
        Me.lblReverseCode.Size = New System.Drawing.Size(50, 15)
        Me.lblReverseCode.TabIndex = 15
        Me.lblReverseCode.Text = "CN No :"
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.White
        Me.txtRemarks.Location = New System.Drawing.Point(154, 119)
        Me.txtRemarks.MaxLength = 500
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks.Size = New System.Drawing.Size(587, 122)
        Me.txtRemarks.TabIndex = 14
        '
        'lbl_Remarks
        '
        Me.lbl_Remarks.AutoSize = True
        Me.lbl_Remarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Remarks.Location = New System.Drawing.Point(37, 122)
        Me.lbl_Remarks.Name = "lbl_Remarks"
        Me.lbl_Remarks.Size = New System.Drawing.Size(64, 15)
        Me.lbl_Remarks.TabIndex = 13
        Me.lbl_Remarks.Text = "Remarks :"
        '
        'lblReceivedDate
        '
        Me.lblReceivedDate.AutoSize = True
        Me.lblReceivedDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceivedDate.Location = New System.Drawing.Point(330, 10)
        Me.lblReceivedDate.Name = "lblReceivedDate"
        Me.lblReceivedDate.Size = New System.Drawing.Size(60, 15)
        Me.lblReceivedDate.TabIndex = 11
        Me.lblReceivedDate.Text = "CN Date :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(747, 56)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(139, 96)
        Me.lblFormHeading.TabIndex = 10
        Me.lblFormHeading.Text = "Credit Note WithOut Items"
        Me.lblFormHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblSelectMRNNO
        '
        Me.lblSelectMRNNO.AutoSize = True
        Me.lblSelectMRNNO.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectMRNNO.Location = New System.Drawing.Point(37, 93)
        Me.lblSelectMRNNO.Name = "lblSelectMRNNO"
        Me.lblSelectMRNNO.Size = New System.Drawing.Size(89, 15)
        Me.lblSelectMRNNO.TabIndex = 0
        Me.lblSelectMRNNO.Text = "Reference No :"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'C1FlexGrid1
        '
        Me.C1FlexGrid1.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.C1FlexGrid1.Location = New System.Drawing.Point(0, 0)
        Me.C1FlexGrid1.Name = "C1FlexGrid1"
        Me.C1FlexGrid1.Rows.DefaultSize = 17
        Me.C1FlexGrid1.Size = New System.Drawing.Size(0, 0)
        Me.C1FlexGrid1.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("C1FlexGrid1.Styles"))
        Me.C1FlexGrid1.TabIndex = 0
        '
        'frm_CreditNote_WO_Items
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TbRMRN)
        Me.Name = "frm_CreditNote_WO_Items"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TbRMRN.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GBRMWPM.ResumeLayout(False)
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TbRMRN As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSelectMRNNO As System.Windows.Forms.Label
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents lbl_Remarks As System.Windows.Forms.Label
    Friend WithEvents lblReceivedDate As System.Windows.Forms.Label
    Friend WithEvents lblCN_Code As System.Windows.Forms.Label
    Friend WithEvents lblReverseCode As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents GBRMWPM As System.Windows.Forms.GroupBox
    Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lbl_CNDate As System.Windows.Forms.Label
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents lblCap2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtRefNo As TextBox
    Friend WithEvents dtpRefDate As DateTimePicker
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtCreditAmt As TextBox
    Friend WithEvents lblCreditAmt As Label
    Friend WithEvents lblTotalTaxAmt As Label
    Friend WithEvents txt28Per As TextBox
    Friend WithEvents lbl28Per As Label
    Friend WithEvents txt18Per As TextBox
    Friend WithEvents lbl18Per As Label
    Friend WithEvents txt12Per As TextBox
    Friend WithEvents lbl12Per As Label
    Friend WithEvents txt5Per As TextBox
    Friend WithEvents lblTaxAmt As Label
    Friend WithEvents lbl5Per As Label
    Friend WithEvents lbl3Per As Label
    Friend WithEvents txt3Per As TextBox
End Class
