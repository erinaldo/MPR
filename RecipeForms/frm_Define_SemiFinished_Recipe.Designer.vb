<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Define_SemiFinished_Recipe
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Define_SemiFinished_Recipe))
        Me.tbSemifinisheditems = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grdSemiFinishedItemMaster = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.dgv_Semifinisheditemsadded = New System.Windows.Forms.DataGridView()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.cmbSemiFinishedItems = New System.Windows.Forms.ComboBox()
        Me.txtSemiItemQty = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnAddSemiFinishedItems = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.grdSemifinishedItemdetail = New System.Windows.Forms.DataGridView()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lbl_id = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbItemUnit = New System.Windows.Forms.ComboBox()
        Me.txtItem_Name = New System.Windows.Forms.TextBox()
        Me.txtItem_Code = New System.Windows.Forms.TextBox()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.tbSemifinisheditems.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdSemiFinishedItemMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.dgv_Semifinisheditemsadded, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.grdSemifinishedItemdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbSemifinisheditems
        '
        Me.tbSemifinisheditems.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tbSemifinisheditems.Controls.Add(Me.TabPage1)
        Me.tbSemifinisheditems.Controls.Add(Me.TabPage2)
        Me.tbSemifinisheditems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbSemifinisheditems.ImageList = Me.ImageList1
        Me.tbSemifinisheditems.Location = New System.Drawing.Point(0, 0)
        Me.tbSemifinisheditems.Name = "tbSemifinisheditems"
        Me.tbSemifinisheditems.SelectedIndex = 0
        Me.tbSemifinisheditems.Size = New System.Drawing.Size(910, 630)
        Me.tbSemifinisheditems.TabIndex = 1
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
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.grdSemiFinishedItemMaster)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(25, 67)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(857, 513)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "List of Semi Finished Items"
        '
        'grdSemiFinishedItemMaster
        '
        Me.grdSemiFinishedItemMaster.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSemiFinishedItemMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSemiFinishedItemMaster.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSemiFinishedItemMaster.Location = New System.Drawing.Point(7, 21)
        Me.grdSemiFinishedItemMaster.Name = "grdSemiFinishedItemMaster"
        Me.grdSemiFinishedItemMaster.RowHeadersVisible = False
        Me.grdSemiFinishedItemMaster.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSemiFinishedItemMaster.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdSemiFinishedItemMaster.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.grdSemiFinishedItemMaster.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdSemiFinishedItemMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSemiFinishedItemMaster.Size = New System.Drawing.Size(844, 486)
        Me.grdSemiFinishedItemMaster.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(25, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(857, 62)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search Option"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Orange
        Me.Label1.Location = New System.Drawing.Point(6, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Type Here to Search Menu Items :"
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(9, 36)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(842, 18)
        Me.txtSearch.TabIndex = 1
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
        Me.GroupBox4.Controls.Add(Me.GroupBox7)
        Me.GroupBox4.Controls.Add(Me.GroupBox6)
        Me.GroupBox4.Controls.Add(Me.GroupBox5)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox4.Location = New System.Drawing.Point(3, 130)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(896, 467)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.dgv_Semifinisheditemsadded)
        Me.GroupBox7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.ForeColor = System.Drawing.Color.White
        Me.GroupBox7.Location = New System.Drawing.Point(5, 254)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(884, 151)
        Me.GroupBox7.TabIndex = 2
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "List of SemiFinished Items Added"
        '
        'dgv_Semifinisheditemsadded
        '
        Me.dgv_Semifinisheditemsadded.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgv_Semifinisheditemsadded.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Semifinisheditemsadded.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_Semifinisheditemsadded.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgv_Semifinisheditemsadded.Location = New System.Drawing.Point(3, 17)
        Me.dgv_Semifinisheditemsadded.Name = "dgv_Semifinisheditemsadded"
        Me.dgv_Semifinisheditemsadded.RowHeadersVisible = False
        Me.dgv_Semifinisheditemsadded.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dgv_Semifinisheditemsadded.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.dgv_Semifinisheditemsadded.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.dgv_Semifinisheditemsadded.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgv_Semifinisheditemsadded.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv_Semifinisheditemsadded.Size = New System.Drawing.Size(878, 131)
        Me.dgv_Semifinisheditemsadded.TabIndex = 0
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.cmbSemiFinishedItems)
        Me.GroupBox6.Controls.Add(Me.txtSemiItemQty)
        Me.GroupBox6.Controls.Add(Me.Label8)
        Me.GroupBox6.Controls.Add(Me.Label3)
        Me.GroupBox6.Controls.Add(Me.btnAddSemiFinishedItems)
        Me.GroupBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox6.ForeColor = System.Drawing.Color.White
        Me.GroupBox6.Location = New System.Drawing.Point(5, 407)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(881, 56)
        Me.GroupBox6.TabIndex = 1
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Add SemiFinished Item"
        '
        'cmbSemiFinishedItems
        '
        Me.cmbSemiFinishedItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbSemiFinishedItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbSemiFinishedItems.ForeColor = System.Drawing.Color.White
        Me.cmbSemiFinishedItems.FormattingEnabled = True
        Me.cmbSemiFinishedItems.Location = New System.Drawing.Point(161, 23)
        Me.cmbSemiFinishedItems.Name = "cmbSemiFinishedItems"
        Me.cmbSemiFinishedItems.Size = New System.Drawing.Size(277, 23)
        Me.cmbSemiFinishedItems.TabIndex = 21
        '
        'txtSemiItemQty
        '
        Me.txtSemiItemQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSemiItemQty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSemiItemQty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSemiItemQty.ForeColor = System.Drawing.Color.White
        Me.txtSemiItemQty.Location = New System.Drawing.Point(573, 23)
        Me.txtSemiItemQty.Name = "txtSemiItemQty"
        Me.txtSemiItemQty.Size = New System.Drawing.Size(124, 18)
        Me.txtSemiItemQty.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(503, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Quantity :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(23, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(132, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Semi Finished Items :"
        '
        'btnAddSemiFinishedItems
        '
        Me.btnAddSemiFinishedItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddSemiFinishedItems.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddSemiFinishedItems.ForeColor = System.Drawing.Color.White
        Me.btnAddSemiFinishedItems.Location = New System.Drawing.Point(772, 21)
        Me.btnAddSemiFinishedItems.Name = "btnAddSemiFinishedItems"
        Me.btnAddSemiFinishedItems.Size = New System.Drawing.Size(98, 25)
        Me.btnAddSemiFinishedItems.TabIndex = 20
        Me.btnAddSemiFinishedItems.Text = "Add"
        Me.btnAddSemiFinishedItems.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.grdSemifinishedItemdetail)
        Me.GroupBox5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.White
        Me.GroupBox5.Location = New System.Drawing.Point(5, 12)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(885, 238)
        Me.GroupBox5.TabIndex = 0
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "List of Ingredients"
        '
        'grdSemifinishedItemdetail
        '
        Me.grdSemifinishedItemdetail.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSemifinishedItemdetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSemifinishedItemdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSemifinishedItemdetail.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSemifinishedItemdetail.Location = New System.Drawing.Point(3, 17)
        Me.grdSemifinishedItemdetail.Name = "grdSemifinishedItemdetail"
        Me.grdSemifinishedItemdetail.RowHeadersVisible = False
        Me.grdSemifinishedItemdetail.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdSemifinishedItemdetail.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.grdSemifinishedItemdetail.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Orange
        Me.grdSemifinishedItemdetail.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.grdSemifinishedItemdetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdSemifinishedItemdetail.Size = New System.Drawing.Size(879, 218)
        Me.grdSemifinishedItemdetail.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lbl_id)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.cmbItemUnit)
        Me.GroupBox3.Controls.Add(Me.txtItem_Name)
        Me.GroupBox3.Controls.Add(Me.txtItem_Code)
        Me.GroupBox3.Controls.Add(Me.lblFormHeading)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.White
        Me.GroupBox3.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox3.Size = New System.Drawing.Size(896, 103)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Semi Finished Item Master"
        '
        'lbl_id
        '
        Me.lbl_id.AutoSize = True
        Me.lbl_id.Location = New System.Drawing.Point(341, 31)
        Me.lbl_id.Name = "lbl_id"
        Me.lbl_id.Size = New System.Drawing.Size(45, 15)
        Me.lbl_id.TabIndex = 48
        Me.lbl_id.Text = "Label3"
        Me.lbl_id.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Item Code :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Item Name :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 76)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Item Unit :"
        '
        'cmbItemUnit
        '
        Me.cmbItemUnit.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmbItemUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbItemUnit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbItemUnit.ForeColor = System.Drawing.Color.White
        Me.cmbItemUnit.FormattingEnabled = True
        Me.cmbItemUnit.Location = New System.Drawing.Point(104, 74)
        Me.cmbItemUnit.Name = "cmbItemUnit"
        Me.cmbItemUnit.Size = New System.Drawing.Size(215, 23)
        Me.cmbItemUnit.TabIndex = 5
        '
        'txtItem_Name
        '
        Me.txtItem_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtItem_Name.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtItem_Name.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem_Name.ForeColor = System.Drawing.Color.White
        Me.txtItem_Name.Location = New System.Drawing.Point(104, 48)
        Me.txtItem_Name.MaxLength = 0
        Me.txtItem_Name.Name = "txtItem_Name"
        Me.txtItem_Name.Size = New System.Drawing.Size(215, 18)
        Me.txtItem_Name.TabIndex = 3
        '
        'txtItem_Code
        '
        Me.txtItem_Code.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtItem_Code.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtItem_Code.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem_Code.ForeColor = System.Drawing.Color.White
        Me.txtItem_Code.Location = New System.Drawing.Point(104, 21)
        Me.txtItem_Code.MaxLength = 0
        Me.txtItem_Code.Name = "txtItem_Code"
        Me.txtItem_Code.Size = New System.Drawing.Size(215, 18)
        Me.txtItem_Code.TabIndex = 1
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(536, 14)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(329, 25)
        Me.lblFormHeading.TabIndex = 47
        Me.lblFormHeading.Text = "Define Semi Finished Recipe"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Zoom_search_find_magnifying_glass.png")
        Me.ImageList1.Images.SetKeyName(1, "Inventory_box_shipment_product.png")
        '
        'frm_Define_SemiFinished_Recipe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tbSemifinisheditems)
        Me.Name = "frm_Define_SemiFinished_Recipe"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.tbSemifinisheditems.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdSemiFinishedItemMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        CType(Me.dgv_Semifinisheditemsadded, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.grdSemifinishedItemdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbSemifinisheditems As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbItemUnit As System.Windows.Forms.ComboBox
    Friend WithEvents txtItem_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtItem_Code As System.Windows.Forms.TextBox
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents grdSemiFinishedItemMaster As System.Windows.Forms.DataGridView
    Friend WithEvents grdSemifinishedItemdetail As System.Windows.Forms.DataGridView
    Friend WithEvents lbl_id As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbSemiFinishedItems As System.Windows.Forms.ComboBox
    Friend WithEvents txtSemiItemQty As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnAddSemiFinishedItems As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents dgv_Semifinisheditemsadded As System.Windows.Forms.DataGridView
    Friend WithEvents ImageList1 As ImageList
End Class
