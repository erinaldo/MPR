<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MRS_MainStore
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_MRS_MainStore))
        Me.TBC_MRS_Master = New System.Windows.Forms.TabControl()
        Me.List = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GBMRSDetail = New System.Windows.Forms.GroupBox()
        Me.DGVMRSMaster = New System.Windows.Forms.DataGridView()
        Me.Detail = New System.Windows.Forms.TabPage()
        Me.lblErrorMsg = New System.Windows.Forms.Label()
        Me.GBMRSItemInfo = New System.Windows.Forms.GroupBox()
        Me.DGVMRSItem = New System.Windows.Forms.DataGridView()
        Me.GBMRSMaster = New System.Windows.Forms.GroupBox()
        Me.lbl_Status = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lbl_MRSDate = New System.Windows.Forms.Label()
        Me.lblMRSDate = New System.Windows.Forms.Label()
        Me.txtMRSRemarks = New System.Windows.Forms.TextBox()
        Me.DTPMRSReqDate = New System.Windows.Forms.DateTimePicker()
        Me.lbl_MRSCode = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblMRSRemarks = New System.Windows.Forms.Label()
        Me.lblMRSCode = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lbl_IndentStatus = New System.Windows.Forms.Label()
        Me.lblIndentStatus = New System.Windows.Forms.Label()
        Me.lbl_IndentDate = New System.Windows.Forms.Label()
        Me.lblIndentDate = New System.Windows.Forms.Label()
        Me.txtIndentReamrks = New System.Windows.Forms.TextBox()
        Me.dtpRequiredDate = New System.Windows.Forms.DateTimePicker()
        Me.lbl_IndentCode = New System.Windows.Forms.Label()
        Me.lblRequiredDate = New System.Windows.Forms.Label()
        Me.lblIndentRemarks = New System.Windows.Forms.Label()
        Me.lblIndentCode = New System.Windows.Forms.Label()
        Me.TBC_MRS_Master.SuspendLayout()
        Me.List.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GBMRSDetail.SuspendLayout()
        CType(Me.DGVMRSMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Detail.SuspendLayout()
        Me.GBMRSItemInfo.SuspendLayout()
        CType(Me.DGVMRSItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBMRSMaster.SuspendLayout()
        Me.SuspendLayout()
        '
        'TBC_MRS_Master
        '
        Me.TBC_MRS_Master.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TBC_MRS_Master.Controls.Add(Me.List)
        Me.TBC_MRS_Master.Controls.Add(Me.Detail)
        Me.TBC_MRS_Master.ImageList = Me.ImageList1
        Me.TBC_MRS_Master.Location = New System.Drawing.Point(0, 0)
        Me.TBC_MRS_Master.Name = "TBC_MRS_Master"
        Me.TBC_MRS_Master.SelectedIndex = 0
        Me.TBC_MRS_Master.Size = New System.Drawing.Size(910, 630)
        Me.TBC_MRS_Master.TabIndex = 0
        '
        'List
        '
        Me.List.BackColor = System.Drawing.Color.DimGray
        Me.List.Controls.Add(Me.GroupBox1)
        Me.List.Controls.Add(Me.GBMRSDetail)
        Me.List.ImageIndex = 0
        Me.List.Location = New System.Drawing.Point(4, 26)
        Me.List.Name = "List"
        Me.List.Padding = New System.Windows.Forms.Padding(3)
        Me.List.Size = New System.Drawing.Size(902, 600)
        Me.List.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(872, 76)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(98, 34)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(727, 18)
        Me.txtSearch.TabIndex = 17
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(12, 35)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 15)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Search By :"
        '
        'GBMRSDetail
        '
        Me.GBMRSDetail.Controls.Add(Me.DGVMRSMaster)
        Me.GBMRSDetail.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBMRSDetail.ForeColor = System.Drawing.Color.White
        Me.GBMRSDetail.Location = New System.Drawing.Point(17, 94)
        Me.GBMRSDetail.Name = "GBMRSDetail"
        Me.GBMRSDetail.Size = New System.Drawing.Size(872, 482)
        Me.GBMRSDetail.TabIndex = 6
        Me.GBMRSDetail.TabStop = False
        Me.GBMRSDetail.Text = "List of MRS"
        '
        'DGVMRSMaster
        '
        Me.DGVMRSMaster.AllowUserToAddRows = False
        Me.DGVMRSMaster.AllowUserToDeleteRows = False
        Me.DGVMRSMaster.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMRSMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVMRSMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVMRSMaster.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMRSMaster.Location = New System.Drawing.Point(3, 17)
        Me.DGVMRSMaster.Name = "DGVMRSMaster"
        Me.DGVMRSMaster.ReadOnly = True
        Me.DGVMRSMaster.RowHeadersVisible = False
        Me.DGVMRSMaster.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMRSMaster.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVMRSMaster.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVMRSMaster.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.DGVMRSMaster.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVMRSMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVMRSMaster.Size = New System.Drawing.Size(866, 462)
        Me.DGVMRSMaster.TabIndex = 0
        '
        'Detail
        '
        Me.Detail.BackColor = System.Drawing.Color.DimGray
        Me.Detail.Controls.Add(Me.lblErrorMsg)
        Me.Detail.Controls.Add(Me.GBMRSItemInfo)
        Me.Detail.Controls.Add(Me.GBMRSMaster)
        Me.Detail.ForeColor = System.Drawing.Color.White
        Me.Detail.ImageIndex = 1
        Me.Detail.Location = New System.Drawing.Point(4, 26)
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New System.Windows.Forms.Padding(3)
        Me.Detail.Size = New System.Drawing.Size(902, 600)
        Me.Detail.TabIndex = 1
        '
        'lblErrorMsg
        '
        Me.lblErrorMsg.AutoSize = True
        Me.lblErrorMsg.Location = New System.Drawing.Point(241, 166)
        Me.lblErrorMsg.Name = "lblErrorMsg"
        Me.lblErrorMsg.Size = New System.Drawing.Size(0, 13)
        Me.lblErrorMsg.TabIndex = 8
        '
        'GBMRSItemInfo
        '
        Me.GBMRSItemInfo.Controls.Add(Me.DGVMRSItem)
        Me.GBMRSItemInfo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBMRSItemInfo.ForeColor = System.Drawing.Color.White
        Me.GBMRSItemInfo.Location = New System.Drawing.Point(3, 174)
        Me.GBMRSItemInfo.Name = "GBMRSItemInfo"
        Me.GBMRSItemInfo.Size = New System.Drawing.Size(896, 405)
        Me.GBMRSItemInfo.TabIndex = 7
        Me.GBMRSItemInfo.TabStop = False
        Me.GBMRSItemInfo.Text = "List of Items"
        '
        'DGVMRSItem
        '
        Me.DGVMRSItem.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMRSItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVMRSItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVMRSItem.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMRSItem.Location = New System.Drawing.Point(3, 17)
        Me.DGVMRSItem.Name = "DGVMRSItem"
        Me.DGVMRSItem.RowHeadersVisible = False
        Me.DGVMRSItem.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMRSItem.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVMRSItem.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVMRSItem.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.DGVMRSItem.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVMRSItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVMRSItem.Size = New System.Drawing.Size(890, 385)
        Me.DGVMRSItem.TabIndex = 0
        '
        'GBMRSMaster
        '
        Me.GBMRSMaster.Controls.Add(Me.lbl_Status)
        Me.GBMRSMaster.Controls.Add(Me.lblStatus)
        Me.GBMRSMaster.Controls.Add(Me.lbl_MRSDate)
        Me.GBMRSMaster.Controls.Add(Me.lblMRSDate)
        Me.GBMRSMaster.Controls.Add(Me.txtMRSRemarks)
        Me.GBMRSMaster.Controls.Add(Me.DTPMRSReqDate)
        Me.GBMRSMaster.Controls.Add(Me.lbl_MRSCode)
        Me.GBMRSMaster.Controls.Add(Me.Label6)
        Me.GBMRSMaster.Controls.Add(Me.lblMRSRemarks)
        Me.GBMRSMaster.Controls.Add(Me.lblMRSCode)
        Me.GBMRSMaster.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBMRSMaster.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBMRSMaster.Location = New System.Drawing.Point(3, 3)
        Me.GBMRSMaster.Name = "GBMRSMaster"
        Me.GBMRSMaster.Size = New System.Drawing.Size(896, 165)
        Me.GBMRSMaster.TabIndex = 5
        Me.GBMRSMaster.TabStop = False
        '
        'lbl_Status
        '
        Me.lbl_Status.AutoSize = True
        Me.lbl_Status.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Status.ForeColor = System.Drawing.Color.Orange
        Me.lbl_Status.Location = New System.Drawing.Point(498, 24)
        Me.lbl_Status.Name = "lbl_Status"
        Me.lbl_Status.Size = New System.Drawing.Size(71, 15)
        Me.lbl_Status.TabIndex = 9
        Me.lbl_Status.Text = "MRS Status"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(393, 23)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(77, 15)
        Me.lblStatus.TabIndex = 8
        Me.lblStatus.Text = "MRS Status :"
        '
        'lbl_MRSDate
        '
        Me.lbl_MRSDate.AutoSize = True
        Me.lbl_MRSDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MRSDate.ForeColor = System.Drawing.Color.Orange
        Me.lbl_MRSDate.Location = New System.Drawing.Point(142, 53)
        Me.lbl_MRSDate.Name = "lbl_MRSDate"
        Me.lbl_MRSDate.Size = New System.Drawing.Size(62, 15)
        Me.lbl_MRSDate.TabIndex = 7
        Me.lbl_MRSDate.Text = "MRS Date"
        '
        'lblMRSDate
        '
        Me.lblMRSDate.AutoSize = True
        Me.lblMRSDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRSDate.Location = New System.Drawing.Point(23, 53)
        Me.lblMRSDate.Name = "lblMRSDate"
        Me.lblMRSDate.Size = New System.Drawing.Size(68, 15)
        Me.lblMRSDate.TabIndex = 6
        Me.lblMRSDate.Text = "MRS Date :"
        '
        'txtMRSRemarks
        '
        Me.txtMRSRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtMRSRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMRSRemarks.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRSRemarks.ForeColor = System.Drawing.Color.White
        Me.txtMRSRemarks.Location = New System.Drawing.Point(142, 80)
        Me.txtMRSRemarks.Multiline = True
        Me.txtMRSRemarks.Name = "txtMRSRemarks"
        Me.txtMRSRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMRSRemarks.Size = New System.Drawing.Size(485, 74)
        Me.txtMRSRemarks.TabIndex = 5
        '
        'DTPMRSReqDate
        '
        Me.DTPMRSReqDate.CalendarForeColor = System.Drawing.Color.White
        Me.DTPMRSReqDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DTPMRSReqDate.CustomFormat = "dd/MMM/yyyy"
        Me.DTPMRSReqDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPMRSReqDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPMRSReqDate.Location = New System.Drawing.Point(501, 47)
        Me.DTPMRSReqDate.Name = "DTPMRSReqDate"
        Me.DTPMRSReqDate.Size = New System.Drawing.Size(126, 21)
        Me.DTPMRSReqDate.TabIndex = 4
        '
        'lbl_MRSCode
        '
        Me.lbl_MRSCode.AutoSize = True
        Me.lbl_MRSCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MRSCode.ForeColor = System.Drawing.Color.Orange
        Me.lbl_MRSCode.Location = New System.Drawing.Point(142, 24)
        Me.lbl_MRSCode.Name = "lbl_MRSCode"
        Me.lbl_MRSCode.Size = New System.Drawing.Size(66, 15)
        Me.lbl_MRSCode.TabIndex = 3
        Me.lbl_MRSCode.Text = "MRS Code"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(393, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 15)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Required Date :"
        '
        'lblMRSRemarks
        '
        Me.lblMRSRemarks.AutoSize = True
        Me.lblMRSRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRSRemarks.Location = New System.Drawing.Point(23, 80)
        Me.lblMRSRemarks.Name = "lblMRSRemarks"
        Me.lblMRSRemarks.Size = New System.Drawing.Size(93, 15)
        Me.lblMRSRemarks.TabIndex = 1
        Me.lblMRSRemarks.Text = "MRS Remarks :"
        '
        'lblMRSCode
        '
        Me.lblMRSCode.AutoSize = True
        Me.lblMRSCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRSCode.Location = New System.Drawing.Point(23, 24)
        Me.lblMRSCode.Name = "lblMRSCode"
        Me.lblMRSCode.Size = New System.Drawing.Size(72, 15)
        Me.lblMRSCode.TabIndex = 0
        Me.lblMRSCode.Text = "MRS Code :"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'lbl_IndentStatus
        '
        Me.lbl_IndentStatus.AutoSize = True
        Me.lbl_IndentStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_IndentStatus.Location = New System.Drawing.Point(476, 24)
        Me.lbl_IndentStatus.Name = "lbl_IndentStatus"
        Me.lbl_IndentStatus.Size = New System.Drawing.Size(70, 13)
        Me.lbl_IndentStatus.TabIndex = 9
        Me.lbl_IndentStatus.Text = "Indent Status"
        '
        'lblIndentStatus
        '
        Me.lblIndentStatus.AutoSize = True
        Me.lblIndentStatus.Location = New System.Drawing.Point(378, 24)
        Me.lblIndentStatus.Name = "lblIndentStatus"
        Me.lblIndentStatus.Size = New System.Drawing.Size(91, 13)
        Me.lblIndentStatus.TabIndex = 8
        Me.lblIndentStatus.Text = "Indent Status :"
        '
        'lbl_IndentDate
        '
        Me.lbl_IndentDate.AutoSize = True
        Me.lbl_IndentDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_IndentDate.Location = New System.Drawing.Point(142, 53)
        Me.lbl_IndentDate.Name = "lbl_IndentDate"
        Me.lbl_IndentDate.Size = New System.Drawing.Size(63, 13)
        Me.lbl_IndentDate.TabIndex = 7
        Me.lbl_IndentDate.Text = "Indent Date"
        '
        'lblIndentDate
        '
        Me.lblIndentDate.AutoSize = True
        Me.lblIndentDate.Location = New System.Drawing.Point(23, 53)
        Me.lblIndentDate.Name = "lblIndentDate"
        Me.lblIndentDate.Size = New System.Drawing.Size(82, 13)
        Me.lblIndentDate.TabIndex = 6
        Me.lblIndentDate.Text = "Indent Date :"
        '
        'txtIndentReamrks
        '
        Me.txtIndentReamrks.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIndentReamrks.Location = New System.Drawing.Point(142, 80)
        Me.txtIndentReamrks.Multiline = True
        Me.txtIndentReamrks.Name = "txtIndentReamrks"
        Me.txtIndentReamrks.Size = New System.Drawing.Size(541, 40)
        Me.txtIndentReamrks.TabIndex = 5
        '
        'dtpRequiredDate
        '
        Me.dtpRequiredDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpRequiredDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpRequiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRequiredDate.Location = New System.Drawing.Point(476, 48)
        Me.dtpRequiredDate.Name = "dtpRequiredDate"
        Me.dtpRequiredDate.Size = New System.Drawing.Size(200, 20)
        Me.dtpRequiredDate.TabIndex = 4
        '
        'lbl_IndentCode
        '
        Me.lbl_IndentCode.AutoSize = True
        Me.lbl_IndentCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_IndentCode.Location = New System.Drawing.Point(142, 24)
        Me.lbl_IndentCode.Name = "lbl_IndentCode"
        Me.lbl_IndentCode.Size = New System.Drawing.Size(65, 13)
        Me.lbl_IndentCode.TabIndex = 3
        Me.lbl_IndentCode.Text = "Indent Code"
        '
        'lblRequiredDate
        '
        Me.lblRequiredDate.AutoSize = True
        Me.lblRequiredDate.Location = New System.Drawing.Point(378, 54)
        Me.lblRequiredDate.Name = "lblRequiredDate"
        Me.lblRequiredDate.Size = New System.Drawing.Size(97, 13)
        Me.lblRequiredDate.TabIndex = 2
        Me.lblRequiredDate.Text = "Required Date :"
        '
        'lblIndentRemarks
        '
        Me.lblIndentRemarks.AutoSize = True
        Me.lblIndentRemarks.Location = New System.Drawing.Point(23, 80)
        Me.lblIndentRemarks.Name = "lblIndentRemarks"
        Me.lblIndentRemarks.Size = New System.Drawing.Size(104, 13)
        Me.lblIndentRemarks.TabIndex = 1
        Me.lblIndentRemarks.Text = "Indent Remarks :"
        '
        'lblIndentCode
        '
        Me.lblIndentCode.AutoSize = True
        Me.lblIndentCode.Location = New System.Drawing.Point(23, 24)
        Me.lblIndentCode.Name = "lblIndentCode"
        Me.lblIndentCode.Size = New System.Drawing.Size(84, 13)
        Me.lblIndentCode.TabIndex = 0
        Me.lblIndentCode.Text = "Indent Code :"
        '
        'frm_MRS_MainStore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TBC_MRS_Master)
        Me.Name = "frm_MRS_MainStore"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TBC_MRS_Master.ResumeLayout(False)
        Me.List.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GBMRSDetail.ResumeLayout(False)
        CType(Me.DGVMRSMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Detail.ResumeLayout(False)
        Me.Detail.PerformLayout()
        Me.GBMRSItemInfo.ResumeLayout(False)
        CType(Me.DGVMRSItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBMRSMaster.ResumeLayout(False)
        Me.GBMRSMaster.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TBC_MRS_Master As System.Windows.Forms.TabControl
    Friend WithEvents List As System.Windows.Forms.TabPage
    Friend WithEvents Detail As System.Windows.Forms.TabPage
    Friend WithEvents GBMRSDetail As System.Windows.Forms.GroupBox
    Friend WithEvents DGVMRSMaster As System.Windows.Forms.DataGridView
    Friend WithEvents lbl_IndentStatus As System.Windows.Forms.Label
    Friend WithEvents lblIndentStatus As System.Windows.Forms.Label
    Friend WithEvents lbl_IndentDate As System.Windows.Forms.Label
    Friend WithEvents lblIndentDate As System.Windows.Forms.Label
    Friend WithEvents txtIndentReamrks As System.Windows.Forms.TextBox
    Friend WithEvents dtpRequiredDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbl_IndentCode As System.Windows.Forms.Label
    Friend WithEvents lblRequiredDate As System.Windows.Forms.Label
    Friend WithEvents lblIndentRemarks As System.Windows.Forms.Label
    Friend WithEvents lblIndentCode As System.Windows.Forms.Label
    Friend WithEvents GBMRSMaster As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_Status As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lbl_MRSDate As System.Windows.Forms.Label
    Friend WithEvents lblMRSDate As System.Windows.Forms.Label
    Friend WithEvents txtMRSRemarks As System.Windows.Forms.TextBox
    Friend WithEvents DTPMRSReqDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbl_MRSCode As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblMRSRemarks As System.Windows.Forms.Label
    Friend WithEvents lblMRSCode As System.Windows.Forms.Label
    Friend WithEvents GBMRSItemInfo As System.Windows.Forms.GroupBox
    Friend WithEvents DGVMRSItem As System.Windows.Forms.DataGridView
    Friend WithEvents lblErrorMsg As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As ImageList
End Class
