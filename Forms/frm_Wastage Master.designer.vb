<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Wastage_Master
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Wastage_Master))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GBIndentDetail = New System.Windows.Forms.GroupBox()
        Me.DGVIdnetMaster = New System.Windows.Forms.DataGridView()
        Me.TBCWastageMaster = New System.Windows.Forms.TabControl()
        Me.List = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GBWastageMaster = New System.Windows.Forms.GroupBox()
        Me.DGVWastageMaster = New System.Windows.Forms.DataGridView()
        Me.Detail = New System.Windows.Forms.TabPage()
        Me.lblErrorMsg = New System.Windows.Forms.Label()
        Me.GBWastageItem = New System.Windows.Forms.GroupBox()
        Me.grdWastageItem = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GBWastageMasterInfo = New System.Windows.Forms.GroupBox()
        Me.lbl_WastageDate = New System.Windows.Forms.Label()
        Me.lblWastageDate = New System.Windows.Forms.Label()
        Me.txtWatageReamrks = New System.Windows.Forms.TextBox()
        Me.lbl_WastageCode = New System.Windows.Forms.Label()
        Me.lblWastageRemarks = New System.Windows.Forms.Label()
        Me.lblWastageCode = New System.Windows.Forms.Label()
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
        Me.DGVIndentItem = New System.Windows.Forms.DataGridView()
        Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GBIndentDetail.SuspendLayout()
        CType(Me.DGVIdnetMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TBCWastageMaster.SuspendLayout()
        Me.List.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GBWastageMaster.SuspendLayout()
        CType(Me.DGVWastageMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Detail.SuspendLayout()
        Me.GBWastageItem.SuspendLayout()
        CType(Me.grdWastageItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBWastageMasterInfo.SuspendLayout()
        CType(Me.DGVIndentItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GBIndentDetail
        '
        Me.GBIndentDetail.Controls.Add(Me.DGVIdnetMaster)
        Me.GBIndentDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBIndentDetail.Location = New System.Drawing.Point(30, 20)
        Me.GBIndentDetail.Name = "GBIndentDetail"
        Me.GBIndentDetail.Size = New System.Drawing.Size(714, 536)
        Me.GBIndentDetail.TabIndex = 5
        Me.GBIndentDetail.TabStop = False
        Me.GBIndentDetail.Text = "Indent Master"
        '
        'DGVIdnetMaster
        '
        Me.DGVIdnetMaster.AllowUserToAddRows = False
        Me.DGVIdnetMaster.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVIdnetMaster.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVIdnetMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVIdnetMaster.DefaultCellStyle = DataGridViewCellStyle2
        Me.DGVIdnetMaster.Location = New System.Drawing.Point(23, 25)
        Me.DGVIdnetMaster.Name = "DGVIdnetMaster"
        Me.DGVIdnetMaster.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVIdnetMaster.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DGVIdnetMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVIdnetMaster.Size = New System.Drawing.Size(662, 489)
        Me.DGVIdnetMaster.TabIndex = 0
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
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(896, 79)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(96, 35)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(785, 18)
        Me.txtSearch.TabIndex = 16
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(17, 35)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 15)
        Me.Label13.TabIndex = 1
        Me.Label13.Text = "Search By :"
        '
        'GBWastageMaster
        '
        Me.GBWastageMaster.Controls.Add(Me.DGVWastageMaster)
        Me.GBWastageMaster.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBWastageMaster.ForeColor = System.Drawing.Color.White
        Me.GBWastageMaster.Location = New System.Drawing.Point(3, 88)
        Me.GBWastageMaster.Name = "GBWastageMaster"
        Me.GBWastageMaster.Size = New System.Drawing.Size(893, 507)
        Me.GBWastageMaster.TabIndex = 6
        Me.GBWastageMaster.TabStop = False
        Me.GBWastageMaster.Text = "List of Wastages"
        '
        'DGVWastageMaster
        '
        Me.DGVWastageMaster.AllowUserToAddRows = False
        Me.DGVWastageMaster.AllowUserToDeleteRows = False
        Me.DGVWastageMaster.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVWastageMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVWastageMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVWastageMaster.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVWastageMaster.Location = New System.Drawing.Point(3, 17)
        Me.DGVWastageMaster.Name = "DGVWastageMaster"
        Me.DGVWastageMaster.ReadOnly = True
        Me.DGVWastageMaster.RowHeadersVisible = False
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVWastageMaster.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DGVWastageMaster.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVWastageMaster.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVWastageMaster.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVWastageMaster.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.DGVWastageMaster.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVWastageMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVWastageMaster.Size = New System.Drawing.Size(887, 487)
        Me.DGVWastageMaster.TabIndex = 0
        '
        'Detail
        '
        Me.Detail.BackColor = System.Drawing.Color.DimGray
        Me.Detail.Controls.Add(Me.GBWastageMasterInfo)
        Me.Detail.Controls.Add(Me.lblErrorMsg)
        Me.Detail.Controls.Add(Me.GBWastageItem)
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
        Me.GBWastageItem.Controls.Add(Me.grdWastageItem)
        Me.GBWastageItem.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBWastageItem.ForeColor = System.Drawing.Color.White
        Me.GBWastageItem.Location = New System.Drawing.Point(-4, 164)
        Me.GBWastageItem.Name = "GBWastageItem"
        Me.GBWastageItem.Size = New System.Drawing.Size(910, 440)
        Me.GBWastageItem.TabIndex = 7
        Me.GBWastageItem.TabStop = False
        '
        'grdWastageItem
        '
        Me.grdWastageItem.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.grdWastageItem.BackColor = System.Drawing.Color.Silver
        Me.grdWastageItem.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.grdWastageItem.ColumnInfo = "1,1,0,0,0,90,Columns:0{Width:26;AllowSorting:False;AllowDragging:False;AllowResiz" &
    "ing:False;AllowMerging:True;AllowEditing:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.grdWastageItem.Location = New System.Drawing.Point(10, 25)
        Me.grdWastageItem.Name = "grdWastageItem"
        Me.grdWastageItem.Rows.Count = 2
        Me.grdWastageItem.Rows.DefaultSize = 18
        Me.grdWastageItem.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.grdWastageItem.Size = New System.Drawing.Size(890, 404)
        Me.grdWastageItem.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("grdWastageItem.Styles"))
        Me.grdWastageItem.TabIndex = 1
        '
        'GBWastageMasterInfo
        '
        Me.GBWastageMasterInfo.Controls.Add(Me.Label1)
        Me.GBWastageMasterInfo.Controls.Add(Me.lbl_WastageDate)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblWastageDate)
        Me.GBWastageMasterInfo.Controls.Add(Me.txtWatageReamrks)
        Me.GBWastageMasterInfo.Controls.Add(Me.lbl_WastageCode)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblWastageRemarks)
        Me.GBWastageMasterInfo.Controls.Add(Me.lblWastageCode)
        Me.GBWastageMasterInfo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBWastageMasterInfo.ForeColor = System.Drawing.Color.White
        Me.GBWastageMasterInfo.Location = New System.Drawing.Point(-4, -9)
        Me.GBWastageMasterInfo.Name = "GBWastageMasterInfo"
        Me.GBWastageMasterInfo.Size = New System.Drawing.Size(910, 198)
        Me.GBWastageMasterInfo.TabIndex = 5
        Me.GBWastageMasterInfo.TabStop = False
        '
        'lbl_WastageDate
        '
        Me.lbl_WastageDate.AutoSize = True
        Me.lbl_WastageDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_WastageDate.ForeColor = System.Drawing.Color.Orange
        Me.lbl_WastageDate.Location = New System.Drawing.Point(147, 76)
        Me.lbl_WastageDate.Name = "lbl_WastageDate"
        Me.lbl_WastageDate.Size = New System.Drawing.Size(88, 13)
        Me.lbl_WastageDate.TabIndex = 7
        Me.lbl_WastageDate.Text = "Wastage Date"
        '
        'lblWastageDate
        '
        Me.lblWastageDate.AutoSize = True
        Me.lblWastageDate.Location = New System.Drawing.Point(19, 75)
        Me.lblWastageDate.Name = "lblWastageDate"
        Me.lblWastageDate.Size = New System.Drawing.Size(94, 15)
        Me.lblWastageDate.TabIndex = 6
        Me.lblWastageDate.Text = "Wastage  Date :"
        '
        'txtWatageReamrks
        '
        Me.txtWatageReamrks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtWatageReamrks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtWatageReamrks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWatageReamrks.ForeColor = System.Drawing.Color.White
        Me.txtWatageReamrks.Location = New System.Drawing.Point(150, 114)
        Me.txtWatageReamrks.MaxLength = 100
        Me.txtWatageReamrks.Multiline = True
        Me.txtWatageReamrks.Name = "txtWatageReamrks"
        Me.txtWatageReamrks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWatageReamrks.Size = New System.Drawing.Size(683, 63)
        Me.txtWatageReamrks.TabIndex = 5
        '
        'lbl_WastageCode
        '
        Me.lbl_WastageCode.AutoSize = True
        Me.lbl_WastageCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_WastageCode.ForeColor = System.Drawing.Color.Orange
        Me.lbl_WastageCode.Location = New System.Drawing.Point(147, 40)
        Me.lbl_WastageCode.Name = "lbl_WastageCode"
        Me.lbl_WastageCode.Size = New System.Drawing.Size(90, 13)
        Me.lbl_WastageCode.TabIndex = 3
        Me.lbl_WastageCode.Text = "Wastage Code"
        '
        'lblWastageRemarks
        '
        Me.lblWastageRemarks.AutoSize = True
        Me.lblWastageRemarks.Location = New System.Drawing.Point(19, 114)
        Me.lblWastageRemarks.Name = "lblWastageRemarks"
        Me.lblWastageRemarks.Size = New System.Drawing.Size(119, 15)
        Me.lblWastageRemarks.TabIndex = 1
        Me.lblWastageRemarks.Text = "Wastage  Remarks :"
        '
        'lblWastageCode
        '
        Me.lblWastageCode.AutoSize = True
        Me.lblWastageCode.Location = New System.Drawing.Point(19, 37)
        Me.lblWastageCode.Name = "lblWastageCode"
        Me.lblWastageCode.Size = New System.Drawing.Size(95, 15)
        Me.lblWastageCode.TabIndex = 0
        Me.lblWastageCode.Text = "Wastage Code :"
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
        Me.txtIndentReamrks.Name = "txtIndentReamrks"
        Me.txtIndentReamrks.Size = New System.Drawing.Size(541, 20)
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
        Me.lblIndentRemarks.Location = New System.Drawing.Point(23, 83)
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
        'DGVIndentItem
        '
        Me.DGVIndentItem.AllowUserToOrderColumns = True
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVIndentItem.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DGVIndentItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVIndentItem.DefaultCellStyle = DataGridViewCellStyle6
        Me.DGVIndentItem.Location = New System.Drawing.Point(23, 25)
        Me.DGVIndentItem.Name = "DGVIndentItem"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVIndentItem.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
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
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(88, Byte), Integer))
        Me.Label1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label1.Location = New System.Drawing.Point(890, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(15, 185)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Wastage  Master"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frm_Wastage_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TBCWastageMaster)
        Me.Name = "frm_Wastage_Master"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.GBIndentDetail.ResumeLayout(False)
        CType(Me.DGVIdnetMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TBCWastageMaster.ResumeLayout(False)
        Me.List.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GBWastageMaster.ResumeLayout(False)
        CType(Me.DGVWastageMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Detail.ResumeLayout(False)
        Me.Detail.PerformLayout()
        Me.GBWastageItem.ResumeLayout(False)
        CType(Me.grdWastageItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GBWastageMasterInfo.ResumeLayout(False)
        Me.GBWastageMasterInfo.PerformLayout()
        CType(Me.DGVIndentItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GBIndentDetail As System.Windows.Forms.GroupBox
    Friend WithEvents DGVIdnetMaster As System.Windows.Forms.DataGridView
    Friend WithEvents TBCWastageMaster As System.Windows.Forms.TabControl
    Friend WithEvents List As System.Windows.Forms.TabPage
    Friend WithEvents Detail As System.Windows.Forms.TabPage
    Friend WithEvents GBWastageMaster As System.Windows.Forms.GroupBox
    Friend WithEvents DGVWastageMaster As System.Windows.Forms.DataGridView
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
    Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents grdWastageItem As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents Label1 As Label
End Class
