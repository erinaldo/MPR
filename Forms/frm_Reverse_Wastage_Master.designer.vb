<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Reverse_Wastage_Master
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Reverse_Wastage_Master))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TBCWastageMaster = New System.Windows.Forms.TabControl()
        Me.List = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GBWastageMaster = New System.Windows.Forms.GroupBox()
        Me.DGVRWastageMaster = New System.Windows.Forms.DataGridView()
        Me.Detail = New System.Windows.Forms.TabPage()
        Me.lblErrorMsg = New System.Windows.Forms.Label()
        Me.GBWastageItem = New System.Windows.Forms.GroupBox()
        Me.grdRWastageItem = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GBWastageMasterInfo = New System.Windows.Forms.GroupBox()
        Me.cmdBoxWastageCode = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.lbl_WastageDate = New System.Windows.Forms.Label()
        Me.lblWastageDate = New System.Windows.Forms.Label()
        Me.txtWatageReamrks = New System.Windows.Forms.TextBox()
        Me.lbl_WastageCode = New System.Windows.Forms.Label()
        Me.lblWastageRemarks = New System.Windows.Forms.Label()
        Me.lblWastageCode = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.dtpRequiredDate = New System.Windows.Forms.DateTimePicker()
        Me.lblRequiredDate = New System.Windows.Forms.Label()
        Me.DGVIndentItem = New System.Windows.Forms.DataGridView()
        Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TBCWastageMaster.SuspendLayout()
        Me.List.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GBWastageMaster.SuspendLayout()
        CType(Me.DGVRWastageMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Detail.SuspendLayout()
        Me.GBWastageItem.SuspendLayout()
        CType(Me.grdRWastageItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBWastageMasterInfo.SuspendLayout()
        CType(Me.DGVIndentItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.TBCWastageMaster.TabIndex = 0
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
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(12, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(879, 64)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(88, 27)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(748, 18)
        Me.txtSearch.TabIndex = 15
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(13, 28)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 15)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Search By :"
        '
        'GBWastageMaster
        '
        Me.GBWastageMaster.Controls.Add(Me.DGVRWastageMaster)
        Me.GBWastageMaster.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBWastageMaster.ForeColor = System.Drawing.Color.White
        Me.GBWastageMaster.Location = New System.Drawing.Point(12, 76)
        Me.GBWastageMaster.Name = "GBWastageMaster"
        Me.GBWastageMaster.Size = New System.Drawing.Size(880, 506)
        Me.GBWastageMaster.TabIndex = 6
        Me.GBWastageMaster.TabStop = False
        Me.GBWastageMaster.Text = "Reverse Wastage"
        '
        'DGVRWastageMaster
        '
        Me.DGVRWastageMaster.AllowUserToAddRows = False
        Me.DGVRWastageMaster.AllowUserToDeleteRows = False
        Me.DGVRWastageMaster.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVRWastageMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVRWastageMaster.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVRWastageMaster.Location = New System.Drawing.Point(3, 19)
        Me.DGVRWastageMaster.Name = "DGVRWastageMaster"
        Me.DGVRWastageMaster.ReadOnly = True
        Me.DGVRWastageMaster.RowHeadersVisible = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVRWastageMaster.RowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVRWastageMaster.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVRWastageMaster.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVRWastageMaster.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVRWastageMaster.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkOrange
        Me.DGVRWastageMaster.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVRWastageMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVRWastageMaster.Size = New System.Drawing.Size(872, 481)
        Me.DGVRWastageMaster.TabIndex = 0
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
        Me.GBWastageItem.Controls.Add(Me.grdRWastageItem)
        Me.GBWastageItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBWastageItem.Location = New System.Drawing.Point(6, 189)
        Me.GBWastageItem.Name = "GBWastageItem"
        Me.GBWastageItem.Size = New System.Drawing.Size(890, 405)
        Me.GBWastageItem.TabIndex = 7
        Me.GBWastageItem.TabStop = False
        '
        'grdRWastageItem
        '
        Me.grdRWastageItem.BackColor = System.Drawing.Color.Silver
        Me.grdRWastageItem.ColumnInfo = "1,1,0,0,0,85,Columns:0{Width:26;AllowSorting:False;AllowDragging:False;AllowResiz" &
    "ing:False;AllowMerging:True;AllowEditing:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.grdRWastageItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdRWastageItem.Location = New System.Drawing.Point(3, 16)
        Me.grdRWastageItem.Name = "grdRWastageItem"
        Me.grdRWastageItem.Rows.Count = 2
        Me.grdRWastageItem.Rows.DefaultSize = 17
        Me.grdRWastageItem.Size = New System.Drawing.Size(884, 386)
        Me.grdRWastageItem.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("grdRWastageItem.Styles"))
        Me.grdRWastageItem.TabIndex = 2
        '
        'GBWastageMasterInfo
        '
        Me.GBWastageMasterInfo.Controls.Add(Me.cmdBoxWastageCode)
        Me.GBWastageMasterInfo.Controls.Add(Me.Label1)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblFormHeading)
        Me.GBWastageMasterInfo.Controls.Add(Me.lbl_WastageDate)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblWastageDate)
        Me.GBWastageMasterInfo.Controls.Add(Me.txtWatageReamrks)
        Me.GBWastageMasterInfo.Controls.Add(Me.lbl_WastageCode)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblWastageRemarks)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblWastageCode)
        Me.GBWastageMasterInfo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBWastageMasterInfo.ForeColor = System.Drawing.Color.White
        Me.GBWastageMasterInfo.Location = New System.Drawing.Point(5, 5)
        Me.GBWastageMasterInfo.Name = "GBWastageMasterInfo"
        Me.GBWastageMasterInfo.Size = New System.Drawing.Size(892, 177)
        Me.GBWastageMasterInfo.TabIndex = 5
        Me.GBWastageMasterInfo.TabStop = False
        Me.GBWastageMasterInfo.Text = "Wastage  Master"
        '
        'cmdBoxWastageCode
        '
        Me.cmdBoxWastageCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmdBoxWastageCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmdBoxWastageCode.ForeColor = System.Drawing.Color.White
        Me.cmdBoxWastageCode.FormattingEnabled = True
        Me.cmdBoxWastageCode.Location = New System.Drawing.Point(196, 24)
        Me.cmdBoxWastageCode.Name = "cmdBoxWastageCode"
        Me.cmdBoxWastageCode.Size = New System.Drawing.Size(141, 23)
        Me.cmdBoxWastageCode.TabIndex = 30
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 15)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Select Wastage No :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.BackColor = System.Drawing.Color.Transparent
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(684, 11)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(206, 25)
        Me.lblFormHeading.TabIndex = 28
        Me.lblFormHeading.Text = "Reverse Wastage"
        '
        'lbl_WastageDate
        '
        Me.lbl_WastageDate.AutoSize = True
        Me.lbl_WastageDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_WastageDate.ForeColor = System.Drawing.Color.Orange
        Me.lbl_WastageDate.Location = New System.Drawing.Point(517, 59)
        Me.lbl_WastageDate.Name = "lbl_WastageDate"
        Me.lbl_WastageDate.Size = New System.Drawing.Size(141, 16)
        Me.lbl_WastageDate.TabIndex = 7
        Me.lbl_WastageDate.Text = "Reverse Wastage Date"
        '
        'lblWastageDate
        '
        Me.lblWastageDate.AutoSize = True
        Me.lblWastageDate.Location = New System.Drawing.Point(364, 59)
        Me.lblWastageDate.Name = "lblWastageDate"
        Me.lblWastageDate.Size = New System.Drawing.Size(140, 15)
        Me.lblWastageDate.TabIndex = 6
        Me.lblWastageDate.Text = "Reverse Wastage Date :"
        '
        'txtWatageReamrks
        '
        Me.txtWatageReamrks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtWatageReamrks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtWatageReamrks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWatageReamrks.ForeColor = System.Drawing.Color.White
        Me.txtWatageReamrks.Location = New System.Drawing.Point(197, 91)
        Me.txtWatageReamrks.MaxLength = 100
        Me.txtWatageReamrks.Multiline = True
        Me.txtWatageReamrks.Name = "txtWatageReamrks"
        Me.txtWatageReamrks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWatageReamrks.Size = New System.Drawing.Size(664, 71)
        Me.txtWatageReamrks.TabIndex = 5
        Me.txtWatageReamrks.Text = " "
        '
        'lbl_WastageCode
        '
        Me.lbl_WastageCode.AutoSize = True
        Me.lbl_WastageCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_WastageCode.ForeColor = System.Drawing.Color.Orange
        Me.lbl_WastageCode.Location = New System.Drawing.Point(194, 59)
        Me.lbl_WastageCode.Name = "lbl_WastageCode"
        Me.lbl_WastageCode.Size = New System.Drawing.Size(144, 16)
        Me.lbl_WastageCode.TabIndex = 3
        Me.lbl_WastageCode.Text = "Reverse Wastage Code"
        '
        'lblWastageRemarks
        '
        Me.lblWastageRemarks.AutoSize = True
        Me.lblWastageRemarks.Location = New System.Drawing.Point(19, 94)
        Me.lblWastageRemarks.Name = "lblWastageRemarks"
        Me.lblWastageRemarks.Size = New System.Drawing.Size(168, 15)
        Me.lblWastageRemarks.TabIndex = 1
        Me.lblWastageRemarks.Text = "Reverse Wastage  Remarks :"
        '
        'lblWastageCode
        '
        Me.lblWastageCode.AutoSize = True
        Me.lblWastageCode.Location = New System.Drawing.Point(19, 59)
        Me.lblWastageCode.Name = "lblWastageCode"
        Me.lblWastageCode.Size = New System.Drawing.Size(144, 15)
        Me.lblWastageCode.TabIndex = 0
        Me.lblWastageCode.Text = "Reverse Wastage Code :"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
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
        'lblRequiredDate
        '
        Me.lblRequiredDate.AutoSize = True
        Me.lblRequiredDate.Location = New System.Drawing.Point(378, 54)
        Me.lblRequiredDate.Name = "lblRequiredDate"
        Me.lblRequiredDate.Size = New System.Drawing.Size(97, 13)
        Me.lblRequiredDate.TabIndex = 2
        Me.lblRequiredDate.Text = "Required Date :"
        '
        'DGVIndentItem
        '
        Me.DGVIndentItem.AllowUserToOrderColumns = True
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVIndentItem.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVIndentItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVIndentItem.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGVIndentItem.Location = New System.Drawing.Point(23, 25)
        Me.DGVIndentItem.Name = "DGVIndentItem"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVIndentItem.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DGVIndentItem.Size = New System.Drawing.Size(662, 336)
        Me.DGVIndentItem.TabIndex = 0
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
        'frm_Reverse_Wastage_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TBCWastageMaster)
        Me.Name = "frm_Reverse_Wastage_Master"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TBCWastageMaster.ResumeLayout(False)
        Me.List.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GBWastageMaster.ResumeLayout(False)
        CType(Me.DGVRWastageMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Detail.ResumeLayout(False)
        Me.Detail.PerformLayout()
        Me.GBWastageItem.ResumeLayout(False)
        CType(Me.grdRWastageItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBWastageMasterInfo.ResumeLayout(False)
        Me.GBWastageMasterInfo.PerformLayout()
        CType(Me.DGVIndentItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TBCWastageMaster As System.Windows.Forms.TabControl
    Friend WithEvents List As System.Windows.Forms.TabPage
    Friend WithEvents Detail As System.Windows.Forms.TabPage
    Friend WithEvents GBWastageMaster As System.Windows.Forms.GroupBox
    Friend WithEvents DGVRWastageMaster As System.Windows.Forms.DataGridView
    Friend WithEvents dtpRequiredDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblRequiredDate As System.Windows.Forms.Label
    Friend WithEvents DGVIndentItem As System.Windows.Forms.DataGridView
    Friend WithEvents GBWastageMasterInfo As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_WastageDate As System.Windows.Forms.Label
    Friend WithEvents lblWastageDate As System.Windows.Forms.Label
    Friend WithEvents txtWatageReamrks As System.Windows.Forms.TextBox
    Friend WithEvents lbl_WastageCode As System.Windows.Forms.Label
    Friend WithEvents lblWastageRemarks As System.Windows.Forms.Label
    Friend WithEvents lblWastageCode As System.Windows.Forms.Label
    Friend WithEvents GBWastageItem As System.Windows.Forms.GroupBox
    Friend WithEvents lblErrorMsg As System.Windows.Forms.Label
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdBoxWastageCode As System.Windows.Forms.ComboBox
    Friend WithEvents grdRWastageItem As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As ImageList
End Class
