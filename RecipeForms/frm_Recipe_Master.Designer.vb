<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Recipe_Master
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Recipe_Master))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grdListMenuItems = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtSearchMenuItem = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.txtSemiItemQty = New System.Windows.Forms.TextBox()
        Me.btnSaveRecipe = New System.Windows.Forms.Button()
        Me.cmbSemiFinishedItems = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnAddSemiFinishedItems = New System.Windows.Forms.Button()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.grdItemsSelected = New System.Windows.Forms.DataGridView()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.grdItemMaster = New System.Windows.Forms.DataGridView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.txtSearchItems = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbMenuHeads = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbMenuItems = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Afbl_mmsDataSet = New MMSPlus.afbl_mmsDataSet()
        Me.DivisionMasterBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.DivisionMasterBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DivisionMasterTableAdapter = New MMSPlus.afbl_mmsDataSetTableAdapters.DivisionMasterTableAdapter()
        Me.flxItems = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdListMenuItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.grdItemsSelected, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        CType(Me.grdItemMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.Afbl_mmsDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DivisionMasterBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DivisionMasterBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.ImageList = Me.ImageList1
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(910, 630)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.DimGray
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.ForeColor = System.Drawing.Color.Maroon
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(902, 600)
        Me.TabPage1.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdListMenuItems)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(9, 71)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(886, 526)
        Me.GroupBox2.TabIndex = 17
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "List of Menu Items"
        '
        'grdListMenuItems
        '
        Me.grdListMenuItems.AllowUserToAddRows = False
        Me.grdListMenuItems.AllowUserToDeleteRows = False
        Me.grdListMenuItems.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdListMenuItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdListMenuItems.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdListMenuItems.Location = New System.Drawing.Point(3, 17)
        Me.grdListMenuItems.Name = "grdListMenuItems"
        Me.grdListMenuItems.ReadOnly = True
        Me.grdListMenuItems.RowHeadersVisible = False
        Me.grdListMenuItems.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdListMenuItems.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.grdListMenuItems.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdListMenuItems.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.grdListMenuItems.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdListMenuItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdListMenuItems.Size = New System.Drawing.Size(880, 506)
        Me.grdListMenuItems.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.txtSearchMenuItem)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(9, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(885, 67)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search Option"
        '
        'txtSearchMenuItem
        '
        Me.txtSearchMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearchMenuItem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchMenuItem.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchMenuItem.ForeColor = System.Drawing.Color.White
        Me.txtSearchMenuItem.Location = New System.Drawing.Point(9, 37)
        Me.txtSearchMenuItem.Name = "txtSearchMenuItem"
        Me.txtSearchMenuItem.Size = New System.Drawing.Size(857, 18)
        Me.txtSearchMenuItem.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Orange
        Me.Label1.Location = New System.Drawing.Point(6, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(156, 15)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Search Listed Menu Items :"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.DimGray
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage2.ImageIndex = 1
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(902, 600)
        Me.TabPage2.TabIndex = 1
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox5)
        Me.GroupBox4.Controls.Add(Me.GroupBox7)
        Me.GroupBox4.Controls.Add(Me.GroupBox6)
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 118)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(893, 480)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtSemiItemQty)
        Me.GroupBox5.Controls.Add(Me.btnSaveRecipe)
        Me.GroupBox5.Controls.Add(Me.cmbSemiFinishedItems)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Controls.Add(Me.btnAddSemiFinishedItems)
        Me.GroupBox5.Controls.Add(Me.lblUnit)
        Me.GroupBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox5.ForeColor = System.Drawing.Color.White
        Me.GroupBox5.Location = New System.Drawing.Point(9, 423)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(878, 51)
        Me.GroupBox5.TabIndex = 10
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "List Of Semi Finished Items"
        '
        'txtSemiItemQty
        '
        Me.txtSemiItemQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSemiItemQty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSemiItemQty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSemiItemQty.ForeColor = System.Drawing.Color.White
        Me.txtSemiItemQty.Location = New System.Drawing.Point(514, 20)
        Me.txtSemiItemQty.Name = "txtSemiItemQty"
        Me.txtSemiItemQty.Size = New System.Drawing.Size(109, 18)
        Me.txtSemiItemQty.TabIndex = 7
        '
        'btnSaveRecipe
        '
        Me.btnSaveRecipe.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveRecipe.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveRecipe.ForeColor = System.Drawing.Color.White
        Me.btnSaveRecipe.Location = New System.Drawing.Point(752, 16)
        Me.btnSaveRecipe.Name = "btnSaveRecipe"
        Me.btnSaveRecipe.Size = New System.Drawing.Size(120, 25)
        Me.btnSaveRecipe.TabIndex = 9
        Me.btnSaveRecipe.Text = "Save Recipe"
        Me.btnSaveRecipe.UseVisualStyleBackColor = True
        '
        'cmbSemiFinishedItems
        '
        Me.cmbSemiFinishedItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbSemiFinishedItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSemiFinishedItems.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSemiFinishedItems.ForeColor = System.Drawing.Color.White
        Me.cmbSemiFinishedItems.FormattingEnabled = True
        Me.cmbSemiFinishedItems.Items.AddRange(New Object() {"Vat 0%", "Vat 4%", "Vat 12.5%", "Vat 10%", "Vat 2%"})
        Me.cmbSemiFinishedItems.Location = New System.Drawing.Point(146, 19)
        Me.cmbSemiFinishedItems.Name = "cmbSemiFinishedItems"
        Me.cmbSemiFinishedItems.Size = New System.Drawing.Size(240, 23)
        Me.cmbSemiFinishedItems.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(444, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Quantity :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(132, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Semi Finished Items :"
        '
        'btnAddSemiFinishedItems
        '
        Me.btnAddSemiFinishedItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddSemiFinishedItems.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddSemiFinishedItems.ForeColor = System.Drawing.Color.White
        Me.btnAddSemiFinishedItems.Location = New System.Drawing.Point(680, 16)
        Me.btnAddSemiFinishedItems.Name = "btnAddSemiFinishedItems"
        Me.btnAddSemiFinishedItems.Size = New System.Drawing.Size(55, 25)
        Me.btnAddSemiFinishedItems.TabIndex = 8
        Me.btnAddSemiFinishedItems.Text = "Add"
        Me.btnAddSemiFinishedItems.UseVisualStyleBackColor = True
        '
        'lblUnit
        '
        Me.lblUnit.AutoSize = True
        Me.lblUnit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnit.ForeColor = System.Drawing.Color.Orange
        Me.lblUnit.Location = New System.Drawing.Point(392, 22)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(29, 13)
        Me.lblUnit.TabIndex = 6
        Me.lblUnit.Text = "Unit"
        Me.lblUnit.Visible = False
        '
        'GroupBox7
        '
        Me.GroupBox7.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox7.Controls.Add(Me.grdItemsSelected)
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox7.ForeColor = System.Drawing.Color.White
        Me.GroupBox7.Location = New System.Drawing.Point(9, 226)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(884, 194)
        Me.GroupBox7.TabIndex = 3
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Recipe Ingredients Detail"
        '
        'grdItemsSelected
        '
        Me.grdItemsSelected.AllowUserToDeleteRows = False
        Me.grdItemsSelected.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdItemsSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItemsSelected.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdItemsSelected.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdItemsSelected.Location = New System.Drawing.Point(3, 17)
        Me.grdItemsSelected.Name = "grdItemsSelected"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItemsSelected.RowHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdItemsSelected.RowHeadersVisible = False
        Me.grdItemsSelected.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdItemsSelected.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.grdItemsSelected.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdItemsSelected.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.grdItemsSelected.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdItemsSelected.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdItemsSelected.Size = New System.Drawing.Size(878, 174)
        Me.grdItemsSelected.TabIndex = 11
        Me.grdItemsSelected.TabStop = False
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox6.Controls.Add(Me.grdItemMaster)
        Me.GroupBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox6.ForeColor = System.Drawing.Color.White
        Me.GroupBox6.Location = New System.Drawing.Point(9, 10)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(884, 213)
        Me.GroupBox6.TabIndex = 2
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "List of All Items"
        '
        'grdItemMaster
        '
        Me.grdItemMaster.AllowUserToDeleteRows = False
        Me.grdItemMaster.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdItemMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdItemMaster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdItemMaster.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdItemMaster.Location = New System.Drawing.Point(3, 17)
        Me.grdItemMaster.Name = "grdItemMaster"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdItemMaster.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdItemMaster.RowHeadersVisible = False
        Me.grdItemMaster.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdItemMaster.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.grdItemMaster.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdItemMaster.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.grdItemMaster.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdItemMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdItemMaster.Size = New System.Drawing.Size(878, 193)
        Me.grdItemMaster.TabIndex = 10
        Me.grdItemMaster.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblFormHeading)
        Me.GroupBox3.Controls.Add(Me.txtSearchItems)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.cmbMenuHeads)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.cmbMenuItems)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.White
        Me.GroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox3.Size = New System.Drawing.Size(896, 114)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Recipe Menu Heads and Items List"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.BackColor = System.Drawing.Color.Transparent
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(680, 8)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(213, 25)
        Me.lblFormHeading.TabIndex = 13
        Me.lblFormHeading.Text = "Define IKT Recipe"
        '
        'txtSearchItems
        '
        Me.txtSearchItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearchItems.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchItems.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchItems.ForeColor = System.Drawing.Color.White
        Me.txtSearchItems.Location = New System.Drawing.Point(14, 88)
        Me.txtSearchItems.Name = "txtSearchItems"
        Me.txtSearchItems.Size = New System.Drawing.Size(867, 18)
        Me.txtSearchItems.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Orange
        Me.Label7.Location = New System.Drawing.Point(11, 70)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(272, 15)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Type Here to Search Recipe Item Category wise :"
        '
        'cmbMenuHeads
        '
        Me.cmbMenuHeads.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbMenuHeads.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMenuHeads.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMenuHeads.ForeColor = System.Drawing.Color.White
        Me.cmbMenuHeads.FormattingEnabled = True
        Me.cmbMenuHeads.Items.AddRange(New Object() {"Vat 0%", "Vat 4%", "Vat 12.5%", "Vat 10%", "Vat 2%"})
        Me.cmbMenuHeads.Location = New System.Drawing.Point(98, 20)
        Me.cmbMenuHeads.Name = "cmbMenuHeads"
        Me.cmbMenuHeads.Size = New System.Drawing.Size(286, 23)
        Me.cmbMenuHeads.TabIndex = 3
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
        'cmbMenuItems
        '
        Me.cmbMenuItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbMenuItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMenuItems.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMenuItems.ForeColor = System.Drawing.Color.White
        Me.cmbMenuItems.FormattingEnabled = True
        Me.cmbMenuItems.Location = New System.Drawing.Point(98, 47)
        Me.cmbMenuItems.Name = "cmbMenuItems"
        Me.cmbMenuItems.Size = New System.Drawing.Size(286, 23)
        Me.cmbMenuItems.TabIndex = 4
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
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'Afbl_mmsDataSet
        '
        Me.Afbl_mmsDataSet.DataSetName = "afbl_mmsDataSet"
        Me.Afbl_mmsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DivisionMasterBindingSource1
        '
        Me.DivisionMasterBindingSource1.DataMember = "DivisionMaster"
        Me.DivisionMasterBindingSource1.DataSource = Me.Afbl_mmsDataSet
        '
        'DivisionMasterBindingSource
        '
        Me.DivisionMasterBindingSource.DataMember = "DivisionMaster"
        Me.DivisionMasterBindingSource.DataSource = Me.Afbl_mmsDataSet
        '
        'DivisionMasterTableAdapter
        '
        Me.DivisionMasterTableAdapter.ClearBeforeFill = True
        '
        'flxItems
        '
        Me.flxItems.AllowEditing = False
        Me.flxItems.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.Light3D
        Me.flxItems.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.flxItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxItems.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.flxItems.Location = New System.Drawing.Point(3, 16)
        Me.flxItems.Name = "flxItems"
        Me.flxItems.Rows.DefaultSize = 17
        Me.flxItems.Size = New System.Drawing.Size(857, 321)
        Me.flxItems.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("flxItems.Styles"))
        Me.flxItems.TabIndex = 1
        '
        'frm_Recipe_Master
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frm_Recipe_Master"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdListMenuItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        CType(Me.grdItemsSelected, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        CType(Me.grdItemMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.Afbl_mmsDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DivisionMasterBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DivisionMasterBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbMenuHeads As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Afbl_mmsDataSet As MMSPlus.afbl_mmsDataSet
    Friend WithEvents DivisionMasterBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DivisionMasterTableAdapter As MMSPlus.afbl_mmsDataSetTableAdapters.DivisionMasterTableAdapter
    Friend WithEvents DivisionMasterBindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents cmbMenuItems As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchItems As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents flxItems As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents grdItemMaster As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents grdItemsSelected As System.Windows.Forms.DataGridView
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbSemiFinishedItems As System.Windows.Forms.ComboBox
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtSemiItemQty As System.Windows.Forms.TextBox
    Friend WithEvents btnAddSemiFinishedItems As System.Windows.Forms.Button
    Friend WithEvents btnSaveRecipe As System.Windows.Forms.Button
    Friend WithEvents txtSearchMenuItem As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdListMenuItems As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents ImageList1 As ImageList
End Class
