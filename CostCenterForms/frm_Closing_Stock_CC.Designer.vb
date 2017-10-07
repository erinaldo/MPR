<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Closing_Stock_CC
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Closing_Stock_CC))
        Me.DGVWastageMaster = New System.Windows.Forms.DataGridView()
        Me.TBCWastageMaster = New System.Windows.Forms.TabControl()
        Me.List = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GBWastageMaster = New System.Windows.Forms.GroupBox()
        Me.DGVMasters = New System.Windows.Forms.DataGridView()
        Me.Detail = New System.Windows.Forms.TabPage()
        Me.lblErrorMsg = New System.Windows.Forms.Label()
        Me.GBWastageItem = New System.Windows.Forms.GroupBox()
        Me.DGVClstkitems = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GBWastageMasterInfo = New System.Windows.Forms.GroupBox()
        Me.ddl_category = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpClosingdt = New System.Windows.Forms.DateTimePicker()
        Me.lbl_status = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.lbl_Closing_Date = New System.Windows.Forms.Label()
        Me.lblClosingdate = New System.Windows.Forms.Label()
        Me.txtAdjustmentRemarks = New System.Windows.Forms.TextBox()
        Me.lblstatus = New System.Windows.Forms.Label()
        Me.lbl_Closing_code = New System.Windows.Forms.Label()
        Me.lblclosingremarks = New System.Windows.Forms.Label()
        Me.lblclosingcode = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        CType(Me.DGVWastageMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBCWastageMaster.SuspendLayout()
        Me.List.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GBWastageMaster.SuspendLayout()
        CType(Me.DGVMasters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Detail.SuspendLayout()
        Me.GBWastageItem.SuspendLayout()
        CType(Me.DGVClstkitems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBWastageMasterInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGVWastageMaster
        '
        Me.DGVWastageMaster.AllowUserToAddRows = False
        Me.DGVWastageMaster.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVWastageMaster.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVWastageMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVWastageMaster.DefaultCellStyle = DataGridViewCellStyle2
        Me.DGVWastageMaster.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DGVWastageMaster.Location = New System.Drawing.Point(3, 19)
        Me.DGVWastageMaster.Name = "DGVWastageMaster"
        Me.DGVWastageMaster.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVWastageMaster.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVWastageMaster.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DGVWastageMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVWastageMaster.Size = New System.Drawing.Size(878, 503)
        Me.DGVWastageMaster.TabIndex = 0
        '
        'TBCWastageMaster
        '
        Me.TBCWastageMaster.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TBCWastageMaster.Controls.Add(Me.List)
        Me.TBCWastageMaster.Controls.Add(Me.Detail)
        Me.TBCWastageMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TBCWastageMaster.ImageList = Me.ImageList1
        Me.TBCWastageMaster.Location = New System.Drawing.Point(0, 0)
        Me.TBCWastageMaster.Name = "TBCWastageMaster"
        Me.TBCWastageMaster.SelectedIndex = 0
        Me.TBCWastageMaster.Size = New System.Drawing.Size(910, 630)
        Me.TBCWastageMaster.TabIndex = 1
        '
        'List
        '
        Me.List.BackColor = System.Drawing.Color.DimGray
        Me.List.Controls.Add(Me.GroupBox1)
        Me.List.Controls.Add(Me.GBWastageMaster)
        Me.List.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.List.ForeColor = System.Drawing.Color.White
        Me.List.ImageIndex = 0
        Me.List.Location = New System.Drawing.Point(4, 26)
        Me.List.Name = "List"
        Me.List.Padding = New System.Windows.Forms.Padding(3)
        Me.List.Size = New System.Drawing.Size(902, 600)
        Me.List.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(896, 82)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(86, 35)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(787, 18)
        Me.txtSearch.TabIndex = 19
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(6, 36)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 15)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Search By :"
        '
        'GBWastageMaster
        '
        Me.GBWastageMaster.Controls.Add(Me.DGVMasters)
        Me.GBWastageMaster.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBWastageMaster.ForeColor = System.Drawing.Color.White
        Me.GBWastageMaster.Location = New System.Drawing.Point(3, 91)
        Me.GBWastageMaster.Name = "GBWastageMaster"
        Me.GBWastageMaster.Size = New System.Drawing.Size(896, 492)
        Me.GBWastageMaster.TabIndex = 6
        Me.GBWastageMaster.TabStop = False
        Me.GBWastageMaster.Text = "Closing Master"
        '
        'DGVMasters
        '
        Me.DGVMasters.AllowUserToAddRows = False
        Me.DGVMasters.AllowUserToDeleteRows = False
        Me.DGVMasters.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMasters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVMasters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVMasters.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMasters.Location = New System.Drawing.Point(3, 17)
        Me.DGVMasters.Name = "DGVMasters"
        Me.DGVMasters.ReadOnly = True
        Me.DGVMasters.RowHeadersVisible = False
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVMasters.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DGVMasters.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMasters.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVMasters.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVMasters.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.DGVMasters.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVMasters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVMasters.Size = New System.Drawing.Size(890, 472)
        Me.DGVMasters.TabIndex = 0
        '
        'Detail
        '
        Me.Detail.BackColor = System.Drawing.Color.DimGray
        Me.Detail.Controls.Add(Me.lblErrorMsg)
        Me.Detail.Controls.Add(Me.GBWastageItem)
        Me.Detail.Controls.Add(Me.GBWastageMasterInfo)
        Me.Detail.ImageIndex = 1
        Me.Detail.Location = New System.Drawing.Point(4, 26)
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New System.Windows.Forms.Padding(3)
        Me.Detail.Size = New System.Drawing.Size(902, 600)
        Me.Detail.TabIndex = 2
        '
        'lblErrorMsg
        '
        Me.lblErrorMsg.AutoSize = True
        Me.lblErrorMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrorMsg.Location = New System.Drawing.Point(247, 155)
        Me.lblErrorMsg.Name = "lblErrorMsg"
        Me.lblErrorMsg.Size = New System.Drawing.Size(0, 13)
        Me.lblErrorMsg.TabIndex = 8
        '
        'GBWastageItem
        '
        Me.GBWastageItem.Controls.Add(Me.DGVClstkitems)
        Me.GBWastageItem.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBWastageItem.ForeColor = System.Drawing.Color.White
        Me.GBWastageItem.Location = New System.Drawing.Point(3, 193)
        Me.GBWastageItem.Name = "GBWastageItem"
        Me.GBWastageItem.Size = New System.Drawing.Size(896, 391)
        Me.GBWastageItem.TabIndex = 7
        Me.GBWastageItem.TabStop = False
        Me.GBWastageItem.Text = "List of Item"
        '
        'DGVClstkitems
        '
        Me.DGVClstkitems.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop
        Me.DGVClstkitems.AutoSearchDelay = 2.0R
        Me.DGVClstkitems.BackColor = System.Drawing.Color.Silver
        Me.DGVClstkitems.ColumnInfo = "10,1,0,0,0,90,Columns:"
        Me.DGVClstkitems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVClstkitems.Location = New System.Drawing.Point(3, 17)
        Me.DGVClstkitems.Name = "DGVClstkitems"
        Me.DGVClstkitems.Rows.Count = 1
        Me.DGVClstkitems.Rows.DefaultSize = 18
        Me.DGVClstkitems.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.DGVClstkitems.Size = New System.Drawing.Size(890, 371)
        Me.DGVClstkitems.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("DGVClstkitems.Styles"))
        Me.DGVClstkitems.TabIndex = 1
        '
        'GBWastageMasterInfo
        '
        Me.GBWastageMasterInfo.Controls.Add(Me.ddl_category)
        Me.GBWastageMasterInfo.Controls.Add(Me.Label1)
        Me.GBWastageMasterInfo.Controls.Add(Me.dtpClosingdt)
        Me.GBWastageMasterInfo.Controls.Add(Me.lbl_status)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblFormHeading)
        Me.GBWastageMasterInfo.Controls.Add(Me.lbl_Closing_Date)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblClosingdate)
        Me.GBWastageMasterInfo.Controls.Add(Me.txtAdjustmentRemarks)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblstatus)
        Me.GBWastageMasterInfo.Controls.Add(Me.lbl_Closing_code)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblclosingremarks)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblclosingcode)
        Me.GBWastageMasterInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBWastageMasterInfo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBWastageMasterInfo.ForeColor = System.Drawing.Color.White
        Me.GBWastageMasterInfo.Location = New System.Drawing.Point(3, 3)
        Me.GBWastageMasterInfo.Name = "GBWastageMasterInfo"
        Me.GBWastageMasterInfo.Size = New System.Drawing.Size(896, 184)
        Me.GBWastageMasterInfo.TabIndex = 5
        Me.GBWastageMasterInfo.TabStop = False
        Me.GBWastageMasterInfo.Text = "Closing  Master/Consumption"
        '
        'ddl_category
        '
        Me.ddl_category.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ddl_category.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ddl_category.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ddl_category.ForeColor = System.Drawing.Color.White
        Me.ddl_category.FormattingEnabled = True
        Me.ddl_category.Location = New System.Drawing.Point(135, 152)
        Me.ddl_category.Name = "ddl_category"
        Me.ddl_category.Size = New System.Drawing.Size(459, 23)
        Me.ddl_category.TabIndex = 33
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 155)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 15)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Category:"
        '
        'dtpClosingdt
        '
        Me.dtpClosingdt.CalendarForeColor = System.Drawing.Color.White
        Me.dtpClosingdt.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpClosingdt.CustomFormat = "dd-MMM-yyyy"
        Me.dtpClosingdt.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpClosingdt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpClosingdt.Location = New System.Drawing.Point(135, 43)
        Me.dtpClosingdt.Name = "dtpClosingdt"
        Me.dtpClosingdt.Size = New System.Drawing.Size(134, 21)
        Me.dtpClosingdt.TabIndex = 31
        '
        'lbl_status
        '
        Me.lbl_status.AutoSize = True
        Me.lbl_status.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_status.Location = New System.Drawing.Point(303, 21)
        Me.lbl_status.Name = "lbl_status"
        Me.lbl_status.Size = New System.Drawing.Size(45, 15)
        Me.lbl_status.TabIndex = 29
        Me.lbl_status.Text = "Status:"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(555, 11)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(338, 25)
        Me.lblFormHeading.TabIndex = 28
        Me.lblFormHeading.Text = "Closing Master/Consumption"
        '
        'lbl_Closing_Date
        '
        Me.lbl_Closing_Date.AutoSize = True
        Me.lbl_Closing_Date.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Closing_Date.ForeColor = System.Drawing.Color.Orange
        Me.lbl_Closing_Date.Location = New System.Drawing.Point(303, 47)
        Me.lbl_Closing_Date.Name = "lbl_Closing_Date"
        Me.lbl_Closing_Date.Size = New System.Drawing.Size(79, 15)
        Me.lbl_Closing_Date.TabIndex = 7
        Me.lbl_Closing_Date.Text = "Closing Date"
        Me.lbl_Closing_Date.Visible = False
        '
        'lblClosingdate
        '
        Me.lblClosingdate.AutoSize = True
        Me.lblClosingdate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClosingdate.Location = New System.Drawing.Point(22, 46)
        Me.lblClosingdate.Name = "lblClosingdate"
        Me.lblClosingdate.Size = New System.Drawing.Size(82, 15)
        Me.lblClosingdate.TabIndex = 6
        Me.lblClosingdate.Text = "Closing Date:"
        '
        'txtAdjustmentRemarks
        '
        Me.txtAdjustmentRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtAdjustmentRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAdjustmentRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdjustmentRemarks.ForeColor = System.Drawing.Color.White
        Me.txtAdjustmentRemarks.Location = New System.Drawing.Point(135, 75)
        Me.txtAdjustmentRemarks.MaxLength = 100
        Me.txtAdjustmentRemarks.Multiline = True
        Me.txtAdjustmentRemarks.Name = "txtAdjustmentRemarks"
        Me.txtAdjustmentRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAdjustmentRemarks.Size = New System.Drawing.Size(459, 67)
        Me.txtAdjustmentRemarks.TabIndex = 5
        '
        'lblstatus
        '
        Me.lblstatus.AutoSize = True
        Me.lblstatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.ForeColor = System.Drawing.Color.Orange
        Me.lblstatus.Location = New System.Drawing.Point(355, 22)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(42, 15)
        Me.lblstatus.TabIndex = 3
        Me.lblstatus.Text = "Status"
        '
        'lbl_Closing_code
        '
        Me.lbl_Closing_code.AutoSize = True
        Me.lbl_Closing_code.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Closing_code.ForeColor = System.Drawing.Color.Orange
        Me.lbl_Closing_code.Location = New System.Drawing.Point(132, 20)
        Me.lbl_Closing_code.Name = "lbl_Closing_code"
        Me.lbl_Closing_code.Size = New System.Drawing.Size(150, 15)
        Me.lbl_Closing_code.TabIndex = 3
        Me.lbl_Closing_code.Text = "Click on ""New"" button first."
        '
        'lblclosingremarks
        '
        Me.lblclosingremarks.AutoSize = True
        Me.lblclosingremarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclosingremarks.Location = New System.Drawing.Point(22, 74)
        Me.lblclosingremarks.Name = "lblclosingremarks"
        Me.lblclosingremarks.Size = New System.Drawing.Size(107, 15)
        Me.lblclosingremarks.TabIndex = 1
        Me.lblclosingremarks.Text = "Closing Remarks:"
        '
        'lblclosingcode
        '
        Me.lblclosingcode.AutoSize = True
        Me.lblclosingcode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblclosingcode.Location = New System.Drawing.Point(22, 20)
        Me.lblclosingcode.Name = "lblclosingcode"
        Me.lblclosingcode.Size = New System.Drawing.Size(86, 15)
        Me.lblclosingcode.TabIndex = 0
        Me.lblclosingcode.Text = "Closing Code:"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'frm_Closing_Stock_CC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TBCWastageMaster)
        Me.Name = "frm_Closing_Stock_CC"
        Me.Size = New System.Drawing.Size(910, 630)
        CType(Me.DGVWastageMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBCWastageMaster.ResumeLayout(False)
        Me.List.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GBWastageMaster.ResumeLayout(False)
        CType(Me.DGVMasters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Detail.ResumeLayout(False)
        Me.Detail.PerformLayout()
        Me.GBWastageItem.ResumeLayout(False)
        CType(Me.DGVClstkitems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBWastageMasterInfo.ResumeLayout(False)
        Me.GBWastageMasterInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGVWastageMaster As System.Windows.Forms.DataGridView
    Friend WithEvents TBCWastageMaster As System.Windows.Forms.TabControl
    Friend WithEvents List As System.Windows.Forms.TabPage
    Friend WithEvents GBWastageMaster As System.Windows.Forms.GroupBox
    Friend WithEvents DGVMasters As System.Windows.Forms.DataGridView
    Friend WithEvents Detail As System.Windows.Forms.TabPage
    Friend WithEvents lblErrorMsg As System.Windows.Forms.Label
    Friend WithEvents GBWastageItem As System.Windows.Forms.GroupBox
    Friend WithEvents GBWastageMasterInfo As System.Windows.Forms.GroupBox
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents lbl_Closing_Date As System.Windows.Forms.Label
    Friend WithEvents lblClosingdate As System.Windows.Forms.Label
    Friend WithEvents txtAdjustmentRemarks As System.Windows.Forms.TextBox
    Friend WithEvents lbl_Closing_code As System.Windows.Forms.Label
    Friend WithEvents lblclosingremarks As System.Windows.Forms.Label
    Friend WithEvents lblclosingcode As System.Windows.Forms.Label
    Friend WithEvents lbl_status As System.Windows.Forms.Label
    Friend WithEvents lblstatus As System.Windows.Forms.Label
    Friend WithEvents dtpClosingdt As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ddl_category As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DGVClstkitems As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ImageList1 As ImageList
End Class
