<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_material_rec_against_PO
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_material_rec_against_PO))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnIssueSlip = New System.Windows.Forms.Button()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.BtnActualMRN = New System.Windows.Forms.Button()
        Me.BtnRevisedMRN = New System.Windows.Forms.Button()
        Me.dgvList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lblMRNType = New System.Windows.Forms.Label()
        Me.lblexciseamt = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.lnkCalculateAmount = New System.Windows.Forms.LinkLabel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.txtotherchrgs = New System.Windows.Forms.TextBox()
        Me.txtdiscount = New System.Windows.Forms.TextBox()
        Me.txt_Amount = New System.Windows.Forms.TextBox()
        Me.lblnetamt = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.lblvatamt = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.lblgrossamt = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dt_Invoice_Date = New System.Windows.Forms.DateTimePicker()
        Me.cmb_MRNAgainst = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_Invoice_No = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.FLXGRD_PO_NON_STOCKABLEITEMS = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.txt_Remarks = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.FLXGRD_PO_Items = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblcustid = New System.Windows.Forms.Label()
        Me.chk_VatCal = New System.Windows.Forms.CheckBox()
        Me.lblSupplier = New System.Windows.Forms.Label()
        Me.lblSupplierAddress = New System.Windows.Forms.Label()
        Me.lblPODate = New System.Windows.Forms.Label()
        Me.dtpReceiveDate = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbPurchaseOrders = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFormHeading = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.FLXGRD_PO_NON_STOCKABLEITEMS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.FLXGRD_PO_Items, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.ForeColor = System.Drawing.Color.White
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 26)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(902, 600)
        Me.TabPage1.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtSearch)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.btnIssueSlip)
        Me.GroupBox4.Controls.Add(Me.dtpTo)
        Me.GroupBox4.Controls.Add(Me.dtpFrom)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.White
        Me.GroupBox4.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(896, 94)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Search Options"
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(164, 61)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(682, 19)
        Me.txtSearch.TabIndex = 25
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(13, 63)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 15)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "Search By :"
        '
        'btnIssueSlip
        '
        Me.btnIssueSlip.Location = New System.Drawing.Point(745, 21)
        Me.btnIssueSlip.Name = "btnIssueSlip"
        Me.btnIssueSlip.Size = New System.Drawing.Size(103, 26)
        Me.btnIssueSlip.TabIndex = 23
        Me.btnIssueSlip.Text = "Show MRN"
        Me.btnIssueSlip.UseVisualStyleBackColor = True
        '
        'dtpTo
        '
        Me.dtpTo.CalendarForeColor = System.Drawing.Color.White
        Me.dtpTo.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpTo.CustomFormat = "dd-MMM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(420, 25)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(180, 21)
        Me.dtpTo.TabIndex = 22
        '
        'dtpFrom
        '
        Me.dtpFrom.CalendarForeColor = System.Drawing.Color.White
        Me.dtpFrom.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dtpFrom.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(164, 24)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(180, 21)
        Me.dtpFrom.TabIndex = 22
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(378, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 15)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "To"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(13, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(116, 15)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Receipt Date From :"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.BtnActualMRN)
        Me.GroupBox3.Controls.Add(Me.BtnRevisedMRN)
        Me.GroupBox3.Controls.Add(Me.dgvList)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.White
        Me.GroupBox3.Location = New System.Drawing.Point(3, 103)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(896, 494)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "List of Material Rec Against PO"
        '
        'BtnActualMRN
        '
        Me.BtnActualMRN.Location = New System.Drawing.Point(762, 461)
        Me.BtnActualMRN.Name = "BtnActualMRN"
        Me.BtnActualMRN.Size = New System.Drawing.Size(128, 30)
        Me.BtnActualMRN.TabIndex = 5
        Me.BtnActualMRN.Text = "Actual MRN"
        Me.BtnActualMRN.UseVisualStyleBackColor = True
        Me.BtnActualMRN.Visible = False
        '
        'BtnRevisedMRN
        '
        Me.BtnRevisedMRN.Location = New System.Drawing.Point(628, 460)
        Me.BtnRevisedMRN.Name = "BtnRevisedMRN"
        Me.BtnRevisedMRN.Size = New System.Drawing.Size(128, 30)
        Me.BtnRevisedMRN.TabIndex = 6
        Me.BtnRevisedMRN.Text = "Revised MRN"
        Me.BtnRevisedMRN.UseVisualStyleBackColor = True
        Me.BtnRevisedMRN.Visible = False
        '
        'dgvList
        '
        Me.dgvList.BackColor = System.Drawing.Color.Silver
        Me.dgvList.ColumnInfo = "10,1,0,0,0,90,Columns:"
        Me.dgvList.Location = New System.Drawing.Point(3, 16)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.Rows.Count = 1
        Me.dgvList.Rows.DefaultSize = 18
        Me.dgvList.Size = New System.Drawing.Size(890, 440)
        Me.dgvList.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("dgvList.Styles"))
        Me.dgvList.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.DimGray
        Me.TabPage2.Controls.Add(Me.lblMRNType)
        Me.TabPage2.Controls.Add(Me.lblexciseamt)
        Me.TabPage2.Controls.Add(Me.Label14)
        Me.TabPage2.Controls.Add(Me.Panel19)
        Me.TabPage2.Controls.Add(Me.lnkCalculateAmount)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.Panel18)
        Me.TabPage2.Controls.Add(Me.Panel13)
        Me.TabPage2.Controls.Add(Me.Panel14)
        Me.TabPage2.Controls.Add(Me.Panel16)
        Me.TabPage2.Controls.Add(Me.Panel17)
        Me.TabPage2.Controls.Add(Me.txtotherchrgs)
        Me.TabPage2.Controls.Add(Me.txtdiscount)
        Me.TabPage2.Controls.Add(Me.txt_Amount)
        Me.TabPage2.Controls.Add(Me.lblnetamt)
        Me.TabPage2.Controls.Add(Me.Label50)
        Me.TabPage2.Controls.Add(Me.Label51)
        Me.TabPage2.Controls.Add(Me.lblvatamt)
        Me.TabPage2.Controls.Add(Me.Label53)
        Me.TabPage2.Controls.Add(Me.lblgrossamt)
        Me.TabPage2.Controls.Add(Me.Label55)
        Me.TabPage2.Controls.Add(Me.Label56)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.dt_Invoice_Date)
        Me.TabPage2.Controls.Add(Me.cmb_MRNAgainst)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.txt_Invoice_No)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Controls.Add(Me.txt_Remarks)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.ForeColor = System.Drawing.Color.White
        Me.TabPage2.ImageIndex = 1
        Me.TabPage2.Location = New System.Drawing.Point(4, 26)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(902, 600)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "  "
        '
        'lblMRNType
        '
        Me.lblMRNType.BackColor = System.Drawing.Color.Transparent
        Me.lblMRNType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRNType.ForeColor = System.Drawing.Color.DimGray
        Me.lblMRNType.Location = New System.Drawing.Point(21, 532)
        Me.lblMRNType.Name = "lblMRNType"
        Me.lblMRNType.Size = New System.Drawing.Size(64, 20)
        Me.lblMRNType.TabIndex = 78
        Me.lblMRNType.Text = "0.00"
        Me.lblMRNType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblexciseamt
        '
        Me.lblexciseamt.BackColor = System.Drawing.Color.Transparent
        Me.lblexciseamt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblexciseamt.ForeColor = System.Drawing.Color.Orange
        Me.lblexciseamt.Location = New System.Drawing.Point(408, 452)
        Me.lblexciseamt.Name = "lblexciseamt"
        Me.lblexciseamt.Size = New System.Drawing.Size(118, 20)
        Me.lblexciseamt.TabIndex = 76
        Me.lblexciseamt.Text = "0.00"
        Me.lblexciseamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblexciseamt.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(303, 453)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(83, 15)
        Me.Label14.TabIndex = 77
        Me.Label14.Text = "Exice Amount:"
        Me.Label14.Visible = False
        '
        'Panel19
        '
        Me.Panel19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel19.Location = New System.Drawing.Point(755, 463)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(117, 1)
        Me.Panel19.TabIndex = 75
        '
        'lnkCalculateAmount
        '
        Me.lnkCalculateAmount.AutoSize = True
        Me.lnkCalculateAmount.BackColor = System.Drawing.Color.Transparent
        Me.lnkCalculateAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkCalculateAmount.LinkColor = System.Drawing.Color.DarkOrange
        Me.lnkCalculateAmount.Location = New System.Drawing.Point(518, 417)
        Me.lnkCalculateAmount.Name = "lnkCalculateAmount"
        Me.lnkCalculateAmount.Size = New System.Drawing.Size(106, 13)
        Me.lnkCalculateAmount.TabIndex = 74
        Me.lnkCalculateAmount.TabStop = True
        Me.lnkCalculateAmount.Text = "Calculate Amount"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(650, 476)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 15)
        Me.Label13.TabIndex = 73
        Me.Label13.Text = "Frieght :"
        '
        'Panel18
        '
        Me.Panel18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel18.Location = New System.Drawing.Point(583, 410)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(1, 142)
        Me.Panel18.TabIndex = 72
        '
        'Panel13
        '
        Me.Panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel13.Location = New System.Drawing.Point(755, 530)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(117, 1)
        Me.Panel13.TabIndex = 68
        '
        'Panel14
        '
        Me.Panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel14.Location = New System.Drawing.Point(755, 554)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(117, 1)
        Me.Panel14.TabIndex = 67
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.Transparent
        Me.Panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel16.Location = New System.Drawing.Point(755, 459)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(117, 1)
        Me.Panel16.TabIndex = 70
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.Transparent
        Me.Panel17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel17.Location = New System.Drawing.Point(755, 437)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(117, 1)
        Me.Panel17.TabIndex = 69
        '
        'txtotherchrgs
        '
        Me.txtotherchrgs.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtotherchrgs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtotherchrgs.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtotherchrgs.ForeColor = System.Drawing.Color.White
        Me.txtotherchrgs.Location = New System.Drawing.Point(777, 499)
        Me.txtotherchrgs.Name = "txtotherchrgs"
        Me.txtotherchrgs.Size = New System.Drawing.Size(118, 18)
        Me.txtotherchrgs.TabIndex = 65
        Me.txtotherchrgs.Text = "0.00"
        Me.txtotherchrgs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtdiscount
        '
        Me.txtdiscount.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtdiscount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtdiscount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdiscount.ForeColor = System.Drawing.Color.White
        Me.txtdiscount.Location = New System.Drawing.Point(777, 528)
        Me.txtdiscount.Name = "txtdiscount"
        Me.txtdiscount.ReadOnly = True
        Me.txtdiscount.Size = New System.Drawing.Size(118, 18)
        Me.txtdiscount.TabIndex = 66
        Me.txtdiscount.Text = "0.00"
        Me.txtdiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_Amount
        '
        Me.txt_Amount.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_Amount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Amount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Amount.ForeColor = System.Drawing.Color.White
        Me.txt_Amount.Location = New System.Drawing.Point(826, 473)
        Me.txt_Amount.Name = "txt_Amount"
        Me.txt_Amount.Size = New System.Drawing.Size(69, 18)
        Me.txt_Amount.TabIndex = 71
        Me.txt_Amount.Text = "0.00"
        Me.txt_Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblnetamt
        '
        Me.lblnetamt.BackColor = System.Drawing.Color.Transparent
        Me.lblnetamt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnetamt.ForeColor = System.Drawing.Color.Orange
        Me.lblnetamt.Location = New System.Drawing.Point(778, 560)
        Me.lblnetamt.Name = "lblnetamt"
        Me.lblnetamt.Size = New System.Drawing.Size(118, 20)
        Me.lblnetamt.TabIndex = 64
        Me.lblnetamt.Text = "0.00"
        Me.lblnetamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.Color.Transparent
        Me.Label50.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(650, 560)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(76, 15)
        Me.Label50.TabIndex = 56
        Me.Label50.Text = "Net Amount :"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.Color.Transparent
        Me.Label51.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(650, 530)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(85, 15)
        Me.Label51.TabIndex = 55
        Me.Label51.Text = "Discount Amt :"
        '
        'lblvatamt
        '
        Me.lblvatamt.BackColor = System.Drawing.Color.Transparent
        Me.lblvatamt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblvatamt.ForeColor = System.Drawing.Color.Orange
        Me.lblvatamt.Location = New System.Drawing.Point(777, 437)
        Me.lblvatamt.Name = "lblvatamt"
        Me.lblvatamt.Size = New System.Drawing.Size(118, 20)
        Me.lblvatamt.TabIndex = 58
        Me.lblvatamt.Text = "0.00"
        Me.lblvatamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.BackColor = System.Drawing.Color.Transparent
        Me.Label53.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(650, 501)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(94, 15)
        Me.Label53.TabIndex = 63
        Me.Label53.Text = "Other Charges :"
        '
        'lblgrossamt
        '
        Me.lblgrossamt.BackColor = System.Drawing.Color.Transparent
        Me.lblgrossamt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgrossamt.ForeColor = System.Drawing.Color.Orange
        Me.lblgrossamt.Location = New System.Drawing.Point(777, 410)
        Me.lblgrossamt.Name = "lblgrossamt"
        Me.lblgrossamt.Size = New System.Drawing.Size(118, 20)
        Me.lblgrossamt.TabIndex = 62
        Me.lblgrossamt.Text = "0.00"
        Me.lblgrossamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.BackColor = System.Drawing.Color.Transparent
        Me.Label55.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(650, 440)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(81, 15)
        Me.Label55.TabIndex = 60
        Me.Label55.Text = "GST Amount :"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.BackColor = System.Drawing.Color.Transparent
        Me.Label56.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(650, 410)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(99, 15)
        Me.Label56.TabIndex = 57
        Me.Label56.Text = "Total Item Value :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(287, 418)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 15)
        Me.Label11.TabIndex = 54
        Me.Label11.Text = "Invoice Date :"
        '
        'dt_Invoice_Date
        '
        Me.dt_Invoice_Date.CalendarForeColor = System.Drawing.Color.White
        Me.dt_Invoice_Date.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.dt_Invoice_Date.CustomFormat = "dd-MMM-yyyy"
        Me.dt_Invoice_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dt_Invoice_Date.Location = New System.Drawing.Point(370, 416)
        Me.dt_Invoice_Date.Name = "dt_Invoice_Date"
        Me.dt_Invoice_Date.Size = New System.Drawing.Size(125, 20)
        Me.dt_Invoice_Date.TabIndex = 52
        '
        'cmb_MRNAgainst
        '
        Me.cmb_MRNAgainst.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cmb_MRNAgainst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_MRNAgainst.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmb_MRNAgainst.ForeColor = System.Drawing.Color.White
        Me.cmb_MRNAgainst.FormattingEnabled = True
        Me.cmb_MRNAgainst.Location = New System.Drawing.Point(116, 452)
        Me.cmb_MRNAgainst.Name = "cmb_MRNAgainst"
        Me.cmb_MRNAgainst.Size = New System.Drawing.Size(155, 23)
        Me.cmb_MRNAgainst.TabIndex = 46
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(18, 453)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(83, 15)
        Me.Label10.TabIndex = 45
        Me.Label10.Text = "MRN Against :"
        '
        'txt_Invoice_No
        '
        Me.txt_Invoice_No.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_Invoice_No.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Invoice_No.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Invoice_No.ForeColor = System.Drawing.Color.White
        Me.txt_Invoice_No.Location = New System.Drawing.Point(116, 416)
        Me.txt_Invoice_No.Name = "txt_Invoice_No"
        Me.txt_Invoice_No.Size = New System.Drawing.Size(155, 18)
        Me.txt_Invoice_No.TabIndex = 53
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(18, 416)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(70, 15)
        Me.Label12.TabIndex = 51
        Me.Label12.Text = "Invoice No :"
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.FLXGRD_PO_NON_STOCKABLEITEMS)
        Me.GroupBox5.ForeColor = System.Drawing.Color.White
        Me.GroupBox5.Location = New System.Drawing.Point(3, 244)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(896, 155)
        Me.GroupBox5.TabIndex = 3
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Non Stockable Items"
        '
        'FLXGRD_PO_NON_STOCKABLEITEMS
        '
        Me.FLXGRD_PO_NON_STOCKABLEITEMS.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop
        Me.FLXGRD_PO_NON_STOCKABLEITEMS.BackColor = System.Drawing.Color.Silver
        Me.FLXGRD_PO_NON_STOCKABLEITEMS.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.FLXGRD_PO_NON_STOCKABLEITEMS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FLXGRD_PO_NON_STOCKABLEITEMS.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.FLXGRD_PO_NON_STOCKABLEITEMS.Location = New System.Drawing.Point(3, 16)
        Me.FLXGRD_PO_NON_STOCKABLEITEMS.Name = "FLXGRD_PO_NON_STOCKABLEITEMS"
        Me.FLXGRD_PO_NON_STOCKABLEITEMS.Rows.DefaultSize = 17
        Me.FLXGRD_PO_NON_STOCKABLEITEMS.Size = New System.Drawing.Size(890, 136)
        Me.FLXGRD_PO_NON_STOCKABLEITEMS.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("FLXGRD_PO_NON_STOCKABLEITEMS.Styles"))
        Me.FLXGRD_PO_NON_STOCKABLEITEMS.TabIndex = 2
        '
        'txt_Remarks
        '
        Me.txt_Remarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txt_Remarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Remarks.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Remarks.ForeColor = System.Drawing.Color.White
        Me.txt_Remarks.Location = New System.Drawing.Point(116, 491)
        Me.txt_Remarks.Multiline = True
        Me.txt_Remarks.Name = "txt_Remarks"
        Me.txt_Remarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_Remarks.Size = New System.Drawing.Size(487, 83)
        Me.txt_Remarks.TabIndex = 13
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.FLXGRD_PO_Items)
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(6, 92)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(893, 151)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Stockable Items"
        '
        'FLXGRD_PO_Items
        '
        Me.FLXGRD_PO_Items.BackColor = System.Drawing.Color.Silver
        Me.FLXGRD_PO_Items.ColumnInfo = "10,1,0,0,0,85,Columns:"
        Me.FLXGRD_PO_Items.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FLXGRD_PO_Items.Location = New System.Drawing.Point(3, 16)
        Me.FLXGRD_PO_Items.Name = "FLXGRD_PO_Items"
        Me.FLXGRD_PO_Items.Rows.Count = 1
        Me.FLXGRD_PO_Items.Rows.DefaultSize = 17
        Me.FLXGRD_PO_Items.Size = New System.Drawing.Size(887, 132)
        Me.FLXGRD_PO_Items.Styles = New C1.Win.C1FlexGrid.CellStyleCollection(resources.GetString("FLXGRD_PO_Items.Styles"))
        Me.FLXGRD_PO_Items.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lblcustid)
        Me.GroupBox1.Controls.Add(Me.chk_VatCal)
        Me.GroupBox1.Controls.Add(Me.lblSupplier)
        Me.GroupBox1.Controls.Add(Me.lblSupplierAddress)
        Me.GroupBox1.Controls.Add(Me.lblPODate)
        Me.GroupBox1.Controls.Add(Me.dtpReceiveDate)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbPurchaseOrders)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblFormHeading)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(896, 88)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'lblcustid
        '
        Me.lblcustid.AutoSize = True
        Me.lblcustid.Location = New System.Drawing.Point(647, 37)
        Me.lblcustid.Name = "lblcustid"
        Me.lblcustid.Size = New System.Drawing.Size(0, 13)
        Me.lblcustid.TabIndex = 29
        '
        'chk_VatCal
        '
        Me.chk_VatCal.AutoSize = True
        Me.chk_VatCal.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_VatCal.Location = New System.Drawing.Point(484, 15)
        Me.chk_VatCal.Name = "chk_VatCal"
        Me.chk_VatCal.Size = New System.Drawing.Size(153, 19)
        Me.chk_VatCal.TabIndex = 28
        Me.chk_VatCal.Text = "Calculate Vat on Excise"
        Me.chk_VatCal.UseVisualStyleBackColor = True
        Me.chk_VatCal.Visible = False
        '
        'lblSupplier
        '
        Me.lblSupplier.AutoSize = True
        Me.lblSupplier.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplier.ForeColor = System.Drawing.Color.Orange
        Me.lblSupplier.Location = New System.Drawing.Point(272, 37)
        Me.lblSupplier.Name = "lblSupplier"
        Me.lblSupplier.Size = New System.Drawing.Size(90, 15)
        Me.lblSupplier.TabIndex = 27
        Me.lblSupplier.Text = "Supplier Name"
        '
        'lblSupplierAddress
        '
        Me.lblSupplierAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSupplierAddress.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplierAddress.ForeColor = System.Drawing.Color.Orange
        Me.lblSupplierAddress.Location = New System.Drawing.Point(88, 56)
        Me.lblSupplierAddress.Name = "lblSupplierAddress"
        Me.lblSupplierAddress.Size = New System.Drawing.Size(549, 26)
        Me.lblSupplierAddress.TabIndex = 27
        Me.lblSupplierAddress.Text = "Supplier Address"
        '
        'lblPODate
        '
        Me.lblPODate.AutoSize = True
        Me.lblPODate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPODate.ForeColor = System.Drawing.Color.Orange
        Me.lblPODate.Location = New System.Drawing.Point(90, 36)
        Me.lblPODate.Name = "lblPODate"
        Me.lblPODate.Size = New System.Drawing.Size(87, 15)
        Me.lblPODate.TabIndex = 27
        Me.lblPODate.Text = "Received Date"
        '
        'dtpReceiveDate
        '
        Me.dtpReceiveDate.AutoSize = True
        Me.dtpReceiveDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpReceiveDate.ForeColor = System.Drawing.Color.Orange
        Me.dtpReceiveDate.Location = New System.Drawing.Point(90, 14)
        Me.dtpReceiveDate.Name = "dtpReceiveDate"
        Me.dtpReceiveDate.Size = New System.Drawing.Size(87, 15)
        Me.dtpReceiveDate.TabIndex = 27
        Me.dtpReceiveDate.Text = "Received Date"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(189, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 15)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Supp Name :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(13, 57)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 15)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Address :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 15)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "PO Date :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 15)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "MRN Date :"
        '
        'cmbPurchaseOrders
        '
        Me.cmbPurchaseOrders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPurchaseOrders.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPurchaseOrders.FormattingEnabled = True
        Me.cmbPurchaseOrders.Location = New System.Drawing.Point(275, 14)
        Me.cmbPurchaseOrders.Name = "cmbPurchaseOrders"
        Me.cmbPurchaseOrders.Size = New System.Drawing.Size(175, 20)
        Me.cmbPurchaseOrders.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(189, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 15)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Choose PO :"
        '
        'lblFormHeading
        '
        Me.lblFormHeading.AutoSize = True
        Me.lblFormHeading.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormHeading.ForeColor = System.Drawing.Color.White
        Me.lblFormHeading.Location = New System.Drawing.Point(687, 8)
        Me.lblFormHeading.Name = "lblFormHeading"
        Me.lblFormHeading.Size = New System.Drawing.Size(211, 75)
        Me.lblFormHeading.TabIndex = 6
        Me.lblFormHeading.Text = "Material Recieved" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "against" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Purchase Order"
        Me.lblFormHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(18, 491)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 15)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "MRN Remarks :"
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
        'frm_material_rec_against_PO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frm_material_rec_against_PO"
        Me.Size = New System.Drawing.Size(910, 630)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.FLXGRD_PO_NON_STOCKABLEITEMS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.FLXGRD_PO_Items, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblFormHeading As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbPurchaseOrders As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_Remarks As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents FLXGRD_PO_Items As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnIssueSlip As System.Windows.Forms.Button
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSupplier As System.Windows.Forms.Label
    Friend WithEvents dtpReceiveDate As System.Windows.Forms.Label
    Friend WithEvents lblSupplierAddress As System.Windows.Forms.Label
    Friend WithEvents lblPODate As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmb_MRNAgainst As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txt_Invoice_No As System.Windows.Forms.TextBox
    Friend WithEvents dt_Invoice_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label

    Friend WithEvents BtnRevisedMRN As System.Windows.Forms.Button
    Friend WithEvents BtnActualMRN As System.Windows.Forms.Button

    Friend WithEvents FLXGRD_PO_NON_STOCKABLEITEMS As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents lnkCalculateAmount As System.Windows.Forms.LinkLabel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents txtotherchrgs As System.Windows.Forms.TextBox
    Friend WithEvents txtdiscount As System.Windows.Forms.TextBox
    Friend WithEvents txt_Amount As System.Windows.Forms.TextBox
    Friend WithEvents lblnetamt As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents lblvatamt As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents lblgrossamt As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents chk_VatCal As System.Windows.Forms.CheckBox
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents lblexciseamt As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblMRNType As Label
    Friend WithEvents lblcustid As System.Windows.Forms.Label
End Class
