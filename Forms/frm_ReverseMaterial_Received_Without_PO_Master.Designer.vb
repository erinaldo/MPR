<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ReverseMaterial_Received_Without_PO_Master
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ReverseMaterial_Received_Without_PO_Master))
        Me.TbRMRN = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GBRMWPM = New System.Windows.Forms.GroupBox()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.FLXGRD_REV_NONSTOCKABLE_ITEMS = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.FLXGRD_MaterialItem = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lbl_Mrn_Date = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_VendorName = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_InvoiceNO = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_RecievedDate = New System.Windows.Forms.Label()
        Me.lblReverse_Code = New System.Windows.Forms.Label()
        Me.lblReverseCode = New System.Windows.Forms.Label()
        Me.txtMrnRemarks = New System.Windows.Forms.TextBox()
        Me.RMRNRemarks = New System.Windows.Forms.Label()
        Me.lblReceivedDate = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.cmbMRNNo = New System.Windows.Forms.ComboBox()
        Me.lblSelectMRNNO = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TbRMRN.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GBRMWPM.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.FLXGRD_REV_NONSTOCKABLE_ITEMS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.FLXGRD_MaterialItem, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Controls.Add(Me.GBRMWPM)
        Me.TabPage1.ForeColor = System.Drawing.Color.White
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(902, 600)
        Me.TabPage1.TabIndex = 0
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.txtSearch)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Location = New System.Drawing.Point(3, 4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(893, 64)
        Me.GroupBox5.TabIndex = 18
        Me.GroupBox5.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(84, 26)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(778, 18)
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
        'GBRMWPM
        '
        Me.GBRMWPM.Controls.Add(Me.dgvList)
        Me.GBRMWPM.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GBRMWPM.ForeColor = System.Drawing.Color.White
        Me.GBRMWPM.Location = New System.Drawing.Point(3, 74)
        Me.GBRMWPM.Name = "GBRMWPM"
        Me.GBRMWPM.Size = New System.Drawing.Size(896, 523)
        Me.GBRMWPM.TabIndex = 0
        Me.GBRMWPM.TabStop = False
        Me.GBRMWPM.Text = "List Reverse Material without PO"
        '
        'dgvList
        '
        Me.dgvList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvList.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvList.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgvList.Location = New System.Drawing.Point(3, 16)
        Me.dgvList.Name = "dgvList"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvList.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgvList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.dgvList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.dgvList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(890, 504)
        Me.dgvList.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.DimGray
        Me.TabPage2.Controls.Add(Me.GroupBox3)
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
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.FLXGRD_REV_NONSTOCKABLE_ITEMS)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.White
        Me.GroupBox3.Location = New System.Drawing.Point(6, 383)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(890, 208)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Non Stockable Items"
        '
        'FLXGRD_REV_NONSTOCKABLE_ITEMS
        '
        Me.FLXGRD_REV_NONSTOCKABLE_ITEMS.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.FLXGRD_REV_NONSTOCKABLE_ITEMS.BackColor = System.Drawing.Color.Silver
        Me.FLXGRD_REV_NONSTOCKABLE_ITEMS.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.FLXGRD_REV_NONSTOCKABLE_ITEMS.Location = New System.Drawing.Point(5, 15)
        Me.FLXGRD_REV_NONSTOCKABLE_ITEMS.Name = "FLXGRD_REV_NONSTOCKABLE_ITEMS"
        Me.FLXGRD_REV_NONSTOCKABLE_ITEMS.Rows.Count = 1
        Me.FLXGRD_REV_NONSTOCKABLE_ITEMS.Rows.DefaultSize = 17
        Me.FLXGRD_REV_NONSTOCKABLE_ITEMS.Size = New System.Drawing.Size(878, 187)
        Me.FLXGRD_REV_NONSTOCKABLE_ITEMS.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("FLXGRD_REV_NONSTOCKABLE_ITEMS.Styles"))
        Me.FLXGRD_REV_NONSTOCKABLE_ITEMS.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.FLXGRD_MaterialItem)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(6, 135)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(890, 244)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Stockable Items"
        '
        'FLXGRD_MaterialItem
        '
        Me.FLXGRD_MaterialItem.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.FLXGRD_MaterialItem.BackColor = System.Drawing.Color.Silver
        Me.FLXGRD_MaterialItem.ColumnInfo = "12,1,0,0,0,85,Columns:"
        Me.FLXGRD_MaterialItem.Location = New System.Drawing.Point(6, 14)
        Me.FLXGRD_MaterialItem.Name = "FLXGRD_MaterialItem"
        Me.FLXGRD_MaterialItem.Rows.Count = 1
        Me.FLXGRD_MaterialItem.Rows.DefaultSize = 17
        Me.FLXGRD_MaterialItem.Size = New System.Drawing.Size(877, 224)
        Me.FLXGRD_MaterialItem.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("FLXGRD_MaterialItem.Styles"))
        Me.FLXGRD_MaterialItem.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lbl_Mrn_Date)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lbl_VendorName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lbl_InvoiceNO)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lbl_RecievedDate)
        Me.GroupBox1.Controls.Add(Me.lblReverse_Code)
        Me.GroupBox1.Controls.Add(Me.lblReverseCode)
        Me.GroupBox1.Controls.Add(Me.txtMrnRemarks)
        Me.GroupBox1.Controls.Add(Me.RMRNRemarks)
        Me.GroupBox1.Controls.Add(Me.lblReceivedDate)
        Me.GroupBox1.Controls.Add(Me.lblFormHeading)
        Me.GroupBox1.Controls.Add(Me.cmbMRNNo)
        Me.GroupBox1.Controls.Add(Me.lblSelectMRNNO)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(6, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(890, 124)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "PO Detail"
        '
        'lbl_Mrn_Date
        '
        Me.lbl_Mrn_Date.AutoSize = True
        Me.lbl_Mrn_Date.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Mrn_Date.ForeColor = System.Drawing.Color.Orange
        Me.lbl_Mrn_Date.Location = New System.Drawing.Point(430, 37)
        Me.lbl_Mrn_Date.Name = "lbl_Mrn_Date"
        Me.lbl_Mrn_Date.Size = New System.Drawing.Size(63, 15)
        Me.lbl_Mrn_Date.TabIndex = 32
        Me.lbl_Mrn_Date.Text = "MRN Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(337, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 15)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "MRN Date :"
        '
        'lbl_VendorName
        '
        Me.lbl_VendorName.AutoSize = True
        Me.lbl_VendorName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_VendorName.ForeColor = System.Drawing.Color.Orange
        Me.lbl_VendorName.Location = New System.Drawing.Point(117, 62)
        Me.lbl_VendorName.Name = "lbl_VendorName"
        Me.lbl_VendorName.Size = New System.Drawing.Size(44, 15)
        Me.lbl_VendorName.TabIndex = 30
        Me.lbl_VendorName.Text = "vendor"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 15)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Vendor :"
        '
        'lbl_InvoiceNO
        '
        Me.lbl_InvoiceNO.AutoSize = True
        Me.lbl_InvoiceNO.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_InvoiceNO.ForeColor = System.Drawing.Color.Orange
        Me.lbl_InvoiceNO.Location = New System.Drawing.Point(430, 62)
        Me.lbl_InvoiceNO.Name = "lbl_InvoiceNO"
        Me.lbl_InvoiceNO.Size = New System.Drawing.Size(65, 15)
        Me.lbl_InvoiceNO.TabIndex = 28
        Me.lbl_InvoiceNO.Text = "invoice no."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(337, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 15)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Invoice No. :"
        '
        'lbl_RecievedDate
        '
        Me.lbl_RecievedDate.AutoSize = True
        Me.lbl_RecievedDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_RecievedDate.ForeColor = System.Drawing.Color.Orange
        Me.lbl_RecievedDate.Location = New System.Drawing.Point(430, 16)
        Me.lbl_RecievedDate.Name = "lbl_RecievedDate"
        Me.lbl_RecievedDate.Size = New System.Drawing.Size(87, 15)
        Me.lbl_RecievedDate.TabIndex = 26
        Me.lbl_RecievedDate.Text = "Received Date"
        '
        'lblReverse_Code
        '
        Me.lblReverse_Code.AutoSize = True
        Me.lblReverse_Code.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReverse_Code.ForeColor = System.Drawing.Color.Orange
        Me.lblReverse_Code.Location = New System.Drawing.Point(117, 16)
        Me.lblReverse_Code.Name = "lblReverse_Code"
        Me.lblReverse_Code.Size = New System.Drawing.Size(86, 15)
        Me.lblReverse_Code.TabIndex = 16
        Me.lblReverse_Code.Text = "Reverse Code"
        '
        'lblReverseCode
        '
        Me.lblReverseCode.AutoSize = True
        Me.lblReverseCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReverseCode.Location = New System.Drawing.Point(14, 18)
        Me.lblReverseCode.Name = "lblReverseCode"
        Me.lblReverseCode.Size = New System.Drawing.Size(86, 15)
        Me.lblReverseCode.TabIndex = 15
        Me.lblReverseCode.Text = "Rev. Doc. No. :"
        '
        'txtMrnRemarks
        '
        Me.txtMrnRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtMrnRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMrnRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMrnRemarks.ForeColor = System.Drawing.Color.White
        Me.txtMrnRemarks.Location = New System.Drawing.Point(120, 80)
        Me.txtMrnRemarks.MaxLength = 500
        Me.txtMrnRemarks.Multiline = True
        Me.txtMrnRemarks.Name = "txtMrnRemarks"
        Me.txtMrnRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMrnRemarks.Size = New System.Drawing.Size(397, 38)
        Me.txtMrnRemarks.TabIndex = 14
        '
        'RMRNRemarks
        '
        Me.RMRNRemarks.AutoSize = True
        Me.RMRNRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RMRNRemarks.Location = New System.Drawing.Point(14, 80)
        Me.RMRNRemarks.Name = "RMRNRemarks"
        Me.RMRNRemarks.Size = New System.Drawing.Size(90, 15)
        Me.RMRNRemarks.TabIndex = 13
        Me.RMRNRemarks.Text = "Rev. Remarks :"
        '
        'lblReceivedDate
        '
        Me.lblReceivedDate.AutoSize = True
        Me.lblReceivedDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceivedDate.Location = New System.Drawing.Point(337, 16)
        Me.lblReceivedDate.Name = "lblReceivedDate"
        Me.lblReceivedDate.Size = New System.Drawing.Size(93, 15)
        Me.lblReceivedDate.TabIndex = 11
        Me.lblReceivedDate.Text = "Rev. Doc. Date :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(698, 15)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(186, 75)
        Me.lblFormHeading.TabIndex = 10
        Me.lblFormHeading.Text = "Reverse MRN" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "of without" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Purchase Order"
        Me.lblFormHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbMRNNo
        '
        Me.cmbMRNNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbMRNNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMRNNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMRNNo.ForeColor = System.Drawing.Color.White
        Me.cmbMRNNo.FormattingEnabled = True
        Me.cmbMRNNo.Location = New System.Drawing.Point(120, 34)
        Me.cmbMRNNo.Name = "cmbMRNNo"
        Me.cmbMRNNo.Size = New System.Drawing.Size(181, 23)
        Me.cmbMRNNo.TabIndex = 1
        '
        'lblSelectMRNNO
        '
        Me.lblSelectMRNNO.AutoSize = True
        Me.lblSelectMRNNO.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectMRNNO.Location = New System.Drawing.Point(14, 39)
        Me.lblSelectMRNNO.Name = "lblSelectMRNNO"
        Me.lblSelectMRNNO.Size = New System.Drawing.Size(96, 15)
        Me.lblSelectMRNNO.TabIndex = 0
        Me.lblSelectMRNNO.Text = "Select MRN No :"
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
        'frm_ReverseMaterial_Received_Without_PO_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TbRMRN)
        Me.Name = "frm_ReverseMaterial_Received_Without_PO_Master"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TbRMRN.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GBRMWPM.ResumeLayout(False)
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.FLXGRD_REV_NONSTOCKABLE_ITEMS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.FLXGRD_MaterialItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TbRMRN As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbMRNNo As System.Windows.Forms.ComboBox
    Friend WithEvents lblSelectMRNNO As System.Windows.Forms.Label
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents RMRNRemarks As System.Windows.Forms.Label
    Friend WithEvents lblReceivedDate As System.Windows.Forms.Label
    Friend WithEvents lblReverse_Code As System.Windows.Forms.Label
    Friend WithEvents lblReverseCode As System.Windows.Forms.Label
    Friend WithEvents txtMrnRemarks As System.Windows.Forms.TextBox
    Friend WithEvents GBRMWPM As System.Windows.Forms.GroupBox
    Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents FLXGRD_MaterialItem As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lbl_RecievedDate As System.Windows.Forms.Label
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents FLXGRD_REV_NONSTOCKABLE_ITEMS As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl_InvoiceNO As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbl_VendorName As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbl_Mrn_Date As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ImageList1 As ImageList
End Class
