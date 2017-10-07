<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_define_recipe
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_define_recipe))
        Me.tbcrecipe = New System.Windows.Forms.TabControl()
        Me.tblist = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.DGVmenuitems = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSearchMenuItem = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbdetail = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.DGVSemiFinisheditems = New System.Windows.Forms.DataGridView()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.cmbSemiFinishedItems = New System.Windows.Forms.ComboBox()
        Me.txtSemiItemQty = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnAddSemiFinishedItems = New System.Windows.Forms.Button()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.DGVrecipedetail = New System.Windows.Forms.DataGridView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lbl_outlet = New System.Windows.Forms.Label()
        Me.cmb_OutletName = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbmenuitems = New System.Windows.Forms.ComboBox()
        Me.cmbmenuheads = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.txtSearchItems = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbcrecipe.SuspendLayout()
        Me.tblist.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DGVmenuitems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.tbdetail.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.DGVSemiFinisheditems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.DGVrecipedetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbcrecipe
        '
        Me.tbcrecipe.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tbcrecipe.Controls.Add(Me.tblist)
        Me.tbcrecipe.Controls.Add(Me.tbdetail)
        Me.tbcrecipe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcrecipe.ImageList = Me.ImageList1
        Me.tbcrecipe.Location = New System.Drawing.Point(0, 0)
        Me.tbcrecipe.Name = "tbcrecipe"
        Me.tbcrecipe.SelectedIndex = 0
        Me.tbcrecipe.Size = New System.Drawing.Size(910, 630)
        Me.tbcrecipe.TabIndex = 0
        '
        'tblist
        '
        Me.tblist.BackColor = System.Drawing.Color.DimGray
        Me.tblist.Controls.Add(Me.GroupBox2)
        Me.tblist.Controls.Add(Me.GroupBox1)
        Me.tblist.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblist.ForeColor = System.Drawing.Color.White
        Me.tblist.ImageIndex = 0
        Me.tblist.Location = New System.Drawing.Point(4, 26)
        Me.tblist.Name = "tblist"
        Me.tblist.Padding = New System.Windows.Forms.Padding(3)
        Me.tblist.Size = New System.Drawing.Size(902, 600)
        Me.tblist.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DGVmenuitems)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(15, 88)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(871, 499)
        Me.GroupBox2.TabIndex = 18
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "List of Menu Items"
        '
        'DGVmenuitems
        '
        Me.DGVmenuitems.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVmenuitems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVmenuitems.DefaultCellStyle = DataGridViewCellStyle1
        Me.DGVmenuitems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVmenuitems.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVmenuitems.Location = New System.Drawing.Point(3, 17)
        Me.DGVmenuitems.Name = "DGVmenuitems"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVmenuitems.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVmenuitems.RowHeadersVisible = False
        Me.DGVmenuitems.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVmenuitems.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVmenuitems.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVmenuitems.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.DGVmenuitems.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVmenuitems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVmenuitems.Size = New System.Drawing.Size(865, 479)
        Me.DGVmenuitems.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.txtSearchMenuItem)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(15, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(871, 75)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search Option"
        '
        'txtSearchMenuItem
        '
        Me.txtSearchMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearchMenuItem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchMenuItem.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchMenuItem.ForeColor = System.Drawing.Color.White
        Me.txtSearchMenuItem.Location = New System.Drawing.Point(9, 39)
        Me.txtSearchMenuItem.Name = "txtSearchMenuItem"
        Me.txtSearchMenuItem.Size = New System.Drawing.Size(845, 18)
        Me.txtSearchMenuItem.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.DarkOrange
        Me.Label1.Location = New System.Drawing.Point(6, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(156, 15)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Search Listed Menu Items :"
        '
        'tbdetail
        '
        Me.tbdetail.BackColor = System.Drawing.Color.DimGray
        Me.tbdetail.Controls.Add(Me.GroupBox5)
        Me.tbdetail.Controls.Add(Me.GroupBox6)
        Me.tbdetail.Controls.Add(Me.GroupBox4)
        Me.tbdetail.Controls.Add(Me.GroupBox3)
        Me.tbdetail.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbdetail.ForeColor = System.Drawing.Color.White
        Me.tbdetail.ImageIndex = 1
        Me.tbdetail.Location = New System.Drawing.Point(4, 26)
        Me.tbdetail.Name = "tbdetail"
        Me.tbdetail.Padding = New System.Windows.Forms.Padding(3)
        Me.tbdetail.Size = New System.Drawing.Size(902, 600)
        Me.tbdetail.TabIndex = 1
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.DGVSemiFinisheditems)
        Me.GroupBox5.ForeColor = System.Drawing.Color.White
        Me.GroupBox5.Location = New System.Drawing.Point(13, 388)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(873, 129)
        Me.GroupBox5.TabIndex = 4
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "List of Semi Finished Items added"
        '
        'DGVSemiFinisheditems
        '
        Me.DGVSemiFinisheditems.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVSemiFinisheditems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVSemiFinisheditems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVSemiFinisheditems.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVSemiFinisheditems.Location = New System.Drawing.Point(3, 17)
        Me.DGVSemiFinisheditems.Name = "DGVSemiFinisheditems"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Orange
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVSemiFinisheditems.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DGVSemiFinisheditems.RowHeadersVisible = False
        Me.DGVSemiFinisheditems.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVSemiFinisheditems.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVSemiFinisheditems.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVSemiFinisheditems.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.DGVSemiFinisheditems.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVSemiFinisheditems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVSemiFinisheditems.Size = New System.Drawing.Size(867, 109)
        Me.DGVSemiFinisheditems.TabIndex = 0
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.cmbSemiFinishedItems)
        Me.GroupBox6.Controls.Add(Me.txtSemiItemQty)
        Me.GroupBox6.Controls.Add(Me.Label8)
        Me.GroupBox6.Controls.Add(Me.Label3)
        Me.GroupBox6.Controls.Add(Me.btnAddSemiFinishedItems)
        Me.GroupBox6.Controls.Add(Me.lblUnit)
        Me.GroupBox6.ForeColor = System.Drawing.Color.White
        Me.GroupBox6.Location = New System.Drawing.Point(14, 523)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(872, 66)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Add Semi Finished Item"
        '
        'cmbSemiFinishedItems
        '
        Me.cmbSemiFinishedItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbSemiFinishedItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSemiFinishedItems.ForeColor = System.Drawing.Color.White
        Me.cmbSemiFinishedItems.FormattingEnabled = True
        Me.cmbSemiFinishedItems.Location = New System.Drawing.Point(168, 28)
        Me.cmbSemiFinishedItems.Name = "cmbSemiFinishedItems"
        Me.cmbSemiFinishedItems.Size = New System.Drawing.Size(277, 23)
        Me.cmbSemiFinishedItems.TabIndex = 15
        '
        'txtSemiItemQty
        '
        Me.txtSemiItemQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSemiItemQty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSemiItemQty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSemiItemQty.ForeColor = System.Drawing.Color.White
        Me.txtSemiItemQty.Location = New System.Drawing.Point(580, 29)
        Me.txtSemiItemQty.Name = "txtSemiItemQty"
        Me.txtSemiItemQty.Size = New System.Drawing.Size(124, 18)
        Me.txtSemiItemQty.TabIndex = 12
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(510, 30)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Quantity :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(30, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(132, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Semi Finished Items :"
        '
        'btnAddSemiFinishedItems
        '
        Me.btnAddSemiFinishedItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddSemiFinishedItems.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddSemiFinishedItems.ForeColor = System.Drawing.Color.White
        Me.btnAddSemiFinishedItems.Location = New System.Drawing.Point(755, 24)
        Me.btnAddSemiFinishedItems.Name = "btnAddSemiFinishedItems"
        Me.btnAddSemiFinishedItems.Size = New System.Drawing.Size(98, 25)
        Me.btnAddSemiFinishedItems.TabIndex = 14
        Me.btnAddSemiFinishedItems.Text = "Add"
        Me.btnAddSemiFinishedItems.UseVisualStyleBackColor = True
        '
        'lblUnit
        '
        Me.lblUnit.AutoSize = True
        Me.lblUnit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnit.ForeColor = System.Drawing.Color.Orange
        Me.lblUnit.Location = New System.Drawing.Point(452, 32)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(29, 13)
        Me.lblUnit.TabIndex = 10
        Me.lblUnit.Text = "Unit"
        Me.lblUnit.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.DGVrecipedetail)
        Me.GroupBox4.ForeColor = System.Drawing.Color.White
        Me.GroupBox4.Location = New System.Drawing.Point(14, 147)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(872, 238)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Recipe Ingredients Detail"
        '
        'DGVrecipedetail
        '
        Me.DGVrecipedetail.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVrecipedetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVrecipedetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVrecipedetail.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVrecipedetail.Location = New System.Drawing.Point(3, 17)
        Me.DGVrecipedetail.Name = "DGVrecipedetail"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Orange
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVrecipedetail.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DGVrecipedetail.RowHeadersVisible = False
        Me.DGVrecipedetail.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.DGVrecipedetail.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGVrecipedetail.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.DGVrecipedetail.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.DGVrecipedetail.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.DGVrecipedetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVrecipedetail.Size = New System.Drawing.Size(866, 218)
        Me.DGVrecipedetail.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lbl_outlet)
        Me.GroupBox3.Controls.Add(Me.cmb_OutletName)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.cmbmenuitems)
        Me.GroupBox3.Controls.Add(Me.cmbmenuheads)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.ForeColor = System.Drawing.Color.White
        Me.GroupBox3.Location = New System.Drawing.Point(14, 9)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(872, 132)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Define Recipe"
        '
        'lbl_outlet
        '
        Me.lbl_outlet.AutoSize = True
        Me.lbl_outlet.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_outlet.Location = New System.Drawing.Point(16, 56)
        Me.lbl_outlet.Name = "lbl_outlet"
        Me.lbl_outlet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbl_outlet.Size = New System.Drawing.Size(87, 13)
        Me.lbl_outlet.TabIndex = 16
        Me.lbl_outlet.Text = "Outlet Name :"
        '
        'cmb_OutletName
        '
        Me.cmb_OutletName.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmb_OutletName.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmb_OutletName.ForeColor = System.Drawing.Color.White
        Me.cmb_OutletName.FormattingEnabled = True
        Me.cmb_OutletName.Location = New System.Drawing.Point(120, 56)
        Me.cmb_OutletName.Name = "cmb_OutletName"
        Me.cmb_OutletName.Size = New System.Drawing.Size(332, 23)
        Me.cmb_OutletName.TabIndex = 15
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(704, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(166, 25)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Define Recipe"
        '
        'cmbmenuitems
        '
        Me.cmbmenuitems.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbmenuitems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbmenuitems.ForeColor = System.Drawing.Color.White
        Me.cmbmenuitems.FormattingEnabled = True
        Me.cmbmenuitems.Location = New System.Drawing.Point(120, 90)
        Me.cmbmenuitems.Name = "cmbmenuitems"
        Me.cmbmenuitems.Size = New System.Drawing.Size(332, 23)
        Me.cmbmenuitems.TabIndex = 12
        '
        'cmbmenuheads
        '
        Me.cmbmenuheads.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbmenuheads.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbmenuheads.ForeColor = System.Drawing.Color.White
        Me.cmbmenuheads.FormattingEnabled = True
        Me.cmbmenuheads.Location = New System.Drawing.Point(120, 24)
        Me.cmbmenuheads.Name = "cmbmenuheads"
        Me.cmbmenuheads.Size = New System.Drawing.Size(332, 23)
        Me.cmbmenuheads.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 93)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(83, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Menu Items :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Menu Heads :"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblFormHeading.Location = New System.Drawing.Point(637, 16)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(213, 25)
        Me.lblFormHeading.TabIndex = 13
        Me.lblFormHeading.Text = "Define IKT Recipe"
        '
        'txtSearchItems
        '
        Me.txtSearchItems.Location = New System.Drawing.Point(14, 88)
        Me.txtSearchItems.Name = "txtSearchItems"
        Me.txtSearchItems.Size = New System.Drawing.Size(838, 20)
        Me.txtSearchItems.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Maroon
        Me.Label7.Location = New System.Drawing.Point(11, 70)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(272, 15)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Type Here to Search Recipe Item Category wise :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(11, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(85, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Menu Heads :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Menu Items :"
        '
        'frm_define_recipe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DimGray
        Me.Controls.Add(Me.tbcrecipe)
        Me.Name = "frm_define_recipe"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.tbcrecipe.ResumeLayout(False)
        Me.tblist.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DGVmenuitems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tbdetail.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.DGVSemiFinisheditems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.DGVrecipedetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbcrecipe As System.Windows.Forms.TabControl
    Friend WithEvents tblist As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearchMenuItem As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents txtSearchItems As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label

    Friend WithEvents Label5 As System.Windows.Forms.Label

    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbdetail As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents DGVrecipedetail As System.Windows.Forms.DataGridView
    Friend WithEvents txtSemiItemQty As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnAddSemiFinishedItems As System.Windows.Forms.Button
    Friend WithEvents DGVmenuitems As System.Windows.Forms.DataGridView
    Friend WithEvents cmbmenuitems As System.Windows.Forms.ComboBox
    Friend WithEvents cmbmenuheads As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbSemiFinishedItems As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_outlet As System.Windows.Forms.Label
    Friend WithEvents cmb_OutletName As System.Windows.Forms.ComboBox
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents DGVSemiFinisheditems As System.Windows.Forms.DataGridView
    Friend WithEvents ImageList1 As ImageList
End Class
