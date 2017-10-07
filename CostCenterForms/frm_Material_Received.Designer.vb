<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Material_Received
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Material_Received))
        Me.TBCMaretialReceived = New System.Windows.Forms.TabControl()
        Me.List = New System.Windows.Forms.TabPage()
        Me.GBMaterialReceived = New System.Windows.Forms.GroupBox()
        Me.DGVMaterialIssue = New System.Windows.Forms.DataGridView()
        Me.Detail = New System.Windows.Forms.TabPage()
        Me.GBMaterialIssueItem = New System.Windows.Forms.GroupBox()
        Me.DGVMIItem = New System.Windows.Forms.DataGridView()
        Me.GBIndentMaster = New System.Windows.Forms.GroupBox()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.lbl_MiRemarks = New System.Windows.Forms.Label()
        Me.lblIndentRemarks = New System.Windows.Forms.Label()
        Me.CBMiNo = New System.Windows.Forms.ComboBox()
        Me.lbl_MIStatus = New System.Windows.Forms.Label()
        Me.lblMIStatus = New System.Windows.Forms.Label()
        Me.lbl_Division = New System.Windows.Forms.Label()
        Me.lblDivision = New System.Windows.Forms.Label()
        Me.lblSelectMINo = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lbl_Status = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lbl_MRSDate = New System.Windows.Forms.Label()
        Me.lblMRSDate = New System.Windows.Forms.Label()
        Me.txtMRSRemarks = New System.Windows.Forms.TextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.lbl_MRSCode = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblMRSCode = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.DateTimePicker3 = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TBCMaretialReceived.SuspendLayout()
        Me.List.SuspendLayout()
        Me.GBMaterialReceived.SuspendLayout()
        CType(Me.DGVMaterialIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Detail.SuspendLayout()
        Me.GBMaterialIssueItem.SuspendLayout()
        CType(Me.DGVMIItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBIndentMaster.SuspendLayout()
        Me.SuspendLayout()
        '
        'TBCMaretialReceived
        '
        Me.TBCMaretialReceived.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TBCMaretialReceived.Controls.Add(Me.List)
        Me.TBCMaretialReceived.Controls.Add(Me.Detail)
        Me.TBCMaretialReceived.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TBCMaretialReceived.ImageList = Me.ImageList1
        Me.TBCMaretialReceived.Location = New System.Drawing.Point(0, 0)
        Me.TBCMaretialReceived.Name = "TBCMaretialReceived"
        Me.TBCMaretialReceived.SelectedIndex = 0
        Me.TBCMaretialReceived.Size = New System.Drawing.Size(910, 630)
        Me.TBCMaretialReceived.TabIndex = 0
        '
        'List
        '
        Me.List.BackColor = System.Drawing.Color.DimGray
        Me.List.Controls.Add(Me.GBMaterialReceived)
        Me.List.ImageIndex = 0
        Me.List.Location = New System.Drawing.Point(4, 26)
        Me.List.Name = "List"
        Me.List.Padding = New System.Windows.Forms.Padding(3)
        Me.List.Size = New System.Drawing.Size(902, 600)
        Me.List.TabIndex = 0
        '
        'GBMaterialReceived
        '
        Me.GBMaterialReceived.Controls.Add(Me.DGVMaterialIssue)
        Me.GBMaterialReceived.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBMaterialReceived.ForeColor = System.Drawing.Color.White
        Me.GBMaterialReceived.Location = New System.Drawing.Point(25, 20)
        Me.GBMaterialReceived.Name = "GBMaterialReceived"
        Me.GBMaterialReceived.Size = New System.Drawing.Size(852, 555)
        Me.GBMaterialReceived.TabIndex = 8
        Me.GBMaterialReceived.TabStop = False
        Me.GBMaterialReceived.Text = "Material Received"
        '
        'DGVMaterialIssue
        '
        Me.DGVMaterialIssue.AllowUserToAddRows = False
        Me.DGVMaterialIssue.AllowUserToDeleteRows = False
        Me.DGVMaterialIssue.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMaterialIssue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVMaterialIssue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVMaterialIssue.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMaterialIssue.Location = New System.Drawing.Point(3, 17)
        Me.DGVMaterialIssue.Name = "DGVMaterialIssue"
        Me.DGVMaterialIssue.ReadOnly = True
        Me.DGVMaterialIssue.RowHeadersVisible = False
        Me.DGVMaterialIssue.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMaterialIssue.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVMaterialIssue.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVMaterialIssue.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.DGVMaterialIssue.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVMaterialIssue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVMaterialIssue.Size = New System.Drawing.Size(846, 535)
        Me.DGVMaterialIssue.TabIndex = 1
        '
        'Detail
        '
        Me.Detail.BackColor = System.Drawing.Color.DimGray
        Me.Detail.Controls.Add(Me.GBMaterialIssueItem)
        Me.Detail.Controls.Add(Me.GBIndentMaster)
        Me.Detail.ForeColor = System.Drawing.Color.White
        Me.Detail.ImageIndex = 1
        Me.Detail.Location = New System.Drawing.Point(4, 26)
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New System.Windows.Forms.Padding(3)
        Me.Detail.Size = New System.Drawing.Size(902, 600)
        Me.Detail.TabIndex = 1
        '
        'GBMaterialIssueItem
        '
        Me.GBMaterialIssueItem.Controls.Add(Me.DGVMIItem)
        Me.GBMaterialIssueItem.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBMaterialIssueItem.ForeColor = System.Drawing.Color.White
        Me.GBMaterialIssueItem.Location = New System.Drawing.Point(41, 194)
        Me.GBMaterialIssueItem.Name = "GBMaterialIssueItem"
        Me.GBMaterialIssueItem.Size = New System.Drawing.Size(819, 365)
        Me.GBMaterialIssueItem.TabIndex = 6
        Me.GBMaterialIssueItem.TabStop = False
        Me.GBMaterialIssueItem.Text = "Material Received Detail"
        '
        'DGVMIItem
        '
        Me.DGVMIItem.AllowUserToAddRows = False
        Me.DGVMIItem.AllowUserToDeleteRows = False
        Me.DGVMIItem.AllowUserToOrderColumns = True
        Me.DGVMIItem.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMIItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVMIItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVMIItem.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMIItem.Location = New System.Drawing.Point(3, 17)
        Me.DGVMIItem.Name = "DGVMIItem"
        Me.DGVMIItem.RowHeadersVisible = False
        Me.DGVMIItem.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVMIItem.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVMIItem.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVMIItem.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.DGVMIItem.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVMIItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVMIItem.Size = New System.Drawing.Size(813, 345)
        Me.DGVMIItem.TabIndex = 1
        '
        'GBIndentMaster
        '
        Me.GBIndentMaster.Controls.Add(Me.lblFormHeading)
        Me.GBIndentMaster.Controls.Add(Me.lbl_MiRemarks)
        Me.GBIndentMaster.Controls.Add(Me.lblIndentRemarks)
        Me.GBIndentMaster.Controls.Add(Me.CBMiNo)
        Me.GBIndentMaster.Controls.Add(Me.lbl_MIStatus)
        Me.GBIndentMaster.Controls.Add(Me.lblMIStatus)
        Me.GBIndentMaster.Controls.Add(Me.lbl_Division)
        Me.GBIndentMaster.Controls.Add(Me.lblDivision)
        Me.GBIndentMaster.Controls.Add(Me.lblSelectMINo)
        Me.GBIndentMaster.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBIndentMaster.ForeColor = System.Drawing.Color.White
        Me.GBIndentMaster.Location = New System.Drawing.Point(44, 28)
        Me.GBIndentMaster.Name = "GBIndentMaster"
        Me.GBIndentMaster.Size = New System.Drawing.Size(814, 135)
        Me.GBIndentMaster.TabIndex = 5
        Me.GBIndentMaster.TabStop = False
        Me.GBIndentMaster.Text = "Material Received Master"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(595, 16)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(211, 25)
        Me.lblFormHeading.TabIndex = 29
        Me.lblFormHeading.Text = "Material Received"
        '
        'lbl_MiRemarks
        '
        Me.lbl_MiRemarks.AutoSize = True
        Me.lbl_MiRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MiRemarks.Location = New System.Drawing.Point(142, 90)
        Me.lbl_MiRemarks.Name = "lbl_MiRemarks"
        Me.lbl_MiRemarks.Size = New System.Drawing.Size(58, 15)
        Me.lbl_MiRemarks.TabIndex = 16
        Me.lbl_MiRemarks.Text = "Remarks"
        '
        'lblIndentRemarks
        '
        Me.lblIndentRemarks.AutoSize = True
        Me.lblIndentRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIndentRemarks.Location = New System.Drawing.Point(23, 87)
        Me.lblIndentRemarks.Name = "lblIndentRemarks"
        Me.lblIndentRemarks.Size = New System.Drawing.Size(64, 15)
        Me.lblIndentRemarks.TabIndex = 15
        Me.lblIndentRemarks.Text = "Remarks :"
        '
        'CBMiNo
        '
        Me.CBMiNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CBMiNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CBMiNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBMiNo.ForeColor = System.Drawing.Color.White
        Me.CBMiNo.FormattingEnabled = True
        Me.CBMiNo.Location = New System.Drawing.Point(142, 21)
        Me.CBMiNo.Name = "CBMiNo"
        Me.CBMiNo.Size = New System.Drawing.Size(392, 23)
        Me.CBMiNo.TabIndex = 10
        '
        'lbl_MIStatus
        '
        Me.lbl_MIStatus.AutoSize = True
        Me.lbl_MIStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MIStatus.Location = New System.Drawing.Point(483, 58)
        Me.lbl_MIStatus.Name = "lbl_MIStatus"
        Me.lbl_MIStatus.Size = New System.Drawing.Size(57, 15)
        Me.lbl_MIStatus.TabIndex = 9
        Me.lbl_MIStatus.Text = "MI Status"
        '
        'lblMIStatus
        '
        Me.lblMIStatus.AutoSize = True
        Me.lblMIStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMIStatus.Location = New System.Drawing.Point(378, 58)
        Me.lblMIStatus.Name = "lblMIStatus"
        Me.lblMIStatus.Size = New System.Drawing.Size(63, 15)
        Me.lblMIStatus.TabIndex = 8
        Me.lblMIStatus.Text = "MI Status :"
        '
        'lbl_Division
        '
        Me.lbl_Division.AutoSize = True
        Me.lbl_Division.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Division.Location = New System.Drawing.Point(142, 58)
        Me.lbl_Division.Name = "lbl_Division"
        Me.lbl_Division.Size = New System.Drawing.Size(54, 15)
        Me.lbl_Division.TabIndex = 7
        Me.lbl_Division.Text = "Division "
        '
        'lblDivision
        '
        Me.lblDivision.AutoSize = True
        Me.lblDivision.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivision.Location = New System.Drawing.Point(23, 58)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(57, 15)
        Me.lblDivision.TabIndex = 6
        Me.lblDivision.Text = "Division :"
        '
        'lblSelectMINo
        '
        Me.lblSelectMINo.AutoSize = True
        Me.lblSelectMINo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectMINo.Location = New System.Drawing.Point(23, 24)
        Me.lblSelectMINo.Name = "lblSelectMINo"
        Me.lblSelectMINo.Size = New System.Drawing.Size(81, 15)
        Me.lblSelectMINo.TabIndex = 0
        Me.lblSelectMINo.Text = "Select MI No :"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'lbl_Status
        '
        Me.lbl_Status.AutoSize = True
        Me.lbl_Status.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Status.Location = New System.Drawing.Point(476, 24)
        Me.lbl_Status.Name = "lbl_Status"
        Me.lbl_Status.Size = New System.Drawing.Size(70, 13)
        Me.lbl_Status.TabIndex = 9
        Me.lbl_Status.Text = "Indent Status"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(378, 24)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(91, 13)
        Me.lblStatus.TabIndex = 8
        Me.lblStatus.Text = "Indent Status :"
        '
        'lbl_MRSDate
        '
        Me.lbl_MRSDate.AutoSize = True
        Me.lbl_MRSDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MRSDate.Location = New System.Drawing.Point(142, 53)
        Me.lbl_MRSDate.Name = "lbl_MRSDate"
        Me.lbl_MRSDate.Size = New System.Drawing.Size(57, 13)
        Me.lbl_MRSDate.TabIndex = 7
        Me.lbl_MRSDate.Text = "MRS Date"
        '
        'lblMRSDate
        '
        Me.lblMRSDate.AutoSize = True
        Me.lblMRSDate.Location = New System.Drawing.Point(23, 53)
        Me.lblMRSDate.Name = "lblMRSDate"
        Me.lblMRSDate.Size = New System.Drawing.Size(73, 13)
        Me.lblMRSDate.TabIndex = 6
        Me.lblMRSDate.Text = "MRS Date :"
        '
        'txtMRSRemarks
        '
        Me.txtMRSRemarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRSRemarks.Location = New System.Drawing.Point(142, 80)
        Me.txtMRSRemarks.Multiline = True
        Me.txtMRSRemarks.Name = "txtMRSRemarks"
        Me.txtMRSRemarks.Size = New System.Drawing.Size(541, 40)
        Me.txtMRSRemarks.TabIndex = 5
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(476, 48)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker1.TabIndex = 4
        '
        'lbl_MRSCode
        '
        Me.lbl_MRSCode.AutoSize = True
        Me.lbl_MRSCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MRSCode.Location = New System.Drawing.Point(142, 24)
        Me.lbl_MRSCode.Name = "lbl_MRSCode"
        Me.lbl_MRSCode.Size = New System.Drawing.Size(59, 13)
        Me.lbl_MRSCode.TabIndex = 3
        Me.lbl_MRSCode.Text = "MRS Code"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(378, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Required Date :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(23, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(95, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "MRS Remarks :"
        '
        'lblMRSCode
        '
        Me.lblMRSCode.AutoSize = True
        Me.lblMRSCode.Location = New System.Drawing.Point(23, 24)
        Me.lblMRSCode.Name = "lblMRSCode"
        Me.lblMRSCode.Size = New System.Drawing.Size(75, 13)
        Me.lblMRSCode.TabIndex = 0
        Me.lblMRSCode.Text = "MRS Code :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(476, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 13)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Indent Status"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(378, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(91, 13)
        Me.Label12.TabIndex = 8
        Me.Label12.Text = "Indent Status :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(142, 53)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(57, 13)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "MRS Date"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(23, 53)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(73, 13)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "MRS Date :"
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(142, 80)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(541, 40)
        Me.TextBox2.TabIndex = 5
        '
        'DateTimePicker3
        '
        Me.DateTimePicker3.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker3.Location = New System.Drawing.Point(476, 48)
        Me.DateTimePicker3.Name = "DateTimePicker3"
        Me.DateTimePicker3.Size = New System.Drawing.Size(200, 20)
        Me.DateTimePicker3.TabIndex = 4
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(142, 24)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(59, 13)
        Me.Label15.TabIndex = 3
        Me.Label15.Text = "MRS Code"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(378, 54)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(97, 13)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "Required Date :"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(23, 80)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(95, 13)
        Me.Label17.TabIndex = 1
        Me.Label17.Text = "MRS Remarks :"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(23, 24)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(75, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "MRS Code :"
        '
        'frm_Material_Received
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.TBCMaretialReceived)
        Me.Name = "frm_Material_Received"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TBCMaretialReceived.ResumeLayout(False)
        Me.List.ResumeLayout(False)
        Me.GBMaterialReceived.ResumeLayout(False)
        CType(Me.DGVMaterialIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Detail.ResumeLayout(False)
        Me.GBMaterialIssueItem.ResumeLayout(False)
        CType(Me.DGVMIItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBIndentMaster.ResumeLayout(False)
        Me.GBIndentMaster.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TBCMaretialReceived As System.Windows.Forms.TabControl
    Friend WithEvents List As System.Windows.Forms.TabPage
    Friend WithEvents Detail As System.Windows.Forms.TabPage
    Friend WithEvents lbl_Status As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lbl_MRSDate As System.Windows.Forms.Label
    Friend WithEvents lblMRSDate As System.Windows.Forms.Label
    Friend WithEvents txtMRSRemarks As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbl_MRSCode As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblMRSCode As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker3 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents GBMaterialReceived As System.Windows.Forms.GroupBox
    Friend WithEvents DGVMaterialIssue As System.Windows.Forms.DataGridView
    Friend WithEvents GBIndentMaster As System.Windows.Forms.GroupBox
    Friend WithEvents CBMiNo As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_MIStatus As System.Windows.Forms.Label
    Friend WithEvents lblMIStatus As System.Windows.Forms.Label
    Friend WithEvents lbl_Division As System.Windows.Forms.Label
    Friend WithEvents lblDivision As System.Windows.Forms.Label
    Friend WithEvents lblSelectMINo As System.Windows.Forms.Label
    Friend WithEvents GBMaterialIssueItem As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_MiRemarks As System.Windows.Forms.Label
    Friend WithEvents lblIndentRemarks As System.Windows.Forms.Label
    Friend WithEvents DGVMIItem As System.Windows.Forms.DataGridView
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As ImageList
End Class
