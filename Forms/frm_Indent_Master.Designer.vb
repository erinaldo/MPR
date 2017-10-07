<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Indent_Master
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Indent_Master))
        Me.TBCIndentMaster = New System.Windows.Forms.TabControl()
        Me.List = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GBIndentDetail = New System.Windows.Forms.GroupBox()
        Me.DGVIdnetMaster = New System.Windows.Forms.DataGridView()
        Me.Detail = New System.Windows.Forms.TabPage()
        Me.lblErrorMsg = New System.Windows.Forms.Label()
        Me.GBIndentMaster = New System.Windows.Forms.GroupBox()
        Me.lblFormHeading = New System.Windows.Forms.Label()
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
        Me.GBIndentItem = New System.Windows.Forms.GroupBox()
        Me.DGVIndentItem = New System.Windows.Forms.DataGridView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TBCIndentMaster.SuspendLayout()
        Me.List.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GBIndentDetail.SuspendLayout()
        CType(Me.DGVIdnetMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Detail.SuspendLayout()
        Me.GBIndentMaster.SuspendLayout()
        Me.GBIndentItem.SuspendLayout()
        CType(Me.DGVIndentItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TBCIndentMaster
        '
        Me.TBCIndentMaster.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TBCIndentMaster.Controls.Add(Me.List)
        Me.TBCIndentMaster.Controls.Add(Me.Detail)
        Me.TBCIndentMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TBCIndentMaster.ImageList = Me.ImageList1
        Me.TBCIndentMaster.Location = New System.Drawing.Point(0, 0)
        Me.TBCIndentMaster.Name = "TBCIndentMaster"
        Me.TBCIndentMaster.SelectedIndex = 0
        Me.TBCIndentMaster.Size = New System.Drawing.Size(910, 630)
        Me.TBCIndentMaster.TabIndex = 0
        '
        'List
        '
        Me.List.BackColor = System.Drawing.Color.DimGray
        Me.List.Controls.Add(Me.GroupBox1)
        Me.List.Controls.Add(Me.GBIndentDetail)
        Me.List.ForeColor = System.Drawing.Color.White
        Me.List.ImageIndex = 0
        Me.List.Location = New System.Drawing.Point(4, 26)
        Me.List.Name = "List"
        Me.List.Padding = New System.Windows.Forms.Padding(3)
        Me.List.Size = New System.Drawing.Size(902, 600)
        Me.List.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(896, 65)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(85, 26)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(784, 19)
        Me.txtSearch.TabIndex = 15
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(6, 29)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 15)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Search By :"
        '
        'GBIndentDetail
        '
        Me.GBIndentDetail.Controls.Add(Me.DGVIdnetMaster)
        Me.GBIndentDetail.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GBIndentDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBIndentDetail.ForeColor = System.Drawing.Color.White
        Me.GBIndentDetail.Location = New System.Drawing.Point(3, 77)
        Me.GBIndentDetail.Name = "GBIndentDetail"
        Me.GBIndentDetail.Size = New System.Drawing.Size(896, 520)
        Me.GBIndentDetail.TabIndex = 5
        Me.GBIndentDetail.TabStop = False
        Me.GBIndentDetail.Text = "Indent Master"
        '
        'DGVIdnetMaster
        '
        Me.DGVIdnetMaster.AllowUserToAddRows = False
        Me.DGVIdnetMaster.AllowUserToDeleteRows = False
        Me.DGVIdnetMaster.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVIdnetMaster.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVIdnetMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVIdnetMaster.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVIdnetMaster.Location = New System.Drawing.Point(3, 15)
        Me.DGVIdnetMaster.MultiSelect = False
        Me.DGVIdnetMaster.Name = "DGVIdnetMaster"
        Me.DGVIdnetMaster.ReadOnly = True
        Me.DGVIdnetMaster.RowHeadersVisible = False
        Me.DGVIdnetMaster.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVIdnetMaster.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVIdnetMaster.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVIdnetMaster.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkOrange
        Me.DGVIdnetMaster.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVIdnetMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVIdnetMaster.Size = New System.Drawing.Size(890, 500)
        Me.DGVIdnetMaster.TabIndex = 0
        '
        'Detail
        '
        Me.Detail.BackColor = System.Drawing.Color.DimGray
        Me.Detail.Controls.Add(Me.lblErrorMsg)
        Me.Detail.Controls.Add(Me.GBIndentMaster)
        Me.Detail.Controls.Add(Me.GBIndentItem)
        Me.Detail.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lblErrorMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrorMsg.Location = New System.Drawing.Point(284, 167)
        Me.lblErrorMsg.Name = "lblErrorMsg"
        Me.lblErrorMsg.Size = New System.Drawing.Size(0, 15)
        Me.lblErrorMsg.TabIndex = 7
        '
        'GBIndentMaster
        '
        Me.GBIndentMaster.Controls.Add(Me.lblFormHeading)
        Me.GBIndentMaster.Controls.Add(Me.lbl_IndentStatus)
        Me.GBIndentMaster.Controls.Add(Me.lblIndentStatus)
        Me.GBIndentMaster.Controls.Add(Me.lbl_IndentDate)
        Me.GBIndentMaster.Controls.Add(Me.lblIndentDate)
        Me.GBIndentMaster.Controls.Add(Me.txtIndentReamrks)
        Me.GBIndentMaster.Controls.Add(Me.dtpRequiredDate)
        Me.GBIndentMaster.Controls.Add(Me.lbl_IndentCode)
        Me.GBIndentMaster.Controls.Add(Me.lblRequiredDate)
        Me.GBIndentMaster.Controls.Add(Me.lblIndentRemarks)
        Me.GBIndentMaster.Controls.Add(Me.lblIndentCode)
        Me.GBIndentMaster.Dock = System.Windows.Forms.DockStyle.Top
        Me.GBIndentMaster.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBIndentMaster.ForeColor = System.Drawing.Color.White
        Me.GBIndentMaster.Location = New System.Drawing.Point(3, 3)
        Me.GBIndentMaster.Name = "GBIndentMaster"
        Me.GBIndentMaster.Size = New System.Drawing.Size(896, 189)
        Me.GBIndentMaster.TabIndex = 4
        Me.GBIndentMaster.TabStop = False
        Me.GBIndentMaster.Text = "Indent  Master"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(721, 13)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(170, 25)
        Me.lblFormHeading.TabIndex = 29
        Me.lblFormHeading.Text = "Indent Master"
        '
        'lbl_IndentStatus
        '
        Me.lbl_IndentStatus.AutoSize = True
        Me.lbl_IndentStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_IndentStatus.Location = New System.Drawing.Point(476, 32)
        Me.lbl_IndentStatus.Name = "lbl_IndentStatus"
        Me.lbl_IndentStatus.Size = New System.Drawing.Size(70, 13)
        Me.lbl_IndentStatus.TabIndex = 9
        Me.lbl_IndentStatus.Text = "Indent Status"
        '
        'lblIndentStatus
        '
        Me.lblIndentStatus.AutoSize = True
        Me.lblIndentStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIndentStatus.Location = New System.Drawing.Point(378, 32)
        Me.lblIndentStatus.Name = "lblIndentStatus"
        Me.lblIndentStatus.Size = New System.Drawing.Size(85, 15)
        Me.lblIndentStatus.TabIndex = 8
        Me.lblIndentStatus.Text = "Indent Status :"
        '
        'lbl_IndentDate
        '
        Me.lbl_IndentDate.AutoSize = True
        Me.lbl_IndentDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_IndentDate.Location = New System.Drawing.Point(155, 69)
        Me.lbl_IndentDate.Name = "lbl_IndentDate"
        Me.lbl_IndentDate.Size = New System.Drawing.Size(63, 13)
        Me.lbl_IndentDate.TabIndex = 7
        Me.lbl_IndentDate.Text = "Indent Date"
        '
        'lblIndentDate
        '
        Me.lblIndentDate.AutoSize = True
        Me.lblIndentDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIndentDate.Location = New System.Drawing.Point(23, 69)
        Me.lblIndentDate.Name = "lblIndentDate"
        Me.lblIndentDate.Size = New System.Drawing.Size(76, 15)
        Me.lblIndentDate.TabIndex = 6
        Me.lblIndentDate.Text = "Indent Date :"
        '
        'txtIndentReamrks
        '
        Me.txtIndentReamrks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtIndentReamrks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIndentReamrks.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIndentReamrks.ForeColor = System.Drawing.Color.White
        Me.txtIndentReamrks.Location = New System.Drawing.Point(158, 101)
        Me.txtIndentReamrks.MaxLength = 500
        Me.txtIndentReamrks.Multiline = True
        Me.txtIndentReamrks.Name = "txtIndentReamrks"
        Me.txtIndentReamrks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtIndentReamrks.Size = New System.Drawing.Size(687, 70)
        Me.txtIndentReamrks.TabIndex = 5
        '
        'dtpRequiredDate
        '
        Me.dtpRequiredDate.CalendarForeColor = System.Drawing.Color.White
        Me.dtpRequiredDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpRequiredDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpRequiredDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpRequiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRequiredDate.Location = New System.Drawing.Point(476, 64)
        Me.dtpRequiredDate.Name = "dtpRequiredDate"
        Me.dtpRequiredDate.Size = New System.Drawing.Size(121, 20)
        Me.dtpRequiredDate.TabIndex = 4
        '
        'lbl_IndentCode
        '
        Me.lbl_IndentCode.AutoSize = True
        Me.lbl_IndentCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_IndentCode.Location = New System.Drawing.Point(155, 32)
        Me.lbl_IndentCode.Name = "lbl_IndentCode"
        Me.lbl_IndentCode.Size = New System.Drawing.Size(65, 13)
        Me.lbl_IndentCode.TabIndex = 3
        Me.lbl_IndentCode.Text = "Indent Code"
        '
        'lblRequiredDate
        '
        Me.lblRequiredDate.AutoSize = True
        Me.lblRequiredDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRequiredDate.Location = New System.Drawing.Point(378, 70)
        Me.lblRequiredDate.Name = "lblRequiredDate"
        Me.lblRequiredDate.Size = New System.Drawing.Size(93, 15)
        Me.lblRequiredDate.TabIndex = 2
        Me.lblRequiredDate.Text = "Required Date :"
        '
        'lblIndentRemarks
        '
        Me.lblIndentRemarks.AutoSize = True
        Me.lblIndentRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIndentRemarks.Location = New System.Drawing.Point(23, 106)
        Me.lblIndentRemarks.Name = "lblIndentRemarks"
        Me.lblIndentRemarks.Size = New System.Drawing.Size(101, 15)
        Me.lblIndentRemarks.TabIndex = 1
        Me.lblIndentRemarks.Text = "Indent Remarks :"
        '
        'lblIndentCode
        '
        Me.lblIndentCode.AutoSize = True
        Me.lblIndentCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIndentCode.Location = New System.Drawing.Point(23, 32)
        Me.lblIndentCode.Name = "lblIndentCode"
        Me.lblIndentCode.Size = New System.Drawing.Size(80, 15)
        Me.lblIndentCode.TabIndex = 0
        Me.lblIndentCode.Text = "Indent Code :"
        '
        'GBIndentItem
        '
        Me.GBIndentItem.Controls.Add(Me.DGVIndentItem)
        Me.GBIndentItem.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GBIndentItem.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBIndentItem.ForeColor = System.Drawing.Color.White
        Me.GBIndentItem.Location = New System.Drawing.Point(3, 198)
        Me.GBIndentItem.Name = "GBIndentItem"
        Me.GBIndentItem.Size = New System.Drawing.Size(896, 399)
        Me.GBIndentItem.TabIndex = 6
        Me.GBIndentItem.TabStop = False
        Me.GBIndentItem.Text = "List of Items"
        '
        'DGVIndentItem
        '
        Me.DGVIndentItem.AllowUserToAddRows = False
        Me.DGVIndentItem.AllowUserToOrderColumns = True
        Me.DGVIndentItem.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVIndentItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVIndentItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVIndentItem.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVIndentItem.Location = New System.Drawing.Point(3, 17)
        Me.DGVIndentItem.Name = "DGVIndentItem"
        Me.DGVIndentItem.RowHeadersVisible = False
        Me.DGVIndentItem.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVIndentItem.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVIndentItem.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVIndentItem.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkOrange
        Me.DGVIndentItem.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVIndentItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVIndentItem.Size = New System.Drawing.Size(890, 379)
        Me.DGVIndentItem.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'frm_Indent_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightGray
        Me.Controls.Add(Me.TBCIndentMaster)
        Me.Name = "frm_Indent_Master"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TBCIndentMaster.ResumeLayout(False)
        Me.List.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GBIndentDetail.ResumeLayout(False)
        CType(Me.DGVIdnetMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Detail.ResumeLayout(False)
        Me.Detail.PerformLayout()
        Me.GBIndentMaster.ResumeLayout(False)
        Me.GBIndentMaster.PerformLayout()
        Me.GBIndentItem.ResumeLayout(False)
        CType(Me.DGVIndentItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TBCIndentMaster As System.Windows.Forms.TabControl
    Friend WithEvents List As System.Windows.Forms.TabPage
    Friend WithEvents Detail As System.Windows.Forms.TabPage
    Friend WithEvents GBIndentDetail As System.Windows.Forms.GroupBox
    Friend WithEvents DGVIdnetMaster As System.Windows.Forms.DataGridView
    Friend WithEvents GBIndentMaster As System.Windows.Forms.GroupBox
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
    Friend WithEvents GBIndentItem As System.Windows.Forms.GroupBox
    Friend WithEvents DGVIndentItem As System.Windows.Forms.DataGridView
    Friend WithEvents lblErrorMsg As System.Windows.Forms.Label
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As ImageList
End Class
