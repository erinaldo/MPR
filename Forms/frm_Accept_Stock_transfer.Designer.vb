<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Accept_stock_transfer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Accept_stock_transfer))
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GBItemInfo = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtMRNRemarks = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbWareOutlet = New System.Windows.Forms.ComboBox()
        Me.lblMrnDate = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbDcNo = New System.Windows.Forms.ComboBox()
        Me.lblMrnNo = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.GBMRSDetail = New System.Windows.Forms.GroupBox()
        Me.flxGridItems = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txt_Search = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.GB_Items = New System.Windows.Forms.GroupBox()
        Me.flxItems = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lbl_DivName = New System.Windows.Forms.Label()
        Me.txt_MrnRemarks = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lbl_DcRemarks = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lbl_DCDate = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lbl_MrnDate = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmb_DcNo = New System.Windows.Forms.ComboBox()
        Me.lbl_MrnNo = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GBMRSDetail.SuspendLayout()
        CType(Me.flxGridItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GB_Items.SuspendLayout()
        CType(Me.flxItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GBItemInfo)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(873, 580)
        Me.TabPage2.TabIndex = 0
        Me.TabPage2.Text = "Detail"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GBItemInfo
        '
        Me.GBItemInfo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GBItemInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBItemInfo.Location = New System.Drawing.Point(3, 273)
        Me.GBItemInfo.Name = "GBItemInfo"
        Me.GBItemInfo.Size = New System.Drawing.Size(867, 304)
        Me.GBItemInfo.TabIndex = 9
        Me.GBItemInfo.TabStop = False
        Me.GBItemInfo.Text = "List of Items"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtMRNRemarks)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.lblRemarks)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.cmbWareOutlet)
        Me.GroupBox2.Controls.Add(Me.lblMrnDate)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cmbDcNo)
        Me.GroupBox2.Controls.Add(Me.lblMrnNo)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(872, 261)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        '
        'txtMRNRemarks
        '
        Me.txtMRNRemarks.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRNRemarks.Location = New System.Drawing.Point(125, 145)
        Me.txtMRNRemarks.Multiline = True
        Me.txtMRNRemarks.Name = "txtMRNRemarks"
        Me.txtMRNRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMRNRemarks.Size = New System.Drawing.Size(541, 80)
        Me.txtMRNRemarks.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(23, 145)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(96, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "MRN Remarks :"
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(114, 113)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(67, 13)
        Me.lblRemarks.TabIndex = 11
        Me.lblRemarks.Text = "DC Remarks"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(23, 113)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "DC Remarks :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.Color.Indigo
        Me.Label6.Location = New System.Drawing.Point(113, 83)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "lblDcDate"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(23, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "DC Date :"
        '
        'cmbWareOutlet
        '
        Me.cmbWareOutlet.FormattingEnabled = True
        Me.cmbWareOutlet.Location = New System.Drawing.Point(534, 44)
        Me.cmbWareOutlet.Name = "cmbWareOutlet"
        Me.cmbWareOutlet.Size = New System.Drawing.Size(121, 21)
        Me.cmbWareOutlet.TabIndex = 7
        '
        'lblMrnDate
        '
        Me.lblMrnDate.AutoSize = True
        Me.lblMrnDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblMrnDate.ForeColor = System.Drawing.Color.Indigo
        Me.lblMrnDate.Location = New System.Drawing.Point(531, 16)
        Me.lblMrnDate.Name = "lblMrnDate"
        Me.lblMrnDate.Size = New System.Drawing.Size(72, 13)
        Me.lblMrnDate.TabIndex = 6
        Me.lblMrnDate.Text = "MRN DATE"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(383, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Outlet/Warehouse :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(383, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "MRN Date :"
        '
        'cmbDcNo
        '
        Me.cmbDcNo.FormattingEnabled = True
        Me.cmbDcNo.Location = New System.Drawing.Point(116, 44)
        Me.cmbDcNo.Name = "cmbDcNo"
        Me.cmbDcNo.Size = New System.Drawing.Size(121, 21)
        Me.cmbDcNo.TabIndex = 3
        '
        'lblMrnNo
        '
        Me.lblMrnNo.AutoSize = True
        Me.lblMrnNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblMrnNo.ForeColor = System.Drawing.Color.Indigo
        Me.lblMrnNo.Location = New System.Drawing.Point(113, 16)
        Me.lblMrnNo.Name = "lblMrnNo"
        Me.lblMrnNo.Size = New System.Drawing.Size(57, 13)
        Me.lblMrnNo.TabIndex = 2
        Me.lblMrnNo.Text = "MRN NO"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(23, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "DC NO :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(23, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "MRN NO :"
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(873, 580)
        Me.TabPage1.TabIndex = 1
        Me.TabPage1.Text = "List"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(881, 606)
        Me.TabControl1.TabIndex = 0
        '
        'TabControl2
        '
        Me.TabControl2.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.ImageList = Me.ImageList1
        Me.TabControl2.Location = New System.Drawing.Point(0, 0)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(910, 630)
        Me.TabControl2.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.DimGray
        Me.TabPage4.Controls.Add(Me.GBMRSDetail)
        Me.TabPage4.Controls.Add(Me.GroupBox1)
        Me.TabPage4.ImageIndex = 0
        Me.TabPage4.Location = New System.Drawing.Point(4, 26)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(902, 600)
        Me.TabPage4.TabIndex = 2
        '
        'GBMRSDetail
        '
        Me.GBMRSDetail.Controls.Add(Me.flxGridItems)
        Me.GBMRSDetail.ForeColor = System.Drawing.Color.White
        Me.GBMRSDetail.Location = New System.Drawing.Point(23, 95)
        Me.GBMRSDetail.Name = "GBMRSDetail"
        Me.GBMRSDetail.Size = New System.Drawing.Size(855, 482)
        Me.GBMRSDetail.TabIndex = 9
        Me.GBMRSDetail.TabStop = False
        Me.GBMRSDetail.Text = "List of Items"
        '
        'flxGridItems
        '
        Me.flxGridItems.AllowEditing = False
        Me.flxGridItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D
        Me.flxGridItems.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.flxGridItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxGridItems.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.flxGridItems.Location = New System.Drawing.Point(3, 16)
        Me.flxGridItems.Name = "flxGridItems"
        Me.flxGridItems.Rows.DefaultSize = 17
        Me.flxGridItems.Size = New System.Drawing.Size(849, 463)
        Me.flxGridItems.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("flxGridItems.Styles"))
        Me.flxGridItems.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txt_Search)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(855, 76)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'txt_Search
        '
        Me.txt_Search.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_Search.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Search.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Search.ForeColor = System.Drawing.Color.White
        Me.txt_Search.Location = New System.Drawing.Point(84, 34)
        Me.txt_Search.Name = "txt_Search"
        Me.txt_Search.Size = New System.Drawing.Size(743, 18)
        Me.txt_Search.TabIndex = 17
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(10, 35)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 15)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Search By :"
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.DimGray
        Me.TabPage3.Controls.Add(Me.GB_Items)
        Me.TabPage3.Controls.Add(Me.GroupBox3)
        Me.TabPage3.ImageIndex = 1
        Me.TabPage3.Location = New System.Drawing.Point(4, 26)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(902, 600)
        Me.TabPage3.TabIndex = 0
        '
        'GB_Items
        '
        Me.GB_Items.Controls.Add(Me.flxItems)
        Me.GB_Items.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GB_Items.ForeColor = System.Drawing.Color.White
        Me.GB_Items.Location = New System.Drawing.Point(3, 237)
        Me.GB_Items.Name = "GB_Items"
        Me.GB_Items.Size = New System.Drawing.Size(896, 357)
        Me.GB_Items.TabIndex = 10
        Me.GB_Items.TabStop = False
        Me.GB_Items.Text = "List of Items"
        '
        'flxItems
        '
        Me.flxItems.AllowEditing = False
        Me.flxItems.BackColor = System.Drawing.Color.Silver
        Me.flxItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D
        Me.flxItems.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.flxItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxItems.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.flxItems.Location = New System.Drawing.Point(3, 16)
        Me.flxItems.Name = "flxItems"
        Me.flxItems.Rows.DefaultSize = 17
        Me.flxItems.Size = New System.Drawing.Size(890, 338)
        Me.flxItems.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("flxItems.Styles"))
        Me.flxItems.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.DimGray
        Me.GroupBox3.Controls.Add(Me.lbl_DivName)
        Me.GroupBox3.Controls.Add(Me.txt_MrnRemarks)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.lbl_DcRemarks)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.lbl_DCDate)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.lbl_MrnDate)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.cmb_DcNo)
        Me.GroupBox3.Controls.Add(Me.lbl_MrnNo)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.ForeColor = System.Drawing.Color.White
        Me.GroupBox3.Location = New System.Drawing.Point(3, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(893, 225)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        '
        'lbl_DivName
        '
        Me.lbl_DivName.AutoSize = True
        Me.lbl_DivName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DivName.ForeColor = System.Drawing.Color.Orange
        Me.lbl_DivName.Location = New System.Drawing.Point(534, 48)
        Me.lbl_DivName.Name = "lbl_DivName"
        Me.lbl_DivName.Size = New System.Drawing.Size(52, 15)
        Me.lbl_DivName.TabIndex = 14
        Me.lbl_DivName.Text = "Label10"
        '
        'txt_MrnRemarks
        '
        Me.txt_MrnRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_MrnRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_MrnRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_MrnRemarks.ForeColor = System.Drawing.Color.White
        Me.txt_MrnRemarks.Location = New System.Drawing.Point(117, 148)
        Me.txt_MrnRemarks.Multiline = True
        Me.txt_MrnRemarks.Name = "txt_MrnRemarks"
        Me.txt_MrnRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_MrnRemarks.Size = New System.Drawing.Size(747, 58)
        Me.txt_MrnRemarks.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(15, 145)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(94, 15)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "MRN Remarks :"
        '
        'lbl_DcRemarks
        '
        Me.lbl_DcRemarks.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DcRemarks.ForeColor = System.Drawing.Color.Orange
        Me.lbl_DcRemarks.Location = New System.Drawing.Point(118, 102)
        Me.lbl_DcRemarks.Name = "lbl_DcRemarks"
        Me.lbl_DcRemarks.Size = New System.Drawing.Size(746, 40)
        Me.lbl_DcRemarks.TabIndex = 11
        Me.lbl_DcRemarks.Text = "DC Remarks"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(15, 103)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(85, 15)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "DC Remarks :"
        '
        'lbl_DCDate
        '
        Me.lbl_DCDate.AutoSize = True
        Me.lbl_DCDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DCDate.ForeColor = System.Drawing.Color.Orange
        Me.lbl_DCDate.Location = New System.Drawing.Point(118, 75)
        Me.lbl_DCDate.Name = "lbl_DCDate"
        Me.lbl_DCDate.Size = New System.Drawing.Size(61, 15)
        Me.lbl_DCDate.TabIndex = 9
        Me.lbl_DCDate.Text = "lblDcDate"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(15, 75)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(60, 15)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "DC Date :"
        '
        'lbl_MrnDate
        '
        Me.lbl_MrnDate.AutoSize = True
        Me.lbl_MrnDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MrnDate.ForeColor = System.Drawing.Color.Orange
        Me.lbl_MrnDate.Location = New System.Drawing.Point(534, 20)
        Me.lbl_MrnDate.Name = "lbl_MrnDate"
        Me.lbl_MrnDate.Size = New System.Drawing.Size(67, 15)
        Me.lbl_MrnDate.TabIndex = 6
        Me.lbl_MrnDate.Text = "MRN DATE"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(383, 48)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(112, 15)
        Me.Label15.TabIndex = 5
        Me.Label15.Text = "Outlet/Warehouse :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(383, 21)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(69, 15)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "MRN Date :"
        '
        'cmb_DcNo
        '
        Me.cmb_DcNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmb_DcNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_DcNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_DcNo.ForeColor = System.Drawing.Color.White
        Me.cmb_DcNo.FormattingEnabled = True
        Me.cmb_DcNo.Location = New System.Drawing.Point(119, 44)
        Me.cmb_DcNo.Name = "cmb_DcNo"
        Me.cmb_DcNo.Size = New System.Drawing.Size(206, 23)
        Me.cmb_DcNo.TabIndex = 3
        '
        'lbl_MrnNo
        '
        Me.lbl_MrnNo.AutoSize = True
        Me.lbl_MrnNo.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_MrnNo.ForeColor = System.Drawing.Color.Orange
        Me.lbl_MrnNo.Location = New System.Drawing.Point(116, 19)
        Me.lbl_MrnNo.Name = "lbl_MrnNo"
        Me.lbl_MrnNo.Size = New System.Drawing.Size(55, 15)
        Me.lbl_MrnNo.TabIndex = 2
        Me.lbl_MrnNo.Text = "MRN NO"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(15, 44)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(52, 15)
        Me.Label18.TabIndex = 1
        Me.Label18.Text = "DC NO :"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(14, 18)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(61, 15)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "MRN NO :"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'frm_Accept_stock_transfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.TabControl2)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frm_Accept_stock_transfer"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.GBMRSDetail.ResumeLayout(False)
        CType(Me.flxGridItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.GB_Items.ResumeLayout(False)
        CType(Me.flxItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GBItemInfo As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMRNRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbWareOutlet As System.Windows.Forms.ComboBox
    Friend WithEvents lblMrnDate As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbDcNo As System.Windows.Forms.ComboBox
    Friend WithEvents lblMrnNo As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents GB_Items As System.Windows.Forms.GroupBox
    Friend WithEvents flxItems As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_DivName As System.Windows.Forms.Label
    Friend WithEvents txt_MrnRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lbl_DcRemarks As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lbl_DCDate As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lbl_MrnDate As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmb_DcNo As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_MrnNo As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_Search As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GBMRSDetail As System.Windows.Forms.GroupBox
    Friend WithEvents flxGridItems As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ImageList1 As ImageList
End Class
