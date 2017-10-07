<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Stock_Transfer_CC_To_CC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Stock_Transfer_CC_To_CC))
        Me.tbctrl_stock_transfer = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.grpbx_material_return = New System.Windows.Forms.GroupBox()
        Me.DGVW_list_StockTransfer = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btntransfer = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtqty = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbrecipe = New System.Windows.Forms.ComboBox()
        Me.rdbsemirecipe = New System.Windows.Forms.RadioButton()
        Me.rdbrecipe = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.DGVStockTransfer = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmb_CostCenters = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_Remarks = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lbl_Status = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dt_Transfer_Date = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_TransferNo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.tbctrl_stock_transfer.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.grpbx_material_return.SuspendLayout()
        CType(Me.DGVW_list_StockTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DGVStockTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbctrl_stock_transfer
        '
        Me.tbctrl_stock_transfer.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tbctrl_stock_transfer.Controls.Add(Me.TabPage1)
        Me.tbctrl_stock_transfer.Controls.Add(Me.TabPage2)
        Me.tbctrl_stock_transfer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbctrl_stock_transfer.ImageList = Me.ImageList1
        Me.tbctrl_stock_transfer.ItemSize = New System.Drawing.Size(42, 18)
        Me.tbctrl_stock_transfer.Location = New System.Drawing.Point(0, 0)
        Me.tbctrl_stock_transfer.Name = "tbctrl_stock_transfer"
        Me.tbctrl_stock_transfer.SelectedIndex = 0
        Me.tbctrl_stock_transfer.Size = New System.Drawing.Size(910, 630)
        Me.tbctrl_stock_transfer.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.DimGray
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Controls.Add(Me.grpbx_material_return)
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(902, 604)
        Me.TabPage1.TabIndex = 0
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.txtSearch)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox5.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(896, 70)
        Me.GroupBox5.TabIndex = 18
        Me.GroupBox5.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(95, 27)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(761, 18)
        Me.txtSearch.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(10, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 15)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Search By :"
        '
        'grpbx_material_return
        '
        Me.grpbx_material_return.Controls.Add(Me.DGVW_list_StockTransfer)
        Me.grpbx_material_return.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpbx_material_return.ForeColor = System.Drawing.Color.White
        Me.grpbx_material_return.Location = New System.Drawing.Point(3, 79)
        Me.grpbx_material_return.Name = "grpbx_material_return"
        Me.grpbx_material_return.Size = New System.Drawing.Size(896, 500)
        Me.grpbx_material_return.TabIndex = 0
        Me.grpbx_material_return.TabStop = False
        Me.grpbx_material_return.Text = "Stock Transfer"
        '
        'DGVW_list_StockTransfer
        '
        Me.DGVW_list_StockTransfer.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVW_list_StockTransfer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVW_list_StockTransfer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVW_list_StockTransfer.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVW_list_StockTransfer.Location = New System.Drawing.Point(3, 17)
        Me.DGVW_list_StockTransfer.Name = "DGVW_list_StockTransfer"
        Me.DGVW_list_StockTransfer.ReadOnly = True
        Me.DGVW_list_StockTransfer.RowHeadersVisible = False
        Me.DGVW_list_StockTransfer.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVW_list_StockTransfer.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVW_list_StockTransfer.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVW_list_StockTransfer.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.DGVW_list_StockTransfer.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVW_list_StockTransfer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVW_list_StockTransfer.Size = New System.Drawing.Size(890, 480)
        Me.DGVW_list_StockTransfer.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.DimGray
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.ForeColor = System.Drawing.Color.White
        Me.TabPage2.ImageIndex = 1
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(902, 604)
        Me.TabPage2.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btntransfer)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.txtqty)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.cmbrecipe)
        Me.GroupBox3.Controls.Add(Me.rdbsemirecipe)
        Me.GroupBox3.Controls.Add(Me.rdbrecipe)
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.White
        Me.GroupBox3.Location = New System.Drawing.Point(13, 520)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(877, 70)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Transfer Recipe"
        '
        'btntransfer
        '
        Me.btntransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btntransfer.Location = New System.Drawing.Point(760, 30)
        Me.btntransfer.Name = "btntransfer"
        Me.btntransfer.Size = New System.Drawing.Size(98, 31)
        Me.btntransfer.TabIndex = 5
        Me.btntransfer.Text = "Transfer"
        Me.btntransfer.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(602, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 15)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Quantity:"
        '
        'txtqty
        '
        Me.txtqty.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtqty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtqty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtqty.ForeColor = System.Drawing.Color.White
        Me.txtqty.Location = New System.Drawing.Point(605, 38)
        Me.txtqty.Name = "txtqty"
        Me.txtqty.Size = New System.Drawing.Size(129, 18)
        Me.txtqty.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(247, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 15)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Select Recipe:"
        '
        'cmbrecipe
        '
        Me.cmbrecipe.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbrecipe.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbrecipe.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbrecipe.ForeColor = System.Drawing.Color.White
        Me.cmbrecipe.FormattingEnabled = True
        Me.cmbrecipe.Location = New System.Drawing.Point(250, 38)
        Me.cmbrecipe.Name = "cmbrecipe"
        Me.cmbrecipe.Size = New System.Drawing.Size(322, 23)
        Me.cmbrecipe.TabIndex = 1
        '
        'rdbsemirecipe
        '
        Me.rdbsemirecipe.AutoSize = True
        Me.rdbsemirecipe.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbsemirecipe.Location = New System.Drawing.Point(17, 42)
        Me.rdbsemirecipe.Name = "rdbsemirecipe"
        Me.rdbsemirecipe.Size = New System.Drawing.Size(196, 19)
        Me.rdbsemirecipe.TabIndex = 0
        Me.rdbsemirecipe.Text = "Transfer Semi Finished Recipe"
        Me.rdbsemirecipe.UseVisualStyleBackColor = True
        '
        'rdbrecipe
        '
        Me.rdbrecipe.AutoSize = True
        Me.rdbrecipe.Checked = True
        Me.rdbrecipe.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbrecipe.Location = New System.Drawing.Point(17, 19)
        Me.rdbrecipe.Name = "rdbrecipe"
        Me.rdbrecipe.Size = New System.Drawing.Size(113, 19)
        Me.rdbrecipe.TabIndex = 0
        Me.rdbrecipe.TabStop = True
        Me.rdbrecipe.Text = "Transfer Recipe"
        Me.rdbrecipe.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DGVStockTransfer)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(14, 140)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(877, 374)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Items"
        '
        'DGVStockTransfer
        '
        Me.DGVStockTransfer.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVStockTransfer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVStockTransfer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVStockTransfer.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVStockTransfer.Location = New System.Drawing.Point(3, 17)
        Me.DGVStockTransfer.Name = "DGVStockTransfer"
        Me.DGVStockTransfer.RowHeadersVisible = False
        Me.DGVStockTransfer.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVStockTransfer.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVStockTransfer.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVStockTransfer.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.DGVStockTransfer.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVStockTransfer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVStockTransfer.Size = New System.Drawing.Size(871, 354)
        Me.DGVStockTransfer.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmb_CostCenters)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txt_Remarks)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lbl_Status)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.dt_Transfer_Date)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lbl_TransferNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblFormHeading)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(877, 125)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cmb_CostCenters
        '
        Me.cmb_CostCenters.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmb_CostCenters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_CostCenters.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_CostCenters.ForeColor = System.Drawing.Color.White
        Me.cmb_CostCenters.FormattingEnabled = True
        Me.cmb_CostCenters.Location = New System.Drawing.Point(373, 13)
        Me.cmb_CostCenters.Name = "cmb_CostCenters"
        Me.cmb_CostCenters.Size = New System.Drawing.Size(210, 23)
        Me.cmb_CostCenters.TabIndex = 22
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(272, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 15)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Transfer To :"
        '
        'txt_Remarks
        '
        Me.txt_Remarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_Remarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Remarks.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Remarks.ForeColor = System.Drawing.Color.White
        Me.txt_Remarks.Location = New System.Drawing.Point(112, 66)
        Me.txt_Remarks.Multiline = True
        Me.txt_Remarks.Name = "txt_Remarks"
        Me.txt_Remarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_Remarks.Size = New System.Drawing.Size(471, 51)
        Me.txt_Remarks.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(21, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(64, 15)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Remarks :"
        '
        'lbl_Status
        '
        Me.lbl_Status.AutoSize = True
        Me.lbl_Status.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Status.ForeColor = System.Drawing.Color.Orange
        Me.lbl_Status.Location = New System.Drawing.Point(109, 18)
        Me.lbl_Status.Name = "lbl_Status"
        Me.lbl_Status.Size = New System.Drawing.Size(45, 15)
        Me.lbl_Status.TabIndex = 19
        Me.lbl_Status.Text = "Label6"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 15)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Status :"
        '
        'dt_Transfer_Date
        '
        Me.dt_Transfer_Date.CalendarForeColor = System.Drawing.Color.White
        Me.dt_Transfer_Date.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dt_Transfer_Date.CustomFormat = "dd/MMM/yyyy"
        Me.dt_Transfer_Date.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dt_Transfer_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dt_Transfer_Date.Location = New System.Drawing.Point(374, 40)
        Me.dt_Transfer_Date.Name = "dt_Transfer_Date"
        Me.dt_Transfer_Date.Size = New System.Drawing.Size(130, 21)
        Me.dt_Transfer_Date.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(272, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 15)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Transfer Date :"
        '
        'lbl_TransferNo
        '
        Me.lbl_TransferNo.AutoSize = True
        Me.lbl_TransferNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TransferNo.ForeColor = System.Drawing.Color.Orange
        Me.lbl_TransferNo.Location = New System.Drawing.Point(109, 43)
        Me.lbl_TransferNo.Name = "lbl_TransferNo"
        Me.lbl_TransferNo.Size = New System.Drawing.Size(45, 15)
        Me.lbl_TransferNo.TabIndex = 13
        Me.lbl_TransferNo.Text = "Label2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 15)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Transfer No. :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(692, 10)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(182, 75)
        Me.lblFormHeading.TabIndex = 11
        Me.lblFormHeading.Text = "Stock Transfer " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "To" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Cost Center"
        Me.lblFormHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'frm_Stock_Transfer_CC_To_CC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tbctrl_stock_transfer)
        Me.Name = "frm_Stock_Transfer_CC_To_CC"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.tbctrl_stock_transfer.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.grpbx_material_return.ResumeLayout(False)
        CType(Me.DGVW_list_StockTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DGVStockTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbctrl_stock_transfer As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents grpbx_material_return As System.Windows.Forms.GroupBox
    Friend WithEvents DGVW_list_StockTransfer As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_Remarks As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lbl_Status As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dt_Transfer_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbl_TransferNo As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents DGVStockTransfer As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmb_CostCenters As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbsemirecipe As System.Windows.Forms.RadioButton
    Friend WithEvents rdbrecipe As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbrecipe As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtqty As System.Windows.Forms.TextBox
    Friend WithEvents btntransfer As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As ImageList
End Class
