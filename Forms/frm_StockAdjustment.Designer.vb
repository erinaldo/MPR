<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_StockAdjustment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_StockAdjustment))
        Me.DGVWastageMaster = New System.Windows.Forms.DataGridView()
        Me.TBCWastageMaster = New System.Windows.Forms.TabControl()
        Me.List = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GBWastageMaster = New System.Windows.Forms.GroupBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Detail = New System.Windows.Forms.TabPage()
        Me.lblErrorMsg = New System.Windows.Forms.Label()
        Me.GBWastageItem = New System.Windows.Forms.GroupBox()
        Me.grdAdjustmentItem = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GBWastageMasterInfo = New System.Windows.Forms.GroupBox()
        Me.txtBarcodeSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.lblWastageDate = New System.Windows.Forms.Label()
        Me.txtAdjustmentRemarks = New System.Windows.Forms.TextBox()
        Me.lbladjustmentdate = New System.Windows.Forms.Label()
        Me.lbl_AdjustmentCode = New System.Windows.Forms.Label()
        Me.lblWastageRemarks = New System.Windows.Forms.Label()
        Me.lblWastageCode = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        CType(Me.DGVWastageMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBCWastageMaster.SuspendLayout()
        Me.List.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GBWastageMaster.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Detail.SuspendLayout()
        Me.GBWastageItem.SuspendLayout()
        CType(Me.grdAdjustmentItem, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GroupBox1.BackColor = System.Drawing.Color.DimGray
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(896, 76)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(96, 33)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(767, 18)
        Me.txtSearch.TabIndex = 17
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(17, 34)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 15)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "Search By :"
        '
        'GBWastageMaster
        '
        Me.GBWastageMaster.Controls.Add(Me.DataGridView1)
        Me.GBWastageMaster.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GBWastageMaster.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBWastageMaster.ForeColor = System.Drawing.Color.White
        Me.GBWastageMaster.Location = New System.Drawing.Point(3, 85)
        Me.GBWastageMaster.Name = "GBWastageMaster"
        Me.GBWastageMaster.Size = New System.Drawing.Size(896, 512)
        Me.GBWastageMaster.TabIndex = 6
        Me.GBWastageMaster.TabStop = False
        Me.GBWastageMaster.Text = "Adjustment Master"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.Gray
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DataGridView1.Location = New System.Drawing.Point(3, 17)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DataGridView1.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.DataGridView1.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(890, 492)
        Me.DataGridView1.TabIndex = 0
        '
        'Detail
        '
        Me.Detail.BackColor = System.Drawing.Color.DimGray
        Me.Detail.Controls.Add(Me.lblErrorMsg)
        Me.Detail.Controls.Add(Me.GBWastageItem)
        Me.Detail.Controls.Add(Me.GBWastageMasterInfo)
        Me.Detail.ForeColor = System.Drawing.Color.White
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
        Me.GBWastageItem.Controls.Add(Me.grdAdjustmentItem)
        Me.GBWastageItem.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GBWastageItem.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBWastageItem.ForeColor = System.Drawing.Color.White
        Me.GBWastageItem.Location = New System.Drawing.Point(3, 141)
        Me.GBWastageItem.Name = "GBWastageItem"
        Me.GBWastageItem.Size = New System.Drawing.Size(896, 456)
        Me.GBWastageItem.TabIndex = 7
        Me.GBWastageItem.TabStop = False
        Me.GBWastageItem.Text = "Adjustment  Items"
        '
        'grdAdjustmentItem
        '
        Me.grdAdjustmentItem.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.grdAdjustmentItem.BackColor = System.Drawing.Color.Silver
        Me.grdAdjustmentItem.ColumnInfo = "1,1,0,0,0,90,Columns:0{Width:26;AllowSorting:False;AllowDragging:False;AllowResiz" &
    "ing:False;AllowMerging:True;AllowEditing:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.grdAdjustmentItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAdjustmentItem.Location = New System.Drawing.Point(3, 17)
        Me.grdAdjustmentItem.Name = "grdAdjustmentItem"
        Me.grdAdjustmentItem.Rows.Count = 2
        Me.grdAdjustmentItem.Rows.DefaultSize = 18
        Me.grdAdjustmentItem.Size = New System.Drawing.Size(890, 436)
        Me.grdAdjustmentItem.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("grdAdjustmentItem.Styles"))
        Me.grdAdjustmentItem.TabIndex = 1
        '
        'GBWastageMasterInfo
        '
        Me.GBWastageMasterInfo.Controls.Add(Me.txtBarcodeSearch)
        Me.GBWastageMasterInfo.Controls.Add(Me.Label1)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblFormHeading)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblWastageDate)
        Me.GBWastageMasterInfo.Controls.Add(Me.txtAdjustmentRemarks)
        Me.GBWastageMasterInfo.Controls.Add(Me.lbladjustmentdate)
        Me.GBWastageMasterInfo.Controls.Add(Me.lbl_AdjustmentCode)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblWastageRemarks)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblWastageCode)
        Me.GBWastageMasterInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBWastageMasterInfo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBWastageMasterInfo.ForeColor = System.Drawing.Color.White
        Me.GBWastageMasterInfo.Location = New System.Drawing.Point(3, 3)
        Me.GBWastageMasterInfo.Name = "GBWastageMasterInfo"
        Me.GBWastageMasterInfo.Size = New System.Drawing.Size(896, 132)
        Me.GBWastageMasterInfo.TabIndex = 5
        Me.GBWastageMasterInfo.TabStop = False
        Me.GBWastageMasterInfo.Text = "Adjustment  Master"
        '
        'txtBarcodeSearch
        '
        Me.txtBarcodeSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtBarcodeSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBarcodeSearch.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcodeSearch.ForeColor = System.Drawing.Color.White
        Me.txtBarcodeSearch.Location = New System.Drawing.Point(157, 103)
        Me.txtBarcodeSearch.MaxLength = 100
        Me.txtBarcodeSearch.Name = "txtBarcodeSearch"
        Me.txtBarcodeSearch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBarcodeSearch.Size = New System.Drawing.Size(568, 19)
        Me.txtBarcodeSearch.TabIndex = 30
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 15)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Barcode :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(666, 12)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(225, 25)
        Me.lblFormHeading.TabIndex = 28
        Me.lblFormHeading.Text = "Adjustment Master"
        '
        'lblWastageDate
        '
        Me.lblWastageDate.AutoSize = True
        Me.lblWastageDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWastageDate.Location = New System.Drawing.Point(287, 21)
        Me.lblWastageDate.Name = "lblWastageDate"
        Me.lblWastageDate.Size = New System.Drawing.Size(107, 15)
        Me.lblWastageDate.TabIndex = 6
        Me.lblWastageDate.Text = "Adjustment  Date :"
        '
        'txtAdjustmentRemarks
        '
        Me.txtAdjustmentRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtAdjustmentRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAdjustmentRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdjustmentRemarks.ForeColor = System.Drawing.Color.White
        Me.txtAdjustmentRemarks.Location = New System.Drawing.Point(157, 47)
        Me.txtAdjustmentRemarks.MaxLength = 100
        Me.txtAdjustmentRemarks.Multiline = True
        Me.txtAdjustmentRemarks.Name = "txtAdjustmentRemarks"
        Me.txtAdjustmentRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAdjustmentRemarks.Size = New System.Drawing.Size(568, 45)
        Me.txtAdjustmentRemarks.TabIndex = 5
        '
        'lbladjustmentdate
        '
        Me.lbladjustmentdate.AutoSize = True
        Me.lbladjustmentdate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbladjustmentdate.ForeColor = System.Drawing.Color.Orange
        Me.lbladjustmentdate.Location = New System.Drawing.Point(418, 20)
        Me.lbladjustmentdate.Name = "lbladjustmentdate"
        Me.lbladjustmentdate.Size = New System.Drawing.Size(98, 15)
        Me.lbladjustmentdate.TabIndex = 3
        Me.lbladjustmentdate.Text = "Adjustment Date"
        '
        'lbl_AdjustmentCode
        '
        Me.lbl_AdjustmentCode.AutoSize = True
        Me.lbl_AdjustmentCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_AdjustmentCode.ForeColor = System.Drawing.Color.Orange
        Me.lbl_AdjustmentCode.Location = New System.Drawing.Point(154, 24)
        Me.lbl_AdjustmentCode.Name = "lbl_AdjustmentCode"
        Me.lbl_AdjustmentCode.Size = New System.Drawing.Size(102, 15)
        Me.lbl_AdjustmentCode.TabIndex = 3
        Me.lbl_AdjustmentCode.Text = "Adjustment Code"
        '
        'lblWastageRemarks
        '
        Me.lblWastageRemarks.AutoSize = True
        Me.lblWastageRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWastageRemarks.Location = New System.Drawing.Point(22, 47)
        Me.lblWastageRemarks.Name = "lblWastageRemarks"
        Me.lblWastageRemarks.Size = New System.Drawing.Size(132, 15)
        Me.lblWastageRemarks.TabIndex = 1
        Me.lblWastageRemarks.Text = "Adjustment  Remarks :"
        '
        'lblWastageCode
        '
        Me.lblWastageCode.AutoSize = True
        Me.lblWastageCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWastageCode.Location = New System.Drawing.Point(24, 24)
        Me.lblWastageCode.Name = "lblWastageCode"
        Me.lblWastageCode.Size = New System.Drawing.Size(108, 15)
        Me.lblWastageCode.TabIndex = 0
        Me.lblWastageCode.Text = "Adjustment Code :"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'frm_StockAdjustment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.TBCWastageMaster)
        Me.Name = "frm_StockAdjustment"
        Me.Size = New System.Drawing.Size(910, 630)
        CType(Me.DGVWastageMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBCWastageMaster.ResumeLayout(False)
        Me.List.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GBWastageMaster.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Detail.ResumeLayout(False)
        Me.Detail.PerformLayout()
        Me.GBWastageItem.ResumeLayout(False)
        CType(Me.grdAdjustmentItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBWastageMasterInfo.ResumeLayout(False)
        Me.GBWastageMasterInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGVWastageMaster As System.Windows.Forms.DataGridView
    Friend WithEvents TBCWastageMaster As System.Windows.Forms.TabControl
    Friend WithEvents List As System.Windows.Forms.TabPage
    Friend WithEvents GBWastageMaster As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Detail As System.Windows.Forms.TabPage
    Friend WithEvents lblErrorMsg As System.Windows.Forms.Label
    Friend WithEvents GBWastageItem As System.Windows.Forms.GroupBox
    Friend WithEvents grdAdjustmentItem As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents GBWastageMasterInfo As System.Windows.Forms.GroupBox
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents lblWastageDate As System.Windows.Forms.Label
    Friend WithEvents txtAdjustmentRemarks As System.Windows.Forms.TextBox
    Friend WithEvents lbl_AdjustmentCode As System.Windows.Forms.Label
    Friend WithEvents lblWastageRemarks As System.Windows.Forms.Label
    Friend WithEvents lblWastageCode As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lbladjustmentdate As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents txtBarcodeSearch As TextBox
    Friend WithEvents Label1 As Label
End Class
