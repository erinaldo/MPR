<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Open_PO_Master
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Open_PO_Master))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.cmdPOStatusSearch = New System.Windows.Forms.ComboBox()
        Me.chkPODate = New System.Windows.Forms.CheckBox()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.chkPONumber = New System.Windows.Forms.CheckBox()
        Me.chkSupplier = New System.Windows.Forms.CheckBox()
        Me.txtPODateSearch = New System.Windows.Forms.DateTimePicker()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.ddlSupplierSearch = New System.Windows.Forms.ComboBox()
        Me.txtPONumberSearch = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.grdOpenPOList = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.txtPaymentTerms = New System.Windows.Forms.TextBox()
        Me.lblExiceAmt = New System.Windows.Forms.Label()
        Me.txtPORemarks = New System.Windows.Forms.TextBox()
        Me.lblVatCst = New System.Windows.Forms.Label()
        Me.lblItemValue = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblCap9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpTransMode = New System.Windows.Forms.ComboBox()
        Me.dtpPriceBasis = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpFreight = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblCap6 = New System.Windows.Forms.Label()
        Me.dtpOctroi = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCess = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtOtherCharges = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grdOpenPoMaster = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbPOType = New System.Windows.Forms.ComboBox()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpPODate = New System.Windows.Forms.DateTimePicker()
        Me.cmbDeliveryRate = New System.Windows.Forms.ComboBox()
        Me.cmbQualityRate = New System.Windows.Forms.ComboBox()
        Me.cmbSupplier = New System.Windows.Forms.ComboBox()
        Me.lblCap10 = New System.Windows.Forms.Label()
        Me.lblCap8 = New System.Windows.Forms.Label()
        Me.lblCap7 = New System.Windows.Forms.Label()
        Me.lblCap4 = New System.Windows.Forms.Label()
        Me.lblCap = New System.Windows.Forms.Label()
        Me.lblCap5 = New System.Windows.Forms.Label()
        Me.lblCap1 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.erp = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.txtDiscountAmount = New System.Windows.Forms.TextBox()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.flxItemList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdOpenPOList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdOpenPoMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.erp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.flxItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox7.SuspendLayout()
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
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(902, 600)
        Me.TabPage1.TabIndex = 0
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.cmdPOStatusSearch)
        Me.GroupBox6.Controls.Add(Me.chkPODate)
        Me.GroupBox6.Controls.Add(Me.chkStatus)
        Me.GroupBox6.Controls.Add(Me.chkPONumber)
        Me.GroupBox6.Controls.Add(Me.chkSupplier)
        Me.GroupBox6.Controls.Add(Me.txtPODateSearch)
        Me.GroupBox6.Controls.Add(Me.btnShow)
        Me.GroupBox6.Controls.Add(Me.ddlSupplierSearch)
        Me.GroupBox6.Controls.Add(Me.txtPONumberSearch)
        Me.GroupBox6.Controls.Add(Me.Label16)
        Me.GroupBox6.Controls.Add(Me.Label15)
        Me.GroupBox6.Controls.Add(Me.Label14)
        Me.GroupBox6.Controls.Add(Me.Label13)
        Me.GroupBox6.ForeColor = System.Drawing.Color.White
        Me.GroupBox6.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(890, 92)
        Me.GroupBox6.TabIndex = 1
        Me.GroupBox6.TabStop = False
        '
        'cmdPOStatusSearch
        '
        Me.cmdPOStatusSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdPOStatusSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPOStatusSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPOStatusSearch.ForeColor = System.Drawing.Color.White
        Me.cmdPOStatusSearch.FormattingEnabled = True
        Me.cmdPOStatusSearch.Location = New System.Drawing.Point(479, 54)
        Me.cmdPOStatusSearch.Name = "cmdPOStatusSearch"
        Me.cmdPOStatusSearch.Size = New System.Drawing.Size(211, 25)
        Me.cmdPOStatusSearch.TabIndex = 14
        '
        'chkPODate
        '
        Me.chkPODate.AutoSize = True
        Me.chkPODate.Location = New System.Drawing.Point(17, 55)
        Me.chkPODate.Name = "chkPODate"
        Me.chkPODate.Size = New System.Drawing.Size(15, 14)
        Me.chkPODate.TabIndex = 13
        Me.chkPODate.UseVisualStyleBackColor = True
        '
        'chkStatus
        '
        Me.chkStatus.AutoSize = True
        Me.chkStatus.Location = New System.Drawing.Point(378, 59)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.Size = New System.Drawing.Size(15, 14)
        Me.chkStatus.TabIndex = 12
        Me.chkStatus.UseVisualStyleBackColor = True
        '
        'chkPONumber
        '
        Me.chkPONumber.AutoSize = True
        Me.chkPONumber.Location = New System.Drawing.Point(378, 28)
        Me.chkPONumber.Name = "chkPONumber"
        Me.chkPONumber.Size = New System.Drawing.Size(15, 14)
        Me.chkPONumber.TabIndex = 11
        Me.chkPONumber.UseVisualStyleBackColor = True
        '
        'chkSupplier
        '
        Me.chkSupplier.AutoSize = True
        Me.chkSupplier.Location = New System.Drawing.Point(17, 28)
        Me.chkSupplier.Name = "chkSupplier"
        Me.chkSupplier.Size = New System.Drawing.Size(15, 14)
        Me.chkSupplier.TabIndex = 10
        Me.chkSupplier.UseVisualStyleBackColor = True
        '
        'txtPODateSearch
        '
        Me.txtPODateSearch.CalendarForeColor = System.Drawing.Color.White
        Me.txtPODateSearch.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPODateSearch.CustomFormat = "dd-MMM-yyyy"
        Me.txtPODateSearch.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtPODateSearch.Location = New System.Drawing.Point(102, 55)
        Me.txtPODateSearch.Name = "txtPODateSearch"
        Me.txtPODateSearch.Size = New System.Drawing.Size(142, 20)
        Me.txtPODateSearch.TabIndex = 9
        '
        'btnShow
        '
        Me.btnShow.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.Location = New System.Drawing.Point(773, 50)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(84, 27)
        Me.btnShow.TabIndex = 8
        Me.btnShow.Text = "SHOW"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'ddlSupplierSearch
        '
        Me.ddlSupplierSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ddlSupplierSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ddlSupplierSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddlSupplierSearch.ForeColor = System.Drawing.Color.White
        Me.ddlSupplierSearch.FormattingEnabled = True
        Me.ddlSupplierSearch.Location = New System.Drawing.Point(102, 25)
        Me.ddlSupplierSearch.Name = "ddlSupplierSearch"
        Me.ddlSupplierSearch.Size = New System.Drawing.Size(251, 25)
        Me.ddlSupplierSearch.TabIndex = 7
        '
        'txtPONumberSearch
        '
        Me.txtPONumberSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPONumberSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPONumberSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONumberSearch.ForeColor = System.Drawing.Color.White
        Me.txtPONumberSearch.Location = New System.Drawing.Point(479, 25)
        Me.txtPONumberSearch.Name = "txtPONumberSearch"
        Me.txtPONumberSearch.Size = New System.Drawing.Size(211, 18)
        Me.txtPONumberSearch.TabIndex = 6
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(405, 56)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(65, 15)
        Me.Label16.TabIndex = 3
        Me.Label16.Text = "PO Status:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(398, 28)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(75, 15)
        Me.Label15.TabIndex = 2
        Me.Label15.Text = "PO Number:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(36, 56)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(56, 15)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "PO Date:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(38, 28)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 15)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Supplier:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.grdOpenPOList)
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.White
        Me.GroupBox3.Location = New System.Drawing.Point(6, 99)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(890, 495)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "List of Open PO"
        '
        'grdOpenPOList
        '
        Me.grdOpenPOList.AllowUserToAddRows = False
        Me.grdOpenPOList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdOpenPOList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdOpenPOList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdOpenPOList.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdOpenPOList.Location = New System.Drawing.Point(3, 17)
        Me.grdOpenPOList.Name = "grdOpenPOList"
        Me.grdOpenPOList.ReadOnly = True
        Me.grdOpenPOList.RowHeadersVisible = False
        Me.grdOpenPOList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdOpenPOList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOpenPOList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdOpenPOList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkOrange
        Me.grdOpenPOList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdOpenPOList.Size = New System.Drawing.Size(884, 475)
        Me.grdOpenPOList.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.DimGray
        Me.TabPage2.Controls.Add(Me.GroupBox7)
        Me.TabPage2.Controls.Add(Me.GroupBox4)
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
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Panel5)
        Me.GroupBox4.Controls.Add(Me.Panel4)
        Me.GroupBox4.Controls.Add(Me.Panel3)
        Me.GroupBox4.Controls.Add(Me.Panel1)
        Me.GroupBox4.Controls.Add(Me.Panel2)
        Me.GroupBox4.Controls.Add(Me.lblNetAmount)
        Me.GroupBox4.Controls.Add(Me.txtPaymentTerms)
        Me.GroupBox4.Controls.Add(Me.lblExiceAmt)
        Me.GroupBox4.Controls.Add(Me.txtPORemarks)
        Me.GroupBox4.Controls.Add(Me.lblVatCst)
        Me.GroupBox4.Controls.Add(Me.lblItemValue)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.lblCap9)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.dtpTransMode)
        Me.GroupBox4.Controls.Add(Me.dtpPriceBasis)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.dtpFreight)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.lblCap6)
        Me.GroupBox4.Controls.Add(Me.dtpOctroi)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.txtCess)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.txtOtherCharges)
        Me.GroupBox4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.White
        Me.GroupBox4.Location = New System.Drawing.Point(15, 405)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(874, 183)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Other Details"
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Location = New System.Drawing.Point(677, 38)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(110, 1)
        Me.Panel5.TabIndex = 58
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Location = New System.Drawing.Point(525, 20)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1, 145)
        Me.Panel4.TabIndex = 57
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Location = New System.Drawing.Point(676, 77)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(110, 1)
        Me.Panel3.TabIndex = 56
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Location = New System.Drawing.Point(676, 157)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(110, 1)
        Me.Panel1.TabIndex = 55
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Location = New System.Drawing.Point(676, 61)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(110, 1)
        Me.Panel2.TabIndex = 54
        '
        'lblNetAmount
        '
        Me.lblNetAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.Orange
        Me.lblNetAmount.Location = New System.Drawing.Point(740, 138)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.Size = New System.Drawing.Size(103, 20)
        Me.lblNetAmount.TabIndex = 53
        Me.lblNetAmount.Text = "0.00"
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPaymentTerms
        '
        Me.txtPaymentTerms.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPaymentTerms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPaymentTerms.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentTerms.ForeColor = System.Drawing.Color.White
        Me.txtPaymentTerms.Location = New System.Drawing.Point(128, 125)
        Me.txtPaymentTerms.Multiline = True
        Me.txtPaymentTerms.Name = "txtPaymentTerms"
        Me.txtPaymentTerms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPaymentTerms.Size = New System.Drawing.Size(370, 41)
        Me.txtPaymentTerms.TabIndex = 9
        '
        'lblExiceAmt
        '
        Me.lblExiceAmt.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExiceAmt.ForeColor = System.Drawing.Color.Orange
        Me.lblExiceAmt.Location = New System.Drawing.Point(740, 58)
        Me.lblExiceAmt.Name = "lblExiceAmt"
        Me.lblExiceAmt.Size = New System.Drawing.Size(103, 20)
        Me.lblExiceAmt.TabIndex = 52
        Me.lblExiceAmt.Text = "0.00"
        Me.lblExiceAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPORemarks
        '
        Me.txtPORemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPORemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPORemarks.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPORemarks.ForeColor = System.Drawing.Color.White
        Me.txtPORemarks.Location = New System.Drawing.Point(128, 77)
        Me.txtPORemarks.Multiline = True
        Me.txtPORemarks.Name = "txtPORemarks"
        Me.txtPORemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPORemarks.Size = New System.Drawing.Size(370, 39)
        Me.txtPORemarks.TabIndex = 8
        '
        'lblVatCst
        '
        Me.lblVatCst.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVatCst.ForeColor = System.Drawing.Color.Orange
        Me.lblVatCst.Location = New System.Drawing.Point(740, 38)
        Me.lblVatCst.Name = "lblVatCst"
        Me.lblVatCst.Size = New System.Drawing.Size(103, 20)
        Me.lblVatCst.TabIndex = 51
        Me.lblVatCst.Text = "0.00"
        Me.lblVatCst.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblItemValue
        '
        Me.lblItemValue.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemValue.ForeColor = System.Drawing.Color.Orange
        Me.lblItemValue.Location = New System.Drawing.Point(740, 15)
        Me.lblItemValue.Name = "lblItemValue"
        Me.lblItemValue.Size = New System.Drawing.Size(103, 20)
        Me.lblItemValue.TabIndex = 50
        Me.lblItemValue.Text = "0.00"
        Me.lblItemValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(721, 448)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(31, 15)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "0.00"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCap9
        '
        Me.lblCap9.AutoSize = True
        Me.lblCap9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap9.Location = New System.Drawing.Point(3, 138)
        Me.lblCap9.Name = "lblCap9"
        Me.lblCap9.Size = New System.Drawing.Size(99, 15)
        Me.lblCap9.TabIndex = 17
        Me.lblCap9.Text = "Payment Terms :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(632, 143)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(76, 15)
        Me.Label11.TabIndex = 49
        Me.Label11.Text = "Net Amount :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(2, 49)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 15)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Transport Mode :"
        '
        'dtpTransMode
        '
        Me.dtpTransMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpTransMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dtpTransMode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTransMode.ForeColor = System.Drawing.Color.White
        Me.dtpTransMode.FormattingEnabled = True
        Me.dtpTransMode.Items.AddRange(New Object() {"On Party behalf", "On Ourbehalf"})
        Me.dtpTransMode.Location = New System.Drawing.Point(110, 46)
        Me.dtpTransMode.Name = "dtpTransMode"
        Me.dtpTransMode.Size = New System.Drawing.Size(154, 23)
        Me.dtpTransMode.TabIndex = 2
        '
        'dtpPriceBasis
        '
        Me.dtpPriceBasis.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpPriceBasis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dtpPriceBasis.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPriceBasis.ForeColor = System.Drawing.Color.White
        Me.dtpPriceBasis.FormattingEnabled = True
        Me.dtpPriceBasis.Items.AddRange(New Object() {"Ex-works,Godown", "F.O.R Destination"})
        Me.dtpPriceBasis.Location = New System.Drawing.Point(344, 47)
        Me.dtpPriceBasis.Name = "dtpPriceBasis"
        Me.dtpPriceBasis.Size = New System.Drawing.Size(154, 23)
        Me.dtpPriceBasis.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(225, 49)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 15)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Price Basis :"
        '
        'dtpFreight
        '
        Me.dtpFreight.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpFreight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dtpFreight.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFreight.ForeColor = System.Drawing.Color.White
        Me.dtpFreight.FormattingEnabled = True
        Me.dtpFreight.Items.AddRange(New Object() {"By Us", "By You"})
        Me.dtpFreight.Location = New System.Drawing.Point(344, 20)
        Me.dtpFreight.Name = "dtpFreight"
        Me.dtpFreight.Size = New System.Drawing.Size(154, 23)
        Me.dtpFreight.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(632, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 15)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "Vat/Cst :"
        '
        'lblCap6
        '
        Me.lblCap6.AutoSize = True
        Me.lblCap6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap6.Location = New System.Drawing.Point(19, 91)
        Me.lblCap6.Name = "lblCap6"
        Me.lblCap6.Size = New System.Drawing.Size(84, 15)
        Me.lblCap6.TabIndex = 15
        Me.lblCap6.Text = "PO Remarks :"
        '
        'dtpOctroi
        '
        Me.dtpOctroi.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpOctroi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dtpOctroi.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpOctroi.ForeColor = System.Drawing.Color.White
        Me.dtpOctroi.FormattingEnabled = True
        Me.dtpOctroi.Items.AddRange(New Object() {"By Us", "By You"})
        Me.dtpOctroi.Location = New System.Drawing.Point(110, 19)
        Me.dtpOctroi.Name = "dtpOctroi"
        Me.dtpOctroi.Size = New System.Drawing.Size(154, 23)
        Me.dtpOctroi.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(632, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 15)
        Me.Label10.TabIndex = 48
        Me.Label10.Text = "Item Value :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(249, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 15)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Freight :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(632, 65)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 15)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "Exice Amt. :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(55, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 15)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Octroi :"
        '
        'txtCess
        '
        Me.txtCess.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtCess.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCess.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCess.ForeColor = System.Drawing.Color.White
        Me.txtCess.Location = New System.Drawing.Point(734, 88)
        Me.txtCess.Name = "txtCess"
        Me.txtCess.Size = New System.Drawing.Size(109, 18)
        Me.txtCess.TabIndex = 7
        Me.txtCess.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(632, 88)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 15)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "Cess % :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(632, 116)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(94, 15)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "Other Charges :"
        '
        'txtOtherCharges
        '
        Me.txtOtherCharges.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtOtherCharges.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtOtherCharges.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtherCharges.ForeColor = System.Drawing.Color.White
        Me.txtOtherCharges.Location = New System.Drawing.Point(734, 116)
        Me.txtOtherCharges.Name = "txtOtherCharges"
        Me.txtOtherCharges.Size = New System.Drawing.Size(109, 18)
        Me.txtOtherCharges.TabIndex = 8
        Me.txtOtherCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdOpenPoMaster)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(15, 139)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(874, 259)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Item List"
        '
        'grdOpenPoMaster
        '
        Me.grdOpenPoMaster.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdOpenPoMaster.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdOpenPoMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdOpenPoMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdOpenPoMaster.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdOpenPoMaster.Location = New System.Drawing.Point(3, 17)
        Me.grdOpenPoMaster.Name = "grdOpenPoMaster"
        Me.grdOpenPoMaster.RowHeadersVisible = False
        Me.grdOpenPoMaster.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdOpenPoMaster.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdOpenPoMaster.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdOpenPoMaster.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkOrange
        Me.grdOpenPoMaster.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdOpenPoMaster.Size = New System.Drawing.Size(868, 239)
        Me.grdOpenPoMaster.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtPONo)
        Me.GroupBox1.Controls.Add(Me.lblMsg)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbPOType)
        Me.GroupBox1.Controls.Add(Me.lblFormHeading)
        Me.GroupBox1.Controls.Add(Me.dtpEndDate)
        Me.GroupBox1.Controls.Add(Me.dtpStartDate)
        Me.GroupBox1.Controls.Add(Me.dtpPODate)
        Me.GroupBox1.Controls.Add(Me.cmbDeliveryRate)
        Me.GroupBox1.Controls.Add(Me.cmbQualityRate)
        Me.GroupBox1.Controls.Add(Me.cmbSupplier)
        Me.GroupBox1.Controls.Add(Me.lblCap10)
        Me.GroupBox1.Controls.Add(Me.lblCap8)
        Me.GroupBox1.Controls.Add(Me.lblCap7)
        Me.GroupBox1.Controls.Add(Me.lblCap4)
        Me.GroupBox1.Controls.Add(Me.lblCap)
        Me.GroupBox1.Controls.Add(Me.lblCap5)
        Me.GroupBox1.Controls.Add(Me.lblCap1)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(15, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(874, 127)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "PO Details"
        '
        'txtPONo
        '
        Me.txtPONo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPONo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.Location = New System.Drawing.Point(89, 21)
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.ReadOnly = True
        Me.txtPONo.Size = New System.Drawing.Size(123, 18)
        Me.txtPONo.TabIndex = 0
        '
        'lblMsg
        '
        Me.lblMsg.AutoSize = True
        Me.lblMsg.Location = New System.Drawing.Point(643, 16)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(0, 15)
        Me.lblMsg.TabIndex = 47
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(218, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 15)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "PO Type:"
        '
        'cmbPOType
        '
        Me.cmbPOType.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbPOType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPOType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPOType.ForeColor = System.Drawing.Color.White
        Me.cmbPOType.FormattingEnabled = True
        Me.cmbPOType.Location = New System.Drawing.Point(300, 18)
        Me.cmbPOType.Name = "cmbPOType"
        Me.cmbPOType.Size = New System.Drawing.Size(156, 25)
        Me.cmbPOType.TabIndex = 1
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.BackColor = System.Drawing.Color.Transparent
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(687, 11)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(186, 50)
        Me.lblFormHeading.TabIndex = 27
        Me.lblFormHeading.Text = "Open" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Purchase Order"
        Me.lblFormHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpEndDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpEndDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(556, 84)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(113, 21)
        Me.dtpEndDate.TabIndex = 5
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpStartDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpStartDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(556, 55)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(113, 21)
        Me.dtpStartDate.TabIndex = 4
        '
        'dtpPODate
        '
        Me.dtpPODate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpPODate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpPODate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPODate.Location = New System.Drawing.Point(556, 28)
        Me.dtpPODate.Name = "dtpPODate"
        Me.dtpPODate.Size = New System.Drawing.Size(113, 21)
        Me.dtpPODate.TabIndex = 3
        '
        'cmbDeliveryRate
        '
        Me.cmbDeliveryRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbDeliveryRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDeliveryRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDeliveryRate.ForeColor = System.Drawing.Color.White
        Me.cmbDeliveryRate.FormattingEnabled = True
        Me.cmbDeliveryRate.Location = New System.Drawing.Point(300, 84)
        Me.cmbDeliveryRate.Name = "cmbDeliveryRate"
        Me.cmbDeliveryRate.Size = New System.Drawing.Size(156, 25)
        Me.cmbDeliveryRate.TabIndex = 7
        '
        'cmbQualityRate
        '
        Me.cmbQualityRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbQualityRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbQualityRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbQualityRate.ForeColor = System.Drawing.Color.White
        Me.cmbQualityRate.FormattingEnabled = True
        Me.cmbQualityRate.Location = New System.Drawing.Point(89, 84)
        Me.cmbQualityRate.Name = "cmbQualityRate"
        Me.cmbQualityRate.Size = New System.Drawing.Size(123, 25)
        Me.cmbQualityRate.TabIndex = 6
        '
        'cmbSupplier
        '
        Me.cmbSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSupplier.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSupplier.ForeColor = System.Drawing.Color.White
        Me.cmbSupplier.FormattingEnabled = True
        Me.cmbSupplier.Location = New System.Drawing.Point(89, 50)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Size = New System.Drawing.Size(367, 25)
        Me.cmbSupplier.TabIndex = 2
        '
        'lblCap10
        '
        Me.lblCap10.AutoSize = True
        Me.lblCap10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap10.Location = New System.Drawing.Point(218, 89)
        Me.lblCap10.Name = "lblCap10"
        Me.lblCap10.Size = New System.Drawing.Size(85, 15)
        Me.lblCap10.TabIndex = 16
        Me.lblCap10.Text = "Delivery Rate :"
        '
        'lblCap8
        '
        Me.lblCap8.AutoSize = True
        Me.lblCap8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap8.Location = New System.Drawing.Point(487, 89)
        Me.lblCap8.Name = "lblCap8"
        Me.lblCap8.Size = New System.Drawing.Size(64, 15)
        Me.lblCap8.TabIndex = 9
        Me.lblCap8.Text = "End Date :"
        '
        'lblCap7
        '
        Me.lblCap7.AutoSize = True
        Me.lblCap7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap7.Location = New System.Drawing.Point(487, 60)
        Me.lblCap7.Name = "lblCap7"
        Me.lblCap7.Size = New System.Drawing.Size(67, 15)
        Me.lblCap7.TabIndex = 7
        Me.lblCap7.Text = "Start Date :"
        '
        'lblCap4
        '
        Me.lblCap4.AutoSize = True
        Me.lblCap4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap4.Location = New System.Drawing.Point(487, 33)
        Me.lblCap4.Name = "lblCap4"
        Me.lblCap4.Size = New System.Drawing.Size(59, 15)
        Me.lblCap4.TabIndex = 5
        Me.lblCap4.Text = "PO Date :"
        '
        'lblCap
        '
        Me.lblCap.AutoSize = True
        Me.lblCap.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap.Location = New System.Drawing.Point(11, 23)
        Me.lblCap.Name = "lblCap"
        Me.lblCap.Size = New System.Drawing.Size(46, 15)
        Me.lblCap.TabIndex = 13
        Me.lblCap.Text = "PO No:"
        '
        'lblCap5
        '
        Me.lblCap5.AutoSize = True
        Me.lblCap5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap5.Location = New System.Drawing.Point(11, 89)
        Me.lblCap5.Name = "lblCap5"
        Me.lblCap5.Size = New System.Drawing.Size(79, 15)
        Me.lblCap5.TabIndex = 11
        Me.lblCap5.Text = "Quality Rate :"
        '
        'lblCap1
        '
        Me.lblCap1.AutoSize = True
        Me.lblCap1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCap1.Location = New System.Drawing.Point(11, 55)
        Me.lblCap1.Name = "lblCap1"
        Me.lblCap1.Size = New System.Drawing.Size(59, 15)
        Me.lblCap1.TabIndex = 8
        Me.lblCap1.Text = "Supplier :"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'erp
        '
        Me.erp.ContainerControl = Me
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.Transparent
        Me.TabPage3.Controls.Add(Me.Panel6)
        Me.TabPage3.Controls.Add(Me.Panel7)
        Me.TabPage3.Controls.Add(Me.Panel8)
        Me.TabPage3.Controls.Add(Me.Panel9)
        Me.TabPage3.Controls.Add(Me.Panel10)
        Me.TabPage3.Controls.Add(Me.TextBox1)
        Me.TabPage3.Controls.Add(Me.txtDiscountAmount)
        Me.TabPage3.Controls.Add(Me.Panel11)
        Me.TabPage3.Controls.Add(Me.TextBox2)
        Me.TabPage3.Location = New System.Drawing.Point(0, 0)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(200, 100)
        Me.TabPage3.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Location = New System.Drawing.Point(722, 552)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(117, 1)
        Me.Panel6.TabIndex = 13
        '
        'Panel7
        '
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Location = New System.Drawing.Point(722, 579)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(117, 1)
        Me.Panel7.TabIndex = 13
        '
        'Panel8
        '
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Location = New System.Drawing.Point(722, 525)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(117, 1)
        Me.Panel8.TabIndex = 13
        '
        'Panel9
        '
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Location = New System.Drawing.Point(722, 498)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(117, 1)
        Me.Panel9.TabIndex = 13
        '
        'Panel10
        '
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel10.Location = New System.Drawing.Point(722, 468)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(117, 1)
        Me.Panel10.TabIndex = 13
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(721, 503)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(118, 20)
        Me.TextBox1.TabIndex = 4
        Me.TextBox1.Text = "0.00"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDiscountAmount
        '
        Me.txtDiscountAmount.Location = New System.Drawing.Point(721, 529)
        Me.txtDiscountAmount.Name = "txtDiscountAmount"
        Me.txtDiscountAmount.Size = New System.Drawing.Size(118, 20)
        Me.txtDiscountAmount.TabIndex = 5
        Me.txtDiscountAmount.Text = "0.00"
        Me.txtDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel11
        '
        Me.Panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel11.Location = New System.Drawing.Point(573, 438)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(1, 145)
        Me.Panel11.TabIndex = 11
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(123, 531)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox2.Size = New System.Drawing.Size(425, 44)
        Me.TextBox2.TabIndex = 3
        '
        'GroupBox5
        '
        Me.GroupBox5.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(200, 100)
        Me.GroupBox5.TabIndex = 0
        Me.GroupBox5.TabStop = False
        '
        'flxItemList
        '
        Me.flxItemList.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.flxItemList.Location = New System.Drawing.Point(0, 0)
        Me.flxItemList.Name = "flxItemList"
        Me.flxItemList.Rows.DefaultSize = 17
        Me.flxItemList.Size = New System.Drawing.Size(0, 0)
        Me.flxItemList.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("flxItemList.Styles"))
        Me.flxItemList.TabIndex = 0
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.Panel12)
        Me.GroupBox7.Controls.Add(Me.Panel13)
        Me.GroupBox7.Controls.Add(Me.Panel14)
        Me.GroupBox7.Controls.Add(Me.Panel15)
        Me.GroupBox7.Controls.Add(Me.Panel16)
        Me.GroupBox7.Controls.Add(Me.Label17)
        Me.GroupBox7.Controls.Add(Me.TextBox3)
        Me.GroupBox7.Controls.Add(Me.Label18)
        Me.GroupBox7.Controls.Add(Me.TextBox4)
        Me.GroupBox7.Controls.Add(Me.Label19)
        Me.GroupBox7.Controls.Add(Me.Label20)
        Me.GroupBox7.Controls.Add(Me.Label21)
        Me.GroupBox7.Controls.Add(Me.Label22)
        Me.GroupBox7.Controls.Add(Me.Label23)
        Me.GroupBox7.Controls.Add(Me.Label24)
        Me.GroupBox7.Controls.Add(Me.ComboBox1)
        Me.GroupBox7.Controls.Add(Me.ComboBox2)
        Me.GroupBox7.Controls.Add(Me.Label25)
        Me.GroupBox7.Controls.Add(Me.ComboBox3)
        Me.GroupBox7.Controls.Add(Me.Label26)
        Me.GroupBox7.Controls.Add(Me.Label27)
        Me.GroupBox7.Controls.Add(Me.ComboBox4)
        Me.GroupBox7.Controls.Add(Me.Label28)
        Me.GroupBox7.Controls.Add(Me.Label29)
        Me.GroupBox7.Controls.Add(Me.Label30)
        Me.GroupBox7.Controls.Add(Me.Label31)
        Me.GroupBox7.Controls.Add(Me.TextBox5)
        Me.GroupBox7.Controls.Add(Me.Label32)
        Me.GroupBox7.Controls.Add(Me.Label33)
        Me.GroupBox7.Controls.Add(Me.TextBox6)
        Me.GroupBox7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.ForeColor = System.Drawing.Color.White
        Me.GroupBox7.Location = New System.Drawing.Point(15, 405)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(874, 183)
        Me.GroupBox7.TabIndex = 2
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Other Details"
        '
        'Panel12
        '
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel12.Location = New System.Drawing.Point(677, 38)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(110, 1)
        Me.Panel12.TabIndex = 58
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.White
        Me.Panel13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel13.Location = New System.Drawing.Point(569, 20)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(1, 145)
        Me.Panel13.TabIndex = 57
        '
        'Panel14
        '
        Me.Panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel14.Location = New System.Drawing.Point(676, 77)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(110, 1)
        Me.Panel14.TabIndex = 56
        '
        'Panel15
        '
        Me.Panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel15.Location = New System.Drawing.Point(676, 157)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(110, 1)
        Me.Panel15.TabIndex = 55
        '
        'Panel16
        '
        Me.Panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel16.Location = New System.Drawing.Point(676, 61)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(110, 1)
        Me.Panel16.TabIndex = 54
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Orange
        Me.Label17.Location = New System.Drawing.Point(740, 138)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(103, 20)
        Me.Label17.TabIndex = 53
        Me.Label17.Text = "0.00"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox3.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.ForeColor = System.Drawing.Color.White
        Me.TextBox3.Location = New System.Drawing.Point(110, 125)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox3.Size = New System.Drawing.Size(388, 41)
        Me.TextBox3.TabIndex = 9
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Orange
        Me.Label18.Location = New System.Drawing.Point(740, 58)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(103, 20)
        Me.Label18.TabIndex = 52
        Me.Label18.Text = "0.00"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox4.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.ForeColor = System.Drawing.Color.White
        Me.TextBox4.Location = New System.Drawing.Point(110, 77)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox4.Size = New System.Drawing.Size(388, 39)
        Me.TextBox4.TabIndex = 8
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Orange
        Me.Label19.Location = New System.Drawing.Point(740, 38)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(103, 20)
        Me.Label19.TabIndex = 51
        Me.Label19.Text = "0.00"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Orange
        Me.Label20.Location = New System.Drawing.Point(740, 15)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(103, 20)
        Me.Label20.TabIndex = 50
        Me.Label20.Text = "0.00"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(721, 448)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(31, 15)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "0.00"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(11, 128)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(99, 15)
        Me.Label22.TabIndex = 17
        Me.Label22.Text = "Payment Terms :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(632, 143)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(76, 15)
        Me.Label23.TabIndex = 49
        Me.Label23.Text = "Net Amount :"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(11, 50)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(99, 15)
        Me.Label24.TabIndex = 33
        Me.Label24.Text = "Transport Mode :"
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.ForeColor = System.Drawing.Color.White
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"On Party behalf", "On Ourbehalf"})
        Me.ComboBox1.Location = New System.Drawing.Point(110, 46)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(154, 23)
        Me.ComboBox1.TabIndex = 2
        '
        'ComboBox2
        '
        Me.ComboBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox2.ForeColor = System.Drawing.Color.White
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"Ex-works,Godown", "F.O.R Destination"})
        Me.ComboBox2.Location = New System.Drawing.Point(344, 47)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(154, 23)
        Me.ComboBox2.TabIndex = 3
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(268, 50)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(76, 15)
        Me.Label25.TabIndex = 34
        Me.Label25.Text = "Price Basis :"
        '
        'ComboBox3
        '
        Me.ComboBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox3.ForeColor = System.Drawing.Color.White
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Items.AddRange(New Object() {"By Us", "By You"})
        Me.ComboBox3.Location = New System.Drawing.Point(344, 19)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(154, 23)
        Me.ComboBox3.TabIndex = 1
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(632, 46)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(51, 15)
        Me.Label26.TabIndex = 39
        Me.Label26.Text = "Vat/Cst :"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(11, 79)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(84, 15)
        Me.Label27.TabIndex = 15
        Me.Label27.Text = "PO Remarks :"
        '
        'ComboBox4
        '
        Me.ComboBox4.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox4.ForeColor = System.Drawing.Color.White
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Items.AddRange(New Object() {"By Us", "By You"})
        Me.ComboBox4.Location = New System.Drawing.Point(110, 18)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(154, 23)
        Me.ComboBox4.TabIndex = 0
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(632, 24)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(70, 15)
        Me.Label28.TabIndex = 48
        Me.Label28.Text = "Item Value :"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(270, 23)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(51, 15)
        Me.Label29.TabIndex = 32
        Me.Label29.Text = "Freight :"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(632, 65)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(68, 15)
        Me.Label30.TabIndex = 40
        Me.Label30.Text = "Exice Amt. :"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(11, 23)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(45, 15)
        Me.Label31.TabIndex = 31
        Me.Label31.Text = "Octroi :"
        '
        'TextBox5
        '
        Me.TextBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox5.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox5.ForeColor = System.Drawing.Color.White
        Me.TextBox5.Location = New System.Drawing.Point(734, 88)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(109, 18)
        Me.TextBox5.TabIndex = 7
        Me.TextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(632, 88)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(57, 15)
        Me.Label32.TabIndex = 41
        Me.Label32.Text = "Cess % :"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(632, 116)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(94, 15)
        Me.Label33.TabIndex = 42
        Me.Label33.Text = "Other Charges :"
        '
        'TextBox6
        '
        Me.TextBox6.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox6.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox6.ForeColor = System.Drawing.Color.White
        Me.TextBox6.Location = New System.Drawing.Point(734, 116)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(109, 18)
        Me.TextBox6.TabIndex = 8
        Me.TextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frm_Open_PO_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frm_Open_PO_Master"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.grdOpenPOList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdOpenPoMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.erp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.flxItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdOpenPoMaster As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents grdOpenPOList As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPONo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCess As System.Windows.Forms.TextBox
    Friend WithEvents txtOtherCharges As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpPriceBasis As System.Windows.Forms.ComboBox
    Friend WithEvents dtpFreight As System.Windows.Forms.ComboBox
    Friend WithEvents dtpTransMode As System.Windows.Forms.ComboBox
    Friend WithEvents dtpOctroi As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbPOType As System.Windows.Forms.ComboBox
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents txtPaymentTerms As System.Windows.Forms.TextBox
    Friend WithEvents txtPORemarks As System.Windows.Forms.TextBox
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpPODate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbDeliveryRate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbQualityRate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSupplier As System.Windows.Forms.ComboBox
    Friend WithEvents lblCap9 As System.Windows.Forms.Label
    Friend WithEvents lblCap10 As System.Windows.Forms.Label
    Friend WithEvents lblCap8 As System.Windows.Forms.Label
    Friend WithEvents lblCap7 As System.Windows.Forms.Label
    Friend WithEvents lblCap4 As System.Windows.Forms.Label
    Friend WithEvents lblCap6 As System.Windows.Forms.Label
    Friend WithEvents lblCap As System.Windows.Forms.Label
    Friend WithEvents lblCap5 As System.Windows.Forms.Label
    Friend WithEvents lblCap1 As System.Windows.Forms.Label
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblItemValue As System.Windows.Forms.Label
    Friend WithEvents lblVatCst As System.Windows.Forms.Label
    Friend WithEvents lblExiceAmt As System.Windows.Forms.Label
    Friend WithEvents lblNetAmount As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents erp As System.Windows.Forms.ErrorProvider
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents txtDiscountAmount As System.Windows.Forms.TextBox
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents flxItemList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents ddlSupplierSearch As System.Windows.Forms.ComboBox
    Friend WithEvents txtPONumberSearch As System.Windows.Forms.TextBox
    Friend WithEvents txtPODateSearch As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkSupplier As System.Windows.Forms.CheckBox
    Friend WithEvents chkPONumber As System.Windows.Forms.CheckBox
    Friend WithEvents chkPODate As System.Windows.Forms.CheckBox
    Friend WithEvents chkStatus As System.Windows.Forms.CheckBox
    Friend WithEvents cmdPOStatusSearch As System.Windows.Forms.ComboBox
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Panel14 As Panel
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Panel16 As Panel
    Friend WithEvents Label17 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label25 As Label
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents Label26 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents ComboBox4 As ComboBox
    Friend WithEvents Label28 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label32 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents TextBox6 As TextBox
End Class
